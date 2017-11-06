using System;

namespace F1Logic
{
    public class Race
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return string.Format($"{Number} {Date.ToShortDateString()} {Country}");
        }
    }
}