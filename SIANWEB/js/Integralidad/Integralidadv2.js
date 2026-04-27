var Lista_Datos = [];
var DatosConsultaV2 = [];
var DatosConstulaApi = [];
var DatosAplicaciones = [];

var DatosTotalesExcel = [];
var TotalDatoTipo = [];
var ListaTotalesBD = [];
var DatosConsultaDetalleHoja1V2 = [];
var ListaTotalesMatriz = [];

function Cargar_ZonaSucursal(CALLBACK_Exito) {
    $('#spinner_Cargando').css('display', 'block');

    $.ajax({
        url: _ApplicationUrl + '/api/Integralidadv2/spListCdiByZona',
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        Estado = response.Estado;
        listado = response.Datos;
       
        if (Estado == 1) {
            if (listado.length > 0) {

                document.getElementById('lblCd').innerHTML = listado[0].Sucursal;
                document.getElementById('lblZona').innerHTML = listado[0].Zona;
            }
           
        }
        if (CALLBACK_Exito) {
            $('#spinner_Cargando').css('display', 'none');
            CALLBACK_Exito();
        }
    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_Cargando').css('display', 'none');
        alertify.error('Error: Cargar_Segmentos(327)');
        console.log(jqXHR);
    });
}
var Integralidadv2 = {
    CargarComboRepre: function (CALLBACK_Exito) {

        $('#spinner_Cargando').css('display', 'block');
        //$('#btnCargarListado').attr('disabled', true);

        $.ajax({
            url: _ApplicationUrl + '/api/CrmRepresentante/Get_List?Id_Cd=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
                 
            var ddlRep = $('#ddlRepresentanteV2').empty();
            ddlRep.append(
                $('<option data-Id_U=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddlRep.append(
                        $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_Rik).text(listado[i].U_Nombre)
                    );
                    ID = listado[i].Id_U;
                }

                hfId_Rik = parseInt(hfId_Rik);
                if (isNaN(hfId_Rik)) {
                    hfId_Rik = 0;
                }

                hfId_TU = parseInt(hfId_TU);
                if (isNaN(hfId_TU)) {
                    hfId_TU = 0;
                }
                if (hfId_TU == 3) {
                    // Si es gerente
                    ddlRep.removeAttr('disabled');
                } else {
                    ddlRep.val(hfId_Rik);
                    //ddlRep.prop('disabled', 'disabled');
                    ddlRep.removeAttr('disabled');
                }
                $('#spinner_Cargando').css('display', 'none');
                $('#btnCargarListado').attr('disabled', false);
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {
            //$('#spinner_GCIndice').css('display', 'none');
            //$('#btnCargarListado').attr('disabled', false);
            alertify.error('Error: CatRepresentante.Cargar_Representante');
            console.log(jqXHR);
        });
    },
    CargarUenSegmento: function (Id_Cd, Tipo, Id_Uen = 0, CALLBACK_Exito, CALLBACK_Error) {

        $.ajax({
            url: _ApplicationUrl + '/api/Integralidadv2/spConsultar_SegmentoUen?Id_Cd=' + 0 + "&Tipo=" + Tipo + "&Id_Uen=" + Id_Uen,
            cache: false,
            type: 'GET',
            async: false,
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            $('#spinner_Cargando').css('display', 'none');
            var Listado = response.Datos;
            if (Estado == 1) {
                if (Tipo == 1) {
                    var ddlRep = $('#ddlFiltroUen').empty();
                    ddlRep.append(
                        $('<option data-Id_U=0>').val(0).text('-- Todos --')
                    );
                    for (var i = 0; i < Listado?.length; i++) {
                        ddlRep.append(
                            $('<option data-Id_U=' + Listado[i].Id_Seg + ' data-IdRik=' + Listado[i].Id_Seg + ' >').val(Listado[i].Id_Seg).text(Listado[i].Seg_Descripcion)
                        );

                    }
                } else if (Tipo == 2) {
                    var ddlRep = $('#ddlFiltroSegmentos').empty();
                    ddlRep.append(
                        $('<option data-Id_U=0>').val(0).text('-- Todos --')
                    );
                    for (var i = 0; i < Listado?.length; i++) {
                        ddlRep.append(
                            $('<option data-Id_U=' + Listado[i].Id_Seg + ' data-IdRik=' + Listado[i].Id_Seg + ' >').val(Listado[i].Id_Seg).text(Listado[i].Seg_Descripcion)
                        );
                    }
                }


            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito(listado);
            }
        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            } else {
                alertify.error('Error: CargarUenSegmento');
                console.log(jqXHR);
                CALLBACK_Error();
            }
        });
    },
    btnAplicarv2_Click: function () {
        Integralidadv2.showAppSi('none');
        Integralidadv2.showAppNo('none');

        let Rik = $('#ddlRepresentanteV2').val();
        let Seg = $('#ddlFiltroSegmentos').val();
        let Ctes = $('#ddlRazonSocialCtes').val();
        let periodo = $('#tbPeriodoInicio').val();
        let partPeriodo = periodo.split(" ");
        let Id_Uen = $('#ddlFiltroUen').val();
        if (!Rik) {
            Rik = 0;
        }

        if (partPeriodo.length <= 1) {
            alertify.alert('Error: Seleccione el periodo');
            return false;
        }

        let anio = partPeriodo[1];
        let mes = partPeriodo[0];

        $('#spinner_Cargando').css('display', 'block');


        Integralidadv2.ConsultarV2(Rik, Seg, Ctes, mes, anio, Id_Uen, 7, function (Lst7) {
            ListaTotalesMatriz = Lst7;
            Integralidadv2.ConsultarV2(Rik, Seg, Ctes, mes, anio, Id_Uen, 3, function (Lst) {
                ListaTotalesBD = Lst;
                $('#spinner_Cargando').css('display', 'block');
                Integralidadv2.ConsultarV2(Rik, Seg, Ctes, mes, anio, Id_Uen, 5, function (Lst) {

                    $('#tblIntegralidad > tbody').empty();
               
                    DatosConstulaApi = Lst;
                    Integralidadv2.Desplegarv2(Lst);
                    Integralidadv2.GenerarListaClientes(Lst);

                    $('#spinner_Cargando').css('display', 'none');
                }, function () {
                    $('#spinner_Cargando').css('display', 'none');
                });

            }, function () {
            });

        }, function () {
        });
  

        Integralidadv2.ConsultarV2(Rik, Seg, Ctes, mes, anio, Id_Uen, 6, function (Lst) {
            TotalDatoTipo = Lst;
        }, function () {
        });
    },

    ConsultarV2: function (Riks, Segs, Ctes, mes, anio, Id_Uen, Tipo = 5, CALLBACK_Exito, CALLBACK_Error) {

        if (Segs == null) {
            Segs = 0
        } if (Ctes == null) {
            Ctes = 0
        } if (Id_Uen == null) {
            Id_Uen = 0
        } if (Tipo == null) {
            Tipo = 0
        }

        $.ajax({
            //url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_IntegralidadMes_Ver2?' +
            url: _ApplicationUrl + '/api/Integralidadv2/sp_MapaAplicaciones_DetalleXMes?' +
                'Id_Rik=' + Riks + '&Id_Cte=' + Ctes + '&Id_Seg=' + Segs + '&AnioIni=' + anio + '&MesIni=' + mes + '&Id_Uen=' + Id_Uen + "&Tipo=" + Tipo,
            cache: false,
            type: 'GET',
            async: true,
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;


            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    if (Tipo == 5) {
                        $('#spinner_Cargando').css('display', 'none');
                        $('#trbtndownload').css('display', 'contents');
                    }

                    CALLBACK_Exito(listado);
                }
            } else {
                alertify.error('Error:  Integralidad.Consultar(992)');
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        }).fail(function (jqXHR, textStatus, error) {
            $('#trbtndownload').css('display', 'none');
            $('#spinner_Cargando').css('display', 'none');
            //$('#spinner_Cargando').css('display', 'none');
            alertify.error('Error:  Integralidad.Consultar(997)');
            console.log(jqXHR);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        });
    },

    Desplegarv2: function (Lst, tipo = 1) {


        if (Lst.length <= 0) {
            $('#trbtndownload').css('display', 'none');

            alertify.alert('No se encontró información con esos parámetros.');
        }
        Lista_Datos = Lst;

        var ventasPorIdOp = {};
        var ventasPorAplicaciones = {};

        DatosConsultaDetalleHoja1V2 = Lst;
        for (var i = 0; i < Lst.length; i++) {
            let { Apl_Descripcion, Id_Op, Venta, Id_Cte, Cliente, VPO, VPOMeta, VPT, GAP, Seg_Descripcion, Id_Rik, Rik, Id_Ter, Anio, Mes, TipoProducto, Bracket, Porc_CoberturaVPO, Porc_CoberturaVPT, Uen_Desc, Id_Uen, Seg_Unidades, Seg_ValUniDim, Id_Seg, Cantidad, PorcentajeAplicacion, Sucursal, AplPotencialGeneralVende, AplPotencialGeneralNoVende } = Lst[i];
            let GAPVPO = VPO > 0 ? (VPO - Venta) : 0;
            let GAPVPT = Venta - VPT;
            //var idPorIdOp = Id_Cte + '-' + Id_Rik;
            var idPorIdOp = Id_Cte ;

            if (!ventasPorIdOp[idPorIdOp]) {
                ventasPorIdOp[idPorIdOp] = {
                    Venta,
                    Id_Cte,
                    Cliente,
                    VPO,
                    VPOMeta,
                    Seg_Descripcion,
                    VPT, GAP, Id_Op,
                    GAPVPO, GAPVPT, Rik, Id_Ter, Anio, Mes, TipoProducto, Bracket,
                    Porc_CoberturaVPO, Porc_CoberturaVPT, Uen_Desc, Id_Uen, Seg_Unidades, Seg_ValUniDim, Id_Seg, Cantidad, PorcentajeAplicacion,
                    PorcentajeAplicacionAppCliente: 0, TotPorcentajeAplicacionPotencialCliente: 0
                    , Sucursal, Id_Rik, AplPotencialGeneralVende, AplPotencialGeneralNoVende
                };
            } else {
                ventasPorIdOp[idPorIdOp].Venta += Venta;
                //ventasPorIdOp[Id_Cte].VPOMeta += VPOMeta;
                ventasPorIdOp[idPorIdOp].GAP += GAP;
                ventasPorIdOp[idPorIdOp].GAPVPO += GAPVPO;
                ventasPorIdOp[idPorIdOp].GAPVPT += GAPVPT;
                ventasPorIdOp[idPorIdOp].PorcentajeAplicacion += PorcentajeAplicacion;
                if (ventasPorIdOp[idPorIdOp].Porc_CoberturaVPO <=0) {
                    ventasPorIdOp[idPorIdOp].Porc_CoberturaVPO = (parseFloat(Porc_CoberturaVPO));
                }
                if (ventasPorIdOp[idPorIdOp].Porc_CoberturaVPT <= 0) {
                    ventasPorIdOp[idPorIdOp].Porc_CoberturaVPT = (parseFloat(Porc_CoberturaVPT));
                }

                if (ventasPorIdOp[idPorIdOp].VPT <= 0) {
                    ventasPorIdOp[idPorIdOp].VPT = (parseFloat(VPT));
                }
                if (ventasPorIdOp[idPorIdOp].VPO <= 0) {
                    ventasPorIdOp[idPorIdOp].VPO = (parseFloat(VPO));
                }
            }
          
            var idApp = Id_Cte + '-' + Apl_Descripcion + '-' + Id_Rik;
            let MontoPotencial = parseFloat(((PorcentajeAplicacion / 100) * VPT).toFixed(2));
//                ((PorcentajeAplicacion / 100) * VPT).toFixed(2)

            if (!ventasPorAplicaciones[idApp]) {
                ventasPorAplicaciones[idApp] = {
                    Venta,
                    Id_Cte,
                    Cliente,
                    VPO,
                    Seg_Descripcion,
                    VPT, Rik, TipoProducto:TipoProducto.trim(), Bracket,
                    Porc_CoberturaVPO, Porc_CoberturaVPT, Uen_Desc, Id_Uen, Seg_Unidades, Seg_ValUniDim, Id_Seg, Cantidad, PorcentajeAplicacion
                    , Sucursal, Id_Rik, Apl_Descripcion, MontoPotencial
                };
            } else {
                ventasPorAplicaciones[idApp].Venta += parseFloat(Venta);
                ventasPorAplicaciones[idApp].MontoPotencial += (parseFloat(MontoPotencial));
                ventasPorAplicaciones[idApp].Porc_CoberturaVPO = (parseFloat(Porc_CoberturaVPO));
                ventasPorAplicaciones[idApp].Porc_CoberturaVPT = (parseFloat(Porc_CoberturaVPT));
                if (ventasPorAplicaciones[idApp].Porc_CoberturaVPO <= 0) {
                    ventasPorAplicaciones[idApp].Porc_CoberturaVPO = (parseFloat(Porc_CoberturaVPO));
                }
                if (ventasPorAplicaciones[idApp].Porc_CoberturaVPT <= 0) {
                    ventasPorAplicaciones[idApp].Porc_CoberturaVPT = (parseFloat(Porc_CoberturaVPT));
                }

                if (ventasPorAplicaciones[idApp].VPO <= 0) {
                    ventasPorAplicaciones[idApp].VPO = (parseFloat(VPO));
                }
                if (ventasPorAplicaciones[idApp].VPT <= 0) {
                    ventasPorAplicaciones[idApp].VPT = (parseFloat(VPT));
                }
            }
        }

        var arreglo = Object.values(ventasPorIdOp);
    
        DatosAplicaciones = Object.values(ventasPorAplicaciones);
        const resultadofff = DatosAplicaciones.filter(item => item.Id_Cte === 2288);


        const currentDate = new Date();
        const anioAct = currentDate.getFullYear();
        const mesAct = currentDate.getMonth() + 1;


        $('#tblIntegralidad > tbody').empty();
        let totalVenta = 0;
        let totalVPT = 0;
        let totalVPO = 0;
        let totalPorcCoberturaVPT = 0;
        let totalPorcCoberturaVPO = 0;
        let totalPorcApp = 0;
        let totalPorcPotencialApp = 0;
 
        DatosConsultaV2 = [...Lista_Datos, { PorcentajeAplicacionAppCliente: 0, TotPorcentajeAplicacionPotencialCliente: 0 }];

        for (var i = 0; i < arreglo.length; i++) {

            const listaApp = Lst.filter(function (item) {
                return item.Id_Cte == arreglo[i].Id_Cte;//&& item.Id_Rik == arreglo[i].Id_Rik;
            });

            const idRiksUnicos = new Set(listaApp.map(app => app.Id_Rik));
          
            let aplicacionesUnicas= listaApp.filter(app => app.PorcentajeAplicacion > 0 && app.Venta > 0) 
            let aplicacionesUnicasNOVende = listaApp.filter(app =>
                app.PorcentajeAplicacion > 0 &&
                app.Venta <= 0 &&
                !aplicacionesUnicas.some(a => a.Apl_Descripcion === app.Apl_Descripcion)
            );
       
          
            var PorcentajeAplicacionAppCliente = 0;
            for (var xx = 0; xx < aplicacionesUnicas.length; xx++) {
                let appsConEsaDescripcionV = listaApp.filter(app => app.Apl_Descripcion === aplicacionesUnicas[xx].Apl_Descripcion && app.Venta > 0);

                let riksUnicos = new Set(appsConEsaDescripcionV.map(app => app.Id_Rik));
                let CountRik2 = riksUnicos.size;

                let { PorcentajeAplicacion } = aplicacionesUnicas[xx];
                //PorcentajeAplicacionAppCliente += (PorcentajeAplicacion > 0 ? Math.floor(PorcentajeAplicacion) / CountRik : 0);
                PorcentajeAplicacionAppCliente += (PorcentajeAplicacion > 0 ? (PorcentajeAplicacion / CountRik2)  : 0);
            }

            var totPorcentajeAplicacionPotencialCliente = 0;
            for (var xx = 0; xx < aplicacionesUnicasNOVende.length; xx++) {
                let appsConEsaDescripcionV = listaApp.filter(app => app.Apl_Descripcion === aplicacionesUnicasNOVende[xx].Apl_Descripcion && app.Venta <= 0);

                let riksUnicos = new Set(appsConEsaDescripcionV.map(app => app.Id_Rik));
                let CountRik2 = riksUnicos.size;

                let { PorcentajeAplicacion } = aplicacionesUnicasNOVende[xx];
                //totPorcentajeAplicacionPotencialCliente += PorcentajeAplicacion > 0 ? Math.floor(PorcentajeAplicacion) / CountRik : 0;
                totPorcentajeAplicacionPotencialCliente += PorcentajeAplicacion > 0 ? (PorcentajeAplicacion / CountRik2) : 0;
            }

            arreglo[i].PorcentajeAplicacionAppCliente = (parseFloat(PorcentajeAplicacionAppCliente) ).toFixed(0);
            arreglo[i].TotPorcentajeAplicacionPotencialCliente = (parseFloat(totPorcentajeAplicacionPotencialCliente) ).toFixed(0);

            totalPorcApp += parseFloat(arreglo[i]?.PorcentajeAplicacionAppCliente);
            totalPorcPotencialApp += parseFloat(arreglo[i]?.TotPorcentajeAplicacionPotencialCliente);

            var ventasNegativas = arreglo.filter(function (item) {
                return item.Venta <= 0;
            });

            var ventasPositivas = arreglo.filter(function (item) {
                return item.Venta > 0;
            });

            var totalAppVentas = ventasNegativas.length + ventasPositivas.length;
            let porcentaje = 0;
            if (ventasPositivas.length > 0) {
                porcentaje = ((ventasPositivas.length / 100) / totalAppVentas)
            }
            let semaforo = '<img onclick="Integralidadv2.verApp(' + arreglo[i].Id_Cte + ', \'' + arreglo[i].Cliente.replace(/'/g, "\\'") + '\', ' + arreglo[i].Venta + ', ' + arreglo[i].Id_Rik + ');" width="14px" src="../../Imagenes/flecha_derecha.png">';


            // var row = $('<tr class="cliente_list" id=trcliente_' + arreglo[i].Id_Cte + '-' + arreglo[i].Id_Rik+  '>');
            var row = $('<tr class="cliente_list" id=trcliente_' + arreglo[i].Id_Cte+ '>');
            
            row.append($('<td class="text-center">').append(
                semaforo
            ));

            row.append($('<td>').append(
                arreglo[i].Id_Cte
            ));
            row.append($('<td>').append(
                arreglo[i].Cliente
            ));

            row.append($('<td>').append(
                arreglo[i].Bracket
            ));

            row.append($('<td>').append(
                arreglo[i].Uen_Desc
            ));


            row.append($('<td>').append(
                arreglo[i].Seg_Descripcion
            ));

            var Venta = arreglo[i].Venta;
            Venta = parseFloat(Venta);
            totalVenta += Venta;
            Venta = parseFloat(Venta.toFixed(2)).formatMoney(2, '.', ',');
            row.append($('<td>').append(
                '$' + Venta
            ));

            var VPT = arreglo[i].VPT;
            VPT = parseFloat(VPT);
            totalVPT += VPT;

            VPT = VPT.formatMoney(2, '.', ',');
            row.append($('<td>').append(
                '<label class="">$' + VPT + '</label>'
            ));

            var VPO = arreglo[i].VPO;
            VPO = parseFloat(VPO);
            totalVPO += VPO;
            VPO = VPO.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                '$' + VPO
            ));

            var VPOMeta = arreglo[i].VPOMeta;
            VPOMeta = parseFloat(VPOMeta);
            VPOMeta = VPOMeta.formatMoney(2, '.', ',');

            let button = '';
            if (arreglo[i].Anio == anioAct && arreglo[i].Mes == mesAct) {
                button = ' <button ' +
                    ' title="Editar Valor portencial meta"' +
                    ' data-id_rik="' + arreglo[i].Id_Rik + '" ' +
                    ' data-reptesentante="' + arreglo[i].Rik + '" ' +
                    ' data-id_cte="' + arreglo[i].Id_Cte + '" ' +
                    ' data-cliente="' + arreglo[i].Cliente + '" ' +
                    ' data-id_ter="' + arreglo[i].Id_Ter + '" ' +
                    ' data-id_seg="' + arreglo[i].Id_Seg + '" ' +
                    ' data-segmento="' + arreglo[i].Seg_Descripcion + '" ' +
                    ' data-seg_unidades="' + arreglo[i].Seg_Unidades + '" ' +
                    ' data-seg_valunidim="' + arreglo[i].Seg_ValUniDim + '" ' +
                    ' data-cantidad="' + arreglo[i].Cantidad + '" ' +
                    ' data-dvpometa="' + arreglo[i].VPOMeta + '" ' +
                    ' data-vpo="' + arreglo[i].VPO + '" ' +
                    ' data-anio="' + arreglo[i].Anio + '" ' +
                    ' data-mes="' + arreglo[i].Mes + '" ' +
                    ' onclick="Integralidadv2.DesplegarEditarVPOMetav2(this);" class="btn btn-primary"><i class="btn btn-primary" aria-hidden="true"></i></button>';
            }

            buttonVP = ' <button ' +
                ' title="Editar Valor portencial meta"' +
                ' data-id_rik="' + arreglo[i].Id_Rik + '" ' +
                ' data-reptesentante="' + arreglo[i].Rik + '" ' +
                ' data-id_cte="' + arreglo[i].Id_Cte + '" ' +
                ' data-cliente="' + arreglo[i].Cliente + '" ' +
                ' data-id_ter="' + arreglo[i].Id_Ter + '" ' +
                ' data-id_seg="' + arreglo[i].Id_Seg + '" ' +
                ' data-segmento="' + arreglo[i].Seg_Descripcion + '" ' +
                ' data-seg_unidades="' + arreglo[i].Seg_Unidades + '" ' +
                ' data-seg_valunidim="' + arreglo[i].Seg_ValUniDim + '" ' +
                ' data-cantidad="' + arreglo[i].Cantidad + '" ' +
                ' data-dvpometa="' + arreglo[i].VPOMeta + '" ' +
                ' data-anio="' + arreglo[i].Anio + '" ' +
                ' data-mes="' + arreglo[i].Mes + '" ' +
                ' data-id_uen="' + arreglo[i].Id_Uen + '" ' +
                ' data-uen_desc="' + arreglo[i].Uen_Desc + '" ' +
                ' data-vpo="' + arreglo[i].VPO + '" ' +
                ' data-vpt="' + arreglo[i].VPT + '" ' +
                ' onclick="Integralidadv2.DesplegarEditarPotencial(this);" class="btn btn-primary"><i class="fa fa-pencil" aria-hidden="true"></i></button>';


  

            totalPorcCoberturaVPT += (parseFloat(arreglo[i]?.Porc_CoberturaVPT)) * 1;
            var colorPorCober = Integralidadv2.colorPrcentajeCobertura((arreglo[i]?.Porc_CoberturaVPT));

            row.append($('<td class="text-right">').append(
                '<label class="color_IntV2 ' + colorPorCober + '">' + (parseFloat(arreglo[i]?.Porc_CoberturaVPT).toFixed(0)) + '%' + '</label>'
            ));


            var GAPVPO = arreglo[i].GAPVPO;
            var colorGAPVPO = 'black';
            if (GAPVPO < 0) {
                colorGAPVPO = 'red';
            } else if (GAPVPO > 0) {
                colorGAPVPO = 'green';
            }

            GAPVPO = parseFloat(GAPVPO);
            GAPVPO = GAPVPO.formatMoney(2, '.', ',');

            totalPorcCoberturaVPO += (parseFloat(arreglo[i]?.Porc_CoberturaVPO)) * 1;

            colorPorCober = Integralidadv2.colorPrcentajeCobertura((parseFloat(arreglo[i]?.Porc_CoberturaVPO)));
            row.append($('<td class="text-right" style="color:' + colorGAPVPO + '">').append(
                '<label class="color_IntV2 ' + colorPorCober + '">' + (parseFloat(arreglo[i]?.Porc_CoberturaVPO).toFixed(0)) + '%' + '</label>'
            ));


            var GAPVPT = arreglo[i].GAPVPT;
            var colorGAPVPT = 'black';
            if (GAPVPT < 0) {
                colorGAPVPT = 'red';
            } else if (GAPVPT > 0) {
                colorGAPVPT = 'green';
            }
            GAPVPT = parseFloat(GAPVPT);

            colorPorCober = Integralidadv2.colorPrcentajeCoberturaApp((parseFloat(arreglo[i].AplPotencialGeneralVende)));
            row.append($('<td class="text-right"  style="color:">').append(
                '<label class="color_IntV2 ' + colorPorCober + '">' + Math.floor(arreglo[i].AplPotencialGeneralVende) + '%' + '</label>'

            ));

            row.append($('<td class="text-right" >').append(
                '<label class=" ">' + Math.floor((arreglo[i].AplPotencialGeneralNoVende)) + '%' + '</label>'
            ));

            row.append($('<td class="text-center">').append(buttonVP));

            $('#tblIntegralidad > tbody').append(row);
            this.CreateTableDetail(arreglo[i]);
            $('#spinner_Cargando').css('display', 'none');

        }
        //datos para exel general
        //DatosConsultaV2 = arreglo;
        let totPorcVPT = 0;
        let totPorcVPO = 0;
        totalVPO = 0;
        totalVPT = 0;
        let VentaTot = 0;
        if (ListaTotalesMatriz.length > 0) {
             ventaTot = Number(ListaTotalesMatriz[0].Venta) || 0;
            totPorcVPT = Number(ListaTotalesMatriz[0].Porc_CoberturaVPT) || 0;
            totPorcVPO = Number(ListaTotalesMatriz[0].Porc_CoberturaVPO) || 0;
            totalVPT = Number(ListaTotalesMatriz[0].VPT) || 0;
            totalVPO = Number(ListaTotalesMatriz[0].VPO) || 0;
            $('#lbltotVenta').html('$' + ventaTot.formatMoney(2, '.', ','));
        } else {
            $('#lbltotVenta').html('$' + (0).formatMoney(2, '.', ','));
        }

        $('#lbl_VPT').html('$' + totalVPT.formatMoney(2, '.', ','));
        $('#lbl_VPO').html('$' + totalVPO.formatMoney(2, '.', ','));

        //filtro por cliente cache


        ///if (ListaTotalesBD.length > 0 && tipo == 1) {
        if (ListaTotalesBD.length > 0 ) {

         
            let totPorcIntApp = 0;
            let totPorcPotencialAapp = 0;
          
           

            ListaTotalesBD.forEach(item => {
                totPorcIntApp += item.PorcentajeAplicacion * 1;
                totPorcPotencialAapp += item.PorcentajeAplicacionPotencial * 1;
            });

            DatosTotalesExcel = [{
                Sucursal: arreglo[0].Sucursal
                , Venta: (isNaN(ListaTotalesMatriz[0].Venta) || !isFinite(ListaTotalesMatriz[0].Venta)) ? 0 : ListaTotalesMatriz[0].Venta.toFixed(2)
                , VPT: totalVPT, VPO: totalVPO
                , PorcVPT: (isNaN(totPorcVPT) || !isFinite(totPorcVPT)) ? 0 : (totPorcVPT)
                , PorcVPO: (isNaN(totPorcVPO) || !isFinite(totPorcVPO)) ? 0 : (totPorcVPO)
                //, PorcentajeAplicacion: (isNaN(totPorcIntApp) || !isFinite(totPorcIntApp)) ? 0 : (totPorcIntApp / totalRik)
                //, PorcentajeAplicacionPotencial: (isNaN(totPorcPotencialAapp) || !isFinite(totPorcPotencialAapp)) ? 0 : (totPorcPotencialAapp / totalRik)
                , PorcentajeAplicacion: (ListaTotalesMatriz[0].AplPotencialGeneralVende).toFixed(0)
                , PorcentajeAplicacionPotencial: (ListaTotalesMatriz[0].AplPotencialGeneralNoVende).toFixed(0),

            }];

            $('#lbl_PorcCoberturaVPT').html(ListaTotalesMatriz[0].Porc_CoberturaVPT + '%');
            $('#lbl_PorcCoberturaVPO').html(ListaTotalesMatriz[0].Porc_CoberturaVPO + '%');
            $('#lbl_PorcIntegralidadApp').html((ListaTotalesMatriz[0].AplPotencialGeneralVende).toFixed(0) + '%');
            $('#lbl_PorcPotencialIntegralidadApp').html((ListaTotalesMatriz[0].AplPotencialGeneralNoVende).toFixed(0) + '%');
            //$('#lbl_PorcCoberturaVPT').html((totalPorcCoberturaVPT / arreglo.length - 1).toFixed(0));
            //$('#lbl_PorcCoberturaVPO').html((totalPorcCoberturaVPO / arreglo.length - 1).toFixed(0));
        }

    },
    colorPrcentajeCobertura: function (valor) {
        var color = '';
        if (valor <= 60) {
            color = 'red';
        } else if (valor <= 80) {
            color = 'orange';
        } else {
            color = 'green';
        }
        return color;
    },
    colorPrcentajeCoberturaApp: function (valor) {
        var color = '';
        if (valor <= 50) {
            color = 'red';
        } else if (valor <= 70) {
            color = 'orange';
        } else {
            color = 'green';
        }
        return color;
    },
    CreateTableDetail: function (datos) {
        //var row = $('<tr  id="tr_detailinfo_' + datos.Id_Cte + '-' + datos.Id_Rik+ '" class="table_detailinfo">');
        var row = $('<tr  id="tr_detailinfo_' + datos.Id_Cte+ '" class="table_detailinfo">');
        row.append($('<td colspan="14" class="">').append(
            // '<div id="div_detail_' + datos.Id_Cte + '-' + datos.Id_Rik + '" class="info_detail">'
            '<div id="div_detail_' + datos.Id_Cte + '" class="info_detail">'
            + '</div>'
        ));


        $('#tblIntegralidad > tbody').append(row);

    },
    DesplegarEditarVPOMetav2: function (obj) {
        var id_rik = $(obj).data('id_rik');
        var reptesentante = $(obj).data('reptesentante');
        var id_cte = $(obj).data('id_cte');
        var cliente = $(obj).data('cliente');
        var id_ter = $(obj).data('id_ter');
        var id_seg = $(obj).data('id_seg');
        var segmento = $(obj).data('segmento');
        var cantidad = $(obj).data('cantidad');
        var seg_unidades = $(obj).data('seg_unidades');
        var seg_valunidim = $(obj).data('seg_valunidim');
        var VPOMeta = $(obj).data('dvpometa');
        var Anio = $(obj).data('anio');
        var Mes = $(obj).data('mes');
        var vpo = $(obj).data('vpo');


        $('#modalIntegralidadVPOMeta').appendTo("body").modal('show');
        $('#dr_repVPOMeta').text(reptesentante);
        $('#dr_clienteVPOMeta').text(id_cte + ' - ' + cliente);
        $('#dr_id_terVPOMeta').text(id_ter);
        $('#dr_segmentoVPOMeta').text(segmento);
        //$('#dr_val_stdVPOMeta').text(seg_valunidim);
        //$('#dr_cantidadVPOMeta').text(cantidad);
        //$('#dr_seg_unidadesVPOMeta').text(seg_unidades);
        $('#hf_Id_CteVPOMeta').val(id_cte);
        $('#hf_Id_RikVPOMeta').val(id_rik);
        $('#hf_Id_SegVPOMeta').val(id_seg);
        $('#txtPotencialMeta').val(vpo);
        $('#txtCantidad').val(cantidad);

        $('#txtAnio').val(Anio);
        $('#txtMes').val(Mes);

    },
    DesplegarEditarPotencial: function (obj) {
        var id_rik = $(obj).data('id_rik');
        var reptesentante = $(obj).data('reptesentante');
        var id_cte = $(obj).data('id_cte');
        var cliente = $(obj).data('cliente');
        var id_ter = $(obj).data('id_ter');
        var id_seg = $(obj).data('id_seg');
        var segmento = $(obj).data('segmento');
        var vpo = $(obj).data('vpo');
        var Anio = $(obj).data('anio');
        var Mes = $(obj).data('mes');
        var id_uen = $(obj).data('id_uen');
        var uen_desc = $(obj).data('uen_desc');
        var seg_valunidim = $(obj).data('seg_valunidim');
        var seg_unidades = $(obj).data('seg_unidades');
        var vpt = $(obj).data('vpt');
        const currentDate = new Date();
        const anioAct = currentDate.getFullYear();
        const mesAct = currentDate.getMonth() + 1;
        $('#txtPotencialMeta2').prop('disabled', true);
        $('#txtCantidad').prop('disabled', true);
        $('#btnProspectoEditarGuardarPotenciales').prop('disabled', true);
        if (Anio == anioAct && Mes == mesAct) {
            $('#txtPotencialMeta2').prop('disabled', false);
            $('#txtCantidad').prop('disabled', false);
            $('#btnProspectoEditarGuardarPotenciales').prop('disabled', false);
        }


        $('#modalIntegralidadPotencial').appendTo("body").modal('show');
        $('#dr_repVPOMeta').text(reptesentante);
        $('#dr_clienteVPOMeta').text(id_cte + ' - ' + cliente);
        $('#dr_id_terVPOMeta').text(id_ter);
        $('#dr_segmentoVPOMeta').text(segmento);
        $('#hf_Id_CteVPOMeta').val(id_cte);
        $('#hf_Id_RikVPOMeta').val(id_rik);
        $('#hf_Id_SegVPOMeta').val(id_seg);
        $('#txtPotencialMeta').val(VPOMeta);
        $('#txtSegmento').val(segmento);
        $('#txtUen').val(uen_desc);
        $('#txtIdSegmento').val(id_seg);
        $('#txtIdUen').val(id_uen);
        $('#txtDimension').val(seg_unidades);
        $('#txtPrecioUnidad').val(seg_valunidim);


        $('#txtAnio').val(Anio);
        $('#txtMes').val(Mes);

        var id_ter = $(obj).data('id_ter');
        var id_seg = $(obj).data('id_seg');
        var segmento = $(obj).data('segmento');
        var cantidad = $(obj).data('cantidad');
        var seg_unidades = $(obj).data('seg_unidades');
        var seg_valunidim = $(obj).data('seg_valunidim');
        var VPOMeta = $(obj).data('dvpometa');
        var Anio = $(obj).data('anio');
        var Mes = $(obj).data('mes');

        $('#txtVPM').val('$' + vpt);
        $('#txtCantidad').val(cantidad);
        $('#txtClienteV2').val(cliente);
        $('#dr_id_terVPOMeta').text(id_ter);

        $('#txtPotencialMeta2').val('$' + vpo);

        $('#txtAnio').val(Anio);
        $('#txtMes').val(Mes);
    },
    onChangeCantidadVPT: function () {

        var txtPrecioUnidad = $('#txtPrecioUnidad').val();
        var txtCantidad = $('#txtCantidad').val()

        var cantidad = txtCantidad;
        var precio = txtPrecioUnidad;
        var result = 0;
        if (txtPrecioUnidad != "" && txtCantidad != "") {
            result = parseFloat(cantidad) * parseFloat(precio);
        }

        $('#txtVPM').val('$' + result);
    },
    actualizarVPOMetav2: function () {
        var Id_Cte = $('#hf_Id_CteVPOMeta').val();
        var Id_Ter = $('#dr_id_terVPOMeta').text();
        var VPOMeta = $('#txtPotencialMeta2').val().replace('$', '');
        var Anio = $('#txtAnio').val();
        var Mes = $('#txtMes').val();
        var IdUen = $('#txtIdUen').val();
        var IdSegmento = $('#txtIdSegmento').val();
        var txtVPM = $('#txtVPM').val().replace('$', '');;
        var txtCantidad = $('#txtCantidad').val();

        $.ajax({
            url: _ApplicationUrl + '/api/ActualizaVPOIntegralidad/spActualizaVPOMeta' +
                '?Id_Cte=' + Id_Cte + '&Id_Ter=' + Id_Ter + '&VPOMeta=' + VPOMeta + "&Anio=" + Anio + "&Mes=" + Mes + "&IdUen=" + IdUen + "&IdSegmento=" + IdSegmento + "&VPT=" + txtVPM + "&Cantidad=" + txtCantidad,
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            //listado = response.Datos;

            if (Estado == 1) {
                alertify.success('Se actualizo correctamente');
                $('#modalIntegralidadPotencial').modal('hide');
                Integralidadv2.btnAplicarv2_Click();
            } else {
                alertify.error('Ocurrio un error: spCrmInt_Integralidad_ActualizaVPOMeta');
            }

        }).fail(function (jqXHR, textStatus, error) {
            //$('#spinner_Cargando').css('display', 'none');
            alertify.error('Error:  spCrmInt_Integralidad_ActualizaVPOMeta');
            console.log(jqXHR);
        });

    },
    createInfoDetail: function (Id_Cte,Id_Rik) {
        var htmlContent = `
            <div class="col-lg-12 mt-3">
                <div class="col-lg-10">
                    CLIENTE: <label id="lblClienteSelect"></label>
                </div>
                <div class="col-lg-2">
                       
                </div>
        </div>
        <div class="col-lg-12 mt-3">

                <div class="col-lg-4">
                    <h3 class="sub_title_integralidad">Total de Aplicaciones:&nbsp;<strong><span id="spTotalApp"></span></strong></h3>
                </div>

                <div class="col-lg-4">
                    <h3 class="sub_title_integralidad">% Integralidad  Aplicaciones:&nbsp;<strong><span id="spIntApp"></span></strong></h3>
                </div>
                <div class="col-lg-4">
                    <h3 class="sub_title_integralidad">% Potencial Integralidad  Aplicaciones:&nbsp;<strong><span id="spPotIntApp"></span></strong></h3>
                </div>
        </div>
        <div class="col-lg-12 mt-3">
                <div class="col-lg-4">
                    <h3 class="sub_title_integralidad">Venta por Categoría</h3>
                </div>

                <div class="col-lg-12">
                    <table id="tblVentaCategoria">
                        <thead>
                        <tr></tr>
                        <tr></tr>
                        <tr></tr>
                        </thead>
                        <tbody id="tbodyventacategoria">

                        </tbody>
                    </table>
                </div>
        </div>
        <div class="col-lg-12 mt-3">
                <div class="col-lg-6">
                    <h3>¿Cuántas vende?:&nbsp;<strong><span id="spAppSi"  class="mr-2"></span></strong> <button type="button"  onclick="Integralidadv2.showAppSi()" class="btn btn-info"><i class="fa fa-search" aria-hidden="true"></i></button></h3>
                        <table id="tblAppSi" class="table table-hover table-" style="display:none">
                            <thead>
                                <tr>
                                    <th class="text-center">Aplicación</th>
                                    <th class="text-center">Venta</th>
                                    <th class="text-center">$ Potencial Integralidad Teórico</th>
                                    <th class="text-center">% Potencial Integralidad Teórico</th>
                                    <th class="text-center">% Cobertura Integralidad</th>
                                    <tr>
                            </thead>
                            <tbody id="tbBodyAppSi">
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="3"><h3>% Integralidad Aplicaciones</h3></td>
                                    
                                    <td  class="text-center"> <strong><span id="spanTotalPorcentajePotencialSi" class="mr-2">0</span></strong> </td>
                                        <td  class="text-center"> <strong><span id="spanTotalPorcentajeCobertura" class="mr-2">0</span></strong> </td>
                                </tr>
                            </tfoot>
                        </table>
                </div>
                <div class="col-lg-6">
                        <h3>¿Cuántas no?:&nbsp;<strong><span id="spAppNo" class="mr-2"></span></strong> &nbsp;<button type="button"  onclick="Integralidadv2.showAppNo()" class="btn btn-info"><i class="fa fa-search" aria-hidden="true"></i></button></h3>
                        <table id="tblAppNo" class="table table-hover table-" style="display:none">
                            <thead>
                                <tr>
                                    <th class="text-center">Aplicación</th>
                                     <th class="text-center">CRM</th>
                                    <th class="text-center">% Potencial Integralidad Teórico</th>
                                    <th class="text-center">$ Potencial Integralidad Teórico</th>
                                    <tr>
                            </thead>
                            <tbody id="tbBodyAppNo">
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="2"><h3>% Potencial Integralidad Aplicaciones</h3></td>
                                    
                                    <td  class="text-center">
                                     <strong> <span id="spanTotalPorcentajePotencialNo" class="mr-2">0</span></strong>
                                    </td>
                                     <td>
                                     </td>
                                </tr>
                            </tfoot>
                        </table>
                </div>
        </div>
        `;
        //$('#div_detail_' + Id_Cte + '-' + Id_Rik).html(htmlContent);
        $('#div_detail_' + Id_Cte).html(htmlContent);
    },
    verApp: function (Id_Cte, clienteNombre, VentaTotalSelect,Id_Rik) {
       
        //var fila = document.getElementById("trcliente_" + Id_Cte + '-' + Id_Rik);
        //var tr_detailinfo = document.getElementById("tr_detailinfo_" + Id_Cte + '-' + Id_Rik);
        var fila = document.getElementById("trcliente_" + Id_Cte );
        var tr_detailinfo = document.getElementById("tr_detailinfo_" + Id_Cte);

        if (tr_detailinfo.style.display === "contents") {
            tr_detailinfo.style.display = 'none';
            return;
        }

        var filas = document.querySelectorAll(".cliente_list");
        var filastable_detailinfo = document.querySelectorAll(".table_detailinfo");
        $('.info_detail').empty();

        filas.forEach(function (fila) {
            fila.style.backgroundColor = "";
        });

        filastable_detailinfo.forEach(function (fila) {
            fila.style.display = 'none';
        });


        //var fila = document.getElementById("trcliente_" + Id_Cte);
        //var tr_detailinfo = document.getElementById("tr_detailinfo_" + Id_Cte);
        tr_detailinfo.style.display = 'contents';

        // Cambia el color de fondo de la fila
        fila.style.backgroundColor = "lightblue";

        //CREAR CONTENIDO DETALLE div_detail createInfoDetail
        this.createInfoDetail(Id_Cte, Id_Rik);
        //fin createInfoDetail

        Integralidadv2.showAppSi('none');
        Integralidadv2.showAppNo('none');
        const lista = Lista_Datos.filter(function (item) {
            return Id_Cte == item.Id_Cte; //&& item.Id_Rik==Id_Rik;
        });

        //datos para excel de aplicaciones
        //DatosAplicaciones = lista;

        document.getElementById('lblClienteSelect').innerHTML = clienteNombre;

        var tbBodyAppSi = document.getElementById('tbBodyAppSi');
        tbBodyAppSi.innerHTML = '';
        var tbBodyAppNo = document.getElementById('tbBodyAppNo');
        tbBodyAppNo.innerHTML = '';
        var filaHTMSI = '';
        var filaHTMNo = '';

        var ventasxApp = {};
        var ventasxTipo = {};

        const idRiksUnicos = new Set(lista.map(app => app.Id_Rik));
        var CountRik = idRiksUnicos.size;
     
        for (var i = 0; i < lista.length; i++) {
            let { Id_Op, Id_Apl, Venta, Apl_Descripcion, Id_Rik, Id_Area, Id_Sol, TipoProducto, Bracket, VPO, PorcentajeAplicacion, VPT } = lista[i];
            let bndventa = 0;
            if (Venta > 0) {
                bndventa = 1;
            }

            
            if (!ventasxApp[Apl_Descripcion]) {
                ventasxApp[Apl_Descripcion ] = {
                    Venta,
                    Id_Cte,
                    Id_Apl,
                    Apl_Descripcion,
                    TipoProducto: TipoProducto,
                    Id_Area, Id_Sol, Id_Rik, Bracket, VPO, PorcentajeAplicacion: (PorcentajeAplicacion > 0 ? (PorcentajeAplicacion) : 0), VPT
                    , PorcentajeAplicacionVende: (PorcentajeAplicacion > 0 && Venta > 0 ? (PorcentajeAplicacion)  : 0)
                    , PorcentajeAplicacionNoVende: (PorcentajeAplicacion > 0 && Venta <= 0 ? (PorcentajeAplicacion)  : 0)
                    //, PorcentajeAplicacionVende: (PorcentajeAplicacion > 0 && Venta > 0 ? Math.floor(PorcentajeAplicacion)  : 0)
                    //, PorcentajeAplicacionNoVende: (PorcentajeAplicacion > 0 && Venta <= 0 ? Math.floor(PorcentajeAplicacion) : 0)

                };
            } else {
                ventasxApp[Apl_Descripcion].Venta += Venta;
                //ventasxApp[Apl_Descripcion].PorcentajeAplicacion += (PorcentajeAplicacion > 0 ? (PorcentajeAplicacion)  : 0);
                //ventasxApp[Apl_Descripcion ].PorcentajeAplicacionVende +=(PorcentajeAplicacion > 0 && Venta > 0 ? (PorcentajeAplicacion)  : 0);
                //ventasxApp[Apl_Descripcion].PorcentajeAplicacionNoVende += (PorcentajeAplicacion > 0 && Venta <= 0 ? (PorcentajeAplicacion) : 0);
                //ventasxApp[Apl_Descripcion].PorcentajeAplicacionVende += (PorcentajeAplicacion > 0 && Venta > 0 ? Math.floor(PorcentajeAplicacion)  : 0);
                //ventasxApp[Apl_Descripcion].PorcentajeAplicacionNoVende += (PorcentajeAplicacion > 0 && Venta <= 0 ? Math.floor(PorcentajeAplicacion)  : 0);
                if (ventasxApp[Apl_Descripcion].PorcentajeAplicacion <= 0) {
                    ventasxApp[Apl_Descripcion].PorcentajeAplicacion = (PorcentajeAplicacion > 0 ? PorcentajeAplicacion : 0);
                }
                if (ventasxApp[Apl_Descripcion].PorcentajeAplicacionVende <= 0) {
                    ventasxApp[Apl_Descripcion].PorcentajeAplicacionVende = (PorcentajeAplicacion > 0 && Venta > 0 ? (PorcentajeAplicacion) : 0);
                }

                if (ventasxApp[Apl_Descripcion].PorcentajeAplicacionNoVende <= 0) {
                    ventasxApp[Apl_Descripcion].PorcentajeAplicacionNoVende = (PorcentajeAplicacion > 0 && Venta <= 0 ? (PorcentajeAplicacion) : 0);
                }

                if (ventasxApp[Apl_Descripcion].VPO <= 0) {
                    ventasxApp[Apl_Descripcion].VPO = VPO;
                }
                if (ventasxApp[Apl_Descripcion].VPT <= 0) {
                    ventasxApp[Apl_Descripcion].VPT = VPT;
                }
            }

     
            if (Venta != 0) {
               
                if (!ventasxTipo[TipoProducto]) {
                    ventasxTipo[TipoProducto] = {
                        Venta,
                        Id_Cte,
                        Id_Apl,
                        Apl_Descripcion,
                        TipoProducto: TipoProducto,
                        Id_Area, Id_Sol, Id_Rik, Bracket, VPO, PorcentajeAplicacion, VPT, PorcentajeVenta:( (Venta / VentaTotalSelect) * 100) 

                    };
                } else {
                    ventasxTipo[TipoProducto].Venta += Venta;
                    ventasxTipo[TipoProducto].PorcentajeVenta += ((Venta / VentaTotalSelect) * 100);
                    if (ventasxTipo[TipoProducto].VPT <= 0) {
                        ventasxTipo[TipoProducto].VPT = VPT;
                    } if (ventasxTipo[TipoProducto].VPO <= 0) {
                        ventasxTipo[TipoProducto].VPO = VPO;
                    }
                }
            }


        }

      

        let ventasNegativas = 0;
        let ventasPositivas = 0;
        var aplicacionesAgrupadas = Object.values(ventasxApp);
        aplicacionesAgrupadas.sort((a, b) => {
            if (a.TipoProducto === "QUIMICOS" && b.TipoProducto !== "QUIMICOS") {
                return -1;
            } else if (a.TipoProducto !== "QUIMICOS" && b.TipoProducto === "QUIMICOS") {
                return 1;
            } else {
                return 0;
            }
        });

        let TotalPorcentajeAPP = 0;
        let TotalPorcentajePotencialAPP = 0;
        let TotalPorcentajeCoberturaAPP = 0;
        
        for (var i = 0; i < aplicacionesAgrupadas.length; i++) {
            let { Id_Apl, Venta, Apl_Descripcion, TipoProducto, Id_Area, Id_Sol, Id_Cte, Id_Rik, PorcentajeAplicacion, VPT, PorcentajeAplicacionVende, PorcentajeAplicacionNoVende } = aplicacionesAgrupadas[i];
            let appsConEsaDescripcionV = lista.filter(app => app.Apl_Descripcion === Apl_Descripcion && app.Venta>0);
            let appsConEsaDescripcionNV = lista.filter(app => app.Apl_Descripcion === Apl_Descripcion && app.Venta <= 0);

            let riksUnicos = new Set(appsConEsaDescripcionV.map(app => app.Id_Rik));
            let CountRik2 = riksUnicos.size;
            let riksUnicos2 = new Set(appsConEsaDescripcionNV.map(app => app.Id_Rik));
            let CountRik3 = riksUnicos2.size;

            PorcentajeAplicacionVende = PorcentajeAplicacionVende / CountRik2;
            PorcentajeAplicacionNoVende = PorcentajeAplicacionNoVende / CountRik3;
          
            Venta = parseFloat(Venta.toFixed(2));
            let VentaFormat = Venta.formatMoney(2, '.', ',');
      
       
            if (Venta <= 0) {
                let PotencialIntegralidad = parseFloat(((PorcentajeAplicacionNoVende / 100) * VPT).toFixed(0));
                let PotencialIntegralidadFormat = PotencialIntegralidad.formatMoney(2, '.', ',');
                let openCrm = '';
                TotalPorcentajePotencialAPP += PorcentajeAplicacionNoVende;
                if (Id_Apl >= 0) {
                    openCrm = '<a tooltip="Nuevo proyecto" target="_blank" href="ProspectosV2.aspx?FlagIntegralidad=1&Id_Cte=' + Id_Cte + '&Id_Rik=' + Id_Rik + '&Id_Op=' + 0 + '&Id_Area=' + Id_Area + '&Id_Sol=' + Id_Sol + '">' +
                        '<i class="fa fa-file-o" aria-hidden="true"></i>' +
                        '</a > ';
                }
                if (Apl_Descripcion != "") {
                    ventasNegativas++;
                }
                filaHTMNo += '<tr>' +
                    /* '<td>' + TipoProducto + '</td>' +*/
                    '<td>' + Apl_Descripcion + '</td>' +
                    '<td>' + openCrm + '</td>' +
                    '<td class="text-center">' + ((PorcentajeAplicacionNoVende).toFixed(2)) + '%</td>' +
                    '<td class="text-center">$' + PotencialIntegralidadFormat + '</td>' +
                    '</tr>';
            } else {
                let PotencialIntegralidad = parseFloat(((PorcentajeAplicacionVende / 100) * VPT).toFixed(0));
                let PotencialIntegralidadFormat = PotencialIntegralidad.formatMoney(2, '.', ',');

                let PorcentajeCobertura = PotencialIntegralidad > 0 ? ((Venta / PotencialIntegralidad) * PorcentajeAplicacionVende) : 0;
                TotalPorcentajeCoberturaAPP += (parseFloat(PorcentajeCobertura.toFixed(0)));
                if (Apl_Descripcion != "") {
                    ventasPositivas++;
                }
                TotalPorcentajeAPP += (parseFloat((PorcentajeAplicacionVende).toFixed(2)));
              
                filaHTMSI += '<tr>' +
                    /* '<td>' + TipoProducto + '</td>' +*/
                    '<td>' + Apl_Descripcion + '</td>' +
                    '<td>$' + VentaFormat + '</td>' +
                    '<td class="text-center">$' + PotencialIntegralidadFormat + '</td>' +
                    '<td class="text-center">' + (PorcentajeAplicacionVende).toFixed(2) + '%</td>' +
                    '<td class="text-center">' + (PorcentajeCobertura).toFixed(2) + ' %</td>' +
                    '</tr>';
            }


        }

        TotalPorcentajePotencialAPP = Math.round(TotalPorcentajePotencialAPP * 100) / 100;

        document.getElementById('spTotalApp').innerHTML = ventasNegativas + ventasPositivas;
        document.getElementById('spAppSi').innerHTML = ventasPositivas;
        document.getElementById('spAppNo').innerHTML = ventasNegativas;

        document.getElementById('spIntApp').innerHTML = (TotalPorcentajeAPP.toFixed(0)) + '%';
        document.getElementById('spPotIntApp').innerHTML = (TotalPorcentajePotencialAPP.toFixed(0)) + '%';

        document.getElementById('spanTotalPorcentajePotencialSi').innerHTML = (TotalPorcentajeAPP.toFixed(0)).toString() + '%';
        document.getElementById('spanTotalPorcentajeCobertura').innerHTML = (TotalPorcentajeCoberturaAPP.toFixed(0)).toString() + '%';

        document.getElementById('spanTotalPorcentajePotencialNo').innerHTML = (TotalPorcentajePotencialAPP.toFixed(0)).toString() + '%';


        tbBodyAppSi.innerHTML = filaHTMSI;
        tbBodyAppNo.innerHTML = filaHTMNo;

        var ventarxTipoAgrupadas = Object.values(ventasxTipo);
        var filaTipoVenta = "";
        for (var i = 0; i < ventarxTipoAgrupadas.length; i++) {
            let { Venta, TipoProducto, PorcentajeVenta } = ventarxTipoAgrupadas[i];

            Venta = parseFloat(Venta);
            let VentaFormat = Venta.formatMoney(2, '.', ',');

            if (TipoProducto?.trim() != "NULL") {
                filaTipoVenta += '<tr>' +
                    /* '<td>' + TipoProducto + '</td>' +*/
                    '<td style="font-size=14px">' + TipoProducto + '</td>' +
                    '<td style="width: 95px;" ></td>' +
                    '<td style="font-size=14px"><strong>' + (PorcentajeVenta.toFixed(0)) + '%<strong></td>' +
                    '</tr>';
            }

        }
        var tbodyventacategoria = document.getElementById('tbodyventacategoria');
        tbodyventacategoria.innerHTML = '';
        tbodyventacategoria.innerHTML = filaTipoVenta;
    },

    showAppSi: function (display = 'block') {
        if (document.getElementById('tblAppSi')) {
            document.getElementById('tblAppSi').style.display = display;
        }
    },
    showAppNo: function (display = 'block') {
        if (document.getElementById('tblAppNo')) {
            document.getElementById('tblAppNo').style.display = display;
        }
    },
    redondeoPersonalizado: function (num) {
        const decimal = num % 1;
        if (decimal > 0.5) {
            return Math.ceil(num);
        } else {
            return num;
        }
    },
    redondeoPersonalizado2: function (num) {
        const decimal = num % 1;
        if (decimal > 0.5) {
            return Math.ceil(num);
        } else {
            return Math.floor(num);
        }
    },
    GenerarListaClientes: function (result) {
        var objClienteDet = {};
        for (var i = 0; i < result.length; i++) {

            if (!objClienteDet[result[i].Id_Cte]) {
                objClienteDet[result[i].Id_Cte] = {
                    aplicaciones: [],
                    Id_Cte: result[i].Id_Cte,
                    Id_Op: result[i].Id_Op,
                    Cliente: result[i].Cliente,
                };
            }

            objClienteDet[result[i].Id_Cte].aplicaciones.push(result[i]);
        }

        var ddlCtes = $('#ddlRazonSocialCtes').empty();
        ddlCtes.append(
            $('<option data-Id_Cte=0>').val(0).text('-- Todos --')
        );
        let datoclientes = Object.values(objClienteDet);
        for (var i = 0; i < datoclientes.length; i++) {
            ddlCtes.append(
                $('<option data-Id_Cte=' + datoclientes[i].Id_Cte + ' data-Id_Cte=' + datoclientes[i].Id_Cte + ' >').val(datoclientes[i].Id_Cte).text(datoclientes[i].Id_Cte + " - " + datoclientes[i].Cliente)
            );
        }

    },
    BuscaDatosCache: function () {
        var IdCliente = $('#ddlRazonSocialCtes').val();
        var IdSeg = $('#ddlFiltroSegmentos').val();
        var ddlFiltroUen = $('#ddlFiltroUen').val();


        let filterData = DatosConstulaApi;
        if (IdCliente > 0) {
            filterData = filterData.filter(item => item.Id_Cte == IdCliente);
        }
        if (IdSeg > 0) {
            filterData = filterData.filter(item => item.Id_Seg == IdSeg);
        }
        if (ddlFiltroUen > 0) {
            filterData = filterData.filter(item => item.Id_Uen == ddlFiltroUen);
        }

        var tipoR = 1;
        if (IdCliente > 0) {
            tipoR = 2;
        }


        let Rik = $('#ddlRepresentanteV2').val();
      
        let periodo = $('#tbPeriodoInicio').val();
        let partPeriodo = periodo.split(" ");
        if (!Rik) {
            Rik = 0;
        }

        let anio = partPeriodo[1];
        let mes = partPeriodo[0];

        Integralidadv2.ConsultarV2(Rik, IdSeg, IdCliente, mes, anio, ddlFiltroUen, 6, function (Lst) {
            TotalDatoTipo = Lst;
        }, function () {
        });

        Integralidadv2.ConsultarV2(Rik, IdSeg, IdCliente, mes, anio, ddlFiltroUen, 7, function (Lst1) {
            ListaTotalesMatriz = Lst1;
            Integralidadv2.ConsultarV2(Rik, IdSeg, IdCliente, mes, anio, ddlFiltroUen, 3, function (Lst) {
                ListaTotalesBD = Lst;
                Integralidadv2.Desplegarv2(filterData, tipoR);
            }, function () {
            });

        }, function () {
        });

  

    },

    btnBajarReporteExcelV2_Click: function () {
        let Rik = $('#ddlRepresentanteV2').val();
        let lbRik = $('#ddlRepresentanteV2 option:selected').text();
        let Seg = $('#ddlFiltroSegmentos').val();
        let lbSeg = $('#ddlFiltroSegmentos option:selected').text();
        let Ctes = $('#ddlRazonSocialCtes').val();
        let lbCtes = $('#ddlRazonSocialCtes option:selected').text();
        let ddlFiltroUen = $('#ddlFiltroUen option:selected').text();
        let lblCd = document.getElementById('lblCd');
        let CD = lblCd.textContent || lblCd.innerText;
     
        if (typeof (DatosConsultaV2) == 'undefined' || DatosConsultaV2?.length <= 0) {
            alertify.error('Debe realizar la consulta.');
        } else {

      
            var ventasxTipo = {};

            let VentaTotalSelect = Lista_Datos.reduce((sum, item) => {
                return sum + (item.Venta || 0);
                return sum;
            }, 0);


            let listaTipoProductos = [];
            let listaTipoProductosH = [];
            if (TotalDatoTipo?.length>0) {
                 listaTipoProductos = [
                    ...new Set(
                        TotalDatoTipo
                            .map(item => item.TipoProducto.trim())  // Extrae el TipoProducto
                            .filter(tipo => tipo?.trim() !== null && tipo.toLowerCase().trim() !== 'null' && tipo !== undefined)  // Filtra null, undefined y cadena vacía
                    )
                ];

                 listaTipoProductosH = [
                    ...new Set(
                        TotalDatoTipo
                            .map(item => item.TipoProducto.trim())
                            .filter(tipo => tipo?.trim() !== null && tipo.toLowerCase().trim() !== 'null' && tipo !== undefined)  // Filtra null, undefined y cadena vacía
                    )
                ].map(tipo => [`% Venta ${tipo}`, `$ Venta ${tipo}`]).flat();
            }

            let periodo = $('#tbPeriodoInicio').val();
            let partPeriodo = periodo.split(" ");


            let anio = partPeriodo[1];
            let mes = partPeriodo[0];

            let PeriodoMes = Integralidadv2.getPeriodo(mes, anio);
            IntegralidadXls.DescararV2(Rik, lbRik, Seg, lbSeg, Ctes, lbCtes, ListaTotalesBD, CD, DatosTotalesExcel, listaTipoProductos, TotalDatoTipo, ddlFiltroUen, listaTipoProductosH, PeriodoMes);
        }
    },getPeriodo: function (numeroMes, anio) {
        const nombresMeses = [
            "enero", "febrero", "marzo", "abril", "mayo", "junio",
            "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"
        ];

        let nombreMes = nombresMeses[numeroMes - 1];
        nombreMes = nombreMes.charAt(0).toUpperCase() + nombreMes.slice(1);

        return `${nombreMes} ${anio}`;
    },
    onClickDescargaExcelAplicacionesDetalle: function () {
        let Rik = $('#ddlRepresentanteV2').val();
        let lbRik = $('#ddlRepresentanteV2 option:selected').text();
        let Seg = $('#ddlFiltroSegmentos').val();
        let lbSeg = $('#ddlFiltroSegmentos option:selected').text();
        let lblCd = document.getElementById('lblCd');
        let CD = lblCd.textContent || lblCd.innerText;
        let ddlFiltroUen = $('#ddlFiltroUen option:selected').text();


        if (typeof (Lista_Datos) == 'undefined' || Lista_Datos?.length <= 0) {
            alertify.error('No hay aplicaciones.');
        } else {


            const agrupados = DatosConsultaDetalleHoja1V2.reduce((acc, { Uen_Desc, Seg_Descripcion, Id_Cte, Cliente, Bracket, Rik, Id_Rik, Venta, VPO, VPT, Porc_CoberturaVPT, Porc_CoberturaVPO, PorcentajeAplicacionAppCliente, TotPorcentajeAplicacionPotencialCliente }) => {
                const rik = Rik.toLowerCase();
                var ident = Id_Cte + '-' + Id_Rik;
                if (acc[ident]) {
                    acc[ident].Venta += Venta;
                    acc[ident].VPO += VPO;
                    acc[ident].VPT += VPT;
                    acc[ident].Porc_CoberturaVPT += Porc_CoberturaVPT;
                    acc[ident].Porc_CoberturaVPO += Porc_CoberturaVPO;
                    acc[ident].PorcentajeAplicacionAppCliente += PorcentajeAplicacionAppCliente;
                    acc[ident].TotPorcentajeAplicacionPotencialCliente += TotPorcentajeAplicacionPotencialCliente;
                } else {
                    acc[ident] = { Uen_Desc, Seg_Descripcion, Id_Cte, Cliente, Bracket, Rik, Id_Rik, Venta, VPO, VPT, Porc_CoberturaVPT, Porc_CoberturaVPO, PorcentajeAplicacionAppCliente, TotPorcentajeAplicacionPotencialCliente }; // Si no existe, crea una nueva entrada
                }
                return acc;
            }, {});

            let resultado = Object.values(agrupados);


            var ventasxTipo = {};
            for (var i = 0; i < Lista_Datos.length; i++) {
                let { Id_Cte, Id_Apl, Venta, Apl_Descripcion, Id_Rik, Id_Area, Id_Sol, TipoProducto, Bracket, VPO, PorcentajeAplicacion, VPT } = Lista_Datos[i];

                let VentaTotalSelect = Lista_Datos.reduce((sum, item) => {
                    if (item.Id_Cte === Id_Cte) {
                        return sum + (item.Venta || 0);
                    }
                    return sum;
                }, 0);

                var id = TipoProducto + Id_Cte;


                if (Venta != 0) {
                    if (!ventasxTipo[id]) {
                        ventasxTipo[id] = {
                            Venta,
                            Id_Cte,
                            Id_Rik,
                            TipoProducto: TipoProducto.trim(),
                            PorcentajeVenta:  ((Venta / VentaTotalSelect) * 100)

                        };
                    } else {
                        ventasxTipo[id].Venta += Venta;
                        ventasxTipo[id].PorcentajeVenta +=  ((Venta / VentaTotalSelect) * 100);
                    }
                }
            }
            let agrupadosTipoProductoResult = Object.values(ventasxTipo);

            const listaTipoProductosH = [
                ...new Set(
                    Lista_Datos
                        .map(item => item.TipoProducto.trim())
                        ///.filter(tipo => tipo?.trim() !== null && tipo.toLowerCase().trim() !== 'null' && tipo !== undefined && tipo?.trim() !== '')
                        .filter(tipo => tipo?.trim() !== null && tipo.toLowerCase().trim() !== 'null' && tipo !== undefined)
                )
            ].map(tipo => [`% Venta ${tipo}`, `$ Venta ${tipo}`]).flat();


            resultado = resultado.sort((a, b) => a.Id_Cte - b.Id_Cte);
            DatosAplicaciones = DatosAplicaciones.sort((a, b) => a.Id_Cte - b.Id_Cte);

            let periodo = $('#tbPeriodoInicio').val();
            let partPeriodo = periodo.split(" ");


            let anio = partPeriodo[1];
            let mes = partPeriodo[0];

            let PeirodoMes = Integralidadv2.getPeriodo(mes, anio);
            IntegralidadXls.DescargarAplicacionesV2(Rik, lbRik, Seg, lbSeg, resultado, CD, listaTipoProductosH, agrupadosTipoProductoResult, DatosAplicaciones, ddlFiltroUen, PeirodoMes);
        }
    }

}
$(document).ready(function () {
    // Cargar Combo REPRESENTANTES
    Cargar_ZonaSucursal();
    Integralidadv2.CargarComboRepre();
    Integralidadv2.CargarUenSegmento(0, 1, 0);
    $('#trbtndownload').css('display', 'none');
    $(document).on('change', 'select[id="ddlRazonSocialCtes"]', function () {
        $('#spinner_Cargando').css('display', 'block');
        Integralidadv2.BuscaDatosCache();
    });

    $(document).on('change', 'select[id="ddlFiltroSegmentos"]', function () {
        //Integralidadv2.BuscaDatosCache();
    });

    $(document).on('change', 'select[id="ddlFiltroUen"]', function () {
        $('#spinner_Cargando').css('display', 'block');
        var ddlRep = $('#ddlFiltroUen').val();

        Integralidadv2.CargarUenSegmento(0, 2, ddlRep);
    });

});