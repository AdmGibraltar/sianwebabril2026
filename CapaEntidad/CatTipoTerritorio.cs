using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CatTipoTerritorio
    {
        int _Id_Tipo;
        string _Descripcion;

        public int Id_Tipo
        {
            get { return _Id_Tipo; }
            set { _Id_Tipo = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
    }
}