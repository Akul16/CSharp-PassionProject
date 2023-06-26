using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TeamData/listTeams
        [HttpGet]
        [ResponseType(typeof(TeamDto))]
        public IHttpActionResult ListTeams()
        {
            List<Team> Team = db.Teams.ToList();
            List<TeamDto> TeamDtos = new List<TeamDto>();

            Team.ForEach(s => TeamDtos.Add(new TeamDto()
            {
                TeamId = s.TeamId,
                TeamName = s.TeamName,
            }));

            return Ok(TeamDtos);
        }

        // GET: api/TeamData/FindTeam/5

        [ResponseType(typeof(TeamDto))]
        [HttpGet]
        public IHttpActionResult FindTeam(int id)
        {
            Team Team = db.Teams.Find(id);
            TeamDto TeamDto = new TeamDto()
            {
                TeamId = id,
                TeamName = Team.TeamName,
            };
            if (Team == null)
            {
                return NotFound();
            }

            return Ok(TeamDto);
        }

        // PUT: api/TeamData/UpdateTeam/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeam(int id, Team Team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Team.TeamId)
            {
                return BadRequest();
            }

            db.Entry(Team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }




        // POST: api/TeamData/AddTeam
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult AddTeam(Team Team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(Team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Team.TeamId }, Team);
        }

        // DELETE: api/TeamData/DeleteTeam/5
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team Team = db.Teams.Find(id);
            if (Team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(Team);
            db.SaveChanges();

            return Ok(Team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.TeamId == id) > 0;
        }
    }
}