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
        Task<List<UsuarioModelo>> PegarTodosUsuariosAsync();
        Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id);
        Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(Usuario Usuario);
        Task AtualizarUsuarioAsync(Usuario Usuario);
        Task AtualizarSenhaUsuarioAsync(Usuario Usuario);
        Task DeletarUsuarioAsync(int id);
    }

}
