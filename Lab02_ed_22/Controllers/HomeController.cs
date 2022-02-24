using CsvHelper;
using Lab02_ed_22.Helpers;
using Lab02_ed_22.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02_ed_22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (Data.Instance.TiempoEjecucion != null)
            {
                var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
                using (var write = new StreamWriter(path + "\\TiemposDeEjecucion.csv"))
                using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(Data.Instance.TiempoEjecucion);
                }
                return View();
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
