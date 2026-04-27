<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/MasterPage/MasterPageNewDesign.Master"
    CodeBehind="ConfigCliente.aspx.cs"
    Inherits="SIANWEB.ConfigCliente" %>

<asp:Content runat="server" ContentPlaceHolderID="CPH">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11.4.4/dist/sweetalert2.min.css" />
    <style type="text/css">
        #app {
            font-size: 13px;
        }
        input {
            font-size:13px !important;
        }
    </style>
    <div id="app">
        <div class="container mt-4">
            <div class="row">
                <div class="col">
                    <h1>Bloqueo de permiso para facturar - clientes</h1>
                </div>
            </div>
            <div class="row align-items-end">
                <div class="col-4">
                    <div>
                        <label>B&uacute;squeda</label>
                        <input type="text" @keyup="getClients" placeholder="búsqueda por nombre / id" v-model="search" class="form-control" />
                    </div>
                </div>
                <div class="col-4"></div>
                <div class="col text-end">
                    <label>Carga a través de Excel</label>
                    <div class="input-group input-group-sm">
                        <input type="file" ref="inputExcel" class="form-control" @change="fileChange($event)">
                        <button class="btn btn-success" :disabled="excelFile == null" type="button" @click="uploadFile">Subir archivo</button>
                    </div>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col text-end">
                    <a href="Files/ExcelQuitarPermisoFacturar.xlsx">Descargar formato Excel</a>
                </div>
            </div>
            <div class="row mt-4">
                <div class="col">
                    <em>&Uacute;ltimos 10 clientes</em>
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Cliente</th>
                                <th>Permiso para facturar</th>
                                <th>Cr&eacute;dito suspendido</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="c in clients">
                                <td>{{ c.ClientId }}</td>
                                <td>{{ c.ClientName }}</td>
                                <td>
                                    <div class="form-check form-switch ms-3">
                                        <input
                                            class="form-check-input"
                                            type="checkbox"
                                            role="switch"
                                            @change="changeBillingStatus(c.ClientId, $event)"
                                            :checked="c.AllowsBilling"
                                            v-model="c.AllowsBilling" />
                                    </div>
                                </td>
                                <td>
                                    <div class="form-check form-switch ms-3">
                                        <input
                                            class="form-check-input"
                                            type="checkbox"
                                            role="switch"
                                            @change="changeCreditSuspended(c.ClientId, $event)"
                                            :checked="c.CreditSuspended"
                                            v-model="c.CreditSuspended"/>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:Label runat="server" ID="appIIsName" style="display:none;"></asp:Label>

    <script src="https://unpkg.com/vue@3/dist/vue.global.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.4.4/dist/sweetalert2.min.js"></script>

    <script>
        const { createApp, ref, onMounted, computed } = Vue
        createApp({
            setup() {
                let instance = null
                const clients = ref([])
                const search = ref('')
                const excelFile = ref(null)
                const excelFileName = ref('')
                const inputExcel = ref(null)

                onMounted(() => {
                    const appIIsName = '<%=appIIsName.Text%>' == '/' ? '' : '<%=appIIsName.Text%>'

                    instance = axios.create({
                        baseURL: appIIsName + '/ConfigCliente.aspx',
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8',
                            'DataType': 'json'
                        }
                    })

                    getClients()
                })

                const getClients = () => {
                    instance.post('/GetLast10Clients', { search: search.value }).then(response => {
                        clients.value = response.data.d
                    })
                }

                const changeBillingStatus = (id, e) => {
                    const v = e.target.checked
                    instance.post('/ChangeBillingStatus', { clientId: id, status: v }).then(response => {
                        if (!response.data.d.Status) {
                            clients.value.find(x => x.ClientId == id).CreditSuspended = !v
                        }
                    })
                }

                const changeCreditSuspended = (id, e) => {
                    const v = e.target.checked
                    instance.post('/ChangeCreditSuspended', { clientId: id, status: v }).then(response => {
                        if (!response.data.d.Status) {
                            clients.value.find(x => x.ClientId == id).AllowsBilling = !v
                        }
                    })
                }

                const fileChange = async (e) => {
                    excelFileName.value = e.target.files[0].name
                    excelFile.value = await toBase64(e.target.files[0])
                }

                const uploadFile = () => {
                    instance.post('/UploadExcel', {
                        base64: excelFile.value,
                        fileName: excelFileName.value
                    }).then(response => {
                        if (!response.data.d.Status) sweetAlertError(response.data.d.Message)
                        else {
                            sweetAlertSuccess('Excel cargado correctamente')
                            getClients()
                            inputExcel.value.value = null
                            excelFile.value = null
                        }
                    })
                }

                const toBase64 = file => new Promise((resolve, reject) => {
                    const reader = new FileReader()
                    reader.readAsDataURL(file)
                    reader.onload = () => resolve(reader.result.replace(/^.*,/, ''))
                    reader.onerror = reject
                })

                const sweetAlertError = (mensaje) => {
                    swal.fire({
                        icon: 'warning',
                        title: mensaje,
                        showConfirmButton: false,
                        timer: 3000
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

                return {
                    search,
                    getClients,
                    clients,
                    changeBillingStatus,
                    changeCreditSuspended,
                    fileChange,
                    uploadFile,
                    excelFile,
                    inputExcel
                }
            }
        }).mount('#app')

        function load() { }
    </script>
</asp:Content>