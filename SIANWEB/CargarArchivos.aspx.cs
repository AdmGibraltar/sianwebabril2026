using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using DevExpress.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CargarArchivos : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }


        #endregion

        protected string SubmissionID
        {
            get
            {
                return HiddenField.Get("SubmissionID").ToString();
            }
            set
            {
                HiddenField.Set("SubmissionID", value);
            }
        }
        UploadedFilesStorage UploadedFilesStorage
        {
            get { return UploadControlHelper3.GetUploadedFilesStorageByKey(SubmissionID); }
        }
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            UploadControlHelper3.RemoveOldStorages3();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SubmissionID = UploadControlHelper3.GenerateUploadedFilesStorageKey();
                UploadControlHelper3.AddUploadedFilesStorage(SubmissionID);
            }
        }

        public void Importar(byte[] Data)
        {
            try
            {
                //Save the uploaded Excel file.
                string FileName = "";
                SaveData(Data, ref FileName);

                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(FileName))
                {
                    IXLWorksheet workSheet;
                    //Read the first Sheet from Excel file.
                    if (Request.QueryString["tipo"].ToString() == "1")
                    {
                        workSheet = workBook.Worksheet("Hoja1");
                    }
                    else
                    {
                        workSheet = workBook.Worksheet("Hoja1");
                    }

                    //Create a new DataTable.
                    DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {

                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                if (dt.Columns.Count > i)
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }

                    }
                    int verificador = 0;
                    CNRSCAgenda CN = new CNRSCAgenda();
                    AgendaRsc agenda;
                    if (Request.QueryString["tipo"].ToString() == "1")
                    {
                        string mensajes = "";
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i].ItemArray[0].ToString() != "")
                            {
                                agenda = new AgendaRsc();

                                DateTime fechaInical = DateTime.Parse(dt.Rows[i].ItemArray[3].ToString()).Date + DateTime.Parse("08:00:00").TimeOfDay;
                                DateTime fechaterminal = DateTime.Parse(dt.Rows[i].ItemArray[3].ToString()).Date + DateTime.Parse("18:00:00").TimeOfDay;

                                agenda.Id_Emp = sesion.Id_Emp;
                                agenda.Id_Cd = sesion.Id_Cd;
                                agenda.usuario = dt.Rows[i].ItemArray[0].ToString();
                                agenda.id_cte = Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString());
                                agenda.HoraInicio = fechaInical;
                                agenda.HoraFinal = fechaterminal;
                                CN.AltaAgendaCargaARchivo(agenda, ref verificador, sesion.Emp_Cnx);

                                if (verificador != 0)
                                {
                                    mensajes = mensajes + "</br>," + "el cliente " + Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString()) + " " + dt.Rows[i].ItemArray[2].ToString() + ", no esta relacionado a un territorio de servicio";
                                    continue;
                                }
                            }
                        }
                        if (mensajes == "")
                        {
                            mensaje("Se guardo la información exitosamente.");
                        }
                        else
                        {
                            mensaje(mensajes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje(ex.Message);
            }
        }


        #region Mensajes

        /// <summary>
        /// Abre el modal de mensaje
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        /// <summary>
        /// Abre el modal de mensaje si se requiere con pregunta
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensajeDecision(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalQuestion", "modalQuestion('" + mensaje + "')", true);
        }

        protected void WCompararRepresentantes_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "comparar", "comparar()", false);
        }



        #endregion

        private void CargarMes(string mes, ref int NumeroMes)
        {


            if (mes.Contains("Enero"))
            {
                NumeroMes = 1;
            }
            if (mes.Contains("Febrero"))
            {
                NumeroMes = 2;
            }
            if (mes.Contains("Marzo"))
            {
                NumeroMes = 3;
            }
            if (mes.Contains("Abril"))
            {
                NumeroMes = 4;
            }
            if (mes.Contains("Mayo"))
            {
                NumeroMes = 5;
            }
            if (mes.Contains("Junio"))
            {
                NumeroMes = 6;
            }
            if (mes.Contains("Julio"))
            {
                NumeroMes = 7;
            }
            if (mes.Contains("Agosto"))
            {
                NumeroMes = 8;
            }
            if (mes.Contains("Septiembre"))
            {
                NumeroMes = 9;
            }
            if (mes.Contains("Octubre"))
            {
                NumeroMes = 10;
            }
            if (mes.Contains("Noviembre"))
            {
                NumeroMes = 11;
            }
            if (mes.Contains("Diciembre"))
            {
                NumeroMes = 12;
            }
        }

        protected bool SaveData(byte[] Data, ref string FileName)
        {
            BinaryWriter Writer = null;
            FileName = Server.MapPath("~/Reportes");
            FileName = FileName + "/presupuesto" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                // Create a new stream to write ~to the file
                Writer = new BinaryWriter(File.OpenWrite(FileName));

                // Writer raw data                
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                //...
                return false;
            }

            return true;
        }
        protected void DocumentsUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            bool isSubmissionExpired = false;
            if (UploadedFilesStorage == null)
            {
                isSubmissionExpired = true;
                UploadControlHelper3.AddUploadedFilesStorage(SubmissionID);
            }
            UploadedFileInfo tempFileInfo = UploadControlHelper3.AddUploadedFileInfo(SubmissionID, e.UploadedFile.FileName);

            e.UploadedFile.SaveAs(tempFileInfo.FilePath);

            if (e.IsValid)
                e.CallbackData = tempFileInfo.UniqueFileName + "|" + isSubmissionExpired;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ValidateInputData();

            List<UploadedFileInfo> resultFileInfos = new List<UploadedFileInfo>();

            bool allFilesExist = true;

            if (UploadedFilesStorage == null)
                UploadedFilesTokenBox.Tokens = new TokenCollection();

            foreach (string fileName in UploadedFilesTokenBox.Tokens)
            {
                UploadedFileInfo demoFileInfo = UploadControlHelper3.GetDemoFileInfo(SubmissionID, fileName);
                FileInfo fileInfo = new FileInfo(demoFileInfo.FilePath);

                if (fileInfo.Exists)
                {
                    ProcessSubmit(demoFileInfo);
                    demoFileInfo.FileSize = fileInfo.Length.ToString();
                    resultFileInfos.Add(demoFileInfo);
                }
                else
                    allFilesExist = false;
            }



            if (allFilesExist && resultFileInfos.Count > 0)
            {
                UploadedFilesTokenBox.ErrorText = "Archivos cargados exitosamente.";
                UploadedFilesTokenBox.IsValid = false;

                UploadControlHelper3.RemoveUploadedFilesStorage(SubmissionID);
                ASPxEdit.ClearEditorsInContainer(FormLayout, true);
            }
            else
            {
                UploadedFilesTokenBox.ErrorText = "Arhivo no cargado. Revise la informacion del archivo.";
                UploadedFilesTokenBox.IsValid = false;
            }
        }

        protected void ProcessSubmit(UploadedFileInfo fileInfo)
        {
            // process uploaded files here
            byte[] fileContent = File.ReadAllBytes(fileInfo.FilePath);
            Importar(fileContent);
        }

        void ValidateInputData()
        {
            bool isInvalid = UploadedFilesTokenBox.Tokens.Count == 0;
            if (isInvalid)
                throw new Exception("Invalid input data");
        }
    }

    public static class UploadControlHelper3
    {
        const int DisposeTimeout = 5;
        const string TempDirectory = "~/UploadControl/Temp/";
        static readonly object storageListLocker = new object();

        static HttpContext Context { get { return HttpContext.Current; } }
        static string RootDirectory { get { return Context.Request.MapPath(TempDirectory); } }

        static IList<UploadedFilesStorage> uploadedFilesStorageList;
        static IList<UploadedFilesStorage> UploadedFilesStorageList
        {
            get
            {
                return uploadedFilesStorageList;
            }
        }

        static UploadControlHelper3()
        {
            uploadedFilesStorageList = new List<UploadedFilesStorage>();
        }

        static string CreateTempDirectoryCore()
        {
            string uploadDirectory = Path.Combine(RootDirectory, Path.GetRandomFileName());
            Directory.CreateDirectory(uploadDirectory);

            return uploadDirectory;
        }
        public static UploadedFilesStorage GetUploadedFilesStorageByKey(string key)
        {
            lock (storageListLocker)
            {
                return GetUploadedFilesStorageByKeyUnsafe(key);
            }
        }
        static UploadedFilesStorage GetUploadedFilesStorageByKeyUnsafe(string key)
        {
            UploadedFilesStorage storage = UploadedFilesStorageList.Where(i => i.Key == key).SingleOrDefault();
            if (storage != null)
                storage.LastUsageTime = DateTime.Now;
            return storage;
        }
        public static string GenerateUploadedFilesStorageKey()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static void AddUploadedFilesStorage(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage storage = new UploadedFilesStorage
                {
                    Key = key,
                    Path = CreateTempDirectoryCore(),
                    LastUsageTime = DateTime.Now,
                    Files = new List<UploadedFileInfo>()
                };
                UploadedFilesStorageList.Add(storage);
            }
        }
        public static void RemoveUploadedFilesStorage(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage storage = GetUploadedFilesStorageByKeyUnsafe(key);
                if (storage != null)
                {
                    Directory.Delete(storage.Path, true);
                    UploadedFilesStorageList.Remove(storage);
                }
            }
        }
        public static void RemoveOldStorages3()
        {
            if (!Directory.Exists(RootDirectory))
                Directory.CreateDirectory(RootDirectory);

            lock (storageListLocker)
            {
                string[] existingDirectories = Directory.GetDirectories(RootDirectory);
                foreach (string directoryPath in existingDirectories)
                {
                    UploadedFilesStorage storage = UploadedFilesStorageList.Where(i => i.Path == directoryPath).SingleOrDefault();
                    if (storage == null || (DateTime.Now - storage.LastUsageTime).TotalMinutes > DisposeTimeout)
                    {
                        Directory.Delete(directoryPath, true);
                        if (storage != null)
                            UploadedFilesStorageList.Remove(storage);
                    }
                }
            }
        }
        public static UploadedFileInfo AddUploadedFileInfo(string key, string originalFileName)
        {
            UploadedFilesStorage currentStorage = GetUploadedFilesStorageByKey(key);
            UploadedFileInfo fileInfo = new UploadedFileInfo
            {
                FilePath = Path.Combine(currentStorage.Path, Path.GetRandomFileName()),
                OriginalFileName = originalFileName,
                UniqueFileName = GetUniqueFileName(currentStorage, originalFileName)
            };
            currentStorage.Files.Add(fileInfo);

            return fileInfo;
        }
        public static UploadedFileInfo GetDemoFileInfo(string key, string fileName)
        {
            UploadedFilesStorage currentStorage = GetUploadedFilesStorageByKey(key);
            return currentStorage.Files.Where(i => i.UniqueFileName == fileName).SingleOrDefault();
        }

        public static string GetUniqueFileName(UploadedFilesStorage currentStorage, string fileName)
        {
            string baseName = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            int index = 1;

            while (currentStorage.Files.Any(i => i.UniqueFileName == fileName))
                fileName = string.Format("{0} ({1}){2}", baseName, index++, ext);

            return fileName;
        }
    }


}