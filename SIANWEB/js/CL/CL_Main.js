/*
  MAR4-2020 Actualizacion RFH
*/

var Producto = {

    DesplegarInfo: function (Id_Prd, CALLBACK_Exito, CALLBACK_Error) {
        CL.LIMPIAR_CONTROLES(function () {
            Producto_Consultar_Ajax(Id_Prd, function (Rec) {
                Cargar_cbmUnindadMedidaSAT(0, function () {
                    Cargar_cmbSATProductosServicios(0, function () {
                        Producto_Desplegar_Informacion(Rec, function () {
                            ajax_LlenarProdcutosHermanos(Id_Prd, function (Datos) {
                                //CALLBACK_Exito
                                $('#txtProductosMismoPadre').val(Datos);
                                LlenarListaPrecios(Id_Prd, function () {
                                    if (CALLBACK_Exito) {
                                        CALLBACK_Exito();
                                    }
                                    $('#modalBuscarProducto').modal('hide');
                                });
                            }, function () {
                                if (CALLBACK_Error) {
                                    CALLBACK_Error();
                                }
                            });
                        });
                    });
                });
            });
        });
    },

    btnSeleccion: function (obj) {
        $('#SPINNER_CL').css('display', 'block');
        $('#spinner_Buscar').css('display', 'block');
        var Id_Prd = $(obj).data('id_prd');
        Producto.DesplegarInfo(Id_Prd, function () {
            // CALLBACK_Exito
            $('#SPINNER_CL').css('display', 'none');
            $('#spinner_Buscar').css('display', 'none');
        }, function () {
            // CALLBACK_Error
            $('#SPINNER_CL').css('display', 'none');
            $('#spinner_Buscar').css('display', 'none');
        });
    }
}

