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
    public class CD_CapAcys2Orden
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // 06 NOV 2018 RFH Insert Update
        //  * InsertUpdate * 

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

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
                Parametro("@Id_Ter", A.Id_Ter);
                Parametro("@Id_Rik", A.Id_Rik);
                Parametro("@Id_Cte", A.Id_Cte);

                Parametro("@Acs_NomComercial", A.Acs_NomComercial);
                Parametro("@Acs_Fecha", A.Acs_Fecha);
                Parametro("@Acs_FechaInicio", A.Acs_FechaInicio);
                Parametro("@Acs_VigenciaApartir", A.Acs_VigenciaApartir);
                Parametro("@Acs_VigenciaTermina", A.Acs_VigenciaTermina);

                Parametro("@Acs_Modalidad", A.Acs_Modalidad);
                Parametro("@Acs_OrdenAbiertaConRep", A.Acs_OrdenAbiertaConRep);

                // PARCIALIDAD TIPO 
                Parametro("@Acs_ParcialidadTipo", A.Acs_ParcialidadTipo);

                Parametro("@Acs_RecCorreo", A.Acs_RecCorreo);
                Parametro("@Acs_RecTelefono", A.Acs_RecTelefono);
                Parametro("@Acs_RecWhatsApp", A.Acs_RecWhatsApp);
                Parametro("@Acs_RecRIK", A.Acs_RecRIK);
                Parametro("@Acs_ReqConfirmacion", A.Acs_ReqConfirmacion);
                Parametro("@Acs_RecOtro", A.Acs_RecOtro);
                Parametro("@Acs_RecOtroDesc", A.Acs_RecOtroDesc);
                Parametro("@Acs_PedidoEncargadoEnviar", A.Acs_PedidoEncargadoEnviar);
                Parametro("@Acs_PedidoPuesto", A.Acs_PedidoPuesto);
                Parametro("@Acs_PedidoTelefono", A.Acs_PedidoTelefono);
                Parametro("@Acs_PedidoTelefono2", A.Acs_PedidoTelefono2);
                Parametro("@Acs_PedidoEmail", A.Acs_PedidoEmail);
                Parametro("@Acs_PedidoEmail2", A.Acs_PedidoEmail2);

                //
                // DOCUMENTOS ENTREGA RESEPCION 
                //
                // L M M J V S D 
                Parametro("@Acs_RecRevLunes", A.Acs_RecRevLunes);
                Parametro("@Acs_RecRevMartes", A.Acs_RecRevMartes);
                Parametro("@Acs_RecRevMiercoles", A.Acs_RecRevMiercoles);
                Parametro("@Acs_RecRevJueves", A.Acs_RecRevJueves);
                Parametro("@Acs_RecRevViernes", A.Acs_RecRevViernes);
                Parametro("@Acs_RecRevSabado", A.Acs_RecRevSabado);
                Parametro("@Acs_RecRevDomingo", A.Acs_RecRevDomingo);
                Parametro("@Acs_RecRevCualquierDia", A.Acs_RecRevCualquierDia);

                Parametro("@Acs_TimePicker1", A.Acs_TimePicker1);
                Parametro("@Acs_TimePicker2", A.Acs_TimePicker2);
                Parametro("@Acs_TimePicker3", A.Acs_TimePicker3);
                Parametro("@Acs_TimePicker4", A.Acs_TimePicker4);

                Parametro("@Acs_RecPersonaRecibe", A.Acs_RecPersonaRecibe);

                //Cita para entregar
                Parametro("@Acs_RecCitaSinCita", A.Acs_RecCitaSinCita);
                Parametro("@Acs_RecCitaMismoDia", A.Acs_RecCitaMismoDia);
                Parametro("@Acs_RecCitaPrevia", A.Acs_RecCitaPrevia);

                // Area de recibo                                
                Parametro("@Acs_RecAreaPropia", A.Acs_RecAreaPropia);
                Parametro("@Acs_RecAreaPlaza", A.Acs_RecAreaPlaza);
                Parametro("@Acs_RecAreaCalle", A.Acs_RecAreaCalle);

                // Estacionamiento 
                Parametro("@Acs_RecEstCortesia", A.Acs_RecEstCortesia);
                Parametro("@Acs_RecEstCosto", A.Acs_RecEstCosto);
                Parametro("@Acs_RecEstMonto", A.Acs_RecEstMonto);

                // Especificaciones Adicionales 1
                // Facturas Key
                Parametro("@Acs_RecDocFactKeyEnt", A.Acs_RecDocFactKeyEnt);
                Parametro("@Acs_RecDocFactKeyEntCop", A.Acs_RecDocFactKeyEntCop);
                Parametro("@Acs_RecDocFactKeyRec", A.Acs_RecDocFactKeyRec);
                Parametro("@Acs_RecDocFactKeyRecCop", A.Acs_RecDocFactKeyRecCop);

                Parametro("@Acs_RecDocOrdCompraEnt", A.Acs_RecDocOrdCompraEnt);
                Parametro("@Acs_RecDocOrdCompraEntCop", A.Acs_RecDocOrdCompraEntCop);
                Parametro("@Acs_RecDocOrdCompraRec", A.Acs_RecDocOrdCompraRec);
                Parametro("@Acs_RecDocOrdCompraRecCop", A.Acs_RecDocOrdCompraRecCop);

                // Orden de reposición
                Parametro("@Acs_RecDocOrdReposEnt", A.Acs_RecDocOrdReposEnt);
                Parametro("@Acs_RecDocOrdReposEntCop", A.Acs_RecDocOrdReposEntCop);
                Parametro("@Acs_RecDocOrdReposRec", A.Acs_RecDocOrdReposRec);
                Parametro("@Acs_RecDocOrdReposRecCop", A.Acs_RecDocOrdReposRecCop);

                // Copia de pedido
                Parametro("@Acs_RecDocCopPedidoEnt", A.Acs_RecDocCopPedidoEnt);
                Parametro("@Acs_RecDocCopPedidoEntCop", A.Acs_RecDocCopPedidoEntCop);
                Parametro("@Acs_RecDocCopPedidoRec", A.Acs_RecDocCopPedidoRec);
                Parametro("@Acs_RecDocCopPedidoRecCop", A.Acs_RecDocCopPedidoRecCop);

                // Remisión
                Parametro("@Acs_RecDocRemisionEnt", A.Acs_RecDocRemisionEnt);
                Parametro("@Acs_RecDocRemisionEntCop", A.Acs_RecDocRemisionEntCop);
                Parametro("@Acs_RecDocRemisionRec", A.Acs_RecDocRemisionRec);
                Parametro("@Acs_RecDocRemisionRecCop", A.Acs_RecDocRemisionRecCop);

                Parametro("@Acs_DocEntregaFormaPago", A.Acs_DocEntregaFormaPago);

                // Certificado 1
                Parametro("@Acs_RecDocCertificadoEnt", A.Acs_RecDocCertificadoEnt);
                Parametro("@Acs_RecDocCertificadoEntCop", A.Acs_RecDocCertificadoEntCop);
                Parametro("@Acs_RecDocCertificadoRec", A.Acs_RecDocCertificadoRec);
                Parametro("@Acs_RecDocCertificadoRecCop", A.Acs_RecDocCertificadoRecCop);

                // Pago de facturas 
                // OCT16-2019                
                Parametro("@Acs_CorreoRecibirFacturas", A.Acs_CorreoRecibirFacturas);
                Parametro("@Acs_CorreoRecibirComplemento", A.Acs_CorreoRecibirComplemento);
                Parametro("@Acs_CorreoRecibir_NA", A.Acs_CorreoRecibir_NA);

                // Eléctronica
                Parametro("@RevFacEmail", A.RevFacEmail);
                Parametro("@RevFacEmailTexto", A.RevFacEmailTexto);
                Parametro("@RevFacPortal", A.RevFacPortal);
                Parametro("@RevFacPortalTexto", A.RevFacPortalTexto);
                Parametro("@RevFacHttp", A.RevFacHttp);
                Parametro("@RevFacUsuario", A.RevFacUsuario);
                Parametro("@RevFacContrasenia", A.RevFacContrasenia);

                //Parametro("@Acs_DocEntregaFormaPago", A.Acs_DocEntregaFormaPago);                                

                Parametro("@Acs_ParcialidadesSi", A.Acs_ParcialidadesSi);
                Parametro("@Acs_ParcialidadesNo", A.Acs_ParcialidadesNo);

                //Condiciones de Pago 
                //Documentos 

                /*
                Parametro("@Acs_CondPagEntFac", A.Acs_CondPagEntFac);
                Parametro("@Acs_CondPagEntFacCop", A.Acs_CondPagEntFacCop);
                Parametro("@Acs_CondPagReFac", A.Acs_CondPagReFac);
                Parametro("@Acs_CondPagReFacCop", A.Acs_CondPagReFacCop);
                Parametro("@Acs_CondPagEntOrdCom", A.Acs_CondPagEntOrdCom);
                Parametro("@Acs_CondPagEntOrdComCop", A.Acs_CondPagEntOrdComCop);
                Parametro("@Acs_CondPagReOrdCom", A.Acs_CondPagReOrdCom);
                Parametro("@Acs_CondPagReOrdComCop", A.Acs_CondPagReOrdComCop);
                Parametro("@Acs_CondPagEntOrdRep", A.Acs_CondPagEntOrdRep);
                Parametro("@Acs_CondPagEntOrdRepCop", A.Acs_CondPagEntOrdRepCop);
                Parametro("@Acs_CondPagReOrdRep", A.Acs_CondPagReOrdRep);
                Parametro("@Acs_CondPagReOrdRepCop", A.Acs_CondPagReOrdRepCop);
                Parametro("@Acs_CondPagEntCopPed", A.Acs_CondPagEntCopPed);
                Parametro("@Acs_CondPagEntCopPedCop", A.Acs_CondPagEntCopPedCop);
                Parametro("@Acs_CondPagReCopPed", A.Acs_CondPagReCopPed);
                Parametro("@Acs_CondPagReCopPedCop", A.Acs_CondPagReCopPedCop);
                Parametro("@Acs_CondPagEntPagRem", A.Acs_CondPagEntPagRem);
                Parametro("@Acs_CondPagEntPagRemCop", A.Acs_CondPagEntPagRemCop);
                Parametro("@Acs_CondPagRePagRem", A.Acs_CondPagRePagRem);
                Parametro("@Acs_CondPagRePagRemCop", A.Acs_CondPagRePagRemCop);
                */

                //CONDICIONES DE PAGO 
                // Documentos para entrega y recepción
                // ESPECIFICACIONES ADICIONALES

                // Factura Key 
                Parametro("@ACS_chk62DocFactKeyEnt", A.ACS_chk62DocFactKeyEnt);
                Parametro("@ACS_txt62DocFactKeyEntCop", A.ACS_txt62DocFactKeyEntCop);
                Parametro("@ACS_chk62DocFactKeyRec", A.ACS_chk62DocFactKeyRec);
                Parametro("@ACS_txt62DocFactKeyRecCop", A.ACS_txt62DocFactKeyRecCop);
                // Orden Compra 
                Parametro("@ACS_chk62DocOrdCompraEnt", A.ACS_chk62DocOrdCompraEnt);
                Parametro("@ACS_txt62DocOrdCompraEntCop", A.ACS_txt62DocOrdCompraEntCop);
                Parametro("@ACS_chk62DocOrdCompraRec", A.ACS_chk62DocOrdCompraRec);
                Parametro("@ACS_txt62DocOrdCompraRecCop", A.ACS_txt62DocOrdCompraRecCop);
                // Orden Reposicion
                Parametro("@ACS_chk62DocOrdReposEnt", A.ACS_chk62DocOrdReposEnt);
                Parametro("@ACS_txt62DocOrdReposEntCop", A.ACS_txt62DocOrdReposEntCop);
                Parametro("@ACS_chk62DocOrdReposRec", A.ACS_chk62DocOrdReposRec);
                Parametro("@ACS_txt62DocOrdReposRecCop", A.ACS_txt62DocOrdReposRecCop);
                //Copia de Pedido
                Parametro("@ACS_chk62DocCopPedidoEnt", A.ACS_chk62DocCopPedidoEnt);
                Parametro("@ACS_txt62DocCopPedidoEntCop", A.ACS_txt62DocCopPedidoEntCop);
                Parametro("@ACS_chk62DocCopPedidoRec", A.ACS_chk62DocCopPedidoRec);
                Parametro("@ACS_txt62DocCopPedidoRecCop", A.ACS_txt62DocCopPedidoRecCop);
                // Remision
                Parametro("@ACS_chk62DocRemisionEnt", A.ACS_chk62DocRemisionEnt);
                Parametro("@ACS_txt62DocRemisionEntCop", A.ACS_txt62DocRemisionEntCop);
                Parametro("@ACS_chk62DocRemisionRec", A.ACS_chk62DocRemisionRec);
                Parametro("@ACS_txt62DocRemisionRecCop", A.ACS_txt62DocRemisionRecCop);

                // CERTIFICADO 2
                Parametro("@ACS_chk62DocCertificadoEnt", A.ACS_chk62DocCertificadoEnt);
                Parametro("@ACS_txt62DocCertificadoEntCop", A.ACS_txt62DocCertificadoEntCop);
                Parametro("@ACS_chk62DocCertificadoRec", A.ACS_chk62DocCertificadoRec);
                Parametro("@ACS_txt62DocCertificadoRecCop", A.ACS_txt62DocCertificadoRecCop);

                // Sevicios de Valor 

                Parametro("@Acs_VisFrecuencia", A.Acs_VisFrecuencia);

                //
                // SERVICIO TECNICO                 
                //
                Parametro("@ACS_chk63Aplicar", A.ACS_chk63Aplicar);
                // L M M J V S D 
                Parametro("@ACS_chk63Lunes", A.ACS_chk63Lunes);
                Parametro("@ACS_chk63Martes", A.ACS_chk63Martes);
                Parametro("@ACS_chk63Miercoles", A.ACS_chk63Miercoles);
                Parametro("@ACS_chk63Jueves", A.ACS_chk63Jueves);
                Parametro("@ACS_chk63Viernes", A.ACS_chk63Viernes);
                Parametro("@ACS_chk63Sabado", A.ACS_chk63Sabado);
                Parametro("@ACS_chk63Domingo", A.ACS_chk63Domingo);
                Parametro("@ACS_chk63CualquierDia", A.ACS_chk63CualquierDia);

                Parametro("@ACS_Rad63TimePicker163", A.ACS_Rad63TimePicker163);
                Parametro("@ACS_Rad63TimePicker263", A.ACS_Rad63TimePicker263);
                Parametro("@ACS_Rad63TimePicker363", A.ACS_Rad63TimePicker363);
                Parametro("@ACS_Rad63TimePicker463", A.ACS_Rad63TimePicker463);

                Parametro("@ACS_Chk63Mismodia", A.ACS_Chk63Mismodia);
                Parametro("@ACS_Chk63Sincita", A.ACS_Chk63Sincita);
                Parametro("@ACS_Chk63Previa", A.ACS_Chk63Previa);

                // RELLENO MANTENIMIENTO
                // OCT23-2019
                //MAY31-2020 RFH
                Parametro("@ST_Relleno", A.ServTecnico.ServRelleno);
                Parametro("@ST_Mantenimiento", A.ServTecnico.ServPreventivo);
                Parametro("@ST_QuienRecibe", A.ServTecnico.QuienRecibe);
                Parametro("@ST_FuncionQuienRecibe", A.ServTecnico.FuncionQuienRecibe);

                Parametro("@ST_Frecuencia", A.ServTecnico.Frecuencia);

                //
                // SERVICIO CAPACITACION 
                //
                Parametro("@ACS_ServCap_Aplicar", A.ServCapacitacion.Aplicar);
                Parametro("@ACS_ServCap_Tipo1", A.ServCapacitacion.Tipo1);
                Parametro("@ACS_ServCap_Tipo2", A.ServCapacitacion.Tipo2);
                // L M M J V S D 
                Parametro("@ACS_ServCap_Lunes", A.ServCapacitacion.Lunes);
                Parametro("@ACS_ServCap_Martes", A.ServCapacitacion.Martes);
                Parametro("@ACS_ServCap_Miercoles", A.ServCapacitacion.Miercoles);
                Parametro("@ACS_ServCap_Jueves", A.ServCapacitacion.Jueves);
                Parametro("@ACS_ServCap_Viernes", A.ServCapacitacion.Viernes);
                Parametro("@ACS_ServCap_Sabado", A.ServCapacitacion.Sabado);
                Parametro("@ACS_ServCap_Domingo", A.ServCapacitacion.Domingo);
                Parametro("@ACS_ServCap_CualquierDia", A.ServCapacitacion.CualquierDia);
                Parametro("@ACS_ServCap_HorariosRecep1", A.ServCapacitacion.HorariosRecep1);
                Parametro("@ACS_ServCap_HorariosRecep2", A.ServCapacitacion.HorariosRecep2);
                Parametro("@ACS_ServCap_HorariosRecep3", A.ServCapacitacion.HorariosRecep3);
                Parametro("@ACS_ServCap_HorariosRecep4", A.ServCapacitacion.HorariosRecep4);
                Parametro("@ACS_ServCap_CitaServ_MismoDia", A.ServCapacitacion.CitaServ_MismoDia);
                Parametro("@ACS_ServCap_CitaServ_Previa", A.ServCapacitacion.CitaServ_Previa);
                Parametro("@ACS_ServCap_Frecuencia", A.ServCapacitacion.Frecuencia);


                //
                // SERVICIO AUDITORIA
                //
                Parametro("@ACS_ServAud_Aplicar", A.ServAuditoria.Aplicar);
                Parametro("@ACS_ServAud_Tipo1", A.ServAuditoria.Tipo1);
                Parametro("@ACS_ServAud_Tipo2", A.ServAuditoria.Tipo2);
                // L M M J V S D 
                Parametro("@ACS_ServAud_Lunes", A.ServAuditoria.Lunes);
                Parametro("@ACS_ServAud_Martes", A.ServAuditoria.Martes);
                Parametro("@ACS_ServAud_Miercoles", A.ServAuditoria.Miercoles);
                Parametro("@ACS_ServAud_Jueves", A.ServAuditoria.Jueves);
                Parametro("@ACS_ServAud_Viernes", A.ServAuditoria.Viernes);
                Parametro("@ACS_ServAud_Sabado", A.ServAuditoria.Sabado);
                Parametro("@ACS_ServAud_Domingo", A.ServAuditoria.Domingo);
                Parametro("@ACS_ServAud_CualquierDia", A.ServAuditoria.CualquierDia);
                Parametro("@ACS_ServAud_HorariosRecep1", A.ServAuditoria.HorariosRecep1);
                Parametro("@ACS_ServAud_HorariosRecep2", A.ServAuditoria.HorariosRecep2);
                Parametro("@ACS_ServAud_HorariosRecep3", A.ServAuditoria.HorariosRecep3);
                Parametro("@ACS_ServAud_HorariosRecep4", A.ServAuditoria.HorariosRecep4);
                Parametro("@ACS_ServAud_CitaServ_MismoDia", A.ServAuditoria.CitaServ_MismoDia);
                Parametro("@ACS_ServAud_CitaServ_Previa", A.ServAuditoria.CitaServ_Previa);
                Parametro("@ACS_ServAud_Frecuencia", A.ServAuditoria.Frecuencia);

                //
                // SERVICIO ASESORIA
                //
                Parametro("@ACS_chk62Aplicar", A.ACS_chk62Aplicar);
                Parametro("@ACS_chk62Tipo1", A.ACS_chk62Tipo1);
                Parametro("@ACS_chk62Tipo2", A.ACS_chk62Tipo2);
                // L M M J V S D 
                Parametro("@ACS_chk62Lunes", A.ACS_chk62Lunes);
                Parametro("@ACS_chk62Martes", A.ACS_chk62Martes);
                Parametro("@ACS_chk62Miercoles", A.ACS_chk62Miercoles);
                Parametro("@ACS_chk62Jueves", A.ACS_chk62Jueves);
                Parametro("@ACS_chk62Viernes", A.ACS_chk62Viernes);
                Parametro("@ACS_chk62Sabado", A.ACS_chk62Sabado);
                Parametro("@ACS_chk62Domingo", A.ACS_chk62Domingo);
                Parametro("@ACS_chk62CualquierDia", A.ACS_chk62CualquierDia);

                Parametro("@ACS_RadTimePicker162", A.ACS_RadTimePicker162);
                Parametro("@ACS_RadTimePicker262", A.ACS_RadTimePicker262);
                Parametro("@ACS_RadTimePicker362", A.ACS_RadTimePicker362);
                Parametro("@ACS_RadTimePicker462", A.ACS_RadTimePicker462);

                Parametro("@ACS_Chk62Mismodia", A.ACS_Chk62Mismodia);
                Parametro("@ACS_Chk62Previa", A.ACS_Chk62Previa);
                Parametro("@ACS_ServAses_Frecuencia", A.ServAsesoria.Frecuencia);

                Parametro("@Acs_ComentariosRecomendaciones", A.Acs_ComentariosRecomendaciones);

                Parametro("@Acs_RevisionFolio", A.Acs_RevisionFolio);
                Parametro("@Acs_RevisionEntAlmacen", A.Acs_RevisionEntAlmacen);
                Parametro("@Acs_RevisionOrdenCompra", A.Acs_RevisionOrdenCompra);
                Parametro("@Acs_RevisionRepConsumo", A.Acs_RevisionRepConsumo);
                Parametro("@Acs_RevisionCopiaFactura", A.Acs_RevisionCopiaFactura);

                Parametro("@Acs_RevisionOtroDoc", A.Acs_RevisionOtroDoc);

                Parametro("@Acs_PagoContraEntrega", A.Acs_PagoContraEntrega);
                Parametro("@Acs_VisitaGestorCobranza", A.Acs_VisitaGestorCobranza);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys2Orden_InsertUpdate",
                    ref dr,
                    Parametros.ToArray(),
                    Valores.ToArray()
                );

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


    }
}