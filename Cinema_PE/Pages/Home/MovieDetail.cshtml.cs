using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema_PE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema_PE.Pages.Home
{
    public class MovieDetailModel : PageModel
    {
        private readonly MovieDBContext context;

        public Movie Movie { get; set; }
        public Rate Rate { get; set; }
        public List<Rate> Rates { get; set; }
        public List<Genre> Genres { get; set; }
        public MovieDetailModel(MovieDBContext _context)
        {
            context = _context;
        }

        public IActionResult OnGet(int id)
        {
            Movie = context.Movies.FirstOrDefault(m => m.MovieId == id);
            Rates = context.Rates.Where(r => r.MovieId == id).ToList();
            Genres = context.Genres.ToList();
            return Page();
        }

        public IActionResult OnPostMovieRating()
        {
            Member member = Helpers.GetObjectFromJson<Member>(HttpContext.Session, "member");
            if (member == null)
            {
                TempData["notFound"] = "You must login to Rating";
                return RedirectToPage("login");
            }
            else
            {
                Rate = new Rate
                {
                    MemberId = member.MemberId,
                    Comment = Request.Form["comment"],
                    MovieId = int.Parse(Request.Form["movieId"]),
                    Rating = double.Parse(Request.Form["rating"]),
                    Time = DateTime.Now
                };

                if (context.Rates.FirstOrDefault(r => r.MemberId == member.MemberId && r.MovieId == Rate.MovieId) != null)
                {
                    TempData["exist"] = "You have already rated this movie";
                    return RedirectToPage("moviedetail", new { id = int.Parse(Request.Form["movieId"]) });
                }
                else
                {
                    context.Rates.Add(Rate);
                    context.SaveChanges();
                    return RedirectToPage("moviedetail", new { id = int.Parse(Request.Form["movieId"]) });
                }

            }

        }
    }
}
