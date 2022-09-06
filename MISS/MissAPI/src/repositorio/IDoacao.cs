using MissAPI.Src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio
{
    public interface IDoacao
    {
        Task<List<DoacaoModelo>> PegarTodasDoacoesAsync();
        Task<DoacaoModelo> PegarDoacaoPeloIdAsync(int id);
        Task NovaDoacaoAsync(Doacao doacao);
        Task AtualizarDoacaoAsync(Doacao doacao);
        Task DeletarDoacaoAsync(int id);
    }
}
