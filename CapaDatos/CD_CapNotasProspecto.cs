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
    public class CD_CapNotasProspecto
    {
        // 18 Ene 2019 RFH  
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }
        public CapNotasProspecto Insertar(int idEmp, int idCd, int idRik, int idCte, int idNota, string cadenaConexionEF)
        {
            CapNotasProspecto result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                result = new CapNotasProspecto()
                {
                    Id_Emp = idEmp,
                    Id_Cd = idCd,
                    Id_Rik = idRik,
                    Id_Cliente = idCte,
                    Id_Nota = idNota
                };

                ctx.CapNotasProspectoes.Add(result);
                ctx.SaveChanges();
                result.CatNotaSerializable = result.CatNota;
            }
            return result;
        }

        public void Eliminar(int idEmp, int idCd, int idRik, int idCte, int idNota, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CapNotasProspectoes
                             where n.Id_Emp == idEmp && n.Id_Cd == idCd && n.Id_Rik == idRik && n.Id_Cliente == idCte && n.Id_Nota == idNota
                             select n).ToList();
                if (notas.Count > 0)
                {
                    ctx.CapNotasProspectoes.Remove(notas[0]);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Regresa el conjunto de notas asociadas al prospecto con identificador de cliente idCte.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idRik"></param>
        /// <param name="idCte">Identificador del cliente asignado al prospecto</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato para uso en EntityFramework</param>
        /// <returns>Conjunto de notas asociados al prospecto idCte</returns>
        public IEnumerable<CapNotasProspecto> ConsultarPorProspecto(int idEmp, int idCd, int idRik, int idCte, string cadenaConexionEF)
        {
            IEnumerable<CapNotasProspecto> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CapNotasProspectoes
                             where n.Id_Emp == idEmp && n.Id_Cd == idCd && n.Id_Rik == idRik && n.Id_Cliente == idCte
                             select n).ToList().Select(cnp => { cnp.CatNotaSerializable = cnp.CatNota; return cnp; }).ToList();
                result = notas;
            }
            return result;
        }

        // 18 Ene 2019 RFH 

        public List<eNotasProspecto> spCrmNotasProspecto(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion)
        {
            List<eNotasProspecto> lst = new List<eNotasProspecto>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Cte", Id_Cte);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmNotasProspecto", ref dr, Parametros.ToArray(), Valores.ToArray());
                while (dr.Read())
                {
                    eNotasProspecto Obj = new eNotasProspecto();
                    Obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    Obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cliente")));
                    Obj.Id_Nota = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Nota")));
                    Obj.Text = dr.GetValue(dr.GetOrdinal("Texto")).ToString();
                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }

        //
    }
}
