using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ConceptoVariables
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

        private string _sVariable_Comentarios;
        public string sVariable_Comentarios
        {
            get { return _sVariable_Comentarios; }
            set { _sVariable_Comentarios = value; }
        }

        private string _sVariable_Formula;
        public string sVariable_Formula
        {
            get { return _sVariable_Formula; }
            set { _sVariable_Formula = value; }
        }





    }
}