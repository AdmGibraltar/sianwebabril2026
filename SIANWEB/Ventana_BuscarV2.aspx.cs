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
using System.Xml;

namespace SIANWEB
{
    public partial class Ventana_BuscarV2 : System.Web.UI.Page
    {

        #region eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["listaVentana"] != null)
            {
                List<Comun> lista = new List<Comun>();
                lista = (List<Comun>)Session["listaVentana"];

                RadGrid1.DataSource = lista;
                RadGrid1.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana("");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["Precio"] != null)
                        {
                            RadGrid1.Columns["ValorDoble"].Visible = true;
                        }
                        if (Request.QueryString["cn"] != null)
                        {
                            RadGrid1.Columns["ValorStr"].Visible = true;
                        }

                        List<Comun> lista = new List<Comun>();

                        lista = GetList();
                        Session["listaVentana"] = lista;

                        RadGrid1.DataSource = lista;
                        RadGrid1.DataBind();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnBuscar1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<Comun> lista = new List<Comun>();
                lista = GetList();
                Session["listaVentana"] = lista;

                RadGrid1.DataSource = lista;
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSelecionar_Click(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string IdStr = c.Grid.GetRowValues(c.VisibleIndex, "IdStr").ToString();
            string Descripcion = c.Grid.GetRowValues(c.VisibleIndex, "Descripcion").ToString();
            if (RadGrid1.Columns["ValorStr"].Visible == true)
            {
                string ValorStr = c.Grid.GetRowValues(c.VisibleIndex, "ValorStr").ToString();
            }
            if (RadGrid1.Columns["ValorDoble"].Visible == true)
            {
                string ValorDoble = c.Grid.GetRowValues(c.VisibleIndex, "ValorDoble").ToString();
            }

            if (Request.QueryString["Precio"] != null)
            {
                Session["Id_Buscar" + Session.SessionID] = IdStr;
                Session["Descripcion_Buscar" + Session.SessionID] = Descripcion;
                CerrarVentana("precio");
            }
            else if (Request.QueryString["cn"] != null)
            {
                Session["Id_Buscar" + Session.SessionID] = IdStr;
                Session["Descripcion_Buscar" + Session.SessionID] = Descripcion;
                CerrarVentana("cn");
            }
            else if (Request.QueryString["DirEnt"] != null)
            {
                Session["Id_Buscar" + Session.SessionID] = IdStr;
                Session["Descripcion_Buscar" + Session.SessionID] = Request.QueryString["cte"].ToString();
                CerrarVentana("direccion");
            }
            else if (Request.QueryString["CN_IdMatriz"] != null)
            {
                Session["Id_Buscar" + Session.SessionID] = IdStr;
                Session["Descripcion_Buscar" + Session.SessionID] = Descripcion;
                CerrarVentana("direccion_CC");
            }
            else
            {
                Session["Id_Buscar" + Session.SessionID] = IdStr;
                Session["Descripcion_Buscar" + Session.SessionID] = Descripcion;
                CerrarVentana("cliente");
            }
        }

        #endregion

        #region Funciones

        private List<Comun> GetList()
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                List<Comun> List = new List<Comun>();
                if (Request.QueryString["Precio"] != null)
                {
                    Session["BuscarPrecio" + Session.SessionID] = null;
                    CN_CatCliente clsCatProveedores = new CN_CatCliente();

                    Clientes prv = new Clientes();
                    prv.Id_Emp = session2.Id_Emp;
                    prv.Id_Cd = session2.Id_Cd_Ver;
                    prv.Id_Cte = Convert.ToInt32(Request.QueryString["cte"]);
                    clsCatProveedores.ConsultaPrecios(prv, session2.Emp_Cnx, ref List, txtClave.Value, txtNombre.Text == "" ? null : txtNombre.Text);

                }
                else if (Request.QueryString["pvd"] != null)
                {

                    CN_CatProducto clsCatProducto = new CN_CatProducto();

                    Producto prd = new Producto();
                    prd.Id_Emp = session2.Id_Emp;
                    prd.Id_Cd = session2.Id_Cd_Ver;
                    prd.Id_Pvd = Convert.ToInt32(Request.QueryString["pvd"]);
                    clsCatProducto.ConsultaBuscar(prd, session2.Emp_Cnx, ref List, txtClave.Value, txtNombre.Text == "" ? null : txtNombre.Text);
                }
                else if (Request.QueryString["DirEnt"] != null)
                {
                    CN_CatCliente clsCliente = new CN_CatCliente();

                    ClienteDirEntrega cliente = new ClienteDirEntrega();
                    cliente.Id_Emp = session2.Id_Emp;
                    cliente.Id_Cd = session2.Id_Cd_Ver;
                    cliente.Id_Cte = Request.QueryString["cte"] == null ? 0 : Convert.ToInt32(Request.QueryString["cte"]);
                    clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx, ref List);
                }
                else if (Request.QueryString["cn"] != null)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_CatCentroDistribucion clsCD = new CN_CatCentroDistribucion();
                    CentroDistribucion cd = new CentroDistribucion();

