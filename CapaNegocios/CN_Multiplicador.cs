using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Multiplicador
    {
        public void AgregarMultiplicadorRIk(CatMultiplicador RegistroMultiplicador, ref int verificador, string Conexion)
        {
            CD_CatMultiplicador cd = new CD_CatMultiplicador();
            cd.AgregarMultiplicadorRIk(RegistroMultiplicador, ref verificador, Conexion);
        }

        public void ConsultaMultiplicadorRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            CD_CatMultiplicador cd = new CD_CatMultiplicador();
            cd.ConsultaMltiplicadorRIk(RegistroMultiplicador, ref list_Multiplicador, Conexion);
        }

        public void ConsultaMltiplicadorMensualRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {

            CD_CatMultiplicador cd = new CD_CatMultiplicador();
            cd.ConsultaMltiplicadorMensualRIk(RegistroMultiplicador, ref list_Multiplicador, Conexion);
        }

        public void ConsultaMltiplicadorMRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {

            CD_CatMultiplicador cd = new CD_CatMultiplicador();
            cd.ConsultaMltiplicadorMRIk(RegistroMultiplicador, ref list_Multiplicador, Conexion);
        }
         


        public void ConsultaMltiplicadorPresupuestoRIk(CatMultiplicador RegistroMultiplicador, ref List<CatMultiplicador> list_Multiplicador, string Conexion)
        {
            CD_CatMultiplicador cd = new CD_CatMultiplicador();
            cd.ConsultaMltiplicadorPresupuestoRIk(RegistroMultiplicador, ref list_Multiplicador, Conexion);
        }

    }
}
