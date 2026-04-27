using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eTerritoriosCte
    {
        int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        int _Id_CteDet;
        public int Id_CteDet
        {
            get { return _Id_CteDet; }
            set { _Id_CteDet = value; }
        }

        int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        
        string _Id_Terr;
        public string Id_Terr
        {
            get { return _Id_Terr; }
            set { _Id_Terr = value; }
        }

        string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        Double _Cte_Potencial;
        public Double Cte_Potencial
        {
            get { return _Cte_Potencial; }
            set { _Cte_Potencial = value; }
        }

        // 16 Ene 2019 RFH

        int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik= value; }
        }

        int _Ter_Activo;
        public int Ter_Activo
        {
            get { return _Ter_Activo; }
            set { _Ter_Activo = value; }
        }

        int _Ter_Asociado;
        public int Ter_Asociado
        {
            get { return _Ter_Asociado; }
            set { _Ter_Asociado = value; }
        }

        int _TerDeRik;
        public int TerDeRik
        {
            get { return _TerDeRik; }
            set { _TerDeRik = value; }
        }

        int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        Double _VPOMeta;
        public Double VPOMeta
        {
            get { return _VPOMeta; }
            set { _VPOMeta = value; }
        }


        //        

    }
}
