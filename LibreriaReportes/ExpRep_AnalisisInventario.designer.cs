namespace LibreriaReportes
{
    partial class ExpRep_AnalisisInventario
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
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.cd_NombreCaptionTextBox = new Telerik.Reporting.TextBox();
            this.cd_NombreDataTextBox = new Telerik.Reporting.TextBox();
            this.coberturaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.coberturaDataTextBox = new Telerik.Reporting.TextBox();
            this.cuentaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.cuentaDataTextBox = new Telerik.Reporting.TextBox();
            this.descripcionCaptionTextBox = new Telerik.Reporting.TextBox();
            this.descripcionDataTextBox = new Telerik.Reporting.TextBox();
            this.diaafectaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.diaafectaDataTextBox = new Telerik.Reporting.TextBox();
            this.diaInventarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.diaInventarioDataTextBox = new Telerik.Reporting.TextBox();
            this.estatusCaptionTextBox = new Telerik.Reporting.TextBox();
            this.estatusDataTextBox = new Telerik.Reporting.TextBox();
            this.id_cdCaptionTextBox = new Telerik.Reporting.TextBox();
            this.id_cdDataTextBox = new Telerik.Reporting.TextBox();
            this.id_PrdCaptionTextBox = new Telerik.Reporting.TextBox();
            this.id_PrdDataTextBox = new Telerik.Reporting.TextBox();
            this.inventarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.inventarioDataTextBox = new Telerik.Reporting.TextBox();
            this.mes1CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes1DataTextBox = new Telerik.Reporting.TextBox();
            this.mes2CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes2DataTextBox = new Telerik.Reporting.TextBox();
            this.mes3CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes3DataTextBox = new Telerik.Reporting.TextBox();
            this.mes4CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes4DataTextBox = new Telerik.Reporting.TextBox();
            this.mes5CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes5DataTextBox = new Telerik.Reporting.TextBox();
            this.mes6CaptionTextBox = new Telerik.Reporting.TextBox();
            this.mes6DataTextBox = new Telerik.Reporting.TextBox();
            this.montoInventarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.montoInventarioDataTextBox = new Telerik.Reporting.TextBox();
            this.montoSobreInventarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.montoSobreInventarioDataTextBox = new Telerik.Reporting.TextBox();
            this.montoVentaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.montoVentaDataTextBox = new Telerik.Reporting.TextBox();
            this.precioAAACaptionTextBox = new Telerik.Reporting.TextBox();
            this.precioAAADataTextBox = new Telerik.Reporting.TextBox();
            this.promedioVtaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.promedioVtaDataTextBox = new Telerik.Reporting.TextBox();
            this.sobreInventarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.sobreInventarioDataTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "LibreriaReportes.Properties.Settings.SIANWEBMTYP";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Emp", System.Data.DbType.Int32, "=Parameters.IdEmp.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Cd", System.Data.DbType.Int32, "=Parameters.IdCd.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@FechaIni", System.Data.DbType.DateTime, "=Parameters.FechaIni.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@FechaFin", System.Data.DbType.DateTime, "=Parameters.FechaFin.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_U", System.Data.DbType.Int32, "=Parameters.IdU.Value")});
            this.sqlDataSource1.SelectCommand = "dbo.spRepAnalisisInventario";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(24.408332824707031D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "ExpRep_AnalisisInventario";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.177708625793457D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.283541679382324D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.177708625793457D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1058332920074463D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.cd_NombreCaptionTextBox,
            this.cd_NombreDataTextBox,
            this.coberturaCaptionTextBox,
            this.coberturaDataTextBox,
            this.cuentaCaptionTextBox,
            this.cuentaDataTextBox,
            this.descripcionCaptionTextBox,
            this.descripcionDataTextBox,
            this.diaafectaCaptionTextBox,
            this.diaafectaDataTextBox,
            this.diaInventarioCaptionTextBox,
            this.diaInventarioDataTextBox,
            this.estatusCaptionTextBox,
            this.estatusDataTextBox,
            this.id_cdCaptionTextBox,
            this.id_cdDataTextBox,
            this.id_PrdCaptionTextBox,
            this.id_PrdDataTextBox,
            this.inventarioCaptionTextBox,
            this.inventarioDataTextBox,
            this.mes1CaptionTextBox,
            this.mes1DataTextBox,
            this.mes2CaptionTextBox,
            this.mes2DataTextBox,
            this.mes3CaptionTextBox,
            this.mes3DataTextBox,
            this.mes4CaptionTextBox,
            this.mes4DataTextBox,
            this.mes5CaptionTextBox,
            this.mes5DataTextBox,
            this.mes6CaptionTextBox,
            this.mes6DataTextBox,
            this.montoInventarioCaptionTextBox,
            this.montoInventarioDataTextBox,
            this.montoSobreInventarioCaptionTextBox,
            this.montoSobreInventarioDataTextBox,
            this.montoVentaCaptionTextBox,
            this.montoVentaDataTextBox,
            this.precioAAACaptionTextBox,
            this.precioAAADataTextBox,
            this.promedioVtaCaptionTextBox,
            this.promedioVtaDataTextBox,
            this.sobreInventarioCaptionTextBox,
            this.sobreInventarioDataTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(24.5141658782959D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "ExpRep_AnalisisInventario";
            // 
            // cd_NombreCaptionTextBox
            // 
            this.cd_NombreCaptionTextBox.CanGrow = true;
            this.cd_NombreCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.cd_NombreCaptionTextBox.Name = "cd_NombreCaptionTextBox";
            this.cd_NombreCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cd_NombreCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.cd_NombreCaptionTextBox.StyleName = "Caption";
            this.cd_NombreCaptionTextBox.Value = "Cd_Nombre:";
            // 
            // cd_NombreDataTextBox
            // 
            this.cd_NombreDataTextBox.CanGrow = true;
            this.cd_NombreDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.60885417461395264D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.cd_NombreDataTextBox.Name = "cd_NombreDataTextBox";
            this.cd_NombreDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cd_NombreDataTextBox.StyleName = "Data";
            this.cd_NombreDataTextBox.Value = "=Fields.Cd_Nombre";
            // 
            // coberturaCaptionTextBox
            // 
            this.coberturaCaptionTextBox.CanGrow = true;
            this.coberturaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1647917032241821D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.coberturaCaptionTextBox.Name = "coberturaCaptionTextBox";
            this.coberturaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.coberturaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.coberturaCaptionTextBox.StyleName = "Caption";
            this.coberturaCaptionTextBox.Value = "Cobertura:";
            // 
            // coberturaDataTextBox
            // 
            this.coberturaDataTextBox.CanGrow = true;
            this.coberturaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7207291126251221D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.coberturaDataTextBox.Name = "coberturaDataTextBox";
            this.coberturaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.coberturaDataTextBox.StyleName = "Data";
            this.coberturaDataTextBox.Value = "=Fields.Cobertura";
            // 
            // cuentaCaptionTextBox
            // 
            this.cuentaCaptionTextBox.CanGrow = true;
            this.cuentaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2766666412353516D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.cuentaCaptionTextBox.Name = "cuentaCaptionTextBox";
            this.cuentaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cuentaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.cuentaCaptionTextBox.StyleName = "Caption";
            this.cuentaCaptionTextBox.Value = "cuenta:";
            // 
            // cuentaDataTextBox
            // 
            this.cuentaDataTextBox.CanGrow = true;
            this.cuentaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8326041698455811D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.cuentaDataTextBox.Name = "cuentaDataTextBox";
            this.cuentaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cuentaDataTextBox.StyleName = "Data";
            this.cuentaDataTextBox.Value = "=Fields.cuenta";
            // 
            // descripcionCaptionTextBox
            // 
            this.descripcionCaptionTextBox.CanGrow = true;
            this.descripcionCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3885416984558105D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.descripcionCaptionTextBox.Name = "descripcionCaptionTextBox";
            this.descripcionCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.descripcionCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.descripcionCaptionTextBox.StyleName = "Caption";
            this.descripcionCaptionTextBox.Value = "Descripcion:";
            // 
            // descripcionDataTextBox
            // 
            this.descripcionDataTextBox.CanGrow = true;
            this.descripcionDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.94447922706604D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.descripcionDataTextBox.Name = "descripcionDataTextBox";
            this.descripcionDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.descripcionDataTextBox.StyleName = "Data";
            this.descripcionDataTextBox.Value = "=Fields.Descripcion";
            // 
            // diaafectaCaptionTextBox
            // 
            this.diaafectaCaptionTextBox.CanGrow = true;
            this.diaafectaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5004167556762695D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.diaafectaCaptionTextBox.Name = "diaafectaCaptionTextBox";
            this.diaafectaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.diaafectaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.diaafectaCaptionTextBox.StyleName = "Caption";
            this.diaafectaCaptionTextBox.Value = "diaafecta:";
            // 
            // diaafectaDataTextBox
            // 
            this.diaafectaDataTextBox.CanGrow = true;
            this.diaafectaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.05635404586792D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.diaafectaDataTextBox.Name = "diaafectaDataTextBox";
            this.diaafectaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.diaafectaDataTextBox.StyleName = "Data";
            this.diaafectaDataTextBox.Value = "=Fields.diaafecta";
            // 
            // diaInventarioCaptionTextBox
            // 
            this.diaInventarioCaptionTextBox.CanGrow = true;
            this.diaInventarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.6122918128967285D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.diaInventarioCaptionTextBox.Name = "diaInventarioCaptionTextBox";
            this.diaInventarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.diaInventarioCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.diaInventarioCaptionTextBox.StyleName = "Caption";
            this.diaInventarioCaptionTextBox.Value = "Dia Inventario:";
            // 
            // diaInventarioDataTextBox
            // 
            this.diaInventarioDataTextBox.CanGrow = true;
            this.diaInventarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.1682291030883789D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.diaInventarioDataTextBox.Name = "diaInventarioDataTextBox";
            this.diaInventarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.diaInventarioDataTextBox.StyleName = "Data";
            this.diaInventarioDataTextBox.Value = "=Fields.DiaInventario";
            // 
            // estatusCaptionTextBox
            // 
            this.estatusCaptionTextBox.CanGrow = true;
            this.estatusCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.7241668701171875D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.estatusCaptionTextBox.Name = "estatusCaptionTextBox";
            this.estatusCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.estatusCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.estatusCaptionTextBox.StyleName = "Caption";
            this.estatusCaptionTextBox.Value = "estatus:";
            // 
            // estatusDataTextBox
            // 
            this.estatusDataTextBox.CanGrow = true;
            this.estatusDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.2801041603088379D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.estatusDataTextBox.Name = "estatusDataTextBox";
            this.estatusDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.estatusDataTextBox.StyleName = "Data";
            this.estatusDataTextBox.Value = "=Fields.estatus";
            // 
            // id_cdCaptionTextBox
            // 
            this.id_cdCaptionTextBox.CanGrow = true;
            this.id_cdCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.8360414505004883D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.id_cdCaptionTextBox.Name = "id_cdCaptionTextBox";
            this.id_cdCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.id_cdCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.id_cdCaptionTextBox.StyleName = "Caption";
            this.id_cdCaptionTextBox.Value = "Id_cd:";
            // 
            // id_cdDataTextBox
            // 
            this.id_cdDataTextBox.CanGrow = true;
            this.id_cdDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.3919792175292969D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.id_cdDataTextBox.Name = "id_cdDataTextBox";
            this.id_cdDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.id_cdDataTextBox.StyleName = "Data";
            this.id_cdDataTextBox.Value = "=Fields.Id_cd";
            // 
            // id_PrdCaptionTextBox
            // 
            this.id_PrdCaptionTextBox.CanGrow = true;
            this.id_PrdCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.9479169845581055D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.id_PrdCaptionTextBox.Name = "id_PrdCaptionTextBox";
            this.id_PrdCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.id_PrdCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.id_PrdCaptionTextBox.StyleName = "Caption";
            this.id_PrdCaptionTextBox.Value = "Id_Prd:";
            // 
            // id_PrdDataTextBox
            // 
            this.id_PrdDataTextBox.CanGrow = true;
            this.id_PrdDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5038537979125977D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.id_PrdDataTextBox.Name = "id_PrdDataTextBox";
            this.id_PrdDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.id_PrdDataTextBox.StyleName = "Data";
            this.id_PrdDataTextBox.Value = "=Fields.Id_Prd";
            // 
            // inventarioCaptionTextBox
            // 
            this.inventarioCaptionTextBox.CanGrow = true;
            this.inventarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.059791564941406D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.inventarioCaptionTextBox.Name = "inventarioCaptionTextBox";
            this.inventarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.inventarioCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.inventarioCaptionTextBox.StyleName = "Caption";
            this.inventarioCaptionTextBox.Value = "Inventario:";
            // 
            // inventarioDataTextBox
            // 
            this.inventarioDataTextBox.CanGrow = true;
            this.inventarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.615729331970215D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.inventarioDataTextBox.Name = "inventarioDataTextBox";
            this.inventarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.inventarioDataTextBox.StyleName = "Data";
            this.inventarioDataTextBox.Value = "=Fields.Inventario";
            // 
            // mes1CaptionTextBox
            // 
            this.mes1CaptionTextBox.CanGrow = true;
            this.mes1CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.171667098999023D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes1CaptionTextBox.Name = "mes1CaptionTextBox";
            this.mes1CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes1CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes1CaptionTextBox.StyleName = "Caption";
            this.mes1CaptionTextBox.Value = "Mes1:";
            // 
            // mes1DataTextBox
            // 
            this.mes1DataTextBox.CanGrow = true;
            this.mes1DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.727603912353516D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes1DataTextBox.Name = "mes1DataTextBox";
            this.mes1DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes1DataTextBox.StyleName = "Data";
            this.mes1DataTextBox.Value = "=Fields.Mes1";
            // 
            // mes2CaptionTextBox
            // 
            this.mes2CaptionTextBox.CanGrow = true;
            this.mes2CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.283541679382324D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes2CaptionTextBox.Name = "mes2CaptionTextBox";
            this.mes2CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes2CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes2CaptionTextBox.StyleName = "Caption";
            this.mes2CaptionTextBox.Value = "Mes2:";
            // 
            // mes2DataTextBox
            // 
            this.mes2DataTextBox.CanGrow = true;
            this.mes2DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.839479446411133D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes2DataTextBox.Name = "mes2DataTextBox";
            this.mes2DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes2DataTextBox.StyleName = "Data";
            this.mes2DataTextBox.Value = "=Fields.Mes2";
            // 
            // mes3CaptionTextBox
            // 
            this.mes3CaptionTextBox.CanGrow = true;
            this.mes3CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.395416259765625D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes3CaptionTextBox.Name = "mes3CaptionTextBox";
            this.mes3CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes3CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes3CaptionTextBox.StyleName = "Caption";
            this.mes3CaptionTextBox.Value = "Mes3:";
            // 
            // mes3DataTextBox
            // 
            this.mes3DataTextBox.CanGrow = true;
            this.mes3DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.951354026794434D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes3DataTextBox.Name = "mes3DataTextBox";
            this.mes3DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes3DataTextBox.StyleName = "Data";
            this.mes3DataTextBox.Value = "=Fields.Mes3";
            // 
            // mes4CaptionTextBox
            // 
            this.mes4CaptionTextBox.CanGrow = true;
            this.mes4CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.507291793823242D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes4CaptionTextBox.Name = "mes4CaptionTextBox";
            this.mes4CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes4CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes4CaptionTextBox.StyleName = "Caption";
            this.mes4CaptionTextBox.Value = "Mes4:";
            // 
            // mes4DataTextBox
            // 
            this.mes4DataTextBox.CanGrow = true;
            this.mes4DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.063229560852051D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes4DataTextBox.Name = "mes4DataTextBox";
            this.mes4DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes4DataTextBox.StyleName = "Data";
            this.mes4DataTextBox.Value = "=Fields.Mes4";
            // 
            // mes5CaptionTextBox
            // 
            this.mes5CaptionTextBox.CanGrow = true;
            this.mes5CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.619166374206543D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes5CaptionTextBox.Name = "mes5CaptionTextBox";
            this.mes5CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes5CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes5CaptionTextBox.StyleName = "Caption";
            this.mes5CaptionTextBox.Value = "Mes5:";
            // 
            // mes5DataTextBox
            // 
            this.mes5DataTextBox.CanGrow = true;
            this.mes5DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.175104141235352D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes5DataTextBox.Name = "mes5DataTextBox";
            this.mes5DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes5DataTextBox.StyleName = "Data";
            this.mes5DataTextBox.Value = "=Fields.Mes5";
            // 
            // mes6CaptionTextBox
            // 
            this.mes6CaptionTextBox.CanGrow = true;
            this.mes6CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.731040954589844D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes6CaptionTextBox.Name = "mes6CaptionTextBox";
            this.mes6CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes6CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.mes6CaptionTextBox.StyleName = "Caption";
            this.mes6CaptionTextBox.Value = "Mes6:";
            // 
            // mes6DataTextBox
            // 
            this.mes6DataTextBox.CanGrow = true;
            this.mes6DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.286979675292969D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.mes6DataTextBox.Name = "mes6DataTextBox";
            this.mes6DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.mes6DataTextBox.StyleName = "Data";
            this.mes6DataTextBox.Value = "=Fields.Mes6";
            // 
            // montoInventarioCaptionTextBox
            // 
            this.montoInventarioCaptionTextBox.CanGrow = true;
            this.montoInventarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.842916488647461D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoInventarioCaptionTextBox.Name = "montoInventarioCaptionTextBox";
            this.montoInventarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoInventarioCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.montoInventarioCaptionTextBox.StyleName = "Caption";
            this.montoInventarioCaptionTextBox.Value = "Monto Inventario:";
            // 
            // montoInventarioDataTextBox
            // 
            this.montoInventarioDataTextBox.CanGrow = true;
            this.montoInventarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.398853302001953D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoInventarioDataTextBox.Name = "montoInventarioDataTextBox";
            this.montoInventarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoInventarioDataTextBox.StyleName = "Data";
            this.montoInventarioDataTextBox.Value = "=Fields.MontoInventario";
            // 
            // montoSobreInventarioCaptionTextBox
            // 
            this.montoSobreInventarioCaptionTextBox.CanGrow = true;
            this.montoSobreInventarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.954792022705078D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoSobreInventarioCaptionTextBox.Name = "montoSobreInventarioCaptionTextBox";
            this.montoSobreInventarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoSobreInventarioCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.montoSobreInventarioCaptionTextBox.StyleName = "Caption";
            this.montoSobreInventarioCaptionTextBox.Value = "Monto Sobre Inventario:";
            // 
            // montoSobreInventarioDataTextBox
            // 
            this.montoSobreInventarioDataTextBox.CanGrow = true;
            this.montoSobreInventarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.51072883605957D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoSobreInventarioDataTextBox.Name = "montoSobreInventarioDataTextBox";
            this.montoSobreInventarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoSobreInventarioDataTextBox.StyleName = "Data";
            this.montoSobreInventarioDataTextBox.Value = "=Fields.MontoSobreInventario";
            // 
            // montoVentaCaptionTextBox
            // 
            this.montoVentaCaptionTextBox.CanGrow = true;
            this.montoVentaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.066667556762695D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoVentaCaptionTextBox.Name = "montoVentaCaptionTextBox";
            this.montoVentaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoVentaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.montoVentaCaptionTextBox.StyleName = "Caption";
            this.montoVentaCaptionTextBox.Value = "Monto Venta:";
            // 
            // montoVentaDataTextBox
            // 
            this.montoVentaDataTextBox.CanGrow = true;
            this.montoVentaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.622604370117188D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.montoVentaDataTextBox.Name = "montoVentaDataTextBox";
            this.montoVentaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.montoVentaDataTextBox.StyleName = "Data";
            this.montoVentaDataTextBox.Value = "=Fields.MontoVenta";
            // 
            // precioAAACaptionTextBox
            // 
            this.precioAAACaptionTextBox.CanGrow = true;
            this.precioAAACaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.17854118347168D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.precioAAACaptionTextBox.Name = "precioAAACaptionTextBox";
            this.precioAAACaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.precioAAACaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.precioAAACaptionTextBox.StyleName = "Caption";
            this.precioAAACaptionTextBox.Value = "Precio AAA:";
            // 
            // precioAAADataTextBox
            // 
            this.precioAAADataTextBox.CanGrow = true;
            this.precioAAADataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.734479904174805D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.precioAAADataTextBox.Name = "precioAAADataTextBox";
            this.precioAAADataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.precioAAADataTextBox.StyleName = "Data";
            this.precioAAADataTextBox.Value = "=Fields.PrecioAAA";
            // 
            // promedioVtaCaptionTextBox
            // 
            this.promedioVtaCaptionTextBox.CanGrow = true;
            this.promedioVtaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(22.290416717529297D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.promedioVtaCaptionTextBox.Name = "promedioVtaCaptionTextBox";
            this.promedioVtaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.promedioVtaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.promedioVtaCaptionTextBox.StyleName = "Caption";
            this.promedioVtaCaptionTextBox.Value = "Promedio Vta:";
            // 
            // promedioVtaDataTextBox
            // 
            this.promedioVtaDataTextBox.CanGrow = true;
            this.promedioVtaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(22.846353530883789D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.promedioVtaDataTextBox.Name = "promedioVtaDataTextBox";
            this.promedioVtaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.promedioVtaDataTextBox.StyleName = "Data";
            this.promedioVtaDataTextBox.Value = "=Fields.PromedioVta";
            // 
            // sobreInventarioCaptionTextBox
            // 
            this.sobreInventarioCaptionTextBox.CanGrow = true;
            this.sobreInventarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.402292251586914D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.sobreInventarioCaptionTextBox.Name = "sobreInventarioCaptionTextBox";
            this.sobreInventarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.sobreInventarioCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.sobreInventarioCaptionTextBox.StyleName = "Caption";
            this.sobreInventarioCaptionTextBox.Value = "Sobre Inventario:";
            // 
            // sobreInventarioDataTextBox
            // 
            this.sobreInventarioDataTextBox.CanGrow = true;
            this.sobreInventarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.958229064941406D), Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D));
            this.sobreInventarioDataTextBox.Name = "sobreInventarioDataTextBox";
            this.sobreInventarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.50302082300186157D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.sobreInventarioDataTextBox.StyleName = "Data";
            this.sobreInventarioDataTextBox.Value = "=Fields.SobreInventario";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.detail.Name = "detail";
            // 
            // ExpRep_AnalisisInventario
            // 
            this.DataSource = this.sqlDataSource1;
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "ExpRep_AnalisisInventario";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "IdEmp";
            reportParameter1.Text = "IdEmp";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Name = "IdCd";
            reportParameter2.Text = "IdCd";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "FechaIni";
            reportParameter3.Text = "FechaIni";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter4.Name = "FechaFin";
            reportParameter4.Text = "FechaFin";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter5.Name = "IdU";
            reportParameter5.Text = "IdU";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(24.5141658782959D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox cd_NombreCaptionTextBox;
        private Telerik.Reporting.TextBox cd_NombreDataTextBox;
        private Telerik.Reporting.TextBox coberturaCaptionTextBox;
        private Telerik.Reporting.TextBox coberturaDataTextBox;
        private Telerik.Reporting.TextBox cuentaCaptionTextBox;
        private Telerik.Reporting.TextBox cuentaDataTextBox;
        private Telerik.Reporting.TextBox descripcionCaptionTextBox;
        private Telerik.Reporting.TextBox descripcionDataTextBox;
        private Telerik.Reporting.TextBox diaafectaCaptionTextBox;
        private Telerik.Reporting.TextBox diaafectaDataTextBox;
        private Telerik.Reporting.TextBox diaInventarioCaptionTextBox;
        private Telerik.Reporting.TextBox diaInventarioDataTextBox;
        private Telerik.Reporting.TextBox estatusCaptionTextBox;
        private Telerik.Reporting.TextBox estatusDataTextBox;
        private Telerik.Reporting.TextBox id_cdCaptionTextBox;
        private Telerik.Reporting.TextBox id_cdDataTextBox;
        private Telerik.Reporting.TextBox id_PrdCaptionTextBox;
        private Telerik.Reporting.TextBox id_PrdDataTextBox;
        private Telerik.Reporting.TextBox inventarioCaptionTextBox;
        private Telerik.Reporting.TextBox inventarioDataTextBox;
        private Telerik.Reporting.TextBox mes1CaptionTextBox;
        private Telerik.Reporting.TextBox mes1DataTextBox;
        private Telerik.Reporting.TextBox mes2CaptionTextBox;
        private Telerik.Reporting.TextBox mes2DataTextBox;
        private Telerik.Reporting.TextBox mes3CaptionTextBox;
        private Telerik.Reporting.TextBox mes3DataTextBox;
        private Telerik.Reporting.TextBox mes4CaptionTextBox;
        private Telerik.Reporting.TextBox mes4DataTextBox;
        private Telerik.Reporting.TextBox mes5CaptionTextBox;
        private Telerik.Reporting.TextBox mes5DataTextBox;
        private Telerik.Reporting.TextBox mes6CaptionTextBox;
        private Telerik.Reporting.TextBox mes6DataTextBox;
        private Telerik.Reporting.TextBox montoInventarioCaptionTextBox;
        private Telerik.Reporting.TextBox montoInventarioDataTextBox;
        private Telerik.Reporting.TextBox montoSobreInventarioCaptionTextBox;
        private Telerik.Reporting.TextBox montoSobreInventarioDataTextBox;
        private Telerik.Reporting.TextBox montoVentaCaptionTextBox;
        private Telerik.Reporting.TextBox montoVentaDataTextBox;
        private Telerik.Reporting.TextBox precioAAACaptionTextBox;
        private Telerik.Reporting.TextBox precioAAADataTextBox;
        private Telerik.Reporting.TextBox promedioVtaCaptionTextBox;
        private Telerik.Reporting.TextBox promedioVtaDataTextBox;
        private Telerik.Reporting.TextBox sobreInventarioCaptionTextBox;
        private Telerik.Reporting.TextBox sobreInventarioDataTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;

    }
}