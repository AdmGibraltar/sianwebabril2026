using System;
using System.Collections.Generic;
using System.Web.UI;
using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using DevExpress.Web.Bootstrap;

namespace SIANWEB
{
    public partial class CapListaPreciosConsultaDet : System.Web.UI.Page
    {
        protected void page_init(object sender, EventArgs e)
        {
            if (Session["dtListaPreciosDet"] != null)
            {

                List<ListaPrecios> ListaReporteCostos = new List<ListaPrecios>();
                ListaReporteCostos = (List<ListaPrecios>)Session["dtListaPreciosDet"];
                grdServicio.DataSource = ListaReporteCostos;
                grdServicio.DataBind();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dtListaPreciosDet"] != null)
                {
                    Session["dtListaPreciosDet"] = null;
                }
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {

                    cargarDatos();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        public void cargarDatos()
        {


            Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
            CN__Comun CN_Comun = new CN__Comun();

            int Id_Emp = gSession.Id_Emp;
            int Id_Cd = gSession.Id_Cd;


            int opcion = 0; //0 trae solo lo que cambio en un mes y 1 trae todo los cambios 

            wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
            wsListaPrecios.ListaPrecios[] ListaFacturaTmp = wslista.GetListaActualizada("key", 0, opcion);


            List<ListaPrecios> ListaFactura = new List<ListaPrecios>();
            foreach (wsListaPrecios.ListaPrecios Item in ListaFacturaTmp)
            {
                ListaPrecios lista = new ListaPrecios();

                lista.Id_Prd = Item.Id_Prd;
                lista.Descripcion = Item.Descripcion;

                lista.NOMBREPROVEEDOR = Item.NOMBREPROVEEDOR;
                lista.NODEARTDEPROVEEDOR = Item.NODEARTDEPROVEEDOR;
                lista.PAAAACTUAL = Item.PAAAACTUAL;
                lista.PLISTAACTUAL = Item.PLISTAACTUAL;
                lista.PAAAAnterior = Item.PAAAAnterior;
                lista.PLISTAANTERIOR = Item.PLISTAANTERIOR;
                lista.PVariacionPAAA = Item.PVariacionPAAA;
                lista.PVariacionPLISTA = Item.PVariacionPLISTA;
                lista.FECHAINICIOVIG = Item.FECHAINICIOVIG;
                ListaFactura.Add(lista);
            }

            Session["dtListaPreciosDet"] = ListaFactura;
            grdServicio.DataSource = ListaFactura;
            grdServicio.DataBind();

        }

        protected void btnConsultar_ServerClick(object sender, EventArgs e)
        {
            cargarDatos();
        }

        protected void btnRegresar_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("CapListaPreciosConsulta.aspx");
        }


    }
}