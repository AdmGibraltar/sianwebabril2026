using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProEmbarqueDet
    {

        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Emb { get; set; }
        public int Id_EmbDet { get; set; }
        public int Emb_Tipo { get; set; }
        public int Id_Doc { get; set; }
        public string Id_DocStr { get; set; }
        public int? Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public double Doc_Importe { get; set; }
        public string Emb_TipoStr { get; set; }
        public DateTime? Doc_FechaIni { get; set; }
        public DateTime? Doc_FechaFin { get; set; }
        public string Doc_Estatus { get; set; }
        public string Doc_EstatusStr { get; set; }
        public DateTime Doc_Fecha { get; set; }
        public bool Seleccionado { get; set; }
        public string UniqueID { get; set; }

        //JFCV 19Ago2019
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public string DtDestino { get; set; }
        public int Importancia { get; set; }
        public int service_Time { get; set; }
        public string Campos_Avanzados { get; set; }



    }
}