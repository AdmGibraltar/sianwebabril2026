using System;
using System.Collections.Generic;

using System.Data.SqlClient;

using CapaEntidad;


namespace CapaDatos
{
    public class CD_Leads
    {

        public void ConsultaLeadsRik(
           int PageNumber, int PageSize,
           Leads c, string TextoBuscar,
           int Representante, int Lead_Activo,
           string Conexion, ref List<Leads> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@PageNumber", "@PageSize", "@Id_Emp", "@Id_Cd",
                                          "@ParametroBusqueda", "@Representante", "@Lead_Activo" };

                object[] Valores = { PageNumber, PageSize, c.Id_Emp, c.Id_Cd,
                                       TextoBuscar, Representante, Lead_Activo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapLeads_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    c = new Leads();

                    c.IdLeads = Convert.ToInt32(dr["IdLeads"]);
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NombreEmpresa = dr["NombreEmpresa"].ToString().Trim();
                    c.IdGiroEmpresa = Convert.ToInt32(dr["IdGiroEmpresa"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.GiroEmpresa = dr["GiroEmpresa"].ToString().Trim();
                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.FechaAlta = Convert.ToDateTime(dr["FechaAlta"]);
                    c.FechaAsignaRep = dr["FechaAsignaRep"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAsignaRep"]);
                    c.IdMedioComunicacion = Convert.ToInt32(dr["IdMedioComunicacion"]);
                    c.MedioComunicacion = dr["MedioComunicacion"].ToString().Trim();
                    c.Ciudad = dr["Ciudad"].ToString().Trim();
                    c.Comentarios = dr["Comentarios"].ToString().Trim();
                    c.Telefono = dr["Telefono"].ToString().Trim();
                    c.Correo = dr["Correo"].ToString().Trim();
                    c.ProductoInteres = dr["ProductoInteres"].ToString().Trim();
                    c.NombreContacto = dr["NombreContacto"].ToString().Trim();
                    c.Idrepresentante = dr["Idrepresentante"].ToString() == "" ? 0 : Convert.ToInt32(dr["Idrepresentante"]);
                    //JFCV Control de cambio colores 2 mzo 2021
                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.PresentarEstatus = dr["PresentarEstatus"].ToString().Trim();
                    c.ColorEstatus = dr["ColorEstatus"].ToString().Trim();
                    c.Color = dr["Color"].ToString().Trim();
                    c.HistorialLeads = dr["HistorialLeads"].ToString().Trim();

                    c.HistorialLeads = c.HistorialLeads.Replace("INICIO#", "<div class='row'><div class='col-sm-4'>");
                    c.HistorialLeads = c.HistorialLeads.Replace("DIV#", "</div><div class='col-sm-2'>");
                    c.HistorialLeads = c.HistorialLeads.Replace("FIN#", "</div></div>");
                    //if (Convert.ToBoolean(c.Estatus))
                    //{
                    //    c.EstatusStr = "Activo";
                    //}
                    //else
                    //{
                    //    territorio.EstatusStr = "Inactivo";
                    //}
                    List.Add(c);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //reporte para gráfica de Tiempo de respuesta ( barra de PIE ) 
        public void ReporteLeads_TiempoRespuesta(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
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


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteLeads_TiempoRespuesta", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Estandard";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempoestandard"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempoestandard")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Límite";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempolimite"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempolimite")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Fuera de Tiempo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("fueradetiempo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fueradetiempo")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteLeads_TiempoRespuestaBarra(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteTiempoEstandar, ref List<ReporteCRM> listaReporteTiempoLimite,
         ref List<ReporteCRM> listaReporteFuera, string Conexion)
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


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteLeads_TiempoRespuesta", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Estandard";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempoestandard"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempoestandard")));
                    listaReporteTiempoEstandar.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Límite";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempolimite"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempolimite")));
                    listaReporteTiempoLimite.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Fuera de Tiempo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("fueradetiempo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fueradetiempo")));
                    listaReporteFuera.Add(RegistroReporteCRM);


                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Consulta los nombres de los gerentes para mostrar en el reporte de tiempo de respuesta
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Conexion"></param>
        /// <param name="list"></param>
        public void ConsultaGerente(Usuario Usuario, string Conexion, ref List<Usuario> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_TU" };
                object[] Valores = { Usuario.Id_Emp, Usuario.Id_Cd, Usuario.Id_Id_U, Usuario.Id_TU };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpLEadsUsuariosCDI_Consulta", ref dr, Parametros, Valores);

                Usuario VarUsuario = default(Usuario);
                while (dr.Read())
                {
                    VarUsuario = new Usuario();
                    VarUsuario.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    VarUsuario.U_Nombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));

                    if (Usuario.Id_TU != 8)
                    {
                        VarUsuario.U_FNac = dr.IsDBNull(dr.GetOrdinal("U_FNac")) ? Convert.ToDateTime("01/01/0001") : (DateTime)dr.GetValue(dr.GetOrdinal("U_FNac"));
                        VarUsuario.U_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_Activo")));
                        VarUsuario.Id_TU = (int)dr.GetValue(dr.GetOrdinal("Id_TU"));
                        VarUsuario.U_VerTodo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_VerTodo")));
                        VarUsuario.U_MultiCentro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_MultiOfi")));
                        VarUsuario.Tu_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Tu_Descripcion"));
                        VarUsuario.U_ActivoStr = (string)dr.GetValue(dr.GetOrdinal("Activo_String"));
                        VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));
                        VarUsuario.Cu_User = (string)dr.GetValue(dr.GetOrdinal("Cu_User"));
                        VarUsuario.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                        VarUsuario.Id_Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_Id_U"));
                        VarUsuario.Ofi_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                        VarUsuario.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                        VarUsuario.U_SusCredito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_SusCredito")));
                        VarUsuario.U_DiasVencimiento = dr.IsDBNull(dr.GetOrdinal("U_DiasVencimiento")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("U_DiasVencimiento")));
                    }
                    list.Add(VarUsuario);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consulta los leads que se muestrán en la pantalla del gerente 
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <param name="c"></param>
        /// <param name="TextoBuscar"></param>
        /// <param name="Representante"></param>
        /// <param name="Lead_Activo"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        /// <param name="estatusBuscar"></param>
        /// <param name="filtrarFecha"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        public void ConsultaLeadsGerente(
           int PageNumber, int PageSize,
           Leads c, string TextoBuscar,
           int Representante, int Lead_Activo,
           string Conexion, ref List<Leads> List, int estatusBuscar, int filtrarFecha, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@PageNumber", "@PageSize", "@Id_Emp", "@Id_Cd",
                                          "@ParametroBusqueda", "@Representante", "@Lead_Activo",
                                    "@EstatusBuscar","@FiltrarFecha" , "@FechaInicial" ,"@FechaFinal"  };

                object[] Valores = { PageNumber, PageSize, c.Id_Emp, c.Id_Cd,
                                       TextoBuscar, Representante, Lead_Activo,
                                      estatusBuscar,filtrarFecha, fechaInicial,fechaFinal };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapLeads_ConsultaGerente", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    c = new Leads();

                    c.IdLeads = Convert.ToInt32(dr["IdLeads"]);
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NombreEmpresa = dr["NombreEmpresa"].ToString().Trim();
                    c.IdGiroEmpresa = Convert.ToInt32(dr["IdGiroEmpresa"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.GiroEmpresa = dr["GiroEmpresa"].ToString().Trim();
                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.FechaAlta = Convert.ToDateTime(dr["FechaAlta"]);
                    c.FechaAsignaRep = dr["FechaAsignaRep"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAsignaRep"]);
                    c.IdMedioComunicacion = Convert.ToInt32(dr["IdMedioComunicacion"]);
                    c.MedioComunicacion = dr["MedioComunicacion"].ToString().Trim();
                    c.Ciudad = dr["Ciudad"].ToString().Trim();
                    c.Comentarios = dr["Comentarios"].ToString().Trim();
                    c.Telefono = dr["Telefono"].ToString().Trim();
                    c.Correo = dr["Correo"].ToString().Trim();
                    c.ProductoInteres = dr["ProductoInteres"].ToString().Trim();
                    c.NombreContacto = dr["NombreContacto"].ToString().Trim();
                    c.Idrepresentante = dr["Idrepresentante"].ToString() == "" ? 0 : Convert.ToInt32(dr["Idrepresentante"]);
                    //JFCV Control de cambio colores 2 mzo 2021
                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.PresentarEstatus = dr["PresentarEstatus"].ToString().Trim();
                    c.ColorEstatus = dr["ColorEstatus"].ToString().Trim();
                    c.Color = dr["Color"].ToString().Trim();
                    c.HistorialLeads = dr["HistorialLeads"].ToString().Trim();

                    c.HistorialLeads = c.HistorialLeads.Replace("INICIO#", "<div class='row'><div class='col-sm-4'>");
                    c.HistorialLeads = c.HistorialLeads.Replace("DIV#", "</div><div class='col-sm-2'>");
                    c.HistorialLeads = c.HistorialLeads.Replace("FIN#", "</div></div>");
                    //if (Convert.ToBoolean(c.Estatus))
                    //{
                    //    c.EstatusStr = "Activo";
                    //}
                    //else
                    //{
                    //    territorio.EstatusStr = "Inactivo";
                    //}
                    List.Add(c);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaListaLeads(Leads leads, ref List<Leads> listLeads, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@IdLeads",
                                         "@Id_Cd",
                                        "@IdGiroEmpresa",
                                         "@IdMedioComunicacion",
                                         "@IdEstatus",
                                         "@TipoFiltro",
                                         "@Filtro",
                                         "@FechaInicial",
                                         "@FechaFinal" };
                object[] Valores = {
                                       leads.IdLeads,
                                       leads.Id_Cd,
                                       leads.IdGiroEmpresa,
                                       leads.IdMedioComunicacion,
                                       leads.Activo,
                                       leads.TipoFiltro,
                                       leads.Filtro,
                                       leads.FechaInicial,
                                       leads.FechaFinal
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_ConsultaLista_v2", ref dr, Parametros, Valores);

                Leads c;
                while (dr.Read())
                {
                    c = new Leads();
                    c.IdLeads = Convert.ToInt32(dr["IdLeads"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NombreEmpresa = dr["NombreEmpresa"].ToString().Trim();
                    c.NumeroEmpleados = dr["NumeroEmpleados"].ToString();
                    c.IdGiroEmpresa = Convert.ToInt32(dr["IdGiroEmpresa"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.GiroEmpresa = dr["GiroEmpresa"].ToString().Trim();
                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.FechaAlta = Convert.ToDateTime(dr["FechaAlta"]);
                    c.FechaAsignaRep = dr["FechaAsignaRep"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAsignaRep"]);
                    c.IdMedioComunicacion = Convert.ToInt32(dr["IdMedioComunicacion"]);
                    c.MedioComunicacion = dr["MedioComunicacion"].ToString().Trim();
                    c.Ciudad = dr["Ciudad"].ToString().Trim();
                    c.Estado = dr["Estado"].ToString().Trim();
                    c.Comentarios = dr["Comentarios"].ToString().Trim();
                    c.Telefono = dr["Telefono"].ToString().Trim();
                    c.Correo = dr["Correo"].ToString().Trim();
                    c.ProductoInteres = dr["ProductoInteres"].ToString().Trim();
                    c.NombreContacto = dr["NombreContacto"].ToString().Trim();
                    c.NomEstatus = dr["NomEstatus"].ToString().Trim();
                    c.MotivoCanceladoGerente = dr["MotivoCanceladoGerente"].ToString();
                    c.MotivoCanceladoRik = dr["MotivoCanceladoRik"].ToString();

                    listLeads.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarLeads(Leads leads, ref int Verificador, ref int Id_Leads, string Conexion)
        {
            //JFCV 
            try
            {

                //--"@IdRepresentante", "@FechaAsignaRep",  "@FechaAlta","@FechaUltMod", leads.FechaAsignaRep, leads.FechaAlta,--leads.FechaUltMod,

                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                      "@NombreEmpresa",
               "@IdGiroEmpresa",
               "@ProductoInteres",
               "@Id_Cd",

               "@IdUsuario",

               "@Activo",
               "@IdMedioComunicacion",
               "@Ciudad",
               "@Comentarios",
               "@Correo",
               "@Telefono",
               "@NombreContacto"};


                object[] Valores = {
                                      leads.NombreEmpresa,
                                      leads.IdGiroEmpresa,
                                      leads.ProductoInteres,
                                      leads.Id_Cd,
                                      leads.IdUsuario,

                                      leads.Activo,
                                      leads.IdMedioComunicacion   ,
                                      leads.Ciudad  ,
                                      leads.Comentarios  ,
                                      leads.Correo  ,
                                      leads.Telefono,
                                      leads.NombreContacto };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

                if (Verificador > 0)
                {
                    Id_Leads = Verificador;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaLeads(int Id_Leads, ref Leads leads, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@IdLeads" };
                object[] Valores = { Id_Leads };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {

                    leads.IdLeads = Convert.ToInt32(dr["IdLeads"]);
                    leads.NombreEmpresa = dr["NombreEmpresa"].ToString();
                    leads.IdGiroEmpresa = Convert.ToInt32(dr["IdGiroEmpresa"]);
                    leads.ProductoInteres = dr["ProductoInteres"].ToString();
                    leads.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    leads.FechaAlta = Convert.ToDateTime(dr["FechaAlta"]);
                    leads.FechaUltMod = Convert.ToDateTime(dr["FechaUltMod"]);
                    leads.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    leads.NomCDI = dr["NomCDI"].ToString();
                    //leads.IdRepresentante = Convert.ToInt32(dr["IdRepresentante"]);
                    leads.FechaAsignaRep = dr["FechaAsignaRep"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAsignaRep"]);
                    leads.Activo = Convert.ToInt32(dr["Activo"]);
                    leads.IdMedioComunicacion = Convert.ToInt32(dr["IdMedioComunicacion"]);
                    leads.Ciudad = dr["Ciudad"].ToString();
                    leads.Comentarios = dr["Comentarios"].ToString() == "" ? "" : Convert.ToString(dr["Comentarios"]);
                    leads.Telefono = dr["Telefono"].ToString();
                    leads.Correo = dr["Correo"].ToString();
                    leads.NombreContacto = dr["NombreContacto"].ToString();

                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ModificaLeads(Leads leads, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                //JFCV Convenio
                string[] Parametros = {
                                        "@IdLeads",
                                        "@NombreEmpresa",
                                        "@IdGiroEmpresa",
                                        "@ProductoInteres",
                                        "@Id_Cd",
                                        "@IdUsuario",
                                        "@IdMedioComunicacion",
                                        "@Ciudad",
                                        "@Comentarios",
                                        "@Correo",
                                        "@Telefono",
                                        "@NombreContacto" };

                object[] Valores = { leads.IdLeads ,
                                    leads.NombreEmpresa ,
                                    leads.IdGiroEmpresa,
                                    leads.ProductoInteres,
                                    leads.Id_Cd,
                                    leads.IdUsuario,
                                    leads.IdMedioComunicacion,
                                    leads.Ciudad   ,
                                    leads.Comentarios  ,
                                    leads.Correo  ,
                                    leads.Telefono  ,
                                    leads.NombreContacto
                                  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Modificar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int CancelaLeads(int idLead, int idEmp, int idusuario, int tipocancelacion, string motivocancelacion, string Conexion, ref int Verificador)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                //JFCV Convenio
                string[] Parametros = {
                                        "@IdLeads",
                                        "@Id_Emp",
                                        "@IdUsuario",
                                        "@IdTipoCancelacion",
                                        "@MotivoCancelacion" };

                object[] Valores = { idLead ,
                                    idEmp ,
                                   idusuario,
                                   tipocancelacion,
                                   motivocancelacion
                                  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Cancelar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
                return 1;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        /// <summary>
        /// Asocia el cliente a un Leads
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <param name="idTer"></param>
        /// <param name="idSeg"></param>
        /// <param name="vpo"></param>
        /// <param name="cadenaDeConexionEF"></param>
        public int asociarLeadAProspecto(int idEmp, int idCd, int idCte, int idRik, int idLead, int idusuario, string Conexion, ref int Verificador)
        {

            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                //JFCV Convenio
                string[] Parametros = {
                                        "@IdLeads",
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdCte",
                                        "@IdRik",
                                        "@IdUsuario" };

                object[] Valores = { idLead ,
                                    idEmp ,
                                   idCd,
                                   idCte,
                                   idRik,
                                   idusuario
                                  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Asociar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Asocia el cliente a un Leads
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <param name="idTer"></param>
        /// <param name="idSeg"></param>
        /// <param name="vpo"></param>
        /// <param name="cadenaDeConexionEF"></param>
        public int ActualizarAgente(int idEmp, int idCd, int idRik, int idLead, int idusuario, string Conexion, ref int Verificador)
        {

            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                //JFCV Convenio
                string[] Parametros = {
                                        "@IdLeads",
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdRik",
                                        "@IdUsuario" };

                object[] Valores = { idLead ,
                                    idEmp ,
                                   idCd,
                                   idRik,
                                   idusuario
                                  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_AsociarAgente", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void LlenaCombo(string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };

                object[] Valores = { };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    Lista.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaLeadsObservarTotales(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Leads nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Tipo",
                                         "@FechaInicial",
                                         "@fechaFinal",
                                         "@Id_Cd"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.TipoFiltro,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.FechaFinal,
                                       RegistroPresupuesto.Id_Cd
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsReporteComercial", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new Leads();
                    nuevoPresupuesto.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    nuevoPresupuesto.ValorCantidad = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaLeadsIntegrarResultados(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Leads nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Tipo",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.TipoFiltro,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsIntegrarResultados", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new Leads();
                    nuevoPresupuesto.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    if (RegistroPresupuesto.TipoFiltro == 1)
                    {
                        nuevoPresupuesto.ValorMonto = 0;
                        nuevoPresupuesto.ValorCantidad = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                    }
                    else
                    {
                        nuevoPresupuesto.ValorMonto = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                        nuevoPresupuesto.ValorCantidad = 0;
                    }

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarProyectosAsig(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CapaDatos.GenerarSqlCommand("SpLeadsConsultarProyectosAsig", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Gráfica de Integrar Leads 

        public void ConsultaIntegrarLeads(CatPresupuesto RegistroPresupuesto, int seleccion, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Seleccion",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       seleccion,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsIntegrar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    if (seleccion == 0)
                    {
                        nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                        nuevoPresupuesto.TotalPresupuesto = Convert.ToDecimal(nuevoPresupuesto.cantidad);
                        nuevoPresupuesto.venta = 0;
                    }
                    else
                    {
                        nuevoPresupuesto.cantidad = 0; //dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                        nuevoPresupuesto.venta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO"))) ? -1 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO")));
                        nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO")));
                    }



                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //reporte para gráfica de Efectividad ( barra de PIE ) 
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


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "En Desarrollo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "En Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelados";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelados")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGraficaEfectividadCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
        ref List<ReporteCRM> listaReporteCRMNegociacion, string Conexion)
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
                                         "@FECHA_FINAL",
                                         "@Totalizar"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal,
                                       RegistroReporteCRM.id_proyect
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelados")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaReporteGraficaEfectividadMonto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
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


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLeads_ReporteEfectividadCentral_Monto", ref dr, Parametros, Valores);
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


        //esta función es para la gráfica de barras de efectividad por cantidad 
        // y si quiere totalizar lleva 1 
        public void ConsultaReporteEfectividadGrafBarrasCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
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
                                         "@FECHA_FINAL",
                                         "@Totalizar"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal,
                                       RegistroReporteCRM.id_proyect  //Me regresa totalizado cuando es 1
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);

                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();


                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cancelados")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);
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