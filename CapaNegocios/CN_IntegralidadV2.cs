using CapaDatos;
using CapaEntidad;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_IntegralidadV2
    {
        public List<eIntegralidadv2> spListCdiByZona(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            CD_IntegralidadV2 CM = new CD_IntegralidadV2();
            return CM.spListCdiByZona(oeIntegralidadv2Par, Conexion);
        }
        public List<eIntegralidadv2> sp_MapaAplicaciones_DetalleXMes(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            CD_IntegralidadV2 CM = new CD_IntegralidadV2();
            return CM.sp_MapaAplicaciones_DetalleXMes(oeIntegralidadv2Par, Conexion);
        }
        public List<eIntegralidadv2> spListIntegralidadByTipoProducto(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            CD_IntegralidadV2 CM = new CD_IntegralidadV2();
            return CM.spListIntegralidadByTipoProducto(Conexion, oeIntegralidadv2Par);
        }

        public eIntegralidadv2 sp_IntegralidadMes_ActualizaVPOMetaV2(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            CD_IntegralidadV2 CM = new CD_IntegralidadV2();
            return CM.sp_IntegralidadMes_ActualizaVPOMetaV2(oeIntegralidadv2Par, Conexion);
        }

        public List<eIntegralidadv2> spConsultar_SegmentoUen(int Id_Emp, int Id_Cd, string Conexion, int tipo, int Id_Uen)
        {
            CD_IntegralidadV2 CR = new CD_IntegralidadV2();
            return CR.spConsultar_SegmentoUen(Id_Emp, Id_Cd, Conexion, tipo, Id_Uen);
        }



    }
}