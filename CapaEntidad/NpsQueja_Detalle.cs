using System;
namespace CapaEntidad
{
    public class NpsQueja_Detalle
    {
        private int _Id_Nps_Queja;
        private int _Id_Nps;
        private int _Id_Cd;
        private int _Id_Nps_Estatus;
        private int _Id_Rik;
        private int _Id_Cte;
        private int _Id_Nps_Plan;
        private string _Rik_Nombre;
        private string _Cte_Nomcomercial;
        private string _Folio_Entrevista;
        private string _Fecha_Entrevista;
        private string _Nps_Descripcion;
        private string _Nps_Tema;
        private string _Cd_Nombre;
        private string _Nps_Plan;
        private string _Nps_Estatus;

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

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Nps_Estatus
        {
            get { return _Id_Nps_Estatus; }
            set { _Id_Nps_Estatus = value; }
        }

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public int Id_Nps_Plan
        {
            get { return _Id_Nps_Plan; }
            set { _Id_Nps_Plan = value; }
        }
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        public string Cte_Nomcomercial
        {
            get { return _Cte_Nomcomercial; }
            set { _Cte_Nomcomercial = value; }
        }

        public string Folio_Entrevista
        {
            get { return _Folio_Entrevista; }
            set { _Folio_Entrevista = value; }
        }
        public string Fecha_Entrevista
        {
            get { return _Fecha_Entrevista; }
            set { _Fecha_Entrevista = value; }
        }

        public string Nps_Descripcion
        {
            get { return _Nps_Descripcion; ; }
            set { _Nps_Descripcion = value; }
        }

        public string Nps_Tema
        {
            get { return _Nps_Tema; }
            set { _Nps_Tema = value; }
        }

        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        public string Nps_Plan
        {
            get { return _Nps_Plan; }
            set { _Nps_Plan = value; }
        }
        public string Nps_Estatus
        {
            get { return _Nps_Estatus; }
            set { _Nps_Estatus = value; }
        }


    }


    public class NpsQueja_ReporteDetalle
    {

        public string Nps_Estatus { get; set; }
        public string Folio_Entrevista { get; set; }
        public string Fecha_Entrevista { get; set; }
        public string Cte_Nomcomercial { get; set; }
        public string Cd_Nombre { get; set; }
        public string Rik_Nombre { get; set; }
        public string Nps_Descripcion { get; set; }
        public string Nps_Tema { get; set; }
        public string Entrevistado { get; set; }
        public string Puesto { get; set; }
        public string Nps_Queja { get; set; }
        public string Nps_PlanAccion { get; set; }
        public string Fecha_Compromiso { get; set; }
        public string Nps_PlanAccion_Segundo { get; set; }
        public string Fecha_Compromiso_Segundo { get; set; }
        public string Fecha_EnDesarrollo { get; set; }
        public string Fecha_Concluido { get; set; }
        public string Fecha_CierreCiclo { get; set; }
        public string Nps_Valor_Inicial { get; set; }
        public string Nps_Tipo_Inicial { get; set; }
        public string Nps_Valor_Segundo { get; set; }
        public string Nps_Tipo_Segundo { get; set; }
        public string Nps_Valor_Final { get; set; }
        public string Nps_Tipo_Final { get; set; }

        public string Envio_Correo { get; set; }
        public string Fecha_Asignado { get; set; }
        public string Fecha_Atendido { get; set; }
        public string Fecha_Reenviado { get; set; }
        public string Fecha_EnDesarrollo_Segundo { get; set; }
        public string Fecha_Atendido_Segundo { get; set; }
        public string Comentario { get; set; }


    }

}