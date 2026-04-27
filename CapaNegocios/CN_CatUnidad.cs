using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatUnidad
    {

        // ENE20-2020 RFH Compras Locales

        public List<CapaEntidad.eUnidadMedidaSat> spSATUnidadesMedida(string CveUnidad, string Conexion)
        {
            CD_CatUnidad CD = new CD_CatUnidad();
            return CD.spSATUnidadesMedida(CveUnidad, Conexion);
        }
        // 27Abr2022 RFH
        public List<CapaEntidad.eListaGenerica> spCatCL_UnidadMedida(int IdUnidad, int Id1, int Id2, string Conexion)
        {
            CD_CatUnidad CD = new CD_CatUnidad();
            return CD.spCatCL_UnidadMedida(IdUnidad, Id1, Id2, Conexion);
        }

        // ENE20-2020 RFH Compras Locales

        public List<CapaEntidad.eListaGenerica> spLlenarComboUnidades(int IdUnidad, int Id1, int Id2, string Conexion)
        {
            CD_CatUnidad CD = new CD_CatUnidad();
            return CD.spLlenarComboUnidades(IdUnidad, Id1, Id2, Conexion);
        }

        public void ConsultaUnidad(int Id_Emp, string Conexion, ref List<Unidad> List)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.ConsultaUnidad(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.InsertarUnidad(unidad, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.ModificarUnidad(unidad, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}