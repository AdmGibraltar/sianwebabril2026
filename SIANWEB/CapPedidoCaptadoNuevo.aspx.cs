using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;
using System.Configuration;
namespace SIANWEB
{
    public partial class CapPedidoCaptadoNuevo : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    cargarComboClientesAcuerdo();
                    Session["PedidoCaptado" + Session.SessionID] = null;
                    CN_CapFactura fac = new CN_CapFactura();
                    string[] datosGen = fac.ConsultaFacturacion_DatosGeneralesFacturacion(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver);
                }
            }
            catch (Exception ex)
            {
                this.mensajes(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos


        protected void btnconusltar_Click(object sender, EventArgs e)
        {
            try
            {
                string permiso = string.Concat(ConfigurationManager.AppSettings["RequiereAutorizacionPedido"].ToString());

                string _PermisoGuardar = Request.QueryString["PermisoGuardar"].ToString();
                string _PermisoModificar = Request.QueryString["PermisoModificar"].ToString();

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = int.Parse(ddlRazonClienteNva.SelectedItem.Value.ToString());

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                vtaInst.ConsultaClienteAcys(pedido, ref lista, sesion.Emp_Cnx);
                if ((lista.Count != 0) && (ConfigurationManager.AppSettings["ValidaAcys"].ToString() != "N"))//pedido no existe
                {
                    var acuerdo = "El cliente ya cuenta con un acuerdo. </br> Numero de acuerdo: " + lista.First().Id_Acs;
                    mensajes(acuerdo);
                }
                else
                {
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = int.Parse(ddlRazonClienteNva.SelectedItem.Value.ToString());
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    if (cliente != null)
                    {
                        bool CreditoSusp = Convert.ToBoolean(cliente.Cte_CreditoSuspendido);

                        if (CreditoSusp && permiso.ToUpper() == "N")
                        {

                            mensajes("Este cliente tiene el crédito suspendido");
                            return;
                        }
                        else
                        {
                            Session["PedidoCaptado" + Session.SessionID] = int.Parse(ddlRazonClienteNva.SelectedItem.Value.ToString());
                            string mensaje = string.Empty;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "close('" + ddlRazonClienteNva.SelectedItem.Value.ToString() + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')", true);

                        }
                    }
                    else
                    {
                        mensajes("Error al consultar el Cliente.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                mensajes(ex.Message.ToString());
            }
        }
        #endregion

        #region Funciones

        private void cargarComboClientesAcuerdo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            PedidoVtaInst pedido = new PedidoVtaInst();
            List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            pedido.Id_Emp = sesion.Id_Emp;
            pedido.Id_Cd = sesion.Id_Cd_Ver;
            CN_CapPedido cn_capPedido = new CN_CapPedido();
            vtaInst.ConsultaClienteAcysCombo(pedido, ref lista, sesion.Emp_Cnx);

            var query = (from tlist in lista
                         group tlist by tlist.Id_Cte into g
                         select new
                         {
                             Id_Cte = g.Key,
                             Acs_NomComercial = g.Key + " - " + g.Select(x => x.Acs_NomComercial).FirstOrDefault()
                         }).ToList().OrderBy(x => x.Id_Cte);

            ddlRazonClienteNva.DataSource = query;
            ddlRazonClienteNva.ValueField = "Id_Cte";
            ddlRazonClienteNva.TextField = "Acs_NomComercial";
            ddlRazonClienteNva.DataBind();

        }

        #endregion

        #region mensajes

        private void mensajes(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "modalMensaje('" + mensaje + "')", true);
        }
        #endregion
    }
}