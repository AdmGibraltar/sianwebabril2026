using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
  CRM 3 
  18 Ene 2019 RFH    
*/

namespace CapaEntidad
{
    public class eNotasProspecto
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

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private int _Id_Nota;
        public int Id_Nota
        {
            get { return _Id_Nota; }
            set { _Id_Nota = value; }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }


    }
}
