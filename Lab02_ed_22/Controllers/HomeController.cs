using CsvHelper;
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
        public IActionResult Index(List<JugadorModel> jugadores = null)
        {
            jugadores = jugadores == null ? new List<JugadorModel>() : jugadores;
            return View(jugadores);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            string fileName = $"{hosting.WebRootPath}\\Files\\{file.FileName}";
            using (FileStream streamFile = System.IO.File.Create(fileName))
            {
                file.CopyTo(streamFile);
                streamFile.Flush();
            }

            var jugadores = this.GetJugadoresList(file.FileName);
            return Index(jugadores);
        }

        private List<JugadorModel> GetJugadoresList(string fileName)
        {
            List<JugadorModel> jugadores = new List<JugadorModel>();

            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var jugador = csv.GetRecord<JugadorModel>();
                    jugadores.Add(jugador);
                }
            }

            return jugadores;
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
