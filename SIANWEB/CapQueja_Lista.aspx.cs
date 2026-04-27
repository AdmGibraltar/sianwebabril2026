using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaNegocios;
using Telerik.Web.UI;
using CapaEntidad;

namespace SIANWEB
{
    public partial class CapQueja_Lista : System.Web.UI.Page
    {
        public string QuejasVariableSesionDestruir = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                    this.ValidarPermisos();
                    CargarTQuejas();
                    if (Sesion.CalendarioIni >= txtFecha1.MinDate && Sesion.CalendarioIni <= txtFecha1.MaxDate)
                    {
                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                    }
                    if (Sesion.CalendarioFin >= txtFecha2.MinDate && Sesion.CalendarioFin <= txtFecha2.MaxDate)
                    {
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;
                    }
                    CargarCentros();
                    rgQuejas.DataSource = GetList();
                    Tabla();
                }
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarTQuejas()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCatTipoQuejas_Combo", ref cmbtquejas);
                if (Lista.Count > 0)
                {
                    cmbtquejas.DataSource = Lista;
                    cmbtquejas.DataValueField = "Id";
                    cmbtquejas.DataTextField = "Descripcion";
                    cmbtquejas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Sm_cve = pagina.Clave;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Queja> GetList()
        {
            try
            {
                string correo = null;
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Queja Negocio = new CN_Queja();
                List<Queja> LstQuejas = new List<Queja>();
                correo = Sesion.U_Correo;
                int tipoqueja = 0;
                if (cmbtquejas.SelectedIndex != -1)
                    tipoqueja = cmbtquejas.SelectedIndex;
                int queja = 0;
                if (txtqueja.Text != "")
                    queja = int.Parse(txtqueja.Text);

                Negocio.ConsultaQuejas(Sesion, correo, ref LstQuejas, queja, tipoqueja, txtEmbarque.Text, txtFlete.Text,
                    this.txtFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha1.SelectedDate),
                    this.txtFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha2.SelectedDate));
                return LstQuejas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Nuevo":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Quejas('", 0, "','", 0, "','", 1, "','", 0, "')"));
                        break;
                }

            }
            catch (Exception ex)
            {
                RAM1.Alert("Error, " + ex.Message);
            }
        }

        protected void rgQuejas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgQuejas.Items[item]["Id_Emp"].Text);
                    int Id_queja = Convert.ToInt32(rgQuejas.Items[item]["Id_queja"].Text);
                    int Id_Cd = Convert.ToInt32(rgQuejas.Items[item]["Id_Cd"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Editar":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Quejas('", Id_queja, "','", Id_Cd, "','", Id_Emp, "','", 1, "')"));
                            break;
                        case "Copiar":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Quejas('", Id_queja, "','", Id_Cd, "','", Id_Emp, "','", 2, "')"));
                            break;
                    }
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                    this.rgQuejas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Tabla()
        {
            double ancho = 0;
            foreach (GridColumn gc in rgQuejas.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgQuejas.Width = Unit.Pixel(Convert.ToInt32(ancho));
        }

        protected void rgQuejas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgQuejas.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                RAM1.Alert("Error, " + ex.Message);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "QuejasVariableSesionDestruir":
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgQuejas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgQuejas.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                RAM1.Alert("Error, " + ex.Message);
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgQuejas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}   