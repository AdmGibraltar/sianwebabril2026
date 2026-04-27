using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCliente
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

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }


        private int _Cte;
        public int Cte
        {
            get { return _Cte; }
            set { _Cte = value; }
        }

        private string _RFC;
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        private string _Cte_Contacto;
        public string Cte_Contacto
        {
            get { return _Cte_Contacto; }
            set { _Cte_Contacto = value; }
        }

        private string _Cte_Email;
        public string Cte_Email
        {
            get { return _Cte_Email; }
            set { _Cte_Email = value; }
        }

        private string _Cte_FacCalle;
        public string Cte_FacCalle
        {
            get { return _Cte_FacCalle; }
            set { _Cte_FacCalle = value; }
        }

        private string _Cte_FacNumero;
        public string Cte_FacNumero
        {
            get { return _Cte_FacNumero; }
            set { _Cte_FacNumero = value; }
        }

        private string _Cte_FacColonia;
        public string Cte_FacColonia
        {
            get { return _Cte_FacColonia; }
            set { _Cte_FacColonia = value; }
        }

        private string _Cte_Telefono;
        public string Cte_Telefono
        {
            get { return _Cte_Telefono; }
            set { _Cte_Telefono = value; }
        }

        private string _Cte_FacMunicipio;
        public string Cte_FacMunicipio
        {
            get { return _Cte_FacMunicipio; }
            set { _Cte_FacMunicipio = value; }
        }

        private string _Cte_FacEstado;
        public string Cte_FacEstado
        {
            get { return _Cte_FacEstado; }
            set { _Cte_FacEstado = value; }
        }

        private string _Cte_FacCP;
        public string Cte_FacCP
        {
            get { return _Cte_FacCP; }
            set { _Cte_FacCP = value; }
        }

        private string _Cte_FacRfc;
        public string Cte_FacRfc
        {
            get { return _Cte_FacRfc; }
            set { _Cte_FacRfc = value; }
        }

        private string _Cte_FacTelefonos;
        public string Cte_FacTelefonos
        {
            get { return _Cte_FacTelefonos; }
            set { _Cte_FacTelefonos = value; }
        }

        private string _Cte_FacTel;
        public string Cte_FacTel
        {
            get { return _Cte_FacTel; }
            set { _Cte_FacTel = value; }
        }

        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private double _VPObservado;
        public double VPObservado
        {
            get { return _VPObservado; }
            set { _VPObservado = value; }
        }

        private int _UEN;
        public int UEN
        {
            get { return _UEN; }
            set { _UEN = value; }
        }

        private int _Segmento;
        public int Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }

        private Int32 _Id_Ter;
        public Int32 Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _TerNombre;
        public string TerNombre
        {
            get { return _TerNombre; }
            set { _TerNombre = value; }
        }

        private int _Activo;
        public int Activo
        { get { return _Activo; } set { _Activo = value; } }


        private Int32 _RegistrosEcontrados;
        public Int32 RegistrosEcontrados
        {
            get { return _RegistrosEcontrados; }
            set { _RegistrosEcontrados = value; }
        }


        private int _Cte_Efectivo;
        public int Cte_Efectivo
        { get { return _Cte_Efectivo; } set { _Cte_Efectivo = value; } }

        private int _Cte_Factoraje;
        public int Cte_Factoraje
        { get { return _Cte_Factoraje; } set { _Cte_Factoraje = value; } }

        private int _Cte_Transferencia;
        public int Cte_Transferencia
        { get { return _Cte_Transferencia; } set { _Cte_Transferencia = value; } }

        private int _Cte_Cheque;
        public int Cte_Cheque
        { get { return _Cte_Cheque; } set { _Cte_Cheque = value; } }

        private int _Cte_Credito;
        public int Cte_Credito
        { get { return _Cte_Credito; } set { _Cte_Credito = value; } }

        private int _Cte_TarjetaDebito;
        public int Cte_TarjetaDebito
        { get { return _Cte_TarjetaDebito; } set { _Cte_TarjetaDebito = value; } }


        private double _Cte_CondPago;
        public double Cte_CondPago
        { get { return _Cte_CondPago; } set { _Cte_CondPago = value; } }

        private double _Cte_LimCobr;
        public double Cte_LimCobr
        { get { return _Cte_LimCobr; } set { _Cte_LimCobr = value; } }


        private string _Cte_RHoraam1;
        public string Cte_RHoraam1
        {
            get { return _Cte_RHoraam1; }
            set { _Cte_RHoraam1 = value; }
        }

        private string _Cte_RHoraam2;
        public string Cte_RHoraam2
        {
            get { return _Cte_RHoraam2; }
            set { _Cte_RHoraam2 = value; }
        }

        private string _Cte_RHorapm1;
        public string Cte_RHorapm1
        {
            get { return _Cte_RHorapm1; }
            set { _Cte_RHorapm1 = value; }
        }

        private string _Cte_RHorapm2;
        public string Cte_RHorapm2
        {
            get { return _Cte_RHorapm2; }
            set { _Cte_RHorapm2 = value; }
        }


        private Int32 _Cte_CPLunes;
        public Int32 Cte_CPLunes
        { get { return _Cte_CPLunes; } set { _Cte_CPLunes = value; } }

        private Int32 _Cte_CPMartes;
        public Int32 Cte_CPMartes
        { get { return _Cte_CPMartes; } set { _Cte_CPMartes = value; } }

        private Int32 _Cte_CPMiercoles;
        public Int32 Cte_CPMiercoles
        { get { return _Cte_CPMiercoles; } set { _Cte_CPMiercoles = value; } }

        private Int32 _Cte_CPJueves;
        public Int32 Cte_CPJueves
        { get { return _Cte_CPJueves; } set { _Cte_CPJueves = value; } }

        private Int32 _Cte_CPViernes;
        public Int32 Cte_CPViernes
        { get { return _Cte_CPViernes; } set { _Cte_CPViernes = value; } }

        private Int32 _Cte_CPSabado;
        public Int32 Cte_CPSabado
        { get { return _Cte_CPSabado; } set { _Cte_CPSabado = value; } }

        private Int32 _Cte_CPCualquierDia;
        public Int32 Cte_CPCualquierDia
        { get { return _Cte_CPCualquierDia; } set { _Cte_CPCualquierDia = value; } }


        private string _Cte_PHoraam1;
        public string Cte_PHoraam1
        {
            get { return _Cte_PHoraam1; }
            set { _Cte_PHoraam1 = value; }
        }
        private string _Cte_PHoraam2;
        public string Cte_PHoraam2
        {
            get { return _Cte_PHoraam2; }
            set { _Cte_PHoraam2 = value; }
        }
        private string _Cte_PHorapm1;
        public string Cte_PHorapm1
        {
            get { return _Cte_PHorapm1; }
            set { _Cte_PHorapm1 = value; }
        }
        private string _Cte_PHorapm2;
        public string Cte_PHorapm2
        {
            get { return _Cte_PHorapm2; }
            set { _Cte_PHorapm2 = value; }
        }

        private Int32 _Cte_RLunes;
        public Int32 Cte_RLunes
        { get { return _Cte_RLunes; } set { _Cte_RLunes = value; } }

        private Int32 _Cte_RMartes;
        public Int32 Cte_RMartes
        { get { return _Cte_RMartes; } set { _Cte_RMartes = value; } }

        private Int32 _Cte_RMiercoles;
        public Int32 Cte_RMiercoles
        { get { return _Cte_RMiercoles; } set { _Cte_RMiercoles = value; } }

        private Int32 _Cte_RJueves;
        public Int32 Cte_RJueves
        { get { return _Cte_RJueves; } set { _Cte_RJueves = value; } }

        private Int32 _Cte_RViernes;
        public Int32 Cte_RViernes
        { get { return _Cte_RViernes; } set { _Cte_RViernes = value; } }

        private Int32 _Cte_RSabado;
        public Int32 Cte_RSabado
        { get { return _Cte_RSabado; } set { _Cte_RSabado = value; } }

        // Listado de Tipos de Pago
        // 07DIC-2020

        private Int32 _Efectivo_1;
        public Int32 Efectivo_1
        { get { return _Efectivo_1; } set { _Efectivo_1 = value; } }

        private Int32 _Cheque_2;
        public Int32 Cheque_2
        { get { return _Cheque_2; } set { _Cheque_2 = value; } }

        private Int32 _TransferenciaE_3;
        public Int32 TransferenciaE_3
        { get { return _TransferenciaE_3; } set { _TransferenciaE_3 = value; } }

        private Int32 _TarjetaDebito_4;
        public Int32 TarjetaDebito_4
        { get { return _TarjetaDebito_4; } set { _TarjetaDebito_4 = value; } }

        private Int32 _MonederoE_5;
        public Int32 MonederoE_5
        { get { return _MonederoE_5; } set { _MonederoE_5 = value; } }

        private Int32 _DineroE_6;
        public Int32 DineroE_6
        { get { return _DineroE_6; } set { _DineroE_6 = value; } }

        private Int32 _ValdesDesp_7;
        public Int32 ValdesDesp_7
        { get { return _ValdesDesp_7; } set { _ValdesDesp_7 = value; } }



        // JUN19-2020 RFH

        private int _Id_TCte;
        public int Id_TCte
        {
            get { return _Id_TCte; }
            set { _Id_TCte = value; }
        }

    }
}