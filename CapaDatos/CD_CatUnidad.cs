using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatUnidad
    {

        // ENE22-2020 RFH Compras Locales 

        public List<CapaEntidad.eUnidadMedidaSat> spSATUnidadesMedida(string CveUnida, string Conexion)
        {
            List<CapaEntidad.eUnidadMedidaSat> Lst = new List<CapaEntidad.eUnidadMedidaSat>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@CveUnida" };
                object[] Valores = { CveUnida };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSATUnidadesMedida", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eUnidadMedidaSat obj = new CapaEntidad.eUnidadMedidaSat();

                    obj.CveUnidad = dr.GetValue(dr.GetOrdinal("Cve")).ToString();
                    obj.DescUnidad = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }
            return Lst;
        }

        public List<CapaEntidad.eListaGenerica> spCatCL_UnidadMedida(int IdUnidad, int Id1, int Id2, string Conexion)
        {
            List<CapaEntidad.eListaGenerica> Lst = new List<CapaEntidad.eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { Id1, Id2 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCL_UnidadMedida", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    //obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.IdCodigo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }
            return Lst;
        }

        public List<CapaEntidad.eListaGenerica> spLlenarComboUnidades(int IdUnidad, int Id1, int Id2, string Conexion)
        {
            List<CapaEntidad.eListaGenerica> Lst = new List<CapaEntidad.eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { Id1, Id2 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    //obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.IdCodigo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        public void ConsultaUnidad(int Id_Emp, string Conexion, ref List<Unidad> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Consulta", ref dr, Parametros, Valores);

                Unidad unidad;
                while (dr.Read())
                {
                    unidad = new Unidad();
                    unidad.Id = dr.GetValue(dr.GetOrdinal("Id_Uni")).ToString();
                    unidad.Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();
                    unidad.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Uni_Tipo")));
                    unidad.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Uni_Activo")));
                    if (Convert.ToBoolean(unidad.Estatus))
                    {
                        unidad.EstatusStr = "Activo";
                    }
                    else
                    {
                        unidad.EstatusStr = "Inactivo";
                    }
                    List.Add(unidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Uni",
                                        "@Id_Emp",
                                        "@Uni_Descripcion",
                                        "@Uni_Tipo",
                                        "@Uni_Activo"
                                      };
                object[] Valores = {
                                        unidad.Id,
                                        unidad.Id_Emp,
                                        unidad.Descripcion,
                                        unidad.Tipo,
                                        unidad.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Uni",

                                        "@Id_Emp",
                                        "@Uni_Descripcion",
                                        "@Uni_Tipo",
                                        "@Uni_Activo"
                                      };
                object[] Valores = {
                                        unidad.Id,

                                        unidad.Id_Emp,
                                        unidad.Descripcion,
                                        unidad.Tipo,
                                        unidad.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}