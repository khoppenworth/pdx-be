using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using System.Data.SqlClient;
using PDX.DAL.Helpers;

namespace PDX.DAL.Reporting.Engine
{
    public class DataTableQueryExecuter
    {
        private readonly string _connectionString;
        public DataTableQueryExecuter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Execute(string query)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    System.Data.DataTable dataTable = new DataTable();
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }

        public Tuple<DataTable, DataTable> ExecuteMultiple(string query)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var dataSet = new DataSet("queryResult");

                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
                    dataAdapter.Fill(dataSet);

                    return dataSet.Tables.Count > 0 ? new Tuple<DataTable, DataTable>(dataSet.Tables[0], dataSet.Tables[1]) : null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }
    }
}