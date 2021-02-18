using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Routine.Api.Data;
using Routine.Api.Services;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Routine.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com",
                            Title = "�д��󣡣���",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "�뿴��ϸ��Ϣ",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId",context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = {"application/problem+json"}
                        };
                    };
                });

            //.net core 3.0���°汾ʹ��
            //services.AddMvc();
            //ÿ��http����
            services.AddScoped<ICompanyRepository,CompanyRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(option=> 
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SparkTodo API",
                    Description = "API for SparkTodo",
                    Contact = new OpenApiContact() { Name = "Cai", Email = "1572926739@qq.com" }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼
                var xmlPath = Path.Combine(basePath ?? string.Empty, "SwaggerDemo.xml");
                option.IncludeXmlComments(xmlPath);
                //option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().FullName}"));
            });

            services.AddDbContext<RoutineDbContext>(option =>
            {
                
                option.UseSqlite("Data Source=routine.db");
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //�ǿ����������쳣ʱ����
                app.UseExceptionHandler(appBuilder => 
                {
                    appBuilder.Run(async context=> 
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error");
                    });
                });
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c=> 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "CaiShaoHua";
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
