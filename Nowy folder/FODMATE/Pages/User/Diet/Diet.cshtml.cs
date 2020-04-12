using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FOODMATE
{
    public class DietModel : PageModel
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
            public decimal ProductProtein { get; set; }
            public decimal ProductCarbs { get; set; }
            public decimal ProductFats { get; set; }
            public string Meal { get; set; }
            public int ProductKcalPerPortion { get; set; }
            public decimal ProductProteinPerPortion { get; set; }
            public decimal ProductCarbsPerPortion { get; set; }
            public decimal ProductFatsPerPortion { get; set; }
        }

        public class ProductMacroSum
        {
            public int Kcal { get; set; }
            public decimal Protein { get; set; }
            public decimal Carbs { get; set; }
            public decimal Fats { get; set; }
        }

        public class DailyMacroIntake
        {
            public int Kcal_proc { get; set; }
            public int Protein_proc { get; set; }
            public int Carbs_proc { get; set; }
            public int Fats_proc { get; set; }
        }

        public class MacroProcent
        {
            public string Kcal_type { get; set; }
            public string Protein_type { get; set; }
            public string Carbs_type { get; set; }
            public string Fats_type { get; set; }
        }

        public List<ProductMacroSum> DailyRequirements { get; set; }
        public List<ProductMacroSum> DailySum { get; set; }
        public List<Product> BreakfastProducts { get; set; }
        public List<ProductMacroSum> BreakfastSum { get; set; }
        public List<Product> SecondBreakfastProducts { get; set; }
        public List<ProductMacroSum> SecondBreakfastSum { get; set; }
        public List<ProductMacroSum> DinnerSum { get; set; }
        public List<Product> DinnerProducts { get; set; }
        public List<ProductMacroSum> SupperSum { get; set; }
        public List<Product> SupperProducts { get; set; }
        public List<DailyMacroIntake> DailyMacroProc { get; set; }
        public List<MacroProcent> ProgressBarType { get; set; }

        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            DateNow = DateTime.UtcNow.ToString("yyyy-MM-dd");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string querySelectMeals = "SELECT x.meal_id, x.product_id, x.meal, x.ammount, y.kcal, y.protein, y.carbs, y.fats, y.product_name_pl " +
                                            "FROM(SELECT * FROM[FOODMATE].[dbo].[Meals] x WHERE user_id = @userID and m_date = @Date) x " +
                                            "JOIN[FOODMATE].[dbo].[Products] y " +
                                            "ON x.product_id = y.product_id;";

                using (SqlCommand command = new SqlCommand(querySelectMeals, connection))
                {
                    command.Parameters.AddWithValue("@Date", DateNow);
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
                                ProductProtein = Convert.ToDecimal(row["protein"]),
                                ProductCarbs = Convert.ToDecimal(row["carbs"]),
                                ProductFats = Convert.ToDecimal(row["fats"]),
                                Meal = row["meal"].ToString(),
                                ProductKcalPerPortion = ((Convert.ToInt32(row["kcal"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductProteinPerPortion = ((Convert.ToDecimal(row["protein"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductCarbsPerPortion = ((Convert.ToDecimal(row["carbs"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductFatsPerPortion = ((Convert.ToDecimal(row["fats"]) * Convert.ToInt32(row["ammount"])) / 100)
                            });
                        } else if (row["meal"].ToString() == "SECOND BREAKFAST")
                        {
                            SecondBreakfastProducts.Add(new Product()
                            {
                                MealID = Convert.ToInt32(row["meal_id"]),
                                ProductID = Convert.ToInt32(row["product_id"]),
                                ProductName = row["product_name_pl"].ToString(),
                                ProductAmmount = Convert.ToInt32(row["ammount"]),
                                ProductKcal = Convert.ToInt32(row["kcal"]),
                                ProductProtein = Convert.ToDecimal(row["protein"]),
                                ProductCarbs = Convert.ToDecimal(row["carbs"]),
                                ProductFats = Convert.ToDecimal(row["fats"]),
                                Meal = row["meal"].ToString(),
                                ProductKcalPerPortion = ((Convert.ToInt32(row["kcal"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductProteinPerPortion = ((Convert.ToDecimal(row["protein"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductCarbsPerPortion = ((Convert.ToDecimal(row["carbs"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductFatsPerPortion = ((Convert.ToDecimal(row["fats"]) * Convert.ToInt32(row["ammount"])) / 100)
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
                                ProductProtein = Convert.ToDecimal(row["protein"]),
                                ProductCarbs = Convert.ToDecimal(row["carbs"]),
                                ProductFats = Convert.ToDecimal(row["fats"]),
                                Meal = row["meal"].ToString(),
                                ProductKcalPerPortion = ((Convert.ToInt32(row["kcal"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductProteinPerPortion = ((Convert.ToDecimal(row["protein"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductCarbsPerPortion = ((Convert.ToDecimal(row["carbs"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductFatsPerPortion = ((Convert.ToDecimal(row["fats"]) * Convert.ToInt32(row["ammount"])) / 100)
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
                                ProductProtein = Convert.ToDecimal(row["protein"]),
                                ProductCarbs = Convert.ToDecimal(row["carbs"]),
                                ProductFats = Convert.ToDecimal(row["fats"]),
                                Meal = row["meal"].ToString(),
                                ProductKcalPerPortion = ((Convert.ToInt32(row["kcal"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductProteinPerPortion = ((Convert.ToDecimal(row["protein"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductCarbsPerPortion = ((Convert.ToDecimal(row["carbs"]) * Convert.ToInt32(row["ammount"])) / 100),
                                ProductFatsPerPortion = ((Convert.ToDecimal(row["fats"]) * Convert.ToInt32(row["ammount"])) / 100)
                            });
                        }
                    }
                    connection.Close();
                }

                string querySelectDailyMacro = "SELECT kcal, protein, carbs, fats FROM [FOODMATE].[dbo].[User] WHERE user_id = @UserID";

                using (SqlCommand command = new SqlCommand(querySelectDailyMacro, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    DailyRequirements = new List<ProductMacroSum>();

                    connection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DailyRequirements.Add(new ProductMacroSum()
                            {
                                Kcal = Convert.ToInt32((dr["kcal"])),
                                Protein = Convert.ToInt32((dr["protein"])),
                                Carbs = Convert.ToInt32((dr["carbs"])),
                                Fats = Convert.ToInt32((dr["fats"]))
                            });
                        }
                    }
                    connection.Close();
                }
            }

            BreakfastSum = new List<ProductMacroSum>();
            BreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = BreakfastProducts.Sum(item => item.ProductKcalPerPortion),
                Protein = BreakfastProducts.Sum(item => item.ProductProtein),
                Carbs = BreakfastProducts.Sum(item => item.ProductCarbs),
                Fats = BreakfastProducts.Sum(item => item.ProductFats),
            });

            SecondBreakfastSum = new List<ProductMacroSum>();
            SecondBreakfastSum.Add(new ProductMacroSum()
            {
                Kcal = SecondBreakfastProducts.Sum(item => item.ProductKcalPerPortion),
                Protein = SecondBreakfastProducts.Sum(item => item.ProductProteinPerPortion),
                Carbs = SecondBreakfastProducts.Sum(item => item.ProductCarbsPerPortion),
                Fats = SecondBreakfastProducts.Sum(item => item.ProductFatsPerPortion),
            });

            DinnerSum = new List<ProductMacroSum>();
            DinnerSum.Add(new ProductMacroSum()
            {
                Kcal = DinnerProducts.Sum(item => item.ProductKcalPerPortion),
                Protein = DinnerProducts.Sum(item => item.ProductProteinPerPortion),
                Carbs = DinnerProducts.Sum(item => item.ProductCarbsPerPortion),
                Fats = DinnerProducts.Sum(item => item.ProductFatsPerPortion),
            });

            SupperSum = new List<ProductMacroSum>();
            SupperSum.Add(new ProductMacroSum()
            {
                Kcal = SupperProducts.Sum(item => item.ProductKcalPerPortion),
                Protein = SupperProducts.Sum(item => item.ProductProteinPerPortion),
                Carbs = SupperProducts.Sum(item => item.ProductCarbsPerPortion),
                Fats = SupperProducts.Sum(item => item.ProductFatsPerPortion),
            });

            DailySum = new List<ProductMacroSum>();
            DailySum.Add(new ProductMacroSum()
            {
                Kcal = BreakfastProducts.Sum(item => item.ProductKcalPerPortion) + SecondBreakfastProducts.Sum(item => item.ProductKcalPerPortion) + DinnerProducts.Sum(item => item.ProductKcalPerPortion) + SupperProducts.Sum(item => item.ProductKcalPerPortion),
                Protein = BreakfastProducts.Sum(item => item.ProductProteinPerPortion) + SecondBreakfastProducts.Sum(item => item.ProductProteinPerPortion) + DinnerProducts.Sum(item => item.ProductProteinPerPortion) + SupperProducts.Sum(item => item.ProductProteinPerPortion),
                Carbs = BreakfastProducts.Sum(item => item.ProductCarbsPerPortion) + SecondBreakfastProducts.Sum(item => item.ProductCarbsPerPortion) + DinnerProducts.Sum(item => item.ProductCarbsPerPortion) + SupperProducts.Sum(item => item.ProductCarbsPerPortion),
                Fats = BreakfastProducts.Sum(item => item.ProductFatsPerPortion) + SecondBreakfastProducts.Sum(item => item.ProductFatsPerPortion) + DinnerProducts.Sum(item => item.ProductFatsPerPortion) + SupperProducts.Sum(item => item.ProductFatsPerPortion),
            });

            var ProgressBarList = new List<string>() { "bg-danger", "bg-warning", "bg-info", "bg-success" };

            foreach (var req in DailyRequirements)
            {
                foreach(var daily in DailySum)
                {
                    DailyMacroProc = new List<DailyMacroIntake>();
                    DailyMacroProc.Add(new DailyMacroIntake()
                    {
                        Kcal_proc = (daily.Kcal * 100 / req.Kcal),
                        Protein_proc = (Convert.ToInt32(daily.Protein) * 100 / Convert.ToInt32(req.Protein)),
                        Carbs_proc = (Convert.ToInt32(daily.Carbs) * 100 / Convert.ToInt32(req.Carbs)),
                        Fats_proc = (Convert.ToInt32(daily.Fats) * 100 / Convert.ToInt32(req.Fats)),
                    });
                }
            }

            foreach(var proc in DailyMacroProc)
            {
                string kcal_type = "";
                string protein_type = "";
                string carbs_type = "";
                string fats_type = "";

                if(proc.Kcal_proc <= 25)
                {
                    kcal_type = ProgressBarList[0];
                }
                else if(proc.Kcal_proc <= 50)
                {
                    kcal_type = ProgressBarList[1];
                }
                else if (proc.Kcal_proc <= 75)
                {
                    kcal_type = ProgressBarList[2];
                }
                else
                {
                    kcal_type = ProgressBarList[3];
                }

                if (proc.Protein_proc <= 25)
                {
                    protein_type = ProgressBarList[0];
                }
                else if (proc.Protein_proc <= 50)
                {
                    protein_type = ProgressBarList[1];
                }
                else if (proc.Protein_proc <= 75)
                {
                    protein_type = ProgressBarList[2];
                }
                else
                {
                    protein_type = ProgressBarList[3];
                }

                if (proc.Carbs_proc <= 25)
                {
                    carbs_type = ProgressBarList[0];
                }
                else if (proc.Carbs_proc <= 50)
                {
                    carbs_type = ProgressBarList[1];
                }
                else if (proc.Carbs_proc <= 75)
                {
                    carbs_type = ProgressBarList[2];
                }
                else
                {
                    carbs_type = ProgressBarList[3];
                }

                if (proc.Fats_proc <= 25)
                {
                    fats_type = ProgressBarList[0];
                }
                else if (proc.Fats_proc <= 50)
                {
                    fats_type = ProgressBarList[1];
                }
                else if (proc.Fats_proc <= 75)
                {
                    fats_type = ProgressBarList[2];
                }
                else
                {
                    fats_type = ProgressBarList[3];
                }


                ProgressBarType = new List<MacroProcent>();
                ProgressBarType.Add(new MacroProcent()
                {
                    Kcal_type = kcal_type,
                    Protein_type = protein_type,
                    Carbs_type = carbs_type,
                    Fats_type = fats_type
                });
            }
        }

        public IActionResult OnPost(string BreakfastButton, string SecondBreakfastButton, string DinnerButton, string SupperButton, string Delete)
        {
            if (!string.IsNullOrEmpty(BreakfastButton))
            {
                HttpContext.Session.SetString("meal", "BREAKFAST");
                return RedirectToPage("DietInsert");
            }
            if (!string.IsNullOrEmpty(SecondBreakfastButton))
            {
                HttpContext.Session.SetString("meal", "SECOND BREAKFAST");
                return RedirectToPage("DietInsert");
            }
            if (!string.IsNullOrEmpty(DinnerButton))
            {
                HttpContext.Session.SetString("meal", "DINNER");
                return RedirectToPage("DietInsert");
            }
            if (!string.IsNullOrEmpty(SupperButton))
            {
                HttpContext.Session.SetString("meal", "SUPPER");
                return RedirectToPage("DietInsert");
            }
            return null;
        }


        public IActionResult OnGetDelete(int? id)
        {
            Console.WriteLine(id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string querySelectMeals = "DELETE FROM [FOODMATE].[dbo].[Meals] WHERE meal_id = @MealID;";

                using (SqlCommand command = new SqlCommand(querySelectMeals, connection))
                {
                    command.Parameters.AddWithValue("@MealID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return RedirectToPage("Diet");
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