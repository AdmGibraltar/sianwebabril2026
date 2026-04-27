using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class entDocVentas
    {
        public object objDocPDF  { get; set; }
        public object objDocXML  { get; set; }
        public string strUUID  { get; set; }
        
        public int Id_Fac { get; set; }
        
        public int Id_Ncr { get; set; }
        public int Id_Nca { get; set; }
        public int Id_PagDet { get; set; }

    }
}