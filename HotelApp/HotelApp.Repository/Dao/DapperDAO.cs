using Dapper;
using HotelApp.Shared.Common;
using Microsoft.Extensions.Configuration;

using Serilog;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelApp.Repository.Dao
{
    public class DapperDao : IDapperDao
    {
        private readonly string _connectionString;
        public DapperDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<T0> ExecuteQuery<T0>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000,
                        commandType: queryType);
                    var res = result.Read<T0>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 90000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                res.Add(result.Read<T2>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<object> ExecuteQuery<T0, T1>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 90000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3, T4>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 90000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                res.Add(result.Read<T2>().ToList());
                res.Add(result.Read<T3>().ToList());
                res.Add(result.Read<T4>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 90000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                res.Add(result.Read<T2>().ToList());
                res.Add(result.Read<T3>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<List<GenericStaticData>> ExecuteQueryWithMultipleSelectValues<T0>(string sqlQuery, object sqlParam,
        CommandType queryType = CommandType.StoredProcedure)
        {
            using var sqlConnection = new SqlConnection(GetConnectionString());
            try
            {
                List<List<GenericStaticData>> listOfGenerics = new List<List<GenericStaticData>>();
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000,
                    commandType: queryType);
                while (!result.IsConsumed)
                {
                    listOfGenerics.Add(result.Read<GenericStaticData>().ToList());
                }
                return listOfGenerics;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private string GetConnectionString()
        {
            return _connectionString ?? "";
        }
    }
}
