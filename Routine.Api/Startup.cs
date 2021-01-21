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
            //.net Code3.0֮ǰ��д��
            //services.AddControllers(setup=>
            //{
            //    //����������ͺ���������Ͳ�һ�¾ͻ᷵��406״̬��
            //    setup.ReturnHttpNotAcceptable = true;
            //    //����xml֧�֣�Ĭ��֧��json���������
            //    //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

            //    //Ĭ��֧��xml�������
            //    //setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
            //});

            //.net Code3.0�����xml֧�ֵ�д��
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            //.net core 3.0���°汾ʹ��
            //services.AddMvc();
            //ÿ��http����
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
