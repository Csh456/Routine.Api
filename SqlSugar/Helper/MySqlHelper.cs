using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using SqlSugar;
using SqlSugarTest.Entities;

namespace SqlSugarTest.Helper
{
    public class MySqlHelper
    {
        private readonly SqlSugarClient _client;
        public MySqlHelper(string connectionStr)
        {
            //Console.WriteLine(Assembly.GetEntryAssembly().Location);
            _client = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionStr,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            CheckEntitiesFolder();

        }

        private void CheckEntitiesFolder()
        {
            if (Directory.GetDirectories(Path.Combine(Assembly.GetEntryAssembly()?.Location ?? string.Empty,
                "../../../../Entities")).Length == 0)
            {
                _client.DbFirst.CreateClassFile(
                    Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase ?? string.Empty, "../../../Entities"),
                    "SqlSugarTest.Entities");
            }
        }
        //插入数据
        public async Task<studentinfo> Insert(studentinfo stu)
        {
           var newStu = await _client.Insertable<studentinfo>(stu).ExecuteReturnEntityAsync();
           return newStu;
        }
    }
}
