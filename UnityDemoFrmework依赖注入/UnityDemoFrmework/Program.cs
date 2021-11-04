using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace UnityDemoFrmework
{
    //摘自 https://www.cnblogs.com/slardar1978/p/4205394.html
    class Program
    {
        private static IUnityContainer _container = null;
        static void Main(string[] args)
        {
            try
            {
                ConfigUnity();
                IBook a = _container.Resolve<IBook>("bb");
                Console.WriteLine(a.Write());
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        static void ConfigUnity()
        {
            _container = new UnityContainer();

            _container.RegisterType<IBook, CBook>("cc");
            _container.RegisterType<IBook, BBook>("bb");

            //UnityConfigurationSection configuration = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            //configuration.Configure(_container);

            //var map = new ExeConfigurationFileMap { ExeConfigFilename = GetConfigFolderForFile("unity.config") };
            //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //var section = (UnityConfigurationSection)config.GetSection(SectionName);
            //if (section != null)
            //{
            //    IUnityContainer container = new UnityContainer();
            //    container = section.Configure(container);
            //}
        }
    }
}
