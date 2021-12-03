using System.Collections.Generic;
using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.Clients
{
    public class Client
    {
        private List<AbstractAccount> _accounts = new List<AbstractAccount>();

        public Client(string name, string surname, string address, string passport)
        {
            if (name == string.Empty)
                throw new IncorrectClientNameException();
            if (surname == string.Empty)
                throw new IncorrectClientSurnameException();
            if (passport == string.Empty)
                throw new IncorrectPassportException();

            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }

        public bool IsDoubtfulAccount()
        {
            return Address == string.Empty || Passport == string.Empty;
        }

        public void AddAccount(AbstractAccount account)
        {
            _accounts.Add(account);
        }

        public string GetInfo()
        {
            return $"Name: {Name} {Surname}\n" +
                   $"Address: {Address}\n" +
                   $"Passport: {Passport}";
        }
    }
}