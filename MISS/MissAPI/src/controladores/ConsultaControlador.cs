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
        /// <summary> 
        /// Pegar todos as consultas
        /// </summary>
        /// <para> Resumo: Método assincrono para pegar todas as consultas</para>
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna todos as consultas</response> 
        /// <response code="403">Usuário não autorizado</response>
        [HttpGet("todasConsultas")]
        public async Task<ActionResult> PegarTodasConsultasAsync()
        {
            var lista = await _repositorio.PegarTodasConsultasAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar consulta pelo Id
        /// </summary>
        /// <param name="idConsulta">Id da consulta</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Consulta encontrada</response> 
        /// <response code="404">Id não existente</response>
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

        /// <summary>
        /// Criar uma nova consulta
        /// </summary> 
        /// <param name="consulta">Contrutor para criar uma nova consulta</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Consultas/novaConsulta
        ///     { 
        ///         "dataHora": "2022-09-15",
        ///         "local": "Rua 1, n 123",
        ///         "status": "ATIVO",
        ///         "usuario": {
        ///             "id": 0
        ///         },
        ///         "medico":{
        ///             "id": 0
        ///         }
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna médico criado</response> 
        /// <response code="422">Médico ja cadastrado</response>
        [HttpPost("novaConsulta")]
        public async Task<ActionResult> NovaConsultaAsync([FromBody] Consulta consulta)
        {
            await _repositorio.NovaConsultaAsync(consulta);
            return Created($"api/Consultas", consulta);
        }

        /// <summary>
        /// Atualizar Consulta
        /// </summary> 
        /// <param name="consulta">Construtor para atualizar consulta</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/Consultas/atualizarConsulta 
        ///     { 
        ///         "id": 0,
        ///         "dataHora": "2022-09-15",
        ///         "local": "Rua 1, n 123",
        ///         "status": "ATIVO",
        ///         "usuario": {
        ///             "id": 0
        ///         },
        ///         "medico":{
        ///             "id": 0
        ///         }
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Consulta atualizado</response> 
        /// <response code="400">Erro na requisição</response>
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

        /// <summary>
        /// Deletar médico
        /// <para>Função assíncrona para deletar médico pelo Id</para>
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Consulta deletado</response>
        /// <response code="404">Id da consulta não existe</response>
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
