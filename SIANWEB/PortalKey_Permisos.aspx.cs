using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class PortalKey_Permisos : System.Web.UI.Page
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
            if (Session["PortalPermisos"] != null)
            {
                List<Portakey> Lista = (List<Portakey>)Session["PortalPermisos"];
                GrdPermisos.DataSource = Lista;
                GrdPermisos.DataBind();
                GrdPermisos.ExpandAll();
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
                        Session["PortalPermisos"] = null;
                        GrdPermisos.DataSource = null;
                        GrdPermisos.DataBind();

                        if (Request.QueryString["Id"] != null)
                        {
                            int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                            int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                            int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                            consultar(id, tipo, id_Cd);
                            cargarUsuario(id, tipo, id_Cd);
                            if (tipo == 3 || tipo == 4)
                            {
                                BtnGuardar.Visible = false;
                                BtnGuardar.Disabled = true;
                            }
                            else
                            {
                                BtnGuardar.Visible = true;
                                BtnGuardar.Disabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                danger(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        public void consultar(int Id, int Tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = Id;
            Registro.Tipo = Tipo;
            Registro.id_Cd = id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarMAtriz(Registro, ref List, sesion.Emp_Cnx);

            if (List.Count > 0)
            {
                TxtMatriz.Value = List.First().NombreMatriz;
                TxtCorreo.Text = List.First().Correo;
            }
        }


        private void cargarUsuario(int id, int tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.Tipo = tipo;
            Registro.id_Cd = id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarCorreoUsuario(Registro, ref List, sesion.Emp_Cnx);

            Session["PortalPermisosUsuarios"] = List;
            BCBUsuario.DataSource = List;
            BCBUsuario.TextField = "NombreCompleto";
            BCBUsuario.ValueField = "id_CorreoUsuario";
            BCBUsuario.DataBind();

            BCBUsuario.Items.Add("Seleccionar", "-1");
            BCBUsuario.Value = "-1";
        }


        private void CargarGridPermisos(int id, int tipo, int Id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.id_CorreoUsuario = Convert.ToInt32(BCBUsuario.Value.ToString());
            Registro.Tipo = tipo;
            Registro.id_Cd = Id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarPortalPermiso(Registro, ref List, sesion.Emp_Cnx);

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

            Session["PortalPermisos"] = List;
            GrdPermisos.DataSource = List;
            GrdPermisos.DataBind();
            GrdPermisos.ExpandAll();
        }

        protected void BCBUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BCBUsuario.Value.ToString() == "-1")
            {
                Session["PortalPermisos"] = null;
                GrdPermisos.DataSource = null;
                GrdPermisos.DataBind();
                TxtCorreoPermisos.Text = string.Empty;
            }
            if (Session["PortalPermisosUsuarios"] != null)
            {
                List<Portakey> Lista = (List<Portakey>)Session["PortalPermisosUsuarios"];
                int id_CorreoUsu = Convert.ToInt32(BCBUsuario.Value.ToString());
                if (id_CorreoUsu != -1)
                {
                    

                   
                    Portakey datos = Lista.Where(x => x.id_CorreoUsuario == id_CorreoUsu).First();

                    TxtCorreoPermisos.Text = datos.CorreoUsuario;

                    if (Request.QueryString["Id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                        int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                        int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                        CargarGridPermisos(id, tipo, id_Cd);

                        for (int i = 0; i < GrdPermisos.VisibleRowCount; i++)
                        {
                            int estatus = Convert.ToInt32(GrdPermisos.GetRowValues(i, "permiso"));

                            if (estatus == 1)
                            {
                                GrdPermisos.Selection.SetSelection(i, true);

                            }
                            else
                            {
                                GrdPermisos.Selection.SetSelection(i, false);
                            }
                        }
                    }
                }
                else
                {
                    GrdPermisos.DataSource = null;
                    GrdPermisos.DataBind();
                    GrdPermisos.ExpandAll();
                }
            }
        }

        protected void BtnGuardar_ServerClick(object sender, EventArgs e)
        {
            if (BCBUsuario.Value.ToString() == "-1")
            {
                info("Favor de Seleccionar un Usuario");
            }
            else
            {
                int id_usu = Convert.ToInt32(BCBUsuario.Value.ToString());
                guardarPermisos(id_usu);
            }
        }


        public void guardarPermisos(int id_usu)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_PortalKey CN = new CN_PortalKey();

                int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                int verificador = 0;

                List<Portakey> List = new List<Portakey>();
                Portakey Registro = new Portakey();
                Registro.Id_Emp = sesion.Id_Emp;
                Registro.Id_Portal = id;
                Registro.id_CorreoUsuario = Convert.ToInt32(BCBUsuario.Value.ToString());

                CN.EliminarPermisos(Registro, ref verificador, sesion.Emp_Cnx);

                if (verificador == 1)
                {
                    for (var i = 0; i < GrdPermisos.VisibleRowCount; i++)
                    {
                        string data = GrdPermisos.GetRowValues(i, "permiso").ToString();
                        if (GrdPermisos.GetRowValues(i) != null)
                        {
                            if (GrdPermisos.Selection.IsRowSelected(i))
                            {
                                try
                                {
                                    Registro = new Portakey();
                                    Registro.Id_Emp = sesion.Id_Emp;
                                    Registro.Id_Portal = id;
                                    Registro.id_CorreoUsuario = Convert.ToInt32(BCBUsuario.Value.ToString());
                                    Registro.id_cte = Convert.ToInt32(GrdPermisos.GetRowValues(i, "id_cte").ToString());
                                    Registro.id_Cd = Convert.ToInt32(GrdPermisos.GetRowValues(i, "id_Cd").ToString());
                                    Registro.Id_Region = Convert.ToInt32(GrdPermisos.GetRowValues(i, "Id_Region").ToString());
                                    Registro.unidad = GrdPermisos.GetRowValues(i, "unidad").ToString();
                                    Registro.Id_Direccion = Convert.ToInt32(GrdPermisos.GetRowValues(i, "Id_Direccion").ToString());
                                    Registro.intpermiso = 1;

                                    CN.InsertarPermiso(Registro, ref verificador, sesion.Emp_Cnx);
                                }
                                catch (Exception ex)
                                {
                                    warning("Problema al guardar la información");
                                }
                            }
                        }
                    }
                }
                info("Se guardo la información");
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString());
            }
            finally
            {
                int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                CargarGridPermisos(id, tipo, id_Cd);
            }
        }

        protected void GrdPermisos_DataBound(object sender, EventArgs e)
        {
           
            for (int i = 0; i < GrdPermisos.VisibleRowCount; i++)
            {
                int estatus = Convert.ToInt32(GrdPermisos.GetRowValues(i, "permiso"));

                if (estatus == 1)
                {
                    GrdPermisos.Selection.SetSelection(i,true);
                    
                }
                else
                {
                    GrdPermisos.Selection.SetSelection(i, false);
                }
            }
        }
    }
}