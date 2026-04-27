using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB
{

    public partial class ProPedidoVIRastreo : System.Web.UI.Page
    {
        #region variables
        public List<PedidoVtaInst> ListPedidoVtaInst
        {
            get { return (List<PedidoVtaInst>)Session[Session.SessionID + "ListPedidoVtaInst"]; }
            set { Session[Session.SessionID + "ListPedidoVtaInst"] = value; }
        }
        public IList<Pedido_Internet> ListPedidoInternet
        {
            get { return (IList<Pedido_Internet>)Session[Session.SessionID + "ListPedidoInternet"]; }
            set { Session[Session.SessionID + "ListPedidoInternet"] = value; }
        }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion

        #region eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idP"] != null)
                {
                    cargarrastreo(Request.QueryString["idP"].ToString());
                }
            }
        }


        protected void btnReporte_Click(object sender, EventArgs e)
        {
            
            CN_CapPedidoVtaInst pedidoVta = new CN_CapPedidoVtaInst();
            CN_HistorialPedido HistorialRastreo = new CN_HistorialPedido();

            List<PedidoVtaInst> List = new List<PedidoVtaInst>();
            PedidoVtaInst pedido = new PedidoVtaInst();
            pedido.Id_Emp = session.Id_Emp;
            pedido.Id_Cd = session.Id_Cd;
            pedido.Id_Ped = int.Parse(Request.QueryString["idP"].ToString());

            pedidoVta.ConsultaPedidoRastreo(pedido, session.Emp_Cnx, ref  List);

            if (List.Count > 0)
            {
                HistorialPedidos histPedi = new HistorialPedidos();
                List<HistorialPedidos> ListHistorial = new List<HistorialPedidos>();

                string FechaInicial  = List.First().Ped_Fecha.AddDays(-7).ToString();
                string FechaFinal = List.First().Ped_Fecha.AddDays(7).ToString();
                string strPedido = Request.QueryString["idP"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirhReporteGeneral", "AbrirReporteGeneral('" + strPedido + "', '" + FechaInicial + "' , '" + FechaFinal + "')", true);
            }
            
        }


        #endregion


        #region metodos

        private void cargarrastreo(string idPed)
        {
            lblPedido.Text = idPed;
            CN_CapPedidoVtaInst pedidoVta = new CN_CapPedidoVtaInst();
            CN_HistorialPedido HistorialRastreo = new CN_HistorialPedido();

            List<PedidoVtaInst> List = new List<PedidoVtaInst>();
            PedidoVtaInst pedido = new PedidoVtaInst();
            pedido.Id_Emp = session.Id_Emp;
            pedido.Id_Cd = session.Id_Cd;
            pedido.Id_Ped = int.Parse(idPed);

            pedidoVta.ConsultaPedidoRastreo(pedido, session.Emp_Cnx, ref  List);

            if (List.Count > 0)
            {
                HistorialPedidos histPedi = new HistorialPedidos();
                List<HistorialPedidos> ListHistorial = new List<HistorialPedidos>();

                histPedi.Filtro_FechaInicial = List.First().Ped_Fecha.AddDays(-7).ToString();
                histPedi.Filtro_FechaFinal = List.First().Ped_Fecha.AddDays(7).ToString();
                histPedi.Filtro_strPedido = idPed;
                histPedi.Filtro_Estatus = "1";

                HistorialRastreo.RastreoPedido_Captacion(histPedi, session.Emp_Cnx, ref  ListHistorial);

                //Captados
                var captado = (from tlist in ListHistorial
                               where tlist.Orden == 1
                               select tlist).ToList();



                //Asignado
                var Asignado = (from tlist in ListHistorial
                                where tlist.Orden == 2
                                select tlist).ToList();

                if (Asignado.Where(x => x.porcentaje == 1).ToList().Count() == Asignado.Count)
                {
                    lblAsignado.Text = "PC";
                    lblAsignado.ToolTip = "Pedido Completo";
                }
                else
                {
                    lblAsignado.Text = "PI";
                    lblAsignado.ToolTip = "Pedido Incompleto";
                }



                //Facturado
                var Facturado = (from tlist in ListHistorial
                                 where tlist.Orden == 3
                                 select tlist).ToList();


                if (Facturado.Where(x => x.porcentaje == 1).ToList().Count() == Facturado.Count)
                {
                    lblfacturacion.Text = "PC";
                    lblfacturacion.ToolTip = "Pedido Completo";
                }
                else
                {
                    lblfacturacion.Text = "PI";
                    lblfacturacion.ToolTip = "Pedido Incompleto";
                }

                //Embarque
                var Embarque = (from tlist in ListHistorial
                                where tlist.Orden == 4
                                select tlist).ToList();

                if (Embarque.Where(x => x.porcentaje == 1).ToList().Count() == Embarque.Count)
                {
                    lblEmbarque.Text = "PC";
                    lblEmbarque.ToolTip = "Pedido Completo";
                }
                else
                {
                    lblEmbarque.Text = "PI";
                    lblEmbarque.ToolTip = "Pedido Incompleto";
                }

                //Entregado
                var Entregado = (from tlist in ListHistorial
                                 where tlist.Orden == 5
                                 select tlist).ToList();

                if (Entregado.Where(x => x.porcentaje == 1).ToList().Count() == Entregado.Count)
                {
                    lblEntregado.Text = "PC";
                    lblEntregado.ToolTip = "Pedido Completo";
                }
                else
                {
                    lblEntregado.Text = "PI";
                    lblEntregado.ToolTip = "Pedido Incompleto";
                }

                //Cobranza
                var Cobranzas = (from tlist in ListHistorial
                                 where tlist.Orden == 6
                                 select tlist).ToList();

                if (Cobranzas.Where(x => x.porcentaje == 1).ToList().Count() == Cobranzas.Count)
                {
                    lblcobranza.Text = "PC";
                    lblcobranza.ToolTip = "Pedido Completo";
                }
                else
                {
                    lblcobranza.Text = "PI";
                    lblcobranza.ToolTip = "Pedido Incompleto";
                }


                if (Asignado.Count > 0)
                {
                    divNoCaptado.Visible = true;

                    if (Facturado.Count > 0)
                    {
                        divNoAsignado.Visible = true;

                        if (Embarque.Count > 0)
                        {
                            divNofacturacion.Visible = true;
                            if (Entregado.Count > 0)
                            {
                                divNoEmbarque.Visible = true;

                                if (Cobranzas.Count > 0)
                                {
                                    divNoEntregado.Visible = true;
                                    divCobranza.Visible = true;
                                }
                                else
                                {
                                    divEntregado.Visible = true;
                                    divNoCobranza.Visible = true;
                                }
                            }
                            else
                            {
                                divembarque.Visible = true;
                                divNoEntregado.Visible = true;
                                divNoCobranza.Visible = true;
                            }
                        }
                        else
                        {
                            divfacturacion.Visible = true;
                            divNoEmbarque.Visible = true;
                            divNoEntregado.Visible = true;
                            divNoCobranza.Visible = true;
                        }
                    }
                    else
                    {
                        divAsigando.Visible = true;
                        divNofacturacion.Visible = true;
                        divNoEmbarque.Visible = true;
                        divNoEntregado.Visible = true;
                        divNoCobranza.Visible = true;
                    } 
                }
                else
                {
                    divCaptado.Visible = true;
                    divNoAsignado.Visible = true;
                    divNoEmbarque.Visible = true;
                    divNoEntregado.Visible = true;
                    divNofacturacion.Visible = true;
                    divNoCobranza.Visible = true;
                } 
            } 
        }
        #endregion
    }
}