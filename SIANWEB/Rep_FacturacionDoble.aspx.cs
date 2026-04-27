using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class Rep_FacturacionDoble : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["repFacturacionDoble"] != null)
            {
                List<FacturacionDoble> Lista = (List<FacturacionDoble>)Session["repFacturacionDoble"];
                GrdFacturacion.DataSource = Lista;
                GrdFacturacion.DataBind();
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
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false");
                            return;
                        }

                        Session["repFacturacionDoble"] = null;
                        GrdFacturacion.DataSource = null;
                        GrdFacturacion.DataBind();
                        CargarCentros();
                        TxtfechaIni.Value = Sesion.CalendarioIni;
                        TxtFechaFinal.Value = Sesion.CalendarioFin;
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbCentro_TextChanged(object sender, EventArgs e)
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
                comun.BoostrapCambiarCdVer(CmbCentro, ref sesion);
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + "CmbCentro_TextChanged");
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
                    CN_Comun.DevLlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.ReadOnly = true;
                    CmbCentro.Value = (Sesion.Id_Cd_Ver.ToString());
                    CmbCentro.Enabled = false;

                }
                else
                {
                    CN_Comun.DevLlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Value = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
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

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //} 
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarGrid()
        {
            try
            {
                List<FacturacionDoble> List = new List<FacturacionDoble>();
                CN_facturacionDoble CN = new CN_facturacionDoble();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                FacturacionDoble Datos = new FacturacionDoble();
                Datos.Id_Emp = session2.Id_Emp;
                Datos.Id_Cd = session2.Id_Cd_Ver;
                Datos.FechaInicial = Convert.ToDateTime(TxtfechaIni.Value.ToString());
                Datos.FechaFinal = Convert.ToDateTime(TxtFechaFinal.Value.ToString());
                CN.ConsultarRep_FacturaDoble(Datos, ref List, session2.Emp_Cnx);

                Session["repFacturacionDoble"] = List;
                GrdFacturacion.DataSource = List;
                GrdFacturacion.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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


        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            GrdFacturacion.DataColumns["PDF"].Visible = false;
            GrdFacturacion.DataColumns["XML"].Visible = false;
            GrdFacturacion.DataColumns["PDFCentral"].Visible = false;
            GrdFacturacion.DataColumns["XMLCentral"].Visible = false;

            GrdFacturacion.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            GrdFacturacion.DataColumns["PDF"].Visible = true;
            GrdFacturacion.DataColumns["XML"].Visible = true;
            GrdFacturacion.DataColumns["PDFCentral"].Visible = true;
            GrdFacturacion.DataColumns["XMLCentral"].Visible = true;
        }



        protected void BtnCosnultar_ServerClick(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void BtnXMLCentral_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Factura = c.Grid.GetRowValues(c.VisibleIndex, "Id_Fac").ToString().Trim();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Session["repFacturacionDoble"] != null)
            {
                List<FacturacionDoble> Lista = (List<FacturacionDoble>)Session["repFacturacionDoble"];
                FacturacionDoble datos = (from tlist in Lista
                                          where tlist.Id_Fac == Convert.ToInt32(Factura)
                                          select tlist).First();

                string ruta = null;

                System.IO.StreamWriter sw = null;
                ruta = Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Fac" + datos.FolioAE.ToString() + ".xml";
                if (File.Exists(ruta))
                    File.Delete(ruta);

                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine(datos.Fac_XMLCN.ToString());
                sw.Close();

                Byte[] byteArray = new System.Net.WebClient().DownloadData(ruta); 

                Response.Clear();
                MemoryStream ms = new MemoryStream(byteArray);
                Response.ContentType = "response.contenttype = application/xml";
                Response.AddHeader("content-disposition", "attachment;filename=archivoXmlCN" + sesion.Id_U.ToString() + "Fac" + datos.FolioAE.ToString() + ".xml");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
        }

        protected void BTNPDFCentral_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Factura = c.Grid.GetRowValues(c.VisibleIndex, "Id_Fac").ToString().Trim();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Session["repFacturacionDoble"] != null)
            {
                List<FacturacionDoble> Lista = (List<FacturacionDoble>)Session["repFacturacionDoble"];
                FacturacionDoble datos = (from tlist in Lista
                                          where tlist.Id_Fac == Convert.ToInt32(Factura)
                                          select tlist).First();

                byte[] archivoPdf = (byte[])datos.Fac_PdfCN;
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("FACTURACN_", sesion.Id_Emp.ToString(), "_", sesion.Id_Cd.ToString(), "_", datos.FolioAE.ToString(), ".pdf");

                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);

                    string ruta2 = Server.MapPath("~/xmlsat/") + tempPDFname;

                    System.IO.FileInfo file = new System.IO.FileInfo(ruta2);


                    string Outgoingfile = tempPDFname;
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                        Response.WriteFile(file.FullName);

                    }
                    else
                    {

                        Response.Write("This file does not exist.");
                    }
                }
            }
        }

        protected void BtnXML_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Factura = c.Grid.GetRowValues(c.VisibleIndex, "Id_Fac").ToString().Trim();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Session["repFacturacionDoble"] != null)
            {
                List<FacturacionDoble> Lista = (List<FacturacionDoble>)Session["repFacturacionDoble"];
                FacturacionDoble datos = (from tlist in Lista
                                          where tlist.Id_Fac == Convert.ToInt32(Factura)
                                          select tlist).First();

                string ruta = null;
                 
                System.IO.StreamWriter sw = null;
                ruta = Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Fac" + Factura.ToString() + ".xml";

                if (File.Exists(ruta))
                    File.Delete(ruta);

                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine(datos.Fac_Xml.ToString());
                sw.Close();


                Byte[] byteArray = new System.Net.WebClient().DownloadData(ruta);


                Response.Clear();
                MemoryStream ms = new MemoryStream(byteArray);
                Response.ContentType = "response.contenttype = application/xml";
                Response.AddHeader("content-disposition", "attachment;filename=archivoXml" + sesion.Id_U.ToString() + "Fac" + Factura.ToString() + ".xml");
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
        }

        protected void BtnPDF_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Factura = c.Grid.GetRowValues(c.VisibleIndex, "Id_Fac").ToString().Trim();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Session["repFacturacionDoble"] != null)
            {
                List<FacturacionDoble> Lista = (List<FacturacionDoble>)Session["repFacturacionDoble"];
                FacturacionDoble datos = (from tlist in Lista
                                          where tlist.Id_Fac == Convert.ToInt32(Factura)
                                          select tlist).First();

                byte[] archivoPdf = (byte[])datos.fac_pdf;
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("FACTURA_"
                             , sesion.Id_Emp.ToString()
                             , "_", sesion.Id_Cd.ToString()
                             , "_", Factura.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);

                    string ruta2 = Server.MapPath("~/xmlsat/") + tempPDFname;

                    System.IO.FileInfo file = new System.IO.FileInfo(ruta2);


                    string Outgoingfile = tempPDFname;
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                        Response.WriteFile(file.FullName);

                    }
                    else
                    {

                        Response.Write("This file does not exist.");
                    }

                }
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
    }
}