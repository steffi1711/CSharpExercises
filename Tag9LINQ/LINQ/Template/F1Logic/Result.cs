using System;

namespace F1Logic
{
    public class Result
    {
        public int Id { get; set; }
        public Race Race { get; set; }
        public Driver Driver { get; set; }
        public int Position { get; set; }
        private int[] pointos = new int[] { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };
        public int Points
        {
            get
            {
                return Position < 11 ? pointos[Position - 1] : 0;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} Platz: {2} Punkte: {3}", Race, Driver, Position, Points);
        }


    }
}