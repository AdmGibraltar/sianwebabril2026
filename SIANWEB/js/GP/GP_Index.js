let datos = 0;
let ACyS_ContadorListaProductos = 2;

console.log(_ApplicationUrl);

$(document).ready(function () {
    $('body').on('hidden.bs.modal', function () {
        // Solo agregar `modal-open` si realmente hay modales abiertos
        if ($('.modal.show').length > 0) {
            $('body').addClass('modal-open');
        }
    });
    $('.datepicker').Zebra_DatePicker({
        format: 'd/m/Y'
    });
});

//Agregar renglon de productos 
$('#btnAgregarRenglon').click(function () {
    ListadoProductos_PreAddRow(1);
});

function ListadoProductos_PreAddRow(TipoCTA) {

    ACyS_ContadorListaProductos = ACyS_ContadorListaProductos + 1;

    var Id_Acys = $('#txtCliente').val();
    var Id_Matriz = $('#txtProducto').val();
    console.log(Id_Acys);
    console.log("ListadoProductos_PreAddRow");
    var Reg = {
        "Id_Prd": ""
        , "Prd_Descripcion": ""
        , "Prd_Presentacion": ""
        , "Uni_Descripcion": ""
        , "NomCategoria": ""
        /* , "Acs_Precio": ""*/
        , "PrecioListaActual": ""
        , "Acs_Cantidad": ""
        , "Acs_Frecuencia": ""
        , "Acs_FrecuenciaTipo": 0
        , "Acs_Documento": 0
        , "Id_Ter": 0
        , "Id_AcsDet": ACyS_ContadorListaProductos
        , "Id_Acys": Id_Acys
        , "Id_Matriz": Id_Matriz
        , "PrecioObjetivoProy": 0
        , "PrecioListaProy": 0
        , "PrecioMinRikProy": 0
        , "PrecioGteProy": 0
        , "PrecioNegociadoProy": 0
        /* , "PorcIncrementoProy": 0*/
        , "DescuentoSobrePlistaProy": 0
        , "UnidadesProyectadas": 0
        , "VentaProy": 0
        , "MgRed_PesosProy": 0
        , "MgRed_PorcProy": 0
        , "VarPpbRed_Porc": 0
        , "Comentarios": ""
        , "NomEstatus": ""
        ,"CostoAAAAFuturo": 0

    }
    //if (GLOBAL_Activo_AcysCuentasNacionales == 1) {
    if (1 == 1) {
        ListadoProductos_AddRow(Reg, 1, 1, "#tblAcuerdoProds", TipoCTA);
    } else {
        // 1 Local 
        ListadoProductos_AddRow(Reg, 1, 1, "#tblAcuerdoProds", 1);
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Agregar un renglon al Listado de productos.
//

// Añade Renglon de PRODUCTOS de ACYS

function ListadoProductos_AddRow(Reg, Estatus, PermiteEditar, tblControl, TipoCTA) {
    let PrdLstIndex = 2;
    PrdLstIndex = PrdLstIndex + 1;
    console.log('ListadoProductos_PreAddRow-1');
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


    //var Precio = parseFloat(Reg.Acs_Precio);
    //if (isNaN(Precio)) {
    //    Precio = 0;
    //}

    var precioObjetivoProy = parseFloat(Reg.PrecioObjetivoProy);
    if (isNaN(precioObjetivoProy)) {
        precioObjetivoProy = 0;
    }

    let PrecioListaProy = parseFloat(Reg.PrecioListaProy);
    if (isNaN(PrecioListaProy)) {
        PrecioListaProy = 0;
    }
    let PrecioMinRikProy = parseFloat(Reg.PrecioMinRikProy);

    if (isNaN(PrecioMinRikProy)) {
        PrecioMinRikProy = 0;
    }
    let PrecioGteProy = parseFloat(Reg.PrecioGteProy);
    PrecioGteProy = PrecioGteProy.formatMoney(2, '.', ',');
    if (isNaN(PrecioGteProy)) {
        PrecioGteProy = 0;
    }
    console.log(PrecioGteProy);
    let PrecioNegociadoProy = parseFloat(Reg.PrecioNegociadoProy);

    if (isNaN(PrecioNegociadoProy)) {
        PrecioListaProy = 0;
    }
    //let PorcIncrementoProy = parseFloat(Reg.PorcIncrementoProy);
    //if (isNaN(PorcIncrementoProy)) {
    //    PorcIncrementoProy = 0;
    //}
    let DescuentoSobrePlistaProy = parseFloat(Reg.DescuentoSobrePlistaProy);
    if (isNaN(DescuentoSobrePlistaProy)) {
        DescuentoSobrePlistaProy = 0;
    }
    let VentaProy = parseFloat(Reg.VentaProy);

    if (isNaN(VentaProy)) {
        VentaProy = 0;
    }
    let MgRed_PesosProy = parseFloat(Reg.MgRed_PesosProy);

    if (isNaN(MgRed_PesosProy)) {
        MgRed_PesosProy = 0.01;
    }
    let MgRed_PorcProy = parseFloat(Reg.MgRed_PorcProy);
    if (isNaN(MgRed_PorcProy)) {
        MgRed_PorcProy = 0;
    }
    let VarPpbRed_Porc = parseFloat(Reg.VarPpbRed_Porc);
    if (isNaN(VarPpbRed_Porc)) {
        VarPpbRed_Porc = 0;
    }


    let CostoAAAAFuturo = 0;

    precioObjetivoProy = precioObjetivoProy;
    PrecioListaProy = PrecioListaProy;

    /*var SubTotal = Precio * Cant;*/

    //AcysProductosSuma = AcysProductosSuma + SubTotal;

    //SubTotal = formatMoney(SubTotal);



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
        'style="width:80px;" ' +
        'type="text" name="reg_codigo[]" id="tbCodigo_' + PrdLstIndex + '" value="' + Reg.Id_Prd + '" ' + sReadOnly + '>'
    ));


    // DESCRIPCION
    row.append($('<td style="vertical-align: middle!important;">').append(
        '<img id="spinnerBuscando_' + PrdLstIndex + '" style="display: none;" src="../Img/patternfly/spinner-xs.gif">' +
        '<label id="lbPrdDescripcion_' + PrdLstIndex + '">' + Reg.Prd_Descripcion + '</label>'
    ));
    // Categoria
    row.append($('<td style="vertical-align: middle!important;">').append(
        '<label id="lbNomCategoria_' + PrdLstIndex + '">' + Reg.NomCategoria + '</label>'
    ));
    //// PRESENTACION
    //row.append($('<td style="vertical-align:top!important; text-align:center!important;">').append(
    //    '<label id="lbPresentacion_' + PrdLstIndex + '">' + Reg.Prd_Presentacion + '</label>'
    //));
    //console.log('lbPresentacion_');
    //// UNIDAD
    //row.append($('<td style="text-align:center; vertical-align:top!important;">').append(
    //    '<label id="lbDescripcion_' + PrdLstIndex + '">' + Reg.Uni_Descripcion + '</label>'
    //));


    // Precios Poyectados
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioObjetivoProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + precioObjetivoProy + '</label>'
    ));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioListaProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + PrecioListaProy + '</label>'
    ));

    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioMinRikProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + PrecioMinRikProy + '</label>'
    ));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioGteProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + PrecioGteProy + '</label>'
    ));
    console.log('lbPrecioGteProy_');
    // PRECIO VENTA 
    //row.append($('<td>').append(
    //    '<input type="text" ' +
    //    'data-row="' + PrdLstIndex + '" ' +
    //    'style="width:60px;" readonly="true"' +
    //    'class="form-control input-sm" ' +
    //    'id="tbPrecio_' + PrdLstIndex + '" ' +
    //    'name="reg_precio[]" value="' + Reg.Acs_Precio + '" ' +
    //    'onblur="ListadoProductos_CalculateRow(this);" ' + sReadOnly + ' > '
    //));
    // PRECIOnegociado
    row.append($('<td>').append(
        '<input type="text" ' +
        'data-row="' + PrdLstIndex + '" ' +
        'style="width:60px;" font-weight:bold; ' +
        'class="form-control input-sm" ' +
        'id="tbPrecioNegociadoProy_' + PrdLstIndex + '" ' +
        'name="reg_precionegociadoproy[]" value="' + PrecioNegociadoProy + '" ' +
        'onblur="ListadoProductos_CalculateRow(this);" ' + sReadOnly + ' > '
    ));

    //row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
    //    '<label id="lbPorcIncrementoProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + PorcIncrementoProy + '</label>'
    //));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbDescuentoSobrePlistaProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + DescuentoSobrePlistaProy + '%</label>'
    ));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbVentaProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + Reg.VentaProy + '</label>'
    ));
    console.log('lbVentaProy_');
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


    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbMgRed_PesosProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + MgRed_PesosProy + '</label>'
    ));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbMgRed_PorcProy_' + PrdLstIndex + '" style="margin-top:2px;" >' + MgRed_PorcProy + '</label>'
    ));
    row.append($('<td style="vertical-align:top!important; text-align:right!important;  display:none;">').append(
        '<label id="lbVarPpbRed_Porc_' + PrdLstIndex + '" style="margin-top:2px;" >' + VarPpbRed_Porc + '</label>'
    ));


    console.log('lbMgRed_PorcProy_');
    row.append($('<td>').append(
        '<input type="text" ' +
        'data-row="' + PrdLstIndex + '" ' +
        'style="width:50px;" ' +
        'class="form-control input-sm" ' +
        'name="reg_comentarios[]" id="tbComentarios_' + PrdLstIndex + '" ' +
        'value="' + Reg.Comentarios + '"  >'
    ));
    row.append($('<td style="vertical-align:top!important; display: none; text-align:right!important;">').append(
        '<label id="lbNomEstatus_' + PrdLstIndex + '" style="margin-top:2px;" >Nuevo</label>'
    ));


    // PRECIO LISTA     
    row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
        '<label id="lbPrecioLista_' + PrdLstIndex + '" style="margin-top:2px;" >' + PrecioLista + '</label>'
    ));




    // SUBTOAL
    //row.append($('<td style="vertical-align:top!important; text-align:right!important;">').append(
    //    //'<input type="text" class="form-control" id="tbubtotal_' + PrdLstIndex + '" value="' + SubTotal + '">'
    //    '<label id="lbSubTotal_' + PrdLstIndex + '" style="margin-top:2px;" >' + SubTotal + '</label>'
    //));
    //console.log('lbSubTotal_');

    /*, "PrecioNegociadoProy": 0*/


    /*                     , "UnidadesProyectadas": 0*/



    var btnRemover = '';

    //if (Estatus == 'Nuevo' || Estatus == 'Nuevo' || sReadOnly == 'readonly') {
    //    btnRemover = '<i class="fa fa-times-circle fa-2x hover_hand" ' +
    //        'style="color:orange; margin-top:2px!important;" ' +
    //        'data-rowno="' + PrdLstIndex + '" ' +
    //        'onclick="ProdNgc.Mover_PrdNegociados(this);" ' +
    //        '></i>';
    //} else {
    //    btnRemover = '<i class="fa fa-times-circle fa-2x hover_hand" ' +
    //        'style="color:red; margin-top:2px!important;" ' +
    //        'data-rowno="' + PrdLstIndex + '" ' +
    //        'onclick="ListadoProductos_RemoveRow(this);" ' +
    //        '></i>';
    //}
    row.append($('<td style="vertical-align:top!important; display: none; text-align:right!important;">').append(
        '<label id="lbCostoAAAAFuturo_' + PrdLstIndex + '" style="margin-top:2px;" >' + CostoAAAAFuturo + '</label>'
    ));

    row.append($('<td style="vertical-align:top;">').append(btnRemover));

    $(tblControl + ' > tbody > tr:first').remove();

    $(tblControl + ' > tbody').append(row);

    //$('#selFrecTipo_' + PrdLstIndex).val(Reg.Acs_FrecuenciaTipo);

    //Frecuencia.ControlInit(PrdLstIndex, Reg.Acs_FrecuenciaTipo);

    //if (Reg.Acs_Documento == '') {
    //    $('#tbDocEntrega_' + PrdLstIndex).val('-');
    //} else {
    //    $('#tbDocEntrega_' + PrdLstIndex).val(Reg.Acs_Documento);
    //}

    //if (Reg.RequiereOC == '') {
    //    $('#tbRequiereOC_' + PrdLstIndex).val(0);
    //} else {
    //    $('#tbRequiereOC_' + PrdLstIndex).val(Reg.RequiereOC);
    //}
    //$('#tbDocEntrega_' + PrdLstIndex).focus();
    console.log('fin');

}


