using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Collections;
using System.Text;

namespace SIANWEB
{
    public partial class ProPlaneacionRepartoDireccionEntregaRuta : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inicializar();
            }
        }



        //protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RebindReason == GridRebindReason.ExplicitRebind)
        //        {
        //            rgAsignacion.DataSource = GetList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "RadGrid1_NeedDataSource");
        //    }
        //}



        protected void rgAsignacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                switch (e.CommandName)
                {
                    case "Editar":
                        RAM1.ResponseScripts.Add("return AbrirVentana_PlaneacionReparto('" + gi.Cells[2].Text + "','" + gi.Cells[5].Text + "','" + gi.Cells[6].Text + "')");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    //Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }




        private void Inicializar()
        {
            try
            {


                CargarRutasEntrega();
                HF_Ped.Value = Request.QueryString["Id"].ToString();

                txtPedido.Text = Request.QueryString["Id"].ToString();
                txtCliente.Text = Request.QueryString["Id_Cte"].ToString();
                txtClienteNombre.Text = Request.QueryString["Nom_Cte"].ToString();
                txtFecha.Text = Request.QueryString["Fecha"].ToString();
                txtCredito.Text = Request.QueryString["Credito"].ToString();

                //RadTextBoxRuta.Text = Request.QueryString["Ruta"].ToString();

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                //TxtFechaInicial.DbSelectedDate = session2.CalendarioIni;
                //TxtFechaFinal.DbSelectedDate = session2.CalendarioFin;


                Pedido List = new Pedido();
                CN_CapPedido cn_CapPedido = new CN_CapPedido();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(HF_Ped.Value);

                cn_CapPedido.ConsultaPedidoDireccionEntrega(pedido, session2.Emp_Cnx, ref List);


                txtEcalle.Text = List.Ped_ConsignadoCalle;
                txtEnumero.Text = List.Ped_ConsignadoNo;
                txtEcp.Text = List.Ped_ConsignadoCp;
                txtEcolonia.Text = List.Ped_ConsignadoColonia;
                txtEmunicipio.Text = List.Ped_ConsignadoMunicipio;
                txtEestado.Text = List.Ped_ConsignadoEstado;
                txtEtelefono.Text = List.acs_telefono2;
                cmbRutaG.SelectedIndex = List.Id_Rut;
                cmbRutaG.Text = List.Ruta;



                ValidarPermisos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }
        private void CargarRutasEntrega()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 1, Sesion.Emp_Cnx, "spCatRutas_Combo", ref cmbRutaG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Click_BtnAgregarDirEntrega(object sender, EventArgs e)
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];

            if (cmbRutaG.SelectedValue != "-1")
            {

                Pedido List = new Pedido();
                CN_CapPedido cn_CapPedido = new CN_CapPedido();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);
                pedido.Id_Cte = Convert.ToInt32(txtCliente.Text);
                pedido.Id_Rut = Convert.ToInt32(cmbRutaG.SelectedValue);

                cn_CapPedido.ActualizaPedidoDireccionEntrega(pedido, session2.Emp_Cnx, ref List);
                Alerta("Ruta asignada correctamente");
                return;
            }


        }
        private void ValidarPermisos()
        {
            try
            {
                //if (_PermisoGuardar == false & _PermisoModificar == false)
                //    this.rtb1.Items[5].Visible = false;
                ////Nuevo
                //this.rtb1.Items[6].Visible = false;
                ////Regresar
                //this.rtb1.Items[4].Visible = false;
                ////Eliminar
                //this.rtb1.Items[3].Visible = false;
                ////Imprimir
                //this.rtb1.Items[2].Visible = false;
                ////Correo
                //this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PedidoDet> GetList()
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
                pedido.Id_Ped = Convert.ToInt32(HF_Ped.Value);

                cn_CapPedido.ConsultaPedidoAsig_Picking_Pedido(pedido, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void AlertaCerrar(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("CloseAlert('" + mensaje + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
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
        
    }
}