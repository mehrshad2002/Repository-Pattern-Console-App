using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OI;
using Ser;
using Entity;

namespace RepoPatternApp
{
    public class MainApp
    {
        public static IO io = new IO();
        public static Service service = new Service();
        public static void Main()
        {
            while (true)
            {
                //show List Of Command
                io.Print("1-Create New User\n2-Read All Users\n3-Update User\n4-Delete User\n5-Read by ID \n0-Exit");
                io.PrintAt("Enter Your Command Number : ");

                // Command Value And Fill With Int Value
                int Command;
                try
                {
                    // Get Command Value From User
                    Command = io.GetInt();
                    if(Command == 0)
                    {
                        break;
                    }
                    else
                    {
                       // Caller Method For Calling Correct Command
                       CallCommand(Command);
                    }
                }catch(Exception e)
                {
                    io.Print("Enter Int Without String.");
                }
            }
        } 

        private static void CallCommand(int command)
        {
            switch (command)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    ReadAllUsers();
                    break;
                case 3:
                    UpdateUser();
                    break;
                case 4:
                    ReadAllUsers();
                    DeleteUser();
                    break;
                case 5:
                    ReadUser();
                    break;
            }
        }

        private static void UpdateUser()
        {
            ReadAllUsers();
            io.Print("-------------");

            // First We Need Old ID
            int OldID;
            while (true)
            {
                try
                {
                    io.PrintAt("Enter User ID : ");
                    OldID = io.GetInt();
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Enter <int>");
                }
            }

            io.Print("-------------");
            io.Print("Now Fill New Data");


            // Fill New User Data 
            // We Cant Change ID 
            User newUser = new User();
            while (true)
            {
                try
                {
                    io.PrintAt("Enter New Name : ");
                    newUser.Name = io.GetStr();
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Enter <String>");
                }
            }

            while (true)
            {
                try
                {
                    io.PrintAt("Enter New Password : ");
                    newUser.Password = io.GetStr();
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Enter <String>");
                }
            }

            bool Result = service.UpdateUser(newUser, OldID);
            if (Result)
            {
                io.Print("Users Updated");
                ReadAllUsers();
            }
            else
            {
                io.Print("Somthings Was Wring[try Again]");
            }
        }

        private static void ReadUser()
        {
            io.PrintAt("Enter User ID : ");
            int ID;
            while (true)
            {
                try
                {
                    ID = io.GetInt();
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Enter <int>");
                }
            }
            User user = new User();
            user = service.ReadUser(ID);
            io.Print("--------------");
            io.Print($"ID : {user.Id}");
            io.Print($"Name : {user.Name}");
            io.Print($"Password : {user.Password}");
            io.Print("--------------");
        }

        private static void DeleteUser()
        {
            io.PrintAt("Enter User ID : ");
            int ID;

            while (true)
            {
                try
                {
                    ID = io.GetInt();
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Enter <int>");
                }
            }
            int i = 0;
            bool Result = service.DeleteUser(ID);
            if (Result)
            {
                io.Print("User Deleted.");
                ReadAllUsers();
            }
            else
            {
                io.Print("Somthings Wrong");
            }
        }

        private static void ReadAllUsers()
        {
            List<User> users = new List<User>();
            // get All Users 
            users = service.ReadAllUsers();
            foreach(User user in users)
            {
                io.Print("");
                io.Print("-------------");
                io.Print($"ID : {user.Id}");
                io.Print($"Name : {user.Name}");
                io.Print($"Password : {user.Password}");
                io.Print("-------------");
                io.Print("");
            }
        }

        public static void CreateUser()
        {
            // Create obj and fill that
            User user = new User();
            io.PrintAt("Enter Name : ");
            user.Name = io.GetStr();
            io.PrintAt("Enter Password : ");
            user.Password = io.GetStr();

            bool Result = service.CreateUser(user);
            if (Result)
            {
                io.Print("User Created.");
            }
            else
            {
                io.Print("Somthings Wrong<Try Again>");
            }
        }
    }
}

