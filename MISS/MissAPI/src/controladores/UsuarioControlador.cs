using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissAPI.Src.modelos;
using MissAPI.Src.repositorio;
using MissAPI.Src.servicos;
using System;
using System.Threading.Tasks;

namespace MissAPI.Src.controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        private readonly IAutenticacao _servicos;
        
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio, IAutenticacao servicos)
        {
            _repositorio = repositorio;
            _servicos = servicos;
        }
        #endregion 

        #region Métodos

        /// <summary> 
        /// Pegar todos os usuários
        /// </summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna todos os usuarios</response> 
        /// <response code="403">Usuario não autorizado</response>
        [HttpGet("todosUsuarios")]
        [Authorize(Roles = "MEDICO,ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosUsuariosAsync()
        {
            var lista = await _repositorio.PegarTodosUsuariosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar usuário pelo Id
        /// </summary>
        /// <param name="idUsuario">Id do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("id/{idUsuario}")]
        [Authorize(Roles = "MEDICO,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPeloIdAsync(idUsuario));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary> 
        /// Pegar usuário pelo Nome
        /// </summary> 
        /// <param name="nomeUsuario">Nome do usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("nome/{nomeUsuario}")]
        [Authorize(Roles = "MEDICO,ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloNomeAsync([FromRoute] string nomeUsuario)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPeloNomeAsync(nomeUsuario));
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar usuário pelo Email
        /// </summary>
        /// <param name="emailUsuario">Email do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario));
            }
            catch(Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar usuário pelo CPF
        /// </summary>
        /// <param name="cpfUsuario">CPF do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("cpf/{cpfUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloCPFAsync([FromRoute] string cpfUsuario)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPeloCPFAsync(cpfUsuario));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        /// <param name="usuario">Contrutor para criar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/novoUsuario
        ///     { 
        ///         "nome": "Nome do usuário",
        ///         "email": "usuario@email.com",
        ///         "senha": "senha123",
        ///         "foto": "UrlFoto",
        ///         "telefone": "1122334455",
        ///         "endereco": "Rua 1, n 123",
        ///         "cpf": "11122233344",
        ///         "tipo": "NORMAL"
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="422">Email ja cadastrado</response>
        [HttpPost("novoUsuario")]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);
                return Created($"api/Usuarios/email/{usuario.Email}", usuario);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary> 
        /// <param name="usuario">Construtor para atualizar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/Usuarios/atualizarUsuario 
        ///     { 
        ///         "id": 0,
        ///         "nome": "Nome do usuário",
        ///         "email": "usuario@email.com",
        ///         "senha": "senha123",
        ///         "foto": "UrlFoto",
        ///         "telefone": "1122334455",
        ///         "endereco": "Rua 1, n 123",
        ///         "cpf": "11122233344",
        ///         "tipo": "NORMAL"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("atualizarUsuario")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] Usuario usuario)
        {
            try
            {
                await _repositorio.AtualizarUsuarioAsync(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar usuário
        /// <para>Função assíncrona para deletar usuário pelo Id</para>
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuário deletado</response>
        /// <response code="404">Id do usuário não existe</response>
        [HttpDelete("deletarUsuario/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarUsuarioAsync([FromRoute] int idUsuario)
        {
            try
            {
                await _repositorio.DeletarUsuarioAsync(idUsuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }


        /// <summary> 
        /// Pegar Autorização 
        /// </summary> 
        /// <param name="usuario">Construtor para logar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/logar 
        ///     { 
        ///         "email": "usuario@email.com",
        ///         "senha": "134652" 
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="200">Usuario logado</response> 
        /// <response code="401">E-mail ou senha invalido</response>
        /// 
        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] Usuario usuario)
        {
            var auxiliar = await _repositorio.PegarUsuarioPeloEmailAsync(usuario.Email);

            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "Email inválido!"
            });

            if (auxiliar.Senha != _servicos.CodificarSenha(usuario.Senha))
                return Unauthorized(new { Mensagem = "Senha inválida!" });

            var token = "Bearer " + _servicos.GerarToken(auxiliar);
            return Ok(new { Usuario = auxiliar, Token = token });
        }
        #endregion 
    }
}