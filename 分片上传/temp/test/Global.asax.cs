using Ivony.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace BigFileUploader
{
  public class Global : System.Web.HttpApplication
  {


    public ConfigurationObject Configuration { get; private set; }

    protected void Application_Start( object sender, EventArgs e )
    {

      GlobalConfiguration.Configuration.Formatters.Remove( GlobalConfiguration.Configuration.Formatters.XmlFormatter );
      GlobalConfiguration.Configuration.EnableCors();


      Configuration = Ivony.Configurations.ConfigurationManager.GetConfiguration<Global>();

      RouteTable.Routes.MapHttpRoute( "Create", "", new { controller = "FileUpload", action = "Create" } );
      RouteTable.Routes.MapHttpRoute( "Task", "{token}", new { controller = "FileUpload", action = "Task" } );
      RouteTable.Routes.MapHttpRoute( "Upload", "{token}/{blockIndex}", new { controller = "FileUpload", action = "Upload" } );





    }

  }
}