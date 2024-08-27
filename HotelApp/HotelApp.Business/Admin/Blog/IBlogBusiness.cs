using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Blog
{
    public interface IBlogBusiness
    {
        DbResponse ManageBlogDetails(BlogCommon BlogCommon);
        BlogCommon GetBlogSetupDetails(object param);
        Task<List<BlogCommon>> GetRequiredDetailList(GridParam gridParam);
        DbResponse ManageBlogStaus(object param);

    }
}
