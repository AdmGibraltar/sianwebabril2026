using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapFactura
    {
        public void ConsultaFactura(ref Factura factura, ref List<FacturaDet> listaFacturaDet, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFactura(ref factura, ref listaFacturaDet, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaFacturaExiste(ref Factura factura, ref List<FacturaDet> listaFacturaDet, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaExiste(ref factura, ref listaFacturaDet, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaFacturaOtraBD(ref Factura factura, string serie, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaOtraBD(ref factura, serie, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaOtraBDExiste(ref Factura factura, string serie, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaOtraBDExiste(ref factura, serie, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura(ref Factura factura, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFactura(ref factura, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaNacional(ref Factura factura, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaNacional(ref factura, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarFacturaEspecial(ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarFacturaEspecial(ref facturaEsp, ref listaFacturaProductos, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidarDisponibilidadInventario(Sesion sesion, int cantidad, Int64 producto, int Id_Ped, ref int validador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ValidarDisponibilidadInventario(sesion, cantidad, producto, Id_Ped, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaEncabezado(ref Factura factura, string Conexion, ref bool encontrado)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ConsultaFacturaEncabezado(ref factura, Conexion, ref encontrado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCantidadProdFactura(Sesion sesion, Int64 prd, int fac, int territorio, ref int cantidadFac)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarCantidadProdFactura(sesion, prd, fac, territorio, ref cantidadFac);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que retorna los datos requeridos para el grid de  FacturaXRuta
        /// </summary>
        /// <param name="factura">Entidad de las facturas</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista donde se almacenara el resultado de la operacion</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Fac">Id de la factura</param>
        //public void BuscaFacturaRuta(Factura factura, string conexion, ref List<Factura> lista,
        //    int Id_Emp, int Id_Cd, int Id_Fac)
        //{
        //    try
        //    {
        //        CD_CapFactura CDCapFactura=new CD_CapFactura();

        //        CDCapFactura.BuscaFacturaRuta(factura, conexion, ref lista, Id_Emp, Id_Cd, Id_Fac);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void BuscaFacturaRuta(ref Factura factura, string conexion)
        {
            try
            {
                CD_CapFactura CDCapFactura = new CD_CapFactura();
                CDCapFactura.BuscaFacturaRuta(ref factura, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Factura_DepuracionConsulta(ref Factura factura, string conexion)
        {
            try
            {
                CD_CapFactura CDCapFactura = new CD_CapFactura();
                CDCapFactura.Factura_DepuracionConsulta(ref factura, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Factura_DepuracionActualiza(Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.Factura_DepuracionActualiza(factura, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturaEspecialDetalle(ref List<FacturaDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Fac, int id_Cte)
        {
            try
            {
                CD_CapFactura CDCapFactura = new CD_CapFactura();
                CDCapFactura.ConsultaFacturaEspecialDetalle(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Fac, id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura_Buscar(Factura factura, string Conexion, ref List<Factura> List
                  , int id_Emp
                  , int id_Cd
                  , string nombreCliente
                  , int id_Cte_inicio
                  , int id_Cte_fin
                  , string fac_Tipo
                  , string fac_Estatus
                  , DateTime fac_Fecha_inicio
                  , DateTime Fac_Fecha_fin
                  , int id_Fac_inicio
                  , int id_Fac_fin
                  , int id_Ped_inicio
                  , int id_Ped_fin
                  , bool? acuse
                  , bool? depuracion
                  , int id_U
                  , bool? Complementaria
                  , string OrdenCompra
                  , string EstatusSAT
            )
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ConsultaFactura_Buscar(factura, Conexion, ref List
                    , id_Emp
                    , id_Cd
                    , nombreCliente
                    , id_Cte_inicio
                    , id_Cte_fin
                    , fac_Tipo
                    , fac_Estatus
                    , fac_Fecha_inicio
                    , Fac_Fecha_fin
                    , id_Fac_inicio
                    , id_Fac_fin
                    , id_Ped_inicio
                    , id_Ped_fin
                    , acuse
                    , depuracion
                    , id_U
                    , Complementaria
                    , OrdenCompra
                    , EstatusSAT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] ConsultaFacturacion_DatosGeneralesFacturacion(string Conexion, int id_Emp, int id_Cd)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                return claseCapaDatos.ConsultaFacturacion_DatosGeneralesFacturacion(Conexion, id_Emp, id_Cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insertar factura  
        /// </summary>
        public void InsertarFactura(Sesion sesion, ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion,
            ref int verificador, ref int? Id_Ped, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera,
        string IdRF, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaProductosFacturaEspecial, bool actualizar, String TraePicking)// int Id_Tm_Rem)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.InsertarFactura(sesion, ref factura, ref listaFacturaDet, ref listaFacturaDetAdenda, CantidadR, Conexion, ref verificador, ref Id_Ped, ref listaEntSalRemisiones, listAdendaCabecera, IdRF, arrayRemisiones, ref entSal, ref listaEntSalDetalle, ref facturaEsp, ref listaProductosFacturaEspecial, actualizar, TraePicking);//, Id_Tm_Rem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFactura(Sesion sesion, ref Factura factura, ref Factura facturaNacional, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, ref DataTable listaFacturaDetAdendaNacional, int CantidadR, string Conexion,
            ref int verificador, ref int? Id_Ped, ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, List<AdendaDet> listAdendaNacionalCabecera,
        string IdRF, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaProductosFacturaEspecial, bool actualizar, String TraePicking)// int Id_Tm_Rem)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.InsertarFactura(sesion, ref factura, ref facturaNacional, ref listaFacturaDet, ref listaFacturaDetAdenda, ref listaFacturaDetAdendaNacional, CantidadR, Conexion, ref verificador, ref Id_Ped, ref listaEntSalRemisiones, listAdendaCabecera, listAdendaNacionalCabecera, IdRF, arrayRemisiones, ref entSal, ref listaEntSalDetalle, ref facturaEsp, ref listaProductosFacturaEspecial, actualizar, TraePicking);//, Id_Tm_Rem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Modificar factura
        /// </summary>
        public void ModificarFactura(Sesion sesion, ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, int CantidadR, string Conexion, ref int verificador, ref int? Id_Ped,
            ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ModificarFactura(sesion, ref factura, ref listaFacturaDet, ref listaFacturaDetAdenda, CantidadR, Conexion, ref verificador, ref Id_Ped, ref listaEntSalRemisiones, listAdendaCabecera, arrayRemisiones, ref entSal, ref listaEntSalDetalle, ref facturaEsp, ref listaProductosFacturaEspecial, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarFactura(Sesion sesion, ref Factura factura, ref Factura facturaNacional, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda, ref DataTable listaFacturaDetalleAdendaNacional, int CantidadR, string Conexion, ref int verificador, ref int? Id_Ped,
           ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, List<AdendaDet> listAdendaNacionalCabecera, string arrayRemisiones, ref EntradaSalida entSal, ref List<EntradaSalidaDetalle> listaEntSalDetalle, ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaProductosFacturaEspecial, bool actualizar)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ModificarFactura(sesion, ref factura, ref facturaNacional, ref listaFacturaDet, ref listaFacturaDetAdenda, ref listaFacturaDetalleAdendaNacional, CantidadR, Conexion, ref verificador, ref Id_Ped, ref listaEntSalRemisiones, listAdendaCabecera, listAdendaNacionalCabecera, arrayRemisiones, ref entSal, ref listaEntSalDetalle, ref facturaEsp, ref listaProductosFacturaEspecial, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Insertar factura con pedido previo, tipo MOv. 16, (aparatos in productivos)
        /// </summary>
        public void ModificarFactura_AparatosInproductivos(ref Factura factura, ref DataTable listaFacturaDet, ref DataTable listaFacturaDetAdenda
            , int CantidadR
            , string Conexion
            , ref int verificador
            , ref EntradaSalida entSal
            , ref List<EntradaSalidaDetalle> listaEntSalDetalle
            , ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listAdendaCabecera, string idRF, string arrayRemisiones)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ModificarFactura_AparatosInproductivos(ref factura, ref listaFacturaDet, ref listaFacturaDetAdenda, CantidadR, Conexion, ref verificador, ref entSal
                    , ref listaEntSalDetalle, ref listaEntSalRemisiones, listAdendaCabecera, idRF, arrayRemisiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Actualizar factura con pedido previo, tipo MOv. 16, (aparatos in productivos)
        /// </summary>
        public void InsertarFactura_AparatosInproductivos(ref Factura factura
            , ref DataTable listaFacturaDet
            , ref DataTable listaFacturaDetAdenda
             , int CantidadR
            , string Conexion
            , ref int verificador
            , ref EntradaSalida entSal
            , ref List<EntradaSalidaDetalle> listaEntSalDetalle
            , ref List<EntradaSalida> listaEntSalRemisiones, List<AdendaDet> listCabecera,
        string IdRF, string arrRemisiones)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.InsertarFactura_AparatosInproductivos(ref factura, ref listaFacturaDet, ref listaFacturaDetAdenda, CantidadR, Conexion, ref verificador, ref entSal
                    , ref listaEntSalDetalle, ref listaEntSalRemisiones, listCabecera, IdRF, arrRemisiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFactura_Estatus(Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ModificarFactura_Estatus(factura, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaSAT(Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ModificarFacturaSAT(factura, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSAT(ref Factura factura, string Conexion, ref object resultado)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarFacturaSAT(ref factura, Conexion, ref resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaSAT(ref Factura factura, string Conexion, ref object resultado, ref object resultadoCN)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarFacturaSAT(ref factura, Conexion, ref resultado, ref resultadoCN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Eliminación de factura, Baja lógica
        /// </summary>
        public void EliminarFactura(ref Factura factura, string Conexion, ref int verificador, ref EntradaSalida entSal
            , ref List<EntradaSalidaDetalle> listaEntSalDetalle)
        {
            try
            {
                new CD_CapFactura().EliminarFactura(ref factura, Conexion, ref verificador, ref entSal, ref listaEntSalDetalle);//, ref notaCredito, ref listaNotaCredDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Actualiza_Factura_AcuseCancelacionSAT(Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapFactura().Actualiza_Factura_AcuseCancelacionSAT(factura, Conexion, ref verificador);//, ref notaCredito, ref listaNotaCredDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogError_Insertar(string clave, string error, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.LogError_Insertar(clave, error, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rastreo(ref Factura fac, string Conexion, int tipoBusqueda)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.Rastreo(ref fac, Conexion, tipoBusqueda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Fac, string Tipo1, string Tipo2, ref List<AdendaDet> listCabT, ref List<AdendaDet> listDetT, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Id_Fac, Tipo1, Tipo2, ref listCabT, ref listDetT, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdendaNacional(int Id_Emp, int Id_Cd_Ver, int Id_Fac, string Tipo1, string Tipo2, ref List<AdendaDet> listCabT, ref List<AdendaDet> listDetT, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarAdendaNacional(Id_Emp, Id_Cd_Ver, Id_Fac, Tipo1, Tipo2, ref listCabT, ref listDetT, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DisponibleFacturar(Sesion sesion, Factura fac2, int prd, ref int disponibleFacturar)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.DisponibleFacturar(sesion, fac2, prd, ref disponibleFacturar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FacturaRemision_Nota(Factura factura_remision, string Conexion, ref string agregado_nota)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.FacturaRemision_Nota(factura_remision, Conexion, ref agregado_nota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ArchivoPdf_Xml(ref Factura fac, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ArchivoPdf_Xml(ref fac, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FacturaVI_ValidadorRequisicion(Sesion session, Factura fac, ref bool requisicion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.FacturaVI_ValidadorRequisicion(session, fac, ref requisicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void revisionEspeciales(Sesion sesion, long Id_Prd, int Id_Ter, int id_Fac, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.revisionEspeciales(sesion, Id_Prd, Id_Ter, id_Fac, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaMontosImpresion(Factura factura, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool verificador)
        {
            try
            {
                new CD_CapFactura().ValidaMontosImpresion(factura, Id_Cd, Id_Emp, iTipoDocumento, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCorreoUsuarioAutoriza(Factura factura, string conexion, ref string correo)
        {
            try
            {
                new CD_CapFactura().ConsultaCorreoUsuarioAutoriza(factura, conexion, ref correo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstatus(int Id_Fac, Sesion sesion, ref string Fac_Estatus)
        {
            try
            {
                new CD_CapFactura().ConsultaEstatus(Id_Fac, sesion, ref Fac_Estatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaMontoSAT(Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ModificarMontosFactura(factura, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelaFacturaCtaNac(ref Factura factura, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.CancelaFacturaCtaNac(factura, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        // ABR13-2020 RFH
        //
        //public DocumentosSinTimbar Consulta_ValidacionFacturas(int Id_Emp, int Id_Cd, string Conexion)
        //{
        //    CD_CapFactura CD = new CD_CapFactura();
        //    return CD.Consulta_ValidacionFacturas(Id_Emp, Id_Cd, Conexion);
        //}

        //

        #region FacturaSegundoPlano

        public void ActivaFacturaSegundoPlano(string Conexion, int idUsuario, int intConDescarga, ref FacturaLite entFacturaLite, ref int idGrupoFac)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ActivaFacturaSegundoPlano(Conexion, idUsuario, intConDescarga, ref entFacturaLite, ref idGrupoFac);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesactivaFacturaSegundoPlano(string Conexion, ref FacturaLite entFacturaLite, string strUrlPdf, string strErrorMensaje, string strErrorDetalle, int idGrupoFac)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.DesactivaFacturaSegundoPlano(Conexion, ref entFacturaLite, strUrlPdf, strErrorMensaje, strErrorDetalle, idGrupoFac);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ConsultaFacturaSegundoPlano(string Conexion, int IdUsuario, string strPath, ref FacturaLite entFacturaLite, ref string strMsjResultado, ref List<FacturaLite> lstFacturaLite)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaSegundoPlano(Conexion, IdUsuario, strPath, ref entFacturaLite, ref strMsjResultado, ref lstFacturaLite);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        public void ConsultaFacturaSegundoPlanoEstatus(string Conexion, ref FacturaLite entFacturaLite)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaSegundoPlanoEstatus(Conexion, ref entFacturaLite);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion PagoSegundoPlano

        public void ConsultaFacturaMin(ref Factura entFactura, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultaFacturaMin(ref entFactura, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RastreoDev(ref Factura fac, string Conexion)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.RastreoDev(ref fac, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 
        public void ConsultarMotivo(string Conexion, int intTipoMotivo, ref List<Comun> lstMotivo)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();

                claseCapaDatos.ConsultarMotivo(Conexion, intTipoMotivo, ref lstMotivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarMotivoCancelacionFactura(string Conexion, int intIdEmp, int intIdCd, int id_Fac, int Id_MotivoBaja, int Id_Fac_Reemplaza)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.GuardarMotivoCancelacionFactura(Conexion, intIdEmp, intIdCd, id_Fac, Id_MotivoBaja, Id_Fac_Reemplaza);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarFacturaRelacionada(string Conexion, int intIdEmp, int intIdCd, int intIdFac, int Id_Fac_Reemplaza, ref bool boolFolioValido, ref string strMensaje)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarFacturaRelacionada(Conexion, intIdEmp, intIdCd, intIdFac, Id_Fac_Reemplaza, ref boolFolioValido, ref strMensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMotivoBaja(string Conexion, ref Factura entFactura)
        {
            try
            {
                CD_CapFactura claseCapaDatos = new CD_CapFactura();
                claseCapaDatos.ConsultarMotivoBaja(Conexion, ref entFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}