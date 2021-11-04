using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Customer;
using UnitOfWork.Repositories;

namespace UnitOfWork.Web
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
            services.AddControllers();

            //This is the only service available at ConfigureServices

            //使用内存数据库
            //var connection = new SqliteConnection(Configuration.GetConnectionString("InMemoryConnection"));
            //connection.Open();
            //services.AddDbContext<UnitOfWorkDbContext>(options =>
            //    options.UseSqlite(connection));

            services.AddDbContext<UnitOfWorkDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("SqliteConnection")));
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

            //使用扩展方法注入Uow依赖
            services.AddUnitOfWork<UnitOfWorkDbContext>();
            //使用默认方法注入Uow依赖
            //services.AddScoped<IUnitOfWork, UnitOfWork<UnitOfWorkDbContext>>();

            //注入泛型仓储
            services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepository<,>));

            services.AddTransient<ICustomerAppService, CustomerAppService>();
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
