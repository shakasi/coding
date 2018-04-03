using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SKY.Utils
{
    public class DllInvoke
    {
        #region 动态API
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(String path);
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, String funcName);
        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);
        private IntPtr hLib;

        public DllInvoke(String DLLPath)
        {
            try
            {
                hLib = LoadLibrary(DLLPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~DllInvoke()
        {
            FreeLibrary(hLib);
        }

        public void DisposeOperate()
        {
            FreeLibrary(hLib);
        }

        public Delegate Invoke(String APIName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, APIName);
            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
        }
        #endregion
    }
}
