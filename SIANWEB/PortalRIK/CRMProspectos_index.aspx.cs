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

namespace SIANWEB.PortalRIK
{
    public partial class CRMProspectos_index : BaseServerPage
    {
        public int Id_TU1; // Tipo Usaurio 3.- Gerente         
        public int Id_Rik; // Representante Institucional Key RIK , para recibir parametro 
        public int Id_CD;
        public string CDI_Nombre;

        public int Parametro_IdTU; // Tipo Usaurio 3.- Gerente         
        public int Parametro_IdRik; // Representante Institucional Key RIK , para recibir parametro 
        public string Parametro_Nombre; // Nombre Gerente       
        // Gerente
        public int CRM_Gerente_Id;
        public int CRM_Gerente_Rik;
        public string CRM_Gerente_Nombre;
        // Usuario 
        public int CRM_Usuario_Id;
        public int CRM_Usuario_Rik;
        public string CRM_Usuario_Nombre;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}