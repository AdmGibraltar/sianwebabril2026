<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ProEmbarquesRutas.aspx.cs" Inherits="SIANWEB.ProEmbarquesRutas" %>


 <%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
  
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
   

    <%-- Alertify --%>
    <!--script src="<%=Page.ResolveUrl("~/js/alertify.js-master/src/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>" rel="stylesheet"-->
    
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>        
    
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">    

    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script> 

   
    <%-- FONT AWESOME --%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">    

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/key_acys.css")%>" rel="stylesheet">

    <%--TIME PICKER--%>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>

    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>



  <script type="text/javascript">
      function grid_SelectionChanged(s, e) {
          s.GetSelectedFieldValues("Id_Fac", GetSelectedFieldValuesCallback);
      }
      function GetSelectedFieldValuesCallback(values) {
          selList.BeginUpdate();
          try {
              selList.ClearItems();
              for (var i = 0; i < values.length; i++) {
                  selList.AddItem(values[i].toString());
              }
          } finally {
              selList.EndUpdate();
          }
          document.getElementById("selCount").innerHTML = ASPxGridView2.GetSelectedRowCount();
      }
      function grid_SelectionChanged3(s, e) {
          s.GetSelectedFieldValues("Id_Rem", GetSelectedFieldValuesCallback3);
      }
      function GetSelectedFieldValuesCallback3(values) {
          selListRem.BeginUpdate();
          try {
              selListRem.ClearItems();
              for (var i = 0; i < values.length; i++) {
                  selListRem.AddItem(values[i].toString());
              }
          } finally {
              selListRem.EndUpdate();
          }
          document.getElementById("selCountRem").innerHTML = grRemisiones.GetSelectedRowCount();
      }
      function onFilesUploadStart(s, e) {
          dxbsDemo.uploadedFilesContainer.hide();
      }
      function onFileUploadComplete(s, e) {
          if (e.callbackData) {
              var fileData = e.callbackData.split('|');
              var fileName = fileData[0],
                  fileUrl = fileData[1],
                  fileSize = fileData[2];
              dxbsDemo.uploadedFilesContainer.addFile(s, fileName, fileUrl, fileSize);
          }
      }
      function OnInit(s, e) {
          s.GetTextContainer().className += " glyphicon glyphicon-search";
      }
      function shoeed(s, e) {
          document.getElementById('infooo').style.display = "block";
      }
      function OnGridCallBackBegin(s, e) {
          currentSelectedRowsCount = ASPxGridView1.cp_SelectedRowsCount;
      }
      function checkUncheckSelectableRowsOnPage(isChecked) {
          var selectableRowIndexes = ASPxGridView2.cp_SelectableRows;
          var grdStartIndex = ASPxGridView2.visibleStartIndex;
          var grdEndIndex = grdStartIndex + ASPxGridView2.pageRowCount;
          if (selectableRowIndexes != null && selectableRowIndexes != '') {
              var rowIdxes = selectableRowIndexes.split("#");
              var selectedRowsCount = 0;
              if (rowIdxes != null) {
                  try {
                      for (var i = 0; i < rowIdxes.length; i++) {
                          if (rowIdxes[i] != "") {
                              var rowIndex = parseInt(rowIdxes[i]);
                              if (rowIndex != NaN && rowIndex >= 0 && rowIndex >= grdStartIndex && rowIndex < grdEndIndex) {
                                  if (ASPxClientControl.GetControlCollection().GetByName("cbCheck" + rowIdxes[i]) != null) {
                                      if (isChecked) {
                                          ASPxGridView2.SelectRowOnPage(rowIdxes[i]);
                                          selectedRowsCount++;
                                      }
                                      else
                                          ASPxGridView2.UnselectRowOnPage(rowIdxes[i]);
                                      ASPxClientControl.GetControlCollection().GetByName("cbCheck" + rowIdxes[i]).SetChecked(isChecked);
                                  }
                              }
                          }
                      }
                      //updateSelectedKeys();   // Can be used if the selected keys needs to be saved separately in a Hidden field
                      ASPxGridView2.cp_SelectedRowsCount = selectedRowsCount;
                      currentSelectedRowsCount = selectedRowsCount;
                  }
                  finally {
                  }
              }
          }
      }

  </script>

         
        
    </telerik:RadCodeBlock>
 
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RAM1" runat="server">
 
            <AjaxSettings>

          
            <telerik:AjaxSetting AjaxControlID="btnF">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnR">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBuscar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BootstrapBtnGrabar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="cmbEstatus">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                  <telerik:AjaxSetting AjaxControlID="BootstrapDateEdit1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                  <telerik:AjaxSetting AjaxControlID="fechafin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>


            
                <telerik:AjaxSetting AjaxControlID="ASPxGridView2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>     
                <telerik:AjaxSetting AjaxControlID="grRemisiones">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>    
                 <telerik:AjaxSetting AjaxControlID="BootstrapBtnGenerar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>    
                
                      
                      
            </AjaxSettings>
        </telerik:RadAjaxManager>
 
  <div runat="server" id="divPrincipal" class="row mt5 col-md-12">
     <br />
     <div style="float: left; width: 5%"  class="row mt5">
     <br />
     </div>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="50%">
            <tr>
                <td>
                       <asp:HiddenField ID="HiddenField1" runat="server" />
                       <asp:HiddenField ID="HF_Cve" runat="server" />

                </td>
   
            </tr> 
        </table>




     <div class="container-fluid">

      <div class="row">
          <div class="col-md-12">
              
              <div class="col-md-2">
               <div class="col-md-2"> <i class="fa fa-filter"></i> </div>
                <div class="col-md-10">

                  <dx:ASPxTextBox ID="txtFacturaIni" runat="server" Theme="Material" Width="80px" Caption="Factura Inicial"
                      Height="30px">
                  </dx:ASPxTextBox>
                  </div>
              </div>
              <div class="col-md-2">
                  <dx:ASPxTextBox ID="txtFacturaFin" runat="server" Theme="Material" Width="80px" Caption="Factura Final"
                      Height="30px">
                  </dx:ASPxTextBox>
              </div>
              <div class="col-md-2">
                <div class="col-md-3">Fecha Inicial</div>
                <div class="col-md-9">
                  <dx:BootstrapDateEdit ID="BootstrapDateEdit1" runat="server" Date="2020-02-07" >
                  </dx:BootstrapDateEdit>
                  </div>
              </div>
              <div class="col-md-2">
               <div class="col-md-2">Fecha Final</div>
                <div class="col-md-10">
                  <dx:BootstrapDateEdit ID="fechafin" runat="server"  Date="02/07/2021 13:54:51"  >
                  </dx:BootstrapDateEdit>
                  </div>
              </div>
              <div class="col-md-2">
                  <dx:BootstrapComboBox ID="cmbEstatus" runat="server" SelectedIndex="0" DropDownRows="8">
                      <Items>
                          <dx:BootstrapListEditItem Text="-- Todos --" Value="0" />
                          <dx:BootstrapListEditItem Text="Baja" Value="B" />
                          <dx:BootstrapListEditItem Text="Capturado" Value="C" />
                          <dx:BootstrapListEditItem Text="Embarque" Value="E" />
                          <dx:BootstrapListEditItem Text="Entregado" Value="N" />
                          <dx:BootstrapListEditItem Text="Impreso" Value="I" />
                          <dx:BootstrapListEditItem Text="Re-Factura" Value="RF" />
                          <dx:BootstrapListEditItem Text="Solicitado" Value="S" />
                      </Items>
                      <ButtonTemplate>
                          <span class="dxbs-edit-btn btn btn-info dropdown-toggle" data-toggle="dropdown-show">
                              Estatus</span>
                      </ButtonTemplate>
                  </dx:BootstrapComboBox>
              </div>
              <div class="col-md-1">
                 <%-- <dx:BootstrapButton ID="btnBuscar" runat="server" AutoPostBack="false" Text="Buscar"
                      OnClick="btnBuscar_Click">
                      <CssClasses Icon="glyphicon glyphicon-search" />
                      <SettingsBootstrap RenderOption="Info" />
                  </dx:BootstrapButton>--%>
                
            <asp:LinkButton ID="btnBuscar" 
                        runat="server"  Text="Buscar" 
                        CssClass="btn btn-info"    
                        OnClick="btnBuscar_Click">
                <span class="glyphicon glyphicon-search"></span> Buscar
            </asp:LinkButton>

 

                  
              </div>
               <div class="col-md-1">
                                <%-- <dx:BootstrapButton ID="BootstrapBtnGrabar" runat="server" AutoPostBack="false" Text="Grabar"
                                     OnClick="BootstrapBtnGrabar_Click">
                                     <CssClasses Icon="glyphicon glyphicon-save" />
                                     <SettingsBootstrap RenderOption="Info" />
                                 </dx:BootstrapButton>--%>
                   <asp:LinkButton ID="btnGrabar" runat="server"  CssClass="btn btn-info"
                       OnClick="BootstrapBtnGrabar_Click">
                        <span class="glyphicon glyphicon-save"></span> Grabar
                   </asp:LinkButton>

                </div>

          </div>
      </div>
      <div class="row">
      <br />
      </div>

      </div>



      <div style="float: left; width: 76%">        
       
   
     
         <div class="container-fluid">
             
             <%-- <div class="alert alert-danger" role="alert" id="Alerta">
              <h4 class="alert-heading">Ha ocurrido un error!</h4>
              <p>Aww yeah, you successfully read this important alert message. This example text is going to run a bit longer so that you can see how spacing within an alert works with this kind of content.</p>
              <hr>
              <p class="mb-0">Whenever you need to, be sure to use margin utilities to keep things nice and tidy.</p>
              <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
            </div>--%>
             <%-- grid de facturas --%>
             <div class="row mt5" style="margin-top: 1%;margin-left: 1%;">
 
                    <dx:BootstrapButton ID="btnF" runat="server" AutoPostBack="false" Text="Sel.">
                    <ClientSideEvents Click="function(s, e) { ASPxGridView2.SelectAllRowsOnPage(); }" />
                      <CssClasses Icon="glyphicon glyphicon-check" />
                      <SettingsBootstrap RenderOption="Info" />
                  </dx:BootstrapButton>

                 <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="ASPxGridView2" SettingsText-Title="Seleccione las facturas a embarcar"
                     Styles-TitlePanel-HorizontalAlign="Left" runat="server" AutoGenerateColumns="false"
                     KeyFieldName="Id_Emp;Id_Cd;Id_Fac" Theme="Material" SettingsPager-EnableAdaptivity="true"
                     Width="100%" AllowOnlyOneMasterRowExpanded="true"
                     EnableCallbackCompression="true" EnableCallbackAnimation="true" EnablePagingCallbackAnimation="true">
                     <SettingsDataSecurity AllowInsert="False" />
                     <SettingsText Title="Seleccione las facturas a embarcar"></SettingsText>
                     <EditFormLayoutProperties>
                         <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit"></SettingsAdaptivity>
                     </EditFormLayoutProperties>
                     <Columns>
                         <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="40">
                         </dx:GridViewCommandColumn>
                         <dx:GridViewDataTextColumn Caption="Emp" FieldName="Id_Emp" Visible="false">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Factura" FieldName="Id_Fac" VisibleIndex="1"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Serie" FieldName="Serie" Visible="false">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Id_FacSerie" FieldName="Id_FacSerie" Visible="false">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Pedido" FieldName="Id_Ped" VisibleIndex="2" Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Fac_Tipo" Visible="false">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Fac_TipoStr" VisibleIndex="3"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Estatus" FieldName="Fac_EstatusStr" VisibleIndex="4"
                             Width="80">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataDateColumn Caption="Fecha" FieldName="Fac_Fecha" VisibleIndex="5"
                             Width="100">
                             <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         <dx:GridViewDataTextColumn Caption="Nombre Usuario" FieldName="U_Nombre" VisibleIndex="6"
                             Width="60">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Id_Cte" VisibleIndex="7"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Nombre" FieldName="Cte_NomComercial" VisibleIndex="8"
                             Width="280">
                         </dx:GridViewDataTextColumn>
                     </Columns>
                     <%-- este es de la prueba de selction--%>
                     <ClientSideEvents SelectionChanged="grid_SelectionChanged" />
                     <SettingsPager EnableAdaptivity="True" PageSize="10">
                     </SettingsPager>
                     <Settings ShowTitlePanel="true" ShowFilterRow="true" />
                     <Styles>
                         <TitlePanel HorizontalAlign="Left">
                         </TitlePanel>
                     </Styles>
                 </dx:ASPxGridView>
             </div>
            <%--MODULO AQUI IBA EL GRID --%>
             <%-- fin grid de facturas --%>
             <%-- grid de Remisiones --%>
             <div class="row mt5" style="margin-top: 1%;margin-left: 1%;">
             <dx:BootstrapButton ID="btnR" runat="server" AutoPostBack="false" Text="Sel.">
                    <ClientSideEvents Click="function(s, e) { grRemisiones.SelectAllRowsOnPage(); }" />
                      <CssClasses Icon="glyphicon glyphicon-check" />
                      <SettingsBootstrap RenderOption="Success" />
                  </dx:BootstrapButton>

                 <dx:ASPxGridView ID="grRemisiones" ClientInstanceName="grRemisiones" SettingsText-Title="Seleccione las remisiones a embarcar"
                     Styles-TitlePanel-HorizontalAlign="Left" runat="server" AutoGenerateColumns="True"
                     KeyFieldName="Id_Emp;Id_Cd;Id_Rem" Theme="Material" SettingsPager-EnableAdaptivity="true"
                     Width="100%" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit"
                     EnableCallbackCompression="true" EnableCallbackAnimation="true" EnablePagingCallbackAnimation="true">
                     <SettingsDataSecurity AllowInsert="False" />
                     <SettingsText Title="Seleccione las remisiones a embarcar"></SettingsText>
                     <EditFormLayoutProperties>
                         <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit"></SettingsAdaptivity>
                     </EditFormLayoutProperties>
                     <Columns>
                         <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="70">
                         </dx:GridViewCommandColumn>
                         <dx:GridViewDataTextColumn Caption="Emp" FieldName="Id_Emp" Visible="false">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Remisión" FieldName="Id_Rem" VisibleIndex="1"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Pedido" FieldName="Id_Ped" VisibleIndex="2" Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Rem_Tipo" Visible="false" Width="50">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Tipo" FieldName="Tm_Nombre" VisibleIndex="3"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Estatus" FieldName="Rem_Estatus" VisibleIndex="4"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataDateColumn Caption="Fecha" FieldName="Rem_Fecha" VisibleIndex="5"
                             Width="100">
                             <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         <dx:GridViewDataTextColumn Caption="Nombre Usuario" FieldName="UsuNom" VisibleIndex="6"
                             Width="100">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Id_Cte" VisibleIndex="7"
                             Width="70">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Nombre" FieldName="Cte_NomComercial" VisibleIndex="8"
                             Width="280">
                         </dx:GridViewDataTextColumn>
                     </Columns>
                     <ClientSideEvents SelectionChanged="grid_SelectionChanged3" />
                     <SettingsPager EnableAdaptivity="True" PageSize="7">
                     </SettingsPager>
                     <Settings ShowTitlePanel="true" ShowFilterRow="true" />
                     <Styles>
                         <TitlePanel HorizontalAlign="Left">
                         </TitlePanel>
                     </Styles>
                 </dx:ASPxGridView>
             </div>
             <div class="row mt5">
                 <dx:ASPxGridView ID="grModuloRem" SettingsText-Title="Facturas dis" Styles-TitlePanel-HorizontalAlign="Left"
                     runat="server" AutoGenerateColumns="True" KeyFieldName="Id_Emp" Theme="Material"
                     SettingsPager-EnableAdaptivity="true" ClientInstanceName="grModuloRem" Width="100%"
                     EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit"
                     EnableCallbackCompression="true" EnableCallbackAnimation="true" EnablePagingCallbackAnimation="true"
                     Visible="false">
                     <Columns>
                         <dx:GridViewDataTextColumn Caption="#" FieldName="Id_Emp" VisibleIndex="1">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Empresa" FieldName="Emp_Nombre" VisibleIndex="1">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Fecha Alta" FieldName="Fecha_Alta" VisibleIndex="1">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Usuario" FieldName="Usu_Nombre" VisibleIndex="1">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn VisibleIndex="4" Caption=" ">
                             <DataItemTemplate>
                                 <a href="javascript:void(0);" onclick="openEdit(this, '<%# Container.KeyValue %>')">
                                     <i class="fa fa-edit"></i>Editar</a>
                             </DataItemTemplate>
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn VisibleIndex="5" Caption=" ">
                             <DataItemTemplate>
                                 <a href="javascript:void(0);" onclick="openBaja(this, '<%# Container.KeyValue %>')">
                                     <i class="fa fa-trash-alt"></i>Baja</a>
                             </DataItemTemplate>
                         </dx:GridViewDataTextColumn>
                     </Columns>
                     <Settings ShowTitlePanel="true" ShowFilterRow="true" />
                 </dx:ASPxGridView>
             </div>
             <%-- fin grid de remisiones --%>
         </div>

 

    </div>

    <%--Facturas y rem seleccionadas --%>
     <div style="float: right; width: 15%"  class="mx-auto" >
     
            <div class="row">
                <div class="col-md-12 col-xl-3">
      <a href="javascript:popupFacts.Show()" id="popupLink"> 
            <div class="card bg-c-blue order-card">
                <div class="card-block">
                 <h6>Facturas</h6>
                   
                    <h2 class="text-right"><i class="fa fa-truck f-left"></i><span id="selCount">0</span></h2>
                   
                    <p class="m-b-0">Facturas agregadas</p>
                   
                </div>
            </div>
      </a>
        </div>


         
     <dx:ASPxPopupControl ID="popuFactura" runat="server" ClientInstanceName="popupFacts"
     CloseAction="CloseButton" CloseOnEscape="true"
                    Modal="true" CloseAnimationType="Fade" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AutoUpdatePosition="true" EnableCallbackAnimation="true"  
                    PopupAnimationType="Fade"   EnableCallbackCompression="true" EnableViewState="false"
                    Maximized="false" SettingsLoadingPanel-Enabled="true" ShowPageScrollbarWhenModal="true" 
                    ShowHeader="true" HeaderText="Facturas Seleccionadas" 
                    HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" 
                    HeaderStyle-CssClass="well" Theme="iOS" >
