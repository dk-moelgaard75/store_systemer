using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kmr_uge2.Models;
using kmr_uge2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kmr_uge2.Controllers
{
    public class CovidTestController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICovidTestService _covidTestService;
        public CovidTestController(IPersonService personService, ICovidTestService covidTestService)
        {
            _personService = personService;
            _covidTestService = covidTestService;
        }
        /*
        public async Task<IActionResult> Index()
        {
            return View(await _covidTestService.GetItemsAsync("SELECT * FROM c"));
        }
        */

        
        public async Task<IActionResult> Index()
        {
            return View(await _covidTestService.GetItemsAsync("SELECT * FROM c"));
        }


        [ActionName("Create")]
        public async Task<IActionResult> Create()
        {
            IEnumerable<PersonModel> persons = await _personService.GetItemsAsync("SELECT * FROM c");
            ViewData["Persons"] = new SelectList(persons, "SocialSecurityNumber", "NameIdentifier");

            return View();
        }


    }
}
