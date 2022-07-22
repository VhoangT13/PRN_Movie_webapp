using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cinema_PE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema_PE.Pages.Home
{
    public class RegisterModel : PageModel
    {
        private readonly MovieDBContext context;
        public void OnGet()
        {
        }

        [BindProperty]
        public Member Member { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }

        public RegisterModel(MovieDBContext _context)
        {
            context = _context;
        }

        public IActionResult OnPost()
        {
            Member = new Member
            {
                Fullname = Request.Form["fullname"],
                Gender = Request.Form["gender"],
                IsActive = bool.Parse(Request.Form["isActive"]),
                Type = int.Parse(Request.Form["type"]),
                Email = Request.Form["email"],
                Password = Helpers.EncryptPassword(Request.Form["password"])
            };
            ConfirmPassword = Helpers.EncryptPassword(Request.Form["confirmPassword"]);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (Member.Password != ConfirmPassword)
                {
                    ViewData["errPwd"] = "Confirm password not match!";
                    return Page();
                }
                else if (context.Members.FirstOrDefault(m => m.Email == Member.Email) != null)
                {
                    ViewData["error"] = "Account already exsited!";
                    return Page();
                }
                else
                {
                    context.Members.Add(Member);
                    context.SaveChanges();
                    TempData["msg"] = "Registration succesfully";
                    return RedirectToPage("Login");
                }

            }
        }
    }
}
