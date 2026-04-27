using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Configuration;

namespace SIANWEB
{


    public partial class CapGestionPreciosInformacion : System.Web.UI.Page
    {

        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Convenio convenio
        {
            get { return (Convenio)Session["Convenio" + Session.SessionID]; }
            set { Session["Convenio" + Session.SessionID] = value; }

        }

        public string NombreArchivopdf;
        public string NombreArchivopdf2;
        public string NombreArchivopdf3;
        public string NombreArchivopdf4;
        public string NombreArchivopdf5;
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Consultar();

                        //desabilito el texto y muestro el boton con la liga para descargar el archivo
                        this.lblNombreOriginalPDF.Visible = false;
                        this.lblNombreOriginalPDF2.Visible = false;
                        this.lblNombreOriginalPDF3.Visible = false;
                        this.lblNombreOriginalPDF4.Visible = false;
                        this.lblNombreOriginalPDF5.Visible = false;

                        buttonDescargarpdf.Visible = true;
                        buttonDescargarpdf2.Visible = true;
                        buttonDescargarpdf3.Visible = true;
                        buttonDescargarpdf4.Visible = true;
                        buttonDescargarpdf5.Visible = true;


                        if (Page.Request.QueryString["TipoOp"].ToString() != "1")
                        {
                            this.btnSolVin.Visible = false;
                            this.RadUploadpdf.Visible = false;
                            this.buttonSubmitpdf.Visible = false;

                            this.btnSolVin2.Visible = false;
                            this.RadUploadpdf2.Visible = false;
                            this.buttonSubmitpdf2.Visible = false;

                            this.btnSolVin3.Visible = false;
                            this.RadUploadpdf3.Visible = false;
                            this.buttonSubmitpdf3.Visible = false;

                            this.btnSolVin4.Visible = false;
                            this.RadUploadpdf4.Visible = false;
                            this.buttonSubmitpdf4.Visible = false;

                            this.btnSolVin5.Visible = false;
                            this.RadUploadpdf5.Visible = false;
                            this.buttonSubmitpdf5.Visible = false;


                            //this.lblNombreOriginalPDF5.Visible = false;


                            this.RadToolBar1.Items[0].Visible = false;


                            //buttonDescargarpdf5.Visible = true;


                        }
                        else
                        {
                            this.btnSolVin.Visible = true;
                            this.RadUploadpdf.Visible = true;
                            this.buttonSubmitpdf.Visible = true;


                            this.btnSolVin2.Visible = true;
                            this.RadUploadpdf2.Visible = true;
                            this.buttonSubmitpdf2.Visible = true;

                            this.btnSolVin3.Visible = true;
                            this.RadUploadpdf3.Visible = true;
                            this.buttonSubmitpdf3.Visible = true;

                            this.btnSolVin4.Visible = true;
                            this.RadUploadpdf4.Visible = true;
                            this.buttonSubmitpdf4.Visible = true;

                            this.btnSolVin5.Visible = true;
                            this.RadUploadpdf5.Visible = true;
                            this.buttonSubmitpdf5.Visible = true;

                            this.RadToolBar1.Items[0].Visible = true;


                            //buttonDescargarpdf5.Visible = false;

                        }

