using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Repo
{

    // Our Genetic Interface 
    public interface IGenericRepository<T>
    {
        bool UpdateUser(T entity, int oldID);
        bool CreateUser(T entity);
        bool DeleteByID(int id);
        List<T> GetUsers();
        T GetById(int id);
    }


    // Ef 
    public class EfGenericRepository<T> : IGenericRepository<T>
    {
        public bool CreateUser(T entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(T entity, int oldID)
        {
            throw new NotImplementedException();
        }
    }


    // Ado
    // we send generic data to generic class 
    public class AdoGenericRepository<T> : IGenericRepository<T>
    {
        private readonly string cs = ConfigurationManager.AppSettings["connectionString"];
        // add generic user method
        public bool CreateUser(T entity)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                // important place is here [ our query ] and
                // model mapping is importent place for us 
                string queryCreate = ModelMapping<T>.GetInsertQuery(entity);
                SqlCommand cmd = new SqlCommand(queryCreate, con);
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

        public bool DeleteByID(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
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

        public T GetById(int id)
        {
             
            using (SqlConnection con = new SqlConnection(cs))
            {
                string queryReadUser = "select * from Humen " + $"where ID = {id}";
                SqlCommand cmd = new SqlCommand(queryReadUser, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();


                return ModelMapping<T>.MapEntity(rdr);

            }
        }

        public List<T> GetUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string queryReadUsers = "select * from Humen";
                SqlCommand cmd = new SqlCommand(queryReadUsers, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                return ModelMapping<T>.MapListEntity(rdr);
            }
        }

        public bool UpdateUser(T entity, int oldID)
        {
             
            using (SqlConnection con = new SqlConnection(cs))
            {
                //string queryUpdate = "update Humen " +
                //    $"set Name = '{user.Name}' , Password = '{user.Password}'" +
                //    $"where ID = {oldID}";

                string queryUpdate = ModelMapping<T>.GetUpdateQuery(entity);

                SqlCommand cmd = new SqlCommand(queryUpdate, con);
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


    // Mapping 
    public class ModelMapping<T>
    {
        public static string GetInsertQuery(T entity )
        {
            return "";
        }

        public static string GetUpdateQuery(T entity)
        {
            return "";
        }

        public static T MapEntity(SqlDataReader rdr)
        {
            return DataReaderMapToObject<T>(rdr);
        }

        public static List<T> MapListEntity(SqlDataReader rdr)
        {
            return DataReaderMapToList<T>(rdr);
        }

        // here is another important things and this method try
        // to read all data from any enthity 
        // IDataReader is Builtin Interface
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>(); // create generic list
            T obj = default(T); // ** create generic objects
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>(); // create object 
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static T DataReaderMapToObject<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                 
            }
            return obj;
        }

        public virtual List<T> DataReaderMapToList2(SqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                }
                results.Add(item);
            }
            return results;
        }
    }

}
