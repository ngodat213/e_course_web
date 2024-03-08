using e_course_web.DataQuery;
using ECourse.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace e_course_web.Controllers
{
    public class ContactController : Controller
    {

        public ContactController(){}

        public async Task<IActionResult> Index()
        {
            DataTable dt = new DataTable();
            dt = await APICall.GetAsync<DataTable>(ManagerAddress.domain, ManagerAddress.contact);
            return View(dt);
        }

        public async Task<ActionResult<String>> AddContact(Contact model)
        {
            Contact obj = new Contact()
            {
                Id = model.Id,
                FullName = model.FullName,
                Mail = model.Mail,
                Text = model.Text,
                Topic = model.Topic
            };

            // if() check value
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage getData = await client.PostAsJsonAsync<Contact>("contact", obj);

                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Console.WriteLine("Error calling web API: " + getData.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error calling web API: " + ex.Message);
                }
            }
            return View();
        }
    }
}
