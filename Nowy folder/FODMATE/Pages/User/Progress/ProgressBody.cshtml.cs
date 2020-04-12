using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace FOODMATE
{
    public class ProgressBodyModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        public int userID { get; set; }
        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
        }

        public void OnPostChartGenerator()
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            var MonthList = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
                     
            int StartDay = int.Parse(Request.Form["StartDay"]);
            string StartMonth = Request.Form["StartMonth"].ToString();
            int StartYear = int.Parse(Request.Form["StartYear"]);
            int StartMonthValue = MonthList.IndexOf(StartMonth) + 1;
            string StartDate = StartYear + "-" + StartMonthValue + "-" + StartDay;

            int EndDay = int.Parse(Request.Form["EndDay"]);           
            string EndMonth = Request.Form["EndMonth"].ToString();
            int EndYear = int.Parse(Request.Form["EndYear"]);            
            int EndMonthValue = MonthList.IndexOf(EndMonth) + 1;                          
            string EndDate = EndYear + "-" + EndMonthValue + "-" + EndDay;

            string BodyPart = Request.Form["BodyPart"].ToString();
            IDictionary<string, string> BodyPartQueryList = new Dictionary<string, string>()
            {
                {"Lewa łydka", "l_calve" },
                {"Prawa łydka", "r_calve" },
                {"Lewe udo", "l_thigh" },
                {"Prawe udo", "r_thigh" },
                {"Pośladki", "butt" },
                {"Pas", "waist" },
                {"Klatka piersiowa", "chest" },
                {"Lewe ramię", "l_arm" },
                {"Prawe ramię", "r_arm" },
                {"Lewe przedramię", "l_forearm" },
                {"Prawe przedramię", "r_forearm" },
                {"Waga", "u_weight" }
            };
            string BodyPartQuery = BodyPartQueryList[BodyPart];


            List<string> BodyPartList;
            List<string> DateList;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT x.measurement_id, x.m_date, x.user_id, x.l_calve, x.r_calve, x.l_thigh, x.r_thigh, x.butt, x.waist, x.chest, x.l_arm, x.r_arm, x.l_forearm, x.r_forearm, x.u_weight FROM[FOODMATE].[dbo].[Measurements] x " +
                                "JOIN(SELECT y.m_date FROM [FOODMATE].[dbo].[Measurements] y " +
                                "WHERE y.user_id = @UserID AND y.m_date BETWEEN @StartDate AND @EndDate group by m_date) z " +
                                "ON x.user_id = @UserID AND x.m_date = z.m_date;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@userID", userID);

                    connection.Open();

                    BodyPartList = new List<string>();
                    DateList = new List<string>();

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        BodyPartList.Add(row[BodyPartQuery].ToString());
                        DateList.Add(row["m_date"].ToString().Remove(10));
                    }
                    connection.Close();
                }
            }

            for(int j = 0; j <= BodyPartList.Count - 1; j++)
            {
                if(BodyPartList[j] == ""){

                    BodyPartList[j] = BodyPartList[j - 1];
                    Console.WriteLine(DateList[j]);
                    Console.WriteLine(BodyPartList[j]);
                }
            }

            // create data table to store data
            DataTable ChartData = new DataTable();
            // Add columns to data table
            ChartData.Columns.Add("Czas", typeof(System.String));
            ChartData.Columns.Add("Wielkość", typeof(System.Double));
            // Add rows to data table
            var i = 0;
            foreach (var day in DateList)
            {
                ChartData.Rows.Add(DateList[i], BodyPartList[i]);
                i++;
            }

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);

            Charts.SplineChart splineChart = new Charts.SplineChart("spline_chart");

            splineChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            splineChart.Width.Pixel(700);
            splineChart.Height.Pixel(400);
            splineChart.Data.Source = model;

            splineChart.Caption.Text = BodyPart;
            splineChart.Caption.Bold = true;

            splineChart.Values.Show = true;

            splineChart.XAxis.Text = "Czas";
            splineChart.YAxis.Text = "cm";

            splineChart.Legend.Show = false;
            splineChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ViewData["Chart"] = splineChart.Render();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Login");
        }
    }
}