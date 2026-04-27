/*
Acys2_Func.js
Key Quimica Dic 2018 
08DIC-2020 RFH Actualizado
01DIC-2020 RFH Actualizado
DIC24-2018 RFH 
MAY08 2020 RFH Actualizado
22OCT-2020 RFH Actualizado
      JFCV Alerta de precios 
.- Hacer obligatorios los documentos de entrega "Factura/Remisión" y si "Requiere OC."
*/

//import { substr } from "angular-mocks/ngMock";

var AcysProductosSuma = 0;
// CONTADOR DE PRODUCTOS
var CONT_P = 0; // Acys 2
var CONT_P_CN = 0; // CN
var CONT_P_CC = 0; // CC
var PrdLstIndex = 0;
var PrdNgcIndex = 0;
var Camp_Actualizados = 0;


var ProdNgc = {
    chkTodosProdsNegociados: function (chkAll) {

        var chkBoxList = document.getElementById("tblAcuerdoProdsNegociados");
        var cbs = chkBoxList.getElementsByTagName('input');
        for (var i = 0; i < cbs.length; i++) {
            if (cbs[i].type == 'checkbox') {
                cbs[i].checked = chkAll.checked;
            }
        }
        return false;
    },
    EnviarAcuerdoAll: function () {
        var chkBoxList = document.getElementById("tblAcuerdoProdsNegociados");
        var cbs = chkBoxList.getElementsByTagName('input');
        var trEliminar = [];

        for (var i = 0; i < cbs.length; i++) {

            if (cbs[i].type == 'checkbox') {
                if (cbs[i].checked) {
                    // copiar a Acuerdo Economico

                    if (cbs[i].dataset.rowno !== undefined) {
                        var index = cbs[i].dataset.rowno;

                        var dataReg = JSON.parse($("#hdnDataPrdNgc_" + index).val());
                        ListadoProductos_AddRow(dataReg, dataReg.Estatus, 0, '#tblAcuerdoProds', dataReg.TipoCTA);

                        // Eliminar de Negociados
                        trEliminar.push('#RowPN_' + index);

                        var source = $("#txtProductoBuscar").autocomplete("option", "source");
                        var newSouce = [];

                        for (var j = 0; j < source.length; j++) {
                            if (source[j].idPrd != dataReg.Id_Prd) {
                                newSouce.push(source[j]);
                            }
                        }
                        ProdNgc.LlenarBuscarProducto(newSouce);
                    }

                }
            }
        }
        if (trEliminar.length == 0) {
            alertify.error('Seleccione productos.');
        }

        for (var j = 0; j <= trEliminar.length; j++) {
            $(trEliminar[j]).remove();
        }
        ProdNgc.LimparBusqueda();
        document.getElementById("chbAllProdsNegociados").checked = false;

        return false;
    },
    EnviarAcuerdo: function (elm) {
        var index = $(elm).data('rowno');
        console.log(index);
        // extraer datos
        var dataReg = JSON.parse($("#hdnDataPrdNgc_" + index).val());
        console.log(dataReg);

        ListadoProductos_AddRow(dataReg, dataReg.Estatus, dataReg.PermiteEditar, '#tblAcuerdoProds', dataReg.TipoCTA);

        var source = $("#txtProductoBuscar").autocomplete("option", "source");
        var newSouce = [];

        for (var i = 0; i < source.length; i++) {

            if (source[i].idPrd != dataReg.Id_Prd) {
                newSouce.push(source[i]);
            }
        }
        ProdNgc.LlenarBuscarProducto(newSouce);
        ProdNgc.LimparBusqueda();
        $('#RowPN_' + index).remove();
        return false;
    },
    ListadoPrdNgc_RemoveRow: function (obj) {
        var RowNo = $(obj).data('rowno');
        $('#RowPN_' + RowNo).remove();
    },
    LimparBusqueda: function () {
        $('#txtProductoBuscar').val("");
    },
    LlenarBuscarProducto: function (DataBusqueda) {

        //var source = $("#txtProductoBuscar").autocomplete("option", "source");
        //var newSouce = [];

        //if (source != null) {
        //    for (var i = 0; i < source.length; i++) {
        //        newSouce.push(source[i]);
        //    }
        //}

        //if (DataBusqueda != null) {
        //    for (var j = 0; j < DataBusqueda.length; j++) {
        //        newSouce.push(DataBusqueda[j]);
        //    }
        //}

        $("#txtProductoBuscar").autocomplete({
            source: DataBusqueda,
            focus: function (event, ui) {
                $("#txtProductoBuscar").val(ui.item.desc);
                return false;
            },
            select: function (event, ui) {
                console.log(ui.item.idPrd);
                console.log(ui.item.index);

                $("#chbPrdNgc_" + ui.item.index).prop('checked', true);
                $("#chbPrdNgc_" + ui.item.index).focus();
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.desc + "</div>")
                .appendTo(ul);
        };
    },
    Mover_PrdNegociados: function (elm) {
        console.log("Mover_PrdNegociados");
        var index = $(elm).data("rowno");
        var dataReg = {
            "Id_Prd": parseInt($("#tbCodigo_" + index).val()),//,
            "Prd_Descripcion": $("#lbPrdDescripcion_" + index).html(),
            "Prd_Presentacion": $("#lbPresentacion_" + index).html(),
            "Uni_Descripcion": $("#lbDescripcion_" + index).html(),
            "Acs_Cantidad": $("#tbCant_" + index).val(),
            "PrecioLista": $("#lbPrecioLista_" + index).html(), // limpiar precio
            "Acs_Precio": parseFloat($("#tbPrecio_" + index).val()),
            "Id_Ter": parseInt($("#hfId_Ter_" + index).val()),//
            "Id_AcsDet": parseInt($("#hfId_AcsDet_" + index).val()),//
            "Id_Acys": parseInt($("#hfId_Acys_" + index).val()),//,
            "Id_Matriz": parseInt($("#hfId_Matriz_" + index).val()),//,
            "Acs_Frecuencia": parseInt($("#tbFrecSemana_" + index).val()),
            "Acs_FrecuenciaTipo": parseInt($("#selFrecTipo_" + index).val()),
            "Acs_Documento": $("#tbDocEntrega_" + index).val(),
            "RequiereOC": parseInt($("#tbRequiereOC_" + index).val()),
            "Estatus": $("#hfPrdLstRow_" + index).data("estatus"),
            "PermiteEditar": parseInt($("#tbCodigo_" + index).data("PermiteEditar")),
            "TipoCTA": parseInt($("#hfPrdLstRow_" + index).data("tipocta"))//
            , "Prd_Activo": parseInt($("#hfPrd_Activo_" + index).val()),
        };

        $('#tblAcuerdoProds #RonNo_' + index).remove();
        ListadoProductosNegociados_AddRow(dataReg, dataReg.Estatus, 0, '#tblAcuerdoProdsNegociados', dataReg.TipoCTA);

    }
}

function ListadoProductosNegociados_AddRow(Reg, Estatus, PermiteEditar, tblControl, TipoCTA) {

    var dataReg = {
        "Id_Prd": Reg.Id_Prd,
        "Prd_Descripcion": Reg.Prd_Descripcion,
        "Prd_Presentacion": Reg.Prd_Presentacion,
        "Uni_Descripcion": Reg.Uni_Descripcion,
        "Acs_Cantidad": Reg.Acs_Cantidad,
        "PrecioLista": Reg.PrecioLista,
        "Acs_Precio": Reg.Acs_Precio,
        "Id_Ter": Reg.Id_Ter,
        "Id_AcsDet": Reg.Id_AcsDet,
        "Id_Acys": Reg.Id_Acys,
        "Id_Matriz": Reg.Id_Matriz,
        "Acs_Frecuencia": Reg.Acs_Frecuencia,
        "Acs_FrecuenciaTipo": Reg.Acs_FrecuenciaTipo,
        "Acs_Documento": Reg.Acs_Documento,
        "RequiereOC": Reg.RequiereOC,
        "Estatus": Estatus,
        "PermiteEditar": PermiteEditar,
        "TipoCTA": TipoCTA,
        "Prd_Activo": Reg.Prd_Activo,
    };

    var strData = JSON.stringify(dataReg);
    var strDoc = '';
    var strOC = '';

    PrdNgcIndex = PrdNgcIndex + 1;

    if (PermiteEditar == "0") {
        sReadOnly = 'readonly';
        sDisabled = ' disabled="true" ';
    }


    var PrecioLista = parseFloat(Reg.PrecioLista);
    if (isNaN(PrecioLista)) {
        PrecioLista = 0;
    }
    PrecioLista = PrecioLista.formatMoney(2, '.', ',');


    var row = $('<tr id="RowPN_' + PrdNgcIndex + '"></tr>');

    if (TipoCTA == 2) { // CN
        row.append($('<td style="width:5px; background-color:#ffc845" title="Producto de Cuenta Nacional"></td>'));
    }
    if (TipoCTA == 3) { // CC
        row.append($('<td style="width:5px; background-color:#ffc845" title="Producto de Cuenta Coordinada"></td>'));
    }
    if (TipoCTA == 1) {
        row.append($('<td style="width:5px; background-color:#caccd1" title="Producto de Acuerdo Local"></td>'));
    }

    row.append($('<td id="part_' + PrdNgcIndex + '" style="display:none;" ></td>').append(
        '<input type="hidden" id="hdnDataPrdNgc_' + PrdNgcIndex + '" value="">'
    ));

    // CHEK
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(
        '<input type="checkbox" id="chbPrdNgc_' + PrdNgcIndex + '" ' +
        ' data-rowno="' + PrdNgcIndex + '" ' +
        ' class="form-control chb" style="margin-left:7px;"/>'
    ));

    // CODIGO
    row.append($('<td></td>').append(
        '<label>' + Reg.Id_Prd + '</label>'
    ));

    // DESCRIPCION
    row.append($('<td style="vertical-align: middle!important;"></td>').append(
        '<label>' + Reg.Prd_Descripcion + '</label>'
    ));

    // PRESENTACION
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(
        '<label>' + Reg.Prd_Presentacion + '</label>'
    ));

    // UNIDAD
    row.append($('<td style="text-align:center; vertical-align:top!important;"></td>').append(
        '<label id="lbDescripcion_' + PrdNgcIndex + '">' + Reg.Uni_Descripcion + '</label>'
    ));

    // PRECIO LISTA     
    row.append($('<td style="vertical-align:top!important; text-align:right!important;"></td>').append(
        '<label id="lbPrecioLista_' + PrdNgcIndex + '" style="margin-top:2px;" >' + PrecioLista + '</label>'
    ));

    // PRECIO VENTA 
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(
        '<label id="tbPrecio_' + PrdNgcIndex + '" style="margin-top:2px;" >' + Reg.Acs_Precio + '</label>'
    ));

    // DOCUMENTO DE ENTREGA
    switch (Reg.Acs_Documento) {
        case 'F':
            strDoc = '<label style="margin-top:2px;" >Factura</label>';
            break;
        case 'R':
            strDoc = '<label style="margin-top:2px;" >Remisión</label>';
            break;
        default:
            strDoc = '<label style="margin-top:2px;" >-</label>';
            break;
    }
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(strDoc));

    // REQUIRE ORDEN COMPRA
    switch (Reg.RequiereOC) {
        case 0:
            strOC = '<label style="margin-top:2px;" >No</label>';
            break;
        case 1:
            strOC = '<label style="margin-top:2px;" >Si</label>';
            break;
        default:
            strOC = '<label style="margin-top:2px;" >-</label>';
            break;
    }
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(strOC));

    // BOTON AGREGAR    
    row.append($('<td style="vertical-align:top!important; text-align:center!important;"></td>').append(
        '<button class="btn btn-sm btn-warning" ' +
        ' data-rowno="' + PrdNgcIndex + '" ' +
        ' onclick="ProdNgc.EnviarAcuerdo(this);" >' +
        '<i class= "fa fa-plus fa-lg"></i>&nbsp;</button>'
    ));

    $(tblControl + ' > tbody').append(row);

    $('#hdnDataPrdNgc_' + PrdNgcIndex).val(strData);
}


var ListadoProductos = {
    Actualiza_Cant: function () {
        Camp_Actualizados = 0;
        for (var i = 0; i < 1200; i++) {
            var Codigo = $('#tbCodigo_' + i).val();
            Codigo = parseInt(Codigo);
            if (isNaN(Codigo)) {
                Codigo = 0;
            }

            var Cant = $('#tbCant_' + i).val();

            if (Codigo > 0) {
                var CantDonde = $('#tbActualizarCol_CantDonde').val();
                CantDonde = parseInt(CantDonde);
                if (isNaN(CantDonde)) {
                    CantDonde = 0;
                }
                var CantPoner = $('#tbActualizarCol_CantPoner').val();
                CantPoner = parseInt(CantPoner);
                if (isNaN(CantPoner)) {
                    CantPoner = 0;
                }
                // si la cantidad es igual a la cantidad actualizar
                if (Cant == CantDonde) {
                    $('#tbCant_' + i).val(CantPoner);
                    Camp_Actualizados = Camp_Actualizados + 1;
                }

                ListadoProductos_CalculateRow(null, i);

            }
        }

        $('#modalActualizarDatos').modal('hide');
        alertify.success('Se actualizarón ' + Camp_Actualizados + ' campos');
    },
    Actualiza_NoFrec: function () {
        Camp_Actualizados = 0;
        for (var i = 0; i < 1200; i++) {
            var Codigo = $('#tbCodigo_' + i).val();
            Codigo = parseInt(Codigo);
            if (isNaN(Codigo)) {
                Codigo = 0;
            }

            var FrecSemana = $('#tbFrecSemana_' + i).val();

            if (Codigo > 0) {

                var FrecDonde = $('#tbActualizarCol_FrecDonde').val();
                FrecDonde = parseInt(FrecDonde);
                if (isNaN(FrecDonde)) {
                    FrecDonde = 0;
                }

                var FrecPoner = $('#tbActualizarCol_FrecPoner').val();
                FrecPoner = parseInt(FrecPoner);
                if (isNaN(FrecPoner)) {
                    FrecPoner = 0;
                }
                // si la cantidad es igual a la cantidad actualizar
                if (FrecSemana == FrecDonde) {
                    $('#tbFrecSemana_' + i).val(FrecPoner);
                    Camp_Actualizados = Camp_Actualizados + 1;
                }
            }
        }
        $('#modalActualizarDatos').modal('hide');
        alertify.success('Se actualizarón ' + Camp_Actualizados + ' campos');
    },
    Actualiza_TipoFrec: function () {
        Camp_Actualizados = 0;
        for (var i = 0; i < 1200; i++) {
            var Codigo = $('#tbCodigo_' + i).val();
            Codigo = parseInt(Codigo);
            if (isNaN(Codigo)) {
                Codigo = 0;
            }

            var FrecTipo = $('#selFrecTipo_' + i).val();
            FrecTipo = parseInt(FrecTipo);
            if (isNaN(FrecTipo)) {
                FrecTipo = 0;
            }

            if (Codigo > 0) {

                var FT_Donde = $('#tbActualizarCol_TFrecDonde').val();
                FT_Donde = parseInt(FT_Donde);
                if (isNaN(FT_Donde)) {
                    FT_Donde = 0;
                }

                var FT_Poner = $('#tbActualizarCol_TFrecPoner').val();
                FT_Poner = parseInt(FT_Poner);
                if (isNaN(FT_Poner)) {
                    FT_Poner = 0;
                }
                // Si Cumple Actualizar el Control
                if (FrecTipo == FT_Donde) {
                    $('#selFrecTipo_' + i).val(FT_Poner);
                    let selector = '#reg_fechaInicia_' + i;
                    Camp_Actualizados = Camp_Actualizados + 1;
                }
            }
        }
        $('#modalActualizarDatos').modal('hide');
        alertify.success('Se actualizarón ' + Camp_Actualizados + ' campos');
    },
    Actualiza_DocEntrega: function () { // DOCUMENTO ENTREGA 
        Camp_Actualizados = 0;
        for (var i = 0; i < 1200; i++) {
            var Codigo = $('#tbCodigo_' + i).val();
            Codigo = parseInt(Codigo);
            if (isNaN(Codigo)) {
                Codigo = 0;
            }
            var PermiteEditar = $('#tbCodigo_' + i).data('permiteeditar');
            var Id_Matriz = $('#hfId_Matriz_' + i).val();
            Id_Matriz = parseInt(Id_Matriz);
            if (isNaN(Id_Matriz)) {
                Id_Matriz = 0;
            }

            var DocEntrega = $('#tbDocEntrega_' + i).val();
            /*DocEntrega = parseInt(DocEntrega);
            if (isNaN(DocEntrega)) {
                DocEntrega= 0;
            }
            */

            if (Id_Matriz == 0) {
                if (Codigo > 0 && PermiteEditar == 1) {
                    var DE_Donde = $('#cmbActualizarCol_DocEntregaDonde').val();
                    /*DE_Donde = parseInt(DE_Donde);
                    if (isNaN(DE_Donde)) {
                        DE_Donde= 0;
                    }*/

                    var DE_Poner = $('#cmbActualizarCol_DocEntregaPoner').val();
                    /*DE_Poner = parseInt(DE_Poner);
                    if (isNaN(DE_Poner)) {
                        DE_Poner = 0;
                    }*/
                    // Si Cumple Actualizar el Control
                    if (DocEntrega == DE_Donde) {
                        $('#tbDocEntrega_' + i).val(DE_Poner);
                        Camp_Actualizados = Camp_Actualizados + 1;
                    }
                }
            }

        }
        $('#modalActualizarDatos').modal('hide');
        alertify.success('Se actualizarón ' + Camp_Actualizados + ' campos');
    },
    Actualiza_ReqOC: function () {

        Camp_Actualizados = 0;

        for (var i = 0; i < 1200; i++) {
            var Codigo = $('#tbCodigo_' + i).val();
            Codigo = parseInt(Codigo);
            if (isNaN(Codigo)) {
                Codigo = 0;
            }
            var RequiereOC = $('#tbRequiereOC_' + i).val();
            RequiereOC = parseInt(RequiereOC);
            if (isNaN(RequiereOC)) {
                RequiereOC = 0;
            }
            var Id_Matriz = $('#hfId_Matriz_' + i).val();
            Id_Matriz = parseInt(Id_Matriz);
            if (isNaN(Id_Matriz)) {
                Id_Matriz = 0;
            }

            if (Id_Matriz == 0) {
                if (Codigo > 0) {

                    var ROC_Donde = $('#cmbActualizarCol_ReqOC_Donde').val();
                    ROC_Donde = parseInt(ROC_Donde);
                    if (isNaN(ROC_Donde)) {
                        ROC_Donde = 0;
                    }

                    var ROC_Poner = $('#cmbActualizarCol_ReqOC_Poner').val();
                    ROC_Poner = parseInt(ROC_Poner);
                    if (isNaN(ROC_Poner)) {
                        ROC_Poner = 0;
                    }
                    // Si Cumple Actualizar el Control
                    if (RequiereOC == ROC_Donde) {
                        $('#tbRequiereOC_' + i).val(ROC_Poner);
                        Camp_Actualizados = Camp_Actualizados + 1;
                    }
                }
            }

        }

        $('#modalActualizarDatos').modal('hide');
        alertify.success('Se actualizarón ' + Camp_Actualizados + ' campos');
    }
}

