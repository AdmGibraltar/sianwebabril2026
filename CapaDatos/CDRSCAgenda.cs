using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CDRSCAgenda
    {
        public void ConsultaPedidoRastreo(AgendaRsc agenda, string Conexion, ref List<AgendaRsc> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                         "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@id_tu",
                                        "@id_ActividadGral",
                                        "@id_Actividad",
                                        "@FechaInicio",
                                        "@fechaFinal",
                                        "@Id_Cte"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.id_ActividadGral  == -1? (object)null :   agenda.id_ActividadGral,
                                        agenda.id_Actividad  == -1? (object)null :   agenda.id_Actividad,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal,
                                        agenda.id_cte  == 0? (object)null :   agenda.id_cte,
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaRSC", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    agenda.StartTime = (DateTime)dr.GetValue(dr.GetOrdinal("HoraInicio"));
                    agenda.EndTime = (DateTime)dr.GetValue(dr.GetOrdinal("HoraFinal"));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    string sujeto = dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreCliente")));
                    agenda.Subject = sujeto.Split('-')[1];
                    agenda.Location = sujeto.Split('-')[2];
                    agenda.Descripcion = sujeto.Split('-')[2];
                    agenda.label = dr.IsDBNull(dr.GetOrdinal("tipo")) ? "N" : (string)dr.GetValue(dr.GetOrdinal("tipo"));
                    agenda.estatus = dr.IsDBNull(dr.GetOrdinal("tipo")) ? "N" : (string)dr.GetValue(dr.GetOrdinal("tipo"));
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    agenda.FechaInicio = dr.IsDBNull(dr.GetOrdinal("HoraInicio")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraInicio")));
                    agenda.fechaFinal = dr.IsDBNull(dr.GetOrdinal("HoraFinal")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraFinal")));
                    agenda.inicioEjecucion = dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("InicioEjecucion")));
                    agenda.finalEjecucion = dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FinalEjecucion")));
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_motivo")));
                    agenda.ActividadGral = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_Actividad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_Actividad")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("DescActividad")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DescActividad")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreCliente")));
                    agenda.tipo = dr.IsDBNull(dr.GetOrdinal("tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("tipo")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("NombreUsuario")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreUsuario")));
                    agenda.Comentarios = dr.IsDBNull(dr.GetOrdinal("Comentarios")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Comentarios")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMotivo(int id_emp, int id_usu, int id_cte, ref List<RSCMotivo> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@idusu", "@Id_Cte" };
                object[] Valores = { id_emp, id_usu, id_cte };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRscMotivo_Combo", ref dr, Parametros, Valores);
                RSCMotivo Motivo;
                while (dr.Read())
                {
                    Motivo = new RSCMotivo();
                    Motivo.id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Motivo.descripción = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Lista.Add(Motivo);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSubMotivo(int id_emp, int idMotivo, int id_cte, int id_usu, ref List<RSCMotivo> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@idMotivo", "@Id_Cte", "@idusu" };
                object[] Valores = { id_emp, idMotivo, id_cte, id_usu };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRscSubMotivo_Combo", ref dr, Parametros, Valores);
                RSCMotivo Motivo;
                while (dr.Read())
                {
                    Motivo = new RSCMotivo();
                    Motivo.id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Motivo.descripción = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Lista.Add(Motivo);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAplicacion(int idSubMotivo, ref List<RSCMotivo> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@idSubMotivo" };
                object[] Valores = { 1, idSubMotivo };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRscAplicacion_Combo", ref dr, Parametros, Valores);
                RSCMotivo Motivo;
                while (dr.Read())
                {
                    Motivo = new RSCMotivo();
                    Motivo.id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Motivo.descripción = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Lista.Add(Motivo);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAgendaDetallada(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID" };
                object[] Valores = { Registro.Id_Emp, Registro.Id_Cd, Registro.ID };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaConsultaRSC", ref dr, Parametros, Valores);
                Agenda agenda;
                while (dr.Read())
                {
                    agenda = new Agenda();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    agenda.FechaInicio = dr.IsDBNull(dr.GetOrdinal("HoraInicio")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraInicio")));
                    agenda.fechaFinal = dr.IsDBNull(dr.GetOrdinal("HoraFinal")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraFinal")));
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_motivo")));
                    agenda.ActividadGral = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_submotivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_submotivo")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("subdescripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("subdescripcion")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreCliente")));
                    agenda.tipo = dr.IsDBNull(dr.GetOrdinal("tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("tipo")));
                    agenda.Comentarios = dr.IsDBNull(dr.GetOrdinal("Comentarios")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Comentarios")));
                    agenda.Id_Usu = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    agenda.id_tu = dr.IsDBNull(dr.GetOrdinal("Id_Tu")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tu")));
                    agenda.TipoUsuario = dr.IsDBNull(dr.GetOrdinal("Tu_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tu_Descripcion")));
                    agenda.id_agendaGrupal = dr.IsDBNull(dr.GetOrdinal("Id_ActividadGrupal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ActividadGrupal")));
                    if (!(dr.IsDBNull(dr.GetOrdinal("InicioEjecucion"))))
                    {
                        agenda.inicioEjecucion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("InicioEjecucion"))).ToString();
                    }
                    if (!(dr.IsDBNull(dr.GetOrdinal("FinalEjecucion"))))
                    {
                        agenda.finalEjecucion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FinalEjecucion"))).ToString();
                    }
                    agenda.verificador = 0;
                    Lista.Add(agenda);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPreAgendaDetallada(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID" };
                object[] Valores = { Registro.Id_Emp, Registro.Id_Cd, Registro.ID };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_PreAgendaConsulta", ref dr, Parametros, Valores);
                Agenda agenda;
                while (dr.Read())
                {
                    agenda = new Agenda();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    agenda.FechaInicio = dr.IsDBNull(dr.GetOrdinal("HoraInicio")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraInicio")));
                    agenda.fechaFinal = dr.IsDBNull(dr.GetOrdinal("HoraFinal")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraFinal")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    agenda.tipo = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo")));
                    agenda.Id_Usu = dr.IsDBNull(dr.GetOrdinal("id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_u")));
                    agenda.id_tu = dr.IsDBNull(dr.GetOrdinal("id_tu")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_tu")));
                    agenda.verificador = 0;
                    Lista.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarAgendaDetalladaLog(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@ID" };
                object[] Valores = { Registro.ID };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaConsultaLog", ref dr, Parametros, Valores);
                AgendaRsc agenda;
                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("Id_Log")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Log")));
                    Lista.Add(agenda);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {   "@Id_emp",
                                            "@Id_Cd",
                                            "@HoraInicio",
                                            "@HoraFinal",
                                            "@id_cte",
                                            "@id_ActividadGral",
                                            "@id_Acividad",
                                            "@Comentarios",
                                            "@Id_usuario",
                                            "@tipo"};
                object[] Valores = { 1,
                                    Registro.Id_Cd,
                                    Registro.FechaInicio,
                                    Registro.fechaFinal,
                                    Registro.id_cte,
                                    Registro.id_ActividadGral,
                                    Registro.id_Actividad,
                                    Registro.Comentarios,
                                    Registro.Id_Usu,
                                    Registro.tipo};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaInsertarRSC", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {     "@ID",
                                            "@Id_emp",
                                            "@Id_Cd",
                                            "@HoraInicio",
                                            "@HoraFinal",
                                            "@id_cte",
                                            "@id_ActividadGral",
                                            "@id_Acividad",
                                            "@Comentarios" };
                object[] Valores = { Registro.ID,
                                    Registro.Id_Emp,
                                    Registro.Id_Cd,
                                    Registro.FechaInicio,
                                    Registro.fechaFinal,
                                    Registro.id_cte,
                                    Registro.id_ActividadGral,
                                    Registro.id_Actividad,
                                    Registro.Comentarios };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaActualizarRSC", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaAgendaDetallada(AgendaRsc Agenda, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                int verificador = 0;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID", "@Comentario" };
                object[] Valores = { 1, Agenda.Id_Cd, Agenda.ID, Agenda.Comentarios };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaBajaRSC", ref verificador, Parametros, Valores);
                Agenda agenda;

                agenda = new Agenda();
                agenda.ID = verificador;
                Lista.Add(agenda);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaIniciarAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID_usuario" };
                object[] Valores = { Registro.Id_Emp, Registro.Id_Cd, Registro.Id_Usu };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_ConsultarActividadesIniciadas", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultarusuario(Agenda json, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cd", "@Id_Emp", "@Id_U", "@Id_tu" };
                object[] Valores = { json.Id_Cd,
                                     json.Id_Emp,
                                     json.Id_Usu == -1? (object)null : json.Id_Usu,
                                     json.id_tu  == -1? (object)null : json.id_tu};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuario_ComboCalendario", ref dr, Parametros, Valores);
                Agenda Usu;
                while (dr.Read())
                {
                    Usu = new Agenda();
                    Usu.Id_Usu = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    Usu.nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    Usu.id_tu = dr.IsDBNull(dr.GetOrdinal("Id_Tu")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tu")));
                    Usu.Descripcion = dr.IsDBNull(dr.GetOrdinal("Tu_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tu_Descripcion")));
                    Usu.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_rik")));

                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTipoUsuario(ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { };
                object[] Valores = { };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoUsuario_Combo", ref dr, Parametros, Valores);
                Agenda Usu;
                while (dr.Read())
                {
                    Usu = new Agenda();
                    Usu.id_tu = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Usu.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TipoUsuarioConsultar(Agenda lista, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_u" };
                object[] Valores = { lista.Id_Usu };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoUsuario_Consulta", ref dr, Parametros, Valores);
                Agenda Usu;
                while (dr.Read())
                {
                    Usu = new Agenda();
                    Usu.id_tu = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Usu.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgendaInicioActividades(AgendaRsc Agenda, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                int verificador = 0;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID", "@Fecha" };
                object[] Valores = { 1, Agenda.Id_Cd, Agenda.ID, Agenda.FechaInicio };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaInicioActividades", ref verificador, Parametros, Valores);
                Agenda agenda;

                agenda = new Agenda();
                agenda.verificador = verificador;
                Lista.Add(agenda);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgendaFinalizarActividades(AgendaRsc Agenda, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                int verificador = 0;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID", "@Fecha" };
                object[] Valores = { 1, Agenda.Id_Cd, Agenda.ID, Agenda.fechaFinal };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaFinalizarActividades", ref verificador, Parametros, Valores);
                Agenda agenda;

                agenda = new Agenda();
                agenda.ID = verificador;
                Lista.Add(agenda);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarACtividadInicioFin(AgendaRsc Agenda, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                Agenda registro;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID" };
                object[] Valores = { 1, Agenda.Id_Cd, Agenda.ID };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_ConsultarActividadesID", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registro = new Agenda();
                    registro.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    registro.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registro.Id_Usu = dr.IsDBNull(dr.GetOrdinal("id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_u")));
                    if (!dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")))
                    {
                        registro.FechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("InicioEjecucion")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")))
                    {
                        registro.fechaFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FinalEjecucion")));
                    }
                    registro.verificador = 0;
                    Lista.Add(registro);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarACtividadActivas(AgendaRsc Agenda, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                Agenda registro;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@ID_usuario", "@ID" };
                object[] Valores = { 1, Agenda.Id_Cd, Agenda.Id_Usu,
                                     Agenda.ID  == -1? (object)null : Agenda.ID};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_ConsultarActividadesIniciadas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registro = new Agenda();
                    registro.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    registro.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registro.Id_Usu = dr.IsDBNull(dr.GetOrdinal("id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_u")));
                    if (!dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")))
                    {
                        registro.FechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("InicioEjecucion")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")))
                    {
                        registro.fechaFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FinalEjecucion")));
                    }
                    registro.verificador = 0;
                    Lista.Add(registro);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMonitorActividadesGral(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@id_tu",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaMonitorActividades", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.id_tu = dr.IsDBNull(dr.GetOrdinal("Id_Tu")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tu")));
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_motivo")));
                    agenda.ActividadGral = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    agenda.TiempoMinutos = dr.IsDBNull(dr.GetOrdinal("TiempoMinutos")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TiempoMinutos")));
                    agenda.TiempoHora = CalcularTiempo(agenda.TiempoMinutos);
                    agenda.lugar = dr.IsDBNull(dr.GetOrdinal("Lugar")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Lugar")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarMonitorActividades(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@id_tu",
                                        "@Id_ActividadGral",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.id_ActividadGral,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaMonitorSubActividades", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_motivo")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_submotivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_submotivo")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("subDescripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("subDescripcion")));
                    agenda.TiempoMinutos = dr.IsDBNull(dr.GetOrdinal("TiempoMinutos")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TiempoMinutos")));
                    agenda.TiempoHora = CalcularTiempo(agenda.TiempoMinutos);
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarActividades(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@id_tu",
                                        "@Id_ActividadGral",
                                        "@Id_Actividad",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.id_ActividadGral,
                                        agenda.id_Actividad,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaMonitorActividadesIndividuales", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_motivo")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_submotivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_submotivo")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "Cliente Administrativo" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    agenda.inicioEjecucion = dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")) ? DateTime.Now.ToString() : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("InicioEjecucion"))).ToString();
                    agenda.finalEjecucion = dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")) ? DateTime.Now.ToString() : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FinalEjecucion"))).ToString();
                    agenda.TiempoMinutos = dr.IsDBNull(dr.GetOrdinal("TiempoMinutos")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TiempoMinutos")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    agenda.TiempoHora = CalcularTiempo(agenda.TiempoMinutos);
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaragendaGeolocalizacion(Geolocalizacion geo, ref List<Geolocalizacion> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id"
                                      };
                object[] Valores = {
                                        geo.id_emp,
                                        geo.ID_Cd,
                                        geo.Id_Agenda
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaGeolocalizacion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    geo = new Geolocalizacion();
                    geo.Id_Agenda = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    geo.Latitud = dr.IsDBNull(dr.GetOrdinal("Longitud")) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Longitud")));
                    geo.Longitud = dr.IsDBNull(dr.GetOrdinal("Latitud")) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Latitud")));
                    geo.Estatus = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Estatus")));
                    List.Add(geo);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMonitorAgenda(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@id_tu",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaActividades", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("agenda")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("agenda")));
                    agenda.Descripcion = dr.IsDBNull(dr.GetOrdinal("descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("descripcion")));
                    agenda.Cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cantidad")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMonitorAgendaDetalle(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@id_tu",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal",
                                        "@Cal_mes",
                                        "@Cal_Anio"
                };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal,
                                        agenda.mes == 0? (object)null :   agenda.mes,
                                        agenda.anio == 0? (object)null :   agenda.anio,
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaActividadesdetalle", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("agenda")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("agenda")));
                    agenda.Descripcion = dr.IsDBNull(dr.GetOrdinal("descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("descripcion")));
                    agenda.Cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cantidad")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void monitorActServicioValor(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_monitorActServicioValor", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("agenda")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("agenda")));
                    agenda.Descripcion = dr.IsDBNull(dr.GetOrdinal("descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("descripcion")));
                    agenda.Cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cantidad")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("actividad")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("actividad")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void monitorActServicioValordet(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_ActividadesServicioValor", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    agenda.FechaInicio = (DateTime)dr.GetValue(dr.GetOrdinal("HoraInicio"));
                    agenda.fechaFinal = (DateTime)dr.GetValue(dr.GetOrdinal("HoraFinal"));
                    agenda.Id_Usu = dr.IsDBNull(dr.GetOrdinal("Id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_u")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    agenda.inicioEjecucion = dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("InicioEjecucion")));
                    agenda.finalEjecucion = dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FinalEjecucion")));
                    agenda.fechaModificacion = dr.IsDBNull(dr.GetOrdinal("FechaModificacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacion"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacion")));
                    agenda.fechaEliminacion = dr.IsDBNull(dr.GetOrdinal("FechaEliminacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaEliminacion"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaEliminacion")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_SubMotivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_SubMotivo")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    List.Add(agenda);

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CargarPreagenda(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@id_tu",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal",
                                        "@Id_Cte"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal,
                                        agenda.id_cte == 0? (object)null :   agenda.id_cte,
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_PreAgendaActividades", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    agenda.tipo = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo")));
                    agenda.HoraInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraInicio")));
                    agenda.HoraFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("HoraFinal")));
                    agenda.Id_Usu = dr.IsDBNull(dr.GetOrdinal("id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_u")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void PreagendaBaja(AgendaRsc agenda, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@ID"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.ID
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_PreAgendaBaja", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPrecarga(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {    "@Id_emp",
                                            "@Id_Cd",
                                            "@id_ActividadGral",
                                            "@id_Acividad",
                                             "@id"};
                object[] Valores = { 1,
                                    Registro.Id_Cd,
                                    Registro.id_ActividadGral,
                                    Registro.id_Actividad,
                                   Registro.ID};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_PreAgendaActualizar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaAgendaCargaARchivo(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {   "@Id_emp",
                                            "@Id_Cd",
                                            "@HoraInicio",
                                            "@HoraFinal",
                                            "@id_cte",
                                            "@usuario"};
                object[] Valores = {Registro.Id_Emp,
                                    Registro.Id_Cd,
                                    Registro.HoraInicio,
                                    Registro.HoraFinal,
                                    Registro.id_cte,
                                    Registro.usuario };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaPReInsertarRSC", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTrackingActividad(AgendaRsc agenda, string Conexion, ref List<AgendaRsc> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@id_tu",
                                        "@Id_Usu",
                                        "@FechaInicio",
                                        "@fechaFinal",
                                        "@Cal_mes",
                                        "@Cal_Anio"
                                      };
                object[] Valores = {
                                        agenda.Id_Emp,
                                        agenda.Id_Cd,
                                        agenda.id_tu  == -1? (object)null :   agenda.id_tu,
                                        agenda.Id_Usu == -1? (object)null :   agenda.Id_Usu,
                                        agenda.FechaInicio,
                                        agenda.fechaFinal,
                                        agenda.mes == 0? (object)null :   agenda.mes,
                                        agenda.anio == 0? (object)null :   agenda.anio,
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_trackingActividades", ref dr, Parametros, Valores);

                DateTime hoy = DateTime.Now;
                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.ID = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    agenda.FechaInicio = (DateTime)dr.GetValue(dr.GetOrdinal("HoraInicio"));
                    agenda.fechaFinal = (DateTime)dr.GetValue(dr.GetOrdinal("HoraFinal"));
                    agenda.TipoUsuario = dr.IsDBNull(dr.GetOrdinal("Tu_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tu_Descripcion")));
                    agenda.Id_Usu = dr.IsDBNull(dr.GetOrdinal("Id_u")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_u")));
                    agenda.usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    agenda.inicioEjecucion = dr.IsDBNull(dr.GetOrdinal("InicioEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("InicioEjecucion")));
                    agenda.finalEjecucion = dr.IsDBNull(dr.GetOrdinal("FinalEjecucion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FinalEjecucion")));
                    agenda.fechaModificacion = dr.IsDBNull(dr.GetOrdinal("FechaModificacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacion"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacion")));
                    agenda.fechaModificacionFinal = dr.IsDBNull(dr.GetOrdinal("FechaModificacionfinal")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacionfinal"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaModificacionfinal")));
                    agenda.fechaEliminacion = dr.IsDBNull(dr.GetOrdinal("FechaEliminacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaEliminacion"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaEliminacion")));
                    agenda.Comentarios = dr.IsDBNull(dr.GetOrdinal("Comentarios")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Comentarios")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    agenda.TiempoMinutos = dr.IsDBNull(dr.GetOrdinal("tiempoInvertido")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tiempoInvertido")));

                    agenda.TipoActividad = dr.IsDBNull(dr.GetOrdinal("Lugar")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Lugar")));


                    if (agenda.fechaModificacionFinal != "")
                    {
                        agenda.FechaInicio = dr.IsDBNull(dr.GetOrdinal("FechaOriginal")) ? DateTime.Now : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaOriginal"))) == "01/01/1900 12:00:00 a. m." ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOriginal")));
                        agenda.fechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaOriginalFinal")) ? DateTime.Now : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaOriginalFinal"))) == "01/01/1900 12:00:00 a. m." ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOriginalFinal")));
                    }

                    if (agenda.inicioEjecucion == "" && agenda.finalEjecucion == "" && agenda.fechaModificacion == "" && agenda.fechaEliminacion == "" && agenda.fechaFinal >= hoy)
                    {
                        agenda.estatus = "En Tiempo";
                    }
                    if (agenda.inicioEjecucion == "" && agenda.finalEjecucion == "" && agenda.fechaModificacion == "" && agenda.fechaEliminacion == "" && agenda.fechaFinal < hoy)
                    {
                        agenda.estatus = "Vencidas";
                    }
                    if (agenda.inicioEjecucion != "" && agenda.finalEjecucion != "")
                    {
                        agenda.estatus = "Terminadas";
                    }
                    if (agenda.inicioEjecucion != "" && agenda.finalEjecucion == "")
                    {
                        agenda.estatus = "En Ejecución";
                    }
                    if (agenda.fechaModificacion != "" && agenda.fechaEliminacion == "" && agenda.inicioEjecucion == "")
                    {
                        agenda.estatus = "Reprogramadas";
                    }
                    if (agenda.fechaEliminacion != "")
                    {
                        agenda.estatus = "Bajas";
                    }
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spRepCumplimientoVI_Dinamico(eClenteBuscar_Params Pms, string Conexion, ref List<eClienteBuscar> lst)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Cd",
                    "@Anio",
                    "@Mes",
                    "@Id_Uen",
                    "@Id_Seg",
                    "@Id_Rik",
                    "@Id_Ter",
                    "@Id_Cte",
                    "@Tipo",
                    "@CampoOrden",
                    "@OrdenDir",
                    "@Debug"
                };

                object[] Valores = {
                    Pms.Id_Cd,
                    Pms.Anio,
                    Pms.Mes,
                    Pms.Id_Uen <= 0 ? (object)null : Pms.Id_Uen,
                    Pms.Id_Seg <= 0 ? (object)null : Pms.Id_Seg,
                    Pms.Id_Rik <= 0 ? (object)null : Pms.Id_Rik,
                    Pms.Id_Ter <= 0 ? (object)null : Pms.Id_Ter,
                    Pms.Id_Cte <= 0 ? (object)null : Pms.Id_Cte,
                    Pms.Tipo,
                    Pms.CampoOrden,
                    Pms.OrdenDir,
                    0
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCumplimientoVI_ver3", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eClienteBuscar obj = new eClienteBuscar();
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));

                    obj.MontoConsolidado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MontoConsolidado")));
                    obj.VtaMes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VtaMes")));
                    obj.VtaProm = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VtaProm")));
                    obj.VtaInst = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VtaInst")));
                    obj.MESVI = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MESVI")));
                    obj.PorcMes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcMes")));
                    obj.TRIMVI = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("TRIMVI")));
                    obj.PorcTRIM = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcTRIM")));
                    obj.VtaMesTot = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VtaMesTot")));
                    obj.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));

                    obj.TipoVTA = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoVTA")));
                    obj.TipoPTA = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoPTA")));
                    obj.tipoCliente = Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoCliente")));
                    obj.TipoCuenta = dr.IsDBNull(dr.GetOrdinal("TipoCuenta")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoCuenta")));

                    try
                    {
                        obj.AcysCount = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AcysCount")));
                    }
                    catch (Exception ax)
                    {
                        obj.AcysCount = 0;
                    }

                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
        }

        public void ConsultarClienteTipoCriterio(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@Id_Cd", "@id_cte" };
                object[] Valores = { Registro.Id_Emp, Registro.Id_Cd, Registro.id_cte };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_ClienteTipoCriterio", ref dr, Parametros, Valores);
                Agenda agenda;
                while (dr.Read())
                {
                    agenda = new Agenda();
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    agenda.tipo = dr.IsDBNull(dr.GetOrdinal("tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("tipo")));
                    agenda.verificador = 0;
                    Lista.Add(agenda);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaActHibridas(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@id_u",
                                      };
                object[] Valores = {
                                        1,
                                        agenda.Id_Usu
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCat_ActHibridasConsultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new AgendaRsc();
                    agenda.id_tu = (int)dr.GetValue(dr.GetOrdinal("Id_TU"));
                    agenda.TipoUsuario = dr.IsDBNull(dr.GetOrdinal("rol")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("rol")));
                    agenda.id_ActividadGral = dr.IsDBNull(dr.GetOrdinal("Id_motivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_motivo")));
                    agenda.ActividadGral = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    agenda.id_Actividad = dr.IsDBNull(dr.GetOrdinal("id_SubMotivo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_SubMotivo")));
                    agenda.Actividad = dr.IsDBNull(dr.GetOrdinal("DescActividad")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DescActividad")));
                    agenda.actividaCheck = dr.IsDBNull(dr.GetOrdinal("checked")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("checked")));
                    agenda.actividaChecked = agenda.actividaCheck == 0 ? false : true;
                    List.Add(agenda);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActHibridasBaja(AgendaRsc agenda, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@id_u",
                                      };
                object[] Valores = {
                                        agenda.Id_Usu
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCat_ActHibridasEliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActHibridasInsertar(AgendaRsc agenda, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@id_u",
                                        "@id_tu",
                                        "@id_motivo",
                                        "@id_submotivo",
                                        "@checked"
                                      };
                object[] Valores = {
                                        agenda.Id_Usu,
                                        agenda.id_tu,
                                        agenda.id_ActividadGral,
                                        agenda.id_Actividad,
                                        agenda.actividaCheck
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCat_ActHibridasInsertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VentaServicioAnual(VenEstadisticaVentas Ventas, ref List<VenEstadisticaVentas> List, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                VenEstadisticaVentas VentasLista;

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Anio",
                                         "@Territorio",
                                         "@Cliente",
                                         "@Producto",
                                         "@Tipo",
                                         "@NivelCliente",
                                         "@Nivelproducto",
                                         "@Reporte",
                                         "@Id_U",
                                         "@id_tu"};
                object[] Valores = { Ventas.id_emp,
                                     Ventas.Id_Cd,
                                    Ventas.Anio,
                                    Ventas.Territorio,
                                    Ventas.Cliente,
                                    Ventas.Producto,
                                    Ventas.Mostrar,
                                    Ventas.Nivel,
                                    Ventas.Nivel2,
                                    Ventas.Reporte,
                                    Ventas.id_usu == 0 ? (object)null : Ventas.id_usu,
                                    Ventas.id_tu == -1 ? (object)null : Ventas.id_tu};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasServAnuales", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    VentasLista = new VenEstadisticaVentas();

                    VentasLista.id_cte = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    VentasLista.nombre_Comercial = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "N/A" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    VentasLista.mes1 = dr.IsDBNull(dr.GetOrdinal("mes1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes1")));
                    VentasLista.mes2 = dr.IsDBNull(dr.GetOrdinal("mes2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes2")));
                    VentasLista.mes3 = dr.IsDBNull(dr.GetOrdinal("mes3")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes3")));
                    VentasLista.mes4 = dr.IsDBNull(dr.GetOrdinal("mes4")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes4")));
                    VentasLista.mes5 = dr.IsDBNull(dr.GetOrdinal("mes5")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes5")));
                    VentasLista.mes6 = dr.IsDBNull(dr.GetOrdinal("mes6")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes6")));
                    VentasLista.mes7 = dr.IsDBNull(dr.GetOrdinal("mes7")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes7")));
                    VentasLista.mes8 = dr.IsDBNull(dr.GetOrdinal("mes8")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes8")));
                    VentasLista.mes9 = dr.IsDBNull(dr.GetOrdinal("mes9")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes9")));
                    VentasLista.mes10 = dr.IsDBNull(dr.GetOrdinal("mes10")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes10")));
                    VentasLista.mes11 = dr.IsDBNull(dr.GetOrdinal("mes11")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes11")));
                    VentasLista.mes12 = dr.IsDBNull(dr.GetOrdinal("mes12")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("mes12")));
                    VentasLista.total = dr.IsDBNull(dr.GetOrdinal("total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("total")));
                    VentasLista.tipoUsuario = dr.IsDBNull(dr.GetOrdinal("TipoUsuario")) ? "N/A" : Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoUsuario")));

                    if (Ventas.id_tu != -1)
                    {
                        if (VentasLista.tipoUsuario == "")
                        {
                            continue;
                        }
                    }

                    if (Ventas.Reporte == 1 || Ventas.Reporte == 2 || Ventas.Reporte == 15)
                    {
                        VentasLista.id_rik = dr.IsDBNull(dr.GetOrdinal("Idrik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Idrik")));
                        VentasLista.nombre_rik = dr.IsDBNull(dr.GetOrdinal("Nombrerik")) ? "N/A" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombrerik")));

                    }

                    if (Ventas.Reporte == 9 || Ventas.Reporte == 10 || Ventas.Reporte == 19)
                    {
                        VentasLista.id_ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                        VentasLista.nombre_terr = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "N/A" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));

                        VentasLista.id_rik = dr.IsDBNull(dr.GetOrdinal("Idrik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Idrik")));
                        VentasLista.nombre_rik = dr.IsDBNull(dr.GetOrdinal("Nombrerik")) ? "N/A" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombrerik")));

                    }
                    List.Add(VentasLista);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string CalcularTiempo(Int32 Minutos)
        {
            if (Minutos != 0)
            {
                Int32 horas = (Minutos / 60);
                Int32 minutos = Minutos - (horas * 60);
                return horas.ToString() + ":" + minutos.ToString().PadLeft(2, '0');
            }
            else
            {
                return "0:00";
            }
        }
    }
}