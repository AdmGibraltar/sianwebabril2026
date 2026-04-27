using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
    public class CN_Queja
    {
        public void ConsultaQueja(Sesion Sesion, ref Queja queja, ref List<Documento> lstDocumentos, ref List<Producto> lstproductos)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.ConsultaQueja(Sesion, ref queja, ref lstDocumentos, ref lstproductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaQuejas(Sesion Sesion, string correo, ref List<Queja> LstQuejas, int Id_Queja, int TipoQueja, string Embarque, string Flete, DateTime Fec_Inicio, DateTime Fec_Fin)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.ConsultaQuejas(Sesion, correo, ref LstQuejas, Id_Queja, TipoQueja, Embarque, Flete, Fec_Inicio, Fec_Fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardaQueja(Sesion sesion, Queja queja, DataTable objdtTabla, List<Documento> LstDocumentos, ref int respuesta)
        {
            try
            {
                CD_Queja CD = new CD_Queja();
                CD.GuardaQueja(sesion, queja, objdtTabla, LstDocumentos, ref respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaQueja(Sesion Sesion, ref Queja queja, ref List<Producto> lstProducto, ref List<Documento> lstDocumentos)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.ConsultaQuejas(Sesion, ref queja, ref lstProducto, ref lstDocumentos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaQueja(Sesion sesion, ref Queja queja, DataTable objdtTablaProd, ref List<Documento> LstDocumentos, ref int respuesta)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.ActualizaQueja(sesion, ref queja, objdtTablaProd, ref LstDocumentos, ref respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaMotivos(Sesion Sesion, ref DataSet dsMotivos, int Id_tQueja)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.ConsultaMotivos(Sesion, ref dsMotivos, Id_tQueja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarDocumento(Sesion sesion, int Id_Doc)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.EliminarDocumento(sesion, Id_Doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarProducto(Sesion sesion, int Id_Queja, long Id_Prd)
        {
            try
            {
                CD_Queja Datos = new CD_Queja();
                Datos.EliminarProducto(sesion, Id_Queja, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}