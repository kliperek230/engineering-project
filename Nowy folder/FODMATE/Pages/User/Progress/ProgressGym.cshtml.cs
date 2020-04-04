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

namespace FODMATE
{
    public class ProgressGymModel : PageModel
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

            string Lift = Request.Form["Lift"].ToString();
            IDictionary<string, string> LiftQueryList = new Dictionary<string, string>()
            {
                {"Wyciskanie leżąc", "BENCH" },
                {"Wyciskanie stojąc", "OHP" },
                {"Przysiad", "SQUAT" },
                {"Martwy ciąg", "DEADLIFT" },
            };

            string LiftQuery = LiftQueryList[Lift];


            List<string> LiftList;
            List<string> DateList;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT value, m_date FROM [FOODMATE].[dbo].[Lifts] WHERE user_id = @userID AND lift_name = @liftName AND m_date BETWEEN @StartDate AND @EndDate;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@liftName", LiftQuery);

                    connection.Open();

                    LiftList = new List<string>();
                    DateList = new List<string>();

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        LiftList.Add(row["value"].ToString());
                        DateList.Add(row["m_date"].ToString());
                    }
                    connection.Close();
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
                ChartData.Rows.Add(DateList[i], LiftList[i]);
                i++;
            }

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);

            Charts.LineChart lineChart = new Charts.LineChart("line_chart_db");

            lineChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            lineChart.Width.Pixel(700);
            lineChart.Height.Pixel(400);
            lineChart.Data.Source = model;

            lineChart.Caption.Text = Lift;
            lineChart.Caption.Bold = true;

            lineChart.XAxis.Text = "Czas";
            lineChart.YAxis.Text = "kg";

            lineChart.Legend.Show = false;
            lineChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ViewData["Chart"] = lineChart.Render();
        }
        public IActionResult OnGetLogout()
        {
            return RedirectToPage("Login");
        }
    }
}