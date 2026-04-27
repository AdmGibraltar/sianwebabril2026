using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaEntidad;
using CapaDatos;
using DevExpress.Web.Internal;

namespace CapaNegocios
{
    public class CN_dshIndicadoresACyS
    {

        #region Local

        public void ConsultaDashboardACyS(int idCNEmp, int idCNCDI, int idCNUsConsulta, int iRik, string siRik, string sEstatus, string Conexion,
            ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
            ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleRIKs,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardACyS(idCNEmp, idCNCDI, idCNUsConsulta, iRik, siRik, sEstatus, Conexion,
                    ref ListResumen, ref ListEstatus, ref ListRIKs, ref ListDetaleRIKs,
                    ref ListCon, ref ListSin, ref ListDetACyS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaDashboardACyS_v2(int idCNEmp, int idCNCDI, int idCNUsConsulta, int iRik, string siRik, string sEstatus, string strAnio, string strMes, string Conexion,
            ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
            ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleRIKs,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardACyS_v2(idCNEmp, idCNCDI, idCNUsConsulta, iRik, siRik, sEstatus, strAnio, strMes, Conexion,
                    ref ListResumen, ref ListEstatus, ref ListRIKs, ref ListDetaleRIKs,
                    ref ListCon, ref ListSin, ref ListDetACyS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaRenglones(int idCNEmp, int idCNCDI, int idCNUsConsulta, string sSP, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.LlenaRenglones(idCNEmp, idCNCDI, idCNUsConsulta, sSP, Conexion, ref ListRenglones);

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
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.LlenaRIKs(iCDCDI, sSP, Conexion, ref ListRenglones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardPorRIKACyS(int idCNEmp, int idCNCDI, int idCNUsConsulta, int iRik, string siRik, string sEstatus, string Conexion,
            ref List<DashboardACyS_Resumen> ListResumen,
            ref List<DashboardACyS_RIKS> ListHojasRIKS,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardPorRIKACyS(idCNEmp, idCNCDI, idCNUsConsulta, iRik, siRik, sEstatus, Conexion,
                    ref ListResumen, ref ListHojasRIKS,
                    ref ListCon, ref ListSin, ref ListDetACyS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardPorRIKACyS_v2(int idCNEmp, int idCNCDI, int idCNUsConsulta, int iRik, string siRik, string sEstatus, string strAnio, string strMes, string Conexion,
           ref List<DashboardACyS_Resumen> ListResumen,
           ref List<DashboardACyS_RIKS> ListHojasRIKS,
           ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
           ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardPorRIKACyS_v2(idCNEmp, idCNCDI, idCNUsConsulta, iRik, siRik, sEstatus, strAnio, strMes, Conexion,
                    ref ListResumen, ref ListHojasRIKS,
                    ref ListCon, ref ListSin, ref ListDetACyS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Nacional

        public void LlenaCDINac(int iCDCDI, string Conexion,
           ref List<Renglon> ListRenglones)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.LlenaCDINac(iCDCDI, Conexion, ref ListRenglones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDashboardNacionalACyS(int idCNEmp, int idCNUsConsulta, string siCDI, string sEstatus, string Conexion,
            ref List<DashboardACyS_CDIACyS> ListCDIs,
            ref List<DashboardACyS_Resumen> ListResumen, ref List<DashboardACyS_Estatus> ListEstatus,
            ref List<DashboardACyS_RIKS> ListRIKs, ref List<DashboardACyS_DetalleRIKS> ListDetaleCDIs,
            ref List<DashboardACyS_Clientes> ListCon, ref List<DashboardACyS_Clientes> ListSin,
            ref List<DashboardACyS_DetalleACyS> ListDetACyS)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardNacionalACyS(idCNEmp, idCNUsConsulta, siCDI, sEstatus, Conexion,
                    ref ListCDIs,
                    ref ListResumen, ref ListEstatus,
                    ref ListRIKs, ref ListDetaleCDIs,
                    ref ListCon, ref ListSin, ref ListDetACyS);

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
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardNacionalPorRIKACyS(idCNEmp, idCNUsConsulta, siCDI, sEstatus, Conexion,
                    ref ListCDIs,
                    ref ListResumen, ref ListHojasRIKS,
                    ref ListCon, ref ListSin, ref ListDetACyS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RVD

        public void LlenaRenglones3(int idCNEmp, int idCNCDI, int idCNUsConsulta, string sSP, string Conexion, ref List<Renglon> ListRenglones)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.LlenaRenglones3(idCNEmp, idCNCDI, idCNUsConsulta, sSP, Conexion, ref ListRenglones);

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
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaDashboardRVD(idCDEmp, idCDCDI, iAnio, idCDUsConsulta, iTipo, iIndicador, Conexion,
                    ref ListIndicador, ref ListReporteSinAgrupar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region RVD

        public void ConsultaRVDSemanal(int idCDEmp, int idCDCDI, string Conexion, int iAnio, int iMov80,
            ref List<RVDSemanal> ListVentasSemana)
        {
            try
            {
                CD_dshIndicadoresACyS claseCapaDatos = new CD_dshIndicadoresACyS();
                claseCapaDatos.ConsultaRVDSemanal(idCDEmp, idCDCDI, Conexion, iAnio, iMov80,
                    ref ListVentasSemana);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}