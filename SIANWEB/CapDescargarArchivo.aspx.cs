using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CapDescargarArchivo : System.Web.UI.Page
    {
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Documento Doc = new Documento();
            //int Id_Emp = Convert.ToInt16(Request.QueryString["Id_Emp"]);
            //int Id_Cd = Convert.ToInt16(Request.QueryString["Id_Cd"]);
            Doc.Id_Doc = Convert.ToInt16(Request.QueryString["Id_Doc"]);

            CN_Documentos Negocio = new CN_Documentos();
            Negocio.ConsultaDocumento(sesion, ref Doc);
            Response.AddHeader("Content-type", Doc.Formato);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Doc.Doc_Nombre);
            Response.BinaryWrite(Doc.Archivo);
            Response.Flush();
            Response.End();

        }
    }
}