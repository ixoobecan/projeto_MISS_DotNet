using Microsoft.EntityFrameworkCore;
using MissAPI.Src.contexto;
using MissAPI.Src.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissAPI.Src.repositorio.implementacao
{
    public class DoacaoRepositorio : IDoacao
    {

        #region Atributos
        private readonly MissAPIContexto _contexto;

        public object StatusDoacao { get; private set; }
        #endregion


        #region Construtores
        public DoacaoRepositorio(MissAPIContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion


        #region Metodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar doações</para>
        /// </summary>
        /// <return>Lista DoacaoModelo</return>
        public async Task<List<DoacaoModelo>> PegarTodasDoacoesAsync()
        {
            return await _contexto.Doacoes
                .Where(d => d.Status == StatusDoacao.ATIVO)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um doação pelo ID</para>
        /// </summary>
        /// <param name="id">Id da doação</param>
        /// <return>DoacaoModelo</return>
        /// <exception cref="Exception">Caso não encontre a doação</exception>
        public async Task<DoacaoModelo> PegarDoacaoPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de doação não encontrado");

            return await _contexto.Doacoes.FirstOrDefaultAsync(d => d.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Doacoes.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova doacao</para>
        /// </summary>
        /// <param name="doacao">NovaDoacao</param>
        public async Task NovaDoacaoAsync(Doacao doacao)
        {
            await _contexto.Doacoes.AddAsync(new DoacaoModelo
            {
                Nome = doacao.Nome,
                Descricao = doacao.Descricao,
                Contato = doacao.Contato,
                Dias = doacao.Dias,
                Hora = doacao.Hora,
                Especialidade = doacao.Especialidade,
                CNPJDoador = doacao.CNPJDoador,
                Localizacao = doacao.Localizacao,
                Status = StatusDoacao.ATIVO
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma doacao</para>
        /// </summary>
        /// <param name="doacao">AtualizarDoacao</param>
        public async Task AtualizarDoacaoAsync(Doacao doacao)
        {
            var modelo = await PegarDoacaoPeloIdAsync(doacao.Id);
            modelo.Nome = doacao.Nome;
            modelo.Descricao = doacao.Descricao;
            modelo.Contato = doacao.Contato;
            modelo.Dias = doacao.Dias;
            modelo.Hora = doacao.Hora;
            modelo.Especialidade = doacao.Especialidade;
            modelo.CNPJDoador = doacao.CNPJDoador;
            modelo.Localizacao = doacao.Localizacao;
            _contexto.Update(modelo);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma doacao</para>
        /// </summary>
        /// <param name="id">Id da doacao</param>
        public async Task DeletarDoacaoAsync(int id)
        {
            _contexto.Doacoes.Remove(await PegarDoacaoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
        #endregion
    }
}


       

    

       
       