using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        public string OpponentTeam { get; set; }

        public DateTime Date { get; set; }

        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
    }

    public class MatchDto
    {
        public int MatchId { get; set; }
        public string HomeTeamName { get; set; }

        public string OpponentTeam { get; set; }

        public DateTime Date { get; set; }
        public string VenueName { get; set; }
    }
}