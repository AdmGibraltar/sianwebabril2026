<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ComprasLocales.aspx.cs" Inherits="SIANWEB.ComprasLocales" %>

<asp:Content ID="Content3"  ContentPlaceHolderID="CPH" runat="server">
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />
<script type="text/javascript">




    // Broad cast that your're opening a page.
    localStorage.openpages = Date.now();
    var onLocalStorageEvent = function (e) {
        if (e.key == "openpages") {
            // Listen if anybody else opening the same page!
            localStorage.page_available = Date.now();
        }
        if (e.key == "page_available") {
            alert("Esta pantalla esta abierta en otra sesion..");

            window.history.back();


        }
    };



    function cambiatexto() {
        alert("Es importante considerar que se esta realizando el cambio de las Unidades de Medida del SAT al estándar de KEY");
    }
    function AddItem() {

        var dropDownListRef = document.getElementById('<%= listaClientes.ClientID %>');
        var ddlTextRef = document.getElementById('txtNomCteListado');
        var ddlValueRef = document.getElementById('<%= hdtxtClienteListado.ClientID %>');
        var optionsList = '';
        var optionsListFull = '';
        //  alert(ddlValueRef);
        if (ddlTextRef.value != "" && ddlValueRef.value != "") {

            var option1 = document.createElement("option");
            option1.text = ddlTextRef.value;
            option1.value = ddlValueRef.value;
            dropDownListRef.options.add(option1);
        }
        else {
            alert("no se detectaron valores para agregar");
        }
        for (var i = 0; i < dropDownListRef.options.length; i++) {
            var optionText = dropDownListRef.options[i].text;
            var optionValue = dropDownListRef.options[i].value;


            if (optionsList.length > 0) {
                optionsList += ';';
                optionsList += optionValue;

                optionsListFull += ';';
                optionsListFull += optionText;
            }
            else {
                optionsList = optionValue;
                optionsListFull = optionText;
            }
        }
        document.getElementById('<%= ddlElements.ClientID %>').value = optionsList;
        document.getElementById('<%= ddlElementsFull.ClientID %>').value = optionsListFull;

        ddlTextRef.value = "";
        ddlValueRef.value = "";
    }


    function DelItem() {
        var liste = document.getElementById('<% = listaClientes.ClientID %>');
        var i;
        for (i = liste.options.length - 1; i >= 0; i--) {
            if (liste.options[i].selected)
                liste.remove(i);
        }

        var optionsList = '';
        var optionsListFull = '';

        for (var i = 0; i < liste.options.length; i++) {
            var optionText = liste.options[i].text;
            var optionValue = liste.options[i].value;


            if (optionsList.length > 0) {
                optionsList += ';';
                optionsList += optionValue;

                optionsListFull += ';';
                optionsListFull += optionText;
            }
            else {
                optionsList = optionValue;
                optionsListFull = optionText;
            }
        }

        document.getElementById('<%= ddlElements.ClientID %>').value = optionsList;
        document.getElementById('<%= ddlElementsFull.ClientID %>').value = optionsListFull;
    }


    function AddItemCons() {

        var dropDownListRef = document.getElementById('<%= lstClientesAutorizadosCons.ClientID %>');
        var ddlTextRef = document.getElementById('txtNomCteListadoCons');
        var ddlValueRef = document.getElementById('hdtxtClienteListadoCons');
        var optionsList = '';
        var optionsListFull = '';

        if (ddlTextRef.value != "" && ddlValueRef.value != "") {

            var option1 = document.createElement("option");
            option1.text = ddlTextRef.value;
            option1.value = ddlValueRef.value;
            dropDownListRef.options.add(option1);
        }
        else {
            alert("no se detectaron valores para agregar");
        }
        for (var i = 0; i < dropDownListRef.options.length; i++) {
            var optionText = dropDownListRef.options[i].text;
            var optionValue = dropDownListRef.options[i].value;


            if (optionsList.length > 0) {
                optionsList += ';';
                optionsList += optionValue;

                optionsListFull += ';';
                optionsListFull += optionText;
            }
            else {
                optionsList = optionValue;
                optionsListFull = optionText;
            }
        }
        document.getElementById('<%= ddlElementsCons.ClientID %>').value = optionsList;
        document.getElementById('<%= ddlElementsFullCons.ClientID %>').value = optionsListFull;

        ddlTextRef.value = "";
        ddlValueRef.value = "";
    }


    function DelItemCons() {
        var liste = document.getElementById('<% = lstClientesAutorizadosCons.ClientID %>');
        var i;
        for (i = liste.options.length - 1; i >= 0; i--) {
            if (liste.options[i].selected)
                liste.remove(i);
        }

        var optionsList = '';
        var optionsListFull = '';

        for (var i = 0; i < liste.options.length; i++) {
            var optionText = liste.options[i].text;
            var optionValue = liste.options[i].value;


            if (optionsList.length > 0) {
                optionsList += ';';
                optionsList += optionValue;

                optionsListFull += ';';
                optionsListFull += optionText;
            }
            else {
                optionsList = optionValue;
                optionsListFull = optionText;
            }
        }

        document.getElementById('<%= ddlElementsCons.ClientID %>').value = optionsList;
        document.getElementById('<%= ddlElementsFullCons.ClientID %>').value = optionsListFull;
    }

    function QuitabPedidos() {
        var PedidoSel = document.getElementById('<%= txtValuesPedidos.ClientID %>');
        var Pedidolista = document.getElementById('<%= txtListadSelecionados.ClientID %>');
        PedidoSel.value = "";
        Pedidolista.value = "";
    }


    function lstbPedidos_Click() {
        var PedidoSel = document.getElementById('<%= lstbPedidos.ClientID %>');
        var lisval = document.getElementById('<%= txtValuesPedidos.ClientID %>');
        var lisado = document.getElementById('<%= txtListadSelecionados.ClientID %>');

        try {


            if (PedidoSel.options[PedidoSel.selectedIndex].innerHTML != "") {
                var vallor = PedidoSel.options[PedidoSel.selectedIndex].value;
                var texxto = PedidoSel.options[PedidoSel.selectedIndex].innerHTML;
                // validacion por la recarga, si la lista oculta tiene valores y la desplegada no, se limpia la oculta
                if ((lisval.value != "") && (lisado.value == "")) {
                    lisval.value = "";
                }

                //checa si no esta duplicado el id q se va ainsertar, si no esta,lo deja pasar, si no, lo ignora y saca mensaje
                var string_variable = '...' + lisval.value;

                if (string_variable.indexOf(vallor) != -1) {
                    alert('Orden ya Seleccionada.');
                    return;
                }
                else {
                    try {
                        var option1 = document.createElement("option");
                        option1.text = texxto;
                        option1.value = vallor;
                        //var listtado = ;
                        //  document.getElementById('< %= lstPedidosSeleccionados.ClientID % >').options.add(option1);

                        lisado.value = lisado.value + texxto + " \n";
                        lisval.value = lisval.value + vallor + ", ";

                        //  alert(option1.text);
                        return false;
                    }
                    catch (e) {
                        alert(e.toString());
                    }
                }

            }
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function lstPedidosSeleccionados_Click() {

        var liste = document.getElementById('<%=txtListadSelecionados.ClientID %>');
        var i;
        for (i = liste.options.length - 1; i >= 0; i--) {
            if (liste.options[i].selected)
                liste.remove(i);
        }


    }
    
</script>

 <script type="text/javascript">

     $(function () {
         $("[id$=txtSearchProdServSAT]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoServicioSAT") %>',
                     data: "{ 'ProdServCve': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfCveProdServSAT]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtSearchProdServSATAbasto]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoServicioSAT") %>',
                     data: "{ 'ProdServCve': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfCveProdServSATAbasto]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtSearchProdServSATACte]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoServicioSAT") %>',
                     data: "{ 'ProdServCve': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfCveProdServSATCte]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtSearch]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductName") %>',
                     data: "{ 'prodName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfCustomerId]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtSearchProv]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProvidertName") %>',
                     data: "{ 'provName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfProviderId]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtProductoLocal]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoLocal") %>',
                     data: "{ 'prodName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfProductoLocal]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtSearchProvAbasto]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorAbasto") %>',
                     data: "{ 'provName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfProviderAbastoId]").val(i.item.val);
             },
             minLength: 1
         });
     });


     $(function () {
         $("[id$=txtSearchProvCte]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorCte") %>',
                     data: "{ 'provName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hfProviderIdCte]").val(i.item.val);
             },
             minLength: 1
         });
     });

     $(function () {
         $("[id$=txtBuscaXCodProd]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductName") %>',
                     data: "{ 'prodName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hdtxtBuscaCodi]").val(i.item.val);
             },
             minLength: 1
         });
     });


     $(function () {
         $("[id$=txtBuscaXProvee]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorSoli") %>',
                     data: "{ 'provName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hdtxtBuscaProv]").val(i.item.val);
             },
             minLength: 1
         });
     });


     $(function () {
         $("[id$=txtNomCteListado]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetClienteName") %>',
                     data: "{ 'clienteName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hdtxtClienteListado]").val(i.item.val);
             },
             minLength: 1
         });
     });


     $(function () {
         $("[id$=txtNomCteListadoCons]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetClienteName") %>',
                     data: "{ 'clienteName': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('-')[1],
                                 val: item.split('-')[0]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },
             select: function (e, i) {
                 $("[id$=hdtxtClienteListadoCons]").val(i.item.val);
             },
             minLength: 1
         });
     });



</script>

