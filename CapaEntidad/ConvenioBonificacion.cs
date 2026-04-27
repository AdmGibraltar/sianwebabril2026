using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConvenioBonificacion
    {


        public DateTime Fac_fecha { get; set; }
        public int Id_Cd { get; set; }
        public int Id_concesionario { get; set; }
        public string RazonSocial_Consecionario { get; set; }
        public string Nombrecdi { get; set; }
        public string id_documento { get; set; }
        public int Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public string codigoproveedor { get; set; }
        public int Id_Prd { get; set; }
        public string prd_descripcion { get; set; }
        public int Cantidad { get; set; }

        public double PrecioVta { get; set; }
        public double PAAANormal { get; set; }
        public double PCD_PrecioAAAEsp { get; set; }
        public double Bonificacion { get; set; }
        public double PC_Margen { get; set; }
        public int Id_PC { get; set; }
        public string PC_NoConvenio { get; set; }
        public string PC_Nombre { get; set; }
        public double PCD_PrecioVtaMin { get; set; }
        public double PCD_PrecioVtaMax { get; set; }
        public string origenvinculacion { get; set; }

        public DateTime Filtro_FechaInicial { get; set; }
        public DateTime Filtro_FechaFinal { get; set; }
        public int Filtro_Id_Cat { get; set; }
        public int Filtro_Version { get; set; }
        public int Filtro_Id_Cdinicial { get; set; }
        public int Filtro_Id_Cdfinal { get; set; }



    }
}

