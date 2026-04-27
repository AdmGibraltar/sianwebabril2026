using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ProductoPermiso
    {
        public void ProductoPermiso_Consulta(ProductoPermiso Datos, ref List<ProductoPermiso> lista, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Datos.Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCat_ProductoPermiso_Consulta", ref dr, Parametros, Valores);
                ProductoPermiso Registro;
                while (dr.Read())
                {
                    Registro = new ProductoPermiso();
                    Registro.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Registro.Sucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cd_Nombre")));
                    Registro.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_prd"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_prd")));
                    Registro.Nombre_Producto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));
                    Registro.Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Activo"))) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));

                    lista.Add(Registro);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lista = null;
                throw ex;
            }
        }

        public void ProductoPermiso_Actualizar(ProductoPermiso Datos, ref int verificador, string conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd", "@Id_prd", "@Activo" };
                object[] Valores = { Datos.Id_Cd, Datos.Id_Prd, Datos.Activo };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCat_ProductoPermisos_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}