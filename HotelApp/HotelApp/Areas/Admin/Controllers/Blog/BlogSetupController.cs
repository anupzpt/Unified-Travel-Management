using HotelApp.Business.Admin.Blog;
using HotelApp.Models;
using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BlogSetupController : Controller
    {
        private readonly IBlogBusiness _blogBusiness;
        public BlogSetupController(IBlogBusiness blogBusiness)
        {
            _blogBusiness = blogBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetRequiredDetailList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetRequiredDetailList",
                Search = param.search.value,
                UserName = StaticData.GetUser(HttpContext)
            };
            var gridList = await _blogBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("BlogSetup", item.BlogCode);
            }
            HtmlGrid<BlogCommon> companyGrid = new HtmlGrid<BlogCommon>();
            companyGrid.aaData = gridList;
            var firstDefault = gridList.FirstOrDefault();
            if (firstDefault != null)
            {
                companyGrid.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                companyGrid.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }
            var result = JsonConvert.SerializeObject(companyGrid).ToString();
            return result;
        }

        public IActionResult ManageBlogSetup()
        {
            var details = new BlogCommon();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddBlogSetup(BlogCommon BlogCommon)
        {
            BlogCommon.Flag = "AddBlogDetails";
            BlogCommon.UserName = StaticData.GetUser(HttpContext);
            var response = _blogBusiness.ManageBlogDetails(BlogCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateBlogSetup(string code)
        {
            var param = new
            {
                Flag = "GetBlogDetails",
                BlogCode = StaticData.Base64Decode_URL(code),
            };
            var detail = _blogBusiness.GetBlogSetupDetails(param);
            return View("ManageBlogSetup", detail);
        }
        [HttpPost]
        public IActionResult UpdateBlogSetup(BlogCommon BlogCommon)
        {
            BlogCommon.Flag = "UpdateBlogDetails";
            var response = _blogBusiness.ManageBlogDetails(BlogCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateBlogStatus(string code)
        {
            var param = new
            {
                Flag = "UpdateBlogStatus",
                BlogCode = StaticData.Base64Decode_URL(code),
            };
            var response = _blogBusiness.ManageBlogStaus(param);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
    }
}
