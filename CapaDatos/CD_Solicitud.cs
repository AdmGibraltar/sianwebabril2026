using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.IO;
using System.Data;

namespace CapaDatos
{
    public class CD_Solicitud
    {
        public void ConsultaProductos(string Conexion, ref List<Producto> lstProductos)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Producto producto;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consulta", ref dr);

                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    producto.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                    producto.Presentacion = dr.GetString(dr.GetOrdinal("Presentacion"));
                    producto.Lote = dr.GetString(dr.GetOrdinal("Lote"));
                    producto.Fabricacion = dr.GetDateTime(dr.GetOrdinal("Fec_fabricacion"));
                    producto.Caducidad = dr.GetDateTime(dr.GetOrdinal("Caducidad"));
                    producto.Marca = dr.GetString(dr.GetOrdinal("Marca"));
                    producto.Costo = float.Parse(dr.GetOrdinal("Costo").ToString());
                    producto.Num_Fac = dr.GetInt32(dr.GetOrdinal("Num_Fac"));
                    producto.Nom_Prov = dr.GetString(dr.GetOrdinal("Nom_Prov"));

                    lstProductos.Add(producto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardaSolicitud(Sesion sesion, Solicitud solicitud, ref int respuesta, DataTable objdtTabla)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                respuesta = 0;
                string[] Parametros = new string[]{
                                                    "@Id_Prd",
                                                    "@Cantidad",
                                                    "@Procede",
                                                    "@Rem_Trans",
                                                    "@Id_Doc",
                                                    "@Costo_Estandar",
                                                    "@Nota_Credito",
                                                    "@Id_Queja"
                                                   };
                for (int x = 0; x < objdtTabla.Rows.Count; x++)
                {
                    object[] Valores = new object[]
                                            {
                                                    Convert.ToInt64(objdtTabla.Rows[x][0]),
                                                    Convert.ToInt32(objdtTabla.Rows[x][3]),
                                                    Convert.ToBoolean(objdtTabla.Rows[x][14]),
                                                    Convert.ToInt32(objdtTabla.Rows[x][15]),
                                                    Convert.ToInt32(objdtTabla.Rows[x][16]),
                                                    Convert.ToDecimal(objdtTabla.Rows[x][17]),
                                                    Convert.ToInt32(objdtTabla.Rows[x][18]),
                                                    solicitud.Id_Queja
                                           };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spActualizaProductos", ref respuesta, Parametros, Valores);
                }

                Parametros = new string[]{ 
		                            "@id_Queja",
                                    "@id_Cliente",
				                    "@Nom_Ubicacion",   
				                    "@Nom_Puesto",   
				                    "@Correo", 
				                    "@Id_Grupo",  
				                    "@Id_Investigador", 
				                    "@Id_Estado",  
				                    "@Id_Prioridad",   
				                    "@Id_tServicio",   
				                    "@Id_Accion",   
				                    "@Id_Categoria",   
				                    "@ConCopia", 
				                    "@FechaCreacion",   
				                    "@TxtComentarios"
                                };
                object[] Valor = new object[]{ 
                                            solicitud.Id_Queja,
                                            solicitud.Id_Cliente,
                                            solicitud.Correo,
                                            solicitud.Id_Investigador,
                                            solicitud.Id_Estado,
                                            solicitud.Id_Prioridad,
                                            solicitud.Id_tServicio,
                                            solicitud.Id_Accion,
                                            solicitud.Id_Categoria,
                                            solicitud.ConCopia,
                                            solicitud.FechaCreacion,
                                            solicitud.Comentarios

                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spInsertaSolicitud", ref respuesta, Parametros, Valor);

                solicitud.Id_Solicitud = respuesta;

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaSolicitudes(Sesion sesion, int Id_Tu, ref List<Solicitud> lstSolicitud, int Id_Solicitud, string NomCliente, int Id_Factura, DateTime Fec_Inicio, DateTime Fec_Fin, int TipoServicio, int Id_Accion, int Estatus)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                Solicitud sol = new Solicitud();

                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@U_correo",
                                        "@Id_Solicitud",
                                        "@NomCliente",
                                        "@Id_Factura",
                                        "@Fec_Inicio",
                                        "@Fec_Fin",
                                        "@TipoServicio",
                                        "@Id_Accion",
                                        "@Id_Estado"
                                      };
                object[] Valores = {     
                                         sesion.Id_Emp,
                                         sesion.U_Correo,
                                         Id_Solicitud,
                                         NomCliente,
                                         Id_Factura,
                                         Fec_Inicio,
                                         Fec_Fin,
                                         TipoServicio,
                                         Id_Accion,
                                         Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolicitud_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    sol = new Solicitud();
                    sol.Id_Solicitud = dr.GetInt32(dr.GetOrdinal("Id_Solicitud"));
                    sol.Cte_Nom = dr.GetString(dr.GetOrdinal("cNombre"));
                    sol.Num_Factura = dr.GetInt32(dr.GetOrdinal("Num_Factura"));
                    sol.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                    sol.Tipo_Servicio = dr.GetString(dr.GetOrdinal("Tipo_Servicio"));
                    sol.Estado = dr.GetString(dr.GetOrdinal("estado"));
                    sol.Accion = dr.IsDBNull(dr.GetOrdinal("Accion")) ? "" : dr.GetValue(dr.GetOrdinal("Accion")).ToString();
                    lstSolicitud.Add(sol);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitud(Sesion Sesion, Solicitud solicitud, ref List<Producto> lstProducto, ref List<Documento> lstDocumentos)
        {
            try
            {
                SqlDataReader dr = null;
                Documento documento;
                Producto producto;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);

                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Solicitud"
                                       
                                      };
                object[] Valores = { 
                                         Sesion.Id_Emp,
                                         Sesion.Id_Cd,
                                         solicitud.Id_Solicitud 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSolicitud_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    solicitud.Id_Emp = Sesion.Id_Emp;
                    solicitud.Id_Cd = Sesion.Id_Cd;
                    solicitud.Nom_Sucursal = Sesion.Cd_Nombre;
                    solicitud.Id_Solicitud = dr.GetInt32(dr.GetOrdinal("Id_Solicitud"));
                    solicitud.Id_Cliente = dr.GetInt32(dr.GetOrdinal("Id_Cliente"));
                    solicitud.Cte_Nom = dr.GetString(dr.GetOrdinal("Nom_Cliente"));
                    solicitud.Correo = dr.GetString(dr.GetOrdinal("Correo"));
                    solicitud.Id_Investigador = dr.GetInt32(dr.GetOrdinal("Id_Investigador"));
                    solicitud.Nom_Investigador = dr.GetString(dr.GetOrdinal("Nom_Investigador"));
                    solicitud.Inv_Correo = dr.IsDBNull(dr.GetOrdinal("Inv_Correo")) ? "" : dr.GetValue(dr.GetOrdinal("Inv_Correo")).ToString();
                    solicitud.ConCopia = dr.IsDBNull(dr.GetOrdinal("Concopia")) ? "" : dr.GetValue(dr.GetOrdinal("Concopia")).ToString();
                    solicitud.Id_Estado = dr.GetInt32(dr.GetOrdinal("Id_Estado"));
                    solicitud.Id_Prioridad = dr.GetInt32(dr.GetOrdinal("Id_Prioridad"));
                    solicitud.Id_tServicio = dr.GetInt32(dr.GetOrdinal("Id_tServicio"));
                    solicitud.Id_Accion = dr.GetInt32(dr.GetOrdinal("Id_Accion"));
                    solicitud.Id_Categoria = dr.GetInt32(dr.GetOrdinal("Id_tQueja"));
                    solicitud.FechaEvento = dr.GetDateTime(dr.GetOrdinal("Fec_Evento"));
                    solicitud.Motivos = dr.GetString(dr.GetOrdinal("Motivos"));
                    solicitud.OtroMotivo = dr.GetString(dr.GetOrdinal("OtroMotivo"));
                    solicitud.DondeOcurrio = dr.IsDBNull(dr.GetOrdinal("DondeOcurrio")) ? "" : dr.GetValue(dr.GetOrdinal("DondeOcurrio")).ToString();
                    solicitud.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCreacion")))) solicitud.FechaCreacion = null; else solicitud.FechaCreacion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCreacion")));
                    solicitud.Comentarios = dr.GetString(dr.GetOrdinal("Comentarios"));
                    solicitud.MotRechazo = dr.GetString(dr.GetOrdinal("MotRechazo"));
                    solicitud.NumTiempoEstimado = dr.GetInt32(dr.GetOrdinal("Tiempo_res"));
                }

