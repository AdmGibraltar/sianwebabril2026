using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace LibreriaReportes
{
    /// <summary>
    /// Summary description for Rep_Compras.
    /// </summary>
    public partial class Rep_Compras : Telerik.Reporting.Report
    {
        public Rep_Compras()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            DataSource = null;
        }

        private void Rep_Compras_NeedDataSource(object sender, EventArgs e)
        {
            sqlDataSource.ConnectionString = this.ReportParameters["conexion"].Value.ToString();

            sqlDataSource.Parameters["@proveedor"].Value = ReportParameters["proveedor"].Value;
            sqlDataSource.Parameters["@fechaIni"].Value = ReportParameters["fechaInicio"].Value;
            sqlDataSource.Parameters["@fechaFin"].Value = ReportParameters["fechaFinal"].Value;

            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = sqlDataSource;
        }
    }
}