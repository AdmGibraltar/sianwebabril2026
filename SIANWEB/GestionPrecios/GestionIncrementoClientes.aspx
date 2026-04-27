<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="GestionIncrementoClientes.aspx.cs" Inherits="SIANWEB.GestionPrecios.GestionIncrementoClientes" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 

    <style>
            .banner-incremento-precios {
    background: linear-gradient(90deg, #ff9800 0%, #ff5722 100%);
    color: #fff;
    padding: 12px 20px;
    border-radius: 8px;
    margin-top: 10px;
    margin-bottom: 10px;
    font-size: 1.1em;
    font-weight: bold;
    box-shadow: 0 2px 8px rgba(255, 152, 0, 0.15);
    display: flex;
    align-items: center;
    animation: slideIn 1s ease;
}

.banner-incremento-precios .fa-exclamation-triangle {
    margin-right: 10px;
    font-size: 1.3em;
    animation: shake 1.2s infinite;
}

.banner-incremento-precios .blink {
    animation: blink 1s steps(2, start) infinite;
    margin-left: 8px;
}

@keyframes blink {
    to { visibility: hidden; }
}

@keyframes slideIn {
    from { transform: translateX(-100%); opacity: 0; }
    to { transform: translateX(0); opacity: 1; }
}

@keyframes shake {
    0%, 100% { transform: rotate(-10deg);}
    50% { transform: rotate(10deg);}
}
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
 
 
<!-- jQuery -->
<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>

<!-- Bootstrap CSS -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">

<!-- Bootstrap JS -->
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <%-- ZEBRA DATEPICKER --%>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    
     <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/css/key_acys.css?v=1")%>" rel="stylesheet">
<!-- FontAwesome -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

<!-- Alertify -->
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.core.css")%>">    
<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/themes/alertify.default.css")%>">    
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/alertify.js-0.3.11/src/alertify.js")%>"></script>

<!-- DataTables -->
    <link href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet">
<link href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet">
<script type="text/javascript" src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
    
 
        
   <div class="container" style="width: 100%;">

           <div class="row mt5">

               <div class="col-md-4 col-sm-12 mt5">

                           <asp:HiddenField ID="hId_Tu" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hifId_Rik" runat="server" ClientIDMode="Static" />
                   

                          <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="Label2" runat="server" Text="Representante" />
                            </div>
                            <div class="col-md-8">
                               <select id="ddlRepresentante" class="form-control rfdDecorated" >
                                     <option value="-1">Todos...</option>
                                <!-- Opciones dinámicas se agregarán aquí -->
                            </select> 
                            </div>
                        </div>
                     </div>
              
 
               
               <div class="col-md-2 col-sm-12 mt5">            
                    
                        <div class="form-group">
                             
                       <%--<select id="comboTamanos" class="form-control selectpicker" multiple data-live-search="true" title="Selecciona Tamaños">
                            <option value="todos">Todos</option>
                            <option value="A">A</option>
                            <option value="B">B</option>
                            <option value="C">C</option>
                            <option value="D">D</option>
                            <option value="E">E</option>
                        </select>--%>
                           
                            
                           <select id="ddlcombo" class="form-control rfdDecorated" style="height:100px;" multiple>
                        <option value="todos">Todos</option>
                        <option value="A">A</option>
                        <option value="B">B</option>
                        <option value="C">C</option>
                        <option value="D">D</option>
                        <option value="E">E</option>
                    </select>


                        </div>                            
                           
                </div>     


                <div class="col-md-3 col-sm-12 mt5">    
                       
                    <form class="form-inline">
                        <div class="form-group">
                            <div class="input-group">
                              <span class="input-group-addon" id="basic-addon1">
                                    <i class="fa fa-filter"></i>
                              </span>
                              <input id="txtCliente" type="text" class="form-control" onkeypress="return SoloNumeros(event);" placeholder="Número de cliente a buscar...">                                  
                            </div>
                        </div>                            
                    </form>  
                   
                 <div class="banner-incremento-precios" id="bannerIncremento">
                         <span class="banner-text">
                             <i class="fa fa-exclamation-triangle"></i>
                             El Ejercicio de Incremento de Precios se Cierra el <b>2025</b>. 
                             <span class="blink">Después de este día la edición del GPMA se bloqueará.</span>
                         </span>
                     </div>

                </div>     
               <div class="col-md-3 col-sm-12 mt5">      
                    <div class="row mt5">

                    <form class="form-inline">
                        <div class="form-group">
                            <div class="input-group">
                              <span class="input-group-addon" id="basic-addon1">
                                    <i class="fa fa-filter"></i>
                              </span>
                              <input id="txtProducto" type="text" class="form-control" onkeypress="return SoloNumeros(event);" placeholder="Código producto..">                                  
                            </div>
                        </div>                            
                    </form>         
                        </div>
                    <div class="row mt5">
                                    <div class="col-md-4 col-sm-12 mt5">
 
                                        

                                   </div>
                         
                                    <div class="col-md-6 col-sm-12 mt5">

                                       

                                         <div class="form-group">
                                            <button id="ConsultarClientes" type="button" class="btn btn-primary" onclick="verclientes()" tittle ="Consultar de nuevo." style="width:100%;" >
                                                <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar</button>
                                        </div>


                                        </div>

                     </div>

                </div>     
                 
            </div>

       
        <div class="row mt5">

            <div class="col-md-4">
                
               <button id="btnFiltroIncremento" type="button" class="btn btn-warning" 
                        style="background: linear-gradient(45deg, #ff9800, #ff5722); border: none;">
                    <i class="fa fa-filter"></i> Ver Solo con Incremento
                </button>

            </div>

            <div class="col-md-2 col-sm-12 mt5">
               
            </div>

        </div>
     
           <div id="spinner" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999;">
            <div class="spinner-border text-primary" role="status">
        
                <img src="<%=Page.ResolveUrl("~/Img/patternfly/spinner.gif") %>"  alt="Cargando...">
 
            </div>
        </div>

     <div class="tab-pane active" id="DivtabClientes" style="overflow-x: auto; width: 100%;" >  
        <table  id="clientesTable" class="table table-hover table-bordered RadGrid_Outlook">
            <thead>
                <tr>
                    <th>Id Rik</th>
                    <th>Representante</th>
                    <th>Cliente</th>
                    <th  class="h_cel_center" style="width:200px!important;">Nombre del cliente</th>
                    <th>Matriz</th>
                    <th class="h_cel_center" style="width:200px!important;">Nombre Matriz</th>
                    <th>Tipo Cuenta</th>
                    <th>Tamaño</th>
                    <th>Ventas (Promedio 6 meses Movil)</th>
                    <th>Ventas Proyectada</th>
                    <th>Var Vta $</th>
                    <th>Var Vta %</th>
                    <th>Mg Red Actual $</th>
                     <th>Mg Red Actual %</th>
                    <th>Mg Red Proyectada $</th>
                    <th>Mg Red Proyectada %</th>
                    <th>Var Mg Red $</th>
                    <th>Var Mg Red %</th>
                    <th>Var PPb Red %</th>
                     <th>Estatus</th>                      
                    <th>Accion</th> 
                    <th>Incremento</th> 
                   
 
                </tr>
            </thead>
        </table>
       <%-- <button id="procesarClientes" class="btn btn-success" onclick="verclientes()">Procesar Seleccionados</button>--%>
    
  </div>

    </div>

       
    <script type="text/javascript">

        var _ApplicationUrl = '<%= ResolveUrl("~/") %>';
        let idRikSeleccionado = "-1";
        var _Usuario_Tipo = "<%= Usuario_Tipo %>";
        var hfId_Rik = '<%=Id_Rik %>';
        var Id_TU = '<%=Id_TU %>';
        var hfId_CD = '<%=Id_CD %>';
         var Id_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
        var Id_U = '<%=Id_U %>';



        function SoloNumeros(evt) {



            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };

        $(document).ready(function () {
            $('#spinner').css('display', 'block');
            let idZona = 1;
            let idRikSeleccionado = -1;
            CatRepresentante.CargarCombo(function () {
                
                $('#ddlRepresentante').val(0);
                $('#ddlRepresentante').val(hfId_Rik);
            });
            console.log('Cargando representantes');
            console.log($('#ddlRepresentante'));


            $("#cargarClientes").click(function () {

                $('#spinner').css('display', 'block');
                alert('entro a cargar clientes donde cargo 21 columnas pero no muestro las columnas definidas');
                $.ajax({
                   
                    "scrollX": true, // Permite el desplazamiento horizontal si es necesario
                    "scrollY": true, // Permite el desplazamiento horizontal si es necesario
                    columnDefs: [
                        { targets: 0, width: "40px" },
                        { targets: 1, width: "250px" },
                        { targets: 2, width: "20px" },
                        { targets: 3, width: "250px" },
                        { targets: 4, width: "40px" },
                        { targets: 5, width: "80px" },
                        { targets: 6, width: "20px" },
                        { targets: 7, width: "20px" },
                        { targets: 8, width: "80px" },
                        { targets: 9, width: "80px" },
                        { targets: 10, width: "80px" },
                        { targets: 11, width: "80px" },
                        { targets: 12, width: "80px" },
                        { targets: 13, width: "80px" },
                        { targets: 14, width: "80px" },
                        { targets: 15, width: "80px" },
                        { targets: 16, width: "30px" },
                        { targets: 17, width: "30px" },
                        { targets: 18, width: "30px" },
                         { targets: 19, width: "30px" },
                        { targets: 20, width: "30px" },
                        { targets: 21, visible: false } 
                    ],
                    autoWidth: false,
                    url: '<%= ResolveUrl("~/GestionPrecios/Clientesget.aspx") %>',
                    type: 'GET',
                    data: { action: 'getClientes' }, // Parámetro de consulta
                    success: function (data) {
                        // Inicializar el DataTable con los datos recibidos
                        $('#clientesTable').DataTable({
                            data: data,
                            stateSave: true,
                            "bDestroy": true, // Destruir cualquier instancia previa del DataTable
                            columns: [
                                { data: 'Id_Cte' }  
                                ,{ data: 'IdReporteGP', visible: false } 
                            ]
                        });
                        $('#spinner').hide();
                    },
                    error: function (error) {
                        $('#spinner').hide();
                        console.error("Error al cargar clientes:", error);
                    }
                });
                $('#spinner').hide();
            });
 

            $('.selectpicker').selectpicker();

 

            // Escucha el evento "change" en el select con id "ddlcombo"
            $('#ddlcombo').on('change', function () {
                // Obtén el valor seleccionado
                let selectedValue = $(this).val();
                console.log('Valor seleccionado:', selectedValue);

                // Lógica para manejar la selección
                filtrarDataTable(selectedValue); // Aquí puedes invocar una función para filtrar el DataTable
                
            });


            $.ajax({
                type: "POST",
                url: "GestionIncrementoClientes.aspx/ObtenerFechaCierreIncremento",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var fecha = response.d;
                    if (fecha) {
                        // Formatea la fecha a "30 de julio de 2025"
                        var fechaFormateada = formatearFecha(fecha);
                        $('#bannerIncremento .banner-text b').text(fechaFormateada);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error al obtener la fecha de cierre:", error);
                }
            });

            function formatearFecha(fechaISO) {
                // De "2025-07-30" a "30 de julio de 2025"
                var meses = ["enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"];
                var partes = fechaISO.split("-");
                if (partes.length === 3) {
                    return partes[2] + " de " + meses[parseInt(partes[1], 10) - 1] + " de " + partes[0];
                }
                return fechaISO;
            }


            
        });

      

        function filtrarclientes(valoresSeleccionados) {
            $('#spinner').css('display', 'block');
            let tabla = $('#clientesTable').DataTable();
          
            // Verifica si valoresSeleccionados es un string o un array
            if (typeof valoresSeleccionados === 'string') {
                valoresSeleccionados = [valoresSeleccionados]; // Convierte el string en un array
            }

            // Verifica si se seleccionó "Todos"
            if (valoresSeleccionados.includes('Todos')) {
                console.log('Todos');
                tabla.column(7)
                    .search('') // Limpia el filtro
                    .draw();
            } else {
                // Crea una expresión regular para buscar múltiples valores
                let regex = valoresSeleccionados.join('|'); // Une los valores con "OR" lógico
                console.log('Otros:', regex);
                tabla.column(7)
                    .search(regex, true, false) // true para regex, false para búsqueda exacta
                    .draw();
            }
          
        }


        function filtrarDataTable(valoresSeleccionados) {
            $('#spinner').css('display', 'block');
            let tabla = $('#clientesTable').DataTable();

            // Verifica si se seleccionó "Todos"
            if (valoresSeleccionados.includes('todos')) {
                tabla.column(7)  
                    .search('')
                    .draw();
            } else {
                // Crea una expresión regular para buscar múltiples valores
                let regex = valoresSeleccionados.join('|'); // Une los valores con un OR lógico (|)
                tabla.column(7)  
                    .search(regex, true, false) // true para regex, false para búsqueda exacta
                    .draw();
            }
        }

        let tablaClientes =
            $('#clientesTable').DataTable({
                    "ajax": {
                    "url": "Clientesget.aspx", // Debes implementar este controlador
                    "type": "GET",
                    "dataSrc": "",
                    data: function (d) {
                        // Aquí defines los parámetros que deseas enviar al servidor
                        d.representante = $('#ddlRepresentante').val(); // Valor del combo Representante
                        d.cliente = $('#txtCliente').val();             // Valor del textbox Cliente
                        d.producto = $('#txtProducto').val();           // Valor del textbox Producto
                        d.tiporeporte = 2;
                        d.idreportegp = -1;
                        d.action = 'getClientes';
                    }
                },
                "stateSave": true, // Activa el guardado de estado

                "bDestroy": true,
                "autoWidth": false, // Desactiva el ajuste automático de ancho
                 
                "scrollX": true, // Permite el desplazamiento horizontal si es necesario
                "scrollY": true, // Permite el desplazamiento horizontal si es necesario
                "columnDefs": [
                    { "targets": 0, width: "30px" }, // Oculta la primera columna
                    { "targets": 1, width: "250px" }, // Ajusta el ancho de la segunda columna
                    { "targets": 2, width: "20px" },
                    { "targets": 3, width: "300px" },
                    { "targets": 4, width: "50px" },
                    { "targets": 5, width: "100px" },
                    { "targets": 6, width: "20px" },
                    { "targets": 7, width: "20px" },
                    { "targets": 8, width: "60px" },
                    { "targets": 9, width: "60px" },
                    { "targets": 10, width: "60px" },
                    { "targets": 11, width: "60px" },
                    { "targets": 12, width: "60px" },
                    { "targets": 13, width: "20px" },
                    { "targets": 14, width: "60px" },
                    { "targets": 15, width: "60px" },
                    { "targets": 16, width: "60px" },
                    { "targets": 17, width: "60px" },
                    { "targets": 18, width: "60px" },
                    { "targets": 19, width: "60px" },
                    { "targets": 20, width: "60px" },
                    { "targets": 21, width: "40px" },
                     {
                        targets: 19, // Índice de la columna NomEstatus
                        render: function (data, type, row) {
                            let colorClase = '';
                            switch (data) {
                                case 'Analizar':
                                    colorClase = 'btn btn-warning';
                                    break;
                                case 'Analizando':
                                    colorClase = 'btn btn-info';
                                    break;
                                case 'Analizado':
                                    colorClase = 'btn btn-info';
                                    break;
                                case 'Propuesta Enviada':
                                    colorClase = 'btn btn-secondary';
                                    break;
                                case 'Precios Aceptados':
                                    colorClase = 'btn btn-primary';
                                    break;
                                case 'ACYS Enviado':
                                    colorClase = 'btn btn-light';
                                    break;
                                case 'ACYS Actualizado':
                                    colorClase = 'btn btn-light';
                                    break;
                                case 'Val. Precios':
                                    colorClase = 'btn btn-danger';
                                    break;
                                default:
                                    colorClase = 'btn btn-light';
                            }
                            // Agregar la clase btn-detalle y los mismos atributos data que el botón
                            return `<span style="width:100%; cursor: pointer;" class="${colorClase} btn-detalle"
                            data-id="${row.Id_Cte}"
                            data-idsucursal="${row.Id_Cd}"
                            data-idrik="${row.Id_Rik}" 
                            data-nombrecliente="${row.Cte_NomComercial}" 
                            data-idmatriz="${row.Id_Matriz}" 
                            data-nombrematriz="${row.NombreMatriz}"
                            data-idtamano="${row.Id_Tamaño}" 
                            data-idtipocuenta="${row.TipoCuenta}"
                            data-nomestatus="${row.NomEstatus}"
                            data-idreportegp="${row.IdReporteGP}">
                            ${data}
                        </span>`;
                        }
                    }
                     
                ],
                "columns": [
                    { "data": "Id_Rik" },
                    { "data": "Nombre_Rik" },
                    { "data": "Id_Cte" },
                    { "data": "Cte_NomComercial" },
                    { "data": "Id_Matriz" },
                    { "data": "NombreMatriz" },
                    { "data": "TipoCuenta" },
                    { "data": "Id_Tamaño" },
                    {
                        data: 'Ventas',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'VentasPA',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'Var_VentaMonto',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: "Var_VentaPorc", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'MgRed_MontoActual',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
               
                    {
                        data: "MgRed_PorcActual", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: 'MgRed_Proyectada',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                  
                    {
                        data: "MgRed_PorcProy", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },

                    {
                        data: 'VarMgRed_Monto',
                        render: function (data, type, row) {
                            // Formatear el número con comas y dos decimales
                            return parseFloat(data).toLocaleString('es-MX', {
                                minimumFractionDigits: 2,
                                maximumFractionDigits: 2
                            });
                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: "VarMgRed_Porc", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },
                    {
                        data: "VarPpbRed_Porc", // Campo que contiene el valor decimal
                        render: function (data, type, row) {
                            // Verifica si el tipo de datos es 'display' (para la vista)
                            if (type === 'display' && !isNaN(data)) {
                                return (data * 100).toFixed(2) + '%'; // Multiplica por 100 y agrega el porcentaje
                            }
                            return data;

                        },
                        className: 'text-end' // Alinea el contenido a la derecha
                    },

                    { "data": "NomEstatus" },
                    {
                        data: null, 
                        render: function (data, type, row) {
                            let textoBoton = "Generar Propuesta"; 

                            // Cambia el texto según el estatus
                            switch (row.NomEstatus) {
                                case "Analizar":
                                    textoBoton = "Generar Propuesta";
                                    break;
                                case "Analizando":
                                    textoBoton = "Generar Propuesta";
                                    break;
                                case "Propuesta Enviada":
                                    textoBoton = "Validar Propuesta";
                                    break;
                                case "Precios Aceptados":
                                    textoBoton = "Consultar";
                                    break;
                                case "ACYS Enviado":
                                    textoBoton = "Consultar Propuesta";
                                    break;
                                case "ACYS Actualizado":
                                    textoBoton = "Consultar Propuesta";
                                    break;
                                case 'Val. Precios':
                                    textoBoton = "Validar Propuesta";
                                    break;
                            }

                            return `
                            <button type="button" class="btn btn-primary btn-sm btn-detalle"
                                data-id="${row.Id_Cte}"
                                data-idsucursal="${row.Id_Cd}"
                                data-idrik="${row.Id_Rik}" 
                                data-nombrecliente="${row.Cte_NomComercial}" 
                                data-idmatriz="${row.Id_Matriz}" 
                                data-nombrematriz="${row.NombreMatriz}"
                                data-idtamano="${row.Id_Tamaño}" 
                                data-idtipocuenta="${row.TipoCuenta}"
                                data-nomestatus="${row.NomEstatus}"
                                data-idreportegp="${row.IdReporteGP}";>
                                ${textoBoton}
                            </button>
                        `;
                        }
                    },
                    {
                        data: 'TieneIncremento',
                        name: 'TieneIncremento',
                        title: 'Incremento',
                        className: 'text-center',
                        render: function (data, type, row) {
                            if (data === 1) {
                                return `<div class="d-flex align-items-center justify-content-center">
                                    <span class="badge badge-pill badge-warning" 
                                          data-toggle="tooltip" 
                                          title="Cliente con productos que tienen incremento"
                                          style="position: relative; 
                                                 padding: 8px 12px;
                                                 background: linear-gradient(45deg, #ff9800, #ff5722);
                                                 color: white;
                                                 border-radius: 20px;
                                                 box-shadow: 0 2px 5px rgba(255, 152, 0, 0.3);">
                                        <i class="fa fa-arrow-up"></i> Con Inc
                                    </span>
                                </div>`;
                            }
                            return '';
                        }
                    }  

                ],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    } 
                },
                "createdRow": function (row, data, dataIndex) {
                    if (data.TieneIncremento === 1) {
                        $(row).addClass('row-with-increment');
                    }
                },
                "initComplete": function (settings, json) {
                    $('#spinner').css('display', 'none');
                    $('[data-toggle="tooltip"]').tooltip();
                },

            });

        // Evento para redirigir a la pantalla de detalle
        $('#clientesTable').on('click', '.btn-detalle', function () {
            
            const idCliente = $(this).data('id');
            const idSucursal = $(this).data('idsucursal');
            const idRik = $(this).data('idrik');
            const idtamano = $(this).data('idtamano'); 
            /*const idtamano = $(this).data('A-B-C');*/
            const idtipocuenta = $(this).data('idtipocuenta');

             
            const nombrecliente = $(this).data('nombrecliente');
            const idmatriz = $(this).data('idmatriz');
            const nombrematriz = $(this).data('nombrematriz');
            
            console.log('ID Cliente:', idCliente);
            const data = $('#clientesTable').DataTable().row($(this).parents('tr')).data();

            const nomestatus = $(this).data('nomestatus');
            const idReporteGP = $(this).data('idreportegp'); // <-- Aquí recuperas el valor
            console.log(idReporteGP);

            console.log(data);
            //const id_Cd = $(this).data('Id_Cte');
            //const id_Rik = $(this).data('Id_Rik');
           
            // Redirige a la nueva página con los parámetros en la URL
            window.location.href = `GestionIncrementoClientesDetalle.aspx?idcte=${idCliente}&sucursal=${idSucursal}&representante=${idRik}&nombrecliente=${nombrecliente}&idmatriz=${idmatriz}&nombrematriz=${nombrematriz}&idtamano=${idtamano}&idtipocuenta=${idtipocuenta}&nomestatus=${nomestatus}&idreportegp=${idReporteGP}`;
            //window.open("GestionIncrementoClientesDetalle.aspx", "_self");
             
        });

        function verProductos(idCliente) {
            window.open("GestionIncremento.aspx?idCliente=" + idCliente, "_self");

        }


        function Alerta_IdRik() {
            alertify
                .okBtn("Ok")
                .confirm("<b>Error Identificador Rik </b><br/><p>El usuario actual no tien un Rik asignado, " +
                    "esto causara un mal funcionamiento en el CRM, debe comunicarse a soporte.<p>", function (ev) {
                        ev.preventDefault();
                    }, function (ev) {
                        ev.preventDefault();
                    });
        }

        function verclientes() {
            $('#spinner').css('display', 'block');
            tablaClientes.ajax.reload(function () {
                $('#spinner').hide();
            });
            
            } ;


       
        function cerrarpantalla() {
            console.log('cerrar pantalla');
            window.open('<%= ResolveUrl("~/GestionPrecios/GestionIncrementoClientes.aspx") %>', '_self');

          /** window.open("GestionIncrementoClientes.aspx" , "_self")*/
        }


        var CatRepresentante = {

            CargarCombo: function (CALLBACK_Exito) {

              
                //$('#btnCargarListado').attr('disabled', true);
                

                $.ajax({
                    url: _ApplicationUrl + 'api/CrmRepresentante/Get_List?Id_Cd=0',
                    cache: false,
                    type: 'GET'
                }).done(function (response, textStatus, jqXHR) {
                    Estado = response.Estado;
                    listado = response.Datos;
                    //console.log(response);        
                    var ddl = $('#ddlRepresentante').empty();
                    ddl.append(
                        $('<option data-Id_U=0>').val(0).text('-- Todos --')
                    );
                    console.log(Estado);
                    if (Estado == 1) {
                        var ID = 0;
                        for (var i = 0; i < listado.length; i++) {
                            ddl.append(
                                $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_Rik).text(listado[i].U_Nombre)
                            );
                            ID = listado[i].Id_U;
                        }
                        console.log("representante " + hfId_Rik);
                        console.log(Id_TU);
                        $('#spinner').hide();

                        ddl.val(hfId_Rik);

                        //let hfId_Rik = $('#hId_TU').val();
                        //let Id_TU = $('#hId_TU').val();

                        console.log(Id_TU);
                        if (hfId_Rik > 0) {
                            ddl.val(hfId_Rik);
                        }
                        if (Id_TU == 2   ) {
                            $("#ddlRepresentante").attr("disabled", "disabled");
                        } else {
                            ddl.removeAttr('disabled');
                        } 
                       
                    }
 
                    if (CALLBACK_Exito) {
                        CALLBACK_Exito();
                    }
                }).fail(function (jqXHR, textStatus, error) { 
                    
                    alertify.error('Error: CatRepresentante.Cargar_Representante');
                    console.log(jqXHR);
                });
            },

            CargarListadoCheck: function (CALLBACK_Exito) {
               
                $.ajax({
                    url: _ApplicationUrl + '/api/CrmRepresentante/Get_List?Id_Cd=0',
                    cache: false,
                    type: 'GET'
                }).done(function (response, textStatus, jqXHR) {
                    Estado = response.Estado;
                    listado = response.Datos;
                    //console.log(response);        

                    $('#tblRepresentantesCheck > tbody').empty();

                    if (Estado == 1) {
                        var ID = 0;
                        for (var i = 0; i < listado.length; i++) {
                            var row = $('<tr>');
                            row.append($('<td class="text-center">').append(
                                '<input id="chbRespresentante_' + i + '" data-id_rik=' + listado[i].Id_Rik + ' type="checkbox" />'
                            ));
                            row.append($('<td class="text-left" >').append(
                                '<label>' + listado[i].U_Nombre + '</label>'
                            ));
                            $('#tblRepresentantesCheck > tbody').append(row);
                        }
                    }
                    if (CALLBACK_Exito) {
                        CALLBACK_Exito();
                    }
                }).fail(function (jqXHR, textStatus, error) {
                    alertify.error('Error: CatRepresentante.CargarListadoCheck');
                    console.log(jqXHR);
                });
            }
        }

        // Filtro global para mostrar solo clientes con incremento cuando el botón está activo
        $.fn.dataTable.ext.search.push(
            function (settings, data, dataIndex, row) {
                // Solo aplica el filtro si el botón está activo
                if ($('#btnFiltroIncremento').hasClass('active')) {
                    // row.TieneIncremento puede ser 1, "1", true, "true"
                    return row.TieneIncremento == 1 || row.TieneIncremento == "1" || row.TieneIncremento === true || row.TieneIncremento === "true";
                }
                return true;
            }
        );


        // Para el filtro de tiene incremento
        //$('#btnFiltroIncremento').on('click', function () {
        //    let button = $(this);
        //    let table = $('#clientesTable').DataTable();

        //    if (!button.hasClass('active')) {
        //        // Filtrar solo los que tienen incremento
        //        table.column('TieneIncremento:name').search('1').draw();
        //        button.addClass('active').css('opacity', '0.8');
        //    } else {
        //        // Mostrar todos
        //        table.column('TieneIncremento:name').search('').draw();
        //        button.removeClass('active').css('opacity', '1');
        //    }
        //});
        $('#btnFiltroIncremento').on('click', function () {
            let button = $(this);
            button.toggleClass('active');
            button.css('opacity', button.hasClass('active') ? '0.8' : '1');
            $('#clientesTable').DataTable().draw();
        });

        //document.getElementById("btnIncremento_AutAlerta").addEventListener("click", function () {
        //    const spinner = document.getElementById("spinner");
        //    spinner.style.display = "inline-block";

        //    setTimeout(() => {
        //        alert("Autorización enviada.");
        //        spinner.style.display = "none";
        //    }, 2000); // Simula un retraso de 2 segundos
        //});


        

    </script>
 
 

    </asp:Content>



