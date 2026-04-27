using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    /*
     * ENE28-2020 RFH Compras Locales
     * 
     * */

    public class eCL_CompraLocalDet
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

        private int _Id_Comp;
        public int Id_Comp
        {
            get { return _Id_Comp; }
            set { _Id_Comp = value; }
        }

        private int _Id_CompDet;
        public int Id_CompDet
        {
            get { return _Id_CompDet; }
            set { _Id_CompDet = value; }
        }

        private long _Id_Prd;
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private long _Id_PrdOriginal;
        public long Id_PrdOriginal
        {
            get { return _Id_PrdOriginal; }
            set { _Id_PrdOriginal = value; }
        }

        private float _Det_Costo;
        public float Det_Costo
        {
            get { return _Det_Costo; }
            set { _Det_Costo = value; }
        }

        private int _Det_Estatus;
        public int Det_Estatus
        {
            get { return _Det_Estatus; }
            set { _Det_Estatus = value; }
        }

        private string _Det_FecAut;
        public string Det_FecAut
        {
            get { return _Det_FecAut; }
            set { _Det_FecAut = value; }
        }

        private int _Det_Enfocada;
        public int Det_Enfocada
        {
            get { return _Det_Enfocada; }
            set { _Det_Enfocada = value; }
        }

        private int _Det_Autorizo;
        public int Det_Autorizo
        {
            get { return _Det_Autorizo; }
            set { _Det_Autorizo = value; }
        }
        // 20Abr2022
        private int _IdTipoProducto;
        public int IdTipoProducto
        {
            get { return _IdTipoProducto; }
            set { _IdTipoProducto = value; }
        }

        private int _IdUsuarioAutorizador;
        public int IdUsuarioAutorizador
        {
            get { return _IdUsuarioAutorizador; }
            set { _IdUsuarioAutorizador = value; }
        }

        //

    }
}