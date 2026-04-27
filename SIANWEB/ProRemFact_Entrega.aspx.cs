using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using SIANWEB.API;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SIANWEB
{
    public partial class ProRemFact_Entrega : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); }
                CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgRemisiones.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgRemisiones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Autorizar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        var TipoDoc = rgRemisiones.Items[item]["TipoDoc"].Text;
                        if (TipoDoc == "Factura")
                        {
                            SetDataFactura(Convert.ToInt32(rgRemisiones.Items[item]["Id_Doc"].Text), Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text), Convert.ToInt32(rgRemisiones.Items[item]["Pedido"].Text), true);
                        }
                        else
                        {
                            setDataRemision(Convert.ToInt32(rgRemisiones.Items[item]["Id_Doc"].Text), Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text), Convert.ToInt32(rgRemisiones.Items[item]["Pedido"].Text), true);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void setDataRemision(int Id_Doc, int Num_Cliente, int Pedido, bool returnMsg)
        {
            CN__Comun.RemoverValidadores(Validators);
            RemisionesEntrega remisiones = new RemisionesEntrega();
            remisiones.Id_Rem = Id_Doc;
            remisiones.Num_Cliente = Num_Cliente;
            remisiones.Pedido = Pedido;


            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            Factura factura = new Factura();
            Remision Remision = new Remision();


            factura.Id_Emp = session.Id_Emp;
            factura.Id_Cd = session.Id_Cd_Ver;
            factura.Id_Fac = Id_Doc;
            try
            {
                factura.Fac_PedNum = Pedido;
            }
            catch
            {
                factura.Fac_PedNum = 0;
            }
            factura.Id_U = session.Id_U;
            factura.Fac_Fecha = DateTime.Now;


            List<Factura> lista = new List<Factura>();

            CN_Eccommerce Eccommerce = new CN_Eccommerce();
            Eccommerce.ValidarPedidoFacturaEcommerce(factura, session.Emp_Cnx, ref lista);
            string estatus = "";
            string mensaje = "";



            Remision.Id_Emp = session.Id_Emp;
            Remision.Id_Cd = session.Id_Cd_Ver;
            Remision.Id_Rem = Id_Doc;
            try
            {
                Remision.Fac_PedNum = Pedido;
            }
            catch
            {
                Remision.Fac_PedNum = 0;
            }
            Remision.Id_U = session.Id_U;
            Remision.Fac_Fecha = DateTime.Now;


            /*if (lista.Count > 0)
            {
                Remision.id_pedMag = Convert.ToInt32(lista.First().Id_Ped.ToString());

                APISKEY APis = new APISKEY();
                APis.entregaRemision(Remision, session.Emp_Cnx, ref estatus, ref mensaje);


                if (estatus == "2" || estatus == "0")
                {
                    this.Alerta("Ocurrió un problema al cambiar el estatus de la factura. " + mensaje);
                    return;
                }
                else
                {
                    EnviarCorreo(Num_Cliente, Convert.ToInt32(lista.First().id_pedMag.ToString()), session.Emp_Cnx);

                }
            }*/

            Autorizar(remisiones, returnMsg);
        }

        private void SetDataFactura(int Id_Doc, int Num_Cliente, int Pedido, bool returnMsg)
        {
            FacturaEntrega facturas = new FacturaEntrega();
            facturas.Id_Fac = Id_Doc;//Convert.ToInt32(rgRemisiones.Items[item]["Id_Doc"].Text);
            facturas.Num_Cliente = Num_Cliente;// Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text);
            facturas.Pedido = Pedido;// Convert.ToInt32(rgRemisiones.Items[item]["Pedido"].Text);
            facturas.Numero = Id_Doc.ToString();// rgRemisiones.Items[item]["Id_Doc"].Text;

            AutorizarFactura(facturas, returnMsg);
        }

        private void AutorizarFactura(FacturaEntrega facturas, bool returnMsg = true)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CD_FacturasEntrega clsFactura = new CD_FacturasEntrega();
                int verificador = -1;

                Factura factura = new Factura();

                factura.Id_Emp = session.Id_Emp;
                factura.Id_Cd = session.Id_Cd_Ver;
                factura.Id_Fac = facturas.Id_Fac;
                try
                {
                    factura.Fac_PedNum = facturas.Pedido;
                }
                catch
                {
                    factura.Fac_PedNum = 0;
                }
                factura.Id_U = session.Id_U;
                factura.Fac_Fecha = DateTime.Now;


                List<Factura> lista = new List<Factura>();

                CN_Eccommerce Eccommerce = new CN_Eccommerce();
                Eccommerce.ValidarPedidoFacturaEcommerce(factura, session.Emp_Cnx, ref lista);
                string estatus = "";
                string mensaje = "";
                /*if (lista.Count > 0)
                 {
                     factura.id_pedMag = Convert.ToInt32(lista.First().Id_Ped.ToString());
                     APISKEY APis = new APISKEY();
                     APis.entregaPedido(factura, session.Emp_Cnx, ref estatus, ref mensaje);

                     if (estatus == "2" || estatus == "0")
                     {
                         this.Alerta("Error: al Estatus de Entrega de Pedido Portal Key: " + mensaje);
                         //return "Error: al Estatus de Entrega de Pedido Portal Key: " + mensaje;
                     }
                     else
                     {
                         EnviarCorreo(facturas.Num_Cliente, Convert.ToInt32(lista.First().id_pedMag.ToString()), session.Emp_Cnx);
                     }
                 }*/

                clsFactura.ModificarProFacturaEntrega(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, facturas, session.Emp_Cnx, ref verificador);
                if (returnMsg)
                {
                    if (verificador == 1)
                        Alerta("La factura <b>#" + facturas.Numero + "</b> fue entregada correctamente");
                    else
                        Alerta("No se pudo autorizar la factura #" + facturas.Numero);
                    rgRemisiones.Rebind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void rgRemisiones_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgRemisiones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (dpFecha1.SelectedDate > dpFecha2.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            if (txtCliente.Value > txtCliente2.Value)
            {
                Alerta("El número de cliente inicial no debe ser mayor al número de cliente final");
                return;
            }
            try
            {
                this.rgRemisiones.Rebind();
                this.rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRemisiones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                var tipoDocumento = item["TipoDoc"].Text;
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Doc").ToString()).Replace("[[TIPODOC]]", tipoDocumento);
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
                //{
                //    dpFecha1.DbSelectedDate = sesion.CalendarioIni;
                //}
                //if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
                //{
                //    dpFecha2.DbSelectedDate = sesion.CalendarioFin;
                //}
                rgRemisiones.Rebind();
                rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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
        private void Nuevo()
        {
            try
            {
                txtCliente.Text = string.Empty;
                txtNombre.Text = string.Empty;
                dpFecha1.DateInput.Text = string.Empty;
                dpFecha2.DateInput.Text = string.Empty;
                dpFecha1.Clear();
                dpFecha2.Clear();
                HF_ID.Value = string.Empty;
                HF_PED.Value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Autorizar(RemisionesEntrega remisiones, bool returnMsg = true)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CD_RemisionesEntrega clsRemision = new CD_RemisionesEntrega();
                int verificador = -1;

                clsRemision.ModificarProRemisionEntrega(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, remisiones, session.Emp_Cnx, ref verificador);
                if (returnMsg)
                {
                    if (verificador == 1)
                        Alerta("El registro # " + remisiones.Id_Rem.ToString() + " fue entregado correctamente");
                    else
                        Alerta("No se pudo autorizar el registro #" + remisiones.Id_Rem.ToString());

                    rgRemisiones.Rebind();
                }

                //Nuevo();
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
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EmbarqueFacRem> GetList()
        {
            try
            {
                List<EmbarqueFacRem> List = new List<EmbarqueFacRem>();
                CN_EmbarqueRemFac clsEmbarquesRem = new CN_EmbarqueRemFac();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                EmbarqueFacRem remisionfiltro = new EmbarqueFacRem();

                remisionfiltro.Filtro_Nombre = txtNombre.Text;
                remisionfiltro.Filtro_Id_Cte = txtCliente.Text;
                remisionfiltro.Filtro_Id_Cte2 = txtCliente2.Text;
                remisionfiltro.Filtro_FecIni = dpFecha1.SelectedDate.HasValue ? dpFecha1.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                remisionfiltro.Filtro_FecFin = dpFecha2.SelectedDate.HasValue ? dpFecha2.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                remisionfiltro.TipoDoc = TipoDoc.Value;
                remisionfiltro.Id_Doc = txtNoDocumento.Value == "" ? 0 : int.Parse(txtNoDocumento.Value.Trim()); ;
                clsEmbarquesRem.ConsultaProRemFacEntrega(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, remisionfiltro, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EmbarqueFacRem> GetListEmbarque()
        {
            try
            {
                List<EmbarqueFacRem> List = new List<EmbarqueFacRem>();
                CN_EmbarqueRemFac clsEmbarquesRem = new CN_EmbarqueRemFac();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                EmbarqueFacRem remisionfiltro = new EmbarqueFacRem();

                remisionfiltro.Filtro_FecIni = dpFecha1.SelectedDate.HasValue ? dpFecha1.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                remisionfiltro.Filtro_FecFin = dpFecha2.SelectedDate.HasValue ? dpFecha2.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                remisionfiltro.Id_Emb = txtNoEmbarque.Value == "" ? 0 : int.Parse(txtNoEmbarque.Value.Trim());
                clsEmbarquesRem.ConsultaEmbarqueFacRem(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, remisionfiltro, ref List);
                return List;
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

        protected void rgEmbarques_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgEmbarques.DataSource = GetListEmbarque();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgEmbarques_NeedDataSource");
            }

        }

        protected void rgEmbarques_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName.ToString() == "Autorizar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN_EmbarqueRemFac clsEmbarquesRem = new CN_EmbarqueRemFac();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];

                        var Id_Emb = int.Parse(rgEmbarques.Items[item]["Id_Emb"].Text);
                        EmbarqueFacRem remisionfiltro = new EmbarqueFacRem();
                        List<EmbarqueFacRem> List = new List<EmbarqueFacRem>();

                        remisionfiltro.Id_Emb = Id_Emb;
                        clsEmbarquesRem.ConsultaProRemFacEntrega(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, remisionfiltro, ref List);


                        foreach (EmbarqueFacRem elemento in List)
                        {
                            if (elemento.TipoDoc == "Factura")
                            {

                                SetDataFactura(elemento.Id_Doc, elemento.Num_Cliente, elemento.Pedido, false);
                            }
                            else//remisiones
                            {
                                setDataRemision(elemento.Id_Doc, elemento.Num_Cliente, elemento.Pedido, false);
                            }

                        }

                        Alerta("Los documentos del embarque <b>#" + Id_Emb + "</b> fueron entregados.");
                        rgEmbarques.Rebind();
                        rgRemisiones.Rebind();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgEmbarques_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgEmbarques_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Emb").ToString());
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "1")
            {
                rgRemisiones.Visible = true;
                rgEmbarques.Visible = false;
                tr_cliente.Visible = true;
                tr_rangocliente.Visible = true;
                TipoDoc.Visible = true;
                Label1.Text = "Tipo documento";
                txtNoEmbarque.Visible = false;
                txtNoDocumento.Visible = true;
                lblNoDocumento.Visible = true;
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                rgRemisiones.Visible = false;
                rgEmbarques.Visible = true;
                tr_cliente.Visible = false;
                tr_rangocliente.Visible = false;
                TipoDoc.Visible = false;
                Label1.Text = "Número de embarque";
                txtNoEmbarque.Visible = true;
                txtNoDocumento.Visible = false;
                lblNoDocumento.Visible = false;
            }
        }
    }
}