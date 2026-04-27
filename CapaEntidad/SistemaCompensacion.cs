using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class SistemaCompensacion
    {
        private int _id_Emp;

        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        private int _id_Cd;

        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }


        private int _id_Sistema;

        public int Id_Sistema
        {
            get { return _id_Sistema; }
            set { _id_Sistema = value; }
        }



        private string _NombreSistema;

        public string NombreSistema
        {
            get { return _NombreSistema; }
            set { _NombreSistema = value; }
        }

        private string _Id_Perfil;

        public string Id_Perfil
        {
            get { return _Id_Perfil; }
            set { _Id_Perfil = value; }
        }

        private DateTime? _FechaInicio;

        public DateTime? FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        private DateTime? _FechaFin;

        public DateTime? FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }


        private string _Observaciones;

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }


        private int _Estatus;

        public int Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _NombreEstatus;

        public string NombreEstatus
        {
            get { return _NombreEstatus; }
            set { _NombreEstatus = value; }
        }

        private DateTime? _FechaUltimaMod;

        public DateTime? FechaUltimaMod
        {
            get { return _FechaUltimaMod; }
            set { _FechaUltimaMod = value; }
        }


        private string _ImpEdoConsolidado;

        public string ImpEdoConsolidado
        {
            get { return _ImpEdoConsolidado; }
            set { _ImpEdoConsolidado = value; }
        }

        private List<ConceptoVariable> _listaConceptoVariables;

        public List<ConceptoVariable> listaConceptoVariables
        {
            get { return _listaConceptoVariables; }
            set { _listaConceptoVariables = value; }
        }


        private List<ConceptoVariables> _listaVariables;

        public List<ConceptoVariables> listaVariables
        {
            get { return _listaVariables; }
            set { _listaVariables = value; }
        }

        private List<ConceptoVariableReporte> _listaConceptoVariableReporte;

        public List<ConceptoVariableReporte> ListaConceptoVariableReporte
        {
            get { return _listaConceptoVariableReporte; }
            set { _listaConceptoVariableReporte = value; }
        }




    }
}