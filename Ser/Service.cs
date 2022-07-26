using Entity;
using Repo;

namespace Ser
{
    public class Service
    {
        public Repository.HomeUsers HU = new Repository.HomeUsers();

        public bool CreateUser(User user)
        {
            return HU.CreateUser(user);
        }

        public List<User> ReadAllUsers()
        {
            return HU.GetUsers();
        }

        public bool DeleteUser(int ID)
        {
            return HU.DeleteByID(ID);
        }

        public User ReadUser(int iD)
        {
            return HU.GetById(iD);
        }

        public bool UpdateUser(User newUser, int oldID)
        {
            return HU.UpdateUser(newUser, oldID);
        }
    }
}