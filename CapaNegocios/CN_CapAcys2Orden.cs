using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CapAcys2Orden
    {

        //
        // 13 Ago 2018 RFH 
        // Actualizar  
        //
        public int InsertUpdate(ref eAcys2 acys, ref string MensajeError, string Conexion)
        {
            CD_CapAcys2Orden CD = new CD_CapAcys2Orden();
            return CD.InsertUpdate(ref acys, ref MensajeError, Conexion);
        }

        //
        // 
        // 23 Nov 2018 RFH 
        public void Consultar_Listado(
            int PageNumber, int PageSize,
            int AplicaFecha, int AplicaFolio,
            eAcys2 acys, string Conexion, ref List<eAcys2> List)
        {
            CD_CapAcys2 CD = new CD_CapAcys2();
            CD.spCapAcys2_ListadoOrdenes(
                    PageNumber, PageSize,
                    AplicaFecha, AplicaFolio,
                    acys, Conexion, ref List
             );
        }
        



        //
    }
}
