﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class ViewMatch
    {

        public IEnumerable<VenueDto> VenueOptions { get; set; }
        public IEnumerable<TeamDto> TeamId { get; set; }


    }
}