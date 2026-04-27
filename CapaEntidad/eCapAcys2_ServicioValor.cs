using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCapAcys2_ServicioValor
    {
        private int _IdCapAcysServicioValor;
        public int IdCapAcysServicioValor { get { return _IdCapAcysServicioValor; } set { _IdCapAcysServicioValor = value; } }
        
        private int _Id_Emp;
        public int Id_Emp { get { return _Id_Emp; } set { _Id_Emp = value; } }

        private int _Id_Cd;
        public int Id_Cd { get { return _Id_Cd; } set { _Id_Cd = value; } }

        private int _Id_Acs;
        public int Id_Acs { get { return _Id_Acs; } set { _Id_Acs = value; } }
                
        // Se refiere al tipo de Revicio
        private int _Tipo;        
        public int Tipo { get { return _Tipo; } set { _Tipo = value; } }

        private int _Tipo1;
        public int Tipo1 { get { return _Tipo1; } set { _Tipo1 = value; } }
        
        private int _Tipo2;
        public int Tipo2 { get { return _Tipo2; } set { _Tipo2 = value; } }

        private bool _Aplicar;
        public bool Aplicar { get { return _Aplicar; } set { _Aplicar = value; } }

        // L M M J V S D
        private bool _Lunes;
        public bool Lunes { get { return _Lunes; } set { _Lunes = value; } }        
        private bool _Martes;
        public bool Martes { get { return _Martes; } set { _Martes = value; } }        
        private bool _Miercoles;
        public bool Miercoles { get { return _Miercoles; } set { _Miercoles = value; } }        
        private bool _Jueves;
        public bool Jueves { get { return _Jueves; } set { _Jueves = value; } }        
        private bool _Viernes;
        public bool Viernes { get { return _Viernes; } set { _Viernes = value; } }
        private bool _Sabado;
        public bool Sabado { get { return _Sabado; } set { _Sabado = value; } }
        private bool _Domingo;
        public bool Domingo { get { return _Domingo; } set { _Domingo = value; } }        

        private bool _CualquierDia;        
        public bool CualquierDia { get { return _CualquierDia; } set { _CualquierDia = value; } }

        private string _HorariosRecep1;
        private string _HorariosRecep2;
        private string _HorariosRecep4; 
        private string _HorariosRecep3;        

        public string HorariosRecep1 { get { return _HorariosRecep1; } set { _HorariosRecep1 = value; } }        
        public string HorariosRecep2 { get { return _HorariosRecep2; } set { _HorariosRecep2 = value; } }        
        public string HorariosRecep3 { get { return _HorariosRecep3; } set { _HorariosRecep3 = value; } }        
        public string HorariosRecep4 { get { return _HorariosRecep4; } set { _HorariosRecep4 = value; } }

        private bool _CitaServ_MismoDia;
        public bool CitaServ_MismoDia { get { return _CitaServ_MismoDia; } set { _CitaServ_MismoDia = value; } }

        private bool _CitaServ_Previa;
        public bool CitaServ_Previa { get { return _CitaServ_Previa; } set { _CitaServ_Previa = value; } }

        private bool _ServRelleno;
        public bool ServRelleno { get { return _ServRelleno; } set { _ServRelleno = value; } }

        private bool _ServPreventivo;
        public bool ServPreventivo { get { return _ServPreventivo; } set { _ServPreventivo = value; } }

        private int _SelectorTipoServ;
        public int SelectorTipoServ { get { return _SelectorTipoServ; } set { _SelectorTipoServ = value; } }
        
        private string _PersonaRecibe;
        public string PersonaRecibe { get { return _PersonaRecibe; } set { _PersonaRecibe = value; } }

        private string _Puesto;
        public string Puesto { get { return _Puesto; } set { _Puesto = value; } }

        //OCT23-2019
        private string _QuienRecibe;
        public string QuienRecibe { get { return _QuienRecibe; } set { _QuienRecibe = value; } }

        private string _FuncionQuienRecibe;
        public string FuncionQuienRecibe { get { return _FuncionQuienRecibe; } set { _FuncionQuienRecibe = value; } }

        private int _Frecuencia;
        public int Frecuencia { get { return _Frecuencia; } set { _Frecuencia = value; } }       

    }
}
