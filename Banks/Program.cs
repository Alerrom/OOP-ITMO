using System;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;
using Banks.Entities.Clients;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centralBank = new CentralBank();
            Bank tinkoff = centralBank.RegisterBank("Tinkoff", 15000f);
            Client sasha = new ClientBuilder()
                .SetName("Sasha")
                .SetSurname("Ershov")
                .SetAddress("Вязьма")
                .SetPassport("123456")
                .GetClient();
            var creditAccount = new CreditAccount(4000, sasha, 2, 4000);
            tinkoff.AddClient(sasha);
            tinkoff.AssignAccountToClient(sasha, creditAccount);

            Console.WriteLine(creditAccount.Balance);
            tinkoff.Withdraw(creditAccount.Id, 8000);
            Console.WriteLine(creditAccount.Balance);
        }
    }
}
