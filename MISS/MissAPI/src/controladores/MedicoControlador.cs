using Microsoft.AspNetCore.Mvc;
using MissAPI.Src.modelos;
using MissAPI.Src.repositorio;
using System;
using System.Threading.Tasks;

namespace MissAPI.Src.controladores
{
    [ApiController]
    [Route("api/Medicos")]
    [Produces("application/json")]
    public class MedicoControlador : ControllerBase
    {
        #region Atributos
        private readonly IMedico _repositorio;
        #endregion

        #region Construtores
        public MedicoControlador(IMedico repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Metodos
        [HttpGet("todosMedicos")]
        public async Task<ActionResult> PegarTodosMedicosAsync()
        {
            var lista = await _repositorio.PegarTodosMedicosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        [HttpGet("idMedico/{idMedico}")]
        public async Task<ActionResult> PegarMedicoPeloIdAsync([FromRoute] int idMedico)
        {
            try
            {
                return Ok(await _repositorio.PegarMedicoPeloIdAsync(idMedico));
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("especialidade/{especialidadeMedico}")]
        public async Task<ActionResult> PegarMedicoPelaEspecialidadeAsync([FromRoute] string especialidadeMedico)
        {
            try
            {
                return Ok(await _repositorio.PegarMedicoPelaEspecialidadeAsync(especialidadeMedico));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("cnpj/{cnpjMedico}")]
        public async Task<ActionResult> PegarMedicoPeloCNPJAsync([FromRoute] string cnpjMedico)
        {
            try
            {
                return Ok(await _repositorio.PegarMedicoPeloCNPJAsync(cnpjMedico));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost("novoMedico")]
        public async Task<ActionResult> NovoMedicoAsync([FromBody] Medico medico)
        {
            await _repositorio.NovoMedicoAsync(medico);
            return Created($"api/Medicos", medico);
        }

        [HttpPut("atualizarMedico")]
        public async Task<ActionResult> AtualizarMedicoAsync([FromBody] Medico medico)
        {
            try
            {
                await _repositorio.AtualizarMedicoAsync(medico);
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("deletarMedico/{idMedico}")]
        public async Task<ActionResult> DeletarMedicoAsync([FromRoute] int idMedico)
        {
            try
            {
                await _repositorio.DeletarMedicoAsync(idMedico);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion

    }
}
