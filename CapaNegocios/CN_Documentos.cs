using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_Documentos
    {
        public void ConsultaDocumento(Sesion sesion, ref Documento Doc)
        {
            try
            {
                CD_Documentos Datos = new CD_Documentos();
                Datos.ConsultaDocumento(sesion, ref Doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
