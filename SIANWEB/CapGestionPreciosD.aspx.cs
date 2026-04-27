using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using Telerik.Reporting.Processing;
using System.Data.OleDb;
using Telerik.Web.UI.GridExcelBuilder;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.ComponentModel;

namespace SIANWEB
{
    public partial class CapGestionPreciosD : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;

        public string NombreArchivopdf;
        public string NombreHojaPdf;

        private List<ConvenioDet> ListDet
        {
            get { return (List<ConvenioDet>)Session["ListDet" + Session.SessionID + HF_Cve.Value]; }
            set { Session["ListDet" + Session.SessionID + HF_Cve.Value] = value; }
        }
        public bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                { return false; }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        public bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                { return false; }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
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

        public DataTable dt_detalles
        {
            get
            {
                return Session["dt_DetallesRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_DetallesRem" + Session.SessionID] = value;
            }
        }

        private byte[] PagElec_Soporte4 = null;


        //public DataTable dt_cuentaOriginal
        //{
        //    get
        //    {
        //        return Session["dt_cuentaOriginalRem" + Session.SessionID] as DataTable;
        //    }
        //    set
        //    {
        //        Session["dt_cuentaOriginalRem" + Session.SessionID] = value;
        //    }
        //}

        //static int id_detalle = 0;
        private int id_detalle
        {
            set { Session["id_detalleREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["id_detalleREM" + Session.SessionID]; return st == null ? 0 : (int)st; }
        }
        //static int Id_ConvDet_A = -1; //id de la partida que se va actualizar
        private int Id_ConvDet_A
        {
            set { Session["Id_ConvDet_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["Id_ConvDet_AREM" + Session.SessionID]; return st == null ? -1 : (int)st; }
        }
        //static int cantidad_A = 0; //cantidad de la partida que se va actualizar
        private int cantidad_A
        {
            set { Session["cantidad_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["cantidad_AREM" + Session.SessionID]; return st == null ? 0 : (int)st; }
        }
        //static double costo_A = -1; //costo de la partida que se va actualizar
        private double costo_A
        {
            set { Session["costo_AREM" + Session.SessionID] = value; }
            get { double? st = (double?)Session["costo_AREM" + Session.SessionID]; return st == null ? -1 : (double)st; }
        }
        //static int Id_Prd_A;
        private int Id_Prd_A
        {
            set { Session["Id_Prd_AREM" + Session.SessionID] = value; }
            get { return (int)Session["Id_Prd_AREM" + Session.SessionID]; }
        }

        //JFCV TODO eliminar lo que no ocupe 

        #endregion Variables

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
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        HFId_PC.Value = Page.Request.QueryString["Id_PC"].ToString();
                        this.TxtKeyConvenio.Text = Page.Request.QueryString["Id_PC"].ToString();
                        HFTipoOp.Value = Page.Request.QueryString["TipoOp"].ToString();
                        ValidarPermisos();
                        this.CargarCentros();
                        CargarCategoria();
                        CargarUsuarios();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;
                        List<ConvenioDet> List = new List<ConvenioDet>();

                        this.TxtPC_FechaCreo.Text = DateTime.Now.ToString();
                        this.TxtPC_FechaUltMod.Text = DateTime.Now.ToString();
                        this.TxtPC_FechaInicioConv.SelectedDate = DateTime.Now;


                        crearDT();
                        if (this.HFId_PC.Value != "0")
                        {
                            this.rgDetalles.DataSource = ConsultaConvenio();
                            rgDetalles.DataBind();
                            rgDetalles.Rebind();
                        }
                        else
                        {
                          
                            this.TxtKeyConvenio.Text = MaximoId();
                            rgDetalles.DataSource = dt_detalles;
                            rgDetalles.Rebind();
                        }
                       


                        //////rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                        //////rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));



                    }

                     
                }

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos

        //rg convenio
        protected void rgDetalles_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgDetalles.Rebind();

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
                        rgDetalles.Rebind();
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
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (Page.IsValid)
                {

                    
                    if (btn.CommandName == "imprimir")
                    {
                  
                        Imprimir(Convert.ToInt32(this.TxtKeyConvenio.Text));
                         
                    }


                    if (btn.CommandName == "guardar")
                    {

                        if (int.Parse(HFId_PC.Value) == 0 && int.Parse(HFTipoOp.Value) == 0)
                        {
                            if (!_PermisoGuardar)
                            {
                                Alerta("No tiene permiso para guardar");
                                return;
                            }

                            GuardarAviso();
                        }
                        else if (int.Parse(HFId_PC.Value) != 0 && int.Parse(HFTipoOp.Value) == 0)
                        {
                            if (!_PermisoModificar)
                            {
                                Alerta("No tiene permiso para modificar");
                                return;
                            }

                            ConfirmarModificacion();
                        }
                        else if (int.Parse(HFId_PC.Value) != 0 && int.Parse(HFTipoOp.Value) == 1)
                        {
                            if (!_PermisoGuardar)
                            {
                                Alerta("No tiene permiso para guardar");
                                return;
                            }

                            GuardarAviso();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void CmbId_Cat_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                 

                //if (CmbId_Cat.SelectedValue != "-1")
                //{
                //    CargaConsecutivo();
                //}
                //else
                //{
                //    this.TxtPC_NoConvenio.Text = string.Empty;
                //    TxtPC_NoConvenio.Enabled = true;
                //}

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {

                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();

                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);

                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void RadAsyncUpload1_FileUploadedpdf(object sender, FileUploadedEventArgs e)
        {
            try
            {

                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivopdf = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                NombreHojaPdf = e.File.GetNameWithoutExtension().ToString();

                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);

                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }



        }

        private void BulkCopy(string path, string hoja)
        {
            try
            {
                //'Declare Variables - Edit these based on your particular situation
                String sSQLTable = "TempTableForExcelImport";



                //'Create our connection strings
                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES\";";
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                String sSqlConnectionString = Sesion.Emp_Cnx;


                //'Series of commands to bulk copy data from the excel file into our SQL table
                OleDbConnection OleDbConn = new OleDbConnection(strConn);
                OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM [" + hoja + "]"), OleDbConn);
                OleDbConn.Open();
                OleDbDataReader dr = OleDbCmd.ExecuteReader();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sSqlConnectionString);
                bulkCopy.DestinationTableName = sSQLTable;
                bulkCopy.WriteToServer(dr);
                OleDbConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<ConvenioDet> List = new List<ConvenioDet>();
                CargarGrid(ref List);
                //this.rg ConvenioDet.DataSource = List;
                //rg ConvenioDet.DataBind();
                this.rgDetalles.DataSource = List;
                this.rgDetalles.DataBind();


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



                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivopdf;
                foreach (UploadedFile f in RadUploadpdf.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                    //conv.PC_NombreArchivoPDF = f.FileName;
                    //conv.PC_ArchivoPDF = f.FileName;

                }

                //Label9.Text = "";
                //Label3.Text = "";
                //Label7.Text = "";
                //    Label9.Text = "";


                // Guardar el nombre del archivo PDF en la variable para luego guardarlo o 
                // Bajarlo a pdf 
                lblNombreArchivoPDF.Text = NombreArchivopdf;
                PagElec_Soporte4 = ConvertirFileToByteArray(path);

                //string s1 = Encoding.UTF8.GetString(PagElec_Soporte4);
                string s3 = Convert.ToBase64String(PagElec_Soporte4);
                lblContenidoPDF.Text = s3;

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }



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


        public static byte[] ConvertirFileToByteArray(string ruta)
        {

            FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            /*Create a byte array of file stream length*/
            byte[] b = new byte[fs.Length];
            /*Read block of bytes from stream into the byte array*/
            fs.Read(b, 0, System.Convert.ToInt32(fs.Length));
            /*Close the File Stream*/
            fs.Close();

            return b;
        }

        protected void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            try
            {


                NombreArchivopdf = lblNombreArchivoPDF.Text;
                //string path = Server.MapPath("~/App_Data") + "\\" + NombreArchivopdf;

              
            
                byte[] decByte3 = Convert.FromBase64String(lblContenidoPDF.Text);
                PagElec_Soporte4 = decByte3;

                      if (PagElec_Soporte4 != null)
                {
                    if (  NombreArchivopdf != "")
                    {
                        if (PagElec_Soporte4.Length > 0)
                        {
                            //string tempPDFname = string.Concat("Convenio"
                            //         , "_", NombreArchivopdf.ToString());
                            ////, ".pdf");


                            //string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            //string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                            //Console.WriteLine( WebURLtempPDFGastos);
                            //Console.WriteLine("URLtempPDF " + URLtempPDF); 

                            //this.ByteToTempPDF(URLtempPDF, PagElec_Soporte4);





                            ////////// ------------------------------------------------------------------------------------------------
                            ////////// Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                            ////////// ------------------------------------------------------------------------------------------------
                            ////////RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));


                            ////////string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin5.Value;
                            ////////string pathDestino = Server.MapPath("~/CarpetaConvenios") + "\\" + lblNombreArchivoPDF.Text;
                            ////////string pathOrigen = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + this.HF_SolVin.Value;

                            ////// NombreArchivopdf = this.lblNombreArchivoPDF.Text;
                            ////////string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), NombreArchivopdf));


                            ////////string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), NombreArchivopdf);
                            ////////this.ByteToTempPDF(WebURLtempPDFGastos, PagElec_Soporte4);
                            ////////// ------------------------------------------------------------------------------------------------
                            ////////// Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                            ////////// ------------------------------------------------------------------------------------------------
                            ////////RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));

                            ////// string tempPDFname = string.Concat("Convenio", NombreArchivopdf);
                            //////string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            //////string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);


                            //////this.ByteToTempPDF(URLtempPDF, PagElec_Soporte4);

                            //////RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));

 

                            //PRUEBA ESTE CODIGO 
                                string tempPDFname = string.Concat("Convenio"
                                         , "_", NombreArchivopdf.ToString());
                                //, ".pdf");
                                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + tempPDFname;


                                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                                this.ByteToTempPDF(URLtempPDF, PagElec_Soporte4);

                                

                                //this.ByteToTempPDF(path, PagElec_Soporte4);
                                //string WebURLtempPDF = path;

                                // ------------------------------------------------------------------------------------------------
                                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                                // ------------------------------------------------------------------------------------------------
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirArchivoPDF('", WebURLtempPDFGastos, "')"));

 



                        }
                    }
                }
                else
                {
                    Alerta("El convenio  no tiene un archivo PDF.");
                }



            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarExcel();

            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }
        }

        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private void Borrar(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                string Id_Prd;
                Id_Prd = this.rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].Text;

                ListDet.Remove(ListDet.Where(x => x.Id_Prd == int.Parse(Id_Prd)).ToList()[0]);

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion Eventos

        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                pagina.Url = "CapGestionPreciosP.aspx";

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                _PermisoGuardar = Permiso.PGrabar;
                _PermisoModificar = Permiso.PModificar;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategoria()
        {
            try
            {

                CN__Comun cn_comun = new CN__Comun();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_comun.LlenaCombo(Conexion, "spCatConvCategoria_Combo", ref  CmbId_Cat);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                //cn_comun.LlenaCombo(sesion.Id_Cd_Ver , 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_ULider);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UEjecutivo);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //private void CargaConsecutivo()
        //{
        //    try
        //    {
        //        //CN_Convenio cn_conv = new CN_Convenio();
        //        //Convenio conv = new Convenio();
        //        //string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

        //        //cn_conv.ConsultaConsecutivo(int.Parse(CmbId_Cat.SelectedValue), ref conv, Conexion);

        //        //TxtPC_NoConvenio.Enabled = true;

        //        //this.HFCat_Consecutivo.Value = conv.Cat_Consecutivo.ToString();
        //        //if (conv.Cat_Consecutivo == 1)
        //        //{
        //        //    TxtPC_NoConvenio.Text = conv.PC_NoConvenio;
        //        //    //jfcv no se debe desabilitar el campo de convenio
        //        //    TxtPC_NoConvenio.Enabled = true;
        //        //    //TxtPC_NoConvenio.Enabled = false;
        //        //}
        //        //else
        //        //{
        //        //    this.TxtPC_NoConvenio.Text = string.Empty;
        //        //    TxtPC_NoConvenio.Enabled = true;

        //        //}


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        private void CargarGrid(ref List<ConvenioDet> List)
        {
            try
            {
                OleDbConnection con = default(OleDbConnection);
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                }

                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);

                con.Close();
                con.Open();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");

                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dad.Fill(ds);

                int contadorcolumnas = 0;
                int ClaveKey = 0, PVentaMin = 0, PVentaMax = 0, PAAAEsp = 0,
                    PAAAEspProveedor = 0, PVentaConvenio = 0, FechaInicio = 0, FechaFin = 0, RefConvenio = 0;

                foreach (DataColumn d in ds.Tables[0].Columns)
                {

                    string campo = d.ColumnName.ToString();
                    if (campo == "ClaveKey")
                    {
                        ClaveKey = contadorcolumnas;
                    }
                    if (campo == "PVentaMin")
                    {
                        PVentaMin = contadorcolumnas;
                    }
                    if (campo == "PVentaMax")
                    {
                        PVentaMax = contadorcolumnas;
                    }
                    if (campo == "PAAAEsp")
                    {
                        PAAAEsp = contadorcolumnas;
                    }
                    if (campo == "PAAAEspProveedor")
                    {
                        PAAAEspProveedor = contadorcolumnas;
                    }
                    if (campo == "PVentaConvenio")
                    {
                        PVentaConvenio = contadorcolumnas;
                    }


                    if (campo == "FechaInicio")
                    {
                        FechaInicio = contadorcolumnas;
                    }
                    if (campo == "FechaFin")
                    {
                        FechaFin = contadorcolumnas;
                    }
                    if (campo == "RefConvenio")
                    {
                        RefConvenio = contadorcolumnas;
                    }


                    string encabezado = d.Caption.ToString();
                    contadorcolumnas++;
                }

                if (PAAAEsp == 0 || PAAAEspProveedor == 0 || PVentaConvenio == 0 || FechaInicio == 0 || FechaFin == 0 || RefConvenio == 0)
                {
                    Alerta("El formato no es correcto revise los nombres del as columnas ");
                    return;
                }


                if (ListDet != null)
                {
                    List = ListDet;
                }

                int contador = 0;

                ConvenioDet c;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    contador++;
                    c = new ConvenioDet();
                    if (dr[0].ToString() != "")
                    {
                        //Entidad ent = entidades.FirstOrDefault(o => o.Nombre == label5.Text);
                        c = List.FirstOrDefault(o => o.Id_Prd == Convert.ToInt32(dr[0]));

                        if (c != null)
                        {

                            if (PVentaMin == 0 || PVentaMax == 0)
                            {

                                if (!string.IsNullOrEmpty(dr[PVentaConvenio].ToString()))
                                {

                                    c.PCD_PrecioVtaMin = Convert.ToDouble(dr[PVentaConvenio]) - 0.05;
                                    c.PCD_PrecioVtaMax = Convert.ToDouble(dr[PVentaConvenio]) + 0.05;


                                }

                            }
                            else
                            {

                                c.PCD_PrecioVtaMin = string.IsNullOrEmpty(dr[PVentaMin].ToString()) ? 0 : Convert.ToDouble(dr[PVentaMin]);
                                c.PCD_PrecioVtaMax = string.IsNullOrEmpty(dr[PVentaMax].ToString()) ? 0 : Convert.ToDouble(dr[PVentaMax]);

                            }


                            //Anterior:la fecha fin es menor a la fecha actual
                            c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDouble(dr[PAAAEsp]) : c.PCD_PrecioAAAEspA;
                            c.PCD_FechaInicioA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDateTime(dr[FechaInicio]) : c.PCD_FechaInicioA;
                            c.PCD_FechaFinA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDateTime(dr[FechaFin]) : c.PCD_FechaFinA;
                            //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
                            c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDouble(dr[PAAAEsp]) : c.PCD_PrecioAAAEsp;
                            c.PCD_FechaInicio = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDateTime(dr[FechaInicio]) : c.PCD_FechaInicio;
                            c.PCD_FechaFin = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDateTime(dr[FechaFin]) : c.PCD_FechaFin;
                            //Futuro: La fecha inicio es mayor a la fecha actual 
                            c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDouble(dr[PAAAEsp]) : c.PCD_PrecioAAAEspC;
                            c.PCD_FechaInicioC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDateTime(dr[FechaInicio]) : c.PCD_FechaInicioC;
                            c.PCD_FechaFinC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDateTime(dr[FechaFin]) : c.PCD_FechaFinC;

                            c.PCD_Referencia = dr[RefConvenio].ToString();
                            //agregar 3 campos nuevos 
                            c.PCD_PrecioPAAAEspProv = Convert.ToDouble(dr[PAAAEspProveedor]);
                            c.PCD_PrecioVentaConvenio = Convert.ToDouble(dr[PVentaConvenio]);
                            //c.PCD_Margen = Convert.ToDouble(dr[Margen]);
                            if (!string.IsNullOrEmpty(c.PCD_PrecioVentaConvenio.ToString()) && c.PCD_PrecioAAAEsp != 0)
                            {
                                c.PCD_Margen = (1 - (c.PCD_PrecioAAAEsp / c.PCD_PrecioVentaConvenio)) * 100;

                            }
                            else
                            {
                                c.PCD_Margen = 0;
                            }

                            //// JFCV 23mzo2019 la fecha fin ver que sea igual a fechafin
                            ////if (c.PCD_FechaFinC != (DateTime?)null)
                            ////{
                            ////    c.PCD_FechaFinVer = c.PCD_FechaFinC;
                            ////}
                            ////else if (c.PCD_FechaFin != (DateTime?)null)
                            ////{
                            ////    c.PCD_FechaFinVer = c.PCD_FechaFin;
                            ////}
                            ////else
                            ////{
                            ////    c.PCD_FechaFinVer = c.PCD_FechaFinA;
                            ////}
                            //
                            c.PCD_FechaFinVer = c.PCD_FechaFin;

                        }
                        else
                        {
                            c = new ConvenioDet();
                            c.Id_Prd = Convert.ToInt32(dr[ClaveKey]);
                            c.PCD_ClaveProv = ProductoClaveProveedor(Convert.ToInt32(dr[ClaveKey]));

                            c.Prd_Descripcion = DescripcionProducto(Convert.ToInt32(dr[ClaveKey]));
                            //if (PVentaMin > 0 && PVentaMax > 0)
                            //{
                            //    c.PCD_PrecioVtaMin = Convert.ToDouble(dr[PVentaMin]);
                            //    c.PCD_PrecioVtaMax = Convert.ToDouble(dr[PVentaMax]);
                            //}

                            if (PVentaMin == 0 || PVentaMax == 0)
                            {

                                if (!string.IsNullOrEmpty(dr[PVentaConvenio].ToString()))
                                {
                                    c.PCD_PrecioVtaMin = Convert.ToDouble(dr[PVentaConvenio]) - 0.05;
                                    c.PCD_PrecioVtaMax = Convert.ToDouble(dr[PVentaConvenio]) + 0.05;
                                }

                            }
                            else
                            {

                                c.PCD_PrecioVtaMin = string.IsNullOrEmpty(dr[PVentaMin].ToString()) ? 0 : Convert.ToDouble(dr[PVentaMin]);
                                c.PCD_PrecioVtaMax = string.IsNullOrEmpty(dr[PVentaMax].ToString()) ? 0 : Convert.ToDouble(dr[PVentaMax]);

                            }



                            //Anterior:la fecha fin es menor a la fecha actual
                            c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDouble(dr[PAAAEsp]) : (Double?)null;
                            c.PCD_FechaInicioA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDateTime(dr[FechaInicio]) : (DateTime?)null;
                            c.PCD_FechaFinA = Convert.ToDateTime(dr[FechaFin]) < DateTime.Now ? Convert.ToDateTime(dr[FechaFin]) : (DateTime?)null;
                            //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
                            c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDouble(dr[PAAAEsp]) : (Double?)null;
                            c.PCD_FechaInicio = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDateTime(dr[FechaInicio]) : (DateTime?)null;
                            c.PCD_FechaFin = (Convert.ToDateTime(dr[FechaInicio]) <= DateTime.Now && Convert.ToDateTime(dr[FechaFin]) >= DateTime.Now) ? Convert.ToDateTime(dr[FechaFin]) : (DateTime?)null;
                            //Futuro: La fecha inicio es mayor a la fecha actual 
                            c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDouble(dr[PAAAEsp]) : (Double?)null;
                            c.PCD_FechaInicioC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDateTime(dr[FechaInicio]) : (DateTime?)null;
                            c.PCD_FechaFinC = Convert.ToDateTime(dr[FechaInicio]) > DateTime.Now ? Convert.ToDateTime(dr[FechaFin]) : (DateTime?)null; ;

                            c.PCD_Referencia = dr[RefConvenio].ToString();
                            //agregar 3 campos nuevos 
                            c.PCD_PrecioPAAAEspProv = Convert.ToDouble(dr[PAAAEspProveedor]);
                            c.PCD_PrecioVentaConvenio = Convert.ToDouble(dr[PVentaConvenio]);
                            //c.PCD_Margen = Convert.ToDouble(dr[Margen]);
                            if (!string.IsNullOrEmpty(c.PCD_PrecioVentaConvenio.ToString()) && c.PCD_PrecioAAAEsp != 0)
                            {
                                c.PCD_Margen = (1 - (c.PCD_PrecioAAAEsp / c.PCD_PrecioVentaConvenio)) * 100;

                            }
                            else
                            {
                                c.PCD_Margen = 0;
                            }

                            //// JFCV 23mzo2019 la fecha fin ver que sea igual a fechafin
                            //if (c.PCD_FechaFinC != (DateTime?)null)
                            //{
                            //    c.PCD_FechaFinVer = c.PCD_FechaFinC;
                            //}
                            //else if (c.PCD_FechaFin != (DateTime?)null)
                            //{
                            //    c.PCD_FechaFinVer = c.PCD_FechaFin;
                            //}
                            //else
                            //{
                            //    c.PCD_FechaFinVer = c.PCD_FechaFinA;
                            //}
 
                            c.PCD_FechaFinVer = c.PCD_FechaFin;

                            c.Id_ConvDet = contador;
                            List.Add(c);
                        }
                    }

                }


                ListDet = List;

                //lblMensaje.Text = "cerrada la conexion|";
                //BulkCopy(path, hoja);
                ////lblMensaje.Text = "En base de datos|";
                //GuardarDeExcel();
                ////lblMensaje.Text = "Finalizado";

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }


            ////c.PCD_CantidadMax = Convert.ToInt32(dr[4]);
            ////c.Id_Moneda = Convert.ToInt32(dr[5]);
            ////c.Id_MonedaStr = Convert.ToInt32(dr[5]) == 1 ? "Dlls" : "Mx";
            ////c.PCD_PrecioAAAEsp = Convert.ToDouble(dr[6]);
            ////c.PCD_FechaInicio = Convert.ToDateTime(dr[7]);
            ////Anterior:la fecha fin es menor a la fecha actual
            //c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDouble(dr[3]) : c.PCD_PrecioAAAEspA;
            //c.PCD_FechaInicioA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicioA;
            //c.PCD_FechaFinA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFinA;
            ////Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
            //c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDouble(dr[3]) : c.PCD_PrecioAAAEsp;
            //c.PCD_FechaInicio = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicio;
            //c.PCD_FechaFin = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFin;
            ////Futuro: La fecha inicio es mayor a la fecha actual 
            //c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDouble(dr[3]) : c.PCD_PrecioAAAEspC;
            //c.PCD_FechaInicioC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[7]) : c.PCD_FechaInicioC;
            //c.PCD_FechaFinC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[8]) : c.PCD_FechaFinC;
            ////c.PCD_CatDesp = dr[9].ToString();
            //c.PCD_Referencia = dr[9].ToString();
            ////agregar 3 campos nuevos 
            //c.PCD_PrecioPAAAEspProv = Convert.ToDouble(dr[4]);
            //c.PCD_PrecioVentaConvenio = Convert.ToDouble(dr[5]);
            //c.PCD_Margen = Convert.ToDouble(dr[6]);
            //}
            //           else
            //           {
            //               c = new ConvenioDet();
            //               c.Id_Prd = Convert.ToInt32(dr[0]);
            //               c.PCD_ClaveProv = dr[1].ToString(); //JFCV TODO
            //               c.Prd_Descripcion = DescripcionProducto(Convert.ToInt32(dr[0]));
            //               c.PCD_PrecioVtaMin = Convert.ToDouble(dr[1]);
            //               c.PCD_PrecioVtaMax = Convert.ToDouble(dr[2]);
            //               //JFCV c.PCD_CantidadMax = Convert.ToInt32(dr[4]);
            //               //c.Id_Moneda = Convert.ToInt32(dr[5]);
            //               //c.Id_MonedaStr = Convert.ToInt32(dr[5]) == 1 ? "Dlls" : "Mx";
            //               //c.PCD_PrecioAAAEsp = Convert.ToDouble(dr[6]);
            //               //c.PCD_FechaInicio = Convert.ToDateTime(dr[7]);
            //               //Anterior:la fecha fin es menor a la fecha actual
            //               c.PCD_PrecioAAAEspA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDouble(dr[3]) : (Double?)null;
            //               c.PCD_FechaInicioA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
            //               c.PCD_FechaFinA = Convert.ToDateTime(dr[8]) < DateTime.Now ? Convert.ToDateTime(dr[8]) : (DateTime?)null;
            //               //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
            //               c.PCD_PrecioAAAEsp = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDouble(dr[3]) : (Double?)null;
            //               c.PCD_FechaInicio = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
            //               c.PCD_FechaFin = (Convert.ToDateTime(dr[7]) <= DateTime.Now && Convert.ToDateTime(dr[8]) >= DateTime.Now) ? Convert.ToDateTime(dr[8]) : (DateTime?)null;
            //               //Futuro: La fecha inicio es mayor a la fecha actual 
            //               c.PCD_PrecioAAAEspC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDouble(dr[3]) : (Double?)null;
            //               c.PCD_FechaInicioC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[7]) : (DateTime?)null;
            //               c.PCD_FechaFinC = Convert.ToDateTime(dr[7]) > DateTime.Now ? Convert.ToDateTime(dr[8]) : (DateTime?)null; ;
            //               //c.PCD_CatDesp = dr[9].ToString();
            //               c.PCD_Referencia = dr[9].ToString();
            //               //agregar 3 campos nuevos 
            //               c.PCD_PrecioPAAAEspProv = Convert.ToDouble(dr[4]);
            //               c.PCD_PrecioVentaConvenio = Convert.ToDouble(dr[5]);
            //               c.PCD_Margen = Convert.ToDouble(dr[6]);

        }
        private string DescripcionProducto(int Id_Prd)
        {
            try
            {
                string Prd_Descripcion = string.Empty;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_prd = new CN_CatProducto();
                cn_prd.ConsultaProducto_Descripcion(Id_Prd, ref Prd_Descripcion, sesion.Emp_Cnx);



                return Prd_Descripcion;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string ProductoClaveProveedor(int Id_Prd)
        {
            try
            {
                string Prd_ClaveProveedor = string.Empty;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_prd = new CN_CatProducto();
                cn_prd.ConsultaProducto_ClaveProveedor(Id_Prd, ref Prd_ClaveProveedor, sesion.Emp_Cnx);



                return Prd_ClaveProveedor;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void GuardarAviso()
        {
            //antes de grabar primero pregunto si desea hacerlo 


            try
            {
                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;

                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;

                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();

                CN_Convenio cn_conv = new CN_Convenio();

                int Id_PC = 0;

                LlenarEncabezado(ref conv, sesion.Id_U);

                HFId_PC.Value = Id_PC.ToString();
                string elmensajees = "";

                elmensajees = "Convenio Key: " + TxtKeyConvenio.Text + "<br>";
                elmensajees = elmensajees + "Convenio proveedor: " + conv.PC_NoConvenio.ToString() + "<br>";
                elmensajees = elmensajees + "Nombre de convenio: " + conv.PC_Nombre.ToString() + "<br>";

                RAM1.ResponseScripts.Add("return  callConfirm('Desea guardar los cambios realizados al convenio? <br>" + elmensajees + "')");

                //Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
                //AlertaFocus("Inventario disponible insuficiente, <br>Inventario final: " + Actuales[0] + " <br>Asignado: " + Actuales[1] + " <br>Disponible: " + Actuales[2], ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).ClientID);



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Guardar()
        {
            try
            {
                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;




                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;

                }


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Convenio conv = new Convenio();

                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;
                int Id_PC = 0;

                LlenarEncabezado(ref conv, sesion.Id_U);

                cn_conv.InsertarConvenio(conv, List, ref Verificador, ref Id_PC, Conexion);

                if (Verificador == -1)
                {

                    if (int.Parse(HFTipoOp.Value) == 0)
                    {
                        //JMM: Envio el correo de creacion
                        //jfcv Aqui ya no envía el correo si no que abre otra pantalla
                        //////cn_conv.ConvenioCreo_EnviarCorreo(Id_PC, sesion.Emp_Cnx);
                        RAM1.ResponseScripts.Add("return OpenWindowVincularSuc('" + Id_PC + "','" + conv.PC_NoConvenio + "','" + conv.PC_Nombre + "','" + CmbId_Cat.SelectedItem + "')");


                        //AlertaCerrar("Los datos se guardaron correctamente");
                    }
                    else
                    {
                        //JMM: Sustituyo los clientes 
                        cn_conv.ConvenioSustituyo_ActualizaClientes(Id_PC, ref Verificador, Conexion);
                        //JMM: Envio el correo de sustitucion
                        //jfcv TODO ENVIAR CORREO 
                        //cn_conv.ConvenioSustituyo_EnviarCorreo(Id_PC, sesion.Emp_Cnx);
                        //AlertaCerrar("Se realizó la sustición correctamente");
                        RAM1.ResponseScripts.Add("return OpenWindowVincularSuc('" + Id_PC + "','" + conv.PC_NoConvenio + "','" + conv.PC_Nombre + "','" + CmbId_Cat.SelectedItem + "')");

                    }

                }
                else
                {
                    if (int.Parse(HFTipoOp.Value) == 0)
                    {
                        Alerta("Error al tratar de guardar convenio");
                    }
                    else
                    {
                        Alerta("Error al tratar de realizar la sustitución");
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LlenarEncabezado(ref Convenio conv, int Id_U)
        {
            try
            {
                conv.PC_NoConvenio = this.TxtPC_NoConvenio.Text;
                conv.PC_Nombre = this.TxtPC_Nombre.Text.Trim();
                conv.Id_Cat = int.Parse(this.CmbId_Cat.SelectedValue);

                conv.Id_UEjecutivo = int.Parse(this.CmbId_UEjecutivo.SelectedValue);
                //conv.PC_Margen = double.Parse(TxtPC_Margen.Text);
                conv.PC_Notas = this.TxtPC_Notas.Text.Trim();
                conv.Cat_Consecutivo = int.Parse(HFCat_Consecutivo.Value);
                conv.Id_PCAnterior = this.HFTipoOp.Value == "1" ? int.Parse(this.HFId_PC.Value) : 0;

                conv.Id_U = Id_U;

                conv.PC_NoConvenioFuturo = this.TxtPC_ConvenioOtro.Text;
                //conv.PC_NoConvenioKey = this.HFTipoOp.Value == "1" ? int.Parse(this.TxtKeyConvenio.Text) : 0;
                conv.PC_NoConvenioKey = this.HFTipoOp.Value == "0" ? int.Parse(this.TxtKeyConvenio.Text) : 0;

                conv.PC_FechaInicio = this.TxtPC_FechaInicioConv.SelectedDate.Value;
                conv.PC_DesplegarMatrizCN = Convert.ToInt32(ChkMatrizCN.Checked);

                //string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivopdf;
                //foreach (UploadedFile f in RadUploadpdf.UploadedFiles)
                //{
                //    if (File.Exists(path))
                //    {
                //        File.Delete(path);
                //    }
                //    f.SaveAs(path, true);
                //    conv.PC_NombreArchivoPDF = f.FileName;
                //    conv.PC_ArchivoPDF = f.FileName;

                //}

                //conv.PC_NombreArchivoPDF = NombreArchivopdf;
                //conv.PC_ArchivoPDF = PagElec_Soporte4.ToString();

                conv.PC_NombreArchivoPDF = lblNombreArchivoPDF.Text;

                conv.PC_ArchivoPDF = lblContenidoPDF.Text;
               

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //private void ConsultaConvenio()
        //{
        //    try
        //    {
        //        string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
        //        CN_Convenio cn_conv = new CN_Convenio();
        //        Convenio conv = new Convenio();
        //        List<ConvenioDet> List = new List<ConvenioDet>();
        //        int Id_PC = int.Parse(HFId_PC.Value);

        //        cn_conv.ConsultaConvenio(Id_PC, ref conv, Conexion);
        //        VaciaEncabezado(conv);
        //        cn_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);
        //        ListDet = List;
        //        rgConvenioDet.DataSource = List;
        //        rgConvenioDet.DataBind();

        //        if (this.HFTipoOp.Value == "1")
        //        {
        //            this.TxtConvAnt.Visible = true;
        //            this.LblConvAnt.Visible = true;
        //            CargaConsecutivo();
        //        }
        //        else
        //        {
        //            this.CmbId_Cat.Enabled = false;
        //            //this.TxtPC_NoConvenio.Enabled = false;
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        private List<ConvenioDet> ConsultaConvenio()
        {
            try
            {
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                List<ConvenioDet> List = new List<ConvenioDet>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_PC = int.Parse(HFId_PC.Value);
                cn_conv.ConsultaConvenio(Id_PC, ref conv, Conexion);
                VaciaEncabezado(conv);
                cn_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);
                return ListDet = List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void VaciaEncabezado(Convenio conv)
        {
            try
            {
                this.CmbId_Cat.SelectedValue = conv.Id_Cat.ToString();

                this.CmbId_UEjecutivo.SelectedValue = conv.Id_UEjecutivo.ToString();
                this.TxtPC_FechaCreo.Text = conv.PC_FechaCreo.ToString();
                //JFCV TODO cargar las fechas 
                this.TxtPC_FechaUltMod.Text = conv.PC_FechaMod.ToString();
                if (conv.PC_FechaInicio != null)
                {
                    this.TxtPC_FechaInicioConv.SelectedDate = conv.PC_FechaInicio;
                }
                this.TxtPC_Notas.Text = conv.PC_Notas;
                this.TxtPC_ConvenioOtro.Text = conv.PC_NoConvenioFuturo.ToString();
                if (this.TxtPC_ConvenioOtro.Text == "0")
                {
                    this.TxtPC_ConvenioOtro.Text = String.Empty;
                }

                this.ChkMatrizCN.Checked = Convert.ToBoolean(conv.PC_DesplegarMatrizCN);
                this.NombreHojaPdf = conv.PC_ArchivoPDF;

                lblNombreArchivoPDF.Text = conv.PC_NombreArchivoPDF;
                lblContenidoPDF.Text = conv.PC_ArchivoPDF;

                if (this.HFTipoOp.Value == "1")
                {
                    this.TxtConvAnt.Text = conv.PC_NoConvenio;
                }
                else
                {
                    this.TxtPC_NoConvenio.Text = conv.PC_NoConvenio;
                    this.TxtPC_Nombre.Text = conv.PC_Nombre;
                }

                if (lblNombreArchivoPDF.Text != "")
                {
                    buttonDescargarpdf.Font.Bold = true;
                    buttonDescargarpdf.ForeColor = System.Drawing.Color.Blue;
                    //buttonDescargarpdf.BorderStyle = BorderStyle.Double;
                }
                //else
                //{
                //    txtDescripcion.BorderColor = System.Drawing.Color.Empty;
                //    buttonDescargarpdf.BackColor = System.Drawing.Color.Empty;
                //    buttonDescargarpdf.Font.Bold = true;
                //}


                //TxtPC_Margen.Text = conv.PC_Margen.ToString();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void ConfirmarModificacion()
        {
            //antes de modificar primero pregunto si desea hacerlo 

            try
            {

                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;

                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;

                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();

                LlenarEncabezadoMod(ref conv, sesion.Id_U);


                string elmensajees = "";

                elmensajees = "Convenio Key: " + TxtKeyConvenio.Text + "<br>";
                elmensajees = elmensajees + "Convenio proveedor: " + conv.PC_NoConvenio.ToString() + "<br>";
                elmensajees = elmensajees + "Nombre de convenio: " + conv.PC_Nombre.ToString() + "<br>";

                RAM1.ResponseScripts.Add("return  callConfirmMod('Desea guardar los cambios realizados al convenio? <br>" + elmensajees + "')");


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Modificar()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                List<ConvenioDet> List = new List<ConvenioDet>();
                List = ListDet;

                if (List == null)
                {
                    Alerta("La lista de productos esta vacía");
                    return;
                }
                if (List.Count == 0)
                {
                    Alerta("La lista de productos esta vacía");
                    return;

                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;

                LlenarEncabezadoMod(ref conv, sesion.Id_U);
                cn_conv.ModificaConvenio(conv, ref Verificador, Conexion);

                if (Verificador == -1)
                {
                    cn_conv.InsertarConvenioDet(int.Parse(HFId_PC.Value), List, ref Verificador, Conexion);

                    if (Verificador == -1)
                    {
                        //JMM: Envio el correo de modificación
                        EnviaMailModificacion();

                        //cn_conv.ConvenioModifico_EnviarCorreo(int.Parse(HFId_PC.Value), sesion.Emp_Cnx);
                        AlertaCerrar("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("Error al insertar los detalles del convenio");
                    }

                }
                else
                {
                    Alerta("Error al tratar de modificar el convenio");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void EnviaMailModificacion()
        {
            string Asunto = "";
            int adicionales = 1;  //Gerente JO
            int anexos = 0;
            int administradores = 0;
            int detalle = 0;
            int destinatarios = 0;  //RIKS 
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int tipo_Habilitar = 1; //Ver
            Asunto = "Modificaciónes a Convenio de PAAA esp. / " + TxtPC_Nombre.Text;

            StringBuilder cuerpo_correo = new StringBuilder();

            cuerpo_correo.Append("<div align='left'>");

            cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
            cuerpo_correo.Append("<tr> <td > *Los datos del siguiente convenio han sido modificados.</td>  </tr>");
            cuerpo_correo.Append("<tr> <td > *Favor de revisar los cambios en su SIAN.</td>  </tr>");
            cuerpo_correo.Append("<tr> <td >  &nbsp; </td>  </tr>");
            cuerpo_correo.Append("</table>");

            cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio Key:" + Convert.ToString(HFId_PC.Value));
            cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio proveedor:" + TxtPC_NoConvenio.Text);
            cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Nombre de convenio:" + TxtPC_Nombre.Text);
            cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Categoría:" + CmbId_Cat.SelectedItem);
            cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("</table> <br>");



            EnviaMailConvenio enviarcorreo = new EnviaMailConvenio(int.Parse(HFId_PC.Value), Asunto, cuerpo_correo, "", 1, destinatarios, adicionales, anexos, administradores, 0, sesion, tipo_Habilitar, detalle,"");
            enviarcorreo.EnviaMail();

        }


        private void LlenarEncabezadoMod(ref Convenio conv, int Id_U)
        {
            try
            {
                conv.Id_PC = int.Parse(HFId_PC.Value);
                conv.PC_Nombre = this.TxtPC_Nombre.Text.Trim();

                conv.Id_UEjecutivo = int.Parse(this.CmbId_UEjecutivo.SelectedValue);
                // conv.PC_Margen = double.Parse(TxtPC_Margen.Text);
                conv.PC_Notas = this.TxtPC_Notas.Text.Trim();
                conv.Id_U = Id_U;

                //JFCV Conveni
                //conv.PC_NoConvenio = this.TxtKeyConvenio.Text;
                conv.PC_NoConvenio = this.TxtPC_NoConvenio.Text;
                conv.PC_NoConvenioFuturo = this.TxtPC_ConvenioOtro.Text;
                conv.PC_NoConvenioKey = this.HFTipoOp.Value == "1" ? int.Parse(this.TxtKeyConvenio.Text) : 0;

                if (conv.PC_FechaInicio != (DateTime?)null)
                {
                    conv.PC_FechaInicio = this.TxtPC_FechaInicioConv.SelectedDate.Value;
                }
                else
                {
                    conv.PC_FechaInicio = DateTime.Now;

                }
                conv.PC_DesplegarMatrizCN = Convert.ToInt32(ChkMatrizCN.Checked);

                conv.PC_NombreArchivoPDF = lblNombreArchivoPDF.Text;
                //string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + lblNombreArchivoPDF.Text;

                //PagElec_Soporte4 = ConvertirFileToByteArray(path);


                //conv.PC_ArchivoPDF = PagElec_Soporte4.ToString();
                conv.PC_ArchivoPDF = lblContenidoPDF.Text;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Imprimir(int Id_PC)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                //ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(Conexion);
                 
                ALValorParametrosInternos.Add(Id_PC);


                Type instance = null;

                instance = typeof(LibreriaReportes.RepPrecioConvenio);


                //Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                //Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                //RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");

                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion Funciones

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
        #endregion



        protected void rgDetalles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                GridEditableItem editedItem = e.Item as GridEditableItem;
                ////////////txtCantidad_TextChanged(editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox, e);
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                ////RadComboBox comboboxProductos = (editedItem.FindControl("cmbProductosLista") as RadComboBox);
                //RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                ////comprobar campos vacios
                //if (
                //        ((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "" || (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "-1")
                //        || !TxtProducto.Value.HasValue
                //        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == ""
                //        || (editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text == ""
                //    )
                //{
                //    Alerta("Todos los campos son requeridos");
                //    e.Canceled = true;
                //    return;
                //}

                int territorio = 12121212; //  int.Parse((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);

                int Id_Prd = 28; //  Convert.ToInt32(TxtProducto.Value);
                ////Id_Prd = Convert.ToInt32((editedItem["Id_Prd"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Text);



                double PCD_PrecioVtaMin = double.Parse((editedItem["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin") as RadNumericTextBox).Text);
                double PCD_PrecioVtaMax = double.Parse((editedItem["PCD_PrecioVtaMax"].FindControl("RadNumericTextBoxPrecioVtaMax") as RadNumericTextBox).Text);




                double PCD_PrecioPAAAEspProv = double.Parse((editedItem["PCD_PrecioPAAAEspProv"].FindControl("RadNumericTextBoxPrecioPAAAEspProv") as RadNumericTextBox).Text);
                double PCD_PrecioVentaConvenio = double.Parse((editedItem["PCD_PrecioVentaConvenio"].FindControl("RadNumericTextBoxPrecioVentaConvenio") as RadNumericTextBox).Text);
                double PCD_Margen = double.Parse((editedItem["PCD_Margen"].FindControl("RadNumericTextBoxMargen") as RadNumericTextBox).Text);
                double PCD_PrecioAAAEsp = 0;
                DateTime PCD_FechaFinA = Convert.ToDateTime("01/01/2018");
                DateTime PCD_FechaInicio = Convert.ToDateTime("01/01/2017");
                DateTime PCD_FechaInicioA = Convert.ToDateTime("01/09/2017");
                double PCD_PrecioAAAEspA = 0;



                ////double PCD_PrecioAAAEsp = double.Parse((editedItem["PCD_PrecioAAAEsp"].FindControl("RadNumericTextBoxPrecioAAAEsp") as RadNumericTextBox).Text);
                ////DateTime PCD_FechaFinA = Convert.ToDateTime((editedItem["PCD_Margen"].FindControl("RadNumericTextBoxMargen") as RadNumericTextBox).Text);


                //No se puede ingresar el mismo producto varias veces a menos ke tenga territorio distinto
                //msg Producto ya capturado
                //if (dt_detalles.Select("Id_Prd='" + Id_Prd + "' and Terr='" + territorio + "'").Length > 0)
                //{
                //    Alerta("El producto ya ha sido agregado a la remisión");
                //    e.Canceled = true;
                //    return;
                //}
                //if (cantidad == 0)
                //{
                //    Alerta("No puede ingresar una partida con cantidad 0");
                //    e.Canceled = true;
                //    return;
                //}

                //cmbTipoMovimiento
                //if (precio == 0 && cmbTipoMovimiento.SelectedValue != "92") //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                //{
                //    Alerta("No puede ingresar una partida con precio 0");
                //    e.Canceled = true;
                //    return;
                //}


                string Id_CuentaNacional = "1";


                //    string envio = "" + Id_CuentaNacional + "|" + Id_Prd + "";
                //    object respuesta = ws.TraeProductoCN(envio);
                WS_Producto.Service1 ws = new WS_Producto.Service1();
                ////ws.Url = ConfigurationManager.AppSettings["WS_Producto"].ToString();

                //    XmlNode NodeError = Xml.SelectSingleNode("//Producto/MsgError/@Error");
                //    XmlNode NodeValida = Xml.SelectSingleNode("//CuentaNacional/@ValidaCodEsp");
                string envio = "" + Id_CuentaNacional + "|" + Id_Prd + "";
                object respuesta = ws.TraeProductoCN(envio);
                XmlDocument Xml = new XmlDocument();
                Xml.LoadXml(respuesta.ToString());

                XmlNode NodeError = Xml.SelectSingleNode("//Producto/MsgError/@Error");
                XmlNode NodeValida = Xml.SelectSingleNode("//CuentaNacional/@ValidaCodEsp");
                XmlNode NodeProductoID = Xml.SelectSingleNode("//Producto/@ProNum");
                XmlNode NodeProductoDesc = Xml.SelectSingleNode("//Producto/@ProDesc");
                XmlNode NodeProUM = Xml.SelectSingleNode("//Producto/@ProUM");
                XmlNode NodeProPrecio = Xml.SelectSingleNode("//Producto/@ProPrecio");


                if (!string.IsNullOrEmpty(NodeValida.InnerText))
                {
                    if (NodeValida.InnerText != "N")
                    {

                        if (!string.IsNullOrEmpty(NodeError.InnerText))
                        {
                            Alerta(NodeError.InnerText);
                            e.Canceled = true;
                            return;
                        }
                    }

                }





                //agregar al dt
                DataRow[] editable_dr;
                //if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                //    editable_dr[0].BeginEdit();
                //    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                //    editable_dr[0].AcceptChanges();
                //}
                //else
                //{
                //    dt_cuenta.Rows.Add(new object[] { Id_Prd, cantidad });
                //}

                string PCD_ClaveProv = "";
                string Prd_Descripcion = "";
                string PCD_Referencia = "";


                //new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                dt_detalles.Rows.Add(new object[] { ++id_detalle, territorio, Id_Prd, PCD_ClaveProv, Prd_Descripcion, PCD_Referencia, PCD_PrecioVtaMin, PCD_PrecioVtaMax, PCD_FechaInicio, PCD_PrecioAAAEspA, PCD_FechaInicioA, PCD_PrecioPAAAEspProv, PCD_PrecioVentaConvenio, PCD_Margen, PCD_FechaFinA, PCD_PrecioAAAEsp });

                //if (txtTipoId.Value == 60)
                //{
                //    double total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                //    if (total >= Convert.ToDouble(HiddenField2.Value))
                //    {
                //        valuacion.Visible = true;
                //    }
                //    else
                //    {
                //        valuacion.Visible = false;
                //    }
                //}
            }
            catch (Exception ex)
            { //Alerta("No se pudieron guardar los datos. " + msgerror(ex)); //cambiar esto
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RAM1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgDetalles_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //txtCantidad_TextChanged(sender, e);
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                //comprobar campos vacios


                //if (!TxtProducto.Value.HasValue
                //        || (editedItem["PCD_PrecioVentaConvenio"].FindControl("RadNumericTextBoxPrecioVentaConvenio") as RadNumericTextBox).Text == ""
                //        || (editedItem["PCD_PrecioPAAAEspProv"].FindControl("RadNumericTextBoxPrecioPAAAEspProv") as RadNumericTextBox).Text == ""

                //    )
                //{
                //    Alerta("Todos los campos son requeridos");
                //    e.Canceled = true;
                //    return;
                //}


                GridEditableItem Item = (GridEditableItem)e.Item;
                //EntradasSalidasCentralDet es = new EntradasSalidasCentralDet();

                //es.Id_Emp = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Emp"]);
                //es.Id_Alm = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Alm"]);
                int Id_ConvDet = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_ConvDet"]);
                int Id_Prd = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Prd"]);



                double PCD_PrecioPAAAEspProv = double.Parse((editedItem["PCD_PrecioPAAAEspProv"].FindControl("RadNumericTextBoxPrecioPAAAEspProv") as RadNumericTextBox).Text);
                double PCD_PrecioVentaConvenio = double.Parse((editedItem["PCD_PrecioVentaConvenio"].FindControl("RadNumericTextBoxPrecioVentaConvenio") as RadNumericTextBox).Text);
                // double PCD_Margen = double.Parse((editedItem["PCD_Margen"].FindControl("RadNumericTextBoxMargen") as RadNumericTextBox).Text);
                double PCD_PrecioVtaMax = double.Parse((editedItem["PCD_PrecioVtaMax"].FindControl("RadNumericTextBoxPrecioVtaMax") as RadNumericTextBox).Text);
                double PCD_PrecioVtaMin = double.Parse((editedItem["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin") as RadNumericTextBox).Text);

                double PCD_PrecioAAAEsp = double.Parse((editedItem["PCD_PrecioAAAEspB"].FindControl("RadNumericTextBoxPrecioAAAEsp") as RadNumericTextBox).Text);

                if (PCD_PrecioPAAAEspProv == 0 || PCD_PrecioVentaConvenio == 0)
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                //Prueba
                string PCD_Referencia = (editedItem["PCD_Referencia"].FindControl("RadTextBoxPCD_Referencia") as RadTextBox).Text;
                DateTime? ActualFechaInicio;
                DateTime? ActualFechafin;

                ActualFechaInicio = DateTime.Now;
                if (((RadDatePicker)editedItem.FindControl("tpFechaActualInicio")).SelectedDate != null)
                {
                    ActualFechaInicio = ((RadDatePicker)editedItem.FindControl("tpFechaActualInicio")).SelectedDate;

                    DateTime fecha = DateTime.Now;


                    if (ActualFechaInicio.Value.Date < fecha.Date)
                    {
                        Alerta("La fecha inicial no puede ser menor a la fecha actual.");
                        e.Canceled = true;
                        return;
                    }
                }

                ActualFechafin = DateTime.Now;
                if (((RadDatePicker)editedItem.FindControl("ActualFechafin")).SelectedDate != null)
                {
                    ActualFechafin = ((RadDatePicker)editedItem.FindControl("ActualFechafin")).SelectedDate;

                    DateTime fecha = DateTime.Now;

                    if (ActualFechafin.Value.Date <= fecha.Date)
                    {
                        Alerta("La fecha Final debe ser mayor a la fecha actual.");
                        e.Canceled = true;
                        return;
                    }
                    if (ActualFechafin.Value.Date <= ActualFechaInicio.Value.Date)
                    {
                        Alerta("La fecha Final debe ser mayor a la fecha Inicial.");
                        e.Canceled = true;
                        return;
                    }

                }


                //update dt_detalles
                //DataRow[] editable_dr;
                //editable_dr = null;
                ConvenioDet c = new ConvenioDet();
                c = ListDet.FirstOrDefault(o => o.Id_Prd == Id_Prd);

                //TODO
                // si el precio de convenio no es nulo 
                // entonces pregunto si el precio de venta es 0 , que tome el valor de precio convenio 
                // sumandole al precio máximo 0.05  y restandole al minimo 0.05 
                //  Si en la columna “P.Venta Convenio” no hay datos, esta columna también se deberá quedar en blanco. 
                if (!string.IsNullOrEmpty(PCD_PrecioVentaConvenio.ToString()))
                {
                    c.PCD_PrecioVtaMin = PCD_PrecioVtaMin <= 0 ? PCD_PrecioVentaConvenio - 0.05 : PCD_PrecioVtaMin;
                    c.PCD_PrecioVtaMax = PCD_PrecioVtaMax <= 0 ? PCD_PrecioVentaConvenio + 0.05 : PCD_PrecioVtaMax;
                }
                else
                {
                    c.PCD_PrecioVtaMin = 0;
                    c.PCD_PrecioVtaMax = 0;
                }



                //005 punto 1 
                //1.	La columna “Anterior PAAA Esp.” se modificará si
                //    a.	Se alimenten nuevos datos manual o automáticamente en la columna “Actual PAAA Esp.” 
                //          Una vez guardados los cambios, los precios que estaban en “actual” pasarán a “anterior”, y en “actual” 
                //          se quedarán guardados los nuevos datos. No importando si los datos que estaban en la columna actual aun no vencen.
                //    b.	Llegan a su fin los precios que estaban en la columna “Actual PAAA Esp.” Si termina la vigencia 
                //          de los precios que estaban en “actual”, los precios de “actual” pasarán a “anterior”.
                //if (c.PCD_PrecioAAAEsp != PCD_PrecioAAAEsp)
                //{
                //no importa si los precios vencen o no se deben de pasar a la columna de anterior porque la estoy modificando 
                c.PCD_PrecioAAAEspA = PCD_PrecioAAAEsp;
                c.PCD_FechaFinA = c.PCD_FechaFin;
                c.PCD_FechaInicioA = c.PCD_FechaInicio;
                //}

                if (c.PCD_FechaInicioC.Value.Date <= DateTime.Now.Date)
                {
                    c.PCD_FechaInicioC = (DateTime?)null;
                    c.PCD_FechaFinC = (DateTime?)null;
                    c.PCD_PrecioAAAEspC = (double?)null;
                }

                c.PCD_PrecioPAAAEspProv = PCD_PrecioPAAAEspProv;
                c.PCD_PrecioVentaConvenio = PCD_PrecioVentaConvenio;
                c.PCD_PrecioVtaMin = PCD_PrecioVtaMin;
                c.PCD_PrecioVtaMax = PCD_PrecioVtaMax;
                c.PCD_Referencia = PCD_Referencia;
                c.PCD_FechaInicio = ActualFechaInicio;
                c.PCD_FechaFinVer = ActualFechafin;
                c.PCD_FechaFin = ActualFechafin;
                c.PCD_PrecioAAAEsp = PCD_PrecioAAAEsp;




                if (!string.IsNullOrEmpty(c.PCD_PrecioVentaConvenio.ToString()) && c.PCD_PrecioAAAEsp != 0)
                {
                    c.PCD_Margen = (1 - (c.PCD_PrecioAAAEsp / c.PCD_PrecioVentaConvenio)) * 100;
                }
                else
                {
                    c.PCD_Margen = 0;
                }


                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();

                //if (listDet.Select("Id_ConvDet='" + Id_ConvDet_A + "'").Length > 0)
                //{
                //    editable_dr = dt_detalles.Select("Id_ConvDet='" + Id_ConvDet_A + "'");

                //    editable_dr[0].BeginEdit();
                //    editable_dr[0]["PCD_PrecioPAAAEspProv"] = PCD_PrecioPAAAEspProv;
                //    editable_dr[0]["PCD_PrecioVentaConvenio"] = PCD_PrecioVentaConvenio;
                //    editable_dr[0]["PCD_PrecioVtaMin"] = PCD_PrecioVtaMin;
                //    editable_dr[0]["PCD_PrecioVtaMax"] = PCD_PrecioVtaMax;


                //    editable_dr[0]["PCD_Margen"] = PCD_Margen;
                //    editable_dr[0].AcceptChanges();
                //}
                //else
                //{
                //    throw new Exception("Error: No se pudo editar el detalle");
                //}




            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RAM1.ResponseScripts.Add("showcolum();");
            }
        }

        protected void ValidarFechaInicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            RadDatePicker objFecha = sender as RadDatePicker;
            //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //DateTime fechaInicioPeriodo = sesion.CalendarioIni;

            //if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
            //{
            //    RAM1.ResponseScripts.Add("radalert('La fecha inicio no puede ser menor al periodo actual', 330, 150);");
            //    objFecha.DbSelectedDate = null;
            //    return;
            //}

        }
        protected void ValidarFechaFin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            RadDatePicker objFecha = sender as RadDatePicker;

            //if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
            //{
            //    RAM1.ResponseScripts.Add("radalert('La fecha inicio no puede ser menor al periodo actual', 330, 150);");
            //    objFecha.DbSelectedDate = null;
            //    return;
            //}

        }
        protected void ValidarFechaInicioConv_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            RadDatePicker objFecha = sender as RadDatePicker;

            if (DateTime.Now > Convert.ToDateTime(objFecha.SelectedDate))
            {
                RAM1.ResponseScripts.Add("radalert('La fecha de inicio no puede ser menor a la fecha actual.', 330, 150);");
                objFecha.DbSelectedDate = null;
                return;
            }

        }
        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_prd = "";

                GridEditableItem Item = (GridEditableItem)e.Item;

                // string Id_ConvDet = Convert.ToString(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_ConvDet"]);

                Id_prd = Convert.ToString(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Prd"]);
                ListDet.Remove(ListDet.Where(x => x.Id_Prd == int.Parse(Id_prd)).ToList()[0]);

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RAM1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ErrorManager();
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    List<ConvenioDet> List = new List<ConvenioDet>();
                    if (this.HFId_PC.Value != "0")
                    {
                        this.rgDetalles.DataSource = ConsultaConvenio();
                    }
                    else
                    {
                        //rgDetalles.DataSource = dt_detalles;
                        rgDetalles.DataSource = ListDet;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }

        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ErrorManager();
            try
            {
                if (e.CommandName == "InitInsert")
                {
                    cantidad_A = 0;
                    Id_ConvDet_A = 0;
                    Id_Prd_A = 0;
                    costo_A = 0;

                    if (!validarCamposDetalle())
                    {
                        e.Canceled = true;
                        return;
                    }

                }
                if (e.CommandName == "Edit")
                {
                    Id_ConvDet_A = 1; // int.Parse(rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_ConvDet"].Text);
                    Id_Prd_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                    cantidad_A = 0;
                    //cantidad_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Cantidad"].FindControl("CantidadLabel") as Label).Text);
                    //costo_A = double.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Precio"].FindControl("PrecioLabel") as Label).Text);
                    // RadNumericTextBox rad = (rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("RadNumericTextBox1") as RadNumericTextBox);               
                    //rad.ClientEvents.OnLoad = "";
                    //rad.ClientEvents.OnBlur = "";               
                }

                switch (e.CommandName)
                {
                    case "Cancelar":
                        Borrar(e);
                        break;
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            ErrorManager();

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                string conveniokey = "";
                if (TxtKeyConvenio.Text != String.Empty)
                {

                    conveniokey = TxtKeyConvenio.Text;
                    string convenioproveedor = TxtPC_NoConvenio.Text;
                    string convenionombre = TxtPC_Nombre.Text;

                    //for the Classic RenderMode
                    // LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
                    
                    string Id_prod = (dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["Id_Prd"]).ToString();
                    string PCD_ClaveProv = (dataItem["PCD_ClaveProv"].FindControl("ClaveProvLabel") as Label).Text;



                  


                    //GridEditableItem form = (GridEditableItem)e.Item;
                    //RadNumericTextBox dataField = (RadNumericTextBox)form["PCD_ClaveProv"].FindControl("ClaveProvLabel");

                }


            }

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                { //se habilitan todos los controles
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    //(editItem["importe"].Controls[0] as TextBox).Visible = false;
                }

                Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    //se llenan y deshabilita cmb territorio y txtterritorio, cmb producto y txt producto.
                    //comboterritorio
                    //(editItem.FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//
                    //(editItem.FindControl("txtter1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//txtterr
                    //(editItem.FindControl("RadComboBoxTerr") as RadComboBox).Enabled = false;
                    //(editItem.FindControl("txtter1") as RadNumericTextBox).Enabled = true;
                    //producto
                    //(editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();//txtproducto
                    //(e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;//txtbox id del producto                    
                    //editItem["importe"].Controls[1].Visible = false;
                    Id_ConvDet_A = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_ConvDet"]);
                    ((System.Web.UI.WebControls.WebControl)(editItem["PCD_Margen"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["PCD_PrecioAAEspA"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["PCD_FechaInicioA"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["PCD_PrecioAAEspC"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["PCD_FechaInicioC"])).Enabled = false;


                }

            }

            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin");
                }

                dataField.Focus();
            }
            //-----------------------------------------
        }
        protected void cmbTipoSalidaIdDesc_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadComboBox combo = sender as RadComboBox;
            RadComboBox RadComboBoxConceptoTipoSalida = combo.Parent.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
            new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value), sesion.Emp_Cnx, "sp_CatESConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);
            RadComboBoxConceptoTipoSalida.SelectedValue = "1";
            RadComboBoxConceptoTipoSalida.Text = "";
        }


        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_ConvDet");
            dt_detalles.Columns.Add("Terr");
            dt_detalles.Columns.Add("Id_Prd");



            //dt_detalles.Columns.Add("Precio", typeof(double));
            //dt_detalles.Columns.Add("Importe", typeof(double));

            dt_detalles.Columns.Add("PCD_ClaveProv");
            dt_detalles.Columns.Add("Prd_Descripcion");
            dt_detalles.Columns.Add("PCD_Referencia");


            dt_detalles.Columns.Add("PCD_PrecioVtaMin", typeof(double));
            dt_detalles.Columns.Add("PCD_PrecioVtaMax", typeof(double));
            dt_detalles.Columns.Add("PCD_FechaInicio", typeof(DateTime));
            dt_detalles.Columns.Add("PCD_PrecioAAAEspA", typeof(double));
            dt_detalles.Columns.Add("PCD_FechaInicioA", typeof(DateTime));
            dt_detalles.Columns.Add("PCD_PrecioPAAAEspProv", typeof(double));
            dt_detalles.Columns.Add("PCD_PrecioVentaConvenio", typeof(double));
            dt_detalles.Columns.Add("PCD_Margen", typeof(double));
            dt_detalles.Columns.Add("PCD_FechaFinA", typeof(DateTime));
            dt_detalles.Columns.Add("PCD_PrecioAAAEsp", typeof(double));


        }


        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                ////RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                ////int cantidad = Txtcantidad.Value.HasValue ? (int)Txtcantidad.Value : 0;
                ////int prd = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("RadNumericTextBox1") as RadNumericTextBox).Value);
                ////int ter = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtter1") as RadNumericTextBox).Value);

                ////int disponible = 0;
                ////int invFinal = 0;
                ////int asignado = 0;
                ////int cantidad_B = 0;
                ////new CN_CapEntradaSalida().ConsultarDisponible(session, prd, ref disponible, ref invFinal, ref asignado);

                ////DataRow[] Dr = dt_detalles.Select("Id_Prd='" + prd + "' and Terr <> '" + ter + "'");

                ////Remision remision = new Remision();
                ////List<RemisionDet> detalle = new List<RemisionDet>();
                ////remision.Id_Emp = session.Id_Emp;
                ////remision.Id_Cd = session.Id_Cd_Ver;
                //remision.Id_Rem = !string.IsNullOrEmpty(txtFolio.Text) ? Convert.ToInt32(txtFolio.Text) : -1;
                //new CN_CapRemision().ConsultarRemisionesDetalle(session, remision, ref detalle);

                //if (Dr.Length > 0)
                //{
                //    for (int i = 0; i < Dr.Length; i++)
                //        cantidad_B += !string.IsNullOrEmpty(Dr[i]["Cantidad"].ToString()) ? Convert.ToInt32(Dr[i]["Cantidad"]) : 0;
                //}
                //int count = 0;
                //foreach (RemisionDet rd in detalle)
                //{
                //    if (rd.Id_Prd == prd)
                //        count += rd.Rem_Cant;
                //}

                //int disponible_pedido = 0;
                //#region pedido
                //if (txtPedido.Text != "")
                //{
                //    CN_CapPedido cappedido = new CN_CapPedido();
                //    Pedido pedido = new Pedido();
                //    pedido.Id_Emp = session.Id_Emp;
                //    pedido.Id_Cd = session.Id_Cd_Ver;
                //    pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                //    DataTable dt2 = new DataTable();
                //    dt2.Columns.Add("Id_PedDet");
                //    dt2.Columns.Add("Id_Ter");
                //    dt2.Columns.Add("Ter_Nombre");
                //    dt2.Columns.Add("Id_Prd");
                //    dt2.Columns.Add("Prd_Descripcion");
                //    dt2.Columns.Add("Prd_Presentacion"); dt2.Columns.Add("Prd_Unidad");
                //    dt2.Columns.Add("Prd_Precio");
                //    dt2.Columns.Add("Disponible");
                //    dt2.Columns.Add("Prd_Importe");
                //    dt2.Columns.Add("Id_Rem");
                //    cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, null, session.Emp_Cnx);

                //    DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");
                //    if (dr.Length > 0)
                //    {
                //        for (int i = 0; i < dr.Length; i++)
                //            disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                //    }
                //    if (disponible_pedido < 0)
                //        disponible_pedido = 0;
                //}
                //#endregion

                ////-----------------------------------------------------------
                //int cantidadEnDt_cuentaOriginal = 0;
                ////---si es REMISION DE PEDIDO, contar lo que ya se tenìa
                //if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + prd + "'").Length > 0)
                //{
                //    cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                //    {
                //        //int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad /*+ cuentaActual*/) > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (cantidad > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            return;
                //        }
                //    }
                //}

                ////************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                //if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + prd + "'").Length > 0)
                //{
                //    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                //    {
                //        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                //        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad /*+ cuentaActual */ - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if ((cantidad - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            return;
                //        }
                //    }
                //}

                ////---------------------------------------------
                //if (cantidad + cantidad_B > disponible + count + disponible_pedido)
                //{
                //    AlertaFocus("Producto <b>" + prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + (disponible + count + disponible_pedido).ToString(), Txtcantidad.ClientID);
                //    Txtcantidad.Text = "";
                //    return;
                //}
                //else
                //{
                //    (Txtcantidad.Parent.Parent.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Focus();
                //}

                //if (tipoDeMovimiento == 2 && txtTipoId.Value == 60)
                //{
                //    int territorio = (int)((Txtcantidad.Parent.FindControl("txtter1") as RadNumericTextBox).Value.Value);
                //    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                //    int verificador = 0;
                //    CNentrada.ConsultarSaldo(session.Id_Emp, session.Id_Cd_Ver, prd.ToString(), territorio.ToString(), txtClienteId.Text, session.Emp_Cnx, ref verificador, "60");

                //    if (verificador - (cantidad_A - cantidad) < 0)
                //    {
                //        AlertaFocus("El producto " + prd.ToString() + " no cuenta con saldo suficiente", Txtcantidad.ClientID);
                //        Txtcantidad.Text = "";
                //        return;
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private bool validarCamposDetalle()
        {

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            session = (Sesion)Session["Sesion" + Session.SessionID];

            //if (cmbTipoMovimiento.SelectedValue == "" || cmbTipoMovimiento.SelectedValue == "-1")
            //{
            //    Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            //    return false;
            //}
            //if (!txtClienteId.Value.HasValue)
            //{
            //    Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            //    return false;
            //}
            //if (cmbTerritorio.SelectedValue == "" || cmbTerritorio.SelectedValue == "-1")
            //{
            //    Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            //    return false;
            //}
            //if (tipoDeMovimiento == 3 && txtPedido.Text == "")
            //{
            //    Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            //}
            return true;
        }

        private string MaximoId()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Convenio conv = new Convenio();

                CN_Convenio cn_conv = new CN_Convenio();


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, 0, "ProPrecioConvenio", "Id_PC", Conexion, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        private void ExportarExcel()
        {
            try
            {


                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalleConv(ref tabla);
                tabla.Append("</table></body></html>");
                //CN__Comun cn_comun = new CN__Comun();
                //cn_comun.ExportarExcel("ExportaConvenio", tabla.ToString());

                //Funcion fn = new Funcion();
                //fn.ExportarExcel("RepInvDiarios_" + DateTime.Now, tabla.ToString());  

                string ruta = Server.MapPath("Reportes") + "\\PlantillaConvenio.txt";
                StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                sw.Close();
                if (File.Exists(ruta))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\PlantillaConvenio.xls";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(ruta, Server.MapPath("Reportes") + "\\PlantillaConvenio.xls");
                    Response.Redirect("Reportes\\PlantillaConvenio.xls", false);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void EscribeDetalleConv(ref StringBuilder Tabla)
        {

            String width;

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Convenio cn_conv = new CN_Convenio();
            Convenio conv = new Convenio();

            DataTable dt = new DataTable();
            //dt = dt_detalles;


            //dt = new DataTable();
            dt.Columns.Add("Id_Prd");
            dt.Columns.Add("PCD_ClaveProv");
            dt.Columns.Add("Prd_Descripcion");
            dt.Columns.Add("PCD_PrecioVtaMin", typeof(double));
            dt.Columns.Add("PCD_PrecioVtaMax", typeof(double));
            dt.Columns.Add("PCD_PrecioAAAEspA", typeof(double));
            dt.Columns.Add("PCD_PrecioAAAEsp", typeof(double));
            dt.Columns.Add("PCD_PrecioPAAAEspProv", typeof(double));
            dt.Columns.Add("PCD_PrecioVentaConvenio", typeof(double));
            dt.Columns.Add("PCD_FechaInicioA", typeof(DateTime));
            dt.Columns.Add("PCD_FechaFinA", typeof(DateTime));
            dt.Columns.Add("PCD_Referencia");
            dt.Columns.Add("PCD_FechaInicio", typeof(DateTime));
            dt.Columns.Add("PCD_Margen", typeof(double));
            //



            //List<Convenio> items = GenerateData();
            //DataTable table = ConvertListToDataTable(list);
            //dataGridView1.DataSource = table;
            int contadorrenglon = 0;


            //listdet

            try
            {

                foreach (var array in ListDet)
                {
                    contadorrenglon++;

                    //dt.Rows.Add(new object[] { contadorrenglon, 1, array.Id_Prd, array.PCD_ClaveProv, array.Prd_Descripcion, array.PCD_Referencia, array.PCD_PrecioVtaMin, array.PCD_PrecioVtaMax, array.PCD_FechaInicio, array.PCD_PrecioAAAEspA, array.PCD_FechaInicioA, array.PCD_PrecioPAAAEspProv, array.PCD_PrecioVentaConvenio, array.PCD_Margen, array.PCD_FechaFinA, array.PCD_PrecioAAAEsp });

                    dt.Rows.Add(new object[] { array.Id_Prd, array.PCD_ClaveProv, array.Prd_Descripcion, array.PCD_PrecioVtaMin, array.PCD_PrecioVtaMax, array.PCD_PrecioAAAEspA, array.PCD_PrecioAAAEsp, array.PCD_PrecioPAAAEspProv, array.PCD_PrecioVentaConvenio, array.PCD_FechaInicioA, array.PCD_FechaFinA, array.PCD_Referencia, array.PCD_FechaInicio, array.PCD_Margen });
                }


                Tabla.Append("<tr>");


                ////lectura y escritura de columnas
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                int i = 0;
                //if (dt.Columns[i].ColumnName == "Id_Prd")
                //{
                width = (i == 0) ? "70px" : "90px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("ClaveKey");
                Tabla.Append("</th>");
                i++;//}
                //else
                //    if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                //    {
                width = (i == 0) ? "70px" : "90px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("PVentaMin");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("PVentaMax");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("PAAAEsp");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_PrecioPAAAEspProv")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("PAAAEspProveedor");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("PVentaConvenio");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("FechaInicio");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                //{
                width = (i == 0) ? "100px" : "100px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("FechaFin");
                Tabla.Append("</th>");
                i++;//}
                //else if (dt.Columns[i].ColumnName == "PCD_Referencia")
                //{
                width = (i == 0) ? "80px" : "90px";
                Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                Tabla.Append("RefConvenio");
                Tabla.Append("</th>");
                //}

                //}
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (i = 0; i < dt.Columns.Count; i++)
                    {


                        if (dt.Columns[i].ColumnName == "Id_Prd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("N2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("N2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                        }


                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("N2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioPAAAEspProv")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("N2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("N2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                        }

                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(valor.ToShortDateString());
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(valor.ToShortDateString());
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                        }

                        else if (dt.Columns[i].ColumnName == "PCD_Referencia")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }


                    }

                    Tabla.Append("</tr>");

                }
                //Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



    }
}