using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FOODMATE.Model;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace FOODMATE.Pages
{
    public class LoginModel : PageModel
    {
        FoodmateContext db = new FoodmateContext();

        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(User user)
        {
            
            var _user = db.User.Where(s => s.Username == user.Username);
            // Checking if Username exist in DB
            if (_user.Any())
            {
                // Checking if password match password in DB
                if (_user.Where(s => s.Password == user.Password).Any())
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        string query = "SELECT * FROM [FOODMATE].[dbo].[User] WHERE username= @Username AND password= @Password";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            int userID = 0;
                            string userFirstName = "";
                            string userLastName = "";
                            string userSex = "";
                            string userBirthDate = "";
                            int userHeight = 0;
                            int userWeight = 0;
                            string username = "";
                            int userKcal = 0;
                            int userProtein = 0;
                            int userCarbs = 0;
                            int userFats = 0;
                            int userBench = 0;
                            int userOhp = 0;
                            int userSquat = 0;
                            int userDeadlift = 0;
                           
                            
                            command.Parameters.AddWithValue("@Username", Username);
                            command.Parameters.AddWithValue("@Password", Password);

                            List<Measurements> singleUser = new List<Measurements>();

                            connection.Open();

                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {

                                    userID = Convert.ToInt32((dr["user_id"]));
                                    userFirstName = (dr["first_name"].ToString());
                                    userLastName = (dr["last_name"].ToString());
                                    userSex = (dr["sex"].ToString());
                                    userBirthDate = (dr["birth_date"].ToString());
                                    userHeight = Convert.ToInt32((dr["u_height"]));
                                    userWeight = Convert.ToInt32((dr["u_weight"]));
                                    username = (dr["username"].ToString());
                                    userKcal = Convert.ToInt32((dr["kcal"]));
                                    userProtein = Convert.ToInt32((dr["protein"]));
                                    userCarbs = Convert.ToInt32((dr["carbs"]));
                                    userFats = Convert.ToInt32((dr["fats"]));
                                    //userBench = Convert.ToInt32((dr["bench"]));
                                    //userOhp = Convert.ToInt32((dr["ohp"]));
                                    //userSquat = Convert.ToInt32((dr["squat"]));
                                    //userDeadlift = Convert.ToInt32((dr["deadlift"]));
                                }
                                
                            }
                            connection.Close();

                            HttpContext.Session.SetInt32("userID", userID);
                            HttpContext.Session.SetString("userFirstName", userFirstName);
                            HttpContext.Session.SetString("userLastName", userLastName);
                            HttpContext.Session.SetString("userSex", userSex);
                            HttpContext.Session.SetString("userBirthDate", userBirthDate);
                            HttpContext.Session.SetInt32("userHeight", userHeight);
                            HttpContext.Session.SetInt32("userWeight", userWeight);
                            HttpContext.Session.SetString("username", username);
                            HttpContext.Session.SetInt32("userKcal", userKcal);
                            HttpContext.Session.SetInt32("userProtein", userProtein);
                            HttpContext.Session.SetInt32("userCarbs", userCarbs);
                            HttpContext.Session.SetInt32("userFats", userFats);
                            HttpContext.Session.SetInt32("userBench", userBench);
                            HttpContext.Session.SetInt32("userOhp", userOhp);
                            HttpContext.Session.SetInt32("userSquat", userSquat);
                            HttpContext.Session.SetInt32("userDeadlift", userDeadlift);
                            return RedirectToPage("User/HomePage");                           
                        }                      
                    }                   
                }
            }
            return null;
        }
    }
}