                //Consulta productos de la queja 
                dr.Close();

                string[] Parametro = {
                                 "@Id_Emp"
                                ,"@Id_Cd"
                                ,"@Id_Queja"
                             };
                object[] Valor =   
                             {
                                 Sesion.Id_Emp,
                                 Sesion.Id_Cd,
                                 solicitud.Id_Solicitud
                            };


                sqlcmd = CapaDatos.GenerarSqlCommand("spCapProductos_Consulta", ref dr, Parametro, Valor);
                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    producto.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Uni_Empaque"))) ? 0 : float.Parse(dr.GetValue(dr.GetOrdinal("Uni_Empaque")).ToString());
                    producto.Presentacion = dr.GetString(dr.GetOrdinal("Presentacion"));
                    producto.Lote = dr.GetString(dr.GetOrdinal("Lote"));
                    producto.Fabricacion = dr.GetDateTime(dr.GetOrdinal("Fec_Fabricacion"));
                    producto.Caducidad = dr.GetDateTime(dr.GetOrdinal("Fec_Caducidad"));
                    producto.Marca = dr.GetString(dr.GetOrdinal("Marca"));
                    producto.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_AAA")));
                    producto.Num_Fac = dr.GetInt32(dr.GetOrdinal("Num_Fac"));
                    producto.Nom_Prov = dr.GetString(dr.GetOrdinal("Nom_Prov"));
                    producto.Mantener = dr.GetBoolean(dr.GetOrdinal("Mantener"));
                    producto.Rem_Trans = dr.GetInt32(dr.GetOrdinal("Rem_Trans"));
                    producto.IdDocumento = dr.GetInt32(dr.GetOrdinal("Id_Doc"));
                    producto.Costoestandar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo_Estandar")));
                    producto.NCredito = dr.GetInt32(dr.GetOrdinal("Nota_Credito"));

                    lstProducto.Add(producto);
                }

                //Consulta documentos de la queja 
                dr.Close();

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentos_Consulta", ref dr, Parametro, Valor);
                while (dr.Read())
                {
                    documento = new Documento();
                    documento.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    documento.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    documento.Id_Doc = dr.GetInt32(dr.GetOrdinal("Id_Doc"));
                    documento.Doc_Nombre = dr.GetString(dr.GetOrdinal("Doc_Nombre"));
                    documento.Formato = dr.GetString(dr.GetOrdinal("Formato"));
                    documento.Tamano = dr.GetInt64(dr.GetOrdinal("Tamano"));
                    documento.TipoDoc = dr.GetString(dr.GetOrdinal("TipoDoc"));
                    lstDocumentos.Add(documento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaSolicitud(Sesion sesion, ref string respuesta, ref Solicitud sol)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                respuesta = "";
                string[] Parametros = { 
                                       
		                                	"@Id_Solicitud"
                                      };
                object[] Valores = { 
                                            sol.Id_Solicitud
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCancelaSolicitud", ref respuesta, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}