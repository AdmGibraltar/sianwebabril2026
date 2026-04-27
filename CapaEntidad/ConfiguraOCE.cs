using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ConfiguraOCE
    {

        private int _IdConfiguraOCE;
        private int _Id_Cd;
        private string _Sucursal;
        private string _IdCveConfiguraOCE;
        private string _ConceptoConfiguraOCE;
        private string _ValorConfiguraOCE;
        private string _iActivo;
        private string _Condicion0;
        private string _Param01;
        private string _Param02;
        private string _Factor1;
        private string _Factor2;
        private string _Multiplicador1;
        private string _Factor3;
        private string _Multiplicador2;

        ///     Id_Cd IdCveConfiguraOCE   
        ///     ConceptoConfiguraOCE ValorConfiguraOCE   
        ///     iActivo Condicion0  
        ///     Param01 Param02 
        ///     Factor1 Factor2 
        ///     Multiplicador1 
        ///     Factor3 Multiplicador2

        public int IdConfiguraOCE
        {
            get { return _IdConfiguraOCE; }
            set { _IdConfiguraOCE = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        public string IdCveConfiguraOCE
        {
            get { return _IdCveConfiguraOCE; }
            set { _IdCveConfiguraOCE = value; }
        }

        public string ConceptoConfiguraOCE
        {
            get { return _ConceptoConfiguraOCE; }
            set { _ConceptoConfiguraOCE = value; }
        }

        public string ValorConfiguraOCE
        {
            get { return _ValorConfiguraOCE; }
            set { _ValorConfiguraOCE = value; }
        }

        public string iActivo
        {
            get { return _iActivo; }
            set { _iActivo = value; }
        }

        public string Condicion0
        {
            get { return _Condicion0; }
            set { _Condicion0 = value; }
        }
        public string Param01
        {
            get { return _Param01; }
            set { _Param01 = value; }
        }

        public string Param02
        {
            get { return _Param02; }
            set { _Param02 = value; }
        }

        public string Factor1
        {
            get { return _Factor1; }
            set { _Factor1 = value; }
        }

        public string Factor2
        {
            get { return _Factor2; }
            set { _Factor2 = value; }
        }

        public string Multiplicador1
        {
            get { return _Multiplicador1; }
            set { _Multiplicador1 = value; }
        }

        public string Factor3
        {
            get { return _Factor3; }
            set { _Factor3 = value; }
        }

        public string Multiplicador2
        {
            get { return _Multiplicador2; }
            set { _Multiplicador2 = value; }
        }

    }

}
