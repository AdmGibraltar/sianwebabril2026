<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" 
    CodeBehind="dshReporteVentaDinamico.aspx.cs" Inherits="SIANWEB.Dashboard.dshReporteVentaDinamico" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<!-- Loading -->
<style>
    .loooading {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: white;
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 9999;
        transition: 1s all;
        opacity: 0.6;
    }
    .loooading.show {
        opacity:  0.6;
    }
    
    .loooading.hide {
        opacity: 0;
    }
    .loooading .spin {
        border: 10px solid hsla(185, 100%, 62%, 0.2);
        border-top-color: #3cefff;
        border-radius: 50%;
        width: 5em;
        height: 5em;
        animation: spin 1s linear infinite;
    }
    @keyframes spin {
        to {
        transform: rotate(360deg);
        }
    }            
</style>
<!-- Metro 4 -->    
<link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css" />
<style>
    .tableFixHead          { overflow: auto; height: 100px; }
    .tableFixHead thead th { position: sticky; top: 0; z-index: 1; background-color:white;  box-shadow: inset 1px 1px #000, 1px 1px #000;}

    * {
      box-sizing: border-box;
    }
    input[type=text] {
      background-color: #f1f1f1;
      width: 100%;
    }

/*the container must be positioned relative:*/
    .autocompleteee {
      position: relative;
      display: inline-block;
    }

    .autocompleteee-items {
      position: absolute;
      border: 1px solid #d4d4d4;
      border-bottom: none;
      border-top: none;
      z-index: 99;
      /*position the autocomplete items to be the same width as the container:*/
      top: 100%;
      left: 0;
      right: 0;
    }

    .autocompleteee-items div {
      padding: 10px;
      cursor: pointer;
      background-color: #fff; 
      border-bottom: 1px solid #d4d4d4; 
    }

    /*when hovering an item:*/
    .autocompleteee-items div:hover {
      background-color: #e9e9e9; 
    }

    /*when navigating through the items using the arrow keys:*/
    .autocompleteee-active {
      background-color: DodgerBlue !important; 
      color: #ffffff; 
    }

/* width */
    ::-webkit-scrollbar {
      width: 20px;
    }

    /* Track */
    ::-webkit-scrollbar-track {
      box-shadow: inset 0 0 5px grey; 
      border-radius: 10px;
    }
 
    /* Handle */
    ::-webkit-scrollbar-thumb {
      background: #3385ff; 
      border-radius: 10px;
    }

    /* Handle on hover */
    ::-webkit-scrollbar-thumb:hover {
      background: #3385ff; 
    }
</style>
<div class="loooading show" style=" visibility:hidden;" id="divWait" runat="server" >
    <div class="spin"></div>
</div>
<table style=" width: 95%; border-spacing:25px; column-width:5px">
    <tr>
        <td id="headderTD">
            <table>
                <tr>
                    <td colspan="9" align="right">
                        <table>
                            <tr>
                                <td>
                                    <p class="text-secondary"  id="parrCDI" runat="server" >CDI</p></td>
                                <td>
                                    <asp:DropDownList ID="drpCDI" runat="server" CssClass="text-secondary"
                                        Width="150px" AutoPostBack="true" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width:7%; vertical-align:top;">&nbsp;</td>
                    <td style="width:20%; vertical-align:central;">
                        <div class="icon-box border bd-default">
                            <div class="icon bg-cyan fg-white"><span class="mif-money" title="Acumulada del Año en Curso"></span></div>
                            <div class="content p-4">
                                <div class="text-upper">venta anual</div>
                                <div class="text-upper text-bold text-lead"><h3><%=TotalAcumulado%> </h3></div>
                            </div>
                        </div>
                    </td>
                    <td style="width:2%; vertical-align:top;">&nbsp;</td>
                                        <td style="width:20%; vertical-align:central;">
                        <div class="icon-box border bd-default">
                            <div class="icon bg-teal fg-white"><span class="mif-chart-pie" title="Trimestre en Curso [<%=strTrimestre%>]"></span></div>
                            <div class="content p-4">
                                <div class="text-upper">venta trimestral</div>
                                <div class="text-upper text-bold text-lead"><h3><%=TotalTrimestre%></h3></div>
                            </div>
                        </div>
                    </td>
                    <td style="width:2%; vertical-align:top;">&nbsp;</td>
                    <td style="width:20%; vertical-align:central;">
                        <div class="icon-box border bd-default">
                            <div class="icon bg-emerald fg-white"><span class="mif-calendar" title="Venta Neta del Mes de [<%=strEstemes %>]"></span></div>
                            <div class="content p-4">
                                <div class="text-upper">venta mes actual</div>
                                <div class="text-upper text-bold text-lead"><h3><%=VentaMes%></h3></div>
                            </div>
                        </div>
                    </td>
                    <td style="width:2%; vertical-align:top;">&nbsp;</td>
                    <td style="width:20%; vertical-align:central;">
                        <div class="icon-box border bd-default">
                            <div class="icon bg-cobalt fg-white"><span class="mif-cart" title="Acumulado de Unidades Vendida al Mes de [<%=strEstemes %>]"></span></div>
                            <div class="content p-4">
                                <div class="text-upper">unidades vendidas anual</div>
                                <div class="text-upper text-bold text-lead"><h3><%=TotalUnidades%></h3></div>
                            </div>
                        </div>
                    </td>
                    

                    
                    <td style="width:7%; vertical-align:top;">&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <!-- Separador -->
    <tr><td colspan="7"><h5>&nbsp;<asp:Label ID="lblMensaje" runat="server"></asp:Label>&nbsp;</h5></td></tr>
    <tr>
        <!--  Filtros y Opciones  -->
        <td id="OptionsTD">
            <table>
                <tr>
                    <td style="width:10px;">&nbsp;</td>
                    <td>
                        <ul class="h-menu">
                            <li><a href="#" onclick="ClickCliente();" id="mnuopcCte" style="font-variant-caps:small-caps;" ><span class="mif-user icon fg-cyan"></span>&nbsp;Cliente</a></li>
                            <li><a href="#" onclick="ClickProducto();" id="mnuopcPrd" style="font-variant-caps:small-caps;" ><span class="mif-cart icon fg-cyan"></span>&nbsp;Producto</a></li>
                            <li><a href="#" onclick="ClickTerritorio();" id="mnuopcTerr" style="font-variant-caps:small-caps;" ><span class="mif-apps icon fg-cyan"></span>&nbsp;Territorio</a></li>
                            <li><a href="#" onclick="ClickRepresentante();" id="mnuopcRep" style="font-variant-caps:small-caps;" ><span class="mif-suitcase icon fg-cyan"></span>&nbsp;Representante</a></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td style="width:10px;">&nbsp;</td>
                    <td>
                        <table>
                            <tr>
                                <td style="width:50px; vertical-align:middle;" rowspan="2">
                                    <img alt="Actualizar" src="../Images/imgAplicarFiltro.png" width="35" height="35" onclick="Actualiza();" style="cursor: pointer"/>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width:5px;">&nbsp;</td>
                                <td style=" white-space:nowrap ">
                                    <table>
                                        <tr>
                                            <td  style=" white-space:nowrap ">
                                                <p class="text-secondary" id="lblDXProducto" runat="server">
                                                    <input type="checkbox" id="chkDXProducto0" name="chkDXProducto0"
                                                        onclick="Javascript:ActivaChkProducto();"
                                                        class="btn-custom" runat="server"/>
                                                &nbsp;Por Producto</p>
                                                <!--    OnCheckedChanged="chkDXProducto_CheckedChanged" AutoPostBack="true"  -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" white-space:nowrap ">
                                                <p class="text-secondary" id="lblDXCliente" runat="server">
                                                    <input type="checkbox" id="chkDXCliente0" name="chkDXCliente0"
                                                        onclick="Javascript:ActivachkCliente();"
                                                        class="btn-custom" runat="server"/>
                                                &nbsp;Por Cliente</p>
                                                <!--    OnCheckedChanged="chkDXCliente_CheckedChanged" AutoPostBack="true"   -->
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:5px;">&nbsp;</td>
                                <td style="width:10px; vertical-align:top; border-left:solid thin #009CD9">&nbsp;</td>
                                <td style=" white-space:nowrap ">
                                    <table>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P3" runat="server">
                                                    <input type="radio" id="radMonto" name="radIndicador"
                                                        onclick="Javascript:ActivaUtil();" checked="true"
                                                        class="btn-custom" runat="server" value="1" />
                                                &nbsp;Pesos</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P4" runat="server">
                                                    <input type="radio" id="radUnidades" name="radIndicador"
                                                         onclick="Javascript:ActivaUtil();"
                                                        class="btn-custom" runat="server" value="2" />
                                                &nbsp;Unidades</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P5" runat="server">
                                                    <input type="radio" id="radAmbos" name="radIndicador"
                                                        onclick="Javascript:ActivaUtil();"
                                                        class="btn-custom" runat="server" value="0" />
                                                &nbsp;Pesos y Unidades</p>
                                            </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P6" runat="server">
                                                <asp:checkbox name="chkIncluyeUtilidad" id="chkIncluyeUtilidad" runat="server" CssClass="custom-checkbox" 
                                                    Text=""  />&nbsp;Incluir Utilidad Prima</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:5px;">&nbsp;</td>
                                <td style="width:10px; vertical-align:top; border-left:solid thin #009CD9"">&nbsp;</td>
                                <td style=" white-space:nowrap ">
                                    <table>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P1" runat="server">
                                                <asp:RadioButton name="rdbXRIK" id="rdbXRIK" runat="server" CssClass="btn-custom" 
                                                    GroupName="grpTerr" AutoPostBack="true" OnCheckedChanged="rdbXRIK_CheckedChanged" />&nbsp;RIK/Territorio de Venta</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p class="text-secondary" id="P2" runat="server">
                                                <asp:RadioButton name="rdbXRSC" id="rdbXRSC" runat="server" CssClass="btn-custom" 
                                                    GroupName="grpTerr" AutoPostBack="true" OnCheckedChanged="rdbXRSC_CheckedChanged"  />&nbsp;RSC/Territorio de Servicio</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:5px;">&nbsp;</td>
                                <td style="width:10px; vertical-align:top; border-left:solid thin #009CD9"">&nbsp;</td>
                                <td style=" white-space:nowrap ">
                                    <table class="table subcompact">
                                        <tr>
                                            <td style=" white-space:nowrap ">
                                                <p class="text-secondary" > Producto :</p>
                                            </td>
                                            <td style="width:5px;">&nbsp;</td>
                                            <td>
                                                <div class="autocompleteee">
                                                    <input type="text" style="width:250px"
                                                        id="lblFilXProducto" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" white-space:nowrap ">
                                                <p class="text-secondary" > Cliente :</p>
                                            </td>
                                            <td style="width:5px;">&nbsp;</td>
                                            <td>
                                                <div class="autocompleteee">
                                                    <input type="text" style="width:250px;" 
                                                        id="lblFilXCliente" runat="server"  />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" white-space:nowrap ">
                                                <p class="text-secondary" > Territorio :</p>
                                            </td>
                                            <td style="width:5px;">&nbsp;</td>
                                            <td>
                                                <div class="autocompleteee">
                                                    <input type="text" style="width:250px;"
                                                        id="lblFilXTerritorio" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:5px;">&nbsp;</td>
                                <td style="width:10px; vertical-align:top; border-left:solid thin #009CD9"">&nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <!--
                                            <td>
                                                <p class="text-secondary" > Mes :</p>
                                            </td>
                                            <td style="width:5px;">&nbsp;</td>
                                            <td>
                                                <select id="cmbMes" runat="server" class="drop-container">
                                                    <option value="1" >Enero</option>
                                                    <option value="2" >Febrero</option>
                                                    <option value="3" >Marzo</option>
                                                    <option value="4" >Abril</option>
                                                    <option value="5" >Mayo</option>
                                                    <option value="6" >Junio</option>
                                                    <option value="7" >Julio</option>
                                                    <option value="8" selected="selected" >Agosto</option>
                                                    <option value="9" >Septiembre</option>
                                                    <option value="10" >Octubre</option>
                                                    <option value="11" >Noviembre</option>
                                                    <option value="12" >Diciembre</option>
                                                </select>
                                            </td>
                                            <td style="width:15px;">&nbsp;</td>
                                            -->
                                            <td>
                                                <p class="text-secondary" > Año :</p>
                                            </td>
                                            <td style="width:5px;">&nbsp;</td>
                                            <td>
                                                <asp:DropDownList ID="cmbAniio" runat="server" CssClass="drop-container"/>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                                <td style="width:5px;">&nbsp;</td>
                                <td style="width:10px; vertical-align:top; border-left:solid thin #009CD9"">&nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="2" class="text-secondary" >
                                                    <input type="checkbox" id="chkReporteSemanal" name="chkReporteSemanal" data-caption="Venta Semanal"
                                                        data-role="checkbox" runat="server" onchange="ClickReporteSemanal();" /> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20px;">&nbsp;</td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <p class="text-secondary" > Año :</p>
                                                        </td>
                                                        <td style="width:5px;">&nbsp;</td>
                                                        <td>
                                                            <select id="cmbAniosSemana" runat="server" class="drop-container" disabled="disabled" />
                                                        </td>
                                                        <td style="width:15px;">&nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnExcelReporteSemana" runat="server"
                                                                OnClick="btnExcelReporteSemana_Click" OnClientClick="JavaScript:BajarExcelSemana();"
                                                                CssClass="button primary small rounded" Text="Bajar Archivo Excel" />
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30px;">&nbsp;</td>
                                            <td>
                                                <p class="text-secondary" id="lblIncMov80" runat="server">
                                                <asp:checkbox name="chkIncMov80" id="chkIncMov80" runat="server" CssClass="custom-checkbox" Enabled="false"
                                                    Text="" />&nbsp;Considerar ventas con remisiones de tipo Mov 80</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:5px;">&nbsp;
                                    <asp:HiddenField ID="hdfopcMenu" runat="server" />
                                    <asp:HiddenField ID="hdfTipoIndicador" runat="server" />
                                    
                                    <asp:HiddenField ID="hdfdsgProducto" runat="server" />
                                    <asp:HiddenField ID="hdfdsgCliente" runat="server" />

                                    <asp:HiddenField ID="hdfFiltroProducto" runat="server" />
                                    <asp:HiddenField ID="hdfFiltroCliente" runat="server" />
                                    <asp:HiddenField ID="hdfFiltroTerritorio" runat="server" />
                                    <div style=" visibility:hidden">
                                        <asp:Button ID="btnActualiza" runat="server" OnClick="btnActualiza_Click" Width="10" />

                                        <asp:checkbox name="chkRptXSemana" id="chkRptXSemana" runat="server" CssClass="custom-checkbox" Text="" />

                                        <asp:checkbox name="chkDXProducto" id="chkDXProducto" runat="server" CssClass="custom-checkbox" Text="" />
                                        <asp:checkbox name="chkDXCliente" id="chkDXCliente" runat="server" CssClass="custom-checkbox" Text=""  />
                                        <button class="button primary small rounded" id="btnExcelSemana" style="width:10px"
                                            runat="server" onclick="JavaScript:BajarExcelSemana();" disabled="disabled">
                                            Bajar Archivo Excel &nbsp;&nbsp;<span class="mif-file-excel"></span>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <!-- Separador -->
    <tr><td colspan="7"><h5>&nbsp;&nbsp;</h5></td></tr>
    <tr><td colspan="7" id="RptNameTD" runat="server" >
        <table >
            <tr>
                <td style="width:35px;">&nbsp;</td>
                <td><p style="color:blue;"><%=strTituloDelReporte %></p></td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>
        <!--  Reporte -->
        <td id="DataTD" runat="server" >
            <table>
                <tr>
                    <td style="width:50px; vertical-align:top; "  rowspan="2" colspan="2">&nbsp;
                        <asp:ImageButton runat="server" ID="btnExcelGrafica" ImageUrl="../Images/imgBajarExcel.png" 
                            AlternateText="Descargar" width = "35" height = "35" CssClass = "rpImage"
                             OnClientClick="BajaExcel();" OnClick="btnExcelGrafica_Click"/>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width:10px;">&nbsp;</td>
                    <td >
                        <div style="width: 1500px; height: 350px; overflow-y: scroll;" class="tableFixHead">
                            <table class="table subcompact" id="tblReporteDinamico" >
                                <!-- style="visibility:collapse;" -->
                                <thead>
                                    <%=strHeaderRpt %>
                                    <!--
                                    <tr>
                                        <th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>
                                        <th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >
                                            Núm.
                                        </th>
                                        <th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>
                                            Descripción
                                        </th>
                                        <th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>
                                            1er Trimestre
                                        </th>                            
                                        <th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>
                                            2do Trimestre
                                        </th>                            
                                        <th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>
                                            3er Trimestre
                                        </th>
                                        <th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>
                                            4to Trimestre
                                        </th>
                                        <th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>
                                            Total
                                        </th>                            
                                        <th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>
                                            Total Unidades
                                        </th>
                                    </tr>
                                    <tr>
                                        <th id='hcEneroM' class='text-center border border-size-2 bd-black'>
                                            Enero
                                        </th>
                                        <th id='hcEneroU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Enero
                                        </th>
                                        <th id='hcFebreroM' class='text-center border border-size-2 bd-black'>
                                            Febrero
                                        </th>
                                        <th id='hcFebreroU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Febrero
                                        </th>
                                        <th id='hcMarzoM' class='text-center border border-size-2 bd-black'>
                                            Marzo
                                        </th>
                                        <th id='hcMarzoU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Marzo
                                        </th>
                                        <th id='hcAbrilM' class='text-center border border-size-2 bd-black'>
                                            Abril
                                        </th>
                                        <th id='hcAbrilU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Abril
                                        </th>
                                        <th id='hcMayoM' class='text-center border border-size-2 bd-black'>
                                            Mayo
                                        </th>
                                        <th id='hcMayoU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Mayo
                                        </th>
                                        <th id='hcJunioM' class='text-center border border-size-2 bd-black'>
                                            Junio
                                        </th>
                                        <th id='hcJunioU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Junio
                                        </th>
                                        <th id='hcJulioM' class='text-center border border-size-2 bd-black'>
                                            Julio
                                        </th>
                                        <th id='hcJulioU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Julio
                                        </th>
                                        <th id='hcAgostoM' class='text-center border border-size-2 bd-black'>
                                            Agosto
                                        </th>
                                        <th id='hcAgostoU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Agosto
                                        </th>
                                        <th id='hcSeptiembreM' class='text-center border border-size-2 bd-black'>
                                            Septiembre
                                        </th>
                                        <th id='hcSeptiembreU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Septiembre
                                        </th>
                                        <th id='hcOctubreM' class='text-center border border-size-2 bd-black'>
                                            Octubre
                                        </th>
                                        <th id='hcOctubreU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Octubre
                                        </th>
                                        <th id='hcNoviembreM' class='text-center border border-size-2 bd-black'>
                                            Noviembre
                                        </th>
                                        <th id='hcNoviembreU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Noviembre
                                        </th>
                                        <th id='hcDiciembreM' class='text-center border border-size-2 bd-black'>
                                            Diciembre
                                        </th>
                                        <th id='hcDiciembreU' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>
                                            U Diciembre
                                        </th>
                                    </tr>
                                -->
                                </thead>
                                <tbody>
                                    <%=strTablaRpt %>    
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

</table>
<script>
    var opcion = document.getElementById("<%=hdfopcMenu.ClientID%>");

    function ActivaChkProducto() {

        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
        chkPrd.checked = chkPrd0.checked;
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        if ((rdbUnidades.checked) || (chkPrd0.checked)) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        }
        else {
            chkUtilidad.disabled = false;
        }
    }

    function ActivaChkCliente() {
        var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
        var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
        chkCte.checked = chkCte0.checked;
    }

    function ClickReporteSemanal() {
        var chkSemanal = document.getElementById("<%=chkReporteSemanal.ClientID%>");
        

        if (chkSemanal.checked) {
            /// Apaga todo por ser el reporte semanal
            var TDRpt = document.getElementById("<%=DataTD.ClientID%>");
            TDRpt.style.visibility = "hidden";
            var TDRptName = document.getElementById("<%=RptNameTD.ClientID%>");
            TDRptName.style.visibility = "hidden";

            var cmbSemanal = document.getElementById("<%=cmbAniosSemana.ClientID%>");
            cmbSemanal.disabled = false;
            var chkMov80 = document.getElementById("<%=chkIncMov80.ClientID%>");
            chkMov80.disabled = false;
            var btnExc = document.getElementById("<%=btnExcelReporteSemana.ClientID%>");
            btnExc.disabled = false;

            var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
            chkCte.disabled = true;
            chkCte.checked = false
            var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
            chkPrd.disabled = true;
            chkPrd.checked = false;

            var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
            chkCte0.disabled = true;
            chkCte0.checked = false;
            var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
            chkPrd0.disabled = true;
            chkPrd0.checked = false;

            var rdbRIK = document.getElementById("<%=rdbXRIK.ClientID%>");
            rdbRIK.disabled = true;
            rdbRIK.checked = false;
            var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
            rdbRSC.disabled = true;
            rdbRSC.checked = false;

            var filCte = document.getElementById("<%=lblFilXCliente.ClientID%>");
            filCte.disabled = true;
            filCte.value = "";
            var filProd = document.getElementById("<%=lblFilXProducto.ClientID%>");
            filProd.disabled = true;
            filProd.value = "";
            var filTerr = document.getElementById("<%=lblFilXTerritorio.ClientID%>");
            filTerr.disabled = true;
            filTerr.value = "";

            var cmbAnio1 = document.getElementById("<%=cmbAniio.ClientID%>");
            cmbAnio1.disabled = true;

            var chkXSemana = document.getElementById("<%=chkRptXSemana.ClientID%>");
            chkXSemana.checked = true;
        }
        else {
            var TDRpt = document.getElementById("<%=DataTD.ClientID%>");
            TDRpt.style.visibility = "visible";
            var TDRptName = document.getElementById("<%=RptNameTD.ClientID%>");
            TDRptName.style.visibility = "visible";

            var cmbAnio2 = document.getElementById("<%=cmbAniio.ClientID%>");
            cmbAnio2.disabled = false;

            var chkXSemana2 = document.getElementById("<%=chkRptXSemana.ClientID%>");
            chkXSemana2.checked = false;

            var cmbSemanal = document.getElementById("<%=cmbAniosSemana.ClientID%>");
            cmbSemanal.disabled = true;
            var chkMov80 = document.getElementById("<%=chkIncMov80.ClientID%>");
            chkMov80.disabled = true;
            var btnEx2c = document.getElementById("<%=btnExcelReporteSemana.ClientID%>");
            btnEx2c.disabled = true;

            if (opcion.value == "1") {
                ClickCliente();
            }
            if (opcion.value == "10") {
                ClickProducto();
            }
            if ((opcion.value == "20") || (opcion.value == "30")) {
                ClickTerritorio();
            }
            if ((opcion.value == "40") || (opcion.value == "50")) {
                ClickRepresentante();
            }
        }
    }

    function BajarExcelSemana() {
        var loooadiiing02 = document.getElementById("<%=divWait.ClientID%>");
        loooadiiing02.style.visibility = "visible";

        //  var e = document.getElementById("< %=btnExcelReporteSemana.ClientID%>");
        //  e.click();
        
        setInterval(function () {
            var loooadiiin0g4 = document.getElementById("<%=divWait.ClientID%>");
            loooadiiin0g4.style.visibility = "hidden";
        }, 8000);
    }

    function ActivaUtil() {
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        if ((rdbUnidades.checked) || (chkPrd0.checked)) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        }
        else {
            chkUtilidad.disabled = false;
        }
    }

    function limpiar1() {
        document.getElementById("<%=hdfFiltroCliente.ClientID%>").value = "";
    }
    function limpiar2() {
        document.getElementById("<%=hdfFiltroProducto.ClientID%>").value = "";
    }

    function ClickCliente() {
        opcion.value = "1";
        //  hdfTipoIndicador

        var rdbPesos = document.getElementById("<%=radMonto.ClientID%>");
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var rdbAmbos = document.getElementById("<%=radAmbos.ClientID%>");
        if ((rdbPesos.checked == false) && (rdbUnidades.checked == false) && (rdbAmbos.checked == false)) {
            rdbPesos.checked = true;
        }
        
        var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
        chkCte.disabled = true;
        chkCte.checked = false;
        var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
        chkPrd.disabled = false;

        var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
        chkCte0.disabled = true;
        chkCte0.checked = false;
        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        chkPrd0.disabled = false;
        chkPrd0.checked = false;

        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        if ((rdbUnidades.checked) || (chkPrd0.checked) ) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        }
        else {
            chkUtilidad.disabled = false;
        }

        var rdbRIK = document.getElementById("<%=rdbXRIK.ClientID%>");
        rdbRIK.disabled = true;
        rdbRIK.checked = false;
        var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
        rdbRSC.disabled = true;
        rdbRSC.checked = false;

        var filCte = document.getElementById("<%=lblFilXCliente.ClientID%>");
        filCte.disabled = false;
        var filTerr = document.getElementById("<%=lblFilXTerritorio.ClientID%>");
        filTerr.disabled = true;
        var filProd = document.getElementById("<%=lblFilXProducto.ClientID%>");
        filProd.disabled = true;

        if (chkPrd.checked) {
            filProd.disabled = false;
        }
        document.getElementById("mnuopcCte").style.color = "Black";
        //  document.getElementById("mnuopcCte").style.fontVariantCaps = "normal";
        document.getElementById("mnuopcPrd").style.color = "Blue";
        //  document.getElementById("mnuopcPrd").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcTerr").style.color = "Blue";
        //  document.getElementById("mnuopcTerr").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcRep").style.color = "Blue";
        //  document.getElementById("mnuopcRep").style.fontVariantCaps = "small-caps";
    }

    function ClickProducto() {
        opcion.value = "10";

        var rdbPesos = document.getElementById("<%=radMonto.ClientID%>");
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var rdbAmbos = document.getElementById("<%=radAmbos.ClientID%>");
        if ((rdbPesos.checked == false) && (rdbUnidades.checked == false) && (rdbAmbos.checked == false)) {
            rdbPesos.checked = true;
        }
        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        /// if (rdbUnidades.checked) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        /// }

        var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
        chkCte.disabled = true;
        chkCte.checked = false;
        var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
        chkPrd.disabled = true;
        chkCte.checked = false;

        var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
        chkCte0.disabled = true;
        chkCte0.checked = false;
        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        chkPrd0.disabled = true;
        chkPrd0.checked = false;

        var rdbRIK = document.getElementById("<%=rdbXRIK.ClientID%>");
        rdbRIK.disabled = true;
        rdbRIK.checked = false;
        var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
        rdbRSC.disabled = true;
        rdbRSC.checked = false;

        var filCte = document.getElementById("<%=lblFilXCliente.ClientID%>");
        filCte.disabled = true;
        var filProd = document.getElementById("<%=lblFilXProducto.ClientID%>");
        filProd.disabled = false;
        var filTerr = document.getElementById("<%=lblFilXTerritorio.ClientID%>");
        filTerr.disabled = true;

        document.getElementById("mnuopcCte").style.color = "Blue";
        //  document.getElementById("mnuopcCte").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcPrd").style.color = "Black";
        //  document.getElementById("mnuopcPrd").style.fontVariantCaps = "normal";
        document.getElementById("mnuopcTerr").style.color = "Blue";
        //  document.getElementById("mnuopcTerr").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcRep").style.color = "Blue";
        //  document.getElementById("mnuopcRep").style.fontVariantCaps = "small-caps";
    }

    function ClickTerritorio() {
        var rdbRIK = document.getElementById("<%=rdbXRIK.ClientID%>");
        var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
        opcion.value = "20";

        var rdbPesos = document.getElementById("<%=radMonto.ClientID%>");
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var rdbAmbos = document.getElementById("<%=radAmbos.ClientID%>");
        if ((rdbPesos.checked == false) && (rdbUnidades.checked == false) && (rdbAmbos.checked == false)) {
            rdbPesos.checked = true;
        }
        
        if (rdbRSC.checked) {
            opcion.value = "30";
        }
        var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
        chkCte.disabled = false;
        var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
        chkPrd.disabled = false;

        var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
        chkCte0.disabled = false;
        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        chkPrd0.disabled = false;

        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        if ((rdbUnidades.checked) || (chkPrd0.checked)) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        }
        else {
            chkUtilidad.disabled = false;
        }

        rdbRIK.disabled = false;
        if (opcion.value == "20") {
            rdbRIK.checked = true;
        }
        
        rdbRSC.disabled = false;

        var filCte = document.getElementById("<%=lblFilXCliente.ClientID%>");
        filCte.disabled = true;
        var filTerr = document.getElementById("<%=lblFilXTerritorio.ClientID%>");
        filTerr.disabled = false;
        var filProd = document.getElementById("<%=lblFilXProducto.ClientID%>");
        filProd.disabled = true;

        if (chkCte.checked) {
            filCte.disabled = false;
        }
        if (chkPrd.checked) {
            filProd.disabled = false;
        }
        document.getElementById("mnuopcCte").style.color = "Blue";
        //  document.getElementById("mnuopcCte").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcPrd").style.color = "Blue";
        //  document.getElementById("mnuopcPrd").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcTerr").style.color = "Black";
        //  document.getElementById("mnuopcTerr").style.fontVariantCaps = "normal";
        document.getElementById("mnuopcRep").style.color = "Blue";
        //  document.getElementById("mnuopcRep").style.fontVariantCaps = "small-caps";
    }

    function ClickRepresentante() {
        var rdbRIK = document.getElementById("<%=rdbXRIK.ClientID%>");
        var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
        opcion.value = "40";
        if (rdbRSC.checked) {
            opcion.value = "50";
        }

        var rdbPesos = document.getElementById("<%=radMonto.ClientID%>");
        var rdbUnidades = document.getElementById("<%=radUnidades.ClientID%>");
        var rdbAmbos = document.getElementById("<%=radAmbos.ClientID%>");
        if ((rdbPesos.checked == false) && (rdbUnidades.checked == false) && (rdbAmbos.checked == false)) {
            rdbPesos.checked = true;
        }
        
        var chkCte = document.getElementById("<%=chkDXCliente.ClientID%>");
        chkCte.disabled = false;
        var chkPrd = document.getElementById("<%=chkDXProducto.ClientID%>");
        chkPrd.disabled = false;

        var chkCte0 = document.getElementById("<%=chkDXCliente0.ClientID%>");
        chkCte0.disabled = false;
        var chkPrd0 = document.getElementById("<%=chkDXProducto0.ClientID%>");
        chkPrd0.disabled = false;

        var chkUtilidad = document.getElementById("<%=chkIncluyeUtilidad.ClientID%>");
        if ((rdbUnidades.checked) || (chkPrd0.checked)) {
            chkUtilidad.disabled = true;
            chkUtilidad.checked = false;
        }
        else {
            chkUtilidad.disabled = false;
        }

        rdbRIK.disabled = false;
        if (opcion.value == "40") {
            rdbRIK.checked = true;
        }
        
        /// var rdbRSC = document.getElementById("<%=rdbXRSC.ClientID%>");
        rdbRSC.disabled = false;

        var filCte = document.getElementById("<%=lblFilXCliente.ClientID%>");
        filCte.disabled = true;
        var filTerr = document.getElementById("<%=lblFilXTerritorio.ClientID%>");
        filTerr.disabled = false;
        var filProd = document.getElementById("<%=lblFilXProducto.ClientID%>");
        filProd.disabled = true;

        if (chkCte.checked) {
            filCte.disabled = false;
        }
        if (chkPrd.checked) {
            filProd.disabled = false;
        }
        document.getElementById("mnuopcCte").style.color = "Blue";
        //  document.getElementById("mnuopcCte").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcPrd").style.color = "Blue";
        //  document.getElementById("mnuopcPrd").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcTerr").style.color = "Blue";
        //  document.getElementById("mnuopcTerr").style.fontVariantCaps = "small-caps";
        document.getElementById("mnuopcRep").style.color = "Black";
        //  document.getElementById("mnuopcRep").style.fontVariantCaps = "normal";
    }

    function Actualiza() {
        var loooadiiing = document.getElementById("<%=divWait.ClientID%>");
        loooadiiing.style.visibility = "visible";
        
        var e = document.getElementById("<%=btnActualiza.ClientID%>");
        e.click();
    }

    function BajaExcel() {
        var loooadiiing2 = document.getElementById("<%=divWait.ClientID%>");
        loooadiiing2.style.visibility = "visible";

        setInterval(function () {
            var loooadiiing4 = document.getElementById("<%=divWait.ClientID%>");
            loooadiiing4.style.visibility = "hidden";    
        }, 10000);
    }
        
    if (opcion.value == "1") {
        ClickCliente();
    }
    if (opcion.value == "10") {
        ClickProducto();
    }
    if ((opcion.value == "20") || (opcion.value == "30")) {
        ClickTerritorio();
    }
    if ((opcion.value == "40") || (opcion.value == "50")) {
        ClickRepresentante();
    }
    ClickReporteSemanal();
