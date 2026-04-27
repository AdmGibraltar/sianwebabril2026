using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapRepValuacionParams
    {

        public eRepValuacionParams SpCrmOportunidades_RepValuacionProp(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_RepValuacionProp(Id_Emp, Id_Cd, Id_Op, Conexion);
        }


        public int SpCrmOportunidades_UpdateRepParams(int Id_Emp, int Id_Cd, int Id_Op,
            string tbNombreAtencion, string tbRepresentanteClienteNombre, string tbNombreRik, string tbDireccion1, string tbDireccion2, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_UpdateRepParams(Id_Emp, Id_Cd,
                Id_Op, tbNombreAtencion, tbRepresentanteClienteNombre, tbNombreRik, tbDireccion1, tbDireccion2, Conexion);
        }

        // MAY22-2020 RFH

        public eRepValuacionParams SpCrmOportunidades_RepValuacionProp_Ver1(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_RepValuacionProp_Ver1(Id_Emp, Id_Cd, Id_Op, Conexion);
        }

        // JUN30-2020 

        public eCRM_ValuacionCamposAdicionales_Ver2 SpCrmOportunidades_RepValuacionProp_Ver2(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_RepValuacionProp_Ver2(Id_Emp, Id_Cd, Id_Op, Conexion);
        }

        // MAY22-2020 RFH

        public int SpCrmOportunidades_UpdateRepParams_Ver2(eCRM_ValuacionCamposAdicionales Enti, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_UpdateRepParams_Ver2(Enti, Conexion);
        }

        // JUN30-2020

        public int SpCrmOportunidades_UpdateRepParams_Ver3(eCRM_ValuacionCamposAdicionales_Ver2 Enti, string Conexion)
        {
            CD_CapRepValuacionParams CD = new CD_CapRepValuacionParams();
            return CD.SpCrmOportunidades_UpdateRepParams_Ver3(Enti, Conexion);
        }
        //

    }
}