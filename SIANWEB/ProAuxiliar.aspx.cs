using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Collections;

namespace SIANWEB
{
    public partial class ProAuxiliar : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                    //string str = Context.Items["href"].ToString();
                    //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!IsPostBack)
                    {

                        CargarCentros();
                        CargarRutas();
                        Inicializar();
                        ValidarPermisos();
                        CargarAuxiliares();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                    }
                }
        }

                private List<Pedido> GetList()
        {
            try
            {
                List<Pedido> List = new List<Pedido>();
                CN_CapPedido clsPedido = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Filtro_FecEntregaIni = TxtFechaEntregaInicial.SelectedDate;
                pedido.Filtro_FecEntregaFin = TxtFechaEntregaFinal.SelectedDate;

                pedido.Ruta = cmbRuta.Text;

                if (cmbAuxiliar.SelectedValue=="")
                {
                    pedido.Id_Aux = 0;
                }
                else
                {
                    pedido.Id_Aux = Convert.ToInt32(cmbAuxiliar.SelectedValue);
                }

                clsPedido.ConsultaPedidoAsig_Admin_FechaEntrega_Picking(pedido, session2.Emp_Cnx, ref List);

/*                if (CmbEstatusPedido.SelectedValue == "1")
                {
                    return List.Where(x => x.Ped_Cantidad != x.Ped_Asignado).ToList();
                }
                else if (CmbEstatusPedido.SelectedValue == "2")
                {
                    return List.Where(x => x.Ped_Cantidad == x.Ped_Asignado).ToList();
                }
                else
                {*/
                    return List;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAsignacion.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                    GridSortExpression sortExp = new GridSortExpression();
                    sortExp.FieldName = "Id_Cte";
                    sortExp.SortOrder = GridSortOrder.Ascending;
                    rgAsignacion.MasterTableView.SortExpressions.Clear();
                    rgAsignacion.MasterTableView.SortExpressions.AddSortExpression(sortExp);

                    rgAsignacion.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }


        protected void ChkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.rgAsignacion.Items.Count; x++)
            {
                (rgAsignacion.Items[x].FindControl("ChkSeleccionar") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }




        protected void rg_NeedDataSourceporproducto(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                        rgAsignacionporproducto.DataSource = GetListPorProdcto();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }




        protected void rgAsignacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                switch (e.CommandName)
                {
                    case "Editar":
                        // Id, Id_Cte, Nom_Cte, Fecha, Territorio, Credito
                        RAM1.ResponseScripts.Add("return AbrirVentana_PlaneacionReparto('" + gi.Cells[4].Text + "','" + gi.Cells[7].Text + "','" + gi.Cells[8].Text + "','" + gi.Cells[6].Text + "','0','" + gi.Cells[10].Text + "','" + gi.Cells[2].Text + "')");
                        break;
                    case "Cambiar Ruta":
                        RAM1.ResponseScripts.Add("return AbrirVentana_PlaneacionReparto_DireccionEntrega('" + gi.Cells[4].Text + "','" + gi.Cells[7].Text + "','" + gi.Cells[8].Text + "','" + gi.Cells[6].Text + "','0','" + gi.Cells[10].Text + "','" + gi.Cells[2].Text + "')");
                        break;
                    case "Delete":
                        CancelarPedido(Int32.Parse(gi.Cells[2].Text));
                        break;
                    case "Guardar":
                        string id_Ped = rgAsignacion.Items[gi.ItemIndex]["Id_Ped"].Text;
                        string ruta = ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Ruta"].FindControl("TxtRuta"))).Text;
                        string sector = ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Sector"].FindControl("TxtSector"))).Text;
                        int secuencia = Convert.ToInt32(((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text == "" ? "0" : ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text);

                        GuardarRuta(id_Ped, ruta, secuencia, sector);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        public void CancelarPedido(int id_Ped)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<PedidoDet> list = new List<PedidoDet>();
                PedidoDet det;
                //double? cancelado;
                //foreach (GridDataItem gdi in rgPedido.Items)
                //{
                //    det = new PedidoDet();
                //    det.Id_Prd = Convert.ToInt32(gdi["Id_Prd"].Text);
                //    det.Id_Ter = Convert.ToInt32(gdi["Id_Ter"].Text);
                //    cancelado = (gdi["Cant_cancelado"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Value;
                //    det.Cancelado = cancelado != null ? Convert.ToInt32(cancelado) : 0;
                //    list.Add(det);
                //}

                list = GetListDet(id_Ped);

                foreach (PedidoDet item in list)
                {
                    item.Cancelado = item.Prd_Ord;
                }

                CN_CapPedido cn_cappedido = new CN_CapPedido();

                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = id_Ped;

                int verificador = 0;

                cn_cappedido.BajaParcial(ped, list, sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        protected int? ParametroInt(string valor)
        {
            if (valor == string.Empty)
            {
                return null;
            }
            else
            {
                return Int32.Parse(valor);
            }
        }



        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        //Inicializar();
                        ImbBuscar_Click(null, null);
                        break;
                    case "ok":
                        string status = Session["Ped_Accion" + Session.SessionID].ToString();
                        if (status == "I")
                        {
                            //Imprimir();
                        }
                        else
                        {
                            //Baja();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }

        }
        protected void ImbBuscar_Click(object sender, EventArgs e)
        {


            try
            {


                rgAsignacionporproducto.Rebind();
                rgAsignacion.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarAuxiliares()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAuxiliar_Combo", ref cmbAuxiliar);
                //this.CmbOficina.Items.Remove(0);
                //cmbTerritorio.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRutas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 1, Sesion.Emp_Cnx, "spCatRutasEntregas_Combo", ref cmbRuta);
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
                    //CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    //this.CmbCentro.Visible = false;
                    //this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    //CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    //this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar()
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                //TxtFechaInicial.DbSelectedDate = session2.CalendarioIni;
                //TxtFechaFinal.DbSelectedDate = session2.CalendarioFin;
                TxtFechaEntregaInicial.DbSelectedDate = DateTime.Now;
                TxtFechaEntregaFinal.DbSelectedDate = session2.CalendarioFin;
                rgAsignacionporproducto.Rebind();
                rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }




        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgAsignacionporproducto.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg_PageIndexChangedPedido(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAsignacionporproducto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                switch (e.CommandName)
                {
                    case "Editar":
                        RAM1.ResponseScripts.Add("return AbrirVentana_ProAsignPedxPrd('" + gi.Cells[11].Text + "','" + gi.Cells[12].Text + "','" + gi.Cells[10].Text + "','" + gi.Cells[8].Text + "','" + gi.Cells[9].Text + "','" + gi.Cells[6].Text + "')");
                        this.rgAsignacionporproducto.Rebind();
                        rgAsignacion.Rebind();
                        break;
                    case "Guardar":
                        ConfirmarPedido(Convert.ToInt64(gi.Cells[11].Text), gi.Cells[10].Text, gi.Cells[8].Text, gi.Cells[9].Text, gi.Cells[6].Text);
                        this.rgAsignacionporproducto.Rebind();
                        rgAsignacion.Rebind();
                        break;
                    case "Delete":
                        CancelarPedido(Convert.ToInt64(gi.Cells[11].Text), gi.Cells[10].Text, gi.Cells[8].Text, gi.Cells[9].Text, gi.Cells[6].Text);
                        this.rgAsignacionporproducto.Rebind();
                        rgAsignacion.Rebind();
                        break;
                    case "Incompleto":
                        RAM1.ResponseScripts.Add("return AbrirVentana_Icompleto('" + gi.Cells[11].Text + "','" + gi.Cells[19].Text + "','" + gi.Cells[20].Text + "')");
                        this.rgAsignacionporproducto.Rebind();
                        rgAsignacion.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void rg_PageIndexChangedPorProducto(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgAsignacionporproducto.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }
        public void ConfirmarPedido(Int64 Id_Prd,String Ruta, String Credito, String Parcialidades, String TipoPedido)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPedido cn_cappedido = new CN_CapPedido();

                PedidoDet ped = new PedidoDet();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Prd = Id_Prd;
                ped.Ruta = Ruta;
                ped.CreditoStr = Credito;
                ped.Ped_PermiteParcialidades = Parcialidades;
                ped.TipoPedido = TipoPedido;

                int verificador = 0;

                cn_cappedido.ConfirmarPedido(ped, sesion.Emp_Cnx, ref verificador);

                if (verificador == 0)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }
                rgAsignacionporproducto.Rebind();
                rgAsignacion.Rebind();


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }

        public void CancelarPedido(Int64 Id_Prd, String Ruta, String Credito, String Parcialidades, String TipoPedido)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPedido cn_cappedido = new CN_CapPedido();

                PedidoDet ped = new PedidoDet();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Prd = Id_Prd;
                ped.Ruta = Ruta;
                ped.CreditoStr = Credito;
                ped.Ped_PermiteParcialidades = Parcialidades;
                ped.TipoPedido = TipoPedido;

                int verificador = 0;

                cn_cappedido.CancelarPedido(ped, sesion.Emp_Cnx, ref verificador);

                if (verificador == 0)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }
                rgAsignacionporproducto.Rebind();
                rgAsignacion.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }
        /*
        public void CancelarPedido(int id_Ped)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<PedidoDet> list = new List<PedidoDet>();
                PedidoDet det;
                //double? cancelado;
                //foreach (GridDataItem gdi in rgPedido.Items)
                //{
                //    det = new PedidoDet();
                //    det.Id_Prd = Convert.ToInt32(gdi["Id_Prd"].Text);
                //    det.Id_Ter = Convert.ToInt32(gdi["Id_Ter"].Text);
                //    cancelado = (gdi["Cant_cancelado"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Value;
                //    det.Cancelado = cancelado != null ? Convert.ToInt32(cancelado) : 0;
                //    list.Add(det);
                //}

                list = GetListDet(id_Ped);

                foreach (PedidoDet item in list)
                {
                    item.Cancelado = item.Prd_Ord;
                }

                CN_CapPedido cn_cappedido = new CN_CapPedido();
                
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = id_Ped;

                int verificador = 0;

                cn_cappedido.BajaParcial(ped, list, sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        protected void GuardarRuta(string id_Ped, string ruta, int secuencia, string sector)
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];

            int verificador = 0;
            CN_CapPedido pedido = new CN_CapPedido();
            pedido.AsignarRuta(Int32.Parse(id_Ped), sector, ruta, secuencia, session2.Emp_Cnx, ref verificador);

            Alerta("La Ruta, Sector y Secuencia de entrega se guardarón correctamente.");
        }



        private List<PedidoDet> GetListPorProdcto()
        {
            try
            {
                List<PedidoDet> List = new List<PedidoDet>();
                CN_CapPedido clsPedido = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Filtro_CteIni = "";
                pedido.Filtro_CteFin = "";
                //pedido.Filtro_FecIni = TxtFechaInicial.SelectedDate;
                //pedido.Filtro_FecFin = TxtFechaFinal.SelectedDate;
                pedido.Filtro_FecEntregaIni = TxtFechaEntregaInicial.SelectedDate;
                pedido.Filtro_FecEntregaFin = TxtFechaEntregaFinal.SelectedDate;
                //pedido.Filtro_PedIni = TxtPedidoInicial.Value;
                //pedido.Filtro_PedFin = TxtPedidoFinal.Value;
                pedido.Filtro_Estatus ="1";
                /*                pedido.Filtro_RutaInicial = TxtRutaInicial.Value;
                                pedido.Filtro_RutaFinal = TxtRutaFinal.Value;
                                pedido.Filtro_SectorInicial = TxtSectorInicial.Value;
                                pedido.Filtro_SectorFinal = TxtSectorFinal.Value;*/
                pedido.Filtro_Credito = 0;
                pedido.Ruta = cmbRuta.Text;
                if (cmbAuxiliar.SelectedValue == "")
                {
                    pedido.Id_Aux = 0;
                }
                else
                {
                    pedido.Id_Aux = Convert.ToInt32(cmbAuxiliar.SelectedValue);
                }

                clsPedido.ConsultaPedidoAsig_Admin_FechaEntregaPorProducto_Picking(pedido, session2.Emp_Cnx, ref List);
                return List;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private List<PedidoDet> GetListDet(int id_Ped)
        {
            try
            {
                List<PedidoDet> List = new List<PedidoDet>();
                CN_CapPedido cn_CapPedido = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Id_Ped = id_Ped;

                cn_CapPedido.ConsultaPedidoAsig_Picking(pedido, session2.Emp_Cnx, ref List);
                return List;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

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
    }
}