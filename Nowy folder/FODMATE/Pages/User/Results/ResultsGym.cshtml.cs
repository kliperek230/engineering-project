using System;
using System.Collections.Generic;
using FOODMATE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;


namespace FOODMATE
{
    public class ResultsGymModel : PageModel
    {
        // KONIECZNIE TRZEBA USUNĄĆ STĄD TAK JAWNY CONNECTION STRING!!!!!!!!!!!!!!!!!!!
        private string _connectionString = "Server=KACPER;Database=FOODMATE;Trusted_Connection=True;";

        //public int liftID { get; set; }
        public int userID { get; set; }
        public string measurementDate { get; set; }
        public string liftName { get; set; }
        public int value { get; set; }
        public int benchValue { get; set; }
        public int benchID { get; set; }
        public int ohpValue { get; set; }
        public int ohpID { get; set; }
        public int squatValue { get; set; }
        public int squatID { get; set; }
        public int deadliftValue { get; set; }
        public int deadliftID { get; set; }

        public void OnGet()
        {
            userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            List<int> liftIdList;
            List<string> liftNameList;
            List<string> liftDateList;
            List<int> liftValueList;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT x.lift_id, x.lift_name, x.m_date, x.value FROM[FOODMATE].[dbo].[Lifts] x " +
                    "JOIN (SELECT y.lift_name, MAX(y.m_date) AS max_date FROM[FOODMATE].[dbo].[Lifts] y " +
                    "WHERE y.user_id = @userID GROUP BY y.lift_name) z ON z.lift_name = x.lift_name AND z.max_date = x.m_date";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    connection.Open();

                    liftIdList = new List<int>();
                    liftNameList = new List<string>();
                    liftDateList = new List<string>();
                    liftValueList = new List<int>();

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        liftIdList.Add(Convert.ToInt32(row["lift_id"].ToString()));
                        liftNameList.Add(row["lift_name"].ToString());
                        liftDateList.Add(row["m_date"].ToString());
                        liftValueList.Add(Convert.ToInt32(row["value"].ToString()));
                    }
                    connection.Close();
                }
            }
            benchID = liftIdList[0];
            benchValue = liftValueList[0];

            deadliftID = liftIdList[1];
            deadliftValue = liftValueList[1];

            ohpID = liftIdList[2];
            ohpValue = liftValueList[2];

            squatID = liftIdList[3];
            squatValue = liftValueList[3];
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

            string Lift = Request.Form["Lift"].ToString();
            IDictionary<string, string> LiftQueryList = new Dictionary<string, string>()
            {
                {"Wyciskanie leżąc", "BENCH" },
                {"Wyciskanie stojąc", "OHP" },
                {"Przysiad", "SQUAT" },
                {"Martwy ciąg", "DEADLIFT" },
            };
            string LiftQuery = LiftQueryList[Lift];

            int Value = int.Parse(Request.Form["Value"]);
            int LiftID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string queryLiftInsert = "INSERT INTO [FOODMATE].[dbo].[Lifts] (user_id, m_date, lift_name, value)" +
                                " VALUES (@userID, @liftDate, @liftName, @liftValue);";

                using (SqlCommand command = new SqlCommand(queryLiftInsert, connection))
                {

                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@liftDate", Date);
                    command.Parameters.AddWithValue("@liftName", LiftQuery);
                    command.Parameters.AddWithValue("@liftValue", Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                string queryLiftGet = "SELECT max(lift_id) as lift_id FROM [FOODMATE].[dbo].[Lifts] WHERE user_id = @userID;";

                using (SqlCommand command = new SqlCommand(queryLiftGet, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LiftID = Convert.ToInt32((dr["lift_id"]));
                        }
                    }
                    connection.Close();
                }

                //SPRAWDZIĆ CZY DA SIĘ WSTAWIĆ POPRAWNIE TO @LIFTNAME BO TO JEST BRZYDKIE JAK CHOLERA
                string one = "UPDATE [FOODMATE].[dbo].[User] SET ";
                string two = LiftQuery;
                string three = " = @liftID WHERE user_id = @userID;";
                //string queryUserUpdate = "UPDATE [FOODMATE].[dbo].[User] SET @liftName = @liftID WHERE user_id = @userID;";
                string queryUserUpdate = one + two + three;

                using (SqlCommand command = new SqlCommand(queryUserUpdate, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@liftName", LiftQuery);
                    command.Parameters.AddWithValue("@liftID", LiftID);

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