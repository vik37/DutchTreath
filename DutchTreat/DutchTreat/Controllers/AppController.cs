using DustTreat.Data;
using DustTreat.Services;
using DustTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DustTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repo;
        public AppController(IMailService mailService, IDutchRepository repo)
        {
            _mailService = mailService;
            _repo = repo;
        }
        public IActionResult Index()
        {
            //throw new InvalidProgramException("Bad happends things on good developers");
           // var results = _ctx.Products.ToList();
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            //throw new InvalidOperationException("Bad things happend");
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email
                _mailService.SendMessage("shawn@wildremuth.com", 
                    model.Subject, 
                    $"Form: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.Message = "Mail Send";
                ModelState.Clear();
            }
            
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        public IActionResult Shop()
        {
            //var result = from p in _context.Products
            //             orderby p.Category
            //             select p;
            //return View(result.ToList());

            var result = _repo.GetAllProducts();
            return View(result);
        }
    }
}
