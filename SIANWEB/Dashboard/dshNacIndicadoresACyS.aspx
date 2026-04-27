<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" 
    AutoEventWireup="true" CodeBehind="dshNacIndicadoresACyS.aspx.cs" Inherits="SIANWEB.Dashboard.dshNacIndicadoresACyS" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<!-- Metro 4 -->    
<link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css" />

<link  id='GoogleFontsLink' href='https://fonts.googleapis.com/css?family=Open Sans' rel='stylesheet' type='text/css'>
<script>

    WebFontConfig = {
        google:
            { families: ["Open Sans",] }, active: function () {
                DrawTheChart(ChartData, ChartOptions, "chart-01", "HorizontalBar")
            }
    };

</script>
<script asyn src="https://charts.livegap.com/js/webfont.js" ></script>
<script src="https://charts.livegap.com/js/Chart.min.js"></script>
<script>

    function DrawTheChart(ChartData, ChartOptions, ChartId, ChartType) {
        eval('var myLine = new Chart(document.getElementById(ChartId).getContext("2d")).' + ChartType + '(ChartData,ChartOptions);document.getElementById(ChartId).getContext("2d").stroke();')
    }

</script>
<style>
#container-slider
{
    position: relative;
    display: block;
    width: 100%;
}
#slider {
    position: relative;
    display: block;
    width: 100%;
    height: 350px;
    min-height: 200px;
}
#slider li {
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center;
    position: absolute !important;
    top: 0 !important;
    left: 0 !important;
    width: 100%;
    height: 100%;
    display: block;
    -webkit-transition: opacity 1s;
    -moz-transition: opacity 1s;
    -ms-transition: opacity 1s;
    -o-transition: opacity 1s;
    transition: opacity 1s;
    z-index: -1;
    opacity: 0;
}

#container-slider .arrowPrev, #container-slider .arrowNext{
    /* font-size: 30pt; */
    /* color: rgba(204, 204, 204, 0.65); */
    cursor: pointer;
    position: absolute;
    top: 50%;
    left: 30px;
    z-index: 2; 
}
#container-slider .arrowNext {
    left: initial;
    right: 30px !important;
}
.content_slider{
    padding: 5px 10px;
    color: #000;
    width: 100%;
    height: 100%;
}
.content_slider div{
    text-align: center;
}
.content_slider h2{
    font-family: 'arial';
    font-size: 30pt;
    letter-spacing: 1px;
    text-transform: uppercase;
    margin-bottom: 10px;
}
/*
.content_slider p {
    font-size: 15pt;
    font-family: 'arial';
    color: #000;
    margin-bottom: 20px;
}
*/
#slider li .content_slider{
     /* background: rgba(0, 0, 0, 0.50); */
    padding: 10px 25px;
}
.content_slider{
    display: -webkit-flex;
    display: -moz-flex;
    display: -ms-flex;
    display: -o-flex;
    display: flex;
    justify-content: center;
    align-items: center;
}
.btnSlider{
    color: #AAA;
    font-size: 15pt;
    font-family: 'arial';
    letter-spacing: 1px;
    padding: 10px 50px;
    border: 1px solid #CCC;
    background: rgba(13, 13, 13, 0.55);
    border-radius: 31px;
    text-decoration: none;
    transition: .5s all;
}
.btnSlider:hover{
    background: #111;
    border: 1px solid #111;
}
.listslider {
    position: absolute;
    display: -webkit-flex;
    display: -moz-flex;
    display: -ms-flex;
    display: -o-flex;
    display: flex;
    justify-content: space-between;
    align-items: center;
    left: 50%;
    bottom: 5%;
    list-style: none;
    z-index: 2;
    transform: translateX(-50%);
}
.listslider li {
    border-radius: 50%;
    width: 10px;
    height: 10px;
    cursor: pointer;
    margin: 0 5px;
}
.listslider li a {
    background: #CCC;
    border-radius: 50%;
    width: 100%;
    height: 100%;
    display: block;
}
.item-select-slid {
    background: #009CD9  !important;
}

