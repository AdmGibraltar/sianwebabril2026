using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Diagnostics;

namespace SIANWEB.WebService
{
    public class DashboardKpiDiarioController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage prueba(string strMensaje)
        {
            Debug.WriteLine("Entro post" + strMensaje);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, strMensaje);
        }

        [HttpGet]
        public HttpResponseMessage ConsultarKpiDiario()
        {
            object objData = new object();


            object objHeader = new object();
            object objKpiGeneral = new object();
            object objKpiRik = new object();
            object objKpiFactura = new object();
            object objTtlCartera = new object();
            List<object> lstRikDatos = new List<object>();
            List<object> lstKpiRik = new List<object>();

            CN_DashboardKpi cn_DashboardKpi = new CN_DashboardKpi();
            entDashboardKpiDiario entDsKpi = new entDashboardKpiDiario();
            List<entDashboardKpiDiarioRik> lstDsKpiRik = new List<entDashboardKpiDiarioRik>();
            string strMsjResultado = string.Empty;

            try
            {

                cn_DashboardKpi.ConsultarKpiDiario(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, ref entDsKpi, ref lstDsKpiRik);

                decimal dcmUbData1 = 0;
                decimal dcmUbData2 = 0;
                decimal dcmCumplData1 = 0;
                decimal dcmCumplData2 = 0;

                dcmUbData1 = Math.Round(((entDsKpi.utilidadDiaPorcentaje * 100) / entDsKpi.utilidadPPTOPorcentaje), 2);

                if (dcmUbData1 > 100)
                    dcmUbData2 = 0;
                else
                    dcmUbData2 = Math.Round((100 - dcmUbData1), 2);

                dcmCumplData1 = Math.Round(((entDsKpi.utilidadDiaMoneda * 100) / entDsKpi.utilidadPptoMoneda), 2);

                if (dcmCumplData1 >= 100)
                    dcmCumplData2 = 0;
                else
                    dcmCumplData2 = Math.Round((100 - dcmCumplData1), 2);


                objHeader = new
                {
                    NombreCD = entDsKpi.NombreCD,
                    utilidadPPTOPorcentaje = entDsKpi.utilidadPPTOPorcentaje.ToString() + " %",
                    utilidadDiaPorcentaje = entDsKpi.utilidadDiaPorcentaje.ToString() + " %",
                    utilidadPptoMoneda = entDsKpi.utilidadPptoMoneda.ToString("C"),
                    utilidadDiaMoneda = entDsKpi.utilidadDiaMoneda.ToString("C"),
                    fechaDashBoard = entDsKpi.fechaDashBoard.ToString("dd/MM/yyyy"),
                    gCumplimientoUBPorcenjeData1 = dcmUbData1,
                    gCumplimientoUBPorcenjeData2 = dcmUbData2,
                    gCumplimientoUBPorcenjeLabel1 = dcmUbData1.ToString("0.0") + " %",
                    gCumplimientoUBPorcenjeLabel2 = "",
                    gCumplimientoUBPesosData1 = dcmCumplData1,
                    gCumplimientoUBPesosData2 = dcmCumplData2,
                    gCumplimientoUBPesosLabel1 = dcmCumplData1.ToString("0.0") + " %",
                    gCumplimientoUBPesosLabel2 = "",

                };

                decimal dcmCumplimiento = 0;
                decimal dcmVigenteData1 = 0;
                decimal dcmVigenteData2 = 0;
                decimal dcmVencidaData1 = 0;
                decimal dcmVencidaData2 = 0;



                dcmCumplimiento = (entDsKpi.cumplimientoGnrl * 100) / entDsKpi.presupuestoGnrl;

                dcmVigenteData1 = Math.Round(((entDsKpi.remisionesVigentes * 100) / entDsKpi.presupuestoGnrl), 2);
                dcmVigenteData2 = Math.Round((100 - dcmVigenteData1), 2);

                dcmVencidaData1 = Math.Round(((entDsKpi.remisionesVencidas * 100) / entDsKpi.presupuestoGnrl), 2);
                dcmVencidaData2 = Math.Round((100 - dcmVencidaData1), 2);


                objKpiGeneral = new
                {
                    presupuestoGnrl = entDsKpi.presupuestoGnrl.ToString("C"),
                    presupuestoGnrlRestante = entDsKpi.presupuestoGnrlRestante.ToString("C"),
                    cumplimientoGnrl = entDsKpi.cumplimientoGnrl.ToString("C"),
                    cumplimientoGnrlPorcentaje = dcmCumplimiento.ToString("0.00") + " %",
                    remisionesPxFVigentes = entDsKpi.remisionesVigentes.ToString("C"),
                    remisionesPxFVencidas = entDsKpi.remisionesVencidas.ToString("C"),
                    remisionesPxFGnrl = entDsKpi.remisionesPxFGnrl.ToString("C"),
                    gVigentesData1 = dcmVigenteData1,
                    gVigentesData2 = dcmVigenteData2,
                    gVigentesLabel1 = dcmVigenteData1.ToString("0.00") + "% \n% vs Venta",
                    gVigentesLabel2 = "",
                    gVencidasData1 = dcmVencidaData1,
                    gVencidasData2 = dcmVencidaData2,
                    gVencidasLabel1 = dcmVencidaData1.ToString("0.00") + "% \n% vs Venta",
                    gVencidasLabel2 = "",

                };

                objKpiFactura = new
                {
                    ImporteBaja = entDsKpi.ImporteBaja.ToString("C"),
                    ImporteRefacturado = entDsKpi.ImporteRefacturado.ToString("C"),
                    ImporteFacturas = entDsKpi.ImporteFacturas.ToString("C"),
                    ImporteGeneral = entDsKpi.ImporteGeneral.ToString("C"),
                    entDsKpi.NumBaja,
                    entDsKpi.NumRefacturado,
                    entDsKpi.NumFacturas,
                    gFacturaNumData1 = entDsKpi.NumBaja,
                    gFacturaNumData2 = entDsKpi.NumRefacturado,
                    gFacturaNumData3 = entDsKpi.NumFacturas,
                    gFacturaImporteData1 = entDsKpi.ImporteBaja,
                    gFacturaImporteData2 = entDsKpi.ImporteRefacturado,
                    gFacturaImporteData3 = entDsKpi.ImporteFacturas,
                    gFacturaNumLabel1 = "Baja",
                    gFacturaNumLabel2 = "Refacturado",
                    gFacturaNumLabel3 = "Facturas",
                    gFacturaImporteLabel1 = "Baja",
                    gFacturaImporteLabel2 = "Refacturado",
                    gFacturaImporteLabel3 = "Facturas",
                };

                decimal dcmTiempoData1 = Math.Round((entDsKpi.ttlCarteraTiempo * 100) / entDsKpi.ttlCarteraCobranza);
                decimal dcmTiempoData2 = Math.Round((100 - dcmTiempoData1), 2);

                decimal dcmMenos30diasData1 = Math.Round((entDsKpi.ttlCarteraMenos30dias * 100) / entDsKpi.ttlCarteraCobranza);
                decimal dcmMenos30diasData2 = Math.Round((100 - dcmMenos30diasData1), 2);

                decimal dcmMas30diasData1 = Math.Round((entDsKpi.ttlCarteraMas30dias * 100) / entDsKpi.ttlCarteraCobranza);
                decimal dcmMas30diasData2 = Math.Round((100 - dcmMas30diasData1), 2);

                objTtlCartera = new
                {
                    ttlCarteraCobranza = entDsKpi.ttlCarteraCobranza.ToString("C"),
                    ttlCarteraTiempo = entDsKpi.ttlCarteraTiempo.ToString("C"),
                    ttlCarteraMenos30dias = entDsKpi.ttlCarteraMenos30dias.ToString("C"),
                    ttlCarteraMas30dias = entDsKpi.ttlCarteraMas30dias.ToString("C"),
                    gTiempoData1 = dcmTiempoData1,
                    gTiempoData2 = dcmTiempoData2,
                    gTiempoLabel1 = dcmTiempoData1.ToString("0.00") + " %",
                    gTiempoLabel2 = "",
                    gMenos30Data1 = dcmMenos30diasData1,
                    gMenos30Data2 = dcmMenos30diasData2,
                    gMenos30Label1 = dcmMenos30diasData1.ToString("0.00") + " %",
                    gMenos30Label2 = "",
                    gMas30Data1 = dcmMas30diasData1,
                    gMas30Data2 = dcmMas30diasData2,
                    gMas30Label1 = dcmMas30diasData1.ToString("0.00") + " %",
                    gMas30Label2 = "",
                };

                decimal dcmKpiRikDato1;
                decimal dcmKpiRikDato2;
                decimal dcmRemisionesRikDato1;
                decimal dcmRemisionesRikDato2;


                foreach (var itemDsKpiRik in lstDsKpiRik)
                {
                    if (itemDsKpiRik.cumplimientoRik == 0 || itemDsKpiRik.presupuestoRik == 0)
                    {
                        dcmKpiRikDato1 = 0;
                    }
                    else
                    {

                        dcmKpiRikDato1 = Math.Round((itemDsKpiRik.cumplimientoRik * 100) / itemDsKpiRik.presupuestoRik, 2);
                    }
                    dcmKpiRikDato2 = Math.Round((100 - dcmKpiRikDato1), 2);

                    dcmRemisionesRikDato1 = itemDsKpiRik.UBPprcemtaje;
                    dcmRemisionesRikDato2 = Math.Round((100 - dcmRemisionesRikDato1), 2);

                    lstRikDatos.Add(new
                    {
                        nombreRik = itemDsKpiRik.NombreRik,
                        cumplimientoRik = itemDsKpiRik.cumplimientoRik.ToString("C")
                    });

                    objKpiRik = new
                    {
                        NombreRik = itemDsKpiRik.NombreRik,
                        cumplimientoRik = itemDsKpiRik.cumplimientoRik.ToString("C"),
                        presupuestoRik = itemDsKpiRik.presupuestoRik.ToString("C"),
                        crecimientoRik = itemDsKpiRik.crecimientoRik.ToString("C"),
                        cteActivoRik = itemDsKpiRik.cteActivoRik,
                        carteraTiempoRik = itemDsKpiRik.carteraTiempoRik.ToString("C"),
                        carteraVencidaRik = itemDsKpiRik.carteraVencidaRik.ToString("C"),
                        remisionesTiempoRik = itemDsKpiRik.remisionesTiempoRik.ToString("C"),
                        remisionesVencidaRik = itemDsKpiRik.remisionesVencidaRik.ToString("C"),
                        gKpiRikDato1 = dcmKpiRikDato1,
                        gKpiRikDato2 = dcmKpiRikDato2,
                        gKpiRikLabel1 = dcmKpiRikDato1.ToString("0.00") + " %",
                        gKpiRikLabel2 = "",
                        gRemisionesRikDato1 = dcmRemisionesRikDato1,
                        gRemisionesRikDato2 = dcmRemisionesRikDato2,
                        gRemisionesRikLabel1 = dcmRemisionesRikDato1.ToString("0.00") + " %",
                        gRemisionesRikLabel2 = "",
                    };

                    lstKpiRik.Add(objKpiRik);
                }

                objData = new
                {
                    objHeader,
                    objKpiGeneral,
                    objTtlCartera,
                    lstKpiRik,
                    objKpiFactura
                };

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, objData);
            }
            catch (Exception ex)
            {
                strMsjResultado = ex.ToString();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, strMsjResultado);
            }


        }



        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
        //
    }


}