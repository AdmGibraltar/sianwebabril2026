using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public sealed class ClaseConexion
    {
        private readonly static ClaseConexion _instancia = new ClaseConexion();

        public string conexion;

        private ClaseConexion()
        {
            conexion = ConfigurationSettings.AppSettings["strConnection"];
        }

        public static ClaseConexion GetClaseConexion()
        {
            return _instancia;
        }
    }
}