using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FODMATE
{
    public class ProgressChoiceModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPost(string ProgressGym, string ProgressBody)
        {
            if (!string.IsNullOrEmpty(ProgressGym))
            {
                return RedirectToPage("ProgressGym");
            }
            if (!string.IsNullOrEmpty(ProgressBody))
            {
                return RedirectToPage("ProgressBody");
            }
            return null;
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Login");
        }
    }
}