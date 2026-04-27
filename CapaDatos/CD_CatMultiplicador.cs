using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_CatMultiplicador
    {
        public void AgregarMultiplicadorRIk(CatMultiplicador RegistroMultiplicador, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp", 
                                         "@Sucursal", 
                                         "@Nombre_rik",
                                         "@Mes",
                                         "@Anio",
                                         "@Multiplicador",
                                         "@permiso"
                };
                object[] Valores = { RegistroMultiplicador.Id_Emp , 
                                       RegistroMultiplicador.Sucursal , 
                                       RegistroMultiplicador.NombreRik,
                                       RegistroMultiplicador.Mes ,
                                       RegistroMultiplicador.Anio ,
                                       RegistroMultiplicador.Multiplicador,
                                       RegistroMultiplicador.permiso};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "SpCatMultiplicador_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaMltiplicadorRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatMultiplicador nuevoMultiplocador;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Mes",
                                          "@Anio"
                };
                object[] Valores = {
                                       RegistroMultiplicador.Id_Emp,
                                       RegistroMultiplicador.Id_Cd,
                                       RegistroMultiplicador.Mes,
                                       RegistroMultiplicador.Anio
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpCatMultiplicador_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoMultiplocador = new CatMultiplicador();

                    nuevoMultiplocador.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    nuevoMultiplocador.Id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    nuevoMultiplocador.Sucursal = dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? "" : dr.GetString(dr.GetOrdinal("Sucursal"));
                    nuevoMultiplocador.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoMultiplocador.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoMultiplocador.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoMultiplocador.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoMultiplocador.Multiplicador = dr.IsDBNull(dr.GetOrdinal("Multiplicador")) ? "0" : dr.GetString(dr.GetOrdinal("Multiplicador"));
                    nuevoMultiplocador.TotalMultiplicador = decimal.Parse(nuevoMultiplocador.Multiplicador);

                    list_Multiplicador.Add(nuevoMultiplocador);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaMltiplicadorMensualRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatMultiplicador nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroMultiplicador.Id_Emp,
                                       RegistroMultiplicador.Id_Cd,
                                       RegistroMultiplicador.FechaInicial,
                                       RegistroMultiplicador.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarMultiplicadorMensualRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatMultiplicador();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.TotalMultiplicador = dr.IsDBNull(dr.GetOrdinal("Totalmultiplicador")) ? 0 : (dr.GetDecimal(dr.GetOrdinal("Totalmultiplicador")));
                    list_Multiplicador.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaMltiplicadorMRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatMultiplicador nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroMultiplicador.Id_Emp,
                                       RegistroMultiplicador.Id_Cd,
                                       RegistroMultiplicador.FechaInicial,
                                       RegistroMultiplicador.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarMultiplicadorMenRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatMultiplicador();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.TotalMultiplicador = dr.IsDBNull(dr.GetOrdinal("Totalmultiplicador")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Totalmultiplicador"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    list_Multiplicador.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



         

        public void ConsultaMltiplicadorPresupuestoRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatMultiplicador nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal",
                                         "@id_rik"
                };
                object[] Valores = {
                                       RegistroMultiplicador.Id_Emp,
                                       RegistroMultiplicador.Id_Cd,
                                       RegistroMultiplicador.FechaInicial,
                                       RegistroMultiplicador.FechaFinal,
                                       RegistroMultiplicador.Id_Rik == -1? (object) null : RegistroMultiplicador.Id_Rik
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarMultiplicadorPresupuestoRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatMultiplicador();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    nuevoPresupuesto.Sucursal = dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? "" : dr.GetString(dr.GetOrdinal("Sucursal"));
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.TotalMultiplicador = dr.IsDBNull(dr.GetOrdinal("Totalmultiplicador")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Totalmultiplicador"));
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("Totalpresupuesto")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Totalpresupuesto"));
                    list_Multiplicador.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
