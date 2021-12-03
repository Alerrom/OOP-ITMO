using System;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BankTest
    {
        private CentralBank _centralBank;

        [SetUp]
        public void SetUp()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void AddClient_CreateClientAndAddClientToBank_ClientCreatedSuccessfully()
        {
            Bank tinkoff = _centralBank.RegisterBank("Tinkoff", 15000f);
            Client sasha = new ClientBuilder()
                .SetName("Sasha")
                .SetSurname("Ershov")
                .SetAddress("Вязьма")
                .SetPassport("123456")
                .GetClient();
            var creditAccount = new CreditAccount(4000, sasha, 2, 4000);
            tinkoff.AddClient(sasha);
            tinkoff.AssignAccountToClient(sasha, creditAccount);
            tinkoff.Withdraw(creditAccount.Id, 8000);
            Assert.AreEqual(-4200,creditAccount.Balance);
        }

        [Test]
        public void TopUpAccountBalance_CreateDebitAccountRegisterClientAndAccountTopUpRejectTransaction_TransactionRejected()
        {
            Bank sber = _centralBank.RegisterBank("Sber", 5000f);
            Client sasha = new ClientBuilder()
                .SetName("Sasha")
                .SetSurname("Ershov")
                .SetAddress("Вязьма")
                .SetPassport("123456")
                .GetClient();
            var debitAccount  = new DebitAccount(0, sasha, 2);
            sber.AddClient(sasha);
            sber.AssignAccountToClient(sasha, debitAccount);
            sber.TopUp(debitAccount.Id,15000);
            Assert.AreEqual(15000,debitAccount.Balance);
            sber.RejectTransaction(debitAccount.Id,Guid.Empty);
            Assert.AreEqual(0,debitAccount.Balance);
        }
    }
}