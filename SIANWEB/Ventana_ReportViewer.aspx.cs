using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Collections;
using Telerik.Reporting;
using System.IO;
using SIANWEB.API;
using CapaNegocios;
using System.Configuration;

namespace SIANWEB
{
    public partial class Ventana_ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Dictionary<int, ReportInstances> rptInstances = new Dictionary<int, ReportInstances>();

                if (Sesion == null)
                {

                }
                else
                {
                    if (Page.IsPostBack == false)
                    {
                        string reportName;
                        ArrayList ALValorParametrosInternos;
                        Session["Head" + Session.SessionID] = "Reporte";
                        string cve_pagina = "";

                        if (Page.Request.QueryString["cve"] != null)
                        {
                            cve_pagina = Page.Request.QueryString["cve"].ToString();
                            reportName = Session["assembly" + Session.SessionID + cve_pagina].ToString();
                            rptInstances = (Dictionary<int, ReportInstances>)Session["assemblies" + Session.SessionID];
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID + cve_pagina];
                        }
                        else
                        {
                            reportName = Session["assembly" + Session.SessionID].ToString();
                            rptInstances = (Dictionary<int, ReportInstances>)Session["assemblies" + Session.SessionID];
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID];
                        }

                        if (!string.IsNullOrEmpty(reportName))
                        {
                            Func<string, Report> getReport = (s) =>
                            {
                                Type reportType = Type.GetType(s);
                                return (Report)Activator.CreateInstance(reportType);
                            };

                            Report report = getReport(reportName);

                            if (rptInstances != null && rptInstances.Any())
                            {
                                ReportBook rb = new ReportBook();

                                foreach (KeyValuePair<int, ReportInstances> item in rptInstances)
                                {
                                    Report rpt = getReport(item.Value.ReportInstance);
                                    if (item.Value.Parameters != null)
                                    {
                                        for (int i = 0; i <= item.Value.Parameters.Count - 1; i++)
                                        {
                                            rpt.ReportParameters[i].AllowNull = true;
                                            rpt.ReportParameters[i].Value = item.Value.Parameters[i];

                                        }
                                    }
                                    rb.Reports.Add(rpt);
                                }

                                this.ReportViewer1.ReportSource = rb;
                                Session["assemblies" + Session.SessionID] = new Dictionary<int, ReportInstances>();
                            }
                            else
                            {
                                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                                {
                                    report.ReportParameters[i].AllowNull = true;
                                    report.ReportParameters[i].Value = ALValorParametrosInternos[i];
                                }

                                this.ReportViewer1.ReportSource = report;
                                SaveReport(report, cve_pagina);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SaveReport(Telerik.Reporting.Report report, string cve_pagina)
        {
            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            instanceReportSource.ReportDocument = report;
            Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);



            if (Session["idRem" + Session.SessionID + cve_pagina] != null)
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_Rem = Convert.ToInt32(Session["idRem" + Session.SessionID + cve_pagina].ToString());

                string fileName = result.DocumentName + "Remision" + Id_Rem + "." + result.Extension;
                string path = Server.MapPath("Reportes") + "\\archivopdf" + fileName;


                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                FileStream fs = new FileStream(path,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Close();


                Remision remisiones = new Remision();

                remisiones.Id_Emp = sesion.Id_Emp;
                remisiones.Id_Cd = sesion.Id_Cd_Ver;
                remisiones.Id_Rem = Id_Rem;


                int numoped = 0;
                int id_Cte = 0;

                CN_CapRemision rem = new CN_CapRemision();
                rem.OntenerNumPed(remisiones, sesion.Emp_Cnx, ref numoped, ref id_Cte);

                remisiones.Id_Cte = id_Cte;
                remisiones.Id_Ped = numoped;

                int verificador = 0;
                CN_Eccommerce Eccommerce = new CN_Eccommerce();
                Eccommerce.ValidarPedidoEcommerce(remisiones, sesion.Emp_Cnx, ref verificador);
                string estatus = "";
                string mensaje = "";

                string path2 = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), ("/Reportes" + "/archivopdf" + fileName));

                if (verificador > 0)
                {
                    remisiones.id_pedMag = verificador;
                    APISKEY APis = new APISKEY();

                    APis.RemisionPedido(remisiones, path2, sesion.Emp_Cnx, ref estatus, ref mensaje);
                }
            }
            else
            {
                string fileName = result.DocumentName + "." + result.Extension;
                string path = Server.MapPath("Reportes") + "\\archivopdf" + fileName;


                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                FileStream fs = new FileStream(path,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Close();
            }
        }



        private void Salir()
        {
            try
            {
                string funcion = null;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}