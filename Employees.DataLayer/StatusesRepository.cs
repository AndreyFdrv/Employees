using Employees.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Employees.DataLayer
{
    public class StatusesRepository
    {
        private readonly string ConnectionString;
        public StatusesRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public IEnumerable<Status> GetStatuses()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStatuses";
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Status
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };
                        }
                    }
                }
            }
        }
    }
}