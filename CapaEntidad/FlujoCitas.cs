using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FlujoCitas
    {
        private int _Id_TipoVisitaBase;
        private int _Id_Emp;
        private int _Id_Cd;
        string _TipoVisita;
        string _StrAlternancia;

        string _TD;

        private int _Id_TipoVisitaProximo;
        bool _Activo;

        public int Id_TipoVisitaBase
        {
            get { return _Id_TipoVisitaBase; }
            set { _Id_TipoVisitaBase = value; }
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

        public string TipoVisita
        {
            get { return _TipoVisita; }
            set { _TipoVisita = value; }
        }

        public string StrAlternancia
        {
            get { return _StrAlternancia; }
            set { _StrAlternancia = value; }
        }

        public int Id_TipoVisitaProximo
        {
            get { return _Id_TipoVisitaProximo; }
            set { _Id_TipoVisitaProximo = value; }
        }
        
        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public string TD
        {
            get { return _TD; }
            set { _TD = value; }
        }


    }
}
