using System;
using System.Diagnostics.Eventing.Reader;

namespace CapaEntidad
{
    public class entOrdenServicioFiltros
    {
        public string strNombreCliente { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int intIdCteInicial { get; set; }
        public int intIdCteFinal { get; set; }
        public int intTipoOrden { get; set; }
        public int intIdEstatus { get; set; }
        public string strEstatus { get; set; }
        
        public DateTime? dateFechaInicial { get; set; }
        public DateTime? dateFechaFinal { get; set; }
        public int intIdOrdenServicioInicial { get; set; }
        public int intIdOrdenServicioFinal { get; set; }
    }

    public class entOrdenServicio
    {
        public string strCveServicio { get; set; }
        public int intIdOrdenServicio { get; set; }
        public DateTime? dateFecha { get; set; }
        public int intIdCte { get; set; }
        public string strNombreComercial { get; set; }
        public decimal dcmSubtotal { get; set; }
        public decimal dcmIva { get; set; }
        public decimal dcmTotal { get; set; }
        public int Id_Estatus { get; set; }
        public int intIdUsuarioInserta { get; set; }
        public int intUsuarioSesion { get; set; }  // NUEVO: Usuario en sesión para validación de permisos
        public string strEstatus { get; set; }
    }

    public class entOrdenServicioDetalle
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public string strSerie { get; set; }
        public int intIdOrdenServicio { get; set; }
        public bool isExtemporaneo { get; set; }
        public DateTime? dateCompromiso { get; set; }
        public DateTime? dateEstimada { get; set; }
        public int intMotivoCambioFecha { get; set; }  // NUEVO: Motivo de cambio de fecha compromiso
        public long intIdServicio { get; set; }
        public string strDescripcionServicio { get; set; }  // RESTAURADO: Descripción del servicio
        public int intIdCliente { get; set; }
        public int intIdTer { get; set; }
        public int intIdUsuarioInserta { get; set; }
        public string strUsuarioInserta { get; set; }
        public string strIndicaciones { get; set; }
        public decimal dcmSubTotal { get; set; }

        public decimal dcmIva { get; set; }

        public decimal dcmTotal { get; set; }

        public int Id_Estatus { get; set; }  // Agregar Id_Estatus para mostrar en modal
        public int intIdMatriz { get; set; } 
        public string strMatriz { get; set; }
        public int intIdMotivoIncompleto { get; set; }

    }

    public class entOrdenServicioClienteDireccion
    {
        public int intIdCliente { get; set; }
        public string strNombreComercial { get; set; }
        public string strSegmento { get; set; }
        public string strCalle { get; set; }
        public string strNumCalle { get; set; }
        public string strColonia { get; set; }
        public string strMunicipio { get; set; }
        public string strEstado { get; set; }
        public string strRFC { get; set; }
        public string strTelefono { get; set; }
    }


    public class entDataCombo
    {
        public long Id { get; set; }
        public string Descripcion
        {
            get; set;
        }
    }

    public class entOrdenServicioProductos
    {
        public int Id_SrvDet { get; set; }
        public int Id_Srv { get; set; }
        public int Orden { get; set; }    
       
        public long Id_Prd { get; set; }
        public long Id { get; set; }
        public string Descripcion
        {
            get; set;
        }
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public decimal Total { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaInsercion { get; set; }
        public int TipoProducto { get; set; }
    }

    public class entRolUsuario
    {
        public int intIdRolUsuario { get; set; }
        public int intIdEmp { get; set; }
        public int intIdCd { get; set; }
        public int intIdSrv { get; set; }
        public int intIdRol { get; set; }
        public string strRol { get; set; }
        public int intIdRep { get; set; }
        public string strRepresentante { get; set; }
    }

    public class entOrdenServicioMonitor
    {
        public string strCveServicio { get; set; }
        public int intIdSrv { get; set; }
        public int intIdCte { get; set; }
        public string strCliente { get; set; }
        public int intIdEstatus { get; set; }
        public string strEstatus { get; set; }

        /* Pendiente para agregar en el control de cambios
        public int intIdPedido { get; set; }
        public int intIdFac { get; set; }
        */
        public long intIdPrd { get; set; }
        public string strPrdDescripcion { get; set; }
        public int intUnidades_Producto { get; set; }
        public decimal dcmTotal_Producto { get; set; }
        public int intIdMatriz { get; set; }
        public string strMatriz { get; set; }

        public DateTime? dateCaptura { get; set; }
        public DateTime? dateCompromiso { get; set; }
        public DateTime? dateConfirmacion { get; set; }
        public int intUnidades_Cliente { get; set; }

        public decimal dcmTotal_Cliente { get; set; }

        public int intTiempoConfirmacion { get; set; }
        public string strTipoConfirmacion { get; set; }   
        
        public string strUsuarioCreador { get; set; }
        
        public string strUsuarioAsignado { get; set; }
        public string strMotivoIncompleto { get; set; }
        public string strMotivoCambioFecha { get; set; }
    }

    public class entOrdenServicoIndicadores
    {
        public int intCompleto { get; set; }
        public int intIncompleto { get; set; }
        public int intTotalConfirmado { get; set; }
        public decimal dcmCompletoMonto { get; set; }
        public decimal dcmIncompletoMonto { get; set; }
        public decimal dcmTotalMonto { get; set; }
        public decimal dcmCompletoPorcentaje { get; set; }
        public decimal dcmIncompletoPorcentaje { get; set; }

    }

}