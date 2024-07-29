using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Utility;
using Utility.Object;

namespace DAL
{
    public class Dapperr : IDapper
    {
        protected readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }






        #region async method
        // get 1
        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
        }
        // get nhiều
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryAsync<T>(sp, parms, commandType: commandType);
        }
        // insert, update, delete
        public async Task<int> ExcuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.ExecuteAsync(sp, parms, commandType: commandType);
        }

        public async Task<int> ExcuteAsyncDemo(string sp, Object parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.ExecuteAsync(sp, parms, commandType: commandType);
        }

        //public int ExcuteAsyncDemo(string sp, List<Object> ls, CommandType commandType = CommandType.Text)
        //{
        //    //string processQuery = "INSERT INTO PROCESS_LOGS VALUES (@A, @B)";
        //    //connection.Execute(processQuery, item);
        //    //var ss = new Show();
        //    //ss.color = "1";
        //    //ss.color1 = "abc";
        //    using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
        //    //db.Execute(sp, item, commandType: commandType);
        //    db.Execute(sp, ls, commandType: commandType);
        //    return 1;
        //}
        public class Show
        {
            public string color { set; get; }
            public string color1 { set; get; }
        }
        #endregion
    }
}
