using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using knewinAPI.Models;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace knewinAPI
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
            services.AddDbContext<BancoContext>(opt =>
                                               opt.UseInMemoryDatabase("KnewinList"));

            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "knewinAPI", Version = "v1" });
            });

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "knewinAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BancoContext>();
                PovoarCidades(context);
            }


            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void PovoarCidades(BancoContext context)
        {
            //new Cidade("Salvador", 4000000, new List<string> { "Lauro de Freitas", "Simões Filho", "Candeias" })

            context.Cidade.Add(new Cidade { Id = 1, Nome = "Campo Grande", Habitantes = 796000, Fronteira = new string[] { "Terenos", "Rochedo", "Sidrolandia" } });
            context.Cidade.Add(new Cidade { Id = 2, Nome = "Curitiba", Habitantes = 1948000, Fronteira = new string[] { "São Jose dos Pinhais", "Pinhais", "Araucaia" } });
            context.Cidade.Add(new Cidade { Id = 3, Nome = "Florianopolis", Habitantes = 508000, Fronteira = new string[] { "Sao Jose", "Palhoca" } });
            context.Cidade.Add(new Cidade { Id = 4, Nome = "Porto Alegre", Habitantes = 1488000, Fronteira = new string[] { "Alvorada", "Cachoeirinha", "Canoas" } });
            context.Cidade.Add(new Cidade { Id = 5, Nome = "Gramado", Habitantes = 36000 ,Fronteira = new string[] { "Caxias do Sul", "Tres Coroas", "Canela" } });
            context.Cidade.Add(new Cidade { Id = 6, Nome = "Bonito", Habitantes = 22000, Fronteira = new string[] { "Bodoquena", "Miranda", "Aquidauana" } });
            context.Cidade.Add(new Cidade { Id = 7, Nome = "Terenos", Habitantes = 22000, Fronteira = new string[] { "Campo Grande", "Aquidaudana"} });
            context.Cidade.Add(new Cidade { Id = 8, Nome = "Canela", Habitantes = 45000, Fronteira = new string[] { "Gramado", "Caxias do Sul", "Tres coroas" } });
            context.Cidade.Add(new Cidade { Id = 9, Nome = "Nova petropolis", Habitantes = 21000, Fronteira = new string[] { "Caxias do Sul", "Gramado" } });
            context.Cidade.Add(new Cidade { Id = 10, Nome = "Sao Paulo", Habitantes = 12330000, Fronteira = new string[] { "Guarulhos", "Osasco", "Santo Andre" } });
            context.Cidade.Add(new Cidade { Id = 11, Nome = "Foz do Iguacu", Habitantes = 258000, Fronteira = new string[] { "Puerto Iguazu", "Ciudad del Leste"} });
            context.Cidade.Add(new Cidade { Id = 12, Nome = "Puerto Iguazu", Habitantes = 820000, Fronteira = new string[] { "Foz do Iguacu", "Ciudad del Leste"} });
            context.Cidade.Add(new Cidade { Id = 13, Nome = "Ciudad del Leste", Habitantes = 301000, Fronteira = new string[] { "Foz do Iguacu", "Puerto Iguazu"} });
            context.Cidade.Add(new Cidade { Id = 14, Nome = "Rochedo", Habitantes = 2200, Fronteira = new string[] { "Campo Grande", "Gramado"} });
            context.Cidade.Add(new Cidade { Id = 15, Nome = "Sidrolandia", Habitantes = 2000, Fronteira = new string[] { "Campo Grande", "Bodoquena"} });


            context.SaveChanges();
        }
    }
}