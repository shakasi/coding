using ESTM.Infrastructure.MEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace ESTM.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            try
            {
                //注册MEF
                Regisgter.regisgter().ComposeParts(this);
            }
            catch(Exception e)
            { 
                
            }
        }
    }
}