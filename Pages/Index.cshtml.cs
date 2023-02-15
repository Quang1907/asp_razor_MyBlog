using ASP_RAZOR_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_RAZOR_5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContext blogContext;


        public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myBlogContext )
        {
            _logger = logger;
            blogContext = _myBlogContext;
        }

        public void OnGet()
        {
            var posts = (from a in blogContext.articles
                         orderby a.Created descending
                         select a).ToList();
            ViewData["posts"] = posts;
        }
    }
}