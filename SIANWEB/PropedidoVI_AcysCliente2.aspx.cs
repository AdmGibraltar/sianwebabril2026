using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using CapaDatos;
using System.Data;
using DevExpress.Web.Bootstrap;

namespace SIANWEB
{
    public partial class PropedidoVI_AcysCliente2 : System.Web.UI.Page
    {
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

        public DataTable dt
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI"];
            }
            set
            {
                ViewState["dtPedidoVI"] = value;

            }
        }

        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI_Resto"];
            }
            set
            {
                ViewState["dtPedidoVI_Resto"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            cargarComboClientesAcuerdo();

            if (Session["acys_pemdientesLista"] != null)
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List = (List<PedidoVtaInst>)Session["acys_pemdientesLista"];

                RadGrid1.DataSource = List;
                RadGrid1.DataBind();
            }
 
        }

        protected void detailGrid_DataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["camposAcysVI"] == null)
            {

                string[] array = prod.ToString().Split('|');

                // array[0] = valor del campo id_Acs
                // array[1] = valor del campo num pedido
                // array[2] = valor del campo semana
                // array[3] = valor del campo anio

                if (array[1].ToString() != "0")
                {
                    if (Session["camposAcysVI"] == null)
                    {
                        string pedido = array[1].ToString();

                        BootstrapGridView grid = sender as BootstrapGridView;
                        GetListDet();
                        DataTable dtTemp = dt;
                        CargarPedido(Convert.ToInt32(pedido), ref dtTemp);
                        Session["camposAcysVI"] = prod;
                        grid.DataSource = dtTemp;
                        grid.DataBind();
                    }
                }
                else if (array[0].ToString() != "0")
                {
                    if (Session["camposAcysVI"] == null)
                    {
                        string IdAcs = array[0].ToString();
                        string Semana = array[2].ToString();
                        string anio = array[3].ToString();

                        BootstrapGridView grid = sender as BootstrapGridView;
                        GetListDet();
                        DataTable dtTemp = dt;
                        CargarProductoAcys(Convert.ToInt32(IdAcs), Convert.ToInt32(Semana), Convert.ToInt32(anio), ref  dtTemp);
                        Session["camposAcysVI"] = prod;
                        grid.DataSource = dtTemp;
                        grid.DataBind();
                    }
                }
            }
            else
            {
                Session["camposAcysVI"] = null;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["acys_pemdientesLista"] = null;
                cargarComboClientesAcuerdo();
            }
        }


        private void cargarComboClientesAcuerdo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion != null)
            {
                if(Request.QueryString["IdCte"] != null)
                {
                 PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                CN_CapPedido cn_capPedido = new CN_CapPedido();
                vtaInst.ConsultaClienteAcysCombo(pedido, ref lista, sesion.Emp_Cnx);

                var query = (from tlist in lista
                             where tlist.Id_Cte == Convert.ToInt32(Request.QueryString["IdCte"].ToString())
                             select new
                             {
                                 Acs_NomComercial = tlist.Id_Cte + " - " + tlist.Acs_NomComercial
                             }).FirstOrDefault();

                lblnombreCliente.Text = query.Acs_NomComercial;
               
                GetList(Request.QueryString["IdCte"].ToString());
                }
            }
        }


        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];


                GridViewDataItemTemplateContainer c = ((HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                string Id_Acs = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString();
                string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString();
                string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString();
                string Semana = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Semana").ToString();
                string anio = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Anio").ToString();

                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List = (List<PedidoVtaInst>)Session["acys_pemdientesLista"];

                PedidoVtaInst query = (from tlist in List
                                       where tlist.Id_Acs == Convert.ToInt32(Id_Acs)
                                       select tlist).FirstOrDefault();

                if (query != null)
                {
                    bool CreditoSusp = Convert.ToBoolean(query.Cte_Credito);

                    if (CreditoSusp == true)
                    {
                        mensaje("El cliente tiene crédito suspendido, favor de hacer las gestiones correspondientes para poder captar");
                    }
                    else
                    {
                        CN_CapAcys cn_capacys = new CN_CapAcys();
                        Acys acys = new Acys();
                        acys.Id_Emp = session.Id_Emp;
                        acys.Id_Cd = session.Id_Cd_Ver;
                        acys.Id_Acs = Convert.ToInt32(query.Id_Acs);
                        acys.Id_AcsVersion = 1;
                        cn_capacys.Consultar(ref acys, session.Emp_Cnx);


                        if (acys.Acs_Estatus == "B")
                        {
                            mensaje("No se puede captar el pedido, el Acuerdo esta dado de baja");

                        }
                        else
                        {
                            if (Pedido == "0")
                            {
                                CerrarVentana(Id_Acs, Semana, Pedido, anio);
                            }
                            else
                            {
                                CerrarVentana(Id_Acs, Semana, Pedido, anio);
                            }
                        }
                    }
                }
                RadGrid1.DataSource = List.ToList();
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_PrdOld", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));

                dt.Columns.Add("Mes1", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes2", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes3", System.Type.GetType("System.Double"));

                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_PrecioAcys", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_FechaF", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Mod", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Acs_Dia", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_DiaStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_Frecuencia", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_RemFact", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ped_Asignar", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_TG", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Acs", System.Type.GetType("System.Int32"));

                dt_Resto = dt.Clone();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void GetList(string cte)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID]; 
                Funciones Funcion = new Funciones();
                  List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                CN_CapPedidoVtaInst clsCatArea = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst(); 
                CN_CatSemana Semana = new CN_CatSemana();
                List<Semana> Lista = new List<CapaEntidad.Semana>();


                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver; 
                pedido.Filtro_Cte = cte;


                clsCatArea.Lista_acysPendiente(pedido, session.Emp_Cnx, ref List);


         
                Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(Funcion.GetLocalDateTime(session.Minutos).Year.ToString()), session.Emp_Cnx, ref Lista);



                var query = (from tlist in Lista
                             where tlist.Sem_FechaIni <=  DateTime.Now
                             orderby tlist.Id_Sem descending
                             select tlist

                             ).FirstOrDefault();

                if (query != null)
                {
                    List<PedidoVtaInst> query2 = (from tlist in List
                                                  where tlist.Acs_Semana <= query.Id_Sem
                                                  select tlist).ToList();


                    if (query2.Count > 0)
                    {
                        Session["acys_pemdientesLista"] = query2;
                        RadGrid1.DataSource = query2;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        Session["acys_pemdientesLista"] = List;
                        RadGrid1.DataSource = List;
                        RadGrid1.DataBind();
                    }

                }
                else
                {
                    Session["acys_pemdientesLista"] = List;
                    RadGrid1.DataSource = List;
                    RadGrid1.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentana(string Id_Acs, string Semana, string Pedido, string anio)
        {
            try
            {
                string funcion = "closeAcysCliente('" + Id_Acs.ToString() + "',  '" + Semana + "'  , '" + Pedido + "' , '" + anio + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarPedido(int idPedido, ref DataTable dtTemp)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = idPedido;
                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedido(ref pedido, session.Emp_Cnx);

                dtTemp = dt;
                DataTable dtRestos = dt_Resto;

                cn_capPedido.ConsultaCaptacionPedidoDet(pedido, ref dtTemp, ref dtRestos, session.Emp_Cnx);

                dt = dtTemp;
                dt_Resto = dtRestos;

                dtTemp.Merge(dt_Resto);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarProductoAcys(int IdAcs, int Semana, int anio, ref DataTable dtTemp)
        {
            try
            {
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = IdAcs;
                pedido.Acs_Anio = anio;
                pedido.Acs_Semana = Semana;
                dt = null;

                GetListDet();

                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                string idTGStr = "";
                int? idTGNullable = 0;
                int idTG = 0;
                if (idTGStr != null)
                {
                    if (int.TryParse(idTGStr, out idTG))
                    {
                        idTGNullable = idTG;
                    }
                }
                cn_capPedidoVI.ConsultarDet(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                DataTable dtTemp_Resto = this.dt_Resto;
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, idTG);

                dtTemp.Merge(dtTemp_Resto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "modalMensaje('" + mensaje + "')", true);
        }
    }
}