function Combo_Autorizaciones(IdAcys) {

    var Result = '';

    Text = '<div class="dropdown" id="dImportancia_' + IdAcys + '" data-IdAcys="' + IdAcys + '" ' +
        //'onclick="btnIndicarImportancia(this)" ' +
        '>' +
        '<i class="fa fa-envelope-o cGris" data-IdAcys="' + IdAcys + '"></i>' +
        '</div>';

    Result =
        '<div id="dd_EnvioAutorizacion_' + IdAcys + '">' +
        '<div class="btn-group btn-xs" role="group">' +
        '<button type="button" ' +
        'class="btn btn-default dropdown-toggle btn-sm" ' +
        //'style="border:0px solid transparent;" ' +
        'data-toggle="dropdown" ' +
        'aria-haspopup="true" ' +
        'aria-expanded="false">' +
        Text +
        //'&nbsp;' +
        //'<span class="caret"></span>' +
        '</button>' +
        '<ul class="dropdown-menu">' +
        '<li><a data-IdAcys="' + IdAcys + '" data-nivel="1" onclick="btnEnviar_ACyS(this);">Enviar ACyS</a></li>' +
        '<li><a data-IdAcys="' + IdAcys + '" data-nivel="2" onclick="btnEnviar_ControlOrden(this);">Enviar Control de Orden</a></li>' +
        '</ul>' +
        '</div><div>';

    return Result;
}

function ACyS_Modo_ReadOnly() {
    // Establece los controles de solo lectura.

    $('#tbAcs_Fecha').attr("disabled", "disabled");
    $('#tbAcs_Fecha').prop('readonly', true);

    $('#tbAcs_FechaInicio').attr("disabled", "disabled");
    $('#tbAcs_FechaInicio').prop('readonly', true);

    $('#tbCteTerritorio').attr("disabled", "disabled");
    $('#tbCteTerritorio').prop('readonly', true);

    $('#tbAcys_RikMombre').attr("disabled", "disabled");
    $('#tbAcys_RikMombre').prop('readonly', true);

    $('#tbContacto1Nom').attr("disabled", "disabled");
    $('#tbContacto1Nom').prop('readonly', true);

    $('#tbContacto1Nom').prop('readonly', true);
    $('#tbContacto1Nom').prop('readonly', true);

    $('#tbContacto1Tel').attr("disabled", "disabled");
    $('#tbContacto1Tel').prop('readonly', true);

    $('#tbContacto1Tel').prop('readonly', true);
    $('#tbContacto1Tel').prop('readonly', true);

    $('#tbContacto1Correo').attr("disabled", "disabled");
    $('#tbContacto1Correo').prop('readonly', true);

    $('#tbContacto1Correo').prop('readonly', true);
    $('#tbContacto1Correo').prop('readonly', true);

    $('#tbContacto2Nom').attr("disabled", "disabled");
    $('#tbContacto2Nom').prop('readonly', true);

    $('#tbContacto2Tel').attr("disabled", "disabled");
    $('#tbContacto2Tel').prop('readonly', true);

    $('#tbContacto2Correo').attr("disabled", "disabled");
    $('#tbContacto2Correo').prop('readonly', true);

    $('#tbContacto3Nom').attr("disabled", "disabled");
    $('#tbContacto3Nom').prop('readonly', true);

    $('#tbContacto3Tel').attr("disabled", "disabled");
    $('#tbContacto3Tel').prop('readonly', true);

    $('#tbContacto3Correo').attr("disabled", "disabled");
    $('#tbContacto3Correo').prop('readonly', true);

    $('#tbContacto4Nom').attr("disabled", "disabled");
    $('#tbContacto4Nom').prop('readonly', true);

    $('#tbContacto4Tel').attr("disabled", "disabled");
    $('#tbContacto4Tel').prop('readonly', true);

    $('#tbContacto4Correo').attr("disabled", "disabled");
    $('#tbContacto4Correo').prop('readonly', true);

    $('#tbAcs_VigenciaAPartir').attr("disabled", "disabled");
    $('#tbAcs_VigenciaAPartir').prop('readonly', true);

    $('#tbAcs_Semana').attr("disabled", "disabled");
    $('#tbAcs_Semana').prop('readonly', true);

}

function ACyS_Modo_ReadEditar() {

    // Establece los controles de solo lectura.

    $('#tbAcs_Fecha').removeAttr("disabled");
    $('#tbAcs_Fecha').prop('readonly', false);

    $('#tbAcs_FechaInicio').removeAttr("disabled");
    $('#tbAcs_FechaInicio').prop('readonly', false);

    //$('#tbCteTerritorio').removeAttr("disabled");
    //$('#tbCteTerritorio').prop('readonly', false);

    //$('#tbAcys_RikMombre').removeAttr("disabled");
    //$('#tbAcys_RikMombre').prop('readonly', false);

    $('#tbContacto1Nom').removeAttr("disabled");
    $('#tbContacto1Nom').prop('readonly', false);

    $('#tbContacto1Nom').prop('readonly', false);
    $('#tbContacto1Nom').prop('readonly', false);

    $('#tbContacto1Tel').removeAttr("disabled");
    $('#tbContacto1Tel').prop('readonly', false);

    $('#tbContacto1Tel').prop('readonly', false);
    $('#tbContacto1Tel').prop('readonly', false);

    $('#tbContacto1Correo').removeAttr("disabled");
    $('#tbContacto1Correo').prop('readonly', false);

    $('#tbContacto1Correo').prop('readonly', false);
    $('#tbContacto1Correo').prop('readonly', false);

    $('#tbContacto2Nom').removeAttr("disabled");
    $('#tbContacto2Nom').prop('readonly', false);

    $('#tbContacto2Tel').removeAttr("disabled");
    $('#tbContacto2Tel').prop('readonly', false);

    $('#tbContacto2Correo').removeAttr("disabled");
    $('#tbContacto2Correo').prop('readonly', false);

    $('#tbContacto3Nom').removeAttr("disabled");
    $('#tbContacto3Nom').prop('readonly', false);

    $('#tbContacto3Tel').removeAttr("disabled");
    $('#tbContacto3Tel').prop('readonly', false);

    $('#tbContacto3Correo').removeAttr("disabled");
    $('#tbContacto3Correo').prop('readonly', false);

    $('#tbContacto4Nom').removeAttr("disabled");
    $('#tbContacto4Nom').prop('readonly', false);

    $('#tbContacto4Tel').removeAttr("disabled");
    $('#tbContacto4Tel').prop('readonly', false);

    $('#tbContacto4Correo').removeAttr("disabled");
    $('#tbContacto4Correo').prop('readonly', false);

    $('#tbAcs_VigenciaAPartir').removeAttr("disabled");
    $('#tbAcs_VigenciaAPartir').prop('readonly', false);

    //$('#tbAcs_Semana').removeAttr("disabled");
    //$('#tbAcs_Semana').prop('readonly', false);

}

function BuscarYCarga_InformacionDeProducto(Id_Prd, rowno) {
    // validar si existe el id de producto
    var validacion;
    validacion = ValidaDuplicadoPrd(Id_Prd, rowno);
    if (!validacion) {
        //mensaje y termina ejecucion
        alertify.error('El producto esta duplicado.');
        return false;
    }

    Spinner = $('#spinnerBuscando_' + rowno);
    Consulta_Producto(Id_Prd, rowno, Spinner, function (RES, NR) {
        if (RES == null) {
            alertify.error('No se encontro el producto o esta inactivo.');
        } else {
            if (RES?.Prd_Activo == 2) {

                alertify.error('<h5 style="color:white;">Producto inactivo</h5>reemplazalo con otra alternativa/consulta con el area operativa/CEDIS.');

                $('#lbPrdDescripcion_' + NR).text('');
                $('#lbPresentacion_' + NR).text('');
                $('#lbDescripcion_' + NR).text(''); // Unidad
                $('#tbCant_' + NR).val(1);
                $('#tbPrecio_' + NR).val('');
                $('#lbSubTotal_' + NR).val('');
                $('#lbPrecioLista_' + NR).text('');
                return false;
            }
            $('#lbPrdDescripcion_' + NR).text(RES.Prd_Descripcion);
            $('#lbPresentacion_' + NR).text(RES.Prd_Presentacion);
            $('#lbDescripcion_' + NR).text(RES.Uni_Descripcion); // Unidad
            $('#tbCant_' + NR).val(1);
            $('#tbPrecio_' + NR).val(RES.Acs_Precio);
            $('#lbSubTotal_' + NR).val(RES.Acs_Precio);
            $('#lbPrecioLista_' + NR).text(RES.Prd_PrecioLista);

        }
    });
}

function ListadoProductos_RemoveRow(obj) {
    var RowNo = $(obj).data('rowno');
    $('#RonNo_' + RowNo).remove();

    CalcularSuma();
}

function CalcularSuma() {
    var Suma = 0;
    for (var r = 0; r < 500; r++) {

        var SubT = $('#lbSubTotal_' + r).text();
        SubT = SubT.replace(',', '');
        SubT = parseFloat(SubT);
        if (isNaN(SubT)) {
            SubT = 0;
        }

        Suma = Suma + SubT;
    }

    Suma = Suma.formatMoney(2, '.', ',');
    $('#lbAcysProductosSuma').text(Suma);

}

function ListadoProductos_CalculateRow(obj, ROW_) {
    var row = $(obj).data("row");
    row = parseInt(row);
    if (isNaN(row)) {
        row = 0;
    }
    if (row == 0) {
        row = ROW_;
    }

    var Cantidad = $('#tbCant_' + row).val();
    Cantidad = parseFloat(Cantidad);
    if (isNaN(Cantidad)) {
        Cantidad = 0;
    }

    var Precio = $('#tbPrecio_' + row).val();
    // 02OCT-2020 RFH

    var Precio = Precio.replace(/\,/g, '');

    Precio = parseFloat(Precio);
    if (isNaN(Precio)) {
        Precio = 0;
    }

    $('#tbPrecio_' + row).val(Precio);

    try {
        var Importe = Precio * Cantidad;
    } catch (err) {
        var Importe = 0;
    }

    var fsImporte = Importe.formatMoney(2, '.', ',');

    $('#lbSubTotal_' + row).text(fsImporte);

    CalcularSuma();
}

var Frecuencia = {

    ControlChange: function (obj) {
        var IdControl = $(obj).data('id_control');
        var SelFrecTipo = $(obj).val();
        SelFrecTipo = parseInt(SelFrecTipo);
        if (isNaN(SelFrecTipo)) {
            SelFrecTipo = 0;
        }
        switch (SelFrecTipo) {
            case 0: // SIN SEECCION 
                $('#tbFrecSemana_' + IdControl).attr('disabled', false);
                $("#reg_fechaInicia_" + IdControl).attr('disabled', true);
                $("#reg_fechaInicia_" + IdControl).val("");
                break;
            case 1:
                $('#tbFrecSemana_' + IdControl).attr('disabled', false);
                $("#reg_fechaInicia_" + IdControl).attr('disabled', true);
                $("#reg_fechaInicia_" + IdControl).val("");
                break;
            case 2:
            case 3:
            case 4: // SEMANA
                $('#tbFrecSemana_' + IdControl).val('1');
                $('#tbFrecSemana_' + IdControl).attr('disabled', true);
                let now = new Date();
                let defVal = ('0' + (now.getMonth() + 1)).slice(-2) + '/' + now.getFullYear();
                $("#reg_fechaInicia_" + IdControl).attr('disabled', false);
                $("#reg_fechaInicia_" + IdControl).val(defVal);
                break;
        }
    },
    ControlInit: function (IdControl, SelFrecTipo) {
        switch (SelFrecTipo) {
            case 0: // SIN SEECCION 
                $('#tbFrecSemana_' + IdControl).attr('disabled', false);
                break;
            case 1:
                $('#tbFrecSemana_' + IdControl).attr('disabled', false);
                break;
            case 2:
            case 3:
            case 4: // SEMANA
                $('#tbFrecSemana_' + IdControl).val('1');
                $('#tbFrecSemana_' + IdControl).attr('disabled', true);
                break;
        }

    },
    ValidarFrecuencia: function (obj) {
        var IdControl = $(obj).data('row');
        var Frecuencia = $('#tbFrecSemana_' + IdControl).val();
        Frecuencia = parseInt(Frecuencia);
        if (isNaN(Frecuencia)) {
            Frecuencia = 1;
        }

        if (Frecuencia > 3) {
            alertify.alert('La frecuencia esta limtada a (1,2,3).');
            //alert('La frecuencia esta limtada a (1,2,3).');
            Frecuencia = 3;
        }

        $('#tbFrecSemana_' + IdControl).val(Frecuencia);

    }

}

//// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//function Frecuencia_Changed(SelFrecTipo) {
//    var IdControl = $(obj).data('id_control');
//    var SelFrecTipo = $(obj).val();
//    SelFrecTipo = parseInt(SelFrecTipo);
//    if (isNaN(SelFrecTipo)) {
//        SelFrecTipo = 0;
//    }

//    switch (SelFrecTipo) {
//        case 0: // SIN SEECCION 

//            $('#tbFrecSemana_' + IdControl).attr('disabled', false);

//            break;
//        case 1:

//            $('#tbFrecSemana_' + IdControl).attr('disabled', false);

//            break;
//        case 2:
//        case 3:
//        case 4: // SEMANA

//            $('#tbFrecSemana_' + IdControl).val('1');
//            $('#tbFrecSemana_' + IdControl).attr('disabled', true);

//            break;
//    }

//}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Agregar un renglon al Listado de productos.
//

// Añade Renglon de PRODUCTOS de ACYS

function ListadoProductos_AddRow(Reg, Estatus, PermiteEditar, tblControl, TipoCTA) {

    PrdLstIndex = PrdLstIndex + 1;

    var sReadOnly = '';
    var sDisabled = '';
    if (PermiteEditar == "0") {
        sReadOnly = 'readonly';
        sDisabled = ' disabled="true" ';
    }

    var Cant = parseFloat(Reg.Acs_Cantidad);
    if (isNaN(Cant)) {
        Cant = 0;
    }

    var PrecioLista = parseFloat(Reg.PrecioLista);
    if (isNaN(PrecioLista)) {
        PrecioLista = 0;
    }
    PrecioLista = PrecioLista.formatMoney(2, '.', ',');

    var Precio = parseFloat(Reg.Acs_Precio);
    if (isNaN(Precio)) {
        Precio = 0;
    }

    var SubTotal = Precio * Cant;

    AcysProductosSuma = AcysProductosSuma + SubTotal;

    SubTotal = SubTotal.formatMoney(2, '.', ',');

    // Productos Local
    /*
    var Pref_CN = '';
    if (TipoCTA == 1) {
        CONT_P = CONT_P + 1;
        C_P = CONT_P;
    }
    // Productos CN o CC
    if (TipoCTA == 1) {        
        CONT_P_CN = CONT_P_CN + 1;
        C_P = CONT_P_CN;
    }
    */

    let colorCodeInactivo = '';
    let colorText = '';
    //if ((TipoCTA == 1 || TipoCTA == 3) && Reg.Prd_Activo == 2) {
    console.log('Reg.Prd_Activo', Reg.Prd_Activo);
    if (Reg.Prd_Activo == 2) {
        colorCodeInactivo = 'red';
        colorText = 'white';
    }

    var row = $('<tr id="RonNo_' + PrdLstIndex + '">');
    var sPrdLstIndex = '';
    //if (GLOBAL_Activo_AcysCuentasNacionales == 1) { aqui
    if (1 == 1) {
        // CODIGO
        if (TipoCTA == 1) { // L
            row.append($('<td style="width:5px; background-color:#caccd1" title="Producto de Acuerdo Local">').append(sPrdLstIndex));
        }
        if (TipoCTA == 2) { // CN
            //row.append($('<td style="width:5px; background-color:#8db9ca" title="Producto de Cuenta Nacional">').append(sPrdLstIndex));
            row.append($('<td style="width:5px; background-color:#ffc845" title="Producto de Cuenta Nacional">').append(sPrdLstIndex));
        }
        if (TipoCTA == 3) { // CC
            row.append($('<td style="width:5px; background-color:#ffc845" title="Producto de Cuenta Coordinada">').append(sPrdLstIndex));
        }
    } else {
        row.append($('<td style="width:2px; title="Producto de Acuerdo Local">').append(sPrdLstIndex));
    }

    row.append($('<td id="part_' + PrdLstIndex + '" name="reg_part[]" style="display:none;" >').append(
        '<label id="lbNumeroPartida_' + PrdLstIndex + '">' + PrdLstIndex + '</label>' +
        '<input type="hidden" name="reg_tipocta[]" data-tipocta="' + TipoCTA + '" data-id_ter="' + Reg.Id_Ter + '" data-id_acsdet="' + Reg.Id_AcsDet + '" data-estatus="' + Estatus + '"  id="hfPrdLstRow_' + PrdLstIndex + '" value="' + PrdLstIndex + '">' +
        '<input type="hidden" name="reg_idter[]" id="hfId_Ter_' + PrdLstIndex + '" value="' + Reg.Id_Ter + '">' +
        '<input type="hidden" name="reg_id_acsdet[]" id="hfId_AcsDet_' + PrdLstIndex + '" value="' + Reg.Id_AcsDet + '">' +
        '<input type="hidden" name="reg_id_acs[]" id="hfId_Acys_' + PrdLstIndex + '" value="' + Reg.Id_Acys + '">' +
        '<input type="hidden" name="reg_prd_activo[]" id="hfPrd_Activo_' + PrdLstIndex + '" value="' + Reg.Prd_Activo + '">' +
        '<input type="hidden" name="reg_id_matriz[]" id="hfId_Matriz_' + PrdLstIndex + '" value="' + Reg.Id_Matriz + '">'
    ));

    // CODIGO
    row.append($('<td>').append(
        '<input ' +
        'data-rowno="' + PrdLstIndex + '" ' +
        'data-permiteeditar="' + PermiteEditar + '" ' +
        'onkeypress="ProductoCodigo_Keypress(event,this);" ' +
        'onblur="ProductoCodigo_OnBlur(this);" ' +
        'class="form-control input-sm" ' +
        'style="width:80px;color:' + colorText + ';background: ' + colorCodeInactivo + ';" ' +
        'type="text" name="reg_codigo[]" id="tbCodigo_' + PrdLstIndex + '" value="' + Reg.Id_Prd + '" ' + sReadOnly + '>'
    ));

    // DESCRIPCION
    row.append($('<td style="vertical-align: middle!important;">').append(
        '<img id="spinnerBuscando_' + PrdLstIndex + '" style="display: none;" src="../Img/patternfly/spinner-xs.gif">' +
        '<label id="lbPrdDescripcion_' + PrdLstIndex + '">' + Reg.Prd_Descripcion + '</label>'
    ));

    // PRESENTACION
    row.append($('<td style="vertical-align:top!important; text-align:center!important;">').append(
        '<label id="lbPresentacion_' + PrdLstIndex + '">' + Reg.Prd_Presentacion + '</label>'
    ));

    // UNIDAD
    row.append($('<td style="text-align:center; vertical-align:top!important;">').append(
        '<label id="lbDescripcion_' + PrdLstIndex + '">' + Reg.Uni_Descripcion + '</label>'
    ));

    // CANTIDAD
    row.append($('<td>').append(
        '<input type="text" ' +
        'data-row="' + PrdLstIndex + '" ' +
        'style="width:50px;" ' +
        'class="form-control input-sm" ' +
        'name="reg_cantidad[]" id="tbCant_' + PrdLstIndex + '" ' +
        'value="' + Reg.Acs_Cantidad + '" ' +
        'onblur="ListadoProductos_CalculateRow(this);" >'
    ));

    // PRECIO LISTA     
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioLista_' + PrdLstIndex + '" style="margin-top:2px;" >' + PrecioLista + '</label>'
    ));

    // PRECIO VENTA 
    row.append($('<td>').append(
        '<input type="text" ' +
        'data-row="' + PrdLstIndex + '" ' +
        'style="width:60px;" ' +
        'class="form-control input-sm" ' +
        'id="tbPrecio_' + PrdLstIndex + '" ' +
        'name="reg_precio[]" value="' + Reg.Acs_Precio + '" ' +
        'onblur="ListadoProductos_CalculateRow(this);" ' + sReadOnly + ' > '
    ));
    // SUBTOAL
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        //'<input type="text" class="form-control" id="tbubtotal_' + PrdLstIndex + '" value="' + SubTotal + '">'
        '<label id="lbSubTotal_' + PrdLstIndex + '" style="margin-top:2px;" >' + SubTotal + '</label>'
    ));
    // FRECUENCIA / Tipo Frecuencia
    row.append($('<td>').append(
        '<table style="width:120px;">' +
        '<tbody>' +
        '<tr>' +
        '<td style="width:40px;">' +
        '<input ' +
        'id="tbFrecSemana_' + PrdLstIndex + '" ' +
        'style="width:40px;" ' +
        'class="form-control input-sm" ' +
        'type="text" ' +
        'data-row="' + PrdLstIndex + '" ' +
        'onblur="Frecuencia.ValidarFrecuencia(this);" ' +
        'value="' + Reg.Acs_Frecuencia + '">' +

        '</td>' +
        '<td style="width:90px;" >' +
        '<select ' +
        'id="selFrecTipo_' + PrdLstIndex + '" ' +
        'data-id_control="' + PrdLstIndex + '" ' +
        'class="form-control input-sm" onchange="Frecuencia.ControlChange(this);">' +
        '<option value="0">-</option>' +
        '<option value="1">Semanal</option>' +
        '<option value="2">Mensual</option>' +
        '<option value="3">Bimestral</option>' +
        '<option value="4">Trimestral</option>' +
        '<option value="5">Semestral</option>' +
        '</select>' +
        '</td>' +
        '</tr>' +
        '</tbody>' +
        '</table>'
    ));

    // el control calendario selecciona solo mes y año
    row.append($('<td>').append(
        '<input id="reg_fechaInicia_' + PrdLstIndex + '" type="text" class="form-control datepicker-mes" ' +
        'name="reg_fechaInicia[]" value="" ' +
        'placeholder="MM/AAAA" ' +
        'style="width:100px; margin-top:2px!important;" ' +
        sDisabled + '>'
    ));

    // DOCUMENTO DE ENTREGA
    row.append($('<td>').append(
        '<select name="reg_docentrega[]" id="tbDocEntrega_' + PrdLstIndex + '" class="form-control input-sm" ' + sDisabled + '>' +
        '<option value="-">-</option>' +
        '<option value="F">Factura</option>' +
        '<option value="R">Remisión</option>' +
        '</select>'
    ));
    // REQUIRE ORDEN COMPRA
    row.append($('<td>').append(
        '<select name="reg_reqoc[]" id="tbRequiereOC_' + PrdLstIndex + '" class="form-control input-sm" ' + sDisabled + '>' +
        '<option value="">-</option>' +
        '<option value="1">Si</option>' +
        '<option value="0">No</option>' +
        '</select>'
    ));

    var btnRemover = '';

    if (Estatus == 'B' || Estatus == 'B' || sReadOnly == 'readonly') {
        btnRemover = '<i class="fa fa-times-circle fa-2x hover_hand" ' +
            'style="color:orange; margin-top:2px!important;" ' +
            'data-rowno="' + PrdLstIndex + '" ' +
            'onclick="ProdNgc.Mover_PrdNegociados(this);" ' +
            '></i>';
    } else {
        btnRemover = '<i class="fa fa-times-circle fa-2x hover_hand" ' +
            'style="color:red; margin-top:2px!important;" ' +
            'data-rowno="' + PrdLstIndex + '" ' +
            'onclick="ListadoProductos_RemoveRow(this);" ' +
            '></i>';
    }

    row.append($('<td style="vertical-align:top;">').append(btnRemover));

    $(tblControl + ' > tbody').append(row);

    $('#selFrecTipo_' + PrdLstIndex).val(Reg.Acs_FrecuenciaTipo);

    Frecuencia.ControlInit(PrdLstIndex, Reg.Acs_FrecuenciaTipo);

    if (Reg.Acs_Documento == '') {
        $('#tbDocEntrega_' + PrdLstIndex).val('-');
    } else {
        $('#tbDocEntrega_' + PrdLstIndex).val(Reg.Acs_Documento);
    }

    if (Reg.RequiereOC == '') {
        $('#tbRequiereOC_' + PrdLstIndex).val(0);
    } else {
        $('#tbRequiereOC_' + PrdLstIndex).val(Reg.RequiereOC);
    }
    $('#tbDocEntrega_' + PrdLstIndex).focus();


    let selector = '#reg_fechaInicia_' + PrdLstIndex;

    // Inicializa datepicker solo mes/año para el nuevo control reg_fechaInicia_
    $(selector).Zebra_DatePicker({
        format: 'm/Y',
        view: 'months',
        select_other_months: true,
        show_clear_date: false,
        onSelect: function (view, elements) {
            validarMesActual(selector);
        }
    });

    let now = new Date();
    let defVal = ('0' + (now.getMonth() + 1)).slice(-2) + '/' + now.getFullYear();

    if (Reg.Acs_FrecuenciaTipo > 1) {
        if (Reg.Acs_FrecMesIni > 0) {
            $(selector).val(Reg.Acs_FrecMesIni + '/' + Reg.Acs_FrecAnioIni);
        } else {
            $(selector).val(defVal);
        }
        $(selector).attr('disabled', false);
    } else {
        $(selector).attr('disabled', true);
        $(selector).val("");
    }

    Frecuencia.ControlInit(PrdLstIndex, Reg.Acs_FrecuenciaTipo);
}

