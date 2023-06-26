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
    public class TeamController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TeamController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44346/api/");
        }
        // GET: Team/List
        public ActionResult List()
        {

            string url = "TeamData/listTeams";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The Response is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<TeamDto> Teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result;
            Debug.WriteLine("Number of Teames received: ");
            Debug.WriteLine(Teams.Count());

            return View(Teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            //DetailsTeam ViewModel = new DetailsTeam();

            string url = "TeamData/findTeam/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the respone code is ");
            Debug.WriteLine(response.StatusCode);

            TeamDto selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;
            Debug.WriteLine("Teames : ");
            Debug.WriteLine(selectedTeam.TeamId);

            //ViewModel.SelectedTeam = selectedTeam;

            return View(selectedTeam);
        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Team/New
        public ActionResult New()
        {
            string url = "Teamdata/listTeam";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<TeamDto> TeamOptions = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result;

            return View();
        }


        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(Team Team)
        {
            string url = "TeamData/addTeam";

            string jsonpayload = jss.Serialize(Team);
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

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        { 

            //the existing Team information
            string url = "Teamdata/findTeam/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeamDto SelectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;
            return View(SelectedTeam);

        }



        // POST: Team/Update/5
        [HttpPost]
        public ActionResult Update(int id, Team Team)
        {

            string url = "Teamdata/updateTeam/" + id;
            string jsonpayload = jss.Serialize(Team);
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
        // GET: Team/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Teamdata/findTeam/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeamDto selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;
            return View(selectedTeam);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Teamdata/deleteTeam/" + id;
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