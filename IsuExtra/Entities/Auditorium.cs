namespace IsuExtra.Entities
{
    public class Auditorium
    {
        public Auditorium(int number, Buildings building)
        {
            Number = number;
            Building = building;
        }

        public int Number { get; }
        public Buildings Building { get; }
    }
}