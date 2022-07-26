using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Repo
{
    public interface IUser
    {
        // We Have All Command --> CRUD 
        bool UpdateUser(User user , int oldID);
        bool CreateUser(User user);
        bool DeleteByID(int id);
        List<User> GetUsers();
        User GetById(int id);
    }
}
