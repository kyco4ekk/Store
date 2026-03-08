using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Services
{
    internal class DataBaseService
    {
        private readonly string _connectionString;
        public DataBaseService(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public int Execute(string sql, Action<SqlCommand>? parameterize = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(sql, conn);

            parameterize?.Invoke(cmd);

            return cmd.ExecuteNonQuery();
        }
        public T? QuerySingle<T>(string sql, Func<SqlDataReader, T> map, Action<SqlCommand> parameterize = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(sql, conn);
            parameterize?.Invoke(cmd);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? map(reader) : default;
        }

        public List<T>? QueryList<T>(string sql, Func<SqlDataReader, T> map, Action<SqlCommand>? parameterize = null)
        {
            var list = new List<T>();

            using var conn = GetConnection();
            using var cmd = new SqlCommand(sql, conn);
            parameterize?.Invoke(cmd);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(map(reader));

            return list;
        }
    }
}