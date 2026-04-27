using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CapAcys_EspecAdi
    {
        private int _IdCapAcysEspecAdi;
        public int IdCapAcysEspecAdi { get { return _IdCapAcysEspecAdi; } set { _IdCapAcysEspecAdi = value; } }

        private int _Id_Emp;
        public int Id_Emp { get { return _Id_Emp; } set { _Id_Emp = value; } }

        private int _Id_Cd;
        public int Id_Cd { get { return _Id_Cd; } set { _Id_Cd = value; } }

        private int _Id_Acs;
        public int Id_Acs { get { return _Id_Acs; } set { _Id_Acs = value; } }

        private bool _Activo;
        public bool Activo { get { return _Activo; } set { _Activo = value; } }

        private bool _FactKeyChb1;
        private bool _FactKeyChb2;
        private int _FactKeyCopias1;
        private int _FactKeyCopias2;

        public bool FactKeyChb1 { get { return _FactKeyChb1; } set { _FactKeyChb1 = value; } }
        public bool FactKeyChb2 { get { return _FactKeyChb2; } set { _FactKeyChb2 = value; } }
        public int FactKeyCopias1 { get { return _FactKeyCopias1; } set { _FactKeyCopias1 = value; } }
        public int FactKeyCopias2 { get { return _FactKeyCopias2; } set { _FactKeyCopias2 = value; } }

        private bool _Ordcompchb1;
        private bool _Ordcompchb2;
        private int _OrdcompCopias1;
        private int _OrdcompCopias2;

        public bool Ordcompchb1 { get { return _Ordcompchb1; } set { _Ordcompchb1 = value; } }
        public bool Ordcompchb2 { get { return _Ordcompchb2; } set { _Ordcompchb2 = value; } }
        public int OrdcompCopias1 { get { return _OrdcompCopias1; } set { _OrdcompCopias1 = value; } }
        public int OrdcompCopias2 { get { return _OrdcompCopias2; } set { _OrdcompCopias2 = value; } }

        private bool _OrdRepChb1;
        private bool _OrdRepChb2;
        private int _OrdRepCopias1;
        private int _OrdRepCopias2;

        public bool OrdRepChb1 { get { return _OrdRepChb1; } set { _OrdRepChb1 = value; } }
        public bool OrdRepChb2 { get { return _OrdRepChb2; } set { _OrdRepChb2 = value; } }
        public int OrdRepCopias1 { get { return _OrdRepCopias1; } set { _OrdRepCopias1 = value; } }
        public int OrdRepCopias2 { get { return _OrdRepCopias2; } set { _OrdRepCopias2 = value; } }

        private bool _CopPedChb1;
        private bool _CopPedChb2;
        private int _CopPedCopias1;
        private int _CopPedCopias2;

        public bool CopPedChb1 { get { return _CopPedChb1; } set { _CopPedChb1 = value; } }
        public bool CopPedChb2 { get { return _CopPedChb2; } set { _CopPedChb2 = value; } }
        public int CopPedCopias1 { get { return _CopPedCopias1; } set { _CopPedCopias1 = value; } }
        public int CopPedCopias2 { get { return _CopPedCopias2; } set { _CopPedCopias2 = value; } }

        private bool _RemChb1;
        private bool _RemChb2;
        private int _RemCopias1;
        private int _RemCopias2;

        public bool RemChb1 { get { return _RemChb1; } set { _RemChb1 = value; } }
        public bool RemChb2 { get { return _RemChb2; } set { _RemChb2 = value; } }
        public int RemCopias1 { get { return _RemCopias1; } set { _RemCopias1 = value; } }
        public int RemCopias2 { get { return _RemCopias2; } set { _RemCopias2 = value; } }
        private bool _CerChb1;
        private bool _CerChb2;
        private int _CerCopias1;
        private int _CerCopias2;

        public bool CerChb1 { get { return _CerChb1; } set { _CerChb1 = value; } }
        public bool CerChb2 { get { return _CerChb2; } set { _CerChb2 = value; } }
        public int CerCopias1 { get { return _CerCopias1; } set { _CerCopias1 = value; } }
        public int CerCopias2 { get { return _CerCopias2; } set { _CerCopias2 = value; } }


    }
}