function ListadoProductos_PreAddRow(TipoCTA) {
    ACyS_ContadorListaProductos = ACyS_ContadorListaProductos + 1;

    var Id_Acys = $('#hfId_Acys').val();
    var Id_Matriz = $('#hfId_Matriz').val();

    var Reg = {
        "Id_Prd": ""
        , "Prd_Descripcion": ""
        , "Prd_Presentacion": ""
        , "Uni_Descripcion": ""
        , "Acs_Precio": ""
        , "Acs_Cantidad": ""
        , "Acs_Frecuencia": ""
        , "Acs_FrecuenciaTipo": 0
        , "Acs_Documento": 0
        , "Id_Ter": 0
        , "Id_AcsDet": ACyS_ContadorListaProductos
        , "Id_Acys": Id_Acys
        , "Id_Matriz": Id_Matriz
    }
    //if (GLOBAL_Activo_AcysCuentasNacionales == 1) {
    if (1 == 1) {
        ListadoProductos_AddRow(Reg, 1, 1, "#tblAcuerdoProds", TipoCTA);
    } else {
        // 1 Local 
        ListadoProductos_AddRow(Reg, 1, 1, "#tblAcuerdoProds", 1);
    }
}

var Acys_Ajax = {

    CatAcysDet_GetList: function (Id_Acys, Acs_Version, Acs_Estatus, TipoCuenta, CALLBACK_Exito) {

        $.ajax({
            url: _ApplicationUrl + '/api/CatAcysDet/GetList' +
                '?IdAcys=' + Id_Acys +
                '&AcsVersion=' + Acs_Version + '&Param1=0' +
                '&TipoCuenta=' + TipoCuenta,
            cache: false,
            type: 'GET',
            async: false,
            /*,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Cargar_AcysByIdDetalle_Ajax, null, $, Id_Acys, Acs_Version, Acs_Estatus, CALLBACK);
                }
            }*/
        }).done(function (response, textStatus, jqXHR) {
            var lst = response.Datos;
            var Mensaje = response.Mensaje;
            var Estado = response.Estado;

            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(lst, Acs_Estatus);
                }
            } else {
                if (Estado == -1) {
                    alertify.error('Error: ' + Mensaje);
                    console.log('Error: ' + Mensaje);
                } else {
                    alertify.error('Error: ' + Mensaje);
                    console.log('Error: ' + Mensaje);
                }
            }

        }).fail(function (jqXHR, textStatus, error) {

            alertify.error('Error: funcion Acys_Ajax.CatAcysDet_GetList().');
            console.log(jqXHR.responseText);
            /*
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvModalPropuestaTE_ver2').modal('hide');
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Error: funcion Cargar_AcysByIdDetalle_Ajax.');
            }
            */
        });
    }
}

var Acys_CN = {

    Productos: function (Id_Cte, Id_Acs, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CapAcysCN/spAcysCN_Productos' +
                '?Id_Cte=' + Id_Cte +
                '&Id_Acs=' + Id_Acs +
                '&AcsVersion=0' +
                '&Param2=0',
            cache: false,
            type: 'GET',
            async: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var RES = response.Datos;
            var Estado = response.Estado;

            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(RES);
                }
            }

            if (Estado == -1) {
                CALLBACK_Error();
            }

        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
            } else {
                alertify.alert('ERROR GRAVE', 'funcion Consultar_AcysCN().');
            }
        });
    },

    Permisos: function (Id_Cte, CALLBACK_Exito, CALLBACK_Error) {

        $.ajax({
            url: _ApplicationUrl + '/api/CapAcysCN/spAcysCN_Permisos' +
                '?Id_Cte=' + Id_Cte +
                '&Id_Acs=0' +
                '&AcsVersion=0' +
                '&Param1=0',
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var RES = response.Datos;
            var Estado = response.Estado;

            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(RES);
                }
            }

            if (Estado == -1) {
                CALLBACK_Error();
            }

        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
            } else {
                alertify.success('Error: funcion Consultar_AcysCN().');
            }
        });

    }

}

