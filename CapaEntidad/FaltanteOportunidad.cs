using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FaltanteOportunidad
    {
        private string tipoReporte;

        public string TipoReporte
        {
            get { return tipoReporte; }
            set { tipoReporte = value; }
        }
        private string tipoDesglose;

        public string TipoDesglose
        {
            get { return tipoDesglose; }
            set { tipoDesglose = value; }
        }
        private string desglose;

        public string Desglose
        {
            get { return desglose; }
            set { desglose = value; }
        }

        private int id_Cd;

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }


    }
}