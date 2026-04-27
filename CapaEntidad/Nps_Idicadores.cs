using System;

namespace CapaEntidad
{

    public class Nps_IndicadorNps
    {
        public int Id_Nps_Tipo { get; set; }
        public string Nps_descripcion { get; set; }
        public int Total { get; set; }
        public int GranTotal { get; set; }
        public decimal Porcentaje { get; set; }
    }

    public class Nps_IndicadorTrazabilidad
    {
        public int Id_Nps_Estatus { get; set; }
        public string Estatus_Descripcion { get; set; }
        public int Total { get; set; }
        public int GranTotal { get; set; }
        public decimal Porcentaje { get; set; }
    }
    public class Nps_IndicadorTrazabilidadCliente
    {
        public string Cte_NomComercial { get; set; }
        public int Total_Estatus1 { get; set; }
        public int Total_Estatus2 { get; set; }
        public int Total_Estatus3 { get; set; }
        public int Total_Estatus4 { get; set; }
        public int Total_Estatus5 { get; set; }
        public decimal Porcentaje_Estatus1 { get; set; }
        public decimal Porcentaje_Estatus2 { get; set; }
        public decimal Porcentaje_Estatus3 { get; set; }
        public decimal Porcentaje_Estatus4 { get; set; }
        public decimal Porcentaje_Estatus5 { get; set; }


    }


    public class Nps_IndicadorConversion
    {

        public string Cte_NomComercial { get; set; }
        public int Id_Nps_Tipo_Inicial { get; set; }
        public string Tipo_Inicial { get; set; }
        public int Valor_Inicial { get; set; }
        public int Id_Nps_Tipo_Final { get; set; }
        public string Tipo_Final { get; set; }
        public int Valor_Final { get; set; }
        public int Valor_Conversion { get; set; }
        public int Porcentaje_Conversion { get; set; }
    }


}