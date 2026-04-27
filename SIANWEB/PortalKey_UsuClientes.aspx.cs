using CapaEntidad;
using CapaNegocios;
using DevExpress.Office.Utils;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraRichEdit.Layout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class PortalKey_UsuClientes : System.Web.UI.Page
    {
        #region Variables

        private Sesion sesion
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

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }



        #endregion Variables

        #region Mensajes 


        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void sucess(string mensaje)
        {
            ShowMessage(mensaje, "Success");
        }
        private void danger(string mensaje)
        {
            ShowMessage(mensaje, "Error");
        }
        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void info(string mensaje)
        {
            ShowMessage(mensaje, "Info");
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Portalclientes"] != null)
            {
                List<Portakey> Lista = (List<Portakey>)Session["Portalclientes"];
                GrdClientes.DataSource = Lista;
                GrdClientes.DataBind();

            }
            if (Session["PortalUsuarios"] != null)
            {
                List<Portakey> Lista = (List<Portakey>)Session["PortalUsuarios"];
                GrdUsuario.DataSource = Lista;
                GrdUsuario.DataBind();

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
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Session["Portalclientes"] = null;
                        GrdClientes.DataSource = null;
                        GrdClientes.DataBind();
                        Session["PortalUsuarios"] = null;
                        GrdUsuario.DataSource = null;
                        GrdUsuario.DataBind();

                        cargarClientes(sesion.Id_Cd_Ver);
                        if (Request.QueryString["Id"] != null)
                        {
                            int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                            int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                            int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                            if (tipo.ToString().ToUpper() == "1" || tipo.ToString().ToUpper() == "3" || tipo.ToString().ToUpper() == "4")
                            {
                                BtnGuardarCliente.Visible = false;
                                BtnGuardarCliente.Enabled = false;
                                BtnGuardarUsuario.Visible = false;
                                BtnGuardarUsuario.Enabled = false;
                            }
                            else
                            {
                                BtnGuardarCliente.Visible = true;
                                BtnGuardarCliente.Enabled = true;
                                BtnGuardarUsuario.Visible = true;
                                BtnGuardarUsuario.Enabled = true;
                            }


                            consultar(id, tipo, id_Cd);

                            if (tipo.ToString().ToUpper() == "4")
                            {
                                divClientes.Visible = false;
                                BpcUsuario.TabPages[1].Visible = false;
                                cargargridUsuario(id, tipo, id_Cd);
                                Session["idusuario"] = null;
                            }
                            else
                            {
                                cargargridUsuario(id, tipo, id_Cd);
                                Session["idusuario"] = null;
                                cargarRegion(id, tipo, id_Cd);
                                cargargridCliente(id, tipo, id_Cd);
                            }
                            BCBCliente.Items.Add("Seleccionar", "-1");
                            BCBCliente.Value = "-1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                danger(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        public void consultar(int Id, int tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = Id;
            Registro.Tipo = tipo;
            Registro.id_Cd = id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarMAtriz(Registro, ref List, sesion.Emp_Cnx);

            if (List.Count > 0)
            {
                TxtMatriz.Value = List.First().NombreMatriz;
                TxtCorreo.Text = List.First().Correo;
            }
        }

        private void cargarRegion(int id, int tipo, int Id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.Tipo = tipo;
            Registro.id_Cd = Id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarRegion(Registro, ref List, sesion.Emp_Cnx);

            Session["PortalRegion"] = List;
            BCBRegion.DataSource = List;
            BCBRegion.TextField = "NombreRegion";
            BCBRegion.ValueField = "Id_Region";
            BCBRegion.DataBind();

            BCBRegion.Items.Add("Seleccionar", "-1");
            BCBRegion.Value = "-1";
        }

        private void cargargridUsuario(int id, int tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.Tipo = tipo;
            Registro.id_Cd = id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarCorreoUsuario(Registro, ref List, sesion.Emp_Cnx);

            Session["PortalUsuarios"] = List;
            GrdUsuario.DataSource = List;
            GrdUsuario.DataBind();
        }

        private void cargargridCliente(int id, int tipo, int Id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.Tipo = tipo;
            Registro.id_Cd = Id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarClientePortal(Registro, ref List, sesion.Emp_Cnx);

            foreach (Portakey portal in List)
            {
                if (portal.Id_Direccion != -1)
                {
                    List<Portakey> List2 = new List<Portakey>();
                    Portakey Registro2 = new Portakey();
                    Registro2.Id_Emp = sesion.Id_Emp;
                    Registro2.Id_Portal = id;
                    Registro2.Tipo = tipo;
                    Registro2.id_Cd = portal.id_Cd;
                    Registro2.id_cte = portal.id_cte;
                    Registro2.Id_Direccion = portal.Id_Direccion;
                    cn.ConsultaClienteEnviodet(Registro2, ref List2, sesion.Emp_Cnx);

                    if (List2.Count != 0)
                    {
                        portal.DireccionCompleta = List2.First().DireccionCompleta;
                    }
                }
            }

            Session["Portalclientes"] = List;
            GrdClientes.DataSource = List;
            GrdClientes.DataBind();
        }


        protected void BtnGuardarUsuario_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int verificador = 0;
                string mensaje = "";

                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();
                List<Portakey> listaCorreo = new List<Portakey>();

                if (txtCorreoUsuario.Text.Trim().Length == 0 && txtApellido.Text.Trim().Length == 0)
                {
                    info("Favor de Capturar los Datos del Usuario.");
                    return;
                }

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                Datos.NombreCorreoUsuario = txtNombre.Text.Trim();
                Datos.ApellidosCorreoUsuario = txtApellido.Text.Trim();
                Datos.CorreoUsuario = txtCorreoUsuario.Text;
                Datos.Correo = txtCorreoUsuario.Text;
                Datos.Tipo = 7;
                Datos.id_Cd = sesion.Id_Cd;
                Datos.id_cte = 0;

                List<Portakey> Lista = new List<Portakey>();



                if (Session["idusuario"] != null)
                {
                    string id_correoUsuario = Session["idusuario"].ToString();
                    Datos.id_CorreoUsuario = Convert.ToInt32(id_correoUsuario);

                    Cn.ConsultarCorreoUnidad(Datos, ref listaCorreo, sesion.Emp_Cnx);
                    if (listaCorreo.Count > 0)
                    {
                        info("El usuario no puede capturarse, el correo ya esta siendo utilizado por el cliente para el acceso al portal.");
                        return;
                    }


                    Cn.ModificarCorreoUsuario(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se Actualizo la Información Correctamente");
                        Session["idusuario"] = null;
                        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                        int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                        cargargridUsuario(id, tipo, id_Cd);
                        LimpiarUsuario();
                    }
                }
                else
                {
                    Cn.ConsultarCorreoUnidad(Datos, ref listaCorreo, sesion.Emp_Cnx);
                    if (listaCorreo.Count > 0)
                    {
                        info("El usuario no puede capturarse, el correo ya esta siendo utilizado por el cliente para el acceso al portal.");
                        return;
                    }


                    Cn.InsertarCorreoUsuario(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se guardo la Información Correctamente");
                        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                        int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                        cargargridUsuario(id, tipo, id_Cd);
                        LimpiarUsuario();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message);
            }
        }

        public void LimpiarUsuario()
        {
            txtCorreoUsuario.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
        }
        protected void BtnGuardarCliente_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int verificador = 0;
                string mensaje = "";

                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                if (BCBCliente.Value.ToString() == "-1")
                {
                    info("Favor de seleccionar el cliente");
                    return;
                }

                if (BCBRegion.Value.ToString() == "-1")
                {
                    info("Favor de seleccionar la región");
                    return;
                }

                if (BcbDirEntrega.Value.ToString() == "-1")
                {
                    info("Favor de seleccionar una dirección de entrega");
                    return;
                }

                if (txtUnidad.Text == "")
                {
                    info("Favor de seleccionar la Unidad");
                    return;
                }

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                Datos.id_cte = Convert.ToInt32(BCBCliente.Value.ToString());
                Datos.Id_Region = Convert.ToInt32(BCBRegion.Value.ToString());
                Datos.id_Cd = sesion.Id_Cd_Ver;
                Datos.nombreCliente = BCBCliente.Text.ToString().Split('-')[1].ToString();
                Datos.unidad = txtUnidad.Text;
                Datos.Id_Direccion = Convert.ToInt32(BcbDirEntrega.Value.ToString());

                List<Portakey> Lista = new List<Portakey>();

                VerificarCliente(sesion.Id_Cd_Ver, Convert.ToInt32(BCBCliente.Value.ToString()), ref mensaje);

                if (mensaje != "")
                {
                    info(mensaje);
                    return;
                }

                int verificadorUnidad = 0;
                Cn.ConsultaClienteUnidad(Datos, ref verificadorUnidad, Emp_CnxCentral);
                if (verificadorUnidad != 0)
                {
                    info("no se puede guardar la información, Ya existe la unidad regitrada");
                    return;
                }


                if (Session["idCliente"] != null)
                {
                    Cn.ModificarCliente(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se Actualizo la Información Correctamente");
                        Session["idCliente"] = null;
                        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                        int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                        cargargridCliente(id, tipo, id_Cd);
                        limpiarDatosCliente();
                    }
                }
                else
                {
                    List<Portakey> List = new List<Portakey>();

                    Cn.ConsultaClienteCapturados(Datos, ref List, sesion.Emp_Cnx);
                    if (List.Count() > 0)
                    {
                        info("No se Puede Capturar el Cliente, El cliente ya esta registrado");
                        return;
                    }

                    Cn.InsertarCliente(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se guardo la Información Correctamente");
                        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                        int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                        cargargridCliente(id, tipo, id_Cd);
                        limpiarDatosCliente();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message);
            }

        }

        protected void BtneliminarCliente_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BootstrapButton btn = sender as BootstrapButton;
                GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
                int index = container.VisibleIndex;

                string id_Cd = DataBinder.Eval(container.DataItem, "id_Cd").ToString().Trim();
                string Id_Region = DataBinder.Eval(container.DataItem, "Id_Region").ToString().Trim();
                string id_cte = DataBinder.Eval(container.DataItem, "id_cte").ToString().Trim();
                string unidad = DataBinder.Eval(container.DataItem, "unidad").ToString().Trim();
                string Direccion = DataBinder.Eval(container.DataItem, "Id_Direccion").ToString().Trim();

                int verificador = 0;
                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                Datos.id_Cd = Convert.ToInt32(id_Cd);
                Datos.id_cte = Convert.ToInt32(id_cte);
                Datos.Id_Direccion = Convert.ToInt32(Direccion);
                Cn.EliminarCliente(Datos, ref verificador, sesion.Emp_Cnx);

                info("Se Elimino la Información Correctamente");
                int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                int id_Cdi = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                cargargridCliente(id, tipo, id_Cdi);

            }
            catch (Exception ex)
            {
                warning("Error al intentar Eliminar Datos.");
            }
        }

        protected void BtnEditarCliente_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;

            string id_Cd = DataBinder.Eval(container.DataItem, "id_Cd").ToString().Trim();
            string Id_Region = DataBinder.Eval(container.DataItem, "Id_Region").ToString().Trim();
            string id_cte = DataBinder.Eval(container.DataItem, "id_cte").ToString().Trim();
            string unidad = DataBinder.Eval(container.DataItem, "unidad").ToString().Trim();
            string Direccion = DataBinder.Eval(container.DataItem, "Id_Direccion").ToString().Trim();

            Session["idCliente"] = id_cte;
            BCBRegion.Value = Id_Region;

            cargarClientes(Convert.ToInt32(id_Cd));
            BCBCliente.Value = id_cte;
            CargarDireccionesEntrega(Convert.ToInt32(id_Cd), Convert.ToInt32(id_cte));
            BcbDirEntrega.Value = Direccion;
            txtUnidad.Text = unidad;
        }

        public void limpiarDatosCliente()
        {
            BCBRegion.Value = "-1";
            BCBCliente.Value = "-1";
            BcbDirEntrega.Value = "-1";
            txtUnidad.Text = string.Empty;
        }

        protected void BtnEditarUsuario_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;

            string Id = DataBinder.Eval(container.DataItem, "id_CorreoUsuario").ToString().Trim();
            string nombre = DataBinder.Eval(container.DataItem, "NombreCorreoUsuario").ToString().Trim();
            string Apellido = DataBinder.Eval(container.DataItem, "ApellidosCorreoUsuario").ToString().Trim();
            string correo = DataBinder.Eval(container.DataItem, "CorreoUsuario").ToString().Trim();


            txtNombre.Text = nombre;
            txtCorreoUsuario.Text = correo;
            txtApellido.Text = Apellido;
            Session["idusuario"] = Id;
        }

        protected void BtneliminarUsuario_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BootstrapButton btn = sender as BootstrapButton;
                GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
                int index = container.VisibleIndex;

                string Id = DataBinder.Eval(container.DataItem, "id_CorreoUsuario").ToString().Trim();
                string nombre = DataBinder.Eval(container.DataItem, "NombreCorreoUsuario").ToString().Trim();
                string Apellido = DataBinder.Eval(container.DataItem, "ApellidosCorreoUsuario").ToString().Trim();
                string correo = DataBinder.Eval(container.DataItem, "CorreoUsuario").ToString().Trim();

                int verificador = 0;
                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                Datos.id_CorreoUsuario = Convert.ToInt32(Id);

                Cn.EliminarCorreoUsuario(Datos, ref verificador, sesion.Emp_Cnx);

                if (verificador != 0)
                {
                    info("Se Elimino la Información Correctamente");
                    int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                    int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());

                    cargargridUsuario(id, tipo, id_Cd);
                    LimpiarUsuario();
                }
            }
            catch (Exception ex)
            {
                warning("Error al intentar Eliminar Datos.");
            }
        }

        public void cargarClientes(int id_Cd)
        {
            try
            {

                CN_PortalKey CN = new CN_PortalKey();
                Portakey Portal = new Portakey();
                List<Portakey> lista = new List<Portakey>();
                Portal.Id_Emp = sesion.Id_Emp;
                Portal.id_Cd = id_Cd;
                CN.ConsultarDatosCliente(Portal, ref lista, Emp_CnxCentral);

                BCBCliente.DataSource = lista;
                BCBCliente.TextField = "descricpion";
                BCBCliente.ValueField = "id_cte";
                BCBCliente.DataBind();

                BCBCliente.Items.Add("Seleccionar", "-1");
                BCBCliente.Value = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void VerificarCliente(int id_Cd, int id_Cte, ref string mensaje)
        {
            try
            {
                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();
                List<Portakey> DatosCliente = new List<Portakey>();
                List<Portakey> segmento = new List<Portakey>();
                List<Portakey> ListaCredito = new List<Portakey>();

                string password = "";
                Datos.Id_Emp = sesion.Id_Emp;
                Datos.id_Cd = sesion.Id_Cd;
                Datos.id_cte = id_Cte;

                Cn.ConsultaDatosCliente(Datos, ref DatosCliente, sesion.Emp_Cnx);
                if (DatosCliente.Count > 0)
                {
                    foreach (Portakey Registro in DatosCliente)
                    {
                        if (Registro.Cte_FacRfc == "" || Registro.Cte_FacRfc == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: RFC";
                            }
                            else
                            {
                                mensaje = mensaje + ", RFC";
                            }
                        }
                        if (Registro.calle == "" || Registro.calle == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Calle";
                            }
                            else
                            {
                                mensaje = mensaje + ", Calle";
                            }
                        }
                        if (Registro.Cte_Numero == "" || Registro.Cte_Numero == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Número";
                            }
                            else
                            {
                                mensaje = mensaje + ", Número";
                            }
                        }
                        if (Registro.Cte_Municipio == "" || Registro.Cte_Municipio == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Municipio";
                            }
                            else
                            {
                                mensaje = mensaje + ", Municipio";
                            }
                        }
                        if (Registro.Cte_Estado == "" || Registro.Cte_Estado == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Estado";
                            }
                            else
                            {
                                mensaje = mensaje + ", Estado";
                            }
                        }
                        if (Registro.Cte_CP == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Código Postal";
                            }
                            else
                            {
                                mensaje = mensaje + ", Código Postal";
                            }
                        }
                        if (Registro.Cte_Telefono == "" || Registro.Cte_Telefono == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Teléfono";
                            }
                            else
                            {
                                mensaje = mensaje + ", Teléfono";
                            }
                        }
                        if (Registro.DirEntregacte_calle == "" || Registro.DirEntregacte_calle == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Calle dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", dirección de entrega";
                            }
                        }
                        if (Registro.DirEntregacte_numero == "" || Registro.DirEntregacte_numero == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Número dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", Número dirección de entrega";
                            }
                        }
                        if (Registro.DirEntregaCte_municipio == "" || Registro.DirEntregaCte_municipio == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Municipio dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", Municipio dirección de entrega";
                            }
                        }
                        if (Registro.DirEntregaCte_Estado == "" || Registro.DirEntregaCte_Estado == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Estado dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", Estado dirección de entrega";
                            }
                        }
                        if (Registro.DirEntregacte_Cp == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Código Postal dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", Código Postal dirección de entrega";
                            }
                        }
                        if (Registro.DirEntregacte_telefono == "" || Registro.DirEntregacte_telefono == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Teléfono dirección de entrega";
                            }
                            else
                            {
                                mensaje = mensaje + ", Teléfono dirección de entrega";
                            }
                        }

                        if (Registro.nombre == "" || Registro.nombre == null || Registro.nombre2 == "" || Registro.nombre2 == null)
                        {
                            if (mensaje == "")
                            {
                                mensaje = "Falta información en el catalogo de cliente de: Contacto (debera de incluir nombre completo y apellido paterno)";
                            }
                            else
                            {
                                mensaje = mensaje + ",  Contacto (debera de incluir nombre completo y apellido paterno)";
                            }
                        }

                    }
                }

                //Cn.ConsultarSegmentoCliente(Datos, ref segmento, Emp_CnxCentral);

                //if (segmento.Count == 0)
                //{
                //    if (mensaje == "")
                //    {
                //        mensaje = "El usuario no puede capturarse, favor de capturar un territorio para el cliente y el territorio del usuario no debe ser venta mostrador o venta directa.";
                //    }
                //    else
                //    {
                //        mensaje = mensaje + " <br> El usuario no puede capturarse, favor de capturar un territorio para el cliente y el territorio del usuario no debe ser venta mostrador o venta directa. .";
                //    }
                //}

                Cn.ConsultaCredito(Datos, ref ListaCredito, sesion.Emp_Cnx);

                if (ListaCredito.Count > 0)
                {
                    if (ListaCredito.First().Credito == 0 || ListaCredito.First().limite == 0)
                    {
                        if (mensaje == "")
                        {
                            mensaje = "El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                        }
                        else
                        {
                            mensaje = mensaje + " <br> El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }
        }

        protected void BtnEditarCliente_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BtneliminarCliente_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BtnEditarUsuario_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BtneliminarUsuario_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BCBCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Cd = sesion.Id_Cd_Ver;
            int Id_Cte = Convert.ToInt32(BCBCliente.Value.ToString());
            CargarDireccionesEntrega(ID_Cd, Id_Cte);
        }

        public void CargarDireccionesEntrega(int ID_Cd, int Id_Cte)
        {
            try
            {
                BcbDirEntrega.DataSource = null;
                BcbDirEntrega.DataBind();

                if (ID_Cd != 0 && Id_Cte != 0)
                {
                    CN_PortalKey CN = new CN_PortalKey();
                    Portakey Portal = new Portakey();
                    List<Portakey> lista = new List<Portakey>();
                    Portal.Id_Emp = sesion.Id_Emp;
                    Portal.id_Cd = ID_Cd;
                    Portal.id_cte = Id_Cte;
                    CN.ConsultaClienteEnvio(Portal, ref lista, sesion.Emp_Cnx);

                    BcbDirEntrega.DataSource = lista;
                    BcbDirEntrega.TextField = "DireccionCompleta";
                    BcbDirEntrega.ValueField = "Id_Direccion";
                    BcbDirEntrega.DataBind();
                }
                BcbDirEntrega.Items.Add("Seleccionar", "-1");
                BcbDirEntrega.Value = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}