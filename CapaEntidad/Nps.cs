using System;

namespace CapaEntidad
{
    public class Nps
    {
        private int _Id_Nps;
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Rik;
        private int _Id_Clt;
        private int _Nps_Valor;
        private int _Id_Nps_Tipo;
        private string _Nps_Tipo;
        private string _Folio_Entrevista;
        private string _Cd_Nombre;
        private string _Rik_Nombre;
        private string _Cte_NomComercial;


        private DateTime _Fecha_Entrevista;
        private string _strFecha_Entrevista;
        private string _Entrevistado;
        private string _Puesto;
        private int _Id_U_Ins;
        private int _Id_U_Mod;
        private DateTime _Fecha_Ins;
        private DateTime _Fecha_Mod;

        private int _Nps_Valor_Inicial;
        private string _Nps_Tipo_Inicial;
        private int _Id_Nps_Tipo_Inicial;

        private int _Nps_Valor_Segundo;
        private string _Nps_Tipo_Segundo;
        private int _Id_Nps_Tipo_Segundo;

        private int _Nps_Valor_Final;
        private string _Nps_Tipo_Final;
        private int _Id_Nps_Tipo_Final;

        private int _Id_Nps_Estatus;
        private string _Nps_Estatus;

        public int Id_Nps
        {
            get { return _Id_Nps; }
            set { _Id_Nps = value; }
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
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Clt; }
            set { _Id_Clt = value; }
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
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }
        public string Folio_Entrevista
        {
            get { return _Folio_Entrevista; }
            set { _Folio_Entrevista = value; }
        }
        public DateTime Fecha_Entrevista
        {
            get { return _Fecha_Entrevista; }
            set { _Fecha_Entrevista = value; }
        }
        public string strFecha_Entrevista
        {
            get { return _strFecha_Entrevista; }
            set { _strFecha_Entrevista = value; }
        }
        public string Nps_Tipo
        {
            get { return _Nps_Tipo; }
            set { _Nps_Tipo = value; }
        }

        public string Entrevistado
        {
            get { return _Entrevistado; }
            set { _Entrevistado = value; }
        }
        public string Puesto
        {
            get { return _Puesto; }
            set { _Puesto = value; }
        }

        public int Id_U_Ins
        {
            get { return _Id_U_Ins; }
            set { _Id_U_Ins = value; }
        }

        public int Id_U_Mod
        {
            get { return _Id_U_Mod; }
            set { _Id_U_Mod = value; }
        }

        public DateTime Fecha_Ins
        {
            get { return _Fecha_Ins; }
            set { _Fecha_Ins = value; }
        }

        public DateTime Fecha_Mod
        {
            get { return _Fecha_Mod; }
            set { _Fecha_Mod = value; }
        }

        public int Nps_Valor_Inicial
        {
            get { return _Nps_Valor_Inicial; }
            set { _Nps_Valor_Inicial = value; }
        }
        public int Id_Nps_Tipo_Inicial
        {
            get { return _Id_Nps_Tipo_Inicial; }
            set { _Id_Nps_Tipo_Inicial = value; }
        }
        public string Nps_Tipo_Inicial
        {
            get { return _Nps_Tipo_Inicial; }
            set { _Nps_Tipo_Inicial = value; }
        }

        public int Nps_Valor_Segundo
        {
            get { return _Nps_Valor_Segundo; }
            set { _Nps_Valor_Segundo = value; }
        }
        public int Id_Nps_Tipo_Segundo
        {
            get { return _Id_Nps_Tipo_Segundo; }
            set { _Id_Nps_Tipo_Segundo = value; }
        }
        public string Nps_Tipo_Segundo
        {
            get { return _Nps_Tipo_Segundo; }
            set { _Nps_Tipo_Segundo = value; }
        }

        public int Nps_Valor_Final
        {
            get { return _Nps_Valor_Final; }
            set { _Nps_Valor_Final = value; }
        }
        public int Id_Nps_Tipo_Final
        {
            get { return _Id_Nps_Tipo_Final; }
            set { _Id_Nps_Tipo_Final = value; }
        }
        public string Nps_Tipo_Final
        {
            get { return _Nps_Tipo_Final; }
            set { _Nps_Tipo_Final = value; }
        }
        public int Id_Nps_Estatus
        {
            get { return _Id_Nps_Estatus; }
            set { _Id_Nps_Estatus = value; }
        }
        public string Nps_Estatus
        {
            get { return _Nps_Estatus; }
            set { _Nps_Estatus = value; }
        }
    }

    public class ItemsTextValue
    {

        public int intValue { get; set; }
        public int intText { get; set; }
        public string strValue { get; set; }
        public string strText { get; set; }
    }

    public class entRespuestaNps
    {
        public int intValor { get; set; }
        public string strMensaje { get; set; }
        public bool boolResultado { get; set; }
        public object objResultado { get; set; }
    }

    public class Nps_Filtro_Busqueda
    {

        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }

        public int Id_Nps_Tipo { get; set; }
        public int Id_Nps_Tema { get; set; }
        public int Id_Nps_Estatus { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Rik { get; set; }
        public string Buscar_Texto { get; set; }
        public string Cd_Nombre { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string strUEN { get; set; }
    }

    public class entNpsIndicadorFiltro
    {

        public string strStatus { get; set; }
        public string strCDs { get; set; }
        public string strUEN { get; set; }
        public int intRik { get; set; }
        public DateTime dateInicial { get; set; }
        public DateTime dateFinal { get; set; }
    }
}