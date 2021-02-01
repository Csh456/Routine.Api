using System;
using System.Configuration;
using System.IO;

namespace SqlSugar.Helper
{
    public class MySqlHelper
    {
        private readonly SqlSugarClient _client;
        public MySqlHelper(string connectionStr)
        {
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
            if (Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase ?? string.Empty,
                "Entities")).Length == 0)
            {
                _client.DbFirst.CreateClassFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Entities");
            }
        }
        
    }
}
