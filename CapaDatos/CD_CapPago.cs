using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapPago
    {
        public void ConsultarCantidadPagosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapPagosCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago(Pago pago, string Conexion, ref List<Pago> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_Estatus",
                                          "@Filtro_PagIni",
                                          "@Filtro_PagFin",
                                          "@Filtro_usuario",
                                          "@Filtro_Extemporaneo",
                                          "@Filtro_ClienteIni",
                                          "@Filtro_ClienteFin"

                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Filtro_FecIni ,
                                       pago.Filtro_FecFin ,
                                       pago.Filtro_Estatus== ""? (object)null:pago.Filtro_Estatus,
                                       pago.Filtro_PagIni== ""? (object)null:pago.Filtro_PagIni,
                                       pago.Filtro_PagFin== ""? (object)null:pago.Filtro_PagFin,
                                       pago.Filtro_usuario== ""? (object)null:pago.Filtro_usuario,
                                       pago.Filtro_Extemporaneo == -1?(object)null:pago.Filtro_Extemporaneo,
                                       string.IsNullOrEmpty(pago.Filtro_ClienteIni)?(object)null:pago.Filtro_ClienteIni,
                                       string.IsNullOrEmpty(pago.Filtro_ClienteFin)?(object)null:pago.Filtro_ClienteFin,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Lista", ref dr, Parametros, Valores);

                Pago p;
                while (dr.Read())
                {
                    p = new Pago();
                    p.Pag_TipoStr = "Pago";
                    p.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    p.Pag_Estatus = dr.IsDBNull(dr.GetOrdinal("Pag_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Pag_Estatus")).ToString();
                    p.Pag_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Pag_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Pag_Estatus")).ToString());
                    p.Id_Pag = dr.IsDBNull(dr.GetOrdinal("Id_Pag")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                    p.Pag_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Pag_Fecha")));
                    p.Pag_Total = dr.IsDBNull(dr.GetOrdinal("Pag_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pag_Total")));
                    p.Filtro_usuario = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    p.Pag_ExtemporaneoStr = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pag_ExtemporaneoStr")));
                    p.Pag_Extemporaneo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Pag_Extemporaneo")));
                    p.Id_CdOrigenStr = dr.GetValue(dr.GetOrdinal("Id_CdOrigenStr")).ToString();
                    p.Id_PagExt = dr.GetValue(dr.GetOrdinal("Id_PagOrigen")).ToString();
                    p.Ejecutor = dr.GetValue(dr.GetOrdinal("Ejecutor")).ToString();
                    p.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")).ToString());
                    p.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString());
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Estatus(string p)
        {
            switch (p)
            {
                case "C": return "Capturado";
                case "I": return "Impreso";
                case "B": return "Baja";
                default: return "";
            }
        }

        public void ConsultaPagoFicha(ref Factura ficha, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Serie",
                                          "@Id_Ref"
                                      };
                object[] Valores = {
                                       ficha.Id_Emp,
                                       ficha.Id_Cd,
                                       ficha.Serie,
                                       ficha.Id_FacSerie
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaFicha_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    ficha.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    ficha.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    ficha.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    ficha.Fac_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fac_Fecha")));
                    ficha.Fac_Estatus = dr.IsDBNull(dr.GetOrdinal("Fac_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Fac_Estatus")).ToString();
                    ficha.Fac_Saldo = dr.IsDBNull(dr.GetOrdinal("Fac_Saldo")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Saldo")));
                    ficha.Fac_Pagado = dr.IsDBNull(dr.GetOrdinal("Fac_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    ficha.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ficha.Fac_FolioFiscal = dr.GetValue(dr.GetOrdinal("Fac_FolioFiscal")).ToString();
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public void ConsultaPagoNotaFicha(ref NotaCargo ficha, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Serie",
                                          "@Id_Ref"
                                      };

                object[] Valores = {
                                       ficha.Id_Emp,
                                       ficha.Id_Cd,
                                       ficha.Serie,
                                       ficha.Id_Nca
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoFicha_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    ficha.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    ficha.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    ficha.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    ficha.Nca_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Nca_Fecha")));
                    ficha.Nca_Estatus = dr.IsDBNull(dr.GetOrdinal("Nca_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Nca_Estatus")).ToString();
                    ficha.Importe = dr.IsDBNull(dr.GetOrdinal("Nca_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Importe")));
                    ficha.Nca_Pagado = dr.IsDBNull(dr.GetOrdinal("Nca_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nca_Pagado")));
                    ficha.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ficha.Nca_FolioFiscal = dr.GetValue(dr.GetOrdinal("Nca_FolioFiscal")).ToString();
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Tipo",
                                          "@Pag_Fecha",
                                          "@Id_Tmov",
                                          "@Pag_Importe",
                                          "@Pag_Total",
                                          "@Id_U",
                                          "@Pag_Estatus",
                                          "@Pag_Extemporaneo"
                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Tipo,
                                       pago.Pag_Fecha,
                                       pago.Id_Tmov,
                                       pago.Pag_Importe,
                                       pago.Pag_Total,
                                       pago.Id_U,
                                       pago.Pag_Estatus,
                                       pago.Pag_Extemporaneo
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Insertar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    pago.Id_Pag = verificador;
                    Parametros = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_Pag",
                            "@Num",
                            "@Id_Ban",
                            "@Fecha",
                            "@Importe",
                            "@NumOperacion",
                            "@Pag_RefBancaria"
                    };
                    for (int x = 0; x < list_fichas.Count; x++)
                    {
                        Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            pago.Id_Pag,
                            list_fichas[x].Pag_Ficha,
                            list_fichas[x].Id_Ban,
                            list_fichas[x].Pag_Fecha,
                            list_fichas[x].Pag_Importe,
                            list_fichas[x].Pag_NumOperacion,
                            list_fichas[x].Pag_RefBancaria
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Insertar", ref verificador, Parametros, Valores);
                    }


                    if (verificador == 1)
                    {
                        Parametros = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Serie",
                            "@Id_Pag",
                            "@Id_PagDet",
                            "@Mov",
                            "@Ref",
                            "@Ficha",
                            "@Cheque",
                            "@Importe",
                            "@Fecha",
                            "@Pag_Id_Cd",
                            "@Pag_Id_Ter",
                            "@Pag_Doc_Fecha",
                            "@Pag_Id_Cte",
                            "@Pag_Cte_Nombre"

                    };
                        for (int x = 0; x < list_pagos.Count; x++)
                        {
                            Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            list_pagos[x].Serie.ToString().ToUpper(),
                            pago.Id_Pag,
                            x,
                            list_pagos[x].Mov,
                            list_pagos[x].Ref,
                            list_pagos[x].Ficha,
                            list_pagos[x].Cheque,
                            list_pagos[x].Importe,
                            pago.Pag_Fecha,
                            list_pagos[x].Pag_Id_cd,
                            list_pagos[x].Pag_Id_Ter,
                            list_pagos[x].Pag_Fac_Fecha,
                            list_pagos[x].Pag_Id_cte,
                            list_pagos[x].Pag_Cte_Nombre,
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Insertar", ref verificador, Parametros, Valores);
                        }

                    }
                    CapaDatos.CommitTrans();
                    verificador = pago.Id_Pag;
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void ModificarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador, ref List<int> centros)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                ObtenerCentros(pago, ref centros, Conexion);

                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Pag", "@Tipo", "@Pag_Fecha", "@Id_Tmov", "@Pag_Importe", "@Pag_Total", "@Id_U", "@Pag_Estatus" };
                object[] Valores = { pago.Id_Emp, pago.Id_Cd, pago.Id_Pag, pago.Tipo, pago.Pag_Fecha, pago.Id_Tmov, pago.Pag_Importe, pago.Pag_Total, pago.Id_U, pago.Pag_Estatus, };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Modificar", ref verificador, Parametros, Valores);

                if (verificador == 1)
                {
                    Parametros = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Id_Pag",
                            "@Num",
                            "@Id_Ban",
                            "@Fecha",
                            "@Importe",
                            "@NumOperacion",
                            "@Pag_RefBancaria"
                    };
                    for (int x = 0; x < list_fichas.Count; x++)
                    {
                        Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            pago.Id_Pag,
                            x+1,
                            list_fichas[x].Id_Ban,
                            list_fichas[x].Pag_Fecha,
                            list_fichas[x].Pag_Importe,
                            list_fichas[x].Pag_NumOperacion,
                            list_fichas[x].Pag_RefBancaria
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Insertar", ref verificador, Parametros, Valores);
                    }


                    if (verificador == 1)
                    {
                        Parametros = new string[] {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Serie",
                            "@Id_Pag",
                            "@Id_PagDet",
                            "@Mov",
                            "@Ref",
                            "@Ficha",
                            "@Cheque",
                            "@Importe",
                            "@Fecha",
                            "@Pag_Id_Cd",
                            "@Pag_Id_Ter",
                            "@Pag_Doc_Fecha",
                            "@Pag_Id_Cte",
                            "@Pag_Cte_Nombre"
                    };
                        for (int x = 0; x < list_pagos.Count; x++)
                        {
                            Valores = new object[] {
                            pago.Id_Emp,
                            pago.Id_Cd,
                            list_pagos[x].Serie.ToString().ToUpper(),
                            pago.Id_Pag,
                            x,
                            list_pagos[x].Mov,
                            list_pagos[x].Ref,
                            list_pagos[x].Ficha,
                            list_pagos[x].Cheque,
                            list_pagos[x].Importe,
                            pago.Pag_Fecha,
                            list_pagos[x].Pag_Id_cd,
                            list_pagos[x].Pag_Id_Ter,
                            list_pagos[x].Pag_Fac_Fecha,
                            list_pagos[x].Pag_Id_cte,
                            list_pagos[x].Pag_Cte_Nombre,
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Insertar", ref verificador, Parametros, Valores);
                        }

                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private void ObtenerCentros(Pago pago, ref List<int> centros, string Conexion)
        {
            CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            string[] ParametrosCentros = { "@Id_Emp", "@Id_Cd", "@Id_Pag" };
            object[] ValoresCentros = { pago.Id_Emp, pago.Id_Cd, pago.Id_Pag };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar", ref dr, ParametrosCentros, ValoresCentros);

            int cen = 0;
            while (dr.Read())
            {
                cen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
                if (!centros.Contains(cen) && cen != pago.Id_Cd)
                {
                    centros.Add(cen);
                }
            }
            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaPago(ref Pago pago, ref List<Banco_Ficha> list_fichas, ref List<PagoDet> list_pagos, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Id_Pag
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Consultar", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pago.Tipo = dr.IsDBNull(dr.GetOrdinal("Pag_Tipo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Pag_Tipo"));
                    pago.Pag_Fecha = dr.IsDBNull(dr.GetOrdinal("Pag_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Pag_Fecha"));
                    pago.Id_Tmov = dr.IsDBNull(dr.GetOrdinal("Id_Tm")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Tm"));
                    pago.Pag_Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Importe"));
                    pago.Pag_Total = dr.IsDBNull(dr.GetOrdinal("Pag_Total")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Total"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista de FICHAS
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Consultar", ref dr, Parametros, Valores);
                Banco_Ficha ficha = default(Banco_Ficha);
                while (dr.Read())
                {
                    ficha = new Banco_Ficha();
                    ficha.Pag_Ficha = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : dr.GetInt32(dr.GetOrdinal("Pag_Ficha"));
                    ficha.Pag_Fecha = dr.IsDBNull(dr.GetOrdinal("Pag_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Pag_Fecha"));
                    ficha.Id_Ban = dr.IsDBNull(dr.GetOrdinal("Id_Ban")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ban"));
                    ficha.Ban_Nombre = dr.IsDBNull(dr.GetOrdinal("Ban_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Ban_Nombre"));
                    ficha.Pag_Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Importe"));
                    ficha.Ban_Cuenta = dr.IsDBNull(dr.GetOrdinal("Ban_Cuenta")) ? "" : dr.GetString(dr.GetOrdinal("Ban_Cuenta"));
                    ficha.Pag_NumOperacion = dr.IsDBNull(dr.GetOrdinal("Pag_NumOperacion")) ? "" : dr.GetString(dr.GetOrdinal("Pag_NumOperacion"));
                    ficha.Pag_RefBancaria = dr.IsDBNull(dr.GetOrdinal("Pag_RefBancaria")) ? "" : dr.GetString(dr.GetOrdinal("Pag_RefBancaria"));
                    list_fichas.Add(ficha);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista de DETALLES
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar", ref dr, Parametros, Valores);
                PagoDet detalle = default(PagoDet);
                while (dr.Read())
                {
                    detalle = new PagoDet();
                    detalle.Mov = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento")));
                    detalle.MovStr = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? "" : (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento"))) == 1 ? "Factura" : "Nota de cargo");
                    detalle.Ref = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    detalle.Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Fac_FolioFiscal = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscal")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscal"));
                    detalle.Id_Terr = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalle.Pag_Id_cd = dr.IsDBNull(dr.GetOrdinal("Pag_Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
                    detalle.Doc_Fecha = dr.IsDBNull(dr.GetOrdinal("Fac_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    detalle.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    detalle.Cte_Nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    detalle.Pag_Numero = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Ficha")));
                    detalle.Pag_Cheque = dr.IsDBNull(dr.GetOrdinal("Pag_Cheque")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Cheque"));
                    detalle.Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pag_Importe")));
                    detalle.Doc_Estatus = dr.IsDBNull(dr.GetOrdinal("Fac_Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Estatus"));
                    detalle.Doc_Importe = dr.IsDBNull(dr.GetOrdinal("Fac_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    detalle.Doc_Pagado = dr.IsDBNull(dr.GetOrdinal("Fac_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    detalle.Cte_Rfc = dr.IsDBNull(dr.GetOrdinal("CteRFC")) ? "" : dr.GetString(dr.GetOrdinal("CteRFC"));
                    list_pagos.Add(detalle);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaPago2(ref Pago pago, ref List<Banco_Ficha> list_fichas, ref List<PagoDet> list_pagos, string Conexion, ref List<PagoDetComplemento> List_DetComplemento)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Id_Pag
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Consultar", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pago.Tipo = dr.IsDBNull(dr.GetOrdinal("Pag_Tipo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Pag_Tipo"));
                    pago.Pag_Fecha = dr.IsDBNull(dr.GetOrdinal("Pag_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Pag_Fecha"));
                    pago.Id_Tmov = dr.IsDBNull(dr.GetOrdinal("Id_Tm")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Tm"));
                    pago.Pag_Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Importe"));
                    pago.Pag_Total = dr.IsDBNull(dr.GetOrdinal("Pag_Total")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Total"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista de FICHAS
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_Consultar", ref dr, Parametros, Valores);
                Banco_Ficha ficha = default(Banco_Ficha);
                while (dr.Read())
                {
                    ficha = new Banco_Ficha();
                    ficha.Pag_Ficha = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : dr.GetInt32(dr.GetOrdinal("Pag_Ficha"));
                    ficha.Pag_Fecha = dr.IsDBNull(dr.GetOrdinal("Pag_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Pag_Fecha"));
                    ficha.Id_Ban = dr.IsDBNull(dr.GetOrdinal("Id_Ban")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ban"));
                    ficha.Ban_Nombre = dr.IsDBNull(dr.GetOrdinal("Ban_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Ban_Nombre"));
                    ficha.Pag_Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Importe"));
                    ficha.Ban_Cuenta = dr.IsDBNull(dr.GetOrdinal("Ban_Cuenta")) ? "" : dr.GetString(dr.GetOrdinal("Ban_Cuenta"));
                    ficha.Pag_NumOperacion = dr.IsDBNull(dr.GetOrdinal("Pag_NumOperacion")) ? "" : dr.GetString(dr.GetOrdinal("Pag_NumOperacion"));
                    ficha.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    ficha.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    list_fichas.Add(ficha);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista de DETALLES
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar3", ref dr, Parametros, Valores);
                PagoDet detalle = default(PagoDet);
                while (dr.Read())
                {
                    detalle = new PagoDet();
                    detalle.Mov = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento")));
                    detalle.MovStr = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? "" : (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento"))) == 1 ? "Factura" : "Nota de cargo");
                    detalle.Ref = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    detalle.Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Fac_FolioFiscal = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscal")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscal"));
                    detalle.Fac_Xml = dr.IsDBNull(dr.GetOrdinal("Fac_Xml")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Xml"));
                    detalle.Id_Terr = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalle.Pag_Id_cd = dr.IsDBNull(dr.GetOrdinal("Pag_Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
                    detalle.Doc_Fecha = dr.IsDBNull(dr.GetOrdinal("Fac_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    detalle.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    detalle.Cte_Nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    detalle.Pag_Numero = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Ficha")));
                    detalle.Pag_Cheque = dr.IsDBNull(dr.GetOrdinal("Pag_Cheque")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Cheque"));
                    detalle.Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pag_Importe")));
                    detalle.Doc_Estatus = dr.IsDBNull(dr.GetOrdinal("Fac_Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Estatus"));
                    detalle.Doc_Importe = dr.IsDBNull(dr.GetOrdinal("Fac_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    detalle.Doc_Pagado = dr.IsDBNull(dr.GetOrdinal("Fac_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    detalle.Cte_Rfc = dr.IsDBNull(dr.GetOrdinal("CteRFC")) ? "" : dr.GetString(dr.GetOrdinal("CteRFC"));
                    detalle.Pag_Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    detalle.Id_Pag = dr.IsDBNull(dr.GetOrdinal("Id_Pag")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                    detalle.Pag_Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? string.Empty : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Id_PagDet = dr.IsDBNull(dr.GetOrdinal("Id_PagDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                    list_pagos.Add(detalle);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista pagos anteriores
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("ConsultaSaldosCFDI", ref dr, Parametros, Valores);
                PagoDetComplemento DetComplemento = default(PagoDetComplemento);
                while (dr.Read())
                {
                    DetComplemento = new PagoDetComplemento();
                    DetComplemento.Pago_Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    DetComplemento.pag_referencia = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    DetComplemento.Total_Pagado_Timbrado = dr.IsDBNull(dr.GetOrdinal("ImportePagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ImportePagado")));
                    DetComplemento.Parcialidad_Timbrada = dr.IsDBNull(dr.GetOrdinal("Parcialidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Parcialidad"));
                    List_DetComplemento.Add(DetComplemento);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaListaPagos(ref Pago pago, ref List<PagoDet> list_pagos, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Id_Pag
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Lista de DETALLES
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar2", ref dr, Parametros, Valores);
                PagoDet detalle = default(PagoDet);
                while (dr.Read())
                {
                    detalle = new PagoDet();
                    detalle.Mov = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento")));
                    detalle.MovStr = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? "" : (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento"))) == 1 ? "Factura" : "Nota de cargo");
                    detalle.Ref = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    detalle.Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Fac_FolioFiscal = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscal")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscal"));
                    detalle.Id_Terr = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalle.Pag_Id_cd = dr.IsDBNull(dr.GetOrdinal("Pag_Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
                    detalle.Doc_Fecha = dr.IsDBNull(dr.GetOrdinal("Fac_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    detalle.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    detalle.Cte_Nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    detalle.Pag_Numero = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Ficha")));
                    detalle.Pag_Cheque = dr.IsDBNull(dr.GetOrdinal("Pag_Cheque")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Cheque"));
                    detalle.Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pag_Importe")));
                    detalle.Doc_Estatus = dr.IsDBNull(dr.GetOrdinal("Fac_Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Estatus"));
                    detalle.Doc_Importe = dr.IsDBNull(dr.GetOrdinal("Fac_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    detalle.Doc_Pagado = dr.IsDBNull(dr.GetOrdinal("Fac_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    detalle.Cte_Rfc = dr.IsDBNull(dr.GetOrdinal("CteRFC")) ? "" : dr.GetString(dr.GetOrdinal("CteRFC"));
                    detalle.Pag_Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    detalle.Id_Pag = dr.IsDBNull(dr.GetOrdinal("Id_Pag")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                    detalle.Pag_Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? string.Empty : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Id_PagDet = dr.IsDBNull(dr.GetOrdinal("Id_PagDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                    list_pagos.Add(detalle);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void Baja(Pago pag, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Pag"
                                      };
                object[] Valores = {
                                        pag.Id_Emp,
                                        pag.Id_Cd,
                                        pag.Id_Pag
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Baja", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pago pag, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Pag"
                                      };
                object[] Valores = {
                                        pag.Id_Emp,
                                        pag.Id_Cd,
                                        pag.Id_Pag
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Imprimir", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaReferenciaBancaria(Sesion sesion, int banco, string Pag_RefBancaria, ref string ValidaReferencia)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            SqlDataReader dr = null;


            try
            {
                string[] Parametros = {
                                        "@Id_Ban",
                                        "@Pag_RefBancaria",
                                      };
                object[] Valores = {
                                        banco,
                                        Pag_RefBancaria
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_ValidaReferenciaBancaria", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ValidaReferencia = dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Mensaje")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ConsultaCuentaBanco(Sesion sesion, int banco, ref string cuenta)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Banco"
                                      };
                object[] Valores = {
                                        sesion.Id_Emp,
                                        sesion.Id_Cd,
                                        banco
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_ConsultarCuenta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    cuenta = dr.IsDBNull(dr.GetOrdinal("Ban_Cuenta")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Ban_Cuenta")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PermitirExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Fecha"
                                      };
                object[] Valores = {
                                        pag.Id_Emp,
                                        pag.Id_Cd,
                                        pag.Pag_Fecha == default(DateTime)?(DateTime?)null:pag.Pag_Fecha
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPagoExtemporaneo_Permitir", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CierreExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Fecha"
                                      };
                object[] Valores = {
                                        pag.Id_Emp,
                                        pag.Id_Cd,
                                        pag.Pag_Fecha
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreExtemporaneo", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentro(int Id_Emp, string serie, ref DbCentro centro, string ConexionCob, int Tipo_CDC)
        {
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(ConexionCob);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Db_Serie", "@Cd_Tipo" };
                object[] Valores = { Id_Emp, serie, Tipo_CDC };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCatDb_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    centro.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    centro.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    centro.Db_Nombre = dr.GetValue(dr.GetOrdinal("Db_Nombre")).ToString();
                    centro.Db_CdNombre = dr.GetValue(dr.GetOrdinal("Db_CdNombre")).ToString();
                    centro.Db_CerradoExtemporaneo = dr.IsDBNull(dr.GetOrdinal("Cal_FechaExtemporaneo")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cal_FechaExtemporaneo")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region PagoDetComplemento

        public void ConsultarPago(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_PagDet",
                                      "@id_pagcomp"};
            object[] Valores = { pagoDetComplemento.Id_Emp,
                                   pagoDetComplemento.Id_Cd,
                                   pagoDetComplemento.Id_Pag,
                                   pagoDetComplemento.Id_PagDet,
                                   pagoDetComplemento.Id_PagComp};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_Consultar", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                {
                    pagoDetComplemento.Pago_Xml = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                }
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                {
                    pagoDetComplemento.Pago_Pdf = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                }
                pagoPdf = dr["Pago_Pdf"];
                pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
            }
            else
            {
                pagoDetComplemento = null;
            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }


        public void ConsultaComplementoPago(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_PagDet"
                                     ,"@RFC"
            }
            ;
            object[] Valores = { pagoDetComplemento.Id_Emp,
                                   pagoDetComplemento.Id_Cd,
                                   pagoDetComplemento.Id_Pag,
                                   pagoDetComplemento.Id_PagDet
                                  ,pagoDetComplemento.RFC
            };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_Consultar_ver2", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                {
                    pagoDetComplemento.Pago_Xml = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                }
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                {
                    pagoDetComplemento.Pago_Pdf = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                }
                pagoPdf = dr["Pago_Pdf"];
                pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
            }
            else
            {
                pagoDetComplemento = null;
            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        /// <summary>
        /// Funcion que consulta el detalle de los documentos
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="pagoPdf"></param>
        /// <param name="Conexion"></param>
        public void ConsultaComplementoPagoConsultaDetlista(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_PagDet",
                                      "@id_pagdettimb"};
            object[] Valores = { pagoDetComplemento.Id_Emp,
                                   pagoDetComplemento.Id_Cd,
                                   pagoDetComplemento.Id_Pag,
                                   pagoDetComplemento.Id_PagDet,
                                   pagoDetComplemento.Id_PagDetTimb};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultarDetalleLista2", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                {
                    pagoDetComplemento.Pago_Xml = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                }
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                {
                    pagoDetComplemento.Pago_Pdf = null;
                }
                else
                {
                    pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                }
                pagoPdf = dr["Pago_Pdf"];
                pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
            }
            else
            {
                pagoDetComplemento = null;
            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        /// <summary>
        /// Funcion que trae la lista de todos los elementos capturados 
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="pagoPdf"></param>
        /// <param name="Conexion"></param>
        public void ConsultaDetalleComplementoPagoListaDocs(PagoDetComplemento pagoDetComplemento, ref List<PagoDetComplemento> ListaDocumentos, ref object pagoPdf, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_PagDet" };
            object[] Valores = { pagoDetComplemento.Id_Emp,
                                   pagoDetComplemento.Id_Cd,
                                   pagoDetComplemento.Id_Pag,
                                   pagoDetComplemento.Id_PagDet};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemneto_ListaDocumentos", ref dr, Parametros, Valores);

            PagoDetComplemento ListDetComplemento;
            while (dr.Read())
            {
                ListDetComplemento = new PagoDetComplemento();
                ListDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                ListDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                ListDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                ListDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                ListDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                ListDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                ListDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                ListDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                ListDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                ListDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                ListDetComplemento.Id_PagDetTimb = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_PagDetTimb"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDetTimb")));
                ListDetComplemento.FechaCreacionXML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCreacionXML"))) ?
                    null : (DateTime?)Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCreacionXML")));


                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                {
                    ListDetComplemento.Pago_Xml = null;
                }
                else
                {
                    ListDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                }
                if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                {
                    ListDetComplemento.Pago_Pdf = null;
                }
                else
                {
                    ListDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                }
                pagoPdf = dr["Pago_Pdf"];
                ListDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));

                ListaDocumentos.Add(ListDetComplemento);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        /// <summary>
        /// Funcion que trae la informacion de los timbres 
        /// </summary>
        /// <param name="id_Emp"></param>
        /// <param name="id_Cd"></param>
        /// <param name="id_Pag"></param>
        /// <param name="id_Cte"></param>
        /// <param name="id_PagComp"></param>
        /// <param name="Conexion"></param>
        /// <param name="lista"></param>
        public void ConsultaListaComplementosNoTimbrados(int id_Emp, int id_Cd, int id_Pag, int id_Cte, int id_PagDet, string Conexion, ref List<PagoDetComplemento> lista)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_Cte",
                                      "@Id_PagDet"
                                     };
            object[] Valores = { id_Emp,
                                  id_Cd,
                                  id_Pag,
                                  id_Cte,
                                  id_PagDet
                               };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultaListaNoTimbrados", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                //Se verifica si esta en la tabla anterior o en la nueva tabla y trae el ultimo archivo subido
                if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_pagdettimb"))) != -1)
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoXML"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("PagoXML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoPDF"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("PagoPDF"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CancelacionXML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("CancelacionXML"));
                }
                else
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
                }
                lista.Add(pagoDetComplemento);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, int id_Cte, int id_PagComp, string Conexion, ref List<PagoDetComplemento> lista)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_Cte",
                                      "@Id_PagComp"};
            object[] Valores = { id_Emp,
                                  id_Cd,
                                  id_Pag,
                                  id_Cte,
                                  id_PagComp};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultaLista", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
                //Se verifica si esta en la tabla anterior o en la nueva tabla y trae el ultimo archivo subido
                if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_pagdettimb"))))
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoXML"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("PagoXML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoPDF"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("PagoPDF"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CancelacionXML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("CancelacionXML"));
                }
                else
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
                }
                lista.Add(pagoDetComplemento);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, int id_PagComp, string Conexion, ref List<PagoDetComplemento> lista)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_PagComp"};
            object[] Valores = { id_Emp,
                                  id_Cd,
                                  id_Pag,
                                  id_PagComp};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultaListaPorFolio", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();

                //Se verifica si esta en la tabla anterior o en la nueva tabla y trae el ultimo archivo subido
                if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_pagdettimb"))))
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoXML"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("PagoXML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoPDF"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("PagoPDF"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CancelacionXML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("CancelacionXML"));
                }
                else
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
                }
                lista.Add(pagoDetComplemento);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, string Conexion, ref List<PagoDetComplemento> lista)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag"};
            object[] Valores = { id_Emp,
                                  id_Cd,
                                  id_Pag};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultaListaPorPago", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                pagoDetComplemento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                pagoDetComplemento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                pagoDetComplemento.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                pagoDetComplemento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                pagoDetComplemento.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                pagoDetComplemento.Cte_Fpago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fpago"))) ?
                    null : (int?)Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Fpago")));
                pagoDetComplemento.Cte_UsoCFDI = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();
                pagoDetComplemento.Pago_Serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Serie"))) ?
                        string.Empty : dr.GetValue(dr.GetOrdinal("Pago_Serie")).ToString();
                pagoDetComplemento.Pago_FolioFiscal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();

                //Se verifica si esta en la tabla anterior o en la nueva tabla y trae el ultimo archivo subido
                if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_pagdettimb"))))
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoXML"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("PagoXML"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PagoPDF"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("PagoPDF"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CancelacionXML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("CancelacionXML"));
                }
                else
                {
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Xml"))))
                    {
                        pagoDetComplemento.Pago_Xml = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Xml = (object)dr.GetValue(dr.GetOrdinal("Pago_Xml"));
                    }
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pago_Pdf"))))
                    {
                        pagoDetComplemento.Pago_Pdf = null;
                    }
                    else
                    {
                        pagoDetComplemento.Pago_Pdf = (object)dr.GetValue(dr.GetOrdinal("Pago_Pdf"));
                    }
                    pagoDetComplemento.Cancelacion_XML = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion_XML"))) ? null : (object)dr.GetValue(dr.GetOrdinal("Cancelacion_XML"));
                }
                lista.Add(pagoDetComplemento);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void InsertarComplementoPago(PagoDetComplemento pagoDetComplemento, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                          "@Id_Cte",
                                          "@Id_PagDet",
                                          "@Serie"
                                          ,"@RFC"
                                      };
                object[] Valores = {
                                       pagoDetComplemento.Id_Emp,
                                       pagoDetComplemento.Id_Cd,
                                       pagoDetComplemento.Id_Pag,
                                       pagoDetComplemento.Id_Cte,
                                       pagoDetComplemento.Id_PagDet,
                                       pagoDetComplemento.Pago_Serie
                                      ,pagoDetComplemento.RFC
                };


                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_InsertarV2", ref verificador, Parametros, Valores);
                if (verificador > 0)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();

                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }


        /// <summary>
        /// Función que consultar los documentos con error de timbrado
        /// </summary>
        /// <param name="PagoDetalleComplementoShowLog"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void ConsultarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog PagoDetalleComplementoShowLog, string Conexion, ref List<PagoDetalleComplementoShowLog> Lista)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            CapaDatos = new CapaDatos.CD_Datos(Conexion);
            CapaDatos.StartTrans();

            string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Filtro_FecIni",
                                        "@Filtro_FecFin"
                                      };
            object[] Valores = {
                                       PagoDetalleComplementoShowLog.Id_Emp,
                                       PagoDetalleComplementoShowLog.Id_Cd,
                                       PagoDetalleComplementoShowLog.Filtro_FecIni,
                                       PagoDetalleComplementoShowLog.Filtro_FecFin
                                   };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapMonitores_ComplementodePAgoNoTimbrado", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                PagoDetalleComplementoShowLog PagoDetalleShowLog = new PagoDetalleComplementoShowLog();
                PagoDetalleShowLog.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                PagoDetalleShowLog.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                PagoDetalleShowLog.Id_Pag = dr.IsDBNull(dr.GetOrdinal("Id_Pag")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pag"));
                PagoDetalleShowLog.Id_ShowLog = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_showLog")));
                PagoDetalleShowLog.Atendido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Atendido")));
                PagoDetalleShowLog.Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")));
                PagoDetalleShowLog.Observaciones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Observaciones"))) ?
                                    string.Empty : dr.GetValue(dr.GetOrdinal("Observaciones")).ToString();
                Lista.Add(PagoDetalleShowLog);
            }

            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        /// <summary>
        /// Funcion que actualiza el estatus de timbre del show log.
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="Conexion"></param>
        /// <param name="insertar"></param>
        /// <param name="verificador"></param>
        public void ModificarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog pagoDetComplemento, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Showlog", "@Id_Emp", "@Id_Cd" };
                object[] Valores = { pagoDetComplemento.Id_ShowLog, pagoDetComplemento.Id_Emp, pagoDetComplemento.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCapPagoDetTimbradoShowLog_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void BorrarComplementoPago(int id_Emp, int id_Cd, int id_Pag, int id_PagDet, int ID_Comp, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                          "@Id_PagDet",
                                          "@Id_Comp"
                                      };
                object[] Valores = {
                                       id_Emp,
                                       id_Cd,
                                       id_Pag,
                                       id_PagDet,
                                       ID_Comp
                                   };


                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_BorrarPorPagoDet", ref verificador, Parametros, Valores);
                if (verificador > 0)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();

                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void ModificarComplementoPago(PagoDetComplemento pagoDetComplemento, string Conexion, bool insertar, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                //CapaDatos.StartTrans();

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Pag", "@Id_Cte", "@Id_PagDet", "@Id_PagComp", "@Cte_UsoCFDI",
                                        "@Cte_Fpago","@Pago_FolioFiscal", "@Pago_Xml", "@Pago_Pdf", "@Cancelacion_Xml", "@insertar",
                                        "@FechaCreacion" , "@FechaCancelacion"};
                object[] Valores = { pagoDetComplemento.Id_Emp, pagoDetComplemento.Id_Cd, pagoDetComplemento.Id_Pag,
                                       pagoDetComplemento.Id_Cte, pagoDetComplemento.Id_PagDet,
                                       pagoDetComplemento.Id_PagComp,pagoDetComplemento.Cte_UsoCFDI,
                                       pagoDetComplemento.Cte_Fpago, pagoDetComplemento.Pago_FolioFiscal,
                                       pagoDetComplemento.Pago_Xml,pagoDetComplemento.Pago_Pdf,
                                       pagoDetComplemento.Cancelacion_XML, insertar, pagoDetComplemento.FechaCreacionXML, pagoDetComplemento.FechaCancelacion};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_Modificar", ref verificador, Parametros, Valores);
                //CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void BajaComplementoPago(PagoDetComplemento pagoDetComplemento, bool otraBD, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                SqlCommand sqlcmd;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Pag", "@Id_Cte", "@Id_PagDet", "@Id_PagComp", "@Cte_UsoCFDI",
                                          "@Cte_Fpago","@Pago_FolioFiscal", "@Pago_Xml", "@Pago_Pdf", "@Cancelacion_Xml", "@FechaCancelacion" };
                object[] Valores = { pagoDetComplemento.Id_Emp, pagoDetComplemento.Id_Cd, pagoDetComplemento.Id_Pag,
                                       pagoDetComplemento.Id_Cte, pagoDetComplemento.Id_PagDet,
                                       pagoDetComplemento.Id_PagComp,pagoDetComplemento.Cte_UsoCFDI,
                                       pagoDetComplemento.Cte_Fpago, pagoDetComplemento.Pago_FolioFiscal,
                                       pagoDetComplemento.Pago_Xml,pagoDetComplemento.Pago_Pdf,
                                       pagoDetComplemento.Cancelacion_XML, pagoDetComplemento.FechaCancelacion};
                if (otraBD)
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_BajaOtraBD", ref verificador, Parametros, Valores);
                else
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_Baja", ref verificador, Parametros, Valores);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaSerieYFolio(int id_Emp, int id_Cd, ref string serie, ref int folio, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd" };
            object[] Valores = { id_Emp,
                                 id_Cd};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoConsultar_FolioSerie", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                //folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                folio = 0;
                serie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Serie"))) ?
                    string.Empty : dr.GetValue(dr.GetOrdinal("Serie")).ToString();

            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        public void ConsultaNumeroDeParcialidad(int id_Emp, int id_Cd, string serie, int folio, DateTime fecha, ref int numParcialidad, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                    "@Id_Cd",
                                    "@folioFactura",
                                    "@Serie",
                                    "@fechaPago"};
            object[] Valores = { id_Emp,
                                 id_Cd,
                                 folio,
                                 serie,
                                 fecha};

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_ContarPagos", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                numParcialidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Num"))) ?
                    0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Num")));

            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

        /// <summary>
        /// Funcion que consulta lad facturas que aun no han sida timbradas tanto en la vieja estructura de la tabla como en la nueva
        /// </summary>
        /// <param name="Fecha"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaDocumentosNoTimbrados(DateTime Fecha, string Conexion, ref List<PagoDetTimbrado> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Fecha" };
                object[] Valores = { Fecha };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_ConsultarDocMonitorNotimbrados", ref dr, Parametros, Valores);

                PagoDetTimbrado DetTimbrado;
                while (dr.Read())
                {
                    DetTimbrado = new PagoDetTimbrado();
                    DetTimbrado.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DetTimbrado.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    DetTimbrado.Id_Pag = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                    DetTimbrado.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    DetTimbrado.Id_PagComp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagComp")));
                    DetTimbrado.Id_PagDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                    List.Add(DetTimbrado);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoTimbres(ref Pago pago, ref List<PagoDet> list_pagos, string Conexion, ref List<PagoDetComplemento> List_DetComplemento)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Pag",
                                      };
                object[] Valores = {
                                       pago.Id_Emp,
                                       pago.Id_Cd,
                                       pago.Id_Pag
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPago_Consultar", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pago.Tipo = dr.IsDBNull(dr.GetOrdinal("Pag_Tipo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Pag_Tipo"));
                    pago.Pag_Fecha = dr.IsDBNull(dr.GetOrdinal("Pag_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Pag_Fecha"));
                    pago.Id_Tmov = dr.IsDBNull(dr.GetOrdinal("Id_Tm")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Tm"));
                    pago.Pag_Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Importe"));
                    pago.Pag_Total = dr.IsDBNull(dr.GetOrdinal("Pag_Total")) ? 0 : dr.GetDouble(dr.GetOrdinal("Pag_Total"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista de DETALLES
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDet_Consultar2", ref dr, Parametros, Valores);
                PagoDet detalle = default(PagoDet);
                while (dr.Read())
                {
                    detalle = new PagoDet();
                    detalle.Mov = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento")));
                    detalle.MovStr = dr.IsDBNull(dr.GetOrdinal("Pag_Movimiento")) ? "" : (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Movimiento"))) == 1 ? "Factura" : "Nota de cargo");
                    detalle.Ref = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    detalle.Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Fac_FolioFiscal = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscal")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscal"));
                    detalle.Id_Terr = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalle.Pag_Id_cd = dr.IsDBNull(dr.GetOrdinal("Pag_Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Id_Cd")));
                    detalle.Doc_Fecha = dr.IsDBNull(dr.GetOrdinal("Fac_Fecha")) ? default(DateTime) : dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    detalle.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    detalle.Cte_Nombre = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    detalle.Pag_Numero = dr.IsDBNull(dr.GetOrdinal("Pag_Ficha")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pag_Ficha")));
                    detalle.Pag_Cheque = dr.IsDBNull(dr.GetOrdinal("Pag_Cheque")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Cheque"));
                    detalle.Importe = dr.IsDBNull(dr.GetOrdinal("Pag_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Pag_Importe")));
                    detalle.Doc_Estatus = dr.IsDBNull(dr.GetOrdinal("Fac_Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Estatus"));
                    detalle.Doc_Importe = dr.IsDBNull(dr.GetOrdinal("Fac_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Importe")));
                    detalle.Doc_Pagado = dr.IsDBNull(dr.GetOrdinal("Fac_Pagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Fac_Pagado")));
                    detalle.Cte_Rfc = dr.IsDBNull(dr.GetOrdinal("CteRFC")) ? "" : dr.GetString(dr.GetOrdinal("CteRFC"));
                    detalle.Pag_Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    detalle.Id_Pag = dr.IsDBNull(dr.GetOrdinal("Id_Pag")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pag")));
                    detalle.Pag_Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? string.Empty : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    detalle.Id_PagDet = dr.IsDBNull(dr.GetOrdinal("Id_PagDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagDet")));
                    list_pagos.Add(detalle);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //Lista pagos anteriores
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                sqlcmd = CapaDatos.GenerarSqlCommand("ConsultaSaldosCFDI", ref dr, Parametros, Valores);
                PagoDetComplemento DetComplemento = default(PagoDetComplemento);
                while (dr.Read())
                {
                    DetComplemento = new PagoDetComplemento();
                    DetComplemento.Pago_Serie = dr.IsDBNull(dr.GetOrdinal("Pag_Serie")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Serie"));
                    DetComplemento.pag_referencia = dr.IsDBNull(dr.GetOrdinal("Pag_Referencia")) ? "" : dr.GetString(dr.GetOrdinal("Pag_Referencia"));
                    DetComplemento.Total_Pagado_Timbrado = dr.IsDBNull(dr.GetOrdinal("ImportePagado")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ImportePagado")));
                    DetComplemento.Parcialidad_Timbrada = dr.IsDBNull(dr.GetOrdinal("Parcialidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Parcialidad"));
                    List_DetComplemento.Add(DetComplemento);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void VerMonitorComplementosNoTimbrados(int Id_Emp, int Id_Cd, int Id_Usuario, string Conexion, ref int NumDocNTimbrados)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Usuario"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                        Id_Usuario
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVerMonitorComplemento", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    NumDocNTimbrados = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CANTIDAD")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion PagoDetComplemento       
        public void PagoSegundoPlanoConsultaEstatus(string Conexion, ref FacturaLite entFacturaLite)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                            "@Id_Pago"
                                      };
                object[] Valores = {
                                       entFacturaLite.id_Pag
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoSegundoPlano_ConsultarEstatus", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    entFacturaLite.Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Activo")));
                    entFacturaLite.strRstAlerta = dr.GetValue(dr.GetOrdinal("Mensaje")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoSegundoPlano(string Conexion, ref FacturaLite entFacturaLite, ref string strMsjResultado)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                            "@Id_Pago"
                                      };
                object[] Valores = {
                                       entFacturaLite.id_Pag
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoSegundoPlano_consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    entFacturaLite.Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Activo")));
                    entFacturaLite.Notificado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Notificado")));
                    strMsjResultado = dr.GetValue(dr.GetOrdinal("Mensaje")).ToString();
                    entFacturaLite.strRstAlerta = dr.GetValue(dr.GetOrdinal("RstAlerta")).ToString();
                    entFacturaLite.strRstScript = dr.GetValue(dr.GetOrdinal("RstScript")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActivaPagoSegundoPlano(string Conexion, ref FacturaLite entFacturaLite, ref string strMsjResultado)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                            "@Id_Pago",
                            "@MetodoPago",
                            "@id_Fac",
                            "@id_Emp",
                            "@id_Cd",
                            "@id_cte"
                                      };
                object[] Valores = {
                                       entFacturaLite.id_Pag,
                                       entFacturaLite.MetodoPago,
                                       entFacturaLite.id_Fac,
                                       entFacturaLite.id_Emp,
                                       entFacturaLite.id_Cd,
                                        entFacturaLite.id_cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoSegundoPlano_Insertar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    entFacturaLite.Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PagoSegundoPlano")));
                    strMsjResultado = dr.GetValue(dr.GetOrdinal("Mensaje")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DesactivaPagoSegundoPlano(string Conexion, FacturaLite entFacturaLite)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlDataReader dr = null;
            int intActivo;
            try
            {
                string[] Parametros = {
                            "@Id_Pago",
                            "@MetodoPago",
                            "@id_Fac",
                            "@id_Emp",
                            "@id_Cd",
                            "@id_cte",
                            "@strRstAlerta",
                            "@strRstScript"
                                      };
                object[] Valores = {
                                       entFacturaLite.id_Pag,
                                       entFacturaLite.MetodoPago,
                                       entFacturaLite.id_Fac,
                                       entFacturaLite.id_Emp,
                                       entFacturaLite.id_Cd,
                                       entFacturaLite.id_cte,
                                       entFacturaLite.strRstAlerta,
                                       entFacturaLite.strRstScript
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoSegundoPlano_desactivar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    intActivo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Activo")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Cambios Referencia Pagos
        public void ConsultaReferenciaBanco(Sesion sesion, string Referencia, ref int Id_Ban, ref string cuenta)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Referencia"
                                      };
                object[] Valores = {
                                        sesion.Id_Emp,
                                        sesion.Id_Cd,
                                        Referencia
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoFicha_ConsultarBancoReferencia", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    cuenta = dr.IsDBNull(dr.GetOrdinal("Ban_Cuenta")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Ban_Cuenta")));
                    Id_Ban = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ban")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void spCatBanco_Combo(Sesion sesion, int id_Ban, ref string cuenta)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                                        "@Id1",
                                        "@Id2",
                                        "@Id3"
                                      };
                object[] Valores = {
                                        id_Ban,
                                        sesion.Id_Emp,
                                        sesion.Id_Cd,

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatBanco_Combo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    cuenta = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidaSucursal(Sesion sesion, ref int Id_Cd)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            SqlDataReader dr = null;
            try
            {
                string[] Parametros = {
                                        "@Id_Cd"
                                      };
                object[] Valores = {
                                         sesion.Id_Cd_Ver
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValidarSucursal", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoDetTimbrado(string strConexion, ref PagoDetTimbrado entPagoDetTimbrado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);

                string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Pag",
                                      "@Id_Cte",
                                      "@Id_PagDet" };
                object[] Valores = { entPagoDetTimbrado.Id_Emp,
                                   entPagoDetTimbrado.Id_Cd,
                                   entPagoDetTimbrado.Id_Pag,
                                   entPagoDetTimbrado.Id_Cte,
                                   entPagoDetTimbrado.Id_PagDet};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemneto_Existe", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    entPagoDetTimbrado.Pago_FolioFiscal = dr.GetValue(dr.GetOrdinal("Pago_FolioFiscal")).ToString();
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