using MissAPI.Src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio
{
    public interface IMedico
    {
        Task<List<Medico>> PegarTodosMedicosAsync();

        Task<Medico> PegarMedicoPeloIdAsync(int id);
        Task<Medico> PegarMedicoPelaEspecialidadeAsync(string especialidade);
        Task<Medico> PegarMedicoPeloCNPJAsync(string cnpj);

        Task NovoMedicoAsync(Medico medico);
        Task AtualizarMedicoAsync(Medico medico);
        Task DeletarMedicoAsync(int id);
    }
}
