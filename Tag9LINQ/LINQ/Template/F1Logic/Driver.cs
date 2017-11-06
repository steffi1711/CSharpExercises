namespace F1Logic
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StartNumber { get; set; }
        public string Nation { get; set; }
        public Team Team { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} Startnummer: {2} Team {3}", LastName, FirstName, StartNumber, Team);
        }
    }
}