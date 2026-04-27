using System;

namespace CapaEntidad
{
    public class NpsQueja
    {
        private int _Id_Nps_Queja;
        private int _Id_Nps;
        private int _Id_Nps_Tema;
        private string _Nps_Tema;
        private string _Otro_Tema;
        private int _Nps_Valor;
        private int _Id_Nps_Tipo;
        private string _Nps_Tipo;
        private string _Nps_Queja;
        private string _Nps_Protocolo;
        private int _Nps_Valor_Final;
        private int _Id_Nps_EstatusActual;

        private string _Nps_PlanAccion;
        private string _strFecha_Compromiso;
        private DateTime _Fecha_Compromiso;

        private string _Nps_PlanAccion_Segundo;
        private string _strFecha_Compromiso_Segundo;
        private DateTime _Fecha_Compromiso_Segundo;

        public int Id_Nps_Queja
        {
            get { return _Id_Nps_Queja; }
            set { _Id_Nps_Queja = value; }
        }

        public int Id_Nps
        {
            get { return _Id_Nps; }
            set { _Id_Nps = value; }
        }

        public int Id_Nps_Tema
        {
            get { return _Id_Nps_Tema; }
            set { _Id_Nps_Tema = value; }
        }
        public string Nps_Tema
        {
            get { return _Nps_Tema; }
            set { _Nps_Tema = value; }
        }

        public string Otro_Tema
        {
            get { return _Otro_Tema; }
            set { _Otro_Tema = value; }
        }

        public int Nps_Valor
        {
            get { return _Nps_Valor; }
            set { _Nps_Valor = value; }
        }

        public int Id_Nps_Tipo
        {
            get { return _Id_Nps_Tipo; }
            set { _Id_Nps_Tipo = value; }
        }

        public string Nps_Tipo
        {
            get { return _Nps_Tipo; }
            set { _Nps_Tipo = value; }
        }

        public string Nps_Queja
        {
            get { return _Nps_Queja; }
            set { _Nps_Queja = value; }
        }
        public string Nps_Protocolo
        {
            get { return _Nps_Protocolo; }
            set { _Nps_Protocolo = value; }
        }
        public int Nps_Valor_Final
        {
            get { return _Nps_Valor_Final; }
            set { _Nps_Valor_Final = value; }
        }
        public int Id_Nps_EstatusActual
        {
            get { return _Id_Nps_EstatusActual; }
            set { _Id_Nps_EstatusActual = value; }
        }

        public string Nps_PlanAccion
        {
            get { return _Nps_PlanAccion; }
            set { _Nps_PlanAccion = value; }
        }

        public string strFecha_Compromiso
        {
            get { return _strFecha_Compromiso; }
            set { _strFecha_Compromiso = value; }
        }

        public DateTime Fecha_Compromiso
        {
            get { return _Fecha_Compromiso; }
            set { _Fecha_Compromiso = value; }
        }

        public string Nps_PlanAccion_Segundo
        {
            get { return _Nps_PlanAccion_Segundo; }
            set { _Nps_PlanAccion_Segundo = value; }
        }

        public string strFecha_Compromiso_Segundo
        {
            get { return _strFecha_Compromiso_Segundo; }
            set { _strFecha_Compromiso_Segundo = value; }
        }

        public DateTime Fecha_Compromiso_Segundo
        {
            get { return _Fecha_Compromiso_Segundo; }
            set { _Fecha_Compromiso_Segundo = value; }
        }
    }

}