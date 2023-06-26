using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }

    public class TeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}