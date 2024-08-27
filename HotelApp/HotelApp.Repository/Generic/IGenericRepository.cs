using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Generic
{
    public interface IGenericRepository
    {
        DbResponse ManageData<T>(string spName, T input);
        T ManageDataWithSingleObject<T>(string spName, object obj);
        List<T> ManageDataWithListObject<T>(string spName, object obj);
        List<List<SelectListItem>> ManageDataWithMultipleSelectListItem(string spName, string flagName);
        List<object> ManageDataWithListObjectMultiple<T0, T1>(string spName, object obj);
        List<object> ManageDataWithListObjectMultiple<T0, T1, T2, T3, T4>(string spName, object obj);
        List<object> ManageDataWithListObjectMultiple<T0, T1, T2>(string spName, object obj);
        List<object> ManageDataWithListObjectMultiple<T0, T1, T2, T3>(string spName, object obj);
        List<List<SelectListItem>> ManageDataWithMultipleSelectList(string spName, object param);
    }
}
