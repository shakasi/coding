using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Finders
{
    /// <summary>
    /// 应用程序目录程序集查找器
    /// </summary>
    public class DependencyAssemblyFinder : FinderBase<Assembly>, IAssemblyFinder
    {
        private readonly DependencyContext depContext;
        private readonly Func<string, bool> filter;

        /// <summary>
        /// 初始化一个<see cref="AppAssemblyFinder"/>类型的新实例
        /// </summary>
        public DependencyAssemblyFinder(DependencyContext depContext, Func<string, bool> filter)
        {
            this.depContext = depContext;
            this.filter = filter;
        }

        /// <summary>
        /// 重写以实现程序集的查找
        /// </summary>
        /// <returns></returns>
        protected override Assembly[] FindAllItems()
        {
            Check.NotNull(depContext, nameof(depContext));

            List<string> names = new List<string>();
            string[] dllNames = depContext.CompileLibraries.SelectMany(m => m.Assemblies).Distinct()
                .Select(m => m.Replace(".dll", ""))
                .OrderBy(m => m).ToArray();

            if (dllNames.Length > 0)
            {
                names = (from name in dllNames
                         let i = name.LastIndexOf('/') + 1
                         select name.Substring(i, name.Length - i)).Distinct()
                    .WhereIf(name => filter.Invoke(name), filter != null)
                    .OrderBy(m => m).ToList();
            }
            else
            {
                foreach (var lib in depContext.CompileLibraries)
                {
                    string name = lib.Name;
                    if (filter == null || !filter.Invoke(name))
                    {
                        continue;
                    }

                    if (!names.Contains(name))
                    {
                        names.Add(name);
                    }
                }
            }
            return names.Select(Assembly.LoadFrom).Distinct().ToArray();
        }
    }
}