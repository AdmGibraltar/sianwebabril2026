using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Collections;
using System.Configuration;
namespace CapaDatos
{
    public class CD_CapPedidoVtaInst
    {
        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Estatus",
                                          "@Vigencia",
                                          "@CteIni",
                                          "@CteFin",
                                          "@SemIni",
                                          "@SemFin",
                                          "@AnioIni",
                                          "@AnioFin",
                                          "@TerIni",
                                          "@TerFin",
                                          "@Id_U",
                                          "@Cte_Nombre",
                                          "@Credito"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus,
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_SemIni  == ""? (object)null: pedido.Filtro_SemIni ,
                                       pedido.Filtro_SemFin  == ""? (object)null: pedido.Filtro_SemFin ,
                                       pedido.Filtro_AnioIni  == ""? (object)null: pedido.Filtro_AnioIni ,
                                       pedido.Filtro_AnioFin  == ""? (object)null: pedido.Filtro_AnioFin ,
                                       pedido.Filtro_TerIni ,
                                       pedido.Filtro_TerFin ,
                                       pedido.Id_U,
                                       pedido.Filtro_Nombre  == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_Credito == ""? (object)null : pedido.Filtro_Credito
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = (string)dr.GetValue(dr.GetOrdinal("Vigencia"));
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Estatus",
                                          "@Vigencia",
                                          "@CteIni",
                                          "@CteFin",
                                          "@SemIni",
                                          "@SemFin",
                                          "@AnioIni",
                                          "@AnioFin",
                                          "@TerIni",
                                          "@TerFin",
                                          "@Id_U",
                                          "@Cte_Nombre",
                                          "@Credito",
                                          "@filtroTipoGarantia"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus,
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_SemIni  == ""? (object)null: pedido.Filtro_SemIni ,
                                       pedido.Filtro_SemFin  == ""? (object)null: pedido.Filtro_SemFin ,
                                       pedido.Filtro_AnioIni  == ""? (object)null: pedido.Filtro_AnioIni ,
                                       pedido.Filtro_AnioFin  == ""? (object)null: pedido.Filtro_AnioFin ,
                                       pedido.Filtro_TerIni ,
                                       pedido.Filtro_TerFin ,
                                       pedido.Id_U,
                                       pedido.Filtro_Nombre  == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_Credito == ""? (object)null : pedido.Filtro_Credito,
                                       modalidadVenta
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista_ModalidadVenta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = (string)dr.GetValue(dr.GetOrdinal("Vigencia"));
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Baja", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RechazarPedidoVI(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Rechazar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Consultar(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedido = new PedidoVtaInst();

                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Id_AcsVersion = Convert.ToInt32(dr["Id_AcsVersion"]);
                    pedido.Cte_Nom = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    pedido.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    pedido.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    pedido.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    pedido.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();
                    pedido.Acs_ReqOrden = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    pedido.Acs_ReqDocReposicion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocReposicion")));
                    pedido.Acs_ReqDocFolio = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFolio")));
                    pedido.Acs_ReqDocOtro = dr["Acs_RecDocOtro"].ToString();
                    pedido.Acs_Modalidad = dr["Acs_Modalidad"].ToString().Trim();
                    cc.Cte_Calle = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    cc.Cte_Numero = dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString();
                    cc.Cte_Colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    cc.Cte_Municipio = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    cc.Cte_Cp = dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cc.Cte_Estado = dr.GetValue(dr.GetOrdinal("Cte_Estado")).ToString();
                    pedido.Acs_PedidoEncargadoEnviar = dr["Acs_PedidoEncargadoEnviar"].ToString();
                    pedido.Acs_PedidoPuesto = dr["Acs_PedidoPuesto"].ToString();
                    pedido.Acs_PedidoTelefono = dr["Acs_PedidoTelefono"].ToString();
                    pedido.Acs_PedidoEmail = dr["Acs_PedidoEmail"].ToString();
                    pedido.Acs_Contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.Acs_Telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.Acs_Email3 = dr["Acs_Email3"].ToString();


                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int? idTG)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_TG"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       idTG
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        0,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] ParametrosFec = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio"
                                      };
                object[] ValoresFec = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio
                                   };

                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, ParametrosFec, ValoresFec);
                PedidoVtaInst p;
                while (dr2.Read())
                {
                    if (dr2["Id_TG"] != null)
                    {
                        if (dr2["Id_TG"].ToString() == idTG.Value.ToString())
                        {
                            p = new PedidoVtaInst();
                            p.Id_Prd = Convert.ToInt64(dr2["Id_Prd"]);
                            p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                            p.Acs_Lunes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Lunes")) ? false : Convert.ToBoolean(dr2["Acs_Lunes"]);
                            p.Acs_Martes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Martes")) ? false : Convert.ToBoolean(dr2["Acs_Martes"]);
                            p.Acs_Miercoles = dr2.IsDBNull(dr2.GetOrdinal("Acs_Miercoles")) ? false : Convert.ToBoolean(dr2["Acs_Miercoles"]);
                            p.Acs_Jueves = dr2.IsDBNull(dr2.GetOrdinal("Acs_Jueves")) ? false : Convert.ToBoolean(dr2["Acs_Jueves"]);
                            p.Acs_Viernes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Viernes")) ? false : Convert.ToBoolean(dr2["Acs_Viernes"]);
                            p.Acs_Sabado = dr2.IsDBNull(dr2.GetOrdinal("Acs_Sabado")) ? false : Convert.ToBoolean(dr2["Acs_Sabado"]);
                            p.Acs_Documento = dr2["Acs_Documento"].ToString();

                            List.Add(p);
                        }
                    }
                    else if (idTG.Value == 0)
                    {
                        p = new PedidoVtaInst();
                        p.Id_Prd = Convert.ToInt64(dr2["Id_Prd"]);
                        p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                        p.Acs_Lunes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Lunes")) ? false : Convert.ToBoolean(dr2["Acs_Lunes"]);
                        p.Acs_Martes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Martes")) ? false : Convert.ToBoolean(dr2["Acs_Martes"]);
                        p.Acs_Miercoles = dr2.IsDBNull(dr2.GetOrdinal("Acs_Miercoles")) ? false : Convert.ToBoolean(dr2["Acs_Miercoles"]);
                        p.Acs_Jueves = dr2.IsDBNull(dr2.GetOrdinal("Acs_Jueves")) ? false : Convert.ToBoolean(dr2["Acs_Jueves"]);
                        p.Acs_Viernes = dr2.IsDBNull(dr2.GetOrdinal("Acs_Viernes")) ? false : Convert.ToBoolean(dr2["Acs_Viernes"]);
                        p.Acs_Sabado = dr2.IsDBNull(dr2.GetOrdinal("Acs_Sabado")) ? false : Convert.ToBoolean(dr2["Acs_Sabado"]);
                        p.Acs_Documento = dr2["Acs_Documento"].ToString();

                        List.Add(p);
                    }

                }

                CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        DBNull.Value,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                //SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                //PedidoVtaInst p;
                //while (dr2.Read())
                //{
                //    p = new PedidoVtaInst();
                //    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                //    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                //    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                //    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                //    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                //    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                //    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                //    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                //    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                //    List.Add(p);

                //}

                //CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarDetadmin(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int? idTG)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_TG"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       idTG
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                              dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                            });
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet_Restoadmin(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                              dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                            });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el detalle del resto de productos de un pedido en captura de garantía.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="List"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="Id_TG">Identificador del tipo de garantía</param>
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int Id_TG)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    bool cargar = false;
                    if (Id_TG == 0)
                    {
                        if (dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                        {
                            //cargar
                            cargar = true;
                        }
                        else //no es nulo: validar que el tipo de garantía coincida
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    else //no es cero: validar que el tipo de garantía coincida
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("Id_TG"))) // primero se valida que no sea nulo
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    if (cargar == true)
                    {
                        dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            -100,
                            -100,
                            -100,
                            dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            0,
                            (dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            0,
                            DBNull.Value,
                            dr.GetValue(dr.GetOrdinal("Id_TG")),
                            dr.GetValue(dr.GetOrdinal("Id_Acs")),
                            0,
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        });
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                //SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                //PedidoVtaInst p;
                //while (dr2.Read())
                //{
                //    p = new PedidoVtaInst();
                //    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                //    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                //    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                //    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                //    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                //    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                //    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                //    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                //    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                //    List.Add(p);

                //}

                //CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el detalle del resto de productos de un pedido en captura de garantía.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="List"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="Id_TG">Identificador del tipo de garantía</param>
        public void ConsultarDet_RestoDetalle(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int Id_TG)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    bool cargar = false;
                    if (Id_TG == 0)
                    {
                        if (dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                        {
                            //cargar
                            cargar = true;
                        }
                        else //no es nulo: validar que el tipo de garantía coincida
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    else //no es cero: validar que el tipo de garantía coincida
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("Id_TG"))) // primero se valida que no sea nulo
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    if (cargar == true)
                    {
                        if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")).ToString()) != 0)
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            -100,
                            -100,
                            -100,
                            dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            0,
                            0,
                            dr.GetValue(dr.GetOrdinal("Id_TG")),
                            dr.GetValue(dr.GetOrdinal("Id_Acs"))
                        });
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDetAcys(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                SqlDataReader dr2 = null;

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio
                                   };



                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                PedidoVtaInst p;
                while (dr2.Read())
                {
                    p = new PedidoVtaInst();
                    p.Id_Prd = Convert.ToInt64(dr2["Id_Prd"]);
                    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                    List.Add(p);

                }

                CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object Str(object p)
        {
            switch (p.ToString().ToUpper())
            {
                case "F": return "Factura";
                case "R": return "Remisión";
                default: return "";
            }
        }

        private object Nombre(object p)
        {
            switch (p.ToString().ToLower())
            {
                case "l": return "Lunes";
                case "m": return "Martes";
                case "mi": return "Miercoles";
                case "j": return "Jueves";
                case "v": return "Viernes";
                case "s": return "Sabado";
                default: return "";
            }
        }
        public void ConsultarPedidoExistente(PedidoVtaInst pvi, long Id_Prd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Semana",
                                          "@Id_Cte",
                                          "@Id_ter"
                                      };
                object[] Valores = {
                                        pvi.Id_Emp,
                                        pvi.Id_Cd,
                                        Id_Prd,
                                        pvi.Acs_Semana,
                                        pvi.Id_Cte,
                                        pvi.Id_Ter
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Existente", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarrPedidEcommerce(PedidoVtaInst pedido, string Conexion, CapaDatos.CD_Datos CapaDatos, ref int verificador)
        {

            try
            {

                string[] Parametros = {
                                                "@Num_Pedido",
                                                "@id_cte",
                                                "@Id_ped",
                                                "@id_cd"
                                              };
                object[] Valores = {
                                                pedido.Requisicion,
                                                pedido.Id_Cte,
                                                pedido.Id_Ped,
                                                pedido.Id_Cd
                                           };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_guardarPedidocommerce", ref verificador, Parametros, Valores);


            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }





        public void Insertar(PedidoVtaInst pedido, int tipoPedido, System.Data.DataTable dt, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificardorLog = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion"
                                        ,"@UsoCFDI"

                //                                pedido.FechaFacAcys = rdFechaF.SelectedDate.Value;
                //pedido.PedAcys = this.TxtPed_PedAcys.Text.Trim();
                //pedido.ReqAcys = this.TxtPed_ReqAcys.Text.Trim();
                //pedido.OcAcys = this.TxtPed_OCAcys.Text.Trim();
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion,
                                        pedido.UsoCFDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Garantias", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    string[] ParametrosLog;
                    object[] ValoresLog;

                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@id_ped",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                pedido.Id_U,
                                pedido.Id_Cd,
                                pedido.Id_Ped, // IdOC
                                0,
                                0,
                                "Se crea el pedido: "+ pedido.Id_Ped.ToString(),
                                pedido.Id_Cte,
                                0
                            };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spPedidos_Log", ref verificardorLog, ParametrosLog, ValoresLog);

                    verificador = -1;
                    // ====================
                    // INSERTAR DETALLE
                    // ====================
                    InsertarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG.Value);


                    if (tipoPedido == 1)
                    {
                        ActualizarrPedidEcommerce(pedido, Conexion, CapaDatos, ref verificador);
                    }
                    verificador = -1;

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void Insertar(PedidoVtaInst pedido, System.Data.DataTable dt, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion",
                                        "@UsoCFDI"

                //                                pedido.FechaFacAcys = rdFechaF.SelectedDate.Value;
                //pedido.PedAcys = this.TxtPed_PedAcys.Text.Trim();
                //pedido.ReqAcys = this.TxtPed_ReqAcys.Text.Trim();
                //pedido.OcAcys = this.TxtPed_OCAcys.Text.Trim();
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion,
                                        pedido.UsoCFDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Garantias", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    InsertarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG.Value);

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private static void InsertarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    if (Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        ParametrosAcys = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_AcsDet",
                            "@Id_Acs",
                            "@Id_Prd",
                            "@Acs_Cantidad",
                            "@Acs_Documento",
                            "@Acs_Sabado",
                            "@Acs_Viernes",
                            "@Acs_Jueves",
                            "@Acs_Miercoles",
                            "@Acs_Martes",
                            "@Acs_Lunes",
                            "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
                            0,
                            pedido.Id_Acs,
                            dt.Rows[x]["Id_Prd"],
                            dt.Rows[x]["Prd_Cantidad"],
                            dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                            dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                            dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                    else//ACTUALIZACION
                    {
                        ParametrosAcys = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_AcsDet",
                            "@Id_Acs",
                            "@Id_Prd",
                            "@Acs_Cantidad",
                            "@Acs_Documento",
                            "@Acs_Sabado",
                            "@Acs_Viernes",
                            "@Acs_Jueves",
                            "@Acs_Miercoles",
                            "@Acs_Martes",
                            "@Acs_Lunes",
                            "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
                            0,
                            pedido.Id_Acs,
                            dt.Rows[x]["Id_Prd"],
                            dt.Rows[x]["Prd_Cantidad"],
                            dt.Rows[x]["Acs_Doc"],
                            dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                            dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Modificar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                }
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {


                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                       dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
        }


        private static void InsertarDet_v2(int Id_U, int Id_Cd, PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {

            int verificadorLog = 0;

            if (dt.Rows.Count == 0) return;

            string[] ParametrosAcys;
            object[] ValoresAcys;

            string[] ParametrosLog;
            object[] ValoresLog;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {

                    if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) != Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]))
                    {
                        // LOG
                        // Movimientos
                        ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                        ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]),
                                Convert.ToInt64(dt.Rows[x]["Id_Prd"]),
                                "'Se sustituye producto "+  Convert.ToString(dt.Rows[x]["Id_PrdOld"]) +" por "+ Convert.ToString(dt.Rows[x]["Id_Prd"]),
                                pedido.IdOC,
                                pedido.Id_Cte,
                                dt.Rows[x]["Prd_Cantidad"]
                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificadorLog, ParametrosLog, ValoresLog);
                    }

                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                       dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
        }

        private static void InsertarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int idTg)
        {
            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    if (Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        ParametrosAcys = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_AcsDet",
                            "@Id_Acs",
                            "@Id_Prd",
                            "@Acs_Cantidad",
                            "@Acs_Documento",
                            "@Acs_Sabado",
                            "@Acs_Viernes",
                            "@Acs_Jueves",
                            "@Acs_Miercoles",
                            "@Acs_Martes",
                            "@Acs_Lunes",
                            "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
                            0,
                            pedido.Id_Acs,
                            dt.Rows[x]["Id_Prd"],
                            dt.Rows[x]["Prd_Cantidad"],
                            dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                            dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                            dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                    else//ACTUALIZACION
                    {
                        ParametrosAcys = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_AcsDet",
                            "@Id_Acs",
                            "@Id_Prd",
                            "@Acs_Cantidad",
                            "@Acs_Documento",
                            "@Acs_Sabado",
                            "@Acs_Viernes",
                            "@Acs_Jueves",
                            "@Acs_Miercoles",
                            "@Acs_Martes",
                            "@Acs_Lunes",
                            "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
                            0,
                            pedido.Id_Acs,
                            dt.Rows[x]["Id_Prd"],
                            dt.Rows[x]["Prd_Cantidad"],
                            dt.Rows[x]["Acs_Doc"],
                            dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                            dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Modificar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                }


                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) != Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]))
                    {
                        int verificadorLog = 0;
                        string[] ParametrosLog;
                        object[] ValoresLog;

                        // LOG
                        // Movimientos
                        ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@id_ped",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Id_Cte",
                            "@Cantidad"
                            };
                        ValoresLog = new object[] {
                                pedido.Id_Emp,
                                pedido.Id_U,
                                pedido.Id_Cd,
                                pedido.Id_Ped.ToString(),
                                Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]),
                                Convert.ToInt64(dt.Rows[x]["Id_Prd"]),
                                "'Se sustituye producto "+  Convert.ToString(dt.Rows[x]["Id_PrdOld"]) +" por "+ Convert.ToString(dt.Rows[x]["Id_Prd"]),
                                pedido.Id_Cte,
                                dt.Rows[x]["Prd_Cantidad"]
                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spPedidos_Log", ref verificadorLog, ParametrosLog, ValoresLog);
                    }


                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"],
                                        idTg
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? id_tg)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                if (captado > 0)
                {
                    string[] Parametros2 = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Credito",
                                           "@Id_PedVI"
                                       };
                    object[] Valores2 = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        2,
                                        captado
                                     };

                    SqlCommand sqlcmd2 = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros2, Valores2);
                }

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@Id_Ped",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys"

                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.Id_Ped,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Modificar2", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    ModificarDet2(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, id_tg);

                    foreach (int i in eliminados)
                    {
                        Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@Id_Prd" };
                        Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, i };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);
                    }

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_CorrigeDet2", ref verificador, Parametros, Valores);

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                if (captado > 0)
                {
                    string[] Parametros2 = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Credito",
                                           "@Id_PedVI"
                                       };
                    object[] Valores2 = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        2,
                                        captado
                                     };

                    SqlCommand sqlcmd2 = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros2, Valores2);
                }

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@Id_Ped",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                        "@UsoCFDI"

                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.Id_Ped,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys,
                                        pedido.UsoCFDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Modificar2", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    ModificarDetInternet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);

                    foreach (int i in eliminados)
                    {
                        Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@Id_Prd" };
                        Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, i };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);
                    }

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_CorrigeDet", ref verificador, Parametros, Valores);

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private static void ModificarDetCN(int Id_U, int Id_Cd, PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            string[] ParametrosLog;
            object[] ValoresLog;
            int verificadorLog = 0;

            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;


            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };

            if (dt.Rows.Count == 0) return;

            // LOG
            // Movimientos
            ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Requisicion",
                           "@Id_Cte"
                            };
            ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                pedido.IdOC,
                                pedido.Id_Cte
                            };
            sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_delLog", ref verificadorLog, ParametrosLog, ValoresLog);


            string[] Parametrosdel = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped"
                                      };
            //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
            object[] Valoresdel = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };
            sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Eliminar", ref verificador, Parametrosdel, Valoresdel);


            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {

                    if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) != Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]))
                    {
                        // LOG
                        // Movimientos
                        ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                        ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]),
                                Convert.ToInt64(dt.Rows[x]["Id_Prd"]),
                                "'Se sustituye producto "+  Convert.ToString(dt.Rows[x]["Id_PrdOld"]) +" por "+ Convert.ToString(dt.Rows[x]["Id_Prd"]),
                                pedido.IdOC,
                                pedido.Id_Cte,
                                dt.Rows[x]["Prd_Cantidad"]
                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificadorLog, ParametrosLog, ValoresLog);
                    }

                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                       dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);

                }
            }
        }


        private static void ModificarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {


            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };


            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    ParametrosAcys = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_AcsDet",
                            "@Id_Acs",
                            "@Id_Prd",
                            "@Acs_Cantidad",
                            "@Acs_Documento",
                            "@Acs_Sabado",
                            "@Acs_Viernes",
                            "@Acs_Jueves",
                            "@Acs_Miercoles",
                            "@Acs_Martes",
                            "@Acs_Lunes",
                            "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                    ValoresAcys = new object[] {
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
                            0,
                            pedido.Id_Acs,
                            dt.Rows[x]["Id_Prd"],
                            dt.Rows[x]["Prd_Cantidad"],
                            dt.Rows[x]["Acs_Doc"],
                            dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                            dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                            dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);


                }
                var prueba = dt.Rows[x]["Acs_Doc"].ToString();

                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        // dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1)
                                        dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]

                                   };
                    if (Convert.ToInt32(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);
                    }
                    else //ACTUALIZAR
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Modificar", ref verificador, Parametros, Valores);
                    }
                }
            }
        }


        private static void ModificarDetInternet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };


            for (int x = 0; x < dt.Rows.Count; x++)
            {


                var prueba = dt.Rows[x]["Acs_Doc"].ToString();

                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,

                                        dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]

                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);

                }
            }
        }

        public void ListaFacturacion(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Cte_Nombre",
                                          "@Id_CteIni",
                                          "@Id_CteFin",
                                          "@Ped_FechaIni",
                                          "@Ped_FechaFin",
                                          "@Ped_FechaFIni",
                                          "@Ped_FechaFFin",
                                          "@Estatus",
                                          "@Id_U",
                                          "@Ped_Doc",
                                          "@Picking"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_FecFIni,
                                       pedido.Filtro_FecFFin,
                                       pedido.Filtro_Estatus,
                                       pedido.Filtro_usuario,
                                       pedido.Filtro_Documento,
                                       pedido.Filtro_Tipo
                                   };

                String Filtro_Piching = pedido.Filtro_Tipo;

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoVi_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Id_Ped = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    pedido.Ped_FechaEntrega = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega"));
                    pedido.Ped_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_Fecha"));
                    pedido.Acs_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys"));
                    pedido.Ped_Comentarios = dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    pedido.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Ped_Total = (double)dr.GetValue(dr.GetOrdinal("Ped_Total"));
                    pedido.Ped_Asign = dr.GetValue(dr.GetOrdinal("Ped_Asign")).ToString();
                    pedido.Ped_AsignStr = AsignadoStr(dr.GetValue(dr.GetOrdinal("Ped_Asign")).ToString());
                    pedido.Rut_Descripcion = dr.GetValue(dr.GetOrdinal("Rut_Descripcion")).ToString();
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    pedido.TG_Nombre = dr.GetValue(dr.GetOrdinal("TG_Nombre")).ToString();
                    if (ConfigurationManager.AppSettings["ValidaPicking"].ToString() == "S")
                    {
                        if (Filtro_Piching == "K")
                        {
                            pedido.ValidaPicking = "S";
                        }
                        else
                        {
                            pedido.ValidaPicking = "N";
                        }
                    }
                    else
                    {
                        pedido.ValidaPicking = "S";
                    }
                    List.Add(pedido);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //EDSG 08042015
        private string Tipo(string p)
        {
            switch (p)
            {
                case "1":
                    return "Sin distribución";
                case "2":
                    return "Con distribución";
                case "3":
                    return "Venta instalada";
                case "4":
                    return "Venta nueva";
                case "5":
                    return "Internet";
                default:
                    return "";
            }
        }


        private string AsignadoStr(string p)
        {
            switch (p)
            {
                case "A": return "Si";
                case "N": return "No";
                case "P": return "Parcial";
                default: return "";
            }
        }

        public void ConsultarAAAEspecial(int Id_Emp, int Id_Cd, double Id_Cte, string Id_prd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Prd"
                                      };
                object[] Valores = {
                                        Id_Emp,
                                        Id_Cd,
                                        Id_Cte,
                                        Id_prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioAAAEspecial", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet2(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int? idTG)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_TG"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       idTG
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        0,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs")),
                        dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? "No" : dr.GetBoolean(dr.GetOrdinal("ACS_ReqOC"))? "Sí" : "No",
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        0
                        ,dr.GetValue(dr.GetOrdinal("Prd_Activo"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] ParametrosFec = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio"
                                      };
                object[] ValoresFec = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio
                                   };

                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, ParametrosFec, ValoresFec);
                PedidoVtaInst p;
                while (dr2.Read())
                {
                    if (dr2["Id_TG"] != null)
                    {
                        if (dr2["Id_TG"].ToString() == idTG.Value.ToString())
                        {
                            p = new PedidoVtaInst();
                            p.Id_Prd = Convert.ToInt64(dr2["Id_Prd"]);
                            p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                            p.Acs_Lunes = dr2["Acs_Lunes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Lunes"]);
                            p.Acs_Martes = dr2["Acs_Martes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Martes"]);
                            p.Acs_Miercoles = dr2["Acs_Miercoles"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Miercoles"]);
                            p.Acs_Jueves = dr2["Acs_Jueves"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Jueves"]);
                            p.Acs_Viernes = dr2["Acs_Viernes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Viernes"]);
                            p.Acs_Sabado = dr2["Acs_Sabado"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Sabado"]);
                            p.Acs_Documento = dr2["Acs_Documento"].ToString();

                            List.Add(p);
                        }
                    }
                    else if (idTG.Value == 0)
                    {
                        p = new PedidoVtaInst();
                        p.Id_Prd = Convert.ToInt64(dr2["Id_Prd"]);
                        p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                        p.Acs_Lunes = dr2["Acs_Lunes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Lunes"]);
                        p.Acs_Martes = dr2["Acs_Martes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Martes"]);
                        p.Acs_Miercoles = dr2["Acs_Miercoles"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Miercoles"]);
                        p.Acs_Jueves = dr2["Acs_Jueves"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Jueves"]);
                        p.Acs_Viernes = dr2["Acs_Viernes"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Viernes"]);
                        p.Acs_Sabado = dr2["Acs_Sabado"] == DBNull.Value ? false : Convert.ToBoolean(dr2["Acs_Sabado"]);
                        p.Acs_Documento = dr2["Acs_Documento"].ToString();

                        List.Add(p);
                    }

                }

                CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet_Resto2(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        DBNull.Value,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarDet_Resto2(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int Id_TG)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    bool cargar = false;
                    if (Id_TG == 0)
                    {
                        if (dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                        {
                            //cargar
                            cargar = true;
                        }
                        else //no es nulo: validar que el tipo de garantía coincida
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    else //no es cero: validar que el tipo de garantía coincida
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("Id_TG"))) // primero se valida que no sea nulo
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    if (cargar == true)
                    {
                        dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            -100,
                            -100,
                            -100,
                            dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                           0,
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            0,
                            DBNull.Value,
                            dr.GetValue(dr.GetOrdinal("Id_TG")),
                            dr.GetValue(dr.GetOrdinal("Id_Acs")),
                              dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? "No" :  dr.GetBoolean(dr.GetOrdinal("ACS_ReqOC"))? "Sí" : "No"
                        });
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                //SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                //PedidoVtaInst p;
                //while (dr2.Read())
                //{
                //    p = new PedidoVtaInst();
                //    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                //    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                //    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                //    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                //    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                //    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                //    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                //    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                //    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                //    List.Add(p);

                //}

                //CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAgendaCliente(AgendaRsc Agenda, ref List<Cliente> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_Emp", "@idUsuario", "@Id_Cd" };
                object[] Valores = { Agenda.Id_Emp, Agenda.Id_Usu, Agenda.Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_AgendaClienteRSC", ref dr, Parametros, Valores);
                Cliente agenda;
                while (dr.Read())
                {
                    agenda = new Cliente();
                    agenda.idCte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));
                    agenda.nombre = dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreCliente")));
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


        public void ConsultaClienteAcysCombo(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_ConsultaClienteAcysCombo2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    pedido.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Acs_NomComercial = (dr.GetValue(dr.GetOrdinal("Acs_NomComercial"))).ToString();
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaClienteAcys(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@cte" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Cte };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_ConsultaClienteAcys2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    pedido.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar2(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Consultar2", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedido = new PedidoVtaInst();

                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Id_AcsVersion = Convert.ToInt32(dr["Id_AcsVersion"]);
                    pedido.Cte_Nom = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    pedido.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    pedido.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    pedido.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    pedido.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();
                    pedido.Acs_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Acs_RecOrdenCompra")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOrdenCompra")));
                    pedido.Acs_ReqDocReposicion = dr.IsDBNull(dr.GetOrdinal("Acs_RecOrdenReposicion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOrdenReposicion")));
                    pedido.Acs_ReqDocFolio = dr.IsDBNull(dr.GetOrdinal("Acs_RecDocCertificadoEnt")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCertificadoEnt")));
                    pedido.Acs_ReqFacturaKey = dr.IsDBNull(dr.GetOrdinal("Acs_RecFacturaKey")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFacturaKey")));
                    pedido.Acs_ReqCopia = dr.IsDBNull(dr.GetOrdinal("Acs_RecDocCopia")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopia")));
                    pedido.ACS_ReqRemision = dr.IsDBNull(dr.GetOrdinal("Acs_RecRemision")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRemision")));

                    pedido.Acs_ReqOrdencop = dr.IsDBNull(dr.GetOrdinal("Acs_RecOrdenCompracop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecOrdenCompracop")));
                    pedido.Acs_ReqDocReposicioncop = dr.IsDBNull(dr.GetOrdinal("Acs_RecOrdenReposicioncop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecOrdenReposicioncop")));
                    pedido.Acs_ReqDocFoliocop = dr.IsDBNull(dr.GetOrdinal("Acs_RecDocCertificadoEntcop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCertificadoEntcop")));
                    pedido.Acs_ReqFacturaKeyCop = dr.IsDBNull(dr.GetOrdinal("Acs_RecFacturaKeycop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecFacturaKeycop")));
                    pedido.Acs_ReqCopiaCop = dr.IsDBNull(dr.GetOrdinal("Acs_RecDocCopiacop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopiacop")));
                    pedido.ACS_ReqRemisionCop = dr.IsDBNull(dr.GetOrdinal("Acs_RecRemisioncop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRemisioncop")));


                    pedido.Acs_ReqDocOtro = dr["Acs_RecDocOtro"].ToString();
                    pedido.Acs_Modalidad = dr["Acs_Modalidad"].ToString().Trim();
                    cc.Cte_Calle = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    cc.Cte_Numero = dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString();
                    cc.Cte_Colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    cc.Cte_Municipio = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    cc.Cte_Cp = dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cc.Cte_Estado = dr.GetValue(dr.GetOrdinal("Cte_Estado")).ToString();
                    pedido.Acs_PedidoEncargadoEnviar = dr["Acs_PedidoEncargadoEnviar"].ToString();
                    pedido.Acs_PedidoPuesto = dr["Acs_PedidoPuesto"].ToString();
                    pedido.Acs_PedidoTelefono = dr["Acs_PedidoTelefono"].ToString();
                    pedido.Acs_PedidoEmail = dr["Acs_PedidoEmail"].ToString();
                    pedido.Acs_Contacto2 = dr["Acs_Contacto2"].ToString();
                    pedido.Acs_Telefono2 = dr["Acs_Telefono2"].ToString();
                    pedido.Acs_Email2 = dr["Acs_Email2"].ToString();
                    pedido.Acs_Contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.Acs_Telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.Acs_Email3 = dr["Acs_Email3"].ToString();
                    pedido.Acs_Contacto4 = dr["Acs_Contacto4"].ToString();
                    pedido.Acs_Telefono4 = dr["Acs_Telefono4"].ToString();
                    pedido.Acs_Email4 = dr["Acs_Email4"].ToString();
                    pedido.Acs_Contacto5 = dr["Acs_Contacto5"].ToString();
                    pedido.Acs_Telefono5 = dr["Acs_Telefono5"].ToString();
                    pedido.Acs_Email5 = dr["Acs_Email5"].ToString();
                    pedido.UsoCFDI = dr["Cte_UsoCFDI"].ToString();
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoRastreo(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_ConsultarPedido", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();

                    pedido.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    pedido.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    pedido.Id_Ped = (int)dr.GetValue(dr.GetOrdinal("id_ped"));
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("ped_fecha")));

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RechazarPedidoVI2(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana",
                                          "@Acs_Pedido"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana,
                                        pedido.pedido
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Rechazar2", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar2(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana",
                                          "@Acs_Pedido",
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana,
                                        pedido.Pedido_del
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Baja2", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarProductoAcys(PedidoVtaInst pedido, DataTable dt, string Conexion, ref string verificador)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string[] Parametros = new string[]{
                                      "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_AcsDet",
                                      "@Id_Acs",
                                      "@Id_Prd",
                                      "@Acs_Cantidad",
                                      "@Acs_Documento",
                                      "@Acs_Frecuencia",
                                      "@Acs_Precio"
                                      };

                //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                object[] Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        0,
                                        pedido.Id_Acs,
                                        dt.Rows[x]["Id_Prd"],
                                       dt.Rows[x]["Prd_Cantidad"],
                                        dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Acs_Frecuencia"],
                                        dt.Rows[x]["Prd_Precio"]
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_InsertarProductoCaptacionCon", ref verificador, Parametros, Valores);
            }
        }

        public void guardarInformacionPedidosSinAcys(PedidoVtaInst pedido, string Conexion, ref int Cliente, ref int Usuario)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_U",
                                        "@Fecha",
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Id_U,
                                        DateTime.Now
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CapPedidosCaptadosSolicitados", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Cliente = (int)dr.GetValue(dr.GetOrdinal("cliente"));
                    Usuario = (int)dr.GetValue(dr.GetOrdinal("usuario"));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Lista2(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Estatus",
                                          "@Vigencia",
                                          "@Cte",
                                          "@Sem",
                                          "@mes",
                                          "@mesFinal",
                                          "@Anio",
                                          "@AnioFinal",
                                          "@Id_U",
                                          "@Frecuencia",
                                          "@Pedido",
                                          "@semInicial",
                                          "@semFinal"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus,
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_Cte == ""? (object)null: pedido.Filtro_Cte,
                                       pedido.Filtro_Sem == 0 ? (object)null : pedido.Filtro_Sem,
                                       pedido.Filtro_mes == ""? (object)null: pedido.Filtro_mes ,
                                       pedido.Filtro_mesFinal == ""? (object)null: pedido.Filtro_mesFinal ,
                                       pedido.Filtro_AnioFinal == ""? (object)null: pedido.Filtro_Anios,
                                       pedido.Filtro_AnioFinal == ""? (object)null: pedido.Filtro_AnioFinal,
                                       pedido.Id_U,
                                       pedido.Filtro_Frecuencia == ""? (object)null: pedido.Filtro_Frecuencia,
                                       (object)null ,
                                       pedido.Filtro_SemIni == "" ? (object)null : pedido.Filtro_SemIni,
                                       pedido.Filtro_SemFin == "" ? (object)null : pedido.Filtro_SemFin,

                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista_ModalidadVenta2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Identificador = (string)dr.GetValue(dr.GetOrdinal("Identificador"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = dr.IsDBNull(dr.GetOrdinal("Acs_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = dr["Acs_Estatus"].ToString() == "C" ? dr["Acs_Estatus"].ToString() : dr["Vigencia"].ToString();
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    pedido.pedido = dr["pedido"].ToString();
                    pedido.Acs_Mes = (int)dr.GetValue(dr.GetOrdinal("mes"));
                    pedido.id_cteDirEntrega = dr.IsDBNull(dr.GetOrdinal("id_cteDirEntrega")) ? -1 : (int)dr.GetValue(dr.GetOrdinal("id_cteDirEntrega"));
                    pedido.Cte_Calle = dr.IsDBNull(dr.GetOrdinal("Cte_Calle")) ? "N/A" : dr["Cte_Calle"].ToString();
                    pedido.cte_numero = dr.IsDBNull(dr.GetOrdinal("cte_numero")) ? "" : dr["cte_numero"].ToString();
                    pedido.CTE_Colonia = dr.IsDBNull(dr.GetOrdinal("CTE_Colonia")) ? "" : dr["CTE_Colonia"].ToString();
                    pedido.CTe_Estado = dr.IsDBNull(dr.GetOrdinal("CTe_Estado")) ? "" : dr["CTe_Estado"].ToString();
                    pedido.Direccion = pedido.Cte_Calle + " " + pedido.cte_numero + ", " + pedido.CTE_Colonia + ", " + pedido.CTe_Estado;

                    pedido.ID_sol = dr.IsDBNull(dr.GetOrdinal("id_sol")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_sol")));
                    pedido.EstatusSOl = dr.IsDBNull(dr.GetOrdinal("EstatusSol")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("EstatusSol")));


                    if (pedido.EstatusSOl == "N")
                    {
                        pedido.EstatusStr = "Pendiente";
                    }
                    else if (pedido.EstatusSOl == "P")
                    {
                        pedido.EstatusStr = "Pendiente Autorización";
                    }
                    else if (pedido.EstatusSOl == "R")
                    {
                        pedido.EstatusStr = "Rechazado";
                    }
                    else if (pedido.EstatusSOl == "A")
                    {
                        pedido.EstatusStr = "Autorizado";
                    }

                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Cliente_credito(ref PedidoVtaInst pedido, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Cte",

                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Cte
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Credito", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedido = new PedidoVtaInst();
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("id_cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("cte_nomComercial"));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListaOrderCompra(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Estatus",
                                          "@Vigencia",
                                          "@Cte",
                                          "@Sem",
                                          "@mes",
                                          "@mesFinal",
                                          "@Anio",
                                          "@AnioFinal",
                                          "@Id_U",
                                          "@Frecuencia",
                                          "@Pedido",
                                          "@semInicial",
                                          "@semFinal",
                                          "@Filtro_tipoPed"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus,
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_Cte == ""? (object)null: pedido.Filtro_Cte,
                                       pedido.Filtro_Sem == 0 ? (object)null : pedido.Filtro_Sem,
                                       pedido.Filtro_mes == ""? (object)null: pedido.Filtro_mes ,
                                       pedido.Filtro_mesFinal == ""? (object)null: pedido.Filtro_mesFinal ,
                                       pedido.Filtro_AnioFinal == ""? (object)null: pedido.Filtro_Anios,
                                       pedido.Filtro_AnioFinal == ""? (object)null: pedido.Filtro_AnioFinal,
                                       pedido.Id_U,
                                       pedido.Filtro_Frecuencia == ""? (object)null: pedido.Filtro_Frecuencia,
                                       pedido.filtro_pedido  == ""? (object)null: pedido.filtro_pedido,
                                       pedido.Filtro_SemIni == "" ? (object)null : pedido.Filtro_SemIni,
                                       pedido.Filtro_SemFin == "" ? (object)null : pedido.Filtro_SemFin,
                                        pedido.Filtro_tipoPed  == "" ? (object)null : pedido.Filtro_tipoPed,

                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista_ModalidadVentaOrderCompra", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = dr.IsDBNull(dr.GetOrdinal("Acs_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.OrdenCompra = (string)dr.GetValue(dr.GetOrdinal("OrdenCompra"));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = dr["Acs_Estatus"].ToString() == "C" ? dr["Acs_Estatus"].ToString() : dr["Vigencia"].ToString();
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    pedido.pedido = dr["pedido"].ToString();
                    pedido.Acs_Mes = (int)dr.GetValue(dr.GetOrdinal("mes"));
                    pedido.id_cteDirEntrega = dr.IsDBNull(dr.GetOrdinal("id_cteDirEntrega")) ? -1 : (int)dr.GetValue(dr.GetOrdinal("id_cteDirEntrega"));
                    pedido.Cte_Calle = dr.IsDBNull(dr.GetOrdinal("Cte_Calle")) ? "N/A" : dr["Cte_Calle"].ToString();
                    pedido.cte_numero = dr.IsDBNull(dr.GetOrdinal("cte_numero")) ? "" : dr["cte_numero"].ToString();
                    pedido.CTE_Colonia = dr.IsDBNull(dr.GetOrdinal("CTE_Colonia")) ? "" : dr["CTE_Colonia"].ToString();
                    pedido.CTe_Estado = dr.IsDBNull(dr.GetOrdinal("CTe_Estado")) ? "" : dr["CTe_Estado"].ToString();
                    pedido.Direccion = pedido.Cte_Calle + " " + pedido.cte_numero + ", " + pedido.CTE_Colonia + ", " + pedido.CTe_Estado;
                    pedido.str_Tipo_pedido = dr.IsDBNull(dr.GetOrdinal("str_Tipo_pedido")) ? "" : dr["str_Tipo_pedido"].ToString();
                    pedido.IsTieneOC = dr.IsDBNull(dr.GetOrdinal("TieneOC")) ? "0" : dr["TieneOC"].ToString();

                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPedido(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Pedido"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Ped

                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_ConsultarPedido", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.str_Tipo_pedido = dr.IsDBNull(dr.GetOrdinal("str_Tipo_pedido")) ? "" : dr["str_Tipo_pedido"].ToString();

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Lista_acysPendiente(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Cte"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista_AcysPendiente", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = dr["Acs_Estatus"].ToString() == "C" ? dr["Acs_Estatus"].ToString() : dr["Vigencia"].ToString();
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    pedido.pedido = dr["pedido"].ToString();

                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(PedidoVtaInst pedido, System.Data.DataTable dt, System.Data.DataTable dtRestos, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@ReqAcys",
                                        "@PedAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion",

                                           //Llena la informacion de compras
                                        "@Acs_Contacto2",
                                        "@Acs_Telefono2",
                                        "@Acs_Email2",
                                        //Llena la información de almacen
                                        "@Acs_Contacto3",
                                        "@Acs_Telefono3",
                                        "@Acs_Email3",
                                        //Llena la información de Mantenimiento
                                        "@Acs_Contacto4",
                                        "@Acs_Telefono4",
                                        "@Acs_Email4",
                                        //Llena la información de Pagos
                                        "@Acs_Contacto5",
                                        "@Acs_Telefono5",
                                        "@Acs_Email5",
                                        //Secccion Requiere documentos
                                        "@Acs_ReqDocFolio",
                                        "@Acs_ReqDocReposicion",
                                        "@Acs_ReqDocOtro",
                                        "@Ped_ReqFactKey ",
                                        "@Ped_ReqCopiaDoc",
                                        "@Ped_reqRemision",

                                        "@Acs_ReqFacturaKeyCop",
                                        "@Acs_ReqOrdencop",
                                        "@Acs_ReqOrdenrep",
                                        "@ACS_ReqRemisionCop",
                                        "@Acs_ReqCopiaCop",
                                        "@Acs_ReqDocFoliocop",
                                        "@id_cteDirEntrega",
                                        "@UsoCFDI"
            };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.ReqAcys,
                                        pedido.PedAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion,                                        
                                        //Llena la informacion de compras
                                        pedido.Acs_Contacto2 ,
                                        pedido.Acs_Telefono2,
                                        pedido.Acs_Email2 ,
                                        //Llena la información de almacen
                                        pedido.Acs_Contacto3 ,
                                        pedido.Acs_Telefono3 ,
                                        pedido.Acs_Email3 ,
                                        //Llena la información de Mantenimiento
                                        pedido.Acs_Contacto4 ,
                                        pedido.Acs_Telefono4 ,
                                        pedido.Acs_Email4 ,
                                        //Llena la información de Pagos
                                        pedido.Acs_Contacto5,
                                        pedido.Acs_Telefono5,
                                        pedido.Acs_Email5,
                                        //Secccion Requiere documentos
                                        pedido.Acs_ReqDocFolio ,
                                        pedido.Acs_ReqDocReposicion ,
                                        pedido.Acs_ReqDocOtro,

                                        pedido.Acs_ReqFacturaKey,
                                        pedido.Acs_ReqCopia,
                                        pedido.ACS_ReqRemision,

                                        pedido.Acs_ReqFacturaKeyCop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.ACS_ReqRemisionCop,
                                        pedido.Acs_ReqCopiaCop,
                                        pedido.Acs_ReqDocFoliocop,
                                        pedido.id_cteDirEntrega,
                                        pedido.UsoCFDI
        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Garantias2", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    InsertarDet(pedido, dt, dtRestos, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG.Value);

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarOrderCompra(PedidoVtaInst pedido, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Pedido"
                                       ,"@ID_Acys"
                                       ,"@OrdenCompra"
                                       ,"@ID_Cte"
                                       ,"@NombreDoc"
                                       ,"@Extension"
                                       ,"@Archivo"
                                       ,"@FechaDoc"
            };
                object[] Valores = {
                                   pedido.Id_Ped
                                  ,pedido.Id_Acs
                                  ,pedido.OrdenCompra
                                  ,pedido.Id_Cte
                                  ,pedido.nombreDocumento
                                  ,pedido.extension
                                  ,pedido.archivo
                                  ,DateTime.Now
        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_OrdenCompra", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }



        public void CoonsultarOrdenCompra(PedidoVtaInst pedido, ref List<PedidoVtaInst> pedidoDescarga, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Pedido"
                                       ,"@id_cd"
                                       ,"@id_emp"

            };
                object[] Valores = {
                                   pedido.Id_Ped
                                  ,pedido.Id_Cd
                                  ,pedido.Id_Emp
        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Consultar_OrdenCompra", ref dr, Parametros, Valores);
                PedidoVtaInst listapedido;

                while (dr.Read())
                {
                    listapedido = new PedidoVtaInst();
                    listapedido.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    listapedido.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    listapedido.Id_Ped = (int)dr.GetValue(dr.GetOrdinal("Id_Pedido"));
                    listapedido.nombreDocumento = dr.GetValue(dr.GetOrdinal("NombreDoc")).ToString();
                    listapedido.extension = dr.GetValue(dr.GetOrdinal("Extension")).ToString();
                    listapedido.archivo = dr.GetValue(dr.GetOrdinal("Archivo")).ToString();

                    pedidoDescarga.Add(listapedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void InsertarDet(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int idTg)
        {
            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG",
                                        "@Tipo_VTA",
                                        "@id_CteNuevo",
                                        "@reCOrdCompra"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        dt.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"],
                                        idTg,
                                        "VI",
                                        pedido.Id_Cte,
                                        dt.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dt.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }


            for (int x = 0; x < dtRestos.Rows.Count; x++)
            {
                if (Convert.ToInt32(dtRestos.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dtRestos.Rows[x]["Id_Prd"],
                                        dtRestos.Rows[x]["Prd_Precio"],
                                        dtRestos.Rows[x]["Prd_Cantidad"],
                                        dtRestos.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dtRestos.Rows[x]["Acs_Doc"].ToString() ==""?"":dtRestos.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dtRestos.Rows[x]["Mod"] == null? 0 :   dtRestos.Rows[x]["Mod"],
                                        idTg,
                                        "NE",
                                        pedido.Id_Cte,
                                        dtRestos.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dtRestos.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);




        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? idTG)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                if (captado > 0)
                {
                    string[] Parametros2 = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Credito",
                                           "@Id_PedVI"
                                       };
                    object[] Valores2 = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        2,
                                        captado
                                     };

                    SqlCommand sqlcmd2 = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros2, Valores2);
                }

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Ped_Solicito",
                                        "@Requisicion",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",

                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",

                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",

                                        "@Id_Acs",
                                        "@Id_Ped",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                       
                                            //Llena la informacion de compras
                                        "@Acs_Contacto2",
                                        "@Acs_Telefono2",
                                        "@Acs_Email2",
                                        //Llena la información de almacen
                                        "@Acs_Contacto3",
                                        "@Acs_Telefono3",
                                        "@Acs_Email3",
                                        //Llena la información de Mantenimiento
                                        "@Acs_Contacto4",
                                        "@Acs_Telefono4",
                                        "@Acs_Email4",
                                        //Llena la información de Pagos
                                        "@Acs_Contacto5",
                                        "@Acs_Telefono5",
                                        "@Acs_Email5",

                                        //Secccion Requiere documentos
                                        "@Acs_ReqDocFolio",
                                        "@Acs_ReqDocReposicion",
                                        "@Acs_ReqDocOtro",

                                        "@Ped_ReqFactKey",
                                        "@Ped_ReqCopiaDoc",
                                        "@Ped_reqRemision",

                                        "@Acs_ReqFacturaKeyCop",
                                        "@Acs_ReqOrdencop",
                                        "@Acs_ReqOrdenrep",
                                        "@ACS_ReqRemisionCop",
                                        "@Acs_ReqCopiaCop",
                                        "@Acs_ReqDocFoliocop",
                                        "@UsoCFDI"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Ped_Solicito,
                                        pedido.Requisicion,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.Id_Ped,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys == null?  "" :  pedido.ReqAcys,
                                        pedido.OcAcys  == null?  "" :  pedido.OcAcys,
                                          //Llena la informacion de compras
                                        pedido.Acs_Contacto2 ,
                                        pedido.Acs_Telefono2,
                                        pedido.Acs_Email2 ,
                                        //Llena la información de almacen
                                        pedido.Acs_Contacto3 ,
                                        pedido.Acs_Telefono3 ,
                                        pedido.Acs_Email3 ,
                                        //Llena la información de Mantenimiento
                                        pedido.Acs_Contacto4 ,
                                        pedido.Acs_Telefono4 ,
                                        pedido.Acs_Email4 ,
                                        //Llena la información de Pagos
                                        pedido.Acs_Contacto5,
                                        pedido.Acs_Telefono5,
                                        pedido.Acs_Email5,
                                        //Secccion Requiere documentos
                                        pedido.Acs_ReqDocFolio ,
                                        pedido.Acs_ReqDocReposicion ,
                                        pedido.Acs_ReqDocOtro,

                                            pedido.Acs_ReqFacturaKey,
                                        pedido.Acs_ReqCopia,
                                        pedido.ACS_ReqRemision,

                                               pedido.Acs_ReqFacturaKeyCop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.ACS_ReqRemisionCop,
                                        pedido.Acs_ReqCopiaCop,
                                        pedido.Acs_ReqDocFoliocop,
                                        pedido.UsoCFDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Modificar2", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {

                    verificador = -1;
                    ModificarDet(pedido, dt, dtRestos, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG);


                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_CorrigeDet2", ref verificador, Parametros, Valores);

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                    CapaDatos.CommitTrans();

                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarCN(int id_u, int ID_cd, PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? idTG)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                if (captado > 0)
                {
                    string[] Parametros2 = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Credito",
                                           "@Id_PedVI"
                                       };
                    object[] Valores2 = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        2,
                                        captado
                                     };

                    SqlCommand sqlcmd2 = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros2, Valores2);
                }

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Ped_Solicito",
                                        "@Requisicion",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",

                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",

                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",

                                        "@Id_Acs",
                                        "@Id_Ped",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                       
                                            //Llena la informacion de compras
                                        "@Acs_Contacto2",
                                        "@Acs_Telefono2",
                                        "@Acs_Email2",
                                        //Llena la información de almacen
                                        "@Acs_Contacto3",
                                        "@Acs_Telefono3",
                                        "@Acs_Email3",
                                        //Llena la información de Mantenimiento
                                        "@Acs_Contacto4",
                                        "@Acs_Telefono4",
                                        "@Acs_Email4",
                                        //Llena la información de Pagos
                                        "@Acs_Contacto5",
                                        "@Acs_Telefono5",
                                        "@Acs_Email5",

                                        //Secccion Requiere documentos
                                        "@Acs_ReqDocFolio",
                                        "@Acs_ReqDocReposicion",
                                        "@Acs_ReqDocOtro",

                                        "@Ped_ReqFactKey",
                                        "@Ped_ReqCopiaDoc",
                                        "@Ped_reqRemision",

                                        "@Acs_ReqFacturaKeyCop",
                                        "@Acs_ReqOrdencop",
                                        "@Acs_ReqOrdenrep",
                                        "@ACS_ReqRemisionCop",
                                        "@Acs_ReqCopiaCop",
                                        "@Acs_ReqDocFoliocop"
                                        ,"@UsoCFDI"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Ped_Solicito,
                                        pedido.Requisicion,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.Id_Ped,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys == null?  "" :  pedido.ReqAcys,
                                        pedido.OcAcys  == null?  "" :  pedido.OcAcys,
                                          //Llena la informacion de compras
                                        pedido.Acs_Contacto2 ,
                                        pedido.Acs_Telefono2,
                                        pedido.Acs_Email2 ,
                                        //Llena la información de almacen
                                        pedido.Acs_Contacto3 ,
                                        pedido.Acs_Telefono3 ,
                                        pedido.Acs_Email3 ,
                                        //Llena la información de Mantenimiento
                                        pedido.Acs_Contacto4 ,
                                        pedido.Acs_Telefono4 ,
                                        pedido.Acs_Email4 ,
                                        //Llena la información de Pagos
                                        pedido.Acs_Contacto5,
                                        pedido.Acs_Telefono5,
                                        pedido.Acs_Email5,
                                        //Secccion Requiere documentos
                                        pedido.Acs_ReqDocFolio ,
                                        pedido.Acs_ReqDocReposicion ,
                                        pedido.Acs_ReqDocOtro,

                                            pedido.Acs_ReqFacturaKey,
                                        pedido.Acs_ReqCopia,
                                        pedido.ACS_ReqRemision,

                                               pedido.Acs_ReqFacturaKeyCop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.ACS_ReqRemisionCop,
                                        pedido.Acs_ReqCopiaCop,
                                        pedido.Acs_ReqDocFoliocop,
                                        pedido.UsoCFDI
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Modificar2", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    string[] ParametrosLog;
                    object[] ValoresLog;
                    int verificardorLog = 0;
                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                            "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                id_u,
                                ID_cd,
                                pedido.Requisicion.ToString(), // IdOC
                                0,
                                0,
                                "Se Actualizo el pedido: "+ pedido.Id_Ped.ToString()+" de IdOC="+pedido.IdOC.ToString(),
                                pedido.IdOC,
                                pedido.Id_Cte,
                                0
                            };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificardorLog, ParametrosLog, ValoresLog);

                    verificador = -1;

                    ModificarDetCN(id_u, ID_cd, pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);


                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_CorrigeDet2", ref verificador, Parametros, Valores);

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@FecAsig", "@UsrAsig", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Ped_Fecha, pedido.Id_U, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                    CapaDatos.CommitTrans();

                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        private static void ModificarDet(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int? idTg)
        {


            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);


            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG",
                                        "@Tipo_VTA",
                                        "@id_CteNuevo",
                                        "@reCOrdCompra"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        dt.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"],
                                        idTg,
                                        "VI",
                                        pedido.Id_Cte,
                                        dt.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dt.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }


            for (int x = 0; x < dtRestos.Rows.Count; x++)
            {
                if (Convert.ToInt32(dtRestos.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dtRestos.Rows[x]["Id_Prd"],
                                        dtRestos.Rows[x]["Prd_Precio"],
                                        dtRestos.Rows[x]["Prd_Cantidad"],
                                        dtRestos.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dtRestos.Rows[x]["Acs_Doc"].ToString() ==""?"":dtRestos.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dtRestos.Rows[x]["Mod"] == null? 0 :   dtRestos.Rows[x]["Mod"],
                                        idTg,
                                        "NE",
                                        pedido.Id_Cte,
                                        dtRestos.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dtRestos.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }
        }

        private static void ModificarDet2(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int? idTg)
        {


            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);


            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG",
                                        "@Tipo_VTA",
                                        "@id_CteNuevo",
                                        "@reCOrdCompra"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        dt.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"],
                                        idTg,
                                        "VI",
                                        pedido.Id_Cte,
                                        dt.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dt.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }
        }


        public void Actualizartokenportalcliente(ref string token, int tipo, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                                "@token",
                                                "@tipoConsulta"
                                              };
                object[] Valores = {
                                              token,
                                                tipo,
                                           };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Sp_Actualizartokenportalcliente", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    token = Convert.ToString(dr.GetValue(dr.GetOrdinal("token")));


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void logPortalCliente(int id_Cd, int PedidoExterno, string TipoAPi, int Estatus, string mensaje, ref int verificador, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                                  "@id_Cd"
                                              ,"@PedidoExterno"
                                              ,"@TipoAPi"
                                              ,"@Estatus"
                                              ,"@mensaje"
                                              };
                object[] Valores = {
                                            id_Cd,
                                            PedidoExterno,
                                            TipoAPi,
                                            Estatus,
                                            mensaje
                                           };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("WS_LoggerPoratl", ref verificador, Parametros, Valores);



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsutarlogPortalClienteRemision(int id_Cd, int PedidoExterno, string TipoAPi, int estatus, ref int verificador, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                                  "@id_Cd"
                                              ,"@PedidoExterno"
                                              ,"@TipoAPi"
                                             ,"@Estatus"
                                              };
                object[] Valores = {
                                            id_Cd,
                                            PedidoExterno,
                                            TipoAPi
                                           ,estatus
                                           };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("WS_ConsultarLoggerPortal", ref verificador, Parametros, Valores);



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarAutorizacion(PedidoVtaInst pedido, System.Data.DataTable dt, System.Data.DataTable dtRestos, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@ReqAcys",
                                        "@PedAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion",

                                           //Llena la informacion de compras
                                        "@Acs_Contacto2",
                                        "@Acs_Telefono2",
                                        "@Acs_Email2",
                                        //Llena la información de almacen
                                        "@Acs_Contacto3",
                                        "@Acs_Telefono3",
                                        "@Acs_Email3",
                                        //Llena la información de Mantenimiento
                                        "@Acs_Contacto4",
                                        "@Acs_Telefono4",
                                        "@Acs_Email4",
                                        //Llena la información de Pagos
                                        "@Acs_Contacto5",
                                        "@Acs_Telefono5",
                                        "@Acs_Email5",
                                        //Secccion Requiere documentos
                                        "@Acs_ReqDocFolio",
                                        "@Acs_ReqDocReposicion",
                                        "@Acs_ReqDocOtro",
                                        "@Ped_ReqFactKey ",
                                        "@Ped_ReqCopiaDoc",
                                        "@Ped_reqRemision",

                                        "@Acs_ReqFacturaKeyCop",
                                        "@Acs_ReqOrdencop",
                                        "@Acs_ReqOrdenrep",
                                        "@ACS_ReqRemisionCop",
                                        "@Acs_ReqCopiaCop",
                                        "@Acs_ReqDocFoliocop",
                                        "@id_cteDirEntrega"
            };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.ReqAcys,
                                        pedido.PedAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion,                                        
                                        //Llena la informacion de compras
                                        pedido.Acs_Contacto2 ,
                                        pedido.Acs_Telefono2,
                                        pedido.Acs_Email2 ,
                                        //Llena la información de almacen
                                        pedido.Acs_Contacto3 ,
                                        pedido.Acs_Telefono3 ,
                                        pedido.Acs_Email3 ,
                                        //Llena la información de Mantenimiento
                                        pedido.Acs_Contacto4 ,
                                        pedido.Acs_Telefono4 ,
                                        pedido.Acs_Email4 ,
                                        //Llena la información de Pagos
                                        pedido.Acs_Contacto5,
                                        pedido.Acs_Telefono5,
                                        pedido.Acs_Email5,
                                        //Secccion Requiere documentos
                                        pedido.Acs_ReqDocFolio ,
                                        pedido.Acs_ReqDocReposicion ,
                                        pedido.Acs_ReqDocOtro,

                                        pedido.Acs_ReqFacturaKey,
                                        pedido.Acs_ReqCopia,
                                        pedido.ACS_ReqRemision,

                                        pedido.Acs_ReqFacturaKeyCop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.Acs_ReqOrdencop,
                                        pedido.ACS_ReqRemisionCop,
                                        pedido.Acs_ReqCopiaCop,
                                        pedido.Acs_ReqDocFoliocop,
                                        pedido.id_cteDirEntrega
        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_SolicitudPedido", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    InsertarDetSol(pedido, dt, dtRestos, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG.Value);

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarSolicitudOrderCompra(PedidoVtaInst pedido, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Pedido"
                                       ,"@ID_Acys"
                                       ,"@OrdenCompra"
                                       ,"@ID_Cte"
                                       ,"@NombreDoc"
                                       ,"@Extension"
                                       ,"@Archivo"
                                       ,"@FechaDoc"
            };
                object[] Valores = {
                                   pedido.Id_Ped
                                  ,pedido.Id_Acs
                                  ,pedido.OrdenCompra
                                  ,pedido.Id_Cte
                                  ,pedido.nombreDocumento
                                  ,pedido.extension
                                  ,pedido.archivo
                                  ,DateTime.Now
        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_OrdenCompra", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private static void InsertarDetSol(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int idTg)
        {
            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG",
                                        "@Tipo_VTA",
                                        "@id_CteNuevo",
                                        "@reCOrdCompra"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        dt.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"],
                                        idTg,
                                        "VI",
                                        pedido.Id_Cte,
                                        dt.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dt.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapSolicituDet_Insertar", ref verificador, Parametros, Valores);

                }
            }


            for (int x = 0; x < dtRestos.Rows.Count; x++)
            {
                if (Convert.ToInt32(dtRestos.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dtRestos.Rows[x]["Id_Prd"],
                                        dtRestos.Rows[x]["Prd_Precio"],
                                        dtRestos.Rows[x]["Prd_Cantidad"],
                                        dtRestos.Rows.Count,
                                        pedido.Ped_AcysSemana,
                                        pedido.Id_Acs,
                                        pedido.Ped_AcysAnio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dtRestos.Rows[x]["Acs_Doc"].ToString() ==""?"":dtRestos.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dtRestos.Rows[x]["Mod"] == null? 0 :   dtRestos.Rows[x]["Mod"],
                                        idTg,
                                        "NE",
                                        pedido.Id_Cte,
                                        dtRestos.Rows[x]["ACS_ReqOC"].ToString() =="" ? 0 : (dtRestos.Rows[x]["ACS_ReqOC"].ToString() == "Sí" ? 1 : 0),
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapSolicituDet_Insertar", ref verificador, Parametros, Valores);

                }
            }
        }
        public void ConsultaSolicitudPedidos(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> Solicitud)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                    "@FechaInicial",
                                    "@FechaFinal",
                                    "@Estatus",
                                    "@Id_Cte"
                                      };
                object[] Valores = {
                                        pedido.FechaInicial,
                                        pedido.FechaFinal,
                                        pedido.Estatus,
                                        pedido.Id_Cte != 0? (object)null:   pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedSolicitud_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.id_Sol = (int)dr.GetValue(dr.GetOrdinal("id_Sol"));
                    pedido.Ped_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_Fecha"));
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Estatus = Convert.ToString(dr.GetValue(dr.GetOrdinal("Estatus")));
                    pedido.Id_U = (int)dr.GetValue(dr.GetOrdinal("id_u"));
                    pedido.Id_Ped = dr.IsDBNull(dr.GetOrdinal("id_ped")) ? 0 : Convert.ToInt32(dr["id_ped"]);
                    if (pedido.Estatus == "P")
                    {
                        pedido.estatusSTR = "Pendiente";
                    }
                    else if (pedido.Estatus == "A")
                    {
                        pedido.estatusSTR = "Autorizado";
                    }
                    else if (pedido.Estatus == "C")
                    {
                        pedido.estatusSTR = "Autorizado y Captado";
                    }
                    else
                    {
                        pedido.estatusSTR = "Rechazado";
                    }
                    Solicitud.Add(pedido);

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSolicitudPedido(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                        "@Id_Emp"   ,
                                        "@Id_Cd"    ,
                                        "@Id_Sol"   ,
                                        "@Estatus"  ,
                                        "@ID_U",
                                        "@Id_Ped"
                                      };
                object[] Valores = {    pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.id_Sol,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Id_Ped == 0? (object)null: pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidoSolicitud_Actualizar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarLogHistPedido(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                        "@Id_Emp"   ,
                                        "@Id_Cd"    ,
                                        "@FechaInicial"   ,
                                        "@FechaFinal"
                                      };
                object[] Valores = {    pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.FechaInicial,
                                        pedido.FechaFinal

                                   };
                PedidoVtaInst datos;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLogPedSolicitud_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    datos = new PedidoVtaInst();
                    datos.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr["Id_Emp"]);
                    datos.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr["Id_Cd"]);
                    datos.ID_sol = dr.IsDBNull(dr.GetOrdinal("Id_Sol")) ? 0 : Convert.ToInt32(dr["Id_Sol"]);
                    datos.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr["Id_U"]);
                    datos.U_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr["U_Nombre"]);
                    datos.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("fecha")) ? DateTime.MinValue : Convert.ToDateTime(dr["fecha"]);
                    datos.Estatus = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : Convert.ToString(dr["Estatus"]);
                    datos.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr["Id_Cte"]);
                    datos.Cte_Nom = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr["Cte_NomComercial"]);

                    if (datos.Estatus == "P")
                    {
                        datos.estatusSTR = "Pendiente";
                    }
                    else if (datos.Estatus == "A")
                    {
                        datos.estatusSTR = "Autorizado";
                    }
                    else if (datos.Estatus == "C")
                    {
                        datos.estatusSTR = "Autorizado y Captado";
                    }
                    else
                    {
                        datos.estatusSTR = "Rechazado";
                    }

                    List.Add(datos);

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidosAutorizados(PedidoVtaInst pedido, string Conexion, ref int Verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            try
            {
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                    "@Id_emp",
                                    "@Id_Cd",
                                    "@Id_Ped"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPed_ConsultaPedidoAut", ref Verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoSustituto(PedidoVtaInst Registro, ref List<PedidoVtaInst> Lista, string conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

            string[] Parametros = { "@Id_Cd",
                                    "@Id_Cte",
                                    "@Id_Prd",
                                    "@id_ped"};
            object[] Valores = { Registro.Id_Cd,
                                 Registro.Id_Cte,
                                 Registro.Id_Prd,
                                 Registro.Id_Ped};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPortalkey_ProdSustituto", ref dr, Parametros, Valores);
            PedidoVtaInst detalle;
            while (dr.Read())
            {
                detalle = new PedidoVtaInst();
                detalle.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("id_prd")));
                detalle.prodOriginal = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("prodOriginal")));
                detalle.cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ped_cantidad")));
                detalle.Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ped_precio")));
                detalle.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));

                detalle.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cte")));

                detalle.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                detalle.Prd_Presentacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));
                detalle.ClaveProdServ = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")));
                detalle.ClaveUnidad = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")));
                detalle.Prd_UniNe = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_UniNe")));
                detalle.Prd_UniNs = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_UniNs")));

                Lista.Add(detalle);
            }
            dr.Close();
            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }


        #region OrdencompraCentral

        public void InsertarOrdenCompraCentral(int Id_U, int Id_Cd, PedidoVtaInst pedido, System.Data.DataTable dt, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            int verificardorLog = 0;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion",
                                        "@UsoCFDI"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion
                                        ,pedido.UsoCFDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Garantias", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                            "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                            "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(), // IdOC
                                0,
                                0,
                                "Se crea pedido: "+ pedido.Id_Ped.ToString()+" de IdOC="+pedido.IdOC.ToString(),
                                pedido.IdOC,
                                pedido.Id_Cte,
                                0
                            };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificardorLog, ParametrosLog, ValoresLog);

                    verificador = -1;
                    // ====================
                    // INSERTAR DETALLE
                    // ====================
                    InsertarDet_v2(Id_U, Id_Cd, pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                              "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                0,
                                0,
                                "Error en creación de pedido IdOC:"+pedido.IdOC.ToString(),
                                pedido.IdOC,
                                  pedido.Id_Cte,
                                0
                            };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificardorLog, ParametrosLog, ValoresLog);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }




        public void InsertarOrdenCompraCentralSolicitud(int Id_U, int Id_Cd, PedidoVtaInst pedido, System.Data.DataTable dt, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            int verificardorLog = 0;

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel",
                                        "@Ped_SolicitoEmail",
                                        "@Ped_SolicitoPuesto",
                                        "@Ped_ConsignadoCalle",
                                        "@Ped_ConsignadoNo",
                                        "@Ped_ConsignadoCp",
                                        "@Ped_ConsignadoMunicipio",
                                        "@Ped_ConsignadoEstado",
                                        "@Ped_ConsignadoColonia",
                                        "@Ped_ReqOrden",
                                        "@Ped_OrdenCompra",
                                        "@Ped_AcysSemana",
                                        "@Ped_AcysAnio",
                                        "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys",
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel,
                                        pedido.Ped_SolicitoEmail,
                                        pedido.Ped_SolicitoPuesto,
                                        pedido.Ped_ConsignadoCalle,
                                        pedido.Ped_ConsignadoNo,
                                        pedido.Ped_ConsignadoCp,
                                        pedido.Ped_ConsignadoMunicipio,
                                        pedido.Ped_ConsignadoEstado,
                                        pedido.Ped_ConsignadoColonia,
                                        pedido.Ped_ReqOrden,
                                        pedido.Ped_OrdenCompra,
                                        pedido.Ped_AcysSemana,
                                        pedido.Ped_AcysAnio,
                                        pedido.Id_Acs,
                                        pedido.FechaFacAcys,
                                        pedido.PedAcys,
                                        pedido.ReqAcys,
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Solicitud", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {


                    CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                              "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(), // IdOC
                                0,
                                0,
                                "Se la solicitud: "+ pedido.Id_Ped.ToString()+" de IdOC="+pedido.IdOC.ToString(),
                                pedido.IdOC,
                                  pedido.Id_Cte,
                               0
                            };
                    SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificardorLog, ParametrosLog, ValoresLog);
                    CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);
                    verificador = -1;
                    // ====================
                    // INSERTAR DETALLE
                    // ====================
                    InsertarDet_Solicitud(Id_U, Id_Cd, pedido, dt, ref verificador, Conexion);

                }
                else
                {
                    CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                    // Registro de Movimiento en LOG 
                    ParametrosLog = new string[] {
                               "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                    ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                0,
                                0,
                                "Error en creación de pedido IdOC:"+pedido.IdOC.ToString(),
                                pedido.IdOC,
                                  pedido.Id_Cte,
                               0
                            };
                    SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificardorLog, ParametrosLog, ValoresLog);
                    CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private static void InsertarDet_Solicitud(int Id_U, int Id_Cd, PedidoVtaInst pedido, DataTable dt, ref int verificador, string Conexion)
        {

            int verificadorLog = 0;

            if (dt.Rows.Count == 0) return;



            string[] Parametros;
            object[] Valores;

            string[] ParametrosLog;
            object[] ValoresLog;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                      "@Id_PrdOri",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.CD_Datos CapaDatos3 = new CapaDatos.CD_Datos(Conexion);

                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {

                    if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) != Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]))
                    {
                        // LOG
                        // Movimientos
                        ParametrosLog = new string[] {
                               "@Id_Emp",
                            "@Id_U",
                            "@Id_Cd",
                            "@IdOC",
                            "@Id_Prd",
                            "@Id_Prd_Sustituto",
                            "@Notas",
                            "@Requisicion",
                           "@Id_Cte",
                            "@Cantidad"
                            };
                        ValoresLog = new object[] {
                                pedido.Id_Emp,
                                Id_U,
                                Id_Cd,
                                pedido.Requisicion.ToString(),
                                Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]),
                                Convert.ToInt64(dt.Rows[x]["Id_Prd"]),
                                "'Se sustituye producto "+  Convert.ToString(dt.Rows[x]["Id_PrdOld"]) +" por "+ Convert.ToString(dt.Rows[x]["Id_Prd"]),
                                pedido.IdOC,
                                  pedido.Id_Cte,
                                dt.Rows[x]["Prd_Cantidad"]
                            };
                        SqlCommand sqlcmd = CapaDatos2.GenerarSqlCommand("spCatCNac_OrdenCompra_Log", ref verificadorLog, ParametrosLog, ValoresLog);

                        CapaDatos2.LimpiarSqlcommand(ref sqlcmd);
                    }

                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_PrdOld"],
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                       dt.Rows[x]["Mod"] == null? 0 :   dt.Rows[x]["Mod"]
                                   };
                    SqlCommand sqlcmd2 = CapaDatos3.GenerarSqlCommand("spCapPedidoSolicitudDet_Insertar", ref verificador, Parametros, Valores);
                    CapaDatos3.LimpiarSqlcommand(ref sqlcmd2);
                }
            }
        }


        public void ConsultaSolicitud(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> Solicitud)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                    "@FechaInicial",
                                    "@FechaFinal",
                                    "@Estatus"
                                      };
                object[] Valores = {
                                        pedido.FechaInicial,
                                        pedido.FechaFinal,
                                        pedido.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPedidoSolicitud_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.id_Sol = (int)dr.GetValue(dr.GetOrdinal("id_Sol"));
                    pedido.Ped_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_Fecha"));
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.OrdenCompra = (dr.GetValue(dr.GetOrdinal("Ped_Requisicion"))).ToString();
                    pedido.Estatus = Convert.ToString(dr.GetValue(dr.GetOrdinal("Estatus")));

                    if (pedido.Estatus == "P")
                    {
                        pedido.estatusSTR = "Pendiente";
                    }
                    else if (pedido.Estatus == "A")
                    {
                        pedido.estatusSTR = "Autorizado";
                    }
                    else
                    {
                        pedido.estatusSTR = "Rechazado";
                    }
                    Solicitud.Add(pedido);

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudDet(PedidoDet pedido, string Conexion, ref List<PedidoDet> ListaPed)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                    "@id_sol"
                                      };
                object[] Valores = {
                                        pedido.id_sol
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPedidoSolicitud_ConsultaDet", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoDet();
                    pedido.id_sol = (int)dr.GetValue(dr.GetOrdinal("Id_Solicitud"));
                    pedido.Id_PrdOri = (Int64)dr.GetValue(dr.GetOrdinal("Id_PrdOri"));
                    pedido.Prd_DescripcionOri = (string)dr.GetValue(dr.GetOrdinal("descripcion_ori"));
                    pedido.Id_Prd = (Int64)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    pedido.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    pedido.Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Precio")));
                    pedido.Ped_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    pedido.AAA = 0;

                    ListaPed.Add(pedido);

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizarSolicitud(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] ParametrosLog;
                object[] ValoresLog;

                string[] Parametros = {
                                     "@Id_Sol",
                                    "@Estatus"
                                      };
                object[] Valores = {
                                        pedido.id_Sol,
                                        pedido.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPedidoSolicitud_Actualizar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ReporteCaptacionPedido(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@fechaInicial",
                                          "@fechaFinal",
                                          "@Filtro_tipoPed"
                                      };
                object[] Valores = {

                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.FechaInicial,
                                       pedido.FechaFinal,
                                       pedido.Filtro_tipoPed  == "" ? (object)null : pedido.Filtro_tipoPed
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_MonitorPedido", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.cantidad = (int)dr.GetValue(dr.GetOrdinal("Cantidad"));
                    pedido.Filtro_Frecuencia = (string)dr.GetValue(dr.GetOrdinal("str_tipo_pedido"));

                    if (pedido.Filtro_Frecuencia == "Facturacion Directa")
                    {
                        pedido.Filtro_Frecuencia = "Remisiones / Refacturación";
                    }
                    pedido.TotalFacturacion = (double)dr.GetValue(dr.GetOrdinal("total"));
                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}