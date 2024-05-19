using FenixLauncher.Functions;
using FenixLauncher.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace FenixLauncher.Data
{
    public class ProcessData
    {
        string connection_string = new SysConfiguration().connection_string;

        public List<ProcessInfoModel> List(string pattern)
        {
            var processes = new List<ProcessInfoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,name,url,timeout_url,updated_at FROM processes p WHERE p.name LIKE @pattern ORDER BY updated_at DESC", connection);
                query.Parameters.AddWithValue("@pattern", "%" + pattern + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    processes.Add(
                        new ProcessInfoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Name = Convert.ToString(dr["name"]),
                            URL = Convert.ToString(dr["url"]),
                            Timeout_URL = Convert.ToInt32(dr["timeout_url"]),
                            Updated_at = Convert.ToString(dr["updated_at"])
                        }
                    );
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return processes;
        }

        public ProcessInfoModel Get(int id_process)
        {
            var process = new ProcessInfoModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,name,url,timeout_url,updated_at FROM processes p WHERE p.id = @id_process", connection);
                query.Parameters.AddWithValue("@id_process", id_process);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    process = new ProcessInfoModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["name"]),
                        URL = Convert.ToString(dr["url"]),
                        Timeout_URL = Convert.ToInt32(dr["timeout_url"]),
                        Updated_at = Convert.ToString(dr["updated_at"])
                    };
                }
                else
                {
                    return null;
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return process;
        }

        public bool Add(ProcessInfoModel process)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                DateTime datetime_string = DateTime.Now;
                string datetime_format = datetime_string.ToString("yyyy-MM-dd HH:mm:ss");

                var query = new MySqlCommand("INSERT INTO processes(name,url,timeout_url,updated_at) VALUES(@p1,@p2,@p3,@p4)", connection);
                query.Parameters.AddWithValue("@p1", process.Name);
                query.Parameters.AddWithValue("@p2", process.URL);
                query.Parameters.AddWithValue("@p3", process.Timeout_URL);
                query.Parameters.AddWithValue("@p4", datetime_format);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool Update(ProcessInfoModel process)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                DateTime datetime_string = DateTime.Now;
                string datetime_format = datetime_string.ToString("yyyy-MM-dd HH:mm:ss");

                var query = new MySqlCommand("UPDATE processes SET name = @p1, url = @p2, timeout_url = @p3, updated_at = @p4 WHERE id = @p5", connection);
                query.Parameters.AddWithValue("@p1", process.Name);
                query.Parameters.AddWithValue("@p2", process.URL);
                query.Parameters.AddWithValue("@p3", process.Timeout_URL);
                query.Parameters.AddWithValue("@p4", datetime_format);
                query.Parameters.AddWithValue("@p5", process.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(ProcessInfoModel process)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM processes WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", process.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
