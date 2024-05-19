using FenixLauncher.Functions;
using FenixLauncher.Models;
using MySql.Data.MySqlClient;

namespace FenixLauncher.Data
{
    public class LogData
    {
        string connection_string = new SysConfiguration().connection_string;

        public List<LogInfoModel> List(string pattern)
        {
            var logs = new List<LogInfoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,details,pid,id_task FROM logs l WHERE l.details LIKE @pattern", connection);
                query.Parameters.AddWithValue("@pattern", "%" + pattern + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    logs.Add(
                        new LogInfoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Details = Convert.ToString(dr["details"]),
                            Pid = Convert.ToInt32(dr["pid"]),
                            Id_task = Convert.ToInt32(dr["id_task"])
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

            return logs;
        }

        public List<LogInfoModel> ListByTask(int id_task)
        {
            var logs = new List<LogInfoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,details,pid,id_task FROM logs l WHERE l.id_task = @id_task", connection);
                query.Parameters.AddWithValue("@id_task", id_task);

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    logs.Add(
                        new LogInfoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Details = Convert.ToString(dr["details"]),
                            Pid = Convert.ToInt32(dr["pid"]),
                            Id_task = Convert.ToInt32(dr["id_task"])
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

            return logs;
        }

        public LogInfoModel Get(int id_log)
        {
            var log = new LogInfoModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,details,pid,id_task FROM logs l WHERE l.id = @id_log", connection);
                query.Parameters.AddWithValue("@id_log", id_log);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    log = new LogInfoModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Details = Convert.ToString(dr["details"]),
                        Pid = Convert.ToInt32(dr["pid"]),
                        Id_task = Convert.ToInt32(dr["id_task"])
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

            return log;
        }

        public bool Add(LogInfoModel log)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("INSERT INTO logs(details,pid,id_task) VALUES(@p1,@p2,@p3)", connection);
                query.Parameters.AddWithValue("@p1", log.Details);
                query.Parameters.AddWithValue("@p2", log.Pid);
                query.Parameters.AddWithValue("@p3", log.Id_task);

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

        public bool Update(LogInfoModel log)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE logs SET details = @p1, pid = @p2, id_task = @p3 WHERE id = @p4", connection);
                query.Parameters.AddWithValue("@p1", log.Details);
                query.Parameters.AddWithValue("@p2", log.Pid);
                query.Parameters.AddWithValue("@p3", log.Id_task);
                query.Parameters.AddWithValue("@p4", log.Id);

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

        public bool Delete(LogInfoModel log)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM logs WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", log.Id);

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

        public bool DeleteByPID(LogInfoModel log)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM logs WHERE pid = @p1", connection);
                query.Parameters.AddWithValue("@p1", log.Pid);

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
