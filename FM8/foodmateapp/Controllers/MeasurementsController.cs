using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using foodmateapp.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace foodmateapp.Controllers
{
    public class MeasurementsController : Controller
    {
        FoodmateContext db = new FoodmateContext();
        public IActionResult Index()
        {
            return View();
        }
    }
}