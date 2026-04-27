using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ConceptoVariable
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

        private int _Id_ConceptoVariable;
        public int Id_ConceptoVariable
        {
            get { return _Id_ConceptoVariable; }
            set { _Id_ConceptoVariable = value; }
        }

        private string _Concepto_Descripcion;
        public string Concepto_Descripcion
        {
            get { return _Concepto_Descripcion; }
            set { _Concepto_Descripcion = value; }
        }

        private string _Concepto_Observaciones;
        public string Concepto_Observaciones
        {
            get { return _Concepto_Observaciones; }
            set { _Concepto_Observaciones = value; }
        }

        private string _Operador;
        public string Operador
        {
            get { return _Operador; }
            set { _Operador = value; }
        }

        private int _TipoVariable;
        public int TipoVariable
        {
            get { return _TipoVariable; }
            set { _TipoVariable = value; }
        }

        private int _IdVariable;
        public int IdVariable
        {
            get { return _IdVariable; }
            set { _IdVariable = value; }
        }

        private string _VariableDescripcion;
        public string VariableDescripcion
        {
            get { return _VariableDescripcion; }
            set { _VariableDescripcion = value; }
        }

        private DateTime? _FechaElaboracion;

        public DateTime? FechaElaboracion
        {
            get { return _FechaElaboracion; }
            set { _FechaElaboracion = value; }
        }

    }
}