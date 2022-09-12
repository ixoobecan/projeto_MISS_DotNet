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

        #region Atributos
        private readonly MissContexto _contexto;
        #endregion


        #region Construtores
        public UsuarioRepositorio(MissContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Metodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos usuarios</para>
        /// </summary>
        /// <return>Lista UsuarioModelo</return>
        public async Task<List<Usuario>> PegarTodosUsuariosAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuário pelo ID</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        /// <exception cref="Exception">Caso não encontre o usuário</exception>
        public async Task<Usuario> PegarUsuarioPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de usuário não encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(i => i.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(i => i.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuário pelo nome</para>
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        /// <returns>Usuario</returns>
        public async Task<Usuario> PegarUsuarioPeloNomeAsync(string nome)
        {
            if (!ExisteNome(nome)) throw new Exception("Nome de usuário não encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(n => n.Nome == nome);

            // função auxiliar
            bool ExisteNome(string nome)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(n => n.Nome == nome);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuário pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <return>Usuario</return>
        public async Task<Usuario> PegarUsuarioPeloEmailAsync(string email)
        {
            if (!ExisteEmail(email)) throw new Exception("Email do usuário não encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(e => e.Email == email);

            // função auxiliar
            bool ExisteEmail(string email)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(e => e.Email == email);
                return auxiliar != null;
            }

        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuário pelo cpf</para>
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public async Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf)
        {
            if (!ExisteCPF(cpf)) throw new Exception("CPF usuário não encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(c => c.CPF == cpf);

            // função auxiliar
            bool ExisteCPF(string cpf)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(c => c.CPF == cpf);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuário</para>
        /// </summary>
        /// <param name="usuario">NovoUsuario</param>
        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Telefone = usuario.Telefone,
                Endereco = usuario.Endereco,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuário</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuario</param>
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            var aux = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            aux.Nome = usuario.Nome;
            aux.Email = usuario.Email;
            aux.Senha = usuario.Senha;
            aux.Foto = usuario.Foto;
            aux.Telefone = usuario.Telefone;
            aux.Endereco = usuario.Endereco;
            aux.CPF = usuario.CPF;

            _contexto.Usuarios.Update(aux);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuário</para>
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