                    clsCD.ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    SIANWEB.EnviaCuentaNacional.EnviaCuentaNacional servicio = new EnviaCuentaNacional.EnviaCuentaNacional();
                    string resultado = servicio.CtaNacional(cd.Cd_Rfc, sesion.Id_Cd_Ver, 0);

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(resultado);

                    foreach (XmlNode xmlClientes in xml.ChildNodes)
                    {
                        if (xmlClientes.Name == "CuentasNacionales")
                        {
                            foreach (XmlNode xmlCliente in xmlClientes.ChildNodes)
                            {
                                if (xmlCliente.Name == "CuentaNacional")
                                {
                                    Comun cliente = new Comun();
                                    cliente.IdStr = xmlCliente.Attributes["CliNum"].Value;
                                    cliente.Descripcion = xmlCliente.Attributes["CliNom"].Value;
                                    cliente.ValorStr = xmlCliente.Attributes["Calle"].Value + " #" + xmlCliente.Attributes["NoExterior"].Value + " Col. " + xmlCliente.Attributes["Colonia"].Value + " " + xmlCliente.Attributes["Municipio"].Value + " ," + xmlCliente.Attributes["Estado"].Value;// +xmlCliente.Attributes["CodPost"].Value;
                                    List.Add(cliente);
                                }
                            }
                        }
                    }
                }
                else if (Request.QueryString["TerAse"] != null)
                {
                    CN_CatCliente clsCliente = new CN_CatCliente();

                    Clientes cliTerAse = new Clientes();
                    cliTerAse.Id_Emp = session2.Id_Emp;
                    cliTerAse.Id_Cd = session2.Id_Cd_Ver;
                    cliTerAse.Id_Rik = Request.QueryString["TerAse"] == null ? 0 : Convert.ToInt32(Request.QueryString["TerAse"]);
                    clsCliente.ConsultaClientesTerAsesor(cliTerAse, session2.Emp_Cnx, txtClave.Value, txtNombre.Text == "" ? null : txtNombre.Text, ref List);
                }
                else if (Request.QueryString["CN_IdMatriz"] != null)
                {
                    int idMatriz = Int32.Parse(Request.QueryString["CN_IdMatriz"]);
                    CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

                    var diFisc = cn.ConsultaDireccionesFiscales(idMatriz);

                    foreach (CapaModelo_CC.CuentasCoorporativas.CatACYS_DirFiscales dir in diFisc)
                    {
                        Comun com = new Comun();
                        com.Id = dir.Id;
                        com.IdStr = dir.Id.ToString();
                        com.Descripcion = "<b>" + dir.ClienteDirFis + "</b>" + "," + dir.DireccionDirFis + "," + dir.NumeroDirFis + "," + dir.ColoniaDirFis + "," + dir.CPDirFis + "," + dir.MunicipioDirFis + "," + dir.EstadoDirFis + "," + dir.RFCDirFis;

                        List.Add(com);
                    }



                }
                else if (Request.QueryString["ClienteSIAN"] != null)
                {
                    string idClienteSIAN = Request.QueryString["ClienteSIAN"];
                    CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();


                    List<catcnac_DireccionesFiscales> listadireccion = new List<catcnac_DireccionesFiscales>();
                    cn.ConsultaDireccionesFiscales_SP(idClienteSIAN, session2.Id_Cd_Ver, ref listadireccion, session2.Emp_Cnx);
                    var diFisc = listadireccion;

                    foreach (catcnac_DireccionesFiscales dir in diFisc)
                    {
                        Comun com = new Comun();
                        com.Id = dir.Id;
                        com.IdStr = dir.Id.ToString();
                        com.Descripcion = "<b>" + dir.ClienteDirFis + "</b>" + "," + dir.DireccionDirFis + "," + dir.ColoniaDirFis + "," + dir.CPDirFis + "," + dir.MunicipioDirFis + "," + dir.EstadoDirFis + "," + dir.RFCDirFis;

                        List.Add(com);
                    }
                }
                else
                {
                    CN_CatCliente clsCatProveedores = new CN_CatCliente();

                    Clientes prv = new Clientes();
                    prv.Id_Emp = session2.Id_Emp;
                    prv.Id_Cd = session2.Id_Cd_Ver;
                    prv.Id_Terr = Request.QueryString["ter"] == null ? (int?)null : Convert.ToInt32(Request.QueryString["ter"]);
                    clsCatProveedores.ConsultaClientes(prv, session2.Emp_Cnx, ref List, txtClave.Value, txtNombre.Text == "" ? null : txtNombre.Text);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "close('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}