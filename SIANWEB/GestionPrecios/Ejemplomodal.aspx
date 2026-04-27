<%@ Page Title="Producto Detalle" Language="C#"  MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeFile="Ejemplomodal.aspx.cs" Inherits="SIANWEB.GestionPrecios.Ejemplomodal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <!-- DataTables CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container">
        <h2>Listado de Productos</h2>
        <button type="button" id="btnShowModal" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
            Agregar Producto
        </button>
        <br /><br />
        <table id="productTable" class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Categoría</th>
                    <th>Precio</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <!-- Las filas se cargan dinámicamente con JavaScript -->
            </tbody>
        </table>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Agregar Producto</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form id="addProductForm">
                        <div class="form-group">
                            <label for="productName">Nombre del Producto</label>
                            <input type="text" class="form-control" id="productName" placeholder="Nombre">
                        </div>
                        <div class="form-group">
                            <label for="productCategory">Categoría</label>
                            <input type="text" class="form-control" id="productCategory" placeholder="Categoría">
                        </div>
                        <div class="form-group">
                            <label for="productPrice">Precio</label>
                            <input type="number" class="form-control" id="productPrice" placeholder="Precio">
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button type="button" id="btnSaveProduct" class="btn btn-primary">Guardar Producto</button>
                </div>
            </div>
        </div>
    </div>
 
    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <!-- DataTables JS -->
    <script src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Inicializar DataTable
            var table = $('#productTable').DataTable({
                "ajax": {
                    "url": "Ejemplomodal.aspx/GetProducts",
                    "type": "POST",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }
                },
                "columns": [
                    { "data": "Id" },
                    { "data": "Name" },
                    { "data": "Category" },
                    { "data": "Price" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="btn btn-warning btn-edit">Editar</button>`;
                        }
                    }
                ]
            });

            // Guardar Producto desde el Modal
            $('#btnSaveProduct').on('click', function () {
                var newProduct = {
                    Id: table.data().length + 1,
                    Name: $('#productName').val(),
                    Category: $('#productCategory').val(),
                    Price: $('#productPrice').val()
                };

                table.row.add(newProduct).draw(); // Agregar a DataTable
                $('#exampleModal').modal('hide'); // Cerrar modal
            });
        });
    </script>
</asp:Content>