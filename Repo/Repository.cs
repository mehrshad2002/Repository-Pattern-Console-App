using Entity;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;

namespace Repo
{
    public class Repository
    {
        // Class Which we can implement IUsers and 
        // Connection to data Base
        public class HomeUsers : IUser
        {
            private string cs = @"Data Source=DESKTOP-6E77HUQ;Initial Catalog=Rip;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            public bool CreateUser(User user)
            {
                using(SqlConnection con = new SqlConnection(cs))
                {
                    string queryCreate = "insert into Humen " +
                        $"Values('{user.Name}','{user.Password}')";
                    SqlCommand cmd = new SqlCommand(queryCreate, con);
                    con.Open();
                    try
                    {
                        int Result = cmd.ExecuteNonQuery();
                        if(Result != 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }catch(Exception e)
                    {
                        return false;
                    }
                }
            }

            public bool DeleteByID(int id)
            {
                using(SqlConnection con = new SqlConnection(cs))
                {
                    string queryDelete = "delete from Humen " +
                        $"where ID = {id}";
                    SqlCommand cmd = new SqlCommand(queryDelete, con);
                    con.Open();

                    int i = 0;
                    try
                    {
                        int Result = cmd.ExecuteNonQuery();
                        if (Result != 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            public User GetById(int id)
            {
                User user = new User();
                using(SqlConnection con = new SqlConnection(cs))
                {
                    string queryReadUser = "select * from Humen " +
                        $"where ID = {id}";
                    SqlCommand cmd = new SqlCommand(queryReadUser , con );
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read()){
                        user.Id = rdr.GetInt32("id");
                        user.Name = Convert.ToString(rdr["Name"]);
                        user.Password = Convert.ToString(rdr["Password"]);
                    }

                    return user;
                }
            }

            public List<User> GetUsers()
            {
                List<User> users = new List<User>();
                using(SqlConnection con = new SqlConnection(cs))
                {
                    string queryReadUsers = "select * from Humen";
                    SqlCommand cmd = new SqlCommand(queryReadUsers , con );
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        User user = new User();
                        user.Id = rdr.GetInt32("id");
                        user.Name = Convert.ToString(rdr["Name"]);
                        user.Password = Convert.ToString(rdr["Password"]);

                        users.Add(user);
                    }

                    return users;
                }
            }

            public bool UpdateUser(User user , int oldID)
            {
                User OldUser = new User();
                using(SqlConnection con = new SqlConnection(cs))
                {
                    string queryUpdate = "update Humen " +
                        $"set Name = '{user.Name}' , Password = '{user.Password}'" +
                        $"where ID = {oldID}";

                    SqlCommand cmd = new SqlCommand(queryUpdate , con );
                    con.Open();

                    try
                    {
                        int Result = cmd.ExecuteNonQuery();
                        if (Result != 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
        }
    }
}