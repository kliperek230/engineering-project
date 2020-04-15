using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FOODMATE.Model;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace FOODMATE.Pages
{
    public class LoginModel : PageModel
    {
        FoodmateContext db = new FoodmateContext();

        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=localhost;Database=FOODMATE;Trusted_Connection=True;";

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
            if (Username == null)
            {
                Msg = "Wpisz swój Login";
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM [FOODMATE].[dbo].[User] WHERE username= @Username";
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
                        string HashedPassword = "";
                        //int userKcal = 0;
                        //int userProtein = 0;
                        //int userCarbs = 0;
                        //int userFats = 0;
                        //int userBench = 0;
                        //int userOhp = 0;
                        //int userSquat = 0;
                        //int userDeadlift = 0;


                        command.Parameters.AddWithValue("@Username", Username);

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
                                HashedPassword = (dr["password"].ToString());
                                username = (dr["username"].ToString());
                                //userKcal = Convert.ToInt32((dr["kcal"]));
                                //userProtein = Convert.ToInt32((dr["protein"]));
                                //userCarbs = Convert.ToInt32((dr["carbs"]));
                                //userFats = Convert.ToInt32((dr["fats"]));
                                //userBench = Convert.ToInt32((dr["bench"]));
                                //userOhp = Convert.ToInt32((dr["ohp"]));
                                //userSquat = Convert.ToInt32((dr["squat"]));
                                //userDeadlift = Convert.ToInt32((dr["deadlift"]));
                            }

                        }
                        connection.Close();

                        //Decrypting Hashed Password and Comparing it with Users input
                        try
                        {
                            byte[] hashBytes = Convert.FromBase64String(HashedPassword);
                            byte[] salt = new byte[16];
                            Array.Copy(hashBytes, 0, salt, 0, 16);

                            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 10000);

                            byte[] hash = pbkdf2.GetBytes(20);

                            int ok = 1;
                            for (int i = 0; i < 20; i++)
                            {
                                if (hashBytes[i + 16] != hash[i])
                                {
                                    ok = 0;
                                }
                            }

                            if (ok == 1)
                            {
                                HttpContext.Session.SetInt32("userID", userID);
                                HttpContext.Session.SetString("userFirstName", userFirstName);
                                HttpContext.Session.SetString("userLastName", userLastName);
                                HttpContext.Session.SetString("userSex", userSex);
                                HttpContext.Session.SetString("userBirthDate", userBirthDate);
                                HttpContext.Session.SetInt32("userHeight", userHeight);
                                HttpContext.Session.SetInt32("userWeight", userWeight);
                                HttpContext.Session.SetString("username", username);
                                //HttpContext.Session.SetInt32("userKcal", userKcal);
                                //HttpContext.Session.SetInt32("userProtein", userProtein);
                                //HttpContext.Session.SetInt32("userCarbs", userCarbs);
                                //HttpContext.Session.SetInt32("userFats", userFats);
                                //HttpContext.Session.SetInt32("userBench", userBench);
                                //HttpContext.Session.SetInt32("userOhp", userOhp);
                                //HttpContext.Session.SetInt32("userSquat", userSquat);
                                //HttpContext.Session.SetInt32("userDeadlift", userDeadlift);
                                return RedirectToPage("../User/HomePage");
                            }
                            else
                            {
                                Msg = "Hasło nieprawidłowe, spróbuj ponownie";
                            }
                        }
                        catch
                        {
                            Msg = "Hasło nieprawidłowe, spróbuj ponownie";
                        }
                    }
                }
            }
            return null;
        }
    }
}
