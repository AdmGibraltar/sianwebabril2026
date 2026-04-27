/*
Proyectos_tablaAgrupada_PropuestaTE.js
30ABR-2021 RFH Actualizado
01OCT-2020 Actualizado Se corrige Desbordado de productos KEY en Propuesta TE
11AGO-2020 Actualizado 
12 May 2018 
19MAR-2020 RFH Actualizado
*/

var MAX_CONTROLS = 0;
var VISUALIZANDO_REPORTE = 0;

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function Calcular_CostoEnUso(D) {
    var costoEnUso = 0.0;
    if (D.Vap_Cantidad != '') {
        if (D.COP_EsQuimico == true) {
            if (D.COP_DilucionConsecuente != '') {
                var precio = D.Vap_Precio; //ProductoActual.Prd_Pesos;
                var unidadesPresentacion = D.Prd_UniEmp;
                var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * D.Vap_Cantidad) * (parseInt(D.COP_DilucionConsecuente) + 1));
                if (consumoMensualEnLitrosDiluidos != 0.0) {
                    costoEnUso = (D.Vap_Cantidad * precio) / consumoMensualEnLitrosDiluidos;
                }
            }
        }
    }
    return costoEnUso;
};

function Propuesta_ModoLectura() {
    for (var i = 0; i < MAX_CONTROLS; i++) {
        AplDilucion = $('#hfId_AplDilucion_' + i).val();

        $('#lbVap_Cantidad_' + i).css('display', 'block');
        $('#tbVap_Cantidad_' + i).css('display', 'none');

        $('#tbCOP_DilucionAntecedente_' + i).css('display', 'none');
        $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', false);

        $('#tbCOP_DilucionConsecuente_' + i).css('display', 'none');
        $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', false);

        if (AplDilucion == 0) {
            // no visible 
            $('#lbCOP_DilucionAntecedente_' + i).css('display', 'none');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'none');
        } else {
            // si visible 
            $('#lbCOP_DilucionAntecedente_' + i).css('display', 'block');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'block');
        }

        $('#chbAplDilucion_' + i).css('display', 'none');

        //$('#chbAplDilucion_' + i).prop('disabled', );

        $('#lbProductoActual_' + i).css('display', 'block');
        $('#tbProductoActual_' + i).css('display', 'none');

        $('#lbSituacionActual_' + i).css('display', 'block');
        $('#tbSituacionActual_' + i).css('display', 'none');

        $('#lbVentajasKey_' + i).css('display', 'block');
        $('#tbVentajasKey_' + i).css('display', 'none');

        $('#btnImagenSolucionActual_' + i).css('display', 'none');
        $('#btnImagenSolucionKey_' + i).css('display', 'none');

    }
}

// Calcula los valores del renglon "no"
function Calcular_Renglon_ByNo(no) {

    AplDilucion = $('#chbAplDilucion_CEU_' + no).is(":checked");

    if (AplDilucion == true) {
        tbCOP_DilucionA = $('#tbCOP_DilucionAntecedente_CEU_' + no).val();
        if (tbCOP_DilucionA == '') {
            $('#tbCOP_DilucionAntecedente_CEU_' + no).val(1);
        }
        tbCOP_DilucionD = $('#tbCOP_DilucionConsecuente_CEU_' + no).val();
        if (tbCOP_DilucionA == '') {
            $('#tbCOP_DilucionConsecuente_CEU_' + no).val(1);
        }
    } else {

    }

    //var no = $(obj).data("no");

    var Precio = $('#Vap_Precio_' + no).text();

    if (typeof Precio === "undefined") {
        Precio = 0;
    }
    Precio = Precio.replace(',', '');
    Precio = parseFloat(Precio);
    if (isNaN(Precio)) {
        Precio = 0;
    }

    var Cantidad = $('#tbVap_Cantidad_' + no).val();
    if (typeof Cantidad === "undefined") {
        Cantidad = 0;
    }
    Cantidad = parseFloat(Cantidad);
    if (isNaN(Cantidad)) {
        Cantidad = 0;
    }

    var GastoMensual = Precio * Cantidad;
    //GastoMensual = GastoMensual.replace(',', '');

    if (isNaN(GastoMensual)) {
        GastoMensual = 0;
    }
    //GastoMensual = GastoMensual.toFixed(2);
    var fGastoMensual = GastoMensual;
    var GastoMensual = GastoMensual.formatMoney(0, '.', ',');


    //var Vap_Precio = lst[i].Vap_Precio.formatMoney(2, '.', ',');

    //var CostoEnUso = Calcular_CostoEnUso(lst[i]).formatMoney(2, '.', ',');

    $('#GastoMensual_' + no).text(GastoMensual);
    $('#GastoMensual_CEU_' + no).text(GastoMensual);

    // UniEmp = Unidad de Empaque
    var UniEmp = $('#hfPrd_UniEmp_' + no).val();
    //var ConsumoLitros = Cantidad * UniEmp;

    // 18 Sep 2018 RFH 
    // Calculo correcto de Consumo litros 

    var Presentacion = $('#Prd_Presentacion_' + no).text();
    Presentacion = parseFloat(Presentacion);
    if (isNaN(Presentacion)) {
        Presentacion = 0;
    }
    var ConsumoLitros = Cantidad * Presentacion;

    // 

    ConsumoLitros = ConsumoLitros.toFixed(2);

    $('#ConsumoMensualL_' + no).text(ConsumoLitros);
    $('#ConsumoMensualL_CEU_' + no).text(ConsumoLitros);

    DilucionC = $('#tbCOP_DilucionConsecuente_CEU_' + no).val();
    DilucionC = parseFloat(DilucionC);
    if (isNaN(DilucionC)) {
        DilucionC = 0;
    }

    /*
    ConsumoMensualLDiluidos = ConsumoLitros * (DilucionC + 1);
    ConsumoMensualLDiluidos = parseFloat(ConsumoMensualLDiluidos);
    if (isNaN(ConsumoMensualLDiluidos)) {
    ConsumoMensualLDiluidos = 0;
    }
    ConsumoMensualLDiluidos = ConsumoMensualLDiluidos.toFixed(2);
    */
    // 3 Oct 2018 RFH
    if (DilucionC <= 0) {
        ConsumoMensualLDiluidos = ConsumoLitros * (DilucionC + 1);
    } else {
        ConsumoMensualLDiluidos = ConsumoLitros * (DilucionC);
    }

    ConsumoMensualLDiluidos = ConsumoMensualLDiluidos.toFixed(2);

    if (AplDilucion == true) {
        $('#ConsumoMensualLDiluidos_' + no).text(ConsumoMensualLDiluidos);
        $('#ConsumoMensualLDiluidos_CEU_' + no).text(ConsumoMensualLDiluidos);
    } else {
        $('#ConsumoMensualLDiluidos_' + no).text('');
        $('#ConsumoMensualLDiluidos_CEU_' + no).text('');
    }

    var fCMLDs = 0.0;
    fCMLDs = parseFloat(ConsumoMensualLDiluidos);
    if (isNaN(fCMLDs)) {
        fCMLDs = 0
    }
    CostoEnUso = fGastoMensual / fCMLDs;
    var CostoEnUso = CostoEnUso.formatMoney(2, '.', ',');

    if (AplDilucion == true) {
        $('#CostoEnUso_' + no).text(CostoEnUso);
        $('#CostoEnUso_CEU_' + no).text(CostoEnUso);
    } else {
        $('#CostoEnUso_' + no).text('');
        $('#CostoEnUso_CEU_' + no).text('');
    }

}

