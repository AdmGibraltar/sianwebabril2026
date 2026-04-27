using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapPago
    {
        public void ConsultarCantidadPagosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapPago().ConsultarCantidadPagosCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago(Pago pago, string Conexion, ref List<Pago> List)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPago(pago, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoFicha(ref Factura ficha, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoFicha(ref ficha, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoNotaFicha(ref NotaCargo ficha, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoNotaFicha(ref ficha, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.InsertarPago(pago, list_fichas, list_pagos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador, ref List<int> centros)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ModificarPago(pago, list_fichas, list_pagos, Conexion, ref verificador, ref centros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago(ref Pago pago, ref List<Banco_Ficha> list_fichas, ref List<PagoDet> list_pagos, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPago(ref pago, ref list_fichas, ref list_pagos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago2(ref Pago pago, ref List<Banco_Ficha> list_fichas, ref List<PagoDet> list_pagos, string Conexion, ref List<PagoDetComplemento> List_DetComplemento)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPago2(ref pago, ref list_fichas, ref list_pagos, Conexion, ref List_DetComplemento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaPagos(ref Pago pago, ref List<PagoDet> list_pagos, string Conexion)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ConsultaListaPagos(ref pago, ref list_pagos, Conexion);

        }

        public void Baja(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.Baja(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.Imprimir(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidaReferenciaBancaria(Sesion sesion, int banco, string Pag_RefBancaria, ref string ValidaReferencia)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.ValidaReferenciaBancaria(sesion, banco, Pag_RefBancaria, ref ValidaReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ConsultaCuentaBanco(Sesion sesion, int banco, ref string cuenta)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.ConsultaCuentaBanco(sesion, banco, ref cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PermitirExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.PermitirExtemporaneo(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CierreExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.CierreExtemporaneo(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentro(int Id_Emp, string serie, ref DbCentro centro, string ConexionCob, int Tipo_CDC)
        {
            try
            {
                CD_CapPago cppago = new CD_CapPago();
                cppago.ConsultarCentro(Id_Emp, serie, ref centro, ConexionCob, Tipo_CDC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region PagoDetComplemento
        public void ConsultaComplementoPago(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaComplementoPago(ref pagoDetComplemento, ref pagoPdf, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPago(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultarPago(ref pagoDetComplemento, ref pagoPdf, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcion que conuslta la lista de complemento de pago y traes sus documentos
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="pagoPdf"></param>
        /// <param name="Conexion"></param>
        public void ConsultaComplementoPagoConsultaDetlista(ref PagoDetComplemento pagoDetComplemento, ref object pagoPdf, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPdf, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// trae la información de los documentos
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="pagoPdf"></param>
        /// <param name="Conexion"></param>
        public void ConsultaDetalleComplementoPagoListaDocs(PagoDetComplemento pagoDetComplemento, ref List<PagoDetComplemento> ListaDocumentos, ref object pagoPdf, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaDetalleComplementoPagoListaDocs(pagoDetComplemento, ref ListaDocumentos, ref pagoPdf, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que consultar los documentos con error de timbrado
        /// </summary>
        /// <param name="PagoDetalleComplementoShowLog"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void ConsultarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog PagoDetalleComplementoShowLog, string Conexion, ref List<PagoDetalleComplementoShowLog> Lista)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ConsultarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog, Conexion, ref Lista);
        }


        /// <summary>
        /// Funcion que actualiza el estatus de timbre del show log.
        /// </summary>
        /// <param name="pagoDetComplemento"></param>
        /// <param name="Conexion"></param>
        /// <param name="insertar"></param>
        /// <param name="verificador"></param>
        public void ModificarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog pagoDetComplemento, string Conexion, ref int verificador)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ModificarPagoDetTimbradoShowLog(pagoDetComplemento, Conexion, ref verificador);
        }

        public void InsertarComplementoPago(PagoDetComplemento pagoDetComplemento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.InsertarComplementoPago(pagoDetComplemento, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarComplementoPago(PagoDetComplemento pagoDetComplemento, string Conexion, bool insertar, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ModificarComplementoPago(pagoDetComplemento, Conexion, insertar, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaComplementoPago(PagoDetComplemento pagoDetComplemento, bool otraBD, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.BajaComplementoPago(pagoDetComplemento, otraBD, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSerieYFolio(int id_Emp, int id_Cd, ref string serie, ref int folio, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaSerieYFolio(id_Emp, id_Cd, ref serie, ref folio, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, int id_Cte, int id_PagComp, string Conexion, ref List<PagoDetComplemento> lista)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaListaComplementosPago(id_Emp, id_Cd, id_Pag, id_Cte, id_PagComp, Conexion, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, string Conexion, ref List<PagoDetComplemento> lista)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaListaComplementosPago(id_Emp, id_Cd, id_Pag, Conexion, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaComplementosPago(int id_Emp, int id_Cd, int id_Pag, int id_PagComp, string Conexion, ref List<PagoDetComplemento> lista)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaListaComplementosPago(id_Emp, id_Cd, id_Pag, id_PagComp, Conexion, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarComplementoPago(int id_Emp, int id_Cd, int id_Pag, int id_PagDet, int Id_Comp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.BorrarComplementoPago(id_Emp, id_Cd, id_Pag, id_PagDet, Id_Comp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaNumeroDeParcialidad(int id_Emp, int id_Cd, string serie, int folio, DateTime fecha, ref int numParcialidad, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaNumeroDeParcialidad(id_Emp, id_Cd, serie, folio, fecha, ref numParcialidad, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDocumentosNoTimbrados(DateTime Fecha, string Conexion, ref List<PagoDetTimbrado> List)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaDocumentosNoTimbrados(Fecha, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ConsultaPagoTimbres(ref Pago pago, ref List<PagoDet> list_pagos, string Conexion, ref List<PagoDetComplemento> List_DetComplemento)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ConsultaPagoTimbres(ref pago, ref list_pagos, Conexion, ref List_DetComplemento);
        }

        public void ConsultaListaComplementosNoTimbrados(int id_Emp, int id_Cd, int id_Pag, int id_Cte, int id_PagDet, string Conexion, ref List<PagoDetComplemento> lista)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ConsultaListaComplementosNoTimbrados(id_Emp, id_Cd, id_Pag, id_Cte, id_PagDet, Conexion, ref lista);
        }

        /// <summary>
        /// Función que muestra la cantidad de docuemntos que no han sido timbrados y que tenga los permisos
        /// </summary>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd"></param>
        /// <param name="Id_Usuario"></param>
        /// <param name="Conexion"></param>
        /// <param name="NumDocNTimbrados"></param>
        public void VerMonitorComplementosNoTimbrados(int Id_Emp, int Id_Cd, int Id_Usuario, string Conexion, ref int NumDocNTimbrados)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.VerMonitorComplementosNoTimbrados(Id_Emp, Id_Cd, Id_Usuario, Conexion, ref NumDocNTimbrados);

        }

        #endregion PagoDetComplemento
        #region PagoSegundoPlano

        public void ActivaPagoSegundoPlano(string Conexion, ref FacturaLite entFacturaLite, ref string strMsjResultado)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ActivaPagoSegundoPlano(Conexion, ref entFacturaLite, ref strMsjResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesactivaPagoSegundoPlano(string Conexion, FacturaLite entFacturaLite)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.DesactivaPagoSegundoPlano(Conexion, entFacturaLite);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //public void ConsultaPagoSegundoPlano2(string Conexion, ref FacturaLite entFacturaLite,ref string strMsjResultado)
        //{
        //    try
        //    {
        //        CD_CapPago claseCapaDatos = new CD_CapPago();
        //        claseCapaDatos.ConsultaPagoSegundoPlano2(Conexion, ref entFacturaLite,ref  strMsjResultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public void ConsultaPagoSegundoPlano(string Conexion, ref FacturaLite entFacturaLite, ref string strMsjResultado)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoSegundoPlano(Conexion, ref entFacturaLite, ref strMsjResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        public void PagoSegundoPlanoConsultaEstatus(string Conexion, ref FacturaLite entFacturaLite)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.PagoSegundoPlanoConsultaEstatus(Conexion, ref entFacturaLite);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion PagoSegundoPlano

        #region Cambios Referencia Pagos
        public void ConsultaReferenciaBanco(Sesion sesion, string Referencia, ref int Id_Ban, ref string cuenta)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ConsultaReferenciaBanco(sesion, Referencia, ref Id_Ban, ref cuenta);
        }
        public void spCatBanco_Combo(Sesion sesion, int id_Ban, ref string cuenta)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.spCatBanco_Combo(sesion, id_Ban, ref cuenta);
        }

        public void ValidaSucursal(Sesion sesion, ref int id_Cd)
        {
            CD_CapPago claseCapaDatos = new CD_CapPago();
            claseCapaDatos.ValidaSucursal(sesion, ref id_Cd);
        }
        #endregion

        public void ConsultaPagoDetTimbrado(string Conexion, ref PagoDetTimbrado pagoDetTimbrado)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoDetTimbrado(Conexion, ref pagoDetTimbrado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}