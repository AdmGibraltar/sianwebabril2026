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
    /// Summary description for ExpRep_InvAnalisisInventario.
    /// </summary>
    public partial class ExpRep_InvAnalisisInventario : Telerik.Reporting.Report
    {
        public ExpRep_InvAnalisisInventario()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;
        }

        private void ExpRep_InvAnalisisInventario_NeedDataSource(object sender, EventArgs e)
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
                this.sqlSelectCommand1.CommandText = "dbo.spRepAnalisisInventarioReporteSucursal";

                string ordentitulo = this.ReportParameters["Ordentitulo"].Value.ToString();
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                DateTime fecha_Actual = DateTime.Now;

                //JFCV agregue dos parametros para controlar lo del mes actual y mes de cierre  22jul2019
                int mesactual = Convert.ToInt32(this.ReportParameters["MesActual"].Value);
                int actualocierre = Convert.ToInt32(this.ReportParameters["ActualoCierre"].Value);

                //if (Convert.ToInt32(this.ReportParameters["Id_Prd"].Value) == 2)
                //{
                //fecha_Actual = Convert.ToDateTime(this.ReportParameters["Fecha"].Value);
                if (actualocierre == 2)
                {

                    //JFCV 22jul2019 calcular fecha actual
                    fecha_Actual = new DateTime(DateTime.Now.Year, mesactual, 1);
                    textBox59.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-6).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-6).ToString("-yy"));
                    textBox60.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-5).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-5).ToString("-yy"));
                    textBox61.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-4).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-4).ToString("-yy"));
                    textBox24.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-3).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-3).ToString("-yy"));
                    textBox25.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-2).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-2).ToString("-yy"));
                    textBox26.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-1).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-1).ToString("-yy"));
                }
                else
                {
                    //JFCV 22jul2019
                    fecha_Actual = new DateTime(DateTime.Now.Year, mesactual, 1);
                    textBox59.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-5).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-5).ToString("-yy"));
                    textBox60.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-4).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-4).ToString("-yy"));
                    textBox61.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-3).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-3).ToString("-yy"));
                    textBox24.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-2).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-2).ToString("-yy"));
                    textBox25.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-1).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-1).ToString("-yy"));
                    textBox26.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(0).ToString("MMM")).Substring(0, 3) + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(0).ToString("-yy"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}