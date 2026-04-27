using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using CapaNegocios;
using System.Text;
using SIANWEB.Core.UI;
using CapaModelo;
using System.Configuration;
namespace SIANWEB.Terr
{
    public partial class CatTerritoriosvT : BaseServerPage
    {
        // Tipo de Usuario 
        public int Usuario_Tipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario_Tipo = session.Id_TU;
            }
        }

        public Sesion session
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
    }      

    //

}