using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Rep_AmortizacionPoliza
    {
        #region Parametros
        string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Fecha"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                            };

        #endregion

        public DataTable ConsultaAmortizacionPoliza(string Conexion, int Tipo, int Id_Cd, int NivelCliente, int NivelProducto, int mes, int anio)
        {
            try
            {
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Tipo"
                             ,"@Id_Cd"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@mes"
                             ,"@anio"
                            };
                object[] Valores ={
                                   Tipo
                                  ,Id_Cd
                                  ,NivelCliente
                                  ,NivelProducto
                                  ,mes
                                  ,anio
                                  };
                //, Parametros, Valores
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_Rep_PolizaAmortizacion", ref dr, Parametros, Valores);

                DataTable dtAmortizar = new DataTable("SaldoAmortizar");

                //Load DataReader into the DataTable.
                dtAmortizar.Load(dr);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return dtAmortizar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}