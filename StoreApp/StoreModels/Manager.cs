namespace StoreModels
{
    public class Manager
    {
        public Manager()
        {
        }
        public Manager(string name, string username, string password){
            this.Name = name;
            this.UserName = username;
            Password = password;
        }
        public Manager(string username){
            UserName = username;
            Name = null;
        }

        public string Name { get; set; }
        public string UserName { get; set;}
        public string Password { get; set; }

        public bool Equals(string username){
            return this.UserName.Equals(username);
        }

        public override string ToString()
        {
            return $" Username: {UserName} \t Name: {Name}";
        }
    }
}