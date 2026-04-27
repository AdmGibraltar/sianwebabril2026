using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Xml;
using CapaModelo;

namespace SIANWEB
{
    public partial class Ventana_Buscar : System.Web.UI.Page
    {

        private string uniqueKey
        {
            get
            {
                if (Request.QueryString["Ukey"] != null)
                {
                    return Request.QueryString["Ukey"].ToString();
                }
                return string.Empty;
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
                        string uk = uniqueKey;
                        //if (Session["BuscarPrecio" + Session.SessionID] != null)
                        //{
                        if (Request.QueryString["Precio"] != null)
                        {
                            RadGrid1.Columns.FindByUniqueName("Precio").Display = true;
                        }
                        if (Request.QueryString["cn"] != null)
                        {
                            RadGrid1.Columns.FindByUniqueName("Direccion").Display = true;
                        }
                        RadGrid1.Rebind();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RadGrid1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }

        private List<Comun> GetList()
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                List<Comun> List = new List<Comun>();
                if (Request.QueryString["Precio"] != null)
                {
                    Session["BuscarPrecio" + Session.SessionID + uniqueKey] = null;
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
                    clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx, ref List); //ConsultaClientes(prv, session2.Emp_Cnx, ref List, txtClave.Value, txtNombre.Text == "" ? null : txtNombre.Text);
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
                        //com.ValorStr = dir.DireccionDirFis + " " + dir.ColoniaDirFis + " " + dir.CPDirFis + " " + dir.MunicipioDirFis + " " + dir.EstadoDirFis + " "+ dir.RFCDirFis;
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
                        //com.ValorStr = dir.DireccionDirFis + " " + dir.ColoniaDirFis + " " + dir.CPDirFis + " " + dir.MunicipioDirFis + " " + dir.EstadoDirFis + " "+ dir.RFCDirFis;
                        com.IdStr = dir.Id.ToString();
                        com.Descripcion = "<b>" + dir.ClienteDirFis + "</b>" + "," + dir.DireccionDirFis + "," + dir.ColoniaDirFis + "," + dir.CPDirFis + "," + dir.MunicipioDirFis + "," + dir.EstadoDirFis + "," + dir.RFCDirFis;

                        List.Add(com);
                    }



                }

                else if (Request.QueryString["Chofer"] != null)
                {
                    int folio = 0;
                    if (txtClave.Text == "")
                        folio = 0;
                    else
                    {
                        folio = int.Parse(txtClave.Text);
                    }

                    CN_CapRemision CN = new CN_CapRemision();

                    Chofer chofer = new Chofer();
                    chofer.Id_Emp = session2.Id_Emp;
                    chofer.Id_Cd = session2.Id_Cd_Ver;
                    chofer.Folio = folio;
                    chofer.Nombre = txtNombre.Text;
                    CN.ConsultaChoferesList(chofer, session2.Emp_Cnx, ref List);
                }

                else if (Request.QueryString["Vehiculo"] != null)
                {
                    CN_CapRemision CN = new CN_CapRemision();

                    Vehiculo Veh = new Vehiculo();
                    Veh.Id_Emp = session2.Id_Emp;
                    Veh.Id_Cd = session2.Id_Cd_Ver;
                    Veh.PlacaVehiculo = txtClave.Text;
                    Veh.Nom_Vehiculo = txtNombre.Text;
                    CN.ConsultaVehiculosList(Veh, session2.Emp_Cnx, ref List);
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
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }
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
                Alerta(Message);
                //this.lblMensaje.Text = Message;
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Select")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        if (Request.QueryString["Precio"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("precio");
                        }
                        else if (Request.QueryString["cn"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("cn");
                        }
                        else if (Request.QueryString["DirEnt"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = Request.QueryString["cte"].ToString();
                            CerrarVentana("direccion");
                        }
                        else if (Request.QueryString["CN_IdMatriz"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("direccion_CC");
                        }
                        else if (Request.QueryString["Chofer"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("chofer");
                        }
                        else if (Request.QueryString["Vehiculo"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("vehiculo");
                        }
                        else
                        {
                            Session["Id_Buscar" + Session.SessionID + uniqueKey] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID ] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("cliente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}