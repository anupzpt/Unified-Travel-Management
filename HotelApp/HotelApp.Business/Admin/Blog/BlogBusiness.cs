using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Blog
{
    public class BlogBusiness : IBlogBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;

        private static string StoreProcedureName = "Setting.Proc_BlogManagement";
        public BlogBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }
        public DbResponse ManageBlogDetails(BlogCommon BlogCommon)
        {
            var BlogImage = BlogCommon.BlogImageFile != null ? _fileHelperExtension.UploadFile(BlogCommon.BlogImageFile).Result : BlogCommon.BlogImageView;
            var param = new
            {
                BlogImageView = BlogImage,
                Flag = BlogCommon.Flag,
                BlogCode = BlogCommon.BlogCode,
                Title = BlogCommon.Title,
                SloganName = BlogCommon.SloganName,
                Description = BlogCommon.Description
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public BlogCommon GetBlogSetupDetails(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<BlogCommon>(StoreProcedureName, param);
            return details;
        }
        public async Task<List<BlogCommon>> GetRequiredDetailList(GridParam gridParam)
        {
            var BlogList = _genericRepository.ManageDataWithListObject<BlogCommon>(StoreProcedureName, gridParam);
            return BlogList;
        }
        public DbResponse ManageBlogStaus(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
    }
}
