using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}