<script type="text/javascript">


    function OnDateSelected(sender, eventArgs) {
        var date1 = sender.get_selectedDate();
        if (date1 == null) {
            return false;
        }
        else {

            var hoy = new Date();
            hoy.setHours(0, 0, 0, 0);
            try {
                if (hoy > date1) {
                    alert("Fecha De Vigencia No Valida");
                    sender.clear();
                    //    return false;
                }
            }
            catch (e) {
                alert(e.toString());
            }
        }
    }


    function validarEmail() {
        var email = document.getElementById('<%= txtAutoriza1.ClientID %>');
        expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (email.value != "") {
            if (!expr.test(email.value))
                alert("Error: La dirección de correo " + email.value + " es incorrecta.");
        }
    }


    function cmbMotivoCL_ClientSelectedIndexChanged(sender, args) {
        try {

            var item = args.get_item();
            var cmbMotivo = document.getElementById('<%= cmbMotivoCL.ClientID %>');
            var divAplicacion = document.getElementById('<%= divAplicacionOculta.ClientID %>');
            var divAplicacion2 = document.getElementById('<%= divAplicacionOculta2.ClientID %>');
            var cmbMotivo2 = $find('<%= cmbMotivoCL.ClientID %>');

            if (cmbMotivo2.get_value() == 3) {

                divAplicacion.style.display = 'block';
                divAplicacion2.style.display = 'block';
            }
            else {
                try {

                    divAplicacion.style.display = 'none';
                    divAplicacion2.style.display = 'none';
                }
                catch (e) {
                    alert(e.toString());
                }
            }
        }
        catch (e) {
            alert(e.toString());
        }
    }


    //--------------------------------------------------------------------------------------------------
    //Variables de la forma
    //--------------------------------------------------------------------------------------------------
    var tabSeleccionada = '';
    var arregloSubFamilias = '';


    //--------------------------------------------------------------------------------------------------
    //Limpiar controles del catalogo de Producto
    //--------------------------------------------------------------------------------------------------
    function LimpiarControlesProducto() {
        //debugger;

        //Controles de la pestaña 'Valuacion de proyectos'                    
        var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
        var chkActivo = document.getElementById('<%= chkActivo.ClientID %>');
        var chkProductoNuevo = document.getElementById('<%= chkProductoNuevo.ClientID %>');
        var txtCodProd = $find('<%= txtCodProd.ClientID %>');
        var TextPrd_Descrpcion = $find('<%= TextPrd_Descrpcion.ClientID %>');
        var txtPresentacion = $find('<%= txtPresentacion.ClientID %>');
        var txtTipoProducto = $find('<%= txtTipoProducto.ClientID %>');
        var cmbTipoProducto = $find('<%= cmbTipoProducto.ClientID %>');
        var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
        var cmbSisProp = $find('<%= cmbSisProp.ClientID %>');
        var txtAgrupadoSpo = $find('<%= txtAgrupadoSpo.ClientID %>');
        var txtCategoria = $find('<%= txtCategoria.ClientID %>');
        var cmbCategoria = $find('<%= cmbCategoria.ClientID %>');

        var txtSubFam = $find('<%= txtSubFam.ClientID %>');
        var cmbSubFam = $find('<%= cmbSubFam.ClientID %>');
        var txtProveedor = $find('<%= txtProveedor.ClientID %>');
        var cmbProveedor = $find('<%= cmbProveedor.ClientID %>');
        var cmbUentrada = $find('<%= cmbUentrada.ClientID %>');
        var txtFactorConversion = $find('<%= txtFactorConversion.ClientID %>');
        var cmbUsalida = $find('<%= cmbUsalida.ClientID %>');
        var txtUempaque = $find('<%= txtUempaque.ClientID %>');

        LimpiarTextBox(TextId_Prd);
        LimpiarCheckBox(chkActivo, true);
        LimpiarCheckBox(chkProductoNuevo, true);
        LimpiarTextBox(txtCodProd);
        LimpiarTextBox(TextPrd_Descrpcion);
        LimpiarTextBox(txtPresentacion);
        LimpiarTextBox(txtTipoProducto);
        LimpiarComboSelectIndex0(cmbTipoProducto);
        LimpiarTextBox(TextId_Spo);
        LimpiarComboSelectIndex0(cmbSisProp);
        LimpiarTextBox(txtAgrupadoSpo);
        LimpiarTextBox(txtCategoria);
        LimpiarComboSelectIndex0(cmbCategoria);

        var txtFam = $find('<%= txtFam.ClientID %>');
        var cmbFam = $find('<%= cmbFam.ClientID %>');
        LimpiarTextBox(txtFam);
        LimpiarComboSelectIndex0(cmbFam);

        LimpiarTextBox(txtSubFam);
        LimpiarComboSelectIndex0(cmbSubFam);
        LimpiarTextBox(txtProveedor);
        LimpiarComboSelectIndex0(cmbProveedor);
        LimpiarComboSelectIndex0(cmbUentrada);
        LimpiarTextBox(txtFactorConversion);
        LimpiarComboSelectIndex0(cmbUsalida);
        LimpiarTextBox(txtUempaque);


        var txtInvSeguridad = $find('<%= txtInvSeguridad.ClientID %>');
        var chkSistProp = document.getElementById('<%= chkSistProp.ClientID %>');
        var txtTentrega = $find('<%= txtTentrega.ClientID %>');
        var txtTtransporte = $find('<%= txtTtransporte.ClientID %>');
        var txtRentabilidad = $find('<%= txtRentabilidad.ClientID %>');
        var chkComprasLocales = document.getElementById('<%= chkComprasLocales.ClientID %>');
        var txtAmortizacion = $find('<%= txtAmortizacion.ClientID %>');
        var txtPesos = $find('<%= txtPesos.ClientID %>');
        var txtExistencia = $find('<%= txtExistencia.ClientID %>');
        var txtUbicacion = $find('<%= txtUbicacion.ClientID %>');
        var txtContribucion = $find('<%= txtContribucion.ClientID %>');

        LimpiarTextBox(txtInvSeguridad);
        LimpiarCheckBox(chkSistProp);
        LimpiarTextBox(txtTentrega);
        LimpiarTextBox(txtTtransporte);
        LimpiarTextBox(txtRentabilidad);
        LimpiarCheckBox(chkComprasLocales);
        LimpiarTextBox(txtAmortizacion);
        LimpiarTextBox(txtPesos);
        LimpiarTextBox(txtExistencia);
        LimpiarTextBox(txtUbicacion);
        LimpiarTextBox(txtContribucion);

        var txtAsignado = $find('<%= txtAsignado.ClientID %>');
        var txtInicial = $find('<%= txtInicial.ClientID %>');
        var txtOrdenado = $find('<%= txtOrdenado.ClientID %>');
        var txtFinal = $find('<%= txtFinal.ClientID %>');
        var txtTransito = $find('<%= txtTransito.ClientID %>');
        var txtFisico = $find('<%= txtFisico.ClientID %>');

        LimpiarTextBox(txtAsignado);
        LimpiarTextBox(txtInicial);
        LimpiarTextBox(txtOrdenado);
        LimpiarTextBox(txtFinal);
        LimpiarTextBox(txtTransito);
        LimpiarTextBox(txtFisico);

        var txtFnombre = $find('<%= txtFnombre.ClientID %>');
        var txtFcodigo = $find('<%= txtFcodigo.ClientID %>');
        var txtFdescripcion = $find('<%= txtFdescripcion.ClientID %>');
        var txtFpresentacion = $find('<%= txtFpresentacion.ClientID %>');
        var txtPnombre = $find('<%= txtPnombre.ClientID %>');
        var txtPcodigo = $find('<%= txtPcodigo.ClientID %>');
        var txtPdescripcion = $find('<%= txtPdescripcion.ClientID %>');
        var txtPpresentacion = $find('<%= txtPpresentacion.ClientID %>');

        LimpiarTextBox(txtFnombre);
        LimpiarTextBox(txtFcodigo);
        LimpiarTextBox(txtFdescripcion);
        LimpiarTextBox(txtFpresentacion);
        LimpiarTextBox(txtPnombre);
        LimpiarTextBox(txtPcodigo);
        LimpiarTextBox(txtPdescripcion);
        LimpiarTextBox(txtPpresentacion);
    }

    //---------------------------------------------------------------------------------------------------------------------
    //  Nuevas pestañas
    //---------------------------------------------------------------------------------------------------------------------
    function LimpiarControlesProductoAbasto() {
        //debugger;

        //Controles de la pestaña 'Valuacion de proyectos'                    
        var textProductoLocal = $find('<%= txtProductoLocal.ClientID %>');
        var txtPnombreAbasto = $find('<%= txtPnombreAbasto.ClientID %>');

        var TextPrd_Descrpcion = $find('<%= txtSearchProvAbasto.ClientID %>');
        var txtTipoProducto = $find('<%= hfProviderAbastoId.ClientID %>');
        var txtTipoProducto = $find('<%= hfNumSolicitudAbasto.ClientID %>');
        var txtTipoProducto = $find('<%= hfProductoLocal.ClientID %>');

        LimpiarTextBox(textProductoLocal);
        LimpiarTextBox(txtPnombreAbasto);
        LimpiarTextBox(txtSearchProvAbasto);
        LimpiarTextBox(hfProviderAbastoId);
        LimpiarTextBox(hfNumSolicitudAbasto);
        LimpiarTextBox(hfProductoLocal);
    }

    function LimpiarControlesProductoCliente() {
        //debugger;

        //Controles de la pestaña 'Valuacion de proyectos'                    
        var TextId_Prd = $find('<%= TextId_PrdCte.ClientID %>');
        var chkActivo = document.getElementById('<%= chkActivoCte.ClientID %>');
        var chkProductoNuevo = document.getElementById('<%= chkProdNuevoCte.ClientID %>');
        var txtCodProd = $find('<%= txtCodProdCte.ClientID %>');
        var TextPrd_Descrpcion = $find('<%= TextPrd_DescrpcionCte.ClientID %>');
        var txtPresentacion = $find('<%= txtPresentacionCte.ClientID %>');
        var txtTipoProducto = $find('<%= txtTipoProductoCte.ClientID %>');
        var cmbTipoProducto = $find('<%= cmbTipoProductoCte.ClientID %>');
        var TextId_Spo = $find('<%= TextId_SpoCte.ClientID %>');
        var cmbSisProp = $find('<%= cmbSisPropCte.ClientID %>');
        //  var txtAgrupadoSpo = $find('<%= txtAgrupadoSpo.ClientID %>');
        var txtCategoria = $find('<%= txtCategoriaCte.ClientID %>');
        var cmbCategoria = $find('<%= cmbCategoriaCte.ClientID %>');
        var txtFam = $find('<%= txtFamCte.ClientID %>');
        var cmbFam = $find('<%= cmbFamCte.ClientID %>');
        var txtSubFam = $find('<%= txtSubFamCte.ClientID %>');
        var cmbSubFam = $find('<%= cmbSubFamCte.ClientID %>');
        var txtProveedor = $find('<%= txtProveedorCte.ClientID %>');
        var cmbProveedor = $find('<%= cmbProveedorCte.ClientID %>');
        var cmbUentrada = $find('<%= cmbUentradaCte.ClientID %>');
        var txtFactorConversion = $find('<%= txtFactorConversionCte.ClientID %>');
        var cmbUsalida = $find('<%= cmbUsalidaCte.ClientID %>');
        var txtUempaque = $find('<%= txtUempaqueCte.ClientID %>');

        LimpiarTextBox(TextId_Prd);
        LimpiarCheckBox(chkActivo, true);
        LimpiarCheckBox(chkProductoNuevo, true);
        LimpiarTextBox(txtCodProd);
        LimpiarTextBox(TextPrd_Descrpcion);
        LimpiarTextBox(txtPresentacion);
        LimpiarTextBox(txtTipoProducto);
        LimpiarComboSelectIndex0(cmbTipoProducto);
        LimpiarTextBox(TextId_Spo);
        LimpiarComboSelectIndex0(cmbSisProp);
        //  LimpiarTextBox(txtAgrupadoSpo);
        LimpiarTextBox(txtCategoria);
        LimpiarComboSelectIndex0(cmbCategoria);
        LimpiarTextBox(txtFam);
        LimpiarComboSelectIndex0(cmbFam);
        LimpiarTextBox(txtSubFam);
        LimpiarComboSelectIndex0(cmbSubFam);
        LimpiarTextBox(txtProveedor);
        LimpiarComboSelectIndex0(cmbProveedor);
        LimpiarComboSelectIndex0(cmbUentrada);
        LimpiarTextBox(txtFactorConversion);
        LimpiarComboSelectIndex0(cmbUsalida);
        LimpiarTextBox(txtUempaque);


        var txtInvSeguridad = $find('<%= txtInvSeguridadCte.ClientID %>');
        var chkSistProp = document.getElementById('<%= chkSistPropCte.ClientID %>');
        var txtTentrega = $find('<%= txtTentregaCte.ClientID %>');
        var txtPlanAbasto = $find('<%= txtPlanAbastoCte.ClientID %>');  // *
        var txtMinCompra = $find('<%= txtMinCompraCte.ClientID %>');    // *
        var txtTtransporte = $find('<%= txtTtransporteCte.ClientID %>');
        var chkComprasLocales = document.getElementById('<%= chkComprasLocalesCte.ClientID %>');
        var txtAmortizacion = $find('<%= txtAmortizacionCte.ClientID %>');
        var txtPesos = $find('<%= txtPesosCte.ClientID %>');
        var txtExistencia = $find('<%= txtExistenciaCte.ClientID %>');
        var txtUbicacion = $find('<%= txtUbicacionCte.ClientID %>');
        var txtMotivoSolicita = $find('<%= txtMotivoSolicita.ClientID %>');    // *

        //  var txtRentabilidad = $find('<%= txtRentabilidad.ClientID %>');
        //  var txtContribucion = $find('<%= txtContribucion.ClientID %>');

        LimpiarTextBox(txtInvSeguridad);
        LimpiarCheckBox(chkSistProp);
        LimpiarTextBox(txtTentrega);
        LimpiarTextBox(txtPlanAbasto);
        LimpiarTextBox(txtMinCompra);
        LimpiarTextBox(txtTtransporte);
        LimpiarCheckBox(chkComprasLocales);
        LimpiarTextBox(txtAmortizacion);
        LimpiarTextBox(txtPesos);
        LimpiarTextBox(txtExistencia);
        LimpiarTextBox(txtUbicacion);
        LimpiarTextBox(txtMotivoSolicita);
        //  LimpiarTextBox(txtRentabilidad);
        //  LimpiarTextBox(txtContribucion);

        var txtAsignado = $find('<%= txtAsignadoCte.ClientID %>');
        var txtInicial = $find('<%= txtInicialCte.ClientID %>');
        var txtOrdenado = $find('<%= txtOrdenadoCte.ClientID %>');
        var txtFinal = $find('<%= txtFinalCte.ClientID %>');
        var txtTransito = $find('<%= txtTransitoCte.ClientID %>');
        var txtFisico = $find('<%= txtFisicoCte.ClientID %>');

        LimpiarTextBox(txtAsignado);
        LimpiarTextBox(txtInicial);
        LimpiarTextBox(txtOrdenado);
        LimpiarTextBox(txtFinal);
        LimpiarTextBox(txtTransito);
        LimpiarTextBox(txtFisico);

        var txtFnombre = $find('<%= txtFnombreCte.ClientID %>');
        var txtFcodigo = $find('<%= txtFcodigoCte.ClientID %>');
        var txtFdescripcion = $find('<%= txtFdescripcionCte.ClientID %>');
        var txtFpresentacion = $find('<%= txtFpresentacionCte.ClientID %>');
        var txtPnombre = $find('<%= txtPnombreCte.ClientID %>');
        var txtPcodigo = $find('<%= txtPcodigoCte.ClientID %>');
        var txtPdescripcion = $find('<%= txtPdescripcionCte.ClientID %>');
        var txtPpresentacion = $find('<%= txtPpresentacionCte.ClientID %>');

        LimpiarTextBox(txtFnombre);
        LimpiarTextBox(txtFcodigo);
        LimpiarTextBox(txtFdescripcion);
        LimpiarTextBox(txtFpresentacion);
        LimpiarTextBox(txtPnombre);
        LimpiarTextBox(txtPcodigo);
        LimpiarTextBox(txtPdescripcion);
        LimpiarTextBox(txtPpresentacion);

    }
    //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
    //y selecciona la Tab donde esta el control DEL LADO DEL CLIENTE 
    function ValidaObjetoRequeridoInput(textBox, label, indiceTab) {

        //  var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

        if (textBox.value == '') {
            label.innerHTML = '** Requerido';
            //  radTabStrip.get_allTabs()[indiceTab].select();
            return false;
        }

        return true;
    }

    //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
    //y selecciona la Tab donde esta el control
    function ValidaObjetoRequerido(textBox, label, indiceTab) {

        //  var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');
        if (textBox != null) {
            if (textBox.get_textBoxValue() == '') {
                label.innerHTML = '** Requerido';
                //  radTabStrip.get_allTabs()[indiceTab].select();
                return false;
            }
        }

        return true;
    }

    //Validar datos que son requeridos Siempre
    function ValidarControlesRequeridos() {
        //debugger;
        var validacionResult = true;

        var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

        // lbl_Val_TextId_Prd = document.getElementById('<%= lbl_Val_TextId_Prd.ClientID %>');
        //  lbl_Val_TextPrd_Descrpcion = document.getElementById('<%= lbl_Val_TextPrd_Descrpcion.ClientID %>');
        //  lbl_Val_txtPresentacion = document.getElementById('<%= lbl_Val_txtPresentacion.ClientID %>');
        //  lbl_Val_txtTipoProducto = document.getElementById('<%= lbl_Val_txtTipoProducto.ClientID %>');
        //  lbl_Val_txtCategoria = document.getElementById('<%= lbl_Val_txtCategoria.ClientID %>');
        lbl_Val_txtProveedor = document.getElementById('<%=lbl_Val_txtProveedor.ClientID %>');
        //  lbl_Val_cmbUentrada = document.getElementById('<%= lbl_Val_cmbUentrada.ClientID %>');
        lbl_Val_cmbMotDesabasto = document.getElementById('<%=lbl_Val_cmbMotivoDEsabasto.ClientID %>');

        lbl_Val_PedidoSeleccionado = document.getElementById('<%=lblPedidoSeleccionado.ClientID %>');
        lbl_Val_cmbProdServicioSAT = $find('<%=lblcmbProdServicioSATDesabasto.ClientID %>');
        //  lbl_Val_cmbProdServicioSAT = document.getElementById('<%= hfCveProdServSAT.ClientID %> ');
        /// hdtxtClienteListado
        lbl_Val_cmbUnidadMedidaSAT = $find('<%=lblcmbUnidadMedidaSATDesabasto.ClientID %>');


        //  lbl_Val_TextId_Prd.innerHTML = '';
        //  lbl_Val_TextPrd_Descrpcion.innerHTML = '';
        //  lbl_Val_txtPresentacion.innerHTML = '';
        //  lbl_Val_txtTipoProducto.innerHTML = '';
        //  lbl_Val_txtCategoria.innerHTML = '';
        lbl_Val_txtProveedor.innerHTML = '';
        //  lbl_Val_cmbUentrada.innerHTML = '';
        lbl_Val_cmbMotDesabasto.innerHTML = '';
        // lbl_Val_PedidoSeleccionado.innerHTML = '';
        //cmbProdServicioSATDesabasto   cmbUnidadMedidaSATDesabasto
        lbl_Val_cmbProdServicioSAT = '';
        lbl_Val_cmbUnidadMedidaSAT = '';

        if (ValidaObjetoRequerido($find('<%=txtProveedor.ClientID %>'), lbl_Val_txtProveedor, 0) == false) {
            validacionResult = false;
            try {
                radTabStrip.get_allTabs()[0].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Proveedor');
        }

        //  cmbUentrada = $find('<%= cmbUentrada.ClientID %>');
        //  if (cmbUentrada.get_value() == '-1') {
        //      lbl_Val_cmbUentrada.innerHTML = '*Requerido';
        //        validacionResult = false
        //  }

        cmbMDesabnasto = $find('<%= cmbCausaDesabasto.ClientID %>');
        if (cmbMDesabnasto.get_value() == '-1') {
            lbl_Val_cmbMotDesabasto.innerHTML = '*Requerido';
            try {
                radTabStrip.get_allTabs()[0].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Causa del Desabasto');
            validacionResult = false;
        }

        /*
        cmbProductoSAT = $find('<% = cmbProdServicioSATDesabasto.ClientID %>');
        if (cmbProductoSAT.get_value() == '-1') {
        lbl_Val_cmbProdServicioSAT.innerHTML = '*Requerido';
        try {
        radTabStrip.get_allTabs()[2].select();
        }
        catch (e) {
        alert(e.toString());
        }
        alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        validacionResult = false
        }   
        */

        if (ValidaObjetoRequeridoInput(document.getElementById('<%=hfCveProdServSAT.ClientID %>'), lbl_Val_cmbProdServicioSAT, 0) == false) {
            validacionResult = false;
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        }


        cmbUnidadesSAT = $find('<%= cmbUnidadMedidaSATDesabasto.ClientID %>');
        if (cmbUnidadesSAT.get_value() == '-1') {
            lbl_Val_cmbUnidadMedidaSAT.innerHTML = '*Requerido';
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Unidad de Medida de SAT');
            validacionResult = false
        }

        try {

            //  lstbPedidos = $find('<%= lstbPedidos.ClientID %>');
            var lstbPedidos = document.getElementById('<%= lstbPedidos.ClientID %>');
            var CHK = document.getElementById("<%=chklstPedidos.ClientID%>");
            var counter = 0;

            if ((cmbMDesabnasto.get_value() == '1') || (cmbMDesabnasto.get_value() == '2')) {

                for (var i = 0; i < CHK.length; i++) {
                    if (CHK[i].checked) {
                        counter++;
                    }
                }

                if (1 > counter) {
                    lbl_Val_PedidoSeleccionado.innerHTML = '*Requerido';
                    try {
                        radTabStrip.get_allTabs()[0].select();
                    }
                    catch (e) {
                        alert(e.toString());
                    }
                    alert('Datos Incompletos:: Seleccionar Pedido Desabasto');
                    validacionResult = false;
                }
            }
            // else {

            // lbl_Val_PedidoSeleccionado.innerHTML = '*Requerido2';
            //            validacionResult = false; }
        }
        catch (e) {
            alert(e.toString());
        }

        return validacionResult;
    }


    function EnviarSolicitudClients(sender, args) {
        var continuarAccion = true;
        var button = args.get_item();

        switch (button.get_value()) {
            case 'save':
                continuarAccion = ValidarControlesRequeridosCte();
                if (continuarAccion == true) {
                    alert('datos completos');
                    var submitBtn = document.getElementById('<%= btnEnviaSolicitudCliente.ClientID %>');
                    try {
                        if (submitBtn) {
                            submitBtn.click();
                        }
                    }
                    catch (e) {
                        alert(e.toString());
                    }
                    continuarAccion = true;
                }
                break;
            case 'undo':
                CancelarSolicitudClients();
                break;
        }
        args.set_cancel(!continuarAccion);
    }

    function CancelarSolicitudClients(sender, args) {
        var submitBtn = document.getElementById('<%= btnCancelaSolicitudCliente.ClientID %>');
        try {
            if (submitBtn) {
                submitBtn.click();
            }
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function ValidarControlesRequeridosAba() {
        //debugger;
        var validacionResult = true;

        var radTabStrip = $find('<%= RadTabStripAbasto.ClientID %>');

        lbl_Val_txtProveedor = document.getElementById('<%= lbl_Val_txtProveedorAba.ClientID %>');
        lbl_Val_cmbProdServicioSAT = document.getElementById('<%= lbl_Val_cmbProdServicioSATAbasto.ClientID %>');
        lbl_Val_cmbUnidadMedidaSAT = document.getElementById('<%= lbl_Val_cmbUnidadMedidaSATAbasto.ClientID %>');

        lbl_Val_txtProveedor.innerHTML = '';
        lbl_Val_cmbProdServicioSAT = '';
        lbl_Val_cmbUnidadMedidaSAT = '';

        if (ValidaObjetoRequerido($find('<%= txtProveeAba.ClientID %>'), lbl_Val_txtProveedor, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Seleccionar Proveedor');
        }
        /*
        cmbProductoSAT = $find('<%= cmbProdServicioSATAbasto.ClientID %>');
        if (cmbProductoSAT.get_value() == '-1') {
        lbl_Val_cmbProdServicioSAT.innerHTML = '*Requerido';
        try {
        radTabStrip.get_allTabs()[2].select();
        }
        catch (e) {
        alert(e.toString());
        }
        alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        validacionResult = false
        }
        */

        if (ValidaObjetoRequeridoInput(document.getElementById('<%=hfCveProdServSATAbasto.ClientID %>'), lbl_Val_cmbProdServicioSAT, 0) == false) {
            validacionResult = false;
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        }


        cmbUnidadesSAT = $find('<%= cmbUnidadMedidaSATAbasto.ClientID %>');
        if (cmbUnidadesSAT.get_value() == '-1') {
            lbl_Val_cmbUnidadMedidaSAT.innerHTML = '*Requerido';
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Unidad de Medida de SAT');
            validacionResult = false
        }

        return validacionResult;
    }



    function EnviarSolicitudAbas(sender, args) {
        var continuarAccion = true;
        var button = args.get_item();

        switch (button.get_value()) {
            case 'save':
                continuarAccion = ValidarControlesRequeridosAba();
                if (continuarAccion == true) {
                    alert('datos completos');
                    var submitBtn = document.getElementById('<%= btnEnviarAbasto.ClientID %>');
                    try {
                        if (submitBtn) {
                            submitBtn.click();
                        }
                    }
                    catch (e) {
                        alert(e.toString());
                    }
                    continuarAccion = true;
                }
                break;
            case 'undo':
                CancelarSolicitudAbas();
                break;
        }
        args.set_cancel(!continuarAccion);
    }

    function CancelarSolicitudAbas(sender, args) {
        var submitBtn = document.getElementById('<%= btnCancelarAbasto.ClientID %>');
        try {
            if (submitBtn) {
                submitBtn.click();
            }
        }
        catch (e) {
            alert(e.toString());
        }
    }


    function ValidarControlesRequeridosCte() {
        //debugger;
        var validacionResult = true;

        var radTabStrip = $find('<%= RadTabStripPrincipalCte.ClientID %>');

        //  lbl_Val_TextId_Prd = document.getElementById('<%= lbl_Val_TextId_PrdCte.ClientID %>');
        lbl_Val_TextPrd_Descrpcion = document.getElementById('<%= lbl_Val_TextPrd_DescrpcionCte.ClientID %>');
        lbl_Val_txtPresentacion = document.getElementById('<%= lbl_Val_txtPresentacionCte.ClientID %>');
        lbl_Val_txtTipoProducto = document.getElementById('<%= lbl_Val_txtTipoProductoCte.ClientID %>');
        //  lbl_Val_txtCategoria = document.getElementById('<%= lbl_Val_txtCategoriaCte.ClientID %>');
        lbl_Val_txtProveedor = document.getElementById('<%= lbl_Val_txtProveedorCte.ClientID %>');
        lbl_Val_cmbUentrada = document.getElementById('<%= lbl_Val_cmbUentradaCte.ClientID %>');
        lbl_Val_cmbUsalidaCte = document.getElementById('<%= lbl_Val_cmbUsalidaCte.ClientID %>');
        lbl_Val_txtMotivo = document.getElementById('<%= lbl_Val_txtMotivo.ClientID %>');
        lbl_Val_Vigencia = document.getElementById('<%= lbl_Val_rdpVigencia.ClientID %>');
        lbl_Val_txtFam = document.getElementById('<%= lbl_Val_txtFam.ClientID %>');

        lbl_Val_ClientesExclusivos = document.getElementById('<%= lbl_Val_ClientesExclusivos.ClientID %>');

        lbl_Val_txtUempaque = document.getElementById('<%= lbl_Val_txtUempaque.ClientID %>');

        lbl_Val_cmbProdServicioSAT = document.getElementById('<%= lbl_Val_cmbProdServicioSATCte.ClientID %>');
        lbl_Val_cmbUnidadMedidaSAT = document.getElementById('<%= lbl_Val_cmbUnidadMedidaSATCte.ClientID %>');


        //  lbl_Val_TextId_Prd.innerHTML = '';
        lbl_Val_TextPrd_Descrpcion.innerHTML = '';
        lbl_Val_txtPresentacion.innerHTML = '';
        lbl_Val_txtTipoProducto.innerHTML = '';
        //  lbl_Val_txtCategoria.innerHTML = '';
        lbl_Val_txtProveedor.innerHTML = '';
        lbl_Val_cmbUentrada.innerHTML = '';
        lbl_Val_cmbUsalidaCte.innerHTML = '';
        lbl_Val_Vigencia.innerHTML = '';
        lbl_Val_txtMotivo.innerHTML = '';
        lbl_Val_txtFam.innerHTML = '';
        lbl_Val_ClientesExclusivos.innerHTML = '';
        lbl_Val_cmbProdServicioSAT = '';
        lbl_Val_cmbUnidadMedidaSAT = '';


        //  if (ValidaObjetoRequerido($find('<%= TextId_PrdCte.ClientID %>'), lbl_Val_TextId_Prd, 0) == false) validacionResult = false
        if (ValidaObjetoRequerido($find('<%= TextPrd_DescrpcionCte.ClientID %>'), lbl_Val_TextPrd_Descrpcion, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Capturar la Descripción del Producto');
        }
        if (ValidaObjetoRequerido($find('<%= txtPresentacionCte.ClientID %>'), lbl_Val_txtPresentacion, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Capturar la Presentación del Producto');
        }
        if (ValidaObjetoRequerido($find('<%= txtTipoProductoCte.ClientID %>'), lbl_Val_txtTipoProducto, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Seleccionar el Tipo del Producto');
        }
        //  if (ValidaObjetoRequerido($find('<%= txtCategoriaCte.ClientID %>'), lbl_Val_txtCategoria, 0) == false) validacionResult = false
        if (ValidaObjetoRequerido($find('<%= txtProveedorCte.ClientID %>'), lbl_Val_txtProveedor, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Seleccionar el Proveedor del Producto');
        }
        if (ValidaObjetoRequerido($find('<%= txtMotivoSolicita.ClientID %>'), lbl_Val_txtMotivo, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Capturar el Motivo de la Orden');
        }
        if (ValidaObjetoRequerido($find('<%= txtFamCte.ClientID %>'), lbl_Val_txtFam, 0) == false) {
            validacionResult = false;
            /// no aplica porque siempre llega con una familia[aplicacion]/subfamilia seleccionada
        }

        //        if (ValidaObjetoRequerido($find('<%= TextId_SpoCte.ClientID %>'), lbl_Val_Id_Spo, 0) == false) validacionResult = false
        /*
        try{
        if (ValidaObjetoRequerido($find('<%= rdpVigencia.ClientID %>'), lbl_Val_Vigencia, 0) == false) {
        validacionResult = false;
        radTabStrip.get_allTabs()[0].select();
        alert('Datos Incompletos:: Capturar la Vigencia de la Orden');
        }
        }
        catch (e) {
        alert(e.toString());
        }
        */
        if (ValidaObjetoRequerido($find('<%= txtUempaqueCte.ClientID %>'), lbl_Val_txtUempaque, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Seleccione la Unidad de Empaque del Producto');
        }
        //aqui
        //  esta es la validacion de los clientes exlucivos
        var hidClientes = document.getElementById('<%= ddlElements.ClientID %>');
        //  $find('= ddlElements.ClientID ')
        if (ValidaObjetoRequeridoInput(hidClientes, lbl_Val_ClientesExclusivos, 0) == false) {
            validacionResult = false;
            radTabStrip.get_allTabs()[5].select();
            alert('Datos Incompletos:: Debe Especificar los Clientes Exclusivos del Producto');
        }


        cmbUentrada = $find('<%= cmbUentradaCte.ClientID %>');
        if (cmbUentrada.get_value() == '-1') {
            lbl_Val_cmbUentrada.innerHTML = '*Requerido';
            radTabStrip.get_allTabs()[0].select();
            alert('Datos Incompletos:: Seleccione la Unidad de Entrada del Producto');
            validacionResult = false
        }
        /*
        cmbUsalidaCte = $find('<%= cmbUsalidaCte.ClientID %>');
        if (cmbUentrada.get_value() == '-1') {
        lbl_Val_cmbUsalidaCte.innerHTML = '*Requerido';
        radTabStrip.get_allTabs()[0].select();
        alert('Datos Incompletos:: Seleccione la Unidad de Salida del Producto');
        validacionResult = false
        }
        
        cmbProductoSAT = $find('<%= cmbProdServicioSATCte.ClientID %>');
        if (cmbProductoSAT.get_value() == '-1') {
        lbl_Val_cmbProdServicioSAT.innerHTML = '*Requerido';
        try {
        radTabStrip.get_allTabs()[2].select();
        }
        catch (e) {
        alert(e.toString());
        }
        alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        validacionResult = false
        }
        */


        if (ValidaObjetoRequeridoInput(document.getElementById('<%=hfCveProdServSATCte.ClientID %>'), lbl_Val_cmbProdServicioSAT, 0) == false) {
            validacionResult = false;
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Producto/Servicio de SAT');
        }

        cmbUnidadesSAT = $find('<%= cmbUnidadMedidaSATCte.ClientID %>');
        if (cmbUnidadesSAT.get_value() == '-1') {
            lbl_Val_cmbUnidadMedidaSAT.innerHTML = '*Requerido';
            try {
                radTabStrip.get_allTabs()[2].select();
            }
            catch (e) {
                alert(e.toString());
            }
            alert('Datos Incompletos:: Seleccionar Unidad de Medida de SAT');
            validacionResult = false
        }

        return validacionResult;
    }

    var grabando = true;

    function EnviarSolicitudCteClient(sender, args) {
        var continuarAccion = true;
        var button = args.get_item();
        if (grabando) {
            switch (button.get_value()) {
                case 'save':
                    grabando = false;
                    continuarAccion = ValidarControlesRequeridos();
                    if (continuarAccion == true) {
                        alert('datos completos');
                        var submitBtn = document.getElementById('<%= btnEnviaSolicitud.ClientID %>');
                        try {
                            if (submitBtn) {
                                submitBtn.click();
                                //  EnviarSolicitud();
                                //  alert('post click');
                            }
                        }
                        catch (e) {
                            alert(e.toString());
                        }
                        continuarAccion = true;
                    }
                    break;
                case 'undo':
                    break;
            }
            args.set_cancel(!continuarAccion);
        }
        else {
            alert('Ya se encuentra grabando una solicitud.');
        }
    }


    //    function CancelarSolicitudCteClient(sender, args) {
    //        var submitBtn = document.getElementById('<%= btnCancelaConsulta.ClientID %>');
    //        try {
    //            if (submitBtn) {
    //                submitBtn.click();
    //            }
    //        }
    //        catch (e) {
    //            alert(e.toString());
    //        }
    //    } 


    function ValidarControlesRequeridosCons() {
        //debugger;
        var validacionResult = true;

        var TxtTipoSol = document.getElementById('<%= hfTipooSolicitudCons.ClientID %>');
        try {


            lbl_Val_TextId_Prd = document.getElementById('<%= lbl_Val_TextId_PrdCons.ClientID %>');
            lbl_Val_TextPrd_Descrpcion = document.getElementById('<%= lbl_Val_TextPrd_DescrpcionCoins.ClientID %>');
            lbl_Val_txtPresentacion = document.getElementById('<%= lbl_Val_txtPresentacionCons.ClientID %>');
            lbl_Val_txtTipoProducto = document.getElementById('<%= lbl_Val_txtTipoProductoCons.ClientID %>');
            lbl_Val_txtCategoria = document.getElementById('<%= lbl_Val_txtCategoriaCons.ClientID %>');
            lbl_Val_txtSisProp = document.getElementById('<%= lbl_Val_txtSisPropCons.ClientID %>');
            lbl_Val_txtProveedor = document.getElementById('<%= lbl_Val_txtProveedorCons.ClientID %>');
            lbl_Val_txtFamilia = document.getElementById('<%= lbl_Val_txtFamiliaCons.ClientID %>');
            lbl_Val_txtSubFamilia = document.getElementById('<%= lbl_Val_txtSubFamiliaCons.ClientID %>');
            lbl_Val_cmbUentrada = document.getElementById('<%= lbl_Val_cmbUentradaCons.ClientID %>');
            lbl_Val_cmbUSalida = document.getElementById('<%= lbl_Val_cmbUsalidaCons.ClientID %>');
            lbl_Val_txtMotivoSolicita = document.getElementById('<%= lbl_Val_txtMotivoSolicitaCons.ClientID %>');
            lbl_Val_cmbMotDesabasto = document.getElementById('<%= lbl_Val_cmbMotivoDEsabastoCons.ClientID %>');
            lbl_Val_PedidoSeleccionado = document.getElementById('<%= lblPedidoSeleccionadoCons.ClientID %>');

            lbl_Val_TextId_Prd.innerHTML = '';
            lbl_Val_TextPrd_Descrpcion.innerHTML = '';
            lbl_Val_txtPresentacion.innerHTML = '';
            lbl_Val_txtTipoProducto.innerHTML = '';
            lbl_Val_txtCategoria.innerHTML = '';
            lbl_Val_txtSisProp.innerHTML = '';
            lbl_Val_txtProveedor.innerHTML = '';
            lbl_Val_txtFamilia.innerHTML = '';
            lbl_Val_txtSubFamilia.innerHTML = '';
            lbl_Val_cmbUentrada.innerHTML = '';
            lbl_Val_cmbUSalida.innerHTML = '';
            lbl_Val_txtMotivoSolicita.innerHTML = '';
            lbl_Val_cmbMotDesabasto.innerHTML = '';
            lbl_Val_PedidoSeleccionado.innerHTML = '';

            if (TxtTipoSol.value == '3') {
                if (ValidaObjetoRequerido($find('<%= TextId_PrdCons.ClientID %>'), lbl_Val_TextId_Prd, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= TextPrd_DescrpcionCons.ClientID %>'), lbl_Val_TextPrd_Descrpcion, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtPresentacionCons.ClientID %>'), lbl_Val_txtPresentacion, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtTipoProductoCons.ClientID %>'), lbl_Val_txtTipoProducto, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtCategoriaCons.ClientID %>'), lbl_Val_txtCategoria, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= TextId_SpoCons.ClientID %>'), lbl_Val_txtSisProp, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtFamCons.ClientID %>'), lbl_Val_txtFamilia, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtSubFamCons.ClientID %>'), lbl_Val_txtSubFamilia, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtProveedorCons.ClientID %>'), lbl_Val_txtProveedor, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtMotivoSolicitaCons.ClientID %>'), lbl_Val_txtMotivoSolicita, 0) == false) validacionResult = false

                cmbUentrada = $find('<%= cmbUentradaCons.ClientID %>');
                if (cmbUentrada.get_value() == '-1') {
                    lbl_Val_cmbUentrada.innerHTML = '*Requerido';
                    validacionResult = false
                }

                cmbUsalida = $find('<%= cmbUsalidaCons.ClientID %>');
                if (cmbUentrada.get_value() == '-1') {
                    lbl_Val_cmbUSalida.innerHTML = '*Requerido';
                    validacionResult = false
                }
            }

            if (TxtTipoSol.value == '2') {
                // el tipo 2 solo permite cambiar el proveedor en su creacion, por lo q ya una vez 
                // grabado no se puede modificar salvo los precios
            }

            if (TxtTipoSol.value == '1') {
                // el tipo 1 solo cambia de precios y de motivo de desabasto

                cmbMDesabnasto = $find('<%= cmbCausaDesabastoCons.ClientID %>');
                if (cmbMDesabnasto.get_value() == '-1') {
                    lbl_Val_cmbMotDesabasto.innerHTML = '*Requerido';
                    validacionResult = false
                }

                var lstbPedidos = document.getElementById('<%= lstbPedidosCons.ClientID %>');
                if (lstbPedidos != null) {

                    var listValue = lstbPedidos.value;
                    if (listValue == "") {
                        lbl_Val_PedidoSeleccionado.innerHTML = '*Requerido';
                        validacionResult = false;
                    }
                }

                //  lstbPedidos = $find('<%= lstbPedidosCons.ClientID %>');
                //  if (lstbPedidos.get_value() == '') {
                //    lbl_Val_PedidoSeleccionado.innerHTML = '*Requerido';
                //    validacionResult = false
                //  }
            }

        }
        catch (e) {
            alert(e.toString());
        }
        return validacionResult;
    }


    function EnviarSolicitudConsClient(sender, args) {
        var continuarAccion = true;
        var button = args.get_item();
        //  alert('no, esta aca, pero estan volteados');
        switch (button.get_value()) {
            case 'save':
                continuarAccion = ValidarControlesRequeridosCons();
                if (continuarAccion == true) {
                    alert('datos completos');
                    var submitBtn = document.getElementById('<%= btnGrabasolicitud.ClientID %>');
                    try {
                        if (submitBtn) {
                            submitBtn.click();
                        }
                    }
                    catch (e) {
                        alert(e.toString());
                    }
                    continuarAccion = true;
                }
                break;
            case 'undo':
                CancelarSolicitudConsClient();
                break;
        }
        args.set_cancel(!continuarAccion);
    }

    function CancelarSolicitudConsClient(sender, args) {
        var submitBtn = document.getElementById('<%= btnCancelaConsulta.ClientID %>');
        try {
            if (submitBtn) {
                submitBtn.click();
            }
        }
        catch (e) {
            alert(e.toString());
        }
    }



    //Valida datos requeridos que dependen de la captura de otros datos al momento de insertar o actualizar un Producto
    function ValidarControlesEspeciales() {
        var validacionResult = true;

        //obtener objetos (Labels) para desplegar avisos de dato requerido...
        var lbl_Val_txtCodProd = document.getElementById('<%= lbl_Val_txtCodProd.ClientID %>');
        var lbl_val_TextId_Spo = document.getElementById('<%= lbl_val_TextId_Spo.ClientID %>');
        var lbl_val_txtSubFam = document.getElementById('<%= lbl_val_txtSubFam.ClientID %>');
        var lbl_Val_Rentabilidad = document.getElementById('<%= lbl_Val_Rentabilidad.ClientID %>');

        var lbl_Val_Amortizacion = document.getElementById('<%= lbl_Val_Amortizacion.ClientID %>');

        var lbl_Val_txtFnombre = document.getElementById('<%= lbl_Val_txtFnombre.ClientID %>');
        var lbl_Val_txtFcodigo = document.getElementById('<%= lbl_Val_txtFcodigo.ClientID %>');
        var lbl_Val_txtFdescripcion = document.getElementById('<%= lbl_Val_txtFdescripcion.ClientID %>');
        var lbl_Val_txtFpresentacion = document.getElementById('<%= lbl_Val_txtFpresentacion.ClientID %>');
        var lbl_Val_txtPnombre = document.getElementById('<%= lbl_Val_txtPnombre.ClientID %>');
        var lbl_Val_txtPcodigo = document.getElementById('<%= lbl_Val_txtPcodigo.ClientID %>');
        var lbl_Val_txtPdescripcion = document.getElementById('<%= lbl_Val_txtPdescripcion.ClientID %>');
        var lbl_Val_txtPpresentacion = document.getElementById('<%= lbl_Val_txtPpresentacion.ClientID %>');

        var lbl_val_txtAgrupadoSpo = document.getElementById('<%= lbl_val_txtAgrupadoSpo.ClientID %>');

        lbl_Val_txtCodProd.innerHTML = '';
        lbl_val_TextId_Spo.innerHTML = '';
        lbl_val_txtSubFam.innerHTML = '';
        lbl_Val_Rentabilidad.innerHTML = '';

        lbl_Val_txtFnombre.innerHTML = '';
        lbl_Val_txtFcodigo.innerHTML = '';
        lbl_Val_txtFdescripcion.innerHTML = '';
        lbl_Val_txtFpresentacion.innerHTML = '';
        lbl_Val_txtPnombre.innerHTML = '';
        lbl_Val_txtPcodigo.innerHTML = '';
        lbl_Val_txtPdescripcion.innerHTML = '';
        lbl_Val_txtPpresentacion.innerHTML = '';

        lbl_val_txtAgrupadoSpo.innerHTML = '';

        var txtTipoProducto = $find('<%= txtTipoProducto.ClientID %>');
        var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
        //si el tipo de producto es tipo accesorios (Id_Ptp == 1) y elige una opcion del combo de sistemas propietarios
        //el agrupado de equipos de sistemas propietarios es requerido
        if (txtTipoProducto.get_textBoxValue() == '1' && TextId_Spo.get_textBoxValue() != '') {
            if (ValidaObjetoRequerido($find('<%= txtAgrupadoSpo.ClientID %>'), lbl_val_txtAgrupadoSpo, 0) == false) validacionResult = false
        }

        if (txtTipoProducto.get_textBoxValue() == '1') {
            if (ValidaObjetoRequerido($find('<%= txtCodProd.ClientID %>'), lbl_Val_txtCodProd, 0) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtAgrupadoSpo.ClientID %>'), lbl_val_txtAgrupadoSpo, 0) == false) validacionResult = false
        }

        //valida que se capture codigo unico si el producto pertenece a un sistema de propietario
        //valida que se capture meses de amortización
        var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
        if (TextId_Spo.get_textBoxValue() != '') {
            if (ValidaObjetoRequerido($find('<%= txtAmortizacion.ClientID %>'), lbl_Val_Amortizacion, 1) == false) validacionResult = false
        }

        //valida que se capture una subfamilia cuando se captura una Familia
        var txtFam = $find('<%= txtFam.ClientID %>');
        if (txtFam.get_textBoxValue() != '') {
            if (ValidaObjetoRequerido($find('<%= txtSubFam.ClientID %>'), lbl_val_txtSubFam, 0) == false) validacionResult = false
        }

        //valida que se capture la rentabilidad cuando no esta activado el check de producto para compra local
        var chkComprasLocales = document.getElementById('<%= chkComprasLocales.ClientID %>');

        if (chkComprasLocales.checked == false) {
            if (ValidaObjetoRequerido($find('<%= txtRentabilidad.ClientID %>'), lbl_Val_Rentabilidad, 1) == false) validacionResult = false
        }
        else {
            //si si esta activado --> es obligatorio el sistema propietario

            if (txtTipoProducto.get_textBoxValue() == '1') {
                if (ValidaObjetoRequerido($find('<%= TextId_Spo.ClientID %>'), lbl_val_TextId_Spo, 0) == false) validacionResult = false
            }
            //si si esta activado --> todos los datos de la pestañs de compras locales son obligatorios
            if (ValidaObjetoRequerido($find('<%= txtFnombre.ClientID %>'), lbl_Val_txtFnombre, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtFcodigo.ClientID %>'), lbl_Val_txtFcodigo, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtFdescripcion.ClientID %>'), lbl_Val_txtFdescripcion, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtFpresentacion.ClientID %>'), lbl_Val_txtFpresentacion, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtPnombre.ClientID %>'), lbl_Val_txtPnombre, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtPcodigo.ClientID %>'), lbl_Val_txtPcodigo, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtPdescripcion.ClientID %>'), lbl_Val_txtPdescripcion, 4) == false) validacionResult = false
            if (ValidaObjetoRequerido($find('<%= txtPpresentacion.ClientID %>'), lbl_Val_txtPpresentacion, 4) == false) validacionResult = false
        }

        //validar que tiempo de entrega y de transporte sean multiplos de 7
        var txtTentrega = $find('<%= txtTentrega.ClientID %>');
        var txtTtransporte = $find('<%= txtTtransporte.ClientID %>');
        var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

        if (validacionResult == true) {
            if (txtTentrega.get_textBoxValue() != '') {
                var tiempoEntrega = parseFloat(txtTentrega.get_textBoxValue());
                if ((tiempoEntrega % 7) != 0) {
                    var Alerta_tiempoEntrega = radalert('El tiempo de entrega debe estar en múltiplos de 7', 600, 10, tituloMensajes);
                    validacionResult = false
                    Alerta_tiempoEntrega.add_close(
                            function () {
                                radTabStrip.get_allTabs()[1].select();
                                txtTentrega.focus();
                            });
                }
            }
        }

        if (validacionResult == true) {
            if (txtTtransporte.get_textBoxValue() != '') {
                var tiempoTransporte = parseFloat(txtTtransporte.get_textBoxValue());
                if ((tiempoTransporte % 7) != 0) {
                    var Alerta_tiempoTransporte = radalert('El tiempo de transporte debe estar en múltiplos de 7', 600, 10, tituloMensajes);
                    validacionResult = false
                    Alerta_tiempoTransporte.add_close(
                            function () {
                                radTabStrip.get_allTabs()[1].select();
                                txtTtransporte.focus();
                            });
                }
            }
        }

        return validacionResult;
    }

    //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid de precios de producto
    var datePickerFechaInicioClientId = '';
    var datePickerFechaFinClientId = '';
    var txtPrd_PesosClientId = '';

    //--------------------------------------------------------------------------------------------------
    //Cuando un botón del toolBar es clickeado
    //--------------------------------------------------------------------------------------------------
    function ToolBar_ClientClick(sender, args) {
        //debugger;
        var continuarAccion = true;
        var habilitaValidacion = false;
        var button = args.get_item();

        //habilitar/deshabilitar validators
        if (button.get_value() == 'save')
            habilitaValidacion = true;
        else {
            habilitaValidacion = false;
        }
        //                for (i = 0; i < Page_Validators.length; i++) {
        //                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
        //                }

        //debugger;
        //if (tabSeleccionada == 'Datos generales')
        switch (button.get_value()) {
            case 'new':
                //debugger;
                LimpiarControlesProducto();

                //select tab datos generales
                var RadTabStripPrincipal = $find('<%= RadTabStripPrincipal.ClientID %>');
                RadTabStripPrincipal.get_allTabs()[0].select();

                //registro nuevo -> se limpia bandera de actualización
                var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                hiddenId.value = '';

                //poner el doco en txtIdProducto
                var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
                TextId_Prd.enable();

                var urlArchivo = 'ObtenerMaximo.aspx';
                parametros = "Catalogo=CatProducto";
                parametros = parametros + "&sp=spCatCentral_Maximo";
                parametros = parametros + "&columna=Id_Prd";
                var resultado = obtenerrequest(urlArchivo, parametros);
                TextId_Prd.set_value(resultado);

                TextId_Prd.focus();
                continuarAccion = true;
                break;
            case 'save':
                //select tab datos generales
                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');
                radTabStrip.get_allTabs()[0].select();

                continuarAccion = ValidarControlesRequeridos();
                if (continuarAccion == true) {
                    continuarAccion = ValidarControlesEspeciales();
                }
                break;
        }
        args.set_cancel(!continuarAccion);
    }

    //--------------------------------------------------------------------------------------------------
    //Setea variable de pestaña del TabStrip es clickeada
    //--------------------------------------------------------------------------------------------------
    function OnClientTabSelectingHandler(sender, args) {
        tabSeleccionada = args.get_tab().get_text();
    }

    //--------------------------------------------------------------------------------------------------
    //Doble click en un Row del Grid de Precios dispara evento de edición
    //--------------------------------------------------------------------------------------------------
    function rgPrecios_ClientRowDblClick(sender, eventArgs) {
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    }

    function rgPreciosAbasto_ClientRowDblClick(sender, eventArgs) {
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    }

    function rgPreciosCte_ClientRowDblClick(sender, eventArgs) {
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el combo cmbUentrada cambia el item seleccionado
    //--------------------------------------------------------------------------------------------------
    function cmbUentrada_OnClientSelectedIndexChanged(sender, args) {
        var item = args.get_item();
        var cmbUsalida = $find('<%= cmbUsalida.ClientID %>');
        var cmbUsalida_item = cmbUsalida.findItemByValue(item.get_value())
        cmbUsalida_item.select();
    }

    function cmbUentradaCte_OnClientSelectedIndexChanged(sender, args) {
        var item = args.get_item();
        var cmbUsalida = $find('<%= cmbUsalidaCte.ClientID %>');
        var cmbUsalida_item = cmbUsalidaCte.findItemByValue(item.get_value())
        cmbUsalida_item.select();
    }

    function cmbUentradaCons_OnClientSelectedIndexChanged(sender, args) {
        var item = args.get_item();
        var cmbUsalida = $find('<%= cmbUentradaCons.ClientID %>');
        var cmbUsalida_item = cmbUsalidaCte.findItemByValue(item.get_value())
        cmbUsalida_item.select();
    }

    //--------------------------------------------------------------------------------------------------
    // Cuando TextId_Prd TextPrd_Descrpcion pierde el foco, establece el titulo del producto
    //--------------------------------------------------------------------------------------------------
    function TextId_Prd_OnBlur(sender, args) {
        EstablecerLabelTituloProducto();
    }
    function TextId_PrdCte_OnBlur(sender, args) {
        EstablecerLabelTituloProductoCte();
    }

    function TextId_PrdCons_OnBlur(sender, args) {
        EstablecerLabelTituloProductoCons();
    }

    function TextPrd_DescrpcionCons_OnBlur(sender, args) {
        EstablecerLabelTituloProducto();
    }

    function TextPrd_Descrpcion_OnBlur(sender, args) {
        EstablecerLabelTituloProducto();
    }

    function TextPrd_DescrpcionCte_OnBlur(sender, args) {
        EstablecerLabelTituloProductoCte();
    }

    function EstablecerLabelTituloProducto() {
        var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
        var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
        var TextPrd_Descrpcion = $find('<%= TextPrd_Descrpcion.ClientID %>');

        var string_variable = TextPrd_Descrpcion.get_value();

        var intIndexOfMatch = string_variable.indexOf("'");
        while (intIndexOfMatch != -1) {
            string_variable = string_variable.replace("'", "")
            intIndexOfMatch = string_variable.indexOf("'");
        }
        TextPrd_Descrpcion.set_value(string_variable);
        label.innerHTML = TextId_Prd.get_textBoxValue() + ' - ' + TextPrd_Descrpcion.get_textBoxValue();
    }

    function contiene(buscado) {
        var txtProductosPadre = document.getElementById('<%= txtProductosMismoPadre.ClientID %>');
        try {
            var string_variable = '...' + txtProductosPadre.value;

            return string_variable.indexOf(buscado)
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function contieneAbasto(buscado) {
        var txtProductosPadre = document.getElementById('<%= txtProductosMismoPadreAbasto.ClientID %>');
        try {   // ~
            var string_variable = '...' + txtProductosPadre.value;
            // alert(buscado + '  ::  ' + string_variable);
            return string_variable.indexOf(buscado)
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function contieneCte(buscado) {
        var txtProductosPadre = document.getElementById('<%=  txtProductosMismoPadreCte.ClientID %>');
        try {
            var string_variable = '...' + txtProductosPadre.get_textBoxValue();
            alert(string_variable);
            return string_variable.indexOf(buscado)
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function EstablecerLabelTituloProductoDesAbasto() {
        try {
            var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
            var TextId_Prd = document.getElementById('<%= TextId_Prd.ClientID %>');
            var txtProveedor = document.getElementById('<%= txtProveedor.ClientID %>');

            var txtCDI = document.getElementById("<%= txtCentrosDist.ClientID %>");

            var lblPrd_Descrpcion = document.getElementById('<%= TextPrd_Descrpcion.ClientID %>');
            var t6xtCodigo = document.getElementById('<%= txtCodProd.ClientID %>');

            // '00000' + rigth de este lado
            var string_CDI = "000" + txtCDI.value;
            var string_proveedor = "000000" + txtProveedor.value;

            if (txtProveedor.length > 5) {
                alert("Proveedor no valido. Favor de contactar a Sistemas");
                return false();
            }

            var string_producto = "00000" + TextId_Prd.value;

            var string_variable = lblPrd_Descrpcion.value;

            label.innerHTML = string_CDI.slice(-3) + "1" + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
            t6xtCodigo.value = string_CDI.slice(-3) + "1" + string_proveedor.slice(-5) + string_producto.slice(-6)
        }
        catch (e) {
            alert(e.toString());
        }
    }


    function EstablecerLabelTituloProductoAbasto() {
        try {

            var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
            var lblCProd = document.getElementById('<%= lblCodProd.ClientID %>');
            var TextId_Prd = document.getElementById('<%= lblId_Prd.ClientID %>');
            var txtProveedor = document.getElementById('<%= txtProveeAba.ClientID %>');

            var txtCDI = document.getElementById("<%= txtCentrosDist.ClientID %>");

            var lblPrd_Descrpcion = document.getElementById('<%= lblPrd_DescrpcionAbasto.ClientID %>');


            if (txtProveedor.length > 5) {
                alert("Proveedor no valido. Favor de contactar a Sistemas");
                return false();
            }

            // '00000' + rigth de este lado
            var string_CDI = "000" + txtCDI.value;
            var string_proveedor = "000000" + txtProveedor.value;

            var string_producto = "00000" + TextId_Prd.innerHTML;

            var string_variable = lblPrd_Descrpcion.value;

            label.innerHTML = string_CDI.slice(-3) + "2" + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
            lblCProd.innerHTML = string_CDI.slice(-3) + "2" + string_proveedor.slice(-5) + string_producto.slice(-6);
            //  alert(lblCProd.innerHTML);
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function EstablecerLabelTituloProductoCte() {
        try {

            var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
            var TextId_Prd = $find('<%= TextId_PrdCte.ClientID %>');
            var txtProveedor = $find('<%= txtProveedorCte.ClientID %>');

            var txtCDI = document.getElementById("<%= txtCentrosDist.ClientID %>");

            var TextPrd_Descrpcion = $find('<%= TextPrd_DescrpcionCte.ClientID %>');

            var txtCodProducto = document.getElementById("<%= txtCodProdCte.ClientID %>");


            if (txtProveedor.length > 5) {
                alert("Proveedor no valido. Favor de contactar a Sistemas");
                return false();
            }

            // '00000' + rigth de este lado
            var string_CDI = "000" + txtCDI.value;  // dice q its not a fucntion
            var string_proveedor = "000000" + txtProveedor.get_textBoxValue();
            var string_producto = "00000" + TextId_Prd.get_textBoxValue();

            var string_variable = TextPrd_Descrpcion.get_value();

            var intIndexOfMatch = string_variable.indexOf("'");
            while (intIndexOfMatch != -1) {
                string_variable = string_variable.replace("'", "")
                intIndexOfMatch = string_variable.indexOf("'");
            }
            TextPrd_Descrpcion.set_value(string_variable);

            label.innerHTML = string_CDI.slice(-3) + "3" + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + TextPrd_Descrpcion.get_textBoxValue();
            txtCodProducto.value = string_CDI.slice(-3) + "3" + string_proveedor.slice(-5) + string_producto.slice(-6);
        }
        catch (e) {
            alert(e.toString());
        }
    }


    function EstablecerLabelTituloProductoCons() {
        var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
        var TextId_Prd = $find('<%= TextId_PrdCons.ClientID %>');
        var TextPrd_Descrpcion = $find('<%= TextPrd_DescrpcionCons.ClientID %>');

        var string_variable = TextPrd_Descrpcion.get_value()

        var intIndexOfMatch = string_variable.indexOf("'");
        while (intIndexOfMatch != -1) {
            string_variable = string_variable.replace("'", "")
            intIndexOfMatch = string_variable.indexOf("'");
        }
        TextPrd_Descrpcion.set_value(string_variable);
        label.innerHTML = TextId_Prd.get_textBoxValue() + ' - ' + TextPrd_Descrpcion.get_textBoxValue();
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbTipoProducto
    //--------------------------------------------------------------------------------------------------
    function cmbTipoProducto_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtTipoProducto.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbTipoProductoCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtTipoProductoCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbTipoProductoCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtTipoProductoCte.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el txtTipoProducto pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtTipoProducto_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbTipoProducto.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El tipo de producto con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtTipoProductoCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbTipoProductoCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El tipo de producto con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }


    function txtTipoProductoCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbTipoProductoCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El tipo de producto con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbSisProp
    //--------------------------------------------------------------------------------------------------
    function cmbSisProp_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= TextId_Spo.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbSisPropCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= TextId_SpoCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbSisPropCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= TextId_SpoCte.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el TextId_Spo pierde el foco
    //--------------------------------------------------------------------------------------------------
    function TextId_Spo_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSisProp.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El Sistema de propietario con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function TextId_SpoCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSisPropCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El Sistema de propietario con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function TextId_SpoCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSisPropCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El Sistema de propietario con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbCategoria
    //--------------------------------------------------------------------------------------------------
    function cmbCategoria_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtCategoria.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbCategoriaCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtCategoriaCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbCategoriaCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtCategoriaCte.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }
    //--------------------------------------------------------------------------------------------------
    //Cuando el txtCategoria pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtCategoria_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbCategoria.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La categoría con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtCategoriaCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbCategoriaCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La categoría con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtCategoriaCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbCategoriaCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La categoría con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbFam
    //--------------------------------------------------------------------------------------------------
    function cmbFam_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;

        var item = eventArgs.get_item();
        var textBox = $find('<%= txtFam.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
                //Llenar combo de subfamilias
                var combo = $find("<%= cmbSubFam.ClientID %>");
                combo.clearItems();
                var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                comboItem.set_value('-1');
                comboItem.set_text('-- Seleccionar --');
                combo.trackChanges();
                combo.get_items().add(comboItem);
                comboItem.select();
                combo.commitChanges();
                for (i = 0; i < arregloSubFamilias[0].length; i++) {
                    if (arregloSubFamilias[0][i] == item.get_value()) {//Sila subfamilia pertnece a la familia seleccionada
                        comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_value(arregloSubFamilias[1][i]);
                        comboItem.set_text(arregloSubFamilias[2][i]);
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        combo.commitChanges();
                    }
                }
            }
            else
                textBox.set_value('');
    }

    function cmbFamCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;

        var item = eventArgs.get_item();
        var textBox = $find('<%= txtFamCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
                //Llenar combo de subfamilias
                var combo = $find("<%= cmbSubFamCons.ClientID %>");
                combo.clearItems();

                var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                comboItem.set_value('-1');
                comboItem.set_text('-- Seleccionar --');
                combo.trackChanges();
                combo.get_items().add(comboItem);

                comboItem.select();
                combo.commitChanges();

                for (i = 0; i < arregloSubFamiliasCons[0].length; i++) {
                    if (arregloSubFamiliasCons[0][i] == item.get_value()) {//Sila subfamilia pertnece a la familia seleccionada
                        comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_value(arregloSubFamiliasCons[1][i]);
                        comboItem.set_text(arregloSubFamiliasCons[2][i]);
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        combo.commitChanges();
                    }
                }
            }
            else
                textBox.set_value('');
    }

    function cmbFamCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;

        var item = eventArgs.get_item();
        var textBox = $find('<%= txtFamCte.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
                //Llenar combo de subfamilias
                var combo = $find("<%= cmbSubFamCte.ClientID %>");
                combo.clearItems();
                var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                comboItem.set_value('-1');
                comboItem.set_text('-- Seleccionar --');
                combo.trackChanges();
                combo.get_items().add(comboItem);

                comboItem.select();
                combo.commitChanges();

                for (i = 0; i < arregloSubFamiliasCte[0].length; i++) {
                    if (arregloSubFamiliasCte[0][i] == item.get_value()) {//Sila subfamilia pertnece a la familia seleccionada
                        comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_value(arregloSubFamiliasCte[1][i]);
                        comboItem.set_text(arregloSubFamiliasCte[2][i]);
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        combo.commitChanges();
                    }
                }
            }
            else
                textBox.set_value('');
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el txtFam pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtFam_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbFam.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtFamCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbFamCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtFamCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbFamCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbSubFam
    //--------------------------------------------------------------------------------------------------
    function cmbSubFam_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtSubFam.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }


    function cmbSubFamCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtSubFamCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    function cmbSubFamCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtSubFamCte.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1')
                textBox.set_value(item.get_value());
            else
                textBox.set_value('');
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el txtSubFam pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtSubFam_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSubFam.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La subfamilia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtSubFamCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSubFamCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La subfamilia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtSubFamCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbSubFamCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La subfamilia con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }


    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbProveedor
    //--------------------------------------------------------------------------------------------------
    function cmbCausaDesabasto_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= cmbCausaDesabasto.ClientID %>');

        if (item != null)
            if (item.get_value() != '') {
                // evalua si fueron los primero motivos
                try {
                    var divPedidosRefAbasto2 = document.getElementById('<%= divPedidosRefAbasto.ClientID %>')
                    var lstbPedidos = document.getElementById('<%= lstbPedidos.ClientID %>')


                    if (item.get_value() > 2) {
                        //esconde la parte de los pedidos
                        divPedidosRefAbasto2.style.display = 'none';
                        lstbPedidos.style.display = 'none';
                    }
                    else {
                        divPedidosRefAbasto2.style.display = '';
                        lstbPedidos.style.display = '';
                    }
                }
                catch (e) {
                    alert(e.toString());
                }

            }

    }


    function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtProveedor.ClientID %>');
        var txtBxCodigo = document.getElementById('<%= txtCodProd.ClientID %>');
        var combo = $find("<%= cmbProveedor.ClientID %>");

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());

                EstablecerLabelTituloProductoDesAbasto();

                if (contiene(txtBxCodigo.value) != -1) {
                    alert('Ya existe una solicitud de compra con el mismo Producto.');

                    textBox.set_value('');
                    LimpiarComboSelectIndex0(combo);
                    return;
                }

            }
            else
                textBox.set_value('');
    }

    function cmbProductosHabiliCompraLocal_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        try {
            var item = eventArgs.get_item();
            var textBox = document.getElementById('<%= hfProductoLocal.ClientID %>');
            var boton = document.getElementById('<%= btnBuscaProductoLocal.ClientID %>');

            if (item != null)
                if (item.get_value() != '-1') {
                    textBox.value = item.get_value();
                    boton.click();
                }
                else
                    textBox.set_value('');
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function cmbProveedorAba_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        try {
            var item = eventArgs.get_item();
            var textBox = $find('<%= txtProveeAba.ClientID %>');
            var boton = $find('<%= btnRefTituloAba.ClientID %>');
            var lblCPrd = document.getElementById('<%= lblCodProd.ClientID %>');
            var combo = $find("<%= cmbProveedorAba.ClientID %>");

            if (item != null)
                if (item.get_value() != '-1') {
                    textBox.set_value(item.get_value());

                    EstablecerLabelTituloProductoAbasto();

                    //  alert('::  ' + lblCPrd.innerHTML);
                    var codnvProdA = lblCPrd.innerHTML;
                    if (contieneAbasto(codnvProdA) != -1) {
                        alert('Ya existe una solicitud de compra con el mismo Producto.');

                        textBox.set_value('');
                        LimpiarComboSelectIndex0(combo);
                        return;
                    }
                }
                else
                    textBox.set_value('');
        }
        catch (e) {
            alert(e.toString());
        }
    }

    function cmbProveedorCons_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtProveedorCons.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
            }
            else
                textBox.set_value('');
    }

    function cmbProveedorCte_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtProveedorCte.ClientID %>');
        var combo = $find("<%= cmbProveedorCte.ClientID %>");
        if (item != null) {

            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
                try {
                    //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                    EstablecerLabelTituloProductoCte();

                    //                        if (contieneCte(txtCodProducto.value) != -1) {
                    //                            alert('Ya existe una solicitud de compra con el mismo Producto.');

                    //                            textBox.set_value('');
                    //                            LimpiarComboSelectIndex0(combo);
                    //                            return;
                    //                        }

                }
                catch (e) {
                    alert(e.toString());
                }
            }
            else {
                textBox.set_value('');
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el txtProveedor pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtProveedor_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbProveedor.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;

                //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                txtPnombre = $find("<%= txtProveedor.ClientID %>");
                txtPnombre.set_value(item.get_text());

                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El proveedor con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtProveedorAba_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbProveedorAba.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;

                //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                txtPnombre = $find("<%= txtProveeAba.ClientID %>");
                txtPnombre.set_value(item.get_text());

                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El proveedor con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtProveedorCons_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbProveedorCons.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;

                //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                txtPnombre = $find("<%= txtProveedorCons.ClientID %>");
                txtPnombre.set_value(item.get_text());

                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El proveedor con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    function txtProveedorCte_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbProveedorCte.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                try {
                    itemSelect = true;
                    combo.get_items().getItem(i).select();
                    EstablecerLabelTituloProductoCte();

                }
                catch (e) {
                    alert(e.toString());
                }
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'El proveedor con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }
        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando se selecciona un opcion del cmbRentabilidad
    //--------------------------------------------------------------------------------------------------
    function cmbRentabilidad_ClientSelectedIndexChanged(sender, eventArgs) {
        //debugger;
        var item = eventArgs.get_item();
        var textBox = $find('<%= txtRentabilidad.ClientID %>');

        if (item != null)
            if (item.get_value() != '-1') {
                textBox.set_value(item.get_value());
            }
            else
                textBox.set_value('');
    }

    //--------------------------------------------------------------------------------------------------
    //Cuando el txtRentabilidad pierde el foco
    //--------------------------------------------------------------------------------------------------
    function txtRentabilidad_OnBlur(sender, args) {
        //debugger;
        var textBox = sender;
        var combo = $find("<%= cmbRentabilidad.ClientID %>");

        var itemSelect = false;
        for (var i = 0; i < combo.get_items().get_count(); i++) {
            var item = combo.get_items().getItem(i);
            if (textBox.get_value() == item.get_value()) {
                itemSelect = true;
                combo.get_items().getItem(i).select();
                break;
            }
        }

        if (combo.get_value() == '-1' || combo.get_value() == '') {
            if (textBox.get_value() != '') {
                var mens = 'La rentabilidad con clave ' + textBox.get_value() + ' no existe';
                textBox.set_value('');
                radalert(mens, 600, 10, tituloMensajes);
            }
        }

        if (!itemSelect)
            LimpiarComboSelectIndex0(combo)
    }


    function KeyPress(sender, eventArgs) {
        var c = eventArgs.get_keyCode();
        //debugger;
        //                if (c == 39)
        //                    eventArgs.set_cancel(true);
    }
    function ddlElementsCons_onblur() {

    }

</script>

<script type="text/javascript">
    function HistorialPrecios(sender, args) {
        try {
        var iprd = <%= strIdPrd %>;
        var url = "ComprasLocalesHistorialPrecios.aspx?idPrd=" + iprd;
        popupWindow = window.open(url,'popUpWindow','height=250,width=500,left=100,top=100,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=yes');
	 }
        catch (e) {
            alert(e.toString());
        }
    }


    function Fechainicio01_OnBlur(sender, args) {
        try {
            var fecc = document.getElementById('<%= rgPrecios.ClientID %>');
            alert('cambiar la fecha');

            alert(this);

            var d = new Date();
            alert(d);
            var year = d.getFullYear();
            var month = d.getMonth();
            var day = d.getDate();
            var c = new Date(year + 1, month, day)

            alert(c.toString());

            //  datePickerFechaInicio
            //  datePickerFechaFin
            }
        catch (e) {
            alert(e.toString());
        }
    }

</script>

<table id="TblEncabezado" style=" font-family: verdana; font-size: 8pt" runat="server"
        width="99%" >
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="LabelCDI81" runat="server" Text="Centro de distribucion "></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <div id="dvCmbCentros" runat="server" >
                    <telerik:RadComboBox ID="cmbCentrosDist" MaxHeight="300px" runat="server" 
                        OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                    
                </div>
                <input type="hidden" id="txtCentrosDist" name="txtCentrosDist" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 90%; text-align: center" colspan="4">
                <asp:Label ID="lblTituloProducto" runat="server" CssClass="tituloProducto" Font-Size="28px"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                
            </AjaxSettings>
        </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal" style="font-family: Verdana; font-size: 8pt" runat="server">
        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenRefrescapagina" runat="server" />
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <table>
                        <tr>
                            <td colspan="4">
                                <table cellspacing="3" cellpadding="0">
                                    <tr>
                                        <td><asp:Label ID="Label7" runat="server" Text="Motivo para la Compra Local" /></td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCategorias" runat="server" Width="420px" MaxHeight="300px" AutoPostBack="True"
                                                LoadingMessage="Cargando" onSelectedIndexChanged="cmbCategorias_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="10px">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="cmbCategorias" Display="Dynamic" ErrorMessage="*Requerido" 
                                                ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="Generar"></asp:RequiredFieldValidator>
                                        </td>
                                        <td><div style="visibility:hidden">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                                ToolTip="Confirmar" OnClick="ImageButton1_Click"
                                                ValidationGroup="Generar" />
                                                </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="10px" colspan="4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div runat="server" id="divActivacion" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <telerik:radtoolbar id="toolbarop1" runat="server" width="100%" dir="rtl" onbuttonclick="EnviarSolicitud" onclientbuttonclicked="EnviarSolicitudCteClient" >
                                    <Items>
                                        <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false"/>
                                        <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                    </Items>
                                </telerik:radtoolbar>
                                <table>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lbldivAc01" runat="server" Text="Código Key del Producto en Desabasto"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="450px" MaxLength="20"/>
                                            <asp:HiddenField ID="hfCustomerId" runat="server" />
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" Text="Seleccionar" runat="server" OnClick="Submit" /></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Código de abasto del producto"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodigoUsadoProd" runat="server" Width="200px" ></asp:TextBox>
                                            <asp:HiddenField ID="hfNumSolicitud" runat="server" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="right">&nbsp;
                                            <asp:Label ID="lblNumSolicitud" runat="server"  CssClass="tituloProducto"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="4">
                                            <div runat="server" id="formularioProductos" style="margin-left: 10px; margin-right: 10px;">
                                                <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                                                    SelectedIndex="0" TabIndex="-1">
                                                    <Tabs>
                                                        <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewParametros" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewIndicadores" Text="<u>E</u>xistencia Inv" AccessKey="E" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewDetalles" Text="<u>P</u>recios" AccessKey="P" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewSAT" Text="SA<u>T</u>" AccessKey="T" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewCompLocal" Text="Proveedor <u>C</u> Locales" AccessKey="C" visible="false" >
                                                        </telerik:RadTab>
                                                    </Tabs>
                                                </telerik:RadTabStrip>
                                                <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="0"
                                                    Width="800px">
                                                    <!-- Aqui empieza el contenido de los tabs--->
                                                    <telerik:RadPageView ID="RadPageViewDGrales" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <table>
                                                                        <!--Tab 1  Tabla 1-->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" Text="Código del producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TextId_Prd" name="TextId_Prd" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text="Código de abasto del producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCodProd" runat="server" Width="200px" Enabled="false">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" Enabled="false" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="lbl_Val_UnidadesDisponibles" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_TextId_Prd" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td></td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtCodProd" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table border=0 cellpadding=1 cellspacing=1>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Text="Descripción"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadTextBox ID="TextPrd_Descrpcion" runat="server" Width="456px" MaxLength="100" Enabled="false">
                                                                                    
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkProductoNuevo" runat="server" Text="Producto nuevo" Checked="true" Enabled="false" /></div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">&nbsp;
                                                                                <asp:Label ID="lbl_Val_TextPrd_Descrpcion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" Text="Presentación"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadNumericTextBox ID="txtPresentacion" runat="server" Width="70px" MaxLength="5"
                                                                                    MinValue="1" Enabled="false">
                                                                                    <NumberFormat DecimalDigits="2" ></NumberFormat>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPresentacion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                <asp:Label ID="Label2" runat="server" Text="Tipo de producto"></asp:Label>
                                                                            </td>
                                                                            <td> 
                                                                                <telerik:RadNumericTextBox ID="txtTipoProducto" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1" Enabled="false" >
                                                                                    <NumberFormat DecimalDigits="0" ></NumberFormat>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                           <td >
                                                                                <telerik:RadComboBox ID="cmbTipoProducto" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" LoadingMessage="Cargando..." MaxHeight="200px" Enabled="false">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td><asp:Label ID="lbl_Val_txtTipoProducto" runat="server" ForeColor="Red"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label8" runat="server" Text="Sistemas propietarios"  visible="false"></asp:Label>
                                                                                <telerik:RadNumericTextBox ID="TextId_Spo" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1" Enabled="false"  visible="false"> 
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbSisProp" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" Enabled="false" LoadingMessage="Cargando..." MaxHeight="200px"  visible="false">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_val_TextId_Spo" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label45" runat="server" Text="Agrupado de equipos de sistemas propietarios"  visible="false"></asp:Label>
                                                                                <telerik:RadNumericTextBox ID="txtAgrupadoSpo" runat="server" MaxLength="9" Width="70px"
                                                                                    MinValue="0" Enabled="false"  visible="false">
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:Label ID="lbl_val_txtAgrupadoSpo" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td  colspan="4">
                                                                                <asp:Label ID="Label9" runat="server" Text="Categoría de producto"  visible="false"></asp:Label>
                                                                                <telerik:RadNumericTextBox ID="txtCategoria" runat="server" MaxLength="9" MinValue="1"
                                                                                    Width="70px" Enabled="false" visible="false">
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbCategoria" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                                    Enabled="false" visible="false"
                                                                                    Width="250px" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_txtCategoria" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                <asp:Label ID="Label10" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2" nowrap>
                                                                                <telerik:RadComboBox ID="cmbFam" runat="server" Width="450px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                                                    MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" 
                                                                                    Enabled="false" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                              <div style="visibility:hidden">
                                                                                <telerik:RadNumericTextBox ID="txtFam" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1" Enabled="false">
                                                                                </telerik:RadNumericTextBox> </div>
                                                                           </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                <asp:Label ID="Label11" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                            <!-- </td>
                                                                            <td>-->
                                                                                <telerik:RadComboBox ID="cmbSubFam" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" Enabled="false" 
                                                                                    LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                             <div style="visibility:hidden">
                                                                                <telerik:RadNumericTextBox ID="txtSubFam" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1" Enabled="false">
                                                                                </telerik:RadNumericTextBox> </div>
                                                                                <asp:Label ID="lbl_val_txtSubFam" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label12" runat="server" Text="Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtProveedor_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:TextBox ID="txtProductosMismoPadre" runat="server" style='width:10px;visibility:hidden;'  ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtProveedor" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table>
                                                                        <!--Tab 1 Tabla 3-->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label13" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUentrada" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" Enabled="false" MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_cmbUentrada" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label14" runat="server" Text="Factor de conversión"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFactorConversion" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0" Enabled="false">
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label15" runat="server" Text="Unidad de salida"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUsalida" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" MaxHeight="200px"  Enabled="false">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label16" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtUempaque" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0"  Enabled="false">
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr><td colspan="4">&nbsp;</td></tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Labeldesabastocausa" runat="server" Text="Causa del desabasto"></asp:Label>
                                                                            </td>
                                                                            <td  colspan="3">
                                                                                <telerik:RadComboBox ID="cmbCausaDesabasto" runat="server" Width="400px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Desc_CausaDesAbasto"
                                                                                    DataValueField="Id" MaxHeight="300px"  Autopostback="true"
                                                                                    onSelectedIndexChanged="cmbCausaDesabasto_SelectedIndexChanged"
                                                                                    >
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_cmbMotivoDEsabasto" runat="server" ForeColor="Red"></asp:Label>
                                                                                <telerik:RadTextBox Runat=server ID="txtMotivoDesabasto" MaxLength="250" RenderMode="Lightweight" TextMode="MultiLine" Width="4px"  Height="1px" visible="false" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr><td colspan="4">&nbsp;</td></tr>
                                                                        <tr>
                                                                            <td valign="top" colspan="8">
                                                                                <div id="divPedidosRefAbasto" runat="server" >
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="Label812" runat="server" Text="Pedido Desabastecido"></asp:Label>
                                                                                                <br /><asp:Label ID="lblPedidoSeleccionado" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                                                                                                <input id="hddPedidoAbasto" type="hidden"  name="hddPedidoAbasto" runat="server" />
                                                                                            </td>
                                                                                            <td  colspan="3">
                                                                                                <table border="0" cellpadding="1" cellspacing="1">
                                                                                                    <tr>
                                                                                                        <td colspan="3"> 
                                                                                                        <div id="divSegmento" style="width: 450px; height: 120px; overflow-y: scroll; ">
                                                                                                            <asp:CheckBoxList runat="server" ID="chklstPedidos" AutoPostBack="false" 
                                                                                                                RepeatColumns="3" CellSpacing="3" CellPadding="3" Width="400px"/>
                                                                                                        </div>    
                                                                                                                <input type="hidden" id="hdn1" value="yes" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                         <td>
                                                                                                            <div style=" visibility:hidden">
                                                                                                                <asp:ListBox ID="lstbPedidos" runat="server" Width="20px" Rows="6"  visible="false" OnDblClick="JavaScript: alert('doblecklick');" ></asp:ListBox>
                                                                                                                <input name="btnAgregaPedido" value="->>" onclick="JavaScript: lstbPedidos_Click();"  type="button" style="visibility:hidden"/>
                                                                                                                <asp:TextBox id="txtValuesPedidos" runat="server" Width="2px" style="visibility:hidden"/><br />
                                                                                                                <input name="btnLimpiaPedidos" value="Limpiar" onclick="JavaScript: QuitabPedidos();"  type="button" style="visibility:hidden" />
                                                                                                                <asp:ListBox ID="lstPedidosSeleccionados" runat="server" Width="20px" Rows="6"  name="lstPedidosSeleccionados" visible="false" > </asp:ListBox>
                                                                                                                <asp:TextBox ID="txtListadSelecionados" runat="server" Rows="6" TextMode="MultiLine" visible="false" ReadOnly="true" BorderStyle="Solid" ></asp:TextBox>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewParametros" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <strong>
                                                                                    <asp:Label ID="Label17" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label18" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInvSeguridad" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkSistProp" runat="server" Text="Aparato de sistema propietario" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label19" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTentrega" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label46" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtPlanAbasto" runat="server" Width="150px" MaxLength="20">
                                                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                                                 <ClientEvents OnKeyPress="SoloAlfanumerico"></ClientEvents>
                                                                                 </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label47" runat="server" Text="Minimo de compra"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtMinCompra" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label20" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTtransporte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label21" runat="server" Text="Rentabilidad"></asp:Label>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtRentabilidad" runat="server" Width="20px"
                                                                                    MaxLength="1">
                                                                                    <ClientEvents OnKeyPress="SoloAlfabetico" OnBlur="txtRentabilidad_OnBlur" />
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadComboBox ID="cmbRentabilidad" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbRentabilidad_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_Rentabilidad" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" style="text-align: right">
                                                                                <asp:CheckBox ID="chkComprasLocales" runat="server" Text="Compras locales" Enabled="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table>
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label22" runat="server" Text="Meses de amortización"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAmortizacion" runat="server" Width="70px" MaxLength="3"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_Amortizacion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label23" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtPesos" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label24" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtExistencia" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label25" runat="server" Text="Ubicación"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtUbicacion" runat="server" Width="70px"
                                                                                    MaxLength="5">
                                                                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label44" runat="server" Text="Contribuci&oacute;n"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtContribucion" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblPorUtilidades" runat="server" Text="Porcentaje de participaci&oacute;n de utilidades"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtPorUtilidades" runat="server" Width="70px" MaxLength="3"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewIndicadores" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label26" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                            <td style="width: 20px">
                                                                            </td>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label27" runat="server" Text="Inventarios"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label28" runat="server" Text="Asignado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAsignado" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label29" runat="server" Text="Inicial"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInicial" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label30" runat="server" Text="Ordenado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtOrdenado" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label33" runat="server" Text="Final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFinal" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label31" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTransito" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label32" runat="server" Text="F&iacute;sico"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFisico" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewDetalles" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td><asp:HiddenField ID="hdfAAA" runat="server" />
                                                                    <asp:HiddenField ID="hdFechaInicial" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <p style="font-family: Verdana; font-size:small; font-style:italic">
                                                                                    <a href="JavaScript:HistorialPrecios()" id="lnkHistorialPreciosCodigo" >Historial de Precios</a>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                    <telerik:RadGrid ID="rgPrecios" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                        PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                        OnNeedDataSource="grdPrecios_NeedDataSource" OnUpdateCommand="grdPrecios_UpdateCommand"
                                                                                        OnPreRender="grdPrecios_PreRender" OnItemDataBound="grdPrecios_ItemDataBound"  
                                                                                        OnPageIndexChanged="grdPrecios_PageIndexChanged">
                                                                                        <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                            EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                            Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                            <ExpandCollapseColumn Visible="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                    UniqueName="Prd_FechaInicio">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaInicio" runat="server" MinDate="1900-01-01" enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaInicio") %>' >
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaInicio" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaFin" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaFin" runat="server" MinDate="1900-01-01"  enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaFin" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                    UniqueName="Pre_Descripcion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecio" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioEdit" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                            Font-Bold="true" />
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_Pesos" runat="server" Text='<%# Eval("Prd_Pesos") %>' 
                                                                                                            />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadNumericTextBox ID="txtPrd_Pesos" runat="server" Width="100px" MaxLength="9"
                                                                                                            MinValue="0" Text='<%# Eval("Prd_Pesos") %>' >
                                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                        </telerik:RadNumericTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                    UniqueName="Prd_PreDescripcion" Display="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PreDescripcion" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcion" runat="server"
                                                                                                            Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                        </telerik:RadTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                    EditText="Editar" HeaderText="Editar">
                                                                                                </telerik:GridEditCommandColumn>
                                                                                            </Columns>
                                                                                            <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                                InsertCaption="Agregar nuevo precio de producto">
                                                                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                    BorderColor="#000000" BorderWidth="1" />
                                                                                                <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                                <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                    EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                                </EditColumn>
                                                                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                            </EditFormSettings>
                                                                                        </MasterTableView>
                                                                                        <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                            LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                            PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                        <GroupingSettings CaseSensitive="False" />
                                                                                        <ClientSettings>
                                                                                            <ClientEvents OnRowDblClick="rgPrecios_ClientRowDblClick" />
                                                                                            <Selecting AllowRowSelect="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </telerik:RadAjaxPanel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewSAT" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label819" runat="server" Text="Unidad de Medida (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUnidadMedidaSATDesabasto" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="300px" onSelectedIndexChanged="cmbUnidadMedidaSATDesabasto_SelectedIndexChanged" AutoPostBack="True" >
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lblcmbUnidadMedidaSATDesabasto" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label826" runat="server" Text="Producto/Servicios (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>                                                                                
                                                                                <input ID="txtSearchProdServSAT"  type="text"  runat="server" name="txtSearchProdServSAT" style='width:650px' onchange="cambiatexto()"  />
                                                                                <input id="hfCveProdServSAT" type="hidden"  name="hfCveProdServSAT" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbProdServicioSATDesabasto" runat="server" Width="5px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="10px" visible="false" onSelectedIndexChanged="cmbProdServicioSATDesabasto_SelectedIndexChanged" AutoPostBack="True">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lblcmbProdServicioSATDesabasto" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewCompLocal" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label34" runat="server" Text="Fabricante"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label35" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFnombre" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFnombre" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label36" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFcodigo" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFcodigo" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label37" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFdescripcion" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label38" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFpresentacion" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label39" runat="server" Text="Proveedor"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label40" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPnombre" runat="server" Width="150px" visible="false"
                                                                                    MaxLength="100" >
                                                                                </telerik:RadTextBox>
                                                                                 <asp:TextBox ID="txtSearchProv" runat="server" Width="300px" MaxLength="6" />
                                                                                  <asp:HiddenField ID="hfProviderId" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPnombre" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label41" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPcodigo" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPcodigo" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label42" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPdescripcion" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label43" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPpresentacion" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>  
                                                </telerik:RadMultiPage>
                                                <br />
                                                <asp:HiddenField ID="hddPrecioCostoCodigo" runat="server" />
                                                <asp:HiddenField ID="hddPrecioAAACodigo" runat="server" />
                                                <br />
                                                <span style="text-align:right">
                                                    <div id="ver_off1" style="display: none">
                                                    <asp:Button ID="btnEnviaSolicitud" Text="." runat="server" OnClick="EnviarSolicitud" Autopostback="true" 
                                                        style=" width:0; height:0; border-style:none; display:none; border:0; "  />
                                                    </div>
                                                </span>
                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                                </div>
                                <div runat="server" id="divAbasto" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <telerik:radtoolbar id="toolbarop2" runat="server" width="100%" dir="rtl" onbuttonclick="EnviarSolicitudAbasto" onclientbuttonclicked="EnviarSolicitudAbas" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false" />
                                        <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                    </Items>
                                </telerik:radtoolbar>
                                <table>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="txtProductoLocal" runat="server" Width="450px" MaxLength="20" Visible=false/>
                                            <asp:HiddenField ID="hfProductoLocal" runat="server" />
                                            <asp:HiddenField ID="hddPrecioAAAOriginal" runat="server" />
                                            <asp:HiddenField ID="hddPrecioCostoOriginal" runat="server" />
                                            <asp:HiddenField ID="hfNumSolicitudAbasto" runat="server" />
                                            <div style="visibility:hidden" id="divgenabastopost">
                                                <telerik:RadComboBox ID="cmbGeneAbasto" runat="server" DataTextField="Descripcion" DataValueField="Id" />
                                                <telerik:RadComboBox ID="cmbGeneAbastoSubFam" runat="server" DataTextField="Descripcion" DataValueField="Id" />

                                                <asp:Button ID="btnBuscaProductoLocal" Text="Seleccionar" runat="server" OnClick="SubmitProductoLocal" />
                                            </div>

                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td><asp:Label ID="Label48" runat="server" Text="Código del Producto (Solo Local)"></asp:Label>&nbsp; </td>
                                        <td colspan="2">
                                            <telerik:RadComboBox ID="cmbProductosHabiliCompraLocal" runat="server" DataTextField="Descripcion" DataValueField="Id" width="500px" 
                                                OnClientSelectedIndexChanged="cmbProductosHabiliCompraLocal_ClientSelectedIndexChanged"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="right">&nbsp;
                                            <asp:Label ID="lblNumSolicitudAbasto" runat="server"  CssClass="tituloProducto"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="4">
                                             <telerik:RadTabStrip ID="RadTabStripAbasto" runat="server" MultiPageID="RadMultiPagePrincipalAbasto"
                                                    SelectedIndex="0" TabIndex="-1">
                                                    <Tabs>
                                                        <telerik:RadTab PageViewID="RadPageViewDGralesAbasto" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewParametrosAbasto" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewIndicadoresAbasto" Text="<u>E</u>xistencia Inv" AccessKey="E" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewDetallesAbasto" Text="<u>P</u>recios" AccessKey="P">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewSATAbasto" Text="SA<u>T</u>" AccessKey="T">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewCompLocalAbasto" Text="Proveedor <u>C</u> Locales" AccessKey="C" visible="false" >
                                                        </telerik:RadTab>
                                                    </Tabs>
                                                </telerik:RadTabStrip>
                                                <telerik:RadMultiPage ID="RadMultiPagePrincipalAbasto" runat="server" SelectedIndex="0"
                                                    Width="800px">
                                                    <!-- Aqui empieza el contenido de los tabs--->
                                                    <telerik:RadPageView ID="RadPageViewDGralesAbasto" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 16;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table border="0" cellpadding=1 cellspacing=1>
                                                                        <!--Tab 1  Tabla 1-->
                                                                        <tr >
                                                                            <td>
                                                                                <asp:Label ID="Label49" runat="server" Text="Código del producto"></asp:Label>
                                                                            </td>
                                                                            <td width="200px">&nbsp;
                                                                                <asp:Label runat="server" ID="lblId_Prd"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label50" runat="server" Text="Código Compra Local del producto"></asp:Label>
                                                                            </td>
                                                                            <td width="200px">&nbsp;
                                                                                <asp:Label runat="server" ID="lblCodProd" ></asp:Label>
                                                                            </td>
                                                                            <td ><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkActivoAbasto" Checked="True" runat="server" Text="Activo"/></div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="Label51" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="Label52" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label53" runat="server" Text="Descripción"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">&nbsp;
                                                                                <asp:Label runat="server" ID="lblPrd_Descrpcion" ></asp:Label>
                                                                                <input type="hidden" name="lblPrd_DescrpcionAbasto" runat="server" id="lblPrd_DescrpcionAbasto" />
                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkProductoNuevoAbasto" runat="server" Text="Producto nuevo" Enabled="false" /></div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label54" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label55" runat="server" Text="Presentación"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2"">&nbsp;
                                                                                <asp:Label runat="server" ID="lblPresentacion" ></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label56" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label57" runat="server" Text="Tipo de producto"></asp:Label>
                                                                            </td>
                                                                            <td >&nbsp;
                                                                                <asp:Label runat="server" ID="lblTipoProducto"  ></asp:Label>
                                                                            </td>
                                                                            <td >
                                                                                <asp:Label runat="server" ID="lblcmbTipoProducto" ></asp:Label>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <asp:Label ID="Label58" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label59" runat="server" Text="Sistemas propietarios"  visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblId_Spo"  visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblcmbSisProp"  visible="false"></asp:Label>
                                                                                <asp:Label ID="Label60" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                                <asp:Label ID="Label61"  visible="false" runat="server" Text="Agrupado de equipos de sistemas propietarios"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAgrupadoSpo"  visible="false" ></asp:Label>
                                                                                <asp:Label ID="Label62" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                                <asp:Label ID="Label63" runat="server" Text="Categoría de producto"  visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblCategoria"  visible="false" ></asp:Label>
                                                                                <asp:Label runat="server" ID="lblcmbCategoria"  visible="false" ></asp:Label>
                                                                                <asp:Label ID="Label64" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label65" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="3">&nbsp;
                                                                                <asp:Label runat="server" ID="lblFam" visible="false"></asp:Label>
                                                                            <!-- </td>
                                                                            <td Width="270px">&nbsp; -->
                                                                                <asp:Label runat="server" ID="lblcmbFam" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label66" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">&nbsp;
                                                                                <asp:Label runat="server" ID="lblSubFam" visible="false" ></asp:Label>
                                                                            <!-- </td>
                                                                            <td Width="270px">&nbsp;-->
                                                                                <asp:Label runat="server" ID="lblcmbSubFam" ></asp:Label>
                                                                                <asp:HiddenField ID="hddFamiliaAbasto" runat="server" />
                                                                            </td>
                                                                            <td>&nbsp; 
                                                                                <asp:Label ID="Label67" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label68" runat="server" Text="Proveedor" Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblProveedor"  Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblcmbProveedor"  Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label69" runat="server" ForeColor="Red"  Visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label811" runat="server" Text="Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtProveeAba" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtProveedorAba_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:HiddenField ID="hidRpoveedorOriginal" runat="server" />
                                                                                <asp:HiddenField ID="hidProductoOriginal" runat="server" />
                                                                                <asp:TextBox ID="txtProductosMismoPadreAbasto" runat="server" style='width:10px;visibility:hidden;'  ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbProveedorAba" runat="server" Width="350px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorAba_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtProveedorAba" runat="server" ForeColor="Red"></asp:Label>
                                                                                <div id="FijarProveedor" style="visibility:hidden">
                                                                                <asp:Button ID="btnRefTituloAba"   runat="server" OnClick="ActualizaTItuloAba" Text="Fijar Proveedor" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <!--Tab 1 Tabla 3-->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label70" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                            </td>
                                                                            <td width="220px" >&nbsp;
                                                                                <asp:Label runat="server" ID="lblcmbUentrada" ></asp:Label>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <asp:Label ID="Label71" runat="server" ForeColor="Red"></asp:Label>
                                                                                &nbsp;
                                                                                <asp:Label ID="Label72" runat="server" Text="Factor de conversión"></asp:Label>
                                                                            </td>
                                                                            <td Width="12px">&nbsp;
                                                                                <asp:Label runat="server" ID="lbltxtFactorConversion" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label73" runat="server" Text="Unidad de salida"></asp:Label>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <asp:Label runat="server" ID="lblcmbUsalida" ></asp:Label>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <asp:Label ID="Label74" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                            </td>
                                                                            <td Width="200px">&nbsp;
                                                                                <asp:Label runat="server" ID="lbltxtUempaque" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewParametrosAbasto" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table cellpadding="5px">
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <strong>
                                                                                    <asp:Label ID="Label75" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label76" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtInvSeguridad" ></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkPropietarioAbasto" runat="server" Text="Aparato de sistema propietario" Enabled="false" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label77" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:Label runat="server" ID="lbltxtTentrega" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label78" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                                            </td>
                                                                            <td width="170px">
                                                                                <asp:Label runat="server" ID="lbltxtPlanAbasto" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label79" runat="server" Text="Minimo de compra"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:Label runat="server" ID="lbltxtMinCompra" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label80" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:Label runat="server" ID="lbltxtTtransporte" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <!--
                                                                            <td width="40px">
                                                                                <asp:Label ID="Label81" runat="server" Text="Rentabilidad"></asp:Label>
                                                                                <asp:Label runat="server" ID="lbltxtRentabilidad" ></asp:Label>
                                                                                <asp:Label runat="server" ID="lbllblRentabilidad" ></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label82" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" style="text-align: right">
                                                                                <asp:CheckBox ID="chkComprasLocalesAbasto" runat="server" Text="Compras locales" Enabled="false" />
                                                                            </td>
                                                                        </tr>
                                                                        -->
                                                                    </table>
                                                                    <table cellpadding="5px">
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label83" runat="server" Text="Meses de amortización"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtAmortizacion" ></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label84" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label85" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtPesos" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label86" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtExistencia" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label87" runat="server" Text="Ubicación"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtUbicacion" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <!--
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label88" runat="server" Text="Contribuci&oacute;n"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtContribucion" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label89" runat="server" Text="Porcentaje de participaci&oacute;n de utilidades"></asp:Label>
                                                                            </td>
                                                                            <td width="90px">
                                                                                <asp:Label runat="server" ID="lbltxtPorUtilidades"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        -->
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewIndicadoresAbasto" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table cellpadding="5px">
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label90" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                            <td style="width: 20px">
                                                                            </td>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label91" runat="server" Text="Inventarios"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label92" runat="server" Text="Asignado"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtAsignado"></asp:label>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="Label93" runat="server" Text="Inicial"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtInicial" ></asp:label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label94" runat="server" Text="Ordenado"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtOrdenado"></asp:label>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="Label95" runat="server" Text="Final"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtFinal" ></asp:label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1131" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtTransito"></asp:label>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="Label1132" runat="server" Text="F&iacute;sico"></asp:Label>
                                                                            </td>
                                                                            <td width="70px">
                                                                                <asp:label runat="server" ID="lbltxtFisico"></asp:label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewSATAbasto" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><asp:HiddenField ID="HiddenField4" runat="server" />
                                                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label828" runat="server" Text="Unidad de Medida (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUnidadMedidaSATAbasto" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="300px" onSelectedIndexChanged="cmbUnidadMedidaSATDesabasto_SelectedIndexChanged" AutoPostBack="True">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_cmbUnidadMedidaSATAbasto" runat="server" ForeColor="Red"  ></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><telerik:RadComboBox ID="cmbProdServicioSATAbasto" runat="server" Width="5px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="10px" visible="false" onSelectedIndexChanged="cmbProdServicioSATDesabasto_SelectedIndexChanged" AutoPostBack="True">
                                                                                </telerik:RadComboBox>
                                                                            <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label839" runat="server" Text="Producto/Servicios (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <input ID="txtSearchProdServSATAbasto"  type="text" runat="server" name="txtSearchProdServSATAbasto" style='width:650px' onchange="cambiatexto()"/>
                                                                                <input id="hfCveProdServSATAbasto" type="hidden"  name="hfCveProdServSATAbasto" runat="server" />
                                                                                <asp:Label ID="lbl_Val_cmbProdServicioSATAbasto" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewDetallesAbasto" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <p style="font-family: Verdana; font-size:small; font-style:italic">
                                                                                    <a href="JavaScript:HistorialPrecios()" id="lnkHPreciosAbasto" >Historial de Precios</a>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadAjaxPanel ID="ajaxFormPanelAbasto" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                    <telerik:RadGrid ID="rgPreciosAbasto" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                        PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                        OnNeedDataSource="grdPreciosAbasto_NeedDataSource" OnUpdateCommand="grdPreciosAbasto_UpdateCommand"
                                                                                        OnPreRender="grdPreciosAbasto_PreRender" OnItemDataBound="grdPreciosAbasto_ItemDataBound"
                                                                                        OnPageIndexChanged="grdPreciosAbasto_PageIndexChanged">
                                                                                        <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                            EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                            Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                            <ExpandCollapseColumn Visible="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                    UniqueName="Prd_FechaInicio">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaInicioAba" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %> '
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaInicioAba" runat="server" MinDate="1900-01-01"  enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaInicioAba" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaFinAba" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaFinAba" runat="server" MinDate="1900-01-01"  enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaFinAba" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                    UniqueName="Pre_Descripcion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioAba" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioEditAba" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                            Font-Bold="true" />
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PesosAba" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadNumericTextBox ID="txtPrd_PesosAba" runat="server" Width="100px" MaxLength="9"
                                                                                                            MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                        </telerik:RadNumericTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                    UniqueName="Prd_PreDescripcion" Display="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PreDescripcionAba" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionAba" runat="server"
                                                                                                            Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                        </telerik:RadTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                    EditText="Editar" HeaderText="Editar">
                                                                                                </telerik:GridEditCommandColumn>
                                                                                            </Columns>
                                                                                            <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                                InsertCaption="Agregar nuevo precio de producto">
                                                                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                    BorderColor="#000000" BorderWidth="1" />
                                                                                                <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                                <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                    EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                                </EditColumn>
                                                                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                            </EditFormSettings>
                                                                                        </MasterTableView>
                                                                                        <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                            LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                            PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                        <GroupingSettings CaseSensitive="False" />
                                                                                        <ClientSettings>
                                                                                            <ClientEvents OnRowDblClick="rgPreciosAbasto_ClientRowDblClick" />
                                                                                            <Selecting AllowRowSelect="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </telerik:RadAjaxPanel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewCompLocalAbasto" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table cellpadding="5px">
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label1134" runat="server" Text="Fabricante"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table cellpadding="5px">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1135" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td width="170px">
                                                                                <asp:label runat="server" ID="lbltxtFnombre"></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFnombre1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1136" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="120px">
                                                                                <asp:label runat="server" ID="lbltxtFcodigo" ></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFcodigo1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1137" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="170px">
                                                                                <asp:label runat="server" ID="lbltxtFdescripcion"></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFdescripcion1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1138" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="120px">
                                                                                <asp:label runat="server" ID="lbltxtFpresentacion"></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFpresentacion1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table cellpadding="5px">
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label1139" runat="server" Text="Proveedor"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table cellpadding="5px">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1140" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPnombreAbasto" runat="server" Width="150px" visible="false"
                                                                                    MaxLength="100" >
                                                                                </telerik:RadTextBox>
                                                                                 <asp:TextBox ID="txtSearchProvAbasto" runat="server" Width="300px" MaxLength="6" />
                                                                                  <asp:HiddenField ID="hfProviderAbastoId" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPnombre1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1141" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="120px">
                                                                                <asp:label runat="server" ID="lbltxtPcodigo"></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPcodigo1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1142" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="120px">
                                                                                <asp:label runat="server" ID="lbltxtPdescripcion"></asp:label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPdescripcion1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label1143" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td width="120px">
                                                                                <asp:label runat="server" ID="lbltxtPpresentacion" ></asp:label> 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPpresentacion1" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                </telerik:RadMultiPage>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                    <div id="ver_off2" style="display: none" runat="server" >
                                        <asp:Button ID="btnEnviarAbasto" Text="Envío de Solicitud" runat="server" OnClick="EnviarSolicitudAbasto" style="visibility:hidden" />
                                        <asp:Button ID="btnCancelarAbasto" Text="Cancelar" runat="server"  OnClientClick="LimpiarControlesProductoAbasto" />
                                    </div>
                                </div>
                                <div runat="server" id="divSolicitudCliente" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <telerik:radtoolbar id="toolbarop3" runat="server" width="100%" dir="rtl" onbuttonclick="EnviarSolicitudCliente" onclientbuttonclicked="EnviarSolicitudClients" AutoPostBack="True">
                                    <Items>
                                        <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" visible="false"/>
                                        <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                    </Items>
                                </telerik:radtoolbar>
                                   <table>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label181" runat="server" Text="Aplicación:"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbAplicacionSoli" runat="server" Width="350px" 
                                                    AutoPostBack="True" DropDownWidth="450" onSelectedIndexChanged="cmbAplicacionSoli_SelectedIndexChanged"
                                                    LoadingMessage="Cargando...">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label182" runat="server" Text="SubFamilia:"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmdSubFamiliaSoli" runat="server" Width="350px" 
                                                    AutoPostBack="True"
                                                    onSelectedIndexChanged="cmdSubFamiliaSoli_SelectedIndexChanged"
                                                    LoadingMessage="Cargando..." enabled='false'>   
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td colspan="3">
                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProduct" runat="server" Height="200" Width="450px" 
                                                    DropDownWidth="480" EmptyMessage="Elige un Producto" HighlightTemplatedItems="true"
                                                    EnableLoadOnDemand="true" Filter="StartsWith"  enabled="false"  onSelectedItem="SubmitProductoSoli" 
                                                    Label="Producto: " visible="false">
                                                    <HeaderTemplate>
                                                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    ID
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    Producto
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Ids']")%>
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Producto']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td rowspan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSeleccionaProdSol" Text="Seleccionar" runat="server" OnClick="SubmitProductoSoli" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblCodigoProductoCteF4F" runat="server" Text="Código del Producto:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIdProductoCteF4F" runat="server" Width="550px" Enabled="true" Visible="false"/>
                                                <asp:HiddenField ID="hdfProductoCteF4F" runat="server" />
                                                <asp:HiddenField ID="hdSubFamiliaSoli" runat="server" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan=4 style="height:30px" align="right">&nbsp;
                                            <asp:HiddenField ID="hfNumSolicitudCte" runat="server" />
                                             <asp:Label ID="lblNumSolicitudCte" runat="server" text="" CssClass="tituloProducto"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="4">
                                            <div runat="server" id="PestanasCte" style="margin-left: 10px; margin-right: 10px;" Visible="false">
                                                <telerik:RadTabStrip ID="RadTabStripPrincipalCte" runat="server" MultiPageID="RadMultiPagePrincipalCte"
                                                    SelectedIndex="0" TabIndex="-1">
                                                    <Tabs>
                                                        <telerik:RadTab PageViewID="RadPageViewDGralesCte" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewParametrosCte" Text="<u>I</u>nfo Inventarios" AccessKey="I" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewIndicadoresCte" Text="<u>E</u>xistencia Inv" AccessKey="E"  visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewDetallesCte" Text="<u>P</u>recios" AccessKey="P">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewSATCte" Text="SA<u>T</u>" AccessKey="T">
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewCompLocalCte" Text="Proveedor <u>C</u> Locales" AccessKey="D" visible="false" >
                                                        </telerik:RadTab>
                                                        <telerik:RadTab PageViewID="RadPageViewClientesAut" Text="<u>C</u>lientes Exclusivos" AccessKey="C" >
                                                        </telerik:RadTab>
                                                    </Tabs>
                                                </telerik:RadTabStrip>
                                                <telerik:RadMultiPage ID="RadMultiPagePrincipalCte" runat="server" SelectedIndex="0"
                                                    Width="800px">
                                                    <!-- Aqui empieza el contenido de los tabs--->
                                                    <telerik:RadPageView ID="RadPageViewDGralesCte" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td nowrap>
                                                                    <table border="0" cellpadding=1 cellspacing=1>
                                                                        <!--Tab 1  Tabla 1-->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label681" runat="server" Text="Código del producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadNumericTextBox ID="TextId_PrdCte" runat="server" Width="150px" MaxLength="16"  enabled="false">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="TextId_PrdCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            
                                                                                <asp:Label ID="Label682" runat="server" Text="Código usado del producto" Visible="false"></asp:Label>
                                                                            
                                                                               <asp:HiddenField  ID="txtCodProdCte" runat="server" />
                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkActivoCte" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                                                    AutoPostBack="True" /> </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_TextId_PrdCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label97" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label96" runat="server" Text="Descripción"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadTextBox ID="TextPrd_DescrpcionCte" runat="server" Width="306px" MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SinComilla" OnBlur="TextPrd_DescrpcionCte_OnBlur" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td><div style="visibility:hidden">
                                                                                <asp:CheckBox ID="chkProdNuevoCte" runat="server" Text="Producto nuevo"  Checked="True"  /></div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_TextPrd_DescrpcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label98" runat="server" Text="Presentación"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtPresentacionCte" runat="server" Width="70px" MaxLength="5"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:Label ID="lbl_Val_txtPresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label889" runat="server" Text="Vigencia"/>
                                                                            </td>
                                                                            <td><telerik:RadDatePicker Runat="server" id="rdpVigencia" ClientEvents-OnDateSelected="OnDateSelected" >
                                                                                </telerik:RadDatePicker>
                                                                                <asp:Label ID="lbl_Val_rdpVigencia" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label100" runat="server" Text="Tipo de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTipoProductoCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtTipoProductoCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>&nbsp;
                                                                                <telerik:RadComboBox ID="cmbTipoProductoCte" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbTipoProductoCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtTipoProductoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label106" runat="server" Text="Sistemas propietarios"  Visible="false"></asp:Label>
                                                                                <telerik:RadNumericTextBox ID="TextId_SpoCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1"  Visible="false" >
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="TextId_SpoCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbSisPropCte" runat="server" Width="250px" Filter="Contains"  Visible="false"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbSisPropCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_TextId_SpoCte" runat="server" ForeColor="Red" visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label1106" runat="server" Text="Categoría de producto"  visible="false"></asp:Label>
                                                                                <div style='visibility:hidden'>
                                                                                <telerik:RadNumericTextBox ID="txtCategoriaCte" runat="server"  visible="false" Width="70px" MaxLength="9" MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtCategoriaCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                </div>
                                                                                <telerik:RadComboBox ID="cmbCategoriaCte" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                                    OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbCategoriaCte_ClientSelectedIndexChanged"
                                                                                    Width="250px" LoadingMessage="Cargando..." MaxHeight="200px"  visible="false">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_txtCategoriaCte" runat="server" ForeColor="Red"  visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label108" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2" nowrap>
                                                                                <telerik:RadComboBox ID="cmbFamCte" runat="server" Width="450px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                                                    MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFamCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFam" runat="server" ForeColor="Red"></asp:Label>
                                                                                 <div style='visibility:hidden'>
                                                                                    <telerik:RadNumericTextBox ID="txtFamCte" runat="server" Width="70px" MaxLength="9"
                                                                                        MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                        <ClientEvents OnBlur="txtFamCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    </telerik:RadNumericTextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label109" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadComboBox ID="cmbSubFamCte" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbSubFamCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                            <div style='visibility:hidden'>
                                                                                    <telerik:RadNumericTextBox ID="txtSubFamCte" runat="server" Width="70px" MaxLength="9"
                                                                                        MinValue="1">
                                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                        <ClientEvents OnBlur="txtSubFamCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    </telerik:RadNumericTextBox>
                                                                                </div>
                                                                                <asp:Label ID="Label0106" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label111" runat="server" Text="Proveedor"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtProveedorCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="1">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnBlur="txtProveedorCte_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:TextBox ID="txtProductosMismoPadreCte" runat="server" style='width:10px;visibility:hidden;' ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbProveedorCte" runat="server" Width="250px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorCte_ClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px"
                                                                                    Autopostback="false"
                                                                                    >
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtProveedorCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <!--Tab 1 Tabla 3-->
                                                                        </table>
                                                                        <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label113" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUentradaCte" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientSelectedIndexChanged="cmbUentradaCte_OnClientSelectedIndexChanged"
                                                                                    OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_cmbUentradaCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label115" runat="server" Text="Factor de conversión"  Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFactorConversionCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0"  Visible="false">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label116" runat="server" Text="Unidad de salida"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUsalidaCte" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Id" OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_cmbUsalidaCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label117" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtUempaqueCte" runat="server" Width="50px" MaxLength="9" >
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:Label ID="lbl_Val_txtUempaque" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr><td colspan="4">&nbsp;</td></tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Label ID="Label789" runat="server" Text="Motivo por el cual el cliente solicita este producto en especial"></asp:Label>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:Label ID="lbl_Val_txtMotivo" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td colspan="3">
                                                                                <telerik:RadTextBox Runat=server ID="txtMotivoSolicita" MaxLength="250" RenderMode="Lightweight" TextMode="MultiLine" Width="440px"  Height="70px" w ></telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewParametrosCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <strong>
                                                                                    <asp:Label ID="Label518" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label519" runat="server" Text="Inv. Seguridad"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInvSeguridadCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkSistPropCte" runat="server" Text="Sistema propietario" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label520" runat="server" Text="Tiempo de entrega"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTentregaCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label521" runat="server" Text="Planeación de Abasto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtPlanAbastoCte" runat="server" Width="150px" MaxLength="20">
                                                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                                                 <ClientEvents OnKeyPress="SoloAlfanumerico"></ClientEvents>
                                                                                 </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label522" runat="server" Text="Minimo de compra"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtMinCompraCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label523" runat="server" Text="Tiempo de transporte"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTtransporteCte" runat="server" Width="50px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td colspan=2>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" style="text-align: right">
                                                                                <asp:CheckBox ID="chkComprasLocalesCte" runat="server" Text="Compras locales" Enabled="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table >
                                                                        <!--Tab 2 Tabla 1 -->
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label526" runat="server" Text="Meses de amortización"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAmortizacionCte" runat="server" Width="70px" MaxLength="3"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label127" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label528" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtPesosCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label529" runat="server" Text="Máximo en existencia final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtExistenciaCte" runat="server" Width="70px" MaxLength="9"
                                                                                    MinValue="0">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label530" runat="server" Text="Ubicación"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtUbicacionCte" runat="server" Width="70px"
                                                                                    MaxLength="5">
                                                                                    <ClientEvents OnKeyPress="SoloAlfabetico" ></ClientEvents>
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewIndicadoresCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <!-- Tabla principal--->
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label533" runat="server" Text="Administración de inv."></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                            <td style="width: 20px">
                                                                            </td>
                                                                            <td colspan="2" style="text-align: center">
                                                                                <strong>
                                                                                    <asp:Label ID="Label534" runat="server" Text="Inventarios"></asp:Label></strong>
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label535" runat="server" Text="Asignado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtAsignadoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label536" runat="server" Text="Inicial"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtInicialCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label537" runat="server" Text="Ordenado"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtOrdenadoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label433" runat="server" Text="Final"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFinalCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label431" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtTransitoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label432" runat="server" Text="F&iacute;sico"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtFisicoCte" runat="server" Width="50px" Enabled="false"
                                                                                    MaxLength="9">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ></NumberFormat>
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" ></ClientEvents>
                                                                                </telerik:RadNumericTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewDetallesCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadAjaxPanel ID="ajaxFormPanelCte" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                    <telerik:RadGrid ID="rgPreciosCte" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                        PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                        OnNeedDataSource="rgPreciosCte_NeedDataSource" OnUpdateCommand="rgPreciosCte_UpdateCommand"
                                                                                        OnPreRender="rgPreciosCte_PreRender" OnItemDataBound="rgPreciosCte_ItemDataBound"
                                                                                        OnPageIndexChanged="rgPreciosCte_PageIndexChanged">
                                                                                        <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                            EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                            Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                            <ExpandCollapseColumn Visible="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                    ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                    Display="false" ReadOnly="true">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                    UniqueName="Prd_FechaInicio">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaInicioCte" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaInicioCte" runat="server" MinDate="1900-01-01" enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="dateCalendarFechaInicioCte" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFechaFinCte" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                            Width="200px" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadDatePicker ID="datePickerFechaFinCte" runat="server" MinDate="1900-01-01" enabled="false"
                                                                                                            DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                            <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                            <Calendar ID="datePickerFechaFinCte" runat="server" RangeMinDate="1900-01-01">
                                                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                    TodayButtonCaption="Hoy" />
                                                                                                            </Calendar>
                                                                                                        </telerik:RadDatePicker>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                    UniqueName="Pre_Descripcion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioCte" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:Label ID="lblTipoPrecioEditCte" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                            Font-Bold="true" />
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>
                                                                                                
                                                                                                <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PesosCte" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadNumericTextBox ID="txtPrd_PesosCte" runat="server" Width="100px" MaxLength="9"
                                                                                                            MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                        </telerik:RadNumericTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                    UniqueName="Prd_PreDescripcion" Display="false" >
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblPrd_PreDescripcionCte" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionCte" runat="server"
                                                                                                            Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                        </telerik:RadTextBox>
                                                                                                    </EditItemTemplate>
                                                                                                </telerik:GridTemplateColumn>

                                                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                    EditText="Editar" HeaderText="Editar">
                                                                                                </telerik:GridEditCommandColumn>
                                                                                            </Columns>
                                                                                            <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                                InsertCaption="Agregar nuevo precio de producto">
                                                                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                    BorderColor="#000000" BorderWidth="1" />
                                                                                                <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                                <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                    EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                                </EditColumn>
                                                                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                            </EditFormSettings>
                                                                                        </MasterTableView>
                                                                                        <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                            LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                            PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                        <GroupingSettings CaseSensitive="False" />
                                                                                        <ClientSettings>
                                                                                            <ClientEvents OnRowDblClick="rgPreciosCte_ClientRowDblClick" />
                                                                                            <Selecting AllowRowSelect="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </telerik:RadAjaxPanel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewSATCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><asp:HiddenField ID="HiddenField6" runat="server" />
                                                                    <asp:HiddenField ID="HiddenField7" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label99" runat="server" Text="Unidad de Medida (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="cmbUnidadMedidaSATCte" runat="server" Width="450px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="300px" >
                                                                                </telerik:RadComboBox>
                                                                                <asp:Label ID="lbl_Val_cmbUnidadMedidaSATCte" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><telerik:RadComboBox ID="cmbProdServicioSATCte" runat="server" Width="5px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                    DataValueField="Cve" LoadingMessage="Cargando..." MaxHeight="10px" visible="false" >
                                                                                </telerik:RadComboBox>
                                                                            <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label101" runat="server" Text="Producto/Servicios (SAT):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <input ID="txtSearchProdServSATACte"  type="text" name="txtSearchProdServSATACte" style='width:650px' />
                                                                                <input id="hfCveProdServSATCte" type="hidden"  name="hfCveProdServSATCte" runat="server" />
                                                                                <asp:Label ID="lbl_Val_cmbProdServicioSATCte" runat="server" ForeColor="Red"  ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><br /><br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageViewCompLocalCte" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label434" runat="server" Text="Fabricante"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label435" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFnombreCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFnombreCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label436" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFcodigoCte" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFcodigoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label437" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFdescripcionCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFdescripcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label438" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtFpresentacionCte" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtFpresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>
                                                                                    <asp:Label ID="Label439" runat="server" Text="ProveedorCte"></asp:Label></strong>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label440" runat="server" Text="Nombre"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPnombreCte" runat="server" Width="150px" visible="false"
                                                                                    MaxLength="100" >
                                                                                </telerik:RadTextBox>
                                                                                 <asp:TextBox ID="txtSearchProvCte" runat="server" Width="300px" MaxLength="6" />
                                                                                  <asp:HiddenField ID="hfProviderIdCte" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPnombreCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label441" runat="server" Text="Código de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPcodigoCte" runat="server" Width="100px"
                                                                                    MaxLength="30">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPcodigoCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label442" runat="server" Text="Descripción de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPdescripcionCte" runat="server" Width="150px"
                                                                                    MaxLength="100">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPdescripcionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label443" runat="server" Text="Presentación de producto"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox onpaste="return false" ID="txtPpresentacionCte" runat="server" Width="100px"
                                                                                    MaxLength="20">
                                                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Val_txtPpresentacionCte" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                     <telerik:RadPageView ID="RadPageViewClientesAut" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <table style="font-family: vernada; font-size: 8;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ListBox ID="listaClientes" runat="server" Width="550px" Rows="4" OnDblClick="JavaScript: DelItem();"></asp:ListBox>
                                                                                <input id="ddlElements" type="hidden" name="ddlElements" runat="server"  />
                                                                                <input id="ddlElementsFull" type="hidden" name="ddlElementsFull" runat="server"  />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td align="left">
                                                                    <table>
                                                                     <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td> Cliente:</td>
                                                                        <td><input ID="txtNomCteListado"  type="text" name="txtNomCteListado" style='width:450px' />
                                                                            <input id="hdtxtClienteListado" type="hidden"  name="hdtxtClienteListado" runat="server" />
                                                                        </td>
                                                                        <td align="right">&nbsp;
                                                                            <input onclick="JavaScript: AddItem();" type="button" value="Agregar Cliente" />
                                                                            <asp:Button ID="btnAgregarClienteListado" Text="Agregar" visible="false" runat="server" OnClientClick="AddItem" />
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                     </tr>
                                                                     <tr><td colspan="5"><asp:Label ID="lbl_Val_ClientesExclusivos" runat="server" ForeColor="Red"></asp:Label> </td>
                                                                     </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </telerik:RadPageView>
                                                </telerik:RadMultiPage>
                                                <br />
                                                <br />
                                                <span style="text-align:right">
                                                <div id="Div2" style="display: none">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnEnviaSolicitudCliente" Text="Envío de Solicitud" runat="server" OnClick="EnviarSolicitudCliente" style="visibility:hidden" />
                                                <asp:Button ID="btnCancelaSolicitudCliente" Text="Cancelar" runat="server" OnClientClick="LimpiarControlesProductoCliente"  />
                                                </div>
                                                </span>
                                            </div>
                                        </td>
                                        </tr>
                                 </table>
                                </div>
                                <div runat="server" id="divConsultaSolicitud" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <table width="950px" border="0" cellpadding="3" cellspacing="1" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border=0 cellpadding=2 cellspacing=2>
                                                <tr>
                                                    <td nowrap="nowrap">Número de Solicitud:</td>
                                                    <td><asp:TextBox ID="txtBuscaXSolCom" runat="server" Width="100px" Enabled="true" MaxLength="6" /></td>
                                                    <td>&nbsp;<asp:Button ID="btnBuscaSoli" Text="Buscar por Solicitud" runat="server" OnClick="BuscaSolixSoli" Width="130px"  Visible="false"/></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Codigo del producto:</td>
                                                    <td><asp:TextBox ID="txtBuscaXCodProd" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                                                    <td>&nbsp;<asp:Button ID="btnBuscaCod" Text="Buscar por Producto" runat="server" OnClick="BuscaSoliXProdu" Width="130px"  Visible="false"/></td>
                                                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaCodi" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Proveedor:</td>
                                                    <td><asp:TextBox ID="txtBuscaXProvee" runat="server" Width="450px" Enabled="true" MaxLength="20" /></td>
                                                    <td>&nbsp;<asp:Button ID="btnBuscaProv" Text="Buscar por Proveedor" runat="server" OnClick="BuscaSoliXProve" Width="130px" Visible="false"/></td>
                                                    <td>&nbsp;<asp:HiddenField ID="hdtxtBuscaProv" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" >&nbsp;<asp:Button ID="btnBuscaGen" Text="Buscar" runat="server" OnClick="BuscaCombinado" Width="100px"/>
                                                        &nbsp;&nbsp;
                                                        &nbsp;<asp:Button ID="btnRegresarCons" Text="Regresar" runat="server" OnClick="RegresaDeConsulta" Width="100px"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">
                                            <telerik:RadGrid ID="rgCompraLocal" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCompraLocal_PageIndexChanged" OnNeedDataSource="rgCompraLocal_NeedDataSource"
                                                OnItemCommand="rgCompraLocal_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Comp" HeaderText="Clave" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcve" runat="server" Text='<%# Bind("Id_Comp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Cd_Nombre" HeaderText="CDI" UniqueName="column1" Visible="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Solicito_Nombre" HeaderText="Solicito" UniqueName="column2">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud" UniqueName="idTipoSolicitud" Visible="false">
                                                            <HeaderStyle Width="5px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="TipoSolicitud" HeaderText="Tipo de Solicitud" UniqueName="column3">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FechaAut" HeaderText="Fecha Autorización" UniqueName="column4"
                                                            Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FechaSol" HeaderText="Fecha Solicitud" UniqueName="column5">
                                                            <HeaderStyle Width="180px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EstatusAut" HeaderText="Partidas Autorizadas" UniqueName="column6" >
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Detail" ImageUrl="Imagenes/iconos/book_blue_view.png" 
                                                            Text="Ver Detalle" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Autorizar" ImageUrl="Imagenes/flecha.jpg" 
                                                            Text="Autorizar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                         </td>
                                        <td >&nbsp;</td>
                                        <td valign="top" nowrap="nowrap">
                                            <telerik:RadGrid ID="rgDetalleSolicitud" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                PageSize="30" AllowPaging="false" AllowSorting="false"  width="100%"
                                                OnItemCommand="rgDetalleSolicitud_ItemCommand"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                 visible="true">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Solicitud" HeaderText="Solicitud" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSolicitud" runat="server" Text='<%# Bind("Solicitud") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="TipoSol" HeaderText="" UniqueName="column5" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoSolicitud" runat="server" Text='<%# Bind("TipoSol") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud" UniqueName="idTipoSolicitud" Visible="false">
                                                            <HeaderStyle Width="5px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Num" HeaderText="Num" UniqueName="column1">
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Costo" HeaderText="Costo" UniqueName="column2">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="column3"
                                                            Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="column4">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="EditSol" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar Producto" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                        </td>
                                     </tr>
                                     <tr><td colspan="3">
                                        <asp:Label ID="titSolicitud" runat="server" CssClass="tituloProducto" Font-Size="28px" ForeColor="blue"></asp:Label>
                                     </td>
                                     </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" nowrap="nowrap">
                                        <div runat="server" id="divConsulta" style="margin-left: 10px; margin-right: 10px;" Visible="false">
                                            <telerik:radtoolbar id="toolbarop4" runat="server" width="100%" dir="rtl" onbuttonclick="GrabaSolicitudConsulta" onclientbuttonclicked="EnviarSolicitudConsClient"  AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" />
                                                    <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ToolTip="Guardar" ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />       
                                                </Items>
                                            </telerik:radtoolbar>
                                            <telerik:RadTabStrip ID="RadTabStripPrincipalCons" runat="server" MultiPageID="RadMultiPagePrincipalCons"
                                                SelectedIndex="0" >
                                                <Tabs>
                                                    <telerik:RadTab PageViewID="RadPageViewDGralesCons" Text="Datos <u>g</u>enerales " AccessKey="G" Selected="True">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab PageViewID="RadPageViewDetallesCons" Text="<u>P</u>recios" AccessKey="P">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab PageViewID="RadPageViewClientesAutCons" Text="<u>C</u>lientes Exclusivos" AccessKey="C" >
                                                    </telerik:RadTab>
                                                </Tabs>
                                            </telerik:RadTabStrip>
                                            <telerik:RadMultiPage ID="RadMultiPagePrincipalCons" runat="server" SelectedIndex="0" Width="800px">
                                                <telerik:RadPageView ID="RadPageViewDGralesCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                    <table style="font-family: vernada; font-size: 8;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <table>
                                                                    <!--Tab 1  Tabla 1-->
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label134" runat="server" Text="Código del producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="TextId_PrdCons" runat="server" Width="150px" MaxLength="16"  enabled="false"
                                                                                    MinValue="1" MaxValue="999999999999999">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="TextId_PrdCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label135" runat="server" Text="Código usado del producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtCodProdCons" runat="server" Width="200px" MaxLength="16" enabled="false"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td><div style="visibility:hidden">
                                                                            <asp:CheckBox ID="chkActivoCons" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                                                AutoPostBack="True" /></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td><asp:Label ID="lbl_Val_TextId_PrdCons" runat="server" ForeColor="Red"></asp:Label></td>
                                                                        <td></td>
                                                                        <td><asp:Label ID="Label137" runat="server" ForeColor="Red"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label138" runat="server" Text="Descripción"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadTextBox ID="TextPrd_DescrpcionCons" runat="server" Width="306px" MaxLength="100">
                                                                                <ClientEvents OnKeyPress="SinComilla" OnBlur="TextPrd_DescrpcionCons_OnBlur" />
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkProdNuevoCons" runat="server" Text="Producto nuevo"  Checked="True"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_TextPrd_DescrpcionCoins" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label140" runat="server" Text="Presentación"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtPresentacionCons" runat="server" Width="70px" MaxLength="5" MinValue="1">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lbl_Val_txtPresentacionCons" runat="server" ForeColor="Red"></asp:Label>
                                                                            <asp:Label ID="lblDiceVigencia" runat="server" Text="Vigencia hasta"/>
                                                                        </td>
                                                                        <td><telerik:RadDatePicker Runat="server" id="rdpVigenciaCons"></telerik:RadDatePicker></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label143" runat="server" Text="Tipo de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtTipoProductoCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtTipoProductoCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbTipoProductoCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbTipoProductoCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtTipoProductoCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label145" runat="server" Text="Sistemas propietarios"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="TextId_SpoCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="TextId_SpoCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbSisPropCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSisPropCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtSisPropCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label147" runat="server" Text="Categoría de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtCategoriaCons" runat="server" MaxLength="9" MinValue="1"
                                                                                Width="70px">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtCategoriaCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbCategoriaCons" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                                DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                                OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbCategoriaCons_ClientSelectedIndexChanged"
                                                                                Width="250px" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtCategoriaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label149" runat="server" Text="Aplicación de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtFamCons" runat="server" Width="70px" MaxLength="9" MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtFamCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbFamCons" runat="server" Width="450px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                                                MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFamCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtFamiliaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label150" runat="server" Text="Sub-familia de producto"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtSubFamCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtSubFamCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbSubFamCons" runat="server" Width="450px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSubFamCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtSubFamiliaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label152" runat="server" Text="Proveedor"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtProveedorCons" runat="server" Width="70px" MaxLength="9"
                                                                                MinValue="1">
                                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                <ClientEvents OnBlur="txtProveedorCons_OnBlur" OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbProveedorCons" runat="server" Width="250px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedorCons_ClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px"
                                                                                Autopostback="false"
                                                                                >
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_txtProveedorCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <!--Tab 1 Tabla 3-->
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label154" runat="server" Text="Unidad de entrada"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbUentradaCons" runat="server" Width="200px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbUentradaCons_OnClientSelectedIndexChanged"
                                                                                OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_cmbUentradaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label156" runat="server" Text="Factor de conversión"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtFactorConversionCons" runat="server" Width="50px" MaxLength="9"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label157" runat="server" Text="Unidad de salida"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cmbUsalidaCons" runat="server" Width="200px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                                                DataValueField="Id" OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Val_cmbUsalidaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label158" runat="server" Text="Unidades de empaque"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtUempaqueCons" runat="server" Width="50px" MaxLength="9"
                                                                                MinValue="0">
                                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;</td></tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="lblMotivoSolicitud" runat="server" Text="Motivo por el cual el cliente solicita este producto en especial"></asp:Label>
                                                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_Val_txtMotivoSolicitaCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td colspan="3">
                                                                            <telerik:RadTextBox Runat=server ID="txtMotivoSolicitaCons" MaxLength="250" RenderMode="Lightweight" TextMode="MultiLine" Width="440px"  Height="70px" w ></telerik:RadTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;</td></tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCausaDEsabastoCons" runat="server" Text="Causa del desabasto"></asp:Label>
                                                                        </td>
                                                                        <td  colspan="3">
                                                                            <telerik:RadComboBox ID="cmbCausaDesabastoCons" runat="server" Width="400px" Filter="Contains"
                                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Desc_CausaDesAbasto"
                                                                                DataValueField="Id" MaxHeight="300px"  >
                                                                            </telerik:RadComboBox>
                                                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_Val_cmbMotivoDEsabastoCons" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr><td colspan="4">&nbsp;
                                                                            <div>
                                                                                <asp:ListBox ID="lstbPedidosCons" runat="server" Width="10px" Rows="1" visible="false"></asp:ListBox>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <div id="divPedidosRefCons" runat="server">
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <asp:Label ID="Label816" runat="server" Text="Pedido Desabastecido"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblPedidoSeleccionadoCons" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                            <td  colspan="3">
                                                                                <table border="0" cellpadding="1" cellspacing="1">
                                                                                    <tr>
                                                                                        <td colspan="3"> 
                                                                                            <div id="divSegmentoCons" style="width: 450px; height: 120px; overflow-y: scroll; ">
                                                                                                <asp:CheckBoxList runat="server" ID="chklstPedidosCons" AutoPostBack="false" 
                                                                                                    RepeatColumns="3" CellSpacing="3" CellPadding="3" Width="400px"/>
                                                                                            </div>    
                                                                                            <input id="hddPedidoAbastoCons" type="hidden"  name="hddPedidoAbastoCons" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                         <td>
                                                                                         </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>

                                                                        </div>                                                                        
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageViewDetallesCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                    <table style="font-family: vernada; font-size: 8;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <p style="font-family: Verdana; font-size:small; font-style:italic">
                                                                                <a href="JavaScript:HistorialPrecios()" id="lnkHPrecios" >Historial de Precios</a>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <telerik:RadAjaxPanel ID="ajaxFormPanelCons" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                                <telerik:RadGrid ID="rgPreciosCons" runat="server" GridLines="None" DataMember="listaPrecios"
                                                                                    PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                                                    OnNeedDataSource="rgPreciosCons_NeedDataSource" OnUpdateCommand="rgPreciosCons_UpdateCommand"
                                                                                    OnPreRender="rgPreciosCons_PreRender" OnItemDataBound="rgPreciosCons_ItemDataBound"
                                                                                    OnPageIndexChanged="rgPreciosCons_PageIndexChanged">
                                                                                    <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                                                        <ExpandCollapseColumn Visible="True">
                                                                                        </ExpandCollapseColumn>
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                                                ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                                                ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                                                Display="false" ReadOnly="true">
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                                                UniqueName="Prd_FechaInicio">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label160" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                                                        Width="200px" />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadDatePicker ID="datePickerFechaInicioCons" runat="server" MinDate="1900-01-01"
                                                                                                        DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                        <Calendar ID="dateCalendarFechaInicioCons" runat="server" RangeMinDate="1900-01-01">
                                                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                TodayButtonCaption="Hoy" />
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label161" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                                                        Width="200px" />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadDatePicker ID="datePickerFechaFinCons" runat="server" MinDate="1900-01-01"
                                                                                                        DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                                                        <Calendar ID="datePickerFechaFinCons" runat="server" RangeMinDate="1900-01-01">
                                                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                                                TodayButtonCaption="Hoy" />
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                                                UniqueName="Pre_Descripcion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTipoPrecioCons" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblTipoPrecioEditCons" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                                                        Font-Bold="true" />
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>

                                                                                            <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrd_PesosCons" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadNumericTextBox ID="txtPrd_PesosCons" runat="server" Width="100px" MaxLength="9"
                                                                                                        MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                                                    </telerik:RadNumericTextBox>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridTemplateColumn HeaderText="Comentario" DataField="Prd_PreDescripcion"
                                                                                                UniqueName="Prd_PreDescripcion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrd_PreDescripcionCons" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcionCons" runat="server"
                                                                                                        Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                                                    </telerik:RadTextBox>
                                                                                                </EditItemTemplate>
                                                                                            </telerik:GridTemplateColumn>


                                                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                                                EditText="Editar" HeaderText="Editar">
                                                                                            </telerik:GridEditCommandColumn>
                                                                                        </Columns>
                                                                                        <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                                                            InsertCaption="Agregar nuevo precio de producto">
                                                                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                                                BorderColor="#000000" BorderWidth="1" />
                                                                                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                                                            <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                                                EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                                                            </EditColumn>
                                                                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                                        </EditFormSettings>
                                                                                    </MasterTableView>
                                                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                                                    <GroupingSettings CaseSensitive="False" />
                                                                                    <ClientSettings>
                                                                                        <ClientEvents OnRowDblClick="rgPreciosCte_ClientRowDblClick" />
                                                                                        <Selecting AllowRowSelect="true" />
                                                                                    </ClientSettings>
                                                                                </telerik:RadGrid>
                                                                            </telerik:RadAjaxPanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageViewClientesAutCons" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                                <table style="font-family: vernada; font-size: 8;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="lstClientesAutorizadosCons" runat="server" Width="550px" Rows="4" OnDblClick="JavaScript: DelItemCons();"   ></asp:ListBox>
                                                                        <input id="ddlElementsCons" type="hidden" name="ddlElementsCons" runat="server"  onblur="return ddlElementsCons_onblur()" />
                                                                        <input id="ddlElementsFullCons" type="hidden" name="ddlElementsFullCons" runat="server"  />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                <td>&nbsp;</td>
                                                                <td> Cliente:</td>
                                                                <td><input ID="txtNomCteListadoCons"  type="text" name="txtNomCteListadoCons" style='width:450px' />
                                                                <input id="hdtxtClienteListadoCons" type="hidden"  name="hdtxtClienteListadoCons" />
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                    <input onclick="JavaScript: AddItemCons();" type="button" value="Agregar Cliente" />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadPageView>
                                            </telerik:RadMultiPage>
                                            <br />
                                            <br />
                                            <span style="text-align:right">
                                                <div id="ver_off4" style="display: none">
                                                    <input id="hfNumSolicitudCons" type="hidden"  name="hfNumSolicitudCons" runat="server" />
                                                    <input id="hfTipooSolicitudCons" type="hidden"  name="hfTipooSolicitudCons" runat="server" />
                                                    <input id="hddListaClientesOriginal" type="hidden"  name="hddListaClientesOriginal" runat="server" />
                                                    <asp:Button ID="btnGrabasolicitud" Text="Actualizar Solicitud" runat="server" OnClick="GrabaSolicitudConsulta"   />
                                                    <asp:Button ID="btnCancelaConsulta" Text="Regresar" runat="server" OnClick="CancelarSolicitudConsulta" />
                                                </div>
                                            </span>
                                        </div>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <div runat="server" id="divCatalogoDesabasto" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td nowrap="nowrap">Causa de Desabasto:</td>
                                                    <td><asp:TextBox ID="txtCatCausaDes" runat="server" Width="450px" Enabled="true" MaxLength="50" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="HiddenField2" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="AgregarCausa" ID="btnAgregaCDes" runat="server" OnClick="btnAgregaCDes_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarCausa" ID="btnCancelarCDes" runat="server" OnClick="btnCancelarCDes_Click" />
                                                            
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">
                                            <telerik:RadGrid ID="rgCausasDesabasto" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCausasDesabasto_PageIndexChanged" OnNeedDataSource="rgCausasDesabasto_NeedDataSource"
                                                OnItemCommand="rgCausasDesabasto_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Causa" HeaderText="Clave" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCausaDes" runat="server" Text='<%# Bind("Id_Causa") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Desc_CausaDesAbasto" HeaderText="Descripción" UniqueName="column2">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Desactivar" ImageUrl="Imagenes/iconos/forbidden.png" 
                                                            Text="Desactivar" UniqueName="DetailColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Eliminar" ImageUrl="Imagenes/delete2.png" 
                                                            Text="Eliminar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>
                                </div>
                                <div runat="server" id="divMotivosCompraLocal" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="ModificarMotivo" ID="btnAgregaCMotivo" runat="server" OnClick="btnAgregaCMotivo_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarMotivo" ID="btnCancelarCMotivo" runat="server" OnClick="btnCancelarCMotivo_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Motivo de Compra Local: </td>
                                                    <td><asp:TextBox ID="txtDescMotivoCL" runat="server" Width="450px" Enabled="true" MaxLength="50" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="hddIdMotivoCL" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Porcentaje AAA: </td>
                                                    <td><asp:TextBox runat="server" Width="100px" Enabled="true" MaxLength="50" ID="txtAAA"   /></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:CheckBox ID="chkAplica" Checked="false" runat="server" Text="Aplica" visible="false" /> </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">
                                            <telerik:RadGrid ID="rgMotivosCL" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgMotivosCL_PageIndexChanged" OnNeedDataSource="rgMotivosCL_NeedDataSource"
                                                OnItemCommand="rgMotivosCL_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_MotivoCL" HeaderText="Id" UniqueName="column">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMotivoCL" runat="server" Text='<%# Bind("Id_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Desc_MotivoCL" HeaderText="Descripción" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescMotivoCL" runat="server" Text='<%# Bind("Desc_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="PorcentajeAAA" HeaderText="Porcentaje" UniqueName="column2" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPorcentajeAAA" runat="server" Text='<%# Bind("PorcentajeAAA") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>
                                </div>
                                <div runat="server" id="divConfiguraCorreos" style="font-family: Verdana; font-size: 8pt" visible="false">
                                <table width="950px" border="0" cellpadding="3" cellspacing="0" >
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr> 
                                        <td colspan="3" nowrap="nowrap">
                                            <table border="0" cellpadding="2" cellspacing="2" width="700px">
                                                <tr>
                                                    <td colspan="4" align="right" >&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/document_edit.png"  CommandName="NuevoMotivo" ID="ImageButton20" runat="server" OnClick="btnNuevoCatCorreo_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/disk_blue.png"  CommandName="ModificarMotivo" ID="ImageButton2" runat="server" OnClick="btnAgregaCatCorreo_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ImageUrl="Imagenes/iconos/undo.png"  CommandName="CancelarMotivo" ID="ImageButton3" runat="server" OnClick="btnCancelarCatCorreo_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">CDI: </td>
                                                    <td><telerik:RadComboBox ID="cmbCDIMotivoCL" MaxHeight="300px" runat="server" 
                                                            Width="350px" >
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;<asp:HiddenField ID="hddEmpresa" runat="server" /><asp:HiddenField ID="hddSecuencia" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Motivo de Compra Local</td>
                                                    <td><telerik:RadComboBox ID="cmbMotivoCL" runat="server" Width="350px" MaxHeight="300px"
                                                        LoadingMessage="Cargando" 
                                                        OnClientSelectedIndexChanged="cmbMotivoCL_ClientSelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td nowrap="nowrap">Autorizador </td>
                                                    <td><telerik:RadTextBox runat="server" Width="350px" Enabled="true" ID="txtAutoriza1" OnBlur="validarEmail" >
                                                            <ClientEvents OnBlur="validarEmail" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td nowrap="nowrap"><div id="divAplicacionOculta2" style="display: none" runat="server">
                                                        Aplicacion * </div></td>
                                                    <td>
                                                    <div id="divAplicacionOculta" style="display: none" runat="server">
                                                    <telerik:RadComboBox ID="cmbAplicacionMotCL" runat="server" Width="350px" 
                                                            DropDownWidth="450" LoadingMessage="Cargando...">
                                                        </telerik:RadComboBox>
                                                    </div>    
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                               
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" nowrap="nowrap">
                                            <telerik:RadGrid ID="rgCorreosAutorizadores" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                PageSize="20" AllowPaging="True" 
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnPageIndexChanged="rgCorreosAutorizadores_PageIndexChanged" OnNeedDataSource="rgCorreosAutorizadores_NeedDataSource"
                                                OnItemCommand="rgCorreosAutorizadores_ItemCommand">
                                                <MasterTableView>
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="columnH1" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Emp" runat="server" Text='<%# Bind("Id_Emp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="columnH2" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Cd" runat="server" Text='<%# Bind("Id_Cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Conf" HeaderText="Id_Conf" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Conf" runat="server" Text='<%# Bind("Id_Conf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_MotivoCL" HeaderText="Id_MotivoCL" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Motivo" runat="server" Text='<%# Bind("Id_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Aplicacion" HeaderText="Id_Aplicacion" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Aplicacion" runat="server" Text='<%# Bind("Id_Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Secuencia" HeaderText="Secuencia" UniqueName="columnH3" visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Desc_MotivoCL" HeaderText="Motivo" UniqueName="column1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc_MotivoCL" runat="server" Text='<%# Bind("Desc_MotivoCL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Aplicacion" HeaderText="Aplicacion" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Concepto" HeaderText="Concepto" UniqueName="column2" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Correo" HeaderText="Correo" UniqueName="column2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCorreo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridTemplateColumn>

                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" ImageUrl="Imagenes/iconos/document_edit.png" 
                                                            Text="Editar" UniqueName="DetailColumn2">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                         </telerik:GridButtonColumn>
                                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Eliminar" ImageUrl="Imagenes/delete2.png" 
                                                            Text="Eliminar" UniqueName="DetailColumn3">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="19px" VerticalAlign="Top"  />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>
                                         </td>
                                        <td >&nbsp;</td>
                                     </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>