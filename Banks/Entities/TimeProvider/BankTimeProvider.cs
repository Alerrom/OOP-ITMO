using System;
using Banks.Tools;

namespace Banks.Entities.TimeProvider
{
    public class BankTimeProvider
    {
        public BankTimeProvider(DateTime currentTime, DateTime lastUpdate)
        {
            CurrentTime = currentTime;
            LastUpdate = lastUpdate;
        }

        public DateTime LastUpdate { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public void SetLastUpdate(DateTime newTime)
        {
            LastUpdate = newTime;
        }

        public void Change(DateTime future)
        {
            if (future < CurrentTime)
                throw new IncorrectDateTimeException();
            LastUpdate = CurrentTime;
            CurrentTime = future;
        }
    }
}