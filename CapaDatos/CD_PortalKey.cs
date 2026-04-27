using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_PortalKey
    {
        public void ConsultaSucursal(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@fechaInicial",
                                        "@fechaFinal" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.fechainicio, Portal.fecchafinal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spEccommerce_ReporteSucursalCentral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("cdi")) ? 0 : dr.GetInt32(dr.GetOrdinal("cdi"));
                    registro.nombre = dr.IsDBNull(dr.GetOrdinal("sucursal")) ? "" : dr.GetString(dr.GetOrdinal("sucursal"));
                    registro.ClienteAlta = dr.IsDBNull(dr.GetOrdinal("clientealta")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientealta"));
                    registro.ClientePtes = dr.IsDBNull(dr.GetOrdinal("clientePendientes")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientePendientes"));
                    registro.ClienteTotal = dr.IsDBNull(dr.GetOrdinal("clientetotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientetotal"));
                    registro.PorcCliente = Convert.ToDouble(registro.ClienteAlta) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.ClienteAlta) / Convert.ToDouble(registro.ClienteTotal));

                    registro.CantidadPedidoPortal = dr.IsDBNull(dr.GetOrdinal("cantidadPortal")) ? 0 : dr.GetInt32(dr.GetOrdinal("cantidadPortal"));
                    registro.CantidadPedidoTotal = dr.IsDBNull(dr.GetOrdinal("Cantidadtotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("Cantidadtotal"));
                    registro.PorcPortal = registro.CantidadPedidoPortal == 0 ? 0 : Convert.ToDouble(registro.CantidadPedidoPortal) / Convert.ToDouble(registro.CantidadPedidoTotal);
                    registro.facturacionPortal = dr.IsDBNull(dr.GetOrdinal("TotalPortal")) ? 0 : dr.GetDouble(dr.GetOrdinal("TotalPortal"));
                    registro.facturaciontotal = dr.IsDBNull(dr.GetOrdinal("totalPedidos")) ? 0 : dr.GetDouble(dr.GetOrdinal("totalPedidos"));
                    registro.porcfacturacionPortal = Convert.ToDouble(registro.facturacionPortal) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.facturacionPortal) / Convert.ToDouble(registro.facturaciontotal));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultaRepresentante(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@fechaInicial",
                                        "@fechaFinal" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.fechainicio, Portal.fecchafinal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spEccommerce_ReporteRepresentanteCentral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    registro.nombre = dr.IsDBNull(dr.GetOrdinal("sucursal")) ? "" : dr.GetString(dr.GetOrdinal("sucursal"));
                    registro.id_rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    registro.nombreRik = dr.IsDBNull(dr.GetOrdinal("rik_nombre")) ? "" : dr.GetString(dr.GetOrdinal("rik_nombre"));
                    registro.ClienteAlta = dr.IsDBNull(dr.GetOrdinal("clientealta")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientealta"));
                    registro.ClientePtes = dr.IsDBNull(dr.GetOrdinal("clientePendientes")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientePendientes"));
                    registro.ClienteTotal = dr.IsDBNull(dr.GetOrdinal("clientetotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientetotal"));
                    registro.PorcCliente = Convert.ToDouble(registro.ClienteAlta) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.ClienteAlta) / Convert.ToDouble(registro.ClienteTotal));

                    registro.CantidadPedidoPortal = dr.IsDBNull(dr.GetOrdinal("cantidadPortal")) ? 0 : dr.GetInt32(dr.GetOrdinal("cantidadPortal"));
                    registro.CantidadPedidoTotal = dr.IsDBNull(dr.GetOrdinal("Cantidadtotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("Cantidadtotal"));
                    registro.PorcPortal = registro.CantidadPedidoPortal == 0 ? 0 : Convert.ToDouble(registro.CantidadPedidoPortal) / Convert.ToDouble(registro.CantidadPedidoTotal);
                    registro.facturacionPortal = dr.IsDBNull(dr.GetOrdinal("TotalPortal")) ? 0 : dr.GetDouble(dr.GetOrdinal("TotalPortal"));
                    registro.facturaciontotal = dr.IsDBNull(dr.GetOrdinal("totalPedidos")) ? 0 : dr.GetDouble(dr.GetOrdinal("totalPedidos"));
                    registro.porcfacturacionPortal = Convert.ToDouble(registro.facturacionPortal) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.facturacionPortal) / Convert.ToDouble(registro.facturaciontotal));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void ConsultaCliente(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@fechaInicial",
                                        "@fechaFinal" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.fechainicio, Portal.fecchafinal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spEccommerce_ReporteClienteCentral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    registro.nombre = dr.IsDBNull(dr.GetOrdinal("sucursal")) ? "" : dr.GetString(dr.GetOrdinal("sucursal"));
                    registro.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cte"));
                    registro.nombreCliente = dr.IsDBNull(dr.GetOrdinal("cte_nomcomercial")) ? "" : dr.GetString(dr.GetOrdinal("cte_nomcomercial"));
                    registro.id_rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    registro.nombreRik = dr.IsDBNull(dr.GetOrdinal("rik_nombre")) ? "" : dr.GetString(dr.GetOrdinal("rik_nombre"));
                    registro.ClienteAlta = dr.IsDBNull(dr.GetOrdinal("clientealta")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientealta"));
                    registro.ClientePtes = dr.IsDBNull(dr.GetOrdinal("clientePendientes")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientePendientes"));
                    registro.ClienteTotal = dr.IsDBNull(dr.GetOrdinal("clientetotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientetotal"));
                    registro.CantidadPedidoPortal = dr.IsDBNull(dr.GetOrdinal("cantidadPortal")) ? 0 : dr.GetInt32(dr.GetOrdinal("cantidadPortal"));
                    registro.CantidadPedidoTotal = dr.IsDBNull(dr.GetOrdinal("Cantidadtotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("Cantidadtotal"));
                    registro.PorcPortal = registro.CantidadPedidoPortal == 0 ? 0 : Convert.ToDouble(registro.CantidadPedidoPortal) / Convert.ToDouble(registro.CantidadPedidoTotal);
                    registro.facturacionPortal = dr.IsDBNull(dr.GetOrdinal("TotalPortal")) ? 0 : dr.GetDouble(dr.GetOrdinal("TotalPortal"));
                    registro.facturaciontotal = dr.IsDBNull(dr.GetOrdinal("totalPedidos")) ? 0 : dr.GetDouble(dr.GetOrdinal("totalPedidos"));
                    registro.porcfacturacionPortal = Convert.ToDouble(registro.facturacionPortal) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.facturacionPortal) / Convert.ToDouble(registro.facturaciontotal));
                    registro.ClientedeAlta = registro.ClienteAlta == 1 ? true : false;
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaPedido(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@fechaInicial",
                                        "@fechaFinal" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.fechainicio, Portal.fecchafinal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SPEccommerce_reportePedidoCentral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    registro.nombre = dr.IsDBNull(dr.GetOrdinal("sucursal")) ? "" : dr.GetString(dr.GetOrdinal("sucursal"));
                    registro.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cte"));
                    registro.nombreCliente = dr.IsDBNull(dr.GetOrdinal("cte_nomcomercial")) ? "" : dr.GetString(dr.GetOrdinal("cte_nomcomercial"));
                    registro.id_rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    registro.nombreRik = dr.IsDBNull(dr.GetOrdinal("rik_nombre")) ? "" : dr.GetString(dr.GetOrdinal("rik_nombre"));
                    registro.id_pedido = dr.IsDBNull(dr.GetOrdinal("pedido")) ? 0 : dr.GetInt32(dr.GetOrdinal("pedido"));
                    registro.ClienteAlta = dr.IsDBNull(dr.GetOrdinal("clientealta")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientealta"));
                    registro.ClientePtes = dr.IsDBNull(dr.GetOrdinal("clientePendientes")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientePendientes"));
                    registro.ClienteTotal = dr.IsDBNull(dr.GetOrdinal("clientetotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("clientetotal"));
                    registro.CantidadPedidoPortal = dr.IsDBNull(dr.GetOrdinal("cantidadPortal")) ? 0 : dr.GetInt32(dr.GetOrdinal("cantidadPortal"));
                    registro.CantidadPedidoTotal = dr.IsDBNull(dr.GetOrdinal("Cantidadtotal")) ? 0 : dr.GetInt32(dr.GetOrdinal("Cantidadtotal"));
                    registro.PorcPortal = registro.CantidadPedidoPortal == 0 ? 0 : Convert.ToDouble(registro.CantidadPedidoPortal) / Convert.ToDouble(registro.CantidadPedidoTotal);
                    registro.facturacionPortal = dr.IsDBNull(dr.GetOrdinal("TotalPortal")) ? 0 : dr.GetDouble(dr.GetOrdinal("TotalPortal"));
                    registro.facturaciontotal = dr.IsDBNull(dr.GetOrdinal("totalPedidos")) ? 0 : dr.GetDouble(dr.GetOrdinal("totalPedidos"));
                    registro.porcfacturacionPortal = Convert.ToDouble(registro.facturacionPortal) == 0 ? 0 : Convert.ToDouble(Convert.ToDouble(registro.facturacionPortal) / Convert.ToDouble(registro.facturaciontotal));
                    registro.ClientedeAlta = registro.ClienteAlta == 1 ? true : false;
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarMAtriz(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id",
                                        "@Tipo",
                                        "@Id_Cd"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaDatosPortalKey", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.Id_Portal = dr.IsDBNull(dr.GetOrdinal("ID_Portal")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_Portal"));
                    registro.NombreMatriz = dr.IsDBNull(dr.GetOrdinal("Nombre_Portal")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Portal"));
                    registro.Correo = dr.IsDBNull(dr.GetOrdinal("Correo")) ? "" : dr.GetString(dr.GetOrdinal("Correo"));
                    registro.nombre = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Nombre"));
                    registro.Apellidos = dr.IsDBNull(dr.GetOrdinal("Apellidos")) ? "" : dr.GetString(dr.GetOrdinal("Apellidos"));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarGralMAtriz(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey Datos;
                string[] Parametros = { "@Id_Emp",
                                        "@Id"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaPortalKey", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Datos = new Portakey();
                    Datos.Id_Portal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_portal")));
                    Datos.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tipo")));
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Datos.id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.Id_Usu = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));

                    Datos.NombreMatriz = Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre_Portal")));
                    Datos.Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));
                    Datos.Contrasena = Convert.ToString(dr.GetValue(dr.GetOrdinal("Contrasena")));
                    Datos.Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Db_cdNombre")));

                    Datos.clave = Datos.Tipo.ToString() + Datos.id_Cd.ToString("D5") + Datos.Id_Portal.ToString("D6");


                    if (Datos.Tipo == 1)
                        Datos.NombreTipo = "Cuenta Local";
                    if (Datos.Tipo == 2)
                        Datos.NombreTipo = "Matriz Local";
                    if (Datos.Tipo == 3)
                        Datos.NombreTipo = "Matriz Central";
                    if (Datos.Tipo == 4)
                        Datos.NombreTipo = "Matriz Cuenta Nacional";

                    lista.Add(Datos);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@id_emp", "@id_Cd", "@Id_U", "@Nombre", "@Correo", "@Tipo", "@id_cte", "@NombreUsu", "@Apellido" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.Id_Usu, Portal.NombreMatriz, Portal.Correo, Portal.Tipo, Portal.id_cte, Portal.nombre, Portal.Apellidos };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaMAtrizPortalKey", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Portal", "@Tipo", "@id_Cd", "@id_cte", "@NombrePortal", "@Correo", "@Nombre", "@Apellidos" };
                object[] Valores = { Portal.Id_Portal, Portal.Tipo, Portal.id_Cd, Portal.id_cte, Portal.NombreMatriz, Portal.Correo, Portal.nombre, Portal.Apellidos };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ModificarMAtrizPortalKey", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tipo", "@Id_Portal" };
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd, Portal.Tipo, Portal.Id_Portal };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_EliminarMatrizPortalKey", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarRegion(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id",
                                        "@Tipo",
                                        "@Id_Cd"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaRegion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.Id_Portal = dr.IsDBNull(dr.GetOrdinal("ID_Portal")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_Portal"));
                    registro.Id_Region = dr.IsDBNull(dr.GetOrdinal("Id_region")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_region"));
                    registro.NombreRegion = dr.IsDBNull(dr.GetOrdinal("Nombre_Region")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Region"));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Portal", "@Nombre" };
                object[] Valores = { Portal.Id_Portal, Portal.NombreRegion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaRegion", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Portal", "@Id_Region", "@Nombre" };
                object[] Valores = { Portal.Id_Portal, Portal.Id_Region, Portal.NombreRegion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ModificarRegion", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@Id_Region" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Id_Region };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_EliminarRegion", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarCorreoUsuario(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Portal",
                                        "@Tipo",
                                        "@Id_Cd"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaCorreoUsuario", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.Id_Portal = dr.IsDBNull(dr.GetOrdinal("ID_Portal")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_Portal"));
                    registro.id_CorreoUsuario = dr.IsDBNull(dr.GetOrdinal("ID_UsuarioPortal")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_UsuarioPortal"));
                    registro.NombreCorreoUsuario = dr.IsDBNull(dr.GetOrdinal("Nombre_Usuario")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Usuario"));
                    registro.CorreoUsuario = dr.IsDBNull(dr.GetOrdinal("Correo")) ? "" : dr.GetString(dr.GetOrdinal("Correo"));
                    registro.ApellidosCorreoUsuario = dr.IsDBNull(dr.GetOrdinal("apellidos")) ? "" : dr.GetString(dr.GetOrdinal("apellidos"));
                    registro.NombreCompleto = registro.NombreCorreoUsuario + " " + registro.ApellidosCorreoUsuario;
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Portal", "@Tipo", "@id_Cd", "@id_cte", "@Nombre_Usuario", "@CorreoUsuario", "@Apellidos" };
                object[] Valores = { Portal.Id_Portal, Portal.Tipo, Portal.id_Cd, Portal.id_cte, Portal.NombreCorreoUsuario, Portal.CorreoUsuario, Portal.ApellidosCorreoUsuario };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaCorreoUsuario", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Portal", "@Tipo", "@id_Cd", "@id_cte", "@ID_UsuarioPortal", "@NombreCorreoUsuario", "@CorreoUsuario", "@Apellidos" };
                object[] Valores = { Portal.Id_Portal, Portal.Tipo, Portal.id_Cd, Portal.id_cte, Portal.id_CorreoUsuario, Portal.NombreCorreoUsuario, Portal.CorreoUsuario, Portal.ApellidosCorreoUsuario };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ModificarCorreoUsuario", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@ID_UsuarioPortal" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_CorreoUsuario };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_EliminarCorreoUsuario", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarClientePortal(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Portal",
                                        "@Id_Tipo",
                                        "@Id_Cd"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaCliente", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("ID_CDi")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_CDi"));
                    registro.Id_Region = dr.IsDBNull(dr.GetOrdinal("ID_region")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_region"));
                    registro.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    registro.nombreCliente = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Nombre"));
                    registro.NombreRegion = dr.IsDBNull(dr.GetOrdinal("Nombre_Region")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Region"));
                    registro.Sucursal = dr.IsDBNull(dr.GetOrdinal("Db_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Db_Nombre"));
                    registro.unidad = dr.IsDBNull(dr.GetOrdinal("Unidad")) ? "" : dr.GetString(dr.GetOrdinal("Unidad"));
                    registro.Id_Direccion = dr.IsDBNull(dr.GetOrdinal("Id_CteDirEntrega")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_CteDirEntrega"));
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@Id_cte", "@Id_Cd", "@Id_Region", "@Nombre", "@Unidad", "@Id_Direccion" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_cte, Portal.id_Cd, Portal.Id_Region, Portal.nombreCliente, Portal.unidad, Portal.Id_Direccion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaCliente", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@Id_cte", "@Id_Cd", "@Id_Region", "@Nombre", "@Unidad", "@Id_Direccion" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_cte, Portal.id_Cd, Portal.Id_Region, Portal.nombreCliente, Portal.unidad, Portal.Id_Direccion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ModificarCliente", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@Id_cte", "@Id_Cd", "@Id_Direccion" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_cte, Portal.id_Cd, Portal.Id_Direccion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_EliminarCliente", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPortalPermiso(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Portal",
                                        "@id_Usuario",
                                        "@Tipo",
                                        "@ID_CD"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_CorreoUsuario, Portal.Tipo, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaClientePermiso", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_Cd = dr.IsDBNull(dr.GetOrdinal("ID_CDi")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_CDi"));
                    registro.Id_Region = dr.IsDBNull(dr.GetOrdinal("ID_region")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID_region"));
                    registro.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    registro.nombreCliente = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Nombre"));
                    registro.NombreRegion = dr.IsDBNull(dr.GetOrdinal("Nombre_Region")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Region"));
                    registro.Sucursal = dr.IsDBNull(dr.GetOrdinal("Db_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Db_Nombre"));
                    registro.intpermiso = dr.IsDBNull(dr.GetOrdinal("permisos")) ? 0 : dr.GetInt32(dr.GetOrdinal("permisos"));
                    if (registro.intpermiso == 1)
                    {
                        registro.permiso = true;
                    }
                    else
                    {
                        registro.permiso = false;
                    }
                    registro.unidad = dr.IsDBNull(dr.GetOrdinal("Unidad")) ? "" : dr.GetString(dr.GetOrdinal("Unidad"));
                    registro.Id_Direccion = dr.IsDBNull(dr.GetOrdinal("Id_CteDirEntrega")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_CteDirEntrega"));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarPortalPresupuesto(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Portal",
                                        "@ID_Tipo",
                                        "@Anio",
                                        "@Id_Cd"};
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.Año, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ConsultaPresupuesto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();



                    registro.Id_Region = dr.IsDBNull(dr.GetOrdinal("id_Region")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_Region"));
                    registro.unidad = dr.IsDBNull(dr.GetOrdinal("Unidad")) ? "" : dr.GetString(dr.GetOrdinal("Unidad"));
                    registro.NombreRegion = dr.IsDBNull(dr.GetOrdinal("Nombre_Region")) ? "" : dr.GetString(dr.GetOrdinal("Nombre_Region"));
                    registro.Ene = dr.IsDBNull(dr.GetOrdinal("Ene")) ? 0 : dr.GetDouble(dr.GetOrdinal("Ene"));
                    registro.Feb = dr.IsDBNull(dr.GetOrdinal("Feb")) ? 0 : dr.GetDouble(dr.GetOrdinal("Feb"));
                    registro.Mar = dr.IsDBNull(dr.GetOrdinal("Mar")) ? 0 : dr.GetDouble(dr.GetOrdinal("Mar"));
                    registro.Abr = dr.IsDBNull(dr.GetOrdinal("Abr")) ? 0 : dr.GetDouble(dr.GetOrdinal("Abr"));
                    registro.May = dr.IsDBNull(dr.GetOrdinal("May")) ? 0 : dr.GetDouble(dr.GetOrdinal("May"));
                    registro.Jun = dr.IsDBNull(dr.GetOrdinal("Jun")) ? 0 : dr.GetDouble(dr.GetOrdinal("Jun"));
                    registro.Jul = dr.IsDBNull(dr.GetOrdinal("Jul")) ? 0 : dr.GetDouble(dr.GetOrdinal("Jul"));
                    registro.Ago = dr.IsDBNull(dr.GetOrdinal("Ago")) ? 0 : dr.GetDouble(dr.GetOrdinal("Ago"));
                    registro.Sep = dr.IsDBNull(dr.GetOrdinal("Sep")) ? 0 : dr.GetDouble(dr.GetOrdinal("Sep"));
                    registro.Oct = dr.IsDBNull(dr.GetOrdinal("Oct")) ? 0 : dr.GetDouble(dr.GetOrdinal("Oct"));
                    registro.Nov = dr.IsDBNull(dr.GetOrdinal("Nov")) ? 0 : dr.GetDouble(dr.GetOrdinal("Nov"));
                    registro.Dic = dr.IsDBNull(dr.GetOrdinal("Dic")) ? 0 : dr.GetDouble(dr.GetOrdinal("Dic"));

                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarPresupuesto(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Portal",
                                        "@ID_Tipo",
                                        "@ID_Cd",
                                        "@Id_Region",
                                        "@unidad",
                                        "@Anio",
                                        "@Mes",
                                        "@Presupuesto"  };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.Tipo, Portal.id_Cd, Portal.Id_Region, Portal.unidad, Portal.Año, Portal.NombreMes, Portal.Presupuesto };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaPresupuesto", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarPermiso(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@Id_cte", "@Id_Cd", "@Id_Region", "@Id_usu", "@Unidad", "@Id_Direccion" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_cte, Portal.id_Cd, Portal.Id_Region, Portal.id_CorreoUsuario, Portal.unidad, Portal.Id_Direccion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_AltaClientePermiso", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPermisos(Portakey Portal, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Portal", "@id_Usuario" };
                object[] Valores = { Portal.Id_Emp, Portal.Id_Portal, Portal.id_CorreoUsuario };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_EliminarClientePermiso", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCorreo(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = { "@Id_Portal", "@Tipo", "@id_Cd", "@id_cte", "@Nombre" };
                Object[] Valores = {Registro.Id_Portal == 0? (object)null : Registro.Id_Portal,
                    Registro.Tipo,
                    Registro.id_Cd,
                    Registro.id_cte,
                    Registro.Correo };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_ConsultarCorreo", ref dr, Parametros, Valores);

                Portakey Datos;

                while (dr.Read())
                {
                    Datos = new Portakey();

                    Datos.id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));

                    List.Add(Datos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCorreoUnidad(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = { "@Id_Portal", "@Tipo", "@id_Cd", "@id_cte", "@Nombre" };
                Object[] Valores = {Registro.id_CorreoUsuario == 0? (object)null : Registro.id_CorreoUsuario,
                    Registro.Tipo,
                    Registro.id_Cd,
                    Registro.id_cte,
                    Registro.Correo };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_ConsultarCorreo", ref dr, Parametros, Valores);

                Portakey Datos;

                while (dr.Read())
                {
                    Datos = new Portakey();

                    Datos.id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));

                    List.Add(Datos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaDatosCliente(Portakey Datos, ref List<Portakey> Lista, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                      "@Id_Emp"
                                     ,"@Id_Cd"
                                     ,"@Id_Cte"
                                 };
                Object[] Valores = {
                                  Datos.Id_Emp
                                  ,Datos.id_Cd
                                  ,Datos.id_cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_pk_ConsultarDatosCliente", ref dr, Parametros, Valores);
                Portakey Registros;

                while (dr.Read())
                {
                    Registros = new Portakey();


                    Datos.Cte_FacRfc = dr.IsDBNull(dr.GetOrdinal("Cte_FacRfc")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_FacRfc")));
                    Datos.calle = dr.IsDBNull(dr.GetOrdinal("Cte_Calle")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_Calle")));
                    Datos.Cte_Telefono = dr.IsDBNull(dr.GetOrdinal("Cte_Telefono")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_Telefono")));
                    Datos.Cte_Numero = dr.IsDBNull(dr.GetOrdinal("Cte_Numero")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_Numero")));
                    Datos.Cte_Municipio = dr.IsDBNull(dr.GetOrdinal("Cte_Municipio")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_Municipio")));
                    Datos.Cte_Estado = dr.IsDBNull(dr.GetOrdinal("Cte_Estado")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_Estado")));
                    Datos.Cte_CP = dr.IsDBNull(dr.GetOrdinal("Cte_CP")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_CP")));
                    Datos.DirEntregacte_calle = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_calle")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_calle")));
                    Datos.DirEntregacte_numero = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_numero")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_numero")));
                    Datos.DirEntregacte_Cp = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_Cp")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DirEntregacte_Cp")));
                    Datos.DirEntregaCte_colonia = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_colonia")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_colonia")));
                    Datos.DirEntregaCte_municipio = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_municipio")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_municipio")));
                    Datos.DirEntregaCte_Estado = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_Estado")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_Estado")));
                    Datos.DirEntregacte_telefono = dr.IsDBNull(dr.GetOrdinal("DirEntregacte_telefono")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("DirEntregacte_telefono")));
                    Datos.nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    Datos.nombre2 = dr.IsDBNull(dr.GetOrdinal("nombre2")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre2")));
                    Datos.nombre3 = dr.IsDBNull(dr.GetOrdinal("nombre3")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre3")));
                    Datos.rik1 = dr.IsDBNull(dr.GetOrdinal("rik1")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("rik1")));
                    Datos.rik2 = dr.IsDBNull(dr.GetOrdinal("rik2")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("rik2")));
                    Datos.rik3 = dr.IsDBNull(dr.GetOrdinal("rik3")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("rik3")));

                    Lista.Add(Datos);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw new Exception("Error, Favor de reviar los datos del cliente.", ex);
            }
        }

        public void ConsultarSegmentoCliente(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = {
                                      "@Id_Emp"
                                     ,"@Id_Cd"
                                     ,"@Id_Cte"
                                 };
                Object[] Valores = {
                                  Registro.Id_Emp
                                  ,Registro.id_Cd
                                  ,Registro.id_cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_pk_ValidaSegmentoUsuario", ref dr, Parametros, Valores);

                Portakey Datos;

                while (dr.Read())
                {
                    Datos = new Portakey();
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Datos.id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_uen")));
                    Datos.id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_Seg")));

                    Lista.Add(Datos);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCredito(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = {
                                      "@Id_Emp"
                                     ,"@Id_Cd"
                                     ,"@Id_Cte"
                                 };
                Object[] Valores = {
                                  Registro.Id_Emp
                                  ,Registro.id_Cd
                                  ,Registro.id_cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_pk_ValidaCreditoActivo", ref dr, Parametros, Valores);

                Portakey Datos;
                while (dr.Read())
                {
                    Datos = new Portakey();
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_emp")));
                    Datos.id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cd")));
                    Datos.Credito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cte_credito")));
                    Datos.limite = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_LimCobr")));


                    Lista.Add(Datos);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteEnvio(Portakey Datos, ref List<Portakey> Lista, string Conexion)
        {
            try
            {


                CapaDatos.CD_Datos CapaDatos3 = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr3 = null;
                string[] Parametros3 = { "@IdCte", "@id_Cd" };
                object[] Valores3 = { Datos.id_cte, Datos.id_Cd };
                SqlCommand sqlcmd3 = CapaDatos3.GenerarSqlCommand("SP_clienteDireccionEnvio", ref dr3, Parametros3, Valores3);

                Portakey envio;

                while (dr3.Read())
                {
                    envio = new Portakey();
                    envio.Id_Direccion = dr3.IsDBNull(dr3.GetOrdinal("id_cteDirEntrega")) ? 0 : Convert.ToInt32(dr3.GetValue(dr3.GetOrdinal("id_cteDirEntrega")));
                    envio.calle = dr3.IsDBNull(dr3.GetOrdinal("cte_calle")) ? "" : Convert.ToString(dr3.GetValue(dr3.GetOrdinal("cte_calle")));
                    envio.numero = dr3.IsDBNull(dr3.GetOrdinal("cte_numero")) ? "" : Convert.ToString(dr3.GetValue(dr3.GetOrdinal("cte_numero")));

                    envio.DireccionCompleta = envio.calle.ToString() + " " + envio.numero.ToString();

                    Lista.Add(envio);
                }
                CapaDatos3.LimpiarSqlcommand(ref sqlcmd3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaClienteEnviodet(Portakey Datos, ref List<Portakey> Lista, string Conexion)
        {
            try
            {


                CapaDatos.CD_Datos CapaDatos3 = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr3 = null;
                string[] Parametros3 = { "@IdCte", "@id_Cd", "@Id_Direccion" };
                object[] Valores3 = { Datos.id_cte, Datos.id_Cd, Datos.Id_Direccion };
                SqlCommand sqlcmd3 = CapaDatos3.GenerarSqlCommand("SP_clienteDireccionEnviodet", ref dr3, Parametros3, Valores3);

                Portakey envio;

                while (dr3.Read())
                {
                    envio = new Portakey();
                    envio.Id_Direccion = dr3.IsDBNull(dr3.GetOrdinal("id_cteDirEntrega")) ? 0 : Convert.ToInt32(dr3.GetValue(dr3.GetOrdinal("id_cteDirEntrega")));
                    envio.calle = dr3.IsDBNull(dr3.GetOrdinal("cte_calle")) ? "" : Convert.ToString(dr3.GetValue(dr3.GetOrdinal("cte_calle")));
                    envio.numero = dr3.IsDBNull(dr3.GetOrdinal("cte_numero")) ? "" : Convert.ToString(dr3.GetValue(dr3.GetOrdinal("cte_numero")));

                    envio.DireccionCompleta = envio.calle.ToString() + " " + envio.numero.ToString();

                    Lista.Add(envio);
                }
                CapaDatos3.LimpiarSqlcommand(ref sqlcmd3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDatosPortal(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                      "@Id_Emp"
                                     ,"@Id_Cd"
                                     ,"@Id_Portal"
                                 };
                Object[] Valores = {
                                   Registro.Id_Emp
                                  ,Registro.id_Cd
                                  ,Registro.Id_Portal
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("PK_ConsultaCliente", ref dr, Parametros, Valores);

                Portakey Datos;

                while (dr.Read())
                {
                    Datos = new Portakey();
                    Datos.Id_Portal = dr.IsDBNull(dr.GetOrdinal("id_portal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_portal")));
                    Datos.Tipo = dr.IsDBNull(dr.GetOrdinal("tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tipo")));
                    Datos.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Datos.id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.Id_Usu = dr.IsDBNull(dr.GetOrdinal("Id_Usu")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));
                    Datos.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.NombreMatriz = dr.IsDBNull(dr.GetOrdinal("nombre_Portal")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre_Portal")));
                    Datos.Correo = dr.IsDBNull(dr.GetOrdinal("correo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));
                    Datos.Contrasena = dr.IsDBNull(dr.GetOrdinal("Contrasena")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Contrasena")));
                    Datos.nombre = dr.IsDBNull(dr.GetOrdinal("NombreUsu")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreUsu")));
                    Datos.Apellidos = dr.IsDBNull(dr.GetOrdinal("Apellidos")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Apellidos")));

                    if (Datos.Tipo == 1)
                        Datos.NombreTipo = "Cliente";
                    if (Datos.Tipo == 2)
                        Datos.NombreTipo = "Matriz Local";
                    if (Datos.Tipo == 3)
                        Datos.NombreTipo = "Matriz Central";
                    if (Datos.Tipo == 4)
                        Datos.NombreTipo = "Matriz Cuenta Nacional";


                    List.Add(Datos);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDatosCliente(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Portakey registro;
                string[] Parametros = { "@Id1",
                                        "@Id2"};
                object[] Valores = { Portal.Id_Emp, Portal.id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCliente_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Portakey();
                    registro.id_cte = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    registro.nombreCliente = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    registro.descricpion = (registro.id_cte == -1 ? "" : registro.id_cte.ToString()) + " - " + registro.nombreCliente;
                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaClienteCapturados(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                      "@Id_Cd"
                                     ,"@ID_Cte"
                                     ,"@Id_Direccion"
                                     ,"@id_Portal"
                                     ,"@Id_Tipo"
                                 };
                Object[] Valores = {
                                   Registro.id_Cd
                                  ,Registro.id_cte
                                  ,Registro.Id_Direccion
                                  ,Registro.Id_Portal
                                  ,Registro.Tipo
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_ValidaClienteExiste", ref dr, Parametros, Valores);

                Portakey Datos;

                while (dr.Read())
                {
                    Datos = new Portakey();
                    Datos.Id_Portal = dr.IsDBNull(dr.GetOrdinal("ID_Portal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID_Portal")));
                    Datos.Tipo = dr.IsDBNull(dr.GetOrdinal("tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tipo")));
                    Datos.id_Cd = dr.IsDBNull(dr.GetOrdinal("ID_CD")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID_CD")));
                    Datos.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Id_Direccion = dr.IsDBNull(dr.GetOrdinal("Id_CteDirEntrega")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CteDirEntrega")));

                    List.Add(Datos);
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteUnidad(Portakey Registro, ref int verificador, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);

                String[] Parametros = {
                                      "@Id_Emp"
                                     ,"@Id_Portal"
                                     ,"@Unidad"
                                      ,"@id_Cte"
                                      ,"@Id_direccion"
                };
                Object[] Valores = {
                                   Registro.Id_Emp
                                  ,Registro.Id_Portal
                                  ,Registro.unidad
                                  ,Registro.id_cte
                                  ,Registro.Id_Direccion
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SP_PK_CosultarUnidadCliente", ref verificador, Parametros, Valores);
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}