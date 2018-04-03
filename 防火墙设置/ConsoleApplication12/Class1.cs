using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
//using Microsoft.TeamFoundation.Build.Common;

namespace ConsoleApplication12
{
    public class Class1
    {
        //public void CloseFileWall()
        //{
        //    // 1. 判断当前系统为XP或Win7
        //    RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"Software\\Microsoft\\Windows NT\\CurrentVersion");
        //    var VersionName = rk.GetValue("ProductName").ToString();
        //    rk.Close();
        //    // 2.关闭防火墙
        //    if (VersionName.Contains("XP"))
        //    {
        //        RegistryKey rekey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\SharedAccess\\Parameters\\FirewallPolicy\\StandardProfile", true);
        //        var Enablefilewall = rekey.GetValue("EnableFirewall").ToString();
        //        if (Enablefilewall == "1")
        //        {
        //            rekey.SetValue("EnableFirewall", 0);
        //        }
        //        rekey.Close();
        //    }
        //    else
        //    {
        //        INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        //        // 禁用<高级安全Windows防火墙> - 专有配置文件的防火墙
        //        firewallPolicy.set_FirewallEnabled(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE, false);
        //        // 禁用<高级安全Windows防火墙> - 公用配置文件的防火墙
        //        firewallPolicy.set_FirewallEnabled(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC, false);
        //    }
        //}
    }
}
