using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    // ENE21-2020 RFH CL Unidad Medida Sat

    public class eUnidadMedidaSat
    {
        private string _CveUnidad;
        public string CveUnidad
        {
            get { return _CveUnidad; }
            set { _CveUnidad = value; }
        }

        private string _DescUnidad;
        public string DescUnidad
        {
            get { return _DescUnidad; }
            set { _DescUnidad = value; }
        }
        
    }
}
