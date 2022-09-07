﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissAPI.Src.modelos;
using MissAPI.Src.repositorio;
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
        
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion 

        #region Métodos

       
        [HttpGet("todosUsuarios")]
        public async Task<ActionResult> PegarTodosUsuariosAsync()
        {
            var lista = await _repositorio.PegarTodosUsuariosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

       
        [HttpGet("id/{idUsuario}")]
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

        [HttpGet("nome/{nomeUsuario}")]
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


        [HttpGet("email/{emailUsuario}")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);
            if (usuario == null) return NotFound(new { message = "Usuario não existe" });
            return Ok(usuario);
        }

        [HttpGet("cpf/{cpfUsuario}")]
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

        [HttpPost]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] Usuario usuario)
        {
            await _repositorio.NovoUsuarioAsync(usuario);
            return Created($"api/Usuarios", usuario);
        }

        
        [HttpPut]
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
        
        [HttpDelete("deletarUsuario/{id}")]
        public async Task<ActionResult> DeletarUsuarioAsync([FromRoute] int id)
        {
            try
            {
                await _repositorio.DeletarUsuarioAsync(id);
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