using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SIANWEB.WebAPI.Security;
using System.Web.Routing;
using System.Web.Http;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SIANWEB
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciarse la aplicación

            //Register Syncfusion license            
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqXE1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfRFxhSXtWdUBlX3hWcg==;Mgo+DSMBPh8sVXJ1S0R+VVpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jT39RdkBmWnpYdn1XRA==;ORg4AjUWIQA/Gnt2VFhiQlhPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXhSdEVmXHxbdnNVRWI=;MjM4NjgzMEAzMjMxMmUzMDJlMzBjL3hoQmR5cVp5bi9QbmszUjhNblpCM0dVeC9yejBlbWJDYldVdHp3QVpvPQ==;MjM4NjgzMUAzMjMxMmUzMDJlMzBUallxb0hDMHRqS3Q1TldYVTRKWW9VUmhHTklLRkpmY09XR1VBVkhRNWpFPQ==;NRAiBiAaIQQuGjN/V0d+Xk9NfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Vd0djXHpddXJSQWNe;MjM4NjgzM0AzMjMxMmUzMDJlMzBYdDRHSXdLMWpHN3o1azNXLzJUWEl2bndrb3Q2cXpiZnBpMzJ4dHlYY3dBPQ==;MjM4NjgzNEAzMjMxMmUzMDJlMzBlcXMrVVdWbnMzZXVIWlhsWjUxNzV5SHFuczhFaWhNT29TY3NjM1FvcmE0PQ==;Mgo+DSMBMAY9C3t2VFhiQlhPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXhSdEVmXHxbdnxRRGI=;MjM4NjgzNkAzMjMxMmUzMDJlMzBEa1psZE8xZXZwS0plc3JFWG1vUGlBUG9GK1NNZ05hSjk5cHlYUVZhb01VPQ==;MjM4NjgzN0AzMjMxMmUzMDJlMzBWN2kxbWd5cUpQRTI3eG5kUi8weWZ5VDhmc3FWN2dDOStvTldxaGUvZWN3PQ==;MjM4NjgzOEAzMjMxMmUzMDJlMzBYdDRHSXdLMWpHN3o1azNXLzJUWEl2bndrb3Q2cXpiZnBpMzJ4dHlYY3dBPQ==");

            // Configurar Web API
            ConfigureWebApi(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.Filters.Add(new SIANAuthorizationAttribute());
            //var route = RouteTable.Routes.MapHttpRoute("SIANWEBApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            //route.RouteHandler = new SIANWEB.WebAPI.Configuration.SIANHttpControllerRouteHandler();
            CheckDependencies();

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            // 1. Routing convencional con action (IMPORTANTE: debe ir primero para permitir métodos específicos)
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 2. Routing convencional sin action (fallback)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 3. Configurar el RouteHandler personalizado
            var routeWithAction = RouteTable.Routes["DefaultApiWithAction"] as Route;
            if (routeWithAction != null)
            {
                routeWithAction.RouteHandler = new SIANWEB.WebAPI.Configuration.SIANHttpControllerRouteHandler();
            }

            var routeDefault = RouteTable.Routes["DefaultApi"] as Route;
            if (routeDefault != null)
            {
                routeDefault.RouteHandler = new SIANWEB.WebAPI.Configuration.SIANHttpControllerRouteHandler();
            }
        }

        protected void CheckDependencies()
        {
            IAssembliesResolver assembliesResolver = GlobalConfiguration.Configuration.Services.GetAssembliesResolver();

            ICollection<Assembly> assemblies = assembliesResolver.GetAssemblies();

            StringBuilder errorsBuilder = new StringBuilder();

            foreach (Assembly assembly in assemblies)
            {
                Type[] exportedTypes = null;
                if (assembly == null || assembly.IsDynamic)
                {
                    // can't call GetExportedTypes on a dynamic assembly
                    continue;
                }

                try
                {
                    exportedTypes = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    exportedTypes = ex.Types;
                }
                catch (Exception ex)
                {
                    errorsBuilder.AppendLine(ex.ToString());
                }
            }

            if (errorsBuilder.Length > 0)
            {
                //Log errors into Event Log
                Trace.TraceError(errorsBuilder.ToString());
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Código que se ejecuta al producirse un error no controlado
            Trace.TraceError(e.ToString());
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando se inicia una nueva sesión

        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando finaliza una sesión.
            // Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
            // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
            // o SQLServer, el evento no se genera.

        }

    }
}