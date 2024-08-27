using HotelApp.Shared.Common;
using HotelApp.Shared.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Login
{
    public interface ILoginBusiness
    {
        DbResponse ManageUserDetails(LoginCommon param);
        LoginCommon CheckUserLogin(LoginCommon loginCommon);

	}
}
