using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ConfiguracionDiasConvenio
    {


        public void ConsultarConfiguracion_DiasAlerta(ref List<ConfiguracionDiasConvenio> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ConsultarConfiguracion_DiasAlerta(ref ListaCentroDistribucion, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConfiguracion_DiasAlerta(ref ConfiguracionDiasConvenio ListaCentroDistribucion, Sesion sesion, int verificador)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ModificarConfiguracion_DiasAlerta(ref ListaCentroDistribucion, sesion, verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarConfiguracion_DiasAlerta(List<ConfiguracionDiasConvenio> ListaCentroDistribucion, int Id_Cd, Sesion sesion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.AgregarConfiguracion_DiasAlerta(ListaCentroDistribucion, Id_Cd, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //categorias 
        public void ConsultarConfiguracion_Categoria(ref List<CatConvCategoria> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ConsultarConfiguracion_Categoria(ref ListaCentroDistribucion, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConfiguracion_Categoria(CatConvCategoria categoria, Sesion sesion, int verificador)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ModificarConfiguracion_Categoria(ref categoria, sesion, verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificaMasivoConfiguracion_Categoria(List<CatConvCategoria> ListaCentroDistribucion, int Id_Cd, Sesion sesion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ModificaMasivoConfiguracion_Categoria(ListaCentroDistribucion, Id_Cd, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarConfiguracion_Categoria(CatConvCategoria categoria, int Id_Cd, Sesion sesion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.AgregarConfiguracion_Categoria(categoria, Id_Cd, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarConfiguracion_Categoria(int Id_Emp, int Id_Cd, int Id_Cat, int id_UCreo, string Emp_Cnx)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.EliminarConfiguracion_Categoria(Id_Emp, Id_Cd, Id_Cat, id_UCreo, Emp_Cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Administrador
        public void ConsultarConfiguracion_Administrador(ref List<ProPrecioConv_Usuarios> listaadministrador, int Id_Cd, int Id_Emp, int tipo_usuario, string Conexion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ConsultarConfiguracion_Administrador(ref listaadministrador, Id_Cd, Id_Emp, tipo_usuario, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarConfiguracion_Administrador(List<ProPrecioConv_Usuarios> ListaCentroDistribucion, int Id_Cd, int tipo_usuario, Sesion sesion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.AgregarConfiguracion_Administrador(ListaCentroDistribucion, Id_Cd, tipo_usuario, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CorreosAnexos
        public void ConsultarCorreosAnexos(ref List<ProPrecioConv_Usuarios> listaadministrador, int Id_Cd, int Id_Emp, int id_pc, string Conexion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ConsultarCorreosAnexos(ref listaadministrador, Id_Cd, Id_Emp, id_pc, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Adicionales
        public void ConsultarCorreosAdicionales(ref List<ProPrecioConv_Usuarios> listaadministrador, int Id_Cd, int Id_Emp, int id_pc, int Tipo_Habilitar, string Conexion)
        {
            try
            {
                CD_ConfiguracionDiasConvenio cd = new CD_ConfiguracionDiasConvenio();
                cd.ConsultarCorreosAdicionales(ref listaadministrador, Id_Cd, Id_Emp, id_pc, Tipo_Habilitar, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
