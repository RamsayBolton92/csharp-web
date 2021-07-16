namespace BeautyAndThePet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;

    using BeautyAndThePet.Web.ViewModels.WebService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;

    public class WebServiceController : Controller
    {
        private static readonly string ConnectionString = "Server=.;Database=BeautyAndThePet;Trusted_Connection=True;MultipleActiveResultSets=true";

        [HttpGet]
        public string GetUsers()
        {

            List<UserViewModel> users = new List<UserViewModel>();
            string query = string.Format("SELECT * FROM AspNetUsers");

            SqlConnection connection = new SqlConnection(ConnectionString);
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(
                            new UserViewModel
                            {
                                Id = reader["Id"].ToString(),
                                Username = reader["UserName"].ToString(),
                            });
                    }
                }

                var jsonString = JsonSerializer.Serialize(users);
                return jsonString;
            }
        }
    }
}
