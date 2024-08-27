using HotelApp.Repository.Dao;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Generic
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDapperDao _dapperDao;

        public GenericRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }
        /// <summary>
        /// Generic Method For CRUD operation
        /// </summary>
        /// <typeparam name="T">Object For Stored Procedure</typeparam>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="input">Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public DbResponse ManageData<T>(string spName, T input)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<DbResponse>(procedureName, input);
            return response.FirstOrDefault();
        }

        public T ManageDataWithSingleObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            return response.FirstOrDefault();
        }

        ///<summary>
        /// Generic Method For List of Anonymous Object as a return
        /// </summary>
        /// <typeparam name = "T" > Object For Stored Procedure</typeparam>
        /// <param name = "spName" > Stored Procedure Name</param>
        /// <param name = "obj" > Object For Stored Procedure</param>
        /// <returns>System Response With Code And Messages</returns>
        public List<T> ManageDataWithListObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            return response;
        }
        public List<List<SelectListItem>> ManageDataWithMultipleSelectListItem(string spName, string flagName)
        {
            var param = new
            {
                Flag = flagName
            };
            var multipleSelectList = new List<List<SelectListItem>>();
            var response = _dapperDao.ExecuteQueryWithMultipleSelectValues<List<List<SelectListItem>>>(spName, param);
            foreach (var eachResponse in response)
            {
                multipleSelectList.Add(eachResponse.Select(x => new SelectListItem { Value = x.Value, Text = x.Description }).ToList());
            }
            return multipleSelectList;
        }
        public List<object> ManageDataWithListObjectMultiple<T0, T1>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T0, T1>(procedureName, obj);
            return response;
        }
        public List<object> ManageDataWithListObjectMultiple<T0, T1,T2>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T0, T1,T2>(procedureName, obj);
            return response;
        }
        public List<object> ManageDataWithListObjectMultiple<T0, T1, T2, T3, T4>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T0, T1, T2, T3, T4>(procedureName, obj);
            return response;
        }
        public List<object> ManageDataWithListObjectMultiple<T0, T1, T2, T3>(string spName, object obj)
        {
            string procedureName = spName;
            var response = _dapperDao.ExecuteQuery<T0, T1, T2, T3>(procedureName, obj);
            return response;
        }
        public List<List<SelectListItem>> ManageDataWithMultipleSelectList(string spName, object param)
        {
            var multipleSelectList = new List<List<SelectListItem>>();
            var response = _dapperDao.ExecuteQueryWithMultipleSelectValues<List<List<SelectListItem>>>(spName, param);
            foreach (var eachResponse in response)
            {
                multipleSelectList.Add(eachResponse.Select(x => new SelectListItem { Value = x.Value, Text = x.Description }).ToList());
            }
            return multipleSelectList;
        }
    }
}
