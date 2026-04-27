using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Cn_matriz
    {
        public void ConsultarMatriz(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarMatriz(datos, ref Lista, Conexion);
        }


        public void ConsultarVinculacion(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarVinculacion(datos, ref Lista, Conexion);
        }

        public void ConsultarUsuario(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarUsuario(datos, ref Lista, Conexion);
        }

        public void ConsultarRemisionesMov80(ref List<CN_Matriz> Lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarRemisionesMov80(ref Lista, Conexion);
        }

        public void ConsultarSolicitudes(CN_Matriz datos, ref List<CN_Matriz> Lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarSolicitudes(datos, ref Lista, Conexion);
        }


        public void InsertarSolicitudes(CN_Matriz datos, ref int verificador, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.InsertarSolicitudes(datos, ref verificador, Conexion);
        }

        public void InsertarSolicitudesdirfiscal(CN_Matriz datos, ref int verificador, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.InsertarSolicitudesdirfiscal(datos, ref verificador, Conexion);
        }

        public void ConsultarAcysProductos(CatCnac_Producto Registros, ref List<CatCnac_Producto> lista, string Conexion)
        {
            CD_CN_Matriz CD = new CD_CN_Matriz();
            CD.ConsultarAcysProductos(Registros, ref lista, Conexion);
        }
    }
}