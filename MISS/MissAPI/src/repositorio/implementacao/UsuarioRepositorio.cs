using MissAPI.Src.modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissAPI.Src.contexto;

namespace MissAPI.Src.repositorio.implementacao
{

    
    public class UsuarioRepositorio : IUsuario
    {

        #region Attributes
        private readonly MissAPIContexto _contexto;
        #endregion


        #region Constructors
        public UsuarioRepositorio(MissAPIContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Metodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuarios</para>
        /// </summary>
        /// <return>Lista UsuarioModelo</return>
        public async Task<List<UsuarioModelo>> PegarTodosUsuariosAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo ID</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        /// <exception cref="Exception">Caso não encontre o usuario</exception>
        public async Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de usuario não encontrado");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="usuario">NovoUsuario</param>
        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(new UsuarioModelo
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Telefone = usuario.Telefone,
                Endereco = usuario.Endereco,
                Tipo = usuario.Tipo,
                CPF = usuario.CPF,

            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuario</param>
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            UsuarioModelo modelo = await PegarUsuarioPeloIdAsync(usuario.Id);
            modelo.Nome = usuario.Nome;
            modelo.Email = usuario.Email;
            modelo.Foto = usuario.Foto;
            modelo.Telefone = usuario.Telefone;
            modelo.Endereco = usuario.Endereco;
            modelo.CPF = usuario.CPF;
            _contexto.Update(modelo);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar senha de usuario</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuario</param>
        public async Task AtualizarSenhaUsuarioAsync(Usuario usuario)
        {
            var modelo = await PegarUsuarioPeloIdAsync(usuario.Id);

            if (modelo.Senha != CodificarSenha(usuario.SenhaAntiga)) throw new Exception("Senha antiga incorreta");

            modelo.Senha = CodificarSenha(usuario.SenhaNova);

            _contexto.Update(modelo);
            await _contexto.SaveChangesAsync();

            // função auxiliar
            string CodificarSenha(string senha)
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _contexto.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        #endregion
    }
}