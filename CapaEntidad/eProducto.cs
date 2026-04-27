using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// 19 Mar 2019 Cracion RFH
//  

namespace CapaEntidad
{
    public class eProducto
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

        private Int64 _Id_Prd;
        public Int64 Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Id_PrdLoc;
        public int Id_PrdLoc
        {
            get { return _Id_PrdLoc; }
            set { _Id_PrdLoc = value; }
        }

        private int _Id_Spo;
        public int Id_Spo
        {
            get { return _Id_Spo; }
            set { _Id_Spo = value; }
        }

        private int _Id_Ptp;
        public int Id_Ptp
        {
            get { return _Id_Ptp; }
            set { _Id_Ptp = value; }
        }

        private int _Id_Cpr;
        public int Id_Cpr
        {
            get { return _Id_Cpr; }
            set { _Id_Cpr = value; }
        }

        private int _Id_Fam;
        public int Id_Fam
        {
            get { return _Id_Fam; }
            set { _Id_Fam = value; }
        }

        private int _Id_Sub;
        public int Id_Sub
        {
            get { return _Id_Sub; }
            set { _Id_Sub = value; }
        }

        private int _Id_Pvd;
        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private string _Prd_Presentacion;
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        private int _Prd_AgrupadoSpo;
        public int Prd_AgrupadoSpo
        {
            get { return _Prd_AgrupadoSpo; }
            set { _Prd_AgrupadoSpo = value; }
        }

        private float _Prd_FactorConv;
        public float Prd_FactorConv
        {
            get { return _Prd_FactorConv; }
            set { _Prd_FactorConv = value; }
        }

        private bool _Prd_AparatoSisProp;
        public bool Prd_AparatoSisProp
        {
            get { return _Prd_AparatoSisProp; }
            set { _Prd_AparatoSisProp = value; }
        }

        private int _Prd_InvSeg;
        public int Prd_InvSeg
        {
            get { return _Prd_InvSeg; }
            set { _Prd_InvSeg = value; }
        }

        private string _Prd_UniNe;
        public string Prd_UniNe
        {
            get { return _Prd_UniNe; }
            set { _Prd_UniNe = value; }
        }

        private string _Prd_UniNs;
        public string Prd_UniNs
        {
            get { return _Prd_UniNs; }
            set { _Prd_UniNs = value; }
        }

        private int _Prd_Unico;
        public int Prd_Unico
        {
            get { return _Prd_Unico; }
            set { _Prd_Unico = value; }
        }

        private float _Prd_UniEmp;
        public float Prd_UniEmp
        {
            get { return _Prd_UniEmp; }
            set { _Prd_UniEmp = value; }
        }

        private bool _Prd_Colo;
        public bool Prd_Colo
        {
            get { return _Prd_Colo; }
            set { _Prd_Colo = value; }
        }

        private string _Prd_Ren;
        public string Prd_Ren
        {
            get { return _Prd_Ren; }
            set { _Prd_Ren = value; }
        }

        private int _Prd_Mes;
        public int Prd_Mes
        {
            get { return _Prd_Mes; }
            set { _Prd_Mes = value; }
        }

        private string _Prd_Ubicacion;
        public string Prd_Ubicacion
        {
            get { return _Prd_Ubicacion; }
            set { _Prd_Ubicacion = value; }
        }

        private float _Prd_Contribucion;
        public float Prd_Contribucion
        {
            get { return _Prd_Contribucion; }
            set { _Prd_Contribucion = value; }
        }

        private bool _Prd_Nuevo;
        public bool Prd_Nuevo
        {
            get { return _Prd_Nuevo; }
            set { _Prd_Nuevo = value; }
        }

        private float _Prd_PesConTecnico;
        public float Prd_PesConTecnico
        {
            get { return _Prd_PesConTecnico; }
            set { _Prd_PesConTecnico = value; }
        }

        private string _Prd_CptSv;
        public string Prd_CptSv
        {
            get { return _Prd_CptSv; }
            set { _Prd_CptSv = value; }
        }

        private bool _Prd_Activo;
        public bool Prd_Activo
        {
            get { return _Prd_Activo; }
            set { _Prd_Activo = value; }
        }

        private string _Prd_FecAlta;
        public string Prd_FecAlta
        {
            get { return _Prd_FecAlta; }
            set { _Prd_FecAlta = value; }
        }

        private float _Prd_PorUtilidades;
        public float Prd_PorUtilidades
        {
            get { return _Prd_PorUtilidades; }
            set { _Prd_PorUtilidades = value; }
        }

        private string _Prd_PlanAbasto;
        public string Prd_PlanAbasto
        {
            get { return _Prd_PlanAbasto; }
            set { _Prd_PlanAbasto = value; }
        }

        private int _Prd_Minimo;
        public int Prd_Minimo
        {
            get { return _Prd_Minimo; }
            set { _Prd_Minimo = value; }
        }

        private bool _Prd_NoFacturable;
        public bool Prd_NoFacturable
        {
            get { return _Prd_NoFacturable; }
            set { _Prd_NoFacturable = value; }
        }

        private string _Prd_ClaveProdServ;
        public string Prd_ClaveProdServ
        {
            get { return _Prd_ClaveProdServ; }
            set { _Prd_ClaveProdServ = value; }
        }

        private string _Prd_ClaveUnidad;
        public string Prd_ClaveUnidad
        {
            get { return _Prd_ClaveUnidad; }
            set { _Prd_ClaveUnidad = value; }
        }

        //

    }
}