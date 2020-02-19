using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using foodmateapp.Model;
using System.Linq;

namespace foodmateapp.Controllers
{
    public class AccountController : Controller
    {
        FoodmateContext db = new FoodmateContext();

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Validate(Users user)
        {
            var _user = db.Users.Where(s => s.Email == user.Email);
            if (_user.Any())
            {
                if (_user.Where(s => s.Pswd == user.Pswd).Any())
                {
                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }
        }
    }
}