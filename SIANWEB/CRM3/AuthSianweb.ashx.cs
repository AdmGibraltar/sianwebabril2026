using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace SIANWEB.CRM3
{
    /// <summary>
    /// Summary description for AuthSianweb
    /// </summary>
    public class AuthSianweb : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var session = context.Session;

            var result = new
            {
                loggedIn = false,
                userId = (int?)null,
                userName = (string)null,
                role = (string)null,
                cd = (int?)null,
            };

            var sesion = session?["Sesion" + session.SessionID] as CapaEntidad.Sesion;
            if (sesion != null)
            {
                string role = null;
                switch (sesion.Id_TU)
                {
                    case 2: role = "rik";
                        break;
                    case 3: role = "gte";
                        break;
                }
                result = new
                {
                    loggedIn = true,
                    userId = sesion.Id_U as int?,
                    userName = sesion.U_Nombre,
                    role,
                    cd = sesion.Id_Cd as int?
                };
            }

            var serializer = new JavaScriptSerializer();
            context.Response.Write(serializer.Serialize(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}