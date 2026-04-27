using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eTerritoriosCteDetalle
    {
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

        string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        int _Cte_Activo;
        public int Cte_Activo
        {
            get { return _Cte_Activo; }
            set { _Cte_Activo = value; }
        }
        
        //

    }
}
