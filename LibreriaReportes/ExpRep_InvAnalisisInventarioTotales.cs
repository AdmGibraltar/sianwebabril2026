namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Globalization;

    /// <summary>
    /// Summary description for ExpRep_InvAnalisisInventarioTotales.
    /// </summary>
    public partial class ExpRep_InvAnalisisInventarioTotales : Telerik.Reporting.Report
    {
        public ExpRep_InvAnalisisInventarioTotales()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;
        }

        private void ExpRep_InvAnalisisInventarioTotales_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["Fecha"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["Fecha"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Orden"].Value = this.ReportParameters["Ordenadopor"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Dias"].Value = this.ReportParameters["Apartirde"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_General"].Value = 1;
                this.sqlSelectCommand1.CommandText = "dbo.spRepAnalisisInventario";

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                DateTime fecha_Actual = DateTime.Now;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}