using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using CapaEntidad;
using System.IO;
using Ionic.Zip;

namespace SIANWEB
{
    public partial class VentanaSoportes : System.Web.UI.Page
    {

        private int Id_Rem
        {
            set { Session["Id_Rem" + Session.SessionID] = value; }
            get { int? st = (int?)Session["Id_Rem" + Session.SessionID]; return st == null ? -1 : (int)st; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Id_Rem = Int32.Parse(Request.QueryString["Id_Rem"]);
                OcultarMostrarDocSoporte(Id_Rem.ToString());
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":

                        break;
                }
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        public bool GuardarSoporteRemision()
        {
            Sesion sesion = new Sesion();

            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int CDI = sesion.Id_Cd;

            string DirectorioAlmenamiento = ConfigurationManager.AppSettings["URLDirectorioSoporteRemision"].ToString();

            if (string.IsNullOrEmpty(DirectorioAlmenamiento))
            {
                Alerta("No hay un directorio predefinido para el alamacenamiento de estos archivos.");

                return false;
            }

            Div2.InnerHtml = "";

            if (RadUploadSoporteRemision.UploadedFiles.Count > 0)
            {
                foreach (Telerik.Web.UI.UploadedFile postedFile in RadUploadSoporteRemision.UploadedFiles)
                {
                    Div2.InnerHtml += "<b>Informacion del archivo cargado</b>: <hr />";



                    if (!Object.Equals(postedFile, null))
                    {
                        if (postedFile.ContentLength > 0)
                        {
                            if (postedFile.ContentLength > 24000000)
                            {

                                Alerta("Se excede los 3MB, permitidos.");
                                return false;
                            }

                            Div2.InnerHtml += string.Format("<br /><b>Nombre de archivo</b>: {0}", postedFile.GetName());
                            Div2.InnerHtml += string.Format("<br /><b>Tamaño</b>: {0} bytes", postedFile.ContentLength);

                            string fn = (Id_Rem.ToString() + postedFile.GetExtension());


                            try
                            {
                                if (!Directory.Exists(Server.MapPath(DirectorioAlmenamiento + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString())))
                                {

                                    Directory.CreateDirectory(Server.MapPath(DirectorioAlmenamiento + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString()));
                                }

                                string SaveLocation = Server.MapPath(DirectorioAlmenamiento) + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString() + "\\" + postedFile.GetName();

                                postedFile.SaveAs(SaveLocation);





                                Div2.InnerHtml += string.Format("<br /><b>ARCHIVO GUARDADO</b>");



                            }
                            catch (Exception ex)
                            {
                                Div2.InnerHtml += string.Format("<br /><b>ERROR </b>" + ex.Message);
                                Alerta(ex.Message);
                                //Note: Exception.Message returns a detailed message that describes the current exception. 
                                //For security reasons, we do not recommend that you return Exception.Message to end users in 
                                //production environments. It would be better to put a generic error message. 
                                return false;
                            }
                        }
                        else
                        {
                            Div2.InnerHtml += "<br />No ha seleccionado archivos.";
                        }
                    }
                    else
                    {
                        Div2.InnerHtml += "<br />No ha seleccionado archivos.";
                    }
                }


                try
                {
                    string pathname = Server.MapPath(DirectorioAlmenamiento);

                    string[] filename = Directory.GetFiles(Server.MapPath(DirectorioAlmenamiento) + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString() + "");

                    using (ZipFile zip = new ZipFile())
                    {

                        zip.AddFiles(filename, "file");

                        zip.Save(pathname + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString() + ".zip");
                        // zip.Save(Server.MapPath("~/zipa.zip"));

                        Directory.Delete(Server.MapPath(DirectorioAlmenamiento) + "\\" + CDI.ToString() + "\\" + Id_Rem.ToString(), true);

                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Alerta(ex.Message);
                    return false;
                }

            }

            return false;
        }
        public void OcultarMostrarDocSoporte(string FolioRemision)
        {
            Sesion sesion = new Sesion();

            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int CDI = sesion.Id_Cd;

            string DirectorioAlmenamiento = ConfigurationManager.AppSettings["URLDirectorioSoporteRemision"].ToString();
            if (!File.Exists(Server.MapPath(DirectorioAlmenamiento + "\\" + CDI.ToString() + "\\" + FolioRemision + ".zip")))
            {
                uploadPanel.Visible = true;
                DivArchivoSoporte.Visible = false;
            }
            else
            {
                uploadPanel.Visible = true;
                DivArchivoSoporte.Visible = false;

                //uploadPanel.Visible = false;
                //DivArchivoSoporte.Visible = true;
                //LinkDescargaArchivo.HRef = DirectorioAlmenamiento + "\\" + CDI.ToString() + "\\" + FolioRemision + ".zip";
            }

        }

        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, "Alerta");
            }
        }

        public void ButtonSendSoporteRem_Click(object sender, System.EventArgs e)
        {
            var result = GuardarSoporteRemision();

            if (result)
            {
                Alerta("Documento Guardado");
            }

        }



    }
}