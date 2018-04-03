using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh;

namespace NetTest
{
     public  class SshTest:ITest
    {
         string host;
         string username; 
         string password; 
         string commandText; 
         string pkFile;
         public SshTest(string p_host, string p_username, string p_password, string p_commandText, string p_pkFile)
         {
             this.host = p_host;
             this.username = p_username;
             this.password = p_password;
             this.commandText = p_commandText;
             this.pkFile = p_pkFile;
         }
         public bool Test()
         {
             bool result = false;
             SshExec exec = new SshExec(host, username);
            // SshShell exec = new SshShell(host, username);
             try
             {
                 exec.Password = password;
                 if (!string.IsNullOrEmpty(pkFile))
                 {
                     exec.AddIdentityFile(pkFile);
                 }
                 exec.Connect();
                 string output = exec.RunCommand(commandText);
                 Console.WriteLine(output);
                 result = true;
             }
             catch (Exception)
             {
                 result = false;
             }
             finally
             {
                 try
                 {
                     if (exec != null && exec.Connected)
                     {
                         exec.Close();
                     }
                 }
                 catch
                 { 
                 
                 }  
             }
             return result;
         }
    }
}
