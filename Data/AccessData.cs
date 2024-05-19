using FenixLauncher.Models;
using FenixLauncher.Functions;
using MySql.Data.MySqlClient;

namespace FenixLauncher.Data
{
    public class AccessData
    {
        string connection_string = new SysConfiguration().connection_string;

        public bool Login(LoginModel login)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,user,pwd FROM users WHERE user = @user", connection);
                query.Parameters.AddWithValue("@user", login.User);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    int id = Convert.ToInt32(dr["id"]);
                    string user = Convert.ToString(dr["user"]);
                    string pwd = Convert.ToString(dr["pwd"]);

                    try
                    {
                        if (BCrypt.Net.BCrypt.Verify(login.Pwd, pwd))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }
        }
    }
}
