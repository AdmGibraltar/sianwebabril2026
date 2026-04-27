using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 26 Nov 2018 RFH - Acys 2  
namespace CapaEntidad
{
    public class eCapAcys2_TipoServicio
    {
        private int _IdCapAcys_ServicioValor_Tipo;
        public int IdCapAcys_ServicioValor_Tipo { 
            get { return _IdCapAcys_ServicioValor_Tipo; } 
            set { _IdCapAcys_ServicioValor_Tipo = value; } 
        }

        private int _IdTipoServicio;
        public int IdTipoServicio
        {
            get { return _IdTipoServicio; }
            set { _IdTipoServicio = value; }
        }

        private int _IdTipoServicioPadre;
        public int IdTipoServicioPadre
        {
            get { return _IdTipoServicioPadre; }
            set { _IdTipoServicioPadre = value; }
        }

        private string _TipoServicioNombre;
        public string TipoServicioNombre
        {
            get { return _TipoServicioNombre; }
            set { _TipoServicioNombre = value; }
        }

        //
    }
}
