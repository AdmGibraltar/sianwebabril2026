using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class OrdenCompra_Detalle
    {
        public string IdOC { get; set; }
        public Nullable<long> Id_PrdOld { get; set; }
        public Nullable<long> Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string Prd_Presentacion { get; set; }
        public string Prd_Unidad { get; set; }
        public Nullable<int> Inventario { get; set; }
        public Nullable<int> Prd_Cantidad { get; set; }
        public Nullable<int> Prd_CantidadOld { get; set; }
        public Nullable<double> Prd_Precio { get; set; }
        public Nullable<double> Prd_Importe { get; set; }
        public string Acs_Documento { get; set; }
        public int mes1 { get; set; }
        public int mes2 { get; set; }
        public int mes3 { get; set; }
        public double Acs_PrecioAcys { get; set; }
        public bool Acs_Mod { get; set; }
        public System.DateTime Acs_Fecha { get; set; }
        public int Ped_Asignar { get; set; }
        public string Acs_Dia { get; set; }
        public string Acs_DiaStr { get; set; }
        public int Acs_Frecuencia { get; set; }
        public Nullable<int> Prd_RemFact { get; set; }

        public int Id_TG { get; set; }
        public int Id_Acs { get; set; }
        public string ACS_ReqOC { get; set; }
        public double Prd_PrecioLista { get; set; }
        public string Tipo_producto { get; set; }
        public int Prd_Cantidadold { get; set; }
        public int Prd_Activo { get; set; }
    }
}