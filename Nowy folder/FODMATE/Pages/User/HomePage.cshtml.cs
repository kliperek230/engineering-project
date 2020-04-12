using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FOODMATE.Pages
{
    public class HomePageModel : PageModel
    {
        //public int userID { get; set; }
        public string username { get; set; }
        //public int userHeight { get; set; }
        
        public void OnGet()
        {
            //userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            //userHeight = Convert.ToInt32(HttpContext.Session.GetInt32("userHeight"));
            username = HttpContext.Session.GetString("username");
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Index");
        }
    }
}