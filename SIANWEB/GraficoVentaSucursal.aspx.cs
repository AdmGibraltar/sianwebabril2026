using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Threading;
using System.IO;
using System.Text;

namespace SIANWEB
{
    public partial class GraficoVentaSucursal : System.Web.UI.Page
    {
        public string strdataset1 = "";
        public string strdataset2 = "";
        public string strdataset3 = "";
        public string strlabel1 = "";
        public string strlabel2 = "";
        public string strlabel3 = "";
        public string strEtiquetas = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.rgVentas.DataSource = GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
        }

        #region Eventos

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                //  comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

                //if (!_PermisoImprimir)
                //{
                //    this.Alerta("No tiene permisos para ver el reporte");
                //    return;
                //}
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                //if (Page.IsValid)
                //{
                //    if (btn.CommandName == "print")
                //    {
                //        Imprimir();
                //    }

                //}
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }

        protected void rgVentas_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgVentas.DataSource = GetList();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgVentas_NeedDataSource"));
            }
        }

        #endregion

        #region Funciones

        private List<Embudo> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas clsCatCriterioCitas = new CN_CatCriterioCitas();

                List<Embudo> listElEmbudo = new List<Embudo>();
                Embudo ElEmbudo = new Embudo();
                ElEmbudo.Id_Cd = sesion.Id_Cd;
                ElEmbudo.Id_Emp = sesion.Id_Emp;
                clsCatCriterioCitas.ConsultaVISucursal(ElEmbudo, sesion.Emp_Cnx, ref listElEmbudo);
                LlenaCadenas(listElEmbudo);
                return listElEmbudo;
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        private void LlenaCadenas(List<Embudo> lista)
        {
            try
            {
                strdataset1 = "";
                strdataset2 = "";
                strdataset3 = "";
                strlabel1 = "";
                strlabel2 = "";
                strlabel3 = "";
                foreach (Embudo elem in lista)
                {
                    strlabel1 = "'Vta Instalada'";
                    strlabel2 = "'Vta Promedio'";
                    strlabel3 = "'Vta Real'";
                    strdataset1 = elem.VtaIn.ToString();
                    strdataset2 = elem.VtaPro.ToString();
                    strdataset3 = elem.VtaRe.ToString();
                    strEtiquetas = "'" + elem.RowDesc + "'";
                    
                    //strdataset1 = strdataset1 + ", " + elem.VtaIn.ToString();
                    //strdataset2 = strdataset2 + ", " + elem.VtaPro.ToString();
                    //strdataset3 = strdataset3 + ", " + elem.VtaRe.ToString();
                    //  strEtiquetas = strEtiquetas + "'" + elem.RowDesc + "', ";
                    //strdataset1 = strdataset1 + ", " + elem.VtaIn.ToString();
                    //strdataset2 = strdataset2 + ", " + elem.VtaPro.ToString();
                    //strdataset3 = strdataset3 + ", " + elem.VtaRe.ToString();
                    //  strEtiquetas = strEtiquetas + "'" + elem.RowDesc + "'";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion
    }
}