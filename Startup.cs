using internapi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using internapi.Repository.IRepository;
using internapi.Repository;
using internapi.Mapper;
using AutoMapper;

namespace internapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((option) =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IInternshipRepository, InternshipRepository>();
            services.AddAutoMapper(typeof(InternapiMappings));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