var Acys = {

    CargaListadoProductos: function (Id_Acs, Acs_Version, RES, TipoCuenta, IdProd = 0) {
        //$('#tblAcuerdoProds_CN').css('display', 'none');
        // LISTADO DE PRODUCTOS de ACYS
        Acys_Ajax.CatAcysDet_GetList(Id_Acs, Acs_Version, RES.Acs_Estatus, TipoCuenta,
            function (lst, Estatus) {
                //$('#tblAcuerdoProds > tbody').empty();
                if (lst != null) {
                    AcysProductosSuma = 0;
                    lst?.sort((a, b) => (b.Prd_Activo === 2 ? 1 : 0) - (a.Prd_Activo === 2 ? 1 : 0));
                    for (var i = 0; i < lst.length; i++) {

                        ListadoProductos_AddRow(lst[i], Estatus, 1, '#tblAcuerdoProds', 1);

                        //ListadoProductos_AddRow(lst[i], Estatus, RES.Documento_PermiteEditara, '#tblAcuerdoProds', 0);
                        /*Id_AcsDet = parseInt(lst[i].Id_AcsDet);
                        if (Id_AcsDet > ACyS_ContadorListaProductos) {
                            ACyS_ContadorListaProductos = Id_AcsDet;
                        }*/
                    }
                    ACyS_ContadorListaProductos = lst.length;
                    AcysProductosSuma = AcysProductosSuma.formatMoney(2, '.', ',');
                    $('#lbAcysProductosSuma').text(AcysProductosSuma);
                    AgregarProductoReporte(IdProd, TipoCuenta);
                    const productosInactivos = lst.filter(producto => producto.Prd_Activo == 2 && producto.Acs_Cantidad != 0);
                    console.log('productos', lst);
                    if (productosInactivos.length > 0) {
                        //document.getElementById('btnAcys_Guardar').style.display = 'none';
                        alert('Dentro de tu acuerdo se encuentra SKUs catalogados como inactivos, valida y aplica el cambio/actualización correspondiente');
                    }
                }
            }
        );
    },

    // CUENTA NACIONAL 
    CargarProductosCuentaNacional: function (Id_Cte, Id_Acs, Acs_Version, RES) {
        //$('#tblAcuerdoProds_CN').css('display', 'block');
        //alert('Cargar Productos Cuenta Coordinada');
        // LISTADO DE PRODUCTOS de CUENTAS NACIONAL 
        Acys_CN.Productos(Id_Cte, Id_Acs,
            function (lst, Estatus) {
                //$('#tblAcuerdoProds_CN > tbody').empty();
                if (lst != null) {
                    var DataBusqueda = [];
                    AcysProductosSuma = 0;
                    for (var i = 0; i < lst.length; i++) {
                        //ListadoProductos_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProds_CN', 2);
                        if (lst[i].Acs_Cantidad == 0) {
                            ListadoProductosNegociados_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProdsNegociados', 2);
                            DataBusqueda.push({
                                value: lst[i].Id_Prd + " " + lst[i].Prd_Descripcion,
                                desc: lst[i].Id_Prd + " " + lst[i].Prd_Descripcion,
                                idPrd: lst[i].Id_Prd,
                                index: PrdNgcIndex
                            });
                        } else {
                            ListadoProductos_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProds', 2);
                        }
                        Id_AcsDet = parseInt(lst[i].Id_AcsDet);
                        if (Id_AcsDet > ACyS_ContadorListaProductos) {
                            ACyS_ContadorListaProductos = Id_AcsDet;
                        }
                    }
                    var source = $("#txtProductoBuscar").autocomplete("option", "source");
                    if (source != null) {
                        for (var i = 0; i < source.length; i++) {
                            DataBusqueda.push(source[i]);
                        }
                    }
                    ProdNgc.LlenarBuscarProducto(DataBusqueda);



                    const productosInactivos = lst.filter(producto => producto.Prd_Activo == 2 && producto.Acs_Cantidad != 0);
                    console.log('productos2', lst);
                    //if (productosInactivos.length > 0 && (TipoCuenta == 0 || TipoCuenta == 2)) {
                    if (productosInactivos.length > 0) {
                        //document.getElementById('btnAcys_Guardar').style.display = 'none';
                        alert('Dentro de tu acuerdo se encuentra SKUs catalogados como inactivos, valida y aplica el cambio/actualización correspondiente');
                    }
                }
            }, function () {
                //  CALLBACK_Error
                alertify.alert('Ocurrió un Error, Carga de productos de Cuenta Nacional, Reporte el error a soporte.');

            });
    },

    // CUENTA COORDINADA 
    CargarProductosCuentaCoordinada: function (Id_Cte, Id_Acs, Acs_Version, RES) {
        //$('#tblAcuerdoProds_CN').css('display', 'block');
        //alert('Cargar Productos Cuenta Coordinada');
        // LISTADO DE PRODUCTOS de CUENTAS COORDINADA
        Acys_CN.Productos(Id_Cte, Id_Acs,
            function (lst, Estatus) {
                //$('#tblAcuerdoProds_CN > tbody').empty();
                if (lst != null) {
                    var DataBusqueda = [];
                    AcysProductosSuma = 0;
                    for (var i = 0; i < lst.length; i++) {

                        //ListadoProductos_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProds_CN', 3);
                        if (lst[i].Acs_Cantidad == 0) {
                            ListadoProductosNegociados_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProdsNegociados', 3);
                            DataBusqueda.push({
                                value: lst[i].Id_Prd + " " + lst[i].Prd_Descripcion,
                                desc: lst[i].Id_Prd + " " + lst[i].Prd_Descripcion,
                                idPrd: lst[i].Id_Prd,
                                index: PrdNgcIndex
                            });
                        } else {
                            ListadoProductos_AddRow(lst[i], Estatus, 0, '#tblAcuerdoProds', 3);
                        }

                        Id_AcsDet = parseInt(lst[i].Id_AcsDet);
                        if (Id_AcsDet > ACyS_ContadorListaProductos) {
                            ACyS_ContadorListaProductos = Id_AcsDet;
                        }
                    }
                    //var source = $("#txtProductoBuscar").autocomplete("option", "source");
                    //if (source != null) {
                    //    for (var i = 0; i < source.length; i++) {
                    //        DataBusqueda.push(source[i]);
                    //    }
                    //}
                    ProdNgc.LlenarBuscarProducto(DataBusqueda);

                    const productosInactivos = lst.filter(producto => producto.Prd_Activo == 2 && producto.Acs_Cantidad != 0);
                    console.log('productos3', lst);
                    //if (productosInactivos.length > 0 && (TipoCuenta == 0 || TipoCuenta == 2)) {
                    if (productosInactivos.length > 0) {
                        //document.getElementById('btnAcys_Guardar').style.display = 'none';
                        alert('Dentro de tu acuerdo se encuentra SKUs catalogados como inactivos, valida y aplica el cambio/actualización correspondiente');
                    }
                }
            }, function () {
                //  CALLBACK_Error
                alertify.alert('Ocurrió un Error, Carga de productos de Cuenta Coordinada, Reporte el error a soporte.');
            });
    }

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// NOV19-2019 .- CARGA INFROMACION DEL ACYS (NO orden cliente)
// Carga la informacion del ACyS primer tab.
// Y el detalle de productos.
//
function btnAcys_Editar(obj, IdProd = 0) {
    console.log(obj);
    $(obj).attr('disabled', 'disabled');
    var Id_Acs = $(obj).data('id_acs');
    var MyId_Cte = $(obj).data('id_cte');

    var Acs_Version = $(obj).data('acs_version');
    var ProcedenciaRepVI = $(obj).data('procedencia_rep_vi');
    ProcedenciaRepVI = parseInt(ProcedenciaRepVI);
    if (isNaN(ProcedenciaRepVI)) {
        ProcedenciaRepVI = 0;
    }
    if (ProcedenciaRepVI > 0) {
        $('#hfAcs_Procedencia').val(1);
    } else {
        $('#hfAcs_Procedencia').val(0);
    }

    $('#spinnerAcysTitle').css('display', 'block');
    $('#MensajeCargando').show();
    $('#tabOpcPrdNeg').css('display', 'none');
    $('#modalAcys').appendTo("body").modal('show');

    //
    // CARAR ACYS
    //
    Cargar_AcysById_Ajax(Id_Acs, Acs_Version, function (RES) {

        var Id_Cte = RES.Id_Cte;

        $('#hfAcs_Version').val(Acs_Version);
        $('#tbCteTerritorio').css('display', 'block');
        $('#selCteTerritorio').css('display', 'none');

        //$('#modalAcys').appendTo("body").modal('show');
        $("#tabCliente").trigger("click");
        $('#tabPage a[href="#tabCliente"]').tab('show');

        Inicializar_Acys();
        //
        //Carga los cabecera        
        //
        $('#tbCteTerritorio').val(RES.Id_Ter);

        $('#hfId_Acs').val(Id_Acs);
        $('#tbId_Acs').text(Id_Acs);
        $('#hfId_AcsVersion').val(RES.Id_AcsVersion);

        $('#hfAcs_Version').val(RES.Id_AcsVersion);

        $('#tbAcs_Fecha').val(RES.Acs_Fecha);
        $('#tbAcs_FechaInicio').val(RES.Acs_FechaInicio);
        $('#tbAcs_FechaFin').val(RES.Acs_FechaFin);

        $('#ddlAcys_Estatus').val(RES.Acs_Estatus);

        // Datos del cliente 
        $('#tbAcys_CteNombre').val(RES.Cte_Nombre);
        $('#tbAcys_CteNumero').val(RES.Id_Cte);
        $('#hfAcys_CteNumero').val(RES.Id_Cte);

        $('#tbAcys_CteDireccion').val(RES.ClienteDireccion);
        $('#tbAcys_CteCol').val(RES.ClienteColonia);
        $('#tbAcys_CteMunicipio').val(RES.ClienteMunicipio);
        $('#tbAcys_CteCP').val(RES.ClienteCPE);
        $('#tbAcys_CteRFC').val(RES.ClienteRFC);

        $('#tbAcys_CteTerritorio').val(RES.Id_Ter);
        $('#tbAcys_RikMombre').val(RES.Acs_RikNombre);
        $('#tbAcys_RikNumero').val(RES.Id_Rik);

        $('#tbEspAd_CDI').text(RES.Cd_Nombre);
        //
        // IDETIFICA CUENTA CORPORATIVA                
        //


        /*
        if (GLOBAL_Activo_AcysCuentasNacionales == 1) {
            $('#hfId_Acys').val(Permisos.Id);
            $('#hfId_Matriz').val(Permisos.Id_Matriz);
            $('#btnAgregarRenglon').css('display', 'none');
            $('#btnAgregarRenglon_CC').css('display', 'none');
            $('#tblAcuerdoProds_CN > tbody').empty();
            if (RES.TipoCuenta == 1 || RES.TipoCuenta == 2) {
                //console.log(Permisos);
                // 1 Cuenta Nacional 
                if (RES.TipoCuenta == 1) {
                    $('#hfAcysCNTipo').val(Permisos.TipoCuenta);
                    //$('#lbAcysCNTipo').text('Cuenta Nacional');
                    $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-primary">Cuenta Nacional</span>');
                    Acys.CargarProductosCuentaNacional(Id_Cte, Id_Acs, Acs_Version, RES);
                }
                // 2 Coordinada
                if (RES.TipoCuenta == 2) {
                    $('#hfAcysCNTipo').val(Permisos.TipoCuenta);
                    //$('#lbAcysCNTipo').text('Cuenta Coordinada');
                    $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-primary">Cuenta Coordinada</span>');
                    // PRODUCTO CUENTA COORDINADA
                    $('#btnAgregarRenglon').css('display', 'block');
                    Acys.CargarProductosCuentaCoordinada(Id_Cte, Id_Acs, Acs_Version, RES);
                    // PRODUCTOS DE ACYS 2
                    Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES);
                }
            } else {
                $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-default">Local</span>');
                $('#btnAgregarRenglon').css('display', 'block');
                $('#btnAgregarRenglon_CC').css('display', 'none');
                Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES);
            }
            CalcularSuma();
        } else {
            // 20AGO-2021 RFH
            // SIN Cuentas Nacionales
            //$('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-default">Local</span>');
            $('#btnAgregarRenglon').css('display', 'block');
            $('#btnAgregarRenglon_CC').css('display', 'none');
            Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES);
        }
*/


        if (RES.TipoCuenta == 0) {
            //$('#lbEspAd_CuentaCorporativa').text('Si');
            $('#lbEspAd_CuentaCorporativa').val(0);
        } else {
            //$('#lbEspAd_CuentaCorporativa').text('No');
            $('#lbEspAd_CuentaCorporativa').val(1);
        }

        $('#tbEspAd_CuentaCorporativa').val(RES.CuentaCorporativa);
        $('#tbEspAd_NombreComer').val(RES.Cte_Nombre);

        //Contacto PAGOS 
        $('#tbContacto1Nom').val(RES.Acs_Contacto5);
        $('#tbContacto1Tel').val(RES.Acs_Telefono5);
        $('#tbContacto1Correo').val(RES.Acs_Correo5);

        // Contacto COMPRAS
        $('#tbContacto2Nom').val(RES.Acs_Contacto2);
        $('#tbContacto2Tel').val(RES.Acs_Telefono2);
        $('#tbContacto2Correo').val(RES.Acs_email2);
        // Contacto ALMACEN
        $('#tbContacto3Nom').val(RES.Acs_Contacto3);
        $('#tbContacto3Tel').val(RES.Acs_Telefono3);
        $('#tbContacto3Correo').val(RES.Acs_email3);
        // Contacto MANTENIMIENTO
        $('#tbContacto4Nom').val(RES.Acs_Contacto4);
        $('#tbContacto4Tel').val(RES.Acs_Telefono4);
        $('#tbContacto4Correo').val(RES.Acs_email4);

        $('#tbAcs_VigenciaIni').val(RES.Acs_VigenciaIni);
        $('#tbAcs_Semana').val(RES.Acs_Semana);

        $('#tbAcs_VigenciaAPartir').val(RES.Acs_VigenciaApartir);

        //
        // INICIA CONTADORES
        // 

        CONT_P = 0;
        CONT_CN = 0;
        CONT_CC = 0;
        PrdLstIndex = 0;





        // 19AGO-2021 RFH
        //        if (GLOBAL_Activo_AcysCuentasNacionales == 1 ) {aqui

        if (1 == 1) {

            //
            // CUENTA NACIONAL 
            //
            Acys_CN.Permisos(Id_Cte, function (Permisos) {

                $('#hfId_Acys').val(Permisos.Id);
                $('#hfId_Matriz').val(Permisos.Id_Matriz);

                $('#btnAgregarRenglon').css('display', 'none');
                $('#btnAgregarRenglon_CC').css('display', 'none');

                $('#tblAcuerdoProds_CN > tbody').empty();

                if (Permisos.TipoCuenta == 1 || Permisos.TipoCuenta == 2) {

                    $('#tabOpcPrdNeg').css('display', 'block');

                    //console.log(Permisos);
                    // 1 Cuenta Nacional 
                    if (Permisos.TipoCuenta == 1) {
                        $('#hfAcysCNTipo').val(Permisos.TipoCuenta);
                        //$('#lbAcysCNTipo').text('Cuenta Nacional');
                        $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-primary">Cuenta Nacional</span>');

                        Acys.CargarProductosCuentaNacional(Id_Cte, Id_Acs, Acs_Version, RES);
                    }
                    // 2 Coordinada
                    if (Permisos.TipoCuenta == 2) {

                        $('#hfAcysCNTipo').val(Permisos.TipoCuenta);
                        //$('#lbAcysCNTipo').text('Cuenta Coordinada');
                        $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-primary">Cuenta Coordinada</span>');

                        // PRODUCTO CUENTA COORDINADA

                        $('#btnAgregarRenglon').css('display', 'block');

                        Acys.CargarProductosCuentaCoordinada(Id_Cte, Id_Acs, Acs_Version, RES);

                        // PRODUCTOS DE ACYS 2
                        Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES, Permisos.TipoCuenta, IdProd);

                    }
                } else {

                    $('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-default">Local</span>');

                    $('#btnAgregarRenglon').css('display', 'block');
                    $('#btnAgregarRenglon_CC').css('display', 'none');
                    $('#tabOpcPrdNeg').css('display', 'none');
                    Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES, Permisos.TipoCuenta, IdProd);
                }

                CalcularSuma();

            }, function () {
                alertify.alert('Error', 'No se pudo consultar el tipo Cuenta Nacional (fun Acys_CN.Permisos(...).');
            });

        } else {
            // 20AGO-2021 RFH
            // SIN Cuentas Nacionales
            //$('#divAcysCN').html('<span id="spanAcysCN" style="font-size:1em;" class="label label-default">Local</span>');
            $('#btnAgregarRenglon').css('display', 'block');
            $('#btnAgregarRenglon_CC').css('display', 'none');
            Acys.CargaListadoProductos(Id_Acs, Acs_Version, RES);
        }

        //
        // DESPILGA LISTADO LOGS
        //

        $('#tblAcysLogs > tbody').empty();

        EXEC_spCapAcys2_GetLogs_Ajax(Id_Acs, Acs_Version
            , function (lstlogs) {
                // Exito            
                if (lstlogs != null) {
                    for (var i = 0; i < lstlogs.length; i++) {
                        var rowLog = $('<tr>');
                        rowLog.append($('<td style="text-align:left;">').append(
                            lstlogs[i].Fecha + ' ' + lstlogs[i].Nota
                        ));
                        $('#tblAcysLogs > tbody').append(rowLog);
                    }
                    if (lstlogs.length <= 0) {
                        $('#tblAcysLogs > tbody').append('No hay historial.');
                    }
                } else {
                    $('#tblAcysLogs > tbody').append('No hay historial.');
                }

            }, function (Mensaje) {
                // Error            
                $('#tblAcysLogs > tbody').append('Error.');
            });

        $('#lbAcysEstatus').removeClass('label-default');
        $('#lbAcysEstatus').removeClass('label-info');
        $('#lbAcysEstatus').removeClass('label-success');
        $('#lbAcysEstatus').removeClass('label-primary');
        $('#lbAcysEstatus').removeClass('label-warning');

        switch (RES.Acs_Estatus) {
            case 'B':
                $('#lbAcysEstatus').text('Baja');
                $('#lbAcysEstatus').addClass('label-default');
                break;
            case 'C':
                $('#lbAcysEstatus').text('Capturado');
                $('#lbAcysEstatus').addClass('label-info');
                break;
            case 'S':
                $('#lbAcysEstatus').text('Solicitado');
                $('#lbAcysEstatus').addClass('label-primary');
                break;
            case 'A':
                $('#lbAcysEstatus').text('Autorizado');
                $('#lbAcysEstatus').addClass('label-success');
                break;
            case 'R':
                $('#lbAcysEstatus').text('Rechazado');
                $('#lbAcysEstatus').addClass('label-warning');
                break;
            default:
                $('#lbAcysEstatus').text('???');
        }

        // Si no permite editar
        if (RES.Documento_PermiteEditara == "0") {
            ACyS_Modo_ReadOnly();
            $('#btnAcys_Guardar').attr("disabled", "disabled");
            $('#btnAgregarRenglon').attr("disabled", "disabled");
            $('#btnActivarVI').attr("disabled", "disabled");
        } else {
            ACyS_Modo_ReadEditar();
        }

        //
        // SI REQUIERE AUTORIZACION 
        //
        if (RES.Desplegar_BtnAutorizar == "1") {
            $('#btnEjecutarAutorizacion_Gerente').css('display', 'block');
            $('#btnEjecutarRechazar_Gerente').css('display', 'block');
        } else {
            $('#btnEjecutarAutorizacion_Gerente').css('display', 'none');
            $('#btnEjecutarRechazar_Gerente').css('display', 'none');
        }

        $('#hfAcysCNTipo').val('0');
        //$('#lbAcysCNTipo').text('');
        $('#divAcysCN').html('');

        $(obj).removeAttr('disabled');

        $('#MensajeCargando').fadeOut();
        $('#spinnerAcysTitle').css('display', 'none');

    },
        function (RES) {
            $(obj).removeAttr('disabled');
            $('#spinnerAcysTitle').css('display', 'none');
            alertify.error("Ha ocurriodo un error al intentar cargar el documento [" + Id_Acs + "]");
        });

    var res = ConsultaCordinadorCuentaByClienteId(MyId_Cte);
}

var lastday = function (y, m) {
    return new Date(y, m + 1, 0).getDate();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Controles permitidos para ACyS Nuevo 
function Inicializar_Acys_ControlNuevo() {
    $('#lbAcysEstatus').text('Capturado');
    $('#lbAcysEstatus').removeClass('label-warning');
    $('#lbAcysEstatus').addClass('label-info');
    $('#hfAcys_CteNumero').val("");
    $('#hfId_Acs').val("");

    // Cliente
    $('#btnBuscarCliente').removeAttr("disabled");
    $('#btnBuscarCliente').css('display', 'block');
    $('#btnAcys_Guardar').removeAttr("disabled");
    $('#btnAgregarRenglon').removeAttr("disabled");

    $('#btnAgregarRenglon_CC').removeAttr("disabled");

    $('#btnActivarVI').removeAttr("disabled");

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var year = d.getFullYear();
    if (month == 12) {
        year = year + 1;
    }

    var fecha = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + year;

    $('#tbAcs_Fecha').val(fecha);

    //$('#tbAcs_FechaInicio').val(fecha);
    $('#tbAcs_FechaInicio').val('');

    var fecha_fin = lastday(year, 12) + '/12/' + year;

    $('#tbAcs_FechaFin').val(fecha_fin);
    $('#tbCteTerritorio').css('display', 'none');
    $('#selCteTerritorio').css('display', 'block');
}

function Inicializar_Acys() {

    $('#hfId_Acs').text(0);
    $('#hfId_AcsVersion').val(0);
    $('#hfAcs_Version').val(0);
    $('#tbId_Acs').text(0);
    $('#lbAcysEstaus').text('-');

    $('#hfId_Acys').val(0);
    $('#hfId_Matriz').val(0);

    $('#tbAcys_Folio').val('');
    $('#tbAcys_Folio').prop('readonly', true);
    $('#tbAcys_Folio').attr("disabled", "disabled");

    $('#tbAcs_Fecha').val('');
    $('#tbAcs_Fecha').prop('readonly', true);
    $('#tbAcs_Fecha').attr("disabled", "disabled");

    $('#tbAcys_FechaInicio').val('');

    $('#tbAcs_FechaFin').val('');
    $('#tbAcs_FechaFin').prop('readonly', true);
    $('#tbAcs_FechaFin').attr("disabled", "disabled");

    $('#ddlAcys_Estatus').val('');

    $('#tbAcys_CteNombre').val('');
    $('#tbAcys_CteNumero').val('');

    $('#tbAcys_CteDireccion').val('');
    $('#tbAcys_CteCol').val('');

    $('#tbAcys_CteMunicipio').val('');
    $('#tbAcys_CteCP').val('');
    $('#tbAcys_CteRFC').val('');

    $('#tbAcys_CteTerritorio').val('');
    $('#tbAcys_CteTerritorio').prop('readonly', true);
    $('#tbAcys_CteTerritorio').attr("disabled", "disabled");

    //$('#tbCteTerritorio').val('');    

    $('#tbAcys_RikMombre').val('');
    $('#tbAcys_RikMombre').prop('readonly', true);
    $('#tbAcys_RikMombre').attr("disabled", "disabled");

    $('#tbAcys_RikNumero').val('');
    $('#tbAcys_RikNumero').prop('readonly', true);
    $('#tbAcys_RikNumero').attr("disabled", "disabled");

    $('#selCteTerritorio').empty();
    //$('#selCteTerritorio').prop('readonly', true);
    //$('#selCteTerritorio').attr("disabled", "disabled");

    $('#tbCteTerritorio').val('');
    $('#tbCteTerritorio').prop('readonly', true);
    $('#tbCteTerritorio').attr("disabled", "disabled");

    $('#tbCteTerritorio').val('');
    $('#tbCteTerritorio').prop('readonly', true);
    $('#tbCteTerritorio').attr("disabled", "disabled");

    $('#tbAcys_RikMombre').val('');
    $('#tbAcys_RikMombre').prop('readonly', true);
    $('#tbAcys_RikMombre').attr("disabled", "disabled");

    $('#tblAcuerdoProds tbody').empty();
    $('#tblAcuerdoProdsNegociados tbody').empty();
    /* $("#txtProductoBuscar").autocomplete({ source: null});*/
    $('#tblAcuerdoProds_CN tbody').empty();

    $('#lbAcysProductosSuma').text('0.0');

    // Cliente
    $('#btnBuscarCliente').attr("disabled", "disabled");
    $('#btnBuscarCliente').css('display', 'none');

    CONT_P = 0; // Partidas Acys 2
    CONT_P_CN = 0; // Partidas Acys CN

    // CONTACTOS DE CLIENTE

    $('#tbContacto1Nom').val('');
    $('#tbContacto1Tel').val('');
    $('#tbContacto1Correo').val('');

    $('#tbContacto2Nom').val('');
    $('#tbContacto2Tel').val('');
    $('#tbContacto2Correo').val('');

    $('#tbContacto3Nom').val('');
    $('#tbContacto3Tel').val('');
    $('#tbContacto3Correo').val('');

    $('#tbContacto4Nom').val('');
    $('#tbContacto4Tel').val('');
    $('#tbContacto4Correo').val('');

    // Cliente solo lectura 

    $('#tbAcys_CteNombre').prop('readonly', true);
    $('#tbAcys_CteNombre').attr("disabled", "disabled");

    $('#tbAcys_CteNumero').prop('readonly', true);
    $('#tbAcys_CteNumero').attr("disabled", "disabled");

    $('#tbAcys_CteDireccion').prop('readonly', true);
    $('#tbAcys_CteDireccion').attr("disabled", "disabled");

    $('#tbAcys_CteCol').prop('readonly', true);
    $('#tbAcys_CteCol').attr("disabled", "disabled");

    $('#tbAcys_CteMunicipio').prop('readonly', true);
    $('#tbAcys_CteMunicipio').attr("disabled", "disabled");

    $('#tbAcys_CteCP').prop('readonly', true);
    $('#tbAcys_CteCP').attr("disabled", "disabled");

    $('#tbAcys_CteRFC').prop('readonly', true);
    $('#tbAcys_CteRFC').attr("disabled", "disabled");

    $('#tbCteTerritorio').prop('readonly', true);
    $('#tbCteTerritorio').attr("disabled", "disabled");

    $('#tbAcys_CteTerritorio').prop('readonly', true);
    $('#tbAcys_CteTerritorio').attr("disabled", "disabled");

    $('#tbAcys_RikMombre').prop('readonly', true);
    $('#tbAcys_RikMombre').attr("disabled", "disabled");

    $('#tbAcys_RikNumero').prop('readonly', true);
    $('#tbAcys_RikNumero').attr("disabled", "disabled");

    $('#tbAcs_VigenciaAPartir').val('');

    $('#tbAcs_Semana').val('');

    // ESPECIFICACIONES ADICIONALES
    $('#tbEspAd_CDI').text('');

    //$('#lbEspAd_CuentaCorporativa').text('');
    $('#lbEspAd_CuentaCorporativa').val(0);
    $('#tbEspAd_NombreComer').val('');
    $('#Cliente_Alert').css('display', 'none');
    $('#btnAcys_Guardar').removeAttr("disabled");
    $('#btnAcys_Guardar').css('display', 'block');

    $('#btnAgregarRenglon').removeAttr("disabled");

    $('#btnActivarVI').removeAttr("disabled");
    $('#tbAcs_Semana').prop('readonly', true);
    $('#tbAcs_Semana').attr("disabled", "disabled");
    $('#tblAcysLogs > tbody').empty();
    var rowLog = $('<tr>');
    rowLog.append($('<td style="text-align:left;">').append(
        'Vacio.'
    ));
    $('#tblAcysLogs > tbody').append(rowLog);

}

// ELIMINAR
function btnAcys_Eliminar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');

    /*
    alertify.okBtn("Aceptar")
    .cancelBtn("No")
    .confirm("<b>Cancelar Acys</b><br/>¿Est&aacute; seguro de dar de baja el acuerdo #" + Id_Acs + "<br/> con el cliente #" + Id_Cte + " para el territorio #" + Id_Ter + "?", function (ev) 
    {
        ev.preventDefault();
            Acys2_Eliminar(Id_Acs, Acs_Version, Id_Cte, Id_Ter, function () {
            $('#lbEstatusTexto_' + Id_Acs).html('<span id="lbAcysEstaus" class="label label-default">Baja</span>');
            alertify.success('Se ha eliminado el ACyS:' + Id_Acs);
        }, function () {
            alertify.error('Error al tratar de eliminado el ACyS:' + Id_Acs);
        });
    }, function (ev) {
        ev.preventDefault();
    });
    */

    alertify.confirm("<b>Cancelar Acys</b><br/>¿Est&aacute; seguro de dar de baja el acuerdo No.:" + Id_Acs + "<br/> con el cliente: " + Id_Cte + " para el territorio: " + Id_Ter + "?", function (e) {
        if (e) {
            Acys2_Eliminar(Id_Acs, Acs_Version, Id_Cte, Id_Ter, function () {

                $('#lbEstatusTexto_' + Id_Acs).html('<span id="lbAcysEstaus" class="label label-default">Baja</span>');
                $('#lbAcysEstaus_' + Id_Acs).html('<span id="lbAcysEstaus" class="label label-default">Cancelado</span>');

                $('#btnAcys_Eliminar_' + Id_Acs).attr('disabled', 'disabled');
                $('#spinner_AcysIndice').css('display', 'none');
                alertify.success('Se ha eliminado el ACyS:' + Id_Acs);
            }, function () {
                $('#spinner_AcysIndice').css('display', 'none');
                alertify.error('Error al tratar de eliminado el ACyS:' + Id_Acs);
            });
        } else {
            // user clicked "cancel"
            //alert("y");
        }
    });

}

