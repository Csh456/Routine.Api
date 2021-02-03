using System;
using Microsoft.Extensions.Configuration;
using SqlSugarTest.Helper;
namespace SqlSugarTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var config = ConfigHelper.AddConfigFiles();

            var connectionStr = config.GetConnectionString("MySqlConnection");

            var sqlHelper=new MySqlHelper(connectionStr);
            Console.ReadKey();
        }
    }
}
