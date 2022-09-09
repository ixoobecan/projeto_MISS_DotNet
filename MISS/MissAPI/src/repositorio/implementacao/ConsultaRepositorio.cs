using Microsoft.EntityFrameworkCore;
using MissAPI.Src.contexto;
using MissAPI.Src.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio.implementacao
{
    public class ConsultaRepositorio : IConsulta
    {

        #region Atributos
        private readonly MissContexto _contexto;
        #endregion


        #region Construtores
        public ConsultaRepositorio(MissContexto context)
        {
            _contexto = context;
        }
        #endregion


        #region Metodos
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas as consultas</para>
        /// </summary>
        /// <return>Lista Consulta</return>
        public async Task<List<Consulta>> PegarTodasConsultasAsync()
        {
            return await _contexto.Consultas
                .Include(m => m.Medico)
                .Include(u => u.Usuario)
                .ToListAsync();
        }

        public async Task<Consulta> PegarConsultaPeloIdAsync(int idConsulta)
        {
            if (!ExisteIdConsulta(idConsulta)) throw new Exception("Id da consulta não encontrado");

            return await _contexto.Consultas
                .Include(m => m.Medico)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(c => c.Id == idConsulta);

            bool ExisteIdConsulta(int id)
            {
                var aux = _contexto.Consultas.FirstOrDefaultAsync(c => c.Id == id);
                return aux != null;
            }
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova consulta</para>
        /// </summary>
        /// <param name="dto">NovaConsultaDTO</param>
        public async Task NovaConsultaAsync(Consulta consulta)
        {
            if (!ExisteIdMedico(consulta.Medico.Id)) throw new Exception("Id do médico não encontrado");

            if (!ExisteIdUsuario(consulta.Usuario.Id)) throw new Exception("Id do paciente não encontrado");

            await _contexto.Consultas.AddAsync(new Consulta
            {
                Medico = await _contexto.Medicos.FirstOrDefaultAsync(m => m.Id == consulta.Medico.Id),
                Usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == consulta.Usuario.Id),
                DataHora = consulta.DataHora,
                StatusConsulta = consulta.StatusConsulta
            });
            await _contexto.SaveChangesAsync();

            // função auxiliar
            bool ExisteIdMedico(int idMedico)
            {
                var auxiliar = _contexto.Medicos.FirstOrDefault(m => m.Id == idMedico);
                return auxiliar != null;
            }

            bool ExisteIdUsuario(int idUsuario)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(d => d.Id == idUsuario);
                return auxiliar != null;
            }
        }

        public async Task AtualizarConsultaAsync(Consulta consulta)
        {
            var aux = await _contexto.Consultas.FirstOrDefaultAsync(c => c.Id == consulta.Id);
            aux.Medico = consulta.Medico;
            aux.DataHora = consulta.DataHora;
            aux.StatusConsulta = consulta.StatusConsulta;

            _contexto.Consultas.Update(aux);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma consulta</para>
        /// </summary>
        /// <param name="id">Id da Consultas</param>
        public async Task DeletarConsultaAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id da consulta não encontrado");

            _contexto.Consultas.Remove(await _contexto.Consultas.FirstOrDefaultAsync(s => s.Id == id));
            await _contexto.SaveChangesAsync();

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Consultas.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }
        #endregion
    }

}