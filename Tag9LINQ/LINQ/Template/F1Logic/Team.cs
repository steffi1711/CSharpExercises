using System.Collections.Generic;

namespace F1Logic
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Nation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Motor: {1}", Name, Engine);
        }
    }
}