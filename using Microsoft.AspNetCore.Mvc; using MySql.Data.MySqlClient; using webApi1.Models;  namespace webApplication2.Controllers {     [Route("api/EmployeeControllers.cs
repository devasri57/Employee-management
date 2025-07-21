using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webApi1.Models;

namespace webApplication2.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeControllers : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeControllers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult viewEmployee()
        {
            string query = "SELECT * FROM employee";
            var employees = new List<EmployeeModels>();

            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new EmployeeModels
                            {
                                EmployeeId = reader.GetInt32("id"),
                                EmployeeName = reader.GetString("name"),
                                EmployeeSalary = reader.GetInt32("salary"),
                                EmployeeAge = reader.GetInt32("age")
                            });
                        }
                    }
                }
            }

            return Ok(employees);
        }

        [HttpPost]
        public IActionResult createEmployee(EmployeeModels employee)
        {
            string sql = "INSERT INTO employee (id, name, salary, age) VALUES (@id, @name, @salary, @age)";

            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@name", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@salary", employee.EmployeeSalary);
                    cmd.Parameters.AddWithValue("@age", employee.EmployeeAge);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                        return Ok(employee);
                    else
                        return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteEmployee(int id)
        {
            string sql = "DELETE FROM employee WHERE id = @id";

            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                        return Ok();
                    else
                        return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult update(int id, EmployeeModels employee)
        {
            if (id != employee.EmployeeId)
                return BadRequest("ID mismatch");

            string sql = "UPDATE employee SET name = @name, salary = @salary, age = @age WHERE id = @id";

            using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@name", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@salary", employee.EmployeeSalary);
                    cmd.Parameters.AddWithValue("@age", employee.EmployeeAge);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                        return Ok(employee);
                    else
                        return BadRequest();
                }
            }
        }
    }
}
