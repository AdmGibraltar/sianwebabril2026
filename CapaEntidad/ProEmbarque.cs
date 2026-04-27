using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProEmbarque
    {

        public int? Filtro_Id_Emb { get; set; }
        public DateTime? Filtro_EmbFechaIni { get; set; }
        public DateTime? Filtro_EmbFechaFin { get; set; }
        public int? Filtro_Emb_Destino { get; set; }

        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Emb { get; set; }
        public int Id_U { get; set; }
        public string Emb_EstatusStr { get; set; }
        public string U_Nombre { get; set; }
        public string Emb_DestinoStr { get; set; }
        public int Emb_Destino { get; set; }
        public DateTime? Emb_Fecha { get; set; }
        public DateTime? Emb_Dia { get; set; }
        public string Emb_Chofer { get; set; }
        public string Emb_Camioneta { get; set; }
    }
}