using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ReporteFacturacionCDC
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        public int ReporteFacturacionCDC(int IdEmp, int IdCDI, DateTime fInicio, DateTime fFinal, string Conexion, ref List<ResultReporteFacturacionCDC> ResultadosValuacion)
        {
            int iResult = 0;
            ResultReporteFacturacionCDC itemResultado = new ResultReporteFacturacionCDC();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", IdEmp);
                Parametro("@Id_Cd", IdCDI);
                Parametro("@Fecha_inicio", fInicio);
                Parametro("@Fecha_fin", fFinal);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteFacturacionCDC", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    itemResultado = new ResultReporteFacturacionCDC();

                    ///     Id_Emp Id_Cd   Id_Cte Cte_NomComercial    Id_Fac 
                    ///     ImporteConIVA   DiasPago FecFactura  FecVenceFactura 
                    ///     Fac_Pagado  Fac_EstatusStr Fac_PedNum  Pag_Importe OrdenCompra

                    itemResultado.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    itemResultado.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    itemResultado.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    itemResultado.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    itemResultado.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));

                    itemResultado.ImporteConIVA = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ImporteConIVA"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ImporteConIVA")));
                    itemResultado.DiasPago = (int)dr.GetValue(dr.GetOrdinal("DiasPago"));
                    itemResultado.FecFactura = (DateTime)dr.GetValue(dr.GetOrdinal("FecFactura"));
                    itemResultado.FecVenceFactura = (DateTime)dr.GetValue(dr.GetOrdinal("FecVenceFactura"));

                    itemResultado.Fac_Pagado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Fac_Pagado"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    itemResultado.Fac_EstatusStr = (string)dr.GetValue(dr.GetOrdinal("Fac_EstatusStr"));
                    itemResultado.Fac_PedNum = (string)dr.GetValue(dr.GetOrdinal("Fac_PedNum"));
                    itemResultado.Pag_Importe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pag_Importe"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Pag_Importe")));
                    itemResultado.OrdenCompra = (string)dr.GetValue(dr.GetOrdinal("OrdenCompra"));

                    //RBM Oct 2024
                    //Nuevos Campos
                    itemResultado.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    itemResultado.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    itemResultado.Rik_Nombre = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    itemResultado.Fac_Estatus = (string)dr.GetValue(dr.GetOrdinal("Fac_Estatus"));
                    itemResultado.FPago = (string)dr.GetValue(dr.GetOrdinal("FPago"));
                    itemResultado.PagoMetodo = (string)dr.GetValue(dr.GetOrdinal("PagoMetodo"));
                    //itemResultado.CondPago = (int)dr.GetValue(dr.GetOrdinal("CondPago"));
                    itemResultado.Analista = (string)dr.GetValue(dr.GetOrdinal("Analista"));
                    itemResultado.Grupo = (string)dr.GetValue(dr.GetOrdinal("Grupo"));

                    //Sección 2
                    itemResultado.DiasRevision = (string)dr.GetValue(dr.GetOrdinal("DiasRevision"));
                    itemResultado.HorarioRevision = (string)dr.GetValue(dr.GetOrdinal("HorarioRevision"));
                    itemResultado.DocAdicionales = (string)dr.GetValue(dr.GetOrdinal("DocAdicionales"));
                    itemResultado.CampoOtro = (string)dr.GetValue(dr.GetOrdinal("CampoOtro"));
                    itemResultado.DiasPagos = (string)dr.GetValue(dr.GetOrdinal("DiasPagos"));
                    itemResultado.HorarioPago = (string)dr.GetValue(dr.GetOrdinal("HorarioPago"));
                    itemResultado.ContraEntrega = (string)dr.GetValue(dr.GetOrdinal("ContraEntrega"));

                    //Sección 3
                    itemResultado.DiasRecepcion = (string)dr.GetValue(dr.GetOrdinal("DiasRecepcion"));
                    itemResultado.HorarioEntrega = (string)dr.GetValue(dr.GetOrdinal("HorarioEntrega"));
                    itemResultado.PersonaRecibe = (string)dr.GetValue(dr.GetOrdinal("PersonaRecibe"));
                    itemResultado.CitaEntrega = (string)dr.GetValue(dr.GetOrdinal("CitaEntrega"));
                    itemResultado.AreaRecibo = (string)dr.GetValue(dr.GetOrdinal("AreaRecibo"));
                    itemResultado.Estacionamiento = (string)dr.GetValue(dr.GetOrdinal("Estacionamiento"));
                    itemResultado.DocEntrega = (string)dr.GetValue(dr.GetOrdinal("DocEntrega"));
                    itemResultado.DocRecepcion = (string)dr.GetValue(dr.GetOrdinal("DocRecepcion"));

                    ResultadosValuacion.Add(itemResultado);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult;
        }
    }
}