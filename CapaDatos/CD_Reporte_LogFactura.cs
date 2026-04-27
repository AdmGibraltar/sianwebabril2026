using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte_LogFactura
    {
        public void ListaLog(eReporte_LogFactura search, string Conexion, ref List<eReporte_LogFactura> List)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac", "@Fac_FolioFiscal", "@Id_Cte", "@FechaIni", "@FechaFin" };
                object[] Valores = { search.Id_Emp, search.Id_Cd, search.Id_Fac, search.Fac_FolioFiscal, search.Id_Cte, search.FechaIni, search.FechaFin };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("FacturaLogReporte_Consultar", ref dr, Parametros, Valores);//spCapMetas_Lista
                while (dr.Read())
                {
                    eReporte_LogFactura result = new eReporte_LogFactura();
                    result.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cliente"));

                    result.UsuarioFactura = (string)dr.GetValue(dr.GetOrdinal("UsuarioFactura"));
                    result.FechaFactura = (string)dr.GetValue(dr.GetOrdinal("FechaFactura"));
                    result.ActFactura = (string)dr.GetValue(dr.GetOrdinal("ActFactura"));
                    result.DocFactura = (string)dr.GetValue(dr.GetOrdinal("DocFactura"));

                    result.UsuarioCancela = (string)dr.GetValue(dr.GetOrdinal("UsuarioCancela"));
                    result.FechaCancela = (string)dr.GetValue(dr.GetOrdinal("FechaCancela"));
                    result.ActCancela = (string)dr.GetValue(dr.GetOrdinal("ActCancela"));
                    result.DocCancela = (string)dr.GetValue(dr.GetOrdinal("DocCancela"));

                    result.UsuarioRelacionAlmacen = (string)dr.GetValue(dr.GetOrdinal("UsuarioRelacionAlmacen"));
                    result.FechaRelacionAlmacen = (string)dr.GetValue(dr.GetOrdinal("FechaRelacionAlmacen"));
                    result.ActRelacionAlmacen = (string)dr.GetValue(dr.GetOrdinal("ActRelacionAlmacen"));
                    result.DocRelacionAlmacen = (string)dr.GetValue(dr.GetOrdinal("DocRelacionAlmacen"));

                    result.UsuarioConfirmadaAlmacen = (string)dr.GetValue(dr.GetOrdinal("UsuarioConfirmadaAlmacen"));
                    result.FechaConfirmadaAlmacen = (string)dr.GetValue(dr.GetOrdinal("FechaConfirmadaAlmacen"));
                    result.ActConfirmadaAlmacen = (string)dr.GetValue(dr.GetOrdinal("ActConfirmadaAlmacen"));
                    result.DocConfirmadaAlmacen = (string)dr.GetValue(dr.GetOrdinal("DocConfirmadaAlmacen"));

                    result.UsuarioEmbarque = (string)dr.GetValue(dr.GetOrdinal("UsuarioEmbarque"));
                    result.FechaEmbarque = (string)dr.GetValue(dr.GetOrdinal("FechaEmbarque"));
                    result.ActEmbarque = (string)dr.GetValue(dr.GetOrdinal("ActEmbarque"));
                    result.DocEmbarque = (string)dr.GetValue(dr.GetOrdinal("DocEmbarque"));

                    result.UsuarioRegresoAlmacen = (string)dr.GetValue(dr.GetOrdinal("UsuarioRegresoAlmacen"));
                    result.FechaRegresoAlmacen = (string)dr.GetValue(dr.GetOrdinal("FechaRegresoAlmacen"));
                    result.ActRegresoAlmacen = (string)dr.GetValue(dr.GetOrdinal("ActRegresoAlmacen"));
                    result.DocRegresoAlmacen = (string)dr.GetValue(dr.GetOrdinal("DocRegresoAlmacen"));

                    result.UsuarioRelacionCobranza = (string)dr.GetValue(dr.GetOrdinal("UsuarioRelacionCobranza"));
                    result.FechaRelacionCobranza = (string)dr.GetValue(dr.GetOrdinal("FechaRelacionCobranza"));
                    result.ActRelacionCobranza = (string)dr.GetValue(dr.GetOrdinal("ActRelacionCobranza"));
                    result.DocRelacionCobranza = (string)dr.GetValue(dr.GetOrdinal("DocRelacionCobranza"));

                    result.UsuarioEnviadaRevision = (string)dr.GetValue(dr.GetOrdinal("UsuarioEnviadaRevision"));
                    result.FechaEnviadaRevision = (string)dr.GetValue(dr.GetOrdinal("FechaEnviadaRevision"));
                    result.ActEnviadaRevison = (string)dr.GetValue(dr.GetOrdinal("ActEnviadaRevison"));
                    result.DocEnviadaRevision = (string)dr.GetValue(dr.GetOrdinal("DocEnviadaRevision"));

                    result.UsuarioConfirmadaRevision = (string)dr.GetValue(dr.GetOrdinal("UsuarioConfirmadaRevision"));
                    result.FechaConfirmadaRevision = (string)dr.GetValue(dr.GetOrdinal("FechaConfirmadaRevision"));
                    result.ActConfirmadaRevision = (string)dr.GetValue(dr.GetOrdinal("ActConfirmadaRevision"));
                    result.DocConfirmadaRevision = (string)dr.GetValue(dr.GetOrdinal("DocConfirmadaRevision"));

                    result.UsuarioEnviadaCobro = (string)dr.GetValue(dr.GetOrdinal("UsuarioEnviadaCobro"));
                    result.FechaEnviadaCobro = (string)dr.GetValue(dr.GetOrdinal("FechaEnviadaCobro"));
                    result.ActEnviadaCobro = (string)dr.GetValue(dr.GetOrdinal("ActEnviadaCobro"));
                    result.DocEnviadaCobro = (string)dr.GetValue(dr.GetOrdinal("DocEnviadaCobro"));

                    result.UsuarioEmbarqueV2 = (string)dr.GetValue(dr.GetOrdinal("UsuarioEmbarqueV2"));
                    result.FechaEmbarqueV2 = (string)dr.GetValue(dr.GetOrdinal("FechaEmbarqueV2"));
                    result.ActEmbarqueV2 = (string)dr.GetValue(dr.GetOrdinal("ActEmbarqueV2"));
                    result.DocEmbarqueV2 = (string)dr.GetValue(dr.GetOrdinal("DocEmbarqueV2"));

                    result.UsuarioEntregada = (string)dr.GetValue(dr.GetOrdinal("UsuarioEntregada"));
                    result.FechaEntregada = (string)dr.GetValue(dr.GetOrdinal("FechaEntregada"));
                    result.ActEntregada = (string)dr.GetValue(dr.GetOrdinal("ActEntregada"));
                    result.DocEntregada = (string)dr.GetValue(dr.GetOrdinal("DocEntregada"));

                    result.UsuarioConfirmadaCobranza = (string)dr.GetValue(dr.GetOrdinal("UsuarioConfirmadaCobranza"));
                    result.FechaConfirmadaCobranza = (string)dr.GetValue(dr.GetOrdinal("FechaConfirmadaCobranza"));
                    result.ActConfirmadaCobranza = (string)dr.GetValue(dr.GetOrdinal("ActConfirmadaCobranza"));
                    result.DocConfirmadaCobranza = (string)dr.GetValue(dr.GetOrdinal("DocConfirmadaCobranza"));


                    List.Add(result);
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