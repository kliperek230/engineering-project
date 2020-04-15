using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FOODMATE
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPost(string Login, string Register)
        {
            if (!string.IsNullOrEmpty(Login))
            {
                return RedirectToPage("Anonymous/Login");
            }
            if (!string.IsNullOrEmpty(Register))
            {
                return RedirectToPage("Anonymous/Register");
            }
            return null;
        }
    }
}