using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CapaDatos
{
    public class CD_OrdenServicio_Admin
    {
        public List<Comun> CatalogoEstatus(string conexion)
        {
            var lista = new List<Comun>();
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ComboEstatus", ref dr, new string[] { }, new object[] { });
                while (dr.Read())
                {
                    lista.Add(new Comun { Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString(),Relacion="" });
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error CatalogoTipoOrden: " + ex.Message);
            }
            return lista;
        }

        public List<entOrdenServicio> BuscarOrdenesServicios(string conexion, entOrdenServicioFiltros filtros)
        {
            var lista = new List<entOrdenServicio>();
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = {"@Id_Emp","@Id_Cd","@strNombreCliente","@intIdCteInicial","@intIdCteFinal", "@intIdEstatus", "@dateFechaInicial","@dateFechaFinal","@intIdOrdenServicioInicial","@intIdOrdenServicioFinal"};
                object[] valores = {
                    filtros.Id_Emp,
                    filtros.Id_Cd,
                    (object) (string.IsNullOrWhiteSpace(filtros.strNombreCliente)? null: filtros.strNombreCliente),
                    filtros.intIdCteInicial==0? (object) null: filtros.intIdCteInicial,
                    filtros.intIdCteFinal==0? (object) null: filtros.intIdCteFinal,
                    filtros.intIdEstatus==0? (object) null: filtros.intIdEstatus,
                    filtros.dateFechaInicial,
                    filtros.dateFechaFinal,
                    filtros.intIdOrdenServicioInicial==0? (object) null: filtros.intIdOrdenServicioInicial,
                    filtros.intIdOrdenServicioFinal==0? (object) null: filtros.intIdOrdenServicioFinal
                };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_Consultar", ref dr, parametros, valores);
                while (dr.Read())
                {
                    lista.Add(new entOrdenServicio
                    {
                        strCveServicio = dr["strCveServicio"].ToString(),
                        intIdOrdenServicio = Convert.ToInt32(dr["intIdOrdenServicio"]),
                        dateFecha = dr["dateFecha"]==DBNull.Value? (DateTime?)null: Convert.ToDateTime(dr["dateFecha"]),
                        intIdCte = Convert.ToInt32(dr["intIdCte"]),
                        strNombreComercial = dr["strNombreComercial"].ToString(),
                        dcmSubtotal = dr["dcmSubtotal"]==DBNull.Value?0: Convert.ToDecimal(dr["dcmSubtotal"]),
                        dcmIva = dr["dcmIva"]==DBNull.Value?0: Convert.ToDecimal(dr["dcmIva"]),
                        dcmTotal = dr["dcmTotal"]==DBNull.Value?0: Convert.ToDecimal(dr["dcmTotal"]),
                        Id_Estatus = Convert.ToInt32(dr["Id_Estatus"]),
                        intIdUsuarioInserta = Convert.ToInt32(dr["Id_U_Capturador"]),
                        strEstatus = dr["strEstatus"].ToString(),

                    });
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr!=null && !dr.IsClosed) dr.Close();
                throw new Exception("Error BuscarOrdenesServicios: " + ex.Message);
            }
            return lista;
        }

        // --- Nuevos métodos para monitoreo (por ahora reutilizan la misma consulta) ---
        public void BuscarOrdenServicioMonitor(string conexion, entOrdenServicioFiltros filtros, ref List<entOrdenServicioMonitor> lstOrdenProductos, ref  List<entOrdenServicioMonitor> lstOrdenCliente, ref entOrdenServicoIndicadores entIndicadores)
        {
            int intTablaResultado = 0;
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                
                string[] parametros = { "@Id_Emp", "@Id_Cd","@intIdEstatus", "@dateFechaInicial", "@dateFechaFinal", "@intIdOrdenServicioInicial", "@intIdOrdenServicioFinal" };
                object[] valores = {
                    filtros.Id_Emp,                   
                    filtros.Id_Cd,
                    filtros.intIdEstatus==0? (object) null: filtros.intIdEstatus,
                    filtros.dateFechaInicial,
                    filtros.dateFechaFinal,
                    filtros.intIdOrdenServicioInicial==0? (object) null: filtros.intIdOrdenServicioInicial,
                    filtros.intIdOrdenServicioFinal==0? (object) null: filtros.intIdOrdenServicioFinal
                };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ConsultarMonitor", ref dr, parametros, valores);
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        intTablaResultado = Convert.ToInt32(dr["NumTabla"]);
                        switch (intTablaResultado)
                        {
                            case 1:
                                lstOrdenProductos.Add(new entOrdenServicioMonitor
                                {
                                    //strCveServicio = dr["strCveServicio"].ToString(),
                                    intIdSrv = Convert.ToInt32(dr["Id_Srv"]),
                                    intIdCte = Convert.ToInt32(dr["Id_Cte"]),
                                    strCliente = dr["Cliente"].ToString(),
                                    intIdEstatus = Convert.ToInt32(dr["Id_Estatus"]),
                                    strEstatus = dr["Estatus"].ToString(),
                                    intIdPrd = Convert.ToInt64(dr["Id_Prd"]),
                                    strPrdDescripcion = dr["Prd_Descripcion"].ToString(),
                                    intUnidades_Producto = Convert.ToInt32(dr["Unidades_Producto"]),
                                    dcmTotal_Producto = Convert.ToDecimal(dr["Total_Producto"]),
                                    intIdMatriz = Convert.ToInt32(dr["Id_Matriz"]),
                                    strMatriz = dr["Matriz"].ToString(),
                                    dateCaptura = dr["Fecha_Captura"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Captura"]),
                                    dateCompromiso = dr["Fecha_Compromiso"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Compromiso"]),
                                    dateConfirmacion = dr["Fecha_Confirmacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Confirmacion"]),
                                    strTipoConfirmacion = dr["strTipo_Confirmacion"].ToString(),
                                    intTiempoConfirmacion = Convert.ToInt32(dr["Tiempo_Confirmacion"]),
                                    strUsuarioCreador = dr["Usuario_Creador"].ToString(),
                                    strUsuarioAsignado = dr["strUsuarioAsignado"].ToString(),
                                    strMotivoIncompleto = dr["Motivo_Incompleto"].ToString(),
                                    strMotivoCambioFecha = dr["Motivo_Cambio_Fecha"].ToString()
                                    // strUsuarioAsignado
                                });
                                break;
                            case 2:
                                lstOrdenCliente.Add(new entOrdenServicioMonitor
                                {
                                    //strCveServicio = dr["strCveServicio"].ToString(),
                                    intIdSrv = Convert.ToInt32(dr["Id_Srv"]),
                                    intIdCte = Convert.ToInt32(dr["Id_Cte"]),
                                    strCliente = dr["Cliente"].ToString(),
                                    intIdEstatus = Convert.ToInt32(dr["Id_Estatus"]),
                                    strEstatus = dr["Estatus"].ToString(),
                                    //intIdPrd = Convert.ToInt64(dr["Id_Prd"]),
                                    //strPrdDescripcion = dr["Prd_Descripcion"].ToString(),
                                    //intUnidades_Producto = Convert.ToInt32(dr["Unidades_Producto"]),
                                    //dcmTotal_Producto = Convert.ToDecimal(dr["Total_Producto"]),
                                    intIdMatriz = Convert.ToInt32(dr["Id_Matriz"]),
                                    strMatriz = dr["Matriz"].ToString(),
                                    dateCaptura = dr["Fecha_Captura"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Captura"]),
                                    dateCompromiso = dr["Fecha_Compromiso"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Compromiso"]),
                                    dateConfirmacion = dr["Fecha_Confirmacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Fecha_Confirmacion"]),
                                    dcmTotal_Cliente = dr["Total_Cliente"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Total_Cliente"]),
                                    intUnidades_Cliente = Convert.ToInt32(dr["Unidades_Cliente"]),
                                    strTipoConfirmacion = dr["strTipo_Confirmacion"].ToString(),
                                    intTiempoConfirmacion = Convert.ToInt32(dr["Tiempo_Confirmacion"]),
                                    strUsuarioCreador = dr["Usuario_Creador"].ToString(),
                                    strUsuarioAsignado = dr["strUsuarioAsignado"].ToString(),
                                    strMotivoIncompleto = dr["Motivo_Incompleto"].ToString(),
                                    strMotivoCambioFecha = dr["Motivo_Cambio_Fecha"].ToString()
                                });
                                break;
                            case 3:
                                entIndicadores.intCompleto = Convert.ToInt32(dr["Completo"]);
                                entIndicadores.intIncompleto = Convert.ToInt32(dr["Incompleto"]);
                                entIndicadores.intTotalConfirmado = Convert.ToInt32(dr["Total_Confirmado"]);
                                entIndicadores.dcmCompletoMonto = Convert.ToDecimal(dr["Completo_Monto"]);
                                entIndicadores.dcmIncompletoMonto = Convert.ToDecimal(dr["Incompleto_Monto"]);
                                entIndicadores.dcmTotalMonto = Convert.ToDecimal(dr["Total_Monto"]);
                                entIndicadores.dcmCompletoPorcentaje = Convert.ToDecimal(dr["Completo_Porcentaje"]);
                                entIndicadores.dcmIncompletoPorcentaje = Convert.ToDecimal(dr["Incompleto_Porcentaje"]);
                                break;
                        }
                    }
                    dr.NextResult();
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error BuscarOrdenesServicios: " + ex.Message);
            }
        }
        public List<entOrdenServicio> ExportarOrdenServicioMonitor(string conexion, entOrdenServicioFiltros filtros)
        {
            return BuscarOrdenesServicios(conexion, filtros);
        }
        // ------------------------------------------------------------------------------

        public void CatalogoServicios(string conexion, int intidEmp, int intIdCd, ref List<entOrdenServicioProductos> lstServicio)
        {
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd" };
                object[] valores = { intidEmp, intIdCd };
                // Servicio
                var cmdServ = datos.GenerarSqlCommand("spCapOrdenServicio_ComboServicio", ref dr, parametros, valores);
                lstServicio = new List<entOrdenServicioProductos>();
                while (dr.Read())
                    lstServicio.Add(new entOrdenServicioProductos
                    {
                        Id = Convert.ToInt64(dr["Id"]),
                        Id_Prd = Convert.ToInt64(dr["Id"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Costo = dr["Costo"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Costo"]),
                        TipoProducto = 1 // Indica que es un servicio
                    });
                dr.Close();
                datos.LimpiarSqlcommand(ref cmdServ);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error CatalogoServicios: " + ex.Message);
            }
        }

        public void CatalogoUsuarios(string conexion, int intidEmp, int intIdCd, ref List<Comun> lstRol, ref List<Comun> lstUsuarios)
        {
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd" };
                object[] valores = { intidEmp, intIdCd };
                // Rol
                var cmdRol = datos.GenerarSqlCommand("spCapOrdenServicio_ComboRol", ref dr, parametros, valores);
                lstRol = new List<Comun>();
                while(dr.Read()) lstRol.Add(new Comun { Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString(), Relacion = "" });
                dr.Close(); 
                datos.LimpiarSqlcommand(ref cmdRol);

                datos = new CD_Datos(conexion);
                var cmdUsuarios = datos.GenerarSqlCommand("spCapOrdenServicio_ComboUsuarios", ref dr, parametros, valores);
                lstUsuarios = new List<Comun>();
                while (dr.Read()) lstUsuarios.Add(new Comun { Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString(), Relacion = "", ValorInt = Convert.ToInt32(dr["Tipo_Rep"]) });
                dr.Close();
                datos.LimpiarSqlcommand(ref cmdUsuarios);                
            }
            catch(Exception ex)
            {
                if(dr!=null && !dr.IsClosed) dr.Close();
                throw new Exception("Error CatalogoUsuarios: " + ex.Message);
            }
        }

        public void GuardarOrdenServicio(string conexion, entOrdenServicioDetalle det, List<entOrdenServicioProductos> lstProductos, List<int> lstProductosAEliminar, List<entRolUsuario> lstRolUsuario, List<int> lstIdRolUsuarioEliminar, ref string mensaje)
        {
            try
            {
                var datos = new CD_Datos(conexion);
                int verif = 0;
                string[] parametros = {
                    "@Id_Emp", 
                    "@Id_Cd",
                    "@intIdOrdenServicio",
                    "@isExtemporaneo",
                    "@dateEstimada",
                    "@dateCompromiso",
                    "@intMotivoCambioFecha",  // NUEVO: Agregar motivo de cambio de fecha
                    "@intIdServicio",
                    "@strDescripcionServicio",
                    "@intIdCliente",
                    "@intIdTer", 
                    "@dcmSubTotal",
                    "@dcmIva", 
                    "@dcmTotal",
                    "@intIdMatriz",
                    "@strIndicaciones",
                    "@intIdUsuarioInserta" ,
                    "@strUsuarioInserta",
                    "@intIdEstaus"
                };
                
                object[] valores = {
                    det.Id_Emp,
                    det.Id_Cd,
                    det.intIdOrdenServicio,
                    det.isExtemporaneo,
                    (det.intIdOrdenServicio > 0) ? det.dateCompromiso: (object) null,
                    det.dateCompromiso,
                    det.intMotivoCambioFecha,  // NUEVO: Pasar motivo de cambio de fecha
                    det.intIdServicio,
                    det.strDescripcionServicio,
                    det.intIdCliente,
                    det.intIdTer,
                    det.dcmSubTotal ,
                    det.dcmIva,
                    det.dcmTotal,
                    det.intIdMatriz==0? (object) null: det.intIdMatriz,
                    string.IsNullOrEmpty(det.strIndicaciones)? (object) null: det.strIndicaciones,
                    det.intIdUsuarioInserta,
                    det.strUsuarioInserta,
                    det.Id_Estatus
                };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_Guardar", ref verif, parametros, valores);
                datos.LimpiarSqlcommand(ref cmd);
                
                // Si se guardó correctamente la orden de servicio
                if (verif != 0)
                {
                    int Id_Srv = verif; // El SP retorna el ID de la orden de servicio guardada
                    
                    // Primero, realizar eliminación lógica de productos marcados
                    if (lstProductosAEliminar != null && lstProductosAEliminar.Count > 0)
                    {
                        foreach (var idSrvDet in lstProductosAEliminar)
                        {
                            try
                            {
                                datos = new CD_Datos(conexion);
                                string[] paramEliminar = {"@Id_Emp", "@Id_Cd", "@Id_SrvDet"};
                                object[] valoresEliminar = { det.Id_Emp, det.Id_Cd, idSrvDet };
                                
                                int verifEliminar = 0;
                                var cmdEliminar = datos.GenerarSqlCommand("spCapOrdenServicio_EliminarProducto", ref verifEliminar, paramEliminar, valoresEliminar);
                                datos.LimpiarSqlcommand(ref cmdEliminar);
                                
                                if (verifEliminar == 0)
                                {
                                    mensaje = $"Error al eliminar el producto con ID {idSrvDet}";
                                    return;
                                }
                            }
                            catch (Exception exElim)
                            {
                                mensaje = $"Error al eliminar producto: {exElim.Message}";
                                return;
                            }
                        }
                    }
                    
                    // Guardar o actualizar productos
                    if (lstProductos != null && lstProductos.Count > 0)
                    {
                        foreach (var producto in lstProductos)
                        {
                            try
                            {
                                datos = new CD_Datos(conexion);
                                string[] paramProductos = {"@Id_Emp", "@Id_Cd", "@Id_Srv", "@Id_SrvDet", "@Orden", "@IdPrd", "@Descripcion", "@Cantidad", "@Costo", "@Total", "@TipoProducto"};
                                object[] valoresProductos = { 
                                    det.Id_Emp, 
                                    det.Id_Cd, 
                                    Id_Srv, 
                                    producto.Id_SrvDet,
                                    producto.Orden, 
                                    producto.Id_Prd, 
                                    producto.Descripcion,
                                    producto.Cantidad,
                                    producto.Costo,
                                    producto.Total,
                                    producto.TipoProducto
                                };
                                
                                int verifProducto = 0;
                                var cmdProducto = datos.GenerarSqlCommand("spCapOrdenServicio_GuardarProductos", ref verifProducto, paramProductos, valoresProductos);
                                datos.LimpiarSqlcommand(ref cmdProducto);
                                
                                if (verifProducto == 0)
                                {
                                    mensaje = $"Error al guardar el producto en orden {producto.Orden}";
                                    return;
                                }
                            }
                            catch (Exception exProd)
                            {
                                mensaje = $"Error al guardar producto: {exProd.Message}";
                                return;
                            }
                        }
                    }
                    
                    // Realizar eliminación lógica de roles y usuarios marcados
                    if (lstIdRolUsuarioEliminar != null && lstIdRolUsuarioEliminar.Count > 0)
                    {
                        foreach (var idRolUsuario in lstIdRolUsuarioEliminar)
                        {
                            try
                            {
                                datos = new CD_Datos(conexion);
                                string[] paramEliminarRU = {"@Id_Emp", "@Id_Cd", "@Id_Srv","@Id_RolUsuario" };
                                object[] valoresEliminarRU = { det.Id_Emp, det.Id_Cd, Id_Srv, idRolUsuario };
                                
                                int verifEliminarRU = 0;
                                var cmdEliminarRU = datos.GenerarSqlCommand("spCapOrdenServicio_QuitarRolUsuario", ref verifEliminarRU, paramEliminarRU, valoresEliminarRU);
                                datos.LimpiarSqlcommand(ref cmdEliminarRU);
                                
                                if (verifEliminarRU == 0)
                                {
                                    mensaje = $"Error al eliminar el rol usuario con ID {idRolUsuario}";
                                    return;
                                }
                            }
                            catch (Exception exElimRU)
                            {
                                mensaje = $"Error al eliminar rol usuario: {exElimRU.Message}";
                                return;
                            }
                        }
                    }
                    
                    // Guardar o actualizar roles y usuarios
                    if (lstRolUsuario != null && lstRolUsuario.Count > 0)
                    {
                        foreach (var rolUsuario in lstRolUsuario)
                        {
                            try
                            {
                                datos = new CD_Datos(conexion);
                                string[] paramRolUsuario = {"@Id_Emp", "@Id_Cd", "@Id_Srv", "@Id_RolUsuario", "@Id_Rol", "@Rol", "@Id_Rik", "@Representante"};
                                object[] valoresRolUsuario = { 
                                    det.Id_Emp, 
                                    det.Id_Cd, 
                                    Id_Srv, 
                                    rolUsuario.intIdRolUsuario,
                                    rolUsuario.intIdRol, 
                                    rolUsuario.strRol, 
                                    rolUsuario.intIdRep,
                                    rolUsuario.strRepresentante
                                };
                                
                                int verifRolUsuario = 0;
                                var cmdRolUsuario = datos.GenerarSqlCommand("spCapOrdenServicio_GuardarUsuario", ref verifRolUsuario, paramRolUsuario, valoresRolUsuario);
                                datos.LimpiarSqlcommand(ref cmdRolUsuario);
                                
                                if (verifRolUsuario == 0)
                                {
                                    mensaje = $"Error al guardar el rol usuario {rolUsuario.strRol} - {rolUsuario.strRepresentante}";
                                    return;
                                }
                            }
                            catch (Exception exRolUsuario)
                            {
                                mensaje = $"Error al guardar rol usuario: {exRolUsuario.Message}";
                                return;
                            }
                        }
                    }
                    
                    mensaje = "Orden de servicio, productos y usuarios guardados correctamente";
                }
                else
                {
                    mensaje = "No se pudo guardar la orden de servicio";
                }
            }
            catch(Exception ex)
            { 
                mensaje = "Error al guardar: " + ex.Message;
                throw new Exception("Error GuardarOrdenServicio: "+ex.Message); 
            }
        }

        public void ConsultarOrdenServicio(string strConexionLocal, string strConexionCentral, ref entOrdenServicioDetalle det, ref List<entOrdenServicioProductos> lstProductos, ref entOrdenServicioClienteDireccion dir, ref List<Comun> lstRol, ref List<entOrdenServicioProductos> lstServicio, ref List<Comun> lstTerritorio, ref List<Comun> lstUsuario, ref List<entRolUsuario> lstRolUsuario, ref List<Comun> lstCambioCompromiso)
        {
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(strConexionLocal);
                string[] parametros = { "@Id_Emp", "@Id_Cd", "@intIdOrdenServicio" };
                object[] valores = { det.Id_Emp, det.Id_Cd, det.intIdOrdenServicio };
                // Detalle principal
                var cmdDet = datos.GenerarSqlCommand("spCapOrdenServicio_ConsultaDetalle", ref dr, parametros, valores);
                if(dr.Read())
                {
                    det.strSerie = dr["strSerie"].ToString();
                    det.intIdOrdenServicio = Convert.ToInt32(dr["intIdOrdenServicio"]);
                    det.isExtemporaneo = dr["isExtemporaneo"] != DBNull.Value && Convert.ToBoolean(dr["isExtemporaneo"]);
                    det.dateCompromiso = dr["dateCompromiso"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["dateCompromiso"]);
                    det.dateEstimada = dr["dateEstimada"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["dateEstimada"]);
                    det.intIdServicio = dr["intIdServicio"] == DBNull.Value ? 0 : Convert.ToInt64(dr["intIdServicio"]);
                    det.intIdCliente = dr["intIdCliente"] == DBNull.Value ? 0 : Convert.ToInt32(dr["intIdCliente"]);
                        det.intIdTer = dr["intIdTer"] == DBNull.Value ? 0 : Convert.ToInt32(dr["intIdTer"]);
                        det.Id_Estatus = dr["Id_Estatus"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id_Estatus"]);
                    det.strIndicaciones = dr["strIndicaciones"] == DBNull.Value ? string.Empty : dr["strIndicaciones"].ToString();
                    det.strMatriz = dr["strMatriz"] == DBNull.Value ? string.Empty : dr["strMatriz"].ToString();

                }
                dr.Close(); datos.LimpiarSqlcommand(ref cmdDet);
                // Productos
                datos = new CD_Datos(strConexionLocal);
                var cmdProd = datos.GenerarSqlCommand("spCapOrdenServicio_ConsultaProductos", ref dr, parametros, valores);
                while(dr.Read())
                {
                    lstProductos.Add(new entOrdenServicioProductos{
                        Id_SrvDet = dr["Id_SrvDet"]==DBNull.Value?0: Convert.ToInt32(dr["Id_SrvDet"]),
                        Orden = dr["Orden"]==DBNull.Value?0: Convert.ToInt32(dr["Orden"]),
                        Id_Prd = Convert.ToInt64(dr["Id_Prd"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Cantidad = dr["Cantidad"]==DBNull.Value?1: Convert.ToInt32(dr["Cantidad"]),
                        Costo = dr["Costo"]==DBNull.Value?0: Convert.ToDecimal(dr["Costo"]),
                        Total = dr["Total"]==DBNull.Value?0: Convert.ToDecimal(dr["Total"]),
                        TipoProducto = dr["TipoProducto"]==DBNull.Value?0: Convert.ToInt32(dr["TipoProducto"])
                    });
                }
                dr.Close(); datos.LimpiarSqlcommand(ref cmdProd);
                
                // Consultar roles y usuarios de la orden de servicio
                datos = new CD_Datos(strConexionLocal);
                var cmdRolUsuario = datos.GenerarSqlCommand("spCapOrdenServicio_ConsultaRolUsuario", ref dr, parametros, valores);
                while(dr.Read())
                {
                    lstRolUsuario.Add(new entRolUsuario{
                        intIdRolUsuario = dr["Id_RolUsuario"]==DBNull.Value?0: Convert.ToInt32(dr["Id_RolUsuario"]),
                        intIdEmp = dr["Id_Emp"]==DBNull.Value?0: Convert.ToInt32(dr["Id_Emp"]),
                        intIdCd = dr["Id_Cd"]==DBNull.Value?0: Convert.ToInt32(dr["Id_Cd"]),
                        intIdSrv = dr["Id_Srv"]==DBNull.Value?0: Convert.ToInt32(dr["Id_Srv"]),
                        intIdRol = dr["Id_Rol"]==DBNull.Value?0: Convert.ToInt32(dr["Id_Rol"]),
                        strRol = dr["Rol"]==DBNull.Value?string.Empty: dr["Rol"].ToString(),
                        intIdRep = dr["Id_Rik"]==DBNull.Value?0: Convert.ToInt32(dr["Id_Rik"]),
                        strRepresentante = dr["Representante"]==DBNull.Value?string.Empty: dr["Representante"].ToString()
                    });
                }
                dr.Close(); datos.LimpiarSqlcommand(ref cmdRolUsuario);
                
                // Dirección cliente
                string strMensaje = string.Empty;
                List<entOrdenServicioClienteDireccion> lstDireccion = new List<entOrdenServicioClienteDireccion>();
                ConsultarCliente(strConexionLocal, det.intIdCliente, string.Empty, ref strMensaje, ref lstDireccion, ref lstTerritorio, ref lstUsuario);
                dir = lstDireccion.FirstOrDefault();
                
                CatalogoServicios(strConexionCentral, det.Id_Emp, det.Id_Cd, ref lstServicio);
                CatalogoUsuarios(strConexionLocal, det.Id_Emp, det.Id_Cd, ref lstRol, ref lstUsuario);

                lstCambioCompromiso = ConsultarCatMotivoCambioCompromiso(strConexionCentral, det.Id_Emp, det.Id_Cd);
            }
            catch(Exception ex)
            {
                if(dr!=null && !dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarOrdenServicio: "+ex.Message);
            }
        }

        private List<Comun> CatalogoSimple(string conexion, string stored)
        {
            var lst = new List<Comun>();
            SqlDataReader dr = null;
            var datos = new CD_Datos(conexion);
            var cmd = datos.GenerarSqlCommand(stored, ref dr, new string[]{}, new object[]{});
            while(dr.Read()) lst.Add(new Comun{ Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString() });
            dr.Close(); datos.LimpiarSqlcommand(ref cmd);
            return lst;
        }

        public void ConsultarCliente(string conexion, int intIdCliente, string strNombreCliente, ref string strMensaje, ref List<entOrdenServicioClienteDireccion> lstDireccion, ref List<Comun> lstTerritorio, ref List<Comun> lstUsuario)
        {
            SqlDataReader dr = null;
            entOrdenServicioClienteDireccion entDireccion = new entOrdenServicioClienteDireccion();
            try
            {
                var datos = new CD_Datos(conexion);
                var sqlParametros = new[] { "@intIdCliente", "@strNombreComercial" };
                var sqlValores = new object[] { intIdCliente == 0 ? (object)null : intIdCliente, string.IsNullOrWhiteSpace(strNombreCliente) ? (object)null : strNombreCliente };
                
                // Dirección cliente
                var cmdDir = datos.GenerarSqlCommand("spCapOrdenServicio_ClienteDireccion", ref dr, sqlParametros, sqlValores);
                
                if (!dr.HasRows)
                {
                    strMensaje = "Cliente no se encontrado.";
                    dr.Close(); datos.LimpiarSqlcommand(ref cmdDir);
                    return;
                }

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (ExisteColumna(dr, "intIdCliente"))
                        {
                            entDireccion = new entOrdenServicioClienteDireccion
                            {
                                intIdCliente = dr["intIdCliente"] == DBNull.Value ? 0 : Convert.ToInt32(dr["intIdCliente"]),
                                strNombreComercial = dr["strNombreComercial"].ToString(),
                                strSegmento =  dr["strSegmento"] == DBNull.Value ? string.Empty : dr["strSegmento"].ToString(),
                                strCalle = dr["strCalle"] == DBNull.Value ? string.Empty : dr["strCalle"].ToString(),
                                strNumCalle = dr["strNumCalle"] == DBNull.Value ? string.Empty : dr["strNumCalle"].ToString(),
                                strColonia = dr["strColonia"] == DBNull.Value ? string.Empty : dr["strColonia"].ToString(),
                                strMunicipio = dr["strMunicipio"] == DBNull.Value ? string.Empty : dr["strMunicipio"].ToString(),
                                strEstado = dr["strEstado"] == DBNull.Value ? string.Empty : dr["strEstado"].ToString(),
                                strRFC = dr["strRFC"] == DBNull.Value ? string.Empty : dr["strRFC"].ToString(),
                                strTelefono = dr["strTelefono"] == DBNull.Value ? string.Empty : dr["strTelefono"].ToString()
                            };
                            lstDireccion.Add(entDireccion);
                        }
                        if (ExisteColumna(dr, "Id_Ter"))
                        {
                            lstTerritorio.Add(new Comun { Id = Convert.ToInt32(dr["Id_Ter"]), Descripcion = dr["Ter_Nombre"].ToString(), ValorInt= Convert.ToInt32(dr["Id_Cte"]), Relacion= string.Empty });
                        }
                        if (ExisteColumna(dr, "Id_Rik"))
                        {
                            //lstTerritorio.Add(new Comun { Id = Convert.ToInt32(dr["Id_Ter"]), Descripcion = dr["Ter_Nombre"].ToString(), ValorInt = Convert.ToInt32(dr["Id_Cte"]), Relacion = string.Empty });
                            lstUsuario.Add(new Comun { Id = Convert.ToInt32(dr["Id_Rik"]), Descripcion = dr["Rik_Nombre"].ToString(), ValorInt = Convert.ToInt32(dr["Id_Cte"]), Relacion = string.Empty });
                        }
                    }
                    dr.NextResult();
                }
                dr.Close(); datos.LimpiarSqlcommand(ref cmdDir);
               
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarCliente: " + ex.Message);
            }
        }

        public static bool ExisteColumna(SqlDataReader Reader, string ColumnName)
        {
            foreach (DataRow row in Reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == ColumnName)
                    return true;
            } //Still here? Column not found. 
            return false;
        }

        public List<Comun> ConsultarCatMotivoIncompleto(string conexion, int Id_Emp, int Id_Cd)
        {
            var lista = new List<Comun>();
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd" };
                object[] valores = { Id_Emp,  Id_Cd };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ComboMotivoConfirmacion", ref dr, parametros, valores);
                while (dr.Read())
                {
                    lista.Add(new Comun 
                    { 
                        Id = Convert.ToInt32(dr["Id"]), 
                        Descripcion = dr["Descripcion"].ToString(),
                        Relacion = ""
                    });
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarCatMotivoIncompleto: " + ex.Message);
            }
            return lista;
        }

        public List<Comun> ConsultarCatMotivoCambioCompromiso(string conexion, int Id_Emp, int Id_Cd)
        {
            var lista = new List<Comun>();
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd" };
                object[] valores = { Id_Emp, Id_Cd };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ComboMotivoCambioCompromiso", ref dr, parametros, valores);
                while (dr.Read())
                {
                    lista.Add(new Comun
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Relacion = ""
                    });
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarCatMotivoCambioCompromiso: " + ex.Message);
            }
            return lista;
        }


        public List<Comun> ConsultarCatMotivoEliminacion(string conexion, int Id_Emp, int Id_Cd)
        {
            var lista = new List<Comun>();
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd" };
                object[] valores = { Id_Emp, Id_Cd };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ComboMotivoBaja", ref dr, parametros, valores);
                while (dr.Read())
                {
                    lista.Add(new Comun
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Relacion = ""
                    });
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarCatMotivoEliminacion: " + ex.Message);
            }
            return lista;
        }

        public void EliminarOrdenServicio(string conexion, int Id_Emp, int Id_Cd, int Id_Usuario, int intIdOrdenServicio, int intMotivoEliminacion, ref string mensaje)
        {
            try
            {
                var datos = new CD_Datos(conexion);
                int verif = 0;
                string[] parametros = { "@Id_Emp", "@Id_Cd", "@Id_Usuario", "@intIdOrdenServicio", "@intMotivoEliminacion" };
                object[] valores = { Id_Emp, Id_Cd, Id_Usuario, intIdOrdenServicio, intMotivoEliminacion };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_Eliminar", ref verif, parametros, valores);
                datos.LimpiarSqlcommand(ref cmd);

                if (verif != 0)
                {
                    mensaje = "Orden de servicio eliminada correctamente";
                }
                else
                {
                    mensaje = "No se pudo eliminar la orden de servicio. Verifique los permisos.";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar orden de servicio: " + ex.Message;
                throw new Exception("Error EliminarOrdenServicio: " + ex.Message);
            }
        }

        public void ConsultaProducto(string conexion, int Id_Emp, int Id_Cd, long Id_Prd, ref entOrdenServicioProductos producto, ref string strMensaje)
        {
            var datos = new CD_Datos(conexion);
            int verif = 0;
            //string[] parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
            //object[] valores = { Id_Emp, Id_Cd, Id_Prd };

            string[] parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@Nuevo", "@EmpBen", "@ValidaInactivo" };

            object[] valores = { Id_Emp, Id_Cd, Id_Prd,(object)null,(object)null,60,1,(object)null,(object)null,1 };
            SqlDataReader dr = null;
            try
            {
                /*
                 exec [spCatProducto_Consultar]    
                     @Id_Emp =1,    
                     @Id_Cd =110,    
                     @Id_Prd =995,    
                     @Id_Cte  = NULL,    
                     @Id_Ter  = NULL,    
                     @Id_Mov  = 60,    
                     @SisProp  = 1,  --  1 si se necesita que sea sistema propietario    
                     @Nuevo  = NULL,
                     @EmpBen  = NULL,
                     @ValidaInactivo  = 1
                 
                 */
                var cmd = datos.GenerarSqlCommand("spCatProducto_Consultar", ref dr, parametros, valores);
                if (dr.Read())
                {
                    producto = new entOrdenServicioProductos
                    {
                        Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd")),
                        Descripcion = dr["Prd_Descripcion"].ToString(),
                        Costo = dr["Prd_Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Prd_Precio"]),
                        TipoProducto = 2 // Indica que es un producto
                    }; 
                }
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                strMensaje = ex.Message;
            }
        }
        public void GuardarConfirmacion(string conexion, int Id_Emp, int Id_Cd, int intIdOrdenServicio, int intTipoConfirmacion, int intMotivoIncompleto, int intIdU, ref string mensaje)
        {
            try
            {
                var datos = new CD_Datos(conexion);
                int verif = 0;
                string[] parametros = { "@Id_Emp", "@Id_Cd", "@intIdOrdenServicio", "@TipoConfirmacion", "@intMotivoIncompleto" , "@Id_U_Confirmado" };
                object[] valores = { Id_Emp, Id_Cd, intIdOrdenServicio, intTipoConfirmacion, intMotivoIncompleto, intIdU };
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_EstatusConfirmacion", ref verif, parametros, valores);
                datos.LimpiarSqlcommand(ref cmd);

                if (verif != 0)
                {
                    mensaje = "Confirmación guardada correctamente";
                }
                else
                {
                    mensaje = "No se pudo guardar la confirmación";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al guardar confirmación: " + ex.Message;
                throw new Exception("Error GuardarConfirmacion: " + ex.Message);
            }
        }

        public void ConsultarRepresentantesServicio(string conexion, int Id_Emp, int Id_Cd, int intIdCte, int intIdTer, ref List<Comun> lstRepresentantes)
        {
            SqlDataReader dr = null;
            try
            {
                var datos = new CD_Datos(conexion);
                string[] parametros = { "@Id_Emp", "@Id_Cd", "@intIdCte", "@intIdTer" };
                object[] valores = { Id_Emp, Id_Cd, intIdCte, intIdTer };
                
                var cmd = datos.GenerarSqlCommand("spCapOrdenServicio_ConsultarRepresentanteSevicio", ref dr, parametros, valores);
                lstRepresentantes.Clear();
                
                while (dr.Read())
                {
                    lstRepresentantes.Add(new Comun
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        ValorInt = dr["ValorInt"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ValorInt"]),
                        Relacion = ""
                    });
                }
                
                dr.Close();
                datos.LimpiarSqlcommand(ref cmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error ConsultarRepresentantesServicio: " + ex.Message);
            }
        }
    }
}