</script>
<script>
    function autocompleteee(inp, hddn, arr) {
        /*the autocomplete function takes two arguments,
        the text field element and an array of possible autocompleted values:*/
        var currentFocus;
        /*execute a function when someone writes in the text field:*/
        inp.addEventListener("input", function (e) {
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocompleteee-list");
            a.setAttribute("class", "autocompleteee-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*check if the item starts with the same letters as the text field value:*/
                if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                    /*create a DIV element for each matching element:*/
                    b = document.createElement("DIV");
                    /*make the matching letters bold:*/
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    /*insert a input field that will hold the current array item's value:*/
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                    /*execute a function when someone clicks on the item value (DIV element):*/
                    b.addEventListener("click", function (e) {
                        /*insert the value for the autocomplete text field:*/
                        inp.value = this.getElementsByTagName("input")[0].value;
                        hddn.value = inp.value;
                        /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        /*execute a function presses a key on the keyboard:*/
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocompleteee-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode == 40) {
                /*If the arrow DOWN key is pressed,
                increase the currentFocus variable:*/
                currentFocus++;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 38) { //up
                /*If the arrow UP key is pressed,
                decrease the currentFocus variable:*/
                currentFocus--;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 13) {
                /*If the ENTER key is pressed, prevent the form from being submitted,*/
                e.preventDefault();
                if (currentFocus > -1) {
                    /*and simulate a click on the "active" item:*/
                    if (x) x[currentFocus].click();
                }
            }
        });
        function addActive(x) {
            /*a function to classify an item as "active":*/
            if (!x) return false;
            /*start by removing the "active" class on all items:*/
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            /*add class "autocomplete-active":*/
            x[currentFocus].classList.add("autocompleteee-active");
        }
        function removeActive(x) {
            /*a function to remove the "active" class from all autocomplete items:*/
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocompleteee-active");
            }
        }
        function closeAllLists(elmnt) {
            /*close all autocomplete lists in the document,
            except the one passed as an argument:*/
            var x = document.getElementsByClassName("autocompleteee-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        /*execute a function when someone clicks in the document:*/
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }

    var listadodeclientes = [<%=strListadoClientes%>];
    var listadodeproductos = [<%=strListadoProductos%>];
    var listadodeterritorio = [<%=strListadoAgrupador%>];

    autocompleteee(document.getElementById("<%=lblFilXCliente.ClientID%>"), document.getElementById("<%=hdfFiltroCliente.ClientID%>"), listadodeclientes);
    autocompleteee(document.getElementById("<%=lblFilXProducto.ClientID%>"), document.getElementById("<%=hdfFiltroProducto.ClientID%>"), listadodeproductos);
    autocompleteee(document.getElementById("<%=lblFilXTerritorio.ClientID%>"), document.getElementById("<%=hdfFiltroTerritorio.ClientID%>"), listadodeterritorio);
</script>

<!-- Metro 4 -->
<script src="https://cdn.metroui.org.ua/v4.3.2/js/metro.min.js"></script>


</asp:Content>
