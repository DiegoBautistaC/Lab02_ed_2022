using CsvHelper;
using Lab02_ed_22.Helpers;
using Lab02_ed_22.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab02_ed_22.Controllers
{
    public class EquipoController : Controller
    {
        // GET: EquipoController
        public ActionResult Index()
        {
            return View(Data.Instance.equipoList);
        }

        [HttpGet]
        public IActionResult Index(List<EquipoModel> equipos = null)
        {
            return View(Data.Instance.equipoList);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            if (file != null)
            {
                string fileName = $"{hosting.WebRootPath}\\Files\\{file.FileName}";
                using (FileStream stramFile = System.IO.File.Create(fileName))
                {
                    file.CopyTo(stramFile);
                    stramFile.Flush();
                }

                this.SetEquiposList(file.FileName);
                return View(Data.Instance.equipoList);
            }
            return RedirectToAction(nameof(Index));
        }

        private void SetEquiposList(string fileName)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var equipo = csv.GetRecord<EquipoModel>();
                    Data.Instance.equipoList.Add(equipo);
                }
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución lectura de csv de equipos: " + reloj.ElapsedMilliseconds + " ms\n");
        }

        // GET: EquipoController/Details/5
        public ActionResult Details(string id)
        {
            var equipo = Data.Instance.equipoList.Find(model => model.NombreEquipo == id);
            return View(equipo);
        }

        // GET: EquipoController/Create
        public ActionResult Create()
        {
            return View(new EquipoModel());
        }

        // POST: EquipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var reloj = new Stopwatch();
                reloj.Start();
                var validacion = EquipoModel.Guardar(new EquipoModel
                {
                    NombreEquipo = collection["NombreEquipo"],
                    Coach = collection["Coach"],
                    Liga = collection["Liga"],
                    FechaCreacion = Convert.ToDateTime(collection["FechaCreacion"])
                });
                if (validacion)
                {
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución creación de nuevo equipo: " + reloj.ElapsedMilliseconds + " ms\n");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EquipoController/Edit/5
        public ActionResult Edit(string id)
        {
            var equipo = Data.Instance.equipoList.Find(modelo => modelo.NombreEquipo == id);
            return View(equipo);
        }

        // POST: EquipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var reloj = new Stopwatch();
                reloj.Start();
                var equipo = Data.Instance.equipoList.Find(modelo => modelo.NombreEquipo == id);
                var validacion = EquipoModel.Editar(equipo, new EquipoModel
                {
                    NombreEquipo = collection["NombreEquipo"],
                    Coach = collection["Coach"],
                    Liga = collection["Liga"],
                    FechaCreacion = Convert.ToDateTime(collection["FechaCreacion"])
                });
                if (validacion)
                {
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución edición de un equipo: " + reloj.ElapsedMilliseconds + " ms\n");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EquipoController/Delete/5
        public ActionResult Delete(string id)
        {
            var equipo = Data.Instance.equipoList.Find(modelo => modelo.NombreEquipo == id);
            return View(equipo);
        }

        // POST: EquipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var reloj = new Stopwatch();
                reloj.Start();
                var validacion = EquipoModel.Eliminar(id);
                if (validacion)
                {
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución eliminación de un equipo: " + reloj.ElapsedMilliseconds + " ms\n");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BusquedaLiga(string Liga)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.equipoList select s;
            if (!string.IsNullOrEmpty(Liga))
            {
                model = model.Where(equipo => equipo.Liga.Contains(Liga));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por rol: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaCoach(string Coach)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.equipoList select s;
            if (!string.IsNullOrEmpty(Coach))
            {
                model = model.Where(equipo => equipo.Coach.Contains(Coach));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por rol: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaFecha(string Fecha)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.equipoList select s;
            if (!string.IsNullOrEmpty(Fecha))
            {
              DateTime gr = Convert.ToDateTime(Fecha);
                return View(model.Where(X => X.FechaCreacion == gr));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por KDA: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaNombreE(string NombreE)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.equipoList select s;
            if (!string.IsNullOrEmpty(NombreE))
            {
                model = model.Where(equipo => equipo.NombreEquipo.Contains(NombreE));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por nombre: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }


    }
}
