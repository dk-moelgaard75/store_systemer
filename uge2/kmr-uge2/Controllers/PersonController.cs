using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kmr_uge2.Models;
using kmr_uge2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;

namespace kmr_uge2.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _personService.GetItemsAsync("SELECT * FROM c"));
        }
        [HttpPost]
        public IActionResult Index(PersonModel person)
        {
            ViewBag.Result = $"Personen:" + person.FirstName + " - " + person.LastName + " - CPR:" + person.SocialSecurityNumber + " oprettet";
            return View();
        }


        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("FirstName,LastName,SocialSecurityNumber")] PersonModel person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await _personService.AddItemAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }



        [ActionName("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            PersonModel person = null;
            if (id != null && id.Length > 0)
            {
                person = await _personService.GetItemAsync(id);
                return View(person);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync([Bind("FirstName,LastName,SocialSecurityNumber")] PersonModel person)
        {
            if (ModelState.IsValid)
            {
                await _personService.UpdateItemAsync(person.Id,person);
            }
            return View();
        }

        [ActionName("Details")]
        public async Task<ActionResult> Details(string id)
        {
            PersonModel person = null;
            if (id != null && id.Length > 0)
            {
                person = await _personService.GetItemAsync(id);
                return View(person);
            }
            return View();
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            PersonModel person = null;
            if (id != null && id.Length > 0)
            {
                person = await _personService.GetItemAsync(id);
                return View(person);
            }
            return View();

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id != null && id.Length > 0)
            {
                await _personService.DeleteItemAsync(id);
                return RedirectToAction("Index");
            }
            return View();

        }

        

    }
}
