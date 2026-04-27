using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// ABR27-2020 RFH
// Mantiene los error en el ACyS el envio previo a utorizar.
// 

namespace CapaEntidad
{
    public class eAcys2ValiacionesPreAutorizacion
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

        private int _Id_Acs;
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }

        private int _Id_AcsVersion;
        public int Id_AcsVersion
        {
            get { return _Id_AcsVersion; }
            set { _Id_AcsVersion = value; }
        }
        //S
        private int _ErrorSemanal;
        public int ErrorSemanal
        {
            get { return _ErrorSemanal; }
            set { _ErrorSemanal = value; }
        }
        //M
        private int _ErrorMensual;
        public int ErrorMensual
        {
            get { return _ErrorMensual; }
            set { _ErrorMensual = value; }
        }
        //B
        private int _ErrorBimestral;
        public int ErrorBimestral
        {
            get { return _ErrorBimestral; }
            set { _ErrorBimestral = value; }
        }
        //T
        private int _ErrorTrimestral;
        public int ErrorTrimestral
        {
            get { return _ErrorTrimestral; }
            set { _ErrorTrimestral = value; }
        }
        //S
        private int _ErrorSemestral;
        public int ErrorSemestral
        {
            get { return _ErrorSemestral; }
            set { _ErrorSemestral = value; }
        }

        private int _SemanaInicial;
        public int SemanaInicial
        {
            get { return _SemanaInicial; }
            set { _SemanaInicial = value; }
        }

        private int _Acs_Semana;
        public int Acs_Semana
        {
            get { return _Acs_Semana; }
            set { _Acs_Semana = value; }
        }

        // Productos sin tipo de documento

        private int _Prods_SinTipoDoc;
        public int Prods_SinTipoDoc
        {
            get { return _Prods_SinTipoDoc; }
            set { _Prods_SinTipoDoc = value; }
        }
        // JFCV para validar precios al enviar a autorizar el Acys 
        private int _ErrorAlertaPrecio;
        public int ErrorAlertaPrecio
        {
            get { return _ErrorAlertaPrecio; }
            set { _ErrorAlertaPrecio = value; }
        }

        //

    }
}