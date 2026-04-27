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

    //var liste = document.getElementById('<%=txtListadSelecionados.ClientID %>');

    var i;
    for (i = liste.options.length - 1; i >= 0; i--) {
        if (liste.options[i].selected)
            liste.remove(i);
    }


}




//    $(function () {
//        $("[id$=txtSearchProdServSATAbasto]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoServicioSAT") %>',
//                    data: "{ 'ProdServCve': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfCveProdServSATAbasto]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });

//    $(function () {
//        $("[id$=txtSearchProdServSATACte]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoServicioSAT") %>',
//                    data: "{ 'ProdServCve': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfCveProdServSATCte]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });



//    $(function () {
//        $("[id$=txtSearchProv]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProvidertName") %>',
//                    data: "{ 'provName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfProviderId]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });

//    $(function () {
//        $("[id$=txtProductoLocal]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductoLocal") %>',
//                    data: "{ 'prodName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfProductoLocal]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });

//    $(function () {
//        $("[id$=txtSearchProvAbasto]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorAbasto") %>',
//                    data: "{ 'provName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfProviderAbastoId]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });


//    $(function () {
//        $("[id$=txtSearchProvCte]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorCte") %>',
//                    data: "{ 'provName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfProviderIdCte]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });
//    
//    $(function () {
//        $("[id$=txtBuscaXCodProd]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductName") %>',
//                    data: "{ 'prodName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hdtxtBuscaCodi]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });


//    $(function () {
//        $("[id$=txtBuscaXProvee]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProveedorSoli") %>',
//                    data: "{ 'provName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hdtxtBuscaProv]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });


//    $(function () {
//        $("[id$=txtNomCteListado]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetClienteName") %>',
//                    data: "{ 'clienteName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hdtxtClienteListado]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });


//    $(function () {
//        $("[id$=txtNomCteListadoCons]").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetClienteName") %>',
//                    data: "{ 'clienteName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hdtxtClienteListadoCons]").val(i.item.val);
//            },
//            minLength: 1
//        });
//    });



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


//            $(function () {
//                $("[id$=txtSearch]").autocomplete({
//                    source: function (request, response) {
//                        $.ajax({
//                            url: '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductName") %>',
//                            data: "{ 'prodName': '" + request.term + "'}",
//                            dataType: "json",
//                            type: "POST",
//                            contentType: "application/json; charset=utf-8",
//                            success: function (data) {
//                                response($.map(data.d, function (item) {
//                                    return {
//                                        label: item.split('-')[1],
//                                        val: item.split('-')[0]
//                                    }
//                                }))
//                            },
//                            error: function (response) {
//                                alert(response.responseText);
//                            },
//                            failure: function (response) {
//                                alert(response.responseText);
//                            }
//                        });
//                    },
//                    select: function (e, i) {
//                        $("[id$=hfCustomerId]").val(i.item.val);
//                    },
//                    minLength: 1
//                });
//            });


//        $("#txtSearch").autocomplete({
//            source: function (request, response) {
//                $.ajax({
//                    url: _ApplicationUrl + '/ComprasLocales.aspx/GetProductName',
//                    data: "{ 'prodName': '" + request.term + "'}",
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (data) {
//                        response($.map(data.d, function (item) {
//                            return {
//                                label: item.split('-')[1],
//                                val: item.split('-')[0]
//                            }
//                        }))
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            },
//            select: function (e, i) {
//                $("[id$=hfCustomerId]").val(i.item.val);
//            },
//            minLength: 1
//        });

//console.log('>>>>' + '<%=ResolveUrl("~/ComprasLocales.aspx/GetProductName") %>');
//console.log(_ApplicationUrl + '/ComprasLocales.aspx/GetProductName');



// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
function LimpiarControlesProductoAbasto() {
    //debugger;

    //Controles de la pestaña 'Valuacion de proyectos'                    
    /*
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
    */

    $('#lblTituloProducto').text('');
    $('#txtSearch').val('');
    $('#txtCodigoUsadoProd').val('');
    $('#TextId_Prd').val('');
    $('#txtCodProd').val('');
    $('#TextPrd_Descrpcion').val('');
    $('#txtPresentacion').val('');

    $('#txtTipoProducto').val('');
    $('#cmbTipoProducto').val(0);

    $('#txtTipoProducto').val('');
    $('#cmbTipoProducto').val(0);

    $('#cmbFam').val(0);
    $('#cmbSubFam').val(0);

    $('#txtProveedor').val('');

    $('#cmbProveedor').val(-1);

    $('#cmbUentrada').val(0);
    $('#txtFactorConversion').val('');
    $('#cmbUsalida').val(0);
    $('#txtUempaque').val('');

    $('#cmbCausaDesabasto').val(0);

    $('#tblPedidoDesabastecido > tbody').empty();

    $('#hfCont_PedidoDesabastecido').val(0);

    $('#cmbUnidadMedidaSATDesabasto').val(0);

    $('#tbl_rgPrecios > tbody').empty();

    $('#cmbUnidadMedidaSATDesabasto').val(0);

    $('#cmbProdServicioSATDesabasto').val(0);


    $('#divPedidosRefAbasto').css('dispaly', 'none');

}


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
function EstablecerLabelTituloProductoDesAbasto() {
    try {
        var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
        var TextId_Prd = document.getElementById('<%= TextId_Prd.ClientID %>');
        var txtProveedor = document.getElementById('<%= txtProveedor.ClientID %>');

        var txtCDI = document.getElementById("<%= txtCentrosDist.ClientID %>");

        var txtCategoria = $('#cmbCategorias').val();
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

        label.innerHTML = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
        t6xtCodigo.value = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6)
        $('#txtCodigoUsadoProd').val(t6xtCodigo_);
    }
    catch (e) {
        alert(e.toString());
    }
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
//ENE21-220 RFH 

