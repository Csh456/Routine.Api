using System.IO;
using Microsoft.Extensions.Configuration;

namespace SqlSugarTest.Helper
{
    public class ConfigHelper
    {
        public static IConfigurationRoot AddConfigFiles()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            return config;
        }
    }
}