function ProductoCodigo_Keypress(event, obj) {
    var Key = event.key;
    var input = $(obj);
    var rowno = $(obj).data('rowno');
    var permiteeditar = $(obj).data('permiteeditar');
    var Id_Prd = input.val();

    if (Key == 'Enter') {
        //alert(val);
        BuscarYCarga_InformacionDeProductoF(Id_Prd, rowno);
    }
}

function BuscarYCarga_InformacionDeProductoF(Id_Prd, rowno) {
    // validar si existe el id de producto
    var validacion;
    validacion = ValidaDuplicadoPrdObj(Id_Prd, rowno);
    if (!validacion) {
        //mensaje y termina ejecucion
        alertify.error('El producto ya existe en el listado.');
        return false;
    }

    Spinner = $('#spinnerBuscando_' + rowno);
    Consulta_ProductoObj(Id_Prd, rowno, Spinner, function (RES, NR) {
        if (RES == null) {
            alertify.error('No se encontro el producto o esta inactivo.');
        } else {

            $('#lbPrdDescripcion_' + NR).text(RES.Prd_Descripcion);
            $('#lbNomCategoria_' + NR).text(RES.NomCategoria);
            $('#lbPresentacion_' + NR).text(RES.Prd_Presentacion);
            /* $('#lbDescripcion_' + NR).text(RES.Uni_Descripcion); // Unidad*/
            $('#tbCant_' + NR).val(1);
            /*  $('#tbPrecio_' + NR).val(RES.Acs_Precio);*/
            /*$('#lbSubTotal_' + NR).val(RES.Acs_Precio);*/
            console.log(RES.PrecioListaActual);
            $('#lbPrecioLista_' + NR).text(Number(RES.PrecioListaActual).formatMoney(2, ".", ","));
            $('#lbPrecioObjetivoProy_' + NR).text(Number(RES.PrecioObjetivoProy || 0).formatMoney(2, ".", ",")); 
            $('#lbPrecioListaProy_' + NR).text(Number(RES.PrecioListaProy || 0).formatMoney(2, ".", ","));
            $('#lbPrecioMinRikProy_' + NR).text(Number(RES.PrecioMinRikProy || 0).formatMoney(2, ".", ","));
            $('#lbPrecioGteProy_' + NR).text(Number(RES.PrecioGteProy || 0).formatMoney(2, ".", ","));
            $('#tbPrecioNegociadoProy_' + NR).val(Number(RES.PrecioNegociadoProy || 0).formatMoney(2, ".", ","));
            /* $('#lbPorcIncrementoProy_' + NR).text(RES.PorcIncrementoProy);*/
            $('#lbDescuentoSobrePlistaProy_' + NR).val(RES.DescuentoSobrePlistaProy);
            $('#lbVentaProy_' + NR).text(Number(RES.VentaProy || 0).formatMoney(2, ".", ","));
            $('#lbMgRed_PesosProy_' + NR).text(Number(RES.MgRed_PesosProy || 0).formatMoney(2, ".", ","));
            $('#lbMgRed_PorcProy_' + NR).text(RES.MgRed_PorcProy);
            $('#lbVarPpbRed_Porc_' + NR).text(RES.VarPpbRed_Porc);

            $('#tbComentarios_' + NR).text(RES.Comentarios);
            $('#lbNomEstatus_' + NR).val(RES.NomEstatus);
            $('#lbCostoAAAAFuturo_' + NR).text(RES.CostoAAAAFuturo);
            console.log('termine'); 
            /* , "UnidadesProyectadas": 0*/

        }
    });
}