<HeaderStyle VerticalAlign="Middle" CssClass="well" Font-Bold="True"></HeaderStyle>
                     <ContentCollection>
                         <dx:PopupControlContentControl ID="PopupControlContentControl12" runat="server">
                             <dx:ASPxCallbackPanel ID="ASPxCallbackPanel11" RenderMode="Div" ClientInstanceName="callbackPanel"
                                 runat="server" ClientSideEvents-EndCallback="shoeed">
<ClientSideEvents EndCallback="shoeed"></ClientSideEvents>
                                 <PanelCollection>
                                     <dx:PanelContent ID="PanelContent12" runat="server">
                                         <div class="container-fluid">
                                             <div class="row form-group well text-center">
                                                 <dx:ASPxListBox ID="ASPxListBox2" ClientInstanceName="selList" runat="server" Height="200px"
                                                     Width="90%" Theme="iOS" />
                                             </div>
                                         </div>
                                     </dx:PanelContent>
                                 </PanelCollection>
                             </dx:ASPxCallbackPanel>
                         </dx:PopupControlContentControl>
                     </ContentCollection>
     </dx:ASPxPopupControl>
     </div>

         <div class="row">
             <div class="col-md-12 col-xl-3">
                 <%--<a href="#info2" class="inf"> --%>
                 <a href="javascript:popupRems.Show()" id="popupLink">
                     <div class="card  bg-c-green order-card">
                         <div class="card-block">
                             <h6>
                                 Remisiones</h6>
                             <h2 class="text-right">
                                 <i class="fa fa-cart-plus f-left"></i><span id="selCountRem">0</span></h2>
                             <p class="m-b-0">
                                 Remisiones agregadas</p>
                         </div>
                     </div>
                 </a>
             </div>

       
    </div>

    <%--Botón exportar en pantalla--%>

    <div class="row">
        <div class="col-md-12 col-xl-3">
            <div class="col-md-3 col-xl-3">
            </div>
            <div class="col-md-3 col-xl-3">
                <dx:BootstrapButton ID="BootstrapBtnGenerar" runat="server" AutoPostBack="false"
                    Text="Exportar" OnClick="BootstrapBtnGenerar_Click">
                    <CssClasses Icon="glyphicon glyphicon-cloud-upload" />
                    <SettingsBootstrap RenderOption="Info" />
                </dx:BootstrapButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-xl-3">
                <dx:ASPxTextBox ID="txtChofer" runat="server" Theme="Material" Width="80px" Caption="Chofer"
                    Height="30px" Visible="true">
                </dx:ASPxTextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-xl-3">
                <dx:ASPxTextBox ID="txtCamioneta" runat="server" Theme="Material" Width="80px" Caption="Camioneta"
                    Height="30px" Visible="true">
                </dx:ASPxTextBox>
            </div>
        </div>
    </div>

   </div>
    <%--Fin Facturas y renglones seleccionadas--%>

            <dx:ASPxPopupControl ID="popupRemisiones" runat="server" ClientInstanceName="popupRems"
     CloseAction="CloseButton" CloseOnEscape="true"
                    Modal="true" CloseAnimationType="Fade" 
          PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AutoUpdatePosition="true" EnableCallbackAnimation="true"  
                    PopupAnimationType="Fade"   EnableCallbackCompression="true" EnableViewState="false"
                    Maximized="false" SettingsLoadingPanel-Enabled="true" ShowPageScrollbarWhenModal="true" 
                    ShowHeader="true" HeaderText="Remisiones Seleccionadas" 
          HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" 
                    HeaderStyle-CssClass="well" Theme="Material" >
