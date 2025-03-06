﻿using System;
using System.Collections.Generic;

namespace System_Rezerwacji_Biletów.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string email { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
