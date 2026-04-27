using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_BitacoraCancelacionDocumento
    {
        public void InsertarBitacora(ref eBitacoraCancelacionDocumento notaCredito, string Conexion, ref int verificador)
        {
            try
            {
                new CD_BitacoraCancelacionDocumento().InsertarBitacora(ref notaCredito, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaBitacora_Buscar(eBitacoraCancelacionDocumento notaCredito, ref List<eBitacoraCancelacionDocumento> listaNotaCredito, string Conexion)
        {
            try
            {
                new CD_BitacoraCancelacionDocumento().ConsultaBitacora_Buscar(notaCredito, ref listaNotaCredito, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}