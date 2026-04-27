<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentanaSoportes.aspx.cs" Inherits="SIANWEB.VentanaSoportes" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>



     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgOrdenes" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
     </telerik:RadAjaxManager>




    <div class="demo-container size-wide no-bg " style="margin-left:auto;margin-right:auto;">
        <telerik:RadAjaxPanel ID="RadAjaxPanelSoporteRem" runat="server" ClientEvents-OnRequestStart="requestStart"
            LoadingPanelID="LoadingPanel1">
            <div class="uploadPanel" id="uploadPanel" runat ="server">
                <div class="leftPanel">
                   
                    <div class="controls">
                        <span class="label">Archivo(s)</span>
                        <telerik:RadUpload ID="RadUploadSoporteRemision" runat="server" MaxFileInputsCount="2" OverwriteExistingFiles="false"
                            ControlObjectsVisibility="RemoveButtons" Skin="Default">
                        </telerik:RadUpload>
                        <asp:Button ID="ButtonEnviar" Visible="true" OnClick="ButtonSendSoporteRem_Click" runat="server" Text="Guardar"
                            CssClass="button"></asp:Button>
                    </div>
                </div>
                <div class="rightPanel">
                    <div id="Div2" class="UploadedFileClass" runat="server">
                        No ha seleccionado archivos.
                    </div>
                </div>
            </div>


            <div id="DivArchivoSoporte" runat="server">
             <hr />
                <h3>Archivo Soporte</h3>
             <a id="LinkDescargaArchivo" runat="server" href="">Descargar</a>
            </div>

        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="LoadingPanelSoporteRem" runat="server" InitialDelayTime="0" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadCodeBlock ID="RadCodeBlockSoporteRem" runat="server">
            <script type="text/javascript" >

                (function ($) {
                    requestStart = function (target, arguments) {
                        if (arguments.get_eventTarget().indexOf("ButtonEnviar") > -1) {
                            arguments.set_enableAjax(false);
                        }
                    }
                })($telerik.$);
            
            </script>
        </telerik:RadCodeBlock>
    </div>



        <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript">

            </script>
        </telerik:radcodeblock>

</asp:Content>
