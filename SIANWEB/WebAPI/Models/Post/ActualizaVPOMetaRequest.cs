using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIANWEB.WebAPI.Models.Post
{
    public class ActualizaVPOMetaRequest
    {
        public int Id_Cte { get; set; }
        public int Id_Ter { get; set; }
        public float VPOMeta { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int IdUen { get; set; }
        public int IdSegmento { get; set; }
        public float VPT { get; set; }
        public float Cantidad { get; set; }
    }
}