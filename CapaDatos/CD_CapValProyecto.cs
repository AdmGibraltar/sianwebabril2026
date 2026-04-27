using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;

using System.Data;

//
// 22JUL-2019 RFH
// 

namespace CapaDatos
{
    public class CD_CapValProyecto
    {

        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        public List<eCrmProyecto> GetCrmProyecto(
            int Id_Emp, int Id_Cd, int Id_Rik, int Id_Cte, int Agrupado, string Conexion)
        {
            List<eCrmProyecto> lst = new List<eCrmProyecto>();

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Rik", Id_Rik);
                Parametro("@Id_Cte", Id_Cte);
                Parametro("@Agrupado", Agrupado);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_SelProyectos", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eCrmProyecto obj = new eCrmProyecto();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Op = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Op")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.NombreCliente = dr.GetValue(dr.GetOrdinal("ClienteNombre")).ToString();
                    obj.AreaDeAplicacion = dr.IsDBNull(dr.GetOrdinal("AreaDeAplicacion")) ? "" : dr.GetValue(dr.GetOrdinal("AreaDeAplicacion")).ToString();
                    obj.Vap_UtilidadRemanente = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_UtilidadRemanente")));
                    obj.Vap_ValorPresenteNeto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_ValorPresenteNeto")));
                    obj.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    obj.Vap_Estatus2 = dr.GetValue(dr.GetOrdinal("Vap_Estatus2")).ToString();
                    obj.Valuacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Valuacion")));
                    obj.Id_Ter = dr.GetValue(dr.GetOrdinal("Id_Ter")).ToString();
                    obj.Estatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));
                    obj.Id_CrmProspecto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CrmProspecto")));
                    obj.Crm_TipoVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Crm_TipoVenta")));
                    obj.VinculadoCentral = dr.GetValue(dr.GetOrdinal("VinculadoCentral")).ToString();
                    obj.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    obj.Id_CteDet = dr.IsDBNull(dr.GetOrdinal("Id_CteDet")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CteDet")));

                    obj.Uen_Descripcion = dr.IsDBNull(dr.GetOrdinal("Uen_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Uen_Descripcion")).ToString();
                    obj.Seg_Descripcion = dr.IsDBNull(dr.GetOrdinal("Seg_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Seg_Descripcion")).ToString();
                    obj.Area_Descripcion = dr.IsDBNull(dr.GetOrdinal("Area_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Area_Descripcion")).ToString();
                    obj.Sol_Descripcion = dr.IsDBNull(dr.GetOrdinal("Sol_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Sol_Descripcion")).ToString();
                    obj.Apl_Descripcion = dr.IsDBNull(dr.GetOrdinal("Apl_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();

                    obj.ValorPotencialTeorico = dr.IsDBNull(dr.GetOrdinal("ValorPotencialTeorico")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ValorPotencialTeorico")));
                    obj.VentaPromedioMensualEsperada = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaPromedioMensualEsperada")));
                    obj.VentaPromedioMensualEsperadaAntesCierre = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaPromedioMensualEsperadaAntesCierre")));
                    obj.CountProd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CountProd")));
                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                lst = null;
            }

            return lst;
        }

        //

    }
}