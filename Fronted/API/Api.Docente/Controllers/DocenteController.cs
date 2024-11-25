using Microsoft.AspNetCore.Mvc;
using System.Data;
using Entity;
using Bussiness;

namespace Api.Docente.Controllers
{
    [Route("Api/Docente")]
    public class DocenteController : Controller
    {
        [Route("Registrar_Docente")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar_Docente([FromBody] DocenteE objDocente)
        {
            try
            {
                var resultado = await new DocenteBus().Registrar_Docente(objDocente);
                if (resultado.IdRegistro == -1)
                {
                    return BadRequest(resultado.Mensaje);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Listar_All")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Listar_All(string buscar)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(buscar)) { buscar = ""; }
                var resultado = await new DocenteBus().Listar_All( buscar);
                if (resultado.IdRegistro == -1)
                {
                    return BadRequest(resultado.Mensaje);
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   
    }
}
