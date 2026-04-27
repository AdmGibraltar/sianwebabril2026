<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizacionVinculacion.aspx.cs" Inherits="SIANWEB.AutorizacionVinculacion"  MasterPageFile="~/MasterPage/MasterPage02.Master"
    %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <style type="text/css">
        .rfdSkinnedButton , .rfdDecorated {
            
        }
    </style>

     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog       
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
            return oWindow;
        }
        //Cierra la venata actual y regresa el foco a la ventana padre
        //Cierra la venata actual y regresa el foco a la ventana padre
        function CloseWindow(id_u, id_cd, nombre,tipoautorizacion) {
            GetRadWindow().Close();
            GetRadWindow().BrowserWindow.AutorizarVinculacionCliente(id_u, id_cd, nombre, tipoautorizacion);
        }
        function GetSteptOne() {
            document.getElementById("steptwo").style.display = "";
            document.getElementById("stepOne").style.display = "none";
        }
    </script>

</telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ajx">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="formulario"  runat="server">
        <div class="row">
            <div class="col-lg-3 "></div> 
            <inpt type="hidden" id="ajx"> 
            
            <center id="stepOne" runat="server">
                 <h4>
                    ¿Este cliente pertenece a una cuenta corporativa?
                </h4>
               <asp:Button ID="btnSi" runat="server"
                        Style="margin-top: 6px" Text="Si" class="rmLink" OnClick="btnSi_Click" />
                <asp:Button ID="Button2" runat="server"
                        Style="margin-top: 6px" class="rmLink" Text="No" OnClick="Button2_Click"  />
            </center> 
            <div class="col-lg-3 " id="steptwo" runat="server" style="display:none">
                <center runat="server" >
                     <h4>
                        Solicitar vinculación a Matriz
                    </h4>
                
                        RFC: <input id="txtRfc" type="text" runat="server" value="" readonly/>
                
                </center>
                <center runat="server" >
                    <br />
                    <asp:Button ID="Button1" runat="server"
                            Text="Solicitar" OnClick="btnSolicitar_Click" />
                </center>
                <center id="Center1" runat="server">
                    <h4>
                       En breve podrá acceder al módulo de vinculaciones para concluir el proceso.
                    </h4>
                </center>
            </div> 
            <div class="col-lg-3 " id="divPrincipal"></div> 
        </div>
    </div>
</asp:Content>