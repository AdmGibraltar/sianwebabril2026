/*

    Key Quimica Dic 2018 

    24 Dic 2018 RFH 

*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function ExportExcel_Ordenes_BajarReporteExcel(Data) {
    //var Periodo = $('#ddPeriodo option:selected').text();

    var excel = $JExcel.new();                      
    var excel = $JExcel.new("Arial 9 #333333");    
    var excel = $JExcel.new("Arial 9 #333333");            

    var P=1; //Periodo.replace(/-/g,'');

    excel.set( {sheet:0,value:P } );
    var evenRow=excel.addStyle( { border: "none,none,none,thin #333333"});                                                    
    var oddRow=excel.addStyle ( { fill: "#ECECEC" ,border: "none,none,none,thin #333333"}); 
            
    var formatTitulo=excel.addStyle ( {                
        border: "none,none,none,none",font: "Arial 9 #0000AA B"}
    );                                                         
                        
    var line = 0;                        
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    
    //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();    
    var rbTipo= $('#cphBodyContent_rbTipo option:selected').text();    
    
    //var Zonas = $('#ddlZonas option:selected').text();
    var Zonas = 0;
    
    var dStyle = excel.addStyle ( {                       
        align: "L",                                                                                
        format: "d-mmm-yy",                                                                             
        border: "none,none,none,none",
        font: "Arial 9 #0000AA B"}                
    );           

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");
    excel.set(0,0,line,"Modulo ACyS - "+rbTipo, formatTitulo);             
    excel.set(0,0,line+1,"", formatTitulo);             
    excel.set(0,0,line+1,"Representante: "+Representante, formatTitulo);             
    excel.set(0,0,line+2,"Fecha: "+Fecha, formatTitulo);             
    //excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
    var CDI_Nombre = 'NOMBRE CDI';
    excel.set(0,0,line+4,"CDS : "+CDI_Nombre, formatTitulo);             
                
    line = 6;

    var formatHeader=excel.addStyle ({
        align: "C",                                                                                
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B"
    });                                                         

    var headers=["TipoCuenta","Folio","Ver","Estatus","Núm","Cliente","Terr.","Rik",
    "Fecha", "Fecha Inicio","Fecha Fin","Vencido","Modalidad"];                            
            
    for (var i=0;i<headers.length;i++){                       // Loop headers
        excel.set(0,i,6,headers[i],formatHeader);             // Set CELL header text & header format
        excel.set(0,i,undefined,"auto");                      // Set COLUMN width to auto 
    }            

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);                                                                       
    
    var formatCell=excel.addStyle ( {                        
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    );     
    var formatCell_C=excel.addStyle ( {                
        align: "C",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
    var formatCell_L=excel.addStyle ( {                
        align: "L",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
            
    var format_Monto=excel.addStyle ( {
        //format: '#,##0.00',
        align: "C",
        format: '$#,##0',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 

    // renglon amarillo
    var formatCell_Amarillo=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_L=excel.addStyle ({
        align: "L",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_C=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 

  

    line = 7;

    var Totales_Linea = 0;    
    var Ultimo_Rik = 0; 
    var Total1 = 0;
    var Total2 = 0;
    var Total3 = 0;
    var Total4 = 0;
    var Total5 = 0;
            
    var Inicio = line + 1; // Salta Renglon  

    for (var i=0; i<Data.length; i++) {             
        // Imprime registro Normal

        excel.set(0, 1, line, Data[i].CNDescripcion, formatCell_C);
        excel.set(0,2,line,Data[i].Id_Acs, formatCell_C);                    
        excel.set(0,3,line,Data[i].Id_AcsVersion, formatCell);                    
        excel.set(0,4,line,Data[i].Acs_EstatusTexto, formatCell);                    
        excel.set(0,5,line,Data[i].Id_Cte, formatCell);                    
        excel.set(0,6,line,Data[i].Cte_NomComercial, formatCell);                    
        excel.set(0,7,line,Data[i].Id_Ter, formatCell);                    
        excel.set(0,8,line,Data[i].Id_Rik, formatCell);                    
        excel.set(0,9,line,Data[i].Acs_Fecha, formatCell);                    
        excel.set(0,10,line,Data[i].Acs_FechaInicio, formatCell);                    
        excel.set(0,11,line,Data[i].Acs_FechaFin, formatCell);                    
        excel.set(0,12,line,Data[i].Acs_Vencido, formatCell);                    
        excel.set(0,13,line,Data[i].Acs_Modalidad, formatCell);                    
        line = line+1;
    }

    /*
    var Fin = line+1;
    line = line+1;
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    */
    /*
    excel.set(0,3,line,"", formatCell);  
    excel.set(0,4,line,"", formatCell);  
    excel.set(0,5,line,"", formatCell_L);  
    excel.set(0,6,line,"", formatCell);      
    excel.set(0,7,line,"", formatCell);  
    excel.set(0,8,line,"", formatCell);  
    excel.set(0,9,line,"", formatCell);  
    excel.set(0,10,line,"", formatCell);  
    excel.set(0,11,line,"", formatCell);  
    excel.set(0,12,line,"", format_Monto); 
    excel.set(0,13,line,"", formatCell);  
    excel.set(0,14,line,"", formatCell);  
    excel.set(0,15,line,"", formatCell);  
    excel.set(0,16,line,"", formatCell);  
    excel.set(0,17,line,"", formatCell);  
    */    
    //Periodo=Periodo.replace(/-/g,'');
    excel.generate("ControlDeOrdenes "+ "1" +".xlsx");
}      

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function ExportExcel_BajarReporteExcel(Data) {
    //var Periodo = $('#ddPeriodo option:selected').text();

    var excel = $JExcel.new();                      
    var excel = $JExcel.new("Arial 9 #333333");    
    var excel = $JExcel.new("Arial 9 #333333");            

    var P=1; //Periodo.replace(/-/g,'');

    excel.set( {sheet:0,value:P } );
    var evenRow=excel.addStyle( { border: "none,none,none,thin #333333"});                                                    
    var oddRow=excel.addStyle ( { fill: "#ECECEC" ,border: "none,none,none,thin #333333"}); 
            
    var formatTitulo=excel.addStyle ( {                
        border: "none,none,none,none",font: "Arial 9 #0000AA B"}
    );                                                         
                        
    var line = 0;                        
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    
    //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();    
    var rbTipo= $('#cphBodyContent_rbTipo option:selected').text();    
    
    //var Zonas = $('#ddlZonas option:selected').text();
    var Zonas = 0;
    
    var dStyle = excel.addStyle ( {                       
        align: "L",                                                                                
        format: "d-mmm-yy",                                                                             
        border: "none,none,none,none",
        font: "Arial 9 #0000AA B"}                
    );           

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");
    excel.set(0,0,line,"Modulo ACyS - "+rbTipo, formatTitulo);             
    excel.set(0,0,line+1,"", formatTitulo);             
    excel.set(0,0,line+1,"Representante: "+Representante, formatTitulo);             
    excel.set(0,0,line+2,"Fecha: "+Fecha, formatTitulo);             
    //excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
    var CDI_Nombre = 'NOMBRE CDI';
    excel.set(0,0,line+4,"CDS : "+CDI_Nombre, formatTitulo);             
                
    line = 6;

    var formatHeader=excel.addStyle ({
        align: "C",                                                                                
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B"
    });                                                         

    var headers=["TipoCuenta","Folio","Ver","Estatus","Núm","Cliente","Terr.","Rik",
    "Fecha", "Fecha Inicio","Fecha Fin","Vencido","Modalidad"];                            
            
    for (var i=0;i<headers.length;i++){                       // Loop headers
        excel.set(0,i,6,headers[i],formatHeader);             // Set CELL header text & header format
        excel.set(0,i,undefined,"auto");                      // Set COLUMN width to auto 
    }            

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);                                                                       
    
    var formatCell=excel.addStyle ( {                        
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    );     
    var formatCell_C=excel.addStyle ( {                
        align: "C",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
    var formatCell_L=excel.addStyle ( {                
        align: "L",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
            
    var format_Monto=excel.addStyle ( {
        //format: '#,##0.00',
        align: "C",
        format: '$#,##0',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 

    // renglon amarillo
    var formatCell_Amarillo=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_L=excel.addStyle ({
        align: "L",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_C=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 

  

    line = 7;

    var Totales_Linea = 0;    
    var Ultimo_Rik = 0; 
    var Total1 = 0;
    var Total2 = 0;
    var Total3 = 0;
    var Total4 = 0;
    var Total5 = 0;
            
    var Inicio = line + 1; // Salta Renglon  

    for (var i=0; i<Data.length; i++) {             
        // Imprime registro Normal
        
        excel.set(0, 0, line, Data[i].CNDescripcion, formatCell_C);
        excel.set(0,1,line,Data[i].Id_Acs, formatCell_C);                    
        excel.set(0,2,line,Data[i].Id_AcsVersion, formatCell);                    
        excel.set(0,3,line,Data[i].Acs_EstatusTexto, formatCell);                    
        excel.set(0,4,line,Data[i].Id_Cte, formatCell);                    
        excel.set(0,5,line,Data[i].Cte_NomComercial, formatCell);                    
        excel.set(0,6,line,Data[i].Id_Ter, formatCell);                    
        excel.set(0,7,line,Data[i].Id_Rik, formatCell);                    
        excel.set(0,8,line,Data[i].Acs_Fecha, formatCell);                    
        excel.set(0,9,line,Data[i].Acs_FechaInicio, formatCell);                    
        excel.set(0,10,line,Data[i].Acs_FechaFin, formatCell);                    
        excel.set(0,11,line,Data[i].Acs_Vencido, formatCell);                    
        excel.set(0,12,line,Data[i].Acs_Modalidad, formatCell);                    
        line = line+1;
    }

    /*
    var Fin = line+1;
    line = line+1;
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    */
    /*
    excel.set(0,3,line,"", formatCell);  
    excel.set(0,4,line,"", formatCell);  
    excel.set(0,5,line,"", formatCell_L);  
    excel.set(0,6,line,"", formatCell);      
    excel.set(0,7,line,"", formatCell);  
    excel.set(0,8,line,"", formatCell);  
    excel.set(0,9,line,"", formatCell);  
    excel.set(0,10,line,"", formatCell);  
    excel.set(0,11,line,"", formatCell);  
    excel.set(0,12,line,"", format_Monto); 
    excel.set(0,13,line,"", formatCell);  
    excel.set(0,14,line,"", formatCell);  
    excel.set(0,15,line,"", formatCell);  
    excel.set(0,16,line,"", formatCell);  
    excel.set(0,17,line,"", formatCell);  
    */    
    //Periodo=Periodo.replace(/-/g,'');
    excel.generate("Reporte "+ "1" +".xlsx");
}      

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Acys_BajarReporteExcel() {

    $('#spinner_AcysIndice').css('display','block');

    var tbFechaInicio = '';
    var tbFechaFin = '';
    var AplicarFecha = '';
    var AplicarFolio = '';

    var IdCte = '0';
    var NombreClente = '';
    var chbPorCliente = $('#chbPorCliente').is(':checked');
     if (chbPorCliente) {        
        var IdCte = $('#tbNumeroCliente').val();
        IdCte = parseInt(IdCte);
        if (isNaN(IdCte)) {
            IdCte = 0;
        }
        NombreClente = $('#tbNombreCliente').val();
    }

    var chbPorFolios = $('#chbPorFolios').is(':checked');
    var tbFolioIncial = $('#tbFolioIncial').val();
    var tbFolioFinal = $('#tbFolioFinal').val();

    var chbPorFechas = $('#chbPorFechas').is(':checked');
    if (chbPorFechas) {
        tbFechaInicio = $('#tbFechaInicio').val();
        tbFechaFin = $('#tbFechaFin').val();
    } else {
        tbFechaInicio = '';
        tbFechaFin = '';
    }

    Cargar_Indice_Ajax(
        1, 5000,
        chbPorCliente, IdCte, NombreClente,
        chbPorFechas, tbFechaInicio, tbFechaFin,
        chbPorFolios, tbFolioIncial, tbFolioFinal
    , function (lst) { 

        ExportExcel_BajarReporteExcel(lst);

        $('#spinner_AcysIndice').css('display','none');

    },function (){
        // CALLBACK_Error
        $('#spinner_AcysIndice').css('display','none');

    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function AcysOrdenes_BajarReporteExcel() {

    $('#spinner_AcysIndice').css('display','block');

    var tbFechaInicio = '';
    var tbFechaFin = '';
    var AplicarFecha = '';
    var AplicarFolio = '';

    var chbPorCliente = $('#chbPorCliente').is(':checked');

    var chbPorFolios = $('#chbPorFolios').is(':checked');
    var tbFolioIncial = $('#tbFolioIncial').val();
    var tbFolioFinal = $('#tbFolioFinal').val();

    var chbPorFechas = $('#chbPorFechas').is(':checked');
    if (chbPorFechas) {
        tbFechaInicio = $('#tbFechaInicio').val();
        tbFechaFin = $('#tbFechaFin').val();
    } else {
        tbFechaInicio = '';
        tbFechaFin = '';
    }
            
    Cargar_Indice_Ajax(
        1, 5000,
        chbPorFechas, tbFechaInicio, tbFechaFin,
        chbPorFolios, tbFolioIncial, tbFolioFinal
    , function (lst) { 
        ExportExcel_BajarReporteExcel(lst);
        $('#spinner_AcysIndice').css('display','none');
    },function (){
        // CALLBACK_Error
        $('#spinner_AcysIndice').css('display','none');
    });

}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

});