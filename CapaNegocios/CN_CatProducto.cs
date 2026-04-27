using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI.WebControls;

using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatProducto
    {

        // 10Dic-2021 RFH 
        public List<Producto> spCaptacionPedido_ProductosSubstituto(long Id_Prd, int Id_Cd, string Conexion)
        {
            CD_CatProducto CD = new CD_CatProducto();
            return CD.spCaptacionPedido_ProductosSubstituto(Id_Prd, Id_Cd, Conexion);
        }


        // ENE21-2020 RFH -  CL Busqueda de producto 
        public List<CapaEntidad.Producto> BusquedaDeProducto(string Termino, string Conexion)
        {
            CD_CatProducto claseCapaDatos = new CD_CatProducto();
            return claseCapaDatos.BusquedaDeProducto(Termino, Conexion);
        }
        // 7Abr-2022 RFH Compras Locales
        public int spCL_ConsultarProducto(int Id_Emp, int Id_Cd, Int64 Id_Prd, string Conexion)
        {
            CD_CatProducto CD = new CD_CatProducto();
            return CD.spCL_ConsultarProducto(Id_Emp, Id_Cd, Id_Prd, Conexion);
        }

        // FEB7-2020 RFH Compras Locales
        public List<CapaEntidad.Producto> CL_spAABuscaProductosCompraLocalTodos(long Id_Prd, string Conexion)
        {
            CD_CatProducto CD = new CD_CatProducto();
            return CD.Busqueda_spAABuscaProductosCompraLocalTodos(Id_Prd, Conexion);
        }

        public void ConsultaProducto_OrdenCompra(ref Producto producto, string Conexion, int id_Ord, long id_Prd, int id_Emp, int id_Cd)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_OrdenCompra(ref producto, Conexion, id_Ord, id_Prd, id_Emp, id_Cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta una transacción de la capa de negocio
        /// </summary>
        /// <param name="Id_Prd"></param>
        /// <param name="Conexion"></param>
        /// <param name="EsPapel"></param>
        /// <param name="Prd_PesosConTecnico"></param>
        /// <param name="Prd_Mes"></param>
        /// <param name="Prd_PesosAAA"></param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void CatProducto_Informacion_VP(long Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA, IBusinessTransaction ibt)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.CatProducto_Informacion_VP(Id_Prd, Conexion, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatProducto_Informacion_VP(long Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.CatProducto_Informacion_VP(Id_Prd, Conexion, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> list, object Activo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(ref producto, Conexion, id_Emp, id_Cd, filtro, ref list, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaListaProductoSpo(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> list, object Activo)
        {//rm lista de productos sistema de propietarios
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProductoSpo(ref producto, Conexion, id_Emp, id_Cd, filtro, ref list, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaListaProductoFacturacion(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Ter, string filtro, ref List<Producto> List, object Activo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProductoFacturacion(ref producto, Conexion, id_Emp, id_Cd, id_Ter, filtro, ref List, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv)

        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, ValidaInv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // 13 Jul 2018 RFH
        //
        public void Consulta_Producto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.Consulta_Producto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, ValidaInv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public void Consulta_ProductoPrecioObjetivo(ref Producto producto, ref GestionIncrementoPrecios gestioprecionincremento, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv, int Id_Cte, string Id_Tamaño, int id_rik)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.Consulta_ProductoPrecioObjetivo(ref producto, ref gestioprecionincremento, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, ValidaInv, Id_Cte, Id_Tamaño, id_rik);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 25 Jul 2019 RFH
        //
        //public List<Producto> spCatProducto_Paginacion(int Id_Emp, int Id_Cd, int PageSize, int PageNumber, string Conexion)
        //{
        //    CD_CatProducto claseCapaDatos = new CD_CatProducto();
        //    return claseCapaDatos.spCatProducto_Paginacion(Id_Emp, Id_Cd, PageSize, PageNumber, Conexion);
        //}

        public void ConsultaProductoInventario(ref int verificador, string Conexion, int id_Emp, int id_Cd, int Id_Es, int Es_Naturaleza, int Id_EsDet)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoInventario(ref verificador, Conexion, id_Emp, id_Cd, Id_Es, Es_Naturaleza, Id_EsDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, long id_Prd, int ValidaInactivo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, Id_Cd, id_Prd, ValidaInactivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProducto_CL(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.InsertarProducto_CL(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // ENE31-2020 RFH Compras Locales 
        // Lisado de precios del producto 
        //
        public List<eCL_ProductoPrecios> spSel_ProductoPrecios_CL_ver2(int Id_Emp, int Id_Cd, long Id_Prd, string Conexion)
        {
            CD_CatProducto CD = new CD_CatProducto();
            return CD.spSel_ProductoPrecios_CL_ver2(Id_Emp, Id_Cd, Id_Prd, Conexion);
        }

        // ENE28-2020 RFH 
        public void InsertarProducto_CL_ver2(
            int Id_Emp, int Id_Cd, Producto producto, string Conexion, ref int verificador)
        {
            CD_CatProducto claseCapaDatos = new CD_CatProducto();
            claseCapaDatos.InsertarProducto_CL_ver2(Id_Emp, Id_Cd, producto, Conexion, ref verificador);
        }

        // ENE28-2020 RFH 
        public void InsertarProducto_CL_ver3(
            int Id_Emp, int Id_Cd, Producto producto, string Conexion, ref int verificador)
        {
            CD_CatProducto claseCapaDatos = new CD_CatProducto();
            claseCapaDatos.InsertarProducto_CL_ver3(Id_Emp, Id_Cd, producto, Conexion, ref verificador);
        }

        public void ConsultaProducto_CL(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_CL(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ENE21-2020 RFH Compras Locales consulta producto 
        public CapaEntidad.Producto ConsultaProductoById(
                ref int Validador, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo, string Conexion)
        {
            CD_CatProducto CN = new CD_CatProducto();
            return CN.ConsultaProductoById(ref Validador, id_Emp, id_Cd, id_Cd_Ver, id_Prd, catalogo, Conexion);
        }

        // JUN03-2020 RFH 
        // CRM - PROYECTO - PRODUCTO - APLICACION 

        public CapaEntidad.Producto CRM_BusquedaProducto(
                int Id_Emp, int Id_Cd, int Id_Cte, int Id_Op, long Id_Prd, int TipoBusqueda, string Conexion)
        {
            CD_CatProducto CN = new CD_CatProducto();
            return CN.CRM_BusquedaProducto(Id_Emp, Id_Cd, Id_Cte, Id_Op, Id_Prd, TipoBusqueda, Conexion);
        }

        public void InsertarProducto(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.InsertarProducto(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProducto_CL(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ModificarProducto_CL(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarProducto(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ModificarProducto(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoCte(ref Producto producto, string Conexion, int cliente)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoCte(ref producto, Conexion, cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto_Descripcion(long Id_Prd, ref string Prd_Descripcion, string Conexion)
        {
            try
            {
                CD_CatProducto cd_prd = new CD_CatProducto();
                cd_prd.ConsultaProducto_Descripcion(Id_Prd, ref Prd_Descripcion, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void ConsultarMaxLocal(int Id_Cd, int Id_Emp, string Conexion, ref int max)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultarMaxLocal(Id_Cd, Id_Emp, ref max, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProducto_Disponible(int empresa, int Cd, string Prd, ref List<int> Actuales, string Conexion)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_Disponible(empresa, Cd, Prd, ref Actuales, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoAsig_Admin(Producto prd, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoAsig_Admin(prd, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ConsultaAsignPedxPrd_Picking_Producto



        public void ConsultaAsignPedxPrd(ProductoDet prdDet, string Conexion, ref List<ProductoDet> List)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaAsignPedxPrd(prdDet, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarPedXPrd(Pedido pedido, List<ProductoDet> list, string Conexion, ref int verificador, int asignable, long Id_Prd)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.AsignarPedXPrd(pedido, Conexion, list, ref verificador, asignable, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public void ConsultaProductoCte_Lista(Producto prd, string Conexion, int Id_Cte, int Id_Acs, ref List<Producto> list)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(prd, Conexion, Id_Cte, Id_Acs, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarVentas(ref Producto pr, int Id_Cte, string Conexion)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultarVentas(pr, Conexion, Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidarProductoEcommerce(Int64 producto, string Conexion, ref int Verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ValidarProductoEcommerce(producto, Conexion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarProduto_Ecommerce(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultarProduto_Ecommerce(producto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaListaProductoAgrupador(Producto prd, int Id_Acs, string Conexion, ref List<Comun> list)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(prd, Id_Acs, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaBuscar(Producto prd, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaBuscar(prd, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductos(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, long id_Prd, ref int productoNuevo, int ValidaInactivo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductos(ref producto, Conexion, id_Emp, Id_Cd, id_Prd, ref productoNuevo, ValidaInactivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Determina si el producto es "no facturable"
        /// </summary>
        /// <param name="p">Producto</param>
        /// <param name="conexion">Cadena de conexión</param>
        /// <returns>true en caso de que el producto sea no facturable; false en caso contrario</returns>
        public bool EsProductoNoFacturable(Producto p, string conexion)
        {
            bool noFacturable = false;
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                noFacturable = claseCapaDatos.EsProductoNoFacturable(p.Id_Emp, p.Id_Cd, p.Id_Prd, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return noFacturable;
        }

        /// <summary>
        /// Determina si el producto es "no facturable"
        /// </summary>
        /// <param name="p">Producto</param>
        /// <param name="conexion">Cadena de conexión</param>
        /// <returns>true en caso de que el producto sea no facturable; false en caso contrario</returns>
        public bool EsProductoNoFacturable(int idEmp, int idCd, long idPrd, string conexion)
        {
            bool noFacturable = false;
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                noFacturable = claseCapaDatos.EsProductoNoFacturable(idEmp, idCd, idPrd, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return noFacturable;
        }

        /// <summary>
        /// Devuelve la instancia de la entidad [CatProducto], dado el identificador de producto.
        /// </summary>
        /// <param name="s">Sesion del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns>CatProducto. Instancia de la entidad [CatProducto] en caso de que el producto exista; null en caso contrario.</returns>
        public CatProducto ObtenerPorId(Sesion s, long idPrd)
        {
            CD_CatProducto cdCatProducto = new CD_CatProducto();
            return cdCatProducto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idPrd, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve la instancia de la entidad [CatProducto], dado el identificador de producto.
        /// </summary>
        /// <param name="s">Sesion del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns>CatProducto. Instancia de la entidad [CatProducto] en caso de que el producto exista; null en caso contrario.</returns>
        public CatProducto ObtenerPorId(Sesion s, long idPrd, IBusinessTransaction ibt)
        {
            CD_CatProducto cdCatProducto = new CD_CatProducto();
            return cdCatProducto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idPrd, ibt.DataContext);
        }

        public void ConsultaProducto_ClaveProveedor(Int64 Id_Prd, ref string Prd_ClaveProveedor, string Conexion)
        {
            try
            {
                CD_CatProducto cd_prd = new CD_CatProducto();
                cd_prd.ConsultaProducto_ClaveProveedor(Id_Prd, ref Prd_ClaveProveedor, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // RBM Nov 2023
        //Consulta Presentacion de productos para compras locales
        public List<eListaGenerica> spPresentacion_ComboCompraLocal(int id_Emp, string emp_Cnx)
        {
            try
            {
                CD_CatProducto CD = new CD_CatProducto();
                return CD.spPresentacion_ComboCompraLocal(id_Emp, emp_Cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Producto ConsultaCompraLocalById(ref int validador, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo, string Conexion)
        {
            CD_CatProducto CN = new CD_CatProducto();
            return CN.ConsultaCompraLocalById(ref validador, id_Emp, id_Cd, id_Cd_Ver, id_Prd, catalogo, Conexion);
        }


    }

}