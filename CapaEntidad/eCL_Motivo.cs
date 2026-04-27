using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCL_Motivo
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private int _Id_MotivoCL;
        public int Id_MotivoCL
        {
            get { return _Id_MotivoCL; }
            set { _Id_MotivoCL = value; }
        }

        private string _Desc_MotivoCL;
        public string Desc_MotivoCL
        {
            get { return _Desc_MotivoCL; }
            set { _Desc_MotivoCL = value; }
        }

        private Double _PorcentajeAAA;
        public Double PorcentajeAAA
        {
            get { return _PorcentajeAAA; }
            set { _PorcentajeAAA = value; }
        }

        private int _Aplica;
        public int Aplica
        {
            get { return _Aplica; }
            set { _Aplica = value; }
        }

        //

    }
}