// IMPRIMIR 
function btnAcys_Imprimir(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Id_Ter = $(obj).data('id_ter');
    var Id_Cte = $(obj).data('id_cte');
    $('#modalImprimir').appendTo("body").modal('show');
    // Hay que pasar el RIK aqui.
    $("#iframeVisorReporte").attr("src", 'CapAcys2_VisorReporte.aspx?Id_Acs=' + Id_Acs + '&Acs_Version=' + Acs_Version + '&Id_Ter=' + Id_Ter + '&Id_Cte=' + Id_Cte);
}

// RENOVAR 
function btnAcys_Reenovar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');
    var Vencido = $(obj).data('vencido');

    Acys2_Renovar(Id_Acs, Acs_Version,
        function () {
            // Func Exito
            //alertify.alert('La solicitud se encuentra en estatus no válido para su envio.', function () { alertify.success('Ok'); });

            $('#btnAcys_Editar_' + Id_Acs).click();

            return;
        },
        function () {
            alertify.alert('Error.', function () { alertify.success('Ok'); });
            return;
        }
    );
}

// ENVIAR
function btnAcys_Enviar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');

    /* alertify.okBtn("Aceptar")
                 .cancelBtn("No")
                 .confirm("<b>Envio de ACyS</b><br/>Se enviara el ACyS a autorizacion, </br> ¿Est&aacute; seguro? '#" + Id_Acs + "<br/> con el cliente #" + Id_Cte + " para el territorio #" + Id_Ter + "?", function (ev) {
                     ev.preventDefault();
                     Acys2_Enviar(Id_Acs, Acs_Version, Id_Cte, Id_Ter
                     , function () {
                         $('#lbEstatusTexto_' + Id_Acs).html('<span id="lbAcysEstaus" class="label label-primary">Solicitado</span>');
                         $('#btnAcysEnviar_' + Id_Acs).css('display', 'none');
                         alertify.success('El ACyS #' + Id_Acs + ' ha cambiado de estatus a <b>Solicitado</b>');
                     }, function () {
                         alertify.error('Error al tratar de ENVIAR el ACyS:' + Id_Acs);
                     });
             }, function (ev) {
         ev.preventDefault();
     });*/

    alertify.confirm("<b>Envio de ACyS a Autorizaci&oacute;n</b><br/>Se enviara el ACyS " + Id_Acs + " <br/>Cliente No.:" + Id_Cte + " Territorio: " + Id_Ter + "</br>¿Enviar ahora?", function (e) {
        if (e) {
            Acys2_Enviar(Id_Acs, Acs_Version, Id_Cte, Id_Ter, function () {
                $('#lbEstatusTexto_' + Id_Acs).html('<span id="lbAcysEstaus" class="label label-primary">Solicitado</span>');
                $('#btnAcysEnviar_' + Id_Acs).attr('disabled', 'none');
                $('#spinner_AcysIndice').css('display', 'none');
                alertify.success('El ACyS #' + Id_Acs + ' ha cambiado de estatus a <b>Enviado</b>');
            }, function () {
                $('#spinner_AcysIndice').css('display', 'none');
                alertify.error('Error al tratar de ENVIAR el ACyS:' + Id_Acs);
            });
        } else {
            // user clicked "cancel"
            //alert("y");
        }
    });
}

function btnAcys_Versiones(obj) {
    alert('Boto Versiones');
}

function btnAcys_Autorizar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Vencido = $(obj).data('vencido');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');

    if (Id_Ter == undefined)
        Id_Ter = 0;



    if (Vencido == "SI") {
        alertify.alert('<b>Mensaje</b></br></br>El acuerdo comercial no se encuentra vigente');
        return;
    }

    Acys2_EsAutorizable(Id_Acs, Acs_Version, Id_Cte, Id_Ter, function (RES) {
        //-alert('Se regesa el estatus :' + RES);
        if (RES == 4) {

            $('#lbEstatusTexto_' + Id_Acs).html('<span class="label label-success">Autorizado</span>');
            $('#btnAcysAutorizar_' + Id_Acs).attr("disabled", "disabled");
            $('#btnAcysAutorizar_' + Id_Acs).css("color", "silver");

            alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estatus de documento [Autorizado].');

        } else {
            alertify.alert('<b>Mensaje Error</b></br></br>Ha ocurrido un error al intentar actualizar el estatus del Acys.');
        }
    }, function (RES) {
        alert('ERROR : Se regresa el estatus :' + RES);
    });

}

//  DESPLIEGA MODAL PARA SELCCION DE TIPO DE AUTORIZACION GERENTE O JEFE OP.
// SI YA SE ENVIO LA AUTORIZACION DEHABILITA EL CONTROL. 
function btnAcys_Autorizar_Modal(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Vencido = $(obj).data('vencido');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');
    var Acs_ReqAutGerente = $(obj).data('acs_reqautgerente');
    Acs_ReqAutGerente = parseInt(Acs_ReqAutGerente);
    if (isNaN(Acs_ReqAutGerente)) {
        Acs_ReqAutGerente = 0;
    }
    var Acs_ReqAutJefeOp = $(obj).data('acs_reqautjefeop');
    Acs_ReqAutJefeOp = parseInt(Acs_ReqAutJefeOp);
    if (isNaN(Acs_ReqAutJefeOp)) {
        Acs_ReqAutJefeOp = 0;
    }

    if (Vencido == "SI") {
        alertify.alert('<b>Mensaje</b></br></br>El acuerdo comercial no se encuentra vigente');
        return;
    }

    if (Acs_ReqAutGerente == 1 || Acs_ReqAutGerente == 2) {
        $('#btnEnviar_Autorizacion_Gerente').prop('disabled', true);
        // SI YA ESTA AUT POR GTE. 
        if (Acs_ReqAutGerente == 2) {
            $('#btnEnviar_Autorizacion_Gerente').prop('disabled', false);
        }
    } else {
        $('#btnEnviar_Autorizacion_Gerente').prop('disabled', false);
    }

    if (Acs_ReqAutJefeOp == 1) {
        $('#btnEnviarAutorizacion_ControlOrden').prop('disabled', true);
    } else {
        $('#btnEnviarAutorizacion_ControlOrden').prop('disabled', false);
    }

    $('#hfAut_Id_Acs').val(Id_Acs);
    $('#hfAut_Acs_Version').val(Acs_Version);
    $('#hfAut_Vecido').val(Vencido);
    $('#hfAut_Id_Cte').val(Id_Cte);
    $('#hfAut_Id_Ter').val(Id_Ter);

    $('#modalEnviarAutorizacion').appendTo("body").modal('show');
}

// EL JEFE OP RECHAZA CONTROL DE ORDEN
function btnRechazarControlDeOrden_JefeOp() {
    $('#hfMotivoRechazo_TipoUsuario').val(2); // JEFE OP
    $('#taMotivoRechazo').val('');
    $('#modalMotivoRechazo').appendTo("body").modal('show');
}

// EL GERENTE RECHAZA EL ACYS
function btnRechazarAcys_Gerente() {
    $('#hfMotivoRechazo_TipoUsuario').val(1); // GERENTE
    $('#taMotivoRechazo').val('');
    $('#modalMotivoRechazo').appendTo("body").modal('show');
}

function btnAutorizarControlDeOrden_JefeOp_Rechazo() {
    var TipoUsuario = $('#hfMotivoRechazo_TipoUsuario').val();
    var Notas = $('#taMotivoRechazo').val();
    // 1.- Gerente, 2.-Jefe Op   
    if (TipoUsuario == 1) {
        var Id_Acs = $('#hfId_Acs').val();
        var Acs_Version = $('#hfAcs_Version').val();
        var Id_Cte = $('#hfId_Cte').val();
        var Id_Ter = $('#hfId_Ter').val();
    }
    if (TipoUsuario == 2) {
        var Notas = $('#taMotivoRechazo').val();
        var Id_Acs = $('#hfCO_Id_Acs').val();
        var Acs_Version = $('#hfCO_Acs_Version').val();
        var Id_Cte = $('#hfCO_Id_Cte').val();
        var Id_Ter = $('#hfCO_Id_Ter').val();
    }

    btnAutorizarControlDeOrden_JefeOp_Rechazo_Ajax(
        Id_Acs, Acs_Version, Id_Cte, Id_Ter, Notas, TipoUsuario, // 6 Rechazo
        function (res) {
            if (res == 1) {
                alertify.alert('<b>Error</b></br></br>No se pudo completar el rechazo: ' + Id_Acs);
            }
            if (res == 2) {
                if (TipoUsuario == 1) {
                    $('#modalMotivoRechazo').modal('hide');
                    $('#modalAcys').modal('hide');
                    alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estado del ACyS: ' + Id_Acs);
                    btnCargarListado_Evento();
                }
                if (TipoUsuario == 2) {
                    $('#modalMotivoRechazo').modal('hide');
                    $('#modalOrdenCliente').modal('hide');
                    alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estado del Control de Orden: ' + Id_Acs);
                    btnCargarListado_Evento();
                }
            }
        }
        , function (res) {
            alertify.alert('<b>ERROR</b></br></br>OCURRIO UN ERROR INESPERADO function (btnAutorizarControlDeOrden_JefeOp_Rechazo):' + Id_Acs);
        });

}

/*
function btnAcys_DesplegarModalAutorizar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Vencido = $(obj).data('vencido');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');
    var Id_Ter = $(obj).data('id_ter');
    var Acs_ReqAutGerente = $(obj).data('acs_reqautgerente');
    Acs_ReqAutGerente = parseInt(Acs_ReqAutGerente);
    if (isNaN(Acs_ReqAutGerente)) {
        Acs_ReqAutGerente = 0;
    }
    var Acs_ReqAutJefeOp = $(obj).data('acs_reqautjefeop');
    Acs_ReqAutJefeOp = parseInt(Acs_ReqAutJefeOp);
    if (isNaN(Acs_ReqAutJefeOp)) {
        Acs_ReqAutJefeOp = 0;
    }
    if (Vencido == "SI") {
        alertify.alert('<b>Mensaje</b></br></br>El acuerdo comercial no se encuentra vigente');
        return;
    }
    if (Acs_ReqAutGerente == 1) {
        $('#btnEnviar_Autorizacion_Gerente').prop('disabled', true);
    } else {
        $('#btnEnviar_Autorizacion_Gerente').prop('disabled', false);
    }
    if (Acs_ReqAutJefeOp == 1) {
        $('#btnEnviarAutorizacion_ControlOrden').prop('disabled', true);
    } else {
        $('#btnEnviarAutorizacion_ControlOrden').prop('disabled', false);
    }
    $('#hfAut_Id_Acs').val(Id_Acs);
    $('#hfAut_Acs_Version').val(Acs_Version);
    $('#hfAut_Vecido').val(Vencido);
    $('#hfAut_Id_Cte').val(Id_Cte);
    $('#hfAut_Id_Ter').val(Id_Ter);
    $('#modalEnviarAutorizacion').appendTo("body").modal('show');
}
*/

// ENVIA AUTORIZACION DE GERENTE ACYS
function btnAutorizarAcys_Gerente() {
    var Id_Acs = $('#hfId_Acs').val();
    var Acs_Version = $('#hfAcs_Version').val();
    var Id_Cte = $('#hfId_Cte').val();
    var Id_Ter = $('#hfId_Ter').val();
    var Vecido = $('#hfVencido').val();

    btnAutorizarAcys_Gerente_Ajax(
        Id_Acs, Acs_Version, Id_Cte, Id_Ter, 2,
        function (RES) {

            alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estado del Acuerdo Comercial:' + Id_Acs);

            $('#modalAcys').modal('hide');

            btnCargarListado_Evento();

        }, function (RES) {
            alert('ERROR : Se regesa el estatus :' + RES);
        });
}

// ENVIA AUTORIZACION DE CONTROL DE ORDEN JEFE OP 
function btnAutorizarControlDeOrden_JefeOp() {
    var Id_Acs = $('#hfCO_Id_Acs').val();
    var Acs_Version = $('#hfCO_Acs_Version').val();
    var Id_Cte = $('#hfCO_Id_Cte').val();
    var Id_Ter = $('#hfCO_Id_Ter').val();
    var Vecido = $('#hfCO_Vencido').val();

    btnAutorizarControlDeOrden_JefeOp_Ajax(
        Id_Acs, Acs_Version, Id_Cte, Id_Ter, 2,
        function (RES) {
            alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estado el Control de Ordenes:' + Id_Acs);
            $('#modalOrdenCliente').modal('hide');
            btnCargarListado_Evento();
        }, function (RES) {
            alert('ERROR: btnAutorizarControlDeOrden_JefeOp(' + RES + ')');
        });
}

// ENVIO AUTORIZACION GERENTE 
function Enviar_Autorizacion_Gerente() {
    var hfAut_Id_Acs = $('#hfAut_Id_Acs').val();
    var hfAut_Acs_Version = $('#hfAut_Acs_Version').val();
    var hfAut_Vecido = $('#hfAut_Vecido').val();
    var hfAut_Id_Cte = $('#hfAut_Id_Cte').val();
    var hfAut_Id_Ter = $('#hfAut_Id_Ter').val();

    Enviar_Autorizacion_Gerente_Ajax(hfAut_Id_Acs, hfAut_Acs_Version, hfAut_Id_Cte, hfAut_Id_Ter, 1,
        function (RES) {

            alertify.alert('<b>Mensaje</b></br></br>Se ha enviado una solicitud de autorizaci&oacute;n.');
            $('#modalEnviarAutorizacion').modal('hide');
            btnCargarListado_Evento();

            //-alert('Se regesa el estatus :' + RES);
            /*
            if (RES == 4) {
            $('#lbEstatusTexto_' + Id_Acs).html('<span class="label label-success">Autorizado</span>');
            $('#btnAcysAutorizar_' + Id_Acs).attr("disabled", "disabled");
            $('#btnAcysAutorizar_' + Id_Acs).css("color", "silver");
            alertify.alert('<b>Mensaje</b></br></br>Se ha actualizado el estatus de documento.');
            } else {
            alertify.alert('<b>Mensaje Error</b></br></br>Ha ocurrido un error al intentar actualizar el estatus del Acys.');
            }
            */
        }, function (RES) {
            $('#modalEnviarAutorizacion').modal('hide');
            alert('ERROR : Enviar_Autorizacion_Gerente (' + RES + ')');
        });
}

// ENVIO AUTORIZACION JEFE OP
function Enviar_Autorizacion_JefeOp() {
    var hfAut_Id_Acs = $('#hfAut_Id_Acs').val();
    var hfAut_Acs_Version = $('#hfAut_Acs_Version').val();
    var hfAut_Vecido = $('#hfAut_Vecido').val();
    var hfAut_Id_Cte = $('#hfAut_Id_Cte').val();
    var hfAut_Id_Ter = $('#hfAut_Id_Ter').val();

    Enviar_Autorizacion_JefeOp_Ajax(hfAut_Id_Acs, hfAut_Acs_Version, hfAut_Id_Cte, hfAut_Id_Ter, 1,
        function (RES) {

            alertify.alert('<b>Mensaje</b></br></br>Se ha enviado una solicitud de autorizaci&oacute;n.');
            $('#modalEnviarAutorizacion').modal('hide');
            btnCargarListado_Evento();

        }, function (RES) {
            $('#modalEnviarAutorizacion').modal('hide');
            alert('ERROR : Se regesa el estatus :' + RES);
        });

}

function btnAcys_Reactivar(obj) {
    var Id_Acs = $(obj).data('id_acs');
    var Acs_Version = $(obj).data('acs_version');
    var Vencido = $(obj).data('vencido');
    var Id_Cte = $(obj).data('id_cte');
    var Id_Ter = $(obj).data('id_ter');

    alertify.confirm("<b>Reactivar Acys</b><br/>¿Est&aacute; seguro que desea reactivar el documento #" + Id_Acs + "<br/> con el cliente #" + Id_Cte + " para el territorio #" + Id_Ter + "?", function (e) {
        if (e) {
            //
            // MAY08-2020 RFH Id_Ter debe tener un valor 
            //
            //Acys2_Reactivar(Id_Acs, Acs_Version, Id_Cte, Id_Ter, function (RES) {
            Acys2_Reactivar(Id_Acs, Acs_Version, Id_Cte, 0, function (RES) {
                //-alert('Se regesa el estatus :' + RES);
                if (RES == 4) {
                    alertify.alert('<b>Mensaje</b></br></br>Usted no es usuario Gerente.');
                }
                if (RES == 2) {
                    alertify.alert('<b>Exito</b></br></br>Se ha actualizado el estatus del documento a [Capturado] exitosamente.');
                    $('#lbEstatusTexto_' + Id_Acs).html('<span class="label label-info">Capturado</span>');
                    $('#btnAcysReactivar_' + Id_Acs).css("display", "none");
                }
                if (RES == 3) {
                    alertify.alert('<b>Mensaje</b></br></br>No es posible actualizar el estatus de documento actual.');
                }
                if (RES == 1) {
                    alertify.alert('<b>Mensaje</b></br></br>No fue posible actualizar el estatus del documento [Result=1].');
                }

            }, function (RES) {
                alert('ERROR : Se regesa el estatus :' + RES);
            });

        } else {
            // user clicked "cancel"
            //alert("y");
        }
    });

}

// Helper para parsear MM/YYYY -> { mes, anio }
function ParsearMesAnio(valor) {
    if (!valor)
        return { mes: 0, anio: 0 };
    valor = valor.trim();
    // Esperamos longitud 7: "MM/YYYY"
    if (valor.length !== 7 || valor.indexOf('/') !== 2)
        return { mes: 0, anio: 0 };
    var partes = valor.split('/');
    if (partes.length !== 2)
        return { mes: 0, anio: 0 };
    var mes = parseInt(partes[0], 10);
    var anio = parseInt(partes[1], 10);
    if (isNaN(mes) || mes < 1 || mes > 12) mes = 0;
    if (isNaN(anio) || anio < 1000 || anio > 9999) anio = 0;

    return { mes: mes, anio: anio };
}

