using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    //
    // ENE28-2020 RFH Compras Locales 
    //

    public class eCL_SATProductosYServicios
    {
        private string _CveProdServ;
        public string CveProdServ
        {
            get { return _CveProdServ; }
            set { _CveProdServ = value; }
        }

        private string _DescProdServ;
        public string DescProdServ
        {
            get { return _DescProdServ; }
            set { _DescProdServ = value; }
        }

        //
    }
}