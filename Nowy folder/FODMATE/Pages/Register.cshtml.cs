using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FOODMATE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FOODMATE.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        FoodmateContext db = new FoodmateContext();

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Sex { get; set; }

        [BindProperty]
        public string BirthDate { get; set; }

        [BindProperty]
        public string UHeight { get; set; }

        [BindProperty]
        public string UWeight { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string EmailConfirm { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PasswordConfirm { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(User user)
        {
            //Checking if emails and passwords are the same
            if (Email == EmailConfirm && Password == PasswordConfirm)
            {
                //Adding new user from form inputs and clearing them
                db.User.Add(user);
                db.SaveChanges();
                //HttpContext.Session.SetString("username", Username);
                Msg = "Poprawnie przeprowadzono rejestracjê!";
                return RedirectToPage("Login");
            }
            else
            {
                Msg = "Emaile/Has³a nie zgadzaj¹ siê";
                return Page();
            }
        }
    }
}
