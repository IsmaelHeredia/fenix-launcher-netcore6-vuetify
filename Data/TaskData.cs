using FenixLauncher.Models;
using FenixLauncher.Functions;
using MySql.Data.MySqlClient;

namespace FenixLauncher.Data
{
    public class TaskData
    {
        string connection_string = new SysConfiguration().connection_string;

        public List<TaskInfoModel> ListByProcess(int id_process)
        {
            var tasks = new List<TaskInfoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,name,directory,command,window_style,uac,id_process FROM tasks t WHERE t.id_process = @id_process", connection);
                query.Parameters.AddWithValue("@id_process", id_process);

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    tasks.Add(
                        new TaskInfoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Name = Convert.ToString(dr["name"]),
                            Directory = Convert.ToString(dr["directory"]),
                            Command = Convert.ToString(dr["command"]),
                            WindowStyle = Convert.ToInt32(dr["window_style"]),
                            UAC = Convert.ToInt32(dr["uac"]),
                            IdProcess = Convert.ToInt32(dr["id_process"])
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

            return tasks;
        }

        public List<TaskInfoModel> List(string pattern)
        {
            var tasks = new List<TaskInfoModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,name,directory,command,window_style,uac,id_process FROM tasks t WHERE t.name LIKE @pattern", connection);
                query.Parameters.AddWithValue("@pattern", "%" + pattern + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    tasks.Add(
                        new TaskInfoModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Name = Convert.ToString(dr["name"]),
                            Directory = Convert.ToString(dr["directory"]),
                            Command = Convert.ToString(dr["command"]),
                            WindowStyle = Convert.ToInt32(dr["window_style"]),
                            UAC = Convert.ToInt32(dr["uac"]),
                            IdProcess = Convert.ToInt32(dr["id_process"])
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

            return tasks;
        }

        public TaskInfoModel Get(int id_task)
        {
            var task = new TaskInfoModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,name,directory,command,window_style,uac,id_process FROM tasks t WHERE t.id = @id_task", connection);
                query.Parameters.AddWithValue("@id_task", id_task);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    task = new TaskInfoModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["name"]),
                        Directory = Convert.ToString(dr["directory"]),
                        Command = Convert.ToString(dr["command"]),
                        WindowStyle = Convert.ToInt32(dr["window_style"]),
                        UAC = Convert.ToInt32(dr["uac"]),
                        IdProcess = Convert.ToInt32(dr["id_process"])
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

            return task;
        }

        public bool Add(TaskInfoModel task)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("INSERT INTO tasks(name,directory,command,window_style,uac,id_process) VALUES(@p1,@p2,@p3,@p4,@p5,@p6)", connection);
                query.Parameters.AddWithValue("@p1", task.Name);
                query.Parameters.AddWithValue("@p2", task.Directory);
                query.Parameters.AddWithValue("@p3", task.Command);
                query.Parameters.AddWithValue("@p4", task.WindowStyle);
                query.Parameters.AddWithValue("@p5", task.UAC);
                query.Parameters.AddWithValue("@p6", task.IdProcess);

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

        public bool Update(TaskInfoModel task)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE tasks SET name = @p1, directory = @p2, command = @p3, window_style = @p4, uac = @p5, id_process = @p6 WHERE id = @p7", connection);
                query.Parameters.AddWithValue("@p1", task.Name);
                query.Parameters.AddWithValue("@p2", task.Directory);
                query.Parameters.AddWithValue("@p3", task.Command);
                query.Parameters.AddWithValue("@p4", task.WindowStyle);
                query.Parameters.AddWithValue("@p5", task.UAC);
                query.Parameters.AddWithValue("@p6", task.IdProcess);
                query.Parameters.AddWithValue("@p7", task.Id);

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

        public bool Delete(TaskInfoModel task)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("DELETE FROM tasks WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", task.Id);

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
