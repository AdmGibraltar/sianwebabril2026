using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_UsuarioRik
    {
        public List<eUsuarioRik> Lista(int Id_Emp, int Id_Cd, string Conexion)
        {
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMUsuarioRik_Lista", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    eUsuarioRik Obj = new eUsuarioRik();                   
                    Obj.Id_Emp= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    Obj.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    Obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    Obj.EsGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsGerente")));
                    lst.Add(Obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null; 
            }

            return lst;
        }

        // NOV22-2019 RFH

        public List<eUsuarioRik> Lista_Combo(int Id_Emp, int Id_Cd, int Id_Tu, int Id_Uen, int TipoRik, string Conexion)
        {
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4", "@Id5", "@IdTipoRep" };

                object[] Valores = { 1, Id_Emp, Id_Cd, null, Id_Uen, TipoRik };

                if (Id_Tu == -1)
                {
                    Valores[0] =1;
                    Valores[1] =Id_Emp;
                    Valores[2] =Id_Cd;
                    Valores[3] =null;
                    Valores[4] =Id_Uen;
                    Valores[5] =TipoRik;
                }
                else
                {
                    Valores[0] =1;
                    Valores[1] =Id_Emp;
                    Valores[2] =Id_Cd;
                    Valores[3] =Id_Tu;
                    Valores[4] =Id_Uen;
                    Valores[5] =TipoRik;
                }                

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eUsuarioRik Obj = new eUsuarioRik();
                    Obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    Obj.U_Nombre = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();                    
                    lst.Add(Obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }

            return lst;
        }


        //
    }
}
