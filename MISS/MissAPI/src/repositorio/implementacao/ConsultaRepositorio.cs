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
        private readonly MissAPIContexto _contexto;

        public object StatusDoacao { get; private set; }
        #endregion


        #region Construtores
        public ConsultaRepositorio(MissAPIContexto context)
        {
            _contexto = context;
        }
        #endregion


        #region Metodos
         /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma consulta pelo Cnpj</para>
        /// </summary>
        /// <param name="idMedico">Id da consulta</param>
        /// <return>Lista ConsultaModelo</return>
        /// <exception cref="Exception">Caso não encontre o Medico</exception>
        public async Task<List<ConsultaModelo>> PegarConsultaPeloIdMedicoAsync(int idMedico)
        {
            if (!ExisteId(idMedico)) throw new Exception("Id do Medico não encontrado");

            return await _contexto.Consultas
                .Include(s => s.Medico)
                .Include(s => s.Doacao)
                .Where(s => s.Medico.Id == idMedico)
                .ToListAsync();

            // função auxiliar
            bool ExisteId(int idMedico)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == idMedico);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova consulta</para>
        /// </summary>
        /// <param name="dto">NovaConsultaDTO</param>
        public async Task NovaConsultaAsync(Consulta consulta)
        {
            if (!ExisteIdMedico(consulta.IdMedico)) throw new Exception("Id da ONG não encontrado");

            if (!ExisteIdDoacao(consulta.IdDoacao)) throw new Exception("Id da doação não encontrado");

            await RegraRestaInativa(consulta.IdDoacao);

            await _contexto.Consultas.AddAsync(new ConsultaModelo
            {
                Medico = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == consulta.IdMedico),
                Doacao = await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == consulta.IdDoacao)
            });
            await _contexto.SaveChangesAsync();

            // função auxiliar
            bool ExisteIdMedico(int idMedico)
            {
                var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Id == idMedico);
                return auxiliar != null;
            }

            bool ExisteIdDoacao(int idDoacao)
            {
                var auxiliar = _contexto.Doacoes.FirstOrDefault(d => d.Id == idDoacao);
                return auxiliar != null;
            }

            async Task RegraRestaInativa(int idDoacao)
            {
                var aux1 = await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == idDoacao);
                var result1 = aux1.Dias - aux1.Hora;

                if (result1 <= 0)
                {
                    aux1.Dias = 0;
                    aux1.Status = StatusDoacao.INATIVO;
                }
                else
                {
                    aux1.Hora = result1;
                }
                await _contexto.SaveChangesAsync();
            }
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas consultas</para>
        /// </summary>
        /// <return>Lista ConsultaModelo</return>
        public async Task<List<ConsultaModelo>> PegarTodasConsultaAsync()
        {  
            return await _contexto.Consultas
                .Include(s => s.Medico)
                .Include(s => s.Doacao)
                .ToListAsync();
        }
        #endregion
    }
    
}