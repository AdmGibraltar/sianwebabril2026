using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Documentos
    {
        public void ConsultaDocumento(Sesion sesion, ref Documento Doc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Doc" };
                object[] Valores = { Doc.Id_Emp, Doc.Id_Cd, Doc.Id_Doc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaDocumento", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Doc = new Documento();
                    Doc.Doc_Nombre = dr.GetString(dr.GetOrdinal("Nombre"));
                    Doc.Formato = dr.GetString(dr.GetOrdinal("Formato"));
                    Doc.Archivo = (byte[])dr.GetValue(dr.GetOrdinal("Archivo"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
