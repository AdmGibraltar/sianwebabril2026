using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaNegocios
{
    public class CN_Rep_PolizaAmortizacion
    {


        public DataTable ConsultaReportePoliza(string Conexion, int Tipo, int Id_Cd, int NivelCliente, int NivelProducto, int mes, int anio)
        {
            try
            {
                CD_Rep_AmortizacionPoliza claseCapaDatos = new CD_Rep_AmortizacionPoliza();
                DataTable drResponse = claseCapaDatos.ConsultaAmortizacionPoliza(Conexion, Tipo, Id_Cd, NivelCliente, NivelProducto, mes, anio);
                return drResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}