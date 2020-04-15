using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FODMATE
{
    public class ResultsChoiceModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPost(string ResultsGym, string ResultsBody, string ResultsWeight)
        {
            if (!string.IsNullOrEmpty(ResultsGym))
            {
                return RedirectToPage("ResultsGym");
            }
            if (!string.IsNullOrEmpty(ResultsBody))
            {
                return RedirectToPage("ResultsBody");
            }
            if (!string.IsNullOrEmpty(ResultsWeight))
            {
                return RedirectToPage("ResultsWeight");
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