// GUARDAR 
// NOV19-2019 RFH Se corrige lectura de valor de Version de documento [hfAcs_Version]
function ACyS_Guardar(CALLBACK_Exito) {

    var SpinnerAcysGuardando = $('#spinnerAcysGuardando');
    var AcysCNTipo = $('#hfAcysCNTipo').val();

    let reg_prd_activo = document.getElementsByName("reg_prd_activo[]");
    var found = Array.from(reg_prd_activo).some(function (input) {
        return input.value == "2";
    });

    if (found) {
        alertify.alert('Dentro de tu acuerdo se encuentra SKUs catalogados como inactivos, valida y aplica el cambio/actualización correspondiente');
        return;
    }

    //$('#spinnerAcysGuardando').css('display', 'block');
    var hfAcys_CteNumero = $('#hfAcys_CteNumero').val();
    hfAcys_CteNumero = parseInt(hfAcys_CteNumero);
    if (isNaN(hfAcys_CteNumero)) {
        hfAcys_CteNumero = 0;
    }
    if (hfAcys_CteNumero <= 0) {

        alertify.alert('La información del cliente no es valida.');
        return;
    }
    var Id_Acs = $('#hfId_Acs').val();
    Id_Acs = parseInt(Id_Acs);
    if (Id_Acs <= 0) {
        Modal_Mensaje(1, 'Error', 'El folio del ACyS es erroneo.');
        return;
    }
    var Acs_Semana = $('#tbAcs_Semana').val();
    Acs_Semana = parseInt(Acs_Semana);

    //var Id_AcsVersion = $('#hfId_AcsVersion').val();
    var Id_AcsVersion = $('#hfAcs_Version').val();
    Id_AcsVersion = parseInt(Id_AcsVersion);

    var Acs_Procedencia = $('#hfAcs_Procedencia').val();
    Acs_Procedencia = parseInt(Acs_Procedencia);
    if (isNaN(Acs_Procedencia)) {
        Acs_Procedencia = 0;
    }
    // Se tiene que pasar la entidad completa 
    // los datos que falten aqui se completaran en la entidad.

    var Acs_Fecha = $('#tbAcs_FechaInicio').val()
    Acs_Fecha = format_YYYYMMDD(Acs_Fecha);

    var Acs_FechaInicio = $('#tbAcs_FechaInicio').val()
    Acs_FechaInicio = format_YYYYMMDD(Acs_FechaInicio);
    if (Acs_FechaInicio == "" || Acs_FechaInicio == "--") {
        alertify.alert('No ha establecido el dato [Fecha inicio].');
        return;
    }

    var Acs_FechaFin = $('#tbAcs_FechaFin').val();
    Acs_FechaFin = format_YYYYMMDD(Acs_FechaFin);

    var Acs_VigenciaAPartir = $('#tbAcs_VigenciaAPartir').val()
    Acs_VigenciaAPartir = format_YYYYMMDD(Acs_VigenciaAPartir);
    if (Acs_VigenciaAPartir == "" || Acs_VigenciaAPartir == "--") {
        alertify.alert('No ha establecido el dato [Vigencia a Partir de].');
        return;
    }

    var tbContacto1Nom = $('#tbContacto1Nom').val().trim().length;
    var tbContacto1Tel = $('#tbContacto1Tel').val().trim().length;
    var tbContacto1Correo = $('#tbContacto1Correo').val().trim().length;
    var tbContacto2Nom = $('#tbContacto1Correo').val().trim().length;
    var tbContacto2Tel = $('#tbContacto2Tel').val().trim().length;
    var tbContacto2Correo = $('#tbContacto2Correo').val().trim().length;

    if (tbContacto1Nom == 0 || tbContacto1Tel == 0 || tbContacto1Correo == 0 ||
        tbContacto2Nom == 0 || tbContacto2Tel == 0 || tbContacto2Correo == 0) {
        alertify.alert('Los contactos del cliente Pagos y Compras son obligatorios.');
        return;
    }

    var Id_Ter = $('#selCteTerritorio').val();

    var eACyS2 = {
        'Id_Emp': 0,
        'Id_Cd': 0,
        'Id_Acs': $('#hfId_Acs').val(),
        'Id_Ter': Id_Ter,
        'Id_Rik': 0,
        'Id_Cte': hfAcys_CteNumero,
        'Acs_NomComercial': $('#tbAcys_CteNombre').val(),
        'Acs_Fecha': "",
        // Contacto PRINCIPAL
        'Acs_Contacto': "",
        'Acs_Puesto': "",
        'Acs_Telefono': "",
        'Acs_email': "",

        'Acs_Contacto2': $('#tbContacto2Nom').val(),
        'Acs_Telefono2': $('#tbContacto2Tel').val(),
        'Acs_Email2': $('#tbContacto2Correo').val(),

        'Acs_Contacto3': $('#tbContacto3Nom').val(),
        'Acs_Telefono3': $('#tbContacto3Tel').val(),
        'Acs_email3': $('#tbContacto3Correo').val(),

        'Acs_Contacto4': $('#tbContacto4Nom').val(),
        'Acs_Telefono4': $('#tbContacto4Tel').val(),
        'Acs_email4': $('#tbContacto4Correo').val(),

        'Acs_Contacto5': $('#tbContacto1Nom').val(),
        'Acs_Telefono5': $('#tbContacto1Tel').val(),
        'Acs_email5': $('#tbContacto1Correo').val(),

        'Acs_Contacto6': "",
        'Acs_Telefono6': 0,
        'Acs_email6': "",
        'Acs_NumPrv': "",
        'Acs_Ruta1': 0,
        'Acs_Ruta2': 0,
        'Acs_ReqOrden': 0,
        'Acs_VigenciaApartir': Acs_VigenciaAPartir,
        'Acs_ReqConfirmacion': 0,
        'Acs_RecCorreo': 0,
        'Acs_RecFax': 0,
        'Acs_RecTelefono': 0,
        'Acs_RecRepresentante': 0,
        'Acs_RecOtro': 0,
        'Acs_RecOtroDesc': "",
        'Acs_Estatus': "",
        'Id_U': 0,
        'Acs_Semana': Acs_Semana,
        'Acs_VigenciaTermina': "",
        'Acs_PedidoEncargadoEnviar': "",
        'Acs_PedidoPuesto': "",
        'Acs_Pedidotelefono': "", 'Acs_Pedidotelefono2': "",
        'Acs_PedidoEmail': "", 'Acs_PedidoEmail2': "",
        'Acs_RecDocReposicion': 0,
        'Acs_RecDocFolio': 0,
        'Acs_RecDocOtro': "",
        'Acs_Contado': 0,
        'Acs_VisitaOtro': "",
        'Acs_ReqServAsesoria': 0,
        'Acs_ReqServTecnicoRelleno': 0,
        'Acs_ReqServMantenimiento': 0,
        'Acs_Notas': "",
        'Acs_ContactoRepVenta': 0,
        'Acs_ContactoRepVentaTel': "",
        'Acs_ContactoRepVentaEmail': "",
        'Acs_ContactoRepServ': 0,
        'Acs_ContactoRepServTel': "",
        'Acs_ContactoRepServEmail': "",
        'Acs_ContactoJefServ': 0,
        'Acs_ContactoJefServTel': "",
        'Acs_ContactoJefServEmail': "",
        'Acs_ContactoAseServ': 0,
        'Acs_ContactoAseServTel': "",
        'Acs_ContactoAseServEmail': "",
        'Acs_ContactoJefOper': 0,
        'Acs_ContactoJefOperTel': "",
        'Acs_ContactoJefOperEmail': "",
        'Acs_ContactoCAlmRep': 0,
        'Acs_ContactoCAlmRepTel': "",
        'Acs_ContactoCAlmRepEmail': "",
        'Acs_ContactoCServTec': 0,
        'Acs_ContactoCServTecTel': "",
        'Acs_ContactoCServTecEmail': "",
        'Acs_ContactoCCreCob': 0,
        'Acs_ContactoCCreCobTel': "",
        'Acs_ContactoCCreCobEmail': "",
        'Acs_FechaInicio': Acs_FechaInicio,
        'Acs_FechaFin': Acs_FechaFin,
        'Acs_Modalidad': "",
        'Acs_ConsigFechaFin': "",
        'Acs_CanTotal': 0,
        'Acs_VisFrecuencia': 0,
        'Acs_version': 0,
        'Id_AcsVersion': Id_AcsVersion,
        'Id_CteDirEntrega': 0,
        'Id_Val': 0,
        'EsCuentaNacional': "",
        'Acs_Sucursal': "",
        'Acs_ParcialidadesSi': 0,
        'Acs_ParcialidadesNo': 0,
        'Acs_ConfirmacionPedidosSI': 0,
        'Acs_ConfirmacionPedidosnO': 0,
        'Acs_chkRecRevLunes': 0,
        'Acs_RecRevMartes': 0,
        'Acs_RecRevMiercoles': 0,
        'Acs_RecRevJueves': 0,
        'Acs_RecRevViernes': 0,
        'Acs_RecRevSabado': 0,
        'Acs_TimePicker1': "",
        'Acs_TimePicker2': "",
        'Acs_TimePicker3': "",
        'Acs_TimePicker4': "",
        'Acs_RecPersonaRecibe': "",
        'Acs_RecPuesto': "",
        'Acs_RecCitaMismoDia': 0,
        'Acs_RecCitaSinCita': 0,
        'Acs_RecCitaPrevia': 0,
        'Acs_RecCitaContacto': "",
        'Acs_RecCitaTelefono': "",
        'Acs_RecCitaDiasdeAnticipacion': 0,
        'Acs_RecAreaPropia': 0,
        'Acs_RecAreaPlaza': 0,
        'Acs_RecAreaCalle': 0,
        'Acs_RecAreaAvTransitada': 0,
        'Acs_RecEstCortesia': 0,
        'Acs_RecEstCosto': 0,
        'Acs_RecEstMonto': 0,
        'Acs_RecDocFactFranquiciaEnt': 0,
        'Acs_RecDocFactFranquiciaEntCop': 0,
        'Acs_RecDocFactFranquiciaRec': 0,
        'Acs_RecDocFactFranquiciaRecCop': 0,
        'Acs_RecDocFactKeyEnt': 0,
        'Acs_RecDocFactKeyEntCop': 0,
        'Acs_RecDocFactKeyRec': 0,
        'Acs_RecDocFactKeyRecCop': 0,
        'Acs_RecDocOrdCompraEnt': 0,
        'Acs_RecDocOrdCompraEntCop': 0,
        'Acs_RecDocOrdCompraRec': 0,
        'Acs_RecDocOrdCompraRecCop': 0,
        'Acs_RecDocOrdReposEnt': 0,
        'Acs_RecDocOrdReposEntCop': 0,
        'Acs_RecDocOrdReposRec': 0,
        'Acs_RecDocOrdReposRecCop': 0,
        'Acs_RecDocCopPedidoEnt': 0,
        'Acs_RecDocCopPedidoEntCop': 0,
        'Acs_RecDocCopPedidoRec': 0,
        'Acs_RecDocCopPedidoRecCop': 0,
        'ACS_RecDocRemisionEnt': 0,
        'ACS_RecDocRemisionEntCop': 0,
        'ACS_RecDocRemisionRec': 0,
        'ACS_RecDocRemisionRecCop': 0,
        'ACS_RecDocFolioEnt': 0,
        'ACS_RecDocFolioEntCop': 0,
        'ACS_RecDocFolioRec': 0,
        'ACS_RecDocFolioRecCop': 0,
        'ACS_RecDocContraRecEnt': 0,
        'ACS_RecDocContraRecEntCop': 0,
        'ACS_RecDocContraRecRec': 0,
        'ACS_RecDocContraRecRecCop': 0,
        'ACS_RecDocEntAlmacenEnt': 0,
        'ACS_RecDocEntAlmacenEntCop': 0,
        'ACS_RecDocEntAlmacenRec': 0,
        'ACS_RecDocEntAlmacenRecCop': 0,
        'ACS_RecDocSopServicioEnt': 0,
        'ACS_RecDocSopServicioEntCop': 0,
        'ACS_RecDocSopServicioRec': 0,
        'ACS_RecDocSopServicioRecCop': 0,
        'ACS_RecDocNomFirmaEnt': 0,
        'ACS_RecDocNomFirmaEntCop': 0,
        'ACS_RecDocNomFirmaoRec': 0,
        'ACS_RecDocNomFirmaRecCop': 0,
        'ACS_RecCitaEnt': 0,
        'ACS_RecCitaEntCop': 0,
        'ACS_RecCitaRec': 0,
        'ACS_RecCitaRecCop': 0,
        'ACS_RecOtroRec': "",
        'ACS_chk62Lunes': 0,
        'ACS_chk62Martes': 0,
        'ACS_chk62Miercoles': 0,
        'ACS_chk62Jueves': 0,
        'ACS_chk62Viernes': 0,
        'ACS_chk62Sabado': 0,
        'ACS_RadTimePicker162': "",
        'ACS_RadTimePicker262': "",
        'ACS_RadTimePicker362': "",
        'ACS_RadTimePicker462': "",
        'ACS_txtRecPersonaRecibe62': "",
        'ACS_txtRecPuesto62': "",
        'ACS_Chk62Mismodia': 0,
        'ACS_Chk62Sincita': 0,
        'ACS_Chk62Previa': 0,
        'ACS_txt62CitaContacto': "",
        'ACS_txt62CitaTelefono': "",
        'ACS_txt62CitaDiasdeAnticipacion': 0,
        'ACS_chk62AreaPropia': 0,
        'ACS_chk62AreaPlaza': 0,
        'ACS_chk62AreaCalle': 0,
        'ACS_chk62AreaAvTransitada': 0,
        'ACS_chk62EstCortesia': 0,
        'ACS_chk62EstCosto': 0,
        'ACS_txt62EstMonto': 0,
        'ACS_txt62ClienteDireccion': "",
        'ACS_txt62ClienteColonia': "",
        'ACS_txt62ClienteMunicipio': "",
        'ACS_txt62ClienteEstado': "",
        'ACS_txt62ClienteCodPost': "",
        'ACS_chk62DocFactFranquiciaEnt': 0,
        'ACS_txt62DocFactFranquiciaEntCop': 0,
        'ACS_chk62DocFactFranquiciaRec': 0,
        'ACS_txt62DocFactFranquiciaRecCop': 0,
        'ACS_chk62DocFactKeyEnt': 0,
        'ACS_txt62DocFactKeyEntCop': 0,
        'ACS_chk62DocFactKeyRec': 0,
        'ACS_txt62DocFactKeyRecCop': 0,
        'ACS_chk62DocOrdCompraEnt': 0,
        'ACS_txt62DocOrdCompraEntCop': 0,
        'ACS_chk62DocOrdCompraRec': 0,
        'ACS_txt62DocOrdCompraRecCop': 0,
        'ACS_chk62DocOrdReposEnt': 0,
        'ACS_txt62DocOrdReposEntCop': 0,
        'ACS_chk62DocOrdReposRec': 0,
        'ACS_txt62DocOrdReposRecCop': 0,
        'ACS_chk62DocCopPedidoEnt': 0,
        'ACS_txt62DocCopPedidoEntCop': 0,
        'ACS_chk62DocCopPedidoRec': 0,
        'ACS_txt62DocCopPedidoRecCop': 0,
        'ACS_chk62DocRemisionEnt': 0,
        'ACS_txt62DocRemisionEntCop': 0,
        'ACS_chk62DocRemisionRec': 0,
        'ACS_txt62DocRemisionRecCop': 0,
        'ACS_chk62DocFolioEnt': 0,
        'ACS_txt62DocFolioEntCop': 0,
        'ACS_chk62DocFolioRec': 0,
        'ACS_txt62DocFolioRecCop': 0,
        'ACS_chk62DocContraRecEnt': 0,
        'ACS_txt62DocContraRecEntCop': 0,
        'ACS_chk62DocContraRecRec': 0,
        'ACS_txt62DocContraRecRecCop': 0,
        'ACS_chk62DocEntAlmacenEnt': 0,
        'ACS_txt62DocEntAlmacenEntCop': 0,
        'ACS_chk62DocEntAlmacenRec': 0,
        'ACS_txt62DocEntAlmacenRecCop': 0,
        'ACS_chk62DocSopServicioEnt': 0,
        'ACS_txt62DocSopServicioEntCop': 0,
        'ACS_chk62DocSopServicioRec': 0,
        'ACS_txt62DocSopServicioRecCop': 0,
        'ACS_chk62DocNomFirmaEnt': 0,
        'ACS_txt62DocNomFirmaEntCop': 0,
        'ACS_chk62DocNomFirmaoRec': 0,
        'ACS_txt62DocNomFirmaRecCop': 0,
        'ACS_chk62CitaEnt': 0,
        'ACS_txt62CitaEntCop': 0,
        'ACS_chk62CitaRec': 0,
        'ACS_txt62CitaRecCop': 0,
        'ACS_chk63Lunes': 0,
        'ACS_chk63Martes': 0,
        'ACS_chk63Miercoles': 0,
        'ACS_chk63Jueves': 0,
        'ACS_chk63Viernes': 0,
        'ACS_chk63Sabado': 0,
        'ACS_Rad63TimePicker163': "",
        'ACS_Rad63TimePicker263': "",
        'ACS_Rad63TimePicker363': "",
        'ACS_Rad63TimePicker463': "",
        'ACS_txtRecPersonaRecibe63': "",
        'ACS_txtRecPuesto63': "",
        'ACS_Chk63Mismodia': 0,
        'ACS_Chk63Sincita': 0,
        'ACS_Chk63Previa': 0,
        'ACS_txt63CitaContacto': "",
        'ACS_txt63CitaTelefono': "",
        'ACS_txt63CitaDiasdeAnticipacion': 0,
        'ACS_chk63AreaPropia': 0,
        'ACS_chk63AreaPlaza': 0,
        'ACS_chk63AreaCalle': 0,
        'ACS_chk63AreaAvTransitada': 0,
        'ACS_chk63EstCortesia': 0,
        'ACS_chk63EstCosto': 0,
        'ACS_txt63EstMonto': 0,
        'ACS_txt63ClienteDireccion': "",
        'ACS_txt63ClienteColonia': "",
        'ACS_txt63ClienteMunicipio': "",
        'ACS_txt63ClienteEstado': "",
        'ACS_txt63ClienteCodPost': "",
        'ACS_chk63DocFactFranquiciaEnt': 0,
        'ACS_txt63DocFactFranquiciaEntCop': 0,
        'ACS_chk63DocFactFranquiciaRec': 0,
        'ACS_txt63DocFactFranquiciaRecCop': 0,
        'ACS_chk63DocFactKeyEnt': 0,
        'ACS_txt63DocFactKeyEntCop': 0,
        'ACS_chk63DocFactKeyRec': 0,
        'ACS_txt63DocFactKeyRecCop': 0,
        'ACS_chk63DocOrdCompraEnt': 0,
        'ACS_txt63DocOrdCompraEntCop': 0,
        'ACS_chk63DocOrdCompraRec': 0,
        'ACS_txt63DocOrdCompraRecCop': 0,
        'ACS_chk63DocOrdReposEnt': 0,
        'ACS_txt63DocOrdReposEntCop': 0,
        'ACS_chk63DocOrdReposRec': 0,
        'ACS_txt63DocOrdReposRecCop': 0,
        'ACS_chk63DocCopPedidoEnt': 0,
        'ACS_txt63DocCopPedidoEntCop': 0,
        'ACS_chk63DocCopPedidoRec': 0,
        'ACS_txt63DocCopPedidoRecCop': 0,
        'ACS_chk63DocRemisionEnt': 0,
        'ACS_txt63DocRemisionEntCop': 0,
        'ACS_chk63DocRemisionRec': 0,
        'ACS_txt63DocRemisionRecCop': 0,
        'ACS_chk63DocFolioEnt': 0,
        'ACS_txt63DocFolioEntCop': 0,
        'ACS_chk63DocFolioRec': 0,
        'ACS_txt63DocFolioRecCop': 0,
        'ACS_chk63DocContraRecEnt': 0,
        'ACS_txt63DocContraRecEntCop': 0,
        'ACS_chk63DocContraRecRec': 0,
        'ACS_txt63DocContraRecRecCop': 0,
        'ACS_chk63DocEntAlmacenEnt': 0,
        'ACS_txt63DocEntAlmacenEntCop': 0,
        'ACS_chk63DocEntAlmacenRec': 0,
        'ACS_txt63DocEntAlmacenRecCop': 0,
        'ACS_chk63DocSopServicioEnt': 0,
        'ACS_txt63DocSopServicioEntCop': 0,
        'ACS_chk63DocSopServicioRec': 0,
        'ACS_txt63DocSopServicioRecCop': 0,
        'ACS_chk63DocNomFirmaEnt': 0,
        'ACS_txt63DocNomFirmaEntCop': 0,
        'ACS_chk63DocNomFirmaoRec': 0,
        'ACS_txt63DocNomFirmaRecCop': 0,
        'ACS_chk63CitaEnt': 0,
        'ACS_txt63CitaEntCop': 0,
        'ACS_chk63CitaRec': 0,
        'ACS_txt63CitaRecCop': 0,
        'Acs_NumericTextBox': 0,
        'Rec_Whats': 0,
        'Acs_OrdenAbiertaConRep': 0,
        'Acs_RekRIK': 0,
        'Acs_RecWhatsApp': 0,
        'Acs_RecPedWhats': 0,
        'Acs_EspecsAdic1': 0,
        'ACS_chk63CualquierDia': 0,
        'ACS_ST_Aplicar': 0,
        'ACS_SA_Aplicar': 0,
        'ACS_chk62CualquierDia': 0,
        'ACS_SA_Tipo': 0,
        'ACS_SC_Lunes': 0,
        'ACS_SC_Martes': 0,
        'ACS_SC_Miercoles': 0,
        'ACS_SC_Jueves': 0,
        'ACS_SC_Viernes': 0,
        'ACS_SC_CualquierDia': 0,
        'ACS_SC_Aplicar': 0,
        'ACS_SC_Horario1': "",
        'ACS_SC_Horario2': "",
        'ACS_SC_Horario3': "",
        'ACS_SC_Horario4': "",
        'ACS_SC_CitaPrev_MismoDia': 0,
        'ACS_SC_CitaPrev_Pevia': 0,
        'ACS_SC_CitaPrev_Tipo': 0,
        'ACS_Aud_Aplicar': 0,
        'ACS_Aud_CitaPrev_Tipo': 0,
        'ACS_Aud_Lunes': 0,
        'ACS_Aud_Martes': 0,
        'ACS_Aud_Miercoles': 0,
        'ACS_Aud_Jueves': 0,
        'ACS_Aud_Viernes': 0,
        'ACS_Aud_CualquierDia': 0,
        'ACS_Aud_Horario1': "",
        'ACS_Aud_Horario2': "",
        'ACS_Aud_Horario3': "",
        'ACS_Aud_Horario4': "",
        'ACS_Aud_CitaPrev_MismoDia': 0,
        'ACS_Aud_CitaPrev_Pevia': 0,
        'ACS_SC_Tipo': 0,
        'ACS_Aud_Tipo': 0,
        'Acs_Procedencia': Acs_Procedencia
    }

    /// validar productos duplicados
    if (!ValidaTodosDuplicadoPrd()) {
        return false;
    }


    var tbCant_Vacio = 0;
    var tbDocEntrega_Vacio = 0;
    var tbRequiereOC_Vacio = 0;
    var tbFrecuenca_Vacio = 0;
    var tbFrecuencaTipo_Vacio = 0;
    var tbFrecuencia_MayorA3 = 0;
    //    
    // LISTADO PRODUCTOS ACYS - LOCAL    
    //
    var lstDetalleProductos = []

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    // JFCV alertadeprecios 
    // este array es para guardar los productos que se vendieron por debajo del precio de venta 
    var lstDetalleProductosAlerta = []
    var validaralertas = 0;
    for (var i = 0; i < 1200; i++) {
        var hfPrdLstRow = $('#hfPrdLstRow_' + i);
        var TipoCTA = $('#hfPrdLstRow_' + i).data('tipocta');

        if (TipoCTA == 1) {

            var tbCodigo = $('#tbCodigo_' + i).val();
            tbCodigo = parseInt(tbCodigo);
            if (isNaN(tbCodigo)) {
                tbCodigo = 0;
            }
            var tbCant = $('#tbCant_' + i).val();
            tbCant = parseFloat(tbCant);
            if (isNaN(tbCant)) {
                tbCant = 0;
            }
            var tbPrecio = $('#tbPrecio_' + i).val();
            tbPrecio = parseFloat(tbPrecio);
            if (isNaN(tbPrecio)) {
                tbPrecio = 0;
            }

            // El NUMERO de Semanas
            var tbFrecSemana = $('#tbFrecSemana_' + i).val();
            tbFrecSemana = parseFloat(tbFrecSemana);
            if (isNaN(tbFrecSemana)) {
                tbFrecSemana = 0;
            }

            // TIPO de FRECUENCIA
            var tbFrecTipo = $('#selFrecTipo_' + i).val();
            tbFrecTipo = parseFloat(tbFrecTipo);
            if (isNaN(tbFrecTipo)) {
                tbFrecTipo = 0;
            }

            let fechaStr = $("#reg_fechaInicia_" + i).val();
            let frecMes = 0;
            let frecAnio = 0;

            if (fechaStr) {
                let partes = fechaStr.split('/');
                let tempMes = parseInt(partes[0], 10);
                let tempAño = parseInt(partes[1], 10);

                // Si no es un número válido, deja en 0
                frecMes = isNaN(tempMes) ? 0 : tempMes;
                frecAnio = isNaN(tempAño) ? 0 : tempAño;
            }

            var tbDocEntrega = $('#tbDocEntrega_' + i).val();
            /*tbDocEntrega=parseInt(tbDocEntrega);
            if (isNaN(tbDocEntrega)) {
            tbDocEntrega= 0;
            }*/

            var tbRequiereOC = $('#tbRequiereOC_' + i).val();
            tbRequiereOC = parseInt(tbRequiereOC);
            if (isNaN(tbRequiereOC)) {
                tbRequiereOC = 0;
            }
            var thfId_Ter = $('#thfId_Ter_' + i).val();
            thfId_Ter = parseInt(thfId_Ter);
            if (isNaN(thfId_Ter)) {
                thfId_Ter = 0;
            }

            var hfId_AcsDet = $('#hfId_AcsDet_' + i).val();
            hfId_AcsDet = parseInt(hfId_AcsDet);
            if (isNaN(hfId_AcsDet)) {
                hfId_AcsDet = 0;
            }

            if (tbCodigo > 0) {
                if (tbCodigo > 0) {
                    if (tbCant == 0) {
                        tbCant_Vacio = tbCant_Vacio + 1;
                    }
                    // El TIPO Frecuenia
                    if (tbFrecTipo == 0) {
                        tbFrecuencaTipo_Vacio = tbFrecuencaTipo_Vacio + 1;
                    }
                    if (tbDocEntrega == '-') {
                        tbDocEntrega_Vacio = tbDocEntrega_Vacio + 1;
                    }
                    if (tbRequiereOC == 0) {
                        tbRequiereOC_Vacio = tbRequiereOC_Vacio + 1;
                    }
                    // El NUMERO de Semanas
                    if (tbFrecSemana == 0) {
                        tbFrecuenca_Vacio = tbFrecuenca_Vacio + 1;
                    }

                    // Frecuencia > 3 
                    if (tbFrecSemana > 3) {
                        tbFrecuencia_MayorA3 = tbFrecuencia_MayorA3 + 1;
                    }
                    var Row = {
                        'Id_Acys': 0,
                        'Id_Matriz': 0,
                        'Id_Emp': 0,
                        'Id_Cd': 0,
                        'Id_Acs': Id_Acs,
                        'Id_AcsDet': hfId_AcsDet,
                        'Id_REg': 0,
                        'Id_Prd': tbCodigo,
                        'Acs_Cantidad': tbCant,
                        'Acs_Frecuencia': tbFrecSemana,
                        'Acs_FrecuenciaTipo': tbFrecTipo,
                        'Acs_Lunes': 0,
                        'Acs_Martes': 0,
                        'Acs_Miercoles': 0,
                        'Acs_Jueves': 0,
                        'Acs_Viernes': 0,
                        'Acs_Sabado': 0,
                        'Acs_Documento': tbDocEntrega,
                        'Acs_Precio': tbPrecio,
                        'Id_Ter': thfId_Ter,
                        'Acs_UltSCpt': 0,
                        'Acs_UltACpt': 0,
                        'Acs_Modalidad': 0,
                        'Acs_ConsigFechaInicio': 0,
                        'Acs_ConsigFechaFin': 0,
                        'Acs_canTTotal': 0,
                        'Id_AcsVersion': Id_AcsVersion,
                        'Id_TG': 0,
                        'RequiereOC': tbRequiereOC , // 29 items
                        'Acs_FrecMesIni': frecMes,
                        'Acs_FrecAnioIni': frecAnio
                    }
                    lstDetalleProductos.push(Row);

                    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                    // JFCV alertadeprecios 
                    // por cada producto que quiera guardar valido si el precio de venta es menor al precio minimo de rik 
                    // Id_Emp, Id_Cd, Id_Cte, Id_Prd,Precio_Vta, Id_Rik, Id_Ter, 
                    Spinner = $('#spinnerBuscando_' + tbCodigo);
                    debugger;
                    var tbter = $('#tbCteTerritorio').val();

                    if (Id_Ter == null) {
                        Id_Ter = tbter;
                    }

                    AlertaPrecioValidaPrecio(1, 0, hfAcys_CteNumero, tbCodigo, tbPrecio, 0, Id_Ter, Spinner, i, $('#tbCant_' + i).val(), $('#lbPrdDescripcion_' + i).text(), $('#tbAcys_CteNombre').val(), function (RES, NR) {
                        if (RES != null) {

                            RES.Prd_Descripcion = $('#lbPrdDescripcion_' + NR).text();
                            RES.Cantidad = $('#tbCant_' + NR).val();
                            RES.Cte_NomComercial = $('#tbAcys_CteNombre').val();
                            //$('#tbPrecio_' + NR).val(RES.Acs_Precio);
                            //$('#lbSubTotal_' + NR).val(RES.Acs_Precio);

                            var RowAlerta = {
                                'Id_Emp': RES.Id_Emp,
                                'Id_Cd': RES.Id_Cd,
                                'Id_Cte': RES.Id_Cte,
                                'IdRepresentante': RES.IdRepresentante,
                                'Id_Ter': RES.Id_Ter,
                                'IdAutorizacionAnterior': RES.IdAutorizacionAnterior,
                                'Id_Prd': RES.Id_Prd,
                                'Precio_MinimoRik': RES.Precio_MinimoRik,
                                'Precio_MinimoGte': RES.Precio_MinimoGte,
                                'Precio_Vta': RES.Precio_Vta,
                                'PrecioLista': RES.PrecioLista,
                                'Cte_NomComercial': RES.Cte_NomComercial,
                                'Prd_Descripcion': RES.Prd_Descripcion,
                                'PrecioLista': RES.PrecioLista,
                                'Precio_AAA': RES.Precio_AAA,
                                'Utilidad': RES.Utilidad,
                                'Porc_Utilidad': RES.Porc_Utilidad,
                                'TipoAutorizacion': 2,
                                'FechaVigencia': RES.FechaVigencia,
                                'Id_Cpr': RES.Id_Cpr,
                                'JustificacionMemo': RES.JustificacionMemo

                            }
                            //validaralertas = 1;
                            if (RowAlerta != null) {
                                validaralertas = 1;

                            }
                            //lstDetalleProductosAlerta.push(RowAlerta);              
                        }
                    });
                }
            }

        }
        //
    }

    //
    // LISTADO PRODUCTOS CUENTA NACIONAL y COORDINADA 
    //
    var lstDetalleProductos_CN = []
    var ValidaCantidad = 0;
    var ValidaFrecuencua = 0;
    for (var i = 0; i < 1200; i++) {

        var hfPrdLstRow = $('#hfPrdLstRow_' + i);
        var TipoCTA = $('#hfPrdLstRow_' + i).data('tipocta');

        if (TipoCTA == 2 || TipoCTA == 3) {

            var tbCodigo = $('#tbCodigo_' + i).val();
            tbCodigo = parseInt(tbCodigo);
            if (isNaN(tbCodigo)) {
                tbCodigo = 0;
            }
            var tbCant = $('#tbCant_' + i).val();
            tbCant = parseFloat(tbCant);
            if (isNaN(tbCant)) {
                tbCant = 0;
            }
            if (tbCant == 0) {
                alertify.alert('Listado de productos, el dato "Cantidad" debe ser mayor a 0.');
                $('#tbCant_' + i).focus();
                return;
            }
            var tbPrecio = $('#tbPrecio_' + i).val();
            tbPrecio = parseFloat(tbPrecio);
            if (isNaN(tbPrecio)) {
                tbPrecio = 0;
            }

            // El NUMERO de Semanas
            var tbFrecSemana = $('#tbFrecSemana_' + i).val();
            tbFrecSemana = parseFloat(tbFrecSemana);
            if (isNaN(tbFrecSemana)) {
                tbFrecSemana = 0;
            }
            if (tbFrecSemana == 0) {
                alertify.alert('Listado de productos, el dato "Freciencia" debe ser mayor a 0.');
                $('#tbFrecSemana_' + i).focus();
                return;
            }
            // TIPO de FRECUENCIA
            var tbFrecTipo = $('#selFrecTipo_' + i).val();
            tbFrecTipo = parseFloat(tbFrecTipo);
            if (isNaN(tbFrecTipo)) {
                tbFrecTipo = 0;
            }
            if (tbFrecTipo == 0) {
                alertify.alert('Listado de productos, seleccione el tipo de "Freciencia"');
                $('#selFrecTipo_' + i).focus();
                return;
            }

            let fechaStr = $("#reg_fechaInicia_" + i).val();
            let frecMes = 0;
            let frecAnio = 0;

            if (fechaStr) {
                let partes = fechaStr.split('/');
                let tempMes = parseInt(partes[0], 10);
                let tempAño = parseInt(partes[1], 10);

                // Si no es un número válido, deja en 0
                frecMes = isNaN(tempMes) ? 0 : tempMes;
                frecAnio = isNaN(tempAño) ? 0 : tempAño;
            }

            var tbDocEntrega = $('#tbDocEntrega_' + i).val();

            var tbRequiereOC = $('#tbRequiereOC_' + i).val();
            tbRequiereOC = parseInt(tbRequiereOC);
            if (isNaN(tbRequiereOC)) {
                tbRequiereOC = 0;
            }
            var thfId_Ter = $('#thfId_Ter_' + i).val();
            thfId_Ter = parseInt(thfId_Ter);
            if (isNaN(thfId_Ter)) {
                thfId_Ter = 0;
            }

            var hfId_AcsDet = $('#hfId_AcsDet_' + i).val();
            hfId_AcsDet = parseInt(hfId_AcsDet);
            if (isNaN(hfId_AcsDet)) {
                hfId_AcsDet = 0;
            }

            // ID_ACYS        
            var hfId_Acys = $('#hfId_Acys_' + i).val();
            hfId_Acys = parseInt(hfId_Acys);
            if (isNaN(hfId_Acys)) {
                hfId_Acys = 0;
            }
            // ID_MATRIZ
            var hfId_Matriz = $('#hfId_Matriz_' + i).val();
            hfId_Matriz = parseInt(hfId_Matriz);
            if (isNaN(hfId_Matriz)) {
                hfId_Matriz = 0;
            }

            if (tbCodigo > 0) {
                if (tbCodigo > 0) {

                    if (tbDocEntrega == '-') {
                        tbDocEntrega_Vacio = tbDocEntrega_Vacio + 1;
                    }
                    // En producto de CN no cuenta los que faltan
                    if (tbRequiereOC == 0) {
                        //tbRequiereOC_Vacio = tbRequiereOC_Vacio + 1;
                    }
                    var Row = {
                        'Id_Acys': hfId_Acys,
                        'Id_Matriz': hfId_Matriz,
                        'Id_Emp': 0,
                        'Id_Cd': 0,
                        'Id_Acs': Id_Acs,
                        'Id_AcsDet': hfId_AcsDet,
                        'Id_REg': 0,
                        'Id_Prd': tbCodigo,
                        'Acs_Cantidad': tbCant,
                        'Acs_Frecuencia': tbFrecSemana,
                        'Acs_FrecuenciaTipo': tbFrecTipo,
                        'Acs_Lunes': 0,
                        'Acs_Martes': 0,
                        'Acs_Miercoles': 0,
                        'Acs_Jueves': 0,
                        'Acs_Viernes': 0,
                        'Acs_Sabado': 0,
                        'Acs_Documento': tbDocEntrega,
                        'Acs_Precio': tbPrecio,
                        'Id_Ter': thfId_Ter,
                        'Acs_UltSCpt': 0,
                        'Acs_UltACpt': 0,
                        'Acs_Modalidad': 0,
                        'Acs_ConsigFechaInicio': 0,
                        'Acs_ConsigFechaFin': 0,
                        'Acs_canTTotal': 0,
                        'Id_AcsVersion': Id_AcsVersion,
                        'Id_TG': 0,
                        'RequiereOC': tbRequiereOC,
                        'Acs_FrecMesIni': frecMes,
                        'Acs_FrecAnioIni': frecAnio
                    }
                    lstDetalleProductos_CN.push(Row);
                }
            }

            //revisa si cantidad es cero y se tiene frecuencia establecida mandara mensaje de devolucion
            if (tbCant == 0) {
                if (tbFrecTipo == 0) {

                }
                else {
                    ValidaFrecuencua = ValidaFrecuencua + 1;
                }
            }

            //revisa si frecuencia no esta establecida y se cantidad diferente de cero mandara mensaje de devolucion
            if (tbFrecTipo == 0) {
                if (tbCant == 0) {

                }
                else {
                    ValidaCantidad = ValidaCantidad + 1;
                }
            }

        }
    }

    // recorrer los productos de la tabla productos negociados
    var lstTr = document.getElementById("tblAcuerdoProdsNegociados");
    var getId;
    var index;
    var dataReg;
    //var lstDataReg = [];
    const tableBody = document.getElementById('tblAcuerdoProdsNegociados');

    for (const row of tableBody.rows) {
        getId = row.id;
        if (getId != "") {
            index = getId.replace("RowPN_", "");
            dataReg = JSON.parse($("#hdnDataPrdNgc_" + index).val());
            var Row = {
                'Id_Acys': dataReg.Id_Acys,
                'Id_Matriz': dataReg.Id_Matriz,
                'Id_Emp': 0,
                'Id_Cd': 0,
                'Id_Acs': Id_Acs, // $('#hfId_Acs').val();
                'Id_AcsDet': dataReg.Id_AcsDet,
                'Id_REg': 0,
                'Id_Prd': dataReg.Id_Prd,
                'Acs_Cantidad': 0,// 0 para productos negociados
                'Acs_Frecuencia': dataReg.Acs_Frecuencia,
                'Acs_FrecuenciaTipo': dataReg.Acs_FrecuenciaTipo,
                'Acs_Lunes': 0,
                'Acs_Martes': 0,
                'Acs_Miercoles': 0,
                'Acs_Jueves': 0,
                'Acs_Viernes': 0,
                'Acs_Sabado': 0,
                'Acs_Documento': dataReg.Acs_Documento,
                'Acs_Precio': dataReg.Acs_Precio,
                'Id_Ter': dataReg.Id_Ter,
                'Acs_UltSCpt': 0,
                'Acs_UltACpt': 0,
                'Acs_Modalidad': 0,
                'Acs_ConsigFechaInicio': 0,
                'Acs_ConsigFechaFin': 0,
                'Acs_canTTotal': 0,
                'Id_AcsVersion': Id_AcsVersion,
                'Id_TG': 0,
                'RequiereOC': dataReg.RequiereOC,
                'Acs_FrecMesIni': 0,
                'Acs_FrecAnioIni': 0
            }
            lstDetalleProductos_CN.push(Row);
        }

    }

    if (lstDetalleProductos.length <= 0 && lstDetalleProductos_CN.length <= 0) {
        alertify.alert('No ha establecido el listado de productos.');
        return;
    }
    if (tbCant_Vacio > 0) {
        alertify.alert('Listado de productos, el dato "Cantidad" debe ser mayor a 0, faltante en ' + tbCant_Vacio + '.');
        return;
    }

    if (ValidaCantidad > 0) {
        alertify.alert('Listado de productos Cuenta Corporativa, Existe Productos con "Cantidad" Capturada, Falta Seleccionar frecuencia, faltante en ' + ValidaCantidad + '.');
        return;
    }

    if (ValidaFrecuencua > 0) {
        alertify.alert('Listado de productos Cuenta Corporativa, Existe Productos con "Frecuencia" Seleccionada, Falta Capturar "Cantidad", faltante en ' + ValidaFrecuencua + '.');
        return;
    }
    // Si es Cuenta Naciona Ignora la Validacion del Campo


    //if (AcysCNTipo == 1) {

    //} else {
    //    if (tbRequiereOC_Vacio > 0) {
    //        alertify.alert('Listado de productos, el dato "Requiere Orden Compra" es Obligatorio, faltan ' + tbRequiereOC_Vacio + '.');
    //        return;
    //    }
    //}

    if (tbDocEntrega_Vacio > 0) {
        alertify.alert('Listado de productos, el dato "Doc. de Entega" es Obligatorio, faltan ' + tbDocEntrega_Vacio + '.');
        return;
    }
    if (tbFrecuenca_Vacio > 0) {
        alertify.alert('Listado de productos, Frecuencia es Dato Obligatorio, faltan ' + tbFrecuenca_Vacio + '.');
        return;
    }
    if (tbFrecuencaTipo_Vacio > 0) {
        alertify.alert('Listado de productos, El Tipo de Frecuencia es Dato Obligatorio, faltan ' + tbFrecuencaTipo_Vacio + '.');
        return;
    }
    if (tbFrecuencia_MayorA3 > 0) {
        alertify.alert('Listado de productos: Hay productos con frecuencias fuera de rango (1,2,3) permitido , Corregir ' + tbFrecuencia_MayorA3 + '.');
        return;
    }

    // var Ruta = "~/Ventana_AutorizacionPrecios.aspx";

    // GUARDAR EL ACUERDO 
    Acys2_Guardar(eACyS2, SpinnerAcysGuardando,
        function (Id_Acs, Estado) {

            // GUARDAR PRODUCTOS ACYS LOCAL
            if (lstDetalleProductos.length > 0) {
                Acys_ProductosGuardar.Acys2_DetalleProductos(Id_Acs, Id_AcsVersion, 1, lstDetalleProductos,
                    function () {
                        /*
                        $('#modalAcys').modal('hide');
                        Inicializar_Acys();
                        $('#spinnerGuardando').css('display', 'none');
                        */

                    }, function () { // CALLBACK Error             
                        alertify.alert('Error: En listado de productos');
                    });
            }

            // GUARDAR PRODUCTOS - CN Y CCORDINADA
            if (lstDetalleProductos_CN.length > 0) {
                Acys_ProductosGuardar.Acys2_DetalleProductos(Id_Acs, Id_AcsVersion, 2, lstDetalleProductos_CN,
                    function () {
                        /*
                        $('#modalAcys').modal('hide');
                        Inicializar_Acys();
                        $('#spinnerGuardando').css('display', 'none');
                        */

                    }, function () { // CALLBACK Error             
                        alertify.alert('Error: En listado de productos');
                    });
            }

            if (Estado == 2) {
                alertify.alert('Se ha generado un nuevo documento ACyS:' + Id_Acs + '.');
            } else {
                alertify.alert('Se actualizo el documento ACyS:' + Id_Acs + '.');
            }

            $('#modalAcys').modal('hide');
            Inicializar_Acys();
            $('#spinnerGuardando').css('display', 'none');

            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }

        }, function () {
            alertify.error('Ocurrio un Error: ACyS_Guardar(2742).');
        }
    );

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    // JFCV alertadeprecios 
    // Si tengo registros con precio menor al de rik habro ventana de autorización 
    //

    if (validaralertas == 1) {
        debugger;
        // var Ruta = "~/Ventana_AutorizacionPrecios.aspx";

        // ///PRUEBAdocument.getElementById('<%=frameAlerta.ClientID%>').src = Ruta;
        // $('#frameAlerta').attr("src", Ruta);
        // var iframe = $('#frameAlerta');
        // //$(iframe).attr('src', Ruta)
        //// $('#ModalAlerta').find("iframe").attr("src", Ruta);

        $('#ModalAlerta').appendTo("body").modal('hide');

        $("#ModalAlerta").modal({ "backdrop": "static" });
        $('#ModalAlerta').modal('show');
        return;

    }
}

