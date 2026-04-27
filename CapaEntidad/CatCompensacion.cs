using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CatCompensacion
    {
        int _id_Compensacion;
        string _compensacion_Descripcion;
        bool _compensacion_Estatus;
        string _EstatusStr;


        public int Id_Compensacion
        {
            get { return _id_Compensacion; }
            set { _id_Compensacion = value; }
        }
        public string Compensacion_Descripcion
        {
            get { return _compensacion_Descripcion; }
            set { _compensacion_Descripcion = value; }
        }
        public bool Compensacion_Estatus
        {
            get { return _compensacion_Estatus; }
            set { _compensacion_Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
    }
}