var CL = {

    LIMPIAR_CONTROLES: function (CALLBACK_Exito) {
        $('#txtFactorConversion').prop('disabled', false);
        $('#lblTituloProducto').text('');
        $('#txtSearch').val('');
        $('#txtCodigoUsadoProd').val('');
        $('#TextId_Prd').val('');
        $('#txtCodProd').val('');
        $('#TextPrd_Descrpcion').val('');
        $('#txtPresentacion').val('');
        $('#txtTipoProducto').val('');
        $('#txtTipoProducto').prop('disabled', true);
        $('#txtTipoProducto').val('');
        $('#cmbTipoProducto').val(0);
        $('#cmbFam').val(0);
        $('#cmbSubFam').val(0);
        $('#txtProveedorCentral').val('');
        $('#txtProveedor').val('');
        $('#cmbProveedor').val(-1);
        $('#cmbUentrada').val(0);
        $('#txtFactorConversion').val('');
        $('#cmbUsalida').val(0);
        $('#txtUempaque').val('');
        $('#cmbCausaDesabasto').val(0);
        $('#tblPedidoDesabastecido > tbody').empty();
        $('#hfCont_PedidoDesabastecido').val(0);
        $('#tbl_rgPrecios > tbody').empty();
        $('#cmbUnidadMedidaSATDesabasto').val('');
        $('#ddlProdServicio_SATDesabasto').val('');
        $('#divPedidosRefAbasto').css('dispaly', 'none');
        $('#cmbAplicacionSoli').val(0);
        $('#cmdSubFamiliaSoli').val(0);
        $('#txtMotivoSolicita').val('');
        $('#Id_Cpr').val('');
        $('#btnSeleccionaProdSol').removeClass('btn-default');
        $('#btnSeleccionaProdSol').addClass('btn-primary');
        $('#btnProducto_AvastoLocal_Ok').removeClass('btn-default');
        $('#btnProducto_AvastoLocal_Ok').addClass('btn-primary');
        $('#tbl_ClientesExclusivos > tbody').empty();

        //Campos nuevos
        $('#cmbPresentacion').val(0);
        //$('#rdpVigencia').val('');
        //$('#rdpVigenciaFin').val('');
        $('#cmbPresentacionProv').val(0);
        $('#txtDesProveedor').val('');
        $('#txtCodProveedor').val('');


        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    },

    Inicia_Formulario_Desabasto: function (CALLBACK_Exito) {
        $('#hfId_Prd').val('');
        $('#hfId_PrdGenerado').val('');
        $('#hfPrd_Descripcion').val('');
        $('#ddlProdServicio_SATDesabasto').val("0");
        $('#txtSearch').val('');
        $('#txtCodigoUsadoProd').val('');
        $('#TextId_Prd').val('');
        $('#txtCodProd').val('');
        $('#TextPrd_Descrpcion').val('');
        $('#TextPrd_Descrpcion').val('');
        $('#txtPresentacion').val('');
        $('#txtTipoProducto').val('');
        $('#txtFactorConversion').val('');
        $('#txtUempaque').val('');
        //$('#tbModificaTipoPrecio_FechaIni').val();
        //$('#tbModificaTipoPrecio_FechaFin').val();
        //$('.datepicker_sololunes').Zebra_DatePicker({
        //    format: 'd/m/Y',
        //    direction: true,
        //    disabled_dates: ['* * * 0,2,3,4,5,6'],
        //    onSelect: function () {
        //        $(this).change();
        //    }
        //});
        $('#txtMotivoSolicita').val('');
        $('#tbl_rgPrecios > tbody').empty();

        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    },

    Deshabilita_Op1_DatosGenerales: function (CALLBACK_Exito) {
        $('#TextId_Prd').prop('disabled', true);
        $('#txtCodProd').prop('disabled', true);
        $('#TextPrd_Descrpcion').prop('disabled', true);
        $('#txtPresentacion').prop('disabled', true);
        $('#txtTipoProducto').prop('disabled', true);
        $('#cmbTipoProducto').prop('disabled', true);
        $('#cmbFam').prop('disabled', true);
        $('#cmbSubFam').prop('disabled', true);
        $('#cmbUentrada').prop('disabled', true);
        $('#cmbUsalida').prop('disabled', true);
        $('#txtFactorConversion').prop('disabled', true);
        $('#txtUempaque').prop('disabled', true);
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    },

    // OPCION 1 - Activacion de Codigo por Desabasto    
    GUARDAR_ActivacionCodigoDesabasto: function () {
        $('#SPINNER_Guardar').css('display', 'block');
        $('#btnGuardarCompraLocal').prop('disabled', true);
        //Motivo para la Compra Local
        let cmbMotivo = $('#cmbCategorias').val();
        // VALIDACIONES    
        let txtCodigoUsadoProd = $('#txtCodigoUsadoProd').val();
        let TextPrd_Descrpcion = $('#TextPrd_Descrpcion').val();
        let TextId_Prd = $('#TextId_Prd').val();
        let txtCodProd = $('#txtCodProd').val();
        if (TextId_Prd == '') {
            alertify.error("Debe seleccionar el producto.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        let cmbProveedor = $('#cmbProveedor').val();
        let ProviderName = $('#cmbProveedor option:selected').text();
        if (cmbProveedor == '-1' || ProviderName == "") {
            alertify.error("Debe seleccionar el proveedor local.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let txtCodProveedor = $('#txtCodProveedor').val();
        if (txtCodProveedor == '') {
            alertify.error("Debe proporcionar un código de producto de proveedor.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let txtDesProveedor = $('#txtDesProveedor').val();
        if (txtDesProveedor == '') {
            alertify.error("Debe proporcionar una descripción de producto de proveedor.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let cmbPresentacionProv = $('#cmbPresentacionProv option:selected').text();
        if (cmbPresentacionProv == '') {
            alertify.error("Debe seleccionar una presentación de producto de proveedor.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let IdCausaDesabasto = $("#cmbCausaDesabasto").val();
        var CausaDesabastoName = $('#cmbCausaDesabasto option:selected').text();
        IdCausaDesabasto = parseInt(IdCausaDesabasto);
        if (IdCausaDesabasto == 0) {
            alertify.error("Debe seleccionar una causa de desabasto.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }


        //Cargar los tipos de presentacion 
        // RBM Noviembre 2023
        let PresentacionName = $('#txtPresentacion').val();
        //if (cmbPresentacion == '-1') {
        //    alertify.error("Debe seleccionar la presentación.");
        //    $('#SPINNER_Guardar').css('display', 'none');
        //    $('#btnGuardarCompraLocal').prop('disabled', false);
        //    return;
        //}

        // IdCausaDesabasto NO APLICA
        //let IdCausaDesabasto = 0;
        //let CausaDesabastoName = $('#cmbCausaDesabasto option:selected').text();
        //if (cmbCausaDesabasto == '0') {
        //    alertify.error("Debe especificar la causa del desabasto.");
        //    return;
        //}

        //ajax_ChecarProductoYaSolicitado(txtCodProd, $('#SPINNER_CL'), function (Datos) {
        //    if (Datos > 0) {
        //        alertify.error("Ya se encuentra una solicitud pendiente de autorizar para ese producto (" + txtCodigoUsadoProd + ").");
        //        $('#SPINNER_Guardar').css('display', 'none');
        //        $('#btnGuardarCompraLocal').prop('disabled', false);
        //        return;
        //    }
        //});
        var Det_Costo = 0;
        var Pre_Pesos = 0;
        // Pedido
        let Costo = 0;
        for (i = 0; i < 5; i++) {
            let Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            if (Pre_Descripcion == 'Precio AAA código compra local') {
                Pre_Pesos = $('#Pre_Pesos_' + i).text();
                Pre_Pesos = parseFloat(Pre_Pesos);
                if (isNaN(Pre_Pesos)) {
                    Pre_Pesos = 0;
                }
                Costo = Pre_Pesos;
            }
        }
        if (Costo <= 0) {
            alertify.error("El precio AAA código compra local no puede ser CERO.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        } else {
            Det_Costo = Pre_Pesos;
        }
        // VALIDACINOES SAT
        let cmbUnidadMedidaSATDesabasto = $('#cmbUnidadMedidaSATDesabasto option:selected').text();// $('#cmbUnidadMedidaSATDesabasto').val();
        let cmbProdServicioSATDesabasto = $('#ddlProdServicio_SATDesabasto option:selected').text();//$('#ddlProdServicio_SATDesabasto').val();

        if (cmbUnidadMedidaSATDesabasto == "0" || cmbProdServicioSATDesabasto == "0") {
            alertify.error("Falta alguno de los datos en las opciones SAT.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        var P = {}
        P = Entidad_Producto;
        // Verificar Precios
        P._Id_Prd = txtCodProd; //  TextId_Prd;  // $('#txtCodigoUsadoProd').val();
        P._Id_Spo = 0; //  TextId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Spo.Text);        
        // IdTipoProducto
        P._Id_Ptp = $('#cmbTipoProducto').val();
        var TipoProducto = $('#cmbTipoProducto').val() + " " + $('#cmbTipoProducto option:selected').text();
        // txtTipoProducto.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProducto.Text);
        var Id_Cpr = $('#Id_Cpr').val();
        if (Id_Cpr == '' || Id_Cpr == '0') {
            Id_Cpr = 1;
        }
        P._Id_Cpr = Id_Cpr;  // txtCategoria.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoria.Text);
        P._Id_Fam = $('#cmbFam').val();  // txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
        var FamName = $('#cmbFam option:selected').text();
        P._Id_Sub = $('#cmbSubFam').val();   //txtSubFam.Value.HasValue ? Convert.ToInt32(txtSubFam.Text) : 0;
        var SubFamName = $('#cmbSubFam option:selected').text();
        P._Id_Pvd = $('#txtProveedorCentral').val(); //txtProveedor.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedor.Text);
        P._Prd_Descripcion = $('#TextPrd_Descrpcion').val(); //TextPrd_Descrpcion.Text;
        P._Prd_Presentacion = PresentacionName;  // txtPresentacion.Text;
        P._Prd_InvInicial = 0; //txtInicial.Text == string.Empty ? 0 : Convert.ToInt32(txtInicial.Text);
        P._Prd_InvFinal = 0; //txtFinal.Text == string.Empty ? 0 : Convert.ToInt32(txtFinal.Text);
        P._Prd_AgrupadoSpo = 0; // txtAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(txtAgrupadoSpo.Text);
        P._Prd_FactorConv = $('#txtFactorConversion').val(); // txtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversion.Text);
        P._Prd_AparatoSisProp = false;  // chkSistProP._Checked;
        P._Prd_Fisico = 0; // txtFisico.Text == string.Empty ? 0 : Convert.ToInt32(txtFisico.Text);
        P._Prd_Ordenado = 0; // = txtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenado.Text);
        P._Prd_Asignado = 0; //  txtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignado.Text);
        P._Prd_InvSeg = 0; // txtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridad.Text);
        P._Prd_TTrans = 0; //txtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporte.Text);
        P._Prd_TEntre = 0; // txtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTentrega.Text);
        P._Prd_Transito = 0; //  txtTransito.Text == string.Empty ? 0 : Convert.ToInt32(txtTransito.Text);
        P._Prd_UniNe = $('#cmbUentrada').val(); //  cmbUentrada.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentrada.SelectedValue;
        P._Prd_UniNs = $('#cmbUsalida').val();  // cmbUsalida.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalida.SelectedValue;
        P._Prd_Unico = TextId_Prd; // TextId_Prd.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Prd.Text);
        P._Prd_UniEmp = $('#txtUempaque').val(); // txtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaque.Text);
        // Indicador de compra local
        P._Prd_Colo = true; // chkComprasLocales.Checked;
        P._Prd_Ren = 0; //  txtRentabilidad.Text.Length > 0 ? txtRentabilidad.Text[0] : ' ';
        P._Prd_Mes = 0; //  txtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacion.Text);
        P._Prd_CLNomFab = ''; //  txtFnombre.Text;
        P._Prd_CLIdFab = ''; // txtFcodigo.Text;
        P._Prd_CLDesFab = ''; // txtFdescripcion.Text;
        P._Prd_CLPreFab = ''; // txtFpresentacion.Text;
        P._Prd_CLNomPro = ''; // txtPnombre.Text;
        P._Prd_CLIdPro = ''; //txtPcodigo.Text;
        P._Prd_CLDesPro = ''; // txtPdescripcion.Text;
        P._Prd_CLPrePro = ''; // txtPpresentacion.Text;
        P._Prd_MaxExistencia = 0; // txtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
        P._Prd_Ubicacion = ''; //  txtUbicacion.Text;
        P._Prd_Contribucion = 0; //  txtContribucion.Text == string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
        P._Prd_PorUtilidades = 0; //  txtPorUtilidades.Text == string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
        P._Prd_Nuevo = false; //  chkProductoNuevo.Checked;
        P._Prd_PesConTecnico = 0; //  txtPesos.Text == string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
        P._Prd_CptSv = ''; // string.Empty;
        P._Prd_Activo = 2 //chkActivoAbasto // chkActivo.Checked;
        P._Prd_FecAlta = ''; //  DateTime.Now;
        P._Prd_Minimo = 0; // txtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
        P._Prd_PlanAbasto = ''; //txtPlanAbasto.Text;
        //RBM Nov 2023
        //Campos nuevos
        P._Prd_ClaveUnidad = cmbUnidadMedidaSATDesabasto.substring(0, 3);
        P._Prd_ClaveProdServ = cmbProdServicioSATDesabasto.substring(0, 8);
        P._Prd_FechaInicio = $('#rdpVigencia').val();
        P._Prd_FechaFin = $('#rdpVigenciaFin').val();
        P._Prd_CodigoProv = $('#txtCodProveedor').val();
        P._Prd_DescripcionProv = $('#txtDesProveedor').val();
        P._Prd_PresentacionProv = $('#cmbPresentacionProv option:selected').text();
        P._Prd_NomProvCentral = $('#txtProveedorCentral').val();

        P._Prd_IdProvLocal = $('#txtProveedor').val();
        P._Prd_NomProvLocal = $('#cmbProveedor option:selected').text();
        P._Prd_NomFamilia = FamName;
        P._Prd_NomSubFamilia = SubFamName;

        ListaProductoPrecios = [];
        var i2 = 0;
        for (i = 0; i < 5; i++) {
            i2 = i2 + 1;
            var Prd_Actual = $('#Prd_Actual_' + i).val();
            var Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            var Prd_FechaInicio = P._Prd_FechaInicio; //$('#Prd_FechaInicio_' + i).text();
            Prd_FechaInicio = format_YYYYMMDD_2(Prd_FechaInicio);
            var Prd_FechaFin = P._Prd_FechaFin;//$('#Prd_FechaFin_' + i).text();
            Prd_FechaFin = format_YYYYMMDD_2(Prd_FechaFin);
            var Pre_PesosX = $('#Pre_Pesos_' + i).text();
            var Pre_Prd = txtCodProd;  // $('#txtCodigoUsadoProd').val();
            if (Pre_Descripcion && Prd_FechaInicio && Prd_FechaFin && Pre_PesosX) {
                if (Prd_Actual == "true") {
                    Prd_Actual = 1;
                } else {
                    Prd_Actual = 0;
                }
                var ObjPrecio = {
                    '_Id_Emp': 0,
                    '_int _Id_Cd': 0,
                    '_long _Id_Prd': Pre_Prd,
                    '_Id_Pre': i2,
                    '_Prd_Actual': Prd_Actual,
                    '_Prd_FechaInicio': Prd_FechaInicio,
                    '_Prd_FechaFin': Prd_FechaFin,
                    '_Prd_PreDescripcion': '',
                    '_Pre_Descripcion': Pre_Descripcion,
                    '_Prd_PreDescripcion': Pre_Descripcion,
                    '_Prd_Pesos': Pre_PesosX
                }
                ListaProductoPrecios.push(ObjPrecio);
            }
        }
        P._ListaProductoPrecios = ListaProductoPrecios;

        //Id_Prv = $('#txtProveedor').val();

        // OPCION 1 - Activacion de Codigo por Desabasto
        alertify.success('Guardando compra Local...');
        AJAX_INSERTARPRODUCTO_CL(P, ListaProductoPrecios, function (Datos) {
            alertify.success('Guardando solicitud...');
            AJAX_INSERTARSOLICITUD(
                cmbMotivo,
                TextId_Prd,
                txtCodProd,
                Det_Costo,
                0, //5
                0,
                P._Prd_Descripcion,
                P._Id_Ptp,
                TipoProducto,
                FamName, //10
                SubFamName,
                P._Prd_FechaFin,
                IdCausaDesabasto,
                CausaDesabastoName,
                cmbUnidadMedidaSATDesabasto,
                cmbProdServicioSATDesabasto,
                cmbProveedor,
                ProviderName,
                0,
                'REF',
                P._Id_Fam,// 20 Aplicacion de Producto
                P._Prd_NomProvCentral,
                P._Prd_CodigoProv,
                P._Prd_DescripcionProv,
                P._Prd_PresentacionProv,

                function (Datos, Mensaje) {
                    alertify.alert('<b>NUEVA SOLICITUD</b></br></br>Se ha generado la solicitud: ' + Mensaje);
                    CL.LIMPIAR_CONTROLES(function () {
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                    });
                });
        },
            function (Mensaje) {
                $('#spinner_Buscar').css('display', 'none');
                alertify.error(Mensaje);
            }
        );

    },

    // OPCION 2 - Condigo Cental Con Abasto Local    
    GUARDAR_CodigoCentralAbastoLocal: function () {
        $('#SPINNER_Guardar').css('display', 'block');
        $('#btnGuardarCompraLocal').prop('disabled', true);
        //console.log('DEBUG: GUARDAR_CodigoCentralAbastoLocal');
        //Motivo para la Compra Local
        let cmbMotivo = $('#cmbCategorias').val();
        // VALIDACIONES    
        let txtCodigoUsadoProd = $('#txtCodigoUsadoProd').val();
        let TextPrd_Descrpcion = $('#TextPrd_Descrpcion').val();
        let TextId_Prd = $('#TextId_Prd').val();
        let txtCodProd = $('#txtCodProd').val();
        if (TextId_Prd == '') {
            alertify.error("Debe seleccionar el producto.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        let cmbProveedor = $('#cmbProveedor').val();
        let ProviderName = $('#cmbProveedor option:selected').text();
        if (cmbProveedor == '-1') {
            alertify.error("Debe seleccionar el proveedor.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let IdMotivoDeDesabasto = 0;
        var CausaDesabastoName = 'NO APLICA';
        let IdCausaDesabasto = 0;

        //// Pedido
        //let SeleccionPedido = false;
        //for (i = 0; i < 30; i++) {
        //    let Id_Pedido = $('#rowPedido_' + i).val();
        //    let chb_Pedido = $('#chb_Pedido_' + Id_Pedido).is(":checked");
        //    if (chb_Pedido) {
        //        SeleccionPedido = true;
        //    }
        //}

        //// Pedido
        var Det_Costo = 0;
        var Costo = 0;
        var Pre_Pesos = 0;
        for (i = 0; i < 10; i++) {
            let Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            if (Pre_Descripcion == 'Precio AAA código compra local') {
                Pre_Pesos = $('#Pre_Pesos_' + i).text();
                Pre_Pesos = parseFloat(Pre_Pesos);
                if (isNaN(Pre_Pesos)) {
                    Pre_Pesos = 0;
                }
                Costo = Pre_Pesos;
            }
        }

        if (Costo <= 0) {
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            alertify.error("El precio de COSTO actual no puede ser CERO.");
            return;
        } else {
            Det_Costo = Pre_Pesos;
        }

        // VALIDACINOES SAT
        // VALIDACINOES SAT
        let cmbUnidadMedidaSATDesabasto = $('#cmbUnidadMedidaSATDesabasto option:selected').text();// $('#cmbUnidadMedidaSATDesabasto').val();
        let cmbProdServicioSATDesabasto = $('#ddlProdServicio_SATDesabasto option:selected').text();//$('#ddlProdServicio_SATDesabasto').val();

        if (cmbUnidadMedidaSATDesabasto == "0" || cmbProdServicioSATDesabasto == "0") {
            alertify.error("Falta alguno de los datos en las opciones SAT.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        let P = {}
        P = Entidad_Producto;
        // Verificar Precios
        P._Id_Prd = txtCodProd; //  TextId_Prd;  // $('#txtCodigoUsadoProd').val();
        P._Id_Spo = 0; //  TextId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Spo.Text);
        // IdTipoProducto
        P._Id_Ptp = $('#cmbTipoProducto').val();
        let TipoProducto = $('#cmbTipoProducto').val() + " " + $('#cmbTipoProducto option:selected').text();
        var IdTipoProducto = $('#cmbTipoProducto').text();
        IdTipoProducto = parseInt(IdTipoProducto);
        if (isNaN(IdTipoProducto)) {
            IdTipoProducto = 0;
        }
        let Id_Cpr = $('#Id_Cpr').val();
        if (Id_Cpr == '' || Id_Cpr == '0') {
            Id_Cpr = 1;
        }
        P._Id_Cpr = Id_Cpr;  // txtCategoria.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoria.Text);
        P._Id_Fam = $('#cmbFam').val();  // txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
        let FamName = $('#cmbFam option:selected').text();
        P._Id_Sub = $('#cmbSubFam').val();   //txtSubFam.Value.HasValue ? Convert.ToInt32(txtSubFam.Text) : 0;
        let SubFamName = $('#cmbSubFam option:selected').text();
        P._Id_Pvd = $('#txtProveedor').val(); //txtProveedor.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedor.Text);
        P._Prd_Descripcion = $('#TextPrd_Descrpcion').val(); //TextPrd_Descrpcion.Text;
        P._Prd_Presentacion = $('#txtPresentacion').val();  // txtPresentacion.Text;
        P._Prd_InvInicial = 0; //txtInicial.Text == string.Empty ? 0 : Convert.ToInt32(txtInicial.Text);
        P._Prd_InvFinal = 0; //txtFinal.Text == string.Empty ? 0 : Convert.ToInt32(txtFinal.Text);
        P._Prd_AgrupadoSpo = 0; // txtAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(txtAgrupadoSpo.Text);
        P._Prd_FactorConv = $('#txtFactorConversion').val(); // txtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversion.Text);
        P._Prd_AparatoSisProp = false;  // chkSistProP._Checked;
        P._Prd_Fisico = 0; // txtFisico.Text == string.Empty ? 0 : Convert.ToInt32(txtFisico.Text);
        P._Prd_Ordenado = 0; // = txtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenado.Text);
        P._Prd_Asignado = 0; //  txtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignado.Text);
        P._Prd_InvSeg = 0; // txtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridad.Text);
        P._Prd_TTrans = 0; //txtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporte.Text);
        P._Prd_TEntre = 0; // txtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTentrega.Text);
        P._Prd_Transito = 0; //  txtTransito.Text == string.Empty ? 0 : Convert.ToInt32(txtTransito.Text);
        P._Prd_UniNe = $('#cmbUentrada').val(); //  cmbUentrada.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentrada.SelectedValue;
        P._Prd_UniNs = $('#cmbUsalida').val();  // cmbUsalida.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalida.SelectedValue;
        P._Prd_Unico = TextId_Prd; // TextId_Prd.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Prd.Text);
        P._Prd_UniEmp = $('#txtUempaque').val(); // txtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaque.Text);
        P._Prd_Colo = true; // chkComprasLocales.Checked;
        P._Prd_Ren = 0; //  txtRentabilidad.Text.Length > 0 ? txtRentabilidad.Text[0] : ' ';
        P._Prd_Mes = 0; //  txtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacion.Text);
        P._Prd_CLNomFab = ''; //  txtFnombre.Text;
        P._Prd_CLIdFab = ''; // txtFcodigo.Text;
        P._Prd_CLDesFab = ''; // txtFdescripcion.Text;
        P._Prd_CLPreFab = ''; // txtFpresentacion.Text;
        P._Prd_CLNomPro = ''; // txtPnombre.Text;
        P._Prd_CLIdPro = ''; //txtPcodigo.Text;
        P._Prd_CLDesPro = ''; // txtPdescripcion.Text;
        P._Prd_CLPrePro = ''; // txtPpresentacion.Text;
        P._Prd_MaxExistencia = 0; // txtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
        P._Prd_Ubicacion = ''; //  txtUbicacion.Text;
        P._Prd_Contribucion = 0; //  txtContribucion.Text == string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
        P._Prd_PorUtilidades = 0; //  txtPorUtilidades.Text == string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
        P._Prd_Nuevo = false; //  chkProductoNuevo.Checked;
        P._Prd_PesConTecnico = 0; //  txtPesos.Text == string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
        P._Prd_CptSv = ''; // string.Empty;
        P._Prd_Activo = 2 //chkActivoAbasto // chkActivo.Checked;
        P._Prd_FecAlta = ''; //  DateTime.Now;
        P._Prd_Minimo = 0; // txtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
        P._Prd_PlanAbasto = ''; //txtPlanAbasto.Text;
        //RBM Nov 2023
        //Campos nuevos

        //P._Prd_FechaInicio = $('#rdpVigencia').val();
        //P._Prd_FechaFin = $('#rdpVigenciaFin').val();
        //P._Prd_IdProvLocal = cmbProveedor;
        //P._Prd_NomProvLocal = ProviderName;
        //P._Prd_CodigoProv = $('#txtCodProveedor').val();
        //P._Prd_DescripcionProv = $('#txtDesProveedor').val();
        //P._Prd_PresentacionProv = $('#cmbPresentacionProv option:selected').text();
        //P._Prd_NomFamilia = FamName;
        //P._Prd_NomSubFamilia = SubFamName;


        P._Prd_ClaveUnidad = cmbUnidadMedidaSATDesabasto.substring(0, 3);
        P._Prd_ClaveProdServ = cmbProdServicioSATDesabasto.substring(0, 8);
        P._Prd_FechaInicio = $('#rdpVigencia').val();
        P._Prd_FechaFin = $('#rdpVigenciaFin').val();
        P._Prd_CodigoProv = $('#txtCodProveedor').val();
        P._Prd_DescripcionProv = $('#txtDesProveedor').val();
        P._Prd_PresentacionProv = $('#cmbPresentacionProv option:selected').text();
        P._Prd_NomProvCentral = $('#txtProveedorCentral').val();

        P._Prd_IdProvLocal = $('#txtProveedor').val();
        P._Prd_NomProvLocal = $('#cmbProveedor option:selected').text();
        P._Prd_NomFamilia = FamName;
        P._Prd_NomSubFamilia = SubFamName;


        ListaProductoPrecios = [];
        let i2 = 0;
        for (i = 0; i < 10; i++) {
            i2 = i2 + 1;
            let Prd_Actual = $('#Prd_Actual_' + i).val();
            let Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            let Prd_FechaInicio = P._Prd_FechaInicio; //$('#Prd_FechaInicio_' + i).text();
            Prd_FechaInicio = format_YYYYMMDD_2(Prd_FechaInicio);
            let Prd_FechaFin = P._Prd_FechaFin; //$('#Prd_FechaFin_' + i).text();
            Prd_FechaFin = format_YYYYMMDD_2(Prd_FechaFin);
            let Pre_PesosX = $('#Pre_Pesos_' + i).text();
            let Pre_Prd = txtCodProd;  // $('#txtCodigoUsadoProd').val();
            if (Pre_Descripcion && Prd_FechaInicio && Prd_FechaFin && Pre_PesosX) {
                if (Prd_Actual == "true") {
                    Prd_Actual = 1;
                } else {
                    Prd_Actual = 0;
                }
                let ObjPrecio = {
                    '_Id_Emp': 0,
                    '_int _Id_Cd': 0,
                    '_long _Id_Prd': Pre_Prd,
                    '_Id_Pre': i2,
                    '_Prd_Actual': Prd_Actual,
                    '_Prd_FechaInicio': Prd_FechaInicio,
                    '_Prd_FechaFin': Prd_FechaFin,
                    '_Prd_PreDescripcion': '',
                    '_Pre_Descripcion': Pre_Descripcion,
                    '_Prd_PreDescripcion': Pre_Descripcion,
                    '_Prd_Pesos': Pre_PesosX
                }
                ListaProductoPrecios.push(ObjPrecio);
            }
        }

        P._ListaProductoPrecios = ListaProductoPrecios;
        Id_Prv = $('#txtProveedor').val();
        let Fecha = new Date();
        let Dia = Fecha.getDate();
        Dia = parseInt(Dia);
        if (Dia <= 9) {
            Dia = '0' + Dia;
        }
        Mes = Fecha.getMonth() + 1;
        Mes = parseInt(Mes);
        if (Mes <= 9) {
            Mes = '0' + Mes;
        }
        let FechaVigencia = Fecha.getFullYear() + '/' + Mes + '/' + Dia;

        alertify.success('Guardando compra Local...');
        AJAX_INSERTARPRODUCTO_CL(P, ListaProductoPrecios, function (Datos) {
            alertify.success('Guardando solicitud...');
            AJAX_INSERTARSOLICITUD(
                cmbMotivo,
                TextId_Prd,
                txtCodProd,
                Det_Costo,
                0, //5
                0,
                P._Prd_Descripcion,
                P._Id_Ptp,
                TipoProducto,
                FamName,
                SubFamName, //10 
                P._Prd_FechaFin,
                IdCausaDesabasto,
                CausaDesabastoName,
                cmbUnidadMedidaSATDesabasto,
                cmbProdServicioSATDesabasto,
                cmbProveedor, //15
                ProviderName,
                0,
                'REF',
                P._Id_Fam, // Aplicacion de PRoducto 
                P._Prd_NomProvCentral,
                P._Prd_CodigoProv,
                P._Prd_DescripcionProv,
                P._Prd_PresentacionProv,

                function (Datos, Mensaje) {
                    //CALLBACK_Exito                                   
                    AJAX_CL_GRABASOLOCOMENTARIOSCLIENTE(Datos, 'Sin Comentarios', function (Datos_Folio) {
                        alertify.alert('<b>NUEVA SOLICITUD</b></br></br>Se ha generado la solicitud: ' + Mensaje + ' ' + Datos_Folio);
                        console.log(Datos.Folio);
                    });

                    CL.LIMPIAR_CONTROLES(function () {

                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                    });
                },
                function (Datos, Mensaje) {
                    alertify.error('<b>Ocurrio un error GRAVE, No se pudo completar la creacion del documento ' + Mensaje);
                });
        }, function () {
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
        }
        );
    },

    // GUARDAR OPCION 3 - SolicitudCliente - EnviarSolicitudClients
    GUARDAR_SolicitudCliente: function () {
        $('#SPINNER_Guardar').css('display', 'block');
        $('#btnGuardarCompraLocal').prop('disabled', true);
        console.log('DEBUG: GUARDAR_SolicitudCliente');
        //Motivo para la Compra Local
        var cmbMotivo = $('#cmbCategorias').val();

        // VALIDA CAMPOS 
        var txtCodigoUsadoProd = $('#txtCodigoUsadoProd').val();
        var TextPrd_Descrpcion = $('#TextPrd_Descrpcion').val();
        var TextId_Prd = $('#hfId_Prd').val();
        var txtCodProd = $('#txtCodProd').val();
        var txtPresentacion = $('#txtPresentacion').val();
        var cmbPresentacion = $('#cmbPresentacion option:selected').text();
        var cmbTipoProducto = $('#cmbTipoProducto').val();
        var cmbProveedor = $('#cmbProveedor').val();
        var ProviderName = $('#cmbProveedor option:selected').text();
        var cmbUentrada = $('#cmbUentrada').val();
        var cmbUsalida = $('#cmbUsalida').val();
        var txtMotivoSolicita = $('#txtMotivoSolicita').val();
        var txtFactorConversion = $('#txtFactorConversion').val();
        var txtUempaque = $('#txtUempaque').val();
        //Campos nuevos
        //RBM Dic 2023
        //var cmbPresentacion = $('#cmbPresentacion option:selected').text();
        var rdpVigencia = $('#rdpVigencia').val();
        var rdpVigenciaFin = $('#rdpVigenciaFin').val();
        var txtProveedorCentral = $('#txtProveedorCentral').val();
        var txtCodProveedor = $('#txtCodProveedor').val();
        var txtDesProveedor = $('#txtDesProveedor').val();

        if (TextPrd_Descrpcion == '') {
            alertify.error("Debe establecer el campo Descripción.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbPresentacion == '') {
            alertify.error("Debe establecer el campo Presentación.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbTipoProducto == '') {
            alertify.error("Debe establecer el campo Tipo de producto.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbTipoProducto == '-1') {
            alertify.error("Debe establecer el campo Tipo de producto.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbProveedor == '-1') {
            alertify.error("Debe seleccionar el proveedor.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbUentrada == '0') {
            alertify.error("Debe seleccionar el campo Unidad de Entrada.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (cmbUsalida == '0') {
            alertify.error("Debe seleccionar el campo Unidad de Salida.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (txtFactorConversion == '') {
            alertify.error("Debe estalecer el campo Factor de Conversión.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (txtUempaque == '') {
            alertify.error("Debe estalecer el campo Unidad de empaque.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        if (txtMotivoSolicita.length <= '15') {
            alertify.error("Debe establecer el motivo de solicitud (16 Carácteres mínimo).");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        var TextId_Prd = $('#TextId_Prd').val();
        let cmbCausaDesabasto = $('#cmbCausaDesabasto').val();
        cmbCausaDesabasto = parseInt(cmbCausaDesabasto);
        if (isNaN(cmbCausaDesabasto)) {
            cmbCausaDesabasto = 0
        }
        let IdCausaDesabasto = cmbCausaDesabasto;
        var CausaDesabastoName = $('#cmbCausaDesabasto option:selected').text();
        var Det_Costo = 0;
        // Pedido
        var Costo = 0;
        let Pre_Pesos = 0;
        for (i = 0; i < 5; i++) {
            var Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            if (Pre_Descripcion == 'Precio AAA código compra local') {
                Pre_Pesos = $('#Pre_Pesos_' + i).text();
                Pre_Pesos = parseFloat(Pre_Pesos);
                if (isNaN(Pre_Pesos)) {
                    Pre_Pesos = 0;
                }
                Costo = Pre_Pesos;
            }
        }

        if (Costo <= 0) {
            alertify.error("El precio de COSTO actual no puede ser CERO.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        } else {
            Det_Costo = Pre_Pesos;
        }

        // 
        // VALIDACINOES SAT
        //
        let cmbUnidadMedidaSATDesabasto = $('#cmbUnidadMedidaSATDesabasto option:selected').text();
        let cmbProdServicioSATDesabasto = $('#ddlProdServicio_SATDesabasto option:selected').text();
        if (cmbUnidadMedidaSATDesabasto == "0" || cmbProdServicioSATDesabasto == "0") {
            alertify.error("Falta alguno de los datos en las opciones SAT.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        var Prod = {}
        Prod = Entidad_Producto;
        // Verificar Precios
        Prod._Id_Prd = txtCodProd; //  TextId_Prd;  // $('#txtCodigoUsadoProd').val();
        Prod._Id_Spo = 0; //  TextId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Spo.Text);
        // IdTipoProducto
        Prod._Id_Ptp = $('#cmbTipoProducto').val();
        var TipoProducto = $('#cmbTipoProducto').val() + " " + $('#cmbTipoProducto option:selected').text();
        // txtTipoProducto.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProducto.Text);
        var Id_Cpr = $('#Id_Cpr').val();
        if (Id_Cpr == '' || Id_Cpr == '0') {
            Id_Cpr = 1;
        }
        Prod._Id_Cpr = Id_Cpr;  // txtCategoria.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoria.Text);
        Prod._Id_Fam = $('#cmbFam').val();  // txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
        var FamName = $('#cmbFam option:selected').text();
        Prod._Id_Sub = $('#cmbSubFam').val();   //txtSubFam.Value.HasValue ? Convert.ToInt32(txtSubFam.Text) : 0;
        var SubFamName = $('#cmbSubFam option:selected').text();
        Prod._Id_Pvd = $('#txtProveedor').val(); //txtProveedor.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedor.Text);
        Prod._Prd_Descripcion = $('#TextPrd_Descrpcion').val(); //TextPrd_Descrpcion.Text;
        Prod._Prd_Presentacion = $('#cmbPresentacion option:selected').text();  // txtPresentacion.Text;
        Prod._Prd_InvInicial = 0; //txtInicial.Text == string.Empty ? 0 : Convert.ToInt32(txtInicial.Text);
        Prod._Prd_InvFinal = 0; //txtFinal.Text == string.Empty ? 0 : Convert.ToInt32(txtFinal.Text);
        Prod._Prd_AgrupadoSpo = 0; // txtAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(txtAgrupadoSpo.Text);
        Prod._Prd_FactorConv = $('#txtFactorConversion').val(); // txtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversion.Text);
        Prod._Prd_AparatoSisProp = false;  // chkSistProP._Checked;
        Prod._Prd_Fisico = 0; // txtFisico.Text == string.Empty ? 0 : Convert.ToInt32(txtFisico.Text);
        Prod._Prd_Ordenado = 0; // = txtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenado.Text);
        Prod._Prd_Asignado = 0; //  txtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignado.Text);
        Prod._Prd_InvSeg = 0; // txtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridad.Text);
        Prod._Prd_TTrans = 0; //txtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporte.Text);
        Prod._Prd_TEntre = 0; // txtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTentrega.Text);
        Prod._Prd_Transito = 0; //  txtTransito.Text == string.Empty ? 0 : Convert.ToInt32(txtTransito.Text);
        Prod._Prd_UniNe = $('#cmbUentrada').val(); //  cmbUentrada.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentrada.SelectedValue;
        Prod._Prd_UniNs = $('#cmbUsalida').val();  // cmbUsalida.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalida.SelectedValue;
        Prod._Prd_Unico = TextId_Prd; // TextId_Prd.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Prd.Text);
        Prod._Prd_UniEmp = $('#txtUempaque').val(); // txtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaque.Text);
        Prod._Prd_Colo = true; // chkComprasLocales.Checked;
        Prod._Prd_Ren = 0; //  txtRentabilidad.Text.Length > 0 ? txtRentabilidad.Text[0] : ' ';
        Prod._Prd_Mes = 0; //  txtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacion.Text);
        Prod._Prd_CLNomFab = ''; //  txtFnombre.Text;
        Prod._Prd_CLIdFab = ''; // txtFcodigo.Text;
        Prod._Prd_CLDesFab = ''; // txtFdescripcion.Text;
        Prod._Prd_CLPreFab = ''; // txtFpresentacion.Text;
        Prod._Prd_CLNomPro = ''; // txtPnombre.Text;
        Prod._Prd_CLIdPro = ''; //txtPcodigo.Text;
        Prod._Prd_CLDesPro = ''; // txtPdescripcion.Text;
        Prod._Prd_CLPrePro = ''; // txtPpresentacion.Text;
        Prod._Prd_MaxExistencia = 0; // txtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
        Prod._Prd_Ubicacion = ''; //  txtUbicacion.Text;
        Prod._Prd_Contribucion = 0; //  txtContribucion.Text == string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
        Prod._Prd_PorUtilidades = 0; //  txtPorUtilidades.Text == string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
        Prod._Prd_Nuevo = false; //  chkProductoNuevo.Checked;
        Prod._Prd_PesConTecnico = 0; //  txtPesos.Text == string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
        Prod._Prd_CptSv = ''; // string.Empty;
        Prod._Prd_Activo = 2 //chkActivoAbasto // chkActivo.Checked;
        Prod._Prd_FecAlta = ''; //  DateTime.Now;
        Prod._Prd_Minimo = 0; // txtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
        Prod._Prd_PlanAbasto = ''; //txtPlanAbasto.Text;
        //RBM Nov 2023
        //Campos nuevos
        Prod._Prd_FechaInicio = $('#rdpVigencia').val();
        Prod._Prd_FechaFin = $('#rdpVigenciaFin').val();
        Prod._Prd_CodigoProv = $('#txtCodProveedor').val();
        Prod._Prd_DescripcionProv = $('#txtDesProveedor').val();
        Prod._Prd_PresentacionProv = $('#cmbPresentacionProv').val();
        Prod._Prd_ClaveUnidad = cmbUnidadMedidaSATDesabasto.substring(0, 3);
        Prod._Prd_ClaveProdServ = cmbProdServicioSATDesabasto.substring(0, 8);
        Prod._Prd_NomFamilia = FamName;
        Prod._Prd_NomSubFamilia = SubFamName;
        Prod._Prd_IdProvLocal = $('#txtProveedor').val();
        Prod._Prd_NomProvLocal = $('#cmbProveedor option:selected').text();

        // LISTA DE PRECIOS 
        ListaProductoPrecios = [];
        var i2 = 0;
        for (i = 0; i < 5; i++) {
            i2 = i2 + 1;
            var Prd_Actual = $('#Prd_Actual_' + i).val();
            var Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
            var Prd_FechaInicio = $('#Prd_FechaInicio_' + i).text();
            Prd_FechaInicio = format_YYYYMMDD_2(Prd_FechaInicio);
            var Prd_FechaFin = $('#Prd_FechaFin_' + i).text();
            Prd_FechaFin = format_YYYYMMDD_2(Prd_FechaFin);
            Pre_Pesos = $('#Pre_Pesos_' + i).text();
            var Pre_Prd = txtCodProd;  // $('#txtCodigoUsadoProd').val();
            if (Pre_Descripcion && Prd_FechaInicio && Prd_FechaFin && Pre_Pesos) {
                if (Prd_Actual == "true") {
                    Prd_Actual = 1;
                } else {
                    Prd_Actual = 0;
                }
                var ObjPrecio = {
                    '_Id_Emp': 0,
                    '_int _Id_Cd': 0,
                    '_long _Id_Prd': Pre_Prd,
                    '_Id_Pre': i2,
                    '_Prd_Actual': Prd_Actual,
                    '_Prd_FechaInicio': Prd_FechaInicio,
                    '_Prd_FechaFin': Prd_FechaFin,
                    '_Prd_PreDescripcion': '',
                    '_Pre_Descripcion': Pre_Descripcion,
                    '_Prd_PreDescripcion': Pre_Descripcion,
                    '_Prd_Pesos': Pre_Pesos
                }
                ListaProductoPrecios.push(ObjPrecio);
            }
        }
        Prod._ListaProductoPrecios = ListaProductoPrecios;

        // CLIENTE EXCLUSIVOS
        var Fecha = new Date();
        var KeyArray_ClienteExclusivos = Fecha.getDate() + '' + (Fecha.getMonth() + 1) + '' + Fecha.getFullYear() + '' + Fecha.getHours() + '' + Fecha.getMinutes() + '' + Fecha.getSeconds();
        var IdArray_ClientesExclusivos = 0;
        for (i = 1; i < 15; i++) {
            var lstCE_IdCte = $('#lstCE_IdCte_' + i).text();
            var lstCE_Nombre = $('#lstCE_Nombre_' + i).text();
            var lstCE_TipoCliente = $('#lstCE_TipoCliente_' + i).text();
            if (lstCE_IdCte != undefined && lstCE_Nombre != undefined && lstCE_TipoCliente != undefined && lstCE_IdCte != '' && lstCE_Nombre != '' && lstCE_TipoCliente != '') {
                AJAX_CL_InsertClienteExclusivo(
                    lstCE_IdCte, lstCE_Nombre, KeyArray_ClienteExclusivos, lstCE_TipoCliente,
                    function () { }
                );
                IdArray_ClientesExclusivos = IdArray_ClientesExclusivos + 1
            }
        }

        if (IdArray_ClientesExclusivos == "0") {
            alertify.alert("Debe seleccionar al menos un Cliente Exclusivo para continuar.");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        Id_Prv = $('#txtProveedor').val();
        //Var ProvCentral = $('#txtProveedorCentral').val();

        alertify.success("Campos completos");

        // OPCION 3 Solicitud Cliente 
        var CausaDesabastoName = $('#txtMotivoSolicita').val();
        var Vigencia = $('#rdpVigencia').val();
        Vigencia = format_YYYYMMDD_2(Vigencia);
        var PedidoReferencia = "0,0,0"; // Documentos de Referencia 
        $('#spinner_Buscar').css('display', 'block');
        AJAX_INSERTARPRODUCTO_CL(Prod, ListaProductoPrecios, function (Datos) {
            alertify.success('Se guardo producto...');
            AJAX_INSERTARSOLICITUD(
                cmbMotivo,
                TextId_Prd,
                txtCodProd,
                Det_Costo,
                0,
                0,
                Prod._Prd_Descripcion,
                Prod._Id_Ptp,
                TipoProducto,
                FamName,
                SubFamName, //10
                Vigencia,
                IdCausaDesabasto,
                CausaDesabastoName,
                cmbUnidadMedidaSATDesabasto,
                cmbProdServicioSATDesabasto, //15
                cmbProveedor,
                ProviderName,
                KeyArray_ClienteExclusivos,
                PedidoReferencia,
                Prod._Id_Fam,  // Aplicacion de Producto
                null,
                Prod._Prd_CodigoProv,
                Prod._Prd_DescripcionProv,
                Prod._Prd_PresentacionProv,
                //P.Prd_IdPvdLocal,
                //P.Prd_NomPvdLocal,
                //P.Prd_NomFamilia,
                //P.Prd_NomSubFamilia,
                function (Datos, Mensaje) {
                    // CALLBACK_Exito
                    alertify.success('Se guardo solicitud...');

                    var Id_Solicitud = Datos;
                    var Vigencia = $('#rdpVigencia').val();
                    var TipoSolicitud = cmbMotivo;
                    var PedidoReferencia = 'REFERENCIA';

                    // Clientes Exclusivos
                    AJAX_CL_InsertClienteExclusivo_UpdateSol(
                        KeyArray_ClienteExclusivos, Id_Solicitud,
                        function () {
                            alertify.success('Actualiza clientes exclusivos');
                        }
                    );

                    AJAX_CL_GRABASOLOCOMENTARIOSCLIENTE(Datos, txtMotivoSolicita, function (Datos_Folio) {
                        alertify.alert('<b>NUEVA SOLICITUD</b></br></br>Se ha generado la solicitud: ' + Mensaje + ' ' + Datos_Folio);
                    });
                    CL.LIMPIAR_CONTROLES(function () {
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                    });

                }, function (Datos, Mensaje) {
                    console.log(Mensaje);
                    // CALBACK_Error
                    $('#spinner_Buscar').css('display', 'none');
                    alertify.error(Mensaje);
                });
        }, function () {
            //$('#spinner_Buscar').css('display', 'none');
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }
        );
    },

    // GUARDAR
    btnGuardar_CompraLocal: function () {
        //Motivo para la Compra Local
        var cmbMotivo = $('#cmbCategorias').val();

        var ProductoCL = $('#cmbProductosHabiliCompraLocal').val();
        if (ProductoCL == '0') {
            alertify.error("Favor de seleccioanr producto y despues dar clic en seleccionar para continuar");
            $('#SPINNER_Guardar').css('display', 'none');
            $('#btnGuardarCompraLocal').prop('disabled', false);
            return;
        }

        cmbMotivo = parseInt(cmbMotivo);
        if (isNaN(cmbMotivo)) {
            cmbMotivo = 0;
        }
        switch (cmbMotivo) {
            case 0:
                alertify.alert('Error en tipo de movimiento');
                return;
                break;
            case 1: // Codigo desabasto
                var Codigo = $('#txtCodProd').val();
                if (Codigo == "") {
                    alertify.error("El Código de abasto local esta incompleto, favor de validar la información solicitada.");
                    $('#SPINNER_Guardar').css('display', 'none');
                    $('#btnGuardarCompraLocal').prop('disabled', false);
                    return;
                }
                CL_Ajax.ConsultarCodigo(Codigo, function (Estado) {
                    if (Estado == 0) {
                        // No existe    
                        CL.GUARDAR_ActivacionCodigoDesabasto();
                    } else if (Estado == 1) {
                        // Ya existe    
                        alertify.error("Esta intentando duplicar un producto que ya registrado.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    } else if (Estado == -1) {
                        // Error
                        alertify.error("Error al intentar guardar esta solicitud, favor de intentar de nuevo.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    }
                });
                break;
            case 2: // Abasto Local                
                var Codigo = $('#txtCodProd').val();
                CL_Ajax.ConsultarCodigo(Codigo, function (Estado) {
                    if (Estado == 0) {
                        // No existe    
                        CL.GUARDAR_CodigoCentralAbastoLocal();
                    } else if (Estado == 1) {
                        // Ya existe    
                        alertify.error("Esta intentando duplicar un producto que ya registrado.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    } else if (Estado == -1) {
                        // Error
                        alertify.error("Error al intentar guardar esta solicitud, favor de intentar de nuevo.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    }
                });
                break;
            case 3: // Solicitud Cliente
                var Codigo = $('#TextId_Prd').val();
                if (Codigo == "<PENDIENTE>") {
                    alertify.error("Favor de seleccionar Aplicación y/o SubFamilia, despues dar clic en Aplicar para continuar");
                    $('#SPINNER_Guardar').css('display', 'none');
                    $('#btnGuardarCompraLocal').prop('disabled', false);
                }
                CL_Ajax.ConsultarCodigo(Codigo, function (Estado) {
                    if (Estado == 0) {
                        // No existe duplicado    
                        CL.GUARDAR_SolicitudCliente();
                    } else if (Estado == 1) {
                        // Ya existe    
                        alertify.error("Esta intentando duplicar un producto que ya registrado.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                    } else if (Estado == -1) {
                        // Error
                        alertify.error("Error al intentar guardar esta solicitud, favor de intentar de nuevo.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    }
                });
                break;
            case 4: // Codigo desabasto
                var Codigo = $('#txtCodProd').val();
                if (Codigo == "") {
                    alertify.error("El Código de abasto local esta incompleto, favor de validar la información solicitada.");
                    $('#SPINNER_Guardar').css('display', 'none');
                    $('#btnGuardarCompraLocal').prop('disabled', false);
                    return;
                }
                CL_Ajax.ConsultarCodigo(Codigo, function (Estado) {
                    if (Estado == 0) {
                        // No existe    
                        CL.GUARDAR_ActivacionCodigoDesabasto();
                    } else if (Estado == 1) {
                        // Ya existe    
                        alertify.error("Esta intentando duplicar un producto que ya registrado.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    } else if (Estado == -1) {
                        // Error
                        alertify.error("Error al intentar guardar esta solicitud, favor de intentar de nuevo.");
                        $('#SPINNER_Guardar').css('display', 'none');
                        $('#btnGuardarCompraLocal').prop('disabled', false);
                        return;
                    }
                });
                break;
        }
    },

    // BOTON SELECCION PRODUCTO 
    // - SOLICITUD DEL CLIENTE             
    btnSeleccionaProdSol_Click: function () {
        //$('#btnSeleccionaProdSol').click(function (e) {
        $('#SPINNER_CL').css('display', 'block'); // SPINNER
        let cmbAplicacionSoli = $('#cmbAplicacionSoli').val();
        if (cmbAplicacionSoli == 0) {
            alertify.error('No  ha seleccionado la Aplicacion');
            $('#SPINNER_CL').css('display', 'none'); // SPINNER
            return;
        }
        let cmdSubFamiliaSoli = $('#cmdSubFamiliaSoli').val();
        if (cmdSubFamiliaSoli == 0) {
            alertify.error('No  ha seleccionado la SubFamilia');
            $('#SPINNER_CL').css('display', 'none'); // SPINNER
            return;
        }
        Interface.Inicializa_SolicitudCliente();
        // Frcha Vencimiento 
        let Fecha = new Date();
        let Dia = Fecha.getDate();
        Dia = parseInt(Dia);
        if (Dia <= 9) {
            Dia = '0' + Dia;
        }
        let Mes = Fecha.getMonth() + 1;
        Mes = parseInt(Mes);
        if (Mes <= 9) {
            Mes = '0' + Mes;
        }
        let Anio = 0;
        Anio = Fecha.getFullYear();
        Anio = Anio + 1;
        Fecha = Dia + '/' + Mes + '/' + Anio;
        $('#rdpVigenciaFin').val(Fecha);

        $('#cmbFam').val(cmbAplicacionSoli);

        $('#SPINNER_CL').css('display', 'block'); // SPINNER
        ajax_CL_CodigoHomologado_Maximo_Consulta(0, '', function (Datos) {
            $('#TextId_Prd').val(Datos);
            let TextId_Prd = $('#TextId_Prd').val();
            Cargar_cmbProductoSubFamilia(cmbAplicacionSoli, function () {
                $('#cmbSubFam').val(cmdSubFamiliaSoli);
                console.log('cmdSubFamiliaSoli:' + cmdSubFamiliaSoli);
                setTimeout(function () { $('#SPINNER_CL').css('display', 'none'); }, 1000);
            });
        });
        $('#btnSeleccionaProdSol').removeClass('btn-primary');
        $('#btnSeleccionaProdSol').addClass('btn-default');
    }
}

var Interface = {

    Cargar_cmbProductoSubFamilia_Op3: function (Id_Familia, CALLBAK_Exito) {
        CL_Ajax.ProductoSubFamiliaCte(Id_Familia, function (lst) {
            $('#cmdSubFamiliaSoli').empty();
            $('#cmdSubFamiliaSoli').append('<option value="0" >-- Seleccione -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#cmdSubFamiliaSoli').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
            }
            if (CALLBAK_Exito) {
                CALLBAK_Exito();
            }
        });
    },

    // Opcion 3 Carga Combo APLICACION
    Cargar_cmbProductoFamilia_op3: function (Control, Id_ProductoFamiliaCte, CALLBAK_Exito) {
        CL_Ajax.ProductoFamiliaCte(0, function (lst) {
            $(Control).empty();
            $(Control).append('<option value="0" >-- Seleccione -- </option>');
            for (i = 0; i < lst.length; i++) {
                $(Control).append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
            }
            var Id_Familia = $(Control).val();
            if (CALLBAK_Exito) {
                CALLBAK_Exito(Id_Familia);
            }
        });
    },

    // Carga Combo 
    Inicializa_Opcion_3: function () {
        Interface.Cargar_cmbProductoFamilia_op3('#cmbAplicacionSoli', 0, function (Id_Familia) {
            // Subfamilia
            Interface.Cargar_cmbProductoSubFamilia_Op3(Id_Familia);
        });
    },

    Inicializa_ControlesHidden: function () {
        $('#hfId_Prd').val('');
        $('#hfPrd_Descripcion').val('');
    },
    // OPCION 1 y 4 .- Codigo Central con Abasto Local y Compra por Estrategia
    Inicializa_ActivacionCodigoDesabasto: function () {
        Interface.Inicializa_ControlesHidden();
        $('#divMotivo_Area_2').css('display', 'block');
        $('#row_CodigoKey').css('display', 'block');
        $('#row_CodigoAbasto').css('display', 'block');
        $('#row_AplicacionSubFam').css('display', 'none');
        $('#col_lbVigencia').css('display', 'block');
        $('#col_tbVigencia').css('display', 'block');
        $('#rdpVigencia').prop('disabled', true);
        $('#col_lbVigenciaFin').css('display', 'block');
        $('#col_tbVigenciaFin').css('display', 'block');
        $('#rdpVigenciaFin').prop('disabled', true);

        $('#row_CausaDesabasto').css('display', 'block');
        $('#rdpVigencia').val('');
        $('#rdpVigenciaInicio').val('');
        $('#rdpVigenciaFin').val('');
        $('#divPedidosRefAbasto').css('display', 'block');
        $('#divMotivoClienteSolicita').css('display', 'none');
        $('#divMotivoClienteSolicita').val('');
        $('#btnAgregarClienteListado').prop('disabled', true);
        $('#divPedidosRefAbasto').css('display', 'none');
        LlenarListaPrecios_Default(0, function () { });
        $('#divProvCentral').prop('display', 'block');
        $('#divProvCentral').css('display', 'block');
        $('#divProvCentral').prop('display', 'block');
        $('#txtProveedorCentral').css('display', 'block');
        $('#txtProveedorCentral').val('');
        $('#txtProveedorCentral').prop('disabled', true);
        //
    },

    // OPCION 2 .- Codigo Central con Abasto Local
    Inicializa_CodigoCentralConAbastoLocal: function () {
        Interface.Inicializa_ControlesHidden();
        $('#row_CausaDesabasto').css('display', 'none');
        $('#SPINNER_CL').css('display', 'block'); // Validar eliminarlo
        $('#divActivacion').css('display', 'block');
        $('#row_CodigoKey').css('display', 'none');
        $('#row_CodigoAbasto').css('display', 'none');
        $('#row_AplicacionSubFam').css('display', 'none');
        $('#col_lbVigencia').css('display', 'block');
        $('#col_tbVigencia').css('display', 'block');
        $('#col_lbVigenciaFin').css('display', 'block');
        $('#col_tbVigenciaFin').css('display', 'block');
        $('#divMotivo_Area_1').css('display', 'block');
        $('#divMotivo_Area_2').css('display', 'block');
        $('#row_AbstoLocal').css('display', 'block');
        Cargar_cmb_AABuscaProductosCompraLocalTodos(0, function () {
            //$('#SPINNER_CL').css('display', 'none');
        }, function () {
            //$('#SPINNER_CL').css('display', 'none');
        });
        LlenarListaPrecios_Default(0, function () { });
        $('#btnAgregarClienteListado').prop('disabled', true);
        $('#divPedidosRefAbasto').css('display', 'none');
        $('#tblPedidoDesabastecido > tbody').empty();
        $('#rdpVigencia').prop('disabled', true);
        $('#rdpVigenciaFin').prop('disabled', true);
        $('#divProvCentral').prop('display', 'block');
        $('#divProvCentral').css('display', 'block');
        $('#txtProveedorCentral').prop('display', 'block');
        $('#txtProveedorCentral').css('display', 'block');

        $('#divMotivoClienteSolicita').css('display', 'none');
    },

    // OPCION 3
    Inicializa_SolicitudCliente: function () {
        Interface.Inicializa_ControlesHidden();
        CONT_ClientesExc = 0;
        $('#divMotivo_Area_2').css('display', 'block');
        $('#TextId_Prd').prop('disabled', true);
        $('#TextId_Prd').val('<PENDIENTE>');
        $('#TextPrd_Descrpcion').prop('disabled', false);
        $('#TextPrd_Descrpcion').val('');
        $('#txtCodProd').prop('disabled', true);
        $('#txtPresentacion').prop('disabled', false);
        $('#rdpVigencia').prop('disabled', true);
        $('#txtTipoProducto').prop('disabled', false);
        $('#cmbTipoProducto').prop('disabled', false);

        Cargar_cmbTipoProducto(0, function () { });

        $('#divProvCentral').prop('display', 'none');
        $('#divProvCentral').css('display', 'none');
        $('#txtProveedorCentral').prop('display', 'none');
        $('#txtProveedorCentral').css('display', 'none');

        $('#cmbPresentacion').prop('display', 'block');
        $('#cmbPresentacion').css('display', 'block');
        $('#txtPresentacion').prop('display', 'none');
        $('#txtPresentacion').css('display', 'none');

        $('#txtProveedor').prop('display', 'block');
        $('#cmbProveedor').prop('display', 'block');
        $('#lbCodProd').css('display', 'block');
        $('#txtCodProd').css('display', 'block');
        $('#txtCodProd').val('');
        $('#cmbUentrada').prop('disabled', false);
        $('#cmbUsalida').prop('disabled', false);
        $('#txtUempaque').prop('disabled', false);
        $('#row_CodigoKey').css('display', 'none');
        $('#row_CodigoAbasto').css('display', 'none');
        $('#row_AplicacionSubFam').css('display', 'block');
        $('#col_lbVigencia').css('display', 'block');
        $('#col_tbVigencia').css('display', 'block');
        $('#rdpVigencia').prop('disabled', true);
        $('#col_lbVigenciaFin').css('display', 'block');
        $('#col_tbVigenciaFin').css('display', 'block');
        $('#rdpVigenciaFin').prop('disabled', true);
        $('#row_CausaDesabasto').css('display', 'none');
        $('#cmbCausaDesabasto').val(0);
        $('#divPedidosRefAbasto').css('display', 'none');
        $('#tblPedidoDesabastecido > tbody').empty();
        $('#divMotivoClienteSolicita').css('display', 'block');
        LlenarListaPrecios_Default(0, function () { });
        // Clientes Exclusivos
        $('#tbl_ClientesExclusivos > tbody').empty();
        $('#btnAgregarClienteListado').prop('disabled', false);
        $('#txtTipoProducto').prop('disabled', true);
        $('#txtFactorConversion').prop('disabled', false);




    },

    Preparar_Interface: function (cmbCategorias) {
        $('#SPINNER_CL').css('display', 'block');
        $('#divMotivo_Area_1').css('display', 'none');
        $('#divMotivo_Area_2').css('display', 'none');
        $('#divMotivo_Area_3').css('display', 'none');
        $('#divArea_Guardar').css('display', 'none');
        $('#row_AbstoLocal').css('display', 'none');

        CL.Inicia_Formulario_Desabasto(function () {
            CL.Deshabilita_Op1_DatosGenerales(function () {
                Cargar_cmbCausaDesabasto(0, function () {
                    Cargar_cmbTipoProducto(0, function () {
                        Cargar_cmbPresentacion(function () {
                            Cargar_cmbPresentacionProv(function () {
                                CLIndex.Cargar_cmbProveedores(function () {
                                    Cargar_cmbUnidadEntrada(0, function () {
                                        Cargar_cmbUnidadSalida(0, function () {
                                            Cargar_cbmUnindadMedidaSAT(0, function () {
                                                Cargar_cmbSATProductosServicios(0, function () {
                                                    // LlenarComboProductoFamiliaCte
                                                    Cargar_cmbProductoFamiliaCte(0, 0, function (Id_Familia) {
                                                        Cargar_cmbProductoSubFamilia(Id_Familia);

                                                        switch (cmbCategorias) {
                                                            case "0":
                                                                break;
                                                            case "1": // Activacion
                                                                $('#divMotivo_Area_1').css('display', 'block');
                                                                $('#divActivacion').css('display', 'block');
                                                                $('#divArea_Guardar').css('display', 'block');
                                                                Interface.Inicializa_ActivacionCodigoDesabasto();
                                                                break;
                                                            case "2": // Abasto Local
                                                                $('#divArea_Guardar').css('display', 'block');
                                                                Interface.Inicializa_CodigoCentralConAbastoLocal();
                                                                break;
                                                            case "3": // Solicitud
                                                                $('#divArea_Guardar').css('display', 'block');
                                                                $('#divMotivo_Area_1').css('display', 'block');
                                                                $('#divActivacion').css('display', 'block');
                                                                Interface.Inicializa_Opcion_3();
                                                                Interface.Inicializa_SolicitudCliente();
                                                                break;
                                                            case "4": // Activacion
                                                                $('#divMotivo_Area_1').css('display', 'block');
                                                                $('#divActivacion').css('display', 'block');
                                                                $('#divArea_Guardar').css('display', 'block');
                                                                Interface.Inicializa_ActivacionCodigoDesabasto();
                                                                break;
                                                        }
                                                        $('#SPINNER_CL').css('display', 'none');
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }
}



function contiene(buscado) {
    var Res = 0;
    var txtProductosPadre = $('#txtProductosMismoPadre').val();
    try {
        var string_variable = '...' + txtProductosPadre;
        Res = string_variable.search(buscado);
    }
    catch (e) {
        alert(e.toString());
    }
    if (Res <= 0) {
        Res = false;
    } else {
        Res = true;
    }
    return Res;
}

//  DESPLIEGA LOS DATOS DEL PRODUCTO
function Producto_Desplegar_Informacion(producto, CALLBACK_Exito) {
    console.log(producto);

    CL.Deshabilita_Op1_DatosGenerales(function () { });

    $('#hfId_Prd').val(producto._Id_Prd);
    $('#hfPrd_Descripcion').val(producto._Prd_Descripcion);
    $('#txtSearch').val(producto._Id_Prd + ' - ' + producto._Prd_Descripcion);
    $('#txtPresentacion').val(producto._Prd_Presentacion);
    $('#txtTipoProducto').val(producto._Id_Ptp);
    $('#TextId_Prd').val(producto._Id_Prd);

    //Se agrega valor de proveedor central desde CU
    $('#txtProveedorCentral').val(producto._Prd_NomProvCentral);

    $('#cmbProveedor').val(producto._Id_Pvd);
    var cmbProveedor = producto._Id_Pvd

    //var cmbProveedor = .val();
    //Motivo para la Compra Local
    var cmbCategorias = $('#cmbCategorias').val();

    if (producto._Prd_Unico == 0) {
        $('#txtCodProd').val('');
        console.log('producto._Prd_Unico' + producto._Prd_Unico);
    } else {
        $('#txtCodProd').val(producto._Prd_Unico);
        console.log('producto._Prd_Unico' + producto._Prd_Unico);
    }

    ajax_MaximoId(producto._Id_Prd, cmbCategorias, cmbProveedor, function (Str) {
        $('#txtCodigoUsadoProd').val(Str);
        $('#txtCodProd').val(Str);
        $('#lblTituloProducto').val(Str + ' - ' + $('TextPrd_Descrpcion').val());
        $('#txtCodigoUsadoProd').prop('disabled', true);
        $('#txtCodProd').prop('disabled', true);
    });
    var txtCodProd = $('#txtCodProd').val();
    var txtCodProd = $('#txtSearch').val();
    $('#lblTituloProducto').val(txtCodProd + ' ' + txtCodProd);
    $('#TextPrd_Descrpcion').val(producto._Prd_Descripcion);
    $('#cmbTipoProducto').val(producto._Id_Ptp);
    $('#lblId_Prd').val(producto._Id_Prd);
    $('#lblCodProd').val(producto._Prd_Unico);
    $('#chkActivoAbasto').Checked = producto._Prd_Activo;
    $('#lblPrd_Descrpcion').val(producto._Prd_Descripcion);
    $('#lblPrd_DescrpcionAbasto').val(producto._Prd_Descripcion);
    //$('#lblTituloProducto').val(string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion));
    $('#lblTituloProducto').val(producto.Id_Prd + " - " + producto.Prd_Descripcion);
    console.log('CODIGO PRODUCTO :' + producto.Id_Prd + " - " + producto.Prd_Descripcion)
    $('#cmbFam').val(0);
    console.log('Familia: ' + producto._Id_Fam);

    //let IdAplicacion = producto._Aplicacion;
    let IdAplicacion = producto._Aplicacion.substring(0, 2);
    IdAplicacion = parseInt(IdAplicacion);
    if (isNaN(IdAplicacion)) {
        $('#cmbFam').val(0);
    } else {
        $('#cmbFam').val(IdAplicacion);
    }

    let IdSubFamilia = producto._Subfamilia.substring(3, 5);
    IdSubFamilia = parseInt(IdSubFamilia);
    if (isNaN(IdSubFamilia)) {
        IdSubFamilia = 0;
    } else {
    }
    if (IdAplicacion > 0) {
        Cargar_cmbProductoSubFamilia(IdAplicacion, function () {
            if (IdSubFamilia > 0) {
                $('#cmbSubFam').val(IdSubFamilia);
            } else {
                $('#cmbSubFam').val(0);
            }
        });
    } else {
        $('#cmbSubFam').val(0);
    }

    if (cmbProveedor == null) {
        alertify.error('El producto NO se encontro en Catálogo Único, o la información esta incompleta.');
        return;
    }

    if (IdAplicacion == 0 || IdSubFamilia == 0) {
        alertify.error('El producto NO se encontro en Catálogo Único, o la información esta incompleta.');
        return;
    }

    console.log('Id_Fam: ' + producto._Id_Fam);
    var Id_cmbFam = $("#cmbFam").val();

    $('#cmbUentrada').val(producto._Prd_UniNe);
    $('#txtFactorConversion').val(producto._Prd_FactorConv);
    $('#cmbUsalida').val(producto._Prd_UniNs);
    $('#txtUempaque').val(producto._Prd_UniEmp);
    EstablecerLabelTituloProductoDesAbasto_();
    $('#txtCategoria').val(producto.Id_Cpr);

    // Se cargan los datos del SAT desde CU
    $('#cmbUnidadMedidaSATDesabasto').val(producto._Id_UnidadSAT);
    $('#ddlProdServicio_SATDesabasto').val(producto._Id_ClaveSAT);


    $('#Id_Cpr').val(producto._Id_Cpr);

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

function LlenarListaPrecios(Id_Prd, CALLBACK_Exito) {
    ProductoConsultaPrecios(Id_Prd, function (Lst) {
    });

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}



function addDaysToDate(date, days) {
    var res = new Date(date);
    res.setDate(res.getDate() + days);
    return res;
}




function LlenarListaPrecios_Default(Id_Prd, CALLBACK_Exito) {
    let cmbMotivo = $('#cmbCategorias').val();
    var Fecha = new Date();
    var FechaFin = new Date();
    var Dia = Fecha.getDate();
    Dia = parseInt(Dia);
    if (Dia <= 9) {
        Dia = '0' + Dia;
    }
    var Mes = 0;
    Mes = Fecha.getMonth() + 1;
    Mes = parseInt(Mes);
    if (Mes <= 9) {
        Mes = '0' + Mes;
    }
    Fecha = Dia + '/' + Mes + '/' + Fecha.getFullYear();

    $('#rdpVigencia').val(Fecha);

    if (cmbMotivo == "1") {
        var FechaFinal = addDaysToDate(FechaFin, 15);
        var DiaFin = FechaFinal.getDate();
        var MesFin = FechaFinal.getMonth() + 1;
        if (DiaFin <= 9) {
            DiaFin = '0' + DiaFin
        }
        if (MesFin <= 9) {
            MesFin = '0' + MesFin
        }


        FechaFin = DiaFin + '/' + MesFin + '/' + FechaFinal.getFullYear();
        $('#rdpVigenciaFin').val(FechaFin);
    } else {
        var Año = FechaFin.getFullYear(FechaFin.getFullYear() + 1);
        Año = Año + 1;
        FechaFin = Dia + '/' + Mes + '/' + Año;
        $('#rdpVigenciaFin').val(FechaFin);
    }

    $('#tbl_rgPrecios > tbody').empty();
    var i = 1;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio de Lista (Venta)</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 2;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Costo</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 3;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio AAA código compra local</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    //row.append($('<td class="text-center">').append(
    //    '<i class="fa fa-pencil fa-2x clickable" ' +
    //    'onclick="PrecioProducto.Editar(this);" ' +
    //    'data-id=' + i + '>' +
    //    '</i>'
    //));
    $('#tbl_rgPrecios > tbody').append(row);

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

function Calcular_Titulo() {
    //Motivo para la Compra Local
    var cmbCategorias = $('#cmbCategorias').val();
    switch (cmbCategorias) {
        case "1":
            break;
        case "2":
            var lblTituloProducto = $('#TextId_Prd').val() + ' - ' + $('#TextPrd_Descrpcion').val();
            $('#lblTituloProducto').text(lblTituloProducto);
            console.log('CODIGO PRODUCTO :' + lblTituloProducto);
            break;
        case "3":
            break;
    }
}

// Seleccion de Producto 
function btnProductoBusqueda_Seleccion(obj) {
    /*
    var Id_Prd = $(obj).data('id_prd');
    Desplegar_Informacion_DeProducto(Id_Prd);
    */
}
// 
function btnClienteExc_Remover(Obj) {
    var row = $(Obj).data('row');
    //rlert(row);
    $('#Row_ClientesExc_' + row).remove();
}
// Agregar Cliente al Listado
function btnBuscarCliente_Agregar(Obj) {
    var tipo_cliente = $(Obj).data('tipo_cte');
    var id_cte = $(Obj).data('id_cte');
    var cte_nombre = $(Obj).data('cte_nombre');
    CONT_ClientesExc = CONT_ClientesExc + 1;
    var row = $('<tr id="Row_ClientesExc_' + CONT_ClientesExc + '" >');

    row.append($('<td>').append(
        '<label id="lstCE_TipoCliente_' + CONT_ClientesExc + '">' + tipo_cliente + '</label>'
    ));

    row.append($('<td>').append(
        '<label id="lstCE_IdCte_' + CONT_ClientesExc + '">' + id_cte + '</label>'
    ));

    row.append($('<td>').append(
        '<label id="lstCE_Nombre_' + CONT_ClientesExc + '">' + cte_nombre + '</label>'
    ));

    row.append($('<td class="text-center">').append(
        '<i class="fa fa-times fa-2x clickable" ' +
        'data-row="' + CONT_ClientesExc + '" ' +
        'onclick="btnClienteExc_Remover(this);" ' +
        '>' +
        '</i>'
    ));
    $('#tbl_ClientesExclusivos > tbody').append(row);
    $('#modalBuscarCliente').modal('hide');
}


var CLIndex = {
    //Proveedores Locales
    Proveedores_Ajax: function (IdProveedorLocal, CallBack_Exito) {
        $.ajax({
            //url: _ApplicationUrl + 'sianwebpruebas/api/CL_Main/spProveedores_ComboCompraLocal',
            url: _ApplicationUrl + '/api/CL_Main/spProveedores_ComboCompraLocal',
            data: {
                IdProveedorLocal: IdProveedorLocal
            },
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;
            if (Estado == 1) {
                if (CallBack_Exito) {
                    CallBack_Exito(Datos);
                }
            } else {
                alertify.error('Error en CL_Ajax.Proveedores_Ajax()');
            }
        }).fail(function (jqXHR, textStatus, error) {
            //alertify.error(jqXHR.responseText);
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },


    Cargar_cmbProveedores: function (CALLBACK_Exito) {
        CLIndex.Proveedores_Ajax(1, function (lst) {
            $('#cmbProveedor').empty();
            //$('#ddlProveedorLocal').append('<option value="" >-- Seleccione -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#cmbProveedor').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        });

    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// ready
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    $('#txtCDINombre').text(sCDI_Nombre);
    $('#hfCDI').val(iCDI);

    //$('.datepicker').Zebra_DatePicker({
    //    format: 'd/m/Y'
    //});

    Cargar_cmbMotivos(0);

    //Cargar_cmbProveedores(0);

    // CAUSA DESABASTO
    $(document.body).on('change', "#cmbCausaDesabasto", function (e) {
        let IdCausa = $("#cmbCausaDesabasto").val();
        IdCausa = parseInt(IdCausa);
        if (isNaN(IdCausa)) {
            IdCausa = 0;
        }
        let Id_Prd = $('#TextId_Prd').val();
        switch (IdCausa) {
            case 0:
                $('#tblPedidoDesabastecido > tbody').empty();
                $('#divPedidosRefAbasto').css('display', 'none');
                break;
            case 1:
                $('#divPedidosRefAbasto').css('display', 'none');
                break;
            case 2:
                $('#divPedidosRefAbasto').css('display', 'none');
                break;
            case 3:
                $('#divPedidosRefAbasto').css('display', 'none');
                break;
            case 4:
                $('#divPedidosRefAbasto').css('display', 'none');
                break;
        }
    });

    // Evento Change - cmdFamilia o Aplicacion de producto
    $(document.body).on('change', "#cmbFam", function (e) {
        var Id = $("#cmbFam").val();
        Cargar_cmbProductoSubFamilia(Id);
    });

    // Tipo 3 - Evento Change en Aplicacion
    $(document.body).on('change', "#cmbAplicacionSoli", function (e) {
        $('#SPINNER_CL').css('display', 'block');
        $('#btnSeleccionaProdSol').removeClass('btn-default');
        $('#btnSeleccionaProdSol').addClass('btn-primary');
        let Id = $("#cmbAplicacionSoli").val();
        CL_Ajax.CargaComob_SubFamilia(Id, function (Lst) {
            //CALLBACK_Exito
            $('#cmdSubFamiliaSoli').empty();
            $('#cmdSubFamiliaSoli').append('<option value="0" >-- Seleccione -- </option>');
            for (let i = 0; i < Lst.length; i++) {
                $('#cmdSubFamiliaSoli').append('<option value="' + Lst[i].Id + '" >' + Lst[i].Descripcion + '</option>');
            }
            setTimeout(function () { $('#SPINNER_CL').css('display', 'none'); }, 1000);
        }, function () {
            setTimeout(function () { $('#SPINNER_CL').css('display', 'none'); }, 1000);
        });
    });

    $(document.body).on('change', "#cmdSubFamiliaSoli", function (e) {
        $('#btnSeleccionaProdSol').removeClass('btn-default');
        $('#btnSeleccionaProdSol').addClass('btn-primary');
    });

    $(document.body).on('change', "#cmbTipoProducto", function (e) {
        var cmbTipoProducto = $("#cmbTipoProducto").val();
        $('#txtTipoProducto').val(cmbTipoProducto);
    });

    // Evento change - Motivo 
    $(document.body).on('change', "#cmbCategorias", function (e) {
        var cmbCategorias = $("#cmbCategorias").val();
        Interface.Preparar_Interface(cmbCategorias);
    });

    $(document.body).on('change', "#cmbProductosHabiliCompraLocal", function (e) {
        $('#btnProducto_AvastoLocal_Ok').removeClass('btn-default');
        $('#btnProducto_AvastoLocal_Ok').addClass('btn-primary');
    });

    // Evento Change Proveedor
    $(document.body).on('change', "#cmbProveedor", function (e) {
        var cmbCategorias = $('#cmbCategorias').val();
        var cmbProveedor = $("#cmbProveedor").val();
        switch (cmbCategorias) {
            case "1":
                $("#txtProveedor").val(cmbProveedor);
                var TextId_Prd = $('#TextId_Prd').val();
                TextId_Prd = TextId_Prd.trim();
                if (TextId_Prd.length <= 0) {
                    alertify.error('No ha seleccionado el producto.');
                    $('#txtProveedor').val('');
                    $('#cmbProveedor').val('-1');
                    return;
                }
                EstablecerLabelTituloProductoDesAbasto_();
                var txtBxCodigo = $('#txtCodProd').val();
                var txtProductosPadre = $('#txtProductosMismoPadre').val();
                //console.log('txtBxCodigo:' + txtBxCodigo);                    
                //console.log(txtProductosPadre);
                Res = txtProductosPadre.search(txtBxCodigo);
                //console.log(txtBxCodigo);
                if (Res != -1) {
                    //alert('Ya existe una solicitud de compra con el mismo Producto(1).');
                    alertify.error('Ya existe una solicitud de compra con el mismo Producto(1).');
                    $('#txtProveedor').val('');
                    $('#cmbProveedor').val('-1');
                    return;
                } else {
                    alertify.success('Código de producto: ' + txtBxCodigo);
                }
                break;
            case "2":
                var cmbProveedor = $("#cmbProveedor").val();
                $("#txtProveedor").val(cmbProveedor);
                EstablecerLabelTituloProductoDesAbasto_2();
                break;
            case "3":
                var cmbProveedor = $("#cmbProveedor").val();
                $("#txtProveedor").val(cmbProveedor);
                EstablecerLabelTituloProductoDesAbasto_2();
                break;
            case "4":
                $("#txtProveedor").val(cmbProveedor);
                var TextId_Prd = $('#TextId_Prd').val();
                TextId_Prd = TextId_Prd.trim();
                if (TextId_Prd.length <= 0) {
                    alertify.error('No ha seleccionado el producto.');
                    $('#txtProveedor').val('');
                    $('#cmbProveedor').val('-1');
                    return;
                }
                EstablecerLabelTituloProductoDesAbasto_();
                var txtBxCodigo = $('#txtCodProd').val();
                var txtProductosPadre = $('#txtProductosMismoPadre').val();
                //console.log('txtBxCodigo:' + txtBxCodigo);                    
                //console.log(txtProductosPadre);
                Res = txtProductosPadre.search(txtBxCodigo);
                //console.log(txtBxCodigo);
                if (Res != -1) {
                    //alert('Ya existe una solicitud de compra con el mismo Producto(1).');
                    alertify.error('Ya existe una solicitud de compra con el mismo Producto(1).');
                    $('#txtProveedor').val('');
                    $('#cmbProveedor').val('-1');
                    return;
                } else {
                    alertify.success('Código de producto: ' + txtBxCodigo);
                }
                break;
        }
    });

    //
    $('#lnkHistorialPreciosCodigo').click(function (e) {
        var TextId_Prd = $('#TextId_Prd').val();
        if (TextId_Prd == '') {
            alertify.error("Debe seleccionar el producto para continuar.");
            return;
        } else {
            $('#modal_HistorialPrecios').appendTo("body").modal('show');
        }
    });



    // Lanza Modal Buscar Cliente
    $('#btnAgregarClienteListado').click(function (e) {
        console.log(e);
        $('#modalBuscarCliente').appendTo("body").modal('show');
    });

    $('#btnBuscarCliente_Ok').click(function (e) {
        TextoBuscar = $('#BuscarCliente_Texto').val();
        Cliente_BuscarCliente(TextoBuscar, function (Datos) {
            if (Datos.length > 0) {
                $('#BuscarCliente_Lista_RE').text(Datos.length + ' Registros encontrados');
                var RegistrosEncontrados = Datos[0].RegistrosEcontrados;
                $('#tblBuscarCliente_Lista > tbody').empty();
                for (var i = 0; i < Datos.length; i++) {
                    var row = $('<tr>');
                    row.append($('<td>').append(Datos[i].Cte));
                    row.append($('<td>').append(Datos[i].RFC));
                    row.append($('<td>').append(Datos[i].NomComercial));
                    row.append($('<td>').append(Datos[i].tipoCliente));
                    if (Datos[i].Activo == 1) {
                        row.append($('<td>').append(
                            '<button ' +

                            ' data-id_cte="' + Datos[i].Cte + '" ' +
                            ' data-cte_nombre="' + Datos[i].NomComercial + '" ' +
                            ' data-tipo_cte="' + Datos[i].tipoCliente + '" ' +
                            ' onclick="btnBuscarCliente_Agregar(this);" ' +
                            'class="button">' +
                            '<span>Agregar</span>' +
                            '</button>'
                        ));
                    } else {
                        row.append($('<td>').append());
                    }
                    $('#tblBuscarCliente_Lista tbody').append(row);
                }
            } else {
                $('#tblBuscarCliente_Lista > tbody').empty();
                $('#BuscarCliente_Lista_RE').text('No se Encontraro Registros');
            }
        });
    });

    $('#btnBuscarProducto').click(function (e) {
        $('#modalBuscarProducto').appendTo("body").modal('show');
    });

    //
    $('#btnEjecutarBusqueda').click(function (e) {
        var Termino = $('#tbTextoBuscar').val();
        ajax_CL_BusquedaProducto(Termino, $('#spinner_Buscar'), function (Lst) {
            //console.log(Lst);
            $('#tblProductoEncontrados > tbody').empty();
            for (var i = 0; i < Lst.length; i++) {
                var row = $('<tr>');
                row.append($('<td style="text-align:left;">').append(
                    Lst[i]._Id_Prd
                ));
                row.append($('<td>').append(
                    Lst[i]._Prd_Descripcion
                ));
                row.append($('<td>').append(
                    '<button type="button" ' +
                    'data-id_prd=' + Lst[i]._Id_Prd + ' ' +
                    'class="btn btn-primary btn-xs" ' +
                    //'onclick="btnProductoBusqueda_Seleccion(this)" ' +
                    'onclick="Producto.btnSeleccion(this)" ' +
                    '> Selección' +
                    '</button>'
                ));
                $('#tblProductoEncontrados > tbody').append(row);
            }
        });
    });

    $('#btnProducto_AvastoLocal_Ok').click(function (e) {
        $('#SPINNER_CL').css('display', 'block');
        var Id_Prd = $('#cmbProductosHabiliCompraLocal').val();
        Id_Prd = parseInt(Id_Prd);
        if (isNaN(Id_Prd)) {
            Id_Prd = 0;
        }
        if (Id_Prd <= 0) {
            $('#SPINNER_CL').css('display', 'none');
            alertify.error("Debe seleccionar le producto.");
            return;
        }
        $('#btnProducto_AvastoLocal_Ok').prop('disabled', true);
        Producto.DesplegarInfo(Id_Prd, function () {
            Calcular_Titulo();
            $('#btnProducto_AvastoLocal_Ok').prop('disabled', false);
            $('#btnProducto_AvastoLocal_Ok').removeClass('btn-primary');
            $('#btnProducto_AvastoLocal_Ok').addClass('btn-default');
            $('#SPINNER_CL').css('display', 'none');
        });
    });

    //if (iCDI == '') {

    //}
    /*
        $("#ddlProdServicio_SATDesabasto").combobox();
        $("#toggle").on("click", function () {
            $("#ddlProdServicio_SATDesabasto").toggle();
        });
    */

});