using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

    }

    public class VenueDto
    {
        public int VenueId { get; set; }
        public string VenueName { get;set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}