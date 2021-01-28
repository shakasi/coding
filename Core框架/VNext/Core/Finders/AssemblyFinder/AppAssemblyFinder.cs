using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VNext.Extensions;

namespace VNext.Finders
{
    /// <summary>
    /// 应用程序目录程序集查找器
    /// </summary>
    public class AppAssemblyFinder : FinderBase<Assembly>, IAppAssemblyFinder
    {
        private readonly List<string> startsWithCollection;
        /// <summary>
        /// 初始化一个<see cref="AppAssemblyFinder"/>类型的新实例
        /// </summary>
        public AppAssemblyFinder(params string[] startsWithArray)
        {
            startsWithCollection ??= new List<string>();
            startsWithCollection.AddRange(startsWithArray?.ToList());
            startsWithCollection.AddIfNotExist(Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 重写以实现程序集的查找
        /// </summary>
        /// <returns></returns>
        protected override Assembly[] FindAllItems()
        {
            string[] filters =
           {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };

            DependencyContext depContext = DependencyContext.Default;
            if (depContext != null)
            {
                List<string> names = new List<string>();
                string[] dllNames = depContext.CompileLibraries.SelectMany(m => m.Assemblies).Distinct()
                    .Select(m => m.Replace(".dll", ""))
                    .OrderBy(m => m).ToArray();
                if (dllNames.Length > 0)
                {
                    names = (from name in dllNames
                             let i = name.LastIndexOf('/') + 1
                             select name.Substring(i, name.Length - i)).Distinct()
                        .WhereIf(name => !filters.Any(name.StartsWith), filters != null)
                        .OrderBy(m => m).ToList();
                }
                else
                {
                    foreach (CompilationLibrary library in depContext.CompileLibraries)
                    {
                        string name = library.Name;
                        if (filters != null && filters.Any(name.StartsWith))
                        {
                            continue;
                        }

                        if (!names.Contains(library.Name))
                        {
                            names.Add(library.Name);
                        }
                    }
                    names = names?.OrderBy(m => m).ToList();
                }

                names = names.WhereIf(name => startsWithCollection.Any(name.StartsWith),
                    startsWithCollection != null && startsWithCollection.Count > 0
                    )?.ToList();

                return LoadFiles(names);
            }

            //遍历文件夹的方式，用于传统.netfx
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();

            return files.Where(file => startsWithCollection.All(token => Path.GetFileName(file)?.StartsWith(token) == true))
                .Where(file => filters.All(token => Path.GetFileName(file)?.StartsWith(token) != true))
                .Select(Assembly.LoadFrom).ToArray();
        }

        private static Assembly[] LoadFiles(IEnumerable<string> files)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string file in files)
            {
                AssemblyName name = new AssemblyName(file);
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies.ToArray();
        }
    }
}