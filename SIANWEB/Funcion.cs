using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace SIANWEB
{
    public class Funcion
    {

        /// <summary>
        /// Clase genérica para convertir una Lista Genérica de elementos en
        /// un objeto DataTable
        /// </summary>
        /// <typeparam name="T">Tipo de datos de los elementos de la Lista. 
        /// Debe ser una clase con un constructor sin parámetros. ver referencia de clases genericas</typeparam>

        public static class Convertidor<T> where T : new()
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="items"></param>
            /// <returns></returns>

            public static DataTable ListaToDatatable(List<T> items)
            {

                // Instancia del objeto a devolver

                DataTable dataTable = new DataTable();

                // Información del tipo de datos de los elementos del List

                Type itemsType = typeof(T);

                // Recorremos las propiedades para crear las columnas del datatable

                foreach (PropertyInfo prop in itemsType.GetProperties())
                {

                    // Crearmos y agregamos una columna por cada propiedad de la entidad

                    DataColumn column = new DataColumn(prop.Name);

                    if (prop.PropertyType.FullName == "System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Double");
                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.DateTime");
                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Int32");

                    }
                    else if (prop.PropertyType.FullName == "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        column.DataType = System.Type.GetType("System.Boolean");

                    }
                    else
                    {
                        column.DataType = prop.PropertyType;
                    }


                    dataTable.Columns.Add(column);

                }



                int j;

                // ahora recorremos la colección para guardar los datos
                // en el DataTable

                foreach (T item in items)
                {

                    j = 0;

                    object[] newRow = new object[dataTable.Columns.Count];

                    // Volvemos a recorrer las propiedades de cada item para
                    // obtener su valor guardarlo en la fila de la tabla

                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        newRow[j] = prop.GetValue(item, null);

                        j++;

                    }

                    dataTable.Rows.Add(newRow);

                }

                // Devolver el objeto creado
                return dataTable;

            }



            /// <summary>
            /// Métod encargado de recorrer el DataTable y asignar propiedades al objeto
            /// </summary>
            /// <returns>Una lista de objetos T</returns>

            public static List<T> DataTableToLista(DataTable tabla)
            {

                List<T> lista = new List<T>();

                T elemento;

                for (int i = 0; i < tabla.Rows.Count; i++)
                {

                    // Información del tipo de datos de los elementos del List

                    Type itemsType = typeof(T);

                    elemento = new T();



                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        //Establecemos cada una de las propiedades

                        prop.SetValue(elemento, ValorDefault(tabla.Rows[i][prop.Name]), null);

                    }

                    lista.Add(elemento);



                }

                return lista;

            }



            //Esta parte hay que buscar hacerla de una mejor modo
            /// <summary>
            /// Método que se encarga de validar los DBNull y convertirlos en una cadena vacia
            /// </summary>
            /// <returns>El mismo objeto de entrada validado</returns>

            private static object ValorDefault(object objeto)
            {

                if (objeto == System.DBNull.Value)

                    return "";

                else

                    return objeto;

            }

        }

        public void ExportarExcel(String nombreArchivo, String tabla)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xls");
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; //Excel
                System.IO.StringWriter sw = new System.IO.StringWriter();
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getCDIName(int Id_Cd)
        {
            var name = "";
            switch (Id_Cd)
            {
                //CDC PROPIAS
                case 5:
                    name = "NOR";
                    break;
                case 6:
                    name = "SAL";
                    break;
                case 8:
                    name = "JER";
                    break;
                case 9:
                    name = "REV";
                    break;
                //CDI PROPIAS
                case 110:
                    name = "MTY";
                    break;
                case 150:
                    name = "SAL";
                    break;
                case 170:
                    name = "TOR";
                    break;
                case 160:
                    name = "MAT";
                    break;
                case 180:
                    name = "LAR";
                    break;
                case 190:
                    name = "LEON";
                    break;
                case 200:
                    name = "TIJ";
                    break;
                case 210:
                    name = "CHI";
                    break;
                case 220:
                    name = "SLP";
                    break;
                case 230:
                    name = "JUA";
                    break;
                case 240:
                    name = "AGS";
                    break;
                case 250:
                    name = "HMO";
                    break;
                case 310:
                    name = "MEX";
                    break;
                case 340:
                    name = "VER";
                    break;
                case 360:
                    name = "MER";
                    break;
                case 370:
                    name = "CAN";
                    break;
                case 380:
                    name = "RIV";
                    break;
                case 390:
                    name = "VAL";
                    break;
                case 400:
                    name = "CAB";
                    break;
                case 410:
                    name = "QRO";
                    break;
                case 430:
                    name = "GDL";
                    break;
                case 510:
                    name = "PUE";
                    break;
                case 610:
                    name = "COA";
                    break;
                case 620:
                    name = "VIL";
                    break;
                case 640:
                    name = "TOL";
                    break;
                //CDI FRANQUICIAS
                case 211:
                    name = "MAZ";
                    break;
                case 212:
                    name = "CUL";
                    break;
                case 810:
                    name = "ZAC";
                    break;
                case 12652:
                    name = "DUR";
                    break;
                case 18279:
                    name = "MON";
                    break;
                case 30707:
                    name = "PIE";
                    break;
                case 30713:
                    name = "ACA";
                    break;
                case 31164:
                    name = "JUA";
                    break;
                case 31420:
                    name = "OAX";
                    break;
                case 31425:
                    name = "ACU";
                    break;
                case 32110:
                    name = "TOR";
                    break;
                case 33281:
                    name = "CUE";
                    break;
                case 34132:
                    name = "MOC";
                    break;
                case 34301:
                    name = "DEL";
                    break;
                case 34525:
                    name = "TUX";
                    break;
                case 34526:
                    name = "TAP";
                    break;
                case 34527:
                    name = "MOR";
                    break;
                case 34529:
                    name = "MAN";
                    break;
                case 34530:
                    name = "TEH";
                    break;
                case 34531:
                    name = "CRI";
                    break;
                case 34532:
                    name = "TAM";
                    break;
                case 34535:
                    name = "CUA";
                    break;
                case 34553:
                    name = "LAZ";
                    break;
                case 34554:
                    name = "COL";
                    break;
                //CDC FRANQUICIAS
                case 31929:
                    name = "GDL SUR";
                    break;
                case 32112:
                    name = "PUE";
                    break;
                case 32113:
                    name = "LEON";
                    break;
                case 34101:
                    name = "TEPA";
                    break;
                case 34322:
                    name = "GDL NORTE";
                    break;
                case 34534:
                    name = "TOL";
                    break;
                case 34545:
                    name = "TLAJ";
                    break;
            }
            return name;
        }

    }
}