
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master"
    AutoEventWireup="true" CodeBehind="ReporteComerciales_MonitoreoProducto.aspx.cs" Inherits="SIANWEB.ReporteComerciales_MonitoreoProducto" %>




<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <style>
          .celdaazultenue
          {
              background-color:transparent;
              text-align:center;
          }
       .footergrid
        {
           background-color:transparent;  
           font: 12px "segoe ui",arial,sans-serif;
           font-weight:bold;
           text-align:center;
           color:#0088d7;
        }
        .EncabezadosIntegralidadAzules
        {
                 font: 12px "segoe ui",arial,sans-serif;
                 font-weight:bold;
               background-color: transparent;
          }
        .tablas{
            font: 12px "segoe ui",arial,sans-serif;
        }
        h3{
             font:"segoe ui",arial,sans-serif;
        }
        .tablaFiltros
        {
            font:12px "segoe ui",arial,sans-serif;
        }
        .Encabezados
        {
             font:16px "segoe ui",arial,sans-serif;
             font-weight:bold;
            
        }
        .boton{
            color: #fff;
             background-color: #337ab7;
             border-color: #2e6da4;
             padding: 5px 10px;
               font-size: 12px;
               line-height: 1.5;
               border-radius: 3px;
               background-image: none;  
        }
 
    </style>


    <h2 style="font-weight: bolder">Reportes de Monitoreo de Producto </h2>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server">
                <TabPages>
                   <dx:TabPage Text="Descargar información">
                       <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True" >
                                <table class="tablaFiltros" style="width:1000px">
                                    <tr>
                                            <td>

                                                Seleccione el modo:
                                            </td>
                                            <td>
                                                    <dx:ASPxRadioButtonList ID="rdlModo" ClientInstanceName="rdlModo" runat="server" RepeatDirection="Horizontal" Border-BorderStyle="None">
                                                        <ClientSideEvents ValueChanged="
                                                            function(s,e)
                                                            {  var n= rdlModo.GetValue();
                                                                if(n==1){ $('.porCampania').show(); $('.porProducto').hide();}
                                                                else { $('.porCampania').hide(); $('.porProducto').show();}
                                                            }
                                                            " />
                                                            <Items>
                                                                <dx:ListEditItem Text="Por Campaña" Value="1" Selected="true"/>
                                                                <dx:ListEditItem Text="Por Producto" Value="2" />
                                                            </Items>

                                                    </dx:ASPxRadioButtonList>
                                            </td>
                                    </tr>

                                     <tr>
                                         <td>Tipo de Reporte:</td>
                                        <td colspan="3">
                                            <dx:ASPxCheckBoxList ID="chkTipoReporte" ClientInstanceName="chkTipoReporte" runat="server"
                                                RepeatDirection="Horizontal" Border-BorderStyle="None"
                                                >
                                                <ClientSideEvents  SelectedIndexChanged="
                                                    function(s,e)
                                                    {
                                                       
                                                       // var res=chkTipoReporte.GetSelectedValues();
                                                       
                                                    }
                                                    " />
                                                <Items>
                                                                <dx:ListEditItem Text="Gestión de proyectos" Value="1"  />
                                                                <dx:ListEditItem Text="Gestión de ACyS" Value="2" />
                                                                <dx:ListEditItem Text="Captura de Pedidos" Value="3" />
                                                                <dx:ListEditItem Text="Facturación" Value="4" />
                                                                
                                                 </Items>
                                            </dx:ASPxCheckBoxList>

                                        </td>
                                    </tr>
                                    <tr>


                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr class="porCampania">
                                        <td>
                                            Campaña:
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbCampania" runat="server"></dx:ASPxComboBox>
                                        </td>
                                   </tr>
                                    <tr class="porCampania">
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr class="porProducto">
                                        <td>
                                            Productos:
                                        </td>
                                        <td>
                                                <dx:ASPxGridLookup ID="listaProductos" runat="server" SelectionMode="Multiple" KeyFieldName="Num" Width="275px">
                                                     <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                        <dx:GridViewDataColumn FieldName="Num" Width="50px" />
                                                        <dx:GridViewDataTextColumn FieldName="Nombre" Width="300px">
                                                               <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <GridViewProperties>
                                                          <Settings ShowFilterRow="True" ShowStatusBar="Visible" AutoFilterCondition="Contains"  />
                                                        <SettingsPager PageSize="10" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                                </dx:ASPxGridLookup>
                                        </td>
                                        
                        
                                        <td>Fecha Inicio: </td> <td><dx:ASPxDateEdit runat="server" ID="dtFechaInicio"></dx:ASPxDateEdit></td>
                                        <td>Fecha Fin: </td> <td><dx:ASPxDateEdit runat="server" ID="dtFechaFin"></dx:ASPxDateEdit></td>
                                    </tr>

                                     <tr  class="porProducto">
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td>Representante</td>
                                        <td>
                                           <dx:ASPxGridLookup ID="listaRepresentantes" runat="server" SelectionMode="Multiple" KeyFieldName="Id_Rik" Width="275px">
                                                      <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"  />
                                                        <dx:GridViewDataColumn FieldName="Id_Rik" Width="50px" Caption="Num" />
                                                        <dx:GridViewDataTextColumn FieldName="Rik_Nombre" Width="300px" Caption="Nombre">
                                                               <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                       <GridViewProperties>
                                                          <SettingsPager PageSize="15" EnableAdaptivity="true" />
                                                      </GridViewProperties>
                                            </dx:ASPxGridLookup>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="7" style="text-align:right">
                                        <dx:ASPxButton ID="btnFiltro" Text="🔎 Aplicar"
                                            runat="server" ClientInstanceName="btnFiltro"  AutoPostBack="false" CssClass="boton" EnableTheming="False">
                                            
                                            <ClientSideEvents Click="function(s,e)
                                                {
                                                    //alert('Hola');
                                                   CallbackPanel1.PerformCallback();
                                              
                                                }
                                                " />
                                           
                                       </dx:ASPxButton>

                                        </td>
                                        <td> &nbsp; &nbsp;  <dx:ASPxButton ID="btnExportar" Text="Exportar" runat="server" CssClass="boton" EnableTheming="False" OnClick="btnExportar_Click"></dx:ASPxButton>
                                                                                    </td>
                                    </tr>
                 </table>

                <br />

                  <dx:ASPxGridViewExporter ID="grdExporter1" runat="server" GridViewID="RepGestProy_Listado" OnRenderBrick="grdExporter1_RenderBrick"  />
                  <dx:ASPxGridViewExporter ID="grdExporter2" runat="server" GridViewID="RepGestACYS_Listado"  OnRenderBrick="grdExporter2_RenderBrick" />
                  <dx:ASPxGridViewExporter ID="grdExporter3" runat="server" GridViewID="RepGestPed_Listado" OnRenderBrick="grdExporter3_RenderBrick" />
                  <dx:ASPxGridViewExporter ID="grdExporter4" runat="server" GridViewID="RepGestFac_Listado" OnRenderBrick="grdExporter4_RenderBrick"  />

               <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" ClientInstanceName="CallbackPanel1"
                 RenderMode="Table">
                    <ClientSideEvents EndCallback="function(s,e)
                                                    {
                                                          $('#divPrincipal').show();
                                                    }
                        " ></ClientSideEvents>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                <div id="divPrincipal">
                




                            <asp:Label ID="lblTitGestProy" runat="server"  CssClass="Encabezados"><br />Gestión de proyectos <br /></asp:Label>

                              <dx:ASPxGridView ID="RepGestProy_Listado" ClientInstanceName="RepGestProy_Listado" runat="server" Width="1000px"  KeyFieldName="Id_Rik" >
                                    <SettingsExport EnableClientSideExportAPI="true" />
                                  <SettingsDetail ExportMode="Expanded" /> 
                                 <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules" Footer-BackColor="Transparent" ></Styles>
                                  <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                


                                 <Columns>

                                       <dx:GridViewDataTextColumn FieldName="Rik_Nombre" Caption="Represesentante" ExportWidth="300"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NumOp" Caption="Total de Proyectos" ExportWidth="120">
                                            <FooterCellStyle CssClass="footergrid" HorizontalAlign="Center" ></FooterCellStyle>
                                             <CellStyle CssClass="celdaazultenue"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Subtotal" Caption="Subtotal" ExportWidth="100">
                                                <PropertiesTextEdit DisplayFormatString="C" />
                                            <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                             <CellStyle CssClass="celdaazultenue"></CellStyle>
                                        </dx:GridViewDataTextColumn>


                                           <dx:GridViewBandColumn Caption="Etapas del Proyecto" HeaderStyle-HorizontalAlign="Center">
                                                 <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="Analisis" Caption="Analisis" ExportCellStyle-BackColor="Transparent" ExportWidth="100">
                                                                        <PropertiesTextEdit DisplayFormatString="C" />
                                                                        <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                        
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Planeacion" Caption="Planeacion" ExportWidth="100">
                                                                         <PropertiesTextEdit DisplayFormatString="C" />
                                                                          <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                         <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Negociacion" Caption="Negociacion" ExportWidth="100">
                                                                         <PropertiesTextEdit DisplayFormatString="C" />
                                                                          <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                         <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Cierre" Caption="Cierre" Name="Cierre" ExportWidth="100">
                                                                         <PropertiesTextEdit DisplayFormatString="C" />
                                                                          <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                         <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                     </Columns>
                                        </dx:GridViewBandColumn>

                                 </Columns>
                                  <TotalSummary>
                                      <dx:ASPxSummaryItem FieldName="NumOp" SummaryType="Sum"  DisplayFormat="{0}"/>
                                      <dx:ASPxSummaryItem FieldName="Subtotal" SummaryType="Sum" ShowInColumn="Subtotal" DisplayFormat="C"/>
                                       <dx:ASPxSummaryItem FieldName="Analisis" SummaryType="Sum" ShowInColumn="Analisis" DisplayFormat="C"/>
                                       <dx:ASPxSummaryItem FieldName="Planeacion" SummaryType="Sum" ShowInColumn="Planeacion" DisplayFormat="C" />
                                       <dx:ASPxSummaryItem FieldName="Negociacion" SummaryType="Sum" ShowInColumn="Negociacion" DisplayFormat="C" />
                                     <dx:ASPxSummaryItem FieldName="Cierre" SummaryType="Sum" ShowInColumn="Cierre" DisplayFormat="C" />
                                    </TotalSummary>

                                             <Templates>
                                                    <DetailRow>

                                                        <br />
                                                             <dx:ASPxGridView ID="RepGestProy_Detalle" ClientInstanceName="RepGestProy_Detalle" KeyFieldName="Id_Op" runat="server" Width="100%" 
                                                                 OnDataBinding="RepGestProy_Detalle_DataBinding" >
                                                                
                                                                 <SettingsDetail ExportMode="Expanded" /> 
                                                                   <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                                              <Columns> 
                                                                  <dx:GridViewDataTextColumn FieldName="Id_Cte" Caption="Num Cte."  ExportWidth="100">
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                  </dx:GridViewDataTextColumn>
                                                                  <dx:GridViewDataTextColumn FieldName="Cte_NomComercial" Caption="Cliente"  ExportWidth="300">
                                                                  </dx:GridViewDataTextColumn>
                                                                  <dx:GridViewDataTextColumn FieldName="Id_Op" Caption="Num Proyecto"  ExportWidth="100">
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                  </dx:GridViewDataTextColumn>

                                                                    <dx:GridViewDataTextColumn FieldName="Analisis" Caption="Analisis"  ExportWidth="100">
                                                                           <PropertiesTextEdit DisplayFormatString="C" />
                                                                        <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Planeacion" Caption="Planeacion"  ExportWidth="100">
                                                                           <PropertiesTextEdit DisplayFormatString="C" />
                                                                        <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Negociacion" Caption="Negociacion"  ExportWidth="100">
                                                                           <PropertiesTextEdit DisplayFormatString="C" />
                                                                        <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Cierre" Caption="Cierre"  ExportWidth="100">
                                                                           <PropertiesTextEdit DisplayFormatString="C" />
                                                                        <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                              </Columns> 
                                                            
                                                                  <TotalSummary>
 
                                                                       <dx:ASPxSummaryItem FieldName="Analisis" SummaryType="Sum" ShowInColumn="Analisis" DisplayFormat="C"/>
                                                                       <dx:ASPxSummaryItem FieldName="Planeacion" SummaryType="Sum" ShowInColumn="Planeacion" DisplayFormat="C" />
                                                                       <dx:ASPxSummaryItem FieldName="Negociacion" SummaryType="Sum" ShowInColumn="Negociacion" DisplayFormat="C" />
                                                                       <dx:ASPxSummaryItem FieldName="Cierre" SummaryType="Sum" ShowInColumn="Cierre" DisplayFormat="C" />
                                                                 </TotalSummary>
                                                                   <Settings ShowFooter="True" />

                                                                      <Templates>
                                                                        <DetailRow>

                                                                            <br />

                                                                                <dx:ASPxGridView ID="RepGestProy_Detalle_Prod" ClientInstanceName="RepGestProy_Detalle_Prod" KeyFieldName="Id_Op" runat="server" Width="100%" 
                                                                                                                  OnDataBinding="RepGestProy_Detalle_Prod_DataBinding"  > 
                                                                                    <SettingsDetail ExportMode="Expanded" /> 
                                                                                       <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules" Footer-BackColor="Transparent" ></Styles>
                                                                                             <Columns> 
                                                                                                         <dx:GridViewDataTextColumn FieldName="Id_Prd" Caption="Num Prod." Width="100px"  ExportWidth="100">
                                                                                                               <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                         </dx:GridViewDataTextColumn>
                                                                                                          <dx:GridViewDataTextColumn FieldName="Prd_Descripcion" Caption="Producto." Width="300px"  ExportWidth="300">
                                                                                                               
                                                                                                          </dx:GridViewDataTextColumn>
                                                                                                          <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades" Width="100px"  ExportWidth="100">
                                                                                                                 <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                          </dx:GridViewDataTextColumn>
                                                                                                          <dx:GridViewDataTextColumn FieldName="Precio" Caption="Precio" Width="100px"  ExportWidth="100">
                                                                                                              <PropertiesTextEdit DisplayFormatString="C" />
                                                                                                                <FooterCellStyle CssClass="footergrid" HorizontalAlign="Center"></FooterCellStyle>
                                                                                                                <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                          </dx:GridViewDataTextColumn>

                                                                                                           <dx:GridViewDataTextColumn FieldName="Subtotal" Caption="Subtotal" Width="100px"  ExportWidth="100">
                                                                                                              <PropertiesTextEdit DisplayFormatString="C" />
                                                                                                                <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                                                <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                           </dx:GridViewDataTextColumn>
                                                                                            </Columns> 
                                                                                 <TotalSummary>
                                                                                      <dx:ASPxSummaryItem FieldName="Precio" SummaryType="Sum" ShowInColumn="Precio" DisplayFormat="C"/>
                                                                                       <dx:ASPxSummaryItem FieldName="Subtotal" SummaryType="Sum" ShowInColumn="Subtotal" DisplayFormat="C"/>
                                                                                 </TotalSummary>
                                                                                   <Settings ShowFooter="True" />
                                                                                          

                                                                                </dx:ASPxGridView>
                                                                             <br />
                                                                         </DetailRow>
                                                                       </Templates>
                                                                             <SettingsDetail ShowDetailRow="true" />
                                                              </dx:ASPxGridView>

                                                        </DetailRow>
                                                </Templates>
                                     <SettingsDetail ShowDetailRow="true" />
                                     <Settings ShowFooter="True" />
                                </dx:ASPxGridView>

                            <asp:Label ID="lblTitGesAcys" runat="server"   CssClass="Encabezados"><br /> Gestión de ACyS<br /></asp:Label>

                             <dx:ASPxGridView ID="RepGestACYS_Listado" ClientInstanceName="RepGestACYS_Listado" runat="server" Width="1000px"  KeyFieldName="Id_Rik" >
                                 <SettingsDetail ExportMode="Expanded" /> 
                                 <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                 <Columns>
                                           <dx:GridViewDataTextColumn FieldName="Rik_Nombre" Caption="Representante"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                           <dx:GridViewDataTextColumn FieldName="Total_Acuerdos" Caption="Total de Acuerdos"  ExportWidth="120">
                                                <CellStyle CssClass="celdaazultenue"></CellStyle>
                                           </dx:GridViewDataTextColumn>
                                           <dx:GridViewDataTextColumn FieldName="VentaConsolidada" Caption="Venta Instalada"  ExportWidth="100">
                                               <PropertiesTextEdit DisplayFormatString="C" />
                                               <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                               <CellStyle CssClass="celdaazultenue"></CellStyle>
                                           </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                <CellStyle CssClass="celdaazultenue"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                 </Columns>
                                    <TotalSummary>
                                         <dx:ASPxSummaryItem FieldName="VentaConsolidada" SummaryType="Sum" ShowInColumn="VentaConsolidada" DisplayFormat="C"/>
                                          <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                       </TotalSummary>
                                        <Settings ShowFooter="True" />

                                             <Templates>
                                                    <DetailRow>

                                        
                                                        <br />
                                                             <dx:ASPxGridView ID="RepGestACYS_Detalle" ClientInstanceName="RepGestACYS_Detalle" KeyFieldName="Id_Acs" runat="server" Width="100%" 
                                                                 OnDataBinding="RepGestACYS_Detalle_DataBinding" > 
                                                                 <SettingsDetail ExportMode="Expanded" /> 
                                                                 <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules" Footer-BackColor="Transparent" ></Styles>
                                                              <Columns> 
                                                                 
                                                                  <dx:GridViewDataTextColumn FieldName="Id_Cte" Caption="Num Cte."  ExportWidth="100">
                                                                      <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                  </dx:GridViewDataTextColumn>
                                                                  <dx:GridViewDataTextColumn FieldName="Cte_NomComercial" Caption="Cliente"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="Id_Acs" Caption="Num ACyS">
                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                       <dx:GridViewDataTextColumn FieldName="VentaConsolidada" Caption="Venta Instalada"  ExportWidth="100">
                                                                            <PropertiesTextEdit DisplayFormatString="C" />
                                                                           <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                       </dx:GridViewDataTextColumn>
                                                                  
                                                                   <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                       <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>   
                                                                   </dx:GridViewDataTextColumn>
                                                              </Columns> 
                                                              <TotalSummary>
                                                                 <dx:ASPxSummaryItem FieldName="VentaConsolidada" SummaryType="Sum" ShowInColumn="VentaConsolidada" DisplayFormat="C"/>
                                                                  <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                               </TotalSummary>
                                                                 <Settings ShowFooter="True" />
                                                                      <Templates>
                                                                        <DetailRow>

                                                                         <br />

                                                                                <dx:ASPxGridView ID="RepGestACYS_Detalle_Prod" ClientInstanceName="RepGestACYS_Detalle_Prod" KeyFieldName="Id_Acs" runat="server" Width="100%" 
                                                                                                    OnDataBinding="RepGestACYS_Detalle_Prod_DataBinding" > 
                                                                                    <SettingsDetail ExportMode="Expanded" /> 
                                                                                    <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                                                                             <Columns> 
                                                                                                  <dx:GridViewDataTextColumn FieldName="Id_Prd" Caption="Código"  ExportWidth="100">
                                                                                                        <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                  </dx:GridViewDataTextColumn>
                                                                                                  <dx:GridViewDataTextColumn FieldName="Prd_Descripcion" Caption="Producto"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="Acs_Precio" Caption="Precio">
                                                                                                      <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                 </dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                                                      <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>   
                                                                                                 </dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="VentaConsolidada" Caption="Subtotal"  ExportWidth="100">
                                                                                                     <PropertiesTextEdit DisplayFormatString="C" />
                                                                                                      <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>   
                                                                                                 </dx:GridViewDataTextColumn>
                                                                                            </Columns> 
                                                                                    <TotalSummary>
                                                                                     <dx:ASPxSummaryItem FieldName="VentaConsolidada" SummaryType="Sum" ShowInColumn="VentaConsolidada" DisplayFormat="C"/>
                                                                                      <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                                   </TotalSummary>
                                                                                     <Settings ShowFooter="True" />
                                                                                </dx:ASPxGridView>
                                                                            <br />
                                                                         </DetailRow>
                                                                       </Templates>
                                                                             <SettingsDetail ShowDetailRow="true" />
                                                              </dx:ASPxGridView>

                                                        </DetailRow>
                                                </Templates>
                                     <SettingsDetail ShowDetailRow="true" />

                                </dx:ASPxGridView>

                            <asp:Label ID="lblGestPedidos" runat="server"   CssClass="Encabezados"><br />Gestión de Pedidos<br /></asp:Label>

                            <dx:ASPxGridView ID="RepGestPed_Listado" ClientInstanceName="RepGestPed_Listado" runat="server" Width="1000px"  KeyFieldName="Id_U" >
                                <SettingsDetail ExportMode="Expanded" /> 
                                <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>

                                             <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="U_Nombre" Caption="Usuario"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                                      <dx:GridViewDataTextColumn FieldName="Total_Pedidos" Caption="Total Pedidos"  ExportWidth="100">
                                                                                           <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                      </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="MontoCaptado" Caption="Monto Captado"  ExportWidth="100">
                                                                                        <PropertiesTextEdit DisplayFormatString="C" />
                                                                                       <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                          
                                                                                       <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                    </dx:GridViewDataTextColumn>
                                             </Columns>
                                                                                            <TotalSummary>
                                                                                                 <dx:ASPxSummaryItem FieldName="MontoCaptado" SummaryType="Sum" ShowInColumn="MontoCaptado" DisplayFormat="C"/>
                                                                                                  <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                                               </TotalSummary>
                                                                                 <Settings ShowFooter="True" />

                                                         <Templates>
                                                                <DetailRow>

                                                   
                                                                    <br />
                                                                         <dx:ASPxGridView ID="RepGestPEd_Detalle" ClientInstanceName="RepGestPEd_Detalle" KeyFieldName="Id_Ped" runat="server" Width="100%" 
                                                                            OnDataBinding="RepGestPEd_Detalle_DataBinding" > 
                                                                             <SettingsDetail ExportMode="Expanded" /> 
                                                                             <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                                                          <Columns> 
                                                                                 <dx:GridViewDataTextColumn FieldName="Id_Cte" Caption="Num Cte"  ExportWidth="100">
                                                                                      <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                 </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn FieldName="Cte_NomComercial" Caption="Cliente"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn FieldName="Id_Ped" Caption="Pedido"  ExportWidth="100">
                                                                                      <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                 </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn FieldName="MontoCaptado" Caption="Monto Captado"  ExportWidth="100">
                                                                                     <PropertiesTextEdit DisplayFormatString="C" />
                                                                                      <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                 </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                                      <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                 </dx:GridViewDataTextColumn>
                                                                          </Columns> 
                                                                                  <TotalSummary>
                                                                                                 <dx:ASPxSummaryItem FieldName="MontoCaptado" SummaryType="Sum" ShowInColumn="MontoCaptado" DisplayFormat="C"/>
                                                                                                  <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                                    </TotalSummary>
                                                                                 <Settings ShowFooter="True" />

                                                                                  <Templates>
                                                                                    <DetailRow>

                                                                                         <br />

                                                                                            <dx:ASPxGridView ID="RepGestPed_Detalle_Prod" ClientInstanceName="RepGestPed_Detalle_Prod" KeyFieldName="Id_Ped" runat="server" Width="100%" 
                                                                                                          OnDataBinding="RepGestPed_Detalle_Prod_DataBinding"       > 
                                                                                                <SettingsDetail ExportMode="Expanded" /> 
                                                                                                <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules" Footer-BackColor="Transparent" ></Styles>
                                                                                                         <Columns> 
                                                                                                            <dx:GridViewDataTextColumn FieldName="Id_Prd" Caption="Num Prod."  ExportWidth="100">
                                                                                                                  <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                             <dx:GridViewDataTextColumn FieldName="Prd_Descripcion" Caption="Producto"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                                                             <dx:GridViewDataTextColumn FieldName="MontoCaptado" Caption="Monto Captado"  ExportWidth="100">
                                                                                                                 <PropertiesTextEdit DisplayFormatString="C" />
                                                                                                                  <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                             </dx:GridViewDataTextColumn>
                                                                                                             <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                                                                  <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                             </dx:GridViewDataTextColumn>

                                                                                                        </Columns> 
                                                                                              <TotalSummary>
                                                                                                             <dx:ASPxSummaryItem FieldName="MontoCaptado" SummaryType="Sum" ShowInColumn="MontoCaptado" DisplayFormat="C"/>
                                                                                                              <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                                               </TotalSummary>
                                                                                         <Settings ShowFooter="True" />

                                                                                            </dx:ASPxGridView>
                                                                                           <br />
                                                                                     </DetailRow>
                                                                                   </Templates>
                                                                                         <SettingsDetail ShowDetailRow="true" />
                                                                          </dx:ASPxGridView>
                                                                       <br />
                                                                    </DetailRow>
                                                            </Templates>
                                                 <SettingsDetail ShowDetailRow="true" />

                                            </dx:ASPxGridView>


                            <asp:Label ID="lblGestFacturas" runat="server"   CssClass="Encabezados"><br />Gestión de Facturas<br /></asp:Label>
                       
                             <dx:ASPxGridView ID="RepGestFac_Listado" ClientInstanceName="RepGestFac_Listado" runat="server" Width="1000px"  KeyFieldName="Id_U" >
                                 <SettingsDetail ExportMode="Expanded" /> 
                                 <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                 <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="U_Nombre" Caption="Usuario"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                          <dx:GridViewDataTextColumn FieldName="Total_Facturas" Caption="Total Facturas"  ExportWidth="100">
                                                                               <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                          </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="MontoFacturado" Caption="Monto Facturado"  ExportWidth="100">
                                                                           <PropertiesTextEdit DisplayFormatString="C" />
                                                                          <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                                <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                 </Columns>
                                                                <TotalSummary>
                                                                                     <dx:ASPxSummaryItem FieldName="MontoFacturado" SummaryType="Sum" ShowInColumn="MontoFacturado" DisplayFormat="C"/>
                                                                                      <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                        </TotalSummary>
                                  <Settings ShowFooter="True" />

                                             <Templates>
                                                    <DetailRow>

                                                       
                                                        <br />
                                                             <dx:ASPxGridView ID="RepGestFac_Detalle" ClientInstanceName="RepGestFac_Detalle" KeyFieldName="Id_Fac" runat="server" Width="100%" 
                                                                OnDataBinding="RepGestFac_Detalle_DataBinding" > 
                                                                 <SettingsDetail ExportMode="Expanded" /> 
                                                                 <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                                              <Columns> 
                                                                     <dx:GridViewDataTextColumn FieldName="Id_Cte" Caption="Num Cte"  ExportWidth="100">
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                     </dx:GridViewDataTextColumn>
                                                                     <dx:GridViewDataTextColumn FieldName="Cte_NomComercial" Caption="Cliente"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                     <dx:GridViewDataTextColumn FieldName="Id_Fac" Caption="Factura">
                                                                          <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                     </dx:GridViewDataTextColumn>
                                                                     <dx:GridViewDataTextColumn FieldName="MontoFacturado" Caption="Monto Facturado"  ExportWidth="100">
                                                                         <PropertiesTextEdit DisplayFormatString="C" />
                                                                           <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                     </dx:GridViewDataTextColumn>
                                                                     <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                           <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                     </dx:GridViewDataTextColumn>
                                                              </Columns> 
                                                                 <TotalSummary>
                                                                                     <dx:ASPxSummaryItem FieldName="MontoFacturado" SummaryType="Sum" ShowInColumn="MontoFacturado" DisplayFormat="C"/>
                                                                                      <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                        </TotalSummary>
                                                                  <Settings ShowFooter="True" />

                                                                      <Templates>
                                                                        <DetailRow>

                                                                            <br />

                                                                                <dx:ASPxGridView ID="RepGestFac_Detalle_Prod" ClientInstanceName="RepGestFac_Detalle_Prod" KeyFieldName="Id_Fac" runat="server" Width="100%" 
                                                                                            OnDataBinding="RepGestFac_Detalle_Prod_DataBinding"     > 
                                                                                    <SettingsDetail ExportMode="Expanded" /> 
                                                                                    <Styles Cell-CssClass="tablas" Header-CssClass="EncabezadosIntegralidadAzules"  Footer-BackColor="Transparent" ></Styles>
                                                                                             <Columns> 

                                                                                                 <dx:GridViewDataTextColumn FieldName="Id_Prd" Caption="Num Prod."  ExportWidth="100">
                                                                                                      <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                 </dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="Prd_Descripcion" Caption="Producto"  ExportWidth="300"></dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="MontoFacturado" Caption="Monto Facturado"  ExportWidth="100">
                                                                                                     <PropertiesTextEdit DisplayFormatString="C" />
                                                                                                       <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                 </dx:GridViewDataTextColumn>
                                                                                                 <dx:GridViewDataTextColumn FieldName="Unidades" Caption="Unidades"  ExportWidth="100">
                                                                                                       <FooterCellStyle CssClass="footergrid"  HorizontalAlign="Center"></FooterCellStyle>
                                                                              <CellStyle CssClass="celdaazultenue"></CellStyle>
                                                                                                 </dx:GridViewDataTextColumn>

                                                                                            </Columns> 
                                                                                    <TotalSummary>
                                                                                     <dx:ASPxSummaryItem FieldName="MontoFacturado" SummaryType="Sum" ShowInColumn="MontoFacturado" DisplayFormat="C"/>
                                                                                      <dx:ASPxSummaryItem FieldName="Unidades" SummaryType="Sum" ShowInColumn="Unidades" DisplayFormat="{0}"/>
                                                                        </TotalSummary>
                                                                                     <Settings ShowFooter="True" />
                                                                                </dx:ASPxGridView>
                                                                            <br />
                                                                         </DetailRow>
                                                                       </Templates>
                                                                             <SettingsDetail ShowDetailRow="true" />
                                                              </dx:ASPxGridView>

                                                        </DetailRow>
                                                </Templates>
                                     <SettingsDetail ShowDetailRow="true" />

                                </dx:ASPxGridView>
              
            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>








                              </dx:ContentControl>
                       </ContentCollection>
                   </dx:TabPage>

                    <dx:TabPage Text="Generar resultados">
                       <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">



                              </dx:ContentControl>
                       </ContentCollection>
                   </dx:TabPage>
                 </TabPages>
           </dx:ASPxPageControl>

    
<script>
    var textSeparator = ";";
    function updateText() {
        var selectedItems = checkListBox.GetSelectedItems();
        checkComboBox.SetText(getSelectedItemsText(selectedItems));
    }
    function synchronizeListBoxValues(dropDown, args) {
        checkListBox.UnselectAll();
        var texts = dropDown.GetText().split(textSeparator);
        var values = getValuesByTexts(texts);
        checkListBox.SelectValues(values);
        updateText(); // for remove non-existing texts
    }
    function getSelectedItemsText(items) {
        var texts = [];
        for (var i = 0; i < items.length; i++)
            texts.push(items[i].text);
        return texts.join(textSeparator);
    }
    function getValuesByTexts(texts) {
        var actualValues = [];
        var item;
        for (var i = 0; i < texts.length; i++) {
            item = checkListBox.FindItemByText(texts[i]);
            if (item != null)
                actualValues.push(item.value);
        }
        return actualValues;
    }
    $(document).ready(function () {
        $('.porProducto').hide();
    });
</script>


</asp:Content>
