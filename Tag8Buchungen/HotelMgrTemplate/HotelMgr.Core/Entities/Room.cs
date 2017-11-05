using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgr.Core.Entities
{
    public class Room: EntityObject
    {
        [Required]
        public string RoomName { get; set; }

        [Required]
        public string RoomNumber { get; set; }
    }
}
