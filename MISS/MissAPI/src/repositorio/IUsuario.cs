using MissAPI.Src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio
{/// <summary>
 /// <para>Resumo: Responsavel por representar ações de CRUD de usuario</para>
 /// <para>Criado por: Samira Ixoobecan</para>
 /// <para>Versão: 1.0</para>
 /// <para>Data: 06/09/2022</para>
 /// </summary>
    public interface IUsuario
    {
        Task<List<Usuario>> PegarTodosUsuariosAsync();

        Task<Usuario> PegarUsuarioPeloIdAsync(int id);
        Task<Usuario> PegarUsuarioPeloNomeAsync(string nome);
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
        Task<Usuario> PegarUsuarioPeloCPFAsync(string cpf);
       
        Task NovoUsuarioAsync(Usuario Usuario);
        Task AtualizarUsuarioAsync(Usuario Usuario);

        Task DeletarUsuarioAsync(int id);
    }

}
