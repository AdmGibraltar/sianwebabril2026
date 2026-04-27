using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// 19 Mar 2019 Cracion RFH
//  


namespace CapaEntidad
{
    public class eCrmOportunidadesProducto
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

        private int _Id_Op;
        public int Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private int _Id_Area;
        public int Id_Area
        {
            get { return _Id_Area; }
            set { _Id_Area = value; }
        }

        private int _Id_Sol;
        public int Id_Sol
        {
            get { return _Id_Sol; }
            set { _Id_Sol = value; }
        }

        private int _Id_Apl;
        public int Id_Apl
        {
            get { return _Id_Apl; }
            set { _Id_Apl = value; }
        }

        private int _Id_SubFam;
        public int Id_SubFam
        {
            get { return _Id_SubFam; }
            set { _Id_SubFam = value; }
        }

        private long _Id_Prd;
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private decimal _COP_Cantidad;
        public decimal COP_Cantidad
        {
            get { return _COP_Cantidad; }
            set { _COP_Cantidad = value; }
        }

        private string _COP_Dilucion;
        public string COP_Dilucion
        {
            get { return _COP_Dilucion; }
            set { _COP_Dilucion = value; }
        }

        private bool _COP_EsQuimico;
        public bool COP_EsQuimico
        {
            get { return _COP_EsQuimico; }
            set { _COP_EsQuimico = value; }
        }

        private decimal _COP_CostoEnUso;
        public decimal COP_CostoEnUso
        {
            get { return _COP_CostoEnUso; }
            set { _COP_CostoEnUso = value; }
        }

        private decimal _COP_ConsumoMensual;
        public decimal COP_ConsumoMensual
        {
            get { return _COP_ConsumoMensual; }
            set { _COP_ConsumoMensual = value; }
        }

        private int _COP_DilucionAntecedente;
        public int COP_DilucionAntecedente
        {
            get { return _COP_DilucionAntecedente; }
            set { _COP_DilucionAntecedente = value; }
        }

        private int _COP_DilucionConsecuente;
        public int COP_DilucionConsecuente
        {
            get { return _COP_DilucionConsecuente; }
            set { _COP_DilucionConsecuente = value; }
        }

        private bool _AplDilucion;
        public bool AplDilucion
        {
            get { return _AplDilucion; }
            set { _AplDilucion = value; }
        }

        // JUN04-2020 RFH

        private int _Id_Val;
        public int Id_Val
        {
            get { return _Id_Val; }
            set { _Id_Val = value; }
        }

        private decimal _Cantidad;
        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private int _Accion;
        public int Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

        //JUN19-2020 RFH

        private string _ImagenProductoBajaRes;
        public string ImagenProductoBajaRes
        {
            get { return _ImagenProductoBajaRes; }
            set { _ImagenProductoBajaRes = value; }
        }

        private string _ImagenProductoAltaRes;
        public string ImagenProductoAltaRes
        {
            get { return _ImagenProductoAltaRes; }
            set { _ImagenProductoAltaRes = value; }
        }

        private int _Prd_Activo;
        public int Prd_Activo
        {
            get { return _Prd_Activo; }
            set { _Prd_Activo = value; }
        }
        //
    }
}