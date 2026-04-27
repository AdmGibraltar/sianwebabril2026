using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaDatos;
namespace CapaNegocios
{
    public class CN_CatProveedores
    {
        // ENE21-2020 RFH Compras Locales consulta 

        public List<CapaEntidad.CatProveedorLocal> spProveedores_ComboCompraLocal(int Id1, int Id2, string Conexion)
        {
            CD_CatProveedores CD = new CD_CatProveedores();
            return CD.spProveedores_ComboCompraLocal(Id1, Id2, Conexion);
        }

        public void ConsultaProveedores(Proveedores prv, string Conexion, ref List<Proveedores> List, string bdCentral)
        {
            try
            {
                CD_CatProveedores claseCapaDatos = new CD_CatProveedores();
                claseCapaDatos.ConsultaProveedores(prv, Conexion, ref List, bdCentral);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaTMov(Movimientos mov, string Conexion, ref List<Movimientos> List, string bdCentral)
        {
            try
            {
                CD_CatProveedores claseCapaDatos = new CD_CatProveedores();
                claseCapaDatos.ConsultaTMov(mov, Conexion, ref List, bdCentral);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClavePorTipo(ProveedorInternoTipo mov, string Conexion, ref List<ProveedorInternoTipo> List, string bdCentral)
        {
            try
            {
                CD_CatProveedores claseCapaDatos = new CD_CatProveedores();
                claseCapaDatos.ConsultarClavePorTipo(mov, Conexion, ref List, bdCentral);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProveedores(Proveedores prv, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProveedores claseCapaDatos = new CD_CatProveedores();
                claseCapaDatos.InsertarProveedores(prv, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProveedores(Proveedores prv, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProveedores claseCapaDatos = new CD_CatProveedores();
                claseCapaDatos.ModificarProveedores(prv, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}