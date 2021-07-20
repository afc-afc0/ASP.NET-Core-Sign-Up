using System;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using DataLibrary.Constants;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString()
        {
            return CONSTANTS.dbConnection;
        }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(GetConnectionString());
        }

        public static List<T> LoadData<T>(string sqlQuery)
        {
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return connection.Query<T>(sqlQuery).AsList<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static List<T> LoadData<T>(string sqlQuery, DynamicParameters parameters)
        {
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return connection.Query<T>(sqlQuery, parameters).AsList<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static T FindData<T>(string sqlQuery, DynamicParameters parameters)
        {
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return connection.QueryFirst<T>(sqlQuery, parameters);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return default;
        }

        public static int InsertData<T>(string query, T data)
        {
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return connection.Execute(query, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        public static int InsertData<T>(string query, DynamicParameters parameters)
        {
            try
            {
                using (NpgsqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return connection.Execute(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }
    }
}
