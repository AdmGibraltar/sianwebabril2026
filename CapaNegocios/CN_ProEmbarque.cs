using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_ProEmbarque
    {
        public void ProEmbarque_ConsultaLista(ProEmbarque emb, ref List<ProEmbarque> List, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_ConsultaLista(emb, ref List, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_ConsultaDocumento(ref ProEmbarqueDet emb, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_ConsultaDocumento(ref emb, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Insertar(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_Insertar(emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueDet_Insertar(List<ProEmbarqueDet> List, int Id_Emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarqueDet_Insertar(List, Id_Emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Consulta(ref ProEmbarque emb, ref List<ProEmbarqueDet> List, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_Consulta(ref emb, ref List, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Modificar(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_Modificar(emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarque_Baja(ProEmbarque emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarque_Baja(emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueConfirmar_Buscar(ProEmbarqueDet emb, ref List<ProEmbarqueDet> List, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarqueConfirmar_Buscar(emb, ref List, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueConfirmaUno(ProEmbarqueDet emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarqueConfirmaUno(emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProEmbarqueConfirmaTodos(ProEmbarqueDet emb, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ProEmbarque cd_emb = new CD_ProEmbarque();
                cd_emb.ProEmbarqueConfirmaTodos(emb, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ListaEmbarque(Embarques embarque, string Conexion, ref List<EmbarquesReporte> List)
        {
            try
            {
                CD_ProEmbarque claseCapaDatos = new CD_ProEmbarque();
                claseCapaDatos.ListaEmbarque(embarque, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ListaEmbarquePlanner(Embarques embarque, string Conexion, ref List<EmbarquesReporte> List)
        {
            try
            {
                CD_ProEmbarque claseCapaDatos = new CD_ProEmbarque();
                claseCapaDatos.ListaEmbarquePlanner(embarque, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}