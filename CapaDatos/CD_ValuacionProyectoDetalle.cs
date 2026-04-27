using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

/*
 * 24 sep 2018 RFH
 */

namespace CapaDatos
{
    public class CD_ValuacionProyectoDetalle
    {
        public List<ValuacionProyectoDetalle> SelByOp(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            List<ValuacionProyectoDetalle> lst = new List<ValuacionProyectoDetalle>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Op" };

                object[] Valores = { Id_Emp, Id_Cd, Id_Op };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ValuacionProyectoDetalle", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ValuacionProyectoDetalle obj = new ValuacionProyectoDetalle();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    obj.Id_VapDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_VapDet")));
                    obj.Vap_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Tipo")));
                    obj.Vap_TipoStr = dr.GetValue(dr.GetOrdinal("Vap_TipoStr")).ToString();
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Producto = new Producto();
                    obj.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    obj.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    obj.Producto.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    obj.Vap_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    obj.Vap_Costo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Costo")));
                    obj.Vap_Precio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    obj.Vap_PrecioEspecial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_PrecioEspecial")));
                    //JFCV agregar territorio alerta precios preciador 
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    obj.Vap_PrecioObjetivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PrecioObjetivo")));

                    lst.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
                //throw ex;
            }
            return lst;
        }
        //
    }
}
