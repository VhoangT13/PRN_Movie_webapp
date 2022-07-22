using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema_PE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema_PE.Pages.Home
{
    public class HomeModel : PageModel
    {
        private readonly MovieDBContext context;
        public HomeModel(MovieDBContext mvContext)
        {
            context = mvContext;
        }
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }

        public void OnGet()
        {
            Movies = context.Movies.ToList();
            Genres = context.Genres.ToList();

        }

        public IActionResult OnGetSearch(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                Movies = context.Movies.ToList();
            }
            else
            {
                Movies = context.Movies.Where(m => m.Title.Contains(query)).ToList();
            }
            Genres = context.Genres.ToList();

            return Page();
        }

        public IActionResult OnGetGenre(int id)
        {
            Movies = context.Movies.Where(m => m.GenreId == id).ToList();
            Genres = context.Genres.ToList();

            return Page();
        }
    }
}
