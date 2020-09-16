using System.Linq;
using Api.Infrastructure;
using Api.Model;
using Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IHeroesRepository, HeroesRepository>();
            services.AddDbContext<HeroesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("heroesConnection")));
            services.AddCors();

            var context = services.BuildServiceProvider().GetService<HeroesContext>();
            
            if (Environment.IsDevelopment())
            {
                context.Database.Migrate();
            }

            //https://github.com/dotnet/AspNetCore.Docs/issues/16452
            if (!context.Heroes.Any())
            {
                context.Heroes.AddRange(InitialHeroes.GetHeroes());
                context.SaveChanges();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var corsValues = Configuration.GetSection("Cors")
                .GetChildren().Select(a => a.Value).Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();

            app.UseCors(builder =>
                builder.WithOrigins(corsValues)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", (context) => context.Response.WriteAsync("Success"));
                endpoints.MapControllers();
            });
        }

        
    }
}
