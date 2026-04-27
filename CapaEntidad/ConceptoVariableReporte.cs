using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ConceptoVariableReporte
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Sistema;
        public int Id_Sistema
        {
            get { return _Id_Sistema; }
            set { _Id_Sistema = value; }
        }

        private int _Id_VariableReporte;
        public int Id_VariableReporte
        {
            get { return _Id_VariableReporte; }
            set { _Id_VariableReporte = value; }
        }

        private int? _Id_Parent;
        public int? Id_Parent
        {
            get { return _Id_Parent; }
            set { _Id_Parent = value; }
        }


        private int _Id_VariableLocal;
        public int Id_VariableLocal
        {
            get { return _Id_VariableLocal; }
            set { _Id_VariableLocal = value; }
        }

        private string _sVariable_Local;
        public string sVariable_Local
        {
            get { return _sVariable_Local; }
            set { _sVariable_Local = value; }
        }

        private string _sVariable_Descripcion;
        public string sVariable_Descripcion
        {
            get { return _sVariable_Descripcion; }
            set { _sVariable_Descripcion = value; }
        }

        private bool _esBold;
        public bool EsBold
        {
            get { return _esBold; }
            set { _esBold = value; }
        }


    }
}