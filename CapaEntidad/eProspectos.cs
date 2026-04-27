using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eProspectos
    {
        private int _Id_CrmProspecto;
        public int Id_CrmProspecto
        {
            get { return _Id_CrmProspecto; }
            set { _Id_CrmProspecto = value; }
        }

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

        private int _Id_CrmTipoCliente;
        public int Id_CrmTipoCliente
        {
            get { return _Id_CrmTipoCliente; }
            set { _Id_CrmTipoCliente = value; }
        }

        private long _Id_Ter_Temporal;
        public long Id_Ter_Temporal
        {
            get { return _Id_Ter_Temporal; }
            set { _Id_Ter_Temporal = value; }
        }

        private string _RFC;
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        private string _Contacto;
        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Calle;
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        private string _Telefonos;
        public string Telefonos
        {
            get { return _Telefonos; }
            set { _Telefonos = value; }
        }

        //
    }
}