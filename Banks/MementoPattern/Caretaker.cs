using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;

namespace Banks.MementoPattern
{
    public class Caretaker
    {
        private readonly List<IMemento> _mementos = new List<IMemento>();
        private AbstractAccount _account;

        public Caretaker(AbstractAccount account)
        {
            _account = account;
        }

        public void Backup()
        {
            _mementos.Add(_account.Save());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
                return;

            IMemento memento = _mementos.Last();
            _mementos.Remove(memento);

            try
            {
                _account.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }
    }
}