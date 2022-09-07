using Microsoft.EntityFrameworkCore;
using MissAPI.Src.modelos;

namespace MissAPI.Src.contexto
{/// <summary>
 /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DdSet</para>
 /// <para>Criado por:Samira Ixoobecan</para>
 /// <para>Versão: 1.0</para>
 /// <para>Data: 06/09/2022</para>
 /// </summary>
    public class MissContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        public MissContexto(DbContextOptions<MissContexto> opt) : base(opt) {
        }
    }
}
         
        
        

