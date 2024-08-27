using HotelApp.Repository.Generic;
using HotelApp.Shared.Common;
using HotelApp.Shared.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Login
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IGenericRepository _genericRepository;
        public static string StoreProcedureName = "Setting.Proc_UserDetailManagement";
        public LoginBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public DbResponse ManageUserDetails(LoginCommon param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public LoginCommon CheckUserLogin(LoginCommon loginCommon)
        {
            var response = _genericRepository.ManageDataWithSingleObject<LoginCommon>(StoreProcedureName, loginCommon);
            return response;
        }
    }
}
