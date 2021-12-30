namespace Banks.MementoPattern
{
    public class ConcreteMemento : IMemento
    {
        private readonly float _balanceState;

        public ConcreteMemento(float balanceState)
        {
            _balanceState = balanceState;
        }

        public float GetState()
        {
            return _balanceState;
        }
    }
}