//

var PTE = {

    // GUARDAR
    Propuesta_Guardar: function () {
        var Op = $('#hf_Id_Op').val();
        Op = parseInt(Op);
        if (isNaN(Op)) {
            Op = 0;
        }
        var Cte = $('#hf_Id_Cte').val();
        Cte = parseInt(Cte);
        if (isNaN(Cte)) {
            Cte = 0;
        }
        var Id_Val = $('#hf_Id_Val').val();
        Id_Val = parseInt(Id_Val);
        if (isNaN(Id_Val)) {
            Id_Val = 0;
        }

        for (var i = 1; i < MAX_CONTROLS + 1; i++) {

            var Prd = $('#hfId_Prd_' + i).val();
            var Cantidad = $('#tbVap_Cantidad_' + i).val();
            var AplDilucion = 0;

            var DilucionA = $('#tbCOP_DilucionAntecedente_CEU_' + i).val();
            var DilucionC = $('#tbCOP_DilucionConsecuente_CEU_' + i).val();

            var fCantidad = $('#tbVap_Cantidad_' + i).val();
            //fCantidad = parseFloat(fCantidad);
            //fCantidad = fCantidad.replace(',', '');
            fCantidad = parseFloat(Cantidad);
            if (isNaN(fCantidad)) {
                fCantidad = 0;
            }

            fCantidad = fCantidad.toFixed(2);

            var chbAplDilucion = $('#chbAplDilucion_CEU_' + i).is(":checked");

            if (chbAplDilucion == true) {
                // Aplica
                AplDilucion = 1;
                $('#lbCOP_DilucionAntecedente_CEU_' + i).text(DilucionA);
                $('#lbCOP_DilucionConsecuente_CEU_' + i).text(DilucionC);

                $('#lbCOP_DilucionAntecedente_CEU_' + i).css('display', 'block');
                $('#lbCOP_DilucionConsecuente_CEU_' + i).css('display', 'block');

                $('#tbCOP_DilucionAntecedente_CEU_' + i).val(DilucionA);
                $('#tbCOP_DilucionConsecuente_CEU_' + i).val(DilucionC);

                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', false);

            } else {
                // NO Aplica
                DilucionA = 0;
                DilucionC = 0;
                $('#lbCOP_DilucionAntecedente_CEU_' + i).text("");
                $('#lbCOP_DilucionConsecuente_CEU_' + i).text("");

                $('#lbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
                $('#lbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');

                $('#tbCOP_DilucionAntecedente_CEU_' + i).val("");
                $('#tbCOP_DilucionConsecuente_CEU_' + i).val("");

                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', true);
            }

            var CPT_ProductoActual = $('#tbProductoActual_' + i).val();
            var CPT_SituacionActual = $('#tbSituacionActual_' + i).val();
            var CPT_VentajasKey = $('#tbVentajasKey_' + i).val();
            var CPT_RecursoImagenProductoActual = $('#imgImagenProdActual_' + i).attr('src');
            var CPT_RecursoImagensolucionKey = $('#imgImagenSolucionKey_' + i).attr('src');

            var COP_CostoEnUso = $('#CostoEnUso_CEU_' + i).text();
            COP_CostoEnUso = COP_CostoEnUso.replace(',', '');
            COP_CostoEnUso = parseFloat(COP_CostoEnUso);
            if (isNaN(COP_CostoEnUso)) {
                COP_CostoEnUso = 0;
            }
            COP_CostoEnUso = COP_CostoEnUso.toFixed(2);

            Update_OportunidadesProductos(
                Op, Id_Val, Cte,
                Prd, Cantidad,
                AplDilucion,
                DilucionA,
                DilucionC,
                CPT_ProductoActual,
                CPT_SituacionActual,
                CPT_VentajasKey,
                CPT_RecursoImagenProductoActual,
                CPT_RecursoImagensolucionKey,
                COP_CostoEnUso, function () {
                    $('#lbVap_Cantidad_' + i).text(Cantidad);
                });

            $('#lbProductoActual_' + i).text(CPT_ProductoActual);

            $('#lbSituacionActual_' + i).text(CPT_SituacionActual);
            $('#lbVentajasKey_' + i).text(CPT_VentajasKey);

            $('#lbVap_Cantidad_' + i).text(fCantidad);
            $('#lbVap_Cantidad_' + i).css('display', 'block');
            $('#tbVap_Cantidad_' + i).css('display', 'none');

            $('#tbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
            $('#tbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');

            $('#chbAplDilucion_CEU_' + i).css('display', 'none');

            // Pasa los valores de los controles text a labels
            var tbProductoActual = $('#tbProductoActual_' + i).val();
            $('#lbProductoActual_' + i).text(tbProductoActual);

            var tbSituacionActual = $('#tbSituacionActual_' + i).val();
            $('#lbSituacionActual_' + i).text(tbSituacionActual);

            var tbVentajasKey = $('#tbVentajasKey' + i).val();
            $('#lbVentajasKey_' + i).text(tbVentajasKey);

            // Visualizar y ocultar
            $('#tbProductoActual_' + i).css('display', 'none');
            $('#tbSituacionActual_' + i).css('display', 'none');
            $('#tbVentajasKey_' + i).css('display', 'none');

            $('#lbProductoActual_' + i).css('display', 'block');
            $('#lbSituacionActual_' + i).css('display', 'block');
            $('#lbVentajasKey_' + i).css('display', 'block');

            $('#imgImagenProdActual_' + i).css('display', 'block');
            $('#imgImagenSolucionKey_' + i).css('display', 'block');

            $('#btnImagenSolucionActual_' + i).css('display', 'none');
            $('#btnImagenSolucionKey_' + i).css('display', 'none');
        }

    },
    // CALCULAR RENGLON 
    Calcular_Renglon: function (obj) {
        var no = $(obj).data("no");
        Calcular_Renglon_ByNo(no)
    },
    // 
    chbAplDilucion_click: function (obj) {
        var AplDilucion = $(obj).is(":checked");
        var no = $(obj).data("no");
        if (AplDilucion == true) {
            $('#tbCOP_DilucionAntecedente_CEU_' + no).val('1');
            $('#tbCOP_DilucionAntecedente_CEU_' + no).prop('disabled', true);
            $('#tbCOP_DilucionConsecuente_CEU_' + no).val('1');
            $('#tbCOP_DilucionConsecuente_CEU_' + no).prop('disabled', false);
        } else {
            $('#tbCOP_DilucionAntecedente_CEU_' + no).val('');
            $('#tbCOP_DilucionAntecedente_CEU_' + no).prop('disabled', true);
            $('#tbCOP_DilucionConsecuente_CEU_' + no).val('');
            $('#tbCOP_DilucionConsecuente_CEU_' + no).prop('disabled', true);
        }
        Calcular_Renglon_ByNo(no);
    },
    //
    Propuesta_ModoEdicion: function () {

        for (var i = 0; i < MAX_CONTROLS + 1; i++) {
            var chbAplDilucion = $('#chbAplDilucion_CEU_' + i).is(":checked");

            $('#lbVap_Cantidad_' + i).css('display', 'none');
            $('#tbVap_Cantidad_' + i).css('display', 'block');

            //$('#lbVap_Cantidad_CEU_' + i).css('display', 'none');
            //$('#tbVap_Cantidad_CEU_' + i).css('display', 'block');

            $('#lbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
            $('#lbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');

            if (chbAplDilucion == false) {
                // deshabilitar TextBoxs
                $('#tbCOP_DilucionAntecedente_CEU_' + i).css('display', 'block');
                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);

                $('#tbCOP_DilucionConsecuente_CEU_' + i).css('display', 'block');
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', true);
            } else {
                // habilitar TextBoxs
                $('#tbCOP_DilucionAntecedente_CEU_' + i).css('display', 'block');
                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);

                $('#tbCOP_DilucionConsecuente_CEU_' + i).css('display', 'block');
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', false);
            }

            $('#chbAplDilucion_CEU_' + i).css('display', 'block');

            //  PROPUESTA TECNICA TAB        
            $('#lbProductoActual_' + i).css('display', 'none');
            $('#tbProductoActual_' + i).css('display', 'block');
            var lbProductoActual = $('#lbProductoActual_' + i).text();
            $('#tbProductoActual_' + i).val(lbProductoActual);

            $('#lbSituacionActual_' + i).css('display', 'none');
            $('#tbSituacionActual_' + i).css('display', 'block');
            var lbSituacionActual = $('#lbSituacionActual_' + i).text();
            $('#tbSituacionActual_' + i).text(lbSituacionActual);

            $('#lbVentajasKey_' + i).css('display', 'none');
            $('#tbVentajasKey_' + i).css('display', 'block');
            var lbVentajasKey = $('#lbVentajasKey_' + i).text();
            $('#lbVentajasKey_' + i).text(lbVentajasKey);

            $('#btnImagenSolucionActual_' + i).css('display', 'block');
            $('#btnImagenSolucionKey_' + i).css('display', 'block');

            // Resplada las imagenes actuales
            var imgImagenProdActual = $('#imgImagenProdActual_' + i).attr('src');
            $('#hf_imgImagenProdActual_' + i).val(imgImagenProdActual);

            var imgImagenSolucionKey = $('#imgImagenSolucionKey_' + i).attr('src');
            $('#hf_imgImagenSolucionKey_' + i).val(imgImagenSolucionKey);
        }
    },

    Cancelar_Edicion: function () {

        for (var i = 0; i < MAX_CONTROLS + 1; i++) {

            var chbAplDilucion = $('#chbAplDilucion_CEU_' + i).is(":checked");

            $('#lbVap_Cantidad_' + i).css('display', 'block');
            var lbVap_Cantidad = $('#lbVap_Cantidad_' + i).text();
            $('#tbVap_Cantidad_' + i).css('display', 'none');
            $('#tbVap_Cantidad_' + i).val(lbVap_Cantidad);

            /*$('#lbVap_Cantidad_CEU_' + i).css('display', 'none');
            $('#tbVap_Cantidad_CEU_' + i).css('display', 'block');*/

            $('#lbVap_Cantidad_CEU_' + i).css('display', 'block');
            var lbVap_Cantidad = $('#lbVap_Cantidad_CEU_' + i).text();
            $('#tbVap_Cantidad_CEU_' + i).css('display', 'none');
            $('#tbVap_Cantidad_CEU_' + i).val(lbVap_Cantidad);

            $('#lbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
            $('#lbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');

            if (chbAplDilucion == false) {
                // deshabilitar TextBoxs
                $('#tbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);

                $('#tbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', true);


            } else {
                // habilitar TextBoxs
                $('#tbCOP_DilucionAntecedente_CEU_' + i).css('display', 'none');
                $('#tbCOP_DilucionAntecedente_CEU_' + i).prop('disabled', true);

                $('#tbCOP_DilucionConsecuente_CEU_' + i).css('display', 'none');
                $('#tbCOP_DilucionConsecuente_CEU_' + i).prop('disabled', false);
            }

            // Contenido de label a textboc        
            lbCOP_DilucionA = $('#lbCOP_DilucionAntecedente_CEU_' + i).text();
            $('#tbCOP_DilucionAntecedente_CEU_' + i).val(lbCOP_DilucionA);

            lbCOP_DilucionC = $('#lbCOP_DilucionConsecuente_CEU_' + i).text();
            $('#tbCOP_DilucionConsecuente_CEU_' + i).val(lbCOP_DilucionC);

            // Activa labels
            $('#lbCOP_DilucionAntecedente_CEU_' + i).css('display', 'block');
            $('#lbCOP_DilucionConsecuente_CEU_' + i).css('display', 'block');

            // chb
            $('#chbAplDilucion_CEU_' + i).css('display', 'none');

            //  PROPUESTA TECNICA TAB
            $('#lbProductoActual_' + i).css('display', 'block');
            $('#tbProductoActual_' + i).css('display', 'none');
            var lbProductoActual = $('#lbProductoActual_' + i).text();
            $('#tbProductoActual_' + i).val(lbProductoActual);

            $('#lbSituacionActual_' + i).css('display', 'block');
            $('#tbSituacionActual_' + i).css('display', 'none');
            var lbSituacionActual = $('#lbSituacionActual_' + i).text();
            $('#tbSituacionActual_' + i).val(lbSituacionActual);

            $('#lbVentajasKey_' + i).css('display', 'block');
            $('#tbVentajasKey_' + i).css('display', 'none');
            var lbVentajasKey = $('#lbVentajasKey_' + i).text();
            $('#tbVentajasKey_' + i).val(lbVentajasKey);

            $('#btnImagenSolucionActual_' + i).css('display', 'none');
            $('#btnImagenSolucionKey_' + i).css('display', 'none');

            // Resplada las imagenes actuales        

            var imgImagenProdActual = $('#hf_imgImagenProdActual_' + i).val();
            $('#imgImagenProdActual_' + i).attr('src', imgImagenProdActual);

            var imgImagenSolucionKey = $('#hf_imgImagenSolucionKey_' + i).val();
            $('#imgImagenSolucionKey_' + i).attr('src', imgImagenSolucionKey);

            Calcular_Renglon_ByNo(i);
        }
    },

    Desplegar: function (MC, PTP, Rec, Id_Ptp) {

        if (Rec.CPT_RecursoImagenProductoActual == "") {
            Rec.CPT_RecursoImagenProductoActual = _ApplicationUrl + '/imgupload/imagen_vacia.jpg'
        }

        if (Rec.CPT_RecursoImagenSolucionKey == "") {
            Rec.CPT_RecursoImagenSolucionKey = _ApplicationUrl + '/imgupload/imagen_vacia.jpg'
        }

        var CostoEnUso = Calcular_CostoEnUso(Rec).formatMoney(0, '.', ',');
        var GastoMensual = Rec.GastoMensual.formatMoney(0, '.', ',');
        var Vap_Precio = Rec.Vap_Precio.formatMoney(0, '.', ',');
        var PrecioDeLista = Rec.PrecioDeLista.formatMoney(0, '.', ',');

        AplDilucion = Rec.AplDilucion;

        if (AplDilucion == 1) {
            var lbDilucionAntecedente = '<label id="lbCOP_DilucionAntecedente_' + PTP + MC + '" style="display:block;">' + Rec.COP_DilucionAntecedente + '</label>';

            var DilucionAntecedente = '<input type="text" id="tbCOP_DilucionAntecedente_' + PTP + MC + '" value="' + Rec.COP_DilucionAntecedente + '" ' +
                'data-no="' + MC + '" ' +
                'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;" readonly>';

            var lbDilucionConsecuente = '<label id="lbCOP_DilucionConsecuente_' + PTP + MC + '" style="display:block;">' + Rec.COP_DilucionConsecuente + '</label>';

            var DilucionConsecuente = '<input type="text" id="tbCOP_DilucionConsecuente_' + PTP + MC + '" value="' + Rec.COP_DilucionConsecuente + '" ' +
                'data-no="' + MC + '" ' +
                'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;">';
        } else {

            /*
            var lbDilucionAntecedente = '<label id="lbCOP_DilucionAntecedente_' + PTP + MC + '" style="display:none;">' + Rec.COP_DilucionAntecedente + '</label>';
            var DilucionAntecedente = '<input type="text" id="tbCOP_DilucionAntecedente_' + PTP + MC + '" value="' + Rec.COP_DilucionAntecedente + '" ' +
            'data-no="' + MC + '" ' +
            'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;">';
            var lbDilucionConsecuente = '<label id="lbCOP_DilucionConsecuente_' + PTP + MC + '" style="display:none;">' + Rec.COP_DilucionConsecuente + '</label>';
            var DilucionConsecuente = '<input type="text" id="tbCOP_DilucionConsecuente_' + PTP + MC + '" value="' + Rec.COP_DilucionConsecuente + '" ' +
            'data-no="' + MC + '" ' +
            'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;">';
            */

            var lbDilucionAntecedente = '<label id="lbCOP_DilucionAntecedente_' + PTP + MC + '" style="display:none;">0</label>';

            var DilucionAntecedente = '<input type="text" id="tbCOP_DilucionAntecedente_' + PTP + MC + '" value="0" ' +
                'data-no="' + MC + '" ' +
                'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;">';

            var lbDilucionConsecuente = '<label id="lbCOP_DilucionConsecuente_' + PTP + MC + '" style="display:none;">0</label>';

            var DilucionConsecuente = '<input type="text" id="tbCOP_DilucionConsecuente_' + PTP + MC + '" value="0" ' +
                'data-no="' + MC + '" ' +
                'onblur="PTE.Calcular_Renglon(this);" style="display:none; width:30px;">';

        }

        //  |-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|
        //
        // PRPUESTA ECONOMICA 
        //
        //  |-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|

        var row = $('<tr>');

        row.append($('<td>').append(
            '<input type="hidden" id="hfId_VapDet_' + PTP + MC + '" value="' + Rec.Id_VapDet + '">' +
            '<input type="hidden" id="hfId_Prd_' + PTP + MC + '" value="' + Rec.Id_Prd + '">' +
            '<input type="hidden" id="hfId_AplDilucion_' + PTP + MC + '" value="' + Rec.AplDilucion + '">' +
            '<input type="hidden" id="hfPrd_UniEmp_' + PTP + MC + '" value="' + Rec.Prd_UniEmp + '">' +
            '<p>' + Rec.Id_Prd + '</p>'
        ));

        // PRODUCTO 
        row.append($('<td>').append(
            '<label id="Prd_Descripcion_' + PTP + MC + '">' + Rec.Prd_Descripcion + '</label>'
        ));

        //PRESENTACION
        row.append($('<td style="text-align:center;">').append(
            '<label id="Prd_Presentacion_' + PTP + MC + '">' + Rec.Prd_Presentacion + '</label>'
        ));

        // UNIDAD 
        row.append($('<td style="text-align:center;">').append(
            '<label id="Prd_Unidad_' + PTP + MC + '">' + Rec.Prd_UniNe + '</label>'
        ));

        //CONSUMO MENSUAL / UNIDADES
        row.append($('<td style="text-align:center;">').append(
            '<label id="lbVap_Cantidad_' + PTP + MC + '">' + Rec.Vap_Cantidad + '</label>' +
            '<input ' +
            'type="text" ' +
            'data-no="' + MC + '" ' +
            'onblur="PTE.Calcular_Renglon(this);" ' +
            'id="tbVap_Cantidad_' + PTP + MC + '" value=' + Rec.Vap_Cantidad + ' style="display:none; width:40px;"/>'
        ));

        // Consumo Mensual L
        row.append($('<td style="text-align:center;">').append(
            '<label id="ConsumoMensualL_' + PTP + MC + '">' + Rec.ConsumoMensualL + '</label>'
        ));

        //PRECIO DE LISTA 
        row.append($('<td style="text-align:center; display:none;">').append(
            '<label id="PrecioDeLista_' + PTP + MC + '">' + PrecioDeLista + '</label>'
        ));

        //PRECIO
        row.append($('<td style="text-align:center;">').append(
            '<label id="Vap_Precio_' + PTP + MC + '">' + Vap_Precio + '</label>'
        ));

        // Gasto Mensual 
        row.append($('<td style="text-align:center;">').append(
            '<label id="GastoMensual_' + PTP + MC + '">' + GastoMensual + '</label>'
        ));

        //
        if (Id_Ptp == 10) {

            // Dilucion
            row.append($('<td align="center">').append(
                '<table id="tblDilucionEditar">' +
                '<tr>' +
                '<td>' +
                '<input ' +
                'type="checkbox" ' +
                'data-no="' + MC + '" ' +
                'data-vapdet="' + Rec.Id_VapDet + '" ' +
                'style="display:none" ' +
                'onclick="PTE.chbAplDilucion_click(this);" ' +
                'id="chbAplDilucion_' + PTP + MC + '" name="chbAplDilucion" ' +
                'value=' + Rec.AplDilucion + ' ' +
                'style="width:30px;" ' +
                '>' +
                '</td><td>' +
                lbDilucionAntecedente +
                DilucionAntecedente +
                '</td>' +
                '<td>:</td>' +
                '<td>' +
                lbDilucionConsecuente +
                DilucionConsecuente +
                '</td>' +
                '</tr>' +
                '</table>'
            ));

            //Consumo mensual Diluidos
            row.append($('<td style="text-align:center;">').append(
                '<label id="ConsumoMensualLDiluidos_' + PTP + MC + '">' + Rec.ConsumoMensualLDiluidos + '</label>'
            ));

            // Costo en Uso
            row.append($('<td style="text-align:center;">').append(
                '<label id="CostoEnUso_' + PTP + MC + '">' + CostoEnUso + '</label>'
            ));

        }

        $('#tbl' + Id_Ptp + '_PropuestaEconomica > tbody').append(row);

        if (Rec.AplDilucion == 1) {

            $('#chbAplDilucion_' + PTP + MC).prop('checked', true);

        } else {

            $('#chbAplDilucion_' + PTP + MC).prop('checked', false);

        }

        /*
        if (Id_Ptp == 1) {
        $('#tbl' + 10 + '_PropuestaEconomica > tbody').append(row);
        }
        */


    },
    // 
    // CARGA PROPUSTA - TECNO-ECONOMICA
    //
    Valuacion_Cargar: function (obj) {
        var idcte = $(obj).data('idcte');
        var idval = $(obj).data('idval');
        var idop = $(obj).data('idop');

        $('#btnVisualizarPropuestaTE_' + idval).prop('disabled', true);

        $('#hf_Id_Op').val(idop);
        $('#hf_Id_Val').val(idval);
        $('#hf_Id_Cte').val(idcte);

        $('#spinner_loading').css('display', 'block');

        alertify.success('Edición de produesta ' + idval + ' / Cliente ' + idcte);

        $('#tbl1_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_1').css('display', 'none');

        $('#tbl2_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_2').css('display', 'none');

        $('#tbl3_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_3').css('display', 'none');

        $('#tbl4_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_4').css('display', 'none');

        $('#tbl5_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_5').css('display', 'none');

        $('#tbl10_PropuestaEconomica > tbody').empty();
        $('#divListadoProducto_10').css('display', 'none');

        $('#tblPropuestaTecnica > tbody').empty();

        $('#dvModalPropuestaTE_ver2').appendTo("body").modal('show');

        var modal_w = $('#dvModalPropuestaTE_ver2').width();
        modal_w = parseInt(modal_w);
        var MWEnt2 = (modal_w / 2) - 5;

        $('#rowPropuestaAcciones').css('display', 'block');

        //
        // DETALLE Listado de Productos 
        //

        //Cargar_PropuestaTecnoEconomica(_CRM_Usuario_Rik, idop , idcte, idval, function () {

        MAX_CONTROLS = 0;

        // QUÍMICO 1
        $('#spinner_tbl1').css('display', 'block');

        Cargar_PropuestaTecnoEconomica_Ver3(_CRM_Usuario_Rik, idop, idcte, idval, 1, function (lst) {
            if (lst.length > 0) {
                $('#divListadoProducto_1').css('display', 'block');
                $('#divListadoProducto_10').css('display', 'block');
                DesplegarDetalle_PTE(lst, MWEnt2, 1, function () {
                    $('#spinner_tbl1').css('display', 'none');
                });
            } else {
                $('#spinner_tbl1').css('display', 'none');
            }
        });

        // PAPEL 2        
        $('#spinner_tbl2').css('display', 'block');
        Cargar_PropuestaTecnoEconomica_Ver3(_CRM_Usuario_Rik, idop, idcte, idval, 2, function (lst) {
            if (lst.length > 0) {
                $('#divListadoProducto_2').css('display', 'block');
                DesplegarDetalle_PTE(lst, MWEnt2, 2, function () {
                    $('#spinner_tbl2').css('display', 'none');
                });
            } else {
                $('#spinner_tbl2').css('display', 'none');
            }
        });

        // SUPLEMENTOS 3        
        $('#spinner_tb3').css('display', 'block');
        Cargar_PropuestaTecnoEconomica_Ver3(_CRM_Usuario_Rik, idop, idcte, idval, 3, function (lst) {
            if (lst.length > 0) {
                $('#divListadoProducto_3').css('display', 'block');
                DesplegarDetalle_PTE(lst, MWEnt2, 3, function () {
                    $('#spinner_tbl3').css('display', 'none');
                });
            } else {
                $('#spinner_tbl3').css('display', 'none');
            }
        });

        //  4        
        $('#spinner_tb4').css('display', 'block');
        Cargar_PropuestaTecnoEconomica_Ver3(_CRM_Usuario_Rik, idop, idcte, idval, 4, function (lst) {
            if (lst.length > 0) {
                $('#divListadoProducto_4').css('display', 'block');
                DesplegarDetalle_PTE(lst, MWEnt2, 4, function () {
                    $('#spinner_tbl4').css('display', 'none');
                });
            } else {
                $('#spinner_tbl4').css('display', 'none');
            }
        });

        //  5        
        $('#spinner_tb5').css('display', 'block');
        Cargar_PropuestaTecnoEconomica_Ver3(_CRM_Usuario_Rik, idop, idcte, idval, 5, function (lst) {
            if (lst.length > 0) {
                $('#divListadoProducto_5').css('display', 'block');
                DesplegarDetalle_PTE(lst, MWEnt2, 5, function () {
                    $('#spinner_tbl5').css('display', 'none');
                });
            } else {
                $('#spinner_tbl5').css('display', 'none');
            }
        });

        $('#btnVisualizarPropuestaTE_' + idval).prop('disabled', true);

        //
        // ENCABEZADO 
        //

        //Cargar_PropuestaTecnoEconomicaEnc(_CRM_Usuario_Rik, 0, idcte, idval, function (Vap_Estatus, Vap_Estatus2) {

        Cargar_PropuestaTecnoEconomicaEnc_Ver2(_CRM_Usuario_Rik, 0, idcte, idval,
            function (Datos) {
                var Vap_Estatus = Datos.Vap_Estatus;
                var Vap_Estatus2 = Datos.Vap_Estatus2;

                // Verifica estatus 
                //var Vap_Estatus = $('#hf_Vap_Estatus').val();
                //var Vap_Estatus2 = $('#hf_Vap_Estatus2').val();            

                var SoloLectura = false

                if (Vap_Estatus = "A" && Vap_Estatus2 == 4) {
                    SoloLectura = true
                }

                if (Vap_Estatus = "C" && Vap_Estatus2 == 5) {
                    SoloLectura = true;
                }

                if (_Parametro_ControlesSoloLectura == 1) {
                    SoloLectura = true; // Para que no se pueda editar.
                }

                if (SoloLectura) {
                    $('#btnPropuestaEditar').prop('disabled', true);
                    $('#btnPropuestaAceptar').prop('disabled', true);
                } else {
                    $('#btnPropuestaEditar').prop('disabled', false);
                    $('#btnPropuestaAceptar').prop('disabled', false);
                }

            }  // func
        );

        $('#spinner_loading').css('display', 'none');

        window.resizeTo(screen.width - 300, screen.height);
    }

}

// JUN12-2020 RFH
function DesplegarDetalle_PTE(lst, MWEnt2, Id_Ptp, CALLBACK_Final) {
    //console.log(lst);     
    var RutaCarpetasImagen = "http://40.124.41.101/CatalogoUnico_Pruebas/Procesos/Archivos/337/";

    $('#tbl' + Id_Ptp + '_PropuestaEconomica > tbody').empty();

    try {
        if (lst.length <= 0) {
            alertify.error('Error: El detalle del documento no contiene registro.');
            return;
        }
    } catch (err) {
        alertify.error('Error: Ocurrio un error al tratar de cargar la valuaci&oacute;n.');
        return;
    }

    // LISTADO DE PRODUCTOS 

    for (var i = 0; i < lst.length; i++) {
        MAX_CONTROLS = MAX_CONTROLS + 1;

        console.log(lst[i]);

        PTE.Desplegar(MAX_CONTROLS, '', lst[i], Id_Ptp);

        if (Id_Ptp == 1) {
            PTE.Desplegar(MAX_CONTROLS, 'CEU_', lst[i], 10);
        }

        //(MC, PTP, Rec)
        // SI TIPO PRODUCTO = 10
        /*
        var PTP = '';
        if (CC == 2) {
            PTP = 'CEU_';
        }
        */


        //  |-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|
        //
        // PROPUESTA TECNICA 
        //
        //  |-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|

        var row_y = $('<tr>');

        row_y.append(
            $('<td style="width:50%;">').append(

                '<div class="panel panel-default" style="width:99%; height:350px;">' +
                '<div class="panel-heading" style="height: 50px;">' +
                '<label id="lbProductoActual_' + MAX_CONTROLS + '">' + lst[i].CPT_ProductoActual + '</label>' +
                '<input type="text" id="tbProductoActual_' + MAX_CONTROLS + '" value="' + lst[i].CPT_ProductoActual + '" style="display:none; width:100%;">' +
                '</div>' +
                '<div class="panel-body" style="height: 198px;">' +
                //'<table style="width:' + MWEnt2 + 'px; max-width:' + MWEnt2 + '">' +
                '<table style="width:100%">' +
                '<tr>' +
                '<td align="center">' +
                '<input type="hidden" id="hf_imgImagenProdActual_' + MAX_CONTROLS + '" value="' + lst[i].CPT_RecursoImagenProductoActual + '">' +
                '<img class="img-rounded" style="width: 140px; height: 140px;" id="imgImagenProdActual_' + MAX_CONTROLS + '" src="' + lst[i].CPT_RecursoImagenProductoActual + '">' +
                '</td>' +
                '</tr>' +
                '<tr>' +
                '<td align="center">' +
                '<button ' +
                'style="margin-top:4px; display:none;" ' +
                'type="button" ' +
                'data-"button" ' +
                'data-contenedor="#imgImagenProdActual_' + MAX_CONTROLS + '" ' +
                'onclick="BuscarImagen(this);" ' +
                'class="btn btn-default btn-sm" ' +
                'id="btnImagenSolucionActual_' + MAX_CONTROLS + '">Elegir imagen' +
                '</button>' +
                '</td>' +
                '</tr>' +
                '</table>' +
                '</div>' +
                '<div class="panel-footer" style="height: 99px;" >' +
                '<p>Situaci&oacute;n actual</p>' +
                '<label id="lbSituacionActual_' + MAX_CONTROLS + '">' + lst[i].CPT_SituacionActual + '</label>' +
                '<textarea style="display:none; width:100%" id="tbSituacionActual_' + MAX_CONTROLS + '" maxlength="250">' + lst[i].CPT_SituacionActual + '</textarea>' +
                '</div>' +
                '</div>'
            ));

        var CarpetaImagen = '';

        if (lst[i].ImagenProductoAltaRes == '') {
            //CarpetaImagen = 'http://localhost:61961/imgupload/imagen_vacia.jpg';
            // Correcto
            //CarpetaImagen = _ApplicationUrl + '/imgupload/imagen_vacia.jpg';

            if (lst[i].CPT_RecursoImagenSolucionKey == '') {
                CarpetaImagen = _ApplicationUrl + '/imgupload/imagen_vacia.jpg';
            } else {
                CarpetaImagen = lst[i].CPT_RecursoImagenSolucionKey;
            }

        } else {
            CarpetaImagen = RutaCarpetasImagen + lst[i].ImagenProductoAltaRes;
        }

        row_y.append(
            $('<td style="width:50%;">').append(
                '<div class="panel panel-default" style="width:99%; height:350px;">' +
                '<div class="panel-heading" style="height: 50px;">' +
                '<label>' + lst[i].ProductoKey + '</label>' +
                '</div>' +
                '<div class="panel-body" style="height: 198px;">' +
                //'<table style="width:' + MWEnt2 + 'px; max-width:' + MWEnt2 + '">' +
                '<table style="width:100%;">' +
                '<tr>' +
                '<td align="center">' +
                '<input type="hidden" id="hf_imgImagenSolucionKey_' + MAX_CONTROLS + '" value="' + CarpetaImagen + '">' +
                //'<img class="img-rounded" style="width: 140px; height: 140px;" id="imgImagenSolucionKey_' + MAX_CONTROLS + '" src="' + lst[i].CPT_RecursoImagenSolucionKey + '">' +
                '<img class="img-rounded" style="height: 140px; " id="imgImagenSolucionKey_' + MAX_CONTROLS + '" src="' + CarpetaImagen + '">' +
                '</td>' +
                '</tr>' +
                '<tr>' +
                '<td align="center">' +
                '<button ' +
                'style="margin-top:4px; display:none;" ' +
                'type="button" ' +
                'data-"button" ' +
                'data-contenedor="#imgImagenSolucionKey_' + MAX_CONTROLS + '" ' +
                'onclick="BuscarImagen(this);" ' +
                'class="btn btn-default btn-sm" id="btnImagenSolucionKey_' + MAX_CONTROLS + '">Elegir imagen</button>' +
                '</td>' +
                '</tr>' +
                '</table>' +
                '</div>' +
                '<div class="panel-footer" style="height: 99px;" >' +
                '<p>Ventajas KEY</p>' +
                '<label id="lbVentajasKey_' + MAX_CONTROLS + '">' + lst[i].CPT_VentajasKey + '</label>' +
                '<textarea style="display:none; width:100%" id="tbVentajasKey_' + MAX_CONTROLS + '" maxlength="250">' + lst[i].CPT_VentajasKey + '</textarea>' +
                '</div>' +
                '</div>'
            ));

        // SOLO TIPO 1 2 3 

        if (Id_Ptp < 10) {
            $('#tblPropuestaTecnica > tbody').append(row_y);
        }

        //$('#tbl' + Id_Ptp + '_PropuestaEconomica > tbody').append(row_y);

        // 

        if (AplDilucion == 1) {
            $('#chbAplDilucion_' + MAX_CONTROLS).prop('checked', true);
        }

        Calcular_Renglon_ByNo(MAX_CONTROLS);

    }

    if (CALLBACK_Final) {
        CALLBACK_Final();
    }
}

function PrevisualizarPropuesta(parameter) {

    console.log('Click En PrevisualizarPropuesta' + parameter);

    VISUALIZANDO_REPORTE = 1;

    parameter = parseInt(parameter);

    IdOp = $('#hf_Id_Op').val();
    IdVal = $('#hf_Id_Val').val();
    IdCte = $('#hf_Id_Cte').val();

    //$('#divPropuestaDetalle').css('display', 'none');
    //$('#pnlVisorDeReporte').css('display', 'block');

    setTimeout(function () {
        $('#lbPreparandoReporte').css('display', 'none');
    }, 2000);

    var URL = _ApplicationUrl + '/PortalRIK/GestionPromocion/Propuestas/VisorReportesPropuestaTecnoEconomica.aspx?IdRik=' + _CRM_Usuario_Rik + '&idTipoRep=' + parameter + '&idCte=' + IdCte + '&idVal=' + IdVal + '&idOp=' + IdOp;

    // Hay que pasar el RIK aqui.
    //$("#iframeVisorReporte").attr("src", URL);

    window.open(URL, '_blank');

}

function BuscarImagen(obj) {

    var contenedor = $(obj).data('contenedor');
    alertify.success(contenedor);
    $('#modalCargaImagen_Contenedor').val(contenedor);

    $('#lbmodalRecursoNombreArchivo').html('');
    $('#CampoURLId').val('');

    $('#modalCargaRecurso').appendTo("body").modal('show');
}

$(document).ready(function () {

    // Hack to enable multiple modals by making sure the .modal-open class
    // is set to the <body> when there is at least one modal open left
    $('body').on('hidden.bs.modal', function () {
        if ($('.modal.in').length > 0) {
            $('body').addClass('modal-open');
        }
    });

    //
    $('#dvModalPropuestaTE_ver2').on('show.bs.modal', function (event) {
        $('#divPropuestaDetalle').css('display', 'block');
        $('#pnlVisorDeReporte').css('display', 'none');

        $('#rowAceptarPropuesta').css('display', 'none');
        $('#rowPropuestaAcciones').css('display', 'none');
        $('#rowPropuestaEdicion').css('display', 'none');
        //$('#rowLoagin').css('display', 'block');
        //Valuacion_CargarDetalle();

        $('#dvModalPropuestaTE_ver2').find('.modal-body').css({
            width: 'auto', //probably not needed
            height: '98%', // 'auto', //probably not needed 
            'max-height': '100%'
        });

        var maxh = screen.height;

        $('#divPropuestaTecnica').css({
            height: '98%',
            'max-height': maxh - 280
        });
    });

    //
    $('#btnPropuestaEditar').on('click', function (e) {
        if (VISUALIZANDO_REPORTE == 1) {
            $('#divPropuestaDetalle').css('display', 'block');
            $('#pnlVisorDeReporte').css('display', 'none');
            VISUALIZANDO_REPORTE = 0;
        } else {
            $('#rowPropuestaAcciones').css('display', 'none');
            $('#rowPropuestaEdicion').css('display', 'block');
            PTE.Propuesta_ModoEdicion();
        }
    });

    // 
    $('#btnPropuestaCancelarEdicion').on('click', function (e) {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowPropuestaEdicion').css('display', 'none');
        PTE.Cancelar_Edicion();
    });

    // 
    $('#btnPropuesta_Guardar').on('click', function (e) {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowPropuestaEdicion').css('display', 'none');
        PTE.Propuesta_Guardar();
        //Propuesta_ModoLectura();
    });

    // 
    $('#btnPropuestaCerrar').on('click', function (e) {
        $('#dvModalPropuestaTE_ver2').modal('hide');
        var hfId_Cte = $('#hfId_Cte').val();
        $('#Slider_' + hfId_Cte).empty();
        CrearTablaHija(hfId_Cte, 0, 1);
    });

    // 
    $('#btnPropuestaCamposDelReporte').on('click', function (e) {

        $('#modalCamposDelReporte').appendTo("body").modal('show');
        $('#btnCamposDelRep_Guardar').prop('disabled', true);
        $('#spinner_CamposDeReporte').css('display', 'block');

        var Id_Op = $('#hf_Id_Op').val();

        $('#tbKey_NombreRik').val('');
        $('#tbKey_RazonSocial').val('');
        $('#tbKey_Calle').val('');
        $('#tbKey_No').val('');
        $('#tbKey_CP').val('');
        $('#tbKey_Colonia').val('');
        $('#tbKey_Municipio').val('');
        $('#tbKey_Estado').val('');
        $('#tbKey_Nomenclatura').val('');

        $('#tbCte_Nombre').val('');
        $('#tbCte_Calle').val('');
        $('#tbCte_No').val('');
        $('#tbCte_CP').val('');
        $('#tbCte_Colonia').val('');
        $('#tbCte_Estado').val('');
        $('#tbCte_AtencionA').val('');

        Get_ReporteParametros(Id_Op, function (res) {

            $('#tbKey_NombreRik').val(res.Key_NombreRik);
            $('#tbKey_RazonSocial').val(res.Key_RazonSocial);
            $('#tbKey_Calle').val(res.Key_Calle);
            $('#tbKey_No').val(res.Key_No);
            $('#tbKey_CP').val(res.Key_CP);
            $('#tbKey_Colonia').val(res.Key_Colonia);
            $('#tbKey_Municipio').val(res.Key_Municipio);
            $('#tbKey_Estado').val(res.Key_Estado);
            $('#tbKey_Nomenclatura').val(res.Key_Nomenclatura);

            $('#tbCte_Nombre').val(res.Cte_Nombre);
            $('#tbCte_Calle').val(res.Cte_Calle);
            $('#tbCte_No').val(res.Cte_No);
            $('#tbCte_CP').val(res.Cte_CP);
            $('#tbCte_Colonia').val(res.Cte_Colonia);
            $('#tbCte_Municipio').val(res.Cte_Municipio);
            $('#tbCte_Estado').val(res.Cte_Estado);
            $('#tbCte_AtencionA').val(res.Cte_AtencionA);

            $('#spinner_CamposDeReporte').css('display', 'none');
            $('#btnCamposDelRep_Guardar').prop('disabled', false);

        },
            function () {
                // CALLBACK_Error
                $('#spinner_CamposDeReporte').css('display', 'none');
                $('#btnCamposDelRep_Guardar').prop('disabled', false);
            });

    });

    // Guardar Campos de Reportes
    /* 
    $('#btnCamposDelRep_Guardar').on('click', function (e) {
    var Id_Op = $('#hf_Id_Op').val();
    var tbNombreAtencion = $('#tbNombreAtencion').val();
    var tbNombreRik = $('#tbNombreRik').val();
    var tbRepresentanteClienteNombre = $('#tbRepresentanteClienteNombre').val();
    var tbDireccion1 = $('#tbDireccion1').val();
    var tbDireccion2 = $('#tbDireccion2').val();
    var tbDireccion_CP = $('#tbDireccion_CP').val();
    var tbDireccion_NoExt = $('#tbDireccion_NoExt').val();
    var tbDireccion_Tel = $('#tbDireccion_Tel').val();
    var tbDiasCredito = $('#tbDiasCredito').val();
    tbDiasCredito = parseInt(tbDiasCredito);
    if (isNaN(tbDiasCredito)) {
    tbDiasCredito = 0;
    }
    $('#tbDiasCredito').val(tbDiasCredito);
    var eCRM_ValuacionCamposAdicionales = {
    'Id_Emp': 0,
    'Id_Cd': 0,
    'Id_Op': Id_Op,
    'NombreAtencion': tbNombreAtencion,
    'RepresentanteClienteNombre': tbRepresentanteClienteNombre,
    'NombreRik': tbNombreRik,
    'Direccion1': tbDireccion1,
    'Direccion2': '',
    'CP': tbDireccion_CP,
    'NoExterior': tbDireccion_NoExt,
    'Telefono': tbDireccion_Tel,
    'DiasCredito': tbDiasCredito
    }
    InsertUpdate_ReporteParametros_Ver2(
    eCRM_ValuacionCamposAdicionales,
    function () {
    $('#modalCamposDelReporte').modal('hide');
    alertify.success('Se actualizo la información correctamente.');
    }
    );
    */

    $('#btnCamposDelRep_Guardar').on('click', function (e) {
        var Id_Op = $('#hf_Id_Op').val();

        var Key_NombreRik = $('#tbKey_NombreRik').val();
        var Key_RazonSocial = $('#tbKey_RazonSocial').val();
        var Key_Calle = $('#tbKey_Calle').val();
        var Key_No = $('#tbKey_No').val();
        var Key_CP = $('#tbKey_CP').val();
        var Key_Colonia = $('#tbKey_Colonia').val();
        var Key_Municipio = $('#tbKey_Municipio').val();
        var Key_Estado = $('#tbKey_Estado').val();
        var Key_Nomenclatura = $('#tbKey_Nomenclatura').val();

        var Cte_Nombre = $('#tbCte_Nombre').val();
        var Cte_Calle = $('#tbCte_Calle').val();
        var Cte_No = $('#tbCte_No').val();
        var Cte_CP = $('#tbCte_CP').val();
        var Cte_Colonia = $('#tbCte_Colonia').val();
        var Cte_Municipio = $('#tbCte_Municipio').val();
        var Cte_Estado = $('#tbCte_Estado').val();
        var Cte_DiasCredito = $('#tbCte_DiasCredito').val();
        var Cte_AtencionA = $('#tbCte_AtencionA').val();

        /*
        tbDiasCredito = parseInt(tbDiasCredito);
        if (isNaN(tbDiasCredito)) {
        tbDiasCredito = 0;
        }
        */
        var tbDiasCredito = 0;
        $('#tbDiasCredito').val(tbDiasCredito);

        var eCRM_ValuacionCamposAdicionales = {
            'Id_Emp': 0,
            'Id_Cd': 0,
            'Id_Op': Id_Op,

            'Key_NombreRik': Key_NombreRik,
            'Key_RazonSocial': Key_RazonSocial,
            'Key_Calle': Key_Calle,
            'Key_No': Key_No,
            'Key_CP': Key_CP,
            'Key_Colonia': Key_Colonia,
            'Key_Municipio': Key_Municipio,
            'Key_Estado': Key_Estado,
            'Key_Nomenclatura': Key_Nomenclatura,

            'Cte_Nombre': Cte_Nombre,
            'Cte_Calle': Cte_Calle,
            'Cte_No': Cte_No,
            'Cte_CP': Cte_CP,
            'Cte_Colonia': Cte_Colonia,
            'Cte_Municipio': Cte_Municipio,
            'Cte_Estado': Cte_Estado,
            'Cte_DiasCredito': Cte_DiasCredito,
            'Cte_AtencionA': Cte_AtencionA
        }

        InsertUpdate_ReporteParametros_Ver2(eCRM_ValuacionCamposAdicionales,
            function () {
                $('#modalCamposDelReporte').modal('hide');
                alertify.success('Se actualizo la información correctamente.');
            }
        );


    });

    // 
    $('#btnVisualizarPropuestaTE').click(function () {
        alert("xx");
    });

    // 
    $('#btnSubitImagen').click(function () {
        var Op = $('#hf_Id_Op').val();
        var IdRep = 0;
        var status = 0;
        var NombreArchivo = "";
        //console.log(NombreArchivo);
        crearRecursoArchivoUsandoFormData(Op, NombreArchivo, IdRep, status);
    });

    // 
    $('#btnBuscarArchivo').click(function () {
        $("#file").click();
    });

    // 
    $('#modalCagaRecurso_Aceptar').click(function () {
        CampoURLId = $('#CampoURLId').val();
        CampoURLId = CampoURLId.trim();
        if (CampoURLId.length > 0) {
            // por urls
            var Contenedor = $('#modalCargaImagen_Contenedor').val();
            $(Contenedor).attr("src", CampoURLId);
            $('#modalCargaRecurso').modal('hide');
        } else {
            // por archivo cargado
            $("#btnSubitImagen").click();
        }
    });

    // 
    $("#file").change(function () {
        var file_data = $('#file').prop('files')[0];

        $('#lbmodalRecursoNombreArchivo').html('Nombre de archivo:</br>' + file_data.name);

    });

    // 
    $('#btnAceptarPro_Aceptar').click(function () {
        var Id_Val = $('#hf_Id_Val').val();
        Id_Val = parseInt(Id_Val);
        if (isNaN(Id_Val)) {
            Id_Val = 0;
        }
        aceptarPropuestaTecnoEconomica(Id_Val);
    });

    // 
    $('#btnPropuestaAceptar').click(function () {
        var Id_Val = $('#hf_Id_Val').val();
        Id_Val = parseInt(Id_Val);
        if (isNaN(Id_Val)) {
            Id_Val = 0;
        }
        $('#rowPropuestaAcciones').css('display', 'none');
        $('#rowAceptarPropuesta').css('display', 'block');

        /*
        BootstrapConfirm.showWarning('Aceptar Propuesta', 'Está a punto de aceptar esta propuesta y ' +
        'generar el ACYS correspondiente. ¿Desea continuar?', function () {
            
        });
        */

        /*
        alertify
        .okBtn("Si, Aceptar propuesta y crear ACYS.")
        .cancelBtn("No" )
        .confirm("<b>Aceptar Propuesta</b><br/>Está a punto de aceptar esta propuesta y generar el ACYS correspondiente. ¿Desea continuar?", function (ev) {
        ev.preventDefault();
        aceptarPropuestaTecnoEconomica(Id_Val);
        $('#dvModalPropuestaTE_ver2').modal('hide');
        }, function (ev) {
        ev.preventDefault();
        }).bringToFront();
        */
    });

    //
    $('#btnAceptarPro_Cancelar').click(function () {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowAceptarPropuesta').css('display', 'none');
    });

});