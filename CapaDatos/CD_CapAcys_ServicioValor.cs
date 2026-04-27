using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapAcys_ServicioValor
    {

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/
        // 30 Ago 2018 RFH
        //
        public List<eCapAcys2_ServicioValor> Consultar_ServicioValor(int Id_Emp, int Id_Cd, int Id_Acs,int Tipo, string Conexion)
        {
            List<eCapAcys2_ServicioValor> Lst = new List<eCapAcys2_ServicioValor>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Tipo" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Acs, Tipo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_ConsultarServicioValor", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    eCapAcys2_ServicioValor obj = new eCapAcys2_ServicioValor();

                    obj.IdCapAcysServicioValor= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCapAcysServicioValor")));
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));                                            
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));                        
                    obj.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Tipo")));                        

                    obj.Aplicar= Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Aplicar"))); 
                    obj.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Lunes"))); 
                    obj.Martes= Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Martes"))); 
                    obj.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Miercoles"))); 
                    obj.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Jueves"))); 
                    obj.Viernes= Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Viernes"))); 

                    obj.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("CualquierDia"))); 

                    obj.HorariosRecep1 =  dr.GetValue(dr.GetOrdinal("HorariosRecep1")).ToString();
                    obj.HorariosRecep2 =  dr.GetValue(dr.GetOrdinal("HorariosRecep2")).ToString();
                    obj.HorariosRecep3 =  dr.GetValue(dr.GetOrdinal("HorariosRecep3")).ToString();
                    obj.HorariosRecep4 =  dr.GetValue(dr.GetOrdinal("HorariosRecep4")).ToString();
                    
                    obj.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("CitaServ_MismoDia"))); 
                    obj.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("CitaServ_Previa"))); 

                    obj.ServRelleno = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ServRelleno"))); 
                    obj.ServPreventivo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ServPreventivo"))); 
                    obj.SelectorTipoServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SelectorTipoServ")));

                    Lst.Add(obj);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);                
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }
            return Lst;
        }

        //

    }
}
