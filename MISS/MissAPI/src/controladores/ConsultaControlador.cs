using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissAPI.Src.modelos;
using MissAPI.Src.repositorio;
using System;
using System.Threading.Tasks;

namespace MissAPI.Src.controladores
{
    [ApiController]
    [Route("api/Consultas")]
    [Produces("application/json")]
    public class ConsultaControlador : ControllerBase
    {
        #region Atributos
        private readonly IConsulta _repositorio;
        #endregion

        #region Metodos
        public ConsultaControlador(IConsulta repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Metodos
        [HttpGet("todasConsultas")]
        public async Task<ActionResult> PegarTodasConsultasAsync()
        {
            var lista = await _repositorio.PegarTodasConsultasAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        [HttpGet("idConsulta/{idConsulta}")]
        public async Task<ActionResult> PegarMedicoPeloIdAsync([FromRoute] int idConsulta)
        {
            try
            {
                return Ok(await _repositorio.PegarConsultaPeloIdAsync(idConsulta));
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost("novaConsulta")]
        public async Task<ActionResult> NovaConsultaAsync([FromBody] Consulta consulta)
        {
            await _repositorio.NovaConsultaAsync(consulta);
            return Created($"api/Consultas", consulta);
        }

        [HttpPut("atualizarConsulta")]
        public async Task<ActionResult> AtualizarMedicoAsync([FromBody] Consulta consulta)
        {
            try
            {
                await _repositorio.AtualizarConsultaAsync(consulta);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("deletarConsulta/{idConsulta}")]
        public async Task<ActionResult> DeletarMedicoAsync([FromRoute] int idConsulta)
        {
            try
            {
                await _repositorio.DeletarConsultaAsync(idConsulta);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion
    }
}
