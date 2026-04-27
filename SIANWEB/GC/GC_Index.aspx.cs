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

namespace SIANWEB.GS
{
    public partial class GS_Index : BaseServerPage
    {
        public int Usuario_Tipo;
        public int Id_TU1; // Tipo Usaurio 3.- Gerente         
        public int Id_Rik; // Representante Institucional Key RIK , para recibir parametro 
        public int Id_CD;
        public int Id_U;
        public string CDI_Nombre;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario_Tipo = session.Id_TU;
                Id_TU1 = session.Id_TU;
                Id_CD = session.Id_Cd;
                Id_Rik = session.Id_Rik;
                Id_U = session.Id_U;
                CDI_Nombre = session.Cd_Nombre;
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
}