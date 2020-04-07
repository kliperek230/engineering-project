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



namespace FOODMATE
{
    public class ResultsBodyModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        public int userID { get; set; }
        public int measurementID { get; set; }
        public string measurementDate { get; set; }
        public int leftCalve { get; set; }
        public int rightCalve { get; set; }
        public int leftThigh { get; set; }
        public int rightThigh { get; set; }
        public int butt { get; set; }
        public int waist { get; set; }
        public int chest { get; set; }
        public int leftArm { get; set; }
        public int rightArm { get; set; }
        public int leftForearm { get; set; }
        public int rightForearm { get; set; }
        public int userWeight { get; set; }

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
                            leftCalve = Convert.ToInt32((dr["l_calve"]));
                            rightCalve = Convert.ToInt32((dr["r_calve"]));
                            leftThigh = Convert.ToInt32((dr["l_thigh"]));
                            rightThigh = Convert.ToInt32((dr["r_thigh"]));
                            butt = Convert.ToInt32((dr["butt"]));
                            waist = Convert.ToInt32((dr["waist"]));
                            chest = Convert.ToInt32((dr["chest"]));
                            leftArm = Convert.ToInt32((dr["l_arm"]));
                            rightArm = Convert.ToInt32((dr["r_arm"]));
                            leftForearm = Convert.ToInt32((dr["l_forearm"]));
                            rightForearm = Convert.ToInt32((dr["r_forearm"]));
                            userWeight = Convert.ToInt32((dr["u_weight"]));
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

            int LeftCalve = int.Parse(Request.Form["LeftCalve"]);
            int RightCalve = int.Parse(Request.Form["RightCalve"]);
            int LeftThigh = int.Parse(Request.Form["LeftThigh"]);
            int RightThigh = int.Parse(Request.Form["RightThigh"]);
            int Butt = int.Parse(Request.Form["Butt"]);
            int Waist = int.Parse(Request.Form["Waist"]);
            int Chest = int.Parse(Request.Form["Chest"]);
            int LeftArm = int.Parse(Request.Form["LeftArm"]);
            int RightArm = int.Parse(Request.Form["RightArm"]);
            int LeftForearm = int.Parse(Request.Form["LeftForearm"]);
            int RightForearm = int.Parse(Request.Form["RightForearm"]);
            int UserWeight = int.Parse(Request.Form["UserWeight"]);



            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO [FOODMATE].[dbo].[Measurements] (m_date, user_id, l_calve, r_calve, l_thigh, r_thigh, butt, waist, chest, " +
                    "l_arm, r_arm, l_forearm, r_forearm, u_weight) VALUES (@Date, @userID, @LeftCalve, @RightCalve, @LeftThigh, @RightThigh, @Butt, @Waist, @Chest, " +
                    "@LeftArm, @RightArm, @LeftForearm, @RightForearm, @UserWeight);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", Date);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@LeftCalve", LeftCalve);
                    command.Parameters.AddWithValue("@RightCalve", RightCalve);
                    command.Parameters.AddWithValue("@LeftThigh", LeftThigh);
                    command.Parameters.AddWithValue("@RightThigh", RightThigh);
                    command.Parameters.AddWithValue("@Butt", Butt);
                    command.Parameters.AddWithValue("@Waist", Waist);
                    command.Parameters.AddWithValue("@Chest", Chest);
                    command.Parameters.AddWithValue("@LeftArm", LeftArm);
                    command.Parameters.AddWithValue("@RightArm", RightArm);
                    command.Parameters.AddWithValue("@LeftForearm", LeftForearm);
                    command.Parameters.AddWithValue("@RightForearm", RightForearm);
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
            return RedirectToPage("../Login");
        }
    }
}