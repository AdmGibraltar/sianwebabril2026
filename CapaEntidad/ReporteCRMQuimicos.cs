using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteCRMQuimicos
    {
        #region Input

        public int Id_Emp { get; set; }
        public int PeriodoInicial { get; set; }
        public int PeriodoFinal { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafinal { get; set; }
        public string IdCdis { get; set; }
        public string IdRIKs { get; set; }
        public string strTipoProductos { get; set; }
        public int TipoReporte { get; set; }
        public int TipoProductoReporte { get; set; }
        public string strAplicaciones { get; set; }


        #endregion

        public int Id_Cd { get; set; }
        public string strCDI { get; set; }
        public int Id_RIK { get; set; }
        public string strRIK { get; set; }
        public int IdAgrupador { get; set; }
        public string strAgrupador { get; set; }
        public int MontoFinal_Proy { get; set; }
        public decimal MontoFinal_Monto { get; set; }
        public int ProyectosActuales_Proy { get; set; }
        public decimal ProyectosActuales_Monto { get; set; }
        public int ProyectosNuevos_Proy { get; set; }
        public decimal ProyectosNuevos_Monto { get; set; }
        public int ProyectosCerrados_Proy { get; set; }
        public decimal ProyectosCerrados_Monto { get; set; }
        public int ProyectosCancelados_Proy { get; set; }
        public decimal ProyectosCancelados_Monto { get; set; }

        #region Data
        public int Data_Id_Emp { get; set; }
        public int Data_Id_Cd { get; set; }
        public int Data_IdProyecto { get; set; }
        public int Data_Estatus { get; set; }
        public string Data_Est_Descripcion { get; set; }
        public int Data_Mes { get; set; }
        public int Data_Año { get; set; }
        public DateTime? Data_FechaCreacion { get; set; }
        public int Data_Id_UEN { get; set; }
        public string Data_Uen_Descripcion { get; set; }
        public int Data_Id_Apl { get; set; }
        public string Data_Apl_Descripcion { get; set; }
        public int Data_Id_Seg { get; set; }
        public string Data_Seg_Descripcion { get; set; }
        public int Data_Id_Ter { get; set; }
        public string Data_Ter_Nombre { get; set; }
        public int Data_IdRik { get; set; }
        public string Data_Rik_Nombre { get; set; }
        public int Data_Id_Cte { get; set; }
        public string Data_cte_nomComercial { get; set; }
        public int Data_IdProducto { get; set; }
        public string Data_TipoProducto { get; set; }
        public decimal Data_PrecLista { get; set; }
        public int Data_COP_Cantidad { get; set; }
        public string Data_TipoVenta { get; set; }
        public DateTime? Data_FechaAnalisis { get; set; }
        public decimal Data_MontoAnalisis { get; set; }
        public DateTime? Data_FechaPresentacion { get; set; }
        public decimal Data_MontoPresentacion { get; set; }
        public DateTime? Data_FechaNegociacion { get; set; }
        public decimal Data_MontoNegociacion { get; set; }
        public DateTime? Data_FechaCierre { get; set; }
        public decimal Data_MontoCierre { get; set; }
        public DateTime? Data_FechaCancelacion { get; set; }
        public decimal Data_MontoCancelacion { get; set; }
        public DateTime? Data_FechaModificacion { get; set; }
        public DateTime? Data_FechaCotizacion { get; set; }
        public decimal Data_MontoProyecto { get; set; }
        public int Data_Situacion { get; set; }

        #endregion

    }
}