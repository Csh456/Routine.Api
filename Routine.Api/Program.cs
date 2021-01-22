using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;

namespace Routine.Api
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                try 
                {
                    var dbContext = scope.ServiceProvider.GetService<RoutineDbContext>();
                    //清空数据库数据
                    dbContext.Database.EnsureDeleted();
                    //迁移数据库
                    dbContext.Database.Migrate();
                }
                catch(Exception e) 
                {
                    //Console.WriteLine(e);
                    //throw;

                    var logger = scope.ServiceProvider.GetRequiredService<Logger<Program>>();
                    logger.LogError(e, "DataBase Migration Error");
                }
            }

            host.Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
