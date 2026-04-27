using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ClienteEcommerce
    {
        public void ConsultaLista(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                      "@Id_Emp",
                                      "@Id_Cd"

                                 };
                Object[] Valores = {
                                   Registro.Id_Emp,
                                   Registro.id_Cd
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_Consulta", ref dr, Parametros, Valores);

                Portakey Datos;

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

                    Datos.clave = Datos.Tipo.ToString() + Datos.id_Cd.ToString("D5") + Datos.Id_Portal.ToString("D6");


                    if (Datos.Tipo == 1)
                        Datos.NombreTipo = "Cuenta Local";
                    if (Datos.Tipo == 2)
                        Datos.NombreTipo = "Matriz Local";
                    if (Datos.Tipo == 3)
                        Datos.NombreTipo = "Matriz Central";
                    if (Datos.Tipo == 4)
                        Datos.NombreTipo = "MAtriz Cuenta Nacional";


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


        public void ConsultaCorreoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> List, string Conexion)
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
                                  ,Registro.Id_Cd
                                  ,Registro.Id_Cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_ConsultaCorreo", ref dr, Parametros, Valores);

                ClienteECommerce Datos;

                while (dr.Read())
                {
                    Datos = new ClienteECommerce();
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Datos.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.Id_Usu = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));
                    Datos.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));


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


        public void ConsultadatosoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> Lista, string Conexion)
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
                                  ,Registro.Id_Cd
                                  ,Registro.Id_Cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCliente_Consultaecommercer", ref dr, Parametros, Valores);
                ClienteECommerce Datos;

                while (dr.Read())
                {
                    Datos = new ClienteECommerce();


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
                throw new Exception("Error en la información del cliente. Favor de revisar.", ex);
            }
        }


        public void ConsultarSegmetnoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> Lista, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = {
                                       "@IdCte"
                                      ,"@Id_Cd"

                                 };
                Object[] Valores = {
                                   Registro.Id_Cte
                                 , Registro.Id_Cd

                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("CatWS_ConsultarUsuarioSegmento", ref dr, Parametros, Valores);

                ClienteECommerce Datos;

                while (dr.Read())
                {
                    Datos = new ClienteECommerce();
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Datos.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
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


        public void ConsultadatosoClienteCredito(ClienteECommerce Registro, ref List<ClienteECommerce> Lista, string Conexion)
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
                                  ,Registro.Id_Cd
                                  ,Registro.Id_Cte
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCliente_CreditoActivo", ref dr, Parametros, Valores);

                ClienteECommerce Datos;
                while (dr.Read())
                {
                    Datos = new ClienteECommerce();
                    Datos.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_emp")));
                    Datos.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_cd")));
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



        public void InsertarLista(ClienteECommerce Registro, string Conexion, ref string verificador)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                 "@Id_Emp"
                                ,"@Id_Cd"
                                ,"@Id_Usu"
                                ,"@Id_Cte"
                                ,"@Nombre"
                                ,"@Contrasena"
                                ,"@Estatus"
                                ,"@NombreUsu"
                                ,"@Apellido"
                                 };
                Object[] Valores = {
                                Registro.Id_Emp,
                                Registro.Id_Cd ,
                                Registro.Id_Usu ,
                                Registro.Id_Cte,
                                Registro.Nombre,
                                Registro.Contrasena,
                                "A",
                                Registro.NombreUsu,
                                Registro.Apellido
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_insertar", ref verificador, Parametros, Valores);
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void spCatClienteEcommerce_ConsultarCorreo(ClienteECommerce Registro, ref List<ClienteECommerce> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;


                String[] Parametros = {

                                 "@Nombre"

                                 };
                Object[] Valores = {

                                Registro.Nombre

                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_ConsultarCorreo", ref dr, Parametros, Valores);

                ClienteECommerce Datos;

                while (dr.Read())
                {
                    Datos = new ClienteECommerce();

                    Datos.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Datos.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Datos.Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("correo")));




                    List.Add(Datos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarLista(ClienteECommerce Registro, string Conexion, ref string verificador)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                  "@Id_Emp"
                                ,"@Id_Cd"
                                ,"@Id_Usu"
                                ,"@Id_Cte"
                                ,"@Nombre"
                                ,"@Contrasena"
                                ,"@Estatus"
                                ,"@NombreUsu"
                                ,"@Apellido"
                                 };
                Object[] Valores = {

                                Registro.Id_Emp,
                                Registro.Id_Cd ,
                                Registro.Id_Usu ,
                                Registro.Id_Cte,
                                Registro.Nombre,
                                Registro.Contrasena,
                                 "A",
                                 Registro.NombreUsu,
                                 Registro.Apellido
            };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_Modificar", ref verificador, Parametros, Valores);
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarEstado(ClienteECommerce Registro, string Conexion, ref int verificador)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                  "@Id_Emp"
                                ,"@Id_Cd"
                                ,"@Id_Usu"
                                ,"@Id_Cte"
                                 ,"@Estatus"
                                 };
                Object[] Valores = {

                                Registro.Id_Emp,
                                Registro.Id_Cd ,
                                Registro.Id_Usu ,
                                Registro.Id_Cte,
                                Registro.Estatus
            };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatClienteEcommerce_ActualizarEstado", ref verificador, Parametros, Valores);
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarJobMail(int IdCliente, int IdUsuario, String Contrasena, string email, string Conexion, ref int verificador)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                String[] Parametros = {
                                 "@Id_Cte"
                                ,"@Id_Usu"
                                ,"@Contrasena"
                                 ,"@Estatus"
                                 ,"@Email"
                                 };
                Object[] Valores = {
                                IdCliente,
                                IdUsuario ,
                                Contrasena ,
                                0,
                                email
                               };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spJobClienteEcom_insertar", ref verificador, Parametros, Valores);
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}