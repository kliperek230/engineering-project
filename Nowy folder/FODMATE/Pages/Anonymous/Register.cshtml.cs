using System;
using System.Collections.Generic;
using FOODMATE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using FusionCharts.Visualization;
using FusionCharts.DataEngine;
using System.Data;
using System.Security.Cryptography;

namespace FOODMATE
{
    public class RegisterModel : PageModel
    {
        // KONIECZNIE TRZEBA USUN�� ST�D TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        FoodmateContext db = new FoodmateContext();

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Sex { get; set; }

        [BindProperty]
        public string UHeight { get; set; }

        [BindProperty]
        public string UWeight { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string EmailConfirm { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PasswordConfirm { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(User user)
        {
            var MonthList = new List<string>() { "Stycze�", "Luty", "Marzec", "Kwiecie�", "Maj", "Czerwiec", "Lipiec", "Sierpie�", "Wrzesie�", "Pa�dziernik", "Listopad", "Grudzie�" };

            int Day = int.Parse(Request.Form["Day"]);
            string Month = Request.Form["Month"].ToString();
            int Year = int.Parse(Request.Form["Year"]);
            int MonthValue = MonthList.IndexOf(Month) + 1;
            string Date = Year + "-" + MonthValue + "-" + Day;

            if (FirstName == null ||
                LastName == null ||
                Sex == null ||
                UHeight == null ||
                UWeight == null ||
                Username == null ||
                Email == null ||
                EmailConfirm == null ||
                Password == null ||
                PasswordConfirm == null)
            {
                Msg = "Prosz� uzupe�ni� wszystkie kolumny";
                return null;
            }



            //Hashing Password Algorythm
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            //End of Algorythm

            //Checking if emails and passwords are the same

            List<string> EmailList;
            List<string> UsernameList;

            if (Email == EmailConfirm && Password == PasswordConfirm)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string queryCheckInformations = "SELECT email, username FROM [FOODMATE].[dbo].[User]";
                    using (SqlCommand command = new SqlCommand(queryCheckInformations, connection))
                    {
                        connection.Open();

                        EmailList = new List<string>();
                        UsernameList = new List<string>();

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            EmailList.Add(row["email"].ToString());
                            UsernameList.Add(row["username"].ToString());
                        }
                        connection.Close();
                    }

                    for(int i = 0; i <= (EmailList.Count -1 ); i++)
                    {
                        if (Email != EmailList[i])
                        {
                            if (Username != UsernameList[i])
                            {

                            }
                            else
                            {
                                Msg = "U�ytkownik o takiej nazwie ju� istnieje";
                                return null;
                            }
                        }
                        else
                        {
                            Msg = "Podany adres email zosta� ju� u�yty do rejestracji w serwisie";
                            return null;
                        }
                    }

                    string queryInsertUser = "INSERT INTO [FOODMATE].[dbo].[User] (first_name, last_name, sex, birth_date, u_height, u_weight, email, password, username)" +
                        " VALUES (@FirstName, @LastName, @Sex, @BirthDate, @Height, @Weight, @Email, @Password, @Username)";

                    using (SqlCommand command = new SqlCommand(queryInsertUser, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@Sex", Sex);
                        command.Parameters.AddWithValue("@BirthDate", Date);
                        command.Parameters.AddWithValue("@Height", UHeight);
                        command.Parameters.AddWithValue("@Weight", UWeight);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", savedPasswordHash);
                        command.Parameters.AddWithValue("@Username", Username);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            return RedirectToPage("Login");
        }
    }
}