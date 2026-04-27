using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapAcys_ServicioValorTipo
    {
        // 26 NOV 2018 RFH  
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }
        
        public List<eCapAcys2_TipoServicio> Consultar_ServicioValor(int Id_Emp, int Id_Cd, int IdTipoServicio, string Conexion)
        {
            List<eCapAcys2_TipoServicio> Lst = new List<eCapAcys2_TipoServicio>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@IdTipoServicio", IdTipoServicio);
                
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_ServicioValorTipo", ref dr, Parametros.ToArray(), Valores.ToArray());
                while (dr.Read())
                {
                    eCapAcys2_TipoServicio obj = new eCapAcys2_TipoServicio();
                    obj.IdCapAcys_ServicioValor_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCapAcys_ServicioValor_Tipo")));
                    obj.TipoServicioNombre = dr.GetValue(dr.GetOrdinal("TipoServicioNombre")).ToString();           
                    obj.IdTipoServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdTipoServicio")));
                    obj.IdTipoServicioPadre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdTipoServicioPadre")));
                    Lst.Add(obj);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }
            return Lst;
        }

    }
}
