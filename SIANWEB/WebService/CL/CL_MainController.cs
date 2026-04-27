using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using System.Data;
using System.Configuration;

namespace SIANWEB.WebService
{
    public class CL_MainController : ApiController
    {

        // 24 May 2022
        [HttpGet]
        public eResponse<int> InsertarSolicitud_CL(
            int IdMotivo,
            long Id_PrdOriginal,
            long Id_Prd,
            float Det_Costo,
            int Det_Estatus,
            int Accion,
            string Prd_Descripcion,
            int IdTipoProducto,
            string TipoProducto,
            string Familia,
            string SubFamilia, //10
            string Vigencia,
            int IdCausaDesabasto,
            string MotivoDesabasto,
            string SAT_CveUnidad,
            string SAT_CveProdServ,
            int IdProveedor, // 17
            string ProviderName,
            long IdArray_ClientesExclusivos,
            string PedidoReferencia,
            int IdAplicacion,
            string ProveedorCentral,
            string CodigoProductoProv,
            string DescripcionProductoProv,
            string PresentacionProductoProv
            )
        {
            CompraLocal CL = new CompraLocal();
            eCL_CompraLocalDet CL_D = new eCL_CompraLocalDet();
            List<eCL_CompraLocalDet> lstProducto = new List<eCL_CompraLocalDet>();

            eResponse<int> result = new eResponse<int>();
            int res = 0;
            result.Estado = 0;
            result.Mensaje = "";
            int CveSol = 0;
            try
            {

                int V;
                V = 0;

                List<eCL_ResponsableAutorizador> LstAutorizadores = new List<eCL_ResponsableAutorizador>();
                CN_ComprasLocales CN_Aut = new CN_ComprasLocales();
                switch (IdMotivo)
                {
                    case 1:
                        LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, IdMotivo, IdAplicacion);
                        break;
                    case 2:
                        LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, IdMotivo, IdAplicacion);
                        break;
                    case 3:
                        LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, IdMotivo, IdAplicacion);
                        break;
                    case 4:
                        LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, IdMotivo, IdAplicacion);
                        break;
                }

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd;
                CL.FechaSol = DateTime.Today;
                CL.Comp_Solicito = Sesion.Id_U;
                CL_D.Id_Emp = Sesion.Id_Emp;
                CL_D.Id_Cd = Sesion.Id_Cd;
                CL_D.Id_Comp = 0;
                CL_D.Id_CompDet = 1;
                CL_D.Id_Prd = Id_Prd;
                CL_D.Det_Costo = Det_Costo;
                CL_D.Det_Estatus = 0;
                CL_D.Det_Enfocada = 0;
                CL_D.Det_FecAut = DateTime.Today.ToString();
                CL_D.Det_Autorizo = 0;
                CL.Id_U = Sesion.Id_U;

                // En Motivo:  1, 2 y 4 
                if (IdMotivo != 3)
                {
                    CL_D.Id_PrdOriginal = 0;
                }
                else
                {
                    CL_D.Id_PrdOriginal = Id_PrdOriginal;
                }
                CL_D.IdTipoProducto = IdTipoProducto;
                CL_D.IdUsuarioAutorizador = LstAutorizadores[0].ResponsableIdUsuario;
                lstProducto.Add(CL_D);

                CN_ProCompraLocal CN = new CN_ProCompraLocal();
                CN.InsertarSolicitud_ver2(
                    CL, lstProducto, CveSol, IdCausaDesabasto, MotivoDesabasto, Vigencia,
                    IdMotivo, PedidoReferencia, IdProveedor, Sesion.Emp_Cnx, ref CveSol);

                if (CveSol > 0)
                {
                    // guarda una copia del original vs el clon para 

                    CN_ComprasLocales clLocales = new CN_ComprasLocales();

                    // En Motivo:  1, 2 y 4 
                    if (IdMotivo != 3)
                    {
                        clLocales.ProductosClonados(CveSol, Id_PrdOriginal, Id_Prd, Sesion.Emp_Cnx);
                    }

                    int verificador_GTCL = 0;
                    int grabo = 0;

                    clLocales.GrabaDatosProductoSAT_ver2(
                        CveSol, Id_Prd, SAT_CveUnidad, SAT_CveProdServ, Sesion.Emp_Cnx);

                    // Correos de RESPONSABLE de AUTORIZAR  - Motivo - Define el tipo de correo a enviar

                    clLocales.Correo_SolicitudDeCompra_ver2(
                        IdMotivo,
                        CveSol,
                        Sesion.Id_Cd,
                        Sesion.Cd_Nombre,
                        Sesion.Id_U,
                        Sesion.U_Nombre,
                        IdProveedor,
                        ProviderName,
                        Id_Prd.ToString(),
                        Id_PrdOriginal.ToString(),
                        Prd_Descripcion,
                        TipoProducto, //12
                        Familia,
                        SubFamilia,
                        Vigencia,
                        MotivoDesabasto,
                        IdArray_ClientesExclusivos,
                        IdAplicacion,
                        LstAutorizadores,
                        Sesion.Emp_Cnx,
                        ProveedorCentral,
                        CodigoProductoProv,
                        DescripcionProductoProv,
                        PresentacionProductoProv
                        );

                    string AutorizacionCL = ConfigurationManager.AppSettings["ProcesoAutorizacionCL1"].ToString();

                    //Si el tipo de solicitud es 1 y la variable en webconfig es 0, las solicitudes se autorizaran en automatico
                    //de lo contrario entraran al proceso normal
                    if (AutorizacionCL == "0")
                    {
                        CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                        cn_Listadocompralocal.AutorizaSolicitud(CveSol.ToString(), Sesion.Id_Emp, Sesion.Id_Cd, 9999, Vigencia, Sesion.Emp_Cnx);
                    }
                }

                result.Mensaje = CveSol.ToString() + " del producto " + Id_Prd.ToString() + " - " + Id_PrdOriginal.ToString();
                result.Datos = CveSol;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error:" + ex.ToString();
            }
            return result;
        }

        //FEB14-2020
        // Autorizacion de Compra Local

        [HttpGet]
        public eResponse<int> CL_spProCompraLocalDet_Modificar(
            int Id_Comp,
            string Estatus,  // A.- Autorizad,  R.- Rechazada
            string Vigencia)
        {
            eResponse<int> result = new eResponse<int>();
            int Res = 0;

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                // LISTADO 
                List<ProductoLocal> List = new List<ProductoLocal>();
                CN_ProCompraLocal clsProCompraLocal = new CN_ProCompraLocal();
                clsProCompraLocal.ConsultaCompraLocalList(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_Comp, Sesion.Emp_Cnx, ref List);

                DateTime CurrentTime = DateTime.UtcNow;

                foreach (ProductoLocal PL in List)
                {
                    PL.Autorizo = Sesion.Id_U;
                    PL.FechaAut = CurrentTime.AddMinutes(Sesion.Minutos).ToString();
                    PL.Estatus = Estatus;
                    //List.Add(PL);
                }

                int verificador = 0;
                if (List != null)
                {
                    // graba el cambio en la fecha de vigencia
                    // Actualiza VIGENCIA
                    int grabo = 0;
                    string FVigencia = "";
                    CN_ComprasLocales comLoc = new CN_ComprasLocales();
                    if (Vigencia != "" && Vigencia != null)
                    {
                        FVigencia = Vigencia;
                        comLoc.GrabaVigencia(FVigencia, Id_Comp, Sesion.Emp_Cnx, ref grabo);
                    }

                    // Ejecuta MODIFICACION                     
                    verificador = -1;
                    CompraLocal cl = new CompraLocal();
                    cl.Id_Emp = Sesion.Id_Emp;
                    cl.Id_Cd = Sesion.Id_Cd_Ver;
                    cl.Id_Comp = Id_Comp;
                    CN_ProCompraLocal CN_PCL = new CN_ProCompraLocal();
                    CN_PCL.ModificarCompraLocal(cl, List, Sesion.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        //EnviaEmail(grabo, FVigencia);
                    }
                }
                result.Datos = 1;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = -1;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //

        [HttpGet]
        public eResponse<string> Get_CodigoHomologado_Maximo_Consulta(int Id_Prov)
        {
            eResponse<string> result = new eResponse<string>();
            string res;
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                res = CN.SelCL_CodigoHomologado_Maximo_Consulta(Id_Prov, Sesion.Emp_Cnx);
                result.Datos = res;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //

        [HttpGet]
        public eResponse<List<CapaEntidad.eCL_Motivo>> Get_ListadoMotivo(int Id_MotivoCL)
        {
            eResponse<List<CapaEntidad.eCL_Motivo>> result = new eResponse<List<CapaEntidad.eCL_Motivo>>();
            List<CapaEntidad.eCL_Motivo> lst = new List<CapaEntidad.eCL_Motivo>();

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoMotivo(Id_MotivoCL, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //

        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> Get_ListadoCausaDesabasto(int Id_Causa)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoCausaDesabasto(Id_Causa, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }


        //

        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> Get_ListadoTipoProducto(int Id_TipoProducto, int Id_Emp)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoTipoProducto(1, Sesion.Id_Emp, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // APLICACION o FAMILIA 

        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> Get_ListadoProductoFamiliaCte(int Id_ProductoFamiliaCte, int Id2)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoProductoFamiliaCte(0, 0, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //
        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> Get_ListadoProductoSubFamilia(int Id1, int Id2, int Id3)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoProductoSubFamilia(Id1, Id2, Id3, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //

        //[HttpGet]
        //public eResponse<List<CapaEntidad.eListaGenerica>> Get_ListadoProductoSubFamilia(int Id1, int Id2, int Id3)
        //{
        //    eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
        //    List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();

        //    result.Estado = 0;
        //    result.Mensaje = "";

        //    try
        //    {
        //        CN_Compras_Locales CN = new CN_Compras_Locales();
        //        lst = CN.Get_ListadoProductoSubFamilia(Id1, Id2, Id3, Sesion.Emp_Cnx);

        //        result.Datos = lst;
        //        result.Estado = 1;
        //    }
        //    catch
        //    {
        //        result.Datos = null;
        //        result.Estado = -1;
        //        result.Mensaje = "Error";
        //    }
        //    return result;
        //}


        // Compras Locales
        // ENE21-2020 RFH -  Busqueda de producto 

        [HttpGet]
        public eResponse<List<CapaEntidad.Producto>> Get_BusquedaProducto(string Termino)
        {
            eResponse<List<CapaEntidad.Producto>> result = new eResponse<List<CapaEntidad.Producto>>();
            List<CapaEntidad.Producto> lst = new List<CapaEntidad.Producto>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatProducto CN = new CN_CatProducto();
                lst = CN.BusquedaDeProducto(Termino, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // 7Abr2022 RFH
        [HttpGet]
        public eResponse<int> spCL_ConsultarProducto(string CodigoProducto, int Param2)
        {
            eResponse<int> result = new eResponse<int>();
            int iContar = 0;
            Int64 iCodigoProducto = 0;
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = -1;
            try
            {
                Int64.TryParse(CodigoProducto, out iCodigoProducto);
                CN_CatProducto CN = new CN_CatProducto();
                iContar = CN.spCL_ConsultarProducto(Sesion.Id_Emp, Sesion.Id_Cd, iCodigoProducto, Sesion.Emp_Cnx);
                result.Datos = iContar;
                result.Estado = 1;
            }
            catch (Exception e)
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = e.Message.ToString();
            }
            return result;
        }

        // ENE21-2020 RFH 
        // Compras Locales -  Consulta Producto 
        [HttpGet]
        public eResponse<CapaEntidad.Producto> Get_ConsultaProductoById(string Id_Prd, bool Catalogo)
        {
            eResponse<CapaEntidad.Producto> result = new eResponse<CapaEntidad.Producto>();
            CapaEntidad.Producto lst = new CapaEntidad.Producto();
            result.Estado = 0;
            result.Mensaje = "";
            int Validador = 0;
            try
            {
                Int64 iPrd = 0;
                Int64.TryParse(Id_Prd, out iPrd);
                CN_CatProducto CN = new CN_CatProducto();
                lst = CN.ConsultaProductoById(ref Validador, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Cd_Ver, iPrd, Catalogo, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = Validador;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE21-2020 RFH -  Busqueda de producto 

        [HttpGet]
        public eResponse<string> Get_NuevoCodigoProducto(string Categoria, string Proveedor, Int64 Producto)
        {
            eResponse<string> result = new eResponse<string>();
            string lst = "";

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                lst = CN.NuevoCodigoProducto_ver2(
                    Sesion.Id_Emp, Sesion.Id_Cd, Categoria, Proveedor, Producto, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE21-2020 RFH -  Busqueda de producto 

        [HttpGet]
        public eResponse<string> Get_MaximoId(Int64 IdProd, string Categoria, string Proveedor)
        {
            eResponse<string> result = new eResponse<string>();
            string lst = "";

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                if (Proveedor == "-1")
                {
                    Proveedor = "";
                }
                CN_ComprasLocales CN = new CN_ComprasLocales();
                lst = CN.NuevoCodigoProducto_ver2(Sesion.Id_Emp, Sesion.Id_Cd, Categoria, Proveedor, IdProd, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE21-2020 RFH -  Busqueda de proveedores

        [HttpGet]
        public eResponse<List<CapaEntidad.CatProveedorLocal>> spProveedores_ComboCompraLocal(int IdProveedorLocal)
        {
            eResponse<List<CapaEntidad.CatProveedorLocal>> result = new eResponse<List<CapaEntidad.CatProveedorLocal>>();
            List<CapaEntidad.CatProveedorLocal> lst = new List<CapaEntidad.CatProveedorLocal>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatProveedores CN = new CN_CatProveedores();
                lst = CN.spProveedores_ComboCompraLocal(IdProveedorLocal, Sesion.Id_Emp, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales / Unidades de Medida 
        // ENE21-2020 RFH -  Busqueda de proveedores

        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> spLlenarComboUnidades(int IdUnidad, int Id1, int Id2)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatUnidad CN = new CN_CatUnidad();
                lst = CN.spCatCL_UnidadMedida(IdUnidad, 1, Sesion.Id_Emp, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }


        // Compras Locales
        // ENE21-2020 RFH -  Unidad Medida SAT

        [HttpGet]
        public eResponse<List<CapaEntidad.eUnidadMedidaSat>> spSATUnidadesMedida(string CveUnidad)
        {
            eResponse<List<CapaEntidad.eUnidadMedidaSat>> result = new eResponse<List<CapaEntidad.eUnidadMedidaSat>>();
            List<CapaEntidad.eUnidadMedidaSat> lst = new List<CapaEntidad.eUnidadMedidaSat>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatUnidad CN = new CN_CatUnidad();
                lst = CN.spSATUnidadesMedida(CveUnidad, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE28-2020 RFH -  

        [HttpGet]
        public eResponse<List<CapaEntidad.eCL_SATProductosYServicios>> spSATProductosYServicios(string CveProdServ)
        {
            eResponse<List<CapaEntidad.eCL_SATProductosYServicios>> result = new eResponse<List<CapaEntidad.eCL_SATProductosYServicios>>();
            List<CapaEntidad.eCL_SATProductosYServicios> lst = new List<CapaEntidad.eCL_SATProductosYServicios>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_CatProductosYServiciosSAT(CveProdServ, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE21-2020 RFH -  Consulta de Pedidos  

        [HttpGet]
        public eResponse<List<CapaEntidad.PreciosComprasLocales>> spCL_PreciosComprasLocales(string Id_Prd, string Param2)
        {
            eResponse<List<CapaEntidad.PreciosComprasLocales>> result = new eResponse<List<CapaEntidad.PreciosComprasLocales>>();
            List<CapaEntidad.PreciosComprasLocales> lst = new List<CapaEntidad.PreciosComprasLocales>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                Int64 i64Id_Producto = 0;
                Int64.TryParse(Id_Prd, out i64Id_Producto);

                PreciosComprasLocales producto = new PreciosComprasLocales();
                producto.Id_Emp = Sesion.Id_Emp;
                producto.Id_Cd = Sesion.Id_Cd;
                producto.Id_Prd = i64Id_Producto;

                CN_ProductoPrecios CN = new CN_ProductoPrecios();
                lst = CN.spCL_ProductoConsultaPreciosCL(producto, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<CapaEntidad.ProductoPrecios>> spCL_ProductoConsultaPrecios(string Id_Producto, string Param2)
        {
            eResponse<List<CapaEntidad.ProductoPrecios>> result = new eResponse<List<CapaEntidad.ProductoPrecios>>();
            List<CapaEntidad.ProductoPrecios> lst = new List<CapaEntidad.ProductoPrecios>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                Int64 i64Id_Producto = 0;
                Int64.TryParse(Id_Producto, out i64Id_Producto);

                ProductoPrecios producto = new ProductoPrecios();
                producto.Id_Emp = Sesion.Id_Emp;
                producto.Id_Cd = Sesion.Id_Cd;
                producto.Id_Prd = i64Id_Producto;

                CN_ProductoPrecios CN = new CN_ProductoPrecios();
                lst = CN.CL_ConsultaListaProductoPrecios(producto, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // ENE21-2020 RFH 

        [HttpGet]
        public eResponse<int> CL_ChecarProductoYaSolicitado(string CodigoUsadoProd, int Param2)
        {
            eResponse<int> result = new eResponse<int>();
            int res = 0;
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                long i64Id_Producto = 0;
                long.TryParse(CodigoUsadoProd, out i64Id_Producto);

                CN_ComprasLocales CN = new CN_ComprasLocales();
                res = CN.CompraLocalPedidosProducto_ChecaDuplicado(i64Id_Producto, Param2, Sesion.Emp_Cnx);

                result.Datos = res;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = -1;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }


        // Compras Locales
        // ENE21-2020 RFH 

        [HttpGet]
        public eResponse<string> CL_LlenarProdcutosHermanos(string Producccto, int Param1, int Param2)
        {
            eResponse<string> result = new eResponse<string>();
            string res = "";
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                long i64Id_Producto = 0;
                long.TryParse(Producccto, out i64Id_Producto);

                CN_ComprasLocales CN = new CN_ComprasLocales();
                res = CN.LlenarProdcutosHermanos(i64Id_Producto, Sesion.Emp_Cnx);

                result.Datos = res;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = "";
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpPut]
        public eResponse<int> CL_InsertarProducto(Producto Prod)
        {
            eResponse<int> result = new eResponse<int>();
            int res = 0;
            result.Estado = 0;
            result.Mensaje = "";
            int verificador = 0;
            try
            {
                Prod.Fecha = DateTime.Today;
                Prod.Id_Emp = Sesion.Id_Emp;
                Prod.Id_Cd = Sesion.Id_Cd;
                CN_CatProducto CN = new CN_CatProducto();
                //CN.InsertarProducto_CL_ver2(Sesion.Id_Emp, Sesion.Id_Cd, Prod, Sesion.Emp_Cnx, ref verificador); 
                CN.InsertarProducto_CL_ver3(Sesion.Id_Emp, Sesion.Id_Cd, Prod, Sesion.Emp_Cnx, ref verificador);

                result.Datos = res;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = -1;
                result.Estado = -1;
                result.Mensaje = "Error:" + ex.ToString();
            }
            return result;
        }

        // Compras Locales
        // FEB11-2020 RFH 
        [HttpGet]
        public eResponse<int> CL_GrabaComentariosCliente(
            int Id_Solicitud, string Comentario, string Vigencia, int TipoSolicitud, string PedidoReferencia)
        {
            eResponse<int> result = new eResponse<int>();
            string res = "";
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.GrabaComentariosCliente(Comentario, Vigencia, TipoSolicitud, Id_Solicitud, Sesion.Emp_Cnx, ref Verifica);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // FEB11-2020 RFH 

        [HttpGet]
        public eResponse<int> CL_GrabaSoloComentariosCliente(
            int Id_Solicitud, string Comentario)
        {
            eResponse<int> result = new eResponse<int>();
            string res = "";
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.GrabaSoloComentariosCliente(Comentario, Id_Solicitud, Sesion.Emp_Cnx, ref Verifica);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<int> CL_InsertClienteExclusivo(
            int IdCte, string Nombre, long Id_Sol, string TipoCliente)
        {
            eResponse<int> result = new eResponse<int>();
            string res = "";
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.CL_InsertClienteExclusivo(IdCte, Nombre, Id_Sol, TipoCliente, Sesion.Emp_Cnx);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<int> CL_InsertClienteExclusivo_UpdateSol(
            string KeyArray_ClienteExclusivos, long Id_Solicitud)
        {
            eResponse<int> result = new eResponse<int>();
            string res = "";
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.CL_InsertClienteExclusivo_UpdateSol(
                    KeyArray_ClienteExclusivos, Id_Solicitud, Sesion.Emp_Cnx);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }


        // Guarda los datos de SAT        
        [HttpGet]
        public eResponse<int> CL_GrabaTipoCompraLocal(
            int Id_Solicitud, int Id_TipoCompra, string Comentario)
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            int grabo = 0;
            try
            {
                DateTime CurrentTime = DateTime.UtcNow;
                CurrentTime.AddMinutes(Sesion.Minutos);

                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = Sesion.Id_Emp;
                cl.Id_Cd = Sesion.Id_Cd_Ver;
                cl.FechaSol = CurrentTime;
                cl.Comp_Solicito = Sesion.Id_U;

                CN_ComprasLocales comLoc = new CN_ComprasLocales();

                comLoc.GrabaTipoCompraLocal(Id_Solicitud, Id_TipoCompra, Sesion.Emp_Cnx, ref grabo);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // Compras Locales
        // FEB7-2020 RFH 

        [HttpGet]
        public eResponse<List<CapaEntidad.Producto>> CL_spAABuscaProductosCompraLocalTodos(long Id_Prd, int Param1, int Param2)
        {
            eResponse<List<CapaEntidad.Producto>> result = new eResponse<List<CapaEntidad.Producto>>();
            List<CapaEntidad.Producto> lst = new List<CapaEntidad.Producto>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatProducto CN = new CN_CatProducto();
                lst = CN.CL_spAABuscaProductosCompraLocalTodos(0, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        //         

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


        //RBM Nov 2023
        //Consulta presentacion de producto para compras locales
        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> spPresentacion_ComboCompraLocal()
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatProducto CN = new CN_CatProducto();
                lst = CN.spPresentacion_ComboCompraLocal(Sesion.Id_Emp, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<CapaEntidad.eComprasLocales>> Get_ComprasLocales_Index(
            int PageNo,
            int PageSize,
            string Vencido,
            Int64 Id_Prd,
            int IdProveedorLocal,
            int Id_Motivo,
            int Id_Comp,
            int Id_Estatus
            )
        {
            eResponse<List<CapaEntidad.eComprasLocales>> result = new eResponse<List<CapaEntidad.eComprasLocales>>();
            List<CapaEntidad.eComprasLocales> lst = new List<CapaEntidad.eComprasLocales>();

            result.Estado = 0;
            result.Mensaje = "";
            int Verificador = 0;

            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ComprasLocales_Index(
                    Sesion.Id_U, // UsuarioQueConsulta 
                    Sesion.Id_Emp,
                    Sesion.Id_Cd,
                    PageNo,
                    PageSize,
                    Vencido,
                    Id_Prd,
                    IdProveedorLocal,
                    Id_Motivo,
                    Id_Comp,
                    Id_Estatus,
                    ref Verificador,
                    Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = Verificador;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<CompraLocal> Get_DetalleCL(int Id_Comp, int Id_Cd, int AutorizadorId)
        {
            eResponse<CompraLocal> result = new eResponse<CompraLocal>();
            List<CapaEntidad.CompraLocal> lst = new List<CapaEntidad.CompraLocal>();
            result.Estado = 0;
            CompraLocal CL = new CompraLocal();
            try
            {
                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Id_Cd;
                CL.Id_Comp = Id_Comp;
                if (Sesion.Id_U == AutorizadorId)
                    CL.PermisoAutorizar = 1;
                else
                    CL.PermisoAutorizar = 0;


                CN_Compras_Locales CN = new CN_Compras_Locales();
                CN.Get_DetalleCL(ref CL, Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = CL;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<ClienteExclusivo>> Get_DatosClientesExclusivosCL(int Id_Comp)
        {
            eResponse<List<ClienteExclusivo>> result = new eResponse<List<ClienteExclusivo>>();
            List<CapaEntidad.ClienteExclusivo> lst = new List<CapaEntidad.ClienteExclusivo>();
            DataSet dsDatos = new DataSet();

            result.Estado = 0;
            CompraLocal CL = new CompraLocal();
            try
            {
                CL.Id_Comp = Id_Comp;

                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ClientesExclusivosCL(ref CL, Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        [HttpGet]
        public eResponse<CapaEntidad.Producto> Get_ConsultaCompraLocalById(int Id_Comp, int Id_Cd, string Id_Prd, bool Catalogo)
        {
            eResponse<CapaEntidad.Producto> result = new eResponse<CapaEntidad.Producto>();
            CapaEntidad.Producto lst = new CapaEntidad.Producto();
            result.Estado = 0;
            result.Mensaje = "";
            int Validador = 0;
            try
            {
                Int64 iPrd = 0;
                Int64.TryParse(Id_Prd, out iPrd);
                CN_CatProducto CN = new CN_CatProducto();
                lst = CN.ConsultaCompraLocalById(ref Validador, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Cd_Ver, iPrd, Catalogo, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = Validador;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // GetLogs  
        [HttpGet]
        public eResponse<List<eCLLogs>> GetLogs(int Id_Comp, int Id_Cd, int LogId)
        {
            eResponse<List<eCLLogs>> result = new eResponse<List<eCLLogs>>();
            result.Estado = 0;
            List<eCLLogs> lst = new List<eCLLogs>();
            try
            {
                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                lst = CN_CL.spComprasLocales_GetLogs(
                    Sesion.Id_Emp, Id_Cd, Id_Comp, Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        [HttpGet]
        public eResponse<int> spCLCen_ModificarCompraLocal(string KeyArray_ClienteExclusivos, int Id_Cd, int Id_Comp, string Id_Prd, string Prd_FechaInicio, string Prd_FechaFin, string Prd_ClaveUnidad, string Prd_ClaveProdServ)
        {
            eResponse<int> result = new eResponse<int>();
            int Res = 0;

            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = 0;

            try
            {
                CompraLocal CL = new CompraLocal();
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.ActualizarCompraLocal(Sesion.Id_Emp, Id_Cd, Id_Comp, Id_Prd, Prd_FechaInicio, Prd_FechaFin, Prd_ClaveUnidad, Prd_ClaveProdServ, Sesion.Emp_Cnx, ref Res, ref CL);

                if (Id_Comp > 0)
                {

                    int V;
                    V = 0;

                    int IdAplicacion = int.Parse(CL.IdAplicacion);
                    List<eCL_ResponsableAutorizador> LstAutorizadores = new List<eCL_ResponsableAutorizador>();
                    CN_ComprasLocales CN_Aut = new CN_ComprasLocales();
                    switch (CL.IdTipoSolicitud)
                    {
                        case 1:
                            LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, CL.IdTipoSolicitud, IdAplicacion);
                            break;
                        case 2:
                            LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, CL.IdTipoSolicitud, IdAplicacion);
                            break;
                        case 3:
                            LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, CL.IdTipoSolicitud, IdAplicacion);
                            break;
                        case 4:
                            LstAutorizadores = CN_Aut.CorreosAutorizador_xMotivoxApp(ref V, CL.IdTipoSolicitud, IdAplicacion);
                            break;
                    }
                    // guarda una copia del original vs el clon para 
                    CN_ComprasLocales clLocales = new CN_ComprasLocales();

                    // Correos de RESPONSABLE de AUTORIZAR  - Motivo - Define el tipo de correo a enviar
                    clLocales.Correo_SolicitudesEditadas(
                        CL.IdTipoSolicitud, //IdMotivo
                        Id_Comp,         //CveSol
                        Sesion.Id_Cd,
                        Sesion.Cd_Nombre,
                        Sesion.Id_U,
                        Sesion.U_Nombre,
                        1,                     //IdProveedor,
                        CL.ProveedorLocal,     //ProviderName,
                        CL.Id_Prd.ToString(),  //Id_Prd.ToString(),
                        CL.CodigoPadre, //Id_PrdOriginal.ToString(),
                        CL.Prd_Descripcion, //Prd_Descripcion,
                        CL.NomTipoProd, //TipoProducto, //12
                        CL.Aplicacion, //Familia,
                        CL.Prd_SubFamilia, //SubFamilia,
                        CL.Vigencia.ToString().Substring(0, 10),   //Vigencia,
                        CL.Comentarios,  //MotivoDesabasto,
                        Convert.ToInt64(KeyArray_ClienteExclusivos),
                        IdAplicacion, //IdAplicacion,
                        LstAutorizadores, //LstAutorizadores,
                        Sesion.Emp_Cnx,
                        CL.ProveedorCentral, //ProveedorCentral,
                        CL.Prd_CodigoProv, //CodigoProductoProv,
                        CL.Prd_DescripcionProv,//DescripcionProductoProv,
                        CL.Prd_PresentacionProv//PresentacionProductoProv
                        );

                    string AutorizacionCL = ConfigurationManager.AppSettings["ProcesoAutorizacionCL1"].ToString();

                    //Si el tipo de solicitud es 1 y la variable en webconfig es 0, las solicitudes se autorizaran en automatico
                    //de lo contrario entraran al proceso normal
                    if (AutorizacionCL == "0")
                    {
                        CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                        cn_Listadocompralocal.AutorizaSolicitudEditar(Id_Comp.ToString(), Sesion.Id_Emp, Sesion.Id_Cd, 9999, CL.Vigencia.ToString().Substring(0, 10), Sesion.Emp_Cnx);
                    }
                }

                result.Datos = Id_Comp;
                result.Estado = 1;

            }
            catch (Exception ex)
            {
                result.Datos = -1;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        [HttpGet]
        public eResponse<int> CL_UpdatePreciosCompraLocal(int Id_Cd, string Id_Prd, int Id_Pre, string Prd_FechaInicio, string Prd_FechaFin, float Prd_Pesos)
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            int Verifica = 0;
            try
            {
                CN_ComprasLocales CN = new CN_ComprasLocales();
                CN.CL_UpdatePreciosCompraLocal(Sesion.Id_Emp, Id_Cd, Id_Prd, Id_Pre, Prd_FechaInicio, Prd_FechaFin, Prd_Pesos, Sesion.Emp_Cnx);

                result.Datos = Verifica;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

    }
}