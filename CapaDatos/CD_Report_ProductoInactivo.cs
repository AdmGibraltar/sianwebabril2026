using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Report_ProductoInactivo
    {
        public List<eReport_ProductoInactivo> sp_ProductoInactivo_Proyectos(eReport_ProductoInactivo Pms)
        {
            List<eReport_ProductoInactivo> lst = new List<eReport_ProductoInactivo>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Pms.Conexion);

                string[] Parametros = {
                    //"@PageNumber" , "@PageSize",
                    "@id_emp",
                    "@id_cd",
                    "@month",
                    "@year"
                };

                object[] Valores = {
                    //Pms.PageNumber, Pms.PageSize
                    Pms.Id_Emp,
                    Pms.Id_Cd,
                    Pms.Mes,
                    Pms.Anio
                };

                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCumplimientoVI_Dinamico_v2", ref dr, Parametros, Valores);                                                                 
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_ProductoInactivo_Proyectos", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eReport_ProductoInactivo obj = new eReport_ProductoInactivo();
                    //obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Op = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Op")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    //obj.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    //obj.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    obj.TipoVenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoVenta")));
                    obj.Cliente = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    obj.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    obj.EstatusStr = Convert.ToString(dr.GetValue(dr.GetOrdinal("EstatusStr")));
                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }

        public List<eReport_ProductoInactivo> sp_ProductoInactivo_Acys(eReport_ProductoInactivo Pms)
        {
            List<eReport_ProductoInactivo> lst = new List<eReport_ProductoInactivo>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Pms.Conexion);

                string[] Parametros = {
                    //"@PageNumber" , "@PageSize",
                    "@id_emp",
                    "@id_cd",
                    "@month",
                    "@year"
                };

                object[] Valores = {
                    //Pms.PageNumber, Pms.PageSize
                    Pms.Id_Emp,
                    Pms.Id_Cd,
                    Pms.Mes,
                    Pms.Anio
                };

                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCumplimientoVI_Dinamico_v2", ref dr, Parametros, Valores);                                                                 
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_ProductoInactivo_Acys", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eReport_ProductoInactivo obj = new eReport_ProductoInactivo();
                    //obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    //obj.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    //obj.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    obj.TipoCuentaStr = Convert.ToString(dr.GetValue(dr.GetOrdinal("TipoCuentaStr")));
                    obj.Cliente = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    obj.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    //obj.EstatusStr = Convert.ToString(dr.GetValue(dr.GetOrdinal("EstatusStr")));
                    obj.VigenciaStr = Convert.ToString(dr.GetValue(dr.GetOrdinal("VigenciaStr")));

                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }
    }
}