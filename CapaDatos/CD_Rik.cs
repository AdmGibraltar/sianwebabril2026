using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Rik
    {
        public void ConsultaRIk(Rik RegistroRik, ref List<Rik> list_Riks, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Rik DatosRik;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@id_Rik", 
                };
                object[] Valores = {
                                       RegistroRik.Id_Emp,
                                       RegistroRik.Id_Cd,
                                       RegistroRik.Id_Rik == -1? (object)null :   RegistroRik.Id_Rik, 
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpCatRik_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    DatosRik = new Rik();

                    DatosRik.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    DatosRik.Id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    DatosRik.Sucursal = dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? "" : dr.GetString(dr.GetOrdinal("Sucursal"));
                    DatosRik.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    DatosRik.nombre_Rik = dr.IsDBNull(dr.GetOrdinal("rik_nombre")) ? "" : dr.GetString(dr.GetOrdinal("rik_nombre"));
                    DatosRik.nombre_UEN = dr.IsDBNull(dr.GetOrdinal("Uen_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Uen_Descripcion"));

                    list_Riks.Add(DatosRik);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaRIkCrm(Rik RegistroRik, ref List<Rik> list_Riks, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Rik DatosRik;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@id_Rik",
                };
                object[] Valores = {
                                       RegistroRik.Id_Emp,
                                       RegistroRik.Id_Cd,
                                       RegistroRik.Id_Rik == -1? (object)null :   RegistroRik.Id_Rik,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpCatRik_ConsultarCRM", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    DatosRik = new Rik();

                    DatosRik.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    DatosRik.Id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    DatosRik.Sucursal = dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? "" : dr.GetString(dr.GetOrdinal("Sucursal"));
                    DatosRik.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    DatosRik.nombre_Rik = dr.IsDBNull(dr.GetOrdinal("rik_nombre")) ? "" : dr.GetString(dr.GetOrdinal("rik_nombre"));
                    DatosRik.nombre_UEN = dr.IsDBNull(dr.GetOrdinal("Uen_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Uen_Descripcion"));

                    list_Riks.Add(DatosRik);
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
