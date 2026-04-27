
using CapaModelo_CC.CuentasCoorporativas;
using CapaDatos;
using System.Collections.Generic;
using System;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatCNac_Matriz
    {


        //public CN_CatCNac_Matriz(sianwebmty_gEntities modelo)
        //{
        //    model = modelo;
        //}


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarTodos();
        }

        public List<CatCNac_Estructura> ConsultarEstructura(int idMatriz, int Id_Emp, int Id_Cd)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarEstructura(idMatriz, Id_Emp, Id_Cd);
        }


        public CatCNac_Estructura ConsultarEstructura(int id_Cte)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarEstructura(id_Cte);
        }


        public List<CatCNac_Estructura> ConsultarEstructura()
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarEstructura();
        }

        public CatCliente ConsultaCliente(int idCliente, int Id_Emp, int Id_Cd)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultaCliente(idCliente, Id_Emp, Id_Cd);
        }


        public List<CatACYS_DirFiscales> ConsultaDireccionesFiscales(int idMatriz)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultaDireccionesFiscales(idMatriz);
        }

        public void ConsultaDireccionesFiscales_SP(string clienteSIAN, int Id_Cd, ref List<catcnac_DireccionesFiscales> listadireccion, string Conexion)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            CMatriz.ConsultaDireccionesFiscales_SP(clienteSIAN, Id_Cd, ref listadireccion, Conexion);
        }


        public List<Intra_CFD_CuentaNacional> ConsultaIntranetCuentaNacional(int idMatriz, int DireccionId)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultaIntranetCuentaNacional(idMatriz, DireccionId);
        }



        public List<CatCNac_Usuario> ComboAsesores(int idMatriz)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ComboAsesores(idMatriz);
        }

        public List<CatCNac_RemisionesMov80> ComboRemisionesMov80()
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ComboRemisionesMov80();
        }


        public int GuardarSolicitud(CatCNac_Solicitudes solicitud)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.GuardarSolicitud(solicitud);

        }

        public spCNac_EnviarSolicitudes_Sol_Result ConsultarSolicitudes_Sol(int id)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarSolicitudes_Sol(id);
        }


        public List<CatCNac_Solicitudes> ConsultarSolicitudes(string usuario)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarSolicitudes(usuario);

        }

        public CatCNac_Solicitudes ConsultarSolicitudes(int idEstructura)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.ConsultarSolicitudes(idEstructura);
        }

        public Boolean CancelarSolicitud(int idEstructura)
        {
            CD_CatCNac_Matriz CMatriz = new CD_CatCNac_Matriz();
            return CMatriz.CancelarSolicitud(idEstructura);
        }


    }
}