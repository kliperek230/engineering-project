using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using foodmateapp.Model;
using System.Linq;
using Microsoft.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace foodmateapp.Controllers
{
    public class AccountController : Controller
    {
        FoodmateContext db = new FoodmateContext();
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Calculator()
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
                    
                    return Json(new { status = false, message = "A" });
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

        [HttpPost]
        public ActionResult RegisterUser(Users user)
        {
            string email = Request.Form["Email"];
            string email_confrm = Request.Form["Email_confirm"];
            string pswd = Request.Form["Pswd"];
            string pswd_confrm = Request.Form["Pswd_confirm"];

            if (email == email_confrm & pswd == pswd_confrm)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Users");
            } else
            {
                //return View();
                return Json(new { status = true, message = "Login Successfullaaaaaaaaaaaaaaaaaaaaaaaa!" });
            }
        }

        [HttpPost]
        public ActionResult UpdateUserMacros(Users users)
        {
            int Kcal = int.Parse(Request.Form["Kcal"]);
            int Protein = int.Parse(Request.Form["Protein"]);
            int Carbs = int.Parse(Request.Form["Carbs"]);
            int Fats = int.Parse(Request.Form["Fats"]);
            int UserId = 1;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET kcal = @Kcal, protein = @Protein, carbs = @Carbs, fats = @Fats WHERE user_id = @UserId";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Kcal", Kcal);
                    command.Parameters.AddWithValue("@Protein", Protein);
                    command.Parameters.AddWithValue("@Carbs", Carbs);
                    command.Parameters.AddWithValue("@Fats", Fats);
                    command.Parameters.AddWithValue("@UserId", UserId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    //Check Error
                    if(result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                }
                return View("Calculator");
            }
        }
        //public class DataToCalc
        //{
        //    public string Sex { get; set; }
        //    public int Age { get; set; }
        //    public double Height { get; set; }
        //    public double Weight { get; set; }
        //    public string Activity { get; set; }
        //    public string Goal { get; set; }
        //    public double Kcal { get; set; }
        //    public double Protein { get; set; }
        //    public double Carbs { get; set; }
        //    public double Fats { get; set; }

        //}
        
        //[HttpPost, ValidateAntiForgeryToken]
        //public IActionResult MacroCalc(DataToCalc datacalc)
        //{
        //    double BMR = 0;
        //    double CPM = 0; //Całkowita przemiana materii
        //    double CZK = 0; //Całkowite zapotrzebowanie kaloryczne
        //    string activity = datacalc.Activity;
        //    string[] activity_types = {
        //        "Brak aktywności, praca siedząca",
        //        "Niska aktywność, praca siedząca i 1/2 treningi w tygodniu",
        //        "Średnia aktywność, praca siedząca i 3/4 treningi w tygodniu",
        //        "Wysoka aktywność, praca fizyczna i 3/4 treningi w tygodniu"
        //    };
        //    double multiplier = 0;
        //    int additional_calories = 0;
        //    string goal = datacalc.Goal;
        //    string[] goal_types = {
        //        "Utrzymanie wagi",
        //        "Zwiększenie masy mięśniowej",
        //        "Redukcja tkanki tłuszczowej"
        //    };

        //    if (activity == activity_types[0])
        //    {
        //        multiplier = 1.2;
        //        ViewBag.multiplier = multiplier;
        //        return View();
        //    }
        //    else if (activity == activity_types[1])
        //    {
        //        multiplier = 1.35;
        //        ViewBag.multiplier = multiplier;
        //        return View();
        //    }
        //    else if (activity == activity_types[2])
        //    {
        //        multiplier = 1.55;
        //        ViewBag.multiplier = multiplier;
        //        return View();
        //    }
        //    else if (activity == activity_types[3])
        //    {
        //        multiplier = 1.75;
        //        ViewBag.multiplier = multiplier;
        //        return View();
        //    }


        //    if (goal == goal_types[0])
        //    {
        //        additional_calories = 0;
        //        ViewBag.additional_calories = additional_calories;
        //        return View();
        //    }
        //    else if (goal == goal_types[1])
        //    {
        //        additional_calories = 300;
        //        ViewBag.additional_calories = additional_calories;
        //        return View();
        //    }
        //    else if (goal == goal_types[2])
        //    {
        //        additional_calories = -300;
        //        ViewBag.additional_calories = additional_calories;
        //        return View();
        //    }

        //    if (datacalc.Sex == "Kobieta")
        //    {
        //        BMR = 655 + (9.6 * datacalc.Weight) + (1.8 * datacalc.Height) - (4.7 * datacalc.Age);
        //        ViewBag.BMR = BMR;
        //        return View();
        //    } 
        //    else if(datacalc.Sex == "Mężczyzna")
        //    {
        //        BMR = 66 + (13.7 * datacalc.Weight) + (5 * datacalc.Height) - (6.76 * datacalc.Age);
        //        ViewBag.BMR = BMR;
        //        return View();
        //    };

        //    CPM = BMR * multiplier;
        //    CZK = CPM + additional_calories;
        //    ViewBag.CZK = CZK;
        //    return View();
            
        //}

    }

}