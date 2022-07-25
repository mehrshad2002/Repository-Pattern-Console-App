namespace Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }



        // Constructor<1>
        public User() { }



        // Constructor<2>
        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}