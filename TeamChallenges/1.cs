using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Http;
using System.Configuration;

namespace VulnerableWebAPI.Controllers
{
    [RoutePrefix("api/secure")]
    public class SecureController : ApiController
    {
        // Secure Database Connection String
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SecureDB"].ConnectionString;

        // Secure endpoint to fetch user data
        [HttpGet]
        [Route("getUserData")]
        public IHttpActionResult GetUserData(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("Invalid user ID.");
            }

            try
            {
                // Use parameterized queries to prevent SQL Injection
                string query = "SELECT * FROM Users WHERE UserId = @UserId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return Ok(new
                        {
                            UserId = reader["UserId"],
                            Name = reader["Name"],
                            Email = reader["Email"]
                        });
                    }

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Improved Error Handling
                return InternalServerError(new Exception("An error occurred while fetching user data."));
            }
        }

        // Secure endpoint to access files
        [HttpGet]
        [Route("getFile")]
        public IHttpActionResult GetFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("Invalid file name.");
            }

            try
            {
                // Validate and sanitize the file name to prevent Insecure Direct Object Reference
                string filePath = Path.Combine("C:\\SecureFiles\\", Path.GetFileName(fileName));

                // Restrict file access to only specific directories
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    return Ok(new { FileName = fileName, Content = content });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while accessing the file."));
            }
        }

        // Secure debug endpoint
        [HttpGet]
        [Route("debug")]
        public IHttpActionResult Debug()
        {
            // Secure Configuration
            return Ok(new
            {
                Environment = "Production",
                MachineName = Environment.MachineName,
                Uptime = TimeSpan.FromMilliseconds(Environment.TickCount).ToString()
            });
        }
    }
}
