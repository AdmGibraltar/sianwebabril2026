using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_ClienteEcommerce
    {
        public void ConsultaLista(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultaLista(Registro, ref List, Conexion);
        }


        public void ConsultaDatosPortal(Portakey Registro, ref List<Portakey> List, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultaDatosPortal(Registro, ref List, Conexion);
        }

        public void spCatClienteEcommerce_ConsultarCorreo(ClienteECommerce Registro, ref List<ClienteECommerce> List, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.spCatClienteEcommerce_ConsultarCorreo(Registro, ref List, Conexion);
        }



        public void ConsultadatosoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> Lista, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultadatosoCliente(Registro, ref Lista, Conexion);
        }


        public void ConsultarSegmentoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> List, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultarSegmetnoCliente(Registro, ref List, Conexion);
        }


        public void ConsultadatosoClienteCredito(ClienteECommerce Registro, ref List<ClienteECommerce> Lista, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultadatosoClienteCredito(Registro, ref Lista, Conexion);
        }


        public void ConsultaCorreoCliente(ClienteECommerce Registro, ref List<ClienteECommerce> List, string Conexion)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ConsultaCorreoCliente(Registro, ref List, Conexion);
        }

        public void InsertarLista(ClienteECommerce Registro, string Conexion, ref string verificador)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.InsertarLista(Registro, Conexion, ref verificador);
        }


        public void ModificarLista(ClienteECommerce Registro, string Conexion, ref string verificador)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ModificarLista(Registro, Conexion, ref verificador);
        }


        public void ActualizarEstado(ClienteECommerce Registro, string Conexion, ref int verificador)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.ActualizarEstado(Registro, Conexion, ref verificador);
        }

        public void InsertarJobMail(int IdCliente, int IdUsuario, String Contrasena, string email, string Conexion, ref int verificador)
        {
            CD_ClienteEcommerce cd = new CD_ClienteEcommerce();
            cd.InsertarJobMail(IdCliente, IdUsuario, Contrasena, email, Conexion, ref verificador);
        }


    }
}