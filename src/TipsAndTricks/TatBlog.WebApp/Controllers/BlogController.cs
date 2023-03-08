using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Constants;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        //public IActionResult Index()
        //{
        //    ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        //    return View();
        //}

        //public IActionResult About()        
        //    =>View();

        //public IActionResult Contact()
        //    => View();

        //public IActionResult Rss()
        //    => Content("Noi dung se duoc cap nhat");

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name ="p")]int pageNumber=1,
            [FromQuery(Name ="ps")] int pageSize=1)


        {
            var postQuery = new PostQuery()
            {
                PublishedOnly = true,

                KeyWord=keyword
            };

            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            ViewBag.PostQuery = postQuery;

            return View(postList);
        }
        
    }
}
