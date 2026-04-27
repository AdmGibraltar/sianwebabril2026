using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

// 5 Marzo 2019 actualiza RFH 
namespace CapaDatos
{
    /// <summary>
    /// Clase de acceso a datos para el repositorio CapValProyectoParams
    /// </summary>
    public class CD_CapValProyectoParams
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        /// <summary>
        /// Inserta un registro en la tabla CapValProyectoParams.
        /// </summary>
        /// <param name="datos">Instancia de datos de la entidad CapValProyectoParam</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValProyecto_Params</returns>
        public CapValProyecto_Params Insertar(CapValProyecto_Params datos, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValProyecto_Params.Add(datos);
        }

        public CapValProyecto_Params Consultar(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var pars = from cvpp in ctx.CapValProyecto_Params
                       where cvpp.Id_Emp == idEmp && cvpp.Id_Cd == idCd && cvpp.Id_Vap == idVal
                       select cvpp;
            if (pars.Count() > 0)
                return pars.First();
            return null;
        }

        //
        public int CapValProyectoParams_InsertUpdate(int Id_Emp, int Id_Cd, eCapValProyectoParams Data, string Conexion)
        {
            int Result = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Vap", Data.Id_Vap);
                Parametro("@Vap_Vigencia", Data.Vap_Vigencia);
                Parametro("@Vap_Participacion", Data.Vap_Participacion);
                Parametro("@Vap_Mano_Obra", Data.Vap_Mano_Obra);
                Parametro("@Vap_Amortizacion", Data.Vap_Amortizacion);
                Parametro("@Vap_Numero_Entregas", Data.Vap_Numero_Entregas);
                Parametro("@Vap_Costo_Entregas", Data.Vap_Costo_Entregas);
                Parametro("@Vap_Comision_Factoraje", Data.Vap_Comision_Factoraje);
                Parametro("@Vap_Comision_Anden", Data.Vap_Comision_Anden);
                Parametro("@Vap_Gasto_Flete_Locales", Data.Vap_Gasto_Flete_Locales);
                Parametro("@Vap_IVA", Data.Vap_IVA);
                Parametro("@Vap_Plazo_Pago_Cliente", Data.Vap_Plazo_Pago_Cliente);
                Parametro("@Vap_Inventario_Key", Data.Vap_Inventario_Key);
                Parametro("@Vap_Inventario_Consignacion", Data.Vap_Inventario_Consignacion);
                Parametro("@Vap_Inventario_Papel", Data.Vap_Inventario_Papel);
                Parametro("@Vap_Consignacion_Papel", Data.Vap_Consignacion_Papel);
                Parametro("@Vap_Credito_Key", Data.Vap_Credito_Key);
                Parametro("@Vap_Credito_Papel", Data.Vap_Credito_Papel);
                Parametro("@Vap_ISR", Data.Vap_ISR);
                Parametro("@Vap_Ucs", Data.Vap_Ucs);
                Parametro("@Vap_Cetes", Data.Vap_Cetes);
                Parametro("@Vap_Adicional_Cetes", Data.Vap_Adicional_Cetes);
                Parametro("@Vap_Costos_Fijos_No_Papel", Data.Vap_Costos_Fijos_No_Papel);
                Parametro("@Vap_Costos_Fijos_Papel", Data.Vap_Costos_Fijos_Papel);
                Parametro("@Vap_Gastos_Admin", Data.Vap_Gastos_Admin);
                Parametro("@Vap_Inversion_Activos", Data.Vap_Inversion_Activos);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_params_InsertUpdate", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    Result = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID_VAP")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Result = -1;
            }
            return Result;
        }

        //
    }
}
