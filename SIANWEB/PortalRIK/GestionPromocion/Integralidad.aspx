  
<%@ Page Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Integralidad.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Integralidad" %>

<%@ Register Assembly="DevExpress.XtraCharts.v19.2.Web, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxG" %>

<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>

<%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v19.2.Web, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <!--script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script-->
    
    <%--<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/font-awesome.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
    
    <script src="<%=Page.ResolveUrl("~/js/jquery-2.1.4.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    
    <style>
        .toast-pf-top-right-rel {
          left: 20px;
          position: relative;
          right: 20px;
          top: 12px;
          z-index: 1035;
          /* Medium devices (desktops, 992px and up) */
        }
        .tdFiltros{
            padding-top:5px;
            padding-bottom:5px;
            padding-left:5px;
            padding-right:5px;
            color: #0088d7
        }
        
        .tdFiltros2{
            padding-top:5px;
            padding-bottom:5px;
            padding-left:5px;
            padding-right:5px;
        
        }
         .tdDatosEncabezado2{
            text-align:center;
            padding-left:15px;
           padding-right:15px;
            background: #0088d7;
             box-shadow: 0 0px 0;
            border-style:solid;
           border-width:1px;
           border-bottom-width:.5px;
           border-color:#c5c7c9;
           color:white;
        }
         
         .tdDatosEncabezado2_Int{
            text-align:center;
            padding-left:15px;
           padding-right:15px;
             background: #f5c542;
             box-shadow: 0 0px 0;
             border-style:solid;
           border-width:1px;
           border-bottom-width:.5px;
           border-color:#c5c7c9;
     
        }
        .tdDatosEncabezado{
            text-align:center;
           border-style:solid;
           border-width:1px;
               border-top-width:.5px;
                border-bottom-width:.5px;
            padding-top:10px;
             padding-bottom:10px;
           border-color:#c5c7c9;
         
        }
         .tdDatosEncabezado_Nar{
                text-align:center;
                border-style:solid;
                border-width:1px;
                border-top-width:.5px;
                border-bottom-width:.5px;
                padding-top:10px;
                padding-bottom:10px;
                background-color:#fcecc0;
                border-color:#c5c7c9;
                font-weight:bold;
         
        }
        .tablaEncabezadoRik tr td
        {
            border-style:solid;
            border-width:1px;
            text-align:center;
            border-color:#c5c7c9;
        }
        .EncabezadosIntegralidad
        {
           background-color:#f5c542;
            color:white;
        }
       .EncabezadosIntegralidadAzules
        {
           background-color:#0088d7;
          color:white
          
        }
        .CeldasIntegralidad
        {
           background-color:#fcecc0;
        }
        .EncabezadoSolucionesPrimeros
        {
            background-color:#d4ecfa;
        }
        .divAreas2 {
            width: 200px;
            background-color: #0088d7;
            color:white;
            height:20px;
        }
    </style>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">

        <script>
            var postponedCallbackRequired = false;
            function openDivApps() {

            }
            function mesComboIndexChange() {
                modalInt.Show();
                modalInt.PerformCallback(-1);
            }
            function GetRowValues(value) {
                //alert(value);
                modalInt.Show();
                modalInt.PerformCallback(value);
            }
            function ShowModal() {
                //alert("entra Modl");
                modalInt.Show();
            }
            function btnMostrar_click(s, e) {
                if (CallbackPanel2.InCallback())
                    postponedCallbackRequired = true;
                else {
                    CallbackPanel2.PerformCallback();

                }

            }
            function BtnClick(s, e) {
                if (CallbackPanel.InCallback())
                    postponedCallbackRequired = true;
                else
                    CallbackPanel.PerformCallback();
            }
            function changeComboRiks(s, e) {
                if (CallbackPanel3.InCallback())
                    postponedCallbackRequired = true;
                else
                    CallbackPanel3.PerformCallback();
            }
            function OnEndCallback(s, e) {
                if (postponedCallbackRequired) {
                    CallbackPanel.PerformCallback();
                    postponedCallbackRequired = false;
                }
            }
            function OnEndCallback2(s, e) {
                if (postponedCallbackRequired) {
                    CallbackPanel2.PerformCallback();

                    postponedCallbackRequired = false;
                }
            }
            function OnEndCallback3(s, e) {
                if (postponedCallbackRequired) {
                    CallbackPanel3.PerformCallback();
                    postponedCallbackRequired = false;
                }
            }
        </script>



    <form id="form1" runat="server" style="font-family:Helvetica, Arial, sans-serif">
    <asp:ScriptManager runat="server" ID="smMainScriptManager">
        </asp:ScriptManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <div class="row" style="display: block;">

            <h3 style="display: inline-block;"><strong>Integralidad</strong></h3> 
        <p></p>

 <dx:ASPxPageControl SkinID="None" Width="100%" EnableViewState="true" ID="ASPxPageControl2" EnableTabScrolling="true"
        runat="server" ActiveTabIndex="0" TabSpacing="0px" CssClass="pcTemplates"
        EnableHierarchyRecreation="True" >
        <TabPages>
            <dx:TabPage Text="Integralidad" Name="Integralidad" >
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <p></p>


                        
               <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel3" ClientInstanceName="CallbackPanel3"
                    RenderMode="Table">
                    <ClientSideEvents EndCallback="OnEndCallback3"></ClientSideEvents>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <table>
                                  <tr>


                                      <td class="tdFiltros">
                                          <label> Representante: </label>
                                      </td>
                                      <td class="tdFiltros2">

                                   <dx:BootstrapComboBox ID="ddlComboRik" runat="server"
                                             Width="350px" IncrementalFilteringMode="Contains"  
                                            DropDownStyle="DropDownList" NullText="Seleccione un RIK" >

                                       <ClientSideEvents SelectedIndexChanged="changeComboRiks" />

                                   </dx:BootstrapComboBox>

                                      </td>


                                    <td class="tdFiltros">
                                          <label> Razón Social: </label>
                                      </td>
                                      <td class="tdFiltros2">
                                        <dx:BootstrapComboBox ID="ddlRazonSocialCliente" runat="server"
                                             Width="350px" IncrementalFilteringMode="Contains" 
                                            DropDownStyle="DropDownList" NullText="Seleccione una razon social" AllowNull="true"
                                            >
                                        </dx:BootstrapComboBox>
                                      </td>



                                 </tr>
                                 <tr>
                                     <td class="tdFiltros">
                                          <label> Segmento: </label>
                                      </td>
                                      <td class="tdFiltros2">

                                           <dx:BootstrapComboBox ID="ddlComboSegmentos" runat="server"
                                             Width="350px" IncrementalFilteringMode="Contains" 
                                            DropDownStyle="DropDownList" NullText="Seleccione un segmento" AllowNull="true" />


                                      </td>

                                       <td class="tdFiltros">
                                        
                                      </td>
                                      <td class="tdFiltros2" style="text-align:right">

                                       <dx:ASPxButton ID="btnBuscar" runat="server" AutoPostBack="False" 
                                                   GroupName="buscar" Text="Buscar" Width="100px" RenderMode = "Button">
                                             <ClientSideEvents Click="BtnClick"  />
                                       </dx:ASPxButton>

                                      </td>

                                  </tr>  

                            </table>
                            
                        <br />
                        <br />
                        </dx:PanelContent>
                     </PanelCollection>   
                 </dx:ASPxCallbackPanel>

               <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" Height="250px" ClientInstanceName="CallbackPanel"
                    RenderMode="Table" Width="1100px">
                    <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">

                        <div style="padding-left:10px">
                            <dx:ASPxGridView ID="dgListadoRiks" runat="server" OnPageIndexChanged="dgListadoRiks_PageIndexChanged" Border-BorderColor="#c5c7c9">
                                    <ClientSideEvents CustomButtonClick="function(s, e) {  
                                                     if(e.buttonID == 'Detalle'){  
                                                            GetRowValues(e.visibleIndex);
                                                            
                                                          //  s.GetRowValues(e.visibleIndex, 'Rik_Nombre;Cd_nombre', GetRowValues);  
                                                         //   modalInt.PerformCallback();                 
                                                         //   ShowModal() ;      
                                                        
                                                         }  
                                    }" />
                                <Styles  Header-Border-BorderColor="#c5c7c9"></Styles>
                            <Columns>
                                
                                <dx:GridViewDataTextColumn FieldName="Id_Rik"  Visible="false" Caption="RIK" />
                                <dx:GridViewDataTextColumn FieldName="Id_Seg"  Visible="false" Caption="RIK" />

                                 <dx:GridViewDataTextColumn FieldName="VPT"  Visible="false" Caption="VPT" />
                                 <dx:GridViewDataTextColumn FieldName="VPO"  Visible="false" Caption="RIK"  />
                               
                                 <dx:GridViewDataTextColumn FieldName="Seg_ValUniDim"  Visible="false" Caption="VPT" HeaderStyle-CssClass="EncabezadosIntegralidadAzules"  />

                                <dx:GridViewDataTextColumn FieldName="Rik_Nombre" Width="200px" Caption="RIK" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Cd_nombre" Width="150px" Caption="Nombre" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Id_Cte" Width="50px" Caption="Num. Cte" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Cte_NomComercial" Width="200px" Caption="Cliente" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Id_Ter" Width="100px" Caption="Territorio" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Seg_Descripcion" Width="100px" Caption="Segmento" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Cantidad"  Width="100px" Visible="true" Caption="Cantidad" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />
                                <dx:GridViewDataTextColumn FieldName="Seg_Unidades"  Width="100px" Visible="true" Caption="Unidad" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White" />

                                 <dx:GridViewDataTextColumn FieldName="Integralidad"  Width="75px" Visible="true" Caption="Integralidad vs Teo." CellStyle-CssClass="CeldasIntegralidad" >
                                         <DataItemTemplate>  
                                             <%# String.Format("{0:p}", Decimal.Parse(Eval("Integralidad").ToString()))  %>  
                                         </DataItemTemplate> 
                                        
                                        <HeaderStyle CssClass="EncabezadosIntegralidad" />
                                     

                                </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn FieldName="Integralidad_Obs"  Width="75px" Visible="true" Caption="Integralidad vs Obs." CellStyle-CssClass="CeldasIntegralidad" >
                                 <HeaderStyle CssClass="EncabezadosIntegralidad" />
                                    <DataItemTemplate>  
                                      <%# String.Format("{0:p}", Decimal.Parse(Eval("Integralidad_Obs").ToString()))  %>  
                                </DataItemTemplate>   

                                </dx:GridViewDataTextColumn>

                              <dx:GridViewCommandColumn  ButtonRenderMode="Image" Width="50px" Caption="  ">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Detalle">
                                        <Image ToolTip="VerDetalle" Url="../../Imagenes/view.png" Width="20px" Height="20px" />
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                               
                             </Columns>
                            </dx:ASPxGridView>
                            </div>
                        </dx:PanelContent>
                     </PanelCollection>   
                 </dx:ASPxCallbackPanel>
                    
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Generar Resultados" Name="GenerarRes" >

                <ContentCollection>


                    <dx:ContentControl runat="server" >
                       <div style="width:100%;text-align:right">
                            <dx:ASPxButton ID="btnExcel" runat="server" Image-Url="~/Imagenes/excel.gif" OnClick="btnExcel_Click"></dx:ASPxButton>
                      </div>

                        <h3 style="color:#0088d7;font-weight:bold">Grafico de Integralidad por Representantes</h3>
                        <hr />

                        <table width="1100px">
                            <tr>
                                <td style="vertical-align:top">
                                     <div style="width:400px;padding-left:30px">
                                        <label>&nbsp;&nbsp;Seleccione los representantes a comparar: </label>
                                        <br />

                                            <dx:ASPxCheckBoxList runat="server" ID="chkListaRiks" Width="365px">
                                                <Items>
                                                    <dx:ListEditItem Text="item 1" Value="1" />
                                                    <dx:ListEditItem Text="item 2" Value="2" />
                                                </Items>
                                            </dx:ASPxCheckBoxList>
                                                                    <br />
                                           <div style="text-align:right;width:100%">
                                             <dx:ASPxButton ID="btnMostrar" runat="server" AutoPostBack="False" 
                                                                       GroupName="Mostrar" Text="Mostrar" Width="100px" RenderMode = "button">
                                                                 <ClientSideEvents Click="btnMostrar_click"  />
                                             </dx:ASPxButton>
                                            </div>
                                        </div>
                                </td>
                                <td style="text-align:right">
                                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel2" Height="250px" ClientInstanceName="CallbackPanel2" RenderMode="Table" Width="600px">
                    <ClientSideEvents EndCallback="OnEndCallback2"></ClientSideEvents>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                               <div id="divGrafRepresentantes" style="padding-left:50px">
                       
                                   
                       
                                   
                    <dxg:WebChartControl ID="chartRiks" runat="server" Height="500px" Width="600px" RenderFormat="Svg" Visible="false"
                                     ClientInstanceName="chartRiks"
                                     AlternateText="">
                                    <SeriesSerializable>
                                        <dx:Series Name="Integralidad Teorica" ArgumentDataMember="Rik_Nombre" ValueDataMembersSerializable="Integralidad" SeriesPointsSorting="Ascending" SeriesPointsSortingKey="Value_1">
                                            <ViewSerializable>
                                                <dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
                                            </ViewSerializable>
                                                      <LabelSerializable>
                                                        <dx:SideBySideBarSeriesLabel TextPattern="{V:F2}">
                                                        </dx:SideBySideBarSeriesLabel>
                                                        
                                                    </LabelSerializable>

                                        </dx:Series>

                                         <dx:Series Name="Integralidad Observada" ArgumentDataMember="Rik_Nombre" ValueDataMembersSerializable="Integralidad_Obs"  >
                                            <ViewSerializable>
                                                <dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
                                            </ViewSerializable>
                                                  <LabelSerializable>
                                                        <dx:SideBySideBarSeriesLabel TextPattern="{V:F2}">
                                                        </dx:SideBySideBarSeriesLabel>
                                                    </LabelSerializable>
                                        </dx:Series>
                                       

                                    </SeriesSerializable>

                                    <BorderOptions Visibility="False" />
                                    <Titles>
                                        <dx:ChartTitle Alignment="Near" Text="" Font="Tahoma, 10pt"></dx:ChartTitle>
                                       
                                    </Titles>
                                    <DiagramSerializable>
                                        <dx:XYDiagram>
                                            <AxisX VisibleInPanesSerializable="-1">
                                                <Label Angle="-30"></Label>
                                            </AxisX>
                                            <AxisY Title-Text="% de Integralidad" Title-Visibility="True" VisibleInPanesSerializable="-1" Interlaced="True">
                                            </AxisY>
                                        </dx:XYDiagram>
                                    </DiagramSerializable>
                                </dxg:WebChartControl>
                               </div>
                         </dx:PanelContent>
                     </PanelCollection>   
                 </dx:ASPxCallbackPanel>
                                </td>
                            </tr>

                        </table>
                         


           

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>

        </TabPages>

        <Paddings Padding="0px" PaddingLeft="12px" />
        <ContentStyle Font-Names="Tahoma" Font-Overline="False" Font-Size="11px">
            <Paddings Padding="0px" />
            <Border BorderColor="#6DA0E7" BorderStyle="Solid" BorderWidth="1px" />
        </ContentStyle>
    </dx:ASPxPageControl>

    </div>  
    
             <dx:ASPxPopupControl ID="modalInt" runat="server" Width="1200" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="modalInt"
                HeaderText="Herramienta de Integralidad" AllowDragging="True" PopupAnimationType="Fade"  ScrollBars="Both"
                 LoadContentViaCallback="None" EnableViewState="true" OnWindowCallback ="modalInt_WindowCallback" Height="500px" Maximized="true"
                 >
                <ClientSideEvents  Shown=""  EndCallback="function(s, e) { $('#contentDiv').show(); }" CloseButtonClick="function(s, e) { $('#contentDiv').hide(); }"
                    Closing="function(s, e) {  
                    
                      }"
                    
                    />
                 <SettingsLoadingPanel Text="Cargando por favor espere..." />  
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div id="contentDiv" style="display:none;">
                        <dx:ASPxPanel ID="Panel1" runat="server">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    
                                    <h3 style="color:#0088d7;font-weight:bold">Datos del Representante</h3>
                              
                                    
                                    <table style="width:1200px" class="tablaEncabezadoRik">

                                        <tr class="EncabezadosIntegralidadAzules">
                                            <td>
                                                <label>RIK</label>
                                            </td>
                                             <td>
                                                <label>Cliente</label>
                                            </td>
                                            <td>
                                                <label>Territorio</label>
                                            </td>
                                            <td>
                                                <label>Segmento</label>
                                            </td>
                                                                                        <td>
                                                <label>Val Std.</label>

                                            </td>
                                              <td>
                                                <label>Cantidad</label>
                                            </td>
                                             <td>
                                                <label>Unidades</label>
                                            </td>

                                          </tr>
                                          <tr>

                                             <td>
                                                <dx:ASPxLabel runat="server" ID="lblRIK" ClientInstanceName="lblRIK" Width="200px"></dx:ASPxLabel>
                                            </td>
                                             <td>
                                               <dx:ASPxLabel runat="server" ID="lblCliente" ClientInstanceName="lblCliente" Width="200px"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel runat="server" ID="lblTerritorio" ClientInstanceName="lblTerritorio" Width="100px"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                               <dx:ASPxLabel runat="server" ID="lblSegmento" ClientInstanceName="lblSegmento" Width="200px"></dx:ASPxLabel>
                                            </td>
                                              
                                              <td>
                                                    <dx:ASPxLabel runat="server" ID="lblValStd" ClientInstanceName="lblValStd" Width="50px"></dx:ASPxLabel>
                                              </td>
                                              <td>
                                                    <dx:ASPxLabel runat="server" ID="lblCantidad" ClientInstanceName="lblCantidad" Width="50px"></dx:ASPxLabel>
                                              </td>
                                               <td>
                                                    <dx:ASPxLabel runat="server" ID="lblUnidades" ClientInstanceName="lblUnidades" Width="100px"></dx:ASPxLabel>
                                              </td>


                                          </tr>


                                          

                                    </table>


      <hr />
    
                        <h3 style="color:#0088d7;font-weight:bold">Historial de Integralidad</h3>


                                <table id="tblAreas">

                                <tr>
                                    <td class="tdDatosEncabezado2">

                                    </td>
                                    <td class="tdDatosEncabezado2">
                                        Mes
                                    </td>
  
                                    <td class="tdDatosEncabezado2">
                                        Venta (Est. de venta)   
                                    </td>
                                    <td class="tdDatosEncabezado2">
                                        VPT 
                                    </td>
                                                                     
                                    <td class="tdDatosEncabezado2">
                                        VPO  
                                    </td>
                                   <td class="tdDatosEncabezado2">
                                       Venta (Aplicacion - Proyecto)
                                    </td>

                                    <td class="tdDatosEncabezado2">
                                        GAP vs Teórico  
                                    </td>
                                     <td class="tdDatosEncabezado2">
                                        GAP vs Observado   
                                    </td>

                                    <td class="tdDatosEncabezado2_Int">
                                        Integralidad vs Teórico    
                                    </td>

                                     <td class="tdDatosEncabezado2_Int">
                                        Integralidad vs Observado    
                                    </td>
                                     <td class="tdDatosEncabezado2_Int">
                                        Oportunidad vs Teórico    
                                    </td>

                                     <td class="tdDatosEncabezado2_Int">
                                        Oportunidad vs Observado    
                                    </td>
                                </tr>

                  <asp:Repeater runat="server" ID="rptIntegralidadMes" OnItemDataBound="rptIntegralidadMes_ItemDataBound">
                        <ItemTemplate> 
                                <tr>
                                     <td class="tdDatosEncabezado2">
                                        <img src="../../Imagenes/plus_icon.png" style="cursor:pointer; width:14px;" onclick="
                                            $('.areas').eq(<%# Container.ItemIndex %>).slideToggle();
                                           
                                            
                                            if(this.src.includes('minus_icon'))
                                            {
                                                    this.src='../../Imagenes/plus_icon.png';
                                            }
                                            else
                                            {
                                                    this.src='../../Imagenes/minus_icon.png';
                                            }
                                        
                                            " />
                                    </td>
                                    <td class="tdDatosEncabezado2">
                                          <dx:ASPxLabel runat="server" ID="lblmes" Text='<%#Eval("Mes") %>'></dx:ASPxLabel> - 
                                          <dx:ASPxLabel runat="server" ID="lblAnio" Text='<%#Eval("Anio") %>'></dx:ASPxLabel>
                                    </td>
                                    <td class="tdDatosEncabezado">
                                        <dx:ASPxLabel runat="server" ID="lblVentaMes" ClientInstanceName="lblVentaMes" Width="100px" Text='<%# "$"+  Math.Round(Double.Parse(Eval("Venta").ToString()),2) %>'></dx:ASPxLabel>
                                    </td>
                                    <td class="tdDatosEncabezado">
                                      <dx:ASPxLabel runat="server" ID="lblVPTMes" ClientInstanceName="lblVPTMes" Width="100px"></dx:ASPxLabel>
                                    </td>
                                                                     
                                    <td class="tdDatosEncabezado">
                                       <dx:ASPxLabel runat="server" ID="lblVPOMes" ClientInstanceName="lblVPOMes" Width="100px"></dx:ASPxLabel>
                                    </td>
                                   <td class="tdDatosEncabezado">
                                        <dx:ASPxLabel runat="server" ID="lblVentaRealMes" ClientInstanceName="lblVentaRealMes" Width="100px"></dx:ASPxLabel>
                                    </td>

                                    <td class="tdDatosEncabezado">
                                       <dx:ASPxLabel runat="server" ID="lblGAP_Teorico" ClientInstanceName="lblGAP_Teorico" Width="100px"></dx:ASPxLabel>
                                    </td>
                                     <td class="tdDatosEncabezado">
                                        <dx:ASPxLabel runat="server" ID="lblGAP_Observado" ClientInstanceName="lblGAP_Observado" Width="100px"></dx:ASPxLabel>
                                    </td>

                                    <td class="tdDatosEncabezado_Nar">
                                        <dx:ASPxLabel runat="server" ID="lblIntegralidadTeorica" ClientInstanceName="lblIntegralidadTeorica" Width="100px"></dx:ASPxLabel>
                                    </td>

                                     <td class="tdDatosEncabezado_Nar">
                                       <dx:ASPxLabel runat="server" ID="lblIntegralidadObs" ClientInstanceName="lblIntegralidadObs" Width="100px"></dx:ASPxLabel>
                                    </td>


                                     <td class="tdDatosEncabezado_Nar">
                                        <dx:ASPxLabel runat="server" ID="lblOpTeorica" ClientInstanceName="lblOpTeorica" Width="100px"></dx:ASPxLabel>
                                    </td>

                                     <td class="tdDatosEncabezado_Nar">
                                       <dx:ASPxLabel runat="server" ID="lblOpObservada" ClientInstanceName="lblOpObservada" Width="100px"></dx:ASPxLabel>
                                    </td>
                                </tr>


                      
                           
                        
                    <tr>
                        <td></td>

                        <td colspan="11">

                                <div id="divAreas" class="areas" style="display: none;">

                               <asp:Repeater runat="server" ID="rptAreas" OnItemDataBound="rptAreas_ItemDataBound">
                                      <ItemTemplate> 


                                          <br />
                                          
                                          <div class="divAreas2">
                                          <b>Area: </b>
                                            <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text='<%#Eval("Id_Area") %>'></dx:ASPxLabel> - 
                                            <dx:ASPxLabel runat="server" ID="lbl1" Text='<%#Eval("Area_Descripcion") %>'></dx:ASPxLabel>
                                          </div>


                                          <br />
                                      

                                    <asp:Repeater runat="server" ID="rpTablaAplicaciones" OnItemDataBound="rpTablaAplicaciones_ItemDataBound">
                                        <ItemTemplate>  
                                           <div>
                                                <div class="divAreas2">
                                               <b>Solución: </b>
                                              
                                               <dx:ASPxLabel runat="server" ID="lblAplicacion" Text='<%#Eval("Sol_Descripcion") %>'></dx:ASPxLabel>
                                                </div>
                                                    <br />
                                                    <br />
                                                <div>
                                                 <dx:ASPxGridView ID="dgListadoAplicaciones" runat="server"  Width="850px">
                                                     
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="Id_Op" Width="100px" Caption="No. Proyecto" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules"  HeaderStyle-ForeColor="White" CellStyle-CssClass="EncabezadoSolucionesPrimeros"  />
                                                                <dx:GridViewDataTextColumn FieldName="Apl_Descripcion" Width="300px" Caption="Aplicación" HeaderStyle-CssClass="EncabezadosIntegralidadAzules"  HeaderStyle-ForeColor="White" CellStyle-CssClass="EncabezadoSolucionesPrimeros" />
                                                            
                                                                <dx:GridViewDataTextColumn FieldName="Apl_Potencial" Width="100px" Caption="%"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules"  HeaderStyle-ForeColor="White"/>
                                                                 <dx:GridViewDataTextColumn FieldName="VPT" Width="100px"   CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules"  HeaderStyle-ForeColor="White" >
                                                                     <DataItemTemplate>  
                                                                                         <%# String.Format("{0:c}", Decimal.Parse(Eval("VPT").ToString()))  %>  
                                                                    </DataItemTemplate>  
                                                                  </dx:GridViewDataTextColumn>

                                                                <dx:GridViewDataTextColumn FieldName="VPO" Width="100px"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White">
                                                                     <DataItemTemplate>  
                                                                                         <%# String.Format("{0:c}", Decimal.Parse(Eval("VPO").ToString()))  %>  
                                                                    </DataItemTemplate>  
                                                                  </dx:GridViewDataTextColumn>
  
                                                                <dx:GridViewDataTextColumn FieldName="Venta" Width="100px" Caption="Venta Real"   CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White">
                                                                     <DataItemTemplate>  
                                                                                         <%# String.Format("{0:c}", Decimal.Parse(Eval("Venta").ToString()))  %>  
                                                                    </DataItemTemplate>  
                                                                  </dx:GridViewDataTextColumn>
                                                                
                                                                <dx:GridViewDataTextColumn FieldName="GAP_Teorico" Width="100px" Caption="GAP Teórico"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White">
                                                                     <DataItemTemplate>  
                                                                                         <%# String.Format("{0:c}", Decimal.Parse(Eval("GAP_Teorico").ToString()))  %>  
                                                                    </DataItemTemplate>  
                                                                  </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="GAP_Observado" Width="100px" Caption="GAP Observado"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" HeaderStyle-ForeColor="White">
                                                                      <DataItemTemplate>  
                                                                                         <%# String.Format("{0:c}", Decimal.Parse(Eval("GAP_Observado").ToString()))  %>  
                                                                    </DataItemTemplate>  
                                                                  </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataHyperLinkColumn Width="200px" HeaderStyle-CssClass="EncabezadosIntegralidadAzules" >
                                                                    <DataItemTemplate>
                                                                         <a target="_blank" href="ProspectosV2.aspx?Id_Op=<%# Int32.Parse(lblCliente.Text.Split('-')[0].Trim())  %>&Id_Area=<%#Eval("Id_Area") %>&Id_Sol=<%#Eval("Id_Sol") %>"><%# Eval("Id_Op").ToString()=="0"?  "Nuevo proyecto":"" %> </a>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataHyperLinkColumn>


                               
                                                             </Columns>
                                                    </dx:ASPxGridView>
                                                </div> 
                                                   <br />
                                               <br />
                                           </div>
                                        </ItemTemplate>  
                                    </asp:Repeater>

                                </ItemTemplate> 
                              </asp:Repeater>

                          </div>

                        </td>

                    </tr>
                    

                        


                             </ItemTemplate> 
                           </asp:Repeater>

                     </table>
                                    <br />

                    
      <hr />
        
                        <h3 style="color:#0088d7;font-weight:bold">Gráfico de Integralidad por mes</h3>        
                         <div style="padding-left:100px;">
                                    <dxG:WebChartControl ID="chart" runat="server" Width="1000px"> 
                                                <SeriesSerializable>
                                                    <dx:Series Name="Int Val Teorico" LabelsVisibility="True">
                                                        <Points>
                                                         
                                                      
                                                        </Points>
                                                        <ViewSerializable>
                                                            <dx:LineSeriesView>
                                                                <LineMarkerOptions size="8"></LineMarkerOptions>
                                                            </dx:LineSeriesView>
                                                        </ViewSerializable>
                                                        <LabelSerializable>
                                                            <dx:PointSeriesLabel BackColor="Transparent" LineLength="5"
                                                                                LineVisibility="False" ResolveOverlappingMode="JustifyAllAroundPoint">
                                                                <Border Visibility="False" />
                                                            </dx:PointSeriesLabel>
                                                        </LabelSerializable>
                                                    </dx:Series>
                                                    
                                                  <dx:Series Name="Int Val Observado" LabelsVisibility="True">
                                                        <Points>

                                                        </Points>
                                                        <ViewSerializable>
                                                            <dx:LineSeriesView>
                                                                <LineMarkerOptions size="8"></LineMarkerOptions>
                                                            </dx:LineSeriesView>
                                                        </ViewSerializable>
                                                        <LabelSerializable>
                                                            <dx:PointSeriesLabel BackColor="Transparent" LineLength="5"
                                                                                LineVisibility="False" ResolveOverlappingMode="JustifyAllAroundPoint">
                                                                <Border Visibility="False" />
                                                            </dx:PointSeriesLabel>
                                                        </LabelSerializable>
                                                    </dx:Series>
                                                </SeriesSerializable>

                                      

                                                <DiagramSerializable>
                                                    <dx:XYDiagram>
                                                        <AxisX Interlaced="True" Title-Text="Periodo" VisibleInPanesSerializable="-1">
                                                            <WholeRange />
                                                            <GridLines visible="True"></GridLines>
                                                            <Label>
                                                            </Label>
                                                        </AxisX>
                                                        <AxisY Title-Text="% Integralidad" Title-Visibility="True" VisibleInPanesSerializable="-1">
                                                            <WholeRange/>
                                                            <Label>
                                                            </Label>
                                                        </AxisY>
                                                    </dx:XYDiagram>
                                                </DiagramSerializable>

                                    </dxG:WebChartControl>
                        </div>        
                                
 
                                   
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
     

                             </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>



   


    </form>



    




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    <!--Toast messages-->
    <!--Login dialog-->
    <div class="modal fade" id="dvDialogoInicioSesion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
           
        </div>
    </div>
    <!--Login dialog-->
    <script type="text/javascript">
</script>
            
    <!--script src="<%=Page.ResolveUrl("~/js/Modernizr-input.min.js")%>"></script-->
    <!--script src="<%=Page.ResolveUrl("~/js/jquery.placeholder.min.js")%>"></script-->

    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>

    <%--date format --%>
    <script src="<%=Page.ResolveUrl("~/js/date.format.js")%>"></script>
        
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/servicios/navegacion/Notificaciones.js") %>"></script>
    <uc:UCNotificaciones_js runat="server" ID="UCNotificaciones_js" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/ui/Notificaciones.js") %>"></script>
    <uc:UINotificaciones runat="server" ID="UINotificaciones1" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/navegacion/Notificaciones.js") %>"></script>

    <%--alertify--%>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
   
    <script src="<%=Page.ResolveUrl("~/js/placeholder-setup.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>                
    <!--script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script-->
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/MapaAplicaciones.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Informe.js")%>"></script>


       
</asp:Content>