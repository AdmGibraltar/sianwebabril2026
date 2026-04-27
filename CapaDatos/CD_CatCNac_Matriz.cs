using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CapaModelo_CC.CuentasCoorporativas;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCNac_Matriz
    {
        sianwebmty_CCEntities model = new sianwebmty_CCEntities();
        //public CD_CatCNac_Matriz(sianwebmty_gEntities modelo)
        //{
        //    model = modelo;
        //}


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            var res = model.CatCNac_Matriz.ToList();
            return res;
        }


        public List<CatCNac_Estructura> ConsultarEstructura(int idMatriz, int Id_Emp, int Id_Cd)
        {
            var res = model.CatCNac_Estructura.Where(x => x.Id_Matriz == idMatriz && x.Nivel_ACYS != null && x.Sucursal == Id_Cd).ToList();
            return res;
        }


        public CatCNac_Estructura ConsultarEstructura(int Id_Cte)
        {
            var res = model.CatCNac_Estructura.Where(x => x.Id_Cte == Id_Cte && x.Nivel_ACYS != null).FirstOrDefault();
            return res;
        }



        public List<CatCNac_Estructura> ConsultarEstructura()
        {
            var res = model.CatCNac_Estructura.Where(x => x.Id_Matriz != null && x.Nivel_ACYS != null).ToList();
            return res;
        }


        public CatCliente ConsultaCliente(int idCliente, int Id_Emp, int Id_Cd)
        {
            var res = model.CatCliente.Where(x => x.Id_Cte == idCliente && x.Id_Emp == Id_Emp && x.Id_Cd == Id_Cd).FirstOrDefault();
            return res;

        }

        public List<CatACYS_DirFiscales> ConsultaDireccionesFiscales(int idMatriz)
        {
            var res = model.CatACYS_DirFiscales.Where(x => x.Id_ClienteMatriz == idMatriz).ToList();
            return res;
        }

        public List<spCatCNac_DireccionesFiscales_Result> ConsultaDireccionesFiscales_SP(string clienteSIAN, int Id_Cd)
        {
            var res = model.spCatCNac_DireccionesFiscales(clienteSIAN, Id_Cd).ToList();
            return res;
        }


        public List<Intra_CFD_CuentaNacional> ConsultaIntranetCuentaNacional(int idMatriz, int DireccionId)
        {

            var res = model.Intra_CFD_CuentaNacional.Where(x => x.id_Matriz == idMatriz && x.DireccionID == DireccionId).ToList();
            return res;
        }

        public void ConsultaDireccionesFiscales_SP(string clienteSIAN, int Id_Cd, ref List<catcnac_DireccionesFiscales> listadireccion, string Conexion)
        {
            try
            {
                catcnac_DireccionesFiscales direccion = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@ClienteSIAN", "@ID_CD" };
                object[] Valores = { clienteSIAN, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_DireccionesFiscales2", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    direccion = new catcnac_DireccionesFiscales();


                    direccion.id_ClienteMatriz = dr.IsDBNull(dr.GetOrdinal("id_ClienteMatriz")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_ClienteMatriz")));
                    direccion.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    direccion.ClienteDirFis = dr.IsDBNull(dr.GetOrdinal("ClienteDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("ClienteDirFis")).ToString();
                    direccion.DireccionDirFis = dr.IsDBNull(dr.GetOrdinal("DireccionDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("DireccionDirFis")).ToString();
                    direccion.MunicipioDirFis = dr.IsDBNull(dr.GetOrdinal("MunicipioDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("MunicipioDirFis")).ToString();
                    direccion.Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Pais")).ToString();
                    direccion.CPDirFis = dr.IsDBNull(dr.GetOrdinal("CPDirFis")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CPDirFis")));
                    direccion.EstadoDirFis = dr.IsDBNull(dr.GetOrdinal("EstadoDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("EstadoDirFis")).ToString();
                    direccion.RFCDirFis = dr.IsDBNull(dr.GetOrdinal("RFCDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("RFCDirFis")).ToString();
                    direccion.EmailFacturasDirFis = dr.IsDBNull(dr.GetOrdinal("EmailFacturasDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("EmailFacturasDirFis")).ToString();
                    direccion.ColoniaDirFis = dr.IsDBNull(dr.GetOrdinal("ColoniaDirFis")) ? string.Empty : dr.GetValue(dr.GetOrdinal("ColoniaDirFis")).ToString();
                    direccion.NumeroDirFis = dr.IsDBNull(dr.GetOrdinal("NumeroDirFis")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("NumeroDirFis")));
                    direccion.FranqConsecionada = dr.IsDBNull(dr.GetOrdinal("FranqConsecionada")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FranqConsecionada")));
                    direccion.Addenda = dr.IsDBNull(dr.GetOrdinal("Addenda")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Addenda")));
                    direccion.AddendaTipo = dr.IsDBNull(dr.GetOrdinal("AddendaTipo")) ? string.Empty : dr.GetValue(dr.GetOrdinal("AddendaTipo")).ToString();
                    direccion.AddendaDesc = dr.IsDBNull(dr.GetOrdinal("AddendaDesc")) ? string.Empty : dr.GetValue(dr.GetOrdinal("AddendaDesc")).ToString();

                    listadireccion.Add(direccion);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CatCNac_Usuario> ComboAsesores(int idMatriz)
        {
            var res = model.CatCNac_Usuario.Where(x => x.IdMatriz == idMatriz).ToList();
            return res;
        }

        public List<CatCNac_RemisionesMov80> ComboRemisionesMov80()
        {
            var res = model.CatCNac_RemisionesMov80.ToList();
            return res;
        }

        public int GuardarSolicitud(CatCNac_Solicitudes solicitud)
        {
            model.CatCNac_Solicitudes.Add(solicitud);
            model.SaveChanges();

            int id = solicitud.Id;

            return id;
        }

        public List<CatCNac_Solicitudes> ConsultarSolicitudes(string usuario)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Usuario == usuario).OrderBy(x => x.Estatus).ToList();
            return res;
        }


        public CatCNac_Solicitudes ConsultarSolicitudes(int idEstructura)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Id_Estructura == idEstructura && (x.Estatus == 1 || x.Estatus == 2)).FirstOrDefault();
            return res;
        }


        public spCNac_EnviarSolicitudes_Sol_Result ConsultarSolicitudes_Sol(int id)
        {
            var res = model.spCNac_EnviarSolicitudes_Sol(id).FirstOrDefault();
            return res;
        }



        public Boolean CancelarSolicitud(int idEstructura)
        {
            var sol = model.CatCNac_Solicitudes.Where(x => x.Id_Estructura == idEstructura && x.Estatus == 1).FirstOrDefault();

            sol.Estatus = 4;
            model.Entry<CatCNac_Solicitudes>(sol).State = EntityState.Modified;
            model.SaveChanges();
            return true;
        }


    }
}