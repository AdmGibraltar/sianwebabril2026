using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_SolConvenio
    {
        public void ProPrecioConv_SolicitudLista(SolConvenio conv, ref List<SolConvenio> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@TipoFiltro",
                                        "@Sol_Estatus",
                                        "@Id_Cat",
                                        "@Filtro",
                                         "@Id_Cd"};
                object[] Valores = { conv.Filtro_TipoFiltro == (int?) null ? (object) null: conv.Filtro_TipoFiltro,
                                     conv.FiltroSol_Estatus == "-1" ?(object) null: conv.FiltroSol_Estatus,
                                     conv.Filtro_Id_Cat == "-1" ?(object) null: conv.Filtro_Id_Cat,
                                     conv.Filtro_Valor == "" ? (object) null: conv.Filtro_Valor,
                                     conv.FiltroId_CD  == (int?) null ? (object) null: conv.FiltroId_CD };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudListaNueva", ref dr, Parametros, Valores);

                SolConvenio c;
                while (dr.Read())
                {
                    c = new SolConvenio();
                    c.Id_Sol = Convert.ToInt32(dr["Id_Sol"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.Id_PC = Convert.ToInt32(dr["Id_PC"]);
                    c.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                    c.Sol_EstatusStr = dr["Sol_EstatusStr"].ToString();
                    c.Sol_IdUCreo = Convert.ToInt32(dr["Sol_IdUCreo"]);
                    c.Sol_UNombre = dr["Sol_UNombre"].ToString();
                    c.Sol_Fecha = Convert.ToDateTime(dr["Sol_Fecha"]);
                    c.CD_Nombre = dr["Cd_Nombre"].ToString();
                    c.Sol_Unique = dr["Sol_Unique"].ToString();
                    c.Id_UAtendio = Convert.ToInt32(dr["Id_UAtendio"]);
                    c.Sol_NombreAtendio = dr["Sol_NombreAtendio"].ToString();
                    c.Sol_FechaAtendio = Convert.ToDateTime(dr["Sol_FechaAtendio"]);
                    c.OrigenSolicitud = Convert.ToInt32(dr["OrigenSolicitud"]);

                    List.Add(c);

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
