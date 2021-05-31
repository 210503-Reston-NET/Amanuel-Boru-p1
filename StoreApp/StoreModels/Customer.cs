namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        public Customer()
        {
        }
        public Customer(string name, string username){
            this.Name = name;
            this.UserName = username;
        }
        
        public Customer(string username){
            UserName = username;
            Name = null;
        }

        public string Name { get; set; }
        public string UserName { get; set; }

        public bool Equals(string username){
            return this.UserName.Equals(username);
        }

        public override string ToString()
        {
            return $" Username: {UserName} \t Name: {Name}";
        }

    }
}