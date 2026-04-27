<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="RepComisionesPresupuestoConsultaRik.aspx.cs" Inherits="SIANWEB.RepComisionesPresupuestoConsultaRik" %>

 <%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
 
 <script href="~/js/jquery-template/jquery.loadTemplate.js"></script>        
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
     <link rel="stylesheet" href="Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css"> 
      <script href="js/bootstrap-select.min.js"></script> 

  


    <%--Para controlar cuando le dan click en la gráfica y actualiza el rik--%>

     <script type="text/javascript">
         var gridViewCommand;
         function onGridViewBeginCallback(s, e) {
             gridViewCommand = e.command;
         }
         function onGridViewEndCallback(s, e) {
             if ((gridViewCommand == "APPLYCOLUMNFILTER") || (gridViewCommand == "SORT"))
                 ASPxCallbackPanel.PerformCallback(gridViewCommand);
         }
     </script>

    
   <%-- Panel movible --%>
   
   <script type="text/javascript">
       function OnSplitterPaneResized(s, e) {
           var name = e.pane.name;
           if (name == 'listBoxContainer')
               ResizeControl(sampleListBox, e.pane);
           else if (name == 'gridContainer  ')
               ResizeControl(sampleGrid, e.pane);
       }
       function ResizeControl(control, splitterPane) {
           control.SetWidth(splitterPane.GetClientWidth());
           control.SetHeight(splitterPane.GetClientHeight());
       }
       function OnGridEndCallback() {
           sampleSplitter.GetPaneByName("gridContainer").RaiseResizedEvent();
       }
       function OnGetRowValues(values) {
           textBoxNumRepresentante.SetText(values[1]);
           textBoxRepresentante.SetText(values[2]);
           var memoText = '';
           if (values[3] != null) memoText += 'Base: ' + values[3] + '\n';
           if (values[4] != null) memoText += 'Base UP: ' + values[4] + '\n';
           if (values[5] != null) memoText += 'Base Up Porc: ' + values[5] + '\n';
           if (values[6] != null) memoText += 'Año: ' + values[6] + '\n';
           memoStuff.SetText(memoText);
           //           JFCV 20 dic 2019 se lo agregue para que al dar click en el grid cargue la gráfica
           BootstrapChart1.SetDataSource();
           ASPxCallbackPanel.PerformCallback("APPLYCOLUMNFILTER");
       }
       function UpdateEditorsValues() {
           sampleGrid.GetRowValues(sampleGrid.GetFocusedRowIndex(), 'Id_Cd;Id_Rik;Nom_Empleado;Base;BaseUP;BaseUp_Porc;Anio', OnGetRowValues);
       }
       var updateEditorsOnEndCallback = false;
       function OnListBoxValueChanged() {
           sampleGrid.Refresh();
           //           BootstrapChart1.Refresh();
           BootstrapChart1.SetDataSource();
           updateEditorsOnEndCallback = true;
           ASPxCallbackPanel.PerformCallback("APPLYCOLUMNFILTER");
       }
       function shoeed(s, e) {
           document.getElementById('infooo').style.display = "block";
       }
       function TxtId_Rik_OnBlur(sender, args) {
           OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
       }
       function CmbId_Rik_ClientSelectedIndexChanged(sender, eventArgs) {
           ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Rik.ClientID %>'));
       }
       function ToolBar_ClientClick(sender, args) {
           //debugger;
           var button = args.get_item();
           switch (button.get_value()) {
               case 'print':
                   continuarAccion = ValidacionesEspeciales();
                   break;
           }
           args.set_cancel(!continuarAccion);
       }
   </script>


    <style type="text/css">
        .custom-form-layout
        {}
    </style>

 
  
 
  <div runat="server" id="divPrincipal" class="row mt5 col-md-12">
        <div runat="server" id="div1" class="row mt5 col-md-12">
 <br />
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <span ID="lblMensajered" runat="server" style="color :Red"><asp:Label ID="lblMensaje" runat="server"></asp:Label></span>
                </td>
   
            </tr> 
        </table>


           
      <div class="container-fluid" runat="server">
          <div   class="row mt5">
              <div class="col-md-12">
                  <table style="border-spacing: 5px; border-collapse: separate; height: 25px; width: 100%;">
                      <tr>
                          <td style="width: 20px;">
                              <i class="fa fa-filter"></i>
                          </td>
                          <td style="width: 160;">
                              <table style="border-collapse: separate; width: 160;">
                                  <tr>
                                      <td>
                                          <dx:BootstrapComboBox ID="cmbAnio" runat="server" SelectedIndex="0" DropDownRows="5">
                                              <Items>
                                                  <dx:BootstrapListEditItem Text="2021" Value="2021" />
                                                  <dx:BootstrapListEditItem Text="2022" Value="2022" />
                                                  <dx:BootstrapListEditItem Text="2023" Value="2023" />
                                                  <dx:BootstrapListEditItem Text="2024" Value="2024" />
                                                  <dx:BootstrapListEditItem Text="2025" Value="2025" />
                                                  <dx:BootstrapListEditItem Text="2026" Value="2026" />
                                                  <dx:BootstrapListEditItem Text="2027" Value="2027" />
                                              </Items>
                                              <ButtonTemplate>
                                                  <span class="dxbs-edit-btn btn btn-primary dropdown-toggle" data-toggle="dropdown-show">Año</span>
                                              </ButtonTemplate>
                                          </dx:BootstrapComboBox>
                                      </td>
                                      <td>
                                         
                                      </td>
                                      
                                  </tr>
                              </table>
                          </td>
                         
                          <td style="width: 100px; text-align: right;">
                      
                              <dx:BootstrapButton ID="btnBuscar" runat="server" AutoPostBack="false" Text="Buscar"
                                  OnClick="btnBuscar_Click">
                                  <CssClasses Icon="glyphicon glyphicon-search" />
                                  <SettingsBootstrap RenderOption="Primary" Sizing="Small" />
                              </dx:BootstrapButton>
                              
                              

                          </td>
                          <td style="width: 100px; text-align: right;">
                              <dx:BootstrapButton ID="BootstrapButton1" runat="server" AutoPostBack="false" Text="Generar Excel"
                                  OnClick="btnGenerarEx_Click">
                                  <CssClasses Icon="glyphicon glyphicon-download-alt" />
                                  <SettingsBootstrap RenderOption="Warning" Sizing="Small" />
                              </dx:BootstrapButton>
                              

                          </td>
                          

                          <td>
        
                                <asp:HiddenField ID="HF_Cve" runat="server" />
                                <asp:HiddenField ID="HF_Rik" runat="server" />

                           </td>
                      </tr>
                       
                  </table>
              </div>
          </div>

          <div class="col-md-12" id="trRik" style="margin-top: 5px;">
                    <div class="col-md-4">
                              <asp:Label ID="LblId_Rik" Text="Representante" runat="server">
                               </asp:Label>&nbsp;
                        </div>
                    <div class="col-md-4">
                        <telerik:RadNumericTextBox ID="TxtId_Rik" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                       <ClientEvents OnBlur="TxtId_Rik_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                    </div>
              <div class="col-md-4">
                        <telerik:RadComboBox ID="cmbRik" runat="server" Width="250px" OnClientBlur="Combo_ClientBlur" 
                                                         OnClientSelectedIndexChanged="CmbId_Rik_ClientSelectedIndexChanged" >
                                </telerik:RadComboBox>

                  </div>

          </div>
          <div>
 
              <dx:ASPxGridView ID="grVertical" SettingsText-Title="Presupuesto Utilidad Prima" Styles-TitlePanel-HorizontalAlign="Left"
                                          runat="server" KeyFieldName="Id_Emp;Id_Rik;Id_Cd;Id_Presupuesto;Anio;Mes" Theme="Material"
                                          SettingsPager-EnableAdaptivity="true" Width="100%" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit"
                                          EnableCallbackAnimation="True" EnablePagingCallbackAnimation="False" EnableTheming="True"
                                          CustomColumnDisplayText="grdHorizontal_CustomColumnDisplayText">
                                          <Settings ShowTitlePanel="True" ShowFilterRow="False" />
                                          <SettingsPager EnableAdaptivity="True">
                                          </SettingsPager>
                                          <SettingsDataSecurity AllowInsert="False" />
                                          <SettingsText Title="Presupuesto Anual"></SettingsText>
                                        
                                          <Columns>

                                              <dx:GridViewBandColumn Caption=" ">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>

                                                                          <dx:GridViewDataTextColumn Caption="#" FieldName="Id_Emp" VisibleIndex="3" Width="80px"
                                                                              Visible="false">
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption="Id_Presupuesto" FieldName="Id_Presupuesto" VisibleIndex="4"
                                                                              Width="80px" Visible="false">
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption=" C D I " FieldName="Id_Cd" VisibleIndex="5" Width="100px" Visible="false">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption="Nombre Sucursal" FieldName="NomCdi" VisibleIndex="6"
                                                                              Width="150px"  Visible="false">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption="Num Rik" FieldName="Id_Rik" VisibleIndex="7"
                                                                              Width="80px">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption="Representante" FieldName="Nom_Empleado" VisibleIndex="8"
                                                                              Width="250px">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>

                                                                           <dx:GridViewDataTextColumn Caption="Mes" FieldName="MesLetra" VisibleIndex="9" Width="70px"
                                                                              HeaderStyle-HorizontalAlign="Center">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn Caption="Base" FieldName="BaseUP" VisibleIndex="10" Width="80px"
                                                                              HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                                  BackColor="#0081b4"></HeaderStyle>
                                                                              <CellStyle Font-Size="Smaller">
                                                                              </CellStyle>
                                                                              <PropertiesTextEdit DisplayFormatString="c">
                                                                              </PropertiesTextEdit>
                                                                          </dx:GridViewDataTextColumn>
                                                </Columns>
                                              </dx:GridViewBandColumn>

                                              <dx:GridViewBandColumn Caption="UTILIDAD PRIMA">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>

                                                            <dx:GridViewDataTextColumn Caption="PRESUPUESTO" FieldName="UP_Presupuesto" VisibleIndex="12" Width="80px"
                                                              HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                  BackColor="#0081b4"></HeaderStyle>
                                                              <CellStyle Font-Size="Smaller">
                                                              </CellStyle>
                                                              <PropertiesTextEdit DisplayFormatString="c">
                                                              </PropertiesTextEdit>
                                                          </dx:GridViewDataTextColumn>
 
                                              
                                                          <dx:GridViewDataTextColumn Caption="REAL" FieldName="UP" VisibleIndex="13" Width="80px"
                                                              HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                              <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                                  BackColor="#0081b4"></HeaderStyle>
                                                              <CellStyle Font-Size="Smaller">
                                                              </CellStyle>
                                                              <PropertiesTextEdit DisplayFormatString="c">
                                                              </PropertiesTextEdit>
                                                          </dx:GridViewDataTextColumn>
                                              </Columns>
                                              </dx:GridViewBandColumn>

                                              <dx:GridViewBandColumn Caption=" ">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>

                                                       <dx:GridViewDataTextColumn Caption="META PPTO" FieldName="Meta_Ppto" VisibleIndex="14" Width="80px"
                                                          HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                          <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue" 
                                                              BackColor="#0081b4"></HeaderStyle>
                                                          <CellStyle Font-Size="Smaller">
                                                          </CellStyle>
                                                          <PropertiesTextEdit DisplayFormatString="c">
                                                          </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                
                                                       </Columns>
                                              </dx:GridViewBandColumn>
                                                <dx:GridViewBandColumn Caption="INCREMENTO">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>
                                                       <dx:GridViewDataTextColumn Caption="REAL" FieldName="Incremento_Real" VisibleIndex="15" Width="80px"
                                                          HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                          <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"  
                                                              BackColor="#0081b4"></HeaderStyle>
                                                          <CellStyle Font-Size="Smaller">
                                                          </CellStyle>
                                                          <PropertiesTextEdit DisplayFormatString="c">
                                                          </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>

                                                      </Columns>
                                              </dx:GridViewBandColumn>
                                                <dx:GridViewBandColumn Caption="%">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>

                                                      <dx:GridViewDataTextColumn Caption="CUMPLIMIENTO" FieldName="Porc_Cumplimiento" VisibleIndex="16" Width="80px"
                                                          HeaderStyle-HorizontalAlign="Center">
                                                          <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"  
                                                              BackColor="#0081b4"></HeaderStyle>
                                                          <CellStyle Font-Size="Smaller">
                                                          </CellStyle>
                                                           <PropertiesTextEdit DisplayFormatString="p">
                                                          </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                             </Columns>
                                              </dx:GridViewBandColumn>
                                                 <dx:GridViewBandColumn Caption=" ">
                                                 <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                 <Columns>

                                                      <dx:GridViewDataTextColumn Caption="MULTIPLICADOR" FieldName="Multiplicador" VisibleIndex="17" Width="80px"
                                                          HeaderStyle-HorizontalAlign="Center">
                                                          <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"  
                                                              BackColor="#0081b4"></HeaderStyle>
                                                          <CellStyle Font-Size="Smaller">
                                                          </CellStyle>
                                                           <PropertiesTextEdit DisplayFormatString="p">
                                                          </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>

                                                      <dx:GridViewDataTextColumn Caption="Mes" FieldName="Mes" VisibleIndex="18" Width="80px"
                                                          HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                          <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium" ForeColor="AliceBlue"
                                                              BackColor="#0081b4"></HeaderStyle>
                                                          <CellStyle Font-Size="Smaller">
                                                          </CellStyle>
                                                      </dx:GridViewDataTextColumn>
                                              </Columns>
                                              </dx:GridViewBandColumn>

                                          </Columns>
                                          <Styles>
                                              <TitlePanel HorizontalAlign="Left">
                                              </TitlePanel>
                                          </Styles>
                                           <SettingsPager EnableAdaptivity="True" PageSize="12">
                     </SettingsPager>
                                      </dx:ASPxGridView>
                                      <dx:ASPxGridViewExporter ID="grVerticalReporter" runat="server" GridViewID="grVertical">
                                      </dx:ASPxGridViewExporter>


               

              <div>
                   <dx:ASPxGridView ID="grdHorizontal"  SettingsText-Title="Presupuesto Utilidad Prima" Styles-TitlePanel-HorizontalAlign="Left"
                                          runat="server" KeyFieldName="Id_Emp;Id_Rik;Id_Cd;Id_Presupuesto;Anio;Mes" Theme="Material"
                                          SettingsPager-EnableAdaptivity="false" Width="100%"  
                                          EnableCallbackAnimation="True" EnablePagingCallbackAnimation="False" EnableTheming="True">
                                          <Settings ShowTitlePanel="True" ShowFilterRow="false" />
                                          <SettingsPager EnableAdaptivity="True">
                                          </SettingsPager>
                                          <SettingsDataSecurity AllowInsert="False" />
                                          <SettingsText Title="Presupuesto Utilidad Prima"></SettingsText>
                                         
                                          <Columns>

                                                                                    <dx:GridViewDataTextColumn Caption="#" FieldName="Id_Emp" VisibleIndex="3" Width="80px"
                                                                                        Visible="false">
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Id_Presupuesto" FieldName="Id_Presupuesto" VisibleIndex="4"
                                                                                        Width="80px" Visible="false">
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption=" C D I " FieldName="Id_Cd" VisibleIndex="5" Width="150px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Nombre Sucursal" FieldName="NomCdi" VisibleIndex="6"
                                                                                        Width="450px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller"  BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Num Rik" FieldName="Id_Rik" VisibleIndex="7"
                                                                                        Width="80px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Representante" FieldName="Nom_Empleado" VisibleIndex="8"
                                                                                        Width="450px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller"  BackColor="#FFC300">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Base" FieldName="BaseUP" VisibleIndex="10"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                               

                                                                               
                                                                                    <dx:GridViewDataTextColumn Caption="Enero" FieldName="UP1" VisibleIndex="11"
                                                                                        Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Febrero" FieldName="UP2" VisibleIndex="14"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Marzo" FieldName="UP3" VisibleIndex="17"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                  
                                                                                    <dx:GridViewDataTextColumn Caption="Abril" FieldName="UP4" VisibleIndex="20"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Mayo" FieldName="UP5" VisibleIndex="23"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Junio" FieldName="UP6" VisibleIndex="26"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Julio" FieldName="UP7" VisibleIndex="29"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Agosto" FieldName="UP8" VisibleIndex="32"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                   
                                                                                    <dx:GridViewDataTextColumn Caption="Septiembre" FieldName="UP9" VisibleIndex="35"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Octubre" FieldName="UP10" VisibleIndex="38"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Noviembre" FieldName="UP11" VisibleIndex="41"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                                         
                                                                                    <dx:GridViewDataTextColumn Caption="Diciembre" FieldName="UP12" VisibleIndex="44"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                              
                                                                                    <dx:GridViewDataTextColumn Caption="Meta" FieldName="Meta_Ppto" VisibleIndex="44"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#FFC300"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>

                                                                                 
                                                                        </Columns>
                                                                        <Styles>
                                                                            <TitlePanel HorizontalAlign="Left">
                                                                            </TitlePanel>
                                                                        </Styles>
                                                                    </dx:ASPxGridView>

                  </div>
              <div>
               <dx:ASPxGridView ID="grdRealUp"  SettingsText-Title="Real Utilidad Prima" Styles-TitlePanel-HorizontalAlign="Left"
                                          runat="server" KeyFieldName="Id_Emp;Id_Rik;Id_Cd;Id_Presupuesto;Anio;Mes" Theme="Material"
                                          SettingsPager-EnableAdaptivity="false" Width="100%"
                                          EnableCallbackAnimation="False" EnablePagingCallbackAnimation="False" EnableTheming="True">
                                          <Settings ShowTitlePanel="True" ShowFilterRow="false" />
                                          <SettingsPager EnableAdaptivity="True">
                                          </SettingsPager>
                                          <SettingsDataSecurity AllowInsert="False" />
                                          <SettingsText Title="Real Utilidad Prima"></SettingsText>
                                                                                    <Columns>

                                                                           
                                                                                     <dx:GridViewDataTextColumn Caption="#" FieldName="Id_Emp" VisibleIndex="3" Width="80px"
                                                                                        Visible="false">
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Id_Presupuesto" FieldName="Id_Presupuesto" VisibleIndex="4"
                                                                                        Width="80px" Visible="false">
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption=" C D I " FieldName="Id_Cd" VisibleIndex="5" Width="150px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Nombre Sucursal" FieldName="NomCdi" VisibleIndex="6"
                                                                                        Width="450px" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller"  BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Num Rik" FieldName="Id_Rik" VisibleIndex="7"
                                                                                        Width="80px" Visible ="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Representante" FieldName="Nom_Empleado" VisibleIndex="8"
                                                                                        Width="450px" Visible ="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7">
                                                                                        </CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Base" FieldName="BaseUP" VisibleIndex="10"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center" UnboundType="Decimal" Visible ="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                               

                                                                               
                                                                                    <dx:GridViewDataTextColumn Caption="Enero" FieldName="UP1"  VisibleIndex="11"
                                                                                        Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Febrero" FieldName="UP2"  VisibleIndex="12"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Marzo" FieldName="UP3"  VisibleIndex="13"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                        <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                  
                                                                                    <dx:GridViewDataTextColumn Caption="Abril" FieldName="UP4"  VisibleIndex="14"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Mayo" FieldName="UP5"  VisibleIndex="15"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Junio" FieldName="UP6"  VisibleIndex="16"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Julio" FieldName="UP7"  VisibleIndex="17"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Agosto" FieldName="UP8" VisibleIndex="18"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                   
                                                                                    <dx:GridViewDataTextColumn Caption="Septiembre" FieldName="UP9" VisibleIndex="19"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Octubre" FieldName="UP10" VisibleIndex="20"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    
                                                                                    <dx:GridViewDataTextColumn Caption="Noviembre" FieldName="UP11" VisibleIndex="21"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                       <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                                         
                                                                                    <dx:GridViewDataTextColumn Caption="Diciembre" FieldName="UP12" VisibleIndex="22"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                              
                                                                                    <dx:GridViewDataTextColumn Caption="Meta" FieldName="UP12" VisibleIndex="44"
                                                                                        Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="Medium"   ForeColor="AliceBlue" BackColor="#0081b4"></HeaderStyle>
                                                                                      <CellStyle Font-Size="Smaller" BackColor="#A6EDF7"> </CellStyle>
                                                                                         <PropertiesTextEdit DisplayFormatString="c"> </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>

                                                                                 
                                                                  
                                                                                 
                                                                        </Columns>
                                                                        <Styles>
                                                                            <TitlePanel HorizontalAlign="Left">
                                                                            </TitlePanel>
                                                                        </Styles>
                                                                    </dx:ASPxGridView>
                                                                   
                                                                </div>
              
              <br />
               <div class="row">
                   </div>
              </div>


                                  <div class="row">
                                     <%-- <dx:BootstrapButton ID="BootBtnVerticalExportar" runat="server" AutoPostBack="false"
                                          Text="Exportar A excel" OnClick="BootBtnVerticalExportar_Click">
                                          <CssClasses Icon="glyphicon glyphicon-search" />
                                          <SettingsBootstrap RenderOption="Primary" Sizing="Small" />
                                      </dx:BootstrapButton>--%>
                                  </div>

          

          </div>
    </div>

            </div>

   
</asp:Content>