using System;
using System.Collections.Generic;
using CellTechUI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CellTechUI.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://localhost:44357/api/";
        // GET: Home
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url 

            List<CellPhone> ProdInfo = new List<CellPhone>();
            using (var client = new HttpClient())
            {
                //Passing service base url 
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format 
                client.DefaultRequestHeaders.Accept.Add(new
               MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllCellPhones using HttpClient
                HttpResponseMessage Res = await client.GetAsync("CellPhone");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    ProdInfo = JsonConvert.DeserializeObject<List<CellPhone>>(PrResponse);
                }
                //returning the Product list to view 
                return View(ProdInfo);
            }
        }

        // GET: Home/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CellPhone cellPhone = null;
            using (var client = new HttpClient())
            {
                await SetHeader(client);
                //Sending request to find web api REST service resource GetAllCellPhones using HttpClient
                HttpResponseMessage Res = await client.GetAsync("CellPhone/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    cellPhone = JsonConvert.DeserializeObject<CellPhone>(PrResponse);
                    Console.WriteLine(cellPhone.Name);
                }
                //returning the Product list to view 
                return View(cellPhone);
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CellPhone cellPhone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("CellPhone/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    cellPhone = JsonConvert.DeserializeObject<CellPhone>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(cellPhone);
        }
        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CellPhone cellPhone)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.PutAsJsonAsync("CellPhone/" + id, cellPhone).Wait();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CellPhone cellPhone)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.PostAsJsonAsync("CellPhone", cellPhone).Wait();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(CellPhone cellPhone, int id)
        {
            CellPhone cellPhoneToDelete = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("CellPhone/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    cellPhoneToDelete = JsonConvert.DeserializeObject<CellPhone>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(cellPhoneToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CellPhone cellPhone)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DeleteAsync("CellPhone/" + id).Wait();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task SetHeader(HttpClient client)
        {
            //Passing service base url 
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            //Define request data format 
            client.DefaultRequestHeaders.Accept.Add(new
           MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}