@media screen and (max-width: 660px){
	.content_slider h2 {
	    font-size: 15pt !important;
	}
	.content_slider p {
	    font-size: 12pt !important;
	}
	#container-slider .arrowPrev, #container-slider .arrowNext{
		font-size: 20pt;
	}
	#container-slider .arrowPrev{
		left: 15px;
	}
	#container-slider .arrowNext{
		right: 15px !important;
	}
	#slider{
		height: 400px;
		min-height: 400px;
	}
	#slider li .content_slider{
		padding: 10px 35px;
	}
	.btnSlider{
		padding: 10px 30px;
    	font-size: 10pt;
	}

}
</style>
<script type="text/javascript">

/// CDIs
    function ChkaTodos() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosCDIs.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkCDIS.ClientID%>");

        var chkkValor = chkBox.checked;

        chkBoxRIKS.disabled = true;
        chkBoxRIKS.checked = false;

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            for (i = 0; i < tgname.length; i++) {
                if (tgname[i].type == 'checkbox') {

                    tgname[i].checked = chkkValor;
                }
            }

        }
        return false;
    }

    function UnChkaElTodos() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosCDIs.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkCDIS.ClientID%>");
        var chkBoxListB = document.getElementById("<%=lstchkCDC.ClientID%>");
        var chkBoxListC = document.getElementById("<%=lstchkFran.ClientID%>");

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            var NoChecked = 0;
            var TotalChecks = tgname.length;

            for (ii = 0; ii < tgname.length; ii++) {
                if (tgname[ii].type == 'checkbox') {
                    if (tgname[ii].checked == false) {
                        NoChecked++;
                    }
                }
            }

            if (chkBoxListB) {
                var tgnameB = chkBoxListB.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameB.length;

                for (ii = 0; ii < tgnameB.length; ii++) {
                    if (tgnameB[ii].type == 'checkbox') {
                        if (tgnameB[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }

            if (chkBoxListC) {
                var tgnameC = chkBoxListC.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameC.length;

                for (ii = 0; ii < tgnameC.length; ii++) {
                    if (tgnameC[ii].type == 'checkbox') {
                        if (tgnameC[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }

            if ((NoChecked + 1) == TotalChecks ) {
                chkBoxRIKS.disabled = false;
            }
            else {
                chkBoxRIKS.disabled = true;
                chkBoxRIKS.checked = false;
            }
            ///

            if (chkBox.checked) {
                for (i = 0; i < tgname.length; i++) {
                    if (tgname[i].type == 'checkbox') {
                        if (tgname[i].checked == false) {
                            chkBox.checked = false;
                            break;
                        }
                    }
                }
            }
        }
    }

/// CDCs
    function ChkaTodosCDC() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosCDC.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkCDC.ClientID%>");

        var chkkValor = chkBox.checked;

        chkBoxRIKS.disabled = true;
        chkBoxRIKS.checked = false;

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            for (i = 0; i < tgname.length; i++) {
                if (tgname[i].type == 'checkbox') {

                    tgname[i].checked = chkkValor;
                }
            }

        }
        return false;
    }

    function UnChkaElTodosCDC() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosCDC.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkCDC.ClientID%>");
        var chkBoxListB = document.getElementById("<%=lstchkCDIS.ClientID%>");
        var chkBoxListC = document.getElementById("<%=lstchkFran.ClientID%>");

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            var NoChecked = 0;
            var TotalChecks = tgname.length;
            for (ii = 0; ii < tgname.length; ii++) {
                if (tgname[ii].type == 'checkbox') {
                    if (tgname[ii].checked == false) {
                        NoChecked++;
                    }
                }
            }

            if (chkBoxListB) {
                var tgnameB = chkBoxListB.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameB.length;

                for (ii = 0; ii < tgnameB.length; ii++) {
                    if (tgnameB[ii].type == 'checkbox') {
                        if (tgnameB[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }

            if (chkBoxListC) {
                var tgnameC = chkBoxListC.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameC.length;

                for (ii = 0; ii < tgnameC.length; ii++) {
                    if (tgnameC[ii].type == 'checkbox') {
                        if (tgnameC[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }
            // verifica si esta encendido solamente un check, para habilitar el check de riks
            // tiene que revisar los 3 listados para cuando solamente sea uno el seleccionado

            if ((NoChecked + 1) == TotalChecks) {
                chkBoxRIKS.disabled = false;
            }
            else {
                chkBoxRIKS.disabled = true;
                chkBoxRIKS.checked = false;
            }
            ///

            if (chkBox.checked) {
                for (i = 0; i < tgname.length; i++) {
                    if (tgname[i].type == 'checkbox') {
                        if (tgname[i].checked == false) {
                            chkBox.checked = false;
                            break;
                        }
                    }
                }
            }
        }
    }

/// Franquicias
    function ChkaTodosFran() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosFranquicias.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkFran.ClientID%>");

        var chkkValor = chkBox.checked;

        chkBoxRIKS.disabled = true;
        chkBoxRIKS.checked = false;

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            for (i = 0; i < tgname.length; i++) {
                if (tgname[i].type == 'checkbox') {

                    tgname[i].checked = chkkValor;
                }
            }

        }
        return false;
    }

    function UnChkaElTodosFran() {
        var chkBoxRIKS = document.getElementById("<%=chkDetalleXRIK.ClientID%>");
        var chkBox = document.getElementById("<%=chkTodosFranquicias.ClientID%>");
        var chkBoxList = document.getElementById("<%=lstchkFran.ClientID%>");
        var chkBoxListB = document.getElementById("<%=lstchkCDIS.ClientID%>");
        var chkBoxListC = document.getElementById("<%=lstchkCDC.ClientID%>");

        if (chkBoxList) {
            checksDeLaLista = chkBoxList.getElementsByTagName("td");
            var tgname = chkBoxList.getElementsByTagName("input");

            var NoChecked = 0;
            var TotalChecks = tgname.length;

            for (ii = 0; ii < tgname.length; ii++) {
                if (tgname[ii].type == 'checkbox') {
                    if (tgname[ii].checked == false) {
                        NoChecked++;
                    }
                }
            }

            if (chkBoxListB) {
                var tgnameB = chkBoxListB.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameB.length;

                for (ii = 0; ii < tgnameB.length; ii++) {
                    if (tgnameB[ii].type == 'checkbox') {
                        if (tgnameB[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }

            if (chkBoxListB) {
                var tgnameC = chkBoxListC.getElementsByTagName("input");

                TotalChecks = TotalChecks + tgnameC.length;

                for (ii = 0; ii < tgnameC.length; ii++) {
                    if (tgnameC[ii].type == 'checkbox') {
                        if (tgnameC[ii].checked == false) {
                            NoChecked++;
                        }
                    }
                }
            }

            if ((NoChecked + 1) == TotalChecks) {
                chkBoxRIKS.disabled = false;
            }
            else {
                chkBoxRIKS.disabled = true;
                chkBoxRIKS.checked = false;
            }
            ///

            if (chkBox.checked) {
                for (i = 0; i < tgname.length; i++) {
                    if (tgname[i].type == 'checkbox') {
                        if (tgname[i].checked == false) {
                            chkBox.checked = false;
                            break;
                        }
                    }
                }
            }
        }
    }

</script>
<table style=" width: 100%; border-spacing:25px; column-width:5px">
    <tr><td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td></tr>
    <tr>
        <td id="headderTD">
            <table style=" width: 100%; border-spacing:5px; column-width:5px" >
                <tr>
                    <td style="width:40%; vertical-align:top;">
                        <table class="table subcompact">
                            <tr>
                                <td style="vertical-align:top;">
                                    <h5>
                                        &nbsp;<img src="../Images/imgAplicarFiltro.png" width="25" height="25" onclick="Actualiza();" style="cursor: pointer"/>&nbsp;Filtros&nbsp;
                                    </h5>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style=" width: 100%; border-spacing:2px" border="1">
                                        <tr>
                                            <td colspan="3"><p class="text-secondary">CDI:</p></td>
                                        </tr>
                                        <tr>
                                            <td style=" width:30%; ">
                                                <p class="text-secondary" style="align-content:end; align-self:end; align-items:end; text-align:end;">
                                                    <asp:checkbox name="chkTodosRIKs" id="chkTodosCDIs" runat="server"
                                                            onclick="ChkaTodos();" checked="true" />&nbsp;<small>Todos los CDIs</small>
                                                </p>
                                            </td>
                                            <td style=" width:30%; ">
                                                <p class="text-secondary" style="align-content:end; align-self:end; align-items:end; text-align:end;">
                                                    <asp:checkbox name="chkTodosCDC" id="chkTodosCDC" runat="server"
                                                            onclick="ChkaTodosCDC();" checked="true" />&nbsp;<small>Todos los CDCs</small>
                                                </p>
                                            </td>
                                            <td style=" width:30%; ">
                                                <p class="text-secondary" style="align-content:end; align-self:end; align-items:end; text-align:end;">
                                                    <asp:checkbox name="chkTodosFranquicias" id="chkTodosFranquicias" runat="server"
                                                            onclick="ChkaTodosFran();" checked="true" />&nbsp;<small>Todos las Franquicias</small>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" width:30%;">
                                                <div style="height:130px; width:100%; overflow-y: scroll;">
                                                <asp:CheckBoxList ID="lstchkCDIS" runat="server" onclick="UnChkaElTodos();"
                                                        RepeatColumns="1" CellPadding="5" Width="100%" Height="120px" TextAlign="Right" />
                                                </div>
                                            </td>
                                            <td style=" width:30%;">
                                                <div style="height:130px; width:100%; overflow-y: scroll;">
                                                <asp:CheckBoxList ID="lstchkCDC" runat="server" onclick="UnChkaElTodosCDC();"
                                                        RepeatColumns="1" CellPadding="5" Width="100%" Height="120px" TextAlign="Right" />
                                                </div>
                                            </td>
                                            <td style=" width:30%;">
                                                <div style="height:130px; width:100%; overflow-y: scroll;">
                                                <asp:CheckBoxList ID="lstchkFran" runat="server" onclick="UnChkaElTodosFran();"
                                                        RepeatColumns="1" CellPadding="5" Width="100%" Height="120px" TextAlign="Right" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="height:5px">
                                            <td ><p class="text-secondary">Estatus:&nbsp;&nbsp; </p>
                                            </td>
                                        <!--</tr>
                                        <tr>-->
                                            <td colspan="2" style="align-content:end; align-self:end; align-items:end; ">
                                                <asp:DropDownList ID="drpEstatus" runat="server" CssClass="text-secondary"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpEstatus_SelectedIndexChanged" >
                                                    <asp:ListItem Value="" Text="-- TODOS --" Selected="True" />
                                                    <asp:ListItem Value="Capturado" Text="CAPTURADO" />
                                                    <asp:ListItem Value="Solicitado G" Text="SOLICITADO G" />
                                                    <asp:ListItem Value="Solicitado J" Text="SOLICITADO J" />
                                                    <asp:ListItem Value="Autorizado" Text="AUTORIZADO" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:44%; vertical-align:top;">
                        <table class="table subcompact" >
                            <tr>
                                <td style="align-content:start; align-self:start; align-items:start; text-align:start; ">
                                    <h5>
                                        &nbsp;<img src="../Images/imgBajarExcel.png" width="25" height="25" />&nbsp;Descargar Información&nbsp;
                                    </h5>
                                </td>
                            </tr>
                            <tr>
                                <td style=" vertical-align:top;">
                                    <table  style="width:100%">
                                        <tr>
                                            <td style="width:10px">&nbsp;</td>
                                            <td style="width:42%; vertical-align:top;">
                                                <table>
                                                    <tr>
                                                        <td style="align-content:center ; align-self:center; align-items:center; text-align:center;vertical-align:top; " >                                                                    
                                                                <asp:ImageButton runat="server" ID="imgRptAvance" ImageUrl="../Images/imgAvance01.png"  AlternateText="Reporte de Avance Detallado"
                                                                OnClick="imgRptAvance_Click" width="35" height="35" CssClass="rpImage"/> 
                                                        </td>
                                                        <td  style="align-content:start; align-self:start; align-items:start; text-align:start ;vertical-align:central; ">
                                                            <p class="text-secondary" onclick="ClickAvance();" style="cursor: pointer">
                                                                Reporte de Avance Detallado
                                                            </p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary">
                                                            <asp:checkbox name="chkLista8020" id="chkLista8020" runat="server" CssClass="custom-checkbox" 
                                                                Text="" checked="true" />&nbsp;<small>Listado de Clientes 80/20</small></p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary">
                                                            <asp:checkbox name="chkListaMatriz" id="chkListaMatriz" runat="server" Cssclass="custom-checkbox"
                                                                Text=""  checked="true"/>&nbsp;<small>ACyS Vigentes</small></p>
                                                        </td>
                                                    </tr>
                                                    <!--
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary" >
                                                            <asp:checkbox name="chkDetalleXRIK" id="chkDetalleXCDI" runat="server" CssClass="custom-checkbox"
                                                                Text="" checked="false"/>&nbsp;<small>Desglosar por CDI</small></p>
                                                        </td>
                                                    </tr>
                                                    -->
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary" >
                                                            <asp:checkbox name="chkDetalleXRIK" id="chkDetalleXRIK" runat="server" CssClass="custom-checkbox"
                                                                Enabled="false" Text="" checked="false"/>&nbsp;<small>Desglosar por RIK</small></p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:42%; vertical-align:top; border-left:solid thin #009CD9">
                                                <table>
                                                    <tr>
                                                        <td style="align-content:center ; align-self:center; align-items:center; text-align:center;vertical-align:top; "
                                                            >
                                                            <asp:ImageButton runat="server" ID="imgRptCumplimiento" ImageUrl="../Images/imgCumplimiento01.png"  AlternateText="Reporte de Cumplimiento en Captura"
                                                                OnClick="imgRptCumplimiento_Click" width="35" height="35" CssClass="rpImage"/>
                                                        </td>
                                                        <td  style="align-content:start; align-self:start; align-items:start; text-align:start ;vertical-align:central; ">
                                                            <p class="text-secondary" onclick="ClickCumplimiento();" style="cursor: pointer">
                                                                Cumplimiento en Captura
                                                            </p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="align-content:start; align-self:start; align-items:start; text-align:start;vertical-align:top; ">
                                                            <p class="text-secondary">
                                                            <asp:checkbox name="chkDesgloseEstatus" id="chkDesgloseEstatus" runat="server" CssClass="custom-checkbox" 
                                                                Text="" checked="true" />&nbsp;<small>Desglose Por Estatus</small></p>
                                                        </td>
                                                    </tr>
                                                    <!--
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary" >
                                                            <asp:checkbox name="chkDetalleXCDICumplimiento" id="chkDetalleXCDICumplimiento" runat="server" CssClass="custom-checkbox"
                                                                Text="" checked="false"/>&nbsp;<small>Desglosar por CDI</small></p>
                                                        </td>
                                                    </tr>
                                                    -->
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <p class="text-secondary" >
                                                            <asp:checkbox name="chkDetalleXRIKCumplimiento" id="chkDetalleXRIKCumplimiento" runat="server" CssClass="custom-checkbox"
                                                                Text="" checked="false"/>&nbsp;<small>Desglosar por RIK</small></p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:10px">&nbsp;
                                                <div style="visibility:hidden">
                                                    <asp:Button ID="btnActualiza" runat="server" OnClick="btnActualiza_Click" />
                                                    <asp:Button ID="btnAvance" runat="server" OnClick="btnAvance_Click" />
                                                    <asp:Button ID="btnCumplimiento" runat="server" OnClick="btnCumplimiento_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td id="middleTD">
            <section id="container-slider">	
               <img class="arrowPrev" src="../Images/imgAtras.png" width="25" height="25" onclick="javascript: fntExecuteSlide('prev');" style="cursor: pointer"/>
               <img class="arrowNext" src="../Images/imgAdelante.png" width="25" height="25" onclick="javascript: fntExecuteSlide('next');" style="cursor: pointer"/>
               <ul class="listslider">
                   <%=strLista %>
                 <!--
                   <li><a itlist='itList_0' href='#' class='item-select-slid'></a></li>
                 <li><a itlist='itList_1' href='#'></a></li>
                 <li><a itlist='itList_2' href='#'></a></li>
                   -->
               </ul>
               <ul id="slider">
                   <%=strMiddle %>
              </ul>
            </section>
        </td>
    </tr>
    <tr>
        <td id="graphTD">
            <table style="width: 100%; border-spacing:5px; column-width:5px" class="table subcompact" >
                <tr><td colspan="2" ><h5>&nbsp;&nbsp;</h5></td></tr>
                <tr>
                    <td style="vertical-align:top;  align-content:center; text-align:center; align-items:center; align-self:center;">
                        <canvas  id="chart-01" width="1000" height="550"
                            style="background-color:rgba(255,255,255,1.00);border-radius:0px;width:950px;
                                height:550px;padding-left:20px;padding-right:10px;padding-top:0px;padding-bottom:0px">
                        </canvas>
                    </td>
                    <td style=" width:80px;vertical-align:top;  align-content:start; text-align:start; align-items:start; align-self:start;">
                        <asp:ImageButton runat="server" ID="btnExcelGrafica" ImageUrl="../Images/imgBajarExcel.png"  AlternateText="Descargar Acuerdos Por CDI"
                            OnClick="btnExcelGrafica_Click" width="35" height="35" CssClass="rpImage"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script>
    function ClickAvance() {
        var e = document.getElementById("<%=btnAvance.ClientID%>");
        e.click();
    }

    function ClickCumplimiento() {
        var e = document.getElementById("<%=btnCumplimiento.ClientID%>");
        e.click();
    }

    function Actualiza() {
        var e = document.getElementById("<%=btnActualiza.ClientID%>");
        e.click();
    }
</script>
<!-- Metro 4 -->
<link rel="stylesheet" href="https://cdn.metroui.org.ua/v4/css/metro-all.min.css" />
<script src="https://cdn.metroui.org.ua/v4/js/metro.min.js"></script>

<script>

    if (document.querySelector('#container-slider')) {
        /// setInterval('fntExecuteSlide("next")', 5000);
    }
    //------------------------------ LIST SLIDER -------------------------
    if (document.querySelector('.listslider')) {
        let link = document.querySelectorAll(".listslider li a");
        link.forEach(function (link) {
            link.addEventListener('click', function (e) {
                e.preventDefault();
                let item = this.getAttribute('itlist');
                let arrItem = item.split("_");
                fntExecuteSlide(arrItem[1]);
                return false;
            });
        });
    }

    function fntExecuteSlide(side) {
        let parentTarget = document.getElementById('slider');
        let elements = parentTarget.getElementsByTagName('li');
        let curElement, nextElement;

        for (var i = 0; i < elements.length; i++) {

            if (elements[i].style.opacity == 1) {
                curElement = i;
                break;
            }
        }
        if (side == 'prev' || side == 'next') {

            if (side == "prev") {
                nextElement = (curElement == 0) ? elements.length - 1 : curElement - 1;
            } else {
                nextElement = (curElement == elements.length - 1) ? 0 : curElement + 1;
            }
        } else {
            nextElement = side;
            side = (curElement > nextElement) ? 'prev' : 'next';

        }
        //RESALTA LOS PUNTOS
        let elementSel = document.getElementsByClassName("listslider")[0].getElementsByTagName("a");
        elementSel[curElement].classList.remove("item-select-slid");
        elementSel[nextElement].classList.add("item-select-slid");
        elements[curElement].style.opacity = 0;
        elements[curElement].style.zIndex = 0;
        elements[nextElement].style.opacity = 1;
        elements[nextElement].style.zIndex = 1;
    }

</script>

<script> 
    function MoreChartOptions() { }
    var ChartData = {
        labels: [ <%=strCDIS%>
                ],
            datasets: [{
                fillColor: ['rgba(135,193,232,0.96)', 'rgba(5,140,62,1)', 'rgba(248,143,255,0.97)', 'rgba(11,216,219,0.95)', 'rgba(54,146,207,1)', 'rgba(237,26,51,0.95)', 'rgba(127,235,85,0.7)', 'rgba(97,18,97,0.95)', 'rgba(230,208,43,0.86)', 'rgba(219,91,17,0.78)',],
                strokeColor: ['rgba(52,152,219,0.5)', 'rgba(46,204,113,1)', 'rgba(167,132,181,1)', 'rgba(241,196,15,1)', 'rgba(78,169,230,1)', 'rgba(83,21,119,0.4)', 'rgba(135,242,92,1)', 'rgba(133,82,133,1)', 'rgba(176,196,167,0.2)', 'rgba(83,21,119,0.4)',],
                pointColor: "rgba(52,152,219,1)",
                markerShape: "circle",
                pointStrokeColor: "rgba(255,255,255,1.00)",
                data: [<%=strDatoCDIS%>
                ///    21, 48, 28, 19, 96, 27, 99, 92, 68, 25,
            ],
            title: ""
        },]
    };

    ChartOptions = {
        decimalSeparator: ".", thousandSeparator: ",", spaceLeft: 12, spaceRight: 12, spaceTop: 12, spaceBottom: 12,
        ///     scaleLabel: "< %=value+''% >",
        ///      scaleLabel: "''",
        yAxisMinimumInterval: 1, scaleShowLabels: true, scaleShowLine: true, scaleLineStyle: "solid",
        scaleLineWidth: 1, scaleLineColor: "rgba(0,0,0,0.6)", scaleOverlay: true, scaleOverride: false, scaleSteps: 10,
        scaleStepWidth: 10, scaleStartValue: 0, inGraphDataShow: true,
        ///     inGraphDataTmpl: '< %=v3% >',
        inGraphDataFontFamily: "'Open Sans'", inGraphDataFontStyle: "normal bold", inGraphDataFontColor: "rgba(0,0,0,0.69)",
        inGraphDataFontSize: 16, inGraphDataPaddingX: 13, inGraphDataPaddingY: 0, inGraphDataAlign: "center", inGraphDataVAlign: "middle",
        inGraphDataXPosition: 3, inGraphDataYPosition: 2, inGraphDataAnglePosition: 2, inGraphDataRadiusPosition: 2, inGraphDataRotate: 0,
        inGraphDataPaddingAngle: 0, inGraphDataPaddingRadius: 0, inGraphDataBorders: false, inGraphDataBordersXSpace: 1,
        inGraphDataBordersYSpace: 1, inGraphDataBordersWidth: 1, inGraphDataBordersStyle: "solid", inGraphDataBordersColor: "rgba(0,0,0,1)",
        legend: true, maxLegendCols: 5, legendBlockSize: 15, legendFillColor: 'rgba(255,255,255,0.00)', legendColorIndicatorStrokeWidth: 1,
        legendPosX: -2, legendPosY: 4, legendXPadding: 0, legendYPadding: 0, legendBorders: false, legendBordersWidth: 1,
        legendBordersStyle: "solid", legendBordersColors: "rgba(102,102,102,1)", legendBordersSpaceBefore: 5, legendBordersSpaceLeft: 5,
        legendBordersSpaceRight: 5, legendBordersSpaceAfter: 5, legendSpaceBeforeText: 5, legendSpaceLeftText: 5, legendSpaceRightText: 5,
        legendSpaceAfterText: 5, legendSpaceBetweenBoxAndText: 5, legendSpaceBetweenTextHorizontal: 5, legendSpaceBetweenTextVertical: 5,
        legendFontFamily: "'Open Sans'", legendFontStyle: "normal normal", legendFontColor: "rgba(0,0,0,1)", legendFontSize: 17,
        showYAxisMin: false, rotateLabels: "smart", xAxisBottom: true, yAxisLeft: true, yAxisRight: false, graphTitleSpaceBefore: 5,
        graphTitleSpaceAfter: 5, graphTitleBorders: false, graphTitleBordersXSpace: 1, graphTitleBordersYSpace: 1, graphTitleBordersWidth: 1,
        graphTitleBordersStyle: "solid", graphTitleBordersColor: "rgba(0,0,0,1)", graphTitle: "Acuerdos Vigentes Por CDI",
        graphTitleFontFamily: "'Open Sans'", graphTitleFontStyle: "normal normal", graphTitleFontColor: "rgba(52,152,219,1)",
        graphTitleFontSize: 26, graphSubTitleSpaceBefore: 5, graphSubTitleSpaceAfter: 5, graphSubTitleBorders: false,
        graphSubTitleBordersXSpace: 1, graphSubTitleBordersYSpace: 1, graphSubTitleBordersWidth: 1, graphSubTitleBordersStyle: "solid",
        graphSubTitleBordersColor: "rgba(0,0,0,1)", graphSubTitle: "Mejores 10",
        graphSubTitleFontFamily: "'Open Sans'", graphSubTitleFontStyle: "normal normal", graphSubTitleFontColor: "rgba(102,102,102,1)",
        graphSubTitleFontSize: 16, scaleFontFamily: "'Open Sans'", scaleFontStyle: "normal normal", scaleFontColor: "rgba(0,0,0,1)",
        scaleFontSize: 14, pointLabelFontFamily: "'Open Sans'", pointLabelFontStyle: "normal normal", pointLabelFontColor: "rgba(102,102,102,1)",
        pointLabelFontSize: 16, angleShowLineOut: true, angleLineStyle: "solid", angleLineWidth: 1, angleLineColor: "rgba(0,0,0,0.1)",
        percentageInnerCutout: 50, scaleShowGridLines: true, scaleGridLineStyle: "solid", scaleGridLineWidth: 1,
        scaleGridLineColor: "rgba(0,0,0,0.22)", scaleXGridLinesStep: 2, scaleYGridLinesStep: 10, segmentShowStroke: true,
        segmentStrokeStyle: "solid", segmentStrokeWidth: 2, segmentStrokeColor: "rgba(255,255,255,1.00)", datasetStroke: true,
        datasetFill: true, datasetStrokeStyle: "solid", datasetStrokeWidth: 2, bezierCurve: true, bezierCurveTension: 0.4,
        pointDotStrokeStyle: "solid", pointDotStrokeWidth: 1, pointDotRadius: 3, pointDot: true, scaleTickSizeBottom: 5,
        scaleTickSizeTop: 5, scaleTickSizeLeft: 5, scaleTickSizeRight: 5, graphMin: 0, barShowStroke: true, barBorderRadius: 0,
        barStrokeStyle: "solid", barStrokeWidth: 0, barValueSpacing: 2, barDatasetSpacing: 5, scaleShowLabelBackdrop: true,
        scaleBackdropColor: 'rgba(255,255,255,0.75)', scaleBackdropPaddingX: 2, scaleBackdropPaddingY: 2, animationEasing: 'linear',
        animateRotate: true, animateScale: false, animationByDataset: false, animationLeftToRight: true, animationSteps: 300,
        animation: true, onAnimationComplete: function () { MoreChartOptions() }
    };
    DrawTheChart(ChartData, ChartOptions, "chart-01", "HorizontalBar");</script>
</asp:Content>