function EstablecerLabelTituloProductoDesAbasto_() {

    var label = $('#lblTituloProducto').text(); // document.getElementById('<%= lblTituloProducto.ClientID %>');
    var TextId_Prd = $('#TextId_Prd').val(); //document.getElementById('<%= TextId_Prd.ClientID %>');
    var txtProveedor = $('#txtProveedor').val();  //document.getElementById('<%= txtProveedor.ClientID %>');

    var txtCDI = $('#hfCDI').val(); // document.getElementById("<%= txtCentrosDist.ClientID %>");

    var txtCategoria = $('#cmbCategorias').val();

    var lblPrd_Descrpcion = $('#TextPrd_Descrpcion').val();  //document.getElementById('<%= TextPrd_Descrpcion.ClientID %>');
    var t6xtCodigo = $('#txtCodProd').val(); // document.getElementById('<%= txtCodProd.ClientID %>');

    // '00000' + rigth de este lado
    var string_CDI = "000" + txtCDI;
    var string_proveedor = "000000" + txtProveedor;

    if (txtProveedor.length > 5) {
        alert("Proveedor no valido. Favor de contactar a Sistemas");
        return false();
    }

    var string_producto = "00000" + TextId_Prd;
    var string_variable = lblPrd_Descrpcion;

    var label_ = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
    $('#lblTituloProducto').text(label_);
    //$('#txtCodProd').val(string_CDI.slice(-3) + "1" + string_proveedor.slice(-5) + string_producto.slice(-6));

    var t6xtCodigo_ = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6)
    $('#txtCodProd').val(t6xtCodigo_);
    $('#txtCodigoUsadoProd').val(t6xtCodigo_);

}

function EstablecerLabelTituloProductoDesAbasto_2() {

    var label = $('#lblTituloProducto').text(); // document.getElementById('<%= lblTituloProducto.ClientID %>');
    var TextId_Prd = $('#TextId_Prd').val(); //document.getElementById('<%= TextId_Prd.ClientID %>');
    var txtProveedor = $('#txtProveedor').val();  //document.getElementById('<%= txtProveedor.ClientID %>');

    var txtCDI = $('#hfCDI').val(); // document.getElementById("<%= txtCentrosDist.ClientID %>");

    var lblPrd_Descrpcion = $('#TextPrd_Descrpcion').val();  //document.getElementById('<%= TextPrd_Descrpcion.ClientID %>');
    var t6xtCodigo = $('#txtCodProd').val(); // document.getElementById('<%= txtCodProd.ClientID %>');

    // '00000' + rigth de este lado
    var string_CDI = "000" + txtCDI;

    var txtCategoria = $('#cmbCategorias').val();

    var string_proveedor = "000000" + txtProveedor;

    if (txtProveedor.length > 5) {
        alert("Proveedor no valido. Favor de contactar a Sistemas");
        return false();
    }

    var string_producto = "00000" + TextId_Prd;
    var string_variable = lblPrd_Descrpcion;

    var label_ = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
    $('#lblTituloProducto').text(label_);
    //$('#txtCodProd').val(string_CDI.slice(-3) + "1" + string_proveedor.slice(-5) + string_producto.slice(-6));

    var t6xtCodigo_ = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6)
    $('#txtCodProd').val(t6xtCodigo_);
    $('#hfId_Prd').val(t6xtCodigo_);
    $('#hfId_PrdGenerado').val(t6xtCodigo_);
    $('#txtCodigoUsadoProd').val(t6xtCodigo_);

}



// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

function EstablecerLabelTituloProductoAbasto() {
    try {

        var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
        var lblCProd = document.getElementById('<%= lblCodProd.ClientID %>');
        var TextId_Prd = document.getElementById('<%= lblId_Prd.ClientID %>');
        var txtProveedor = document.getElementById('<%= txtProveeAba.ClientID %>');

        var txtCDI = document.getElementById("<%= txtCentrosDist.ClientID %>");
        var txtCategoria = $('#cmbCategorias').val();
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

        label.innerHTML = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6) + " - " + string_variable;
        lblCProd.innerHTML = string_CDI.slice(-3) + txtCategoria + string_proveedor.slice(-5) + string_producto.slice(-6);
        //  alert(lblCProd.innerHTML);
        $('#txtCodigoUsadoProd').val(t6xtCodigo_);
    }
    catch (e) {
        alert(e.toString());
    }
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

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