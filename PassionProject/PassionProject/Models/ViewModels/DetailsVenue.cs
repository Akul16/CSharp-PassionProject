using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class DetailsVenue
    {
        public DbSet<MatchDto> Matches { get; set; }
        public DbSet<Venue> Venues { get; set; }

        public MatchDto HomeTeamName { get; set; }

        public MatchDto OpponentTeam { get; set; }
        
    }
}