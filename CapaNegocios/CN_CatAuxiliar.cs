using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatAuxiliar
    {
        public void ConsultaAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref List<CatAuxiliares> List)
        {
            try
            {
                CD_CatAuxiliar claseCapaDatos = new CD_CatAuxiliar();
                claseCapaDatos.ConsultaAuxiliar(Auxiliar, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAuxiliar claseCapaDatos = new CD_CatAuxiliar();
                claseCapaDatos.InsertarAuxiliar(Auxiliar, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAuxiliar claseCapaDatos = new CD_CatAuxiliar();
                claseCapaDatos.ModificarAuxiliar(Auxiliar, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
