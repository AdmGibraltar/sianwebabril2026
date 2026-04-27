using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_dshIndicadoresACyS
    {
        #region ReporteLocal

        public void ConsultaDashboardACyS(int idCDEmp, int idCDCDI, int idCDUsConsulta, int iRik, string siRik, string sEstatus, string Conexion,
            ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
            ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleRIKs,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;
                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();
                DashboardACyS_Estatus renglon2 = new DashboardACyS_Estatus();
                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_DetalleRIKS renglon4 = new DashboardACyS_DetalleRIKS();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdRIK",
                                        "@sIdRIKs",
                                        "@sEstatus",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       idCDCDI,
                                       iRik,
                                       siRik,
                                       sEstatus,
                                       idCDUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresACyS", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    renglon = new DashboardACyS_Resumen();
                    renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                    renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                    renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                    ListResumen.Add(renglon);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Acs_EstatusTexto	ACyS
                    renglon2 = new DashboardACyS_Estatus();
                    renglon2.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon2.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon2.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                    renglon2.AcsEstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                    ListEstatus.Add(renglon2);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Rik_Nombre	ACyS
                    renglon3 = new DashboardACyS_RIKS();
                    renglon3.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon3.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                    renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    ListRIKs.Add(renglon3);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Id_Acs	Acs_EstatusTexto	Id_Cte	Cte_NomComercial	TipoCuenta	Acs_Fecha	Id_Rik	Rik_Nombre	Id_Ter	
                    //  Acs_FechaInicioDocumento	Acs_FechaFinDocumento	Acs_Vencido	Acs_ReqAutGerente	Acs_ReqAutJefeOp
                    renglon4 = new DashboardACyS_DetalleRIKS();
                    renglon4.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon4.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon4.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    renglon4.AcsEstatus = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                    renglon4.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    renglon4.NombreCliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    renglon4.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon4.FechaAcs = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    renglon4.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon4.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    renglon4.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    renglon4.FechaInicioAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    renglon4.FechaFinAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();
                    renglon4.AcsVencido = dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                    //  renglon4.ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                    //  renglon4.ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                    ListDetaleRIKs.Add(renglon4);
                }
                /// del archivo de Avance
                dr.NextResult();
                while (dr.Read())
                {
                    /// Id      Nombre
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListCon.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListSin.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon6 = new DashboardACyS_DetalleACyS();
                    renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                    renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                    renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                    renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                    renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                    renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                    renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                    renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();

                    ListDetACyS.Add(renglon6);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardACyS_v2(int idCDEmp, int idCDCDI, int idCDUsConsulta, int iRik, string siRik, string sEstatus, string strAnio, string strMes, string Conexion,
    ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
    ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleRIKs,
    ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
    ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;
                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();
                DashboardACyS_Estatus renglon2 = new DashboardACyS_Estatus();
                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_DetalleRIKS renglon4 = new DashboardACyS_DetalleRIKS();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Cd_Nombre",
                                        "@IdRIK",
                                        "@sIdRIKs",
                                        "@sEstatus",
                                        "@sAnio",
                                        "@sMes",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       idCDCDI,
                                       "",
                                       iRik,
                                       siRik,
                                       sEstatus,
                                       strAnio,
                                       strMes,
                                       idCDUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresACyS_v2", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    renglon = new DashboardACyS_Resumen();
                    renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                    renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                    renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                    ListResumen.Add(renglon);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Acs_EstatusTexto	ACyS
                    renglon2 = new DashboardACyS_Estatus();
                    renglon2.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon2.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon2.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                    renglon2.AcsEstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                    ListEstatus.Add(renglon2);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Rik_Nombre	ACyS
                    renglon3 = new DashboardACyS_RIKS();
                    renglon3.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon3.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                    renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    ListRIKs.Add(renglon3);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Id_Acs	Acs_EstatusTexto	Id_Cte	Cte_NomComercial	TipoCuenta	Acs_Fecha	Id_Rik	Rik_Nombre	Id_Ter	
                    //  Acs_FechaInicioDocumento	Acs_FechaFinDocumento	Acs_Vencido	Acs_ReqAutGerente	Acs_ReqAutJefeOp
                    renglon4 = new DashboardACyS_DetalleRIKS();
                    renglon4.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon4.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon4.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    renglon4.AcsEstatus = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                    renglon4.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    renglon4.NombreCliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    renglon4.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon4.FechaAcs = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                    renglon4.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon4.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    renglon4.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    renglon4.FechaInicioAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                    renglon4.FechaFinAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();
                    renglon4.AcsVencido = dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                    //  renglon4.ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                    //  renglon4.ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                    ListDetaleRIKs.Add(renglon4);
                }
                /// del archivo de Avance
                dr.NextResult();
                while (dr.Read())
                {
                    /// Id      Nombre
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListCon.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListSin.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon6 = new DashboardACyS_DetalleACyS();
                    renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                    renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                    renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                    renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                    renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                    renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                    renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                    renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();

                    ListDetACyS.Add(renglon6);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardPorRIKACyS_v2(int idCDEmp, int idCDCDI, int idCDUsConsulta, int iRik, string siRik, string sEstatus, string strAnio, string strMes, string Conexion,
            ref List<DashboardACyS_Resumen> ListResumen,
            ref List<DashboardACyS_RIKS> ListHojasRIKS,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;
                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdRIK",
                                        "@sIdRIKs",
                                        "@sEstatus",
                                        "@sAnio",
                                        "@sMes",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       idCDCDI,
                                       iRik,
                                       siRik,
                                       sEstatus,
                                       strAnio,
                                       strMes,
                                       idCDUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresRIKsACyS_v3", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Id_Rik	Rik_Nombre
                    renglon3 = new DashboardACyS_RIKS();
                    renglon3.IdCdi = idCDCDI;
                    renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    ListHojasRIKS.Add(renglon3);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    renglon = new DashboardACyS_Resumen();
                    renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                    renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                    renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                    ListResumen.Add(renglon);
                }
                /// del archivo de Avance
                dr.NextResult();
                while (dr.Read())
                {
                    /// Id      Nombre
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListCon.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListSin.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon6 = new DashboardACyS_DetalleACyS();
                    renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                    renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                    renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                    renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                    renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                    renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                    renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                    renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();

                    ListDetACyS.Add(renglon6);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardPorRIKACyS(int idCDEmp, int idCDCDI, int idCDUsConsulta, int iRik, string siRik, string sEstatus, string Conexion,
           ref List<DashboardACyS_Resumen> ListResumen,
           ref List<DashboardACyS_RIKS> ListHojasRIKS,
           ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
           ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;
                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@IdRIK",
                                        "@sIdRIKs",
                                        "@sEstatus",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       idCDCDI,
                                       iRik,
                                       siRik,
                                       sEstatus,
                                       idCDUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresRIKsACyS_v3", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Id_Rik	Rik_Nombre
                    renglon3 = new DashboardACyS_RIKS();
                    renglon3.IdCdi = idCDCDI;
                    renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    ListHojasRIKS.Add(renglon3);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    renglon = new DashboardACyS_Resumen();
                    renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                    renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                    renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                    ListResumen.Add(renglon);
                }
                /// del archivo de Avance
                dr.NextResult();
                while (dr.Read())
                {
                    /// Id      Nombre
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListCon.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon5 = new DashboardACyS_Clientes();
                    renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    ListSin.Add(renglon5);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    renglon6 = new DashboardACyS_DetalleACyS();
                    renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                    renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                    renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                    renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                    renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                    renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                    renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                    renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                    renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                    renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();

                    ListDetACyS.Add(renglon6);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaRenglones(int idCDEmp, int idCDCDI, int idCDUsConsulta, string sSP, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                SqlDataReader dr = null;
                Renglon reng = new Renglon();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id1",
                                        "@Id2",
                                        "@Id3",
                                        "@Id4",
                                        "@Id5",
                                      };
                object[] Valores = {
                                       1,
                                       idCDEmp,
                                       idCDUsConsulta,
                                       null,
                                       null,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sSP, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    reng = new Renglon();
                    reng.idRng = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    reng.sDescripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    reng.sValor = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    ListRenglones.Add(reng);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaRIKs(int iCDCDI, string sSP, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                SqlDataReader dr = null;
                Renglon reng = new Renglon();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@IdCD",
                                      };
                object[] Valores = {
                                       iCDCDI,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sSP, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    reng = new Renglon();
                    reng.idRng = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    reng.sDescripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    reng.sValor = "    " + dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    ListRenglones.Add(reng);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Nacional

        public void LlenaCDINac(int idCDI, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                SqlDataReader dr = null;
                Renglon reng = new Renglon();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@IdTipoCD",
                                      };
                object[] Valores = {
                                       idCDI,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshTodosCDIs", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Id_Cd	CDI
                    reng = new Renglon();
                    reng.idRng = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    reng.sDescripcion = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    reng.sValor = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                    ListRenglones.Add(reng);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaDashboardNacionalACyS(int idCDEmp, int idCDUsConsulta, string siCDI, string sEstatus, string Conexion,
            ref List<DashboardACyS_CDIACyS> ListCDIs,
            ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
            ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleCDIs,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;

                DashboardACyS_CDIACyS renglon0 = new DashboardACyS_CDIACyS();

                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();
                DashboardACyS_Estatus renglon2 = new DashboardACyS_Estatus();
                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_DetalleRIKS renglon4 = new DashboardACyS_DetalleRIKS();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@sIdCDIs",
                                        "@sEstatus",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       siCDI,
                                       sEstatus,
                                       idCDUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresNacionalesACyS", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// IdCDI	DsCDI
                    renglon0 = new DashboardACyS_CDIACyS();
                    renglon0.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCDI")));
                    renglon0.CDI = dr.GetValue(dr.GetOrdinal("DsCDI")).ToString();
                    ListCDIs.Add(renglon0);
                }

                foreach (DashboardACyS_CDIACyS CCDII in ListCDIs)
                {
                    // para cada CDI, obtiene sus sets de datos
                    dr.NextResult();
                    //  Resumen
                    while (dr.Read())
                    {
                        /// Orden Concepto sValor
                        renglon = new DashboardACyS_Resumen();
                        renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                        renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                        renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                        ListResumen.Add(renglon);
                    }
                    dr.NextResult();
                    //  Estatus
                    while (dr.Read())
                    {
                        /// Acs_EstatusTexto	ACyS
                        renglon2 = new DashboardACyS_Estatus();
                        renglon2.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon2.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon2.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                        renglon2.AcsEstatusTexto = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                        ListEstatus.Add(renglon2);
                    }
                    dr.NextResult();
                    //  RIKS
                    while (dr.Read())
                    {
                        /// Rik_Nombre	ACyS
                        renglon3 = new DashboardACyS_RIKS();
                        renglon3.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon3.NACySValor = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACyS")));
                        renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                        ListRIKs.Add(renglon3);
                    }
                    dr.NextResult();
                    // Detalle Grafica RIKS
                    while (dr.Read())
                    {
                        //  Id_Cdi CDI Id_Acs	Acs_EstatusTexto	Id_Cte	Cte_NomComercial	TipoCuenta	Acs_Fecha	Id_Rik	Rik_Nombre	Id_Ter	
                        //  Acs_FechaInicioDocumento	Acs_FechaFinDocumento	Acs_Vencido	Acs_ReqAutGerente	Acs_ReqAutJefeOp
                        renglon4 = new DashboardACyS_DetalleRIKS();
                        renglon4.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon4.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                        renglon4.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                        renglon4.AcsEstatus = dr.GetValue(dr.GetOrdinal("Acs_EstatusTexto")).ToString();
                        renglon4.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                        renglon4.NombreCliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                        renglon4.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                        renglon4.FechaAcs = dr.GetValue(dr.GetOrdinal("Acs_Fecha")).ToString();
                        renglon4.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon4.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                        renglon4.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                        renglon4.FechaInicioAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")).ToString();
                        renglon4.FechaFinAcs = dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")).ToString();
                        renglon4.AcsVencido = dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                        //  renglon4.ReqAutGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutGerente")));
                        //  renglon4.ReqAutJefeOp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqAutJefeOp")));

                        ListDetaleCDIs.Add(renglon4);
                    }
                    /// EXCELES
                    dr.NextResult();
                    //  Clientes Con ACyS
                    while (dr.Read())
                    {
                        /// Id      Nombre
                        renglon5 = new DashboardACyS_Clientes();
                        renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                        renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                        ListCon.Add(renglon5);
                    }
                    dr.NextResult();
                    //  Clientes Sin ACyS
                    while (dr.Read())
                    {
                        //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                        //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                        renglon5 = new DashboardACyS_Clientes();
                        renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                        renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                        ListSin.Add(renglon5);
                    }
                    dr.NextResult();
                    //  Detalle Matriz
                    while (dr.Read())
                    {
                        //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                        //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                        renglon6 = new DashboardACyS_DetalleACyS();
                        renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                        renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                        renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                        renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                        renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                        renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                        renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                        renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                        renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                        renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                        renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                        renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();
                        ListDetACyS.Add(renglon6);
                    }

                    // el ciclo se repite para Cada CDI
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardNacionalPorRIKACyS(int idCNEmp, int idCNUsConsulta, string siCDI, string sEstatus, string Conexion,
            ref List<DashboardACyS_CDIACyS> ListCDIs,
            ref List<DashboardACyS_Resumen> ListResumen,
            ref List<DashboardACyS_RIKS> ListHojasRIKS,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                SqlDataReader dr = null;
                int idCDCDI = 0;
                DashboardACyS_CDIACyS renglon0 = new DashboardACyS_CDIACyS();

                DashboardACyS_RIKS renglon3 = new DashboardACyS_RIKS();
                DashboardACyS_Resumen renglon = new DashboardACyS_Resumen();

                DashboardACyS_Clientes renglon5 = new DashboardACyS_Clientes();
                DashboardACyS_DetalleACyS renglon6 = new DashboardACyS_DetalleACyS();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@sIdCDIs",
                                        "@sEstatus",
                                        "@UsuarioConsulta_Id",
                                      };
                object[] Valores = {
                                       idCNEmp,
                                       siCDI,
                                       sEstatus,
                                       idCNUsConsulta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshIndicadoresNacionalesPorRIKACyS", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// IdCDI	DsCDI
                    renglon0 = new DashboardACyS_CDIACyS();
                    renglon0.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCDI")));
                    renglon0.CDI = dr.GetValue(dr.GetOrdinal("DsCDI")).ToString();
                    idCDCDI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCDI")));
                    ListCDIs.Add(renglon0);
                }

                foreach (DashboardACyS_CDIACyS CCDII in ListCDIs)
                {
                    dr.NextResult();
                    while (dr.Read())
                    {
                        /// Id_Rik	Rik_Nombre
                        renglon3 = new DashboardACyS_RIKS();
                        renglon3.IdCdi = idCDCDI;
                        renglon3.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon3.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                        ListHojasRIKS.Add(renglon3);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        /// Orden Concepto sValor
                        renglon = new DashboardACyS_Resumen();
                        renglon.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Orden")));
                        renglon.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                        renglon.sValor = dr.GetValue(dr.GetOrdinal("sValor")).ToString();
                        ListResumen.Add(renglon);
                    }
                    /// del archivo de Avance
                    dr.NextResult();
                    while (dr.Read())
                    {
                        /// Id      Nombre
                        renglon5 = new DashboardACyS_Clientes();
                        renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                        renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                        ListCon.Add(renglon5);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
                        //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                        renglon5 = new DashboardACyS_Clientes();
                        renglon5.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon5.IdRik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                        renglon5.iIdCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                        renglon5.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                        ListSin.Add(renglon5);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                        //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                        renglon6 = new DashboardACyS_DetalleACyS();
                        renglon6.IdCdi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cdi")));
                        renglon6.CDI = dr.GetValue(dr.GetOrdinal("CDI")).ToString();
                        renglon6.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                        renglon6.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                        renglon6.NumCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NumCliente")));
                        renglon6.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                        renglon6.Territorio = dr.GetValue(dr.GetOrdinal("Territorio")).ToString();
                        renglon6.Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RIK")));
                        renglon6.FechaACyS = dr.GetValue(dr.GetOrdinal("FechaACyS")).ToString();
                        renglon6.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                        renglon6.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                        renglon6.TipoCuenta = dr.GetValue(dr.GetOrdinal("TipoCuenta")).ToString();
                        renglon6.Vencido = dr.GetValue(dr.GetOrdinal("Vencido")).ToString();

                        ListDetACyS.Add(renglon6);
                    }

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RVD

        public void LlenaRenglones3(int idCDEmp, int idCDCDI, int idCDUsConsulta, string sSP, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                SqlDataReader dr = null;
                Renglon reng = new Renglon();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id1",
                                        "@Id2",
                                        "@Id3",
                                      };
                object[] Valores = {
                                       1,
                                       idCDEmp,
                                       idCDCDI,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sSP, ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// Orden Concepto sValor
                    reng = new Renglon();
                    reng.idRng = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    reng.sDescripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    reng.sValor = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    ListRenglones.Add(reng);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardRVD(int idCDEmp, int idCDCDI, int iAnio, int idCDUsConsulta, int iTipo, int iIndicador, string Conexion,
            ref List<RVDIndicadores> ListIndicador, ref List<RVDReporte> ListReporteSinAgrupar)
        {
            try
            {
                SqlDataReader dr = null;
                RVDIndicadores indicador = new RVDIndicadores();
                RVDReporte renglonRptSG = new RVDReporte();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Anio",
                                        "@UsuarioConsulta_Id",
                                        "@TipoReporte",
                                      };
                object[] Valores = {
                                       idCDEmp,
                                       idCDCDI,
                                       iAnio,
                                       idCDUsConsulta,
                                       iTipo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("dshReporteVentasDinamico", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    /// TotalAcum	TotalUnidades	TotalTrimestre	TotalMes
                    indicador = new RVDIndicadores();
                    indicador.IdCdi = idCDCDI;
                    indicador.TotalAcumulado = dr.GetValue(dr.GetOrdinal("TotalAcum")).ToString(); //  + "M";
                    indicador.TotalUnidades = dr.GetValue(dr.GetOrdinal("TotalUnidades")).ToString();
                    indicador.TotalTrimestre = dr.GetValue(dr.GetOrdinal("TotalTrimestre")).ToString(); //  + "M";
                    indicador.TotalDelMes = dr.GetValue(dr.GetOrdinal("TotalMes")).ToString(); //  + "M";

                    indicador.strEsteMes = dr.GetValue(dr.GetOrdinal("strEsteMes")).ToString();
                    indicador.strTrimestre = dr.GetValue(dr.GetOrdinal("sTrimestre")).ToString();
                    ListIndicador.Add(indicador);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    //  Id Nombre
                    ////    IdPrd	PdrDescrip	IdAgrupa	Agrupador
                    //  Enero U Enero Febrero U Febrero   Marzo U Marzo
                    //  Abril   U Abril Mayo U Mayo Junio   U Junio
                    //  Julio U Julio Agosto  U Agosto    Septiembre U Septiembre
                    //  Octubre U Octubre   Noviembre U Noviembre Diciembre   U Diciembre
                    //  Total U Total
                    renglonRptSG = new RVDReporte();
                    renglonRptSG.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    renglonRptSG.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();

                    renglonRptSG.IdPrd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("IdPrd")));
                    renglonRptSG.PdrDescrip = dr.GetValue(dr.GetOrdinal("PdrDescrip")).ToString();
                    renglonRptSG.IdAgrupa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdAgrupa")));
                    renglonRptSG.Agrupador = dr.GetValue(dr.GetOrdinal("Agrupador")).ToString();

                    renglonRptSG.Enero = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Enero")));
                    renglonRptSG.UnEnero = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnEnero")));
                    renglonRptSG.UtEnero = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtEnero")));
                    renglonRptSG.Febrero = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Febrero")));
                    renglonRptSG.UnFebrero = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnFebrero")));
                    renglonRptSG.UtFebrero = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtFebrero")));
                    renglonRptSG.Marzo = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Marzo")));
                    renglonRptSG.UnMarzo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnMarzo")));
                    renglonRptSG.UtMarzo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtMarzo")));

                    renglonRptSG.Abril = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abril")));
                    renglonRptSG.UnAbril = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnAbril")));
                    renglonRptSG.UtAbril = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtAbril")));
                    renglonRptSG.Mayo = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mayo")));
                    renglonRptSG.UnMayo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnMayo")));
                    renglonRptSG.UtMayo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtMayo")));
                    renglonRptSG.Junio = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Junio")));
                    renglonRptSG.UnJunio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnJunio")));
                    renglonRptSG.UtJunio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtJunio")));

                    renglonRptSG.Julio = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Julio")));
                    renglonRptSG.UnJulio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnJulio")));
                    renglonRptSG.UtJulio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtJulio")));
                    renglonRptSG.Agosto = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Agosto")));
                    renglonRptSG.UnAgosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnAgosto")));
                    renglonRptSG.UtAgosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtAgosto")));
                    renglonRptSG.Septiembre = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Septiembre")));
                    renglonRptSG.UnSeptiembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnSeptiembre")));
                    renglonRptSG.UtSeptiembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtSeptiembre")));

                    renglonRptSG.Octubre = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Octubre")));
                    renglonRptSG.UnOctubre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnOctubre")));
                    renglonRptSG.UtOctubre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtOctubre")));
                    renglonRptSG.Noviembre = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Noviembre")));
                    renglonRptSG.UnNoviembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnNoviembre")));
                    renglonRptSG.UtNoviembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtNoviembre")));
                    renglonRptSG.Diciembre = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Diciembre")));
                    renglonRptSG.UnDiciembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnDiciembre")));
                    renglonRptSG.UtDiciembre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtDiciembre")));

                    renglonRptSG.Total = (float)Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Total")));
                    renglonRptSG.UnTotal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UnTotal")));
                    renglonRptSG.UtTotal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UtTotal")));

                    ListReporteSinAgrupar.Add(renglonRptSG);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRVDSemanal(int idCDEmp, int idCDCDI, string Conexion, int iAnio, int iMov80,
            ref List<RVDSemanal> ListVentasSemana)
        {
            RVDSemanal semanal = new RVDSemanal();
            try
            {
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Fecha"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@Mov_80"
                            };
                object[] Valores ={
                                   idCDEmp
                                  ,idCDCDI
                                  ,iAnio
                                  ,null
                                  ,null
                                  ,null
                                  ,0
                                  ,0
                                  ,0
                                  ,iMov80
                                  };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentasSemanal2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    semanal = new RVDSemanal();
                    semanal.Id_TerSrv = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TerServ")));
                    semanal.Nom_TerSrv = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();

                    semanal.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    semanal.Nom_Ter = dr.GetValue(dr.GetOrdinal("Nom_Ter")).ToString();
                    semanal.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    semanal.Nom_Cte = dr.GetValue(dr.GetOrdinal("Nom_Cte")).ToString();
                    semanal.Id_prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    semanal.Nom_Prd = dr.GetValue(dr.GetOrdinal("Nom_Prd")).ToString();
                    semanal.Unidades = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Unidades")));
                    semanal.Importe = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Importe")));

                    semanal.Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sem")));
                    semanal.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    semanal.Mes = dr.GetValue(dr.GetOrdinal("Mes")).ToString();
                    ListVentasSemana.Add(semanal);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}