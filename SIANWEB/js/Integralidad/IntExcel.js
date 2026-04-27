
var IntegralidadXls = {

    Descarar: function (Rik, lbRik, Seg, lbSeg, Ctes, lbCtes, Data) {

        //var Periodo = $('#ddPeriodo option:selected').text();
        var excel = $JExcel.new();
        var excel = $JExcel.new("Arial 9 #333333");
        var excel = $JExcel.new("Arial 9 #333333");

        var P = 1; //Periodo.replace(/-/g,'');

        excel.set({ sheet: 0, value: P });
        var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
        var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

        var formatTitulo = excel.addStyle({
            border: "none,none,none,none", font: "Arial 9 #0000AA B"
        });

        var line = 0;
        var Zonas = 0;

        var dStyle = excel.addStyle({
            align: "L",
            format: "d-mmm-yy",
            border: "none,none,none,none",
            font: "Arial 9 #0000AA B"
        });

        var CDI_Nombre = 'NOMBRE CDI';

        var Fecha = new Date();
        Fecha = Fecha.format("dd/mm/yyyy");
        excel.set(0, 0, line, "Integralidad ", formatTitulo);
        excel.set(0, 0, line + 1, "CDS : " + CDI_Nombre, formatTitulo);
        excel.set(0, 0, line + 2, "", formatTitulo);
        excel.set(0, 0, line + 3, "Representante: " + Rik + " " + lbRik, formatTitulo);
        excel.set(0, 0, line + 4, "Segmento: " + lbSeg, formatTitulo);
        excel.set(0, 0, line + 5, "Empresa: " + lbCtes, formatTitulo);
        excel.set(0, 0, line + 6, "Fecha: " + Fecha, formatTitulo);

        line = 8;

        var formatHeader = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B"
        });

        var headers = ["Num. Cte", "Cliente", "Territorio", "Segmento", "Cantidad", "Unidad",
            "Promedio Trimestral", "Integralidad vs Teo.", "Integralidad vs Obs.","VPO Meta"];

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
        });
        var formatCell_L = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_R = excel.addStyle({
            align: "R",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });

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

        line = 7;

        var Totales_Linea = 0;
        var Ultimo_Rik = 0;
        var Total1 = 0;
        var Total2 = 0;
        var Total3 = 0;
        var Total4 = 0;
        var Total5 = 0;

        var Inicio = line + 1; // Salta Renglon  

        for (var i = 0; i < Data.length; i++) {
            // Imprime registro Normal
            excel.set(0, 0, line, Data[i].Id_Cte, formatCell_C);
            excel.set(0, 1, line, Data[i].Cte_NomComercial, formatCell);
            excel.set(0, 2, line, Data[i].Id_Ter, formatCell);
            excel.set(0, 3, line, Data[i].Seg_Descripcion, formatCell);
            excel.set(0, 4, line, Data[i].Cantidad, formatCell);
            excel.set(0, 5, line, Data[i].Seg_Unidades, formatCell);

            var PromedioTrimestral = Data[i].PromedioTrimestral;
            PromedioTrimestral = parseFloat(PromedioTrimestral);
            PromedioTrimestral = PromedioTrimestral.formatMoney(2, '.', ',');
            excel.set(0, 6, line, PromedioTrimestral, formatCell_R);

            var Integralidad = Data[i].Integralidad;
            Integralidad = parseFloat(Integralidad);
            Integralidad = Integralidad * 100;
            Integralidad = Integralidad.formatMoney(2, '.', ',');
            excel.set(0, 7, line, Integralidad + '%', formatCell_R);

            var Integralidad_Obs = Data[i].Integralidad_Obs;
            Integralidad_Obs = parseFloat(Integralidad_Obs);
            Integralidad_Obs = Integralidad_Obs * 100;
            Integralidad_Obs = Integralidad_Obs.formatMoney(2, '.', ',');
            excel.set(0, 8, line, Integralidad_Obs + '%', formatCell_R);


            var ReporteVPOMeta = Data[i].VPOMeta;
            ReporteVPOMeta = parseFloat(ReporteVPOMeta);
            ReporteVPOMeta = ReporteVPOMeta.formatMoney(2, '.', ',');
            excel.set(0, 9, line, ReporteVPOMeta, formatCell_R);

            line = line + 1;
        }
        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("ReporteIntegralidad_" + Fecha + ".xlsx");
    },

    //decarga global excel
    DescararV2: function (Rik, lbRik, Seg, lbSeg, Ctes, lbCtes, Data, CDI_Nombre, datostotalesexcel, listaTipoProductos, datosTipoProducto, ddlFiltroUen, listaTipoProductosh, PeriodoMes) {

        

        //var Periodo = $('#ddPeriodo option:selected').text();
        var excel = $JExcel.new();
        var excel = $JExcel.new("Arial 9 #333333");
        var excel = $JExcel.new("Arial 9 #333333");

        var P = 1; //Periodo.replace(/-/g,'');

        excel.set({ sheet: 0, value: P });
        var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
        var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

        var formatTitulo = excel.addStyle({
            border: "none,none,none,none", font: "Arial 9 #0000AA B"
        });

        var line = 0;
        var Zonas = 0;

        var dStyle = excel.addStyle({
            align: "L",
            format: "d-mmm-yy",
            border: "none,none,none,none",
            font: "Arial 9 #0000AA B"
        });

        var Fecha = new Date();
        Fecha = Fecha.format("dd/mm/yyyy");
        excel.set(0, 0, line, "Integralidad ", formatTitulo);
        excel.set(0, 0, line + 1, "CDS : " + CDI_Nombre, formatTitulo);
        excel.set(0, 0, line + 2, "Representante: " + Rik + " " + lbRik, formatTitulo);
        excel.set(0, 0, line + 3, "UEN: " + ddlFiltroUen, formatTitulo);
        excel.set(0, 0, line + 4, "Segmento: " + lbSeg, formatTitulo);
        //excel.set(0, 0, line + 5, "Empresa: " + lbCtes, formatTitulo);
        excel.set(0, 0, line + 5, "Fecha: " + Fecha, formatTitulo);

        line = 8;

        var formatHeader = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B"
        });

        var headers = ["Sucursal","Periodo", "Venta", "VPT Valor Potencial Teórico", "VPO Valor Potencial Observado", "% Cobertura VPT", "% Cobertura VPO", "% Integralidad Aplicaciones", "% Potencial Integralidad Aplicaciones"];

        const listporcentajeVentas = listaTipoProductosh.filter(item => item.includes('% Venta'));
        const listmontoVentas = listaTipoProductosh.filter(item => item.includes('$ Venta'));
        const listaFinalH = [...headers, ...listporcentajeVentas, ...listmontoVentas];

        //const listaFinal = [...headers, ...listaTipoProductos];

        var format_Porcentaje = excel.addStyle({
            format: 't0%',
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );

        var format_Monto = excel.addStyle({
            //format: '#,##0.00',
            align: "C",
            format: '$#,##0.00',
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );


        line = 6;
        let posicion = 0;
        for (var i = 0; i < listaFinalH.length; i++) {                       // Loop headers

            excel.set(0, i, line, listaFinalH[i], formatHeader);             // Set CELL header text & header format
            excel.set(0, i, undefined, "auto");                      // Set COLUMN width to auto 

            posicion++;
        }
        line = 7;
        var initDate = new Date(2000, 0, 1);
        var endDate = new Date(2016, 0, 1);

        var formatCell = excel.addStyle({
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );
        var formatCell_C = excel.addStyle({
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_L = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_R = excel.addStyle({
            align: "R",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });



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
        //cabecera totales       

        for (var i = 0; i < datostotalesexcel.length; i++) {
     
            // imprime registro normal
            let posicion = 0;
            excel.set(0, posicion, line, datostotalesexcel[i].Sucursal, formatCell_C);

            posicion++;
            excel.set(0, posicion, line, PeriodoMes, formatCell_C);


            let venta = parseFloat(datostotalesexcel[i].Venta);
            //venta = venta.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, venta, format_Monto);

            let vpt = parseFloat(datostotalesexcel[i].VPT);
            //vpt = vpt.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, vpt, format_Monto);

            let vpo = parseFloat(datostotalesexcel[i].VPO);
            ///vpo = vpo.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, vpo, format_Monto);

            posicion++;
            excel.set(0, posicion, line, datostotalesexcel[i].PorcVPT / 100, format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, datostotalesexcel[i].PorcVPO / 100, format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, (datostotalesexcel[i].PorcentajeAplicacion / 100 ), format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, (datostotalesexcel[i].PorcentajeAplicacionPotencial / 100), format_Porcentaje);

            for (var z = 0; z < listaTipoProductos.length; z++) {

                let listresultadoPorcentaje = datosTipoProducto.filter(item => item.TipoProducto.trim() === listaTipoProductos[z].replace('% Venta ', ''));
                var PorcentajeAplicacionAppCliente = 0;
                for (var ii = 0; ii < listresultadoPorcentaje.length; ii++) {
                    PorcentajeAplicacionAppCliente += listresultadoPorcentaje[ii].Venta * 1;

                }
               
                posicion++;
                excel.set(0, posicion, line, (PorcentajeAplicacionAppCliente / venta) , format_Porcentaje);

            }

            for (var z = 0; z < listaTipoProductos.length; z++) {

                let listresultadoPorcentaje = datosTipoProducto.filter(item => item.TipoProducto.trim() === listaTipoProductos[z].replace('$ Venta ', ''));
                var Ventatotal = 0;
                for (var ii = 0; ii < listresultadoPorcentaje.length; ii++) {
                    Ventatotal += listresultadoPorcentaje[ii].Venta * 1;

                }

                posicion++;
                excel.set(0, posicion, line, Ventatotal, format_Monto);
            }

            line = line + 1;
        }

        //datos
        for (var i = 0; i < Data.length; i++) {
            // Imprime registro Normal

            let posicion = 0;
            excel.set(0, posicion, line, Data[i].Rik, formatCell_C);
            posicion++;
            excel.set(0, posicion, line, PeriodoMes, formatCell_C);
           

            let Venta = parseFloat(Data[i].Venta);
            // Venta = Venta.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, Venta, format_Monto);

            let VPT = parseFloat(Data[i].VPT);
            //VPT = VPT.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, VPT, format_Monto);

            let VPO = parseFloat(Data[i].VPO);
            ///VPO = VPO.formatMoney(2, '.', ',');
            posicion++;
            excel.set(0, posicion, line, VPO, format_Monto);


            posicion++;
            excel.set(0, posicion, line, (VPO > 0 ? ((Venta / VPT) * 100) / 100 : 0), format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, (VPO > 0 ? ((Venta / VPO) * 100) / 100 : 0), format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, ((Data[i].PorcentajeAplicacion) / 100), format_Porcentaje);

            posicion++;
            excel.set(0, posicion, line, (Data[i].PorcentajeAplicacionPotencial / 100), format_Porcentaje);

            for (var z = 0; z < listaTipoProductos.length; z++) {
                let resultadoPorcentaje = datosTipoProducto.find(item => item.Id_Rik === Data[i].Id_Rik && item.TipoProducto.trim() === listaTipoProductos[z].replace('% Venta ', ''));
                var PorcentajeAplicacionAppCliente = 0;
                if (resultadoPorcentaje) {
                    PorcentajeAplicacionAppCliente = parseFloat(resultadoPorcentaje.PorcentajeAplicacion);
                }
                posicion++;

                excel.set(0, posicion, line, (PorcentajeAplicacionAppCliente) / 100, format_Porcentaje);
            }

            for (var z = 0; z < listaTipoProductos.length; z++) {
                let resultadoPorcentaje = datosTipoProducto.find(item => item.Id_Rik === Data[i].Id_Rik && item.TipoProducto.trim() === listaTipoProductos[z].replace('$ Venta ', ''));
                var Ventatotal = 0;
                if (resultadoPorcentaje) {
                    Ventatotal = resultadoPorcentaje.Venta;
                }
                posicion++;
                excel.set(0, posicion, line, Ventatotal, format_Monto);
            }

            line = line + 1;
        }
        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("ReporteIntegralidad_" + Fecha + ".xlsx");
    },
    //detallado
    DescargarAplicacionesV2: function (Rik, lbRik, Seg, lbSeg, Data, CDI_Nombre, listaTipoProductosh, datosTipoProducto, listAplicaciones, ddlFiltroUen,Periodo) {


        //DETALLADO

        //var Periodo = $('#ddPeriodo option:selected').text();
        var excel = $JExcel.new();
        var excel = $JExcel.new("Arial 9 #333333");
        var excel = $JExcel.new("Arial 9 #333333");

        var P = 'Reporte Aplicaciones'; //Periodo.replace(/-/g,'');

        excel.set({ sheet: 0, value: P });
        var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
        var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

        var formatTitulo = excel.addStyle({
            border: "none,none,none,none", font: "Arial 9 #0000AA B"
        });

        var line = 0;
        var Zonas = 0;

        var dStyle = excel.addStyle({
            align: "L",
            format: "d-mmm-yy",
            border: "none,none,none,none",
            font: "Arial 9 #0000AA B"
        });

        var Fecha = new Date();
        Fecha = Fecha.format("dd/mm/yyyy");
        excel.set(0, 0, line, "Integralidad - Reporte Aplicaciones ", formatTitulo);
        excel.set(0, 0, line + 1, "CDS : " + CDI_Nombre, formatTitulo);
        excel.set(0, 0, line + 2, "Representante: " + Rik + " " + lbRik, formatTitulo);
        excel.set(0, 0, line + 3, "UEN: " + ddlFiltroUen, formatTitulo);
        excel.set(0, 0, line + 4, "Segmento: " + lbSeg, formatTitulo);
        //excel.set(0, 0, line + 5, "Empresa: " + lbCtes, formatTitulo);
        excel.set(0, 0, line + 5, "Fecha: " + Fecha, formatTitulo);
        let sheet = 0;
        line = 8;

        var formatHeader = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B"
        });



        var initDate = new Date(2000, 0, 1);
        var endDate = new Date(2016, 0, 1);

        var formatCell = excel.addStyle({
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );
        var formatCell_C = excel.addStyle({
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_L = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_R = excel.addStyle({
            align: "R",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
        });

        var format_Monto = excel.addStyle({
            //format: '#,##0.00',
            align: "C",
            format: '$#,##0.00',
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );
        var format_Porcentaje = excel.addStyle({
            format: 't0%',
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        }
        );

        var format_PorcentajeDecimal = excel.addStyle({
            format: 't0.00%',
            align: "C",
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
        var headers = ["Periodo","Num. Cliente", "Cliente", "Tamaño", "RIK", "UEN", "Segmento", "Aplicación", "Venta", "$ Potencial Integralidad Teórico", "% Potencial Integralidad Teórico"];

        for (var i = 0; i < headers.length; i++) {                       // Loop headers
            excel.set(sheet, i, line, headers[i], formatHeader);             // Set CELL header text & header format
            excel.set(sheet, i, undefined, "auto");                      // Set COLUMN width to auto 
        }

        line++;

        for (var i = 0; i < listAplicaciones.length; i++) {
            // Imprime registro Normal
            let posicion = 0;
            excel.set(sheet, posicion, line, Periodo, formatCell_C);
            posicion++;

            excel.set(sheet, posicion, line, listAplicaciones[i]?.Id_Cte, formatCell_C);
            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Cliente, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Bracket, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Rik, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Uen_Desc, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Seg_Descripcion, formatCell);

            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Apl_Descripcion, formatCell);

            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.Venta, format_Monto);

            posicion++;
            excel.set(sheet, posicion, line, listAplicaciones[i]?.MontoPotencial, format_Monto);

            posicion++;
            excel.set(sheet, posicion, line, (listAplicaciones[i]?.PorcentajeAplicacion) / 100, format_PorcentajeDecimal);

            line = line + 1;
        }

        //reporte Categoria

        excel.addSheet("Reporte Categoría");  // Añadir hoja manualmente

        var headers = ["Periodo","Num. Cliente", "Cliente", "Tamaño", "RIK", "UEN", "Segmento"];
        const listporcentajeVentas = listaTipoProductosh.filter(item => item.includes('% Venta'));
        const listmontoVentas = listaTipoProductosh.filter(item => item.includes('$ Venta'));
        const listaFinalH = [...headers, ...listporcentajeVentas, ...listmontoVentas];

        line = 0;

        sheet = 1;
        excel.set(sheet, 0, line, "Integralidad - Reporte Categoría ", formatTitulo);
        excel.set(sheet, 0, line + 1, "CDS : " + CDI_Nombre, formatTitulo);
        excel.set(0, 0, line + 2, "Representante: " + Rik + " " + lbRik, formatTitulo);
        excel.set(0, 0, line + 3, "UEN: " + ddlFiltroUen, formatTitulo);
        excel.set(sheet, 0, line + 4, "Segmento: " + lbSeg, formatTitulo);
        //excel.set(0, 0, line + 5, "Empresa: " + lbCtes, formatTitulo);
        excel.set(sheet, 0, line + 5, "Fecha: " + Fecha, formatTitulo);

        line = 7;

        for (var i = 0; i < listaFinalH.length; i++) {                       // Loop headers
            excel.set(sheet, i, 6, listaFinalH[i], formatHeader);             // Set CELL header text & header format
            excel.set(sheet, i, undefined, "auto");                      // Set COLUMN width to auto 
        }

        for (var i = 0; i < Data.length; i++) {
            // Imprime registro Normal
            let posicion = 0;
            excel.set(sheet, posicion, line, Periodo, formatCell_C);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Id_Cte, formatCell_C);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Cliente, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Bracket, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Rik, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Uen_Desc, formatCell);
            posicion++;
            excel.set(sheet, posicion, line, Data[i]?.Seg_Descripcion, formatCell);

       
            for (var z = 0; z < listporcentajeVentas.length; z++) {
             
                const resultadosPorcentaje = datosTipoProducto.filter(item =>
                    item.Id_Cte === Data[i].Id_Cte && item.Id_Rik === Data[i].Id_Rik  &&
                    item.TipoProducto.trim() === listporcentajeVentas[z].replace('% Venta ', '')
                );

                // Sumar los PorcentajeVenta de todos los resultados
                let PorcentajeAplicacionAppCliente = 0;
                if (resultadosPorcentaje.length > 0) {
                    PorcentajeAplicacionAppCliente = resultadosPorcentaje.reduce((sum, item) => sum + item.PorcentajeVenta, 0);
                }
                posicion++;
                excel.set(sheet, posicion, line, ((PorcentajeAplicacionAppCliente) / 100), format_Porcentaje);
            }

            for (var z = 0; z < listmontoVentas.length; z++) {

                const resultadoPorcentaje = datosTipoProducto.filter(item =>
                    item.Id_Cte === Data[i].Id_Cte && item.Id_Rik === Data[i].Id_Rik &&
                    item.TipoProducto.trim() === listmontoVentas[z].replace('$ Venta ', '')
                );

                let ventaTot = 0;
                if (resultadoPorcentaje.length > 0) {
                    ventaTot = resultadoPorcentaje.reduce((sum, item) => sum + item.Venta, 0);
                }

         
                posicion++;
                excel.set(sheet, posicion, line, ventaTot, format_Monto);
            }


            line = line + 1;
        }


        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("ReporteIntegralidad_Aplicaciones_" + Fecha + ".xlsx");
    }

}