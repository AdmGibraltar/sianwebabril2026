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
    public class CD_CatFrecuenciaVisita
    {
        // 20 NOV 2018 RFH  
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }
        

        public List<eCatFrecuenciaVisita> Listado(string Conexion)
        {
            List<eCatFrecuenciaVisita> Lst = new List<eCatFrecuenciaVisita>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };
                object[] Valores = { };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFrecuenciaVisita_Todos", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eCatFrecuenciaVisita obj = new eCatFrecuenciaVisita();
                    obj.Id_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Frecuencia = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(obj);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);                
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        //
    }
}
