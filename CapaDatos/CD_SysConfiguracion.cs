using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_SysConfiguracion
    {
        // 20AGO-2021 RFH         
        public eSysConfiguracion spSysConfiguracionById(int Id_Emp, int Id_Cd, int Id_Conf, string Conexion)
        {
            eSysConfiguracion Obj = new eSysConfiguracion();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Conf" };
                object[] Valores = new object[] { Id_Emp, Id_Cd, Id_Conf };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysConfiguracionById", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_Conf = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Conf")));
                    Obj.Conf_Valor = dr.GetValue(dr.GetOrdinal("Conf_Valor")).ToString();
                    Obj.Conf_Descripcion = dr.GetValue(dr.GetOrdinal("Conf_Descripcion")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Obj = null;
            }
            return Obj;
        }


    }
}