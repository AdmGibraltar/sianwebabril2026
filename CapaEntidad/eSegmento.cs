using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eSegmento
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private string _Seg_Descripcion;
        public string Seg_Descripcion
        {
            get { return _Seg_Descripcion; }
            set { _Seg_Descripcion = value; }
        }

        private int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private string _Seg_Unidades;
        public string Seg_Unidades
        {
            get { return _Seg_Unidades; }
            set { _Seg_Unidades = value; }
        }

        private double _Seg_ValUniDim;
        public double Seg_ValUniDim
        {
            get { return _Seg_ValUniDim; }
            set { _Seg_ValUniDim = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private bool _Seg_Activo;
        public bool Seg_Activo
        {
            get { return _Seg_Activo; }
            set { _Seg_Activo = value; }
        }
        private double _Seg_MetaUB;
        public double Seg_MetaUB
        {
            get { return _Seg_MetaUB; }
            set { _Seg_MetaUB = value; }
        }

        private string _Seg_IdXUen;
        public string Seg_IdXUen
        {
            get { return _Seg_IdXUen; }
            set { _Seg_IdXUen = value; }
        }


    }
}