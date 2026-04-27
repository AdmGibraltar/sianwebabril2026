using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ProEmbarque
    {
        public void ProEmbarque_ConsultaLista(ProEmbarque emb, ref List<ProEmbarque> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Cd",
                                          "@EmbFechaIni",
                                          "@EmbFechaFin",
                                          "@Id_Emb",
                                          "@Emb_Destino"
                                      };
                object[] Valores = {
                                       emb.Id_Cd ,
                                       emb.Filtro_EmbFechaIni ,
                                       emb.Filtro_EmbFechaFin,
                                       emb.Filtro_Id_Emb ,
                                       emb.Filtro_Emb_Destino
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentos_ConsultaLista", ref dr, Parametros, Valores);


                ProEmbarque e;
                while (dr.Read())
                {
                    e = new ProEmbarque();
                    e.Emb_EstatusStr = dr["Emb_EstatusStr"].ToString();
                    e.U_Nombre = dr["U_Nombre"].ToString();
                    e.Id_Emb = Convert.ToInt32(dr["Id_Emb"]);
                    e.Emb_DestinoStr = dr["Emb_DestinoStr"].ToString();
                    e.Emb_Fecha = Convert.ToDateTime(dr["Emb_Fecha"]);
                    e.Emb_Dia = Convert.ToDateTime(dr["Emb_Dia"]);
                    e.Emb_Chofer = dr["Emb_Chofer"].ToString();
                    e.Emb_Camioneta = dr["Emb_Camioneta"].ToString();
                    List.Add(e);
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void ProEmbarque_ConsultaDocumento(ref ProEmbarqueDet emb, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {
                                            "@Emb_Tipo",
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Doc",
                                            "@Id_Emb"
                                      };
                object[] Valores = {
                                       emb.Emb_Tipo,
                                       emb.Id_Emp ,
                                       emb.Id_Cd ,
                                       emb.Id_Doc ,
                                       emb.Id_Emb
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarques_BuscarDocFacRem", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    emb.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    emb.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    emb.Doc_Importe = Convert.ToDouble(dr["Doc_Importe"]);

                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Insertar(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Emb_Destino",
                                          "@Emb_Fecha",
                                          "@Emb_Dia",
                                          "@Emb_Chofer",
                                          "@Emb_Camioneta",
                                          "@Id_UCreo"
                                      };
                object[] Valores = {
                                       emb.Id_Emp ,
                                       emb.Id_Cd ,
                                       emb.Emb_Destino,
                                       emb.Emb_Fecha,
                                       emb.Emb_Dia,
                                       emb.Emb_Chofer,
                                       emb.Emb_Camioneta,
                                       emb.Id_U
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentos_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueDet_Insertar(List<ProEmbarqueDet> List, int Id_Emb, ref int Verificador, string Conexion)
        {
            try
            {



                string[] Parametros = {
                                            "@Id_Emp",
                                            "@Id_Cd" ,
                                            "@Id_Emb" ,
                                            "@Emb_Tipo" ,
                                            "@Id_Doc" ,
                                            "@Id_Cte" ,
                                            "@Doc_Importe"
                                      };

                foreach (ProEmbarqueDet emb in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    object[] Valores = {
                                       emb.Id_Emp ,
                                       emb.Id_Cd ,
                                       Id_Emb ,
                                       emb.Emb_Tipo ,
                                       emb.Id_Doc ,
                                       emb.Id_Cte,
                                       emb.Doc_Importe,

                                   };

                    SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentosDet_Insertar", ref Verificador, Parametros, Valores);

                    cd_datos.LimpiarSqlcommand(ref sqlcmd);

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Consulta(ref ProEmbarque emb, ref List<ProEmbarqueDet> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb"
                                      };
                object[] Valores = {
                                       emb.Id_Emp,
                                       emb.Id_Cd ,
                                       emb.Id_Emb

                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentos_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    emb.Emb_Destino = Convert.ToInt32(dr["Emb_Destino"]);
                    emb.Emb_Fecha = Convert.ToDateTime(dr["Emb_Fecha"]);
                    emb.Emb_Dia = Convert.ToDateTime(dr["Emb_Dia"]);
                    emb.Emb_Chofer = dr["Emb_Chofer"].ToString();
                    emb.Emb_Camioneta = dr["Emb_Camioneta"].ToString();
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

                CD_Datos cd_datos2 = new CD_Datos(Conexion);

                SqlCommand sqlcmd2 = cd_datos2.GenerarSqlCommand("spProEmbarqueDocumentosDet_Consulta", ref dr2, Parametros, Valores);

                ProEmbarqueDet e;
                while (dr2.Read())
                {
                    e = new ProEmbarqueDet();
                    e.Id_Emp = Convert.ToInt32(dr2["Id_Emp"]);
                    e.Id_Cd = Convert.ToInt32(dr2["Id_Cd"]);
                    e.Id_Emb = Convert.ToInt32(dr2["Id_Emb"]);
                    e.Emb_Tipo = Convert.ToInt32(dr2["Emb_Tipo"]);
                    e.Emb_TipoStr = dr2["Emb_TipoStr"].ToString();
                    e.Id_Doc = Convert.ToInt32(dr2["Id_Doc"]);
                    e.Id_Cte = Convert.ToInt32(dr2["Id_Cte"]);
                    e.Cte_NomComercial = dr2["Cte_NomComercial"].ToString();
                    e.Doc_Importe = Convert.ToDouble(dr2["Doc_Importe"]);

                    List.Add(e);
                }

                dr2.Close();
                cd_datos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Modificar(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Emb",
                                        "@Emb_Fecha",
                                        "@Emb_Dia",
                                        "@Emb_Chofer",
                                        "@Emb_Camioneta"
                                       };
                object[] Valores = {
                                       emb.Id_Emp,
                                       emb.Id_Cd ,
                                       emb.Id_Emb,
                                       emb.Emb_Fecha,
                                       emb.Emb_Dia,
                                       emb.Emb_Chofer,
                                       emb.Emb_Camioneta
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentos_Modificar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Baja(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                      "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Emb"
                                  };
                object[] Valores = {
                                   emb.Id_Emp,
                                   emb.Id_Cd ,
                                   emb.Id_Emb
                               };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueDocumentos_Baja", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

                if (Verificador == -1)
                {
                    CD_Datos cd_datos2 = new CD_Datos(Conexion);

                    SqlCommand sqlcmd2 = cd_datos2.GenerarSqlCommand("spProEmbarqueDocumentos_Baja_RegresaEstatus", ref Verificador, Parametros, Valores);

                    cd_datos2.LimpiarSqlcommand(ref sqlcmd2);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void ProEmbarqueConfirmar_Buscar(ProEmbarqueDet emb, ref List<ProEmbarqueDet> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Cte",
                                            "@Emb_Tipo",
                                            "@Cte_NomComercial",
                                            "@Doc_FecInicial",
                                            "@Doc_FecFinal"
                                      };
                object[] Valores = {

                                       emb.Id_Emp,
                                       emb.Id_Cd,
                                       emb.Id_Cte == null ?  (object) null: emb.Id_Cte,
                                       emb.Emb_Tipo == -1 ? (object) null : emb.Emb_Tipo,
                                       emb.Cte_NomComercial,
                                       emb.Doc_FechaIni == null ?  (object) null: emb.Doc_FechaIni,
                                       emb.Doc_FechaFin == null ?  (object) null: emb.Doc_FechaFin
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarqueConfirmacion_Buscar", ref dr, Parametros, Valores);


                ProEmbarqueDet e;
                while (dr.Read())
                {
                    e = new ProEmbarqueDet();
                    emb.Seleccionado = false;
                    e.UniqueID = Guid.NewGuid().ToString();
                    e.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    e.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    e.Id_Emb = Convert.ToInt32(dr["Id_Emb"]);
                    e.Id_Doc = Convert.ToInt32(dr["Id_Doc"]);
                    e.Emb_Tipo = Convert.ToInt32(dr["Emb_Tipo"]);
                    e.Id_DocStr = dr["Id_DocStr"].ToString();
                    e.Doc_Estatus = dr["Doc_Estatus"].ToString();
                    e.Doc_EstatusStr = dr["Doc_EstatusStr"].ToString();
                    e.Doc_Fecha = Convert.ToDateTime(dr["Doc_Fecha"]);
                    e.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    e.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    e.Doc_Importe = Convert.ToDouble(dr["Doc_Importe"]);
                    List.Add(e);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueConfirmaUno(ProEmbarqueDet emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Emb",
                                        "@Id_Doc",
                                        "@Emb_Tipo",
                                        "@Origen"
                                      };
                object[] Valores = {
                                       emb.Id_Emp,
                                       emb.Id_Cd ,
                                       emb.Id_Emb,
                                       emb.Id_Doc,
                                       emb.Emb_Tipo,
                                       1
                                    };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarque_ConfirmaUno", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueConfirmaTodos(ProEmbarqueDet emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Cte",
                                            "@Emb_Tipo",
                                            "@Cte_NomComercial",
                                            "@Doc_FecInicial",
                                            "@Doc_FecFinal"
                                      };
                object[] Valores = {
                                       emb.Id_Emp,
                                       emb.Id_Cd,
                                       emb.Id_Cte == null ?  (object) null: emb.Id_Cte,
                                       emb.Emb_Tipo == -1 ? (object) null : emb.Emb_Tipo,
                                       emb.Cte_NomComercial,
                                       emb.Doc_FechaIni == null ?  (object) null: emb.Doc_FechaIni,
                                       emb.Doc_FechaFin == null ?  (object) null: emb.Doc_FechaFin
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProEmbarque_ConfirmaTodos", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ListaEmbarque(Embarques embarques, string Conexion, ref List<EmbarquesReporte> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb"
                                      };
                object[] Valores = {

                                       embarques.Id_Emp,
                                       embarques.Id_Cd  ,
                                       embarques.Id_Emb

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEmbarque_RutaInfo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    EmbarquesReporte embarque = new EmbarquesReporte();
                    embarque.Id_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Doc")));
                    embarque.Emb_Camioneta = Convert.ToString(dr.GetValue(dr.GetOrdinal("Emb_Camioneta")));
                    embarque.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    embarque.Fac_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Cant")));
                    embarque.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    embarque.Id_Contacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Contacto")));
                    embarque.NomContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("NomContacto")));
                    embarque.Telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Telefono")));
                    embarque.Email_Contacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Email_Contacto")));
                    embarque.Direccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Direccion")));
                    embarque.Latitud = Convert.ToString(dr.GetValue(dr.GetOrdinal("Latitud")));
                    embarque.Longitud = Convert.ToString(dr.GetValue(dr.GetOrdinal("Longitud")));
                    embarque.Fecha_MIN_ENTREGA = Convert.ToDateTime(dr["Fecha_MIN_ENTREGA"]);
                    embarque.Fecha_Max_entrega = Convert.ToDateTime(dr["Fecha_Max_entrega"]);
                    embarque.CT_Destino = Convert.ToString(dr.GetValue(dr.GetOrdinal("CT_Destino")));
                    embarque.PesoVolumen = Convert.ToDouble(dr["PesoVolumen"]);
                    embarque.Importancia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Importancia")));
                    embarque.ServiceTime = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ServiceTime")));
                    embarque.Campos_Avanzados = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados")));

                    List.Add(embarque);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListaEmbarquePlanner(Embarques embarques, string Conexion, ref List<EmbarquesReporte> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Emb"
                                      };
                object[] Valores = {

                                       embarques.Id_Emp,
                                       embarques.Id_Cd  ,
                                       embarques.Id_Emb

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEmbarque_RutaInfo_Planner", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    EmbarquesReporte embarque = new EmbarquesReporte();
                    embarque.Id_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Doc")));
                    embarque.Latitud = Convert.ToString(dr.GetValue(dr.GetOrdinal("Latitud")));
                    embarque.Longitud = Convert.ToString(dr.GetValue(dr.GetOrdinal("Longitud")));
                    embarque.Direccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Direccion")));
                    embarque.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    embarque.Fac_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Fac_Cant")));
                    embarque.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    embarque.Fecha_MIN_ENTREGA = Convert.ToDateTime(dr["Fecha_MIN_ENTREGA"]);
                    embarque.Fecha_Max_entrega = Convert.ToDateTime(dr["Fecha_Max_entrega"]);
                    embarque.MIN_VENTANAHORARIA1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("MIN_VENTANAHORARIA1")));
                    embarque.MIN_VENTANAHORARIA2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("MIN_VENTANAHORARIA2")));
                    embarque.MAX_VENTANAHORARIA1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("MAX_VENTANAHORARIA1")));
                    embarque.MAX_VENTANAHORARIA2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("MAX_VENTANAHORARIA2")));
                    embarque.Costo = Convert.ToDouble(dr["costo"]);
                    embarque.Volumen = Convert.ToDouble(dr["CAPACIDADUNO"]);
                    embarque.Peso = Convert.ToDouble(dr["CAPACIDADDOS"]);
                    embarque.ServiceTime = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ServiceTime")));
                    embarque.Importancia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Importancia")));
                    embarque.Id_Contacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("identificadorcontacto")));
                    embarque.NomContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("NomContacto")));
                    embarque.Telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Telefono")));
                    embarque.Email_Contacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Email_Contacto")));
                    embarque.CT_Destino = Convert.ToString(dr.GetValue(dr.GetOrdinal("CTORIGEN")));
                    embarque.Campos_Avanzados = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados")));
                    embarque.Campos_Avanzados2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados2")));
                    embarque.Campos_Avanzados3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados3")));
                    embarque.Campos_Avanzados4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados4")));
                    embarque.Campos_Avanzados5 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados5")));
                    embarque.Campos_Avanzados6 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Campos_Avanzados6")));
                    embarque.Zona = Convert.ToString(dr.GetValue(dr.GetOrdinal("Zona")));

                    embarque.Calle = Convert.ToString(dr.GetValue(dr.GetOrdinal("Calle")));
                    embarque.Colonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("Colonia")));
                    embarque.CodigoPostal = Convert.ToString(dr.GetValue(dr.GetOrdinal("CodigoPostal")));
                    embarque.Municipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("Municipio")));
                    embarque.Estado = Convert.ToString(dr.GetValue(dr.GetOrdinal("Estado")));
                    embarque.Pais = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pais")));

                    List.Add(embarque);
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