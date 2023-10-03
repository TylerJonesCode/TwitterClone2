using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace testapi.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }
        private IDbConnection connectDB(){
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = connectDB();
            return dbConnection.Query<T>(sql);
        }
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = connectDB();
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = connectDB();
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = connectDB();
            return dbConnection.Execute(sql);
        }
        //Broken Fix later
        public bool ExecuteSqlWithParameters(string sql, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(sql);
            foreach(SqlParameter parameter in parameters)
            {
                sqlCommand.Parameters.Add(parameter);
            }
            
            IDbConnection dbConnection = connectDB();
            dbConnection.Open();
               
            sqlCommand.Connection = (SqlConnection)dbConnection;
            
            int rowsAffected = sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
            return rowsAffected > 0;
        }

    }

    

}