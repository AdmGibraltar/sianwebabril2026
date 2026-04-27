using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_CN_Matriz
    {
        public void ConsultarMatriz(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { datos.id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_ConsultarMAtriz", ref dr, Parametros, Valores);
                CN_Matriz Usu;
                while (dr.Read())
                {
                    Usu = new CN_Matriz();
                    Usu.id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Usu.nombre = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));
                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarVinculacion(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_matriz",
                                        "@Id_Cd",
                                        "@id_estructura"};
                object[] Valores = { datos.id_matriz,
                                     datos.id_Cd,
                                     datos.id_Estructura == 0 ? (object)null : datos.id_Estructura};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_ConsultarEstructura", ref dr, Parametros, Valores);
                CN_Matriz Usu;
                while (dr.Read())
                {
                    Usu = new CN_Matriz();
                    Usu.id = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    Usu.nombrenodo = dr.IsDBNull(dr.GetOrdinal("nombrenodo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombrenodo")));
                    Usu.id_Cte = dr.IsDBNull(dr.GetOrdinal("id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_Cte")));
                    Usu.nombreCliente = dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombreCliente")));
                    Usu.id_matriz = dr.IsDBNull(dr.GetOrdinal("id_matriz")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_matriz")));
                    Usu.NombreMatriz = dr.IsDBNull(dr.GetOrdinal("NombreMatriz")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreMatriz")));
                    Usu.id_acys = dr.IsDBNull(dr.GetOrdinal("id_Acys")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_Acys")));
                    Usu.AcysNombre = dr.IsDBNull(dr.GetOrdinal("NombreMatriz")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("AcysNombre")));
                    Usu.IdEstatus = dr.IsDBNull(dr.GetOrdinal("IdEstatus")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdEstatus")));
                    Usu.nombreEstatus = dr.IsDBNull(dr.GetOrdinal("nombreEstatus")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombreEstatus")));
                    Usu.mov80 = dr.IsDBNull(dr.GetOrdinal("mov80")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("mov80")));
                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarUsuario(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@id_matriz" };
                object[] Valores = { datos.id_matriz };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_ConsultarUsuario", ref dr, Parametros, Valores);
                CN_Matriz Usu;
                while (dr.Read())
                {
                    Usu = new CN_Matriz();
                    Usu.id = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    Usu.nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    Usu.correo = dr.IsDBNull(dr.GetOrdinal("correo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));

                    Usu.cdik = dr.IsDBNull(dr.GetOrdinal("cdik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cdik")));
                    Usu.telefono = dr.IsDBNull(dr.GetOrdinal("telefono")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("telefono")));
                    Usu.rol_auditorias = dr.IsDBNull(dr.GetOrdinal("rol_auditorias")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rol_auditorias")));
                    Usu.contrasenia = dr.IsDBNull(dr.GetOrdinal("contrasenia")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("contrasenia")));
                    Usu.rol_ecommerce = dr.IsDBNull(dr.GetOrdinal("rol_ecommerce")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rol_ecommerce")));

                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesMov80(ref List<CN_Matriz> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { };
                object[] Valores = { };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_ConsultarRemisionesMov80", ref dr, Parametros, Valores);
                CN_Matriz Usu;
                while (dr.Read())
                {
                    Usu = new CN_Matriz();
                    Usu.id = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    Usu.nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));

                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitudes(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {    "@id_Cd"    ,
                                           "@Id_matriz" ,
                                           "@Estructura" ,
                                           "@EsDesvinc" };
                object[] Valores = { datos.id_Cd,
                                     datos.id_matriz,
                                     datos.id_Estructura,
                                     datos.Desvinc};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_ConsultarSolicitudes", ref dr, Parametros, Valores);
                CN_Matriz Usu;
                while (dr.Read())
                {
                    Usu = new CN_Matriz();
                    Usu.id = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id")));
                    Usu.clienteSian = dr.IsDBNull(dr.GetOrdinal("clienteSian")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("clienteSian")));
                    Usu.razonsocial = dr.IsDBNull(dr.GetOrdinal("razonsocial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("razonsocial")));
                    Usu.territorio = dr.IsDBNull(dr.GetOrdinal("territorio")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("territorio")));
                    Usu.sucursal = dr.IsDBNull(dr.GetOrdinal("sucursal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("sucursal")));
                    Usu.asesorid = dr.IsDBNull(dr.GetOrdinal("asesorid")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("asesorid")));
                    Usu.estatus = dr.IsDBNull(dr.GetOrdinal("estatus")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("estatus")));
                    Usu.usuario = dr.IsDBNull(dr.GetOrdinal("usuario")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("usuario")));
                    Usu.comentarios = dr.IsDBNull(dr.GetOrdinal("comentarios")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("comentarios")));
                    Usu.calle = dr.IsDBNull(dr.GetOrdinal("calle")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("calle")));
                    Usu.numinterior = dr.IsDBNull(dr.GetOrdinal("numinterior")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("numinterior")));
                    Usu.numExterior = dr.IsDBNull(dr.GetOrdinal("numExterior")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("numExterior")));
                    Usu.colonia = dr.IsDBNull(dr.GetOrdinal("colonia")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("colonia")));
                    Usu.municipio = dr.IsDBNull(dr.GetOrdinal("municipio")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("municipio")));
                    Usu.Cp = dr.IsDBNull(dr.GetOrdinal("Cp")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cp")));
                    Usu.estado = dr.IsDBNull(dr.GetOrdinal("estado")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("estado")));
                    Usu.RFC = dr.IsDBNull(dr.GetOrdinal("RFC")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("RFC")));
                    Usu.telefonos = dr.IsDBNull(dr.GetOrdinal("telefonos")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("telefonos")));
                    Usu.fax = dr.IsDBNull(dr.GetOrdinal("fax")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("fax")));

                    Lista.Add(Usu);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarSolicitudes(CN_Matriz datos, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {     "@Id_Matriz",
                                            "@Id_Estructura",
                                            "@Sucursal",
                                            "@ClienteSIAN",
                                            "@Territorio",
                                            "@RazonSocial",
                                            "@Remision_Cta_Nac",
                                            "@AsesorId",
                                            "@Comentarios",
                                            "@Estatus",
                                            "@Fecha",
                                            "@Usuario",
                                            "@id_Emp",
                                            "@Id_Cd"  };
                object[] Valores = { datos.id_matriz,
                                     datos.id_Estructura,
                                     datos.sucursal,
                                     datos.clienteSian,
                                     datos.territorio,
                                     datos.razonsocial,
                                     datos.Rem_Cta_Nac,
                                     datos.asesorid,
                                     datos.comentarios,
                                     datos.IdEstatus,
                                     datos.Fecha,
                                     datos.usuario,
                                     datos.id_emp,
                                     datos.id_Cd};
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_insertarSolicitud", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSolicitudesdirfiscal(CN_Matriz datos, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id",
                                        "@Calle",
                                        "@NumInterior",
                                        "@NumExterior",
                                        "@Colonia",
                                        "@Municipio",
                                        "@CP",
                                        "@Estado",
                                        "@RFC",
                                        "@Telefonos",
                                        "@FAX",
                                        "@sucursal"};
                object[] Valores = { datos.id,
                                     datos.calle.Length ==0 ?  (object)null :  datos.calle,
                                     datos.numinterior.Length ==0 ?  (object)null :  datos.numinterior,
                                     datos.numExterior.Length ==0 ?  (object)null :  datos.numExterior,
                                     datos.colonia.Length ==0 ?  (object)null :  datos.colonia,
                                     datos.municipio.Length ==0 ?  (object)null :  datos.municipio,
                                     datos.Cp.Length ==0 ?  (object)null :  datos.Cp,
                                     datos.estado.Length ==0 ?  (object)null :  datos.estado,
                                     datos.RFC.Length ==0 ?  (object)null :  datos.RFC,
                                     datos.telefono.Length ==0 ?  (object)null :  datos.telefono,
                                     datos.fax.Length ==0 ?  (object)null :  datos.fax,
                                     datos.sucursal };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCN_insertarSolicituddirfiscal", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarAcysProductos(CatCnac_Producto Registros, ref List<CatCnac_Producto> lista, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_tg"
                                          ,"@Id_acys"
                                      };
                object[] Valores = {
                                       Registros.Id_TG
                                       ,Registros.Id_ACYS
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatCNac_AcysProd", ref dr, Parametros, Valores);
                CatCnac_Producto Datos;

                while (dr.Read())
                {
                    Datos = new CatCnac_Producto();
                    Datos.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_prd")) ? 0 : Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_prd")));
                    Datos.Id_ACYS = dr.IsDBNull(dr.GetOrdinal("Id_ACYS")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ACYS")));
                    Datos.Id_TG = dr.IsDBNull(dr.GetOrdinal("Id_TG")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));
                    Datos.Cantidad = dr.IsDBNull(dr.GetOrdinal("Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    Datos.Precio = dr.IsDBNull(dr.GetOrdinal("Precio")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio")));
                    Datos.Subtotal = dr.IsDBNull(dr.GetOrdinal("Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Subtotal")));
                    Datos.Frecuencia = dr.IsDBNull(dr.GetOrdinal("Frecuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Frecuencia")));

                    Datos.Lun = dr.IsDBNull(dr.GetOrdinal("Lun")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Lun")));
                    Datos.Mar = dr.IsDBNull(dr.GetOrdinal("Mar")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mar")));
                    Datos.Mie = dr.IsDBNull(dr.GetOrdinal("Mie")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mie")));
                    Datos.Jue = dr.IsDBNull(dr.GetOrdinal("Jue")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Jue")));
                    Datos.Vie = dr.IsDBNull(dr.GetOrdinal("Vie")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Vie")));
                    Datos.Sab = dr.IsDBNull(dr.GetOrdinal("Sab")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Sab")));

                    Datos.Documento = dr.IsDBNull(dr.GetOrdinal("Documento")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Documento")));
                    Datos.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
                    Datos.Unidad = dr.IsDBNull(dr.GetOrdinal("Unidad")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Unidad")));
                    Datos.Presentacion = dr.IsDBNull(dr.GetOrdinal("Presentacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    Datos.RequiereOC = dr.IsDBNull(dr.GetOrdinal("RequiereOC")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RequiereOC")));
                    Datos.Acs_FrecuenciaTipo = dr.IsDBNull(dr.GetOrdinal("Acs_FrecuenciaTipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_FrecuenciaTipo")));
                    lista.Add(Datos);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}