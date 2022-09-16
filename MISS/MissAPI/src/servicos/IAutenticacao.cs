using MissAPI.Src.modelos;
using System.Threading.Tasks;

namespace MissAPI.Src.servicos
{
    /// <summary> 
    /// <para>Resumo: Interface Responsavel por representar ações de autenticação</para> 
    /// <para>Criado por: Rafael Candeias</para> 
    /// <para>Data: 13/09/2022</para>
    /// </summary>
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario); 
    }
}