                        //en sianweb no podré grabar inormación solo consultar 
                        this.RadToolBar1.Items[0].Visible = false;
                        RadUploadpdf.Visible = false;
                        Label6.Visible = false;
                        CmbCentro.Visible = false;
                        RadUploadpdf2.Visible = false;
                        RadUploadpdf3.Visible = false;
                        RadUploadpdf4.Visible = false;
                        RadUploadpdf5.Visible = false;
                        buttonSubmitpdf.Visible = false;
                        buttonSubmitpdf2.Visible = false;
                        buttonSubmitpdf3.Visible = false;
                        buttonSubmitpdf4.Visible = false;
                        buttonSubmitpdf5.Visible = false;



                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }



        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {

                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (Page.IsValid)
                {


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //rg ConvenioDet.Rebind();
                        //rgDetalles.Rebind();
                        break;
                    case "GuardarConvenio":
                        //Guardar();
                        break;
                    case "ModificarConvenio":
                        //Modificar();
                        break;
                    case "continuar":
                        //AlertaCerrar("No tiene permiso para guardar");
                        RAM1.ResponseScripts.Add("CloseWindow();");

                        break;


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        #region archivo 1

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreOriginalPDF.Text;

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                lblNombreArchivoPDF.Text = "";
                lblNombreOriginalPDF.Text = "";
                this.HF_SolVin.Value = "";
                buttonSubmitpdf.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void RadAsyncUpload1_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                //HF_SolVin contiene el nombre original del archivo pdf 
                //NombreArchivopdf es el nombre adicional más la fecha y hora y es el que se subirá al repositorio 

                this.HF_SolVin.Value = e.File.FileName;
                NombreArchivopdf = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                lblNombreArchivoPDF.Text = NombreArchivopdf;
                lblNombreOriginalPDF.Text = this.HF_SolVin.Value;
                //NombreHojaPdf = e.File.GetNameWithoutExtension().ToString();

                //if (e.IsValid)
                //{
                //    ValidFiles.Visible = true;
                //    ValidFiles.Controls.Add(fileName);

                //}
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }
        protected void btnImportarPDF_Click(object sender, EventArgs e)
        {
            try
            {


                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin.Value;

                foreach (UploadedFile f in RadUploadpdf.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    buttonSubmitpdf.Visible = false;

                    //conv.PC_NombreArchivoPDF = f.FileName;
                    //conv.PC_ArchivoPDF = f.FileName;

                }

                //lblNombreArchivoPDF.Text = this.HF_SolVin.Value;


                //try
                //{
                //    File.Delete(path);
                //}
                //catch
                //{
                //}



                //https://stackoverflow.com/questions/1003275/how-to-convert-utf-8-byte-to-string


                //foreach (UploadedFile f in RadUploadpdf.UploadedFiles)
                //    {
                //        NombreArchivo = f.GetName();
                //        Nombreextension = f.GetExtension();

                //        patharchivo = path + NombreArchivo;

                //        Label7.Text = patharchivo;
                //        Label9.Text = RadAsyncUpload1.UploadedFiles[0].FileName;lblNombreArchivoPDF
                //        Label3.Text = RadAsyncUpload1.UploadedFiles[0].ContentType;
                //        if (File.Exists(patharchivo))
                //        {
                //            File.Delete(patharchivo);
                //        }
                //        f.SaveAs(patharchivo, true);
                //        PagElec_Soporte4 = ConvertirFileToByteArray(patharchivo);

                //        RadAsyncUpload1.Visible = false;
                //        btnQuitar.Visible = true;
                //        Label9.Visible = true;

                //        //al cargar el archivo automaticamente agregaba renglón pero ahora no , porque provocaba confusipon
                //        ////object objeto = new object();
                //        ////System.EventArgs evento = new EventArgs();
                //        ////BtnAgregar_Click(objeto, evento);

                //    }

            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        #endregion archivo 1


        #region archivo 2
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreOriginalPDF2.Text;

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                lblNombreArchivoPDF2.Text = "";
                lblNombreOriginalPDF2.Text = "";
                this.HF_SolVin2.Value = "";
                buttonSubmitpdf2.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportarPDF2_Click(object sender, EventArgs e)
        {
            try
            {


                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin2.Value;

                foreach (UploadedFile f in RadUploadpdf2.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    buttonSubmitpdf2.Visible = false;
                }

                //lblNombreArchivoPDF2.Text = this.HF_SolVin2.Value;


                //try
                //{
                //    File.Delete(path);
                //}
                //catch
                //{
                //}

            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }
        protected void RadAsyncUpload2_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                //HF_SolVin contiene el nombre original del archivo pdf 
                //NombreArchivopdf es el nombre adicional más la fecha y hora y es el que se subirá al repositorio 

                this.HF_SolVin2.Value = e.File.FileName;
                NombreArchivopdf2 = e.File.GetNameWithoutExtension().ToString() + "A2" + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                lblNombreArchivoPDF2.Text = NombreArchivopdf2;
                lblNombreOriginalPDF2.Text = this.HF_SolVin2.Value;
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }
        #endregion archivo 2

        #region archivo 3
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreOriginalPDF3.Text;

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                lblNombreArchivoPDF3.Text = "";
                lblNombreOriginalPDF3.Text = "";
                this.HF_SolVin3.Value = "";
                buttonSubmitpdf3.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportarPDF3_Click(object sender, EventArgs e)
        {
            try
            {


                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin3.Value;

                foreach (UploadedFile f in RadUploadpdf3.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    buttonSubmitpdf3.Visible = false;
                }





            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }
        protected void RadAsyncUpload3_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                //HF_SolVin contiene el nombre original del archivo pdf 
                //NombreArchivopdf es el nombre adicional más la fecha y hora y es el que se subirá al repositorio 

                this.HF_SolVin3.Value = e.File.FileName;
                NombreArchivopdf3 = e.File.GetNameWithoutExtension().ToString() + "A3" + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                lblNombreArchivoPDF3.Text = NombreArchivopdf3;
                lblNombreOriginalPDF3.Text = this.HF_SolVin3.Value;
                //NombreHojaPdf = e.File.GetNameWithoutExtension().ToString();

                //if (e.IsValid)
                //{
                //    ValidFiles.Visible = true;
                //    ValidFiles.Controls.Add(fileName);

                //}
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }
        #endregion archivo 3

        #region archivo 4
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreOriginalPDF4.Text;



                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                lblNombreArchivoPDF4.Text = "";
                lblNombreOriginalPDF4.Text = "";
                this.HF_SolVin4.Value = "";
                buttonSubmitpdf4.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportarPDF4_Click(object sender, EventArgs e)
        {
            try
            {


                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin4.Value;

                foreach (UploadedFile f in RadUploadpdf4.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    buttonSubmitpdf4.Visible = false;
                }



            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }
        protected void RadAsyncUpload4_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                this.HF_SolVin4.Value = e.File.FileName;
                NombreArchivopdf4 = e.File.GetNameWithoutExtension().ToString() + "A4" + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                lblNombreArchivoPDF4.Text = NombreArchivopdf4;
                lblNombreOriginalPDF4.Text = this.HF_SolVin4.Value;
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }
        #endregion archivo 4


        #region archivo 5
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreOriginalPDF5.Text;



                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
                lblNombreArchivoPDF5.Text = "";
                lblNombreOriginalPDF5.Text = "";
                this.HF_SolVin5.Value = "";
                buttonSubmitpdf5.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportarPDF5_Click(object sender, EventArgs e)
        {
            try
            {


                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin5.Value;

                foreach (UploadedFile f in RadUploadpdf5.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    buttonSubmitpdf5.Visible = false;
                }



                //try
                //{
                //    File.Delete(path);
                //}
                //catch
                //{
                //}

            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }
        protected void RadAsyncUpload5_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                this.HF_SolVin5.Value = e.File.FileName;
                NombreArchivopdf5 = e.File.GetNameWithoutExtension().ToString() + "A5" + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                lblNombreArchivoPDF5.Text = NombreArchivopdf5;
                lblNombreOriginalPDF5.Text = this.HF_SolVin5.Value;
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }
        #endregion archivo 5



        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();


                pagina.Url = "CapGestionPrecios_Solicitud.aspx?Unq=0";

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                this.Title = "Información";
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                //if (ConsultarAutorizacionPrecio() == "True")
                //{

                //    this.rg1.Columns[11].Visible = true ;
                //}

                //else
                //{
                //    this.rg1.Columns[11].Visible = false;

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = this.lblNombreArchivoPDF.Text;

                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);

                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void btnDescargarPDF2_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = this.lblNombreArchivoPDF2.Text;

                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);

                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void btnDescargarPDF3_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = this.lblNombreArchivoPDF3.Text;

                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);

                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void btnDescargarPDF4_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = this.lblNombreArchivoPDF4.Text;

                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);

                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void btnDescargarPDF5_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = this.lblNombreArchivoPDF5.Text;

                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);

                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void Consultar()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

            CN_Convenio cn_conv = new CN_Convenio();


            List<CapGestionPrecioInformacion> List = new List<CapGestionPrecioInformacion>();

            cn_conv.ConsultarInformacionConvenio(sesion.Id_Emp, -1, ref List, Conexion);
            string pathDestino = "";
            string pathOrigen = "";
            string pathDestino2 = "";
            string pathOrigen2 = "";
            string pathDestino3 = "";
            string pathOrigen3 = "";
            string pathDestino4 = "";
            string pathOrigen4 = "";
            string pathDestino5 = "";
            string pathOrigen5 = "";

            buttonSubmitpdf.Visible = true;
            buttonSubmitpdf2.Visible = true;
            buttonSubmitpdf3.Visible = true;
            buttonSubmitpdf4.Visible = true;
            buttonSubmitpdf5.Visible = true;



            if (List.Count > 0)
            {




                foreach (CapGestionPrecioInformacion lista in List)
                {

                    //string pathDestino = Server.MapPath("~/CarpetaConvenios") + "\\" + lblNombreArchivoPDF.Text;
                    //string pathOrigen = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin.Value;

                    if (lista.Gestion_Tipo == 1)
                    {

                        //HF_Solvin es el nombre del archivo 
                        //lblNombreArchivoPDF es como se llama en la carpeta destino
                        //lblNombreOriginalPDF es el nombre que tenia cuando lo subi

                        this.HF_SolVin.Value = lista.Gestion_NombreArchivo;
                        lblInfo.Text = lista.Gestion_Descripcion; //"¿Cómo realizar las solicitudes de vinculación?";
                        lista.Gestion_NombreArchivo = this.HF_SolVin.Value;
                        //    informacion.Gestion_RutaArchivo = pathDestino;


                        pathDestino = lista.Gestion_RutaArchivo;
                        pathOrigen = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lista.Gestion_NombreArchivo;


                        int longitud = lista.Gestion_RutaArchivo.Length;
                        int l = lista.Gestion_RutaArchivo.IndexOf("CarpetaConvenios");
                        if (l > 0)
                        {
                            lblNombreArchivoPDF.Text = lista.Gestion_RutaArchivo.Substring(l + 17, longitud - l - 17);
                        }

                        buttonSubmitpdf.Visible = false;
                        lblNombreOriginalPDF.Text = lista.Gestion_NombreArchivo;
                        lblNombreOriginalPDF.Visible = true;
                        buttonDescargarpdf.Text = lblNombreOriginalPDF.Text;

                    }

                    if (lista.Gestion_Tipo == 2)
                    {

                        this.HF_SolVin2.Value = lista.Gestion_NombreArchivo;
                        lblInfo2.Text = lista.Gestion_Descripcion; //"¿Cómo realizar las solicitudes de vinculación?";
                        lista.Gestion_NombreArchivo = this.HF_SolVin2.Value;
                        //    informacion.Gestion_RutaArchivo = pathDestino;
                        lblNombreArchivoPDF2.Text = lista.Gestion_RutaArchivo;

                        pathDestino = lista.Gestion_RutaArchivo;
                        pathOrigen = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lista.Gestion_NombreArchivo;

                        int longitud = lista.Gestion_RutaArchivo.Length;
                        int l = lista.Gestion_RutaArchivo.IndexOf("CarpetaConvenios");

                        if (l > 0)
                        {
                            lblNombreArchivoPDF2.Text = lista.Gestion_RutaArchivo.Substring(l + 17, longitud - l - 17);
                        }
                        buttonSubmitpdf2.Visible = false;
                        lblNombreOriginalPDF2.Text = lista.Gestion_NombreArchivo;
                        lblNombreOriginalPDF2.Visible = true;
                        buttonDescargarpdf2.Text = lista.Gestion_NombreArchivo;

                    }

                    if (lista.Gestion_Tipo == 3)
                    {

                        this.HF_SolVin3.Value = lista.Gestion_NombreArchivo;
                        lblInfo3.Text = lista.Gestion_Descripcion; //"¿Cómo realizar las solicitudes de vinculación?";
                        lista.Gestion_NombreArchivo = this.HF_SolVin3.Value;
                        //    informacion.Gestion_RutaArchivo = pathDestino;
                        lblNombreArchivoPDF3.Text = lista.Gestion_RutaArchivo;

                        pathDestino3 = lista.Gestion_RutaArchivo;
                        pathOrigen3 = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lista.Gestion_NombreArchivo;

                        int longitud = lista.Gestion_RutaArchivo.Length;
                        int l = lista.Gestion_RutaArchivo.IndexOf("CarpetaConvenios");
                        if (l > 0)
                        {
                            lblNombreArchivoPDF3.Text = lista.Gestion_RutaArchivo.Substring(l + 17, longitud - l - 17);
                        }
                        buttonSubmitpdf3.Visible = false;
                        lblNombreOriginalPDF3.Text = lista.Gestion_NombreArchivo;
                        lblNombreOriginalPDF3.Visible = true;
                        buttonDescargarpdf3.Text = lista.Gestion_NombreArchivo;

                    }

                    if (lista.Gestion_Tipo == 4)
                    {

                        this.HF_SolVin4.Value = lista.Gestion_NombreArchivo;
                        lblInfo4.Text = lista.Gestion_Descripcion; //"¿Cómo realizar las solicitudes de vinculación?";
                        lista.Gestion_NombreArchivo = this.HF_SolVin4.Value;
                        //    informacion.Gestion_RutaArchivo = pathDestino;
                        lblNombreArchivoPDF4.Text = lista.Gestion_RutaArchivo;

                        pathDestino4 = lista.Gestion_RutaArchivo;
                        pathOrigen4 = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lista.Gestion_NombreArchivo;

                        int longitud = lista.Gestion_RutaArchivo.Length;
                        int l = lista.Gestion_RutaArchivo.IndexOf("CarpetaConvenios");
                        if (l > 0)
                        {
                            lblNombreArchivoPDF4.Text = lista.Gestion_RutaArchivo.Substring(l + 17, longitud - l - 17);
                        }
                        buttonSubmitpdf4.Visible = false;
                        lblNombreOriginalPDF4.Text = lista.Gestion_NombreArchivo;
                        lblNombreOriginalPDF4.Visible = true;
                        buttonDescargarpdf4.Text = lista.Gestion_NombreArchivo;
                    }
                    if (lista.Gestion_Tipo == 5)
                    {

                        this.HF_SolVin5.Value = lista.Gestion_NombreArchivo;
                        lblInfo5.Text = lista.Gestion_Descripcion; //"¿Cómo realizar las solicitudes de vinculación?";
                        lista.Gestion_NombreArchivo = this.HF_SolVin5.Value;
                        //    informacion.Gestion_RutaArchivo = pathDestino;
                        lblNombreArchivoPDF5.Text = lista.Gestion_RutaArchivo;

                        pathDestino5 = lista.Gestion_RutaArchivo;
                        pathOrigen5 = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lista.Gestion_NombreArchivo;

                        int longitud = lista.Gestion_RutaArchivo.Length;
                        int l = lista.Gestion_RutaArchivo.IndexOf("CarpetaConvenios");
                        if (l > 0)
                        {
                            lblNombreArchivoPDF5.Text = lista.Gestion_RutaArchivo.Substring(l + 17, longitud - l - 17);
                        }
                        buttonSubmitpdf5.Visible = false;
                        lblNombreOriginalPDF5.Text = lista.Gestion_NombreArchivo;
                        lblNombreOriginalPDF5.Visible = false;
                        buttonDescargarpdf5.Text = lista.Gestion_NombreArchivo;
                    }

                }
 
               
            }
        }



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
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                this.lblMensaje.Text = "";
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
                this.lblMensaje.Text = Message;
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
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

    }
}