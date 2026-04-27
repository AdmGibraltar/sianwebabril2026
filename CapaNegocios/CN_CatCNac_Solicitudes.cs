using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

using CapaModelo_CC.CuentasCoorporativas;

namespace CapaNegocios
{
    public class CN_CatCNac_Solicitudes
    {

        public List<CatCNac_Solicitudes> ConsultarTodos()
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes();
            return CEst.ConsultarTodos();
        }


        public CatCNac_Solicitudes ConsultarItem(int id)
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes();
            return CEst.ConsultarItem(id);
        }

        public List<CatCNac_EmailsAutorizacion> Consultar_Emails_Aut()
        {
            CD_CatCNac_Solicitudes CEst = new CD_CatCNac_Solicitudes();
            return CEst.Consultar_Emails_Aut();
        }
    }

}