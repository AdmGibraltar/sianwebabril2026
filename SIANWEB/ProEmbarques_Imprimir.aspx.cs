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
using System.Collections;
using DevExpress.Export.Xl;

namespace SIANWEB
{
    public partial class ProEmbarques_Imprimir : System.Web.UI.Page
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
        #endregion Variables
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.txtIdEmb.Text = Page.Request.QueryString["Id_Emb"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "print")
                {
                    if (Page.IsValid)
                    {
                        this.Imprimir();
                    }
                }
                if (btn.CommandName == "expruta")
                {
                    if (Page.IsValid)
                    {
                        switch (this.optOpciones.SelectedItem.Value)
                        {
                            case "rdFacturaCte":
                                this.ImprimirExcel();
                                break;
                            case "rdFacturaPro":
                                this.ImprimirExcel();
                                break;
                            case "rdFacturaFacPro":
                                this.ImprimirExcel();
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos
        #region Metodos
        private void Imprimir()
        {
            try
            {

                CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                CapaDatos.Funciones fecha = new CapaDatos.Funciones();
                ArrayList ALValorParametrosInternos = new ArrayList();
                CapaDatos.Funciones CD = new CapaDatos.Funciones();
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Convert.ToInt32(this.txtIdEmb.Text));


                Type instance = null;

                switch (this.optOpciones.SelectedItem.Value)
                {
                    case "rdFacturaCte":
                        ALValorParametrosInternos.Add(2);
                        instance = typeof(LibreriaReportes.ProFacturaRuta_Cte);
                        break;
                    case "rdFacturaPro":
                        ALValorParametrosInternos.Add(2);
                        instance = typeof(LibreriaReportes.ProFacturaRuta_Pro);
                        break;
                    case "rdFacturaFacPro":
                        ALValorParametrosInternos.Add(3);
                        instance = typeof(LibreriaReportes.ProFacturaRuta_FacPro);
                        break;
                }

                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirExcel()
        {
            try
            {
                CN_ProEmbarque clsCatArea = new CN_ProEmbarque();
                Embarques embarques = new Embarques();

                embarques.Id_Emb = Convert.ToInt32(txtIdEmb.Text);
                embarques.Id_Cd = sesion.Id_Cd_Ver;
                embarques.Id_Emp = sesion.Id_Emp;

                List<EmbarquesReporte> List = new List<EmbarquesReporte>();
                clsCatArea.ListaEmbarque(embarques, sesion.Emp_Cnx, ref List);

                //foreach (EmbarquesReporte ped in List)
                //{


                //    if (ped.Id_Emb == 0)
                //    {
                //        ped.Id_TG = 0;
                //        ped.ModalidadGarantia = "Regular"; //cnTv.Tradicional.TV_Nombre;
                //    }
                //}
                Session["gridembarques"] = List;


                List = (List<EmbarquesReporte>)Session["gridembarques"];

                // Create an exporter instance.  
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                var mappath = HttpContext.Current.Server.MapPath("~/Reportes/RerpoteCaptacionPedidos" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                // Create the FileStream object with the specified file path.  

                if (File.Exists(mappath))
                    File.Delete(mappath);

                #region excel
                using (FileStream stream = new FileStream(mappath, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Create a new document and begin to write it to the specified stream.  
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.  
                        using (IXlSheet sheet = document.CreateSheet())
                        {
                            // Specify the worksheet name. 
                            sheet.Name = "IMPORTADOR";

                            // Create the first column and set its width.  
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            // Create the second column and set its width. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 250;
                            }

                            // Create the third column and set the specific number format for its cells. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 350;
                            }


                            using (IXlColumn column = sheet.CreateColumn()) //cantidad
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //codigo
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //id contacto
                            {
                                column.WidthInPixels = 150;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Nombrecontacto
                            {
                                column.WidthInPixels = 350;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Telefono
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // Email_contacto
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // direccion
                            {
                                column.WidthInPixels = 450;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Latitud
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Longitud
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Fecha min entrega
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // fecha max entrega
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // CT DESTINO
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Pesovolumne
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // importancia
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // service_time
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // camposavanzados
                            {
                                column.WidthInPixels = 200;
                            }
                            // Specify cell font attributes. 
                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Century Gothic";
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row. 
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0));


                            // Create the header row. 
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número de documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "VEHICULO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "NOMBRE ITEM";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CANTIDAD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CODIGO ITEM";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "IDENTIFICADOR CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "NOMBRE CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "TELEFONO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "EMAIL CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "DIRECCION";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "LATITUD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "LONGITUD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "FECHA MIN ENTREGA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "FECHA MAX ENTREGA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CT DESTINO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "PESO O VOLUMEN";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "IMPORTANCIA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "SERVICE_TIME";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CAMPOS AVANZADOS";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                            }

                            // Generate data for the sales report.  
                            for (int i = 0; i < List.Count; i++)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Doc;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Emb_Camioneta;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Prd_Descripcion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Fac_Cant;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Prd;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].NomContacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Telefono;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Email_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Direccion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Latitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Longitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday–Saturday, months as January–December, days as 1–31 and years as 1900–9999. 
                                        cell.Value = List[i].Fecha_MIN_ENTREGA;
                                        cell.Formatting = new XlCellFormatting();
                                        cell.Formatting.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday–Saturday, months as January–December, days as 1–31 and years as 1900–9999. 
                                        cell.Value = List[i].Fecha_Max_entrega;
                                        cell.Formatting = new XlCellFormatting();
                                        cell.Formatting.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                    }

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].Fecha_MIN_ENTREGA;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}
                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].Fecha_Max_entrega;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].CT_Destino;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    // Display 9999.45 as $4,310.45. 
                                    //    cell.Value = Convert.ToString(List[i].PesoVolumen);
                                    //    cell.Formatting = new XlCellFormatting();
                                    //    cell.Formatting.NumberFormat = @"_([-409]* #,##0.00_);_([-409]* \(#,##0.00\);_([-409]* ""-""??_);_(@_)";
                                    //}
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = Convert.ToString(List[i].PesoVolumen);
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Importancia;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].ServiceTime;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Campos_Avanzados;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                }
                            }

                            // Enable AutoFilter for the created cell range. 
                            sheet.AutoFilterRange = sheet.DataRange;
                            // Specify formatting settings for the total row. 
                            XlCellFormatting totalRowFormatting = new XlCellFormatting();
                            totalRowFormatting.CopyFrom(cellFormatting);
                            totalRowFormatting.Font.Bold = true;
                            totalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent5, 0.6));


                        }
                    }
                }
                #endregion excel

                Console.WriteLine(mappath);
                // Open the XLSX document using the default application. 
                ////System.Diagnostics.Process.Start(mappath);

                if (File.Exists(mappath))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\ReporteRutasEmbarque.xlsx";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(mappath, Server.MapPath("Reportes") + "\\ReporteRutasEmbarque.xlsx");
                    Response.Redirect("Reportes\\ReporteRutasEmbarque.xlsx", false);
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex.Message);
            }
        }


        #endregion Metodos
        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {

                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
        #endregion
    }
}