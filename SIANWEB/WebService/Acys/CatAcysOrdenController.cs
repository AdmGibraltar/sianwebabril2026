using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

// 7 Nov 2018 RFH Creacion 
// 9 Abr 2018 RFH Actualizado

namespace SIANWEB.WebService
{
    public class CatAcysOrdenController : ApiController
    {
        [HttpGet]
        public List<eAcys2> Get(
            int PageNumber, int PageSize,
            int AplicaFecha, string FechaIni, string FechaFin,
            int AplicaFolio, int FolioIni, int FolioFin,
            string Estatus, Int32 Territorio, int IdCte, int IdRik,
            int Id_Modalidad)
        {
            List<eAcys2> List = new List<eAcys2>();

            try
            {
                eAcys2 acys = new eAcys2();

                acys.Id_Emp = Sesion.Id_Emp;
                acys.Id_Cd = Sesion.Id_Cd_Ver;

                //DateTime dtFechaIn;
                //DateTime.TryParse(FechaIni, out  dtFechaIn);
                acys.Filtro_FecIni = FechaIni;

                /*if (FechaIni == null)
                {
                    FechaIni = null;
                }*/

                //DateTime dtFechaFin;
                //DateTime.TryParse(FechaFin, out  dtFechaFin);
                acys.Filtro_FecFin = FechaFin;

                /*if (FechaIni == null)
                {
                    FechaIni = null;
                }*/

                acys.Filtro_Estatus = Estatus;
                acys.Filtro_FolIni = FolioIni;
                acys.Filtro_FolFin = FolioFin;
                acys.Filtro_Usuario = "0"; //Sesion.Id_U.ToString();
                acys.Id_Ter = Territorio;
                acys.Id_Cte = IdCte;

                //acys.Id_Rik = IdRik;
                acys.Id_Rik = Sesion.Id_Rik;

                acys.Acs_Vencido = "";
                acys.Id_Modalidad = Id_Modalidad;

                CN_CapAcys2Orden CN = new CN_CapAcys2Orden();
                CN.Consultar_Listado(
                    PageNumber, PageSize,
                    AplicaFecha, AplicaFolio,
                    acys, Sesion.Emp_Cnx, ref List);
            }
            catch (Exception ex)
            {
                List = null;
            }

            return List;

        }

