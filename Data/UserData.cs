using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using FenixLauncher.Models;
using FenixLauncher.Functions;

namespace FenixLauncher.Data
{
    public class UserData
    {
        string connection_string = new SysConfiguration().connection_string;

        public UserModel GetByID(int id_user)
        {
            var user = new UserModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,user,pwd FROM users u WHERE u.id = @id_user", connection);
                query.Parameters.AddWithValue("@id_user", id_user);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    user = new UserModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        User = Convert.ToString(dr["user"]),
                        Pwd = Convert.ToString(dr["pwd"]),
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

            return user;
        }

        public UserModel GetByName(string username)
        {
            var user = new UserModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,user,pwd FROM users u WHERE u.user = @user", connection);
                query.Parameters.AddWithValue("@user", username);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    user = new UserModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        User = Convert.ToString(dr["user"]),
                        Pwd = Convert.ToString(dr["pwd"])
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

            return user;
        }

        public bool ChangeUsername(ChangeUsernameModel user)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE users SET user = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", user.NewUsername);
                query.Parameters.AddWithValue("@p2", user.Id);

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

        public bool ChangePasssword(ChangePasswordModel user)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE users SET pwd = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", user.NewPassword);
                query.Parameters.AddWithValue("@p2", user.Id);

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