function Consulta_ProductoObj(Id_Prd, rowno, Spinner, CALLBACK) {

    var Estatus = '';
    var FolioIni = '';
    var FolioFin = '';
    var Territorio = 0;
    var Id_Cte = 0;
    var IdRik = 0;


    Spinner.css('display', 'block');

    Id_Cte = $('#txtCliente').val();
    console.log('cliente y producto');
    console.log(Id_Cte);
    console.log(Id_Prd);
    console.log(_ApplicationUrl);
    var url = $('#hurl').val();
    id_tamaño = $('#txtIdTamaño').val();
    IdRik = $('#ddlRepresentante').val();
    if (IdRik == null) {
        IdRik = 100;
        IdRik = representante;
    };

    $.ajax({
        url: _ApplicationUrl2 + '/api/GestionPrecios/' +
            '?Id_Cte=' + Id_Cte + '&Id_Prd=' + Id_Prd + '&Id_Tamaño=' + id_tamaño + '&id_rik=' + IdRik,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Consulta_ProductoObj, null, $, Id_Prd, rowno, Spinner, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;


        if (estado == 1) {

            if (res == null) {
                var REG = {
                    "Id_Prd": Id_Prd
                    , "Prd_Descripcion": "* EL PRODUCTO NO EXISTE *"
                    , "NomCategoria": " "
                    , "Prd_Presentacion": ""
                    , "Uni_Descripcion": ""
                    /* , "Acs_Precio": ""*/
                    , "Acs_Cantidad": ""
                    , "Acs_Frecuencia": ""
                    , "Acs_Documento": ""
                    , "PrecioLista": 0
                }
            } else {
                // se psa el resultado a la clas que entienda el call back 
                var REG = {
                    "Id_Prd": res.Id_Prd
                    , "Prd_Descripcion": res.Prd_Descripcion
                    , "NomCategoria": res.NomCategoria
                    , "Prd_Presentacion": ""
                    , "Uni_Descripcion": ""
                    /* , "Acs_Precio": res.PrecioVenta*/
                    , "Acs_Cantidad": 1
                    , "Acs_Frecuencia": 1
                    , "Acs_Documento": 0
                    , "PrecioListaActual": res.PrecioListaActual
                    , "PrecioObjetivoProy": res.PrecioObjetivoProy
                    , "PrecioListaProy": res.PrecioListaProy
                    , "PrecioMinRikProy": res.PrecioMinRikProy
                    , "PrecioGteProy": res.PrecioGteProy
                    , "PrecioNegociadoProy": res.PrecioNegociadoProy
                    /*  , "PorcIncrementoProy": res.PorcIncrementoProy*/
                    , "DescuentoSobrePlistaProy": res.DescuentoSobrePlistaProy
                    , "UnidadesProyectadas": 1
                    , "VentaProy": res.VentaProy
                    , "MgRed_PesosProy": res.MgRed_PesosProy
                    , "MgRed_PorcProy": res.MgRed_PorcProy
                    , "VarPpbRed_Porc": res.VarPpbRed_Porc
                    , "Comentarios": ""
                    , "NomEstatus": "Nuevo"
                    , "CostoAAAAFuturo": res.CostoAAAAFuturo

                };

            }

        } else {

            var REG = {
                "Id_Prd": Id_Prd
                , "Prd_Descripcion": "ERROR: " + mensaje
                , "NomCategoria": ""
                , "Prd_Presentacion": ""
                , "Uni_Descripcion": ""
                /*, "Acs_Precio": ""*/
                , "Acs_Cantidad": ""
                , "Acs_Frecuencia": ""
                , "Acs_Documento": ""
                , "Prd_PrecioLista": ""
                , "PrecioListaActual": ""
                , "PrecioObjetivoProy": ""
                , "PrecioListaProy": ""
                , "PrecioMinRikProy": ""
                , "PrecioGteProy": ""
                , "PrecioNegociadoProy": ""
                /* , "PorcIncrementoProy": ""*/
                , "DescuentoSobrePlistaProy": ""
                , "UnidadesProyectadas": ""
                , "VentaProy": ""
                , "MgRed_PesosProy": ""
                , "MgRed_PorcProy": ""
                ,"VarPpbRed_Porc":""
                , "Comentarios": ""
                , "NomEstatus": "Nuevo"
                ,"CostoAAAAFuturo":0

            }

        }
        Spinner.css('display', 'none');
        if (CALLBACK) {
            CALLBACK(REG, rowno);
        }

    }).fail(function (jqXHR, textStatus, error) {
        Spinner.css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Ocurrió una error: funcion Consulta_ProductoObj.');
        }
    });
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
        BuscarYCarga_InformacionDeProductoF(Id_Prd, rowno);
    }
}
function ListadoProductos_CalculateRow(obj, ROW_) {
    var row = $(obj).data("row");
    row = parseInt(row);

    console.log('ProductoCodigo_OnBlur');

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

    //calcular los nuevos datos proyectados 
    let precioNegociadoProys = $('#tbPrecioNegociadoProy_3').val() || 0.0;
    let precioNegociadoProy = parseFloat(precioNegociadoProys.replace(/,/g, '')).toFixed(2);
    let ventaProyectada = precioNegociadoProy * Cantidad;
    ventaProyectada = ventaProyectada.formatMoney(2, '.', ',');
    //$('#lbVentaProy_' + row).text(precioNegociadoProy * parseInt($('#tbCant_3').val()) || 1);
    $('#lbVentaProy_' + row).text(ventaProyectada);


    let precioListaProyectado = parseFloat($('#lbPrecioListaProy_3').text().replace(/,/g, '')).toFixed(2) || 0.0;
    let mgRed_PesosProy = parseFloat($('#lbMgRed_PesosProy_3').text().replace(/,/g, '')).toFixed(2) || 0.0;
    let mgRed_PorcProy = parseFloat($('#lbMgRed_PorcProy_3').text().replace(/,/g, '')).toFixed(2) || 0.0;
    let varPpbRed_Porc = parseFloat($('#lbVarPpbRed_Porc_3').text()) || 0.0;
    let costoAAAAFuturo = parseFloat($('#lbCostoAAAAFuturo_3').text().replace(/,/g, '')).toFixed(2) || 0.0;
    let descuentoSobrePlistaProy = parseFloat($('#lbDescuentoSobrePlistaProy_3').text().replace(/,/g, '')).toFixed(2) || 0.0;



    // let PrecioVenta = parseFloat(rowData['PrecioVenta'] || 0);


    // Calcular ventas proyectadas
    let ventasProyectadas = 0;
    ventasProyectadas = Cantidad * precioNegociadoProy;

    console.log('precioListaProyectado');

    if (precioListaProyectado <= 0)
    {
        descuentoSobrePlistaProy = 0.0;
    }
    else
    {
        descuentoSobrePlistaProy = (precioListaProyectado - precioNegociadoProy) / precioListaProyectado;
    }
    descuentoSobrePlistaProy = parseFloat(descuentoSobrePlistaProy).toFixed(2) || 0.0;

    /* $('#lbDescuentoSobrePlistaProy_' + row).text(parseFloat(descuentoSobrePlistaProy).toFixed(2));*/
    $('#lbDescuentoSobrePlistaProy_' + row).text(descuentoSobrePlistaProy + '%');



    mgRed_PesosProy = ventasProyectadas - (Cantidad * costoAAAAFuturo);
    mgRed_PorcProy = (ventasProyectadas - (Cantidad * costoAAAAFuturo)) / ventasProyectadas;
    varPpbRed_Porc = 0;

    $('#lbMgRed_PesosProy_' + row).text(mgRed_PesosProy.formatMoney(2, '.', ','));
    /*$('#lbMgRed_PorcProy_' + row).text(formatMoney(mgRed_PorcProy));*/
    $('#lbMgRed_PorcProy_' + row).text(parseFloat(mgRed_PorcProy).toFixed(2) + '%');
    $('#lbVarPpbRed_Porc_' + row).text(varPpbRed_Porc.formatMoney(2, '.', ','));



}


