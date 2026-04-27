using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CatSolucion
    {
        private int _Id_Emp;
        private int _Id_Sol;
        private string _Sol_Descripcion;
        private int _Id_Area;
        private string _Area_Descripcion;
        private int _Sol_Potencial;
        private int _Sol_Activo;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Sol
        {
            get { return _Id_Sol; }
            set { _Id_Sol = value; }
        }
        public string Sol_Descripcion
        {
            get { return _Sol_Descripcion; }
            set { _Sol_Descripcion = value; }
        }

        public string Area_Descripcion
        {
            get { return _Area_Descripcion; }
            set { _Area_Descripcion = value; }
        }

        public int Id_Area
        {
            get { return _Id_Area; }
            set { _Id_Area = value; }
        }
        public int Sol_Potencial
        {
            get { return _Sol_Potencial; }
            set { _Sol_Potencial = value; }
        }
        public int Sol_Activo
        {
            get { return _Sol_Activo; }
            set { _Sol_Activo = value; }
        }

    }
}