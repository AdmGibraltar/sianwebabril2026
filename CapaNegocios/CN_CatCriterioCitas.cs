using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace CapaNegocios
{
    public class CN_CatCriterioCitas
    {
        public void GeneraPaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.GeneraPaginaAyuda(PagASPX, Conexion, ref PagHTML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignaPaginaAyudaPorId(int IdOpcion, string Conexion, string PagHTML, ref int Ret)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.AsignaPaginaAyudaPorId(IdOpcion, Conexion, PagHTML, ref Ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtienePaginaAyudaPorId(string IdPagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ObtienePaginaAyudaPorId(IdPagASPX, Conexion, ref PagHTML);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtienePaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ObtienePaginaAyuda(PagASPX, Conexion, ref PagHTML);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneModulo(string PagHTML, string Conexion, ref string Modulo)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ObtieneModulo(PagHTML, Conexion, ref Modulo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EsDiaFestivo(string Conexion, string Fecha, ref int resul) //spEsDiaFestivo 
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.EsDiaFestivo(Conexion, Fecha, ref resul);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVISucursal(Embudo ElEmbudo, string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaVISucursal(ElEmbudo, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientesDetalle(Embudo ElEmbudo, string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaClientesDetalle(ElEmbudo, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVI(Embudo ElEmbudo, string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaVI(ElEmbudo, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmbudo(Embudo ElEmbudo, string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaEmbudo(ElEmbudo, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteHistorialPedido(string Conexion, ref List<FlujoCitas> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ReporteHistorialPedido(Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFlujoTodos(FlujoCitas fluj, string Conexion, ref List<FlujoCitas> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaFlujoTodos(fluj, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPreRequisitos(PreRequisitos PreReq, string Conexion, ref List<PreRequisitos> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaPreRequisitos(PreReq, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaFlujo(FlujoCitas Flujoo, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertaFlujo(Flujoo, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaPreRequisitos(PreRequisitos PreReq, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertaPreRequisitos(PreReq, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDiasInhabilesAño(DiasInhabiles ElDia, int Anio, int mes, string Conexion, ref List<DiasInhabiles> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaDiasInhabilesAño(ElDia, Anio, mes, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFrecuenciaCita(FrecuenciaCliente LAFrecuecnaiCita, string Conexion, ref List<FrecuenciaCliente> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaFrecuenciaCita(LAFrecuecnaiCita, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaActividadCita(ActividadCita LActividadCita, string Conexion, ref List<ActividadCita> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaActividadCita(LActividadCita, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCategoriasCita(CategoriaCliente CategoCliente, string Conexion, ref List<CategoriaCliente> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaCategoriasCita(CategoCliente, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCriteriosCita(CriterioCita criterioCita, string Conexion, ref List<CriterioCita> List)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ConsultaCriteriosCita(criterioCita, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoPrerequisitosCita_Todos(string Conexion, string sp, string cita, ref CheckBoxList Listado)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                claseCapaDatos.ListadoPrerequisitosCita_Todos(Conexion, sp, cita, ref Lista);

                if (Lista.Count > 0)
                {
                    Listado.DataSource = Lista;
                    Listado.DataValueField = "Id";
                    Listado.DataTextField = "Descripcion";
                    Listado.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoPrerequisitos_Todos(string Conexion, string sp, ref CheckBoxList Listado)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                claseCapaDatos.ListadoPrerequisitos_Todos(Conexion, sp, ref Lista);

                if (Lista.Count > 0)
                {
                    Listado.DataSource = Lista;
                    Listado.DataValueField = "Id";
                    Listado.DataTextField = "Descripcion";
                    Listado.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaPreRequisitos(int idPre, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.EliminaPreRequisitos(idPre, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaDiasInhabiles(int Eldi, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.EliminaDiasInhabiles(Eldi, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertaDiasInhabiles(DiasInhabiles Eldi, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertaDiasInhabiles(Eldi, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFrecuenciaCita(FrecuenciaCliente Acti, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertaFrecuenciaCita(Acti, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarActividadCita(ActividadCita Acti, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertarActividadCita(Acti, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCategoriaCliente(CategoriaCliente Cate, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertarCategoriaCliente(Cate, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.InsertarCriteriosCita(Cita, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ModificarCriteriosCita(Cita, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarFechaCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.AsignarFechaCriteriosCita(Cita, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneTipoVisitaCte(int Id_Emp, int Id_Cd, int cliente, string Conexion, ref string visita)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ObtieneTipoVisitaCte(Id_Emp, Id_Cd, cliente, Conexion, ref visita);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosEmpresaCita(int Id_CitaVisita, ref string Empr, ref string Conta, ref string Fromz, ref string Toz, string Conexion)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.ObtieneDatosEmpresaCita(Id_CitaVisita, ref Empr, ref Conta, ref Fromz, ref Toz, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosCriterioCita(int Id_CitaVisita, ref RadTextBox txtRSC, string Conexion)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                string ttxt = "";
                claseCapaDatos.ObtieneDatosCriterioCita(Id_CitaVisita, ref ttxt, Conexion);
                txtRSC.Text = ttxt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaMotivoModificacion(int Id_CitaVisita, string Motivo, string Conexion)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.GrabaMotivoModificacion(Id_CitaVisita, Motivo, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaModificacion(int Id_CitaVisita, string Conexion)
        {
            try
            {
                CD_CatCriterioCitas claseCapaDatos = new CD_CatCriterioCitas();
                claseCapaDatos.CancelaModificacion(Id_CitaVisita, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
