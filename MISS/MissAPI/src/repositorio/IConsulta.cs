using MissAPI.Src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio
{
    public interface IConsulta
    {
        Task<List<ConsultaModelo>> PegarTodasConsultaAsync();
        Task<List<ConsultaModelo>> PegarConsultaPeloIdMedicoAsync(int idMedico);
        Task NovaConsultaAsync(Consulta consulta);
        Task DeletarConsultaAsync(int id);
        
    }
}
