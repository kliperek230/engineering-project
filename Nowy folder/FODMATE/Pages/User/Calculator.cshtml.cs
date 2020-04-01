using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FOODMATE.Model;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace FOODMATE
{
    public class CalculatorModel : PageModel
    {
        // KONIECZNIE TRZEBA USUN¥Æ ST¥D JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        public int userID{ get; set; }
        public string userSex { get; set; }
        public int userHeight { get; set; }
        public int userWeight { get; set; }
        public int userKcal { get; set; }
        public int userProtein { get; set; }
        public int userCarbs { get; set; }
        public int userFats { get; set; }

        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            HttpContext.Session.GetString("userSex");
            userHeight = Convert.ToInt32(HttpContext.Session.GetInt32("userHeight"));
            userWeight = Convert.ToInt32(HttpContext.Session.GetInt32("userWeight"));
            userKcal = Convert.ToInt32(HttpContext.Session.GetInt32("userKcal"));
            userProtein = Convert.ToInt32(HttpContext.Session.GetInt32("userProtein"));
            userCarbs = Convert.ToInt32(HttpContext.Session.GetInt32("userCarbs"));
            userFats = Convert.ToInt32(HttpContext.Session.GetInt32("userFats"));
        }

        public IActionResult OnPost()
        {
            int Kcal = int.Parse(Request.Form["Kcal"]);
            int Protein = int.Parse(Request.Form["Protein"]);
            int Carbs = int.Parse(Request.Form["Carbs"]);
            int Fats = int.Parse(Request.Form["Fats"]);
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE [FOODMATE].[dbo].[User] SET kcal = @Kcal, protein = @Protein, carbs = @Carbs, fats = @Fats WHERE user_id = @UserID";
                //string query = "INSERT INTO User (kcal, protein, carbs, fats) VALUES (@Kcal, @Protein, @Carbs, @Fats) WHERE user_id = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Kcal", Kcal);
                    command.Parameters.AddWithValue("@Protein", Protein);
                    command.Parameters.AddWithValue("@Carbs", Carbs);
                    command.Parameters.AddWithValue("@Fats", Fats);
                    command.Parameters.AddWithValue("@UserID", userID);


                    connection.Open();
                    return RedirectToPage("HomePage");
                }
            }
        }

        public IActionResult OnGetLogout()
        {
            return RedirectToPage("Login");
        }
    }
}