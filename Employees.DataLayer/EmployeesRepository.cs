using Employees.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Employees.DataLayer
{
    public class EmployeesRepository
    {
        private readonly string ConnectionString;
        public EmployeesRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetEmployees";
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Employee
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                SecondName = reader.GetString(reader.GetOrdinal("second_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                EmployDate = reader.GetDateTime(reader.GetOrdinal("date_employ")),
                                UneployDate = reader.GetDateTime(reader.GetOrdinal("date_uneploy")),
                                Status = reader.GetString(reader.GetOrdinal("status")),
                                Department = reader.GetString(reader.GetOrdinal("department")),
                                Post = reader.GetString(reader.GetOrdinal("post"))
                            };
                        }
                    }
                }
            }
        }
    }
}