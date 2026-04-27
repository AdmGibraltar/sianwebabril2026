using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ReporteCRM
    {
        public void ConsultaReporteGrafica(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_Grafica", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_GraficaCantidad", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaReporteGraficaMontoProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"
                };
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik != ""?      RegistroReporteCRM.Id_Rik : null,
                                       0 ,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal};


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_MontoProyecto", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaReporteExcelMontoProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FechaInicio",
                                         "@fechaFinal " };
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik != "-1"?      RegistroReporteCRM.Id_Rik : null,
                                      RegistroReporteCRM.TipoVenta ,
                                        RegistroReporteCRM.fechainicio,
                                        RegistroReporteCRM.fechafinal
                };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_documentos", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.id_proyect = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_op"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_op")));
                    RegistroReporteCRM.fechaCreacion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechacreacion")));
                    RegistroReporteCRM.uenDescripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("uen_descripcion"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("uen_descripcion")));
                    RegistroReporteCRM.FECHAcierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("cierre")));
                    RegistroReporteCRM.rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Usu"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));
                    RegistroReporteCRM.nombre_cliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cte_nomComercial"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("cte_nomComercial")));
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("u_nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("u_nombre")));

                    RegistroReporteCRM.Analisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    RegistroReporteCRM.Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    RegistroReporteCRM.NEgociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    RegistroReporteCRM.Cierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    RegistroReporteCRM.Cancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    RegistroReporteCRM.TipoVentaStr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TipoVenta"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoVenta")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaReporteGraficaCantidadProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta" ,
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik != ""?      RegistroReporteCRM.Id_Rik : null,
                                      0,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_cantidadProyecto", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaReporteGrafica(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_Grafica", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                        RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteCRM_GraficaCantidad", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void consultaCDI(int id_emp, int cdi, ref string nombre, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd"
                                        };
                object[] Valores = {   id_emp,
                                      cdi,

    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_consulta_CDI", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cd_nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("cd_nombre")));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultaCDI2(int id_emp, int cdi, ref string nombre, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd"
                                        };
                object[] Valores = {   id_emp,
                                      cdi,

    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_consulta_CDI2", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cd_nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("cd_nombre")));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaReporteExcelImpulsosQuimicos(ReporteCRMQuimicos RegistroReporteCRMQuimicos, ref List<ReporteCRMQuimicos> listaReporteCRMQuimicos,
    ref List<ReporteCRMQuimicos> listaCRMQuimicosxSucursal, ref List<ReporteCRMQuimicos> listaCRMQuimicosxSucursalxRIK, ref List<ReporteCRMQuimicos> listaData, string Conexion)
        {
            try
            {
                ReporteCRMQuimicos ItemRptCRMQuimico = new ReporteCRMQuimicos();
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@FechaInicio",
                                         "@FechaFinal",
                                         "@Id_Cds",
                                         "@Id_RIKs",
                                         "@TipoReporte",
                                         "@TipoProductos",
                                         "@Aplicaciones"};
                object[] Valores = {RegistroReporteCRMQuimicos.Id_Emp,
                    RegistroReporteCRMQuimicos.fechainicio,
                    RegistroReporteCRMQuimicos.fechafinal,
                    RegistroReporteCRMQuimicos.IdCdis,
                    /// RegistroReporteCRMQuimicos.IdRIKs != "-1"? RegistroReporteCRMQuimicos.IdRIKs: null,
                    RegistroReporteCRMQuimicos.IdRIKs,
                    RegistroReporteCRMQuimicos.TipoReporte,
                    RegistroReporteCRMQuimicos.strTipoProductos,
                    RegistroReporteCRMQuimicos.strAplicaciones
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteImpulsoQuimicosCentral", ref dr, Parametros, Valores);
                ///                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteImpulsoQuimicos", ref dr, Parametros, Valores);

                if (RegistroReporteCRMQuimicos.TipoReporte == 2)
                {
                    dr.NextResult();
                }

                while (dr.Read())
                {
                    /// IdAgrupador DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                    /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto

                    ItemRptCRMQuimico = new ReporteCRMQuimicos();
                    ItemRptCRMQuimico.IdAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdAgrupador"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdAgrupador")));
                    ItemRptCRMQuimico.strAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("DescAgrupador"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DescAgrupador")));


                    ItemRptCRMQuimico.MontoFinal_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TotalProyectos")));
                    ItemRptCRMQuimico.MontoFinal_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto")));

                    ItemRptCRMQuimico.ProyectosActuales_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosActuales")));
                    ItemRptCRMQuimico.ProyectosActuales_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto")));

                    ItemRptCRMQuimico.ProyectosNuevos_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosNuevo")));
                    ItemRptCRMQuimico.ProyectosNuevos_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto")));

                    ItemRptCRMQuimico.ProyectosCerrados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCerrados")));
                    ItemRptCRMQuimico.ProyectosCerrados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto")));

                    ItemRptCRMQuimico.ProyectosCancelados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCancelados")));
                    ItemRptCRMQuimico.ProyectosCancelados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto")));

                    /*
                    ItemRptCRMQuimico.rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Usu"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));
                    ItemRptCRMQuimico.nombre_cliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cte_nomComercial"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("cte_nomComercial")));
                    ItemRptCRMQuimico.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("u_nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("u_nombre")));
                    */

                    listaReporteCRMQuimicos.Add(ItemRptCRMQuimico);

                }

                dr.NextResult();

                // recordset de Sucursales
                dr.NextResult();
                /*
                if (RegistroReporteCRMQuimicos.TipoReporte == 1)
                {
                    dr.NextResult();
                }
                */

                while (dr.Read())
                {
                    ///  Id_Cd	CDI IdAgrupador DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                    /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto

                    ItemRptCRMQuimico = new ReporteCRMQuimicos();
                    ItemRptCRMQuimico.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ItemRptCRMQuimico.strCDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CDI"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("CDI")));

                    ItemRptCRMQuimico.IdAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdAgrupador"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdAgrupador")));
                    ItemRptCRMQuimico.strAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("DescAgrupador"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DescAgrupador")));


                    ItemRptCRMQuimico.MontoFinal_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TotalProyectos")));
                    ItemRptCRMQuimico.MontoFinal_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto")));

                    ItemRptCRMQuimico.ProyectosActuales_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosActuales")));
                    ItemRptCRMQuimico.ProyectosActuales_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto")));

                    ItemRptCRMQuimico.ProyectosNuevos_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosNuevo")));
                    ItemRptCRMQuimico.ProyectosNuevos_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto")));

                    ItemRptCRMQuimico.ProyectosCerrados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCerrados")));
                    ItemRptCRMQuimico.ProyectosCerrados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto")));

                    ItemRptCRMQuimico.ProyectosCancelados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCancelados")));
                    ItemRptCRMQuimico.ProyectosCancelados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto")));

                    listaCRMQuimicosxSucursal.Add(ItemRptCRMQuimico);
                }


                dr.NextResult();
                /// recordset de RIKS
                dr.NextResult();

                while (dr.Read())
                {
                    ///  Id_Cd	CDI IdRik Rik_Nombre IdAgrupador DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                    /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto

                    ItemRptCRMQuimico = new ReporteCRMQuimicos();
                    ItemRptCRMQuimico.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ItemRptCRMQuimico.strCDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CDI"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("CDI")));

                    ItemRptCRMQuimico.Id_RIK = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdRik"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdRik")));
                    ItemRptCRMQuimico.strRIK = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));

                    ItemRptCRMQuimico.IdAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdAgrupador"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdAgrupador")));
                    ItemRptCRMQuimico.strAgrupador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("DescAgrupador"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DescAgrupador")));

                    ItemRptCRMQuimico.MontoFinal_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TotalProyectos")));
                    ItemRptCRMQuimico.MontoFinal_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TotalProyectos_Monto")));

                    ItemRptCRMQuimico.ProyectosActuales_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosActuales")));
                    ItemRptCRMQuimico.ProyectosActuales_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosActuales_Monto")));

                    ItemRptCRMQuimico.ProyectosNuevos_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosNuevo")));
                    ItemRptCRMQuimico.ProyectosNuevos_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosNuevo_Monto")));

                    ItemRptCRMQuimico.ProyectosCerrados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCerrados")));
                    ItemRptCRMQuimico.ProyectosCerrados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCerrados_Monto")));

                    ItemRptCRMQuimico.ProyectosCancelados_Proy = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ProyectosCancelados")));
                    ItemRptCRMQuimico.ProyectosCancelados_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ProyectosCancelados_Monto")));

                    listaCRMQuimicosxSucursalxRIK.Add(ItemRptCRMQuimico);
                }
                dr.NextResult();

                if (RegistroReporteCRMQuimicos.TipoReporte == 1)
                {
                    dr.NextResult();
                }

                while (dr.Read())
                {
                    ItemRptCRMQuimico = new ReporteCRMQuimicos();

                    ItemRptCRMQuimico.Data_Id_Emp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emp"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ItemRptCRMQuimico.Data_Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ItemRptCRMQuimico.Data_IdProyecto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdProyecto"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdProyecto")));
                    ItemRptCRMQuimico.Data_Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Estatus"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));
                    ItemRptCRMQuimico.Data_Est_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Est_Descripcion"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Est_Descripcion")));
                    ItemRptCRMQuimico.Data_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mes"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    ItemRptCRMQuimico.Data_Año = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Año"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Año")));

                    ItemRptCRMQuimico.Data_FechaCreacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCreacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCreacion")));

                    ItemRptCRMQuimico.Data_Id_UEN = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_UEN"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UEN")));
                    ItemRptCRMQuimico.Data_Uen_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Uen_Descripcion"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Uen_Descripcion")));
                    ItemRptCRMQuimico.Data_Id_Apl = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Apl"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Apl")));
                    ItemRptCRMQuimico.Data_Apl_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Descripcion"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Apl_Descripcion")));
                    ItemRptCRMQuimico.Data_Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Seg"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    ItemRptCRMQuimico.Data_Seg_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Seg_Descripcion"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Seg_Descripcion")));
                    ItemRptCRMQuimico.Data_Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    ItemRptCRMQuimico.Data_Ter_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ter_Nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    ItemRptCRMQuimico.Data_IdRik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdRik"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdRik")));
                    ItemRptCRMQuimico.Data_Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    ItemRptCRMQuimico.Data_Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    ItemRptCRMQuimico.Data_cte_nomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cte_nomComercial"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("cte_nomComercial")));
                    ItemRptCRMQuimico.Data_IdProducto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IdProducto"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdProducto")));
                    ItemRptCRMQuimico.Data_TipoProducto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TipoProducto"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoProducto")));
                    ItemRptCRMQuimico.Data_PrecLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PrecLista"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecLista")));
                    ItemRptCRMQuimico.Data_COP_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("COP_Cantidad"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COP_Cantidad")));
                    ItemRptCRMQuimico.Data_TipoVenta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TipoVenta"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoVenta")));
                    ItemRptCRMQuimico.Data_FechaAnalisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaAnalisis"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaAnalisis")));
                    ItemRptCRMQuimico.Data_MontoAnalisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoAnalisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoAnalisis")));
                    ItemRptCRMQuimico.Data_FechaPresentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaPresentacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaPresentacion")));
                    ItemRptCRMQuimico.Data_MontoPresentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoPresentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoPresentacion")));
                    ItemRptCRMQuimico.Data_FechaNegociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaNegociacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaNegociacion")));
                    ItemRptCRMQuimico.Data_MontoNegociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoNegociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoNegociacion")));
                    ItemRptCRMQuimico.Data_FechaCierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCierre"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCierre")));
                    ItemRptCRMQuimico.Data_MontoCierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoCierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoCierre")));
                    ItemRptCRMQuimico.Data_FechaCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCancelacion")));
                    ItemRptCRMQuimico.Data_MontoCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoCancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoCancelacion")));
                    ItemRptCRMQuimico.Data_FechaModificacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaModificacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaModificacion")));
                    ItemRptCRMQuimico.Data_FechaCotizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCotizacion"))) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCotizacion")));
                    ItemRptCRMQuimico.Data_MontoProyecto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoProyecto"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoProyecto")));
                    ItemRptCRMQuimico.Data_Situacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Situacion"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Situacion")));

                    listaData.Add(ItemRptCRMQuimico);
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