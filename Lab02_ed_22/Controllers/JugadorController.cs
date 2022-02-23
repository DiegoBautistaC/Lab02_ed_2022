using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab02_ed_22.Models;
using Lab02_ed_22.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Lab02_ed_22.Controllers
{
    public class JugadorController : Controller
    {
        // GET: JugadorController
       public IActionResult Index()
       {
            return View(Data.Instance.jugadorlist);
       }

        [HttpGet]
        public IActionResult Index(List<JugadorModel> jugadores = null)
        {
            return View(Data.Instance.jugadorlist);
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

            this.SetJugadoresList(file.FileName);
            return Index(Data.Instance.jugadorlist);
        }

        private void SetJugadoresList(string fileName)
        {
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var jugador = csv.GetRecord<JugadorModel>();
                    Data.Instance.jugadorlist.Add(jugador);
                }
            }

            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using(var write = new StreamWriter(path + "\\NewFile.csv"))
            using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Data.Instance.jugadorlist);
            }
        }

        // GET: JugadorController/Details/5
        public ActionResult Details(string Nombre)
        {
            var model = Data.Instance.jugadorlist.Find(jugador => jugador.Nombre == Nombre);
            return View(model);
        }

        // GET: JugadorController/Create
        public ActionResult Create()
        {
            return View(new JugadorModel());
        }

        // POST: JugadorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var response = JugadorModel.Guardar(new JugadorModel
                {
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Rol = collection["Rol"],
                    KDA = double.Parse(collection["KDA"]),
                    CreepScore = int.Parse(collection["CreepScore"]),
                    Equipo = collection["Equipo"]
                });
                if (response)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: JugadorController/Edit/5
        public ActionResult Edit(string Nombre)
        {
            var model = Data.Instance.jugadorlist.Find(jugador => jugador.Nombre == Nombre);
            return View(model);
        }

        // POST: JugadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Nombre, IFormCollection collection)
        {
            try
            {
                var response = JugadorModel.Editar(Nombre, new JugadorModel
                {
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Rol = collection["Rol"],
                    KDA = double.Parse(collection["KDA"]),
                    CreepScore = int.Parse(collection["CreepScore"]),
                    Equipo = collection["Equipo"]
                });
                if (response)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: JugadorController/Delete/5
        public ActionResult Delete(string Nombre)
        {

            var model = Data.Instance.jugadorlist.Find(jugador => jugador.Nombre == Nombre);
            return View(model);
        }

        // POST: JugadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string Nombre, IFormCollection collection)
        {
            try
            {
                var response = JugadorModel.Eliminar(Nombre);
                if (response)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
