using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{

    public class CD_AlertaAutorizacion
    {

        public void ConsultaAlertaAutorizacionLista(AlertaAutorizacion alertaautorizacion, ref List<AlertaAutorizacion> listalertaautorizaciones, string seleccion, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@IdAutorizacionPrecio",
                                         "@Id_Cd",
                                        "@IdRepresentante",
                                         "@TipoAutorizacion",
                                         "@Id_Cte",
                                         "@Id_Prd",
                                         "@IdEstatus",
                                         "@FechaInicial",
                                         "@FechaFinal",
                                         "@Seleccion"};
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.IdAutorizacionPrecio,
                                       alertaautorizacion.Id_Cd,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.TipoAutorizacion,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Prd,
                                       alertaautorizacion.Estatus,
                                       alertaautorizacion.FechaInicial,
                                       alertaautorizacion.FechaFinal,
                                       seleccion

                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosConsulta", ref dr, Parametros, Valores);

                AlertaAutorizacion c;
                while (dr.Read())
                {
                    c = new AlertaAutorizacion();

                    c.Tipo = dr["Tipo"].ToString() == "" ? "" : Convert.ToString(dr["Tipo"]);
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.IdAutorizacionPrecio = Convert.ToInt32(dr["IdAutorizacionPrecio"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.IdRepresentante = Convert.ToInt32(dr["IdRepresentante"]);
                    c.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    c.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                    c.TipoAutorizacion = Convert.ToInt32(dr["TipoAutorizacion"]);
                    c.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    c.Precio_Vta = dr["Precio_Vta"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_Vta"]);
                    c.Precio_VtaAutorizado = dr["Precio_VtaAutorizado"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_VtaAutorizado"]);
                    c.FechaSolicitud = dr["FechaSolicitud"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaSolicitud"]);
                    c.Req_Aut_Director = dr["Req_Aut_Director"].ToString() == "" ? 0 : Convert.ToInt32(dr["Req_Aut_Director"]);
                    c.Estatus = dr["Estatus"].ToString() == "" ? 0 : Convert.ToInt32(dr["Estatus"]);
                    c.IdUSolicita = dr["IdUSolicita"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUSolicita"]);
                    c.IdUsuarioGteAutorizo = dr["IdUsuarioGteAutorizo"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUsuarioGteAutorizo"]);
                    c.IdUsuarioDirAutorizo = dr["IdUsuarioDirAutorizo"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUsuarioDirAutorizo"]);
                    c.FechaAutorizacionGte = dr["FechaAutorizacionGte"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAutorizacionGte"]);
                    c.Fecha_UltMod = dr["Fecha_UltMod"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_UltMod"]);
                    c.FechaAutorizacionDir = dr["FechaAutorizacionDir"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAutorizacionDir"]);
                    c.FechaRechazoGte = dr["FechaRechazoGte"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaRechazoGte"]);
                    c.Fecha_UltMod = dr["FechaRechazoDir"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaRechazoDir"]);
                    c.IdUsuarioGteRechazo = dr["IdUsuarioGteRechazo"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUsuarioGteRechazo"]);
                    c.IdUsuarioDirRechazo = dr["IdUsuarioDirRechazo"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUsuarioDirRechazo"]);
                    c.MotivoRechazo = dr["MotivoRechazo"].ToString().Trim();
                    c.MotivoCancelacion = dr["MotivoCancelacion"].ToString().Trim();
                    c.Activo = dr["Activo"].ToString() == "" ? false : Convert.ToBoolean(dr["Activo"]);
                    c.Nom_Representante = dr["Nom_Representante"].ToString().Trim();
                    c.Cte_NomComercial = dr["Cte_NomComercial"].ToString().Trim();
                    c.Nom_CDI = dr["NomCDI"].ToString().Trim();
                    c.Prd_Descripcion = dr["Prd_Descripcion"].ToString().Trim();
                    c.NomEstatus = dr["NomEstatus"].ToString().Trim();
                    c.NomTipoSolicitud = dr["NomTipoSolicitud"].ToString().Trim();
                    c.Justificacion = dr["Justificacion"].ToString().Trim();
                    c.PrecioLista = dr["PrecioLista"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioLista"]);

                    c.Precio_MinimoGte = dr["Precio_MinimoGte"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoGte"]);
                    c.Precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);

                    c.Precio_AAA = dr["Precio_AAA"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_AAA"]);
                    c.Utilidad = dr["Utilidad"].ToString() == "" ? 0 : Convert.ToDouble(dr["Utilidad"]);
                    if (c.Precio_Vta > 0)
                    {
                        c.Porc_Utilidad = c.Utilidad == 0 ? 0 : c.Utilidad / c.Precio_Vta;
                    }
                    else
                    {
                        c.Porc_Utilidad = 0;
                    }
                    c.Importe = c.Precio_Vta * c.Cantidad;
                    c.Importe_Utilidad = c.Utilidad * c.Cantidad;
                    c.Id_Motivo = Convert.ToInt32(dr["Id_Motivo"]);
                    c.Nom_Motivo = dr["NomCDI"].ToString().Trim();
                    c.JustificacionMemo = dr["JustificacionMemo"].ToString().Trim();
                    c.Id_Cpr = dr["Id_Cpr"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cpr"]);
                    c.IdAutorizacionAnterior = dr["IdAutorizacionAnterior"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdAutorizacionAnterior"]);
                    //precioObjetivo
                    c.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);
                    c.Id_Tamaño = dr["Id_Tamaño"].ToString().Trim();
                    //julio 23 tipo_Cliente ( CN CL CC ) 
                    c.Tipo_Cliente = dr["Tipo_Cliente"].ToString().Trim();
                    c.Id_MotivoRechazo = Convert.ToInt32(dr["Id_MotivoRechazo"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("FechaVigencia")))
                    {
                        c.FechaVigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVigencia")));
                    }
                    //alertaprecios



                    listalertaautorizaciones.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaAlertaAutorizacionRentabilidad(int id_Emp, int idAutorizacionPrecio, int id_cd, ref List<EstadisticaRentabilidad> listarentabilidad, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@IdAutorizacionPrecio",
                                         "@Id_Cd"};
                object[] Valores = {
                                       id_Emp,
                                       idAutorizacionPrecio,
                                       id_cd

                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosAutorizacionRentabilidad_Consultar", ref dr, Parametros, Valores);

                EstadisticaRentabilidad c;
                while (dr.Read())
                {
                    c = new EstadisticaRentabilidad();

                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    c.Id_Cd_Ver = Convert.ToInt32(dr["Id_Cd"]);

                    c.VentaNetaMon = dr["VentaNetaMon"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentaNetaMon"]);
                    c.UtilidadMon = dr["UtilidadMon"].ToString() == "" ? 0 : Convert.ToDouble(dr["UtilidadMon"]);

                    c.UtilidadPorc = dr["UtilidadPorc"].ToString() == "" ? 0 : Convert.ToDouble(dr["UtilidadPorc"]);
                    c.UafirMensualMon = dr["UafirMensualMon"].ToString() == "" ? 0 : Convert.ToDouble(dr["UafirMensualMon"]);
                    c.UafirMensualPorc = dr["UafirMensualPorc"].ToString() == "" ? 0 : Convert.ToDouble(dr["UafirMensualPorc"]);

                    c.UtilidadRemanenteMon = dr["UtilidadRemanenteMon"].ToString() == "" ? 0 : Convert.ToDouble(dr["UtilidadRemanenteMon"]);

                    listarentabilidad.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AutorizarSolicitudGerente(AlertaAutorizacion alertas, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                //JFCV Convenio
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdAutorizacionPrecio",
                                        "@IdEstatus",
                                        "@IdUsuarioGteAutorizo",
                                        "@Justificacion",
                                        "@FechaVigencia",
                                        "@Precio_Vta",
                                        "@MotivoRechazo",
                                        "@Id_MotivoRechazo" };

                object[] Valores = { alertas.Id_Emp ,
                                     alertas.Id_Cd,
                                     alertas.IdAutorizacionPrecio ,
                                     alertas.Estatus,
                                     alertas.IdUsuarioGteAutorizo  ,
                                     alertas.Justificacion,
                                     alertas.FechaVigencia,
                                     alertas.Precio_Vta,
                                     alertas.MotivoRechazo,
                                     alertas.Id_MotivoRechazo
                                  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosActualizaEstatus", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AlertaPrecioConsultaPrecio(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaprecios, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                SqlCommand sqlcmd = new SqlCommand();



                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                         "@Id_Prd",
                                         "@Precio_Vta",
                                         "@Id_Rik",
                                         "@Id_Ter" };
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Prd,
                                       alertaautorizacion.Precio_Vta,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Ter
                                   };
                //      leads.IdGiroEmpresa,

                sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosValidaPrecio", ref dr, Parametros, Valores);


                AlertaAutorizacion c;
                while (dr.Read())
                {
                    alertaprecios.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    alertaprecios.IdAutorizacionPrecio = Convert.ToInt32(dr["Id_CapAlertaPrecio"]);
                    alertaprecios.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    alertaprecios.IdRepresentante = Convert.ToInt32(dr["Id_Rik"]);
                    alertaprecios.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);

                    alertaprecios.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    alertaprecios.Precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);
                    //alertaprecios.FechaSolicitud = dr["FechaSolicitud"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaSolicitud"]);
                    alertaprecios.Precio_MinimoGte = dr["Precio_MinimoGte"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoGte"]);
                    alertaprecios.PrecioLista = dr["PrecioLista"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioLista"]);
                    alertaprecios.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);
                    alertaprecios.Id_Tamaño = dr["Id_Tamaño"].ToString();

                    alertaprecios.Precio_AAA = dr["Precio_AAA"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_AAA"]);
                    alertaprecios.Utilidad = dr["Utilidad"].ToString() == "" ? 0 : Convert.ToDouble(dr["Utilidad"]);
                    alertaprecios.Id_Cpr = dr["Id_Cpr"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cpr"]);
                    alertaprecios.IdAutorizacionAnterior = dr["IdAutorizacionAnterior"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdAutorizacionAnterior"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("FechaVigencia")))
                    {
                        alertaprecios.FechaVigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVigencia")));
                    }
                    //alertaprecios

                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AlertaPrecioConsultaPrecioAcys(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaprecios, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                SqlCommand sqlcmd = new SqlCommand();


                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                         "@Id_Prd",
                                         "@Precio_Vta",
                                         "@Id_Rik",
                                         "@Id_Ter" };
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Prd,
                                       alertaautorizacion.Precio_Vta,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Ter
                                   };
                //      leads.IdGiroEmpresa,

                sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosValidaPrecioAcys", ref dr, Parametros, Valores);

                AlertaAutorizacion c;
                while (dr.Read())
                {
                    alertaprecios.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    alertaprecios.IdAutorizacionPrecio = Convert.ToInt32(dr["Id_CapAlertaPrecio"]);
                    alertaprecios.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    alertaprecios.IdRepresentante = Convert.ToInt32(dr["Id_Rik"]);
                    alertaprecios.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);

                    alertaprecios.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    alertaprecios.Precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);
                    //alertaprecios.FechaSolicitud = dr["FechaSolicitud"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaSolicitud"]);
                    alertaprecios.Precio_MinimoGte = dr["Precio_MinimoGte"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoGte"]);
                    alertaprecios.PrecioLista = dr["PrecioLista"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioLista"]);

                    alertaprecios.Precio_AAA = dr["Precio_AAA"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_AAA"]);
                    alertaprecios.Utilidad = dr["Utilidad"].ToString() == "" ? 0 : Convert.ToDouble(dr["Utilidad"]);
                    alertaprecios.Id_Cpr = dr["Id_Cpr"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cpr"]);
                    alertaprecios.IdAutorizacionAnterior = dr["IdAutorizacionAnterior"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdAutorizacionAnterior"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("FechaVigencia")))
                    {
                        alertaprecios.FechaVigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVigencia")));
                    }
                    //alertaprecios
                    //JFCV agregar precioObjetivo 
                    alertaprecios.Id_Tamaño = dr["Id_Tamaño"].ToString() == "" ? "E" : dr["Id_Tamaño"].ToString().Trim();
                    alertaprecios.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);

                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AlertaPrecioConsultaPrecioGPMA(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaprecios, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                         "@Id_Prd",
                                         "@Precio_Vta",
                                         "@Id_Rik",
                                         "@Id_Ter" };
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Prd,
                                       alertaautorizacion.Precio_Vta,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Ter
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosValidaPrecioGPMA", ref dr, Parametros, Valores);

                AlertaAutorizacion c;
                while (dr.Read())
                {


                    alertaprecios.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    alertaprecios.IdAutorizacionPrecio = Convert.ToInt32(dr["Id_CapAlertaPrecio"]);
                    alertaprecios.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    alertaprecios.IdRepresentante = Convert.ToInt32(dr["Id_Rik"]);
                    alertaprecios.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);

                    alertaprecios.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    alertaprecios.Precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);
                    //alertaprecios.FechaSolicitud = dr["FechaSolicitud"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaSolicitud"]);
                    alertaprecios.Precio_MinimoGte = dr["Precio_MinimoGte"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoGte"]);
                    alertaprecios.PrecioLista = dr["PrecioLista"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioLista"]);

                    alertaprecios.Precio_AAA = dr["Precio_AAA"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_AAA"]);
                    alertaprecios.Utilidad = dr["Utilidad"].ToString() == "" ? 0 : Convert.ToDouble(dr["Utilidad"]);
                    alertaprecios.Id_Cpr = dr["Id_Cpr"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cpr"]);
                    alertaprecios.IdAutorizacionAnterior = dr["IdAutorizacionAnterior"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdAutorizacionAnterior"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("FechaVigencia")))
                    {
                        alertaprecios.FechaVigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVigencia")));
                    }
                    //alertaprecios
                    //JFCV agregar precioObjetivo 
                    alertaprecios.Id_Tamaño = dr["Id_Tamaño"].ToString() == "" ? "E" : dr["Id_Tamaño"].ToString().Trim();
                    alertaprecios.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //llenar el grid de productos en la autorización de GPMa del gerente
        public void AlertaPrecioConsultaGPMaDetalle(AlertaAutorizacion alertaautorizacion, ref List<AlertaAutorizacion> listaProductos, string seleccion, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
            "@Id_Emp",
            "@IdAutorizacionPrecio",
            "@Id_Cd",
            "@IdRepresentante",
            "@TipoAutorizacion",
            "@Id_Cte",
            "@Id_Prd",
            "@IdEstatus",
            "@FechaInicial",
            "@FechaFinal",
            "@Seleccion",
            "@Id_ReporteGP"
        };

                object[] Valores = {
            alertaautorizacion.Id_Emp,
            alertaautorizacion.IdAutorizacionPrecio,
            alertaautorizacion.Id_Cd,
            alertaautorizacion.IdRepresentante,
            alertaautorizacion.TipoAutorizacion,
            alertaautorizacion.Id_Cte,
            alertaautorizacion.Id_Prd,
            alertaautorizacion.Estatus,
            alertaautorizacion.FechaInicial,
            alertaautorizacion.FechaFinal,
            seleccion,
            alertaautorizacion.Id_ReporteGP
        };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosConsultaGPMa", ref dr, Parametros, Valores);

                AlertaAutorizacion producto;
                while (dr.Read())
                {
                    producto = new AlertaAutorizacion();
                    producto.Tipo = dr["Tipo"].ToString();
                    producto.IdAutorizacionPrecio = Convert.ToInt32(dr["IdAutorizacionPrecio"]);
                    producto.Id_AutorizacionPrecioDet = Convert.ToInt32(dr["Id_AutorizacionPrecioDet"]);
                    producto.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    producto.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    producto.IdRepresentante = Convert.ToInt32(dr["IdRepresentante"]);
                    producto.Nom_Representante = dr["Nom_Representante"].ToString();
                    producto.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    producto.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    producto.Precio_Vta = dr["Precio_Vta"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_Vta"]);
                    producto.PrecioLista = dr["PrecioLista"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioLista"]);
                    producto.Precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);
                    producto.Precio_MinimoGte = dr["Precio_MinimoGte"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoGte"]);
                    producto.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);
                    producto.Precio_AAA = dr["Precio_AAA"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_AAA"]);
                    producto.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                    producto.Id_Tamaño = dr["Id_tamaño"].ToString();
                    producto.Estatus = dr["Estatus"].ToString() == "" ? 0 : Convert.ToInt32(dr["Estatus"]);

                    producto.Id_ReporteGP = Convert.ToInt32(dr["Id_ReporteGP"]);
                    producto.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    producto.Nom_CDI = dr["NomCDI"].ToString();
                    producto.Id_Ter = dr["Id_Ter"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Ter"]);

                    producto.NomEstatus = dr["NomEstatus"].ToString();
                    producto.NomTipoSolicitud = dr["NomTipoSolicitud"].ToString();
                    producto.TipoAutorizacion = dr["TipoAutorizacion"].ToString() == "" ? 0 : Convert.ToInt32(dr["TipoAutorizacion"]);
                    producto.Tipo_Cliente = dr["Tipo_Cliente"].ToString();
                    //producto.FechaSolicitud = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaSolicitud")));
                    // Agregar el producto a la lista
                    // Campos agregados
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaVigencia")))
                    {
                        producto.FechaVigencia = Convert.ToDateTime(dr["FechaVigencia"]);
                    }
                    producto.JustificacionMemo = dr["JustificacionMemo"].ToString(); // Agregado
                    producto.JustificacionGeneral = dr["JustificacionGeneral"].ToString(); // Agregado
                    producto.Justificacion = dr["Justificacion"].ToString(); // Agregado
                    producto.Nom_Motivo = dr["Nom_Motivo"].ToString(); // Agregado
                    producto.Req_Aut_Director = dr["Req_Aut_Director"].ToString() == "" ? 0 : Convert.ToInt32(dr["Req_Aut_Director"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("FechaSolicitud")))
                    {
                        producto.FechaSolicitud = Convert.ToDateTime(dr["FechaSolicitud"]);
                    }
                    if (seleccion == "Encabezado")
                    {
                        producto.UnidadesProyectadas = 0;
                        producto.VentaProy = 0;

                    }
                    else
                    {
                        producto.UnidadesProyectadas = dr["UnidadesProyectadas"].ToString() == "" ? 0 : Convert.ToDouble(dr["UnidadesProyectadas"]);
                        producto.VentaProy = dr["VentaProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentaProy"]);

                    }



                    listaProductos.Add(producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaMotivos(int id_Movimiento, Sesion sesion, ref List<AlertaAutorizacion> alertaAutorizaciones)
        {


            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;

                string[] parametros = {
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_Movimiento.ToString()
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatMotivoAlerta_Combo_Todos", ref dr, parametros, Valores);

                AlertaAutorizacion alertaAutorizacion = new AlertaAutorizacion();
                while (dr.Read())
                {
                    alertaAutorizacion = new AlertaAutorizacion();
                    alertaAutorizacion.Id_Motivo = dr.GetInt32(dr.GetOrdinal("Id"));
                    alertaAutorizacion.Nom_Motivo = dr.GetString(dr.GetOrdinal("Descripcion"));
                    alertaAutorizaciones.Add(alertaAutorizacion);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void CapAlertaAutorizacioInsertar(AlertaAutorizacion alertaautorizacion, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                ///JFCV precioobjetivo 13 nov 
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@IdRepresentante",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@TipoAutorizacion",
                                          "@Id_Prd",
                                          "@Precio_Vta",
                                          "@Precio_MinimoRik",
                                          "@Precio_MinimoGte",
                                          "@Precio_VtaAutorizado",
                                          "@FechaSolicitud",
                                          "@Req_Aut_Director",
                                          "@Estatus",
                                          "@IdUSolicita",
                                          "@IdUsuarioGteAutorizo",
                                          "@IdUsuarioDirAutorizo",
                                          "@FechaAutorizacionGte",
                                          "@Fecha_UltMod",
                                          "@FechaAutorizacionDir",
                                          "@FechaRechazoGte",
                                          "@FechaRechazoDir",
                                          "@IdUsuarioGteRechazo",
                                          "@IdUsuarioDirRechazo",
                                          "@MotivoRechazo",
                                          "@MotivoCancelacion",
                                          "@Activo",
                                          "@Justificacion",
                                          "@JustificacionGte",
                                          "@PrecioLista",
                                          "@Id_Motivo",
                                          "@JustificacionMemo",
                                          "@FechaVigencia",
                                          "@PrecioObjetivo" ,
                                          "@Id_Tamaño"
                                      };
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd ,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Ter ,
                                       alertaautorizacion.TipoAutorizacion ,
                                       alertaautorizacion.Id_Prd ,
                                       alertaautorizacion.Precio_Vta ,
                                       alertaautorizacion.Precio_MinimoRik ,
                                       alertaautorizacion.Precio_MinimoGte ,
                                       alertaautorizacion.Precio_VtaAutorizado ,
                                       alertaautorizacion.FechaSolicitud ,
                                       alertaautorizacion.Req_Aut_Director ,
                                       alertaautorizacion.Estatus ,
                                       alertaautorizacion.IdUSolicita ,
                                       alertaautorizacion.IdUsuarioGteAutorizo ,
                                       alertaautorizacion.IdUsuarioDirAutorizo ,
                                       alertaautorizacion.FechaAutorizacionGte ,
                                       alertaautorizacion.Fecha_UltMod ,
                                       alertaautorizacion.FechaAutorizacionDir ,
                                       alertaautorizacion.FechaRechazoGte ,
                                       alertaautorizacion.FechaRechazoDir ,
                                       alertaautorizacion.IdUsuarioGteRechazo ,
                                       alertaautorizacion.IdUsuarioDirRechazo ,
                                       alertaautorizacion.MotivoRechazo ,
                                       alertaautorizacion.MotivoCancelacion ,
                                       alertaautorizacion.Activo ,
                                       alertaautorizacion.Justificacion ,
                                       alertaautorizacion.JustificacionGte ,
                                       alertaautorizacion.PrecioLista,
                                       alertaautorizacion.Id_Motivo,
                                       alertaautorizacion.JustificacionMemo,
                                       alertaautorizacion.FechaVigencia,
                                       alertaautorizacion.PrecioObjetivo,
                                       alertaautorizacion.Id_Tamaño
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAlertaPreciosInsertar", ref verificador, Parametros, Valores);

                alertaautorizacion.IdAutorizacionPrecio = verificador;


                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
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

        public void CapAlertaAutorizacioRentabilidadInsertar(AlertaAutorizacion alertaautorizacion, EstadisticaRentabilidad estrentabilidad, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@IdAutorizacionPrecio",
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@IdRepresentante",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@VentaNetaMon",
                                          "@UtilidadMon",
                                          "@UtilidadPorc",
                                          "@UafirMensualMon",
                                          "@UafirMensualPorc",
                                          "@UafirCliente",
                                          "@UtilidadRemanenteMon",
                                      };
                object[] Valores = {   alertaautorizacion.IdAutorizacionPrecio,
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd ,
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Ter ,
                                       estrentabilidad.VentaNetaMon,
                                       estrentabilidad.UtilidadMon,
                                       estrentabilidad.UtilidadPorc,
                                       estrentabilidad.UafirMensualMon,
                                       estrentabilidad.UafirMensualPorc,
                                       (estrentabilidad.UafirMensualMon / estrentabilidad.VentaNetaMon)*100,// uafir cte = uafirporc mensual cte 
                                       estrentabilidad.UtilidadRemanenteMon
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAlertaPreciosAutorizacionRentabilidad_Insertar", ref verificador, Parametros, Valores);

                alertaautorizacion.IdAutorizacionPrecio = verificador;


                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
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

        public void CapAlertaAutorizacioInsertarGPMa(AlertaAutorizacion alertaautorizacion, string Conexion, ref int idAutorizacionPrecio)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                ///JFCV precioobjetivo 13 nov 
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();


                string[] Parametros = {
                    "@IdAutorizacionPrecio",
                    "@Id_Emp",
                    "@Id_Cd",
                    "@IdRepresentante",
                    "@Id_Cte",
                    "@Id_Ter",
                    "@TipoAutorizacion",
                    "@FechaSolicitud",
                    "@Req_Aut_Director",
                    "@Estatus",
                    "@IdUSolicita",
                    "@IdUsuarioGteAutorizo",
                    "@IdUsuarioDirAutorizo",
                    "@FechaAutorizacionGte",
                    "@FechaAutorizacionDir",
                    "@FechaRechazoGte",
                    "@FechaRechazoDir",
                    "@IdUsuarioGteRechazo",
                    "@IdUsuarioDirRechazo",
                    "@MotivoRechazo",
                    "@MotivoCancelacion",
                    "@Activo",
                    "@Justificacion",
                    "@JustificacionGte",
                    "@Id_Motivo",
                    "@JustificacionMemo",
                    "@JustificacionGeneral",
                    "@FechaVigencia",
                    "@Fecha_UltMod",
                    "@IdUsuarioMod",
                    "@Id_Tamaño",
                    "@Id_MotivoRechazo",
                    "@Id_Seg",
                    "@Id_ReporteGP"
                };

                object[] Valores = {
                    idAutorizacionPrecio, // OUTPUT
                    alertaautorizacion.Id_Emp,
                    alertaautorizacion.Id_Cd,
                    alertaautorizacion.IdRepresentante,
                    alertaautorizacion.Id_Cte,
                    alertaautorizacion.Id_Ter,
                    alertaautorizacion.TipoAutorizacion,
                    alertaautorizacion.FechaSolicitud ?? (object)DBNull.Value,
                    alertaautorizacion.Req_Aut_Director,
                    alertaautorizacion.Estatus,
                    alertaautorizacion.IdUSolicita,
                    alertaautorizacion.IdUsuarioGteAutorizo,
                    alertaautorizacion.IdUsuarioDirAutorizo,
                    alertaautorizacion.FechaAutorizacionGte ?? (object)DBNull.Value,
                    alertaautorizacion.FechaAutorizacionDir ?? (object)DBNull.Value,
                    alertaautorizacion.FechaRechazoGte ?? (object)DBNull.Value,
                    alertaautorizacion.FechaRechazoDir ?? (object)DBNull.Value,
                    alertaautorizacion.IdUsuarioGteRechazo,
                    alertaautorizacion.IdUsuarioDirRechazo,
                    alertaautorizacion.MotivoRechazo ?? (object)DBNull.Value,
                    alertaautorizacion.MotivoCancelacion ?? (object)DBNull.Value,
                    alertaautorizacion.Activo,
                    alertaautorizacion.Justificacion ?? (object)DBNull.Value,
                    alertaautorizacion.JustificacionGte ?? (object)DBNull.Value,
                    alertaautorizacion.Id_Motivo,
                    alertaautorizacion.JustificacionMemo ?? (object)DBNull.Value,
                    alertaautorizacion.JustificacionGeneral ?? (object)DBNull.Value,
                    alertaautorizacion.FechaVigencia ?? (object)DBNull.Value,
                    alertaautorizacion.Fecha_UltMod ?? (object)DBNull.Value,
                    alertaautorizacion.IdUSolicita ,
                    alertaautorizacion.Id_Tamaño ?? (object)DBNull.Value,
                    alertaautorizacion.Id_MotivoRechazo,
                    alertaautorizacion.Id_Seg,
                    alertaautorizacion.Id_ReporteGP
                };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAlertaPreciosInsertarGPMa", ref idAutorizacionPrecio, Parametros, Valores);


                alertaautorizacion.IdAutorizacionPrecio = idAutorizacionPrecio;


                if (idAutorizacionPrecio < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                if (CapaDatos != null)
                    CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                if (CapaDatos != null && sqlcmd != null)
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void CapAlertaAutorizacioInsertarGPMaDet(AlertaAutorizacion alertaautorizacion, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                ///JFCV precioobjetivo 13 nov 
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@IdAutorizacionPrecio",
                                          "@IdRepresentante",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@TipoAutorizacion",
                                          "@Id_Prd",
                                          "@Precio_Vta",
                                          "@Precio_MinimoRik",
                                          "@Precio_MinimoGte",
                                          "@Precio_VtaAutorizado",
                                          "@FechaSolicitud",
                                          "@Req_Aut_Director",
                                          "@Estatus",
                                          "@IdUSolicita",
                                          "@IdUsuarioGteAutorizo",
                                          "@IdUsuarioDirAutorizo",
                                          "@FechaAutorizacionGte",
                                          "@Fecha_UltMod",
                                          "@FechaAutorizacionDir",
                                          "@FechaRechazoGte",
                                          "@FechaRechazoDir",
                                          "@IdUsuarioGteRechazo",
                                          "@IdUsuarioDirRechazo",
                                          "@MotivoRechazo",
                                          "@MotivoCancelacion",
                                          "@Activo",
                                          "@Justificacion",
                                          "@JustificacionGte",
                                          "@PrecioLista",
                                          "@Id_Motivo",
                                          "@JustificacionMemo",
                                          "@FechaVigencia",
                                          "@PrecioObjetivo" ,
                                          "@Id_Tamaño"
                                      };
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.Id_Cd ,
                                       alertaautorizacion.IdAutorizacionPrecio,//agregue el id del encabezado
                                       alertaautorizacion.IdRepresentante,
                                       alertaautorizacion.Id_Cte,
                                       alertaautorizacion.Id_Ter ,
                                       alertaautorizacion.TipoAutorizacion ,
                                       alertaautorizacion.Id_Prd ,
                                       alertaautorizacion.Precio_Vta ,
                                       alertaautorizacion.Precio_MinimoRik ,
                                       alertaautorizacion.Precio_MinimoGte ,
                                       alertaautorizacion.Precio_VtaAutorizado ,
                                       alertaautorizacion.FechaSolicitud ,
                                       alertaautorizacion.Req_Aut_Director ,
                                       alertaautorizacion.Estatus ,
                                       alertaautorizacion.IdUSolicita ,
                                       alertaautorizacion.IdUsuarioGteAutorizo ,
                                       alertaautorizacion.IdUsuarioDirAutorizo ,
                                       alertaautorizacion.FechaAutorizacionGte ,
                                       alertaautorizacion.Fecha_UltMod ,
                                       alertaautorizacion.FechaAutorizacionDir ,
                                       alertaautorizacion.FechaRechazoGte ,
                                       alertaautorizacion.FechaRechazoDir ,
                                       alertaautorizacion.IdUsuarioGteRechazo ,
                                       alertaautorizacion.IdUsuarioDirRechazo ,
                                       alertaautorizacion.MotivoRechazo ,
                                       alertaautorizacion.MotivoCancelacion ,
                                       alertaautorizacion.Activo ,
                                       alertaautorizacion.Justificacion ,
                                       alertaautorizacion.JustificacionGte ,
                                       alertaautorizacion.PrecioLista,
                                       alertaautorizacion.Id_Motivo,
                                       alertaautorizacion.JustificacionMemo,
                                       alertaautorizacion.FechaVigencia,
                                       alertaautorizacion.PrecioObjetivo,
                                       alertaautorizacion.Id_Tamaño
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAlertaPreciosInsertarGPMaDet", ref verificador, Parametros, Valores);

                alertaautorizacion.IdAutorizacionPrecio = verificador;


                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                if (CapaDatos != null)
                    CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                if (CapaDatos != null && sqlcmd != null)
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void ConsultaAlertaCorreo(AlertaAutorizacion alertaautorizacion, ref string correousuario, ref string correodireccion, ref string nombrecliente, ref string prd_Descripcion, ref double precio_MinimoRik, ref double precioObjetivo, ref Int64 id_Prd, ref int req_aut, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@IdAutorizacionPrecio",
                                         "@Id_Cd",
                                         "@TipoAutorizacion"};
                object[] Valores = {
                                       alertaautorizacion.Id_Emp,
                                       alertaautorizacion.IdAutorizacionPrecio,
                                       alertaautorizacion.Id_Cd,
                                       alertaautorizacion.TipoAutorizacion

                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosConsulta_Correo", ref dr, Parametros, Valores);

                AlertaAutorizacion c;
                while (dr.Read())
                {

                    correousuario = dr["CorreoUsuario"].ToString().Trim();
                    correodireccion = dr["CorreoDireccion"].ToString().Trim();
                    nombrecliente = dr["Cte_NomComercial"].ToString().Trim();
                    prd_Descripcion = dr["Prd_Descripcion"].ToString().Trim();
                    precio_MinimoRik = dr["Precio_MinimoRik"].ToString() == "" ? 0 : Convert.ToDouble(dr["Precio_MinimoRik"]);
                    precioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);
                    id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    req_aut = Convert.ToInt32(dr["req_aut"]);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaCuentaNacional(int id_cte, Sesion sesion, ref string tipocuenta, ref string nombrecliente, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cte",
                                          "@Id_Cd"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),

                                       id_cte.ToString(),
                                       sesion.Id_Cd_Ver.ToString()
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapAlertaPreciosConsultaCN", ref dr, parametros, Valores);


                while (dr.Read())
                {

                    tipocuenta = dr.GetString(dr.GetOrdinal("ntipocuenta"));
                    nombrecliente = dr.GetString(dr.GetOrdinal("NombreCliente"));
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


    }
}