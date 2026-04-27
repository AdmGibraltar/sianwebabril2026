using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaModelo;
using CapaEntidad;

namespace CapaDatos
{

    public class CD_CapValProyecto_Parametros
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

        public CapValProyecto_Parametros Insertar(CapValProyecto_Parametros entityInstance, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValProyecto_Parametros.Add(entityInstance);
        }

        // 
        // May8 - 2019 RFH
        // Reemplaza la version EF 
        //
        public int spCapValProyecto_Insertar(CapValProyecto_Parametros Par, string Conexion)
        {
            int iResult = 0;
            /*
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValProyecto_Parametros.Add(entityInstance);
             */

            eCapValProyectoParametros obj = new eCapValProyectoParametros();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", Par.Id_Emp);
                Parametro("@Id_Cd", Par.Id_Cd);
                Parametro("@Id_Vap", Par.Id_Vap);
                Parametro("@CuentasPorCobrar", Par.txtCuentasPorCobrar);
                Parametro("@Invetario", Par.txtInventario);
                Parametro("@GastosServirCliente", Par.txtGastosServirCliente);
                Parametro("@Vigencia", Par.txtVigencia);
                Parametro("@Fletelocales", Par.txtFleteLocales);
                Parametro("@Isr", Par.txtIsr);
                Parametro("@Cetes", Par.txtCetes);
                Parametro("@Financiamientoproveedores", Par.txtFinanciamientoproveedores);
                Parametro("@Inversionactivosfijos", Par.txtInversionactivosfijos);
                Parametro("@Costodecapital", Par.txtCostodecapital);
                Parametro("@ManoObra", Par.txtManoObra);
                Parametro("@GastosVarAplTerr", Par.txtGastosVarAplTerr);
                Parametro("@FletesPagadosalCliente", Par.txtFletesPagadosalCliente);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_CapValProyectoParametros_InsertUpdate", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfectedRows")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                iResult = 1;
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult;
        }



        /// <summary>
        /// Deuelve el resultado de la consulta al repositorio CapValProyecto_Parametros dado el identificador de la valuación idVal.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValProyecto_Parametros</returns>
        public CapValProyecto_Parametros ConsultarPorValuacion(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var parametros = from cvpp in ctx.CapValProyecto_Parametros
                             where cvpp.Id_Emp == idEmp && cvpp.Id_Cd == idCd && cvpp.Id_Vap == idVal
                             select cvpp;
            if (parametros.Count() > 0)
            {
                return parametros.First();
            }
            return null;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // 10 Jul 2018 
        //
        public eCapValProyectoParametros InsertUpdate_CapValProyectoParametros(eCapValProyectoParametros C, string Conexion)
        {

            eCapValProyectoParametros obj = new eCapValProyectoParametros();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {                     
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Vap",

                    "@CuentasPorCobrar",
                    "@Invetario",
                    "@GastosServirCliente",
                    "@Vigencia",
                    "@Fletelocales",
                    "@Isr",
                    "@Cetes",
                    "@Financiamientoproveedores",
                    "@Inversionactivosfijos",
                    "@Costodecapital",
                    "@ManoObra",
                    "@GastosVarAplTerr",
                    "@FletesPagadosalCliente"
                };

                object[] Valores = { 
                    C.Id_Emp, 
                    C.Id_Cd,
                    C.Id_Vap,

                    C.CuentasPorCobrar,
                    C.Inventario,
                    C.GastosServirCliente,
                    C.Vigencia,
                    C.FleteLocales,
                    C.Isr,
                    C.Cetes,
                    C.Financiamientoproveedores ,
                    C.Inversionactivosfijos  ,
                    C.Costodecapital,
                    C.ManoObra,
                    C.GastosVarAplTerr,
                    C.FletesPagadosalCliente                    
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParametros_InsertUpdate", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    obj.SqlResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SqlResult")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                obj.SqlResult = -1;
                //obj = null;
                //throw ex;
            }
            return obj;
        }

        //

    }
}
