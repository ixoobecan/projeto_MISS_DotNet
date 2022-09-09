using MissAPI.Src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio
{
    public interface IConsulta
    {
        Task<List<Consulta>> PegarTodasConsultasAsync();

        Task<Consulta>PegarConsultaPeloIdAsync(int idConsulta);

        Task NovaConsultaAsync(Consulta consulta);
        Task AtualizarConsultaAsync(Consulta consulta);
        Task DeletarConsultaAsync(int id);
        
    }
}
