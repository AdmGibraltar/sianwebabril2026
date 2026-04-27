using System;


namespace CapaEntidad
{
    public class ConfiguracionDias
    {
        public int Id_Cd { get; set; }
        public int Id_DF { get; set; }
        public int Id_Rik { get; set; }
        public DateTime DF_FechaIni { get; set; }
        public DateTime DF_FechaFin { get; set; }
        public int DF_Nivel { get; set; }
        public int DF_Tipo { get; set; }
        public string DF_Comentario { get; set; }
        public int Id_U { get; set; }
        public string DF_RepNombre { get; set; }
        public string DF_NivelStr { get; set; }
        public string DF_TipoStr { get; set; }
        public string CD_Nombre { get; set; }
    }
}
