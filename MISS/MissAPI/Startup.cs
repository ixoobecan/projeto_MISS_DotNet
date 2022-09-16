using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MissAPI.Src.contexto;
using MissAPI.Src.repositorio;
using MissAPI.Src.repositorio.implementacao;
using MissAPI.Src.servicos;
using MissAPI.Src.servicos.Implementacoes;
using System.Text;

namespace MissAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Config Banco de dados
            services.AddDbContext<MissContexto>(opt =>
                opt.UseSqlServer(Configuration["ConnectionStringsDev:DefaultConnection"]));

            // Repositorios
            services.AddScoped<IUsuario, UsuarioRepositorio>();
            services.AddScoped<IMedico, MedicoRepositorio>();
            services.AddScoped<IConsulta, ConsultaRepositorio>();

            // Config de Serviços
            services.AddScoped<IAutenticacao, AutenticacaoServicos>();

            // Config Token Autenticação JWT Bearer
            var chave = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Controladores
            services.AddCors();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MissContexto contexto)
        {
            // Desenvolvimento
            if (env.IsDevelopment())
            {
                contexto.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

            // Produção
            contexto.Database.EnsureCreated();

            // Rotas
            app.UseRouting();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Autenticação e Autorização
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
