using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;

namespace CapaNegocios
{
    public class CN_Maps
    {

        //-------------------------------------------------------------------------------------------

        public void ObtieneOrigen(ref Maps Ma, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.ObtieneOrigen(ref Ma, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaRuta(ref Maps Ma, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.GrabaRuta(ref Ma, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaSegmento(Maps Ma, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.GrabaSegmento(Ma, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneRutasCliente(Maps Ma, ref List<Maps> RutasMa, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.ObtieneRutasCliente(Ma, ref RutasMa, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDetalleRuta(Maps Ma, ref string DetalleRuta, ref string FinalDeRuta, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.ObtieneDetalleRuta(Ma, ref DetalleRuta, ref FinalDeRuta, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GrabaDireccionEntregaCliente(Maps Ma, string conexion)
        {
            try
            {
                CapaDatos.CD_Maps claseCapaDatos = new CapaDatos.CD_Maps();
                claseCapaDatos.GrabaDireccionEntregaCliente(Ma, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
