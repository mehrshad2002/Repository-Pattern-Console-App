using Entity;
using Repo;

namespace Services
{

    public interface IService
    {
        bool CreateUser(User user);

        List<User> ReadAllUsers();

        bool DeleteUser(int ID);

        User ReadUser(int iD);

        bool UpdateUser(User newUser, int oldID);
    }
    public class Service : IService
    {

        public Service(IUser user)
        {
            HU = user;
        }

        public IUser HU; //= new Repository.HomeUsers();

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