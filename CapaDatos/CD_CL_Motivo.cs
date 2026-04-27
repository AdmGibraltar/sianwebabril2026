using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Globalization;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CL_Motivo
    {
        // ENE20-2020 RFH - Motivo Compra local

        public List<eCL_SATProductosYServicios> spSATProductosYServicios(string CveProdServ, string Conexion)
        {
            List<eCL_SATProductosYServicios> Lst = new List<eCL_SATProductosYServicios>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@CveProdServ"
                                      };
                object[] Valores = {
                                            CveProdServ
                                       };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSATProductosYServicios_ver2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eCL_SATProductosYServicios obj = new CapaEntidad.eCL_SATProductosYServicios();
                    obj.CveProdServ = dr.GetValue(dr.GetOrdinal("CveProdServ")).ToString();
                    obj.DescProdServ = dr.GetValue(dr.GetOrdinal("DescProdServ")).ToString();
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

        // FEB10-2020 Nueva Semilla
        public string SelCL_CodigoHomologado_Maximo_Consulta(int Id_Prov, string Conexion)
        {
            string sProducto = "";
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Prov" };
                object[] Valores = { Id_Prov };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CodigoHomologado_Maximo_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    sProducto = dr.GetValue(dr.GetOrdinal("Producto")).ToString();
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                sProducto = "-1";
            }
            return sProducto;
        }


        // ENE20-2020 RFH - Motivo Compra local

        public List<eCL_Motivo> SelCL_Motivo(int Id_MotivoCL, string Conexion)
        {
            List<eCL_Motivo> Lst = new List<eCL_Motivo>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id1",
                                        "@Id2"
                                      };
                object[] Valores = {
                                            0,
                                            0
                                       };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCompraLocal_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eCL_Motivo obj = new CapaEntidad.eCL_Motivo();

                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    obj.Id_MotivoCL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_MotivoCL")));
                    obj.Desc_MotivoCL = dr.GetValue(dr.GetOrdinal("Desc_MotivoCL")).ToString();
                    obj.PorcentajeAAA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcentajeAAA")));
                    obj.Aplica = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Aplica")));

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


        // ENE20-2020 RFH - Causa desabasto 

        public List<eListaGenerica> SelCL_CausaDesabasto(int Id_Causa, string Conexion)
        {
            List<eListaGenerica> Lst = new List<eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Causa" };
                object[] Valores = { Id_Causa };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalCausaDesabasto_Combo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    //obj.Id_MotivoCL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_MotivoCL")));
                    //obj.Desc_MotivoCL = dr.GetValue(dr.GetOrdinal("Desc_MotivoCL")).ToString();
                    //obj.PorcentajeAAA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcentajeAAA")));
                    //obj.Aplica = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Aplica")));
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

        // ENE20-2020 RFH - Causa desabasto 

        public List<eListaGenerica> SelCL_ListadoTipoProducto(int Id1, int Id2, string Conexion)
        {
            List<eListaGenerica> Lst = new List<eListaGenerica>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { Id1, Id2 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoProducto_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();

                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    //obj.Id_MotivoCL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_MotivoCL")));
                    //obj.Desc_MotivoCL = dr.GetValue(dr.GetOrdinal("Desc_MotivoCL")).ToString();
                    //obj.PorcentajeAAA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcentajeAAA")));
                    //obj.Aplica = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Aplica")));

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

        // ENE20-2020 RFH - Causa desabasto 
        // 12Abr2022 RFH
        public List<eListaGenerica> SelCL_ListadoProductoFamiliaCte(int Id1, int Id2, string Conexion)
        {
            List<eListaGenerica> Lst = new List<eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { Id1, Id2 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Combo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    //obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdAplicacion")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
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

        // ENE20-2020 RFH - Causa desabasto 

        public List<eListaGenerica> SelCL_ListadoProductoSubFamilia(int Id1, int Id2, int Id3, string Conexion)
        {
            List<eListaGenerica> Lst = new List<eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { Id1, Id2, Id3 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAppSubFam_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
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