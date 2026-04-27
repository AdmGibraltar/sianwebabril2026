using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using DevExpress.Web.Bootstrap;
using DevExpress.Web;
using System.Collections;
using Newtonsoft.Json;
using System.Web.Services;

namespace SIANWEB
{
    public partial class ProPedidoProductosAcys2 : System.Web.UI.Page
    {
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

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

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["NuevoPrdocutoAcys"] != null)
            {
                dt = (DataTable)Session["NuevoPrdocutoAcys"];

                rg1.DataSource = dt;
                rg1.DataBind();

            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];


            if (!Page.IsPostBack)
            {
                Session["NuevoPrdocutoAcys"] = null;
                if (Request.QueryString["Id_Acs"] != null)
                {
                    txtIdTer.Value = Request.QueryString["Id_Ter"].ToString();
                    txtIdCte.Value = Request.QueryString["Id_Cte"].ToString();
                    txtIdRik.Value = Request.QueryString["IdRik"].ToString();
                    txtClave.Value = Request.QueryString["Id_Acs"].ToString();
                    HF_IdCd.Value = session.Id_Cd.ToString();
                    HF_IdEmp.Value = session.Id_Emp.ToString();
                    HF_Emp_Cnx.Value = session.Emp_Cnx;

                    CN_CapPedido CNPedido = new CN_CapPedido();
                    List<PedidoDet> listNuevoPedido = new List<PedidoDet>();
                    Pedido totalpedido = new Pedido();
                    totalpedido.Id_Emp = session.Id_Emp;
                    totalpedido.Id_Cd = session.Id_Cd;
                    totalpedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id_Acs"].ToString());

                    CNPedido.ConsultarProductoParaVI2(totalpedido, session.Emp_Cnx, ref listNuevoPedido);

                    List<PedidoDet> query = (from tlist in listNuevoPedido
                                             where tlist.TotalProd >= 5
                                             select tlist).ToList(); ;

                    if (query.Count() > 0)
                    {
                        string Listaproducto = "";
                        for (var i = 0; i < query.Count(); i++)
                        {
                            if (Listaproducto == "")
                            {
                                Listaproducto = query[i].Id_Prd.ToString();
                            }
                            else
                            {
                                Listaproducto = Listaproducto + ", " + query[i].Id_Prd.ToString();
                            }
                        }

                        GetListDet();
                        DataTable dtTemp = dt;
                        totalpedido.listaproductos = Listaproducto;
                        CNPedido.ConsultaProductoRepetidosDet2(totalpedido, session.Emp_Cnx, ref dtTemp);
                        dt = dtTemp;
                        Session["NuevoPrdocutoAcys"] = dt;
                        rg1.DataSource = dt;
                        rg1.DataBind();
                    }
                }
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            string verificador = "";
            DataTable dt = (DataTable)Session["NuevoPrdocutoAcys"];
            CN_CapPedidoVtaInst pedidoVtaInst = new CN_CapPedidoVtaInst();
            PedidoVtaInst pedido = new PedidoVtaInst();
            pedido.Id_Emp = session.Id_Emp;
            pedido.Id_Cd = session.Id_Cd;
            pedido.Id_Acs = int.Parse(Request.QueryString["Id_Acs"].ToString());

            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("Acs_Doc = ''");
                if (dr.Length > 0)
                {
                    mensaje("No se seleccionó documento de entrega para el producto <b>" + dr[0][1] + " - " + dr[0][2] + "</b>");
                    return;
                }

                DataRow[] dr2 = dt.Select("Prd_Cantidad = 0");
                if (dr2.Length > 0)
                {
                    mensaje("debe de capturar la cantidad para el producto <b>" + dr2[0][1] + " - " + dr2[0][2] + "</b>");
                    return;
                }

                DataRow[] dr3 = dt.Select("Acs_Frecuencia != 0");
                if (dr3.Length > 0)
                {
                    mensaje("debe de capturar la frecuencia para el producto <b>" + dr3[0][1] + " - " + dr3[0][2] + "</b>");
                    return;
                }
            }

