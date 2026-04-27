using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatTipoCliente
    {
        // NOV19-2019 RFH

        public List<CapaEntidad.TipoCliente> SelAll(int Id_Emp, string Conexion, ref int verificador)
        {            
            CD_CatTipoCliente claseCapaDatos = new CD_CatTipoCliente();
            return claseCapaDatos.SelAll(Id_Emp, Conexion, ref verificador);
        }
        
        public void ConsultaAutorizadores(TipoCliente tipoCliente, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoCliente claseCapaDatos = new CD_CatTipoCliente();
                claseCapaDatos.ConsultaAutorizadores(tipoCliente, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAutorizadores(TipoCliente tipoCliente, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoCliente claseCapaDatos = new CD_CatTipoCliente();
                claseCapaDatos.ModificarAutorizadores(tipoCliente, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
