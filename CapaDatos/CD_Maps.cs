using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_Maps
    {
        public void XConsultaRequisitosCita(RequisitoCita requisitoCita, string Conexion, ref List<RequisitoCita> List)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPreRequisitos_Todos", ref dr);
                while (dr.Read())
                {
                    /*
                    Id_CriterioCita,
		            IdPreRequisito
                    PreRequisito

                     * */
                    requisitoCita = new RequisitoCita();
                    //  requisitoCita.Id_CriterioCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CriterioCita")));
                    requisitoCita.IdPreRequisito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdPreRequisito")));
                    requisitoCita.PreRequisito = dr.GetValue(dr.GetOrdinal("PreRequisito")).ToString();

                    List.Add(requisitoCita);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XInsertarRequisitosCita(List<RequisitoCita> Listado, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                //  string Ssps;
                object[] Valores;
                SqlCommand sqlcmd;
                RequisitoCita Rrequi;
                Rrequi = Listado[0];
                string[] ParametroX = { 
                                        "@Id_CitaVisita", 
                                      };
                Valores = new object[] {
                    Rrequi.Id_CriterioCita,
                    };

                //  Ssps = "spRequisitosCitas_Limpia" + Rrequi.Id_CriterioCita.ToString();
                sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Limpia", ref verificador, ParametroX, Valores);

                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                        "@IdPreRequisito",
                                        "@Secuencia",
                                      };
                int cont = 0;
                foreach (RequisitoCita Requi in Listado)
                {
                    cont++;
                    Valores = new object[] { 
                        Requi.Id_CriterioCita,
                        Requi.IdPreRequisito,
                        cont
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Alta", ref verificador, Parametros, Valores);
                }

                CapaDatos.CommitTrans();
                //  CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XVerCalendario(string Conexion, int Emp, int Cd, int Usuario, ref int refer)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Usuario", 
                                      };
                object[] Valores = new object[] { 
                                        Emp,
                                        Cd,
                                        Usuario
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVerCalendario", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    refer = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tu")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XListadoPrerequisitosCita_Todos(string Conexion, int Cita, ref List<RequisitoCita> Listado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                      };

                object[] Valores = new object[] { 
                        Convert.ToInt32(Cita)
                    };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosUnaCita_Consulta", ref dr, Parametros, Valores);

                RequisitoCita Comun = default(RequisitoCita);
                while (dr.Read())
                {
                    Comun = new RequisitoCita();
                    Comun.IdPreRequisito = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.PreRequisito = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    Listado.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XListadoRequisitos_Cita(string Conexion, string CitaVisita, ref List<RequisitoCita> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                      };

                object[] Valores = new object[] { 
                        Convert.ToInt32(CitaVisita)
                    };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Consulta", ref dr, Parametros, Valores);

                RequisitoCita Comun = default(RequisitoCita);
                while (dr.Read())
                {
                    Comun = new RequisitoCita();
                    Comun.IdPreRequisito = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.PreRequisito = dr.GetString(dr.GetOrdinal("PreRequisito"));

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

        //  -----------------------------------------------------------------------------------------------------------------------

        public void ObtieneOrigen(ref Maps Ma, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Emp,
                                        Ma.Id_Cd
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtieneCoordenadas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Ma.Sucursal = dr.GetString(dr.GetOrdinal("Sucursal"));
                    Ma.Latitud = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lat")));
                    Ma.Longitud = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lng")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaRuta(ref Maps Ma, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Cd",
                                        "@Id_Ruta",
                                        "@Id_Cliente",
                                        "@Ruta", 
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Cd,
                                        Ma.Id_Ruta,
                                        Ma.Id_Cliente,
                                        Ma.Ruta
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGrabaRuta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Ma.Id_Ruta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdRuta")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GrabaSegmento(Maps Ma, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                int verf = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Ruta",
                                        "@Id_Cliente",
                                        "@Segmento",
                                        "@LatOrigen",
                                        "@LngOrigen",
                                        "@LatDestino",
                                        "@LngDestino",
                                        "@Kms", 
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Ruta,
                                        Ma.Id_Cliente,
                                        Ma.Segmento,
                                        Ma.LatOrigen,
                                        Ma.LngOrigen,
                                        Ma.LatDestino,
                                        Ma.LngDestino,
                                        Ma.Kilometros
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGrabaDetalleRuta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verf = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Segmento")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ObtieneRutasCliente(Maps Ma, ref List<Maps> RutasMa, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Id_Cliente", 
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Cd,
                                        Ma.Id_Cliente
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtieneRutas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Ma = new Maps();
                    Ma.Id_Ruta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ruta")));
                    Ma.Ruta = dr.GetValue(dr.GetOrdinal("Ruta")).ToString();
                    RutasMa.Add(Ma);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDetalleRuta(Maps Ma, ref string DetalleRuta, ref string FinalDeRuta, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Cd", 
                                        "@Id_Cliente", 
                                        "@Id_Ruta",
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Cd,
                                        Ma.Id_Cliente,
                                        Ma.Id_Ruta
                                        };
                DetalleRuta = "";
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtieneDetalleRuta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    DetalleRuta = DetalleRuta + " waypts.push( { location: { lat: " + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lat"))).ToString() + ", lng: " + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lng"))).ToString() + " } , stopover: true } );";
                    FinalDeRuta = "final = {lat: " + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lat"))).ToString() + ",  lng: " + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Lng"))).ToString() + " };";
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GrabaDireccionEntregaCliente(Maps Ma, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                int verf = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                        "@Id_Emp",
	                                    "@Id_Cd",
	                                    "@Id_Cliente",
	                                    "@Direccion",
	                                    "@Latitud",
	                                    "@Longitud", 
                                      };
                object[] Valores = new object[] { 
                                        Ma.Id_Emp,
                                        Ma.Id_Cd,
                                        Ma.Id_Cliente,
                                        Ma.Direccion,
                                        Ma.Latitud,
                                        Ma.Longitud,
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDireccionEntregaCliente_Inserta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verf = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grabo")));
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
