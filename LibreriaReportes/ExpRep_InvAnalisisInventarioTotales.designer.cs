namespace LibreriaReportes
{
    partial class ExpRep_InvAnalisisInventarioTotales
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox48 = new Telerik.Reporting.TextBox();
            this.textBox47 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox58 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox52 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D);
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D);
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.23354172706604D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox32,
            this.textBox34,
            this.textBox3,
            this.textBox43,
            this.textBox37,
            this.textBox48,
            this.textBox47,
            this.textBox6,
            this.textBox5,
            this.textBox58,
            this.textBox13});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.4700140953063965D), Telerik.Reporting.Drawing.Unit.Cm(0.60019993782043457D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.999969482421875D), Telerik.Reporting.Drawing.Unit.Cm(0.59980005025863647D));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Verdana";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox32.Value = "Análisis de inventario por Promedio de Ventas";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.4700140953063965D), Telerik.Reporting.Drawing.Unit.Cm(1.2616315991920146E-08D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.999969482421875D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Name = "Verdana";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Value = "= Parameters.NombreEmpresa";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Verdana";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox3.Value = "Sucursal";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.673885345458984D), Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Name = "Verdana";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox43.Value = "Dias que Afecta\r\nSobre Inventario";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.429553985595703D), Telerik.Reporting.Drawing.Unit.Cm(1.4035418033599854D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.Font.Name = "Verdana";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox37.Value = "Día de \r\nInventario";
            // 
            // textBox48
            // 
            this.textBox48.Format = "{0:dd/MM/yyy}";
            this.textBox48.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7156248092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox48.Style.Font.Name = "Verdana";
            this.textBox48.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox48.Value = "= Parameters.Fecha";
            // 
            // textBox47
            // 
            this.textBox47.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.899899959564209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox47.Style.Font.Bold = true;
            this.textBox47.Style.Font.Name = "Verdana";
            this.textBox47.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox47.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox47.Value = "Fecha";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.25910758972168D), Telerik.Reporting.Drawing.Unit.Cm(1.4083333015441895D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82520818710327148D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Verdana";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox6.Value = "Desabasto";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0147767066955566D), Telerik.Reporting.Drawing.Unit.Cm(1.4035418033599854D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Verdana";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox5.Value = "Monto de \r\nVenta";
            // 
            // textBox58
            // 
            this.textBox58.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(1.4035418033599854D));
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox58.Style.Font.Bold = true;
            this.textBox58.Style.Font.Name = "Verdana";
            this.textBox58.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox58.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox58.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox58.Value = "Monto \r\nInventario";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.844330787658691D), Telerik.Reporting.Drawing.Unit.Cm(1.3952081203460693D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.82999998331069946D));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Verdana";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox13.Value = "Sobre \r\nInventario";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.55291682481765747D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox20,
            this.textBox39,
            this.textBox52,
            this.textBox22,
            this.textBox14,
            this.textBox21});
            this.detail.Name = "detail";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.0764584094285965D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox15.Style.Font.Name = "Arial";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Value = "= Fields.MontoInventario";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:N2}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0147767066955566D), Telerik.Reporting.Drawing.Unit.Cm(0.0764584094285965D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox20.Style.Font.Name = "Arial";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Value = "= Fields.MontoVenta";
            // 
            // textBox39
            // 
            this.textBox39.Format = "{0:N2}";
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.429553985595703D), Telerik.Reporting.Drawing.Unit.Cm(0.0793749988079071D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox39.Style.Font.Name = "Arial";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox39.Value = "= Fields.DiaInventario";
            // 
            // textBox52
            // 
            this.textBox52.Format = "{0:N2}";
            this.textBox52.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.673885345458984D), Telerik.Reporting.Drawing.Unit.Cm(0.0764584094285965D));
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox52.Style.Font.Name = "Arial";
            this.textBox52.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox52.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox52.Value = "= Fields.diaafecta";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.0764584094285965D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox22.Style.Font.Name = "Arial";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.TextWrap = false;
            this.textBox22.Value = "=Fields.[CD_Nombre]";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.844330787658691D), Telerik.Reporting.Drawing.Unit.Cm(0.079999998211860657D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox14.Style.Font.Name = "Arial";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Value = "= Fields.MontoSobreInventario";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:N2}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.25910758972168D), Telerik.Reporting.Drawing.Unit.Cm(0.0764584094285965D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4147770404815674D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox21.Style.Font.Name = "Arial";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Value = "= Fields.Cobertura";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=40.84.229.61;Initial Catalog=SIANCENTRAL;Persist Security Info=True;U" +
    "ser ID=sa;Password=4dmK3yQu1m";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.ConnectionString = "Data Source=(local);Initial Catalog=SIANWEBMTYP;User ID=sa;Password=sistemas";
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spRepAnalisisInventario";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection2;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Id_Orden", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Id_Dias", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Id_General", System.Data.SqlDbType.Int, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "1")});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spRepAnalisisInventario", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Cd_Nombre", "Cd_Nombre"),
                        new System.Data.Common.DataColumnMapping("MontoInventario", "MontoInventario"),
                        new System.Data.Common.DataColumnMapping("MontoVenta", "MontoVenta"),
                        new System.Data.Common.DataColumnMapping("MontoSobreInventario", "MontoSobreInventario"),
                        new System.Data.Common.DataColumnMapping("Cobertura", "Cobertura"),
                        new System.Data.Common.DataColumnMapping("DiaInventario", "DiaInventario"),
                        new System.Data.Common.DataColumnMapping("DiaAfecta", "DiaAfecta")}),
            new System.Data.Common.DataTableMapping("Table1", "Table1", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID_Cd", "ID_Cd"),
                        new System.Data.Common.DataColumnMapping("Cd_Nombre", "Cd_Nombre"),
                        new System.Data.Common.DataColumnMapping("Id_Prd", "Id_Prd"),
                        new System.Data.Common.DataColumnMapping("Descripcion", "Descripcion"),
                        new System.Data.Common.DataColumnMapping("Estatus", "Estatus"),
                        new System.Data.Common.DataColumnMapping("Cuenta", "Cuenta"),
                        new System.Data.Common.DataColumnMapping("Mes1", "Mes1"),
                        new System.Data.Common.DataColumnMapping("Mes2", "Mes2"),
                        new System.Data.Common.DataColumnMapping("Mes3", "Mes3"),
                        new System.Data.Common.DataColumnMapping("Mes4", "Mes4"),
                        new System.Data.Common.DataColumnMapping("Mes5", "Mes5"),
                        new System.Data.Common.DataColumnMapping("Mes6", "Mes6"),
                        new System.Data.Common.DataColumnMapping("PromedioVta", "PromedioVta"),
                        new System.Data.Common.DataColumnMapping("Inventario", "Inventario"),
                        new System.Data.Common.DataColumnMapping("Cobertura", "Cobertura"),
                        new System.Data.Common.DataColumnMapping("PrecioAAA", "PrecioAAA"),
                        new System.Data.Common.DataColumnMapping("MontoInventario", "MontoInventario"),
                        new System.Data.Common.DataColumnMapping("MontoVenta", "MontoVenta"),
                        new System.Data.Common.DataColumnMapping("SobreInventario", "SobreInventario"),
                        new System.Data.Common.DataColumnMapping("MontoSobreINventario", "MontoSobreINventario"),
                        new System.Data.Common.DataColumnMapping("DiaInventario", "DiaInventario"),
                        new System.Data.Common.DataColumnMapping("DiaAfecta", "DiaAfecta")}),
            new System.Data.Common.DataTableMapping("Table2", "Table2", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID_Cd", "ID_Cd"),
                        new System.Data.Common.DataColumnMapping("Cd_Nombre", "Cd_Nombre"),
                        new System.Data.Common.DataColumnMapping("Id_Prd", "Id_Prd"),
                        new System.Data.Common.DataColumnMapping("Descripcion", "Descripcion"),
                        new System.Data.Common.DataColumnMapping("Estatus", "Estatus"),
                        new System.Data.Common.DataColumnMapping("Cuenta", "Cuenta"),
                        new System.Data.Common.DataColumnMapping("Mes1", "Mes1"),
                        new System.Data.Common.DataColumnMapping("Mes2", "Mes2"),
                        new System.Data.Common.DataColumnMapping("Mes3", "Mes3"),
                        new System.Data.Common.DataColumnMapping("Mes4", "Mes4"),
                        new System.Data.Common.DataColumnMapping("Mes5", "Mes5"),
                        new System.Data.Common.DataColumnMapping("Mes6", "Mes6"),
                        new System.Data.Common.DataColumnMapping("PromedioVta", "PromedioVta"),
                        new System.Data.Common.DataColumnMapping("Inventario", "Inventario"),
                        new System.Data.Common.DataColumnMapping("Cobertura", "Cobertura"),
                        new System.Data.Common.DataColumnMapping("PrecioAAA", "PrecioAAA"),
                        new System.Data.Common.DataColumnMapping("MontoInventario", "MontoInventario"),
                        new System.Data.Common.DataColumnMapping("MontoVenta", "MontoVenta"),
                        new System.Data.Common.DataColumnMapping("SobreInventario", "SobreInventario"),
                        new System.Data.Common.DataColumnMapping("MontoSobreINventario", "MontoSobreINventario"),
                        new System.Data.Common.DataColumnMapping("DiaInventario", "DiaInventario"),
                        new System.Data.Common.DataColumnMapping("DiaAfecta", "DiaAfecta")})});
            // 
            // ExpRep_InvAnalisisInventarioTotales
            // 
            this.DataSource = this.sqlDataAdapter1;
            group1.GroupFooter = this.groupFooterSection1;
            group1.GroupHeader = this.groupHeaderSection1;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Id_Cd"));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.detail});
            this.Name = "ExpRep_InvAnalisisInventarioTotales";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Id_Emp";
            reportParameter3.Name = "Id_Cd";
            reportParameter4.Name = "Id_Prd";
            reportParameter5.Name = "Tipo";
            reportParameter6.Name = "Fecha";
            reportParameter7.Name = "Ordenadopor";
            reportParameter8.Name = "NombreEmpresa";
            reportParameter9.Name = "NombreSucursal";
            reportParameter10.Name = "FechaActual";
            reportParameter11.Name = "Apartirde";
            reportParameter12.Name = "OrdenTitulo";
            reportParameter13.Name = "DiasTitulo";
            reportParameter14.Name = "ActualoCierre";
            reportParameter14.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter15.Name = "MesActual";
            reportParameter15.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.ReportParameters.Add(reportParameter9);
            this.ReportParameters.Add(reportParameter10);
            this.ReportParameters.Add(reportParameter11);
            this.ReportParameters.Add(reportParameter12);
            this.ReportParameters.Add(reportParameter13);
            this.ReportParameters.Add(reportParameter14);
            this.ReportParameters.Add(reportParameter15);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(26.700002670288086D);
            this.NeedDataSource += new System.EventHandler(this.ExpRep_InvAnalisisInventarioTotales_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox34;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.Group group1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox52;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox48;
        private Telerik.Reporting.TextBox textBox47;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox58;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox21;
    }
}