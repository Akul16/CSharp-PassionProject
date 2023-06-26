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
    public class VenueDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VenueData/listVenues
        [HttpGet]
        [ResponseType(typeof(VenueDto))]
        public IHttpActionResult ListVenues()
        {
           List<Venue> Venue = db.Venues.ToList();
            List<VenueDto> VenueDtos = new List<VenueDto>();

            Venue.ForEach(s => VenueDtos.Add(new VenueDto()
            {
                VenueId = s.VenueId,
                VenueName= s.VenueName,
                Location= s.Location,
                Capacity= s.Capacity,
            }));

            return Ok(VenueDtos);
        }

        // GET: api/VenueData/FindVenue/5

        [ResponseType(typeof(VenueDto))]
        [HttpGet]
        public IHttpActionResult FindVenue(int id)
        {
            Venue Venue = db.Venues.Find(id);
            VenueDto VenueDto = new VenueDto()
            {
                VenueId = id,
                VenueName= Venue.VenueName,
                Location= Venue.Location,
                Capacity= Venue.Capacity,
            };
            if (Venue == null)
            {
                return NotFound();
            }

            return Ok(VenueDto);
        }

        // PUT: api/VenueData/UpdateVenue/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVenue(int id, Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venue.VenueId)
            {
                return BadRequest();
            }

            db.Entry(venue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
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

        // POST: api/VenueData/AddVenue
        [ResponseType(typeof(Venue))]
        [HttpPost]
        public IHttpActionResult AddVenue(Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Venues.Add(venue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venue.VenueId }, venue);
        }

        // DELETE: api/VenueData/DeleteVenue/5
        [ResponseType(typeof(Venue))]
        [HttpPost]
        public IHttpActionResult DeleteVenue(int id)
        {
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return NotFound();
            }

            db.Venues.Remove(venue);
            db.SaveChanges();

            return Ok(venue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VenueExists(int id)
        {
            return db.Venues.Count(e => e.VenueId == id) > 0;
        }
    }
}