using System;
using Banks.Entities.Clients;
using Banks.MementoPattern;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public abstract class AbstractAccount
    {
        protected AbstractAccount(float balance, Client accountOwner, float interestOnBalance)
        {
            Balance = balance;
            AccountOwner = accountOwner;
            Id = Guid.NewGuid();
            InterestOnBalance = interestOnBalance;
            Caretaker = new Caretaker(this);
        }

        public Guid Id { get; }
        public Client AccountOwner { get; }
        public virtual float InterestOnBalance { get; }
        public float Balance { get; set; }
        public Caretaker Caretaker { get; }

        public IMemento Save()
        {
            return new ConcreteMemento(Balance);
        }

        public void Restore(IMemento memento)
        {
            if (memento is not ConcreteMemento)
                throw new UnknownMementoException();

            Balance = memento.GetState();
        }

        public string GetInfo()
        {
            return $"Owner: {AccountOwner.Name} {AccountOwner.Surname}\n" +
                   $"Id: {Id}\n" +
                   $"Balance: {Balance}\n" +
                   $"Account type: {GetType().Name}\n";
        }
    }
}