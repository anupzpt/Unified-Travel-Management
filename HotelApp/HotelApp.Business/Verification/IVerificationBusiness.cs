using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Verification
{
    public interface IVerificationBusiness
    {
        DbResponse RegisterVerificationCode(object param);
        DbResponse CheckVerificationCode(object param);

    }
}
