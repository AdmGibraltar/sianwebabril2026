using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CapAcys2
    {
        public CN_CapAcys2()
        {
        }

        //
        // 10 Jul 2018 Listado
        // 8 Oct 2018 RFH Actualizado  
        public void ConsultarAcys_Lista2(
            int PageNumber, int PageSize,
            int AplicaFecha, int AplicaFolio, int AplicaCliente,
            eAcys2 ACyS, string Conexion, ref List<eAcys2> List, int UsuarioConsulta_Id, int TipoCuenta)
        {
            CD_CapAcys2 claseCapaDatos = new CD_CapAcys2();

            claseCapaDatos.ConsultarAcys_Lista2(
                    PageNumber, PageSize,
                    AplicaFecha, AplicaFolio, AplicaCliente,
                    ACyS, Conexion, ref List,
                    UsuarioConsulta_Id, TipoCuenta
             );
        }

        // 16 Jul 2018 RFH 
        // Consulta por Id
        public void Consultar_PorFolio(ref eAcys2 acys, int UsuarioConsulta_Id, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            CD.Consultar_PorFolio(ref acys, UsuarioConsulta_Id, Conexion);
        }

        // ABR21-2021  RFH         
        public List<eAcys2> spAcuerdosExistentesPorCliente(
            int Id_Emp, int Id_Cd, int IdCte, int IdRik, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            return CD.spAcuerdosExistentesPorCliente(Id_Emp, Id_Cd, IdCte, IdRik, Conexion);
        }

        // 10 Dic 2018 RFH 
        public List<eAcys2> Consultar_Por_Folio(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, string Conexion)
        {
            List<eAcys2> lst = new List<eAcys2>();
            eAcys2 obj = new eAcys2();

            CD_CapAcys2 CD = new CD_CapAcys2();
            obj = CD.Consultar_Por_Folio(Id_Emp, Id_Cd, Id_Acs, Id_AcsVersion, Conexion);

            lst.Add(obj);
            return lst;
        }

        //
        // 15 Oct 2018 RFH 
        // Consulta por Id
        //
        public void Consultar_PorFolio_ver2(ref eAcys2 acys, int Id_Rik, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            CD.Consultar_PorFolio_ver2(ref acys, Id_Rik, Conexion);
        }

        //
        // 15 Oct 2018 RFH 
        // Consulta por Id
        //
        public void spCapAcys2_GetLogs(ref eAcys2 acys, int Id_Rik, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            CD.Consultar_PorFolio_ver2(ref acys, Id_Rik, Conexion);
        }


        //
        // 13 Ago 2018 RFH 
        // Actualizar  
        //
        public int InsertUpdate(ref eAcys2 acys, ref string MensajeError, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            return CD.InsertUpdate(ref acys, ref MensajeError, Conexion);
        }

        //
        // 29 Oct 2018 RFH 
        // Actualizar  
        //
        public int Renovar(int Id_Emp, int Id_Cd, int Id_Acs, int Acs_Version, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            return CD.Renovar(Id_Emp, Id_Cd, Id_Acs, Acs_Version, Conexion);
        }

        // 30 Oct 2018 
        public int Es_Autorizable(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Es_Autorizable(Id_Emp, Id_Cd, Id_Acs, Id_AcsVersion, Conexion);
        }

        // 30 Oct 2018 
        public int Eliminar(Acys acys, int Id_U_Actual, string Conexion, ref int verificador)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Eliminar(acys, Id_U_Actual, Conexion, ref verificador);
        }

        // 22 Nov 2018 RFH 
        public int Reactivar(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, int Id_Usuario_Actual, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Reactivar(Id_Emp, Id_Cd, Id_Acs, Id_AcsVersion, Id_Usuario_Actual, Conexion);
        }

        public void ConsultaUltimaVersion(ref Acys acys, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            claseCapaDatos.ConsultaUltimaVersion(ref acys, Conexion);
        }

        // 30 Oct 2018 
        public int Actualiza_Estatus(int Id_Emp, int Id_Cd, int Id_Acs, string Acs_Estatus, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Actualiza_Estatus(
                Id_Emp, Id_Cd, Id_Acs, Acs_Estatus, Conexion);
        }

        //
        // 3JUL-2019
        // Autorizacion Gerente
        public int Actualizar_Autorizacion_Gerente(
            int Id_Emp, int Id_Cd, int Id_Acs, int Acs_ReqAutGerente, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Actualizar_Autorizacion_Gerente(
                Id_Emp, Id_Cd, Id_Acs, Acs_ReqAutGerente,
                Conexion);
        }

        //
        // ABR28-2020 RFH
        //
        public eAcys2ValiacionesPreAutorizacion Valizaciones_PreAutorizacion(
            int Id_Emp, int Id_Cd, int Id_Acs, int Acs_Version, string Conexion)
        {
            CD_CapAcys CD = new CD_CapAcys();
            return CD.Valizaciones_PreAutorizacion(Id_Emp, Id_Cd, Id_Acs, Acs_Version, Conexion);
        }

        //
        // 3JUL-2019
        // Autorizacion Jefe Operaciones
        public int Actualizar_Autorizacion_JefeOperacion(
            int Id_Emp, int Id_Cd, int Id_Acs, int Acs_ReqAutGerente, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.Actualizar_Autorizacion_JefeOperacion(
                Id_Emp, Id_Cd, Id_Acs, Acs_ReqAutGerente,
                Conexion);
        }

        //9JUL-2019 
        // GUARDA EL COMENTARIO DE RECHAZO JEFE OP
        public int EXEC_spCapAcys_AplicarRechazo_ReqAutJefeOp(
        int Id_Emp, int Id_Cd, int Id_Acs, int Acs_Version, int Id_Usuario, string Nota, int TipoUsuario,
            string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.EXEC_spCapAcys_AplicarRechazo_ReqAutJefeOp(
            Id_Emp, Id_Cd, Id_Acs, Acs_Version, Id_Usuario, Nota, TipoUsuario, Conexion);
        }

        // 16 JUL 2019 
        // Regresa el mail del usaurio que creo el acys.
        //
        public string EXEC_spCapAcys_MaildelRik(int Id_Emp, int Id_Cd, int Id_Acs, int Acs_Version, string Conexion)
        {
            CD_CapAcys CD = new CD_CapAcys();
            return CD.EXEC_spCapAcys2_MailDeRik(Id_Emp, Id_Cd, Id_Acs, Acs_Version, Conexion);
        }

        //
        public int EXEC_spCapAcys_GenerarCalendario(int Id_Acs, string Conexion)
        {
            CD_CapAcys claseCapaDatos = new CD_CapAcys();
            return claseCapaDatos.EXEC_spCapAcys_GenerarCalendario(Id_Acs, Conexion);
        }

        //
        // 11JUL-2019
        //
        public List<eAcys2Logs> EXEC_spCapAcys2_GetLogs(
            int Id_Emp, int Id_Cd, int Id_Acs, int AcsVersion, string Conexion)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            return CD.EXEC_spCapAcys2_GetLogs(Id_Emp, Id_Cd, Id_Acs, AcsVersion, Conexion);
        }


        //

    }
}