        //
        // GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS 
        // GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS 
        // GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS - GUARDAE DOCUMENTO ACYS 
        //
        // Recibe los campos de la forma activa para ACTUALIZAR 
        //
        //
        [HttpPut]
        public eResponse<int> InsertUpdate(eAcys2 E)
        {
            eResponse<int> result = new eResponse<int>();
            int iStatus = 0;

            try
            {
                /*
                List<eUsuarioRik> lst = new List<eUsuarioRik>();
                CN_UsuarioRik cn = new CN_UsuarioRik();
                lst = cn.Lista(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
                return lst;
                */

                //eAcys2 Acys = new eAcys2();            
                eAcys2 A = new eAcys2();
                //Acys acys = new Acys();
                A.Id_Emp = Sesion.Id_Emp;
                A.Id_Cd = Sesion.Id_Cd;


                if (E.Id_Acs == 0)
                {
                    A.Id_Rik = Sesion.Id_Rik;
                }
                else
                {
                    A.Id_Rik = Sesion.Id_Rik;
                }

                A.Id_Acs = E.Id_Acs;
                A.Id_AcsVersion = E.Id_AcsVersion;

                A.Id_Ter = E.Id_Ter;
                //A.Id_Rik = E.Id_Rik;
                A.Id_Cte = E.Id_Cte;

                A.Acs_NomComercial = E.Acs_NomComercial;
                A.Acs_Fecha = E.Acs_Fecha;
                A.Acs_FechaInicio = E.Acs_FechaInicio;
                A.Acs_FechaFin = E.Acs_FechaFin;

                A.Acs_VigenciaApartir = E.Acs_VigenciaApartir;
                A.Acs_VigenciaTermina = E.Acs_VigenciaTermina;

                A.Acs_Contacto = E.Acs_Contacto;
                A.Acs_Telefono = E.Acs_Telefono;
                A.Acs_email = E.Acs_email;

                A.Acs_Contacto2 = E.Acs_Contacto2;
                A.Acs_Telefono2 = E.Acs_Telefono2;
                A.Acs_email2 = E.Acs_email2;

                A.Acs_Contacto3 = E.Acs_Contacto3;
                A.Acs_Telefono3 = E.Acs_Telefono3;
                A.Acs_email3 = E.Acs_email3;

                A.Acs_Contacto4 = E.Acs_Contacto4;
                A.Acs_Telefono4 = E.Acs_Telefono4;
                A.Acs_email4 = E.Acs_email4;

                A.Acs_Contacto5 = E.Acs_Contacto5;
                A.Acs_Telefono5 = E.Acs_Telefono5;
                A.Acs_email5 = E.Acs_email5;

                A.Acs_Contacto6 = E.Acs_Contacto6;
                A.Acs_Telefono6 = E.Acs_Telefono6;
                A.Acs_email6 = E.Acs_email6;

                A.Acs_Modalidad = E.Acs_Modalidad;
                A.Acs_OrdenAbiertaConRep = E.Acs_OrdenAbiertaConRep;

                A.Acs_ParcialidadesSi = E.Acs_ParcialidadesSi;
                A.Acs_ParcialidadesNo = E.Acs_ParcialidadesNo;

                // TIPO PARCIALIDAD 

                A.Acs_ParcialidadTipo = E.Acs_ParcialidadTipo;

                // FORMA DE NEVIO DE PEDIDO

                A.Acs_RecCorreo = E.Acs_RecCorreo;
                A.Acs_RecTelefono = E.Acs_RecTelefono;
                A.Acs_RecWhatsApp = E.Acs_RecWhatsApp;
                A.Acs_RecRIK = E.Acs_RecRIK;
                A.Acs_ReqConfirmacion = E.Acs_ReqConfirmacion;
                A.Acs_RecOtro = E.Acs_RecOtro;
                A.Acs_RecOtroDesc = E.Acs_RecOtroDesc;

                A.Acs_PedidoEncargadoEnviar = E.Acs_PedidoEncargadoEnviar;
                A.Acs_PedidoPuesto = E.Acs_PedidoPuesto;

                A.Acs_PedidoTelefono = E.Acs_PedidoTelefono;
                A.Acs_PedidoTelefono2 = E.Acs_PedidoTelefono2;

                A.Acs_PedidoEmail = E.Acs_PedidoEmail;
                A.Acs_PedidoEmail2 = E.Acs_PedidoEmail2;

                //
                // DOCUMENTOS ENTREGA RESEPCION 
                //

                // L M M J V S D 
                A.Acs_RecRevLunes = E.Acs_RecRevLunes;
                A.Acs_RecRevMartes = E.Acs_RecRevMartes;
                A.Acs_RecRevMiercoles = E.Acs_RecRevMiercoles;
                A.Acs_RecRevJueves = E.Acs_RecRevJueves;
                A.Acs_RecRevViernes = E.Acs_RecRevViernes;
                A.Acs_RecRevSabado = E.Acs_RecRevSabado;
                A.Acs_RecRevDomingo = E.Acs_RecRevDomingo;
                A.Acs_RecRevCualquierDia = E.Acs_RecRevCualquierDia;

                A.Acs_TimePicker1 = E.Acs_TimePicker1;
                A.Acs_TimePicker2 = E.Acs_TimePicker2;
                A.Acs_TimePicker3 = E.Acs_TimePicker3;
                A.Acs_TimePicker4 = E.Acs_TimePicker4;
                A.Acs_TimePicker4 = E.Acs_TimePicker4;

                A.Acs_RecPersonaRecibe = E.Acs_RecPersonaRecibe;

                //Cita para entregar
                A.Acs_RecCitaSinCita = E.Acs_RecCitaSinCita;
                A.Acs_RecCitaMismoDia = E.Acs_RecCitaMismoDia;
                A.Acs_RecCitaPrevia = E.Acs_RecCitaPrevia;

                // Area de recibo                                
                A.Acs_RecAreaPropia = E.Acs_RecAreaPropia;
                A.Acs_RecAreaPlaza = E.Acs_RecAreaPlaza;
                A.Acs_RecAreaCalle = E.Acs_RecAreaCalle;

                // ESTACIONAMIENTO 
                A.Acs_RecEstCortesia = E.Acs_RecEstCortesia;
                A.Acs_RecEstCosto = E.Acs_RecEstCosto;
                A.Acs_RecEstMonto = E.Acs_RecEstMonto;




                // ESPECIFCACIONES ADICIONALES 1
                // Facturas Key
                A.Acs_RecDocFactKeyEnt = E.Acs_RecDocFactKeyEnt;
                A.Acs_RecDocFactKeyEntCop = E.Acs_RecDocFactKeyEntCop;
                A.Acs_RecDocFactKeyRec = E.Acs_RecDocFactKeyRec;
                A.Acs_RecDocFactKeyRecCop = E.Acs_RecDocFactKeyRecCop;

                A.Acs_RecDocOrdCompraEnt = E.Acs_RecDocOrdCompraEnt;
                A.Acs_RecDocOrdCompraEntCop = E.Acs_RecDocOrdCompraEntCop;
                A.Acs_RecDocOrdCompraRec = E.Acs_RecDocOrdCompraRec;
                A.Acs_RecDocOrdCompraRecCop = E.Acs_RecDocOrdCompraRecCop;

                // Orden de reposición
                A.Acs_RecDocOrdReposEnt = E.Acs_RecDocOrdReposEnt;
                A.Acs_RecDocOrdReposEntCop = E.Acs_RecDocOrdReposEntCop;
                A.Acs_RecDocOrdReposRec = E.Acs_RecDocOrdReposRec;
                A.Acs_RecDocOrdReposRecCop = E.Acs_RecDocOrdReposRecCop;

                // Copia de pedido
                A.Acs_RecDocCopPedidoEnt = E.Acs_RecDocCopPedidoEnt;
                A.Acs_RecDocCopPedidoEntCop = E.Acs_RecDocCopPedidoEntCop;
                A.Acs_RecDocCopPedidoRec = E.Acs_RecDocCopPedidoRec;
                A.Acs_RecDocCopPedidoRecCop = E.Acs_RecDocCopPedidoRecCop;

                // Remisión
                A.Acs_RecDocRemisionEnt = E.Acs_RecDocRemisionEnt;
                A.Acs_RecDocRemisionEntCop = E.Acs_RecDocRemisionEntCop;
                A.Acs_RecDocRemisionRec = E.Acs_RecDocRemisionRec;
                A.Acs_RecDocRemisionRecCop = E.Acs_RecDocRemisionRecCop;

                // CERTIFICADO DE CALIDAD 1
                A.Acs_RecDocCertificadoEnt = E.Acs_RecDocCertificadoEnt;
                A.Acs_RecDocCertificadoEntCop = E.Acs_RecDocCertificadoEntCop;
                A.Acs_RecDocCertificadoRec = E.Acs_RecDocCertificadoRec;
                A.Acs_RecDocCertificadoRecCop = E.Acs_RecDocCertificadoRecCop;

                // Pago de facturas 
                // OCT16-2019 
                A.Acs_CorreoRecibirFacturas = E.Acs_CorreoRecibirFacturas;
                A.Acs_CorreoRecibirComplemento = E.Acs_CorreoRecibirComplemento;
                A.Acs_CorreoRecibir_NA = E.Acs_CorreoRecibir_NA;

                // Eléctronica

                A.RevFacEmail = E.RevFacEmail;
                A.RevFacEmailTexto = E.RevFacEmailTexto;
                A.RevFacEmailTexto2 = E.RevFacEmailTexto2;
                A.RevFacPortal = E.RevFacPortal;
                A.RevFacPortalTexto = E.RevFacPortalTexto;
                A.RevFacHttp = E.RevFacHttp;
                A.RevFacUsuario = E.RevFacUsuario;
                A.RevFacContrasenia = E.RevFacContrasenia;

                //CONDICIONES PAGO 
                // Documentos para entrega y recepcion

                A.Acs_DocEntregaFormaPago = E.Acs_DocEntregaFormaPago;

                // Documentos para entrega y recepción
                // ESPECIFICACIONES ADICIONALES
                // Factura Franquicia

                A.ACS_chk62DocFactKeyEnt = E.ACS_chk62DocFactKeyEnt;
                A.ACS_txt62DocFactKeyEntCop = E.ACS_txt62DocFactKeyEntCop;
                A.ACS_chk62DocFactKeyRec = E.ACS_chk62DocFactKeyRec;
                A.ACS_txt62DocFactKeyRecCop = E.ACS_txt62DocFactKeyRecCop;
                // Orden Compra 
                A.ACS_chk62DocOrdCompraEnt = E.ACS_chk62DocOrdCompraEnt;
                A.ACS_txt62DocOrdCompraEntCop = E.ACS_txt62DocOrdCompraEntCop;
                A.ACS_chk62DocOrdCompraRec = E.ACS_chk62DocOrdCompraRec;
                A.ACS_txt62DocOrdCompraRecCop = E.ACS_txt62DocOrdCompraRecCop;
                // Orden Reposicion
                A.ACS_chk62DocOrdReposEnt = E.ACS_chk62DocOrdReposEnt;
                A.ACS_txt62DocOrdReposEntCop = E.ACS_txt62DocOrdReposEntCop;
                A.ACS_chk62DocOrdReposRec = E.ACS_chk62DocOrdReposRec;
                A.ACS_txt62DocOrdReposRecCop = E.ACS_txt62DocOrdReposRecCop;
                //Copia de Pedido
                A.ACS_chk62DocCopPedidoEnt = E.ACS_chk62DocCopPedidoEnt;
                A.ACS_txt62DocCopPedidoEntCop = E.ACS_txt62DocCopPedidoEntCop;
                A.ACS_chk62DocCopPedidoRec = E.ACS_chk62DocCopPedidoRec;
                A.ACS_txt62DocCopPedidoRecCop = E.ACS_txt62DocCopPedidoRecCop;
                // Remision
                A.ACS_chk62DocRemisionEnt = E.ACS_chk62DocRemisionEnt;
                A.ACS_txt62DocRemisionEntCop = E.ACS_txt62DocRemisionEntCop;
                A.ACS_chk62DocRemisionRec = E.ACS_chk62DocRemisionRec;
                A.ACS_txt62DocRemisionRecCop = E.ACS_txt62DocRemisionRecCop;

                A.ACS_chk62DocCertificadoEnt = E.ACS_chk62DocCertificadoEnt;
                A.ACS_txt62DocCertificadoEntCop = E.ACS_txt62DocCertificadoEntCop;
                A.ACS_chk62DocCertificadoRec = E.ACS_chk62DocCertificadoRec;
                A.ACS_txt62DocCertificadoRecCop = E.ACS_txt62DocCertificadoRecCop;

                // 

                A.Acs_VisFrecuencia = E.Acs_VisFrecuencia;

                //
                // SERVICIO TECNICO 
                //

                // L M M J V S D 
                A.ACS_chk63Aplicar = E.ACS_chk63Aplicar;
                A.ACS_chk63Lunes = E.ACS_chk63Lunes;
                A.ACS_chk63Martes = E.ACS_chk63Martes;
                A.ACS_chk63Miercoles = E.ACS_chk63Miercoles;
                A.ACS_chk63Jueves = E.ACS_chk63Jueves;
                A.ACS_chk63Viernes = E.ACS_chk63Viernes;
                A.ACS_chk63Sabado = E.ACS_chk63Sabado;
                A.ACS_chk63Domingo = E.ACS_chk63Domingo;
                A.ACS_chk63CualquierDia = E.ACS_chk63CualquierDia;

                A.ACS_Rad63TimePicker163 = E.ACS_Rad63TimePicker163;
                A.ACS_Rad63TimePicker263 = E.ACS_Rad63TimePicker263;
                A.ACS_Rad63TimePicker363 = E.ACS_Rad63TimePicker363;
                A.ACS_Rad63TimePicker463 = E.ACS_Rad63TimePicker463;

                A.ACS_Chk63Mismodia = E.ACS_Chk63Mismodia;
                A.ACS_Chk63Sincita = E.ACS_Chk63Sincita;
                A.ACS_Chk63Previa = E.ACS_Chk63Previa;

                A.ServTecnico = new eCapAcys2_ServicioValor();

                //RELLENO //MANTENIMINEOT
                //OCT23-2019 RFH                
                //MAY31-2020 RFH
                A.ServTecnico.ServRelleno = E.ServTecnico.ServRelleno;
                A.ServTecnico.ServPreventivo = E.ServTecnico.ServPreventivo;
                A.ServTecnico.QuienRecibe = E.ServTecnico.QuienRecibe;
                A.ServTecnico.FuncionQuienRecibe = E.ServTecnico.FuncionQuienRecibe;

                A.ServTecnico.Frecuencia = E.ServTecnico.Frecuencia;

                //
                // SERVICIO CAPACITACION 
                //
                A.ServCapacitacion = new eCapAcys2_ServicioValor();
                A.ServCapacitacion.Aplicar = E.ServCapacitacion.Aplicar;

                A.ServCapacitacion.Tipo1 = E.ServCapacitacion.Tipo1;
                A.ServCapacitacion.Tipo2 = E.ServCapacitacion.Tipo2;

                // L M M J V S D 
                A.ServCapacitacion.Lunes = E.ServCapacitacion.Lunes;
                A.ServCapacitacion.Martes = E.ServCapacitacion.Martes;
                A.ServCapacitacion.Miercoles = E.ServCapacitacion.Miercoles;
                A.ServCapacitacion.Jueves = E.ServCapacitacion.Jueves;
                A.ServCapacitacion.Viernes = E.ServCapacitacion.Viernes;
                A.ServCapacitacion.Sabado = E.ServCapacitacion.Sabado;
                A.ServCapacitacion.Domingo = E.ServCapacitacion.Domingo;
                A.ServCapacitacion.CualquierDia = E.ServCapacitacion.CualquierDia;

                A.ServCapacitacion.CitaServ_MismoDia = E.ServCapacitacion.CitaServ_MismoDia;
                A.ServCapacitacion.CitaServ_Previa = E.ServCapacitacion.CitaServ_Previa;

                A.ServCapacitacion.HorariosRecep1 = E.ServCapacitacion.HorariosRecep1;
                A.ServCapacitacion.HorariosRecep2 = E.ServCapacitacion.HorariosRecep2;
                A.ServCapacitacion.HorariosRecep3 = E.ServCapacitacion.HorariosRecep2;
                A.ServCapacitacion.HorariosRecep4 = E.ServCapacitacion.HorariosRecep3;
                A.ServCapacitacion.Frecuencia = E.ServCapacitacion.Frecuencia;

                //
                // SERVICIO AUDITORIA
                //

                A.ServAuditoria = new eCapAcys2_ServicioValor();
                A.ServAuditoria.Aplicar = E.ServAuditoria.Aplicar;

                A.ServAuditoria.Tipo1 = E.ServAuditoria.Tipo1;
                A.ServAuditoria.Tipo2 = E.ServAuditoria.Tipo2;

                A.ServAuditoria.Lunes = E.ServAuditoria.Lunes;
                A.ServAuditoria.Martes = E.ServAuditoria.Martes;
                A.ServAuditoria.Miercoles = E.ServAuditoria.Miercoles;
                A.ServAuditoria.Jueves = E.ServAuditoria.Jueves;
                A.ServAuditoria.Viernes = E.ServAuditoria.Viernes;
                A.ServAuditoria.Sabado = E.ServAuditoria.Sabado;
                A.ServAuditoria.Domingo = E.ServAuditoria.Domingo;
                A.ServAuditoria.CualquierDia = E.ServAuditoria.CualquierDia;

                A.ServAuditoria.CitaServ_MismoDia = E.ServAuditoria.CitaServ_MismoDia;
                A.ServAuditoria.CitaServ_Previa = E.ServAuditoria.CitaServ_Previa;

                A.ServAuditoria.HorariosRecep1 = E.ServAuditoria.HorariosRecep1;
                A.ServAuditoria.HorariosRecep2 = E.ServAuditoria.HorariosRecep2;
                A.ServAuditoria.HorariosRecep3 = E.ServAuditoria.HorariosRecep2;
                A.ServAuditoria.HorariosRecep4 = E.ServAuditoria.HorariosRecep3;
                A.ServAuditoria.Frecuencia = E.ServAuditoria.Frecuencia;

                //
                // SERVICIO ASESORIA
                //

                A.ACS_chk62Aplicar = E.ACS_chk62Aplicar;

                A.ACS_chk62Tipo1 = E.ACS_chk62Tipo1;
                A.ACS_chk62Tipo2 = E.ACS_chk62Tipo2;

                // L M M J V S D 
                A.ACS_chk62Lunes = E.ACS_chk62Lunes;
                A.ACS_chk62Martes = E.ACS_chk62Martes;
                A.ACS_chk62Miercoles = E.ACS_chk62Miercoles;
                A.ACS_chk62Jueves = E.ACS_chk62Jueves;
                A.ACS_chk62Viernes = E.ACS_chk62Viernes;
                A.ACS_chk62Sabado = E.ACS_chk62Sabado;
                A.ACS_chk62Domingo = E.ACS_chk62Domingo;
                A.ACS_chk62CualquierDia = E.ACS_chk62CualquierDia;

                A.ACS_Chk62Mismodia = E.ACS_Chk62Mismodia;
                A.ACS_Chk62Previa = E.ACS_Chk62Previa;

                A.ACS_RadTimePicker162 = E.ACS_RadTimePicker162;
                A.ACS_RadTimePicker262 = E.ACS_RadTimePicker262;
                A.ACS_RadTimePicker362 = E.ACS_RadTimePicker362;
                A.ACS_RadTimePicker462 = E.ACS_RadTimePicker462;

                A.ServAsesoria = new eCapAcys2_ServicioValor();
                A.ServAsesoria.Frecuencia = E.ServAsesoria.Frecuencia;

                A.Acs_ComentariosRecomendaciones = E.Acs_ComentariosRecomendaciones;

                A.Acs_RevisionFolio = E.Acs_RevisionFolio;
                A.Acs_RevisionEntAlmacen = E.Acs_RevisionEntAlmacen;
                A.Acs_RevisionOrdenCompra = E.Acs_RevisionOrdenCompra;
                A.Acs_RevisionRepConsumo = E.Acs_RevisionRepConsumo;
                A.Acs_RevisionCopiaFactura = E.Acs_RevisionCopiaFactura;

                A.Acs_RevisionOtroDoc = E.Acs_RevisionOtroDoc;

                A.Acs_PagoContraEntrega = E.Acs_PagoContraEntrega;
                A.Acs_VisitaGestorCobranza = E.Acs_VisitaGestorCobranza;

                string MensajeError = "";

                CN_CapAcys2Orden CN = new CN_CapAcys2Orden();
                iStatus = CN.InsertUpdate(ref A, ref MensajeError, Sesion.Emp_Cnx);

                result.Estado = iStatus;
                result.Datos = A.Id_Acs;

            }
            catch (Exception ex)
            {
                result.Mensaje = ex.Message.ToString();
                result.Estado = -1;
                result.Datos = 0;
            }

            return result;
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