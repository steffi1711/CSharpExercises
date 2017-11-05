using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgr.Core.Entities
{
    public class ReservationRoom: EntityObject
    {
        [ForeignKey(nameof(Guest_Id))]
        public Guest Guest { get; set; }

        public int Guest_Id { get; set; }

        [ForeignKey(nameof(Room_Id))]
        public Room Room { get; set; }
        public int Room_Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string BookingNr { get; set; }
        public int NumOfAdults { get; set; }
        public int NumOfChilds { get; set; }
        public string AgeOfChilds { get; set; }

        public int FinalCleaning { get; set; }
        public int Towels { get; set; }
        public int BedLinen { get; set; }
        public double CostsRoom { get; set; }

        public double CostsFinalCleaning { get; set; }
        public double CostsTowels { get; set; }
        public double CostsBedLinen { get; set; }    

    }
}
