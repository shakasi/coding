using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.Web.BLL
{
    public class CreatePowerManagerService
    {
        private static ServiceReference_PowerManager.PowerManageWCFServiceClient oPowerManagerClient = null;
        private static object obj = new object();

        public static ServiceReference_PowerManager.PowerManageWCFServiceClient GetInstance()
        {
            lock (obj)
            {
                if (oPowerManagerClient == null)
                {
                    oPowerManagerClient = new ServiceReference_PowerManager.PowerManageWCFServiceClient();
                }
            }
            return oPowerManagerClient;
        }
    }
}
