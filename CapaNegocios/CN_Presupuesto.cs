using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Presupuesto
    {
        public void AgregarPresupuestoRIk(CatPresupuesto RegistroPresupuesto, ref int verificador, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.AgregarPresupuestoRIk(RegistroPresupuesto, ref verificador, Conexion);
        }

        public void AgregarVNRRIk(CatPresupuesto RegistroPresupuesto, ref int verificador, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.AgregarVNRRIk(RegistroPresupuesto, ref verificador, Conexion);
        }

        public void ConsultaUtilidadcategoria(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadcategoria(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void Consultaventanodetalle(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.Consultaventanodetalle(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void Consultamatriz(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.Consultamatriz(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void Consultaventanoacysdetalle(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.Consultaventanoacysdetalle(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaUtilidadProducto(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadProducto(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaUtilidadCliente(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadCliente(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaPresupuestoRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaPresupuestoRIk(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaPresupuestoMesualRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaPresupuestoMesualRIk(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaPresupuestoMesualPvvRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaPresupuestoMesualPvvRIk(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaVentaTotal(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaVentaTotal(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaVentaTotalTrimestral(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaVentaTotalTrimestral(RegistroPresupuesto, ref list_Presupuesto, Conexion);

        }

        public void ConsultaRemisionTotal(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaRemisionTotal(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultRemisionTotalTrimestral(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultRemisionTotalTrimestral(RegistroPresupuesto, ref list_Presupuesto, Conexion);

        }



        public void ConsultaUtilidadRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadRIk(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }


        public void ConsultaUtilidadRIkxmes(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadRIkxmes(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaUtilidadRIkxmesCliente(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaUtilidadRIkxmesCliente(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void PresupuestoMensualRik(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.PresupuestoMensualRik(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaventanoRentable(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.ConsultaventanoRentable(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void PresupuestoMensualCategoria(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Presupuesto cd = new CD_Presupuesto();
            cd.PresupuestoMensualCategoria(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }
    }
}