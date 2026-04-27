using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EmbarqueFacRem
    {
        public int Numero
        {
            get;
            set;
        }

        public int Id_Doc
        {
            get;
            set;
        }

        public int Id_Emb
        {
            get;
            set;
        }

        public string TipoDoc
        {
            get;
            set;
        }

        public string Estatus
        {
            get;
            set;
        }

        public DateTime Fecha
        {
            get;
            set;
        }

        public DateTime Fecha2
        {
            get;
            set;
        }

        public int Pedido
        {
            get;
            set;
        }

        public int Num_Cliente
        {
            get;
            set;
        }

        public string Cliente
        {
            get;
            set;
        }

        public string Filtro_Nombre
        {
            get;
            set;
        }

        public string Filtro_Id_Cte
        {
            get;
            set;
        }

        public string Filtro_Id_Cte2
        {
            get;
            set;
        }

        public string Filtro_FecIni
        {
            get;
            set;
        }

        public string Filtro_FecFin
        {
            get;
            set;
        }

        public string Chofer
        {
            get;
            set;
        }

        public string Camioneta
        {
            get;
            set;
        }

    }

}