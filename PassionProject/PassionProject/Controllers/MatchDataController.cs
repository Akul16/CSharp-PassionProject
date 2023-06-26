using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using PassionProject.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace PassionProject.Controllers
{
    public class MatchDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MatchData/ListMatches
        [HttpGet]
        [ResponseType(typeof(MatchDto))]
        public IEnumerable<MatchDto> ListMatches()
        {
            List<Match> Matches = db.Matches.ToList();
            List<MatchDto> MatchDtos = new List<MatchDto>();

            Matches.ForEach(a => MatchDtos.Add(new MatchDto()
            {
                MatchId = a.MatchId,
                HomeTeamName = a.HomeTeam.TeamName,
                Date = a.Date,
                VenueName = a.Venue.VenueName,
                OpponentTeam = a.OpponentTeam,
            }));
            return MatchDtos;

        }

        // GET: api/MatchData/FindMatch/1
        [ResponseType(typeof(Match))]
        [HttpGet]
        public IHttpActionResult FindMatch(int id)
        {
            Match Match = db.Matches.Find(id);
            MatchDto MatchDto = new MatchDto()
            {
                MatchId = Match.MatchId,
                HomeTeamName = Match.HomeTeam.TeamName,
                Date = Match.Date,
                VenueName = Match.Venue.VenueName,
                OpponentTeam = Match.OpponentTeam,
            };
            if (Match == null)
            {
                return NotFound();
            }

            return Ok(MatchDto);
        }

        [HttpGet]
        public IHttpActionResult ListMatchesForVenue(int venueId)
        {
            // Retrieve matches for the specified venueId using Entity Framework
            var matches = db.Matches.Where(m => m.VenueId == venueId).ToList();

            var matchesDto = matches.Select(m => new MatchDto
            {
                HomeTeamName = m.HomeTeam.TeamName,
                OpponentTeam = m.OpponentTeam,
            });


            // Return the list of matches to the view
            return Ok(matchesDto);
        }

        [HttpGet]
        public IHttpActionResult ListMatchesForTeam(int TeamId)
        {
            // Retrieve matches for the specified venueId using Entity Framework
            var matches = db.Matches.Where(m => m.HomeTeamId == TeamId).ToList();

            // Return the list of matches to the view
            return Ok(matches);
        }


        // PUT: api/MatchData/updatematch/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMatch(int id, Match match)
        {
            Debug.WriteLine("I have reached the update match method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != match.MatchId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + match.MatchId);
                Debug.WriteLine("POST parameter" + match.HomeTeamId);
                Debug.WriteLine("POST parameter " + match.Date);
                Debug.WriteLine("POST parameter " + match.VenueId);
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    Debug.WriteLine("Match not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MatchData/AddMatch
        [ResponseType(typeof(Match))]
        [HttpPost]
        public IHttpActionResult AddMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = match.MatchId }, match);
        }

        // DELETE: api/MatchData/DeleteMatch/5
        [ResponseType(typeof(Match))]
        [HttpPost]
        public IHttpActionResult DeleteMatch(int id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            db.SaveChanges();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.MatchId == id) > 0;
        }
    }
}