using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public interface IUsersGeneric<T> where T : class
    {
        T GetByID(object id);
        bool Insert(T obj);
        bool Delete(T obj);
        bool Update(T obj);

    }
}
