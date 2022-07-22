using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema_PE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema_PE.Pages.Home
{
    public class LoginModel : PageModel
    {
        private readonly MovieDBContext context = new MovieDBContext();

        public string email { get; set; }
        public string password { get; set; }
        public Member Member { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnGetLogout()
        {
            Helpers.RemoveSession(HttpContext.Session, "member");
            return Redirect("home");
        }

        public IActionResult OnPost()
        {
            email = Request.Form["email"];
            password = Helpers.EncryptPassword(Request.Form["password"]);

            Member = context.Members.FirstOrDefault(m => m.Email == email &&
                m.Password == password);
            if (email.Length == 0)
            {
                TempData["emailErr"] = "Email is required";
            }
            else if (password.Length == 0)
            {
                TempData["passwordErr"] = "Password is required";
            }
            else if (Member == null)
            {
                TempData["notFound"] = "Email or password incorrect. Please try again!";
            }
            else if (Member != null && Member.Type == 2)
            {
                Helpers.SetObjectAsJson(HttpContext.Session, "member", Member);
                return RedirectToPage("home");
            }
            return Page();

        }


    }
}
