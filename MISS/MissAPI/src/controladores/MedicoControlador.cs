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

        /// <summary> 
        /// Pegar todos os médicos
        /// </summary>
        /// <para> Resumo: Método assincrono para pegar todos os médicos</para>
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna todos os médicos</response> 
        /// <response code="403">Médico não autorizado</response>
        #region Metodos
        [HttpGet("todosMedicos")]
        public async Task<ActionResult> PegarTodosMedicosAsync()
        {
            var lista = await _repositorio.PegarTodosMedicosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar médico pelo Id
        /// </summary>
        /// <param name="idMedico">Id do médico</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Id não existente</response>
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

        /// <summary> 
        /// Pegar médico pela especialidade
        /// </summary> 
        /// <param name="especialidadeMedico">Especialidade do médico</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Especialidade encontrada</response> 
        /// <response code="404">Especialidade não existente</response>
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

        /// <summary> 
        /// Pegar médico pelo CNPJ
        /// </summary> 
        /// <param name="cnpjMedico">CNPJ do médico</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">CNPJ encontrado</response> 
        /// <response code="404">CNPJ não existente</response>
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

        /// <summary>
        /// Criar novo Médico
        /// </summary> 
        /// <param name="medico">Contrutor para criar médico</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Medicos/novoMedico
        ///     { 
        ///         "nome": "Nome do medico",
        ///         "descricao": "descricao do médico",
        ///         "contato": "1122334455",
        ///         "dataHora": "1999-09-06",
        ///         "especialidade": "Especialidade",
        ///         "cnpj": "00.112.112/001-39",
        ///         "localizacao": "Rua 1, n 123"
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna médico criado</response> 
        /// <response code="422">Médico ja cadastrado</response>
        [HttpPost("novoMedico")]
        public async Task<ActionResult> NovoMedicoAsync([FromBody] Medico medico)
        {
            await _repositorio.NovoMedicoAsync(medico);
            return Created($"api/Medicos", medico);
        }

        /// <summary>
        /// Atualizar Médico
        /// </summary> 
        /// <param name="medico">Construtor para atualizar Médico</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/Medicos/atualizarMedico 
        ///     { 
        ///         "id": 0,
        ///         "nome": "Nome do medico",
        ///         "descricao": "descricao do médico",
        ///         "contato": "1122334455",
        ///         "dataHora": "1999-09-06",
        ///         "especialidade": "Especialidade",
        ///         "cnpj": "00.112.112/001-39",
        ///         "localizacao": "Rua 1, n 123"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Médico atualizado</response> 
        /// <response code="400">Erro na requisição</response>
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

        /// <summary>
        /// Deletar médico
        /// <para>Função assíncrona para deletar médico pelo Id</para>
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Médico deletado</response>
        /// <response code="404">Id do médico não existe</response>
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
