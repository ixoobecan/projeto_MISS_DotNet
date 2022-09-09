﻿using Microsoft.EntityFrameworkCore;
using MissAPI.Src.contexto;
using MissAPI.Src.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio.implementacao
{
    public class MedicoRepositorio : IMedico
    {

        #region Atributos
        private readonly MissContexto _contexto;

        public object StatusDoacao { get; private set; }
        #endregion


        #region Construtores
        public MedicoRepositorio(MissContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Metodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos médicos</para>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Medico>> PegarTodosMedicosAsync()
        {
            return await _contexto.Medicos.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um doação pelo ID</para>
        /// </summary>
        /// <param name="id">Id da doação</param>
        /// <return>DoacaoModelo</return>
        /// <exception cref="Exception">Caso não encontre a doação</exception>
        public async Task<Medico> PegarMedicoPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id do médico não encontrado");

            return await _contexto.Medicos.FirstOrDefaultAsync(d => d.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Medicos.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar médico pela especialidade </para>
        /// </summary>
        /// <param name="especialidade"></param>
        /// <returns></returns>
        public async Task<Medico> PegarMedicoPelaEspecialidadeAsync(string especialidade)
        {
            return await _contexto.Medicos.FirstOrDefaultAsync(e => e.Especialidade == especialidade);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar médico pelo CNPJ </para>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<Medico> PegarMedicoPeloCNPJAsync(string cnpj)
        {
            return await _contexto.Medicos.FirstOrDefaultAsync(c => c.CNPJ == cnpj);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova doacao</para>
        /// </summary>
        /// <param name="doacao">NovaDoacao</param>
        public async Task NovoMedicoAsync(Medico medico)
        {
            await _contexto.Medicos.AddAsync(new Medico
            {
                Nome = medico.Nome,
                Descricao = medico.Descricao,
                Contato = medico.Contato,
                DataHora = medico.DataHora,
                Especialidade = medico.Especialidade,
                CNPJ = medico.CNPJ,
                Localizacao = medico.Localizacao,
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma doacao</para>
        /// </summary>
        /// <param name="doacao">AtualizarDoacao</param>
        public async Task AtualizarMedicoAsync(Medico medico)
        {
            var aux = await _contexto.Medicos.FirstOrDefaultAsync(m => m.Id == medico.Id);
            aux.Nome = medico.Nome;
            aux.Descricao = medico.Descricao;
            aux.Contato = medico.Contato;
            aux.DataHora = medico.DataHora;
            aux.Especialidade = medico.Especialidade;
            aux.Localizacao = medico.Localizacao;

            _contexto.Update(medico);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma doacao</para>
        /// </summary>
        /// <param name="id">Id da doacao</param>
        public async Task DeletarMedicoAsync(int id)
        {
            _contexto.Medicos.Remove(await PegarMedicoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
        #endregion
    }
}


       

    

       
       