<HeaderStyle VerticalAlign="Middle" CssClass="well" Font-Bold="True"></HeaderStyle>
                     <ContentCollection>
                         <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                             <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" RenderMode="Div" ClientInstanceName="callbackPanel"
                                 runat="server" ClientSideEvents-EndCallback="shoeed">
<ClientSideEvents EndCallback="shoeed"></ClientSideEvents>
                                 <PanelCollection>
                                     <dx:PanelContent ID="PanelContent2" runat="server">
                                         <div class="container-fluid">
                                             <div class="row form-group well text-center">
                                                 <dx:ASPxListBox ID="ASPxListBox3" ClientInstanceName="selListRem" 
                                                     runat="server" Height="200px"
                                                    Width="95%" Theme="Material" />
                                             </div>
                                         </div>
                                     </dx:PanelContent>
                                 </PanelCollection>
                             </dx:ASPxCallbackPanel>
                         </dx:PopupControlContentControl>
                     </ContentCollection>
     </dx:ASPxPopupControl>
            <dx:ASPxPopupControl ID="pcrevisar" Theme="IOS" runat="server" CloseAction="CloseButton" CloseOnEscape="true"
                    Modal="true" CloseAnimationType="Fade" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AutoUpdatePosition="false" EnableCallbackAnimation="true" ClientInstanceName="popup"
                    PopupAnimationType="Fade" Width="700px" EnableCallbackCompression="true" EnableViewState="false"
                    Maximized="false" SettingsLoadingPanel-Enabled="true" ShowPageScrollbarWhenModal="true" 
                    ShowHeader="true" HeaderText="Aviso" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" 
                    HeaderStyle-CssClass="well" AllowDragging="true">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dx:ASPxCallbackPanel ID="callbackPanel" RenderMode="Div" ClientInstanceName="callbackPanel" runat="server"  ClientSideEvents-EndCallback="shoeed">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                        <div class="container-fluid">
                                               
                                                                    <asp:HiddenField runat="server" ID="hdnIDCC" Value="0w" />

                                                                    <div class="container-fluid">
                                                                        
                                                                         <div class="row form-group well text-center">

                                                                                <div class="col-lg-11 col-sm-11 col-md-11 col-xs-11">
                                                                                      <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
                                                                                </div>

                                                                                <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6">
                                                                                    <dx:ASPxButton runat="server" ID="btnBAJA" Text="Baja" OnClick="btnBAJA_Click" Visible="false" Theme="Material"></dx:ASPxButton>
                                                                                    <dx:ASPxButton runat="server" ID="btnUPDATE" Text="Actualizar" OnClick="btnUPDATE_Click" Visible="false" Theme="Material"></dx:ASPxButton>
                                                                                    <dx:ASPxButton runat="server" ID="btnSave" Text="Registrar" OnClick="btnSave_Click" Visible="false" Theme="Material"></dx:ASPxButton>
                                                                                </div>
                                                                                <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6 text-center">
                                                                                    <dx:ASPxButton runat="server" ID="btnCancelar" Text="Cerrar"  Theme="Material" OnClick="btnCancelar_Click"></dx:ASPxButton>
                                                                                </div>
                                                                                <div>
                                                                                 <dx:BootstrapButton ID="btnExportardespuesdegrabar" runat="server" AutoPostBack="false"
                                                                                                Text="Exportar" OnClick="BootstrapBtnGenerar_Click" Visible= "false">
                                                                                                <CssClasses Icon="glyphicon glyphicon-cloud-upload" />
                                                                                                <SettingsBootstrap RenderOption="Info" />
                                                                                 </dx:BootstrapButton>


                                                                                </div>
                                                                        </div>
                                                                    </div>

                                                             
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
    </dx:ASPxPopupControl>
            <dx:ASPxPopupControl ID="pcExportar" Theme="IOS" runat="server" CloseAction="CloseButton"
          CloseOnEscape="true" Modal="true" CloseAnimationType="Fade" PopupHorizontalAlign="WindowCenter"
          PopupVerticalAlign="WindowCenter" AutoUpdatePosition="false" EnableCallbackAnimation="true"
          ClientInstanceName="popupexporta" PopupAnimationType="Fade" Width="700px" EnableCallbackCompression="true"
          EnableViewState="false" Maximized="false" SettingsLoadingPanel-Enabled="true"
          ShowPageScrollbarWhenModal="true" ShowHeader="true" HeaderText="Aviso" HeaderStyle-Font-Bold="true"
          HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="well">
          <ContentCollection>
              <dx:PopupControlContentControl ID="Popupexportar" runat="server">
                  <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" RenderMode="Div" ClientInstanceName="callbackPanel"
                      runat="server" ClientSideEvents-EndCallback="shoeed">
                      <PanelCollection>
                          <dx:PanelContent ID="PanelContent3" runat="server">
                              <div class="container-fluid">
                                  <dx:ASPxPageControl runat="server" ID="ASPxPageControl1" Theme="Material" Width="100%"
                                      ActivateTabPageAction="Click">
                                      <TabPages>
                                          <dx:TabPage Text="Exportar archivo Planner">
                                              <ContentCollection>
                                                  <dx:ContentControl>
                                                      <asp:HiddenField runat="server" ID="HiddenField2" Value="0w" />
                                                      <div class="container-fluid">
                                                          <div class="row form-group">
                                                              <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6">
                                                                  <label>
                                                                      Número de Embarque</label>
                                                                  <dx:ASPxTextBox runat="server" ID="txtIdEmb" Theme="Material" Width="30%"  Height="30px" NullText="No. Embarque">
                                                                      <ValidationSettings ErrorTextPosition="Bottom">
                                                                          <RequiredField IsRequired="true" ErrorText="Teclee número de embarque" />
                                                                      </ValidationSettings>
                                                                      <%--EJEMPLO DE VALIDACION--%>
                                                                  </dx:ASPxTextBox>
                                                              </div>
                                                            

                                                              <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6">
                                                                  <label>
                                                                      Día del Embarque</label>
                                                                  <%--<dx:ASPxTextBox runat="server" ID="txtDia"   Width="30%" NullText="Día"></dx:ASPxTextBox>--%>
                                                                  <dx:BootstrapDateEdit ID="FechaDia" runat="server" Date="2019-12-10">
                                                                  </dx:BootstrapDateEdit>
                                                              </div>
                                                               

                                                          </div>
                                                          <div class="row form-group well text-center">
                                                              <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6">
                                                                  <dx:ASPxButton runat="server" ID="btnGenerarPlan" Text="Exportar" OnClick="btnGenerarPlan_Click"
                                                                      Visible="true" Theme="Material">
                                                                  </dx:ASPxButton>
                                                              </div>
                                                              <asp:Label runat="server" ID="Label3" Visible="false" CssClass="label label-danger"></asp:Label>
                                                              <div class="col-lg-6 col-sm-6 col-md-6 col-xs-6 text-center">
                                                                  <dx:ASPxButton runat="server" ID="btnCancelarExp" Text="Salir" OnClick="btnCancelarExp_Click"
                                                                      Theme="Material">
                                                                  </dx:ASPxButton>
                                                              </div>
                                                          </div>

                                                           <div class="row">
                                                           </div>
                                                            <div class="row">
                                                           </div>
                                                            <div class="row">
                                                           </div>
                                                            <div class="row">
                                                           </div>

                                                      </div>
                                                  </dx:ContentControl>
                                              </ContentCollection>
                                          </dx:TabPage>
                                      </TabPages>
                                  </dx:ASPxPageControl>
                              </div>
                          </dx:PanelContent>
                      </PanelCollection>
                  </dx:ASPxCallbackPanel>
              </dx:PopupControlContentControl>
          </ContentCollection>
      </dx:ASPxPopupControl>
  

   </div> <%--este es el panel del lado derecho --%>



     <script type="text/javascript">
         var keyValue;
         function openEdit(element, key) {
             keyValue = key;
             callbackPanel.PerformCallback(keyValue + '|edit');
             popup.ShowAtElement(element);
         }
         function openBaja(element, key) {
             keyValue = key;
             callbackPanel.PerformCallback(keyValue + '|baja');
             popup.ShowAtElement(element);
         }
         var keyValue2;

         function openCity(evt, cityName) {
             var i, tabcontent, tablinks;
             tabcontent = document.getElementsByClassName("tabcontent");
             for (i = 0; i < tabcontent.length; i++) {
                 tabcontent[i].style.display = "none";
             }
             tablinks = document.getElementsByClassName("tablinks");
             for (i = 0; i < tablinks.length; i++) {
                 tablinks[i].className = tablinks[i].className.replace(" active", "");
             }
             document.getElementById(cityName).style.display = "block";
             evt.currentTarget.className += " active";
         }
         (function ($) {
             $(document).ready(function () {
                 // document.getElementById('infooo').style.display = "block";
             });
         })(jQuery);
     </script>
         <style>
         
                 body{
            margin-top:20px;
            background:#FAFAFA;
        }
         .special-card {
            background-color: rgba(245, 245, 245, 0.4) !important;
           
                }
                
        .order-card {
            color: #fff;  background-color: rgba(245, 245, 245, 0.4) !important;
        }
        .bg-c-blue {
            background: linear-gradient(45deg,#4099ff,#73b4ff);
        }
        .bg-c-green {
            background: linear-gradient(45deg,#2ed8b6,#59e0c5);
        }
        .bg-c-yellow {
            background: linear-gradient(45deg,#FFB64D,#ffcb80);
        }
        .bg-c-pink {
            background: linear-gradient(45deg,#FF5370,#ff869a);
        }
        .card {
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2.94px 0.06px rgba(4,26,55,0.16);
            box-shadow: 0 1px 2.94px 0.06px rgba(4,26,55,0.16);
            border: none;
            margin-bottom: 30px;
            -webkit-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
        }
        .card .card-block {
            padding: 25px;
        }
        .order-card i {
            font-size: 26px;
        }
        .f-left {
            float: left;
        }
        .f-right {
            float: right;
        }
        .alert {
            padding: 20px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }
         
        
         </style>
   
</asp:Content>  