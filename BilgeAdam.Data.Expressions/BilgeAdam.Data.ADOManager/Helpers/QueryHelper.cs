using BilgeAdam.Data.ADOManager.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace BilgeAdam.Data.ADOManager.Helpers
{
    public class QueryHelper
    {
        private SqlConnection connection;
        public QueryHelper()
        {
            connection = NorthwindConnection.Connection;
        }
        public List<T> GetData<T>(string query, object param = null)
        {
            var result = connection.Query<T>(query, param).ToList();
            return result;
        }
    }
}
