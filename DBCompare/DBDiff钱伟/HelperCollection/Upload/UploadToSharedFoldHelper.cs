using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.IO;

namespace Utility.HelperCollection.Upload
{
    public class UploadToSharedFoldHelper
    {

        public static void Upload()
        {
            string server = @"\\10.35.2.160";
            string directory = @"\\10.35.2.160\te";
            ManagementScope ms = new ManagementScope(server);
            ConnectionOptions co = new ConnectionOptions();
            co.Username = "administrator";
            co.Password = "gwgl2011";
            ms.Options = co;
            ms.Connect();
            if (ms.IsConnected)
            {
                  if (Directory.Exists(directory))
                    {
                        File.Copy(@"C:\Users\qianwei\Desktop\FrmDecIBill.cs.txt", Path.Combine(directory, "1.txt"), true);
                    }
                }

        }
        
    }
}
