using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;

namespace CapaNegocios
{
    public class CN_CatRepresentantes
    {

        //
        // 14 Ago 2018 RFH
        // Se crea esta funcion para crm2
        //

        public void Consultar_RepresentantesCombo(int Id_Emp, int Id_U, ref List<CrmLista> Lista, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            CR.Consultar_RepresentantesCombo(Id_Emp, Id_U, ref Lista, Conexion);
        }

        // 02SEP-2020
        public List<eUsuarioRik> Consultar_Representantes_ById_Cd(
            int Id_Emp, int Id_Cd, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.Consultar_Representantes_ById_Cd(Id_Emp, Id_Cd, Conexion);
        }

        // DIC-2020
        public List<eUsuarioRik> spGetListByUser(
            int Id_Emp, int Id_Cd, int Id_U, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spGetListByUser(Id_Emp, Id_Cd, Id_U, Conexion);
        }

        // DIC-2020
        public List<eUsuarioRik> spCatUsuario_Search(
            int Id_Emp, int Id_Cd, int Id_U, string Texto, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spCatUsuario_Search(Id_Emp, Id_Cd, Id_U, Texto, Conexion);
        }

        // 27MAY-2021 - Desplegar un listado de REPRESENTANTES segun el cliente
        public List<eUsuarioRik> spCatUsuario_SearchByCte(
            int Id_Emp, int Id_Cd, int Id_Cte, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spCatUsuario_SearchByCte(Id_Emp, Id_Cd, Id_Cte, Conexion);
        }

        // 09MAR2021
        public List<eUsuarioRik> spCatRik_ComboTodos_ver2(int Id_Emp, int Id_Cd, int Id_U, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spCatRik_ComboTodos_ver2(Id_Emp, Id_Cd, Id_U, Conexion);
        }

        // 10MAR2022
        public List<eUsuarioRik> spCatRik_ComboTodos_ver3(
            int Id_Emp, int Id_Cd, int Id_U, int Anio, int Mes, int rol, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spCatRik_ComboTodos_ver3(Id_Emp, Id_Cd, Id_U, Anio, Mes, rol, Conexion);
        }

        public void ConsultarRepresentantesCombo(int Cd, ref List<CrmLista> Lista, string Conexion)
        {
            try
            {
                CD_CatRepresentantes CR = new CD_CatRepresentantes();
                CR.ConsultarRepresentantesCombo(1, 1, Cd, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRepresentantes(Representantes representantes, string Conexion, ref List<Representantes> List)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ConsultarRepresentantes(representantes, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //SAUL GUERRA 20150506  BEGIN
        public void ConsultarRepresentantePorTerritorio(Territorios territorio, string Conexion, ref Representantes representante)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ConsultarRepresentantePorTerritorio(territorio, Conexion, ref representante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //SAUL GUERRA 20150506 EN

        public void InsertarRepresentantes(Representantes representantes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.InsertarRepresentantes(representantes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRepresentantes(Representantes representantes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ModificarRepresentantes(representantes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarRepresentantesDet(Representantes representante, List<Comun> lc, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.InsertarRepresentantesDet(representante, lc, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRepresentantesDet(RikUen representante, string Conexion, ref List<RikUen> lc)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ConsultarRepresentantesDet(representante, Conexion, ref lc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ComboRepresentanteXTerritorio(int pIdEmp, int pIdCd, int pIdTer, string pConexion, ref List<Representantes> pList)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ComboRepresentanteXTerritorio(pIdEmp, pIdCd, pIdTer, pConexion, ref pList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<eUsuarioRik> spCatRik_ComboTodos(int Id1, int Id2, string Conexion)
        {
            CD_CatRepresentantes CR = new CD_CatRepresentantes();
            return CR.spCatRik_ComboTodos(Id1, Id2, Conexion);
        }

    }
}