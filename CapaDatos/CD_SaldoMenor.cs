using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_SaldoMenor
    {
        public void ConsultarSaldo(SaldoMenor Datos, ref List<SaldoMenor> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                SaldoMenor registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@fechaInicial",
                                        "@fechaFinal",
                                        "@tipoDoc"};
                object[] Valores = { Datos.Id_Emp, Datos.Id_Cd, Datos.fechainicial, Datos.fechaFinal, Datos.Doc_tipo };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_RepSaldoMenores", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new SaldoMenor();
                    registro.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    registro.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    registro.Id_Doc = dr.IsDBNull(dr.GetOrdinal("Id_Doc")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Doc"));
                    registro.Doc_tipo = dr.IsDBNull(dr.GetOrdinal("Doc_Tipo")) ? "F" : dr.GetString(dr.GetOrdinal("Doc_Tipo"));
                    registro.Fecha_documento = dr.GetDateTime(dr.GetOrdinal("fecha_Documento"));
                    registro.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    registro.Cte_Nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));

                    registro.Importe = dr.IsDBNull(dr.GetOrdinal("Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Importe"));
                    registro.Saldo = dr.IsDBNull(dr.GetOrdinal("Saldo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Saldo"));
                    registro.Saldo_Menor = dr.IsDBNull(dr.GetOrdinal("SaldoMenor")) ? 0 : dr.GetDouble(dr.GetOrdinal("SaldoMenor"));
                    registro.Fecha_SaldoMenor = dr.GetDateTime(dr.GetOrdinal("fecha_Saldo"));

                    if (registro.Doc_tipo.ToLower() == "f")
                    {
                        registro.Doc = "Factura";
                    }
                    else
                    {
                        registro.Doc = "Nota de Cargo";
                    }
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}