namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_RPlaneacionCteA.
    /// </summary>
    public partial class RepCumplimientoVISemRSC : Telerik.Reporting.Report
    {
        public RepCumplimientoVISemRSC()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
              this.DataSource = null; // this.sqlDataAdapter1;
        }

        private void RepCumplimientoVISemDeta_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Semanas"].Value = this.ReportParameters["Semanas"].Value;
                //                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Seg"].Value = this.ReportParameters["Id_Seg"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Tipo"].Value;
                

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                //if (this.ReportParameters["strSem1"].Value == "") 
                //{this.textBox19.Visible = false;}
                //else
                //{ this.textBox19.Visible = true; }
                int cuenta = 0;
                if (this.ReportParameters["strSem2"].Value == "")
                { 
                    this.textBox21.Visible = false;
                    this.textBox62.Visible = false;
                    this.textBox34.Visible = false;
                    this.textBox51.Visible = false;
                    this.textBox52.Visible = false;
                }
                else
                {
                    cuenta = cuenta + 1;
                    this.textBox21.Visible = true;
                    this.textBox62.Visible = true;
                    this.textBox34.Visible = true;
                    this.textBox51.Visible = true;
                    this.textBox52.Visible = true;
                }

                if (this.ReportParameters["strSem3"].Value == "")
                { 
                    this.textBox23.Visible = false;
                    this.textBox63.Visible = false;
                    this.textBox44.Visible = false;
                    this.textBox53.Visible = false;
                    this.textBox54.Visible = false;
                }
                else
                {
                    cuenta = cuenta + 1;
                    this.textBox23.Visible = true;
                    this.textBox63.Visible = true;
                    this.textBox44.Visible = true;
                    this.textBox53.Visible = true;
                    this.textBox54.Visible = true;
                }

                if (this.ReportParameters["strSem4"].Value == "")
                { 
                    this.textBox26.Visible = false;
                    this.textBox64.Visible = false;
                    this.textBox43.Visible = false;
                    this.textBox55.Visible = false;
                    this.textBox56.Visible = false;
                }
                else
                {
                    cuenta = cuenta + 1;
                    this.textBox26.Visible = true;
                    this.textBox64.Visible = true;
                    this.textBox43.Visible = true;
                    this.textBox55.Visible = true;
                    this.textBox56.Visible = true;
                }

                if (this.ReportParameters["strSem5"].Value == "")
                {
                    this.textBox32.Visible = false;
                    this.textBox65.Visible = false;
                    this.textBox42.Visible = false;
                    this.textBox57.Visible = false;
                    this.textBox58.Visible = false;
                }
                else
                {
                    cuenta = cuenta + 1;
                    this.textBox32.Visible = true;
                    this.textBox65.Visible = true;
                    this.textBox42.Visible = true;
                    this.textBox57.Visible = true;
                    this.textBox58.Visible = true;
                }

                /// aplica igual para el de RSC, solo confirmar las medidas
                double UX = 19.1 + (cuenta * 2.3);
                // desplegado y lineas
                this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(UX), Telerik.Reporting.Drawing.Unit.Cm(3.9968166351318359D));
                //  $
                UX = UX + 0.2;
                this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(UX), Telerik.Reporting.Drawing.Unit.Cm(4.8535933494567871D));
                //  Unidades
                UX = UX + 1.6;
                this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(UX), Telerik.Reporting.Drawing.Unit.Cm(4.8535933494567871D));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}