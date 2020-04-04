using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FODMATE
{
    public class DietInsertModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        public void OnGet() { }

        public IActionResult OnPost()
        {

            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            int ProductID = 0;
            string Product = Request.Form["Product"].ToString();
            int Ammount = int.Parse(Request.Form["Ammount"]);
            string DateNow = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string Meal = HttpContext.Session.GetString("meal");

            Console.WriteLine(Product);
            Console.WriteLine(Ammount);
            Console.WriteLine(DateNow);
            Console.WriteLine(Meal);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string querySelectMealID = "SELECT product_id FROM [FOODMATE].[dbo].[Products] " +
                    "WHERE product_name_pl = @Product;";

                using (SqlCommand command = new SqlCommand(querySelectMealID, connection))
                {
                    command.Parameters.AddWithValue("@Product", Product);

                    connection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductID = Convert.ToInt32((dr["product_id"]));
                        }
                    }

                    connection.Close();
                }

                string queryInsertMeal = "INSERT INTO [FOODMATE].[dbo].[Meals] (user_id, m_date, product_id, meal, ammount)" +
                    " VALUES (@userID, @Date, @ProductID, @Meal, @Ammount);";

                using (SqlCommand command = new SqlCommand(queryInsertMeal, connection))
                {
                    command.Parameters.AddWithValue("@Date", DateNow);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@Meal", Meal);
                    command.Parameters.AddWithValue("@Ammount", Ammount);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return RedirectToPage("Diet");
                }
            }

        }

        public IActionResult OnGetLogout()
        {
            return RedirectToPage("../Login");
        }
    }
}