            pedidoVtaInst.AgregarProductoAcys(pedido, dt, session.Emp_Cnx, ref verificador);

        }

        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        protected void rg1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["NuevoPrdocutoAcys"];

            ASPxGridView gridView = (ASPxGridView)sender;

            int i = rg1.FindVisibleIndexByKeyValue(e.Keys["Id_Prd"]);

            e.Cancel = true;
            DataRow row = null;
            row = dtTemp.Rows[i];

            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                row[enumerator.Key.ToString()] = enumerator.Value;
            }

            gridView.CancelEdit();
            e.Cancel = true;

            Session["NuevoPrdocutoAcys"] = dtTemp;


            rg1.DataSource = dtTemp;
            rg1.DataBind();
        }

        protected void rg1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DataTable dtTemp = (DataTable)Session["NuevoPrdocutoAcys"];

            ASPxGridView gridView = (ASPxGridView)sender;

            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString() != "Count")
                {
                    row[enumerator.Key.ToString()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            dtTemp.Rows.Add(row);
            Session["NuevoPrdocutoAcys"] = dtTemp;


            rg1.DataSource = dtTemp;
            rg1.DataBind();
        }

        protected void rg1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["NuevoPrdocutoAcys"];



            ASPxGridView gridView = (ASPxGridView)sender;
            int i = rg1.FindVisibleIndexByKeyValue(e.Keys["Id_Prd"]);
            e.Cancel = true;



            ((BootstrapGridView)sender).JSProperties["cpIsUpdated"] = 1;


            dtTemp.Rows[i].Delete();


            Session["NuevoPrdocutoAcys"] = dtTemp;

            rg1.DataSource = dtTemp;
            rg1.DataBind();


        }

        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_Frecuencia", System.Type.GetType("System.Int32"));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region webMethod

        [WebMethod]
        public static string cmbProductoDetRestos(string IdProd, string idterr, string idCte, string IdRik, string clave, string IdCd, string IdEmp, string EmpCnx, string pedidoProg)
        {
            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            Producto pr2 = new Producto();
            int productoNuevo = 0;
            try
            {

                CN_CapAcys cnCa = new CN_CapAcys();
                if (idterr == "")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });

                }
                if (idCte == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (IdRik == "")
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                pedidoProg = pedidoProg == "0" ? "false" : pedidoProg;
                pedidoProg = pedidoProg == "1" ? "true" : pedidoProg;


                if (bool.Parse(pedidoProg) && cnCa.ExisteProductoEnGarantia(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), Convert.ToInt32(idterr), Convert.ToInt32(idCte), Convert.ToInt32(IdRik), EmpCnx))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                if (string.IsNullOrEmpty(clave))
                {
                    productoNuevo = 1;
                }
                pr.Id_Cte = !string.IsNullOrEmpty(idCte) ? Convert.ToInt32(idCte) : 0;
                cn_catproducto.ConsultaProductos(ref pr, EmpCnx, Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt32(IdProd), ref productoNuevo, 2);

                cn_catproducto.ConsultarVentas(ref pr2, Convert.ToInt32(idCte), EmpCnx);

                return JsonConvert.SerializeObject(new { id = 0, Presentacion = pr.Prd_Presentacion, PrdUni = pr.Prd_UniNs, Cant = 0, Precio = pr.Prd_Precio, imp = pr.Prd_Precio, Descripcion = pr.Prd_Descripcion, mes1 = pr2.ventaMes[0].ToString(), mes2 = pr2.ventaMes[1].ToString(), mes3 = pr2.ventaMes[2].ToString() });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message.ToString() });

            }
        }

        [WebMethod]
        public static string txtCantidad_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;
                double importe = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) == 0)
                    {
                        return JsonConvert.SerializeObject(new { id = 1 });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(cantidad);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(precio);

                importe = Prd_Cantidad * Prd_Precio;

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Id_prd, ref Actuales, EmpCnx);


                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(idCte))
                    pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt32(idCte), Id_prd, ref verificador, EmpCnx);
                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {

                    if (Actuales.Count > 0)
                    {
                        if (Prd_Cantidad > Actuales[2])
                        {
                            return JsonConvert.SerializeObject(new { id = 2, final = Actuales[0], asignado = Actuales[1], disponible = Actuales[2], importe = importe });
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new { id = 0, importe = importe });

                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { id = 0, importe = importe });

                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, mensaje = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });
            }
        }

        [WebMethod]
        public static string txtPrecio_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToDouble(idCte), Id_prd, ref verificador, EmpCnx);

                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                double importe = double.Parse(cantidad) * double.Parse(precio);
                return JsonConvert.SerializeObject(new { id = 0, importe = importe });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });

            }
        }

        [WebMethod(EnableSession = true)]
        public static string CalcularTotalVisible(string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                double imp = 0;

                DataTable dt = (DataTable)HttpContext.Current.Session["Prod"];
                DataTable dt_Restos = (DataTable)HttpContext.Current.Session["Restos"];

                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                    }
                }

                if (dt_Restos != null)
                {
                    foreach (DataRow j in dt_Restos.Rows)
                    {
                        imp += Convert.ToDouble(j["Prd_Importe"] == DBNull.Value ? 0 : j["Prd_Importe"]);
                    }
                }
                imp.ToString();

                double iva_cd = 0;
                CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
                cn.ConsultarIva(int.Parse(IdEmp), int.Parse(IdCd), ref iva_cd, EmpCnx);
                string IVASistema = (imp * iva_cd / 100).ToString();
                string totalImporte = (imp + iva_cd).ToString();

                return JsonConvert.SerializeObject(new { id = 1, subtotal = imp, iva = IVASistema, total = totalImporte });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = "Error al realizar el el total de importe" });
            }

        }

        #endregion
    }
}