//function formatMoney(n) {
//    let formatoMoneda = new Intl.NumberFormat('es-MX', { style: 'currency', currency: 'MXN' }).format(n);
//    return formatoMoneda;
//    //return "$ " + n.toLocaleString().split(".")[0] + "."
//    //    + n.toFixed(2).split(".")[1];
//}
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d === undefined ? "." : d,
        t = t === undefined ? "," : t,
        s = n < 0 ? "-" : "",
        nAbs = Math.abs(Number(n) || 0).toFixed(c),
        i = String(parseInt(nAbs)),
        j = (i.length > 3) ? i.length % 3 : 0;
    return s +
        (j ? i.substr(0, j) + t : "") +
        i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) +
        (c ? d + nAbs.slice(-c) : "");
};


Number.prototype.formatMoney3 = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-$" : "$",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};


function ValidaDuplicadoPrdObj(Id_Prd, rowno) {

    let tabla = $('#clientesTable').DataTable();
    let data = tabla.rows().data();
    console.log(data);
    console.log(Id_Prd);
    console.log(data.length);

    for (let i = 0; i < data.length; i++) {
        console.log(data[i].Id_Prd);
        if (data[i].Id_Prd == Id_Prd) { // Suponiendo que el 'id_prd' está en la primera columna
            return false; // Producto encontrado
        }
    };
    return true;
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
        BuscarYCarga_InformacionDeProductoF(Id_Prd, rowno);
    }
}

