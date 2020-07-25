using System;
using Healthcare.Data.Infrastructure;
using Healthcare.Data.UnitOfWork;
using HealthCare.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Healthcare.Services;

namespace HealthcareAPI
{
    public class Startup
    {
        readonly string integrated_security = string.Empty;
        readonly string database_host = string.Empty;
        readonly string database_name = string.Empty;
        readonly string database_user = string.Empty;
        readonly string database_password = string.Empty;
        readonly string connection = string.Empty;
        public Startup(IConfiguration configuration)
        {
            integrated_security = Environment.GetEnvironmentVariable("INTEGRATED_SECURITY");
            database_host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            database_name = Environment.GetEnvironmentVariable("DATABASE_NAME");
            database_user = Environment.GetEnvironmentVariable("DATABASE_USER");
            database_password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
            if (integrated_security.ToLower() == "true")
                connection = "Server=" + database_host + ";Database=" + database_name + ";Integrated Security=True;MultipleActiveResultSets=True";
            else
                connection = "Server=" + database_host + ";Database=" + database_name + ";Integrated Security=False;User Id=" + database_user + ";Password=" + database_password + ";MultipleActiveResultSets=True";

            Environment.SetEnvironmentVariable("connectionString", connection);
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HealthcareDBContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IContractService, ContractService>();
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
        }
        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HealthcareDBContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            app.UseAuthentication();
        }
    }
}
