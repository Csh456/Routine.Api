using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Routine.Api.Data;
using Routine.Api.Services;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Routine.Api
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
            //.net Code3.0之前的写法
            //services.AddControllers(setup=>
            //{
            //    //当请求的类型和输出的类型不一致就会返回406状态码
            //    setup.ReturnHttpNotAcceptable = true;
            //    //配置xml支持（默认支持json）（输出）
            //    //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

            //    //默认支持xml（输出）
            //    //setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
            //});

            //.net Code3.0的添加xml支持的写法
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            //.net core 3.0以下版本使用
            //services.AddMvc();
            //每次http请求
            services.AddScoped<ICompanyRepository,CompanyRepository>();
            services.AddDbContext<RoutineDbContext>(option =>
            {
                
                option.UseSqlite("Data Source=routine.db");
            });
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
