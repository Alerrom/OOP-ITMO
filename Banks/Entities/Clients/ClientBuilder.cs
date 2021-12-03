namespace Banks.Entities.Clients
{
    public class ClientBuilder
    {
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _address = string.Empty;
        private string _passport = string.Empty;

        public ClientBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public Client GetClient()
        {
            return new Client(_name, _surname, _address, _passport);
        }
    }
}