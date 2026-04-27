using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 8 Mar 2019 Creacion RFH

namespace CapaEntidad
{
    public class eGraficaInformes
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

        int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        string _Etapa;
        public string Etapa
        {
            get { return _Etapa; }
            set { _Etapa = value; }
        }
        
        double _Valor;
        public double Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }


    }
}
