using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_PortalKey
    {
        public void ConsultaSucursal(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultaSucursal(Portal, ref lista, Conexion);
        }

        public void ConsultaRepresentante(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultaRepresentante(Portal, ref lista, Conexion);
        }

        public void ConsultaCliente(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultaCliente(Portal, ref lista, Conexion);
        }


        public void ConsultarMAtriz(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarMAtriz(Portal, ref lista, Conexion);
        }

        public void ConsultarGralMAtriz(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarGralMAtriz(Portal, ref lista, Conexion);
        }

        public void InsertarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.InsertarMAtriz(Portal, ref Verificador, Conexion);
        }

        public void ModificarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ModificarMAtriz(Portal, ref Verificador, Conexion);
        }

        public void EliminarMAtriz(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.EliminarMAtriz(Portal, ref Verificador, Conexion);
        }

        public void ConsultarRegion(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarRegion(Portal, ref lista, Conexion);
        }

        public void InsertarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.InsertarRegion(Portal, ref Verificador, Conexion);
        }

        public void ModificarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ModificarRegion(Portal, ref Verificador, Conexion);
        }

        public void EliminarRegion(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.EliminarRegion(Portal, ref Verificador, Conexion);
        }


        public void ConsultarCorreoUsuario(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarCorreoUsuario(Portal, ref lista, Conexion);
        }

        public void ConsultarDatosCliente(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarDatosCliente(Portal, ref lista, Conexion);
        }

        public void ConsultaDatosCliente(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaDatosCliente(Registro, ref Lista, Conexion);
        }


        public void ConsultarCorreoUnidad(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultarCorreoUnidad(Registro, ref List, Conexion);
        }


        public void ConsultaCredito(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaCredito(Registro, ref Lista, Conexion);
        }



        public void InsertarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.InsertarCorreoUsuario(Portal, ref Verificador, Conexion);
        }

        public void ModificarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ModificarCorreoUsuario(Portal, ref Verificador, Conexion);
        }

        public void EliminarCorreoUsuario(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.EliminarCorreoUsuario(Portal, ref Verificador, Conexion);
        }


        public void ConsultarClientePortal(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarClientePortal(Portal, ref lista, Conexion);
        }

        public void InsertarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.InsertarCliente(Portal, ref Verificador, Conexion);
        }

        public void ModificarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ModificarCliente(Portal, ref Verificador, Conexion);
        }

        public void EliminarCliente(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.EliminarCliente(Portal, ref Verificador, Conexion);
        }

        public void ConsultarPortalPermiso(Portakey Portal, ref List<Portakey> lista, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.ConsultarPortalPermiso(Portal, ref lista, Conexion);
        }

        public void InsertarPermiso(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.InsertarPermiso(Portal, ref Verificador, Conexion);
        }


        public void EliminarPermisos(Portakey Portal, ref int Verificador, string Conexion)
        {
            CD_PortalKey CD = new CD_PortalKey();
            CD.EliminarPermisos(Portal, ref Verificador, Conexion);
        }

        public void ConsultarCorreo(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultarCorreo(Registro, ref List, Conexion);
        }

        public void ConsultaClienteEnvio(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaClienteEnvio(Registro, ref Lista, Conexion);
        }

        public void ConsultaClienteEnviodet(Portakey Registro, ref List<Portakey> Lista, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaClienteEnviodet(Registro, ref Lista, Conexion);
        }

        public void ConsultaDatosPortal(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaDatosPortal(Registro, ref List, Conexion);
        }

        public void ConsultaClienteCapturados(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaClienteCapturados(Registro, ref List, Conexion);
        }

        public void ConsultaClienteUnidad(Portakey Registro, ref int verificador, string Conexion)
        {
            CD_PortalKey cd = new CD_PortalKey();
            cd.ConsultaClienteUnidad(Registro, ref verificador, Conexion);
        }
    }
}