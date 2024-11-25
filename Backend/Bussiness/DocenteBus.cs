using Dto;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public class DocenteBus
    {
        public async Task<ResultadoTransaccionE<string>> Registrar_Docente(DocenteE objDocente)
        {
            ResultadoTransaccionE<string> transaccionE = new ResultadoTransaccionE<string>();
            try
            {
                transaccionE = await new DocenteDto().Registrar_Docente(objDocente);
            }
            catch (Exception ex)
            {
                transaccionE.IdRegistro = -1;
                transaccionE.Mensaje = ex.Message;
            }
            return transaccionE;
        }

        public async Task<ResultadoTransaccionE<DocenteE>> Listar_All(string buscar)
        {
            ResultadoTransaccionE<DocenteE> transaccionE = new ResultadoTransaccionE<DocenteE>();
            try
            {
                transaccionE = await new DocenteDto().Listar_All(buscar);
            }
            catch (Exception ex)
            {
                transaccionE.IdRegistro = -1;
                transaccionE.Mensaje = ex.Message;
            }
            return transaccionE;
        }
    }
}
