using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Embudo
    {
        private int _Ordern;
        private int _Id_Emp;
        private int _Id_Cd;
        private int _RSC;
        string _RowDesc;

        private int _A;
        private int _B;
        private int _C;
        private int _D;

        private decimal _VtaIn;
        private decimal _VtaPro;
        private decimal _VtaRe;
        private decimal _Por100VI;
        private decimal _Por100Pro;

        bool _Activo;

        public int Ordern
        {
            get { return _Ordern; }
            set { _Ordern = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int RSC
        {
            get { return _RSC; }
            set { _RSC = value; }
        }

        public string RowDesc
        {
            get { return _RowDesc; }
            set { _RowDesc = value; }
        }

        public int A
        {
            get { return _A; }
            set { _A = value; }
        }

        public int B
        {
            get { return _B; }
            set { _B = value; }
        }

        public int C
        {
            get { return _C; }
            set { _C = value; }
        }

        public int D
        {
            get { return _D; }
            set { _D = value; }
        }

        public decimal VtaIn
        {
            get { return _VtaIn; }
            set { _VtaIn = value; }
        }
        public decimal VtaRe
        {
            get { return _VtaRe; }
            set { _VtaRe = value; }
        }
        public decimal VtaPro
        {
            get { return _VtaPro; }
            set { _VtaPro = value; }
        }
        public decimal Por100VI
        {
            get { return _Por100VI; }
            set { _Por100VI = value; }
        }
        public decimal Por100Pro
        {
            get { return _Por100Pro; }
            set { _Por100Pro = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
