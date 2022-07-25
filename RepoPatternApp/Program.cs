using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OI;
using Ser;

namespace RepoPatternApp
{
    public class MainApp
    {
        public static IO io = new IO();
        public static Service service = new Service();
        public static void main()
        {
            while (true)
            {
                //show List Of Command
                io.Print("1-Create New User\n2-Read All Users\n3-Update User\n4-Delete User\n0-Delete");
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
                    bool Result ;
                    break;
            }
        }

        private static void Start()
        {
            throw new NotImplementedException();
        }
    }
}

