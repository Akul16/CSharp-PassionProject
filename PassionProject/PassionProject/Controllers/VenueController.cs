using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.Models;
using System.Web.Script.Serialization;


namespace PassionProject.Controllers
{
    public class VenueController : Controller
    {
        private static readonly HttpClient client;
        private ApplicationDbContext db = new ApplicationDbContext();
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VenueController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44346/api/");
        }
        // GET: Venue/List
        public ActionResult List()
        {

            string url = "VenueData/listVenues";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The Response is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<VenueDto> Venues = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result;
            Debug.WriteLine("Number of Venuees received: ");
            Debug.WriteLine(Venues.Count());

            return View(Venues);
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            //DetailsVenue ViewModel = new DetailsVenue();

            string url = "VenueData/findVenue/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the respone code is ");
            Debug.WriteLine(response.StatusCode);

            VenueDto selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;
            Debug.WriteLine("Venuees : ");
            Debug.WriteLine(selectedVenue.VenueId);

            //ViewModel.SelectedVenue = selectedVenue;

            return View(selectedVenue);
        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Venue/New
        public ActionResult New()
        {
            string url = "Venuedata/listVenue";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<VenueDto> VenueOptions = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result;

            return View();
        }


        // POST: Venue/Create
        [HttpPost]
        public ActionResult Create(Venue Venue)
        {
            string url = "VenueData/addVenue";

            string jsonpayload = jss.Serialize(Venue);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {


            //the existing Venue information
            string url = "Venuedata/findVenue/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VenueDto SelectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;
            return View(SelectedVenue);


        }


        // POST: Venue/Update/5
        [HttpPost]
        public ActionResult Update(int id, Venue Venue)
        {

            string url = "Venuedata/updateVenue/" + id;
            string jsonpayload = jss.Serialize(Venue);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        // GET: Venue/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Venuedata/findVenue/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VenueDto selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;
            return View(selectedVenue);
        }

        // POST: Venue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Venuedata/deleteVenue/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}