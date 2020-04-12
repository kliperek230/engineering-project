using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FOODMATE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FODMATE
{
    public class ResultsWeightModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";
        public int userID { get; set; }
        public int measurementID { get; set; }
        public string measurementDate { get; set; }
        public decimal userWeight { get; set; }

        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [FOODMATE].[dbo].[Measurements] WHERE user_id=@userID AND m_date=(SELECT MAX(m_date) FROM [FOODMATE].[dbo].[Measurements] WHERE user_id=@userID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@userID", userID);

                    List<Measurements> singleUser = new List<Measurements>();

                    connection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            measurementID = Convert.ToInt32((dr["measurement_id"]));
                            measurementDate = (dr["m_date"].ToString());
                            userWeight = Convert.ToDecimal((dr["u_weight"]));
                        }
                    }
                    connection.Close();
                }
            }

        }
        public IActionResult OnPost()
        {

            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            var MonthList = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };

            int Day = int.Parse(Request.Form["Day"]);
            string Month = Request.Form["Month"].ToString();
            int Year = int.Parse(Request.Form["Year"]);
            int MonthValue = MonthList.IndexOf(Month) + 1;
            string Date = Year + "-" + MonthValue + "-" + Day;
            decimal UserWeight = decimal.Parse(Request.Form["UserWeight"]);



            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO [FOODMATE].[dbo].[Measurements] (m_date, user_id, u_weight) VALUES (@Date, @userID, @UserWeight);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@UserWeight", UserWeight);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return RedirectToPage("../HomePage");
                }
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Login");
        }
    }
}