//cerrar ventana JFCV 
window.closeModalalerta = function () {
    $('#ModalAlerta').modal('hide');
}

function GuardarGestioPrecios() {

    var validaralertas = 0;


    AlertaPrecioValidaPrecio(1, 0, hfAcys_CteNumero, tbCodigo, tbPrecio, 0, Id_Ter, Spinner, i, $('#tbCant_' + i).val(), $('#lbPrdDescripcion_' + i).text(), $('#tbAcys_CteNombre').val(), function (RES, NR) {
        alert('validar precios');
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// JFCV alertadeprecios 
// Si tengo registros con precio menor al de rik habro ventana de autorización 
//

if (validaralertas == 1) {
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// JFCV alertadeprecios 
// Si tengo registros con precio menor al de rik habro ventana de autorización 
//
function AlertaPrecioValidaPrecioGPMA(Id_Emp, Id_Cd, Id_Cte, Id_Prd, Precio_Vta, Id_Rik, Id_Ter, Spinner, rowno, cantidad, prd_descripcion, cte_nomcomercial, precioObjetivoProy, Comentarios, CALLBACK) {

    var Estatus = '';
    var FolioIni = '';
    var FolioFin = '';
    var Territorio = 0;
    var IdCte = 0;
    var IdRik = 0;
    Spinner.css('display', 'block');

    if (Id_Emp == 0) {
        Id_Emp = 1;
    }
    if (Id_Ter == null) {
        Id_Ter = 1;
    }

    if (Id_Ter == 0) {
        Id_Ter = 1;
    }

    //url: _ApplicationUrl2 + '/api/GestionPrecios/' +
    //    '?Id_Cte=' + Id_Cte + '&Id_Prd=' + Id_Prd + '&Id_Tamaño=' + id_tamaño + '&id_rik=' + IdRik,
    //    cache: false,

    //    url: _ApplicationUrl + '/api/CatAcys/AlertaPrecioValidaPrecio?' +

    
var url = _ApplicationUrl2 + '/api/GestionPrecios/AlertaPrecioValidaPrecioGPMA?' +
    'Id_Emp=' + Id_Emp +
    '&Id_Cd=' + Id_Cd +
    '&Id_Cte=' + Id_Cte +
    '&Id_Prd=' + Id_Prd +
    '&Precio_Vta=' + Precio_Vta +
    '&Id_Rik=' + Id_Rik +
    '&Id_Ter=' + Id_Ter +
    '&NR=' + rowno +
    '&cantidad=' + cantidad +
    '&prd_descripcion=' + encodeURIComponent(prd_descripcion) +
    '&cte_nomcomercial=' + encodeURIComponent(cte_nomcomercial) +
    '&precioObjetivoProy=' + precioObjetivoProy +
    '&Comentarios=' + encodeURIComponent(Comentarios);

console.log("URL llamada:", url);

    if (cantidad == null || isNaN(cantidad) || cantidad === "") {
        cantidad = 0; // O el valor por defecto que tenga sentido para tu lógica
    }

    $.ajax({
        //url: _ApplicationUrl2 + '/api/CatAcys/AlertaPrecioValidaPrecio?' +
        url: _ApplicationUrl2 + '/api/GestionPrecios/AlertaPrecioValidaPrecioGPMA?' +
            'Id_Emp=' + Id_Emp +
            '&Id_Cd=' + Id_Cd +
            '&Id_Cte=' + Id_Cte +
            '&Id_Prd=' + Id_Prd +
            '&Precio_Vta=' + Precio_Vta +
            '&Id_Rik=' + Id_Rik +
            '&Id_Ter=' + Id_Ter +
            '&NR=' + rowno +
            '&cantidad=' + parseInt(cantidad, 10) +
            '&prd_descripcion=' + encodeURIComponent(prd_descripcion) +
            '&cte_nomcomercial=' + encodeURIComponent(cte_nomcomercial) +
            '&precioObjetivoProy=' + precioObjetivoProy +
            '&Comentarios=' + encodeURIComponent(Comentarios),

        async: false,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Consulta_ProductoObj, null, $, Id_Prd, rowno, Spinner, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;
        Spinner.css('display', 'none');
        console.log('regreso correcto');
        if (estado == 1) {
            if (res == null) {
                var REG = res
            } else {
                // se psa el resultado a la clas que entienda el call back 
                var REG = res;
                //var REG = {
                //    "Id_Prd": res._Id_Prd
                //    , "Prd_Descripcion": res._Prd_Descripcion
                //    , "Prd_Presentacion": res._Prd_Descripcion
                //    , "Uni_Descripcion": res._Prd_Descripcion
                //    , "Acs_Precio": res._Precio_AAA
                //    , "Acs_Cantidad": 1
                //    , "Acs_Frecuencia": 1
                //    , "Acs_Documento": 0
                //}
            }

        } else {
            console.log('error al  llamr ajaxxx  AlertaPrecioValidaPrecioGPMA');
            var REG = {
                "Id_Prd": Id_Prd
                , "Prd_Descripcion": "ERROR: " + mensaje
                , "Prd_Presentacion": ""
                , "Uni_Descripcion": ""
                /*, "Acs_Precio": ""*/
                , "Acs_Cantidad": ""
                , "Acs_Frecuencia": ""
                , "Acs_Documento": ""
            }

        }

        if (CALLBACK) {
            CALLBACK(REG, rowno);
        }

    }).fail(function (jqXHR, textStatus, error) {
        Spinner.css('display', 'none');
        
        console.log(jqXHR.status);
        console.log(error);

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Ocurrió una error: funcion AlertaPrecioValidaPrecioGPMA.' + error );
        }
    });
}

function Disable_Btn(Selector) {
    $(Selector).attr("disabled", "disabled");
}

function Enable_Btn(Selector) {
    $(Selector).removeAttr("disabled");
}

function AlertaPrecioValidaPrecioGPMA_Masivo(productos, callback) {
    $.ajax({
        type: "POST",
        url: _ApplicationUrl2 + '/api/GestionPrecios/AlertaPrecioValidaPrecioGPMA_Masivo',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ productos: productos }),
        success: function (response) {
            if (callback) callback(response.d || response); // response.d para ASMX, response para WebAPI
        },
        error: function (error) {
            alertify.error("Error en validación de precios.");
            if (callback) callback(null, error);
        }
    });
}
