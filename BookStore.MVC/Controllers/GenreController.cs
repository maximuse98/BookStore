using BookStore.Core;
using BookStore.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class GenreController : Controller
    {
        public readonly GenreService genreService;

        public GenreController(GenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Genre> genres = await genreService.GetAllGenres();

            return View(genres);
        }

        public async Task<IActionResult> Profile(int genreId)
        {
            Genre genre = await genreService.Get(genreId);

            return View(genre); 
        }
    }
}
