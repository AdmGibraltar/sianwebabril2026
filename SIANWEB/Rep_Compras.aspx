<%@ Page Title="Reporte Compras"
    Language="C#"
    MasterPageFile="~/MasterPage/MasterPageNewDesign.master"
    AutoEventWireup="true"
    CodeBehind="Rep_Compras.aspx.cs"
    Inherits="SIANWEB.Rep_Compras" %>

<asp:Content runat="server" ContentPlaceHolderID="CPH">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link type="text/css" rel="stylesheet" href="https://unpkg.com/vue-next-select/dist/index.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11.4.4/dist/sweetalert2.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/@vuepic/vue-datepicker@latest/dist/main.css">
    <style type="text/css">
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

        .vue-select {
            border: 1px solid #ced4da !important;
        }
        .vue-dropdown-item {
            font-size: 12px;
        }
        vue-input > input::placeholder {
            color: #212529;
        }
        .vue-dropdown{
            z-index:1000;
        }
    </style>

    <div id="app" class="container mt-3">
        <div ref="spinner" id="spinner">
            <img src="Imagenes/ajax-loader.gif" />
        </div>

        <div class="row">
            <div class="col">
                <h1>Reporte de compras</h1>
                <h4>Filtros</h4>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-6">
                <div class="mb-3">
                    <label>Proveedores</label>
                    <v-select
                        class="form-select w-100"
                        model-value=""
                        v-model="proveedor"
                        placeholder="Todos"
                        :search-placeholder="proveedores.length > 0 ? 'Escriba para buscar' : 'Todos'"
                        :options="proveedores"
                        @selected="seleccionarProveedor"
                        :clear-on-close="true"
                        :close-on-select="true"
                        :searchable="true"></v-select>
                    <div class="form-text">B&uacute;squeda de proveedor</div>
                </div>
            </div>
            <div class="col">
                <div class="mb-3">
                    <label>Fecha inicio</label>
                    <vue-date-picker v-model="model.fechaInicio" locale="es-MX" auto-apply :enable-time-picker="false" :format="format" />
                </div>
            </div>
            <div class="col">
                <div class="mb-3">
                    <label>Fecha final</label>
                    <vue-date-picker v-model="model.fechaFinal" locale="es-MX" auto-apply :enable-time-picker="false" :format="format" />
                </div>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col text-end">
                <button type="button" @click="getReport()" class="btn btn-info">Exportar Excel</button>
            </div>
        </div>
    </div>
    <asp:Label runat="server" ID="appIIsName" style="display:none;"></asp:Label>

<script src="https://unpkg.com/vue@3/dist/vue.global.prod.js"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@vuepic/vue-datepicker@7.4.0/dist/main.css">
<script src="https://cdn.jsdelivr.net/npm/@vuepic/vue-datepicker@7.4.0/dist/vue-datepicker.iife.min.js"></script>

   <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    <script src="https://unpkg.com/vue-next-select/dist/vue-next-select.iife.prod.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.4.4/dist/sweetalert2.min.js"></script>


<script type="text/javascript">
    const { createApp, onMounted, ref } = Vue;

    createApp({
        components: {
            VSelect: VueNextSelect,
            VueDatePicker: window["VueDatePicker"]
        },
        setup() {
            const spinner = ref();
            const proveedores = ref([]);
            const proveedor = -1;
            const model = ref({
                proveedor: -1,
                fechaInicio: null,
                fechaFinal: null
            });

            let instance = null;
            onMounted(() => {
                const appIISName = '<%=appIIsName.Text%>' == '/' ? '' : '<%=appIIsName.Text%>';
                instance = axios.create({
                    baseURL: `${appIISName}/Rep_Compras.aspx`,
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                        'DataType': 'json'
                    }
                });

                getProveedores();
                model.value.fechaFinal = new Date();
                let now = new Date();
                now.setMonth(now.getMonth() - 2);
                model.value.fechaInicio = now;
            });

            const getProveedores = () => {
                instance.post('/GetProveedores', {}).then(response => {
                    const r = response.data.d;
                    r.unshift({
                        Description: 'Todos',
                        Id: -1
                    });
                    proveedores.value = r.map(item => {
                        const txt = item.Id == -1
                            ? item.Description
                            : `${item.Id} - ${item.Description}`;
                        return txt;
                    });
                });
            };

            const seleccionarProveedor = (item) => {
                if (item == 'Todos') model.value.proveedor = -1;
                else model.value.proveedor = item.split('-')[0].trim();
            };

            const getReport = () => {
                showLoader();
                instance.post('/GetReport', {
                    model: {
                        proveedor: model.value.proveedor,
                        fechaInicio: model.value.fechaInicio,
                        fechaFinal: model.value.fechaFinal
                    }
                }).then(response => {
                    if (response.data.d.Estatus) {
                        hideLoader();
                        startDownload(response.data.d.Mensaje);
                    }
                    else {
                        hideLoader();
                        sweetAlertError(response.data.d.Mensaje);
                    }
                });
            };

            const showLoader = () => {
                spinner.value.classList.add('show');
            };

            const hideLoader = () => {
                spinner.value.classList.remove('show');
            };

            const sweetAlertError = (mensaje) => {
                Swal.fire({
                    icon: 'warning',
                    title: mensaje,
                    showConfirmButton: false,
                    timer: 3000
                });
            };

            const format = (date) => {
                const day = date.getDate();
                const month = date.getMonth() + 1;

                const _day = day.toString().length === 1 ? `0${day}` : day;
                const _month = month.toString().length == 1 ? `0${month}` : month;

                return `${_day}/${_month}/${date.getFullYear()}`;
            };

            return {
                spinner,
                model,
                proveedores,
                proveedor,
                seleccionarProveedor,
                format,
                getReport
            };
        }
    }).mount('#app');

    function load() { }
</script>
</asp:Content>