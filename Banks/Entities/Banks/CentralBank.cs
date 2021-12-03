using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.TransactionSystem;
using Banks.Tools;

namespace Banks.Entities.Banks
{
    public class CentralBank
    {
        private readonly List<Bank> _banks = new List<Bank>();

        public Bank RegisterBank(string name, float bankLimitForDoubtfulAccount)
        {
            var tmpBank = new Bank(name, bankLimitForDoubtfulAccount);
            _banks.Add(tmpBank);
            return tmpBank;
        }

        public void Transfer(Guid sourceId, Guid destinationId, float sum)
        {
            AbstractAccount source = AllAccounts().FirstOrDefault(account => account.Id == sourceId);
            if (source == null)
                throw new AccountDoesNotExistException();

            AbstractAccount destination = AllAccounts().FirstOrDefault(account => account.Id == destinationId);
            if (destination == null)
                throw new AccountDoesNotExistException();

            new Transaction(source, destination).Transfer(sum);
        }

        public void RejectTransaction(Guid sourceId, Guid destinationId)
        {
            AbstractAccount source = AllAccounts().FirstOrDefault(account => account.Id == sourceId);
            if (source == null)
                throw new AccountDoesNotExistException();

            AbstractAccount destination = AllAccounts().FirstOrDefault(account => account.Id == destinationId);
            if (destination == null && destinationId != Guid.Empty)
                throw new AccountDoesNotExistException();

            new Transaction(source, destination).Undo();
        }

        private List<AbstractAccount> AllAccounts()
        {
            var ans = new List<AbstractAccount>();
            foreach (Bank bank in _banks)
            {
                ans.AddRange(bank.Accounts);
            }

            return ans;
        }
    }
}