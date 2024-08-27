using HotelApp.Repository.Generic;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Verification
{
    public class VerificationBusiness : IVerificationBusiness
    {

        private readonly IGenericRepository _genericRepository;
        private static string StoreProcedureName = "Setting.VerificationManagement";
        public VerificationBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public DbResponse RegisterVerificationCode(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public DbResponse CheckVerificationCode(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
    }
}
