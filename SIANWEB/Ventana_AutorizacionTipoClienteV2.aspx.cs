using System;
using System.Web.Services;
using CapaNegocios;
using CapaEntidad;
using System.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace SIANWEB
{
    public partial class Ventana_AutorizacionTipoClienteV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        public class ResponseJson
        {
            public string Status { get; set; }
        }

        [WebMethod]
        public static ResponseJson LogIn(string usuario, string password)
        {
            List<int> CDIS = new List<int> { 110, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 310, 340, 350, 360, 370, 380, 390, 400, 430, 510, 620, 640 };
            var result = new ResponseJson();

            try
            {
                string connection = ConfigurationManager.AppSettings.Get("strConnection") + ";Connect Timeout=600";
                int id;
                int min;
                bool dependientes;
                var user = new Usuario
                {
                    Cu_User = usuario,
                    Cu_pass = password
                };

                new CN_Login().Login(ref user, out id, out min, out dependientes, connection);

                //datos correctos
                if (id == 1)
                {
                    //La cuenta no está bloqueada
                    if (user.Cu_Estatus)
                    {
                        if (!user.Cu_Activo)
                        {
                            result.Status = "La cuenta está inactiva";
                        }
                        else
                        {
                            Sesion session = new Sesion();
                            session = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                            int CDI = -1;
                            CDI = CDIS.Find(x => x == session.Id_Cd_Ver);

                            if (!HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "TCte_Autorizadores"].ToString().Contains("," + user.Id_U.ToString() + ",") && CDI != 0)
                            {
                                result.Status = "El usuario no cuenta con la autorización necesaria";
                            }
                            else
                            {
                                HttpContext.Current.Session["autorizoelcambiotipocliente"] = true;
                            }
                        }
                    }
                    else result.Status = "La cuenta está bloqueada";
                }
                else result.Status = "El usuario o contraseña son incorrectos";
            }
            catch { }

            return result;
        }
    }
}