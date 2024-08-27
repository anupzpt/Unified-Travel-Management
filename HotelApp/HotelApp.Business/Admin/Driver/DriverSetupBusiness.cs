using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Driver;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Driver
{
    public class DriverSetupBusiness : IDriverSetupBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;

        private static string StoreProcedureName = "Setting.Proc_DriverManagement";
        public DriverSetupBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }
        public DbResponse ManageDriverDetails(DriverCommon DriverCommon)
        {
            var DriverImage = DriverCommon.DriverImageFile != null ? _fileHelperExtension.UploadFile(DriverCommon.DriverImageFile).Result : DriverCommon.DriverImageView;
            var param = new
            {
                DriverImageView = DriverImage,
                Flag = DriverCommon.Flag,
                DriverCode = DriverCommon.DriverCode,
                FullName = DriverCommon.FullName,
                Age = DriverCommon.Age,
                ContactNo = DriverCommon.ContactNo,
                LiscineNo = DriverCommon.LiscineNo,
                Experience = DriverCommon.Experience,

            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public DriverCommon GetDriverSetupDetails(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<DriverCommon>(StoreProcedureName, param);
            return details;
        }
        public async Task<List<DriverCommon>> GetRequiredDetailList(GridParam gridParam)
        {
            var DriverList = _genericRepository.ManageDataWithListObject<DriverCommon>(StoreProcedureName, gridParam);
            return DriverList;
        }
        public DbResponse ManageDriverStaus(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
    }
}
