<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="CatClientesV3.aspx.cs"
    MasterPageFile="~/MasterPage/MasterPageNewDesign.Master"
    Inherits="SIANWEB.CatClientesV3" %>

<asp:Content runat="server" ContentPlaceHolderID="CPH">
    <style>
        #spinner {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 0.2s;
        }
        #spinner img {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%);
        }
        #spinner {
            visibility: hidden;
            opacity: 0;
        }
        #spinner.show {
            visibility: visible;
            opacity: 1;
        }

        .tablesize {
            font-size:14px;
        }
        .tablesize th, .tablesize td {
            white-space: nowrap;
            padding: 5px 10px 5px 10px;
            vertical-align:middle;
            text-align:center;
        }

        /*datepicker*/
        .dp__selection_preview { display: none; }
        .dp__action_buttons {
            width: 100% !important;
            display: inline-flex;
        }
        .dp__select {
            margin-left: auto !important;
            color: mediumslateblue !important;
        }
        .dp__input {
            font-size:14px !important;
        }
        /*fin datepicker*/

        .btn-icon {
            background-color: transparent;
        }

        .nav-link.active {
            font-weight: 700;
            border-top-color: orange !important;
            border-top-width: initial;
        }
        .vue-dropdown {
            z-index: 9999 !important;
        }
        .vue-select {
            width: 100% !important;
            border: 1px solid #ced4da !important;
        }
        .vue-input input {
            font-size: .9rem !important;
        }
        .vue-dropdown-item.highlighted {
            background-color: #039fdb !important;
        }
        .vue-dropdown-item {
            font-size: 13px;
            margin-left:10px;
            padding: 3px !important;
        }

        .form-control, label {
            font-size: .9rem !important;
        }
        .centerCheckbox {
            justify-content: center;
            display: flex;
            align-items: center;
        }
        .card-title {
            font-size: 15px !important;
            font-style: italic;
        }
    </style>

    

    <div id="app_catclientes">
        <div ref="spinner" id="spinner">
            <img src="Imagenes/ajax-loader.gif" />
        </div>

        <div class="container-fluid mt-4">
            <div class="row align-items-center">
                <div class="col" v-if="sharedData.clienteSeleccionado">
                    <label>Fecha &uacute;ltima modificaci&oacute;n: <b>{{ sharedData.fechaUltimaModificacion }}</b></label>
                    <br />
                    <label>Usuario: <b>{{ sharedData.usuario }}</b></label>
                </div>
                <div class="col d-inline-flex justify-content-end">
                    <div class="form-group">
                        <label>Centro de distribuci&oacute;n</label>
                        <select class="form-control" @change="cambiarCentroDistribucion" v-model="sharedData.centroDistribucion">
                            <option v-for="u in sharedData.centrosDistribucion" :value="u.Id">
                                {{ u.Descripcion }}
                            </option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row align-items-end mt-4 mt-md-0">
                <div class="col-md-4">
                    <div class="form-group mb-0">
                        <label>Cliente</label>
                        <v-select
                            :searchable="true"
                            model-value=""
                            v-model="sharedData.cliente"
                            :clear-on-close="true"
                            placeholder="-- seleccionar --"
                            search-placeholder="Escribe para buscar"
                            class="form-control"
                            :close-on-select="true"
                            @selected="seleccionarCliente"
                            ref="cliente"
                            @focus="borrarBuscadorCliente"
                            :options="sharedData.clientes"></v-select>
                    </div>
                </div>
                <div class="col d-inline-flex justify-content-end mt-3 mt-md-0">
                    <button class="btn btn-secondary" @click="nuevoCliente($event)">Nuevo</button>
                    <button id="guardarCliente" v-if="sharedData.permisoGuardar && !sharedData.clienteSeleccionado" class="btn btn-success ml-3" @click="guardarNuevoCliente($event)">Guardar</button>
                    <button id="modificarCliente" v-if="sharedData.permisoModificar && sharedData.clienteSeleccionado" class="btn btn-success ml-3" @click="guardarNuevoCliente($event)">Guardar</button>
                </div>
            </div>
        </div>
        <div class="container-fluid mt-4">
            <div class="row">
                <div class="col">
                    <nav>
                        <div class="nav nav-tabs" id="myTab" role="tablist">
                            <a class="nav-item nav-link active"
                                id="datos-generales-tab"
                                data-toggle="tab"
                                href="#nav-datos-generales"
                                role="tab"
                                aria-controls="nav-datos-generales"
                                aria-selected="true">Datos generales</a>
                            <a class="nav-item nav-link"
                                id="direccion-entrega-tab"
                                data-toggle="tab"
                                href="#nav-direccion-entrega"
                                role="tab"
                                aria-controls="nav-direccion-entrega"
                                aria-selected="false">Direcciones de entrega</a>
                            <a class="nav-item nav-link"
                                id="cliente-territorio-tab"
                                data-toggle="tab"
                                href="#nav-cliente-territorio"
                                role="tab"
                                aria-controls="nav-cliente-territorio"
                                aria-selected="false">Cliente Territorio</a>
                            <a class="nav-item nav-link"
                                id="cobranza-tab"
                                data-toggle="tab"
                                href="#nav-cobranza"
                                role="tab"
                                aria-controls="nav-cobranza"
                                aria-selected="false">Cobranza</a>
                            <a class="nav-item nav-link"
                                id="campos-usuario-tab"
                                data-toggle="tab"
                                href="#nav-campos-usuario"
                                role="tab"
                                v-if="sharedData.mostrarTabBennets"
                                aria-controls="nav-campos-usuario"
                                aria-selected="false">Campos de usuario</a>
                        </div>
                    </nav>
                    <div class="tab-content">
                        <!--DATOS GENERALES-->
                        <div class="tab-pane fade show active p-3 mb-4" id="nav-datos-generales" role="tabpanel" aria-labelledby="datos-generales-tab">
                            <div class="row align-items-center">
                                <div class="col-12 col-md-3">
                                    <div class="form-group">
                                        <label>Clave</label>
                                        <input min="0" type="number" @input="maxLength($event, 9)" class="form-control" v-model="datosGenerales.idCliente" />
                                    </div>
                                </div>
                                <div class="col justify-content-end d-inline-flex">
                                    <div class="form-check">
                                        <input type="checkbox" @change="deshabilitarCliente" v-model="datosGenerales.activo" />
                                        <label class="form-check-label ml-3">Activo</label>
                                    </div>
                                    <div class="form-check">
                                        <input type="checkbox" v-model="datosGenerales.sucursal" />
                                        <label class="form-check-label ml-3">Sucursal</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Raz&oacute;n social</label>
                                        <input type="text" id="dg_razonSocial" onpaste="return false" class="form-control" v-model="datosGenerales.razonSocial" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nombre comercial</label>
                                        <input type="text" id="dg_nombreComercial" onpaste="return false" class="form-control" v-model="datosGenerales.nombreComercial" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col">
                                    <fieldset :disabled="sharedData.clienteSeleccionado && !sharedData.permisoModificarTerritorios">
                                        <div class="form-group">
                                            <label>UEN</label>
                                            <select class="form-control" id="dg_uen" @change="seleccionarUenDatosGenerales($event)" v-model="datosGenerales.uen">
                                                <option v-for="u in sharedData.uens" :key="u.Id" :value="u.Id">
                                                    {{ u.Descripcion }}
                                                </option>
                                            </select>
                                            <div class="invalid-feedback">* Campo requerido</div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col">
                                    <fieldset :disabled="sharedData.clienteSeleccionado && !sharedData.permisoModificarTerritorios">
                                        <div class="form-group">
                                            <label>Segmentos</label>
                                            <select class="form-control" id="dg_segmento" v-model="datosGenerales.segmento">
                                                <option v-for="u in datosGenerales_sharedData.segmentos" :value="u.Id">
                                                    {{ u.Descripcion }}
                                                </option>
                                            </select>
                                            <div class="invalid-feedback">* Campo requerido</div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6 col-lg-3">
                                    <fieldset>
                                        <div class="form-group">
                                            <label>Tipo de cliente</label>
                                            <select class="form-control" id="dg_tipoCliente" @change="consultaTipoDeCliente($event)" v-model="datosGenerales.tipoCliente">
                                                <template v-for="u in datosGenerales_sharedData.tipoDeClientes">
                                                    <option :value="u.Id">
                                                        <span v-if="u.Id > 0">{{u.Id}} - </span> {{ u.Descripcion }}
                                                    </option>
                                                </template>
                                            </select>
                                            <div class="invalid-feedback">* Campo requerido</div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-6 col-lg-3">
                                    <fieldset :disabled="(datosGenerales.tipoCliente != 3 && datosGenerales.tipoCliente != -1)">
                                        <div class="form-group">
                                            <label>Cuenta corporativa</label>
                                            <select class="form-control" id="dg_cuentaCorporativa" v-model="datosGenerales.cuentaCorporativa">
                                                <template v-for="u in datosGenerales_sharedData.cuentasCorporativas">
                                                    <option :value="u.Id">
                                                        <span v-if="u.Id > 0">{{u.Id}} - </span> {{ u.Descripcion }}
                                                    </option>
                                                </template>
                                            </select>
                                            <div class="invalid-feedback">* Campo requerido</div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Contacto</label>
                                        <input type="text" class="form-control" @keypress="alphanumeric($event)" v-model="datosGenerales.contacto" />
                                    </div>
                                </div>
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Email</label>
                                        <input type="text" class="form-control" v-model="datosGenerales.email" />
                                    </div>
                                </div>
                            </div>
                            <h6 class="mt-4">Datos fiscales</h6>
                            <div class="row mt-3">
                                <div class="col-md-4 col-lg-6">
                                    <div class="form-group">
                                        <label>Calle</label>
                                        <input type="text" class="form-control" id="dg_calle" @keypress="alphanumeric($event)" v-model="datosGenerales.calle" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-2">
                                    <div class="form-group">
                                        <label>Colonia</label>
                                        <input type="text" class="form-control" id="dg_colonia" @keypress="alphanumeric($event)" v-model="datosGenerales.colonia" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-6 col-md-2">
                                    <div class="form-group">
                                        <label>N&uacute;mero</label>
                                        <input type="text" class="form-control" id="dg_numero" @keypress="alphanumeric($event)" v-model="datosGenerales.numero" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-6 col-md-2">
                                    <div class="form-group">
                                        <label>CP</label>
                                        <input type="number" class="form-control" id="dg_cp" v-model="datosGenerales.cp" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Pa&iacute;s</label>
                                        <select autocomplete="??" id="dg_pais" class="form-control" @change="seleccionarPaisDatosGenerales($event)" v-model="datosGenerales.pais">
                                             <option v-for="u in sharedData.paises" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <%--<div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Estado</label>
                                        <input type="text" class="form-control" id="dg_estado" @keypress="alphanumeric($event)" v-model="datosGenerales.estado" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>--%>
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Estado</label>
                                         <select autocomplete="??" id="dg_estado" class="form-control" v-model="datosGenerales.estado">
                                              <option v-for="u in datosGenerales_sharedData.estados" :value="u.Id">
                                                 {{ u.Descripcion }}
                                             </option>
                                         </select>
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Municipio</label>
                                        <input type="text" class="form-control" id="dg_municipio" @keypress="alphanumeric($event)" v-model="datosGenerales.municipio" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Tel&eacute;fonos</label>
                                        <input type="text" class="form-control" v-model="datosGenerales.telefonos" />
                                    </div>
                                </div>
                                
                            </div>
                            <div class="row">
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>RFC</label>
                                        <input type="text" class="form-control" id="dg_rfc" @keypress="alphanumeric($event)" v-model="datosGenerales.rfc" />
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Regimen fiscal</label>
                                        <select class="form-control" v-model="datosGenerales.regimenFiscal">
                                             <option v-for="u in datosGenerales_sharedData.regimenFiscal" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Asignaci&oacute;n de pedido</label>
                                        <select class="form-control" v-model="datosGenerales.asignacionDePedido">
                                             <option v-for="u in datosGenerales_sharedData.asignacionDePedido" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--DIRECCION ENTREGA-->
                        <div class="tab-pane fade p-3 mb-4" id="nav-direccion-entrega" role="tabpanel" aria-labelledby="direccion-entrega-tab">
                            <div class="row mt-0 mt-lg-3 align-items-center">
                                <div class="col-lg-7 col-xl-8">
                                    <div class="row align-items-center">
                                        <div class="col-6 col-lg-4">
                                            <div class="form-group">
                                                <label>Ruta</label>
                                                <select class="form-control" id="de_ruta" v-model="direccionEntrega.ruta">
                                                    <option v-for="u in direccionEntrega_sharedData.rutas" :value="u.Id">
                                                        {{ u.Descripcion }}
                                                    </option>
                                                </select>
                                                <div class="invalid-feedback">* Campo requerido</div>
                                            </div>
                                        </div>
                                        <div class="col col-lg-8 text-right">
                                            <div class="form-check">
                                                <input type="checkbox" class="form-check-input" @change="cloneInfo($event)" v-model="direccionEntrega.direccionFiscal" />
                                                <label class="form-check-label">Direcci&oacute;n fiscal</label>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-12 col-lg-6">
                                            <div class="form-group">
                                                <label>Calle</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.calle" />
                                            </div>
                                        </div>
                                        <div class="col-6 col-lg-2">
                                            <div class="form-group">
                                                <label>Colonia</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.colonia" />
                                            </div>
                                        </div>
                                        <div class="col-3 col-lg-2">
                                            <div class="form-group">
                                                <label>N&uacute;mero</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.numero" />
                                            </div>
                                        </div>
                                        <div class="col-3 col-lg-2">
                                            <div class="form-group">
                                                <label>CP</label>
                                                <input type="number" class="form-control" v-model="direccionEntrega.cp" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-lg-2 mt-0">
                                        <div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Pa&iacute;s</label>
                                                <select autocomplete="??" id="de_pais" class="form-control" @change="seleccionarPaisDireccionEntrega($event)" v-model="direccionEntrega.pais">
                                                     <option v-for="u in sharedData.paises" :value="u.Id">
                                                        {{ u.Descripcion }}
                                                    </option>
                                                </select>
                                                <div class="invalid-feedback">* Campo requerido</div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Estado</label>
                                                <select autocomplete="??" id="de_estado" class="form-control" v-model="direccionEntrega.estado">
                                                     <option v-for="u in direccionEntrega_sharedData.estados" :value="u.Id">
                                                        {{ u.Descripcion }}
                                                    </option>
                                                </select>
                                                <div class="invalid-feedback">* Campo requerido</div>
                                            </div>
                                        </div>
                                        <%--<div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Estado</label>
                                                <input type="text" class="form-control"v-model="direccionEntrega.estado" />
                                            </div>
                                        </div>--%>
                                        <div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Municipio</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.municipio" />
                                            </div>
                                        </div>
                                        <div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Sector</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.sector" />
                                            </div>
                                        </div>
                                        <div class="col-6 col-lg-3">
                                            <div class="form-group">
                                                <label>Tel&eacute;fonos</label>
                                                <input type="text" class="form-control" v-model="direccionEntrega.telefono" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="card" style="border:0; background-color: whitesmoke;">
                                        <div class="card-body">
                                            <h6 class="card-title">Horario de recepci&oacute;n</h6>

                                            <em>Horario matutino</em>
                                            <div class="row mt-2 mb-2 align-items-center">
                                                <div class="col">
                                                    <date-picker
                                                        time-picker
                                                        mode-height="120"
                                                        :is-24="false"
                                                        select-text="seleccionar"
                                                        cancel-text="cancelar"
                                                        uid="horaAm1"
                                                        :start-time="direccionEntrega.horaAm1_startTime"
                                                        :max-time="{ hours: 11, minutes: 59 }"
                                                        :min-time="{ hours: 0, minutes: 0 }"
                                                        v-model="direccionEntrega.horaAm1">
                                                        <template #am-pm-button="{ toggle, value }">
                                                            <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                        </template>
                                                    </date-picker>
                                                </div>
                                                <div class="col-1 text-center">a</div>
                                                <div class="col">
                                                    <date-picker
                                                        time-picker
                                                        mode-height="120"
                                                        :is-24="false"
                                                        select-text="seleccionar"
                                                        cancel-text="cancelar"
                                                        uid="horaAm2"
                                                        :start-time="direccionEntrega.horaAm2_startTime"
                                                        :max-time="{ hours: 11, minutes: 59 }"
                                                        :min-time="{ hours: 0, minutes: 0 }"
                                                        v-model="direccionEntrega.horaAm2">
                                                        <template #am-pm-button="{ toggle, value }">
                                                            <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                        </template>
                                                    </date-picker>
                                                </div>
                                            </div>
                                            <em>Horario vespertino</em>
                                            <div class="row mt-2 mb-2 align-items-center">
                                                <div class="col">
                                                    <date-picker
                                                        time-picker
                                                        mode-height="120"
                                                        :is-24="false"
                                                        select-text="seleccionar"
                                                        cancel-text="cancelar"
                                                        uid="horaPm1"
                                                        :start-time="direccionEntrega.horaPm1_startTime"
                                                        :max-time="{ hours: 23, minutes: 59 }"
                                                        :min-time="{ hours: 12, minutes: 0 }"
                                                        v-model="direccionEntrega.horaPm1">
                                                        <template #am-pm-button="{ toggle, value }">
                                                            <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                        </template>
                                                    </date-picker>
                                                </div>
                                                <div class="col-1 text-center">a</div>
                                                <div class="col">
                                                    <date-picker
                                                        time-picker
                                                        mode-height="120"
                                                        :is-24="false"
                                                        select-text="seleccionar"
                                                        cancel-text="cancelar"
                                                        uid="horaPm2"
                                                        :start-time="direccionEntrega.horaPm2_startTime"
                                                        :max-time="{ hours: 23, minutes: 59 }"
                                                        :min-time="{ hours: 12, minutes: 0 }"
                                                        v-model="direccionEntrega.horaPm2">
                                                        <template #am-pm-button="{ toggle, value }">
                                                            <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                        </template>
                                                    </date-picker>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">                        
                                <%--<div class="col-3 col-lg-3">
                                    <div class="form-group">
                                        <label>Fax</label>
                                        <input type="text" class="form-control" v-model="direccionEntrega.fax" />
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="form-group">
                                        <label># Cuadrante</label>
                                        <input type="text" class="form-control" v-model="direccionEntrega.cuadrante"/>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row mt-3 align-items-end">
                                <div class="col">
                                    <span>Geolocalizaci&oacute;n</span>
                                    <img src="Img/geo.png" />
                                </div>
                                <div class="col d-inline-flex justify-content-end">
                                    <button v-if="direccionEntrega_sharedData.editMode" class="btn mr-3" @click="cancelarDireccionEntrega($event)">Cancelar edici&oacute;n</button>

                                    <button v-if="!direccionEntrega_sharedData.editMode" class="btn btn-primary" @click="agregarDireccionEntrega($event)">Agregar</button>
                                    <button v-else class="btn btn-warning" @click="actualizarDireccionEntrega($event)">Actualizar</button>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="table-responsive">
                                        <table class="table tablesize table-hover table-bordered table-sm">
                                            <thead>
                                                <tr>
                                                    <th scope="col"></th>
                                                    <th scope="col"></th>
                                                    <th scope="col">Ruta</th>
                                                    <th scope="col">Cons.</th>
                                                    <th scope="col">Calle</th>
                                                    <th scope="col">N&uacute;mero</th>
                                                    <th scope="col">CP</th>
                                                    <th scope="col">Pa&iacute;s</th>
                                                    <th scope="col">Estado</th>
                                                    <th scope="col">Colonia</th>
                                                    <th scope="col">Municipio</th>
                                                    <th scope="col">Sector</th>
                                                    <th scope="col">Tel&eacute;fono</th>
                                                    <%--<th scope="col">Fax</th>--%>
                                                    <th scope="col">Am 1</th>
                                                    <th scope="col">Am 2</th>
                                                    <th scope="col">Pm 1</th>
                                                    <th scope="col">Pm 2</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <template v-for="item in direccionEntrega_sharedData.tabla">
                                                    <tr :class="item.Edit ? 'table-warning' : ''">
                                                        <td>
                                                            <button class="btn btn-icon" @click="editarDireccionEntrega($event, item.Id)">
                                                                <i style="color:blue;" class="bi bi-pencil-square"></i>
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <button @click="eliminarDireccionEntrega($event, item.Id)" class="btn btn-icon">
                                                                <i style="color:red;" class="bi bi-x-lg"></i>
                                                            </button>
                                                        </td>
                                                        <td>{{ item.Ruta }}</td>
                                                        <td>{{ item.Id + 1 }}</td>
                                                        <td>{{ item.Calle }}</td>
                                                        <td>{{ item.Numero }}</td>
                                                        <td>{{ item.CP }}</td>
                                                        <td>{{ item.Pais }}</td>
                                                        <td>{{ item.Estado }}</td>
                                                        <td>{{ item.Colonia }}</td>
                                                        <td>{{ item.Municipio }}</td>
                                                        <td>{{ item.Sector }}</td>
                                                        <td>{{ item.Telefono }}</td>
                                                        <%--<td>{{ item.Fax }}</td>--%>
                                                        <td>{{ item.HoraAm1 }}</td>
                                                        <td>{{ item.HoraAm2 }}</td>
                                                        <td>{{ item.HoraPm1 }}</td>
                                                        <td>{{ item.HoraPm2 }}</td>
                                                    </tr>
                                                </template>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--TERRITORIOS-->
                        <div class="tab-pane fade p-3 mb-4" id="nav-cliente-territorio" role="tabpanel" aria-labelledby="cliente-territorio-tab">
                            <div class="row">
                                <div class="col-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Territorio</label>
                                        <select class="form-control" @change="obtenerRepresentanteTerritorio($event, false)" id="t_territorio" v-model="territorio.territorio">
                                            <template v-for="u in territorios_sharedData.territorios">
                                                <option :value="u.Id">
                                                    <span v-if="u.Id > 0">{{ u.Id }} - </span> {{ u.Descripcion }}
                                                </option>
                                            </template>
                                        </select>
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                                <div class="col-6 col-lg-3 text-center">
                                    <div class="form-group">
                                        <label>Representante</label><br />
                                        <b style="font-size:14px;">
                                            <span v-if="territorio.idRepresentanteTerritorio">
                                                {{ territorio.idRepresentanteTerritorio }} -
                                            </span>
                                            {{ territorio.representanteTerritorio }}
                                        </b>
                                    </div>
                                </div>
                                <div class="col-6 mt-2 col-lg-3 mt-lg-0">
                                    <div class="form-group">
                                        <label>Territorio Servicio</label>
                                        <select class="form-control" @change="obtenerRepresentanteTerritorio($event, true)" v-model="territorio.territorioServicio">
                                            <template v-for="u in territorios_sharedData.territoriosServicio">
                                                <option :value="u.Id">
                                                    <span v-if="u.Id > 0">{{ u.Id }} -</span> {{ u.Descripcion }}
                                                </option>
                                            </template>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-6 mt-2 col-lg-3 mt-lg-0 text-center">
                                    <div class="form-group">
                                        <label>Representante servicio</label><br />
                                        <b style="font-size:14px;">
                                            <span v-if="territorio.idRepresentanteServicio && territorio.idRepresentanteServicio > 0">
                                                {{ territorio.idRepresentanteServicio }} - 
                                            </span>
                                            {{ territorio.representanteServicio }}
                                        </b>
                                    </div>
                                </div>
                            
                            
                            
                             <div class="col-6 mt-2 col-lg-3 mt-lg-0">
                                <div class="form-group">
                                    <label>Territorio Servicio Técnico</label>
                                    <select class="form-control" @change="obtenerRepresentanteTerritorioTecnico($event, true)" v-model="territorio.territorioServicioTecnico">
                                        <template v-for="u in territorios_sharedData.territorioServicioTecnico">
                                            <option :value="u.Id">
                                                <span v-if="u.Id > 0">{{ u.Id }} -</span> {{ u.Descripcion }}
                                            </option>
                                        </template>
                                    </select>
                                </div>
                            </div>
                            <div class="col-6 mt-2 col-lg-3 mt-lg-0 text-center">
                                <div class="form-group">
                                    <label>Representante servicio técnico</label><br />
                                    <b style="font-size:14px;">
                                        <span v-if="territorio.idRepresentanteServicioTecnico && territorio.idRepresentanteServicioTecnico > 0">
                                            {{ territorio.idRepresentanteServicioTecnico }} - 
                                        </span>
                                        {{ territorio.representanteServicioTecnico }}
                                    </b>
                                </div>
                            </div>
                        </div>
                            <div class="row mt-3 align-items-end">
                                <div class="col d-inline-flex">
                                    <button v-if="territorios_sharedData.editMode" class="btn mr-3" @click="cancelarTerritorio($event)">Cancelar edici&oacute;n</button>
                                    <button
                                        v-if="territorios_sharedData.territorioPadre"
                                        @click="agregarNuevoTerritorio"
                                        class="btn btn-primary">Agregar territorio padre</button>

                                    <button v-if="!territorios_sharedData.territorioPadre && sharedData.clienteSeleccionado && sharedData.permisoModificarTerritorios && !territorios_sharedData.editMode && !territorio.territoriosPendientesPorAceptar"
                                        @click="agregarNuevoTerritorio"
                                        class="btn btn-primary">Agregar territorio hijo</button>

                                    <%--<button v-if="" class="btn btn-primary" @click="agregarDireccionEntrega($event)">Agregar</button>--%>
                                    <button v-if="territorios_sharedData.editMode" class="btn btn-warning" @click="actualizarTerritorio($event,)">Actualizar</button>
                                </div>
                                <div class="col text-right" v-if="territorios_sharedData.tabla.length > 0 && territorios_sharedData.tabla.some(x => x.TerritorioPadre)">
                                    <span class="badge badge-warning">Territorio padre</span>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="table-responsive">
                                        <table class="table tablesize table-hover table-bordered table-sm">
                                            <thead>
                                                <tr>
                                                    <th scope="col"></th>
                                                    <%--<th scope="col"></th>--%>
                                                    <th scope="col">Activo</th>
                                                    <th scope="col">Territorio</th>
                                                    <th scope="col">Representante</th>
                                                    <th scope="col">Territorio servicio</th>
                                                    <th scope="col">Representante servicio</th>
                                                    <th scope="col">Territorio servicio técnico</th>
                                                    <th scope="col">Representante servicio técnico</th>
                                                    <th scope="col">Fecha solicitud</th>
                                                    <th scope="col">Fecha autorizado</th>
                                                    <th scope="col">Fecha rechazado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <template v-for="(item, index) in territorios_sharedData.tabla">
                                                    <tr :class="item.Edit ? 'table-warning' : ''" >
                                                        <td>
                                                            <button :disabled="territorio.territoriosPendientesPorAceptar" class="btn btn-icon" @click="editarTerritorio($event, item.Id)">
                                                                <i style="color:blue;" class="bi bi-pencil-square"></i>
                                                            </button>
                                                        </td>
                                                        <%--<td>
                                                            <button :disabled="territorios_sharedData.tabla.some((x) => x.Edit) && !item.Edit" @click="eliminarTerritorio($event, item.IdTerritorio)" class="btn btn-icon">
                                                                <i :style="[sharedData.clienteSeleccionado && !sharedData.permisoModificarTerritorios ? { 'color': 'rgb(170, 170, 170)' } : { 'color': 'red' }]" class="bi bi-x-lg"></i>
                                                            </button>
                                                        </td>--%>
                                                        <td>
                                                            <div class="form-check centerCheckbox">
                                                                <input type="checkbox"
                                                                    :disabled="(sharedData.clienteSeleccionado && !sharedData.permisoModificarTerritorios) || (territorio.territoriosPendientesPorAceptar) || (territorios_sharedData.tabla.some((x) => x.Edit) && !item.Edit)"
                                                                    @click="cambiarEstatusActivo($event, item.IdTerritorio)"
                                                                    :checked="item.Activo"
                                                                    class="form-check-input" />
                                                            </div>
                                                        </td>
                                                        <td :class="item.TerritorioPadre ? 'bg-warning' : ''">{{ item.IdTerritorio }} - {{ item.Territorio }}</td>
                                                        <td>{{ item.IdRik }} - {{ item.Rik }}</td>
                                                        <td>
                                                            <span v-if="item.IdTerServ > 0">
                                                                {{ item.IdTerServ }} - {{ item.TerServ }}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span v-if="item.IdRikServ > 0">
                                                                {{ item.IdRikServ }} - {{ item.RikServ }}
                                                            </span>
                                                        </td>
                                                         <td> 
                                                             <span v-if="item.IdTerServTecnico > 0">
                                                                 {{ item.IdTerServTecnico }} - {{ item.TerServTecnico }}
                                                             </span>
                                                         </td>
                                                         <td>
                                                             <span v-if="item.IdRikServTecnico > 0">
                                                                 {{ item.IdRikServTecnico }} - {{ item.RikServTecnico }}
                                                             </span>
                                                         </td>
                                                        <td>{{ item.FechaSolicitud }}</td>
                                                        <td>{{ item.FechaAutorizado }}</td>
                                                        <td>{{ item.FechaRechazado }}</td>
                                                    </tr>
                                                </template>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div v-if="territorios_sharedData.mostrarAdvertenciaTerritorio" class="row mt-2">
                                <div class="col">
                                    <div class="alert alert-warning">
                                        Para poder proceder con esta solicitud en el cliente es importante presionar el bot&oacute;n {{ sharedData.clienteSeleccionado ? 'actualizar' : 'guardar' }} en el catalogo de clientes
                                    </div>
                                </div>
                            </div>
                            <div v-if="territorio.territoriosPendientesPorAceptar" class="row mt-2">
                                <div class="col">
                                    <div class="alert alert-warning">
                                        Hay solicitudes de territorio pendientes de autorizar
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--COBRANZA-->
                        <div class="tab-pane fade p-3 mb-4" id="nav-cobranza" role="tabpanel" aria-labelledby="cobranza-tab">
                            <div class="row align-items-center mt-3">
                                <div class="col-12 col-md-8 justify-content-md-start justify-content-center d-inline-flex">
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" v-model="cobranza.credito">
                                        <label class="form-check-label">
                                            Cr&eacute;dito
                                        </label>
                                    </div>
                                    <div class="form-check ml-4">
                                        <input class="form-check-input" type="checkbox" v-model="cobranza.permiteFacturar">
                                        <label class="form-check-label">
                                            Permitir facturar
                                        </label>
                                    </div>
                                    <div class="form-check ml-4">
                                        <input class="form-check-input" :disabled="!cobranza.enableCreditoSuspendido"  type="checkbox" v-model="cobranza.creditoSuspendido">
                                        <label class="form-check-label">
                                            Cr&eacute;dito suspendido
                                        </label>
                                    </div>
                                </div>
                                <div class="col mt-3 mt-md-0 d-flex">
                                    <div class="form-group ml-auto">
                                        <label>Tipo de moneda</label>
                                        <select class="form-control" id="c_moneda" v-model="cobranza.moneda">
                                            <option v-for="u in cobranza_sharedData.monedas" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                        <div class="invalid-feedback">* Campo requerido</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-6 col-md-3 col-xl-2">
                                    <div class="form-group">
                                        <label>L&iacute;mite de crédito</label>
                                        <input type="number" @input="maxLength($event, 9)" class="form-control" v-model="cobranza.limiteCredito" />
                                    </div>
                                </div>
                                <div class="col-6 col-md-4 col-xl-3">
                                    <div class="form-group">
                                        <label>Forma de revisi&oacute;n y pago</label>
                                        <input type="text" class="form-control" v-model="cobranza.revPago" />
                                    </div>
                                </div>
                                <div class="col-6 col-md-3 col-xl-2 offset-md-2 offset-xl-1">
                                    <div class="form-group">
                                        <label>Condiciones de pago</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">D&iacute;as</div>
                                            </div>
                                            <select class="form-control" v-model="cobranza.condicionPagoDias">
                                                <option v-for="u in cobranza_sharedData.diasCondicionesDePago" :value="u.Id">
                                                    {{ u.Descripcion }}
                                                </option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6 col-md-6 col-xl-4">
                                    <div class="form-group">
                                        <label>Autoriz&oacute;</label>
                                        <input type="text" class="form-control" v-model="cobranza.condicionPagoAutorizo" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="card border-0">
                                        <div class="card-body" style="padding: 1rem 0 1rem 0 !important;">
                                            <h6 class="card-title mb-2">Correos para el envio de estados de cuenta</h6>
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="form-group">
                                                        <label>Correo 1</label>
                                                        <input type="text" class="form-control" v-model="cobranza.correoEdoCuenta1" />
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="form-group">
                                                        <label>Correo 2</label>
                                                        <input type="text" class="form-control" v-model="cobranza.correoEdoCuenta2" />
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="form-group">
                                                        <label>Correo 3</label>
                                                        <input type="text" class="form-control" v-model="cobranza.correoEdoCuenta3" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-12 col-md-6">
                                    <div class="card border-0" style="background-color: whitesmoke;">
                                        <div class="card-body">
                                            <h6 class="card-title">Horario de revisi&oacute;n</h6>
                                            <div class="row">
                                                <div class="col-12 col-xl-6">
                                                    <em>Horario matutino</em>
                                                    <div class="row mt-2 align-items-center">
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonHoraAm1"
                                                                :start-time="cobranza_sharedData.revision_horaAm1_startTime"
                                                                :max-time="{ hours: 11, minutes: 59 }"
                                                                :min-time="{ hours: 0, minutes: 0 }"
                                                                v-model="cobranza.revision_horaAm1">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                        <span>a</span>
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonHoraAm2"
                                                                :start-time="cobranza_sharedData.revision_horaAm2_startTime"
                                                                :max-time="{ hours: 11, minutes: 59 }"
                                                                :min-time="{ hours: 0, minutes: 0 }"
                                                                v-model="cobranza.revision_horaAm2">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-xl-6 mt-xl-0 mt-2">
                                                    <em>Horario vespertino</em>
                                                    <div class="row mt-2 align-items-center">
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonHoraPm1"
                                                                :start-time="cobranza_sharedData.revision_horaPm1_startTime"
                                                                :max-time="{ hours: 23, minutes: 59 }"
                                                                :min-time="{ hours: 12, minutes: 0 }"
                                                                v-model="cobranza.revision_horaPm1">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                        <span>a</span>
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonHoraPm2"
                                                                :start-time="cobranza_sharedData.revision_horaPm2_startTime"
                                                                :max-time="{ hours: 23, minutes: 59 }"
                                                                :min-time="{ hours: 12, minutes: 0 }"
                                                                v-model="cobranza.revision_horaPm2">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h6 class="card-title mt-3">Semanas de revisi&oacute;n</h6>
                                            <div class="row">
                                                <div class="col">
                                                    <input type="number" @input="maxLength($event, 9)" class="form-control" v-model="cobranza.semanaRevision1" />
                                                </div>
                                                <span>.</span>
                                                <div class="col">
                                                    <input type="number" @input="maxLength($event, 9)" class="form-control" v-model="cobranza.semanaRevision2" />
                                                </div>
                                            </div>
                                            <h6 class="card-title mt-3">D&iacute;as de revisi&oacute;n</h6>
                                            <div class="row">
                                                <div class="col-3 col-md-4 col-xl-auto" v-for="(d, index) in cobranza_sharedData.dias">
                                                    <div class="form-check">
                                                        <input :id="'revision_' + index" class="form-check-input" v-model="cobranza.diasRevision" :value="d" type="checkbox">
                                                        <label :for="'revision_' + index" class="form-check-label">{{ d }}</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col mt-3 mt-md-0">
                                    <div class="card" style="border:0; background-color: whitesmoke;">
                                        <div class="card-body">
                                            <h6 class="card-title">Horario de pago</h6>
                                            <div class="row mt-2 align-items-center">
                                                <div class="col-12 col-xl-6">
                                                    <em>Horario matutino</em>
                                                    <div class="row mt-2 align-items-center">
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonPagoAm1"
                                                                :start-time="cobranza_sharedData.pago_horaAm1_startTime"
                                                                :max-time="{ hours: 11, minutes: 59 }"
                                                                :min-time="{ hours: 0, minutes: 0 }"
                                                                v-model="cobranza.pago_horaAm1">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                        <span>a</span>
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonPagoAm2"
                                                                :start-time="cobranza_sharedData.pago_horaAm2_startTime"
                                                                :max-time="{ hours: 11, minutes: 59 }"
                                                                :min-time="{ hours: 0, minutes: 0 }"
                                                                v-model="cobranza.pago_horaAm2">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                    </div> 
                                                </div>
                                                <div class="col-12 col-xl-6 mt-xl-0 mt-2">
                                                    <em>Horario vespertino</em>
                                                    <div class="row mt-2 align-items-center">
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonPagoPm1"
                                                                :start-time="cobranza_sharedData.pago_horaPm1_startTime"
                                                                :max-time="{ hours: 23, minutes: 59 }"
                                                                :min-time="{ hours: 12, minutes: 0 }"
                                                                v-model="cobranza.pago_horaPm1">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                        <span>a</span>
                                                        <div class="col">
                                                            <date-picker
                                                                time-picker
                                                                mode-height="120"
                                                                :is-24="false"
                                                                select-text="seleccionar"
                                                                cancel-text="cancelar"
                                                                uid="c_revisonPagoPm2"
                                                                :start-time="cobranza_sharedData.pago_horaPm2_startTime"
                                                                :max-time="{ hours: 23, minutes: 59 }"
                                                                :min-time="{ hours: 12, minutes: 0 }"
                                                                v-model="cobranza.pago_horaPm2">
                                                                <template #am-pm-button="{ toggle, value }">
                                                                    <button role="button" onclick="return false;" @click="toggle">{{ value }}</button>
                                                                </template>
                                                            </date-picker>
                                                            <div class="invalid-feedback">* Campo requerido</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h6 class="card-title mt-3">Semana de pago</h6>
                                            <div class="row">
                                                <div class="col">
                                                    <input type="number" @input="maxLength($event, 9)" class="form-control" v-model="cobranza.semanaPago" />
                                                </div>
                                            </div>
                                            <h6 class="card-title mt-3">D&iacute;as de pago</h6>
                                            <div class="row">
                                                <div class="col-3 col-md-4 col-xl-auto" v-for="(d, index) in cobranza_sharedData.dias">
                                                    <div class="form-check">
                                                        <input :id="'pago_' + index" class="form-check-input" v-model="cobranza.diasPago" :value="d" type="checkbox">
                                                        <label :for="'pago_' + index" class="form-check-label">{{ d }}</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="card" style="border:0; background-color: whitesmoke;">
                                        <div class="card-body">
                                            <h6 class="card-title">Recepci&oacute;n</h6>
                                            <div class="row">
                                                <div class="col-12 col-sm-5 col-md-3 col-lg-2">
                                                    <div class="form-group">
                                                        <label>Semana de recepci&oacute;n</label>
                                                        <input type="number" @input="maxLength($event, 9)" class="form-control" v-model="cobranza.semanaRecepcion" />
                                                    </div>
                                                </div>
                                                <div class="col-12 col-sm-7 col-md-9 col-lg-10">
                                                    <div class="row justify-content-lg-center">
                                                        <div class="col-12 text-center">
                                                            <label>D&iacute;as de recepci&oacute;n</label>
                                                        </div>
                                                        <div class="col-3 col-sm-4 col-md-3 col-lg-auto" v-for="(d, index) in cobranza_sharedData.dias">
                                                            <div class="form-check">
                                                                <input :id="'recepcion_' + index" class="form-check-input" v-model="cobranza.diasRecepcion" :value="d" type="checkbox">
                                                                <label :for="'recepcion_' + index" class="form-check-label">{{ d }}</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <label>Tel&eacute;fonos de cobranza</label>
                                    <div class="row">
                                        <div class="col">
                                            <input type="text" class="form-control" v-model="cobranza.telefonoCobranza1" />
                                        </div>
                                        <div class="col">
                                            <input type="text" class="form-control" v-model="cobranza.telefonoCobranza2" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="card border-0">
                                        <div class="card-body" style="padding: 1rem 0 1rem 0 !important;">
                                            <h6 class="card-title mb-2">Documentos</h6>
                                            <div class="row align-items-center">
                                                <div class="col col-md-6 col-lg-5 col-xl-12 mb-3 mt-md-0">
                                                    <input type="text" class="form-control" v-model="cobranza.documento" />
                                                </div>
                                                <div class="col-12 col-lg-4 offset-lg-2 offset-xl-0 col-xl-3">
                                                    <div class="form-check">
                                                        <input class="form-check-input" type="checkbox" v-model="cobranza.desgloseIva">
                                                        <label class="form-check-label">
                                                            Desglose de IVA
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-8 col-md-4 col-lg-3 col-xl-2 mt-2 mt-md-0">
                                                    <div class="form-check">
                                                        <input class="form-check-input" type="checkbox" v-model="cobranza.retencionIva">
                                                        <label class="form-check-label">
                                                            Retenc&iacute;on de IVA
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-4 col-md-2 col-lg-2 mb-2 mb-lg-0">
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">%</div>
                                                        </div>
                                                        <input type="text" class="form-control" v-model="cobranza.porcientoRetencion" />
                                                    </div>
                                                </div>
                                                <div class="col-8 col-md-4 offset-lg-2 offset-xl-0 col-xl-3 col-lg-3 text-xl-right">
                                                    <div class="form-check">
                                                        <input class="form-check-input" type="checkbox" v-model="cobranza.ivaCliente">
                                                        <label class="form-check-label">
                                                            Porcentaje de IVA al cliente
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-4 col-md-2">
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text">%</div>
                                                        </div>
                                                        <input type="text" id="c_porcentajeIvaCliente" v-model="cobranza.porcentajeIvaCliente" class="form-control" />
                                                        <div class="invalid-feedback">* Campo requerido</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="card border-0" style="background-color: whitesmoke;">
                                        <div class="card-body">
                                            <h6 class="card-title mb-2">Datos fiscales</h6>
                                            <div class="row align-items-end">
                                                <div class="col-12 col-md-6 col-lg-3">
                                                    <div class="form-group">
                                                        <label>Banco donde nos deposita el cliente</label>
                                                        <select class="form-control" v-model="cobranza.banco">
                                                            <option v-for="u in cobranza_sharedData.bancos" :value="u.Id">
                                                                {{ u.Descripcion }}
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-lg-3">
                                                    <div class="form-group">
                                                        <label>Número de Cuenta</label>
                                                        <input type="text" class="form-control" v-model="cobranza.numeroCuenta" />
                                                    </div>
                                                </div>
                                                <div class="col-6 col-lg-3">
                                                    <div class="form-group">
                                                        <label>Referencia tecleada</label>
                                                        <input type="text" class="form-control" v-model="cobranza.referenciaTecleada" />
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group">
                                                        <label>Portal del cliente para subir informaci&oacute;n</label>
                                                        <input type="text" class="form-control" v-model="cobranza.portal" />
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="form-group">
                                                        <label>Últimos 4 dígitos de la cuenta</label>
                                                        <input type="text" class="form-control" v-model="cobranza.ultimos4Digitos" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12 col-lg-6">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <h6 class="card-title mb-2">Para facturas</h6>
                                                            <div class="row">
                                                                <div class="col">
                                                                    <div class="form-group">
                                                                        <label>Uso CFDI</label>
                                                                        <select class="form-control" v-model="cobranza.usoCFDI">
                                                                            <option v-for="u in cobranza_sharedData.usoCFDI" :value="u.id">
                                                                                {{ u.descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col">
                                                                    <div class="form-group">
                                                                        <label>Metodo de Pago</label>
                                                                        <select class="form-control" v-model="cobranza.metodoPago">
                                                                            <option v-for="u in cobranza_sharedData.metodosPago" :value="u.id">
                                                                                {{ u.descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-lg-6 mt-lg-0 mt-2">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <h6 class="card-title mb-2">Para Notas de cr&eacute;dito</h6>
                                                            <div class="row">
                                                                <div class="col-6 col-md-4">
                                                                    <div class="form-group">
                                                                        <label>Uso CFDI</label>
                                                                        <select class="form-control" v-model="cobranza.ncUsoCFDI">
                                                                            <option v-for="u in cobranza_sharedData.usoCFDI" :value="u.id">
                                                                                {{ u.descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-6 col-md-4">
                                                                    <div class="form-group">
                                                                        <label>Forma de pago</label>
                                                                        <select class="form-control" v-model="cobranza.ncFormaPago">
                                                                            <option v-for="u in cobranza_sharedData.formasDePago" :value="u.Id">
                                                                                {{ u.Descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col">
                                                                    <div class="form-group">
                                                                        <label>Metodo de Pago</label>
                                                                        <select class="form-control" v-model="cobranza.ncMetodoPago">
                                                                            <option v-for="u in cobranza_sharedData.metodosPago" :value="u.id">
                                                                                {{ u.descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <h6 class="card-title mb-2">Para pagos</h6>
                                                            <div class="row">
                                                                <div class="col-6 col-lg-4 col-xl-3">
                                                                    <div class="form-group">
                                                                        <label>Uso CFDI</label>
                                                                        <select class="form-control" disabled v-model="cobranza.pagoUsoCFDI">
                                                                            <option v-for="u in cobranza_sharedData.usoCFDI" :value="u.id">
                                                                                {{ u.descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-6 col-lg-4 col-xl-3">
                                                                    <div class="form-group">
                                                                        <label>Forma de pago</label>
                                                                        <select class="form-control" v-model="cobranza.pagoMetodoPago">
                                                                            <option v-for="u in cobranza_sharedData.formasDePago" :value="u.Id">
                                                                                {{ u.Descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                                                    <div class="form-group">
                                                                        <label>Banco donde nos deposita el cliente</label>
                                                                        <select class="form-control" v-model="cobranza.pagoBanco">
                                                                            <option v-for="u in cobranza_sharedData.bancos" :value="u.Id">
                                                                                {{ u.Descripcion }}
                                                                            </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12 col-md-6 col-xl-3">
                                                                    <div class="form-group">
                                                                        <label>N&uacute;mero de cuenta</label>
                                                                        <input type="text" class="form-control" v-model="cobranza.pagoNumeroCuenta" />
                                                                    </div>
                                                                </div>
                                                                <div class="col col-xl-6">
                                                                    <div class="form-group">
                                                                        <label>Correos para el envío de comprobantes de pago</label>
                                                                        <input type="text" class="form-control" v-model="cobranza.pagoCorreos" />
                                                                        <small class="form-text text-muted">Puede agregar varias direcciones de correo separandolos con ;</small>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col">
                                    <div class="card">
                                        <div class="card-body">
                                            <h6 class="card-title mb-2">Formas de pago</h6>
                                            <div class="row">
                                                <template v-for="u in cobranza_sharedData.formasDePago">
                                                    <div v-if="u.Id > 0" class="col-12 col-sm-6 col-lg-4 col-xl-3">
                                                        <div class="form-check form-check-inline">
                                                            <input class="form-check-input" v-model="cobranza.formasPago" type="checkbox" :value="u.Id" />
                                                            <label class="form-check-label">{{ u.Descripcion }}</label>
                                                        </div>
                                                    </div>
                                                </template>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row align-items-center mt-4">
                                <div class="col-12 col-md-6 col-lg-3">
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" v-model="cobranza.ordenCompra">
                                        <label class="form-check-label">
                                            Requiere orden de compra
                                        </label>
                                    </div>
                                </div>
                                <div class="col-12 mt-2 col-md-6 col-lg-3">
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" v-model="cobranza.comisiones">
                                        <label class="form-check-label">
                                            Comisiones especiales
                                        </label>
                                    </div>
                                </div>
                                <div class="col-8 col-md-4 mt-md-2 mt-xl-0 col-xl-5 text-lg-right">
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" v-model="cobranza.ncFacturar">
                                        <label class="form-check-label">
                                            Crear nota de cr&eacute;dito al facturar por
                                        </label>
                                    </div>
                                </div>
                                <div class="col-4 col-md-2 mt-md-2 mt-xl-0 col-xl-1">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">%</div>
                                        </div>
                                        <input type="text" class="form-control" v-model="cobranza.porcentajeNCFacturar" />
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Remisión electr&oacute;nica</label>
                                        <select class="form-control" v-model="cobranza.remisionElectronica">
                                            <option v-for="u in cobranza_sharedData.remisionesElectronicas" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Serie de consecutivo</label>
                                        <select class="form-control" v-model="cobranza.serie">
                                            <option v-for="u in cobranza_sharedData.serieConsecutivo" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Serie de nota de cr&eacute;dito</label>
                                        <select class="form-control" v-model="cobranza.serieNC">
                                            <option v-for="u in cobranza_sharedData.seriesNotaCredito" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Serie de nota de cargo</label>
                                        <select class="form-control" v-model="cobranza.serieNCargo">
                                            <option v-for="u in cobranza_sharedData.seriesNotaCargo" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Adenda</label>
                                        <select class="form-control" v-model="cobranza.adenda">
                                            <option v-for="u in cobranza_sharedData.adendas" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col col-md-4 col-xl-2">
                                    <div class="form-group">
                                        <label>Versi&oacute;n CFDI</label>
                                        <select class="form-control" v-model="cobranza.versionCFDI">
                                            <option v-for="u in cobranza_sharedData.versionesCFDI" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div v-if="sharedData.mostrarTabBennets && sharedData.bennetsCatalogoAdicional.length > 0" class="tab-pane fade p-3 mb-4" id="nav-campos-usuario" role="tabpanel" aria-labelledby="campos-usuario-tab">
                            <div class="row align-items-center">
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[0].LabelText }}</label>
                                        <input class="form-control" v-model="catalogoAdicional.sucursal" />
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[1].LabelText }}</label>
                                        <select class="form-control" v-model="catalogoAdicional.cat[0].value">
                                            <option v-for="u in sharedData.bennetsCatalogoAdicional[1].CatGenericoDetalle" :key="u.Id" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[2].LabelText }}</label>
                                        <select class="form-control" v-model="catalogoAdicional.cat[1].value">
                                            <option v-for="u in sharedData.bennetsCatalogoAdicional[2].CatGenericoDetalle" :key="u.Id" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[3].LabelText }}</label>
                                        <select class="form-control" v-model="catalogoAdicional.cat[2].value">
                                            <option v-for="u in sharedData.bennetsCatalogoAdicional[3].CatGenericoDetalle" :key="u.Id" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[4].LabelText }}</label>
                                        <select class="form-control" v-model="catalogoAdicional.cat[3].value">
                                            <option v-for="u in sharedData.bennetsCatalogoAdicional[4].CatGenericoDetalle" :key="u.Id" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="form-group">
                                        <label>{{ sharedData.bennetsCatalogoAdicional[5].LabelText }}</label>
                                        <select class="form-control" v-model="catalogoAdicional.cat[4].value">
                                            <option v-for="u in sharedData.bennetsCatalogoAdicional[5].CatGenericoDetalle" :key="u.Id" :value="u.Id">
                                                {{ u.Descripcion }}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--vinculacion cliente y comentario-->
        <div class="modal fade" role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false" id="iframeModal">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ titleModal }}</h5>
                    </div>
                    <div class="modal-body p-0">
                        <div class="container">
                            <div class="row">
                                <div class="col">
                                    <div class="holds-the-iframe">
                                        <iframe style="border:0;" width="100%" id="iframeContent"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--autorizacion tipo cliente -->
        <div class="modal fade" role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false" id="iframeModalTipoCliente">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ titleModal }}</h5>
                    </div>
                    <div class="modal-body p-0">
                        <div class="container">
                            <div class="row">
                                <div class="col">
                                    <div class="holds-the-iframe">
                                        <iframe style="border:0;" width="100%" id="iframeContentTipoCliente"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <button class="d-none" id="bandera_modal_abierto_cerrado" @click="guardarComentario($event)"></button>
    </div>

    
    <%--<link rel="stylesheet" href="https://unpkg.com/@vuepic/vue-datepicker@latest/dist/main.css">--%>
    <link rel="stylesheet" href="js/CatClientesV3/vue-datepicker.css" />
    <%--<link type="text/css" rel="stylesheet" href="https://unpkg.com/vue-next-select/dist/index.min.css" />--%>
    <link rel="stylesheet" href="js/CatClientesV3/next-select.css" />

    <%--<script src="https://unpkg.com/vue@3/dist/vue.global.js"></script>--%>
    <script src="js/CatClientesV3/vue3.js"></script>
    <%--<script src="https://unpkg.com/@vuepic/vue-datepicker@latest"></script>--%>
    <script src="js/CatClientesV3/vue-datepicker.js"></script>
    <%--<script src="https://unpkg.com/vue-next-select/dist/vue-next-select.iife.prod.js"></script>--%>
    <script src="js/CatClientesV3/next-select.js"></script>
    <script>
        const { ref, createApp, onMounted } = Vue

        const app = createApp({
            components: {
                DatePicker: VueDatePicker,
                VSelect: VueNextSelect
            },
            setup() {

                //#region
                /* valores iniciales de la aplicacion
                 **/
                const spinner = ref()
                const cliente = ref()
                const sharedData = ref({
                    permisoGuardar: true,
                    permisoModificar: true,
                    permisoModificarTerritorios: true,
                    centrosDistribucion: [],
                    clientes: [],
                    uens: [],
                    paises: [],
                    usuario: null,
                    fechaUltimaModificacion: null,
                    clienteSeleccionado: false,
                    cliente: null,
                    centroDistribucion: null,
                    mostrarTabBennets: false,
                    bennetsCatalogoAdicional: []
                })

                onMounted(() => inicializar())

                const inicializar = () => {
                    showLoader()

                    Promise.all([
                        obtenerConfiguracionInicial(),
                        obtenerCentros(),
                        obtenerClientes(),
                        obtenerUens(),
                        obtenerPaises(),
                        obtenerTipoDeClientes(),
                        obtenerCuentasCorporativas(),
                        obtenerRegimenFiscal(),
                        obtenerAsignacionDePedido(),
                        obtenerMaximoId(),
                        obtenerRutas(),
                        obtenerTerritorios(),
                        obtenerTerritoriosServicio(),
                        obtenerTerritoriosServicioTecnico(),
                        obtenerFormasDePago(),
                        obtenerTMonedas(),
                        obtenerAdendas(),
                        obtenerSeriesNotaCargo(),
                        obtenerSeriesNotaCredito(),
                        obtenerSerieConsecutivo(),
                        obtenerRemisionesElectronicas(),
                        obtenerVersionCFDI(),
                        obtenerBancos(),
                        obtenerDiasCondicionesDePago(),
                        obtenerUsosCfdi()
                    ]).then(() => {
                        hideLoader()
                    })
                }

                const obtenerConfiguracionInicial = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerConfiguracionInicial', null).then((data) => {
                        sharedData.value.permisoGuardar = data.d.PermisosGuardar
                        sharedData.value.permisoModificar = data.d.PermisoModificar
                        sharedData.value.permisoModificarTerritorios = data.d.PermisoModificarTerritorios
                        document.title = data.d.Title
                        sharedData.value.mostrarTabBennets = data.d.MostrarTabBennets
                        if (data.d.MostrarTabBennets) {
                            ajaxRequest('CatClientesV3.aspx/ObtenerCamposDeUsuario_Bennets', null).then(d => {
                                sharedData.value.bennetsCatalogoAdicional = d.d
                                catalogoAdicional.value.idCatGenerico = d.d[0].IdCatGenerico
                                catalogoAdicional.value.cat[0].id = d.d[1].IdCatGenerico
                                catalogoAdicional.value.cat[0].value = d.d[1].CatGenericoDetalle[0].Id
                                catalogoAdicional.value.cat[1].id = d.d[2].IdCatGenerico
                                catalogoAdicional.value.cat[1].value = d.d[2].CatGenericoDetalle[0].Id
                                catalogoAdicional.value.cat[2].id = d.d[3].IdCatGenerico
                                catalogoAdicional.value.cat[2].value = d.d[3].CatGenericoDetalle[0].Id
                                catalogoAdicional.value.cat[3].id = d.d[4].IdCatGenerico
                                catalogoAdicional.value.cat[3].value = d.d[4].CatGenericoDetalle[0].Id
                                catalogoAdicional.value.cat[4].id = d.d[5].IdCatGenerico
                                catalogoAdicional.value.cat[4].value = d.d[5].CatGenericoDetalle[0].Id
                            })
                        }
                    })
                }

                const obtenerCentros = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerCentros', null).then((data) => {
                        sharedData.value.centrosDistribucion = data.d
                        if (!sharedData.value.centroDistribucion)
                            sharedData.value.centroDistribucion = sharedData.value.centrosDistribucion[0].Id
                    })
                }

                const obtenerClientes = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerClientes', null).then((data) => {
                        sharedData.value.clientes = data.d.map((item) => {
                            return (item.Id == -1 ? '' : (item.Id + ' - ')) + item.Descripcion
                        })
                    })
                }

                const obtenerUens = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerUEN', null).then((data) => {
                        sharedData.value.uens = data.d
                    })
                }

                const obtenerSegmentos = (idUen) => {
                    const param = JSON.stringify({ idUen: idUen })

                    return ajaxRequest('CatClientesV3.aspx/ObtenerSegmentos', param)
                }

                const obtenerPaises = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerPaises', null).then((data) => {
                        sharedData.value.paises = data.d
                    })
                }

                const obtenerEstados = (idPais) => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerEstados', JSON.stringify({ idPais: idPais }))
                }

                const obtenerUsosCfdi = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerUsosCfdi', null).then(data => {
                        data.d.forEach(x => {
                            cobranza_sharedData.value.usoCFDI.push({
                                id: x.StringId,
                                descripcion: x.Descripcion
                            })
                        })
                    })
                }

                const seleccionarCliente = (item) => {
                    showLoader()
                    let id = -1
                    if (item != '-- Seleccionar --') id = item.split('-')[0].trim()

                    if (id == -1) nuevoCliente()
                    else {
                        limpiarDatosGenerales()
                        limpiarCobranza()
                        limpiarTerritorio()
                        limpiarDireccionEntrega()
                        obtenerTodaLaInformacionDelCliente(id)
                        sharedData.value.clienteSeleccionado = true
                    }
                }

                const borrarBuscadorCliente = () => {
                    cliente.value.input.input.value = ''
                }

                const obtenerTodaLaInformacionDelCliente = (idCliente) => {
                    const param = JSON.stringify({ idCliente: idCliente })

                    ajaxRequest('CatClientesV3.aspx/ObtenerDatosDelCliente', param).then((data) => {
                        if (data.d.Mensaje == 'connClose') location.reload()
                        else {
                            datosGenerales.value.idCliente = data.d.IdCliente
                            sharedData.value.usuario = data.d.Usuario
                            sharedData.value.fechaUltimaModificacion = data.d.FechaUltimaModificacion
                            habilitarControles(data.d.DatosGenerales.Sucursal)

                            inicializar_datosGenerales(data.d.DatosGenerales)
                            inicializar_territorios(data.d.Territorios, data.d.TerritoriosPendientesPorAceptar)
                            inicializar_direccionEntrega(data.d.DireccionEntrega)
                            inicializar_cobranza(data.d.Cobranza)
                            inicializar_catalogoAdicional(data.d.CatalogoAdicionalBennets)
                            hideLoader()
                        }
                    })
                }

                const habilitarControles = (val) => {
                    $('#nav-datos-generales :input').prop('disabled', val)
                    $('#nav-cobranza :input').prop('disabled', val)
                }

                const nuevoCliente = (e) => {
                    if (e != null) {
                        e.preventDefault()
                        showLoader()
                    }

                    sharedData.value.cliente = '-- Seleccionar --'
                    borrarBuscadorCliente()
                    sharedData.value.clienteSeleccionado = false
                    limpiarDatosGenerales()
                    limpiarCobranza()
                    limpiarTerritorio()
                    limpiarDireccionEntrega()
                    if (sharedData.value.mostrarTabBennets) limpiarCatalogoAdicional()
                    territorios_sharedData.value.territorioPadre = true
                    inicializarValores()
                    obtenerMaximoId()
                    habilitarControles(false)

                    hideLoader()
                }

                const inicializarValores = () => {
                    ajaxRequest('CatClientesV3.aspx/InicializarValores', null).then((data) => {
                        if (data.d.Estatus) {
                            if (data.d.Mensaje == 'connClose') location.reload()
                            else {
                                direccionEntrega_sharedData.value.tabla = []
                                territorios_sharedData.value.tabla = []
                            }
                        }
                    })
                }

                const guardarNuevoCliente = (e) => {
                    e.preventDefault()

                    if (validarCamposNecesarios_NuevoCliente()) {
                        showLoader()
                        const req = JSON.stringify({
                            model: {
                                nuevo: !sharedData.value.clienteSeleccionado,
                                idCliente: datosGenerales.value.idCliente,
                                datosGenerales: obtenerModelo_datosGenerales(),
                                cobranza: obtenerModelo_cobranza(),
                                catalogoAdicionalBennets: obtenerModelo_catalogoAdicional()
                            }
                        })

                        ajaxRequest('CatClientesV3.aspx/GuardarCliente', req).then((data) => {
                            if (data.d.Estatus) {
                                const clienteSeleccionado = sharedData.value.clienteSeleccionado
                                nuevoCliente()
                                obtenerClientes()
                                hideLoader()
                                sweetAlertSuccess('Cliente ' + (clienteSeleccionado ? 'actualizado' : 'guardado'))
                            } else {
                                if (data.d.Mensaje === 'connClose') location.reload()
                                else if (data.d.Mensaje === 'openWindowVinculacionCliente') {
                                    hideLoader()
                                    openModalVinculacionCliente(datosGenerales.value.rfc)
                                }
                                else if (data.d.Mensaje === 'openWindowAutorizacionTipoCliente') {
                                    hideLoader()
                                    openWindowAutorizacionTipoCliente()
                                }
                                else if (data.d.Mensaje === 'openWindowAutorizacionCliente') {
                                    hideLoader()
                                    openWindowAutorizacion()
                                }
                                else {
                                    hideLoader()
                                    sweetAlertError(data.d.Mensaje)
                                }
                            }
                        })
                    }
                }

                const validarCamposNecesarios_NuevoCliente = () => {
                    const datosGeneralesValid = validaCampos_datosGenerales()
                    if (!datosGeneralesValid) return false

                    if (direccionEntrega_sharedData.value.tabla.length == 0) {
                        jQuery('#direccion-entrega-tab').click()
                        sweetAlertError('No se ha agregado una dirección de entrega')
                        return false
                    }

                    if (territorios_sharedData.value.tabla.length == 0 && !sharedData.value.clienteSeleccionado) {
                        jQuery('#cliente-territorio-tab').click()
                        sweetAlertError('No se ha agregado un territorio')
                        return false
                    }

                    const cobranzaValid = validCampos_cobranza()
                    if (!cobranzaValid) return false

                    return true
                }

                const cambiarCentroDistribucion = () => {
                    const req = JSON.stringify({
                        centroDistribucion: sharedData.value.centrosDistribucion.find(x => x.Id == sharedData.value.centroDistribucion).Descripcion,
                        idCentroDistribucion: sharedData.value.centroDistribucion
                    })

                    ajaxRequest('CatClientesV3.aspx/CambiarCentroDistribucion', req).then((data) => {
                        if (data.d.Mensaje == 'connClose') location.reload()
                        else {
                            showLoader()
                            nuevoCliente()
                            inicializar()
                        }
                    })
                }
                //#endregion


                //#region (TAB) DatosGenerales
                const datosGenerales_sharedData = ref({
                    segmentos: [{
                        Id: -1,
                        Descripcion: '-- Seleccionar --'
                    }],
                    estados: [{
                        Id: -1,
                        Descripcion: '- Seleccionar -'
                    }],
                    tipoDeClientes: [],
                    cuentasCorporativas: [],
                    regimenFiscal: [],
                    asignacionDePedido: []
                })

                const datosGenerales = ref({
                    idCliente: 0,
                    activo: true,
                    sucursal: false,
                    razonSocial: '',
                    nombreComercial: '',
                    uen: -1,
                    segmento: -1,
                    tipoCliente: -1,
                    cuentaCorporativa: -1,
                    contacto: '',
                    email: '',
                    calle: '',
                    colonia: '',
                    numero: '',
                    cp: '',
                    pais: -1,
                    estado: -1,
                    municipio: '',
                    telefonos: '',
                    rfc: '',
                    regimenFiscal: 0,
                    asignacionDePedido: -1
                })

                const obtenerTipoDeClientes = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerTiposDeCliente', null).then((data) => {
                        datosGenerales_sharedData.value.tipoDeClientes = data.d
                    })
                }

                const obtenerCuentasCorporativas = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerCuentasCorporativas', null).then((data) => {
                        datosGenerales_sharedData.value.cuentasCorporativas = data.d
                    })
                }

                const obtenerRegimenFiscal = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerRegimenFiscal', null).then((data) => {
                        datosGenerales_sharedData.value.regimenFiscal = data.d
                    })
                }

                const obtenerMaximoId = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerMaximoId', null).then((data) => {
                        datosGenerales.value.idCliente = data.d.Id
                    })
                }

                const obtenerAsignacionDePedido = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerAsignacionDePedido', null).then((data) => {
                        datosGenerales_sharedData.value.asignacionDePedido = data.d
                    })
                }

                const deshabilitarCliente = () => {
                    if (!datosGenerales.value.activo) {
                        showLoader()

                        const param = JSON.stringify({ clienteId: datosGenerales.value.idCliente })
                        ajaxRequest('CatClientesV3.aspx/DeshabilitarCliente', param).then((data) => {
                            if (data.d.Mensaje == 'connClose') location.reload()
                            else {
                                hideLoader()
                                if (!data.d.Estatus) {
                                    sweetAlertError(data.d.Mensaje)
                                    datosGenerales.value.activo = true
                                }
                            }
                        })
                    }
                }

                const seleccionarUenDatosGenerales = (e) => {
                    obtenerSegmentos(e.target.value).then((data) => {
                        datosGenerales_sharedData.value.segmentos = data.d
                        datosGenerales.value.segmento = datosGenerales_sharedData.value.segmentos[0].Id
                    })
                }

                const seleccionarPaisDatosGenerales = (e) => {
                    obtenerEstados(e.target.value).then((data) => {
                        datosGenerales_sharedData.value.estados = data.d
                        datosGenerales.value.estado = datosGenerales_sharedData.value.estados[0].Id
                    })
                }

                const consultaTipoDeCliente = (e) => {
                    if (e.target.value == -1) {
                        datosGenerales.value.cuentaCorporativa = -1
                        return
                    }

                    const param = JSON.stringify({ tipoDeCliente: e.target.value })

                    ajaxRequest('CatClientesV3.aspx/ConsultaTipoDeCliente', param).then((data) => {
                        if (data.d.Mensaje == 'connClose') location.reload()
                        else {
                            if (!data.d.Estatus) datosGenerales.value.cuentaCorporativa = 10000
                            else datosGenerales.value.cuentaCorporativa = -1
                        }
                    })
                }

                const inicializar_datosGenerales = (data) => {
                    datosGenerales.value.activo = data.Activo
                    datosGenerales.value.sucursal = data.Sucursal
                    datosGenerales.value.razonSocial = data.RazonSocial
                    datosGenerales.value.nombreComercial = data.NombreComercial
                    datosGenerales.value.uen = data.Uen
                    obtenerSegmentos(data.Uen).then((dataS) => {
                        datosGenerales_sharedData.value.segmentos = dataS.d
                        datosGenerales.value.segmento = data.Segmento
                    })
                    datosGenerales.value.tipoCliente = data.TipoCliente
                    if (data.CuentaCorporativa == 0) {
                        if (data.TipoCliente == 1 || data.TipoCliente == 5) {
                            datosGenerales.value.cuentaCorporativa = 10000
                        } else datosGenerales.value.cuentaCorporativa = -1
                    }
                    else datosGenerales.value.cuentaCorporativa = data.CuentaCorporativa
                    datosGenerales.value.contacto = data.Contacto
                    datosGenerales.value.email = data.Email
                    datosGenerales.value.calle = data.Calle
                    datosGenerales.value.colonia = data.Colonia
                    datosGenerales.value.numero = data.Numero
                    datosGenerales.value.cp = data.CP
                    datosGenerales.value.estado = data.Estado
                    datosGenerales.value.municipio = data.Municipio
                    datosGenerales.value.telefonos = data.Telefonos
                    datosGenerales.value.rfc = data.RFC
                    datosGenerales.value.regimenFiscal = data.RegimentFiscal
                    datosGenerales.value.asignacionDePedido = data.AsignacionDePedido
                    datosGenerales.value.pais = data.PaisId
                    obtenerEstados(data.PaisId).then((dataEstados) => {
                        datosGenerales_sharedData.value.estados = dataEstados.d
                        datosGenerales.value.estado = data.EstadoId
                    })
                }

                const limpiarDatosGenerales = () => {
                    datosGenerales.value.activo = true
                    datosGenerales.value.sucursal = false
                    datosGenerales.value.razonSocial = ''
                    datosGenerales.value.nombreComercial = ''
                    datosGenerales.value.uen = -1
                    datosGenerales.value.segmento = -1
                    datosGenerales.value.tipoCliente = -1
                    datosGenerales.value.cuentaCorporativa = -1
                    datosGenerales.value.contacto = ''
                    datosGenerales.value.email = ''
                    datosGenerales.value.calle = ''
                    datosGenerales.value.colonia = ''
                    datosGenerales.value.numero = ''
                    datosGenerales.value.cp = ''
                    datosGenerales.value.estado = -1
                    datosGenerales.value.municipio = ''
                    datosGenerales.value.telefonos = ''
                    datosGenerales.value.rfc = ''
                    datosGenerales.value.regimenFiscal = 0
                    datosGenerales.value.asignacionDePedido = -1
                    datosGenerales.value.pais = -1
                    datosGenerales_sharedData.value.estados = [{
                        Id: -1,
                        Descripcion: '- Seleccionar -'
                    }]
                }

                const validaCampos_datosGenerales = () => {
                    let valid = true
                    const datosGenerales_campos = ['dg_razonSocial', 'dg_nombreComercial', 'dg_uen', 'dg_segmento',
                        'dg_tipoCliente', 'dg_cuentaCorporativa', 'dg_calle', 'dg_colonia',
                        'dg_numero', 'dg_cp', 'dg_pais', 'dg_estado', 'dg_municipio', 'dg_rfc']

                    validateForm.datePicker = false
                    datosGenerales_campos.forEach((i) => {
                        let v = true
                        validateForm.input = document.getElementById(i)
                        switch (i) {
                            case 'dg_uen':
                            case 'dg_segmento':
                            case 'dg_tipoCliente':
                            case 'dg_cuentaCorporativa':
                            case 'dg_pais':
                            case 'dg_estado':
                                v = validateForm.isSelected()
                                break
                            default: v = validateForm.isEmpty()
                        }
                        valid = (valid && v)
                    })

                    if (!valid) {
                        jQuery('#datos-generales-tab').click()
                        return false
                    }

                    return valid
                }

                const obtenerModelo_datosGenerales = () => {
                    return {
                        activo: datosGenerales.value.activo,
                        sucursal: datosGenerales.value.sucursal,
                        razonSocial: datosGenerales.value.razonSocial,
                        nombreComercial: datosGenerales.value.nombreComercial,
                        uen: datosGenerales.value.uen,
                        segmento: datosGenerales.value.segmento,
                        tipoCliente: datosGenerales.value.tipoCliente,
                        cuentaCorporativa: datosGenerales.value.cuentaCorporativa,
                        contacto: datosGenerales.value.contacto,
                        email: datosGenerales.value.email,
                        calle: datosGenerales.value.calle,
                        numero: datosGenerales.value.numero,
                        cP: datosGenerales.value.cp,
                        colonia: datosGenerales.value.colonia,
                        municipio: datosGenerales.value.municipio,
                        estado: '',
                        telefonos: datosGenerales.value.telefonos,
                        rFC: datosGenerales.value.rfc,
                        regimentFiscal: datosGenerales.value.regimenFiscal,
                        asignacionDePedido: datosGenerales.value.asignacionDePedido,
                        vinculado: false,
                        paisId: datosGenerales.value.pais,
                        estadoId: datosGenerales.value.estado
                    }
                }
                //#endregion


                //#region (TAB) DireccionEntrega
                const direccionEntrega_sharedData = ref({
                    rutas: [],
                    tabla: [],
                    editMode: false,
                    selectedId: null,
                    estados: [{
                        Id: -1,
                        Descripcion: '- Seleccionar -'
                    }],
                })

                const direccionEntrega = ref({
                    direccionFiscal: false,
                    calle: null,
                    numero: null,
                    cp: null,
                    colonia: null,
                    municipio: null,
                    pais: -1,
                    estado: -1,
                    sector: null,
                    telefono: null,
                    fax: null,
                    cuadrante: null,
                    horaAm1: null,
                    horaAm1_startTime: { hours: 0, minutes: 0 },
                    horaAm2: null,
                    horaAm2_startTime: { hours: 0, minutes: 0 },
                    horaPm1: null,
                    horaPm1_startTime: { hours: 12, minutes: 0 },
                    horaPm2: null,
                    horaPm2_startTime: { hours: 12, minutes: 0 },
                    ruta: -1
                })

                const obtenerRutas = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerRutas', null).then((data) => {
                        direccionEntrega_sharedData.value.rutas = data.d
                    })
                }

                const agregarDireccionEntrega = (e) => {
                    e.preventDefault()

                    if (!validarFormularioDireccionEntrega()) return

                    showLoader()
                    const param = JSON.stringify({
                        req: {
                            id: 1,
                            calle: direccionEntrega.value.calle,
                            numero: direccionEntrega.value.numero,
                            cp: direccionEntrega.value.cp,
                            colonia: direccionEntrega.value.colonia,
                            municipio: direccionEntrega.value.municipio,
                            estado: direccionEntrega_sharedData.value.estados.find(x => x.Id == direccionEntrega.value.estado).Descripcion,
                            sector: direccionEntrega.value.sector,
                            telefono: direccionEntrega.value.telefono,
                            fax: direccionEntrega.value.fax,
                            horaAm1: document.getElementById('dp-input-horaAm1').value,
                            horaAm2: document.getElementById('dp-input-horaAm2').value,
                            horaPm1: document.getElementById('dp-input-horaPm1').value,
                            horaPm2: document.getElementById('dp-input-horaPm2').value,
                            rutaId: direccionEntrega.value.ruta,
                            ruta: direccionEntrega_sharedData.value.rutas.find(x => x.Id == direccionEntrega.value.ruta).Descripcion,
                            estadoId: direccionEntrega.value.estado,
                            paisId: direccionEntrega.value.pais,
                            pais: sharedData.value.paises.find(x => x.Id == direccionEntrega.value.pais).Descripcion
                        }
                    })

                    ajaxRequest('CatClientesV3.aspx/AgregarDireccionEntrega', param).then((data) => {
                        if (data.d.Estatus) {
                            obtenerTablaDireccionEntrega().then((tdata) => {
                                direccionEntrega_sharedData.value.tabla = tdata.d
                                limpiarDireccionEntrega()
                                hideLoader()
                            })
                        } else {
                            if (data.d.Mensaje === 'connClose') location.reload()
                            else {
                                hideLoader()
                                sweetAlertError(data.d.Mensaje)
                            }
                        }
                    })
                }

                const obtenerTablaDireccionEntrega = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerDtTableDireccionEntrega', null)
                }

                const limpiarDireccionEntrega = () => {
                    direccionEntrega.value.direccionFiscal = false
                    direccionEntrega.value.calle = null
                    direccionEntrega.value.numero = null
                    direccionEntrega.value.cp = null
                    direccionEntrega.value.colonia = null
                    direccionEntrega.value.municipio = null
                    direccionEntrega.value.pais = -1
                    direccionEntrega.value.estado = -1
                    direccionEntrega.value.sector = null
                    direccionEntrega.value.telefono = null
                    direccionEntrega.value.fax = null
                    direccionEntrega.cuadrante = null
                    direccionEntrega.value.horaAm1 = null
                    direccionEntrega.value.horaAm2 = null
                    direccionEntrega.value.horaPm1 = null
                    direccionEntrega.value.horaPm2 = null
                    direccionEntrega.value.ruta = -1

                    direccionEntrega_sharedData.value.tabla.forEach((i) => { i.Edit = false })
                    direccionEntrega_sharedData.value.editMode = false
                    direccionEntrega_sharedData.value.selectedId = null
                    direccionEntrega_sharedData.value.estados = [{
                        Id: -1,
                        Descripcion: '- Seleccionar -'
                    }]
                }

                const inicializar_direccionEntrega = (data) => {
                    direccionEntrega_sharedData.value.tabla = data
                }

                const seleccionarPaisDireccionEntrega = (e) => {
                    obtenerEstados(e.target.value).then((data) => {
                        direccionEntrega_sharedData.value.estados = data.d
                        direccionEntrega.value.estado = direccionEntrega_sharedData.value.estados[0].Id
                    })
                }

                const editarDireccionEntrega = (e, id) => {
                    e.preventDefault()

                    direccionEntrega_sharedData.value.tabla.forEach((i) => { i.Edit = false })
                    const row = direccionEntrega_sharedData.value.tabla.find(x => x.Id == id)
                    direccionEntrega_sharedData.value.selectedId = id

                    row.Edit = true
                    direccionEntrega.value.calle = row.Calle
                    direccionEntrega.value.numero = row.Numero
                    direccionEntrega.value.cp = row.CP
                    direccionEntrega.value.colonia = row.Colonia
                    direccionEntrega.value.municipio = row.Municipio
                    direccionEntrega.value.estado = row.Estado
                    direccionEntrega.value.sector = row.Sector
                    direccionEntrega.value.telefono = row.Telefono
                    direccionEntrega.value.fax = row.Fax

                    let h = row.HoraAm1.split(':')
                    direccionEntrega.value.horaAm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    direccionEntrega.value.horaAm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    h = row.HoraAm2.split(':')
                    direccionEntrega.value.horaAm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    direccionEntrega.value.horaAm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    h = row.HoraPm1.split(':')
                    direccionEntrega.value.horaPm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    direccionEntrega.value.horaPm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    h = row.HoraPm2.split(':')
                    direccionEntrega.value.horaPm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    direccionEntrega.value.horaPm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }

                    direccionEntrega.value.ruta = row.RutaId
                    direccionEntrega.value.pais = row.PaisId
                    obtenerEstados(row.PaisId).then((data) => {
                        direccionEntrega_sharedData.value.estados = data.d
                        direccionEntrega.value.estado = row.EstadoId
                    })

                    direccionEntrega_sharedData.value.editMode = true
                }

                const cancelarDireccionEntrega = (e) => {
                    e.preventDefault()
                    limpiarDireccionEntrega()
                }

                const eliminarDireccionEntrega = (e, id) => {
                    e.preventDefault()

                    sweetAlertWarning('Eliminar dirección de entrega').then((result) => {
                        if (result.isConfirmed) {
                            showLoader()
                            const param = JSON.stringify({
                                idDireccionEntrega: id
                            })

                            ajaxRequest('CatClientesV3.aspx/EliminarDireccionEntrega', param).then((data) => {
                                if (data.d.Estatus) {
                                    obtenerTablaDireccionEntrega().then((data) => {
                                        direccionEntrega_sharedData.value.tabla = data.d
                                        limpiarDireccionEntrega()
                                        hideLoader()
                                    })
                                } else {
                                    if (data.d.Mensaje == 'connClose') location.reload()
                                    else {
                                        hideLoader()
                                        sweetAlertError(data.d.Mensaje)
                                    }
                                }
                            })
                        }
                    })
                }

                const actualizarDireccionEntrega = (e) => {
                    e.preventDefault()

                    if (!validarFormularioDireccionEntrega()) return

                    showLoader()

                    const param = JSON.stringify({
                        req: {
                            id: direccionEntrega_sharedData.value.selectedId,
                            calle: direccionEntrega.value.calle,
                            numero: direccionEntrega.value.numero,
                            cp: direccionEntrega.value.cp,
                            colonia: direccionEntrega.value.colonia,
                            municipio: direccionEntrega.value.municipio,
                            estado: direccionEntrega_sharedData.value.estados.find(x => x.Id == direccionEntrega.value.estado).Descripcion,
                            sector: direccionEntrega.value.sector,
                            telefono: direccionEntrega.value.telefono,
                            fax: direccionEntrega.value.fax,
                            horaAm1: document.getElementById('dp-input-horaAm1').value,
                            horaAm2: document.getElementById('dp-input-horaAm2').value,
                            horaPm1: document.getElementById('dp-input-horaPm1').value,
                            horaPm2: document.getElementById('dp-input-horaPm2').value,
                            rutaId: direccionEntrega.value.ruta,
                            ruta: direccionEntrega_sharedData.value.rutas.find(x => x.Id == direccionEntrega.value.ruta).Descripcion,
                            estadoId: direccionEntrega.value.estado,
                            paisId: direccionEntrega.value.pais,
                            pais: sharedData.value.paises.find(x => x.Id == direccionEntrega.value.pais).Descripcion
                        }
                    })

                    ajaxRequest('CatClientesV3.aspx/ActualizarDireccionEntrega', param).then((data) => {
                        if (data.d.Estatus) {
                            obtenerTablaDireccionEntrega().then((tdata) => {
                                direccionEntrega_sharedData.value.tabla = tdata.d
                                limpiarDireccionEntrega()
                                hideLoader()
                            })
                        } else {
                            if (data.d.Mensaje == 'connClose') location.reload()
                            else {
                                hideLoader()
                                sweetAlertError(data.d.Mensaje)
                            }
                        }
                    })
                }

                const validarFormularioDireccionEntrega = () => {
                    let valid = true
                    const direccionEntrega_campos = ['de_ruta', 'de_pais', 'de_estado']

                    validateForm.datePicker = false
                    direccionEntrega_campos.forEach((i) => {
                        let v = true
                        validateForm.input = document.getElementById(i)

                        v = validateForm.isSelected()
                        valid = (valid && v)
                    })

                    if (!valid) return

                    if (direccionEntrega.value.horaAm1 === null || direccionEntrega.value.horaAm2 === null ||
                        direccionEntrega.value.horaPm1 === null || direccionEntrega.value.horaPm2 === null) {

                        sweetAlertError('Los horarios de recepción son requeridos')
                        return
                    }

                    return valid
                }

                const cloneInfo = (e) => {
                    e.preventDefault()

                    if (direccionEntrega.value.direccionFiscal) {
                        direccionEntrega.value.calle = datosGenerales.value.calle
                        direccionEntrega.value.colonia = datosGenerales.value.colonia
                        direccionEntrega.value.numero = datosGenerales.value.numero
                        direccionEntrega.value.cp = datosGenerales.value.cp
                        direccionEntrega.value.pais = datosGenerales.value.pais
                        obtenerEstados(direccionEntrega.value.pais).then((data) => {
                            direccionEntrega_sharedData.value.estados = data.d
                            direccionEntrega.value.estado = datosGenerales.value.estado
                        })
                        direccionEntrega.value.municipio = datosGenerales.value.municipio
                        direccionEntrega.value.telefono = datosGenerales.value.telefonos
                    } else {
                        direccionEntrega.value.calle = ''
                        direccionEntrega.value.colonia = ''
                        direccionEntrega.value.numero = ''
                        direccionEntrega.value.cp = ''
                        direccionEntrega.value.pais = -1
                        direccionEntrega.value.estado = -1
                        direccionEntrega_sharedData.value.estados = [{
                            Id: -1,
                            Descripcion: '- Seleccionar -'
                        }]
                        direccionEntrega.value.municipio = ''
                        direccionEntrega.value.telefono = ''
                    }
                }
                //#endregion


                //#region (TAB) Territorios
                const territorios_sharedData = ref({
                    territorios: [],
                    territoriosServicio: [],
                    tabla: [],
                    territorioPadre: true,
                    selectedId: null,
                    mostrarAdvertenciaTerritorio: false
                })

                const territorio = ref({
                    territorio: -1,
                    territorioServicio: -1,
                    territorioServicioTecnico: -1, 
                    idRepresentanteServicio: null,
                    representanteServicio: '...',
                    idRepresentanteTerritorio: null,
                    representanteTerritorio: '...',
                    idRepresentanteServicioTecnico: null, 
                    representanteServicioTecnico: '...', 
                    territoriosPendientesPorAceptar: false
                })

                const obtenerTerritorios = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerTerritorios', null).then((data) => {
                        territorios_sharedData.value.territorios = data.d
                    })
                }

                const obtenerTerritoriosServicio = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerTerritoriosServicio', null).then((data) => {
                        territorios_sharedData.value.territoriosServicio = data.d
                    })
                }

                const obtenerRepresentanteTerritorio = (e, representanteServicio) => {
                    showLoader()
                    const param = JSON.stringify({ idTerritorio: e.target.value })

                    return ajaxRequest('CatClientesV3.aspx/ObtenerRepresentantePorTerritorio', param).then((data) => {
                        if (data.d.Mensaje == 'connClose') location.reload()
                        else {
                            if (representanteServicio) {
                                territorio.value.idRepresentanteServicio = data.d.Id
                                territorio.value.representanteServicio = data.d.Representante
                            }
                            else {
                                territorio.value.idRepresentanteTerritorio = data.d.Id
                                territorio.value.representanteTerritorio = data.d.Representante
                            }

                            hideLoader()
                        }
                    })
                }

                // Agregar función para obtener territorios de servicio técnico
                const obtenerTerritoriosServicioTecnico = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerTerritoriosServicioTecnico', null).then((data) => {
                        territorios_sharedData.value.territorioServicioTecnico = data.d
                    })
                }

                // Agregar función para obtener representante de territorio técnico
                const obtenerRepresentanteTerritorioTecnico = (e, representanteServicioTecnico) => {
                    showLoader()
                    const param = JSON.stringify({ idTerritorio: e.target.value })

                    return ajaxRequest('CatClientesV3.aspx/ObtenerRepresentantePorTerritorio', param).then((data) => {
                        if (data.d.Mensaje == 'connClose') location.reload()
                        else {
                            if (representanteServicioTecnico) {
                                territorio.value.idRepresentanteServicioTecnico = data.d.Id
                                territorio.value.representanteServicioTecnico = data.d.Representante
                            }
                            hideLoader()
                        }
                    })
                }

                const agregarNuevoTerritorio = (e) => {
                    e.preventDefault()

                    if (!validarFormularioTerritorio()) return

                    showLoader()
                    const req = JSON.stringify({
                        req: {
                            id: 1,
                            activo: true,
                            idCliente: datosGenerales.value.idCliente,
                            cliente: datosGenerales.value.razonSocial,
                            idTerritorio: territorio.value.territorio,
                            territorio: territorios_sharedData.value.territorios.find(x => x.Id == territorio.value.territorio).Descripcion,
                            idRik: territorio.value.idRepresentanteTerritorio,
                            rik: territorio.value.representanteTerritorio,
                            idTerServ: territorio.value.territorioServicio,
                            terServ: territorios_sharedData.value.territoriosServicio.find(x => x.Id == territorio.value.territorioServicio).Descripcion,
                            idRikServ: territorio.value.idRepresentanteServicio || -1,
                            rikServ: territorio.value.representanteServicio,
                            // campos para territorio de servicio técnico
                            idTerServTecnico: territorio.value.territorioServicioTecnico || -1,
                            terServTecnico: territorio.value.territorioServicioTecnico > 0
                                ? territorios_sharedData.value.territorioServicioTecnico.find(x => x.Id == territorio.value.territorioServicioTecnico).Descripcion
                                : '',
                            idRikServTecnico: territorio.value.idRepresentanteServicioTecnico || -1,
                            rikServTecnico: territorio.value.representanteServicioTecnico || '',
                            // Fin campos territorio servicio técnico
                            fechaSolicitud: null,
                            fechaAutorizado: null,
                            fechaRechazado: null,
                            territorioPadre: territorios_sharedData.value.territorioPadre
                        }
                    })

                    ajaxRequest('CatClientesV3.aspx/AgregarTerritorio', req).then((data) => {
                        if (data.d.Estatus) {
                            obtenerTablaTerritorios().then((tdata) => {
                                territorios_sharedData.value.tabla = tdata.d
                                const idTerritorio = territorio.value.territorio
                                limpiarTerritorio()
                                //territorios_sharedData.value.territorioPadre = false
                                hideLoader()
                                if (idTerritorio.toString().substring(0, 1) != 6 && data.d.EnviarComentarios) openModalComentarios(idTerritorio)
                            })
                        } else {
                            if (data.d.Mensaje === 'connClose') location.reload()
                            else {
                                hideLoader()
                                sweetAlertError(data.d.Mensaje)
                            }
                        }
                    })
                }

                const validarFormularioTerritorio = () => {
                    let valid = true

                    validateForm.input = document.getElementById('t_territorio')
                    validateForm.datePicker = false
                    valid = validateForm.isSelected()

                    if (territorios_sharedData.value.tabla.length > 0 && territorio.value.territorio != -1) {
                        if (territorios_sharedData.value.tabla.some(x => x.IdTerritorio == territorio.value.territorio) && territorios_sharedData.value.selectedId == null) {
                            sweetAlertError('Ya existe el territorio')
                            return false
                        }
                    }

                    if (!datosGenerales.value.razonSocial) {

                        sweetAlertError('Se debe capturar el nombre de razón social en datos generales')
                        return false
                    }

                    return valid
                }

                const obtenerTablaTerritorios = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerDtTableTerritorios', null)
                }

                const limpiarTerritorio = () => {
                    territorio.value.territorio = -1
                    territorio.value.territorioServicio = -1
                    territorio.value.territorioServicioTecnico = -1
                    territorio.value.idRepresentanteServicio = null
                    territorio.value.representanteServicio = '...'
                    territorio.value.idRepresentanteTerritorio = null
                    territorio.value.representanteTerritorio = '...'
                    territorio.value.idRepresentanteServicioTecnico = null 
                    territorio.value.representanteServicioTecnico = '...' 
                    territorios_sharedData.value.selectedId = null
                    territorios_sharedData.value.mostrarAdvertenciaTerritorio = false

                    territorios_sharedData.value.tabla.forEach((i) => { i.Edit = false })
                    territorios_sharedData.value.editMode = false
                    territorio.value.territoriosPendientesPorAceptar = false
                }

                const inicializar_territorios = (data, territoriosPendientesPorAceptar) => {
                    territorios_sharedData.value.tabla = data
                    territorios_sharedData.value.territorioPadre = territorios_sharedData.value.tabla.every(x => !x.TerritorioPadre)
                    territorio.value.territoriosPendientesPorAceptar = territoriosPendientesPorAceptar
                }

                const cancelarTerritorio = (e) => {
                    e.preventDefault()
                    limpiarTerritorio()
                }

                const editarTerritorio = (e, id) => {
                    e.preventDefault()

                    territorios_sharedData.value.tabla.forEach((i) => { i.Edit = false })
                    const row = territorios_sharedData.value.tabla.find(x => x.Id == id)
                    territorios_sharedData.value.selectedId = id

                    row.Edit = true

                    territorio.value.territorio = row.IdTerritorio
                    territorio.value.territorioServicio = row.IdTerServ
                    territorio.value.idRepresentanteServicio = row.IdRikServ
                    territorio.value.representanteServicio = row.RikServ == '' ? '...' : row.RikServ
                    territorio.value.idRepresentanteTerritorio = row.IdRik
                    territorio.value.representanteTerritorio = row.Rik == '' ? '...' : row.Rik
                    territorio.value.territorioServicioTecnico = row.IdTerServTecnico || -1
                    territorio.value.idRepresentanteServicioTecnico = row.IdRIKServTecnico || null
                    territorio.value.representanteServicioTecnico = row.RikServTecnico == '' ? '...' : row.RikServTecnico
                    territorios_sharedData.value.editMode = true
                }

                const actualizarTerritorio = (e) => {
                    e.preventDefault()

                    if (!validarFormularioTerritorio()) return

                    showLoader()
                    const req = JSON.stringify({
                        req: {
                            id: territorios_sharedData.value.selectedId,
                            activo: true,
                            idCliente: datosGenerales.value.idCliente,
                            cliente: datosGenerales.value.razonSocial,
                            idTerritorio: territorio.value.territorio,
                            territorio: territorios_sharedData.value.territorios.find(x => x.Id == territorio.value.territorio).Descripcion,
                            idRik: territorio.value.idRepresentanteTerritorio,
                            rik: territorio.value.representanteTerritorio,
                            idTerServ: territorio.value.territorioServicio,
                            terServ: territorios_sharedData.value.territoriosServicio.find(x => x.Id == territorio.value.territorioServicio).Descripcion,
                            idRikServ: territorio.value.idRepresentanteServicio || -1,
                            rikServ: territorio.value.representanteServicio,
                            IdTerServTecnico: territorio.value.territorioServicioTecnico || -1,
                            TerServTecnico: territorio.value.territorioServicioTecnico > 0
                                ? territorios_sharedData.value.territorioServicioTecnico.find(x => x.Id == territorio.value.territorioServicioTecnico).Descripcion
                                : '',
                            IdRikServTecnico: territorio.value.idRepresentanteServicioTecnico || -1,
                            RikServTecnico: territorio.value.representanteServicioTecnico || '',
                            fechaSolicitud: null,
                            fechaAutorizado: null,
                            fechaRechazado: null,
                            territorioPadre: territorios_sharedData.value.territorioPadre
                        }
                    })

                    ajaxRequest('CatClientesV3.aspx/ActualizarTerritorio', req).then((data) => {
                        if (data.d.Estatus) {
                            obtenerTablaTerritorios().then((tdata) => {
                                territorios_sharedData.value.tabla = tdata.d
                                const idTerritorio = territorio.value.territorio
                                limpiarTerritorio()
                                //territorios_sharedData.value.territorioPadre = false
                                territorios_sharedData.value.mostrarAdvertenciaTerritorio = true
                                hideLoader()
                                if (data.d.EnviarComentarios) openModalComentarios(idTerritorio)
                            })
                        } else {
                            if (data.d.Mensaje === 'connClose') location.reload()
                            else {
                                hideLoader()
                                sweetAlertError(data.d.Mensaje)
                            }
                        }
                    })
                }

                const eliminarTerritorio = (e, id) => {
                    e.preventDefault()
                    if (sharedData.value.clienteSeleccionado && !sharedData.value.permisoModificarTerritorios) return

                    sweetAlertWarning('Eliminar territorio').then((result) => {
                        if (result.isConfirmed) {
                            showLoader()
                            const nuevoClienteYTerritorioPadre = (!sharedData.value.clienteSeleccionado && territorios_sharedData.value.territorioPadre)
                            const param = JSON.stringify({
                                idCliente: datosGenerales.value.idCliente,
                                idTerritorio: id,
                                nuevoClienteYTerritorioPadre: nuevoClienteYTerritorioPadre
                            })

                            ajaxRequest('CatClientesV3.aspx/EliminarTerritorio', param).then((data) => {
                                if (data.d.Estatus) {
                                    obtenerTablaTerritorios().then((data) => {
                                        territorios_sharedData.value.tabla = data.d
                                        limpiarTerritorio()
                                        hideLoader()
                                        territorios_sharedData.value.mostrarAdvertenciaTerritorio = !nuevoClienteYTerritorioPadre
                                    })
                                } else {
                                    if (data.d.Mensaje === 'connClose') location.reload()
                                    else {
                                        hideLoader()
                                        sweetAlertError(data.d.Mensaje)
                                    }
                                }
                            })
                        }
                    })
                }

                const cambiarEstatusActivo = (e, id) => {

                    showLoader()
                    const territorioSeleccionado = territorios_sharedData.value.tabla.find(x => x.IdTerritorio == id)
                    const terServ = territorios_sharedData.value.territoriosServicio.find(x => x.Id == territorioSeleccionado.IdTerServ)
                    const territorio = territorios_sharedData.value.territorios.find(x => x.Id == id)
                    const req = JSON.stringify({
                        req: {
                            id: territorioSeleccionado.Id,
                            idCliente: datosGenerales.value.idCliente,
                            cliente: datosGenerales.value.razonSocial,
                            idTerritorio: id,
                            territorio: territorio != null ? territorio.Descripcion : '',
                            idRik: territorioSeleccionado.IdRik,
                            rik: territorioSeleccionado.Rik,
                            idTerServ: territorioSeleccionado.IdTerServ,
                            terServ: terServ != null ? terServ.Descripcion : '',
                            idRikServ: territorioSeleccionado.IdRikServ || -1,
                            rikServ: territorioSeleccionado.RikServ,
                        },
                        status: e.target.checked
                    })

                    ajaxRequest('CatClientesV3.aspx/CambiarStatusActivoTerritorio', req).then((data) => {
                        if (data.d.Estatus) {
                            obtenerTablaTerritorios().then((tdata) => {
                                territorios_sharedData.value.tabla = tdata.d
                                limpiarTerritorio()
                                hideLoader()
                                if (data.d.EnviarComentarios) openModalComentarios(id)
                            })
                        } else {
                            if (data.d.Mensaje === 'connClose') location.reload()
                            else {
                                hideLoader()
                                sweetAlertError(data.d.Mensaje)
                            }
                        }
                    })
                }

                const guardarComentario = (e) => {
                    e.preventDefault()
                    showLoader()
                    ajaxRequest('CatClientesV3.aspx/AgregarComentarioTerritorio', null).then(() => {
                        hideLoader()
                        territorios_sharedData.value.mostrarAdvertenciaTerritorio = true
                    })
                }
                //#endregion


                //#region (TAB) Cobranza
                const cobranza_sharedData = ref({
                    revision_horaAm1_startTime: { hours: 0, minutes: 0 },
                    revision_horaAm2_startTime: { hours: 0, minutes: 0 },
                    revision_horaPm1_startTime: { hours: 12, minutes: 0 },
                    revision_horaPm2_startTime: { hours: 12, minutes: 0 },
                    pago_horaAm1_startTime: { hours: 0, minutes: 0 },
                    pago_horaAm2_startTime: { hours: 0, minutes: 0 },
                    pago_horaPm1_startTime: { hours: 12, minutes: 0 },
                    pago_horaPm2_startTime: { hours: 12, minutes: 0 },
                    formasDePago: [],
                    monedas: [],
                    adendas: [],
                    seriesNotaCargo: [],
                    seriesNotaCredito: [],
                    serieConsecutivo: [],
                    remisionesElectronicas: [],
                    versionesCFDI: [],
                    bancos: [],
                    metodosPago: [
                        { id: '-1', descripcion: '-- Seleccionar --' },
                        { id: 'PUE', descripcion: 'Pago en una sola exhibición' },
                        { id: 'PPD', descripcion: 'Pago en parcialidades o diferido' }
                    ],
                    usoCFDI: [
                        { id: '-1', descripcion: '-- Seleccionar --' },
                        /*{ id: 'G01', descripcion: 'Adquisición de mercancias' },
                        { id: 'G02', descripcion: 'Devoluciones, descuentos o bonificaciones' },
                        { id: 'G03', descripcion: 'Gastos en general' },
                        { id: 'I01', descripcion: 'Construcciones' },
                        { id: 'I02', descripcion: 'Mobilario y equipo de oficina por inversiones' },
                        { id: 'I03', descripcion: 'Equipo de transporte' },
                        { id: 'I04', descripcion: 'Equipo de computo y accesorios' },
                        { id: 'I05', descripcion: 'Dados, troqueles, moldes, matrices y herramental' },
                        { id: 'I06', descripcion: 'Comunicaciones telefónicas' },
                        { id: 'I07', descripcion: 'Comunicaciones satelitales' },
                        { id: 'I08', descripcion: 'Otra maquinaria y equipo' },
                        { id: 'D01', descripcion: 'Honorarios médicos, dentales y gastos hospitalarios' },
                        { id: 'D02', descripcion: 'Gastos médicos por incapacidad o discapacidad' },
                        { id: 'D03', descripcion: 'Gastos funerales' },
                        { id: 'D04', descripcion: 'Donativos' },
                        { id: 'D05', descripcion: 'Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)' },
                        { id: 'D06', descripcion: 'Aportaciones voluntarias al SAR' },
                        { id: 'D07', descripcion: 'Primas por seguros de gastos médicos' },
                        { id: 'D08', descripcion: 'Gastos de transportación escolar obligatoria' },
                        { id: 'D09', descripcion: 'Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones' },
                        { id: 'D10', descripcion: 'Pagos por servicios educativos (colegiaturas)' },
                        { id: 'P01', descripcion: 'Por definir' }*/
                    ],
                    dias: ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'],
                    diasCondicionesDePago: []
                })

                const cobranza = ref({
                    credito: false,
                    permiteFacturar: false,
                    creditoSuspendido: false,
                    enableCreditoSuspendido: true,
                    moneda: -1,
                    limiteCredito: 0,
                    condicionPagoDias: 1,
                    condicionPagoAutorizo: null,
                    correoEdoCuenta1: null,
                    correoEdoCuenta2: null,
                    correoEdoCuenta3: null,
                    //horario revision
                    revision_horaAm1: null,
                    revision_horaAm2: null,
                    revision_horaPm1: null,
                    revision_horaPm2: null,
                    semanaRevision1: 0,
                    semanaRevision2: 0,
                    diasRevision: [],
                    //horario pago
                    pago_horaAm1: null,
                    pago_horaAm2: null,
                    pago_horaPm1: null,
                    pago_horaPm2: null,
                    semanaPago: 0,
                    diasPago: [],
                    //
                    semanaRecepcion: 0,
                    diasRecepcion: [],
                    //
                    telefonoCobranza1: null,
                    telefonoCobranza2: null,
                    documento: null,
                    desgloseIva: false,
                    retencionIva: false,
                    porcientoRetencion: 0,
                    ivaCliente: false,
                    porcentajeIvaCliente: 0,
                    banco: -1,
                    numeroCuenta: null,
                    referenciaTecleada: null,
                    portal: null,
                    ultimos4Digitos: '',
                    usoCFDI: -1,
                    metodoPago: -1,
                    pagoUsoCFDI: -1,
                    pagoMetodoPago: -1,
                    pagoBanco: -1,
                    pagoNumeroCuenta: null,
                    pagoCorreos: null,
                    ncUsoCFDI: -1,
                    ncFormaPago: -1,
                    ncMetodoPago: -1,
                    formasPago: [],
                    ordenCompra: false,
                    comisiones: false,
                    ncFacturar: false,
                    porcentajeNCFacturar: null,
                    versionCFDI: 1,
                    remisionElectronica: 0,
                    serie: -1,
                    serieNC: -1,
                    serieNCargo: -1,
                    adenda: -1,
                    revPago: ''
                })

                const obtenerFormasDePago = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerFormasDePago', null).then((data) => {
                        cobranza_sharedData.value.formasDePago = data.d
                    })
                }

                const obtenerTMonedas = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerTMoneda', null).then((data) => {
                        cobranza_sharedData.value.monedas = data.d
                    })
                }

                const obtenerVersionCFDI = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerVersionCFDI', null).then((data) => {
                        cobranza_sharedData.value.versionesCFDI = data.d
                        if (data.d.length > 0) {
                            cobranza.value.versionCFDI = data.d[0].Id
                        }
                    })
                }

                const obtenerAdendas = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerAdenda', null).then((data) => {
                        cobranza_sharedData.value.adendas = data.d
                    })
                }

                const obtenerSeriesNotaCargo = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerSerieNotaCargo', null).then((data) => {
                        cobranza_sharedData.value.seriesNotaCargo = data.d
                    })
                }

                const obtenerSeriesNotaCredito = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerSerieNotaCredito', null).then((data) => {
                        cobranza_sharedData.value.seriesNotaCredito = data.d
                    })
                }

                const obtenerSerieConsecutivo = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerSerieConsecutivo', null).then((data) => {
                        cobranza_sharedData.value.serieConsecutivo = data.d
                    })
                }

                const obtenerRemisionesElectronicas = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerRemisionesElectronicas', null).then((data) => {
                        cobranza_sharedData.value.remisionesElectronicas = data.d
                    })
                }

                const obtenerBancos = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerBancos', null).then((data) => {
                        cobranza_sharedData.value.bancos = data.d
                    })
                }

                const obtenerDiasCondicionesDePago = () => {
                    return ajaxRequest('CatClientesV3.aspx/ObtenerDiasCondicionesDePago', null).then((data) => {
                        cobranza_sharedData.value.diasCondicionesDePago = data.d
                    })
                }

                const validCampos_cobranza = () => {
                    let valid = true
                    const cobranza_campos = ['c_moneda', 'c_porcentajeIvaCliente', 'dp-input-c_revisonHoraAm1',
                        'dp-input-c_revisonHoraAm2', 'dp-input-c_revisonHoraPm1', 'dp-input-c_revisonHoraPm2',
                        'dp-input-c_revisonPagoAm1', 'dp-input-c_revisonPagoAm2', 'dp-input-c_revisonPagoPm1',
                        'dp-input-c_revisonPagoPm2']

                    cobranza_campos.forEach((i) => {
                        let v = true
                        validateForm.input = document.getElementById(i)
                        switch (i) {
                            case 'c_moneda':
                                validateForm.datePicker = false
                                v = validateForm.isSelected()
                                break
                            case 'c_porcentajeIvaCliente':
                                validateForm.datePicker = false
                                if (cobranza.value.checkPorcentajeIvaCliente) v = validateForm.isNumber()
                                break
                            default:
                                validateForm.datePicker = true
                                v = validateForm.isEmpty()
                                break
                        }

                        valid = (valid && v)
                    })

                    if (!valid) {
                        jQuery('#cobranza-tab').click()
                        return valid
                    }

                    if (cobranza.value.diasRevision.length == 0) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('No se ha seleccionado ningún día de revisión')
                        return false
                    }

                    if (cobranza.value.diasPago.length == 0) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('No se ha seleccionado ningún día de pago')
                        return false
                    }

                    if (cobranza.value.diasRecepcion.length == 0) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('No se ha seleccionado ningún día de recepción')
                        return false
                    }

                    if (cobranza.value.revision_horaAm1.hours > cobranza.value.revision_horaAm2.hours) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('La 1era. hora de revisión a.m. no debe ser mayor que la 2da. hora a.m')
                        return false
                    }

                    if (cobranza.value.revision_horaPm1.hours > cobranza.value.revision_horaPm2.hours) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('La 1era. hora de revisión p.m. no debe ser mayor que la 2da. hora p.m.')
                        return false
                    }

                    if (cobranza.value.pago_horaAm1.hours > cobranza.value.pago_horaAm2.hours) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('La 1era. hora de pago a.m. no debe ser mayor que la 2da. hora de pago a.m')
                        return false
                    }

                    if (cobranza.value.pago_horaPm1.hours > cobranza.value.pago_horaPm2.hours) {
                        jQuery('#cobranza-tab').click()
                        sweetAlertError('La 1era. hora de pago p.m. no debe ser mayor que la 2da. hora de pago p.m.')
                        return false
                    }

                    return valid
                }

                const obtenerModelo_cobranza = () => {

                    let model = {
                        credito: cobranza.value.credito,
                        permiteFacturar: cobranza.value.permiteFacturar,
                        creditoSuspendido: cobranza.value.creditoSuspendido,
                        habilitarCreditoSuspendido: false,
                        moneda: cobranza.value.moneda,
                        limiteCredito: cobranza.value.limiteCredito,
                        condicionPagoDias: cobranza.value.condicionPagoDias,
                        condicionPagoAutorizo: cobranza.value.condicionPagoAutorizo,
                        correoEdoCuenta1: cobranza.value.correoEdoCuenta1,
                        correoEdoCuenta2: cobranza.value.correoEdoCuenta2,
                        correoEdoCuenta3: cobranza.value.correoEdoCuenta3,
                        rHoraAm1: document.getElementById('dp-input-c_revisonHoraAm1').value,
                        rHoraAm2: document.getElementById('dp-input-c_revisonHoraAm2').value,
                        rHoraPm1: document.getElementById('dp-input-c_revisonHoraPm1').value,
                        rHoraPm2: document.getElementById('dp-input-c_revisonHoraPm2').value,
                        semanaRevision1: cobranza.value.semanaRevision1,
                        semanaRevision2: cobranza.value.semanaRevision2,
                        rLunes: cobranza.value.diasRevision.includes('Lunes'),
                        rMartes: cobranza.value.diasRevision.includes('Martes'),
                        rMiercoles: cobranza.value.diasRevision.includes('Miércoles'),
                        rJueves: cobranza.value.diasRevision.includes('Jueves'),
                        rViernes: cobranza.value.diasRevision.includes('Viernes'),
                        rSabado: cobranza.value.diasRevision.includes('Sábado'),
                        rDomingo: cobranza.value.diasRevision.includes('Domingo'),
                        pHoraAm1: document.getElementById('dp-input-c_revisonPagoAm1').value,
                        pHoraAm2: document.getElementById('dp-input-c_revisonPagoAm2').value,
                        pHoraPm1: document.getElementById('dp-input-c_revisonPagoPm1').value,
                        pHoraPm2: document.getElementById('dp-input-c_revisonPagoPm2').value,
                        semanaPago: cobranza.value.semanaPago,
                        pLunes: cobranza.value.diasPago.includes('Lunes'),
                        pMartes: cobranza.value.diasPago.includes('Martes'),
                        pMiercoles: cobranza.value.diasPago.includes('Miércoles'),
                        pJueves: cobranza.value.diasPago.includes('Jueves'),
                        pViernes: cobranza.value.diasPago.includes('Viernes'),
                        pSabado: cobranza.value.diasPago.includes('Sábado'),
                        pDomingo: cobranza.value.diasPago.includes('Domingo'),
                        semanaRecepcion: cobranza.value.semanaRecepcion,
                        recLunes: cobranza.value.diasRecepcion.includes('Lunes'),
                        recMartes: cobranza.value.diasRecepcion.includes('Martes'),
                        recMiercoles: cobranza.value.diasRecepcion.includes('Miércoles'),
                        recJueves: cobranza.value.diasRecepcion.includes('Jueves'),
                        recViernes: cobranza.value.diasRecepcion.includes('Viernes'),
                        recSabado: cobranza.value.diasRecepcion.includes('Sábado'),
                        recDomingo: cobranza.value.diasRecepcion.includes('Domingo'),
                        telefonoCobranza1: cobranza.value.telefonoCobranza1,
                        telefonoCobranza2: cobranza.value.telefonoCobranza2,
                        documento: cobranza.value.documento,
                        desgloseIva: cobranza.value.desgloseIva,
                        retencionIva: cobranza.value.retencionIva,
                        porcientoRetencion: cobranza.value.porcientoRetencion,
                        ivaCliente: cobranza.value.ivaCliente,
                        porcentajeIvaCliente: cobranza.value.porcentajeIvaCliente,
                        banco: cobranza.value.banco,
                        numeroCuenta: cobranza.value.numeroCuenta,
                        referenciaTecleada: cobranza.value.referenciaTecleada,
                        portal: cobranza.value.portal,
                        uDigitos: cobranza.value.ultimos4Digitos,
                        usoCFDI: cobranza.value.usoCFDI,
                        metodoPago: cobranza.value.metodoPago,
                        pagoUsoCFDI: cobranza.value.pagoUsoCFDI,
                        pagoMetodoPago: cobranza.value.pagoMetodoPago,
                        pagoNumeroCuenta: cobranza.value.pagoNumeroCuenta,
                        pagoBanco: cobranza.value.pagoBanco,
                        pagoCorreos: cobranza.value.pagoCorreos,
                        nCUsoCFDI: cobranza.value.ncUsoCFDI,
                        nCFormaPago: cobranza.value.ncFormaPago,
                        nCMetodoPago: cobranza.value.ncMetodoPago,
                        formasDePago: cobranza.value.formasPago,
                        ordenCompra: cobranza.value.ordenCompra,
                        comisiones: cobranza.value.comisiones,
                        notaCreditoFacturar: cobranza.value.ncFacturar,
                        porcentajeNotaCreditoFacturar: cobranza.value.porcentajeNCFacturar,
                        versionCFDI: cobranza.value.versionCFDI,
                        remisionElectronica: cobranza.value.remisionElectronica,
                        serie: cobranza.value.serie,
                        serieNC: cobranza.value.serieNC,
                        serieNCargo: cobranza.value.serieNCargo,
                        adenda: cobranza.value.adenda,
                        revPago: cobranza.value.revPago
                    }

                    return model
                }

                const inicializar_cobranza = (data) => {
                    cobranza.value.credito = data.Credito
                    cobranza.value.permiteFacturar = data.PermiteFacturar
                    cobranza.value.creditoSuspendido = data.CreditoSuspendido
                    cobranza.value.moneda = data.Moneda
                    cobranza.value.limiteCredito = data.LimiteCredito
                    cobranza.value.condicionPagoDias = data.CondicionPagoDias
                    cobranza.value.condicionPagoAutorizo = data.CondicionPagoAutorizo
                    cobranza.value.correoEdoCuenta1 = data.CorreoEdoCuenta1
                    cobranza.value.correoEdoCuenta2 = data.CorreoEdoCuenta2
                    cobranza.value.correoEdoCuenta3 = data.CorreoEdoCuenta3

                    cobranza.value.enableCreditoSuspendido = data.EnableCreditoSuspendido

                    let h = null
                    if (data.RHoraAm1) {
                        h = data.RHoraAm1.split(':')
                        cobranza_sharedData.value.revision_horaAm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.revision_horaAm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.RHoraAm2) {
                        h = data.RHoraAm2.split(':')
                        cobranza_sharedData.value.revision_horaAm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.revision_horaAm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.RHoraPm1) {
                        h = data.RHoraPm1.split(':')
                        cobranza_sharedData.value.revision_horaPm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.revision_horaPm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.RHoraPm2) {
                        h = data.RHoraPm2.split(':')
                        cobranza_sharedData.value.revision_horaPm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.revision_horaPm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    cobranza.value.semanaRevision1 = data.SemanaRevision1
                    cobranza.value.semanaRevision2 = data.SemanaRevision2
                    cobranza.value.diasRevision = []
                    if (data.RLunes) cobranza.value.diasRevision.push('Lunes')
                    if (data.RMartes) cobranza.value.diasRevision.push('Martes')
                    if (data.RMiercoles) cobranza.value.diasRevision.push('Miércoles')
                    if (data.RJueves) cobranza.value.diasRevision.push('Jueves')
                    if (data.RViernes) cobranza.value.diasRevision.push('Viernes')
                    if (data.RSabado) cobranza.value.diasRevision.push('Sábado')
                    if (data.RDomingo) cobranza.value.diasRevision.push('Domingo')

                    if (data.PHoraAm1) {
                        h = data.PHoraAm1.split(':')
                        cobranza_sharedData.value.pago_horaAm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.pago_horaAm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.PHoraAm2) {
                        h = data.PHoraAm2.split(':')
                        cobranza_sharedData.value.pago_horaAm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.pago_horaAm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.PHoraPm1) {
                        h = data.PHoraPm1.split(':')
                        cobranza_sharedData.value.pago_horaPm1_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.pago_horaPm1 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    if (data.PHoraPm2) {
                        h = data.PHoraPm2.split(':')
                        cobranza_sharedData.value.pago_horaPm2_startTime = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                        cobranza.value.pago_horaPm2 = { hours: parseInt(h[0]), minutes: parseInt(h[1]) }
                    }
                    cobranza.value.semanaPago = data.SemanaPago
                    cobranza.value.diasPago = []
                    if (data.PLunes) cobranza.value.diasPago.push('Lunes')
                    if (data.PMartes) cobranza.value.diasPago.push('Martes')
                    if (data.PMiercoles) cobranza.value.diasPago.push('Miércoles')
                    if (data.PJueves) cobranza.value.diasPago.push('Jueves')
                    if (data.PViernes) cobranza.value.diasPago.push('Viernes')
                    if (data.PSabado) cobranza.value.diasPago.push('Sábado')
                    if (data.PDomingo) cobranza.value.diasPago.push('Domingo')

                    cobranza.value.semanaRecepcion = data.SemanaRecepcion
                    cobranza.value.diasRecepcion = []
                    if (data.RecLunes) cobranza.value.diasRecepcion.push('Lunes')
                    if (data.RecMartes) cobranza.value.diasRecepcion.push('Martes')
                    if (data.RecMiercoles) cobranza.value.diasRecepcion.push('Miércoles')
                    if (data.RecJueves) cobranza.value.diasRecepcion.push('Jueves')
                    if (data.RecViernes) cobranza.value.diasRecepcion.push('Viernes')
                    if (data.RecSabado) cobranza.value.diasRecepcion.push('Sábado')
                    if (data.RecDomingo) cobranza.value.diasRecepcion.push('Domingo')

                    cobranza.value.telefonoCobranza1 = data.TelefonoCobranza1
                    cobranza.value.telefonoCobranza2 = data.TelefonoCobranza2
                    cobranza.value.documento = data.Documento
                    cobranza.value.desgloseIva = data.DesgloseIva
                    cobranza.value.retencionIva = data.RetencionIva
                    cobranza.value.porcientoRetencion = data.PorcientoRetencion
                    cobranza.value.ivaCliente = data.IvaCliente
                    cobranza.value.porcentajeIvaCliente = data.PorcentajeIvaCliente
                    cobranza.value.banco = data.Banco
                    cobranza.value.numeroCuenta = data.NumeroCuenta
                    cobranza.value.referenciaTecleada = data.ReferenciaTecleada
                    cobranza.value.portal = data.Portal
                    cobranza.value.ultimos4Digitos = data.UDigitos
                    cobranza.value.usoCFDI = data.UsoCFDI
                    cobranza.value.metodoPago = data.MetodoPago
                    cobranza.value.pagoUsoCFDI = data.PagoUsoCFDI
                    cobranza.value.pagoMetodoPago = data.PagoMetodoPago
                    cobranza.value.pagoBanco = data.PagoBanco
                    cobranza.value.pagoNumeroCuenta = data.PagoNumeroCuenta
                    cobranza.value.pagoCorreos = data.PagoCorreos
                    cobranza.value.ncUsoCFDI = data.NCUsoCFDI
                    cobranza.value.ncFormaPago = data.NCFormaPago
                    cobranza.value.ncMetodoPago = data.NCMetodoPago
                    cobranza.value.formasPago = data.FormasDePago
                    cobranza.value.ordenCompra = data.OrdenCompra
                    cobranza.value.comisiones = data.Comisiones
                    cobranza.value.ncFacturar = data.NotaCreditoFacturar
                    cobranza.value.porcentajeNCFacturar = data.PorcentajeNotaCreditoFacturar
                    if (cobranza_sharedData.value.versionesCFDI.length > 0 && data.VersionCFDI !== null) {
                        const id = cobranza_sharedData.value.versionesCFDI.find(x => x.Descripcion == data.VersionCFDI)
                        if (id !== undefined) cobranza.value.versionCFDI = cobranza_sharedData.value.versionesCFDI.find(x => x.Descripcion == data.VersionCFDI).Id
                        else cobranza.value.versionCFDI = cobranza_sharedData.value.versionesCFDI[0].Id
                    }
                    cobranza.value.remisionElectronica = data.RemisionElectronica
                    cobranza.value.serie = data.Serie
                    cobranza.value.serieNC = data.SerieNC
                    cobranza.value.serieNCargo = data.SerieNCargo
                    cobranza.value.adenda = data.Adenda
                    cobranza.value.revPago = data.RevPago
                }

                const limpiarCobranza = () => {
                    cobranza.value.credito = false
                    cobranza.value.permiteFacturar = false
                    cobranza.value.creditoSuspendido = false
                    cobranza.value.moneda = -1
                    cobranza.value.limiteCredito = 0
                    cobranza.value.condicionPagoDias = 1
                    cobranza.value.condicionPagoAutorizo = null
                    cobranza.value.correoEdoCuenta1 = null
                    cobranza.value.correoEdoCuenta2 = null
                    cobranza.value.correoEdoCuenta3 = null
                    cobranza.value.revision_horaAm1 = null
                    cobranza.value.revision_horaAm2 = null
                    cobranza.value.revision_horaPm1 = null
                    cobranza.value.revision_horaPm2 = null
                    cobranza.value.semanaRevision1 = 0
                    cobranza.value.semanaRevision2 = 0
                    cobranza.value.diasRevision = []
                    cobranza.value.pago_horaAm1 = null
                    cobranza.value.pago_horaAm2 = null
                    cobranza.value.pago_horaPm1 = null
                    cobranza.value.pago_horaPm2 = null
                    cobranza.value.semanaPago = 0
                    cobranza.value.diasPago = []
                    cobranza.value.semanaRecepcion = 0
                    cobranza.value.diasRecepcion = []
                    cobranza.value.telefonoCobranza1 = null
                    cobranza.value.telefonoCobranza2 = null
                    cobranza.value.documento = null
                    cobranza.value.desgloseIva = false
                    cobranza.value.retencionIva = false
                    cobranza.value.porcientoRetencion = 0
                    cobranza.value.ivaCliente = false
                    cobranza.value.porcentajeIvaCliente = 0
                    cobranza.value.banco = -1
                    cobranza.value.numeroCuenta = null
                    cobranza.value.referenciaTecleada = null
                    cobranza.value.portal = null
                    cobranza.value.ultimos4Digitos = ''
                    cobranza.value.usoCFDI = -1
                    cobranza.value.metodoPago = -1
                    cobranza.value.pagoUsoCFDI = -1
                    cobranza.value.pagoMetodoPago = -1
                    cobranza.value.pagoBanco = -1
                    cobranza.value.pagoNumeroCuenta = null
                    cobranza.value.pagoCorreos = null
                    cobranza.value.ncUsoCFDI = -1
                    cobranza.value.ncFormaPago = -1
                    cobranza.value.ncMetodoPago = -1
                    cobranza.value.formasPago = []
                    cobranza.value.ordenCompra = false
                    cobranza.value.comisiones = false
                    cobranza.value.ncFacturar = false
                    cobranza.value.porcentajeNCFacturar = null
                    cobranza.value.versionCFDI = -1
                    cobranza.value.remisionElectronica = 0
                    cobranza.value.serie = -1
                    cobranza.value.serieNC = -1
                    cobranza.value.serieNCargo = -1
                    cobranza.value.adenda = -1
                    cobranza.value.revPago = ''
                    cobranza.value.enableCreditoSuspendido = true
                }
                //#endregion


                //#region (TAB) Bennets
                const catalogoAdicional = ref({
                    sucursal: '',
                    idCatGenerico: 0,
                    cat: [
                        {
                            id: -1,
                            value: -1
                        },
                        {
                            id: -1,
                            value: -1
                        },
                        {
                            id: -1,
                            value: -1
                        },
                        {
                            id: -1,
                            value: -1
                        },
                        {
                            id: -1,
                            value: -1
                        }
                    ]
                })

                const obtenerModelo_catalogoAdicional = () => {
                    if (!sharedData.value.mostrarTabBennets) return null

                    let c = []
                    catalogoAdicional.value.cat.forEach(x => {
                        c.push({
                            idCatGenerico: x.id,
                            idCatGenericoDetalle: x.value
                        })
                    })
                    return {
                        sucursal: catalogoAdicional.value.sucursal,
                        idCatGenerico: catalogoAdicional.value.idCatGenerico,
                        cat: c
                    }
                }

                const limpiarCatalogoAdicional = () => {
                    catalogoAdicional.value.sucursal = ''
                    catalogoAdicional.value.idCatGenerico = sharedData.value.bennetsCatalogoAdicional[0].IdCatGenerico
                    catalogoAdicional.value.cat[0].id = sharedData.value.bennetsCatalogoAdicional[1].IdCatGenerico
                    catalogoAdicional.value.cat[0].value = sharedData.value.bennetsCatalogoAdicional[1].CatGenericoDetalle[0].Id
                    catalogoAdicional.value.cat[1].id = sharedData.value.bennetsCatalogoAdicional[2].IdCatGenerico
                    catalogoAdicional.value.cat[1].value = sharedData.value.bennetsCatalogoAdicional[2].CatGenericoDetalle[0].Id
                    catalogoAdicional.value.cat[2].id = sharedData.value.bennetsCatalogoAdicional[3].IdCatGenerico
                    catalogoAdicional.value.cat[2].value = sharedData.value.bennetsCatalogoAdicional[3].CatGenericoDetalle[0].Id
                    catalogoAdicional.value.cat[3].id = sharedData.value.bennetsCatalogoAdicional[4].IdCatGenerico
                    catalogoAdicional.value.cat[3].value = sharedData.value.bennetsCatalogoAdicional[4].CatGenericoDetalle[0].Id
                    catalogoAdicional.value.cat[4].id = sharedData.value.bennetsCatalogoAdicional[5].IdCatGenerico
                    catalogoAdicional.value.cat[4].value = sharedData.value.bennetsCatalogoAdicional[5].CatGenericoDetalle[0].Id
                }

                const inicializar_catalogoAdicional = (data) => {
                    if (sharedData.value.mostrarTabBennets && data.Sucursal && data.Cat) {
                        catalogoAdicional.value.sucursal = data.Sucursal
                        catalogoAdicional.value.idCatGenerico = data.IdCatGenerico
                        catalogoAdicional.value.cat[0].value = data.Cat[0].IdCatGenericoDetalle
                        catalogoAdicional.value.cat[1].value = data.Cat[1].IdCatGenericoDetalle
                        catalogoAdicional.value.cat[2].value = data.Cat[2].IdCatGenericoDetalle
                        catalogoAdicional.value.cat[3].value = data.Cat[3].IdCatGenericoDetalle
                        catalogoAdicional.value.cat[4].value = data.Cat[4].IdCatGenericoDetalle
                    }
                }
                //#endregion


                //#region Helper
                const ajaxRequest = (url, data) => {
                    return new Promise((resolve, reject) => {
                        jQuery.ajax({
                            type: 'post',
                            url: url,
                            data: data,
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (data) {
                                resolve(data)
                            },
                            error: function (error) {
                                reject(error)
                            }
                        })
                    })
                }

                const sweetAlertSuccess = (mensaje) => {
                    swal.fire({
                        icon: 'success',
                        title: mensaje,
                        timer: 2000,
                        showConfirmButton: false
                    })
                }

                const sweetAlertError = (mensaje) => {
                    swal.fire({
                        icon: 'warning',
                        title: mensaje,
                        showConfirmButton: false,
                        timer: 3000
                    })
                }

                const sweetAlertWarning = (mensaje) => {
                    return swal.fire({
                        icon: 'warning',
                        title: mensaje,
                        text: '¡No podrás revertir esto!',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Eliminar',
                        cancelButtonText: 'Cancelar'
                    })
                }

                const showLoader = () => {
                    spinner.value.classList.add('show')
                }

                const hideLoader = () => {
                    spinner.value.classList.remove('show')
                }

                const maxLength = (e, length) => {
                    if (e.target.value.length > length) e.target.value = e.target.value.slice(0, length)
                }

                const alphanumeric = (e) => {
                    const char = String.fromCharCode(e.keyCode)
                    if (/^[\w\-\s]+$/.test(char)) return true
                    else e.preventDefault()
                }

                let validateForm = {
                    input: null,
                    datePicker: false,
                    isEmpty: () => {
                        if (validateForm.input.value != '') return validateForm.success()
                        else return validateForm.error()
                    },
                    isSelected: () => {
                        if (validateForm.input.value != '-1') return validateForm.success()
                        else return validateForm.error()
                    },
                    isNumber: () => {
                        if (validateForm.input.value > 0) return validateForm.success()
                        else return validateForm.error()
                    },
                    success: () => {
                        let elm = null
                        if (validateForm.datePicker)
                            elm = validateForm.input.parentElement.parentElement.parentElement.nextElementSibling
                        else elm = validateForm.input.nextSibling

                        elm.style.display = 'none'
                        return true
                    },
                    error: () => {
                        let elm = null
                        if (validateForm.datePicker)
                            elm = validateForm.input.parentElement.parentElement.parentElement.nextElementSibling
                        else elm = validateForm.input.nextSibling

                        elm.style.display = 'block'
                        return false
                    }
                }

                const titleModal = ref('')
                const openModalComentarios = (idTerritorio) => {
                    var iframe = document.getElementById('iframeContent')
                    iframe.src = 'VentanaComentariosTerritoriosV2.aspx?Id_Ter=' + idTerritorio;
                    iframe.style.height = '330px'
                    titleModal.value = 'Motivo de cambio'

                    jQuery('#iframeModal').modal('show')
                }

                const openModalVinculacionCliente = (rfc) => {
                    let iframe = document.getElementById('iframeContent')
                    iframe.src = 'AutorizacionVinculacionV2.aspx?rfc=' + rfc
                    iframe.style.height = '150px'
                    titleModal.value = 'Autorización cliente'

                    jQuery('#iframeModal').modal('show')
                }

                const openWindowAutorizacionTipoCliente = () => {
                    let iframe = document.getElementById('iframeContentTipoCliente')
                    iframe.src = 'Ventana_AutorizacionTipoClienteV2.aspx'
                    iframe.style.height = '280px'
                    titleModal.value = 'Autorización tipo cliente'

                    jQuery('#iframeModalTipoCliente').modal('show')
                }

                const openWindowAutorizacion = () => {
                    let iframe = document.getElementById('iframeContentTipoCliente')
                    iframe.src = 'Ventana_AutorizacionV2.aspx'
                    iframe.style.height = '280px'
                    titleModal.value = 'Autorización'

                    jQuery('#iframeModalTipoCliente').modal('show')
                }

                //#endregion


                return {
                    spinner,
                    cliente,
                    sharedData,
                    seleccionarCliente,
                    cambiarCentroDistribucion,
                    borrarBuscadorCliente,
                    deshabilitarCliente,
                    nuevoCliente,
                    guardarNuevoCliente,
                    datosGenerales_sharedData,
                    datosGenerales,
                    seleccionarUenDatosGenerales,
                    seleccionarPaisDatosGenerales,
                    consultaTipoDeCliente,
                    direccionEntrega_sharedData,
                    direccionEntrega,
                    agregarDireccionEntrega,
                    seleccionarPaisDireccionEntrega,
                    editarDireccionEntrega,
                    eliminarDireccionEntrega,
                    cancelarDireccionEntrega,
                    actualizarDireccionEntrega,
                    cloneInfo,
                    obtenerTerritoriosServicioTecnico,
                    obtenerRepresentanteTerritorioTecnico,
                    territorios_sharedData,
                    territorio,
                    guardarComentario,
                    obtenerRepresentanteTerritorio,
                    eliminarTerritorio,
                    agregarNuevoTerritorio,
                    actualizarTerritorio,
                    editarTerritorio,
                    cancelarTerritorio,
                    cambiarEstatusActivo,
                    cobranza_sharedData,
                    cobranza,
                    maxLength,
                    alphanumeric,
                    titleModal,
                    catalogoAdicional
                }
            }
        })

        app.mount('#app_catclientes')

        function load() { }
    </script>
    <script>
        jQuery(document).ready(function () {
            jQuery('#myTab a').click(function (e) {
                if (jQuery(this).hasClass('disabled')) {
                    e.preventDefault()
                    return false
                }

                jQuery(this).tab('show')

                jQuery('#datos-generales-tab').show()
                jQuery('#direccion-entrega-tab').show()
                jQuery('#cliente-territorio-tab').show()
                jQuery('#cobranza-tab').show()
            })
        })

        function closeModalWindow(operation) {
            jQuery('#iframeModal').modal('hide')
            jQuery('#iframeModalTipoCliente').modal('hide')
            document.getElementById('iframeContent').src = 'about:blank'
            document.getElementById('iframeContentTipoCliente').src = 'about:blank'

            if (operation === 'vinculacioncliente' || operation === 'autorizaciontipocliente' || operation === 'autorizar') {
                const btn = jQuery('#guardarCliente')
                if (btn.length > 0) btn.trigger('click')
                else jQuery('#modificarCliente').trigger('click')
            }
            else if (operation === 'onlyclose') {

            }
            else jQuery('#bandera_modal_abierto_cerrado').trigger('click')
        }
    </script>
</asp:Content>