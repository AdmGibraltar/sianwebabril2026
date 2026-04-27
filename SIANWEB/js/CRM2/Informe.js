/*
   Key Química
   21 Ene 2019 Actualizado RFH
*/

var _onLoginSuccessful = null;

var VAR_idUuen = 0;

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
    console.log(message);
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur(sender, args) {
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Seleccionar --');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur2(sender, args) {
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Todos --');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur3(sender, args) {
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Todas --');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
if ((typeof (console) == undefined) || (typeof (console) == 'undefined')) {
    window.console = new Object();
    window.console.log = function () {
    };
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function login($) {
    $('#wrnDvDialogoInicioSesion').fadeOut();
    $.ajax({
        url: _ApplicationUrl + '/api/Login/',
        data: $('#frmDvDialogoInicioSesion').serialize(),
        cache: false,
        type: 'POST',
        statusCode: {
            506: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            507: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            508: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            509: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            510: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#dvDialogoInicioSesion').modal('hide');
        if (_onLoginSuccessful != null) {
            _onLoginSuccessful();
        }
    }).fail(function (jqXHR, textStatus, error) {
        //Mostrar el toast con el mensaje de error; retirar las llamadas para mostrar el toast en cada uno de los casos de código de respuesta, y solo manejar las acciones de los casos en particular por código.
        $('#wrnDvDialogoInicioSesion #msgWrnDvDialogoInicioSesion').html(jqXHR.responseJSON.Message);
        $('#wrnDvDialogoInicioSesion').fadeIn()
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function redireccionarALogin() {
    self.location = _ApplicationUrl + '/login.aspx';
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function mostrarToast(jqToastElement, jqParent) {
    $(jqToastElement).appendTo($(jqParent));
    $(jqToastElement).fadeIn();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function salirDelSistema() {
    window.location = _ApplicationUrl + '/Login.aspx?Id=1';
}

function dynamicSort(property) {
    var sortOrder = 1;
    if (property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a, b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Representante(idZona) {

    $.ajax({
        url: _ApplicationUrl + '/api/CrmRepresentante?IdCD=' + idZona,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        var SortedResponse = response.sort(dynamicSort("Descripcion"));

        var ddl = $('#ddlRepresentantesComercial').empty();
        for (var i = 0; i < SortedResponse.length; i++) {
            $('#ddlRepresentantesComercial').append(
                $('<option>').val(SortedResponse[i].Id).text(SortedResponse[i].Descripcion)
            );
        }
        try {
            //$('#ddlRepresentantesComercial').selectpicker('val', "-1");
        } catch (err) {

        }
        //$('#ddlRepresentantesComercial').selectpicker('refresh');        
        if (hfId_Rik > 0) {
            $('#ddlRepresentantesComercial').val(hfId_Rik);
        }

        if (Id_TU == 2 || Id_TU == 3 || Id_TU == 4 || Id_TU == 5 || Id_TU == 1) {
            $('#ddlRepresentantesComercial').removeAttr('disabled');
        } else {
            $('#ddlRepresentantesComercial').prop('disabled', 'disabled');
        }
        //$('#ddlRepresentantesComercial').selectpicker('refresh');

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Error: Carga de representantes');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function CargarCombo_Calendario(idZona) {

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }

    $.ajax({

        url: _ApplicationUrl + '/api/CrmCalendario?IdCD=' + idZona,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        var ddl = $('#ddPeriodo').empty();
        for (var i = 0; i < response.length; i++) {
            $('#ddPeriodo').append($('<option>').val(response[i].Id).text(response[i].Descripcion));
        }
        //$('#ddPeriodo').selectpicker('refresh');

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Ocurrio un error al cargar los proyectos.');
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
/*
function CargarCombo_Centros() {
    
    $.ajax({
        url: _ApplicationUrl + '/api/CrmCentroDist?Param=0',
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {               
        var ddl = $('#ddlZonas').empty();
        //var Id_TU= $('#<%=Id_TU.ClientID %>').val(); 
        //var hfId_CD = $('#<%=hfId_CD.ClientID %>').val();                 
        for (var i = 0; i < response.length; i++) {
            $('#ddlZonas').append(
                $('<option>').val(response[i].Id).text(response[i].Descripcion)
            );
        }     
                                
        if (Id_TU==2 || Id_TU==3 || Id_TU==4 || Id_TU==5 || Id_TU==1) {                                        
            $('#ddlZonas').removeAttr('disabled');
        } else {                                  
            $('#ddlZonas').prop('disabled', 'disabled');
        }
        $('#ddlZonas').val(hfId_CD);
        $('#ddlZonas').selectpicker('refresh');
                                                       
        CargarCombo_Calendario(hfId_CD);
        Cargar_Representante(hfId_CD);
                                
    }).fail(function (jqXHR, textStatus, error) {                
        alertify.error('Error: Carga de Centros');
    });
}
 
*/

// 13ENE-2020

function Export_Excel_Informe1(Data) {
    var Periodo = $('#ddPeriodo option:selected').text();

    var excel = $JExcel.new();
    var excel = $JExcel.new("Arial 9 #333333");
    var excel = $JExcel.new("Arial 9 #333333");

    var P = Periodo.replace(/-/g, '');

    excel.set({ sheet: 0, value: P });
    var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
    var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

    var formatTitulo = excel.addStyle({
        border: "none,none,none,none", font: "Arial 9 #0000AA B"
    }
    );

    var line = 0;
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    var rbTipo = $('#cphBodyContent_rbTipo option:selected').text();

    var Zonas = 0;

    var dStyle = excel.addStyle({
        align: "L",
        format: "d-mmm-yy",
        border: "none,none,none,none",
        font: "Arial 9 #0000AA B"
    }
    );

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");
    excel.set(0, 0, line, "Modulo CRM - " + rbTipo, formatTitulo);
    excel.set(0, 0, line + 1, "", formatTitulo);
    excel.set(0, 0, line + 1, "Representante: " + Representante, formatTitulo);
    excel.set(0, 0, line + 2, "Fecha: " + Fecha, formatTitulo);
    excel.set(0, 0, line + 3, "Periodo: " + Periodo, formatTitulo);
    excel.set(0, 0, line + 4, "CDS : " + CDI_Nombre, formatTitulo);

    line = 6;

    var formatHeader = excel.addStyle({
        align: "C",
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B"
    });

    //Jfcv LEADS agregar columan de Tipo de Cliente
    var headers = ["Tipo", "Tipo de Cliente", "Proyecto", "Cliente", "Area", "Solución", "Aplicación", "Producto",
        "Mes de captura", "Día de captura",
        "VPT",
        "Analisis", "Presentación", "Negociación", "Cierre",
        "Cancelación", "MontoProyecto", "Comentarios", "Fecha Modificación", "Estatus",
        "Rik", "Nombre"
    ];

    for (var i = 0; i < headers.length; i++) {                       // Loop headers
        excel.set(0, i, 6, headers[i], formatHeader);             // Set CELL header text & header format
        excel.set(0, i, undefined, "auto");                      // Set COLUMN width to auto 
    }
    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);
    var formatCell = excel.addStyle({
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var formatCell_C = excel.addStyle({
        align: "C",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var formatCell_L = excel.addStyle({
        align: "L",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var format_Monto = excel.addStyle({
        //format: '#,##0.00',
        align: "C",
        format: '$#,##0',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    // renglon amarillo
    var formatCell_Amarillo = excel.addStyle({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });
    var formatCell_Amarillo_L = excel.addStyle({
        align: "L",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });
    var formatCell_Amarillo_C = excel.addStyle({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });


    //            
    //    var Inicio = line+1;
    //    for (var i=0; i<Data.length; i++){
    //        
    //        var Este_Rik  = Data[i].Rik
    //        
    //        // Ultimo Rik Diferente de Este Rik
    //        if (Ultimo_Rik != Este_Rik) 
    //        {
    //            if (Ultimo_Rik > 0) {
    //                if (Ultimo_Rik != Este_Rik) {
    //                    // Imprime la linea e incrementa
    //                    line = line + 1;
    //                    excel.set(0,0,line,Data[i].Proyecto, formatCell_C);                    
    //                    excel.set(0,1,line,Data[i].Cliente, formatCell);                    
    //                    excel.set(0,2,line,Data[i].Area, formatCell);                    
    //                    excel.set(0,3,line,Data[i].Solucion, formatCell);                    
    //                    excel.set(0,4,line,Data[i].Aplicacion, formatCell);                    
    //                    excel.set(0,5,line,Data[i].Productos, formatCell_L);                    
    //                    excel.set(0,6,line,Data[i].VPO, format_Monto);                    
    //                    excel.set(0,7,line,Data[i].Analisis, formatCell_C);                    
    //                    excel.set(0,8,line,Data[i].Presentacion, formatCell_C);                    
    //                    excel.set(0,9,line,Data[i].Negociacion, formatCell_C);                    
    //                    excel.set(0,10,line,Data[i].Cierre, formatCell_C);                    
    //                    excel.set(0,11,line,Data[i].Cancelacion, formatCell);                    
    //                    excel.set(0,12,line,Data[i].MontoProyecto, format_Monto);                    
    //                    excel.set(0,13,line,Data[i].Comentarios, formatCell);  
    //                    excel.set(0,14,line,Data[i].FechaModificacion, formatCell_C);  
    //                    excel.set(0,15,line,Data[i].Estatus, formatCell_C);  
    //                    excel.set(0,16,line,Data[i].Rik, formatCell_C);  
    //                    excel.set(0,17,line,Data[i].Nombre, formatCell);                          
    //                    /*
    //                    excel.set(0,16,line,Data[i].ClienteSIANID, formatCell);  
    //                    excel.set(0,17,line,Data[i].OportunidadID, formatCell);  
    //                    excel.set(0,18,line,Data[i].Rik, formatCell);  
    //                    excel.set(0,19,line,Data[i].Nombre, formatCell);                  
    //                    excel.set(0,20,line,Data[i].Causa, formatCell);  
    //                    */
    //                    Total1 = Total1 + Data[i].VPO;
    //                    Total2 = Total2 + Data[i].MontoProyecto;
    //                    // -- 

    //                    // Imprime TOTALES
    //                    excel.set(0,0,Totales_Linea,"TOTAL"+Este_Rik, formatCell_Amarillo_L);                    
    //                    excel.set(0,6,Totales_Linea,Total1, formatCell_Amarillo);                    
    //                    excel.set(0,12,Totales_Linea,Total2, formatCell_Amarillo);       
    //                    Total1 = 0;             
    //                    Total2 = 0;             
    //                }
    //            }           
    //            Totales_Linea = line;
    //            Total1 = 0;
    //            Total2 = 0;
    //            Total3 = 0;
    //            Total4 = 0;
    //            Total5 = 0;    
    //            // Imprim totales
    //            excel.set(0,0,line,"", formatCell_Amarillo);                    
    //            excel.set(0,1,line,"", formatCell_Amarillo);                    
    //            excel.set(0,2,line,"", formatCell_Amarillo);                    
    //            excel.set(0,3,line,"", formatCell_Amarillo);                    
    //            excel.set(0,4,line,"", formatCell_Amarillo);                    
    //            excel.set(0,5,line,"", formatCell_Amarillo);                    
    //            excel.set(0,6,line,"", formatCell_Amarillo);                    
    //            excel.set(0,7,line,"", formatCell_Amarillo);                    
    //            excel.set(0,8,line,"", formatCell_Amarillo);                    
    //            excel.set(0,9,line,"", formatCell_Amarillo);                    
    //            excel.set(0,10,line,"", formatCell_Amarillo);                    
    //            excel.set(0,11,line,"", formatCell_Amarillo);                    
    //            excel.set(0,12,line,"", formatCell_Amarillo);                    
    //            excel.set(0,13,line,"", formatCell_Amarillo);                    
    //            excel.set(0,14,line,"", formatCell_Amarillo);                    
    //            excel.set(0,15,line,"", formatCell_Amarillo);                    
    //            excel.set(0,16,line,"", formatCell_Amarillo);                    
    //            excel.set(0,17,line,"", formatCell_Amarillo);       
    //            /*            
    //            excel.set(0,18,line,"", formatCell_Amarillo);                    
    //            excel.set(0,19,line,"", formatCell_Amarillo);                    
    //            excel.set(0,20,line,"", formatCell_Amarillo); 
    //            */
    //            Ultimo_Rik = Data[i].Rik;
    //        } else {

    //        line = line + 1;
    //        excel.set(0,0,line,Data[i].Proyecto, formatCell_C);                    
    //        excel.set(0,1,line,Data[i].Cliente, formatCell);                    
    //        excel.set(0,2,line,Data[i].Area, formatCell);                    
    //        excel.set(0,3,line,Data[i].Solucion, formatCell);                    
    //        excel.set(0,4,line,Data[i].Aplicacion, formatCell);                    
    //        excel.set(0,5,line,Data[i].Productos, formatCell_L);                    
    //        excel.set(0,6,line,Data[i].VPO, format_Monto);                    
    //        excel.set(0,7,line,Data[i].Analisis, formatCell_C);                    
    //        excel.set(0,8,line,Data[i].Presentacion, formatCell_C);                    
    //        excel.set(0,9,line,Data[i].Negociacion, formatCell_C);                    
    //        excel.set(0,10,line,Data[i].Cierre, formatCell_C);                    
    //        excel.set(0,11,line,Data[i].Cancelacion, formatCell);                    
    //        excel.set(0,12,line,Data[i].MontoProyecto, format_Monto);                    
    //        excel.set(0,13,line,Data[i].Comentarios, formatCell);  
    //        excel.set(0,14,line,Data[i].FechaModificacion, formatCell_C);  
    //        excel.set(0,15,line,Data[i].Estatus, formatCell_C);  
    //        excel.set(0,16,line,Data[i].Rik, formatCell_C);  
    //        excel.set(0,17,line,Data[i].Nombre, formatCell);                  
    //        
    //        /*
    //        excel.set(0,16,line,Data[i].ClienteSIANID, formatCell);  
    //        excel.set(0,17,line,Data[i].OportunidadID, formatCell);  
    //        excel.set(0,18,line,Data[i].Rik, formatCell);  
    //        excel.set(0,19,line,Data[i].Nombre, formatCell);                  
    //        excel.set(0,20,line,Data[i].Causa, formatCell);  
    //        */
    //        Total1 = Total1 + Data[i].VPO;
    //        Total2 = Total2 + Data[i].MontoProyecto;

    //        }

    //    }

    line = 6;

    var Totales_Linea = 0;
    var Ultimo_Rik = 0;
    var Total1 = 0;
    var Total2 = 0;
    var Total3 = 0;
    var Total4 = 0;
    var Total5 = 0;

    var Total_A = 0;
    var Total_P = 0;
    var Total_N = 0;
    var Total_C = 0;
    var Total_Ca = 0;

    var Inicio = line + 1; // Salta Renglon  

    for (var i = 0; i < Data.length; i++) {

    }

    for (var i = 0; i < Data.length; i++) {

        var Este_Rik = Data[i].Rik

        if (Ultimo_Rik == 0 || Ultimo_Rik != Este_Rik) {
            var bImprimeTotales = 0;
            // Esto indica que el rik es diferente 

            // Si no hay line totales inicializa
            if (Totales_Linea == 0) {
                line = line + 1; // Salta Renglon 
                Totales_Linea = line;
                bImprimeTotales = 0;
            } else {
                // No salta el renglon                 
                line = line + 1; // Salta Renglon                 
                bImprimeTotales = 1;
            }

            if (bImprimeTotales == 1) {
                // Imprime total y continua                     
                excel.set(0, 0, Totales_Linea, "TOTAL", formatCell_Amarillo_L);
                excel.set(0, 1, Totales_Linea, "", formatCell_Amarillo);
                //Excell agregar columna de tipo cliente 
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 7, Totales_Linea, "A", formatCell_Amarillo);
                excel.set(0, 8, Totales_Linea, "B", formatCell_Amarillo);
                excel.set(0, 9, Totales_Linea, Total1, formatCell_Amarillo);

                /*excel.set(0,7,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,8,Totales_Linea,"", formatCell_Amarillo); // A
                excel.set(0,9,Totales_Linea,"", formatCell_Amarillo); // P
                excel.set(0,10,Totales_Linea,"", formatCell_Amarillo); // N
                excel.set(0,11,Totales_Linea,"", formatCell_Amarillo); //C                    */

                excel.set(0, 10, Totales_Linea, Total_A, formatCell_Amarillo); //A                    
                excel.set(0, 11, Totales_Linea, Total_P, formatCell_Amarillo); //P
                excel.set(0, 12, Totales_Linea, Total_N, formatCell_Amarillo); // N
                excel.set(0, 13, Totales_Linea, Total_C, formatCell_Amarillo); // C
                excel.set(0, 14, Totales_Linea, Total_Ca, formatCell_Amarillo); // Ca

                excel.set(0, 15, Totales_Linea, Total2, formatCell_Amarillo);
                excel.set(0, 16, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 17, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 18, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 19, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 20, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 21, Totales_Linea, "", formatCell_Amarillo);

                // Inicia totales
                Total1 = 0;
                Total2 = 0;

                Total_A = 0;
                Total_P = 0;
                Total_N = 0;
                Total_C = 0;
                Total_Ca = 0;

                Totales_Linea = line;
            }

            Ultimo_Rik = Este_Rik;
        }
        line = line + 1; // Salta Renglon 

        // Imprime registro NORMAL
        excel.set(0, 0, line, Data[i].TipoVenta, formatCell_C); // TIPO VENTA     
        //JFCV agregar tipo Cliente
        excel.set(0, 1, line, Data[i].TipoCliente, formatCell_C);
        excel.set(0, 2, line, Data[i].Proyecto, formatCell_C);
        excel.set(0, 3, line, Data[i].Cliente, formatCell);
        excel.set(0, 4, line, Data[i].Area, formatCell);
        excel.set(0, 5, line, Data[i].Solucion, formatCell);
        excel.set(0, 6, line, Data[i].Aplicacion, formatCell);
        excel.set(0, 7, line, Data[i].Productos, formatCell_L);

        // Fragmentar la fecha 
        var Dia = Data[i].FechaCreacion.substr(0, 2);
        Dia = parseInt(Dia);
        if (isNaN(Dia)) {
            Dia = 0;
        }
        var Mes = Data[i].FechaCreacion.substr(3, 2);
        Mes = parseInt(Mes);
        if (isNaN(Mes)) {
            Mes = 0;
        }
        sMes = '';
        switch (Mes) {
            case 0:
                sMes = '-';
                break;
            case 1:
                sMes = 'Enero';
                break;
            case 2:
                sMes = 'Febrero';
                break;
            case 3:
                sMes = 'Marzo';
                break;
            case 4:
                sMes = 'Abril';
                break;
            case 5:
                sMes = 'Mayo';
                break;
            case 6:
                sMes = 'Junio';
                break;
            case 7:
                sMes = 'Julio';
                break;
            case 8:
                sMes = 'Agosto';
                break;
            case 9:
                sMes = 'Septiembre';
                break;
            case 10:
                sMes = 'Octubre';
                break;
            case 11:
                sMes = 'Noviembre';
                break;
            case 12:
                sMes = 'Diciembre';
                break;
        }

        excel.set(0, 8, line, sMes, formatCell_C);
        excel.set(0, 9, line, Dia, formatCell_C);
        excel.set(0, 10, line, Data[i].VPO, format_Monto);

        var VAP_ESTATUS = Data[i].Estatus.substring(0, 1);
        var VAP_ESTATUS_2 = Data[i].Estatus.substring(2, 3);
        var ESTATUS = Data[i].Estatus.substring(4, 5);

        // NEGOCIACION

        /*
        if (VAP_ESTATUS=='C' && VAP_ESTATUS_2=='1') {
            if (Data[i].Negociacion=='') {
                // NEGOCIACION = ANALISIS 
                if (Data[i].Analisis.length>0) {
                    Data[i].Negociacion = Data[i].Analisis;
                }
                // NEGOCIACION = PRESENTACIO 
                if (Data[i].Presentacion.length>0) {
                    Data[i].Negociacion = Data[i].Presentacion;
                }
            }
        }
        if (VAP_ESTATUS=='P' && VAP_ESTATUS_2=='3') {
            if (Data[i].Negociacion=='') {
                // NEGOCIACION = ANALISIS 
                if (Data[i].Analisis.length>0) {
                    Data[i].Negociacion = Data[i].Analisis;
                }
                // NEGOCIACION = PRESENTACIO 
                if (Data[i].Presentacion.length>0) {
                    Data[i].Negociacion = Data[i].Presentacion;
                }
            }
        }      
        */

        excel.set(0, 11, line, Data[i].Analisis, formatCell_C); //A                    
        excel.set(0, 12, line, Data[i].Presentacion, formatCell_C); // P
        excel.set(0, 13, line, Data[i].Negociacion, formatCell_C); // N
        excel.set(0, 14, line, Data[i].Cierre, formatCell_C); //C
        excel.set(0, 15, line, Data[i].Cancelacion, formatCell); //Ca
        excel.set(0, 16, line, Data[i].MontoProyecto, format_Monto);
        excel.set(0, 17, line, Data[i].Comentarios, formatCell);
        excel.set(0, 18, line, Data[i].FechaModificacion, formatCell_C);
        excel.set(0, 19, line, Data[i].Estatus, formatCell_C);
        excel.set(0, 20, line, Data[i].Rik, formatCell_C);
        excel.set(0, 21, line, Data[i].Nombre, formatCell);
        // Todoñ
        //excel.set(0,20,line,Data[i].TipoVenta, formatCell); // TIPO VENTA                   

        Total1 = Total1 + Data[i].VPO;
        Total2 = Total2 + Data[i].MontoProyecto;

        if (Data[i].Analisis.trim().length > 0 && Data[i].Presentacion.trim().length == 0 &&
            Data[i].Negociacion.trim().length == 0 && Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_A = Total_A + Data[i].MontoProyecto;
        }
        if (Data[i].Analisis.trim().length > 0 && Data[i].Presentacion.trim().length > 0 &&
            Data[i].Negociacion.trim().length == 0 && Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_P = Total_P + Data[i].MontoProyecto;
        }
        // Negociacion 
        if (Data[i].Analisis.trim().length > 0 && Data[i].Negociacion.trim().length > 0 &&
            Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_N = Total_N + Data[i].MontoProyecto;
        }
        // Cierre
        if (Data[i].Analisis.trim().length > 0 && Data[i].Cierre.trim().length > 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_C = Total_C + Data[i].MontoProyecto;
        }
        // Cancelacion 
        if (Data[i].Cancelacion.trim().length > 0) {
            Total_Ca = Total_Ca + Data[i].MontoProyecto;
        }


    }

    if (Ultimo_Rik > 0) {
        // Actualiza Totales                   
        excel.set(0, 0, Totales_Linea, "TOTAL", formatCell_Amarillo_L);
        excel.set(0, 1, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);

        excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo); //A                    
        excel.set(0, 7, Totales_Linea, "", formatCell_Amarillo); //A        
        excel.set(0, 8, Totales_Linea, "", formatCell_Amarillo); //JFCV agregar tipo cliente

        excel.set(0, 9, Totales_Linea, Total1, formatCell_Amarillo);

        excel.set(0, 10, Totales_Linea, Total_A, formatCell_Amarillo); //A                    
        excel.set(0, 11, Totales_Linea, Total_P, formatCell_Amarillo); //P
        excel.set(0, 12, Totales_Linea, Total_N, formatCell_Amarillo); // N
        excel.set(0, 13, Totales_Linea, Total_C, formatCell_Amarillo); // C
        excel.set(0, 14, Totales_Linea, Total_Ca, formatCell_Amarillo); // Ca
        excel.set(0, 15, Totales_Linea, Total2, formatCell_Amarillo);
        excel.set(0, 16, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 17, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 18, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 19, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 20, Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 21, Totales_Linea, "", formatCell_Amarillo);
    }

    var Fin = line + 1;
    line = line + 1;
    excel.set(0, 0, line, "", formatCell);
    excel.set(0, 1, line, "", formatCell);
    excel.set(0, 2, line, "", formatCell);
    excel.set(0, 3, line, "", formatCell);
    excel.set(0, 4, line, "", formatCell);
    excel.set(0, 5, line, "", formatCell);
    excel.set(0, 6, line, "", formatCell_L);

    excel.set(0, 7, line, "", formatCell);

    excel.set(0, 8, line, "", formatCell);

    excel.set(0, 9, line, "", formatCell);
    excel.set(0, 10, line, "", formatCell);
    excel.set(0, 11, line, "", formatCell);
    excel.set(0, 12, line, "", formatCell);
    excel.set(0, 13, line, "", formatCell);
    excel.set(0, 14, line, "", formatCell);
    excel.set(0, 15, line, "", format_Monto);
    excel.set(0, 16, line, "", formatCell);
    excel.set(0, 17, line, "", formatCell);
    excel.set(0, 18, line, "", formatCell);
    excel.set(0, 19, line, "", formatCell);
    excel.set(0, 20, line, "", formatCell);
    excel.set(0, 21, line, "", formatCell);

    Periodo = Periodo.replace(/-/g, '');
    excel.generate("Reporte " + Periodo + " " + Fecha + ".xlsx");
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Export_Excel_Informe8(Data) {
    var Periodo = $('#ddPeriodo option:selected').text();

    var excel = $JExcel.new();
    var excel = $JExcel.new("Arial 9 #333333");
    var excel = $JExcel.new("Arial 9 #333333");

    var P = Periodo.replace(/-/g, '');

    excel.set({ sheet: 0, value: P });
    var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
    var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

    var formatTitulo = excel.addStyle({
        border: "none,none,none,none", font: "Arial 9 #0000AA B"
    }
    );

    var line = 0;
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    var rbTipo = $('#cphBodyContent_rbTipo option:selected').text();

    var Zonas = 0;

    var dStyle = excel.addStyle({
        align: "L",
        format: "d-mmm-yy",
        border: "none,none,none,none",
        font: "Arial 9 #0000AA B"
    }
    );

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");
    excel.set(0, 0, line, "Modulo CRM - " + rbTipo, formatTitulo);
    excel.set(0, 0, line + 1, "", formatTitulo);
    excel.set(0, 0, line + 1, "Representante: " + Representante, formatTitulo);
    excel.set(0, 0, line + 2, "Fecha: " + Fecha, formatTitulo);
    excel.set(0, 0, line + 3, "Periodo: " + Periodo, formatTitulo);
    excel.set(0, 0, line + 4, "CDS : " + CDI_Nombre, formatTitulo);

    line = 6;

    var formatHeader = excel.addStyle({
        align: "C",
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B"
    });

    //
    // Determinar hasta que punto debe desplegar 
    //

    var IgnorarColumnas = false;
    var IgnorarColumnas_PROFUNDIDA = 0;

    //UEN
    var ddlUEN = $('#ddlUEN').val();
    ddlUEN = parseInt(ddlUEN);
    if (isNaN(ddlUEN)) {
        ddlUEN = 0;
    }
    if (ddlUEN == 0 && IgnorarColumnas == false) {
        var headers = ["Cliente", "Rik",
            "UEN",//"Segmento", "Área", "Solución", "Aplicación", 
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 1;
    }

    //SEGMENTO
    var ddlSegmento = $('#ddlSegmento').val();
    ddlSegmento = parseInt(ddlSegmento);
    if (isNaN(ddlSegmento)) {
        ddlSegmento = 0;
    }
    if (ddlSegmento == 0 && IgnorarColumnas == false) {
        var headers = ["Cliente", "Rik",
            "UEN", "Segmento",//"Área", "Solución", "Aplicación", 
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 2;
    }

    //AREA
    var ddlArea = $('#ddlArea').val();
    ddlArea = parseInt(ddlArea);
    if (isNaN(ddlArea)) {
        ddlArea = 0;
    }
    if (ddlArea == 0 && IgnorarColumnas == false) {
        var headers = ["Cliente", "Rik",
            "UEN", "Segmento", "Área", //"Solución", "Aplicación", 
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 3;
    }

    //SOLUCION
    var ddlSolucion = $('#ddlSolucion').val();
    ddlSolucion = parseInt(ddlSolucion);
    if (isNaN(ddlSolucion)) {
        ddlSolucion = 0;
    }
    if (ddlSolucion == 0 && IgnorarColumnas == false) {
        var headers = ["Cliente", "Rik",
            "UEN", "Segmento", "Área", "Solución", //"Aplicación", 
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 4;
    }

    //APLICACION
    var ddlAplicacion = $('#ddlAplicacion').val();
    ddlAplicacion = parseInt(ddlAplicacion);
    if (isNaN(ddlAplicacion)) {
        ddlAplicacion = 0;
    }
    if (ddlAplicacion == 0 && IgnorarColumnas == false) {
        var headers = ["Cliente", "Rik",
            "UEN", "Segmento", "Área", "Solución", "Aplicación",
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 5;
    }

    // TODO 
    if (IgnorarColumnas_PROFUNDIDA == 0) {
        var headers = ["Cliente", "Rik",
            "UEN", "Segmento", "Área", "Solución", "Aplicación",
            "Producto", "VPO", "Analisis", "Presentación", "Negociación", "Cierre", "Cancelación", "Fecha", "Proyecto"];
        IgnorarColumnas = true;
        IgnorarColumnas_PROFUNDIDA = 6;
    }


    /*  	
      c) Estructura del reporte: Columna 1. “Cliente”, columna 2. “RIK” (no importa que se repita el nombre), 3. “UEN”, 
      4. “Segmento”, 5. “Área”, 6. “Solución”, 7. “Aplicación” y 8. “Producto” (DESCRIPCIÓN + código), 9. “VPT” 
      (este ya debe de ser OBLIGATORIO y que el RIK tenga que capturarlo, adicional a crear un “pop-up” que 
      advierta que tiene que registrar el monto si no lo ha hecho), y por ultimo las etapas de “Análisis”, 
      “Presentación”, “Negociación” y “Cierre” con sus respectivas fechas de captura y montos totalizados 
      (como en el reporte original).                         
  */

    for (var i = 0; i < headers.length; i++) {                       // Loop headers
        excel.set(0, i, 6, headers[i], formatHeader);             // Set CELL header text & header format
        excel.set(0, i, undefined, "auto");                      // Set COLUMN width to auto 
    }

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);
    var formatCell = excel.addStyle({
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var formatCell_C = excel.addStyle({
        align: "C",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var formatCell_L = excel.addStyle({
        align: "L",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    var format_Monto = excel.addStyle({
        //format: '#,##0.00',
        align: "C",
        format: '$#,##0',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );
    // renglon amarillo
    var formatCell_Amarillo = excel.addStyle({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });
    var formatCell_Amarillo_L = excel.addStyle({
        align: "L",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });
    var formatCell_Amarillo_C = excel.addStyle({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    });


    line = 6;

    var Totales_Linea = 0;
    var Ultimo_Rik = 0;
    var Total1 = 0;
    var Total2 = 0;
    var Total3 = 0;
    var Total4 = 0;
    var Total5 = 0;

    var Total_A = 0;
    var Total_P = 0;
    var Total_N = 0;
    var Total_C = 0;
    var Total_Ca = 0;

    var Inicio = line + 1; // Salta Renglon  

    for (var i = 0; i < Data.length; i++) {

    }

    console.log('IgnorarColumnas_PROFUNDIDA:' + IgnorarColumnas_PROFUNDIDA);

    for (var i = 0; i < Data.length; i++) {

        var Este_Rik = Data[i].Rik;

        if (Ultimo_Rik == 0 || Ultimo_Rik != Este_Rik) {
            var bImprimeTotales = 0;
            // Esto indica que el rik es diferente 

            // Si no hay line totales inicializa
            if (Totales_Linea == 0) {
                line = line + 1; // Salta Renglon 
                Totales_Linea = line;
                bImprimeTotales = 0;
            } else {
                // No salta el renglon                 
                line = line + 1; // Salta Renglon                 
                bImprimeTotales = 1;
            }

            if (bImprimeTotales == 1) {
                // Imprime total y continua                     
                excel.set(0, 0, Totales_Linea, "TOTAL", formatCell_Amarillo_L);
                excel.set(0, 1, Totales_Linea, "", formatCell_Amarillo);

                CC1 = IgnorarColumnas_PROFUNDIDA;
                switch (IgnorarColumnas_PROFUNDIDA) {
                    case 1:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        break;
                    case 2:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                        break;
                    case 3:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                        break;
                    case 4:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                        break;
                    case 5:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo);
                        break;
                    case 6:
                        excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                        excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo);
                        CC1 = 5;
                        break;
                }

                excel.set(0, 2 + (CC1 + 0), Totales_Linea, "", formatCell_Amarillo);                     // Productos
                excel.set(0, 2 + (CC1 + 1), Totales_Linea, Total1, formatCell_Amarillo);

                //excel.set(0,9,Totales_Linea,Total2, formatCell_Amarillo);                    
                excel.set(0, 2 + (CC1 + 2), Totales_Linea, Total_A, formatCell_Amarillo); //A                    
                excel.set(0, 2 + (CC1 + 3), Totales_Linea, Total_P, formatCell_Amarillo); //P
                excel.set(0, 2 + (CC1 + 4), Totales_Linea, Total_N, formatCell_Amarillo); // N
                excel.set(0, 2 + (CC1 + 5), Totales_Linea, Total_C, formatCell_Amarillo); // C
                excel.set(0, 2 + (CC1 + 6), Totales_Linea, Total_Ca, formatCell_Amarillo); // Ca

                excel.set(0, 2 + (CC1 + 7), Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 2 + (CC1 + 8), Totales_Linea, "", formatCell_Amarillo);

                /*
                excel.set(0,7,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,8,Totales_Linea,"", formatCell_Amarillo); // A
                excel.set(0,9,Totales_Linea,"", formatCell_Amarillo); // P
                excel.set(0,10,Totales_Linea,"", formatCell_Amarillo); // N
                excel.set(0,11,Totales_Linea,"", formatCell_Amarillo); //C                    *
                                    
                excel.set(0,14,Totales_Linea,Total2, formatCell_Amarillo); 
                excel.set(0,15,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,16,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,17,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,18,Totales_Linea,"", formatCell_Amarillo);                    
                excel.set(0,19,Totales_Linea,"", formatCell_Amarillo);       
                excel.set(0,20,Totales_Linea,"", formatCell_Amarillo);       
                */

                // Inicia totales
                Total1 = 0;
                Total2 = 0;

                Total_A = 0;
                Total_P = 0;
                Total_N = 0;
                Total_C = 0;
                Total_Ca = 0;

                Totales_Linea = line;
            }

            Ultimo_Rik = Este_Rik;
        }

        line = line + 1; // Salta Renglon 

        // Imprime registro NORMAL
        excel.set(0, 0, line, Data[i].Cliente, formatCell);
        excel.set(0, 1, line, Data[i].Rik, formatCell_C);

        CC = IgnorarColumnas_PROFUNDIDA;
        switch (IgnorarColumnas_PROFUNDIDA) {
            case 1:
                // Ocultar Todo
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                break;
            case 2:
                // Oculta DESDE Segmento 
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                excel.set(0, 3, line, Data[i].Segmento_Nombre, formatCell);
                break;
            case 3:
                // Oculta DESDE Area 
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                excel.set(0, 3, line, Data[i].Segmento_Nombre, formatCell);
                excel.set(0, 4, line, Data[i].Area, formatCell);
                break;
            case 4:
                // Oculta Desde Solucion
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                excel.set(0, 3, line, Data[i].Segmento_Nombre, formatCell);
                excel.set(0, 4, line, Data[i].Area, formatCell);
                excel.set(0, 5, line, Data[i].Sol_Nombre, formatCell_L);
                break;
            case 5:
                // Oculta Desde Solucion
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                excel.set(0, 3, line, Data[i].Segmento_Nombre, formatCell);
                excel.set(0, 4, line, Data[i].Area, formatCell);
                excel.set(0, 5, line, Data[i].Sol_Nombre, formatCell_L);
                excel.set(0, 6, line, Data[i].Aplicacion, formatCell_L);
                break;
            case 6:
                // Oculta Area Solucion
                excel.set(0, 2, line, Data[i].UEN_Nombre, formatCell);
                excel.set(0, 3, line, Data[i].Segmento_Nombre, formatCell);
                excel.set(0, 4, line, Data[i].Area, formatCell);
                excel.set(0, 5, line, Data[i].Sol_Nombre, formatCell_L);
                excel.set(0, 6, line, Data[i].Aplicacion, formatCell_L);
                CC = 5;
                break;
        }

        excel.set(0, 2 + (CC + 0), line, Data[i].Productos, formatCell_C);
        excel.set(0, 2 + (CC + 1), line, Data[i].MontoProyecto, format_Monto);

        excel.set(0, 2 + (CC + 2), line, "", format_Monto); //A                    
        excel.set(0, 2 + (CC + 3), line, "", format_Monto); // P
        excel.set(0, 2 + (CC + 4), line, "", format_Monto); // N
        excel.set(0, 2 + (CC + 5), line, "", format_Monto); //C
        excel.set(0, 2 + (CC + 6), line, "", format_Monto); //C

        var FechaC = Data[i].FechaCreacion.substring(0, 10);

        excel.set(0, 2 + (CC + 7), line, FechaC, formatCell_C); //C
        excel.set(0, 2 + (CC + 8), line, Data[i].Proyecto, formatCell_C); //C

        Total1 = Total1 + Data[i].VPO;
        Total2 = Total2 + Data[i].MontoProyecto;
        // Analisis                
        if (Data[i].Analisis.trim().length > 0 && Data[i].Presentacion.trim().length == 0 &&
            Data[i].Negociacion.trim().length == 0 && Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_A = Total_A + Data[i].MontoProyecto;
            excel.set(0, 2 + (CC + 2), line, Data[i].MontoProyecto, format_Monto); //A                    
        }
        // Presentacion
        if (Data[i].Analisis.trim().length > 0 && Data[i].Presentacion.trim().length > 0 &&
            Data[i].Negociacion.trim().length == 0 && Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_P = Total_P + Data[i].MontoProyecto;
            excel.set(0, 2 + (CC + 3), line, Data[i].MontoProyecto, format_Monto); // P
        }
        // Negociacion 
        if (Data[i].Analisis.trim().length > 0 && Data[i].Negociacion.trim().length > 0 &&
            Data[i].Cierre.trim().length == 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_N = Total_N + Data[i].MontoProyecto;
            excel.set(0, 2 + (CC + 4), line, Data[i].MontoProyecto, format_Monto); // N
        }
        // Cierre
        if (Data[i].Analisis.trim().length > 0 && Data[i].Cierre.trim().length > 0 && Data[i].Cancelacion.trim().length == 0) {
            Total_C = Total_C + Data[i].MontoProyecto;
            excel.set(0, 2 + (CC + 5), line, Data[i].MontoProyecto, format_Monto); // N
        }
        // Cancelacion 
        if (Data[i].Cancelacion.trim().length > 0) {
            Total_Ca = Total_Ca + Data[i].MontoProyecto;
            excel.set(0, 2 + (CC + 6), line, Data[i].MontoProyecto, format_Monto); //C
        }
    }

    if (Ultimo_Rik > 0) {
        // Actualiza Totales                   

        excel.set(0, 0, Totales_Linea, "TOTAL", formatCell_Amarillo_L);
        excel.set(0, 1, Totales_Linea, "", formatCell_Amarillo);

        CC3 = IgnorarColumnas_PROFUNDIDA;
        switch (IgnorarColumnas_PROFUNDIDA) {
            case 1:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                break;
            case 2:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                break;
            case 3:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                break;
            case 4:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                break;
            case 5:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo);
            case 6:
                excel.set(0, 2, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 3, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 4, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 5, Totales_Linea, "", formatCell_Amarillo);
                excel.set(0, 6, Totales_Linea, "", formatCell_Amarillo);
                CC3 = 5;
                break;
        }

        excel.set(0, 2 + (CC3 + 0), Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 2 + (CC3 + 1), Totales_Linea, "", formatCell_Amarillo);

        excel.set(0, 2 + (CC3 + 2), Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 2 + (CC3 + 3), Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 2 + (CC3 + 4), Totales_Linea, "", formatCell_Amarillo);
        excel.set(0, 2 + (CC3 + 5), Totales_Linea, Total1, formatCell_Amarillo);
        excel.set(0, 2 + (CC3 + 6), Totales_Linea, Total_A, formatCell_Amarillo); //A                    

        excel.set(0, 2 + (CC3 + 7), Totales_Linea, Total_P, formatCell_Amarillo); //P
        excel.set(0, 2 + (CC3 + 8), Totales_Linea, Total_N, formatCell_Amarillo); // N

        //excel.set(0,12,Totales_Linea,Total_C, formatCell_Amarillo); // C
        //excel.set(0,13,Totales_Linea,Total_Ca, formatCell_Amarillo); // Ca
        //excel.set(0,14,Totales_Linea,"", formatCell_Amarillo); // Ca
        //excel.set(0,15,Totales_Linea,"", formatCell_Amarillo); // Ca


        /*  
          excel.set(0,8,Totales_Linea,Total1, formatCell_Amarillo);                                   
          excel.set(0,15,Totales_Linea,"", formatCell_Amarillo);                    
          excel.set(0,16,Totales_Linea,"", formatCell_Amarillo);                    
          excel.set(0,17,Totales_Linea,"", formatCell_Amarillo);                    
          excel.set(0,18,Totales_Linea,"", formatCell_Amarillo);                    
          excel.set(0,19,Totales_Linea,"", formatCell_Amarillo);                               
          excel.set(0,20,Totales_Linea,"", formatCell_Amarillo);      
          */
    }


    var Fin = line + 1;
    line = line + 1;
    /*
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    excel.set(0,3,line,"", formatCell);  
    excel.set(0,4,line,"", formatCell);  
    excel.set(0,5,line,"", formatCell_L);  
    excel.set(0,6,line,"", formatCell);      
    excel.set(0,7,line,"", formatCell);  
    excel.set(0,8,line,"", formatCell);  
    excel.set(0,9,line,"", formatCell);  
    excel.set(0,10,line,"", formatCell);  
    excel.set(0,11,line,"", formatCell);  
    excel.set(0,12,line,"", formatCell);  
    excel.set(0,13,line,"", formatCell);  
    excel.set(0,14,line,"", format_Monto); 
    excel.set(0,15,line,"", formatCell);  
    excel.set(0,16,line,"", formatCell);  
    excel.set(0,17,line,"", formatCell);  
    excel.set(0,18,line,"", formatCell);  
    excel.set(0,19,line,"", formatCell);  
    excel.set(0,20,line,"", formatCell);  
    */

    Periodo = Periodo.replace(/-/g, '');
    excel.generate("Reporte " + Periodo + " " + Fecha + ".xlsx");
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\           
function Export_Excel_Informe2(Data) {

    var Periodo = $('#ddPeriodo option:selected').text();
    var excel = $JExcel.new();
    var excel = $JExcel.new("Arial 10 #333333");
    var excel = $JExcel.new("Arial light 10 #333333");

    excel.set({ sheet: 0, value: "Reporte_" + Periodo });
    var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
    var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

    var formatTitulo = excel.addStyle({
        //border: "none,none,none,thin #333333",font: "Arial 12 #0000AA B"}
        border: "none,none,none,none", font: "Arial 12 #0000AA B"
    }
    );

    var line = 0;

    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    var rbTipo = $('#cphBodyContent_rbTipo').text();
    var Zonas = 0;

    var dStyle = excel.addStyle({
        align: "L",
        format: "d-mmm-yy",
        border: "none,none,none,none",
        font: "Arial 12 #0000AA B"
    }
    );

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");

    excel.set(0, 0, line, "Modulo CRM - " + rbTipo, formatTitulo);
    excel.set(0, 0, line + 1, "", formatTitulo);
    excel.set(0, 0, line + 1, "Representante: " + Representante, formatTitulo);
    excel.set(0, 0, line + 2, "Fecha: " + Fecha, formatTitulo);
    excel.set(0, 0, line + 3, "Periodo: " + Periodo, formatTitulo);
    excel.set(0, 0, line + 4, "CDS : " + Zonas, formatTitulo);

    line = 6;

    var formatHeader = excel.addStyle({
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 12 #fff B"
    });

    var headers = ["CDS", "No. Rik", "Representante", "Fecha de entrada"];

    for (var i = 0; i < headers.length; i++) {                       // Loop headers
        excel.set(0, i, 6, headers[i], formatHeader);             // Set CELL header text & header format
        excel.set(0, i, undefined, "auto");                      // Set COLUMN width to auto 
    }

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);
    line = 7;

    var formatCell = excel.addStyle({
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );

    var format_Monto = excel.addStyle({
        format: '#,##0.00',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );

    var Inicio = line + 1;
    for (var i = 0; i < Data.length; i++) {
        line = line + 1;
        excel.set(0, 0, line, Data[i].Zona, formatCell);
        excel.set(0, 1, line, Data[i].UsuarioID, formatCell);
        excel.set(0, 2, line, Data[i].Representante, formatCell);
        excel.set(0, 3, line, Data[i].Fecha, formatCell);
    }

    var Fin = line + 1;
    line = line + 1;
    excel.set(0, 0, line, "", formatCell);
    excel.set(0, 1, line, "", formatCell);
    excel.set(0, 2, line, "", formatCell);
    excel.set(0, 3, line, "", formatCell);

    excel.generate("Reporte_" + Fecha + ".xlsx");
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                                 
function Export_Excel_Informe3(Data) {

    var Periodo = $('#ddPeriodo option:selected').text();
    var excel = $JExcel.new();
    var excel = $JExcel.new("Arial 10 #333333");
    var excel = $JExcel.new("Arial light 10 #333333");

    excel.set({ sheet: 0, value: "Reporte_" + Periodo });
    var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
    var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

    var formatTitulo = excel.addStyle({
        //border: "none,none,none,thin #333333",font: "Arial 12 #0000AA B"}
        border: "none,none,none,none", font: "Arial 12 #0000AA B"
    }
    );

    var line = 0;

    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    var rbTipo = $('#cphBodyContent_rbTipo').text();
    var Zonas = 0;

    var dStyle = excel.addStyle({
        align: "L",
        format: "d-mmm-yy",
        border: "none,none,none,none",
        font: "Arial 12 #0000AA B"
    }
    );

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");

    excel.set(0, 0, line, "Modulo CRM - " + rbTipo, formatTitulo);
    excel.set(0, 0, line + 1, "", formatTitulo);
    excel.set(0, 0, line + 1, "Representante: " + Representante, formatTitulo);
    excel.set(0, 0, line + 2, "Fecha: " + Fecha, formatTitulo);
    excel.set(0, 0, line + 3, "Periodo: " + Periodo, formatTitulo);
    excel.set(0, 0, line + 4, "CDS : " + Zonas, formatTitulo);

    line = 6;

    var formatHeader = excel.addStyle({
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 12 #fff B"
    });

    var headers = ["CDS", "No. Rik", "Representante", "Análisis", "Presentación", "Monto proyecto", "Cierre",
        "Cancelación", "Efectividad cierre"];

    for (var i = 0; i < headers.length; i++) {                       // Loop headers
        excel.set(0, i, 6, headers[i], formatHeader);             // Set CELL header text & header format
        excel.set(0, i, undefined, "auto");                      // Set COLUMN width to auto 
    }

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);
    line = 7;

    var formatCell = excel.addStyle({
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );

    var format_Monto = excel.addStyle({
        format: '#,##0.00',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"
    }
    );

    var Inicio = line + 1;
    for (var i = 0; i < Data.length; i++) {
        line = line + 1;
        excel.set(0, 0, line, Data[i].ZonaId, formatCell);
        excel.set(0, 1, line, Data[i].UsuarioID, formatCell);
        excel.set(0, 2, line, Data[i].Representante, formatCell);
        excel.set(0, 3, line, Data[i].A, formatCell);
        excel.set(0, 4, line, Data[i].P, formatCell);
        excel.set(0, 5, line, Data[i].N, formatCell);
        excel.set(0, 6, line, Data[i].Monto, formatCell);
        excel.set(0, 7, line, Data[i].C, formatCell);
    }

    var Fin = line + 1;
    line = line + 1;
    excel.set(0, 0, line, "", formatCell);
    excel.set(0, 1, line, "", formatCell);
    excel.set(0, 2, line, "", formatCell);
    excel.set(0, 3, line, "", formatCell);

    excel.generate("Reporte_1007_imp" + Fecha + ".xlsx");
}

// ENE13-2020 RFH 

function Generar_Informe1(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo, CALLBACK_Exito) {

    $('#Spinner_Cargando').css('display', 'block');

    var Monto1 = $('#txtDe').val();
    Monto1 = parseFloat(Monto1);
    if (isNaN(Monto1)) {
        Monto1 = 0;
    }

    var Monto2 = $('#txtA').val();
    Monto2 = parseFloat(Monto2);
    if (isNaN(Monto2)) {
        Monto2 = 0;
    }

    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?' +
            'TipoReporte=' + rbTipoReporte +
            '&Zona=' + ddlZonas +
            '&Representante=' + ddlRepresentantes +
            '&Periodo=' + ddPeriodo +
            '&Monto1=' + Monto1 +
            '&Monto2=' + Monto2,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CALLBACK_Exito) {
            CALLBACK_Exito(response);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_Cargando').css('display', 'none');
        //$('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        alertify.error('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        /*$('#toastDanger').fadeIn();
        //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }*/
    });
    //$('#Spinner_Cargando').css('display','none');          
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                                 
function Generar_Informe8(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo, CALLBACK_Exito) {

    $('#Spinner_Cargando').css('display', 'block');

    var Monto1 = $('#txtDe').val();
    Monto1 = parseFloat(Monto1);
    if (isNaN(Monto1)) {
        Monto1 = 0;
    }

    var Monto2 = $('#txtA').val();
    Monto2 = parseFloat(Monto2);
    if (isNaN(Monto2)) {
        Monto2 = 0;
    }

    var Id_Uen = $('#ddlUEN').val();
    var Id_Seg = $('#ddlSegmento').val();
    var Id_Area = $('#ddlArea').val();

    var Id_Sol = $('#ddlSolucion').val();
    Id_Sol = parseInt(Id_Sol);
    if (isNaN(Id_Sol)) {
        Id_Sol = 0;
    }

    var Id_Apl = $('#ddlAplicacion').val();
    Id_Apl = parseInt(Id_Apl);
    if (isNaN(Id_Apl)) {
        Id_Apl = 0;
    }

    /*
    if ($('#chbSegmento').is(':checked')==false) {
        Id_Seg= 0;
    }
    if ($('#chbArea').is(':checked')==false) {
        Id_Area= 0;
    }
    if ($('#chbSolucion').is(':checked')==false) {
        Id_Sol= 0;
    }    
    if ($('#chbAplicacion').is(':checked')==false) {
        Id_Apl = 0;
    }
    */

    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?' +
            'TipoReporte=' + rbTipoReporte +
            '&Zona=' + ddlZonas +
            '&Representante=' + ddlRepresentantes +
            '&Periodo=' + ddPeriodo +
            '&Monto1=' + Monto1 +
            '&Monto2=' + Monto2 +
            '&Id_Uen=' + Id_Uen +
            '&Id_Seg=' + Id_Seg +
            '&Id_Area=' + Id_Area +
            '&Id_Sol=' + Id_Sol +
            '&Id_Apl= ' + Id_Apl,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CALLBACK_Exito) {
            CALLBACK_Exito(response);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_Cargando').css('display', 'none');
        //$('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        alertify.error('Error: ejecucion de Generar_Informe8');
        /*$('#toastDanger').fadeIn();
        //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }*/
    });
    //$('#Spinner_Cargando').css('display','none');          
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Generar_Informe2(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo) {

    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?TipoReporte=' + rbTipoReporte + '&Zona=' + ddlZonas +
            '&Representante=' + ddlRepresentantes + '&Periodo=' + ddPeriodo,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        Export_Excel_Informe2(response);

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Ocurrio un error al preparar el informe 2.');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Generar_Informe3(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo) {

    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?Zona=' + ddlZonas + '&Periodo=' + ddPeriodo,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        Export_Excel_Informe3(response);

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.responseJSON.ExceptionMessage != null && jqXHR.responseJSON.ExceptionMessage != '') {
            alertify.error(jqXHR.responseJSON.ExceptionMessage);
        } else {
            alertify.error('Ocurrio un error al ejecutar la funcion [Generar_Informe3].');
        }
    });
}

function chbSegmento_Click() {
    console.log('chbSegmento_Click');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

$(document).ready(function () {

    Cargar_UEN_SEG_ARE_SOL_APL();

    $('#Spinner_Cargando').css('display', 'block');

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#rbTipo').on('change', function () {

        var rbTipo = $('#rbTipo').val();

        if (rbTipo == "3" || rbTipo == "4" || rbTipo == "5" || rbTipo == "6" || rbTipo == "7") {
            $('#txtDe').val('');
            $('#txtA').val('');
            $('#tbMonto').css('display', 'none');
        } else {
            $('#tbMonto').css('display', 'block');
        }

        if (rbTipo == "8") {
            $('#tbUnidadNegocio').css('display', 'block');

            $('#ddlUEN').removeAttr('disabled');
            $('#ddlSegmento').removeAttr('disabled');
            $('#ddlArea').removeAttr('disabled');
            $('#ddlSolucion').removeAttr('disabled');
            $('#ddlAplicacion').removeAttr('disabled');

        } else {
            $('#tbUnidadNegocio').css('display', 'none');

            $('#ddlUEN').prop('disabled', 'disabled');
            $('#ddlSegmento').prop('disabled', 'disabled');
            $('#ddlArea').prop('disabled', 'disabled');
            $('#ddlSolucion').prop('disabled', 'disabled');
            $('#ddlAplicacion').prop('disabled', 'disabled');
        }
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#ddlUEN').on('change', function () {
        Id_Uen = $('#ddlUEN').val();
        Cargar_SEG_ARE_SOL_APL(Id_Uen);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#ddlSegmento').on('change', function () {
        var Id_Seg = $('#ddlSegmento').val();
        Cargar_ARE_SOL_APL(Id_Seg);
    });

    //
    $('#ddlArea').on('change', function () {
        var Id_Area = $('#ddlArea').val();
        Cargar_SOL_APL(Id_Area);
    });

    //
    $('#ddlSolucion').on('change', function () {
        var Id_Sol = $('#ddlSolucion').val();
        Cargar_APL(Id_Sol);
    });

    $("#chbSegmento").on('change', function () {

        console.log('chbSegmento_ Changue');

    });


    //
    $('.datatable').dataTable({
        "fnDrawCallback": function (oSettings) {
            // if .sidebar-pf exists, call sidebar() after the data table is drawn
            if ($('.sidebar-pf').length > 0) {
                $(document).sidebar();
            }
        }
    });

    $('.tooltip-demo').tooltip({
        selector: '[data-toggle=tooltip]',
        container: 'body'
    });

    if (typeof (crmOnReady) != undefined && typeof (crmOnReady) != 'undefined') {
        crmOnReady($);
    }

    if (!Modernizr.input.placeholder) {
        createPlaceholders();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnGenerarRepote').click(function (e) {
        var rbTipoReporte = $('#rbTipo').val();
        var ddlZonas = 0;
        var ddlRepresentantes = $('#ddlRepresentantesComercial option:selected').val();
        var ddPeriodo = $('#ddPeriodo option:selected').val();

        if (rbTipoReporte == 2) {
            Generar_Informe1(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo, function (response) {
                Export_Excel_Informe1(response);
                $('#Spinner_Cargando').css('display', 'none');
            });
        }
        if (rbTipoReporte == 3) {
            Generar_Informe2(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo);
        }
        if (rbTipoReporte == 5) {
            Generar_Informe3(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo);
        }
        if (rbTipoReporte == 8) {
            Generar_Informe8(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo, function (response) {
                Export_Excel_Informe8(response);
                $('#Spinner_Cargando').css('display', 'none');
            });
        }
    });

    //
    /* 
    $('input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
    */

    //
    /*
    $('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
    */

    $('#tblContenido').numeraljs();

    //
    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
        return function (table, name) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            window.location.href = uri + base64(format(template, ctx))
        };
    })();

    Cargar_Representante(0);
    CargarCombo_Calendario(0);

    $('#Spinner_Cargando').css('display', 'none');

});