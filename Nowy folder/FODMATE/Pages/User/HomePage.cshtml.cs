using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FOODMATE.Pages
{
    public class HomePageModel : PageModel
    {
        public int userID { get; set; }
        public string userNickname { get; set; }
        public int userHeight { get; set; }
        
        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            userHeight = Convert.ToInt32(HttpContext.Session.GetInt32("userHeight"));
            userNickname = HttpContext.Session.GetString("userNickname");
        }
        public IActionResult OnGetLogout()
        {
            return RedirectToPage("../Index");
        }
    }
}