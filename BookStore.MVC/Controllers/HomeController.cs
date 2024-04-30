using BookStore.Core;
using BookStore.Core.Model;
using BookStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthorService authorService;

        public HomeController(AuthorService authorservice)
        {
            this.authorService = authorservice;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> authors = await authorService.GetAllAuthors();
            return View(authors);
        }

        public async Task<IActionResult> AuthorProfile(int authorId)
        {
            Author author = await authorService.GetAuthor(authorId);
            return View(author);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