//cerrar ventana JFCV 
window.closeModalalerta = function () {
    $('#ModalAlerta').modal('hide');
}

function ProductoCodigo_Keypress(event, obj) {
    var Key = event.key;
    var input = $(obj);
    var rowno = $(obj).data('rowno');
    var permiteeditar = $(obj).data('permiteeditar');
    var Id_Prd = input.val();


    if (Key == 'Enter') {
        //alert(val);
        BuscarYCarga_InformacionDeProducto(Id_Prd, rowno);



    }
}

function ValidaDuplicadoPrd(Id_Prd, rowno) {
    var resultado = true;
    var trId;
    var txtPrdId;
    var nFila;
    var valorAnterior;
    $('#tblAcuerdoProds > tbody  > tr').each(function () {
        trId = $(this).attr("id");
        nFila = trId.replace("RonNo_", "");
        txtPrdId = "#tbCodigo_" + nFila
        valorAnterior = $(txtPrdId).val();
        //console.log("fila: " + nFila + ", valor:" + valorAnterior);
        if (rowno != nFila) {
            if (Id_Prd == valorAnterior) {
                resultado = false;
                $('#lbPrdDescripcion_' + rowno).text("ERROR: Producto ya se encuentra agregado.");
                $('#lbPresentacion_' + rowno).text("");
                $('#lbDescripcion_' + rowno).text(""); // Unidad
                $('#tbCant_' + rowno).val("");
                $('#tbPrecio_' + rowno).val("");
                $('#lbSubTotal_' + rowno).val("");
                $('#lbPrecioLista_' + rowno).text("");
                return false;
            }
        }
    });

    return resultado;
}


