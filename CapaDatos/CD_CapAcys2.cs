
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
    public class CD_CapAcys2
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // 20 NOV 2018 RFH 

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        // Abr21-2021 RFH
        public List<eAcys2> spAcuerdosExistentesPorCliente(
          int Id_Emp, int Id_Cd, int Id_Cte, int IdRik, string Conexion)
        {
            List<eAcys2> lst = new List<eAcys2>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Cte", Id_Cte);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcuerdosExistentesPorCliente", ref dr, Parametros.ToArray(), Valores.ToArray());
                while (dr.Read())
                {
                    eAcys2 obj = new eAcys2();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                    obj.Acs_version = dr.GetValue(dr.GetOrdinal("Acs_version")).ToString();
                    obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));

                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    obj.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    try
                    {
                        obj.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFin")).ToString();
                    }
                    catch (Exception ex) { }
                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }

        // 27 Feb 2020 Actualizdo RF
        // Indice ACyS
        public void ConsultarAcys_Lista2(
                int PageNumber, int PageSize,
                int AplicaFecha, int AplicaFolio, int AplicaCliente,
                eAcys2 ACyS, string Conexion, ref List<eAcys2> List,
                int UsuarioConsulta_Id, int TipoCuenta
       )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@PageNumber", PageNumber);
                Parametro("@PageSize", PageSize);
                Parametro("@Id_Emp", ACyS.Id_Emp);
                Parametro("@Id_Cd", ACyS.Id_Cd);
                Parametro("@Filtro_Estatus", ACyS.Filtro_Estatus); // * Estatus

                Parametro("@AplicaFecha", AplicaFecha);
                Parametro("@Filtro_FecIni", ACyS.Filtro_FecIni);
                Parametro("@Filtro_FecFin", ACyS.Filtro_FecFin);

                Parametro("@AplicaFolio", AplicaFolio);
                Parametro("@Filtro_FolIni", ACyS.Filtro_FolIni);
                Parametro("@Filtro_FolFin", ACyS.Filtro_FolFin);

                Parametro("@AplicaCliente", AplicaCliente);
                Parametro("@Id_Cte", ACyS.Id_Cte);
                Parametro("@NombreCliente", ACyS.Cte_Nombre);

                Parametro("@Filtro_usuario", ACyS.Filtro_Usuario);
                Parametro("@Id_Ter", ACyS.Id_Ter);
                Parametro("@Id_Rik", ACyS.Id_Rik);

                Parametro("@Acs_Vencido", ACyS.Acs_Vencido);
                Parametro("@Id_Modalidad", ACyS.Id_Modalidad);

                Parametro("@UsuarioConsulta_Id", UsuarioConsulta_Id);
                Parametro("@TipoCuenta", TipoCuenta);


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista2", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eAcys2 obj = new eAcys2();

                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Acs_version = dr.GetValue(dr.GetOrdinal("Id_AcsVersion")).ToString();
                    obj.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    obj.Acs_EstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();

                    obj.Identificador = dr.GetValue(dr.GetOrdinal("Identificador")).ToString();
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));

                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));

                    obj.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    obj.Acs_FechaInicio = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    obj.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();

                    obj.Acs_Vencido = dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                    obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();

                    obj.Acs_ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                    obj.Acs_ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                    obj.RecordCount = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RecordCount")));

                    obj.UsuarioConsulta_Id_Tu = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UsuarioConsulta_Id_Tu")));
                    obj.UsuarioConsulta_Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UsuarioConsulta_Id_Rik")));
                    //obj.IDCN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDCN")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TipoCuenta"))))
                    {
                        obj.IDCN = 0;
                        obj.CNDescripcion = "L";
                    }
                    else
                    {
                        obj.IDCN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoCuenta")));
                        switch (obj.IDCN)
                        {
                            case 0:
                                obj.CNDescripcion = "L"; // Local
                                break;
                            case 1:
                                obj.CNDescripcion = "CN"; // Cuenta Nacional
                                break;
                            case 2:
                                obj.CNDescripcion = "CC"; // Cuenta Coordinada
                                break;
                        }
                    }

                    List.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 11JUL-2019 
        public List<eAcys2Logs> EXEC_spCapAcys2_GetLogs(
            int Id_Emp, int Id_Cd, int Id_Acs, int Acs_Version, string Conexion)
        {
            List<eAcys2Logs> lst = new List<eAcys2Logs>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Acs_Version", Acs_Version);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_GetLogs", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eAcys2Logs obj = new eAcys2Logs();
                    obj.IdCapAcysLogs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("Fecha")));
                    obj.Id_Usuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usuario")));
                    obj.Nota = dr.GetValue(dr.GetOrdinal("Nota")).ToString();
                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }

        // 22 Nov 2018 
        //  INDICE / ORDENES DE CLIENTE 
        public void spCapAcys2_ListadoOrdenes(
                int PageNumber, int PageSize,
                int AplicaFecha, int AplicaFolio,
                eAcys2 acys, string Conexion, ref List<eAcys2> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@PageNumber", PageNumber);
                Parametro("@PageSize", PageSize);
                Parametro("@Id_Emp", acys.Id_Emp);
                Parametro("@Id_Cd", acys.Id_Cd);

                Parametro("@Filtro_Estatus", acys.Filtro_Estatus);
                Parametro("@AplicaFecha", AplicaFecha);
                Parametro("@Filtro_FecIni", acys.Filtro_FecIni);
                Parametro("@Filtro_FecFin", acys.Filtro_FecFin);

                Parametro("@AplicaFolio", AplicaFolio);
                Parametro("@Filtro_FolIni", acys.Filtro_FolIni);
                Parametro("@Filtro_FolFin", acys.Filtro_FolFin);
                Parametro("@Filtro_usuario", acys.Filtro_Usuario);

                Parametro("@Id_Ter", acys.Id_Ter);
                Parametro("@Id_Rik", acys.Id_Rik);
                Parametro("@Id_Cte", acys.Id_Cte);
                Parametro("@Acs_Vencido", acys.Acs_Vencido);
                Parametro("@Id_Modalidad", acys.Id_Modalidad);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_ListadoOrdenes", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eAcys2 obj = new eAcys2();

                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Acs_version = dr.GetValue(dr.GetOrdinal("Id_AcsVersion")).ToString();
                    obj.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    obj.Acs_EstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();

                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik"))); dr.GetValue(dr.GetOrdinal("Id_Ter")).ToString();
                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));

                    obj.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    obj.Acs_FechaInicio = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    obj.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();

                    obj.Acs_Vencido = dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                    obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();

                    obj.RecordCount = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RecordCount")));

                    List.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 25 Jul 2018 RFH
        // CONSULTAR POR FOLIO 
        // CONSULTA PARA EL NUEVO DOC ACYS
        public void Consultar_PorFolio_ver2(ref eAcys2 acys, int Id_Rik_Consulta, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", acys.Id_Emp);
                Parametro("@Id_Cd", acys.Id_Cd);
                Parametro("@Id_Acs", acys.Id_Acs);
                Parametro("@Id_AcsVersion", acys.Id_AcsVersion);
                // Se refiera al usuario que esta consultando
                Parametro("@Id_Usuario_Consulta", Id_Rik_Consulta);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_ConsultarPorFolio_ver2", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();

                    acys.Documento_PermiteEditara = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Documento_PermiteEditara")));

                    acys.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    acys.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    acys.Acs_RikNombre = dr.GetValue(dr.GetOrdinal("Acs_RikNombre")).ToString();
                    acys.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));

                    acys.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    acys.Acs_EstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();

                    acys.Id_Ade = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));

                    acys.Cte_PagoUsoCFDI = dr.GetValue(dr.GetOrdinal("Cte_PagoUsoCFDI")).ToString();

                    acys.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));

                    acys.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Acs_NomComercial")).ToString();
                    acys.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    acys.Acs_FechaInicio = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    acys.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();

                    acys.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    acys.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    acys.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    acys.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();

                    // DIRECCION DEL CLIENTE
                    acys.ClienteDireccion = dr.GetValue(dr.GetOrdinal("ClienteDireccion")).ToString();
                    acys.ClienteColonia = dr.GetValue(dr.GetOrdinal("ClienteColonia")).ToString();
                    acys.ClienteMunicipio = dr.GetValue(dr.GetOrdinal("ClienteMunicipio")).ToString();
                    acys.ClienteEstado = dr.GetValue(dr.GetOrdinal("ClienteEstado")).ToString();
                    acys.ClienteRFC = dr.GetValue(dr.GetOrdinal("ClienteRFC")).ToString();
                    acys.ClienteCodPost = dr.GetValue(dr.GetOrdinal("ClienteCodPost")).ToString();

                    acys.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    acys.Id_Corp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));

                    //(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) > 1 && Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) != 10000) ? true : false;

                    acys.AddendaSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AddendaSi"))) > 0 ? true : false;

                    acys.DireccionEntrega = dr.GetValue(dr.GetOrdinal("DireccionEntrega")).ToString();
                    acys.ClienteColoniaE = dr.GetValue(dr.GetOrdinal("ClienteColoniaE")).ToString();
                    acys.ClienteMunicipioE = dr.GetValue(dr.GetOrdinal("ClienteMunicipioE")).ToString();
                    acys.ClienteCPE = dr.GetValue(dr.GetOrdinal("ClienteCPE")).ToString();
                    acys.ClienteEstadoE = dr.GetValue(dr.GetOrdinal("ClienteEstadoE")).ToString();

                    // OTROS APOLLOS
                    acys.Acs_Contacto2 = dr.GetValue(dr.GetOrdinal("Acs_Contacto2")).ToString();
                    acys.Acs_Telefono2 = dr.GetValue(dr.GetOrdinal("Acs_Telefono2")).ToString();
                    acys.Acs_email2 = dr.GetValue(dr.GetOrdinal("Acs_Email2")).ToString();

                    acys.Acs_Contacto3 = dr.GetValue(dr.GetOrdinal("Acs_Contacto3")).ToString();
                    acys.Acs_Telefono3 = dr.GetValue(dr.GetOrdinal("Acs_Telefono3")).ToString();
                    acys.Acs_email3 = dr.GetValue(dr.GetOrdinal("Acs_email3")).ToString();

                    acys.Acs_Contacto4 = dr.GetValue(dr.GetOrdinal("Acs_Contacto4")).ToString();
                    acys.Acs_Telefono4 = dr.GetValue(dr.GetOrdinal("Acs_Telefono4")).ToString();
                    acys.Acs_email4 = dr.GetValue(dr.GetOrdinal("Acs_email4")).ToString();

                    acys.Acs_Contacto5 = dr.GetValue(dr.GetOrdinal("Acs_Contacto5")).ToString();
                    acys.Acs_Telefono5 = dr.GetValue(dr.GetOrdinal("Acs_Telefono5")).ToString();
                    acys.Acs_Correo5 = dr.GetValue(dr.GetOrdinal("Acs_email5")).ToString();

                    acys.Acs_Contacto6 = dr.GetValue(dr.GetOrdinal("Acs_Contacto6")).ToString();
                    acys.Acs_Telefono6 = dr.GetValue(dr.GetOrdinal("Acs_Telefono6")).ToString();
                    acys.Acs_Correo6 = dr.GetValue(dr.GetOrdinal("Acs_email6")).ToString();

                    acys.Acs_Proveedor = dr.GetValue(dr.GetOrdinal("Acs_NumPrv")).ToString();

                    acys.Acs_RutaServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta1")));
                    acys.Acs_RutaEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta2")));

                    acys.Acs_ReqOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    acys.Acs_VigenciaIni = dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")).ToString();
                    acys.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));

                    // FROMA DE ENVIO DE PEDIDO 

                    acys.Acs_ReqConfirmacion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqConfirmacion")));

                    acys.Acs_RecCorreo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecCorreo")));
                    acys.Acs_RecFax = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFax")));
                    acys.Acs_RecTelefono = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecTelefono")));
                    acys.Acs_RecRepresentante = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRepresentante")));
                    acys.Acs_RecPedWhats = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecPedWhats")));
                    // Recolectado por Rik
                    acys.Acs_RecRIK = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRIK")));
                    acys.Acs_RecOtro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOtro")));
                    acys.Acs_RecOtroDesc = dr.GetValue(dr.GetOrdinal("Acs_RecOtroDesc")).ToString();


                    acys.Acs_VigenciaApartir = dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")).ToString();

                    //Condiciones de Pago 
                    //Documentos 

                    acys.Acs_CondPagEntFac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntFac")));
                    acys.Acs_CondPagEntFacCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntFacCop")));
                    acys.Acs_CondPagReFac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReFac")));
                    acys.Acs_CondPagReFacCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReFacCop")));

                    acys.Acs_CondPagEntOrdCom = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntOrdCom")));
                    acys.Acs_CondPagEntOrdComCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntOrdComCop")));
                    acys.Acs_CondPagReOrdCom = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReOrdCom")));
                    acys.Acs_CondPagReOrdComCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReOrdComCop")));

                    acys.Acs_CondPagEntOrdRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntOrdRep")));
                    acys.Acs_CondPagEntOrdRepCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntOrdRepCop")));
                    acys.Acs_CondPagReOrdRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReOrdRep")));
                    acys.Acs_CondPagReOrdRepCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReOrdRepCop")));

                    acys.Acs_CondPagEntCopPed = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntCopPed")));
                    acys.Acs_CondPagEntCopPedCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntCopPedCop")));
                    acys.Acs_CondPagReCopPed = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReCopPed")));
                    acys.Acs_CondPagReCopPedCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagReCopPedCop")));

                    acys.Acs_CondPagEntPagRem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntPagRem")));
                    acys.Acs_CondPagEntPagRemCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagEntPagRemCop")));
                    acys.Acs_CondPagRePagRem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagRePagRem")));
                    acys.Acs_CondPagRePagRemCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CondPagRePagRemCop")));

                    // Eléctronica

                    acys.RevFacEmail = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RevFacEmail")));
                    acys.RevFacEmailTexto = dr.GetValue(dr.GetOrdinal("RevFacEmailTexto")).ToString();
                    acys.RevFacEmailTexto2 = dr.GetValue(dr.GetOrdinal("RevFacEmailTexto2")).ToString();
                    acys.RevFacPortal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RevFacPortal")));

                    acys.RevFacPortalTexto = dr.GetValue(dr.GetOrdinal("RevFacPortalTexto")).ToString();
                    acys.RevFacHttp = dr.GetValue(dr.GetOrdinal("RevFacHttp")).ToString();
                    acys.RevFacUsuario = dr.GetValue(dr.GetOrdinal("RevFacUsuario")).ToString();
                    acys.RevFacContrasenia = dr.GetValue(dr.GetOrdinal("RevFacContrasenia")).ToString();

                    acys.Documento_PermiteEditara = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Documento_PermiteEditara")));
                    acys.Acs_ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                    acys.Acs_ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                    acys.Desplegar_BtnAutorizar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Desplegar_BtnAutorizar")));

                    acys.IDCN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDCN")));


                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TipoCuenta"))))
                    {
                        acys.TipoCuenta = 0;
                    }
                    else
                    {
                        acys.TipoCuenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoCuenta")));
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 25 Jul 2018 RFH
        // CONSULTAR POR FOLIO 
        // ACUERDO COMERCIAL Y DE SERVICIO (ACYS)
        public void Consultar_PorFolio(ref eAcys2 acys, int UsuarioConsulta_Id, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion",
                                          "@UsuarioConsulta_Id"
                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       UsuarioConsulta_Id
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_ConsultarOrden_PorFolio", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    acys.TipoCuenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoCuenta")));
                    acys.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));

                    acys.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    acys.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    acys.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));

                    acys.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    acys.Id_Corp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa")));

                    acys.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Acs_NomComercial")).ToString();
                    acys.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    acys.Acs_FechaInicio = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    acys.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();

                    acys.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    acys.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    acys.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    acys.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();

                    // DIRECCION DEL CLIENTE
                    acys.ClienteDireccion = dr.GetValue(dr.GetOrdinal("ClienteDireccion")).ToString();
                    acys.ClienteColonia = dr.GetValue(dr.GetOrdinal("ClienteColonia")).ToString();
                    acys.ClienteMunicipio = dr.GetValue(dr.GetOrdinal("ClienteMunicipio")).ToString();
                    acys.ClienteEstado = dr.GetValue(dr.GetOrdinal("ClienteEstado")).ToString();
                    acys.ClienteRFC = dr.GetValue(dr.GetOrdinal("ClienteRFC")).ToString();
                    acys.ClienteCodPost = dr.GetValue(dr.GetOrdinal("ClienteCodPost")).ToString();
                    acys.CuentaCorporativa = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) > 1 && Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) != 10000) ? true : false;
                    acys.AddendaSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AddendaSi"))) > 0 ? true : false;

                    acys.DireccionEntrega = dr.GetValue(dr.GetOrdinal("DireccionEntrega")).ToString();
                    acys.ClienteColoniaE = dr.GetValue(dr.GetOrdinal("ClienteColoniaE")).ToString();
                    acys.ClienteMunicipioE = dr.GetValue(dr.GetOrdinal("ClienteMunicipioE")).ToString();
                    acys.ClienteCPE = dr.GetValue(dr.GetOrdinal("ClienteCPE")).ToString();
                    acys.ClienteEstadoE = dr.GetValue(dr.GetOrdinal("ClienteEstadoE")).ToString();

                    // OTROS APOLLOS
                    acys.Acs_Contacto2 = dr.GetValue(dr.GetOrdinal("Acs_Contacto2")).ToString();
                    acys.Acs_Telefono2 = dr.GetValue(dr.GetOrdinal("Acs_Telefono2")).ToString();
                    acys.Acs_email2 = dr.GetValue(dr.GetOrdinal("Acs_Email2")).ToString();

                    acys.Acs_Contacto3 = dr.GetValue(dr.GetOrdinal("Acs_Contacto3")).ToString();
                    acys.Acs_Telefono3 = dr.GetValue(dr.GetOrdinal("Acs_Telefono3")).ToString();
                    acys.Acs_email3 = dr.GetValue(dr.GetOrdinal("Acs_email3")).ToString();

                    acys.Acs_Contacto4 = dr.GetValue(dr.GetOrdinal("Acs_Contacto4")).ToString();
                    acys.Acs_Telefono4 = dr.GetValue(dr.GetOrdinal("Acs_Telefono4")).ToString();
                    acys.Acs_email4 = dr.GetValue(dr.GetOrdinal("Acs_email4")).ToString();

                    acys.Acs_Contacto5 = dr.GetValue(dr.GetOrdinal("Acs_Contacto5")).ToString();
                    acys.Acs_Telefono5 = dr.GetValue(dr.GetOrdinal("Acs_Telefono5")).ToString();
                    acys.Acs_Correo5 = dr.GetValue(dr.GetOrdinal("Acs_email5")).ToString();

                    acys.Acs_Contacto6 = dr.GetValue(dr.GetOrdinal("Acs_Contacto6")).ToString();
                    acys.Acs_Telefono6 = dr.GetValue(dr.GetOrdinal("Acs_Telefono6")).ToString();
                    acys.Acs_Correo6 = dr.GetValue(dr.GetOrdinal("Acs_email6")).ToString();

                    acys.Acs_Proveedor = dr.GetValue(dr.GetOrdinal("Acs_NumPrv")).ToString();

                    acys.Acs_RutaServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta1")));
                    acys.Acs_RutaEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta2")));

                    acys.Acs_ReqOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    acys.Acs_VigenciaIni = dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")).ToString();
                    acys.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));

                    // FROMA DE ENVIO DE PEDIDO 

                    acys.Acs_ReqConfirmacion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqConfirmacion")));

                    acys.Acs_RecCorreo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecCorreo")));
                    acys.Acs_RecFax = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFax")));
                    acys.Acs_RecTelefono = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecTelefono")));
                    acys.Acs_RecRepresentante = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRepresentante")));
                    acys.Acs_RecPedWhats = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecPedWhats")));
                    // Recolectado por Rik
                    acys.Acs_RecRIK = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRIK")));
                    acys.Acs_RecOtro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOtro")));
                    acys.Acs_RecOtroDesc = dr.GetValue(dr.GetOrdinal("Acs_RecOtroDesc")).ToString();
                    acys.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();

                    //VISITAS
                    acys.Vis_Frecuencia = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vis_Frecuencia")));

                    // L M M J V S D CD
                    acys.Vis_Lunes = dr.GetBoolean(dr.GetOrdinal("Vis_Lunes"));
                    acys.Vis_Martes = dr.GetBoolean(dr.GetOrdinal("Vis_Martes"));
                    acys.Vis_Miercoles = dr.GetBoolean(dr.GetOrdinal("Vis_Miercoles"));
                    acys.Vis_Jueves = dr.GetBoolean(dr.GetOrdinal("Vis_Jueves"));
                    acys.Vis_Viernes = dr.GetBoolean(dr.GetOrdinal("Vis_Viernes"));
                    acys.Vis_Sabado = dr.GetBoolean(dr.GetOrdinal("Vis_Sabado"));
                    acys.Vis_Domingo = dr.GetBoolean(dr.GetOrdinal("Vis_Domingo"));
                    acys.Vis_CualquierDia = dr.GetBoolean(dr.GetOrdinal("Vis_CualquierDia"));

                    acys.Vis_HrAm1 = dr.GetValue(dr.GetOrdinal("Vis_HrAm1")).ToString();
                    acys.Vis_HrAm2 = dr.GetValue(dr.GetOrdinal("Vis_HrAm2")).ToString();
                    acys.Vis_HrPm1 = dr.GetValue(dr.GetOrdinal("Vis_HrPm1")).ToString();
                    acys.Vis_HrPm2 = dr.GetValue(dr.GetOrdinal("Vis_HrPm2")).ToString();

                    //Documentos Entega y RECEPCION DE PEDIDOS
                    acys.Rec_Semanas = dr.GetValue(dr.GetOrdinal("Rec_Semanas")).ToString();
                    // Dias de recepcion 

                    // L M M J V S D CD
                    acys.Rec_Lunes = dr.GetBoolean(dr.GetOrdinal("Rec_Lunes"));
                    acys.Rec_Martes = dr.GetBoolean(dr.GetOrdinal("Rec_Martes"));
                    acys.Rec_Miercoles = dr.GetBoolean(dr.GetOrdinal("Rec_Miercoles"));
                    acys.Rec_Jueves = dr.GetBoolean(dr.GetOrdinal("Rec_Jueves"));
                    acys.Rec_Viernes = dr.GetBoolean(dr.GetOrdinal("Rec_Viernes"));
                    acys.Rec_Sabado = dr.GetBoolean(dr.GetOrdinal("Rec_Sabado"));
                    acys.Rec_Domingo = dr.GetBoolean(dr.GetOrdinal("Rec_Domingo"));
                    acys.Rec_CualquierDia = dr.GetBoolean(dr.GetOrdinal("Rec_CualquierDia"));

                    acys.Rec_Confirmacion = dr.GetBoolean(dr.GetOrdinal("Rec_Confirmacion"));
                    acys.Rec_Correo = dr.GetBoolean(dr.GetOrdinal("Rec_Correo"));
                    acys.Rec_Fax = dr.GetBoolean(dr.GetOrdinal("Rec_Fax"));
                    acys.Rec_Telefono = dr.GetBoolean(dr.GetOrdinal("Rec_Telefono"));
                    acys.Rec_Representante = dr.GetBoolean(dr.GetOrdinal("Rec_Representante"));
                    acys.Rec_Otro = dr.GetBoolean(dr.GetOrdinal("Rec_Otro"));

                    acys.Acs_RecWhatsApp = dr.GetBoolean(dr.GetOrdinal("Acs_RecWhatsApp"));

                    acys.Rec_OtroStr = dr.GetValue(dr.GetOrdinal("Rec_OtroStr")).ToString();

                    acys.Acs_PedidoEncargadoEnviar = dr.GetValue(dr.GetOrdinal("Acs_PedidoEncargadoEnviar")).ToString();
                    acys.Acs_PedidoPuesto = dr.GetValue(dr.GetOrdinal("Acs_PedidoPuesto")).ToString();
                    acys.Acs_PedidoTelefono = dr.GetValue(dr.GetOrdinal("Acs_PedidoTelefono")).ToString();
                    acys.Acs_PedidoTelefono2 = dr.GetValue(dr.GetOrdinal("Acs_PedidoTelefono2")).ToString();
                    acys.Acs_PedidoEmail = dr.GetValue(dr.GetOrdinal("Acs_PedidoEmail")).ToString();
                    acys.Acs_PedidoEmail2 = dr.GetValue(dr.GetOrdinal("Acs_PedidoEmail2")).ToString();
                    // Fin 


                    acys.Acs_RecDocReposicion = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocReposicion"));
                    acys.Acs_RecDocFolio = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocFolio"));
                    acys.Acs_RecDocOtro = dr.GetValue(dr.GetOrdinal("Acs_RecDocOtro")).ToString();

                    acys.Acs_VisitaOtro = dr.GetValue(dr.GetOrdinal("Acs_VisitaOtro")).ToString();

                    // SERVICIO DE VALOR 

                    acys.Acs_ReqServAsesoria = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServAsesoria"));

                    //acys.Acs_ReqServTecnicoRelleno = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServTecnicoRelleno"));
                    //acys.Acs_ReqServMantenimiento = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServMantenimiento"));

                    acys.Acs_Notas = dr.GetValue(dr.GetOrdinal("Acs_Notas")).ToString();

                    acys.Acs_ContactoRepVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVenta")));
                    acys.Acs_ContactoRepVentaTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaTel")).ToString();
                    acys.Acs_ContactoRepVentaEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaEmail")).ToString();

                    acys.Acs_ContactoRepServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServ")));
                    acys.Acs_ContactoRepServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServTel")).ToString();
                    acys.Acs_ContactoRepServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServEmail")).ToString();

                    acys.Acs_ContactoJefServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServ")));
                    acys.Acs_ContactoJefServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServTel")).ToString();
                    acys.Acs_ContactoJefServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServEmail")).ToString();

                    acys.Acs_ContactoAseServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServ")));
                    acys.Acs_ContactoAseServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServTel")).ToString();
                    acys.Acs_ContactoAseServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServEmail")).ToString();

                    acys.Acs_ContactoJefOper = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOper")));
                    acys.Acs_ContactoJefOperTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperTel")).ToString();
                    acys.Acs_ContactoJefOperEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperEmail")).ToString();

                    acys.Acs_ContactoCAlmRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRep")));
                    acys.Acs_ContactoCAlmRepTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepTel")).ToString();
                    acys.Acs_ContactoCAlmRepEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepEmail")).ToString();

                    acys.Acs_ContactoCServTec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTec")));
                    acys.Acs_ContactoCServTecTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecTel")).ToString();
                    acys.Acs_ContactoCServTecEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecEmail")).ToString();


                    acys.Acs_ContactoCCreCob = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCob")));
                    acys.Acs_ContactoCCreCobTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobTel")).ToString();
                    acys.Acs_ContactoCCreCobEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobEmail")).ToString();

                    acys.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();
                    acys.Acs_OrdenAbiertaConRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_OrdenAbiertaConRep")));

                    acys.Acs_version = dr.GetValue(dr.GetOrdinal("Acs_Version")).ToString();
                    acys.Acs_RikNombre = dr.GetValue(dr.GetOrdinal("Acs_RikNombre")).ToString();
                    acys.IdCte_DirEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Id_CteDirEntrega")));

                    acys.Acs_Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_Sucursal")));
                    acys.Acs_ParcialidadesSi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesSi")));
                    acys.Acs_ParcialidadesNo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesNo")));
                    acys.Acs_ConfirmacionPedidosSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosSI")));
                    acys.Acs_ConfirmacionPedidosnO = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosnO")));

                    acys.Acs_ParcialidadTipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadTipo")));

                    // Tab -> Recepcion de Pedidos
                    // Panel -> DOCUMENTOS PARA ENTREGA RECEPCION

                    // L M M J V S D CD
                    acys.Acs_RecRevLunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevLunes")));
                    acys.Acs_RecRevMartes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMartes")));
                    acys.Acs_RecRevMiercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMiercoles")));
                    acys.Acs_RecRevJueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevJueves")));
                    acys.Acs_RecRevViernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevViernes")));
                    acys.Acs_RecRevSabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevSabado")));
                    acys.Acs_RecRevDomingo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevDomingo")));
                    acys.Acs_RecRevCualquierDia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevCualquierDia")));

                    acys.Acs_TimePicker1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker1")));
                    acys.Acs_TimePicker2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker2")));
                    acys.Acs_TimePicker3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker3")));
                    acys.Acs_TimePicker4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker4")));

                    acys.Acs_RecPersonaRecibe = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPersonaRecibe")));
                    acys.Acs_RecPuesto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPuesto")));
                    acys.Acs_RecCitaMismoDia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaMismoDia")));
                    acys.Acs_RecCitaSinCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaSinCita")));
                    acys.Acs_RecCitaPrevia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaPrevia")));
                    acys.Acs_RecCitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaContacto")));
                    acys.Acs_RecCitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaTelefono")));
                    acys.Acs_RecCitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaDiasdeAnticipacion")));
                    // AREA RECIBO
                    acys.Acs_RecAreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPropia")));
                    acys.Acs_RecAreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPlaza")));
                    acys.Acs_RecAreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaCalle")));

                    acys.Acs_RecAreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaAvTransitada")));
                    acys.Acs_RecEstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCortesia")));
                    acys.Acs_RecEstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCosto")));

                    acys.Acs_RecEstMonto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_RecEstMonto")));


                    // 2 RECEPCION DE PEDIDOS 
                    // -- 2.2 DOCUMENTOS PARA ENTREGAR Y RECEPCION

                    acys.Acs_EspecsAdic1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_EspecsAdic1")));

                    acys.RP_EA = new CapAcys_EspecAdi();
                    // Facturas Key
                    acys.RP_EA.FactKeyChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEnt")));
                    acys.RP_EA.FactKeyCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEntCop")));
                    acys.RP_EA.FactKeyChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRec")));
                    acys.RP_EA.FactKeyCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRecCop")));
                    // Orden Compra 
                    acys.RP_EA.Ordcompchb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEnt")));
                    acys.RP_EA.OrdcompCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEntCop")));
                    acys.RP_EA.Ordcompchb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRec")));
                    acys.RP_EA.OrdcompCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRecCop")));
                    // Orden de Reposicion
                    acys.RP_EA.OrdRepChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEnt")));
                    acys.RP_EA.OrdRepCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEntCop")));
                    acys.RP_EA.OrdRepChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRec")));
                    acys.RP_EA.OrdRepCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRecCop")));
                    //Copia Pedidos
                    acys.RP_EA.CopPedChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEnt")));
                    acys.RP_EA.CopPedCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEntCop")));
                    acys.RP_EA.CopPedChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRec")));
                    acys.RP_EA.CopPedCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRecCop")));
                    //Remision
                    acys.RP_EA.RemChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEnt")));
                    acys.RP_EA.RemCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEntCop")));
                    acys.RP_EA.RemChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRec")));
                    acys.RP_EA.RemCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRecCop")));

                    //  /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                    //CERTIFICADO DE CALIDAD 1                                                                                     
                    //  /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                    acys.RP_EA.CerChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocCertificadoEnt")));
                    acys.RP_EA.CerCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocCertificadoEntCop")));
                    acys.RP_EA.CerChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocCertificadoRec")));
                    acys.RP_EA.CerCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocCertificadoRecCop")));

                    // OCT15-2019
                    acys.Acs_CorreoRecibirFacturas = dr.GetValue(dr.GetOrdinal("Acs_CorreoRecibirFacturas")).ToString();
                    acys.Acs_CorreoRecibirComplemento = dr.GetValue(dr.GetOrdinal("Acs_CorreoRecibirComplemento")).ToString();
                    acys.Acs_CorreoRecibir_NA = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CorreoRecibir_NA")));


                    /*
                    acys.ACS_RecDocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEnt")));
                    acys.ACS_RecDocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEntCop")));
                    acys.ACS_RecDocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRec")));
                    acys.ACS_RecDocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRecCop")));
                    */

                    // Orden Cliente -> 
                    // 5 CONDICIONES DE PAGO / 5.3 Pago de Facturas / Especificaciones Adicinales

                    acys.CondPago_EA = new CapAcys_EspecAdi();

                    acys.CondPago_EA.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Activo")));
                    acys.CondPago_EA.FactKeyChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_FacKeyChb1")));
                    acys.CondPago_EA.FactKeyCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_FacKeyCopias1")));
                    acys.CondPago_EA.FactKeyChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_FacKeyChb2")));
                    acys.CondPago_EA.FactKeyCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_FacKeyCopias2")));

                    acys.CondPago_EA.Ordcompchb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Ordcompchb1")));
                    acys.CondPago_EA.OrdcompCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdcompCopias1")));
                    acys.CondPago_EA.Ordcompchb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Ordcompchb2")));
                    acys.CondPago_EA.OrdcompCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdcompCopias2")));

                    acys.CondPago_EA.OrdRepChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_OrdRepChb1")));
                    acys.CondPago_EA.OrdRepCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdRepcopias1")));
                    acys.CondPago_EA.OrdRepChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_OrdRepChb2")));
                    acys.CondPago_EA.OrdRepCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdRepcopias2")));

                    acys.CondPago_EA.CopPedChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CopPedChb1")));
                    acys.CondPago_EA.CopPedCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CopPedCopias1")));
                    acys.CondPago_EA.CopPedChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CopPedChb2")));
                    acys.CondPago_EA.CopPedCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CopPedCopias2")));

                    //CERTIFICADO DE CALIDAD 2 
                    /*
                    acys.CondPago_EA.CerChb1= Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CerChb1")));
                    acys.CondPago_EA.CerCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CerCopias1")));
                    acys.CondPago_EA.CerChb2= Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CerChb2")));
                    acys.CondPago_EA.CerCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CerCopias2")));
                    */

                    //  /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
                    //CERTIFICADO DE CALIDAD 2                                                                                     
                    //  /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

                    acys.CondPago_EA.CerChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCertificadoEnt")));
                    acys.CondPago_EA.CerCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCertificadoEntCop")));
                    acys.CondPago_EA.CerChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCertificadoRec")));
                    acys.CondPago_EA.CerCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCertificadoRecCop")));

                    // Eléctronica

                    acys.RevFacEmail = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RevFacEmail")));
                    acys.RevFacEmailTexto = dr.GetValue(dr.GetOrdinal("RevFacEmailTexto")).ToString();
                    acys.RevFacEmailTexto2 = dr.GetValue(dr.GetOrdinal("RevFacEmailTexto2")).ToString();
                    acys.RevFacPortal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RevFacPortal")));

                    acys.RevFacPortalTexto = dr.GetValue(dr.GetOrdinal("RevFacPortalTexto")).ToString();
                    acys.RevFacHttp = dr.GetValue(dr.GetOrdinal("RevFacHttp")).ToString();
                    acys.RevFacUsuario = dr.GetValue(dr.GetOrdinal("RevFacUsuario")).ToString();
                    acys.RevFacContrasenia = dr.GetValue(dr.GetOrdinal("RevFacContrasenia")).ToString();

                    // ORDEN DE PAGO 
                    // 2.2 DOCUMENTOS ENTREGA RECEPCION

                    acys.Acs_DocEntregaFormaPago = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_DocEntregaFormaPago")));

                    /*
                    acys.ACS_RecDocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEnt")));
                    acys.ACS_RecDocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEntCop")));
                    acys.ACS_RecDocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRec")));
                    acys.ACS_RecDocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRecCop")));
                    acys.ACS_RecDocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEnt")));
                    acys.ACS_RecDocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEntCop")));
                    acys.ACS_RecDocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRec")));
                    acys.ACS_RecDocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRecCop")));
                    acys.ACS_RecDocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEnt")));
                    acys.ACS_RecDocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEntCop")));
                    acys.ACS_RecDocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRec")));
                    acys.ACS_RecDocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRecCop")));
                    acys.ACS_RecDocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEnt")));
                    acys.ACS_RecDocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEntCop")));
                    acys.ACS_RecDocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRec")));
                    acys.ACS_RecDocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRecCop")));
                    acys.ACS_RecDocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEnt")));
                    acys.ACS_RecDocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEntCop")));
                    acys.ACS_RecDocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaoRec")));
                    acys.ACS_RecDocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaRecCop")));
                    acys.ACS_RecCitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEnt")));
                    acys.ACS_RecCitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEntCop")));
                    acys.ACS_RecCitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRec")));
                    acys.ACS_RecCitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRecCop")));
                    */

                    acys.ACS_RecOtroRec = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RecOtro")));

                    // L M M J V S D CD
                    acys.ACS_chk62Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Lunes")));
                    acys.ACS_chk62Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Martes")));
                    acys.ACS_chk62Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Miercoles")));
                    acys.ACS_chk62Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Jueves")));
                    acys.ACS_chk62Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Viernes")));
                    acys.ACS_chk62Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Sabado")));
                    acys.ACS_chk62Domingo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Domingo")));
                    acys.ACS_chk62CualquierDia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CualquierDia")));

                    acys.ACS_RadTimePicker162 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker162")));
                    acys.ACS_RadTimePicker262 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker262")));
                    acys.ACS_RadTimePicker362 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker362")));
                    acys.ACS_RadTimePicker462 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker462")));
                    acys.ACS_txtRecPersonaRecibe62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPersonaRecibe62")));
                    acys.ACS_txtRecPuesto62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPuesto62")));
                    acys.ACS_Chk62Mismodia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Mismodia")));
                    acys.ACS_Chk62Sincita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Sincita")));
                    acys.ACS_Chk62Previa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Previa")));
                    acys.ACS_txt62CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaContacto")));
                    acys.ACS_txt62CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaTelefono")));
                    acys.ACS_txt62CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaDiasdeAnticipacion")));
                    acys.ACS_chk62AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPropia")));
                    acys.ACS_chk62AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPlaza")));
                    acys.ACS_chk62AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaCalle")));
                    acys.ACS_chk62AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaAvTransitada")));
                    acys.ACS_chk62EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCortesia")));
                    acys.ACS_chk62EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCosto")));
                    acys.ACS_txt62EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62EstMonto")));
                    acys.ACS_txt62ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteDireccion")));
                    acys.ACS_txt62ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteColonia")));
                    acys.ACS_txt62ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteMunicipio")));
                    acys.ACS_txt62ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteEstado")));
                    acys.ACS_txt62ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteCodPost")));

                    // Documentos para entrega y recepción

                    acys.ACS_chk62DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyEnt")));

                    // ESPECIFICACIONES ADICIONALES

                    // Factura Franquicia
                    acys.ACS_chk62DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaEnt")));
                    acys.ACS_txt62DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaEntCop")));
                    acys.ACS_chk62DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaRec")));
                    acys.ACS_txt62DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaRecCop")));
                    // Factura Key 
                    acys.ACS_chk62DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyEnt")));
                    acys.ACS_txt62DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyEntCop")));
                    acys.ACS_chk62DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyRec")));
                    acys.ACS_txt62DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyRecCop")));
                    // Orden Compra 
                    acys.ACS_chk62DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraEnt")));
                    acys.ACS_txt62DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraEntCop")));
                    acys.ACS_chk62DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraRec")));
                    acys.ACS_txt62DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraRecCop")));
                    // Orden Reposicion
                    acys.ACS_chk62DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposEnt")));
                    acys.ACS_txt62DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposEntCop")));
                    acys.ACS_chk62DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposRec")));
                    acys.ACS_txt62DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposRecCop")));
                    //Copia de Pedido
                    acys.ACS_chk62DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoEnt")));
                    acys.ACS_txt62DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoEntCop")));
                    acys.ACS_chk62DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoRec")));
                    acys.ACS_txt62DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoRecCop")));
                    // Remision
                    acys.ACS_chk62DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionEnt")));
                    acys.ACS_txt62DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionEntCop")));
                    acys.ACS_chk62DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionRec")));
                    acys.ACS_txt62DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionRecCop")));
                    // Folio
                    acys.ACS_chk62DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioEnt")));
                    acys.ACS_txt62DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioEntCop")));
                    acys.ACS_chk62DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioRec")));
                    acys.ACS_txt62DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioRecCop")));
                    // Contra Recibo
                    acys.ACS_chk62DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecEnt")));
                    acys.ACS_txt62DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecEntCop")));
                    acys.ACS_chk62DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecRec")));
                    acys.ACS_txt62DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecRecCop")));
                    // Entrada Alamcen
                    acys.ACS_chk62DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenEnt")));
                    acys.ACS_txt62DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenEntCop")));
                    acys.ACS_chk62DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenRec")));
                    acys.ACS_txt62DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenRecCop")));
                    // Soporte Servicio
                    acys.ACS_chk62DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioEnt")));
                    acys.ACS_txt62DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioEntCop")));
                    acys.ACS_chk62DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioRec")));
                    acys.ACS_txt62DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioRecCop")));
                    // Nombre y Firma de Recibo en Documento
                    acys.ACS_chk62DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaEnt")));
                    acys.ACS_txt62DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaEntCop")));
                    acys.ACS_chk62DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaoRec")));
                    acys.ACS_txt62DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaRecCop")));

                    // Cita 
                    acys.ACS_chk62CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaEnt")));
                    acys.ACS_txt62CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaEntCop")));
                    acys.ACS_chk62CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaRec")));
                    acys.ACS_txt62CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaRecCop")));

                    // Servicios de Valor 

                    acys.Acs_VisFrecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_VisFrecuencia")));


                    //
                    // 
                    // SERVICIO TECNICO
                    // 
                    //

                    acys.ServTecnico = new eCapAcys2_ServicioValor();

                    // L M M J V S D CD
                    acys.ServTecnico.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Aplicar")));
                    acys.ServTecnico.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Lunes")));
                    acys.ServTecnico.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Martes")));
                    acys.ServTecnico.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Jueves")));
                    acys.ServTecnico.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Jueves")));
                    acys.ServTecnico.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Viernes")));
                    acys.ServTecnico.Sabado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Sabado")));
                    acys.ServTecnico.Domingo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Domingo")));

                    acys.ServTecnico.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63CualquierDia")));

                    acys.ServTecnico.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker163")));
                    acys.ServTecnico.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker263")));
                    acys.ServTecnico.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker363")));
                    acys.ServTecnico.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker463")));

                    acys.ServTecnico.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk63Mismodia")));
                    acys.ServTecnico.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk63Previa")));

                    //RELLENO  //MANTENIMINEOT
                    //acys.ServTecnico.ServRelleno = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ST_Relleno")));                    
                    //acys.ServTecnico.ServPreventivo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ST_Mantenimiento")));
                    acys.ServTecnico.ServRelleno = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServTecnicoRelleno"));
                    acys.ServTecnico.ServPreventivo = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServMantenimiento"));

                    //OCT23-2019 RFH
                    acys.ServTecnico.QuienRecibe = dr.GetValue(dr.GetOrdinal("ST_QuienRecibe")).ToString();
                    acys.ServTecnico.FuncionQuienRecibe = dr.GetValue(dr.GetOrdinal("ST_FuncionQuienRecibe")).ToString();

                    acys.ServTecnico.Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ST_Frecuencia")));







                    //
                    // CAPACITACION 
                    //

                    acys.ServCapacitacion = new eCapAcys2_ServicioValor();

                    acys.ServCapacitacion.Aplicar = Convert.ToBoolean(dr["ACS_ServCap_Aplicar"].ToString());
                    acys.ServCapacitacion.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Tipo1")));
                    acys.ServCapacitacion.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Tipo2")));
                    acys.ServCapacitacion.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Lunes")));
                    acys.ServCapacitacion.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Martes")));
                    acys.ServCapacitacion.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Miercoles")));
                    acys.ServCapacitacion.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Jueves")));
                    acys.ServCapacitacion.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Viernes")));
                    acys.ServCapacitacion.Sabado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Sabado")));
                    acys.ServCapacitacion.Domingo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Domingo")));
                    acys.ServCapacitacion.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CualquierDia")));

                    acys.ServCapacitacion.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep1")));
                    acys.ServCapacitacion.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep2")));
                    acys.ServCapacitacion.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep3")));
                    acys.ServCapacitacion.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep4")));

                    acys.ServCapacitacion.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CitaPrevia_MismoDia")));
                    acys.ServCapacitacion.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CitaPrevia_Previa")));

                    acys.ServCapacitacion.Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Frecuencia")));












                    //
                    // AUDITORIA
                    //

                    acys.ServAuditoria = new eCapAcys2_ServicioValor();
                    acys.ServAuditoria.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Aplicar")));
                    acys.ServAuditoria.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Tipo1")));
                    acys.ServAuditoria.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Tipo2")));

                    acys.ServAuditoria.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Lunes")));
                    acys.ServAuditoria.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Martes")));
                    acys.ServAuditoria.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Miercoles")));
                    acys.ServAuditoria.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Jueves")));
                    acys.ServAuditoria.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Viernes")));
                    acys.ServAuditoria.Sabado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Sabado")));
                    acys.ServAuditoria.Domingo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Domingo")));
                    acys.ServAuditoria.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CualquierDia")));

                    acys.ServAuditoria.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep1")));
                    acys.ServAuditoria.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep2")));
                    acys.ServAuditoria.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep3")));
                    acys.ServAuditoria.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep4")));

                    acys.ServAuditoria.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CitaPrevia_MismoDia")));
                    acys.ServAuditoria.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CitaPrevia_Previa")));

                    acys.ServAuditoria.Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Frecuencia")));









                    //
                    // ASESORIA
                    //
                    acys.ServAsesoria = new eCapAcys2_ServicioValor();
                    acys.ServAsesoria.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Aplicar")));
                    acys.ServAsesoria.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Tipo1")));
                    acys.ServAsesoria.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Tipo2")));

                    acys.ServAsesoria.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Lunes")));
                    acys.ServAsesoria.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Martes")));
                    acys.ServAsesoria.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Miercoles")));
                    acys.ServAsesoria.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Jueves")));
                    acys.ServAsesoria.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Viernes")));
                    acys.ServAsesoria.Sabado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Sabado")));
                    acys.ServAsesoria.Domingo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Domingo")));
                    acys.ServAsesoria.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62CualquierDia")));

                    acys.ServAsesoria.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker162")));
                    acys.ServAsesoria.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker262")));
                    acys.ServAsesoria.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker362")));
                    acys.ServAsesoria.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker462")));

                    acys.ServAsesoria.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk62Mismodia")));
                    acys.ServAsesoria.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk62Previa")));

                    acys.ServAsesoria.Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAses_Frecuencia")));

                    acys.Acs_ComentariosRecomendaciones = dr.GetValue(dr.GetOrdinal("Acs_ComentariosRecomendaciones")).ToString();

                    /*
                    acys.ACS_txt63CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaContacto")));
                    acys.ACS_txt63CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaTelefono")));
                    acys.ACS_txt63CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaDiasdeAnticipacion")));
                    acys.ACS_chk63AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPropia")));
                    acys.ACS_chk63AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPlaza")));
                    acys.ACS_chk63AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaCalle")));
                    acys.ACS_chk63AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaAvTransitada")));
                    acys.ACS_chk63EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCortesia")));
                    acys.ACS_chk63EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCosto")));
                    acys.ACS_txt63EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63EstMonto")));
                    acys.ACS_txt63ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteDireccion")));
                    acys.ACS_txt63ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteColonia")));
                    acys.ACS_txt63ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteMunicipio")));
                    acys.ACS_txt63ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteEstado")));
                    acys.ACS_txt63ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteCodPost")));
                    acys.ACS_chk63DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaEnt")));
                         
                    acys.ACS_txt63DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaEntCop")));
                    acys.ACS_chk63DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaRec")));
                    acys.ACS_txt63DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaRecCop")));
                    acys.ACS_chk63DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyEnt")));
                    acys.ACS_txt63DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyEntCop")));
                    acys.ACS_chk63DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyRec")));
                    acys.ACS_txt63DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyRecCop")));
                    acys.ACS_chk63DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraEnt")));
                    acys.ACS_txt63DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraEntCop")));
                    acys.ACS_chk63DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraRec")));
                    acys.ACS_txt63DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraRecCop")));
                    acys.ACS_chk63DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposEnt")));
                    acys.ACS_txt63DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposEntCop")));
                    acys.ACS_chk63DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposRec")));
                    acys.ACS_txt63DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposRecCop")));
                    acys.ACS_chk63DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoEnt")));
                    acys.ACS_txt63DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoEntCop")));
                    acys.ACS_chk63DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoRec")));
                    acys.ACS_txt63DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoRecCop")));
                    acys.ACS_chk63DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionEnt")));
                    acys.ACS_txt63DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionEntCop")));
                    acys.ACS_chk63DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionRec")));
                    acys.ACS_txt63DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionRecCop")));
                    acys.ACS_chk63DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioEnt")));
                    acys.ACS_txt63DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioEntCop")));
                    acys.ACS_chk63DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioRec")));
                    acys.ACS_txt63DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioRecCop")));
                    acys.ACS_chk63DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecEnt")));
                    acys.ACS_txt63DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecEntCop")));
                    acys.ACS_chk63DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecRec")));
                    acys.ACS_txt63DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecRecCop")));
                    acys.ACS_chk63DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenEnt")));
                    acys.ACS_txt63DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenEntCop")));
                    acys.ACS_chk63DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenRec")));
                    acys.ACS_txt63DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenRecCop")));
                    acys.ACS_chk63DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioEnt")));
                    acys.ACS_txt63DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioEntCop")));
                    acys.ACS_chk63DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioRec")));
                    acys.ACS_txt63DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioRecCop")));
                    acys.ACS_chk63DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaEnt")));
                    acys.ACS_txt63DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaEntCop")));
                    acys.ACS_chk63DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaoRec")));
                    acys.ACS_txt63DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaRecCop")));
                    acys.ACS_chk63CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaEnt")));
                    acys.ACS_txt63CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaEntCop")));
                    acys.ACS_chk63CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaRec")));
                    acys.ACS_txt63CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaRecCop")));
                    */

                    //
                    // FIN 6.3 Servicio Tecnico 
                    //

                    acys.Acs_ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                    acys.Acs_ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                    acys.Documento_PermiteEditara = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Documento_PermiteEditara")));
                    acys.Desplegar_BtnAutorizar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Desplegar_BtnAutorizar")));

                    acys.Acs_RevisionFolio = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RevisionFolio")));
                    acys.Acs_RevisionEntAlmacen = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RevisionEntAlmacen")));
                    acys.Acs_RevisionOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RevisionOrdenCompra")));
                    acys.Acs_RevisionRepConsumo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RevisionRepConsumo")));
                    acys.Acs_RevisionCopiaFactura = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RevisionCopiaFactura")));
                    acys.Acs_RevisionOtroDoc = dr.GetValue(dr.GetOrdinal("Acs_RevisionOtroDoc")).ToString();

                    acys.Acs_PagoContraEntrega = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_PagoContraEntrega")));
                    acys.Acs_VisitaGestorCobranza = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_VisitaGestorCobranza")));


                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 10 Dic 2018 RFH
        public eAcys2 Consultar_Por_Folio(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, string Conexion)
        {

            eAcys2 acys = new eAcys2();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       Id_Acs,
                                       Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_ConsultarOrden_PorFolio", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();

                    acys.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));

                    acys.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    acys.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    acys.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));

                    acys.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    acys.Id_Corp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa")));

                    acys.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Acs_NomComercial")).ToString();
                    acys.Acs_Fecha = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    acys.Acs_FechaInicio = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    acys.Acs_FechaFin = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();

                    acys.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    acys.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    acys.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    acys.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();

                    // DIRECCION DEL CLIENTE
                    acys.ClienteDireccion = dr.GetValue(dr.GetOrdinal("ClienteDireccion")).ToString();
                    acys.ClienteColonia = dr.GetValue(dr.GetOrdinal("ClienteColonia")).ToString();
                    acys.ClienteMunicipio = dr.GetValue(dr.GetOrdinal("ClienteMunicipio")).ToString();
                    acys.ClienteEstado = dr.GetValue(dr.GetOrdinal("ClienteEstado")).ToString();
                    acys.ClienteRFC = dr.GetValue(dr.GetOrdinal("ClienteRFC")).ToString();
                    acys.ClienteCodPost = dr.GetValue(dr.GetOrdinal("ClienteCodPost")).ToString();
                    acys.CuentaCorporativa = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) > 1 && Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) != 10000) ? true : false;
                    acys.AddendaSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AddendaSi"))) > 0 ? true : false;

                    acys.DireccionEntrega = dr.GetValue(dr.GetOrdinal("DireccionEntrega")).ToString();
                    acys.ClienteColoniaE = dr.GetValue(dr.GetOrdinal("ClienteColoniaE")).ToString();
                    acys.ClienteMunicipioE = dr.GetValue(dr.GetOrdinal("ClienteMunicipioE")).ToString();
                    acys.ClienteCPE = dr.GetValue(dr.GetOrdinal("ClienteCPE")).ToString();
                    acys.ClienteEstadoE = dr.GetValue(dr.GetOrdinal("ClienteEstadoE")).ToString();

                    // OTROS APOLLOS
                    acys.Acs_Contacto2 = dr.GetValue(dr.GetOrdinal("Acs_Contacto2")).ToString();
                    acys.Acs_Telefono2 = dr.GetValue(dr.GetOrdinal("Acs_Telefono2")).ToString();
                    acys.Acs_email2 = dr.GetValue(dr.GetOrdinal("Acs_Email2")).ToString();

                    acys.Acs_Contacto3 = dr.GetValue(dr.GetOrdinal("Acs_Contacto3")).ToString();
                    acys.Acs_Telefono3 = dr.GetValue(dr.GetOrdinal("Acs_Telefono3")).ToString();
                    acys.Acs_email3 = dr.GetValue(dr.GetOrdinal("Acs_email3")).ToString();

                    acys.Acs_Contacto4 = dr.GetValue(dr.GetOrdinal("Acs_Contacto4")).ToString();
                    acys.Acs_Telefono4 = dr.GetValue(dr.GetOrdinal("Acs_Telefono4")).ToString();
                    acys.Acs_email4 = dr.GetValue(dr.GetOrdinal("Acs_email4")).ToString();

                    acys.Acs_Contacto5 = dr.GetValue(dr.GetOrdinal("Acs_Contacto5")).ToString();
                    acys.Acs_Telefono5 = dr.GetValue(dr.GetOrdinal("Acs_Telefono5")).ToString();
                    acys.Acs_Correo5 = dr.GetValue(dr.GetOrdinal("Acs_email5")).ToString();

                    acys.Acs_Contacto6 = dr.GetValue(dr.GetOrdinal("Acs_Contacto6")).ToString();
                    acys.Acs_Telefono6 = dr.GetValue(dr.GetOrdinal("Acs_Telefono6")).ToString();
                    acys.Acs_Correo6 = dr.GetValue(dr.GetOrdinal("Acs_email6")).ToString();

                    acys.Acs_Proveedor = dr.GetValue(dr.GetOrdinal("Acs_NumPrv")).ToString();

                    acys.Acs_RutaServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta1")));
                    acys.Acs_RutaEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta2")));

                    acys.Acs_ReqOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    acys.Acs_VigenciaIni = dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")).ToString();
                    acys.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));

                    // FROMA DE ENVIO DE PEDIDO 

                    acys.Acs_ReqConfirmacion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqConfirmacion")));

                    acys.Acs_RecCorreo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecCorreo")));
                    acys.Acs_RecFax = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFax")));
                    acys.Acs_RecTelefono = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecTelefono")));
                    acys.Acs_RecRepresentante = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRepresentante")));
                    acys.Acs_RecPedWhats = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecPedWhats")));
                    // Recolectado por Rik
                    acys.Acs_RecRIK = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRIK")));
                    acys.Acs_RecOtro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOtro")));
                    acys.Acs_RecOtroDesc = dr.GetValue(dr.GetOrdinal("Acs_RecOtroDesc")).ToString();
                    acys.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();

                    //VISITAS
                    acys.Vis_Frecuencia = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vis_Frecuencia")));
                    acys.Vis_Lunes = dr.GetBoolean(dr.GetOrdinal("Vis_Lunes"));
                    acys.Vis_Martes = dr.GetBoolean(dr.GetOrdinal("Vis_Martes"));
                    acys.Vis_Miercoles = dr.GetBoolean(dr.GetOrdinal("Vis_Miercoles"));
                    acys.Vis_Jueves = dr.GetBoolean(dr.GetOrdinal("Vis_Jueves"));
                    acys.Vis_Viernes = dr.GetBoolean(dr.GetOrdinal("Vis_Viernes"));
                    acys.Vis_Sabado = dr.GetBoolean(dr.GetOrdinal("Vis_Sabado"));
                    acys.Vis_HrAm1 = dr.GetValue(dr.GetOrdinal("Vis_HrAm1")).ToString();
                    acys.Vis_HrAm2 = dr.GetValue(dr.GetOrdinal("Vis_HrAm2")).ToString();
                    acys.Vis_HrPm1 = dr.GetValue(dr.GetOrdinal("Vis_HrPm1")).ToString();
                    acys.Vis_HrPm2 = dr.GetValue(dr.GetOrdinal("Vis_HrPm2")).ToString();

                    //Documentos Entega y RECEPCION DE PEDIDOS
                    acys.Rec_Semanas = dr.GetValue(dr.GetOrdinal("Rec_Semanas")).ToString();
                    // Dias de recepcion 
                    acys.Rec_Lunes = dr.GetBoolean(dr.GetOrdinal("Rec_Lunes"));
                    acys.Rec_Martes = dr.GetBoolean(dr.GetOrdinal("Rec_Martes"));
                    acys.Rec_Miercoles = dr.GetBoolean(dr.GetOrdinal("Rec_Miercoles"));
                    acys.Rec_Jueves = dr.GetBoolean(dr.GetOrdinal("Rec_Jueves"));
                    acys.Rec_Viernes = dr.GetBoolean(dr.GetOrdinal("Rec_Viernes"));
                    acys.Rec_Sabado = dr.GetBoolean(dr.GetOrdinal("Rec_Sabado"));

                    acys.Rec_Confirmacion = dr.GetBoolean(dr.GetOrdinal("Rec_Confirmacion"));
                    acys.Rec_Correo = dr.GetBoolean(dr.GetOrdinal("Rec_Correo"));
                    acys.Rec_Fax = dr.GetBoolean(dr.GetOrdinal("Rec_Fax"));
                    acys.Rec_Telefono = dr.GetBoolean(dr.GetOrdinal("Rec_Telefono"));
                    acys.Rec_Representante = dr.GetBoolean(dr.GetOrdinal("Rec_Representante"));
                    acys.Rec_Otro = dr.GetBoolean(dr.GetOrdinal("Rec_Otro"));

                    acys.Acs_RecWhatsApp = dr.GetBoolean(dr.GetOrdinal("Acs_RecWhatsApp"));

                    acys.Rec_OtroStr = dr.GetValue(dr.GetOrdinal("Rec_OtroStr")).ToString();

                    acys.Acs_PedidoEncargadoEnviar = dr.GetValue(dr.GetOrdinal("Acs_PedidoEncargadoEnviar")).ToString();
                    acys.Acs_PedidoPuesto = dr.GetValue(dr.GetOrdinal("Acs_PedidoPuesto")).ToString();
                    acys.Acs_PedidoTelefono = dr.GetValue(dr.GetOrdinal("Acs_PedidoTelefono")).ToString();
                    acys.Acs_PedidoEmail = dr.GetValue(dr.GetOrdinal("Acs_PedidoEmail")).ToString();
                    // Fin 







                    acys.Acs_RecDocReposicion = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocReposicion"));
                    acys.Acs_RecDocFolio = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocFolio"));
                    acys.Acs_RecDocOtro = dr.GetValue(dr.GetOrdinal("Acs_RecDocOtro")).ToString();

                    acys.Acs_VisitaOtro = dr.GetValue(dr.GetOrdinal("Acs_VisitaOtro")).ToString();

                    // SERVICIO DE VALOR 

                    acys.Acs_ReqServAsesoria = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServAsesoria"));
                    acys.Acs_ReqServTecnicoRelleno = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServTecnicoRelleno"));
                    acys.Acs_ReqServMantenimiento = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServMantenimiento"));

                    acys.Acs_Notas = dr.GetValue(dr.GetOrdinal("Acs_Notas")).ToString();


                    acys.Acs_ContactoRepVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVenta")));
                    acys.Acs_ContactoRepVentaTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaTel")).ToString();
                    acys.Acs_ContactoRepVentaEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaEmail")).ToString();

                    acys.Acs_ContactoRepServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServ")));
                    acys.Acs_ContactoRepServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServTel")).ToString();
                    acys.Acs_ContactoRepServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServEmail")).ToString();

                    acys.Acs_ContactoJefServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServ")));
                    acys.Acs_ContactoJefServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServTel")).ToString();
                    acys.Acs_ContactoJefServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServEmail")).ToString();

                    acys.Acs_ContactoAseServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServ")));
                    acys.Acs_ContactoAseServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServTel")).ToString();
                    acys.Acs_ContactoAseServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServEmail")).ToString();

                    acys.Acs_ContactoJefOper = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOper")));
                    acys.Acs_ContactoJefOperTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperTel")).ToString();
                    acys.Acs_ContactoJefOperEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperEmail")).ToString();

                    acys.Acs_ContactoCAlmRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRep")));
                    acys.Acs_ContactoCAlmRepTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepTel")).ToString();
                    acys.Acs_ContactoCAlmRepEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepEmail")).ToString();

                    acys.Acs_ContactoCServTec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTec")));
                    acys.Acs_ContactoCServTecTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecTel")).ToString();
                    acys.Acs_ContactoCServTecEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecEmail")).ToString();


                    acys.Acs_ContactoCCreCob = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCob")));
                    acys.Acs_ContactoCCreCobTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobTel")).ToString();
                    acys.Acs_ContactoCCreCobEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobEmail")).ToString();
                    acys.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();
                    acys.Acs_version = dr.GetValue(dr.GetOrdinal("Acs_Version")).ToString();
                    acys.Acs_RikNombre = dr.GetValue(dr.GetOrdinal("Acs_RikNombre")).ToString();
                    acys.IdCte_DirEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Id_CteDirEntrega")));

                    acys.Acs_Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_Sucursal")));
                    acys.Acs_ParcialidadesSi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesSi")));
                    acys.Acs_ParcialidadesNo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesNo")));
                    acys.Acs_ConfirmacionPedidosSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosSI")));
                    acys.Acs_ConfirmacionPedidosnO = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosnO")));



                    // Tab -> Recepcion de Pedidos
                    // Panel -> DOCUMENTOS PARA ENTREGA RECEPCION

                    acys.Acs_RecRevLunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevLunes")));
                    acys.Acs_RecRevMartes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMartes")));
                    acys.Acs_RecRevMiercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMiercoles")));
                    acys.Acs_RecRevJueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevJueves")));
                    acys.Acs_RecRevViernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevViernes")));
                    acys.Acs_RecRevSabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevSabado")));
                    acys.Acs_TimePicker1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker1")));
                    acys.Acs_TimePicker2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker2")));
                    acys.Acs_TimePicker3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker3")));
                    acys.Acs_TimePicker4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker4")));

                    acys.Acs_RecPersonaRecibe = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPersonaRecibe")));
                    acys.Acs_RecPuesto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPuesto")));
                    acys.Acs_RecCitaMismoDia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaMismoDia")));
                    acys.Acs_RecCitaSinCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaSinCita")));
                    acys.Acs_RecCitaPrevia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaPrevia")));
                    acys.Acs_RecCitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaContacto")));
                    acys.Acs_RecCitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaTelefono")));
                    acys.Acs_RecCitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaDiasdeAnticipacion")));
                    acys.Acs_RecAreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPropia")));
                    acys.Acs_RecAreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPlaza")));
                    acys.Acs_RecAreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaCalle")));
                    acys.Acs_RecAreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaAvTransitada")));
                    acys.Acs_RecEstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCortesia")));
                    acys.Acs_RecEstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCosto")));

                    acys.Acs_RecEstMonto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_RecEstMonto")));


                    // 2 RECEPCION DE PEDIDOS 
                    // -- 2.2 DOCUMENTOS PARA ENTREGAR Y RECEPCION

                    acys.Acs_EspecsAdic1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_EspecsAdic1")));

                    acys.RP_EA = new CapAcys_EspecAdi();
                    // Facturas Key
                    acys.RP_EA.FactKeyChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEnt")));
                    acys.RP_EA.FactKeyCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEntCop")));
                    acys.RP_EA.FactKeyChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRec")));
                    acys.RP_EA.FactKeyCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRecCop")));
                    // Orden Compra 
                    acys.RP_EA.Ordcompchb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEnt")));
                    acys.RP_EA.OrdcompCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEntCop")));
                    acys.RP_EA.Ordcompchb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRec")));
                    acys.RP_EA.OrdcompCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRecCop")));
                    // Orden de Reposicion
                    acys.RP_EA.OrdRepChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEnt")));
                    acys.RP_EA.OrdRepCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEntCop")));
                    acys.RP_EA.OrdRepChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRec")));
                    acys.RP_EA.OrdRepCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRecCop")));
                    //Copia Pedidos
                    acys.RP_EA.CopPedChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEnt")));
                    acys.RP_EA.CopPedCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEntCop")));
                    acys.RP_EA.CopPedChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRec")));
                    acys.RP_EA.CopPedCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRecCop")));
                    //Remision
                    acys.RP_EA.RemChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEnt")));
                    acys.RP_EA.RemCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEntCop")));
                    acys.RP_EA.RemChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRec")));
                    acys.RP_EA.RemCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRecCop")));

                    /*
                    acys.ACS_RecDocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEnt")));
                    acys.ACS_RecDocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEntCop")));
                    acys.ACS_RecDocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRec")));
                    acys.ACS_RecDocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRecCop")));
                    */

                    // Orden Cliente -> 
                    // 5 CONDICIONES DE PAGO / 5.3 Pago de Facturas / Especificaciones Adicinales

                    acys.CondPago_EA = new CapAcys_EspecAdi();

                    acys.CondPago_EA.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Activo")));
                    acys.CondPago_EA.FactKeyChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_FacKeyChb1")));
                    acys.CondPago_EA.FactKeyCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_FacKeyCopias1")));
                    acys.CondPago_EA.FactKeyChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_FacKeyChb2")));
                    acys.CondPago_EA.FactKeyCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_FacKeyCopias2")));

                    acys.CondPago_EA.Ordcompchb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Ordcompchb1")));
                    acys.CondPago_EA.OrdcompCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdcompCopias1")));
                    acys.CondPago_EA.Ordcompchb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_Ordcompchb2")));
                    acys.CondPago_EA.OrdcompCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdcompCopias2")));

                    acys.CondPago_EA.OrdRepChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_OrdRepChb1")));
                    acys.CondPago_EA.OrdRepCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdRepcopias1")));
                    acys.CondPago_EA.OrdRepChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_OrdRepChb2")));
                    acys.CondPago_EA.OrdRepCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_OrdRepcopias2")));

                    acys.CondPago_EA.CopPedChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CopPedChb1")));
                    acys.CondPago_EA.CopPedCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CopPedCopias1")));
                    acys.CondPago_EA.CopPedChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_CopPedChb2")));
                    acys.CondPago_EA.CopPedCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_CopPedCopias2")));

                    acys.CondPago_EA.RemChb1 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_RemChb1")));
                    acys.CondPago_EA.RemCopias1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_RemCopias1")));
                    acys.CondPago_EA.RemChb2 = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("EA1_RemChb2")));
                    acys.CondPago_EA.RemCopias2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EA1_RemCopias2")));

                    // ORDEN DE PAGO 
                    // 2.2 DOCUMENTOS ENTREGA RECEPCION

                    acys.Acs_DocEntregaFormaPago = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_DocEntregaFormaPago")));

                    /*
                    acys.ACS_RecDocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEnt")));
                    acys.ACS_RecDocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEntCop")));
                    acys.ACS_RecDocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRec")));
                    acys.ACS_RecDocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRecCop")));
                    acys.ACS_RecDocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEnt")));
                    acys.ACS_RecDocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEntCop")));
                    acys.ACS_RecDocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRec")));
                    acys.ACS_RecDocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRecCop")));
                    acys.ACS_RecDocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEnt")));
                    acys.ACS_RecDocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEntCop")));
                    acys.ACS_RecDocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRec")));
                    acys.ACS_RecDocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRecCop")));
                    acys.ACS_RecDocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEnt")));
                    acys.ACS_RecDocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEntCop")));
                    acys.ACS_RecDocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRec")));
                    acys.ACS_RecDocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRecCop")));
                    acys.ACS_RecDocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEnt")));
                    acys.ACS_RecDocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEntCop")));
                    acys.ACS_RecDocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaoRec")));
                    acys.ACS_RecDocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaRecCop")));
                    acys.ACS_RecCitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEnt")));
                    acys.ACS_RecCitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEntCop")));
                    acys.ACS_RecCitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRec")));
                    acys.ACS_RecCitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRecCop")));
                    */

                    acys.ACS_RecOtroRec = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RecOtro")));

                    acys.ACS_chk62Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Lunes")));
                    acys.ACS_chk62Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Martes")));
                    acys.ACS_chk62Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Miercoles")));
                    acys.ACS_chk62Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Jueves")));
                    acys.ACS_chk62Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Viernes")));
                    acys.ACS_chk62Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Sabado")));
                    acys.ACS_RadTimePicker162 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker162")));
                    acys.ACS_RadTimePicker262 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker262")));
                    acys.ACS_RadTimePicker362 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker362")));
                    acys.ACS_RadTimePicker462 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker462")));
                    acys.ACS_txtRecPersonaRecibe62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPersonaRecibe62")));
                    acys.ACS_txtRecPuesto62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPuesto62")));
                    acys.ACS_Chk62Mismodia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Mismodia")));
                    acys.ACS_Chk62Sincita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Sincita")));
                    acys.ACS_Chk62Previa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Previa")));
                    acys.ACS_txt62CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaContacto")));
                    acys.ACS_txt62CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaTelefono")));
                    acys.ACS_txt62CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaDiasdeAnticipacion")));
                    acys.ACS_chk62AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPropia")));
                    acys.ACS_chk62AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPlaza")));
                    acys.ACS_chk62AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaCalle")));
                    acys.ACS_chk62AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaAvTransitada")));
                    acys.ACS_chk62EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCortesia")));
                    acys.ACS_chk62EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCosto")));
                    acys.ACS_txt62EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62EstMonto")));
                    acys.ACS_txt62ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteDireccion")));
                    acys.ACS_txt62ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteColonia")));
                    acys.ACS_txt62ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteMunicipio")));
                    acys.ACS_txt62ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteEstado")));
                    acys.ACS_txt62ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteCodPost")));

                    // Documentos para entrega y recepción

                    acys.ACS_chk62DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyEnt")));

                    // ESPECIFICACIONES ADICIONALES

                    // Factura Franquicia
                    acys.ACS_chk62DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaEnt")));
                    acys.ACS_txt62DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaEntCop")));
                    acys.ACS_chk62DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaRec")));
                    acys.ACS_txt62DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaRecCop")));
                    // Factura Key 
                    acys.ACS_chk62DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyEnt")));
                    acys.ACS_txt62DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyEntCop")));
                    acys.ACS_chk62DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyRec")));
                    acys.ACS_txt62DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyRecCop")));
                    // Orden Compra 
                    acys.ACS_chk62DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraEnt")));
                    acys.ACS_txt62DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraEntCop")));
                    acys.ACS_chk62DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraRec")));
                    acys.ACS_txt62DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraRecCop")));
                    // Orden Reposicion
                    acys.ACS_chk62DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposEnt")));
                    acys.ACS_txt62DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposEntCop")));
                    acys.ACS_chk62DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposRec")));
                    acys.ACS_txt62DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposRecCop")));
                    //Copia de Pedido
                    acys.ACS_chk62DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoEnt")));
                    acys.ACS_txt62DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoEntCop")));
                    acys.ACS_chk62DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoRec")));
                    acys.ACS_txt62DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoRecCop")));
                    // Remision
                    acys.ACS_chk62DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionEnt")));
                    acys.ACS_txt62DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionEntCop")));
                    acys.ACS_chk62DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionRec")));
                    acys.ACS_txt62DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionRecCop")));
                    // Folio
                    acys.ACS_chk62DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioEnt")));
                    acys.ACS_txt62DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioEntCop")));
                    acys.ACS_chk62DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioRec")));
                    acys.ACS_txt62DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioRecCop")));
                    // Contra Recibo
                    acys.ACS_chk62DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecEnt")));
                    acys.ACS_txt62DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecEntCop")));
                    acys.ACS_chk62DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecRec")));
                    acys.ACS_txt62DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecRecCop")));
                    // Entrada Alamcen
                    acys.ACS_chk62DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenEnt")));
                    acys.ACS_txt62DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenEntCop")));
                    acys.ACS_chk62DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenRec")));
                    acys.ACS_txt62DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenRecCop")));
                    // Soporte Servicio
                    acys.ACS_chk62DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioEnt")));
                    acys.ACS_txt62DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioEntCop")));
                    acys.ACS_chk62DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioRec")));
                    acys.ACS_txt62DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioRecCop")));
                    // Nombre y Firma de Recibo en Documento
                    acys.ACS_chk62DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaEnt")));
                    acys.ACS_txt62DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaEntCop")));
                    acys.ACS_chk62DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaoRec")));
                    acys.ACS_txt62DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaRecCop")));
                    // Cita 
                    acys.ACS_chk62CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaEnt")));
                    acys.ACS_txt62CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaEntCop")));
                    acys.ACS_chk62CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaRec")));
                    acys.ACS_txt62CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaRecCop")));

                    // Servicios de Valor 

                    acys.Acs_VisFrecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_VisFrecuencia")));


                    //
                    // SERVICIO TECNICO  6.3 
                    //

                    acys.ServTecnico = new eCapAcys2_ServicioValor();

                    acys.ServTecnico.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Aplicar")));
                    acys.ServTecnico.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Lunes")));
                    acys.ServTecnico.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Martes")));
                    acys.ServTecnico.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Jueves")));
                    acys.ServTecnico.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Jueves")));
                    acys.ServTecnico.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63Viernes")));
                    acys.ServTecnico.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk63CualquierDia")));

                    acys.ServTecnico.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker163")));
                    acys.ServTecnico.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker263")));
                    acys.ServTecnico.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker363")));
                    acys.ServTecnico.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker463")));

                    acys.ServTecnico.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk63Mismodia")));
                    acys.ServTecnico.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk63Previa")));

                    //Servicio Tec. Relleno
                    acys.ServTecnico.ServRelleno = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqServTecnicoRelleno")));
                    //Servicio Tec. Mantenimiento 
                    acys.ServTecnico.ServPreventivo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqServMantenimiento")));

                    //
                    //  Servicio CAPACITACION 
                    //

                    acys.ServCapacitacion = new eCapAcys2_ServicioValor();

                    acys.ServCapacitacion.Aplicar = Convert.ToBoolean(dr["ACS_ServCap_Aplicar"].ToString());
                    acys.ServCapacitacion.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Tipo1")));
                    acys.ServCapacitacion.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Tipo2")));
                    acys.ServCapacitacion.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Lunes")));
                    acys.ServCapacitacion.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Martes")));
                    acys.ServCapacitacion.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Miercoles")));
                    acys.ServCapacitacion.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Jueves")));
                    acys.ServCapacitacion.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_Viernes")));
                    acys.ServCapacitacion.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CualquierDia")));

                    acys.ServCapacitacion.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep1")));
                    acys.ServCapacitacion.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep2")));
                    acys.ServCapacitacion.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep3")));
                    acys.ServCapacitacion.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServCap_HorariosRecep4")));

                    acys.ServCapacitacion.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CitaPrevia_MismoDia")));
                    acys.ServCapacitacion.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServCap_CitaPrevia_Previa")));

                    //
                    // 6.3 Servicio AUDITORIA
                    //

                    acys.ServAuditoria = new eCapAcys2_ServicioValor();
                    acys.ServAuditoria.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Aplicar")));
                    acys.ServAuditoria.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Tipo1")));
                    acys.ServAuditoria.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Tipo2")));
                    acys.ServAuditoria.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Lunes")));
                    acys.ServAuditoria.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Martes")));
                    acys.ServAuditoria.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Miercoles")));
                    acys.ServAuditoria.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Jueves")));
                    acys.ServAuditoria.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_Viernes")));
                    acys.ServAuditoria.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CualquierDia")));

                    acys.ServAuditoria.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep1")));
                    acys.ServAuditoria.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep2")));
                    acys.ServAuditoria.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep3")));
                    acys.ServAuditoria.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_ServAud_HorariosRecep4")));

                    acys.ServAuditoria.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CitaPrevia_MismoDia")));
                    acys.ServAuditoria.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_ServAud_CitaPrevia_Previa")));

                    //
                    // 6.3 Servicio ASESORIA
                    //
                    acys.ServAsesoria = new eCapAcys2_ServicioValor();
                    acys.ServAsesoria.Aplicar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Aplicar")));
                    acys.ServAsesoria.Tipo1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Tipo1")));
                    acys.ServAsesoria.Tipo2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Tipo2")));
                    acys.ServAsesoria.Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Lunes")));
                    acys.ServAsesoria.Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Martes")));
                    acys.ServAsesoria.Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Miercoles")));
                    acys.ServAsesoria.Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Jueves")));
                    acys.ServAsesoria.Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62Viernes")));
                    acys.ServAsesoria.CualquierDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_chk62CualquierDia")));

                    acys.ServAsesoria.HorariosRecep1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker162")));
                    acys.ServAsesoria.HorariosRecep2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker262")));
                    acys.ServAsesoria.HorariosRecep3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker362")));
                    acys.ServAsesoria.HorariosRecep4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker462")));

                    acys.ServAsesoria.CitaServ_MismoDia = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk62Mismodia")));
                    acys.ServAsesoria.CitaServ_Previa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ACS_Chk62Previa")));

                    /*
                    acys.ACS_txt63CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaContacto")));
                    acys.ACS_txt63CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaTelefono")));
                    acys.ACS_txt63CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaDiasdeAnticipacion")));
                    acys.ACS_chk63AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPropia")));
                    acys.ACS_chk63AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPlaza")));
                    acys.ACS_chk63AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaCalle")));
                    acys.ACS_chk63AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaAvTransitada")));
                    acys.ACS_chk63EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCortesia")));
                    acys.ACS_chk63EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCosto")));
                    acys.ACS_txt63EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63EstMonto")));
                    acys.ACS_txt63ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteDireccion")));
                    acys.ACS_txt63ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteColonia")));
                    acys.ACS_txt63ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteMunicipio")));
                    acys.ACS_txt63ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteEstado")));
                    acys.ACS_txt63ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteCodPost")));
                    acys.ACS_chk63DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaEnt")));
                         
                    acys.ACS_txt63DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaEntCop")));
                    acys.ACS_chk63DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaRec")));
                    acys.ACS_txt63DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaRecCop")));
                    acys.ACS_chk63DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyEnt")));
                    acys.ACS_txt63DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyEntCop")));
                    acys.ACS_chk63DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyRec")));
                    acys.ACS_txt63DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyRecCop")));
                    acys.ACS_chk63DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraEnt")));
                    acys.ACS_txt63DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraEntCop")));
                    acys.ACS_chk63DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraRec")));
                    acys.ACS_txt63DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraRecCop")));
                    acys.ACS_chk63DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposEnt")));
                    acys.ACS_txt63DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposEntCop")));
                    acys.ACS_chk63DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposRec")));
                    acys.ACS_txt63DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposRecCop")));
                    acys.ACS_chk63DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoEnt")));
                    acys.ACS_txt63DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoEntCop")));
                    acys.ACS_chk63DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoRec")));
                    acys.ACS_txt63DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoRecCop")));
                    acys.ACS_chk63DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionEnt")));
                    acys.ACS_txt63DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionEntCop")));
                    acys.ACS_chk63DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionRec")));
                    acys.ACS_txt63DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionRecCop")));
                    acys.ACS_chk63DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioEnt")));
                    acys.ACS_txt63DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioEntCop")));
                    acys.ACS_chk63DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioRec")));
                    acys.ACS_txt63DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioRecCop")));
                    acys.ACS_chk63DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecEnt")));
                    acys.ACS_txt63DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecEntCop")));
                    acys.ACS_chk63DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecRec")));
                    acys.ACS_txt63DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecRecCop")));
                    acys.ACS_chk63DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenEnt")));
                    acys.ACS_txt63DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenEntCop")));
                    acys.ACS_chk63DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenRec")));
                    acys.ACS_txt63DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenRecCop")));
                    acys.ACS_chk63DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioEnt")));
                    acys.ACS_txt63DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioEntCop")));
                    acys.ACS_chk63DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioRec")));
                    acys.ACS_txt63DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioRecCop")));
                    acys.ACS_chk63DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaEnt")));
                    acys.ACS_txt63DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaEntCop")));
                    acys.ACS_chk63DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaoRec")));
                    acys.ACS_txt63DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaRecCop")));
                    acys.ACS_chk63CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaEnt")));
                    acys.ACS_txt63CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaEntCop")));
                    acys.ACS_chk63CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaRec")));
                    acys.ACS_txt63CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaRecCop")));
                    */


                    //
                    // FIN 6.3 Servicio Tecnico 
                    //



                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                acys = null;
            }
            return acys;
        }

        // 13 Ago 2018 RFH ACTUALIZAR 
        //  * InsertUpdate * 
        public int InsertUpdate(ref eAcys2 A, ref string MensajeError, string Conexion)
        {
            int iResult = 0;
            int iAfecteRows = 0;

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", A.Id_Emp);
                Parametro("@Id_Cd", A.Id_Cd);
                Parametro("@Id_Acs", A.Id_Acs);
                Parametro("@Id_AcsVersion", A.Id_AcsVersion);
                Parametro("@Acs_Estatus", A.Acs_Estatus);
                Parametro("@Id_Ter", A.Id_Ter);
                Parametro("@Id_Rik", A.Id_Rik);
                Parametro("@Id_Cte", A.Id_Cte);
                Parametro("@Acs_NomComercial", A.Acs_NomComercial);
                Parametro("@Acs_Fecha", A.Acs_Fecha);
                Parametro("@Acs_FechaInicio", A.Acs_FechaInicio);
                Parametro("@Acs_VigenciaApartir", A.Acs_VigenciaApartir);
                // MAR30-2020 RFH
                Parametro("@Acs_Semana", A.Acs_Semana);
                Parametro("@Acs_VigenciaTermina", A.Acs_VigenciaTermina);
                Parametro("@Acs_Contacto", A.Acs_Contacto);
                Parametro("@Acs_Puesto", A.Acs_Puesto);
                Parametro("@Acs_Telefono", A.Acs_Telefono);
                Parametro("@Acs_email", A.Acs_email);
                Parametro("@Acs_Contacto2", A.Acs_Contacto2);
                Parametro("@Acs_Telefono2", A.Acs_Telefono2);
                Parametro("@Acs_Email2", A.Acs_email2);
                Parametro("@Acs_Contacto3", A.Acs_Contacto3);
                Parametro("@Acs_Telefono3", A.Acs_Telefono3);
                Parametro("@Acs_email3", A.Acs_email3);
                Parametro("@Acs_Contacto4", A.Acs_Contacto4);
                Parametro("@Acs_Telefono4", A.Acs_Telefono4);
                Parametro("@Acs_email4", A.Acs_email4);
                Parametro("@Acs_Contacto5", A.Acs_Contacto5);
                Parametro("@Acs_Telefono5", A.Acs_Telefono5);
                Parametro("@Acs_email5", A.Acs_email5);
                Parametro("@Acs_Contacto6", A.Acs_Contacto6);
                Parametro("@Acs_Telefono6", A.Acs_Telefono6);
                Parametro("@Acs_email6", A.Acs_email6);
                Parametro("@Acs_Procedencia", A.Acs_Procedencia);
                // 07Ene-2022 RFH
                Parametro("@Acs_FechaFin", A.Acs_FechaFin);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_InsertUpdate", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Opcion")));
                    iAfecteRows = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfectedRows")));
                    A.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                MensajeError = ex.ToString();
                iResult = -1;
            }

            return iResult;
        }

        // 29 Oct 2018 RFH Renovar
        public int Renovar(int Id_Emp, int Id_Cd, int Id_Acs, int Acs_version, string Conexion)
        {
            int iEstatus = 0;

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_version"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       Id_Acs,
                                       Acs_version
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2_Renovar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    iEstatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                iEstatus = -1;
            }

            return iEstatus;
        }

        public void ConsultaExisteAcisByClienteId(int Id_Cte, string Conexion, ref eAcys2 obj)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                       Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaExisteAcisByClienteId", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();

                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;

            }

        }




        public void ConsultaCordinadorCuentaByClienteId(int Id_Acys, string Conexion, ref CoordinadorCuentaCorreo coordinador)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion.Replace("SIANCENTRAL", "SIANCENTRAL_FACT"));

                string[] Parametros = {
                                          "@Cliente_Id"
                                      };
                object[] Valores = {
                                       Id_Acys

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGetCordinadoresCuentaByClienteId", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();


                    coordinador.Coordinador = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                    coordinador.CorreoCoordinador = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Correo")));


                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;

            }
        }


        //

    }
}