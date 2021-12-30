using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.Clients;
using Banks.Entities.TimeProvider;
using Banks.Tools;
using Transaction = Banks.Entities.TransactionSystem.Transaction;

namespace Banks.Entities.Banks
{
    public class Bank
    {
        private List<Client> _clients = new List<Client>();
        private List<AbstractAccount> _accounts = new List<AbstractAccount>();
        public Bank(string name, float bankLimitForDoubtfulAccount)
        {
            Id = Guid.NewGuid();
            Name = name;
            BankLimitForDoubtfulAccount = bankLimitForDoubtfulAccount;
            BankTime = new BankTimeProvider(DateTime.Now, DateTime.Now);
        }

        public Guid Id { get; }
        public string Name { get; }
        public float BankLimitForDoubtfulAccount { get; }
        public BankTimeProvider BankTime { get; }

        public List<AbstractAccount> Accounts => _accounts;

        public void AddClient(Client client)
        {
            if (_clients.Any(varClient => varClient.Passport == client.Passport))
            {
                throw new ClientHasAlreadyRegisteredInBankException();
            }

            _clients.Add(client);
        }

        public void AssignAccountToClient(Client client, AbstractAccount account)
        {
            _accounts.Add(account);
            client.AddAccount(account);
        }

        public void TopUp(Guid accountId, float sum)
        {
            UpdateAccountInterest();
            AbstractAccount account = _accounts.FirstOrDefault(account => account.Id == accountId);
            if (account == null)
                throw new AccountDoesNotExistException();

            new Transaction(account).TopUp(sum);
        }

        public void Withdraw(Guid accountId, float sum)
        {
            UpdateAccountInterest();
            AbstractAccount account = _accounts.FirstOrDefault(account => account.Id == accountId);
            if (account == null)
                throw new AccountDoesNotExistException();

            if (account.AccountOwner.IsDoubtfulAccount() && BankLimitForDoubtfulAccount < sum)
                throw new TransactionRejectedException("You have exceeded the limit");

            new Transaction(account).Withdraw(sum);
        }

        public void Transfer(Guid sourceId, Guid destinationId, float sum)
        {
            UpdateAccountInterest();
            AbstractAccount source = _accounts.FirstOrDefault(account => account.Id == sourceId);
            if (source == null)
                throw new AccountDoesNotExistException();

            AbstractAccount destination = _accounts.FirstOrDefault(account => account.Id == destinationId);
            if (destination == null)
                throw new AccountDoesNotExistException();

            if (source.AccountOwner.IsDoubtfulAccount() && BankLimitForDoubtfulAccount < sum)
                throw new TransactionRejectedException("You have exceeded the limit");

            new Transaction(source, destination).Transfer(sum);
        }

        public void RejectTransaction(Guid sourceId, Guid destinationId)
        {
            UpdateAccountInterest();
            AbstractAccount source = _accounts.FirstOrDefault(account => account.Id == sourceId);
            if (source == null)
                throw new AccountDoesNotExistException();

            AbstractAccount destination = _accounts.FirstOrDefault(account => account.Id == destinationId);
            if (destination == null && destinationId != Guid.Empty)
                throw new AccountDoesNotExistException();

            new Transaction(source, destination).Undo();
        }

        public void UpdateAccounts()
        {
            UpdateAccountInterest();
            UpdateAccountBalance();
        }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Name: {Name}\n" +
                   $"Number of clients {_clients.Count}\n" +
                   $"Registered accounts: {_accounts.Count}\n";
        }

        private void UpdateAccountInterest()
        {
            foreach (AbstractAccount account in _accounts)
            {
                if ((BankTime.CurrentTime - account.LastInterestUpdate).Days < 1)
                    continue;

                account.ChargeInterestOnBalance((BankTime.CurrentTime - BankTime.LastUpdate).Days, BankTime.CurrentTime);
            }
        }

        private void UpdateAccountBalance()
        {
            foreach (AbstractAccount account in _accounts)
            {
                account.UpdateBalance(account.CurrentInterest);
                account.SetCurrentInterest(0);
            }

            BankTime.SetLastUpdate(BankTime.CurrentTime);
        }
    }
}