function ValidaTodosDuplicadoPrd() {
    var resultado = true;
    var trId;
    var arrPrd = [];
    let duplicados = [];
    var nFila;
    $('#tblAcuerdoProds > tbody  > tr').each(function () {
        trId = $(this).attr("id");
        nFila = trId.replace("RonNo_", "");
        arrPrd.push($("#tbCodigo_" + nFila).val());

    });

    var msj = "";
    const tempArray = arrPrd.sort();

    for (let i = 0; i < tempArray.length; i++) {
        if (tempArray[i + 1] === tempArray[i]) {
            duplicados.push(tempArray[i]);
            msj += " [" + tempArray[i] + "] ";
            resultado = false;
        }
    }

    if (duplicados.length > 0) {
        console.log(duplicados);
        alertify.error('Existen productos duplicados:' + msj);
    }

    return resultado;
}

function ProductoCodigo_OnBlur(obj) {
    var input = $(obj);
    var rowno = $(obj).data('rowno');
    var permiteeditar = $(obj).data('permiteeditar');
    permiteeditar = parseInt(permiteeditar);
    if (isNaN(permiteeditar)) {
        permiteeditar = 0;
    }

    var Id_Prd = input.val();
    Id_Prd = Id_Prd.trim();

    if (Id_Prd.length <= 0) {
        return;
    }

    if (permiteeditar == 1) {
        BuscarYCarga_InformacionDeProducto(Id_Prd, rowno);
        CalcularSuma();
    }
}

// JUN19-2020 RFH Actualizacion 
function btnBuscarCliente_Aplicar(obj) {

    var IdCte = $(obj).data('id_cte');
    var id_Ter = $(obj).data('id_ter');

    var entra = "s";
    var res = ConsultaExisteAcisByClienteId(IdCte,
        function (RES, Estado) {

            if (RES != null) {

                if (RES.Id_Cte != '') {

                    var text1 = $('#lbBuscarCliente_TieneACYS');
                    text1.html("El cliente: " + RES.Id_Cte + " ya cuenta con un acuerdo");

                    var tblAplicar = $('#tbBuscarCliente_Listado');
                    tblAplicar.hide();

                    var text2 = $('#lbBuscarCliente_TieneACYSNoAcuerdo');
                    text2.html("Numero de Acuerdo: " + RES.Id_Acs);
                    entra = "n";


                }

            }


        }, function () {
            $('#tbAcs_Semana').val('Error');
        });


    if (entra == "s") {
        Consultar_PorId_Cte(IdCte, function (REG) {
            $('#hfAcys_CteNumero').val(IdCte);
            $('#modalBuscarCliente').appendTo("body").modal('hide');
            $('#tbAcys_CteRFC').val(REG.Cte_FacRfc);
            $('#tbAcys_CteNombre').val(REG.Cte_NomComercial);
            $('#tbContacto1Nom').val(REG.Cte_Contacto);
            $('#tbContacto1Correo').val(REG.Cte_Email);
            $('#tbAcys_CteDireccion').val(REG.Cte_FacCalle);
            $('#tbAcys_CteCol').val(REG.Cte_FacColonia);
            $('#tbAcys_CteMunicipio').val(REG.Cte_FacMunicipio);
            $('#tbAcys_CteCP').val(REG.Cte_FacCP);
            $('#tbAcys_CteRFC').val(REG.Cte_FacRfc);
            $('#tbContacto1Tel').val(REG.Cte_Telefono);
            $('#tbAcys_CteNumero').val(REG.Id_Cte);

            if (REG.Id_TCte == 3) {
                //$('#lbEspAd_CuentaCorporativa').text('Si');
                $('#lbEspAd_CuentaCorporativa').val(1);
            } else {
                //$('#lbEspAd_CuentaCorporativa').text('No');
                $('#lbEspAd_CuentaCorporativa').val(0);
            }

            // 18MAY-2021

            // TERRITORIOS DE CLIENTE 


            ClienteDet_TerritoriosCte(IdCte, function (lst) {
                var selCteTerritorio = $('#selCteTerritorio').empty();
                for (var i = 0; i < lst.length; i++) {
                    //var txtlabel = $('#tbTexto_' + NP + '_' + i).val();
                    if (id_Ter == undefined) {
                        selCteTerritorio.append(
                            $('<option>').val(lst[i].Id_Terr).text(lst[i].Id_Terr)
                        );
                    }
                    if (id_Ter == lst[i].Id_Terr) {
                        selCteTerritorio.append(
                            $('<option>').val(lst[i].Id_Terr).text(lst[i].Id_Terr)
                        );
                    }
                }
            });
        });
    }
}

function ConsultaCordinadorCuentaByClienteId(Id_Cte) {
    console.log(Id_Cte);
    $.ajax({
        url: _ApplicationUrl + '/api/CatAcys/ConsultaCordinadorCuentaByClienteId' +
            '?MyId=' + Id_Cte,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();

            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {

            if (RES != null) {


                $('#lblCordinadorCuenta').text(RES.Coordinador);
                $('#lblCordinadorCuentaCorreo').text(RES.CorreoCoordinador);


                $('#lblCordinadorCuentaControlOrdenes').text(RES.Coordinador);
                $('#lblCordinadorCuentaCorreoControlOrdenes').text(RES.CorreoCoordinador);

            }

        }

        if (Estado == -1) {

        }


    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Error: funcion ConsultaCordinadorCuentaByClienteId.');
        }
    });
}


//Ivan G
function Guarda_CargaExcel(datosExcel) {
    var reg_producto = document.getElementsByName('reg_codigo[]');
    var reg_cantidad = document.getElementsByName('reg_cantidad[]');
    var reg_tipocta = document.getElementsByName('reg_tipocta[]');
    var reg_precio = document.getElementsByName('reg_precio[]');
    var reg_docentrega = document.getElementsByName('reg_docentrega[]');
    var reg_reqoc = document.getElementsByName('reg_reqoc[]');

    var TipoCuenta = document.getElementById('hfAcysCNTipo').value;

    var reg_codigo_values = Array.from(reg_producto).map(element => (parseInt(element.value)));
    var listcod_exeNo = [];
    var list_newprodcuts = [];
    for (var i = 0; i < datosExcel.length; i++) {
        let codigoProd_exe = datosExcel[i]["Código de producto"];
        let cantidad_exe = datosExcel[i]["Cantidad"];
        let FacRem_exe = datosExcel[i]["Factura/Remisión"];
        let PrecioVenta_exe = datosExcel[i]["Precio de venta"];
        let ReqOrden_exe = datosExcel[i]["Requiere orden de compra"];
        let FacRem = '';
        if (FacRem_exe == 'Factura') {
            FacRem = 'F';
        } if (FacRem_exe == 'Remisión') {
            FacRem = 'R';
        }

        let ReqOrde = -1;
        if (ReqOrden_exe == 'Si') {
            ReqOrde = 1;
        } if (ReqOrden_exe == 'No') {
            ReqOrde = 0;
        }


        var index = reg_codigo_values.indexOf(codigoProd_exe);
        if (index !== -1) {//existe
            var reg_tipo_ctaval = reg_tipocta[index];//3 CC, 2 //Cuenta nacional // 1 Local
            var valorTipoCta = reg_tipo_ctaval.dataset.tipocta;

            if (cantidad_exe > 0) {
                reg_cantidad[index].value = cantidad_exe;
                reg_cantidad[index].onblur();
            }

            if (valorTipoCta == 1) {
                if (PrecioVenta_exe > 0) {
                    reg_precio[index].value = PrecioVenta_exe;
                }

                if (ReqOrde >= 0) {
                    reg_reqoc[index].value = ReqOrde;
                }

                if (FacRem != '') {
                    reg_docentrega[index].value = FacRem;
                }
                list_newprodcuts.push({ index, precio: PrecioVenta_exe });
                reg_precio[index].onblur();
            }


        } else {//nuevo
            var index = listcod_exeNo.indexOf(codigoProd_exe);

            if (index !== -1) { }
            else {
                if (TipoCuenta == 2 || TipoCuenta == 0) {
                    ListadoProductos_PreAddRow(1);
                    listcod_exeNo.push(codigoProd_exe);
                    var countIndx = document.getElementsByName('reg_codigo[]');
                    var ultimoIndex = countIndx.length - 1;
                    reg_cantidad[ultimoIndex].value = cantidad_exe;
                    reg_producto[ultimoIndex].value = codigoProd_exe;

                    BuscarYCarga_InformacionDeProductoEXCEL(codigoProd_exe, ultimoIndex + 1, cantidad_exe, PrecioVenta_exe);

                    reg_precio[ultimoIndex].value = PrecioVenta_exe;

                    if (ReqOrde >= 0) {
                        reg_reqoc[ultimoIndex].value = ReqOrde;
                    }

                    if (FacRem != '') {
                        reg_docentrega[ultimoIndex].value = FacRem;
                    }

                    list_newprodcuts.push({ index: ultimoIndex, precio: PrecioVenta_exe });

                }
            }
        }

        mostrarLoaderCargaExcel(false);
    }

}

function BuscarYCarga_InformacionDeProductoEXCEL(Id_Prd, rowno, Cantidad, Precio) {
    // validar si existe el id de producto
    var validacion;
    validacion = ValidaDuplicadoPrd(Id_Prd, rowno);
    if (!validacion) {
        //mensaje y termina ejecucion
        alertify.error('El producto esta duplicado.');
        return false;
    }

    Spinner = $('#spinnerBuscando_' + rowno);
    Consulta_Producto(Id_Prd, rowno, Spinner, function (RES, NR) {
        if (RES == null) {
            alertify.error('No se encontro el producto o esta inactivo.');
        } else {


            if (RES?.Prd_Activo == 2) {
                const fila = document.getElementById('RonNo_' + NR);
                if (fila) {
                    fila.parentNode.removeChild(fila);
                    alertify.error('<h5 style="color:white;">Producto inactivo</h5>reemplazalo con otra alternativa/consulta con el area operativa/CEDIS.');
                }

            } else {
                let subtotal = Cantidad * Precio;
                subtotal = subtotal.formatMoney(2, '.', ',');

                $('#lbPrdDescripcion_' + NR).text(RES.Prd_Descripcion);
                $('#lbPresentacion_' + NR).text(RES.Prd_Presentacion);
                $('#lbDescripcion_' + NR).text(RES.Uni_Descripcion); // Unidad
                $('#tbCant_' + NR).val(Cantidad);
                $('#tbPrecio_' + NR).val(Precio);
                $('#lbSubTotal_' + NR).text(subtotal);

                $('#lbPrecioLista_' + NR).text(RES.Prd_PrecioLista);
                CalcularSuma();
            }
        }
    });
}


function actualiza_PrecioNuevoProducto(list_newprod) {

    var reg_precio = document.getElementsByName('reg_precio[]');
    for (var i = 0; i < list_newprod.length; i++) {
        var indexSave = list_newprod[i].index;
        var precio = list_newprod[i].precio;
        reg_precio[indexSave].value = precio;
        reg_precio[indexSave].onblur();
    }

    mostrarLoaderCargaExcel(false);
}

function mostrarLoaderCargaExcel(disableBtn = true) {
    let display = 'none';
    if (disableBtn) {
        display = 'contents';
    }
    document.getElementById('loaderCargaExcelModal').style.display = display;
    document.getElementById('CloseCargaExcelModal').disabled = disableBtn;
    document.getElementById('btnSubirExcel_Guarar').disabled = disableBtn;
    document.getElementById('iconbtnCloseCargaExcel').disabled = disableBtn;
    $('#modalCargaExcel').modal('hide');
}


function AgregarProductoReporte(codigoProd, tipoCuenta) {

    if (codigoProd > 0) {

        var reg_producto = document.getElementsByName('reg_codigo[]');

        var reg_codigo_values = Array.from(reg_producto).map(element => (parseInt(element.value)));
        var indexFind = reg_codigo_values.indexOf(codigoProd);

        if (indexFind !== -1) {
        } else {
            ListadoProductos_PreAddRow(1);
            var reg_producto = document.getElementsByName('reg_codigo[]');
            var ultimoIndex = reg_producto.length - 1;
            reg_producto[ultimoIndex].value = codigoProd;
            reg_producto[ultimoIndex].onblur();
        }

    }

}


function Acys2_DetalleProductosUpdate_RptVti(Id_Acs, Id_AcsVersion, TipoAcys, Lst_eACyS2, CALLBACK_Exito, CALLBACK_Error) {
    var sJSON = JSON.stringify(Lst_eACyS2);

    $.ajax({
        url: _ApplicationUrl + '/api/CatAcysDet/Update_RptVti?Id_Acs=' + Id_Acs + '&Id_AcsVersion=' + Id_AcsVersion + '&TipoAcys=' + TipoAcys,
        data: sJSON,
        cache: false,
        type: 'POST',
        contentType: "application/json; utf-8",
        dataType: "json",
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Acys2_DetalleProductos, null, $, Id_Acs, Id_AcsVersion, Lst_eACyS2, CALLBACK_Exito, CALLBACK_Error);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            CALLBACK_Exito(RES);
        } else {
            CALLBACK_Error(RES);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Ocurrió una error: funcion Acys2_DetalleProductos.');
        }
    });
}