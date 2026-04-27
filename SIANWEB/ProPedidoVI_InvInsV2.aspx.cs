using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;
using System.Data;
using DevExpress.Web;

namespace SIANWEB
{
    public partial class ProPedidoVI_InvInsV2 : System.Web.UI.Page
    {
        #region Variables
        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dtPedidoVI" + Session.SessionID];
            }
            set
            {
            }
        }

        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)Session["dtPedidoVI_Resto" + Session.SessionID];
            }
            set
            {

            }
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
        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            Inicializar();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    txtFecha.Value = Request.QueryString["fecha"].ToString().Substring(0, 2) + "/" + Request.QueryString["fecha"].ToString().Substring(2, 2) + "/" + Request.QueryString["fecha"].ToString().Substring(4, 4);
                    txtOrden.Value = Request.QueryString["orden"].ToString();
                    Inicializar();
                }
                catch (Exception ex)
                {
                    mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }

        protected void lnkEquivalencia_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((LinkButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string id_acs = Request.QueryString["Id_Acs"].ToString();
            string id_cte = Request.QueryString["cte"].ToString();
            string Id_Prd = c.Grid.GetRowValues(c.VisibleIndex, "Id_Prd").ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Equivalencia", "AbrirEquivalencia('" + Id_Prd + "', '" + id_acs + "' , '" + id_cte + "')", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = false;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = true;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private DataTable GetListDet()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Asignado", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_InvFinal", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Disponible", System.Type.GetType("System.Int32"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            List<int> actuales;

            foreach (DataRow dr in dt.Rows)
            {

                actuales = new List<int>();
                cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                if (Convert.ToInt32(dr["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                }
            }

            if (dt_Resto != null)
            {
                foreach (DataRow dr in dt_Resto.Rows)
                {

                    actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                    if (Convert.ToInt32(dr["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                    {
                        dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                    }
                }
            }

            return dtTemp;
        }
        private DataTable GetListPrecio()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Precio_Convenido", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Precio_Captado", System.Type.GetType("System.Double"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Prd_PrecioAcys"] == DBNull.Value)
                {
                    dr["Prd_PrecioAcys"] = 0;
                }
                if (Convert.ToInt64(dr["Id_PrdOld"]) != -1 && Convert.ToDouble(dr["Prd_Precio"]) != Convert.ToDouble(dr["Prd_PrecioAcys"]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_PrecioAcys"], dr["Prd_Precio"] });
                }
            }

            if (dt_Resto != null)
            {
                foreach (DataRow dr in dt_Resto.Rows)
                {
                    if (dr["Prd_PrecioAcys"] == DBNull.Value)
                    {
                        dr["Prd_PrecioAcys"] = 0;
                    }
                    if (Convert.ToInt64(dr["Id_Prd"]) != -1 && Convert.ToDouble(dr["Prd_Precio"]) != Convert.ToDouble(dr["Prd_PrecioAcys"]))
                    {
                        dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_PrecioAcys"], dr["Prd_Precio"] });
                    }
                }
            }

            return dtTemp;
        }

        private void Inicializar()
        {
            RadGrid1.DataSource = GetListDet();
            RadGrid2.DataSource = GetListPrecio();

            RadGrid1.DataBind();
            RadGrid2.DataBind();
        }

        #endregion

        #region mensaje

        private void mensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
        #endregion
    }
}