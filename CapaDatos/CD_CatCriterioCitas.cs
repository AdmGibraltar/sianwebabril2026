using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCriterioCitas
    {
        public void ConsultaCriteriosCita(CriterioCita criterioCita, string Conexion, ref List<CriterioCita> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Todos", ref dr);


                while (dr.Read())
                {
                    /*
        Id_CriterioCita,
		Id_Cliente,
        Cliente
		Id_Frecuencia,
		Frecuencia,
		Id_TipoVisita,
		TipoVisita,
		Id_RSC,
		U_Nombre */
                    criterioCita = new CriterioCita();
                    criterioCita.Id_CriterioCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CriterioCita")));
                    criterioCita.Id_Cliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cliente")));
                    criterioCita.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    criterioCita.Id_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frecuencia")));
                    criterioCita.Frecuencia = dr.GetValue(dr.GetOrdinal("Frecuencia")).ToString();
                    criterioCita.Id_TipoVisita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoVisita")));
                    criterioCita.TipoVisita = dr.GetValue(dr.GetOrdinal("TipoVisita")).ToString();
                    criterioCita.Id_RSC = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RSC")));
                    criterioCita.RSC = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    criterioCita.FechaInicial = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicial")).ToString());

                    List.Add(criterioCita);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPrerequisitos(PreRequisitos Prereq, string Conexion, ref List<PreRequisitos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDiasInhabiles_Todos", ref dr);
                while (dr.Read())
                {
                    /*
Id_Cd
Id_PreRequi
PreRequisito
Activo */
                    Prereq = new PreRequisitos();
                    Prereq.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Prereq.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Prereq.Id_PreRequi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PreRequi")));
                    Prereq.PreRequisito = dr.GetValue(dr.GetOrdinal("PreRequisito")).ToString();

                    List.Add(Prereq);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaPrerequisitos(int Prereq, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_PreRequi", 
                                      };
                object[] Valores = { 
                                        Prereq
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPreRequisitos_Eliminar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_PreRequi"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaPrerequisitos(PreRequisitos Prereq, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@PreRequisito", 
                                      };
                object[] Valores = { 
                                       Prereq.Id_Cd,
                                       Prereq.PreRequisito
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPreRequisitos_Nuevo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_PreRequi"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVISucursal(Embudo VI, string Conexion, ref List<Embudo> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd"
                                      };
                object[] Valores = { 
                                       VI.Id_Emp,
                                       VI.Id_Cd
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentaInstaladaSucursal", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /*
                    * IdCD
                    * Sucursal
                    * MontoACyS
                    * MontoPromedio
                    * VentaMes
                    * CumplimientoVIMes
                    * CumplimientoVentaPromedioMes
                    */
                    VI = new Embudo();
                    //  ElDia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //  ElDia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    VI.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    VI.RowDesc = dr.GetValue(dr.GetOrdinal("CD")).ToString();
                    VI.VtaIn = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoACyS")));
                    VI.VtaRe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaMes")));
                    VI.VtaPro = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoPromedio")));
                    VI.Por100VI = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CumplimientoVIMes")));
                    VI.Por100Pro = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CumplimientoVentaPromedioMes")));

                    List.Add(VI);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaClientesDetalle(Embudo VI, string Conexion, ref List<Embudo> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Tipo",
                                        "@Seccion",
                                      };
                object[] Valores = { 
                                       VI.Id_Emp,
                                       VI.Id_Cd,
                                       VI.Ordern,
                                       VI.RowDesc
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepEmbudoClientesDetalle", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /*
                     * Id_Cte
                     * RowDesc
                     * VtaIn

                    */
                    VI = new Embudo();
                    //  ElDia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //  ElDia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    VI.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    VI.RowDesc = dr.GetValue(dr.GetOrdinal("RowDesc")).ToString();
                    VI.VtaIn = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VtaIn")));
                    List.Add(VI);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVI(Embudo VI, string Conexion, ref List<Embudo> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@RSC",
                                      };
                object[] Valores = { 
                                       VI.Id_Emp,
                                       VI.Id_Cd,
                                       VI.RSC
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentaInstalada", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /*
                    * IdRSC
                    * RSC
                    * MontoACyS
                    * MontoPromedio
                    * VentaMes
                    * CumplimientoVIMes
                    * CumplimientoVentaPromedioMes
                    */
                    VI = new Embudo();
                    //  ElDia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //  ElDia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    VI.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Num")));
                    VI.RowDesc = dr.GetValue(dr.GetOrdinal("RSC")).ToString();
                    VI.VtaIn = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoACyS")));
                    VI.VtaRe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaMes")));
                    VI.VtaPro = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoPromedio")));
                    VI.Por100VI = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CumplimientoVIMes")));
                    VI.Por100Pro = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CumplimientoVentaPromedioMes")));

                    List.Add(VI);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmbudo(Embudo ElDia, string Conexion, ref List<Embudo> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                      };
                object[] Valores = { 
                                       ElDia.Id_Emp,
                                       ElDia.Id_Cd
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepEmbudoClientes", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /*
Id_Cd
Ordern
RowDesc
A
B
C
D
Activo */
                    ElDia = new Embudo();
                    //  ElDia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //  ElDia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ElDia.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ordern")));
                    ElDia.RowDesc = dr.GetValue(dr.GetOrdinal("RowDesc")).ToString();
                    ElDia.A = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("A")));
                    ElDia.B = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("B")));
                    ElDia.C = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("C")));
                    //  ElDia.D = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("D")));

                    List.Add(ElDia);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteHistorialPedido(string Conexion, ref List<FlujoCitas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos", ref dr);
                while (dr.Read())
                {
                    FlujoCitas fluj = new FlujoCitas();
                    fluj.TD = dr.GetValue(dr.GetOrdinal("TD")).ToString();
                    List.Add(fluj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaFlujoTodos(FlujoCitas fluj, string Conexion, ref List<FlujoCitas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFlujoCitas_Todos", ref dr);
                while (dr.Read())
                {
                    /* 
                     * Id_Cd
                     * Id_TipoVisitaBase
                     * TipoVisita
                     * StrAlternancia
                     * */
                    fluj = new FlujoCitas();
                    //  fluj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    fluj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    fluj.Id_TipoVisitaBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoVisitaBase")));
                    fluj.TipoVisita = dr.GetValue(dr.GetOrdinal("TipoVisita")).ToString();
                    fluj.StrAlternancia = dr.IsDBNull(dr.GetOrdinal("StrAlternancia")) ? "" : dr.GetValue(dr.GetOrdinal("StrAlternancia")).ToString();

                    List.Add(fluj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPreRequisitos(PreRequisitos ElPre, string Conexion, ref List<PreRequisitos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPreRequisitos_Todos", ref dr);
                while (dr.Read())
                {
                    /*
                     * Id_Cd
                     * Id_PreRequi
                     * PreRequisito
                     * */
                    ElPre = new PreRequisitos();
                    ElPre.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ElPre.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ElPre.Id_PreRequi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PreRequi")));
                    ElPre.PreRequisito = dr.GetValue(dr.GetOrdinal("PreRequisito")).ToString();

                    List.Add(ElPre);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaFlujo(FlujoCitas Flujoo, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd",
                                        "@Id_TipoVisitaBase",
                                        "@strFlujo",
                                      };
                object[] Valores = { 
                                        Flujoo.Id_Cd,
                                        Flujoo.Id_TipoVisitaBase,
                                        Flujoo.StrAlternancia
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFlujoCitas_Nuevo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_TipoVisitaBase"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaPreRequisitos(PreRequisitos Prere, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@PreRequisito",
                                      };
                object[] Valores = { 
                                        Prere.PreRequisito
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPreRequisitos_Nuevo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_PreRequi"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaPreRequisitos(int IdPreRe, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_PreRequi", 
                                      };
                object[] Valores = { 
                                        IdPreRe
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPreRequisitos_Eliminar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_PreRequi"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDiasInhabilesAño(DiasInhabiles ElDia, int Anio, int mes, string Conexion, ref List<DiasInhabiles> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Anio",
                                        "@Mes",
                                      };
                object[] Valores = { 
                                        Anio,
                                        mes
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDiasInhabiles_Todos", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /*
Id_Cd
IdDiaInhabil
Año
Secuencia
Fecha
Activo */
                    ElDia = new DiasInhabiles();
                    ElDia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ElDia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ElDia.IdDiaInhabil = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdDiaInhabil")));
                    ElDia.Año = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Año")));
                    ElDia.Secuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    ElDia.Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")).ToString());

                    List.Add(ElDia);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaDiasInhabiles(int ElDia, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@IdDiaInhabil", 
                                      };
                object[] Valores = { 
                                        ElDia
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDiasInhabiles_Eliminar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("IdDiaInhabil"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaDiasInhabiles(DiasInhabiles ElDia, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Año", 
                                        "@Fecha",
                                      };
                object[] Valores = { 
                                        ElDia.Id_Cd
                                        ,ElDia.Año
                                        ,ElDia.Fecha
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDiasInhabiles_Nuevo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("IdDiaInhabil"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaFrecuenciaCita(FrecuenciaCliente LaFrecuencia, string Conexion, ref List<FrecuenciaCliente> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spFrecuencia_Todos", ref dr);
                while (dr.Read())
                {
                    /*
Id_Cd
Id_Frecuencia
Frecuencia
TipoIntervalo
Intervalo
Activo */
                    LaFrecuencia = new FrecuenciaCliente();
                    LaFrecuencia.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    LaFrecuencia.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    LaFrecuencia.Id_FrecuenciaCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frecuencia")));
                    LaFrecuencia.Frecuencia = dr.GetValue(dr.GetOrdinal("Frecuencia")).ToString();
                    LaFrecuencia.TipoIntervalo = dr.GetValue(dr.GetOrdinal("TipoIntervalo")).ToString();
                    LaFrecuencia.IntTipoIntervalo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IntTipoIntervalo")).ToString());
                    LaFrecuencia.Intervalo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Intervalo")).ToString());

                    //Activida.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    List.Add(LaFrecuencia);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaFrecuenciaCita(FrecuenciaCliente FReq, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Id_Frecuencia", 
                                        "@Frecuencia", 
                                        "@TipoIntervalo", 
                                        "@Intervalo",
                                      };
                object[] Valores = { 
                                        FReq.Id_Cd
                                        ,FReq.Id_FrecuenciaCliente
                                        ,FReq.Frecuencia
                                        ,FReq.IntTipoIntervalo
                                        ,FReq.Intervalo
                                   };
                SqlDataReader dr = null;
                string spsss = "";
                if (FReq.Id_FrecuenciaCliente == 0)
                {
                    spsss = "spFrecuencia_Nuevo";
                }
                else
                {
                    spsss = "spFrecuencia_Cambio";
                }
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(spsss, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_Frecuencia"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaActividadCita(ActividadCita LActividadCita, string Conexion, ref List<ActividadCita> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTipoActividad_Todos", ref dr);


                while (dr.Read())
                {
                    /*
Id_Cd
Id_TipoVisita
TipoVisita
Fit
ColorFondo
Icono
Activo */
                    LActividadCita = new ActividadCita();
                    LActividadCita.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    LActividadCita.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    LActividadCita.Id_TipoVisita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoVisita")));
                    LActividadCita.TipoVisita = dr.GetValue(dr.GetOrdinal("TipoVisita")).ToString();
                    LActividadCita.Complemento = dr.GetValue(dr.GetOrdinal("Complemento")).ToString();
                    LActividadCita.ColorFondo = dr.GetValue(dr.GetOrdinal("ColorFondo")).ToString();
                    LActividadCita.Icono = dr.GetValue(dr.GetOrdinal("Icono")).ToString();

                    //Activida.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    List.Add(LActividadCita);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarActividadCita(ActividadCita Acti, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Id_TipoVisita", 
                                        "@TipoVisita", 
                                        "@Complemento", 
                                        "@ColorFondo",
                                        "@Icono",
                                      };
                object[] Valores = { 
                                        Acti.Id_Cd
                                        ,Acti.Id_TipoVisita
                                        ,Acti.TipoVisita
                                        ,Acti.Complemento
                                        ,Acti.ColorFondo
                                        ,Acti.Icono
                                   };
                SqlDataReader dr = null;
                string spsss = "";
                if (Acti.Id_TipoVisita == 0)
                {
                    spsss = "spTipoActividad_Nuevo";
                }
                else
                {
                    spsss = "spTipoActividad_Cambio";
                }
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(spsss, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_TipoVisita"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaCategoriasCita(CategoriaCliente CatCliente, string Conexion, ref List<CategoriaCliente> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCategoriasCliente_Todos", ref dr);


                while (dr.Read())
                {
                    /*
Id_Emp
Id_Cd
Id_CategoriaCliente
DescCategoria
DescCategoriaCliente
RangoDesde
RangoHasta
TieneDesde
TieneHasta
Activo */
                    CatCliente = new CategoriaCliente();
                    CatCliente.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    CatCliente.Id_CategoriaCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CategoriaCliente")));
                    CatCliente.CategoCliente = dr.GetValue(dr.GetOrdinal("DescCategoria")).ToString();
                    CatCliente.DescCategoriaCliente = dr.GetValue(dr.GetOrdinal("DescCategoriaCliente")).ToString();
                    CatCliente.RangoDesde = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RangoDesde")));
                    CatCliente.RangoHasta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RangoHasta")));

                    CatCliente.TieneDesde = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TieneDesde")));
                    CatCliente.TieneHasta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TieneHasta")));

                    CatCliente.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    List.Add(CatCliente);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCategoriaCliente(CategoriaCliente Cate, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Id_RangoTipoVisita", 
                                        "@DescCategoria", 
                                        "@DescCategoriaCliente", 
                                        "@RangoDesde",
                                        "@RangoHasta",
                                        "@TieneDesde",
                                        "@TieneHasta",
                                      };
                object[] Valores = { 
                                        Cate.Id_Cd
                                        ,Cate.Id_CategoriaCliente
                                        ,Cate.CategoCliente
                                        ,Cate.DescCategoriaCliente
                                        ,Cate.RangoDesde
                                        ,Cate.RangoHasta
                                        ,Cate.TieneDesde
                                        ,Cate.TieneHasta
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCategoriaCliente_Cambio", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_CategoriaCliente"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cliente", 
                                        "@Id_Frecuencia", 
                                        "@Id_TipoVisita", 
                                        "@Id_RSC",
                                        "@TienePreRequi",
                                      };
                object[] Valores = { 
                                        Cita.Id_Cliente
                                        ,Cita.Id_Frecuencia
                                        ,Cita.Id_TipoVisita
                                        ,Cita.Id_RSC
                                        ,Cita.TienePreRequi
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Alta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_CriterioCita"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraPaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Pagina",
                                      };
                object[] Valores = { 
                                        1
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtienePaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdSecuencia"))) == 0)
                    {
                        PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("txt"))) + "<div >" + Environment.NewLine;
                    }
                    else
                    {
                        PagHTML = PagHTML + Convert.ToString(dr.GetValue(dr.GetOrdinal("txt"))) + Environment.NewLine;
                    }
                }

                PagHTML = PagHTML + "</div>";

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignaPaginaAyudaPorId(int IdOpcion, string Conexion, string PagHTML, ref int Ret)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@idPagina",
                                        "@pagHTML"
                                      };
                object[] Valores = { 
                                        IdOpcion,
                                        PagHTML
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAsignaPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Ret = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grabo")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtienePaginaAyudaPorId(string IdPagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@idPagina",
                                      };
                object[] Valores = { 
                                        IdPagASPX
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPaginaAyudaId", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("hlp")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtienePaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@aspx",
                                      };
                object[] Valores = { 
                                        PagASPX
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("hlp")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneModulo(string PagHTML, string Conexion, ref string Modulo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@html",
                                      };
                object[] Valores = { 
                                        PagHTML
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spModuloPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Modulo = Convert.ToString(dr.GetValue(dr.GetOrdinal("modulo")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EsDiaFestivo(string Conexion, string Fecha, ref int resul)//spEsDiaFestivo 
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Fecha",
                                      };
                object[] Valores = { 
                                        Fecha
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEsDiaFestivo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    resul = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsDia")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListadoPrerequisitosCita_Todos(string Conexion, string sp, string cita, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        cita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    list.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListadoPrerequisitos_Todos(string Conexion, string sp, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    list.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cliente", 
                                        "@Id_Frecuencia", 
                                        "@Id_TipoVisita", 
                                        "@Id_RSC",
                                        "@TienePreRequi",
                                      };
                object[] Valores = { 
                                        Cita.Id_Cliente
                                        ,Cita.Id_Frecuencia
                                        ,Cita.Id_TipoVisita
                                        ,Cita.Id_RSC
                                        ,Cita.TienePreRequi
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Alta", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarFechaCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        //  "@Id_CriterioCita", 
                                        "@Id_Cliente", 
                                        "@FechaInicial", 
                                        "@Duracion", 
                                        "@UserID",
                                      };
                object[] Valores = { 
                                        //  Cita.Id_CriterioCita
                                        Cita.Id_Cliente
                                        ,Cita.FechaInicial
                                        ,23
                                        ,Cita.Usuario
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_AsignarFecha", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneTipoVisitaCte(int Id_Emp, int Id_Cd, int cliente, string Conexion, ref string visita)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@IdCliente",
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cd
                                        ,cliente
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtieneTipoVisitaCliente", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    visita = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_TipoVisita")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosEmpresaCita(int Id_CitaVisita, ref string Empr, ref string Conta, ref string Fromz, ref string Toz, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        Id_CitaVisita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDatosCita_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Empr = dr.GetString(dr.GetOrdinal("Cliente"));
                    Conta = dr.GetString(dr.GetOrdinal("Cte_Contacto"));
                    Fromz = dr.GetString(dr.GetOrdinal("U_Correo"));
                    Toz = dr.GetString(dr.GetOrdinal("Cte_Email"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosCriterioCita(int Id_CitaVisita, ref string txtRSC, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        Id_CitaVisita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDatosCita_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    txtRSC = dr.GetString(dr.GetOrdinal("U_Nombre"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaMotivoModificacion(int Id_CitaVisita, string Motivo, string Conexion)
        {
            try
            {
                int verificador = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_CitaVisita", 
                                        "@Motivo",
                                      };
                object[] Valores = {    Id_CitaVisita
                                        ,Motivo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCambioVisita_AgregarALog", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaModificacion(int Id_CitaVisita, string Conexion)
        {
            try
            {
                int verificador = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_CitaVisita",
                                      };
                object[] Valores = {    Id_CitaVisita
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCambioVisita_CancelaModificacion", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
