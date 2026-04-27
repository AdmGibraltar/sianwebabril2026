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
    public partial class GraficaEmbudo : System.Web.UI.Page
    {
        public string strdataset1 = "";
        public string strdataset2 = "";
        public string strdataset3 = "";
        public string strdataset4 = "";
        public string strlabel1 = "";
        public string strlabel2 = "";
        public string strlabel3 = "";
        public string strlabel4 = "";
        public string strdataset11 = "";
        public string strdataset22 = "";
        public string strdataset33 = "";
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
                        this.rgEmbudo.DataSource = GetList();
                        strEtiquetas = "'Visita Personal', 'Llamada Telefonica', 'Correo Electronico'";
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

        protected void rgEmbudo_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgEmbudo.DataSource = GetList();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgEmbudo_NeedDataSource"));
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
                clsCatCriterioCitas.ConsultaEmbudo(ElEmbudo, sesion.Emp_Cnx, ref listElEmbudo);
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
                strdataset4 = "";
                strdataset11 = "";
                strdataset22 = "";
                strdataset33 = "";
                strdataset4 = "";
                foreach (Embudo elem in lista)
                {
                    if (elem.Ordern == 1)
                    {
                        strlabel1 = elem.RowDesc;
                        strdataset1 = elem.A + ", " + elem.B + ", " + elem.C;   // +", " + elem.D;
                        strdataset11 = elem.A.ToString();
                        strdataset22 = elem.B.ToString();
                        strdataset33 = elem.C.ToString();
                    }
                    
                    if (elem.Ordern == 3)
                    {
                        strlabel2 = elem.RowDesc;
                        strdataset2 = elem.A + ", " + elem.B + ", " + elem.C;   // + ", " + elem.D;
                        strdataset11 = strdataset11 + ", " + elem.A.ToString();
                        strdataset22 = strdataset22 + ", " + elem.B.ToString();
                        strdataset33 = strdataset33 + ", " + elem.C.ToString();
                    }

                    if (elem.Ordern == 4)
                    {
                        strlabel3 = elem.RowDesc;
                        strdataset3 = elem.A + ", " + elem.B + ", " + elem.C;   //  + ", " + elem.D;
                        strdataset11 = strdataset11 + ", " + elem.A.ToString();
                        strdataset22 = strdataset22 + ", " + elem.B.ToString();
                        strdataset33 = strdataset33 + ", " + elem.C.ToString();
                    }


                    if (elem.Ordern == 5)
                    {
                        strlabel4 = elem.RowDesc;
                        strdataset4  = elem.A + ", " + elem.B + ", " + elem.C;   //  + ", " + elem.D;
                        strdataset11 = strdataset11 + ", " + elem.A.ToString();
                        strdataset22 = strdataset22 + ", " + elem.B.ToString();
                        strdataset33 = strdataset33 + ", " + elem.C.ToString();
                    }
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