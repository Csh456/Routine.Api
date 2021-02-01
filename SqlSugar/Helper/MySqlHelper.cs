using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using SqlSugar;

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
            if (Directory.GetDirectories(Path.Combine(Assembly.GetEntryAssembly().Location,
                "../../../../Entities")).Length == 0)
            {
                _client.DbFirst.CreateClassFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "../../../Entities");
            }
        }
        
    }
}
