using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

// 5 Feb 2019 RFH

namespace CapaDatos
{
    public class CD_CapRepValuacionParams
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // 5 Feb 2019 RFH 

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(
                Valor);
            return 0;
        }

        // 
        // 7 Feb 2019 RFH
        public eRepValuacionParams SpCrmOportunidades_RepValuacionProp(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            eRepValuacionParams VP = new eRepValuacionParams();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Op", Id_Op);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_RepValuacionProp", ref dr, Parametros.ToArray(), Valores.ToArray());
                if (dr.HasRows)
                {
                    dr.Read();
                    VP.NombreAtencion = dr.GetValue(dr.GetOrdinal("tbNombreAtencion")).ToString();
                    VP.RepresentanteClienteNombre = dr.GetValue(dr.GetOrdinal("tbRepresentanteClienteNombre")).ToString();
                    VP.NombreRik = dr.GetValue(dr.GetOrdinal("tbNombreRik")).ToString();
                    VP.Direccion1 = dr.GetValue(dr.GetOrdinal("tbDireccion1")).ToString();
                    VP.Direccion2 = dr.GetValue(dr.GetOrdinal("tbDireccion2")).ToString();

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                VP = null;
            }
            return VP;
        }

        // 
        // 7 Feb 2019 RFH

        public eRepValuacionParams SpCrmOportunidades_RepValuacionProp_Ver1(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            eRepValuacionParams VP = new eRepValuacionParams();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Op", Id_Op);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_RepValuacionProp_Ver2", ref dr, Parametros.ToArray(), Valores.ToArray());
                if (dr.HasRows)
                {
                    dr.Read();
                    VP.NombreAtencion = dr.GetValue(dr.GetOrdinal("tbNombreAtencion")).ToString();
                    VP.RepresentanteClienteNombre = dr.GetValue(dr.GetOrdinal("tbRepresentanteClienteNombre")).ToString();
                    VP.NombreRik = dr.GetValue(dr.GetOrdinal("tbNombreRik")).ToString();
                    VP.Direccion1 = dr.GetValue(dr.GetOrdinal("tbDireccion1")).ToString();
                    VP.Direccion2 = dr.GetValue(dr.GetOrdinal("tbDireccion2")).ToString();
                    VP.CP = dr.GetValue(dr.GetOrdinal("Direccion_CP")).ToString();
                    VP.NoExterior = dr.GetValue(dr.GetOrdinal("Direccion_NoExterior")).ToString();
                    VP.Telefono = dr.GetValue(dr.GetOrdinal("Direccion_Telefono")).ToString();
                    VP.DiasCredito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DiasCredito")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                VP = null;
            }
            return VP;
        }

        // JUN30-2020 

        public eCRM_ValuacionCamposAdicionales_Ver2 SpCrmOportunidades_RepValuacionProp_Ver2(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            eCRM_ValuacionCamposAdicionales_Ver2 VP = new eCRM_ValuacionCamposAdicionales_Ver2();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Op", Id_Op);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_RepValuacionProp_Ver3", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    VP.Key_NombreRik = dr.GetValue(dr.GetOrdinal("Key_NombreRik")).ToString();
                    VP.Key_RazonSocial = dr.GetValue(dr.GetOrdinal("Key_RazonSocial")).ToString();
                    VP.Key_Calle = dr.GetValue(dr.GetOrdinal("Key_Calle")).ToString();
                    VP.Key_No = dr.GetValue(dr.GetOrdinal("Key_No")).ToString();
                    VP.Key_CP = dr.GetValue(dr.GetOrdinal("Key_CP")).ToString();
                    VP.Key_Colonia = dr.GetValue(dr.GetOrdinal("Key_Colonia")).ToString();
                    VP.Key_Municipio = dr.GetValue(dr.GetOrdinal("Key_Municipio")).ToString();
                    VP.Key_Estado = dr.GetValue(dr.GetOrdinal("Key_Estado")).ToString();
                    VP.Key_Nomenclatura = dr.GetValue(dr.GetOrdinal("Key_Nomenclatura")).ToString();

                    VP.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_Nombre")).ToString();
                    VP.Cte_Calle = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    VP.Cte_No = dr.GetValue(dr.GetOrdinal("Cte_No")).ToString();
                    VP.Cte_CP = dr.GetValue(dr.GetOrdinal("Cte_CP")).ToString();
                    VP.Cte_Colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    VP.Cte_Municipio = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    VP.Cte_Estado = dr.GetValue(dr.GetOrdinal("Cte_Estado")).ToString();
                    VP.Cte_AtencionA = dr.GetValue(dr.GetOrdinal("Cte_AtencionA")).ToString();

                    VP.Cte_DiasCredito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_DiasCredito")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                VP = null;
            }
            return VP;
        }

        // 7 Feb 2019 RFH 
        // Actualiza campos correspondientes al reporte
        // 
        public int SpCrmOportunidades_UpdateRepParams(int Id_Emp, int Id_Cd,
            int Id_Op, string tbNombreAtencion, string tbRepresentanteClienteNombre, string tbNombreRik,
            string tbDireccion1, string tbDireccion2, string Conexion)
        {
            var iResult = 0;
            eRepValuacionParams VP = new eRepValuacionParams();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Op", Id_Op);
                Parametro("@tbNombreAtencion", tbNombreAtencion);
                Parametro("@tbRepresentanteClienteNombre", tbRepresentanteClienteNombre);
                Parametro("@tbNombreRik", tbNombreRik);
                Parametro("@tbDireccion1", tbDireccion1);
                Parametro("@tbDireccion2", tbDireccion2);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_UpdateRepParams", ref dr, Parametros.ToArray(), Valores.ToArray());
                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RowsAffected")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult;
        }

        //
        // MAY20-2020 RFH 
        // Actualiza campos correspondientes al reporte
        //

        public int SpCrmOportunidades_UpdateRepParams_Ver2(eCRM_ValuacionCamposAdicionales E, string Conexion)
        {
            var iResult = 0;
            eRepValuacionParams VP = new eRepValuacionParams();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", E.Id_Emp);
                Parametro("@Id_Cd", E.Id_Cd);
                Parametro("@Id_Op", E.Id_Op);
                Parametro("@tbNombreAtencion", E.NombreAtencion);
                Parametro("@tbRepresentanteClienteNombre", E.RepresentanteClienteNombre);
                Parametro("@tbNombreRik", E.NombreRik);
                Parametro("@tbDireccion1", E.Direccion1);
                Parametro("@tbDireccion2", E.Direccion2);
                Parametro("@Direccion_CP", E.CP);
                Parametro("@Direccion_NoExterior", E.NoExterior);
                Parametro("@Direccion_Telefono", E.Telefono);
                Parametro("@DiasCredito", E.DiasCredito);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_UpdateRepParams_Ver2", ref dr, Parametros.ToArray(), Valores.ToArray());
                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RowsAffected")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult;
        }


        //
        // JUN30-06 2020 RFH 
        // Actualiza campos correspondientes al reporte
        // 
        public int SpCrmOportunidades_UpdateRepParams_Ver3(eCRM_ValuacionCamposAdicionales_Ver2 E, string Conexion)
        {
            var iResult = 0;
            eRepValuacionParams VP = new eRepValuacionParams();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", E.Id_Emp);
                Parametro("@Id_Cd", E.Id_Cd);
                Parametro("@Id_Op", E.Id_Op);

                Parametro("@Key_NombreRik", E.Key_NombreRik);
                Parametro("@Key_RazonSocial", E.Key_RazonSocial);
                Parametro("@Key_Calle", E.Key_Calle);
                Parametro("@Key_No", E.Key_No);
                Parametro("@Key_CP", E.Key_CP);
                Parametro("@Key_Colonia", E.Key_Colonia);
                Parametro("@Key_Municipio", E.Key_Municipio);
                Parametro("@Key_Estado", E.Key_Estado);
                Parametro("@Key_Nomenclatura", E.Key_Nomenclatura);

                Parametro("@Cte_Nombre", E.Cte_Nombre);
                Parametro("@Cte_Calle", E.Cte_Calle);
                Parametro("@Cte_No", E.Cte_No);
                Parametro("@Cte_CP", E.Cte_CP);
                Parametro("@Cte_Colonia", E.Cte_Colonia);
                Parametro("@Cte_Municipio", E.Cte_Municipio);
                Parametro("@Cte_Estado", E.Cte_Estado);
                Parametro("@Cte_AtencionA", E.Cte_AtencionA);

                Parametro("@Cte_DiasCredito", E.Cte_DiasCredito);


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_UpdateRepParams_Ver3", ref dr, Parametros.ToArray(), Valores.ToArray());
                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RowsAffected")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }
            return iResult;
        }

        //

    }
}
