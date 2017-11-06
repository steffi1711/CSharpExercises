namespace F1Logic
{
    public class ResultLine
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public override string ToString()
        {
            return string.Format("{0,2} {1,-25} {2,3}", Position, Name, Points);
        }
    }
}