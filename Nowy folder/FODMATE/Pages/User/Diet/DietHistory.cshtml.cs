using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FODMATE
{
    public class DietHistoryModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";
        public int userID { get; set; }
        public string DateNow { get; set; }

        public class Product
        {
            public int MealID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int ProductAmmount { get; set; }
            public int ProductKcal { get; set; }
            public double ProductProtein { get; set; }
            public double ProductCarbs { get; set; }
            public double ProductFats { get; set; }
            public string Meal { get; set; }
        }

        public class ProductMacroSum
        {
            public int Kcal { get; set; }
            public double Protein { get; set; }
            public double Carbs { get; set; }
            public double Fats { get; set; }
        }

        public List<Product> BreakfastProducts { get; set; }
        public List<ProductMacroSum> BreakfastSum { get; set; }
        public List<Product> SecondBreakfastProducts { get; set; }
        public List<ProductMacroSum> SecondBreakfastSum { get; set; }
        public List<ProductMacroSum> DinnerSum { get; set; }
        public List<Product> DinnerProducts { get; set; }
        public List<ProductMacroSum> SupperSum { get; set; }
        public List<Product> SupperProducts { get; set; }


        public void OnGet()
        {
            BreakfastProducts = new List<Product>();
            SecondBreakfastProducts = new List<Product>();
            DinnerProducts = new List<Product>();
            SupperProducts = new List<Product>();

            BreakfastSum = new List<ProductMacroSum>();
            BreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = 0,
                Protein = 0,
                Carbs = 0,
                Fats = 0
            });

            SecondBreakfastSum = new List<ProductMacroSum>();
            SecondBreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = 0,
                Protein = 0,
                Carbs = 0,
                Fats = 0
            });

            DinnerSum = new List<ProductMacroSum>();
            DinnerSum.Add(new ProductMacroSum()
            {
                Kcal = 0,
                Protein = 0,
                Carbs = 0,
                Fats = 0
            });

            SupperSum = new List<ProductMacroSum>();
            SupperSum.Add(new ProductMacroSum()
            {
                Kcal = 0,
                Protein = 0,
                Carbs = 0,
                Fats = 0
            });
        }

        public void OnPost()
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            var MonthList = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };

            int Day = int.Parse(Request.Form["Day"]);
            string Month = Request.Form["Month"].ToString();
            int Year = int.Parse(Request.Form["Year"]);
            int MonthValue = MonthList.IndexOf(Month) + 1;
            string Date = Year + "-" + MonthValue + "-" + Day;

            

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string querySelectMeals = "SELECT x.meal_id, x.product_id, x.meal, x.ammount, y.kcal, y.protein, y.carbs, y.fats, y.product_name_pl " +
                                            "FROM(SELECT * FROM[FOODMATE].[dbo].[Meals] x WHERE user_id = @userID and m_date = @Date) x " +
                                            "JOIN[FOODMATE].[dbo].[Products] y " +
                                            "ON x.product_id = y.product_id;";

                using (SqlCommand command = new SqlCommand(querySelectMeals, connection))
                {
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@userID", userID);

                    connection.Open();

                    BreakfastProducts = new List<Product>();
                    SecondBreakfastProducts = new List<Product>();
                    DinnerProducts = new List<Product>();
                    SupperProducts = new List<Product>();

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["meal"].ToString() == "BREAKFAST")
                        {
                            BreakfastProducts.Add(new Product()
                            {
                                MealID = Convert.ToInt32(row["meal_id"]),
                                ProductID = Convert.ToInt32(row["product_id"]),
                                ProductName = row["product_name_pl"].ToString(),
                                ProductAmmount = Convert.ToInt32(row["ammount"]),
                                ProductKcal = Convert.ToInt32(row["kcal"]),
                                ProductProtein = Convert.ToDouble(row["protein"]),
                                ProductCarbs = Convert.ToDouble(row["carbs"]),
                                ProductFats = Convert.ToDouble(row["fats"]),
                                Meal = row["meal"].ToString()
                            });
                        }
                        else if (row["meal"].ToString() == "SECOND BREAKFAST")
                        {
                            SecondBreakfastProducts.Add(new Product()
                            {
                                MealID = Convert.ToInt32(row["meal_id"]),
                                ProductID = Convert.ToInt32(row["product_id"]),
                                ProductName = row["product_name_pl"].ToString(),
                                ProductAmmount = Convert.ToInt32(row["ammount"]),
                                ProductKcal = Convert.ToInt32(row["kcal"]),
                                ProductProtein = Convert.ToDouble(row["protein"]),
                                ProductCarbs = Convert.ToDouble(row["carbs"]),
                                ProductFats = Convert.ToDouble(row["fats"]),
                                Meal = row["meal"].ToString()
                            });
                        }
                        else if (row["meal"].ToString() == "DINNER")
                        {
                            DinnerProducts.Add(new Product()
                            {
                                MealID = Convert.ToInt32(row["meal_id"]),
                                ProductID = Convert.ToInt32(row["product_id"]),
                                ProductName = row["product_name_pl"].ToString(),
                                ProductAmmount = Convert.ToInt32(row["ammount"]),
                                ProductKcal = Convert.ToInt32(row["kcal"]),
                                ProductProtein = Convert.ToDouble(row["protein"]),
                                ProductCarbs = Convert.ToDouble(row["carbs"]),
                                ProductFats = Convert.ToDouble(row["fats"]),
                                Meal = row["meal"].ToString()
                            });
                        }
                        else if (row["meal"].ToString() == "SUPPER")
                        {
                            SupperProducts.Add(new Product()
                            {
                                MealID = Convert.ToInt32(row["meal_id"]),
                                ProductID = Convert.ToInt32(row["product_id"]),
                                ProductName = row["product_name_pl"].ToString(),
                                ProductAmmount = Convert.ToInt32(row["ammount"]),
                                ProductKcal = Convert.ToInt32(row["kcal"]),
                                ProductProtein = Convert.ToDouble(row["protein"]),
                                ProductCarbs = Convert.ToDouble(row["carbs"]),
                                ProductFats = Convert.ToDouble(row["fats"]),
                                Meal = row["meal"].ToString()
                            });
                        }
                    }
                    connection.Close();
                }
            }

            BreakfastSum = new List<ProductMacroSum>();
            BreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = BreakfastProducts.Sum(item => item.ProductKcal),
                Protein = BreakfastProducts.Sum(item => item.ProductProtein),
                Carbs = BreakfastProducts.Sum(item => item.ProductCarbs),
                Fats = BreakfastProducts.Sum(item => item.ProductFats),
            });

            SecondBreakfastSum = new List<ProductMacroSum>();
            SecondBreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = SecondBreakfastProducts.Sum(item => item.ProductKcal),
                Protein = SecondBreakfastProducts.Sum(item => item.ProductProtein),
                Carbs = SecondBreakfastProducts.Sum(item => item.ProductCarbs),
                Fats = SecondBreakfastProducts.Sum(item => item.ProductFats),
            });

            DinnerSum = new List<ProductMacroSum>();
            DinnerSum.Add(new ProductMacroSum()
            {
                Kcal = DinnerProducts.Sum(item => item.ProductKcal),
                Protein = DinnerProducts.Sum(item => item.ProductProtein),
                Carbs = DinnerProducts.Sum(item => item.ProductCarbs),
                Fats = DinnerProducts.Sum(item => item.ProductFats),
            });

            SupperSum = new List<ProductMacroSum>();
            SupperSum.Add(new ProductMacroSum()
            {
                Kcal = SupperProducts.Sum(item => item.ProductKcal),
                Protein = SupperProducts.Sum(item => item.ProductProtein),
                Carbs = SupperProducts.Sum(item => item.ProductCarbs),
                Fats = SupperProducts.Sum(item => item.ProductFats),
            });
        }

        public IActionResult OnGetLogout()
        {
            return RedirectToPage("../Login");
        }
    }
}