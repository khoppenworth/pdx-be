using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Npgsql;

namespace PDX.DAL.Reporting.Engine
{
    public class DapperQueryExecuter
    {
        private readonly string _connectionString;
         public DapperQueryExecuter(string connectionString)
         {
             _connectionString = connectionString;
         }

         public IEnumerable<dynamic> Execute(string query){
             using( IDbConnection connection = new NpgsqlConnection(_connectionString)){
                try{
                    connection.Open();
                    var list = connection.Query<dynamic>(query);
                    return list;
                }
                catch( Exception ex){
                    Console.WriteLine(ex.Message);
                }finally{
                    connection.Close();
                }
             }

             return null;
         }

         public Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> ExecuteMultiple(string query){
             using( IDbConnection connection = new NpgsqlConnection(_connectionString)){
                try{
                    connection.Open();
                    var multi = connection.QueryMultiple(query);
                    var data1 = multi.Read<dynamic>();
                    var data2 = multi.Read<dynamic>();
                    return new  Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>>(data1, data2);
                }
                catch( Exception ex){
                    Console.WriteLine(ex.Message);
                }finally{
                    connection.Close();
                }
             }

             return null;
         }
    }
}