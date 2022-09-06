using Microsoft.EntityFrameworkCore;
using MissAPI.Src.modelos;

namespace MissAPI.Src.contexto
{/// <summary>
 /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DdSet</para>
 /// <para>Criado por:Samira Ixoobecan</para>
 /// <para>Versão: 1.0</para>
 /// <para>Data: 06/09/2022</para>
 /// </summary>
    public class MissAPIContexto : DbContext
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; }
        public DbSet<DoacaoModelo> Doacoes { get; set; }
        public DbSet<ConsultaModelo> Consultas { get; set; }

        public MissAPIContexto(DbContextOptions<MissAPIContexto> opt) : base(opt) { }
    }
}
         
        
        

