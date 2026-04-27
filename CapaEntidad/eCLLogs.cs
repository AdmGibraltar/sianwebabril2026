using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eCLLogs
    {
        private int _IdCompraLocalLogs;
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Comp;
        private string _Fecha;
        private int _Id_Usuario;
        private string _Nota;

        public int IdCompraLocalLogs { get => _IdCompraLocalLogs; set => _IdCompraLocalLogs = value; }
        public int Id_Emp { get => _Id_Emp; set => _Id_Emp = value; }
        public int Id_Cd { get => _Id_Cd; set => _Id_Cd = value; }
        public int Id_Comp { get => _Id_Comp; set => _Id_Comp = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public int Id_Usuario { get => _Id_Usuario; set => _Id_Usuario = value; }
        public string Nota { get => _Nota; set => _Nota = value; }
    }
}