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
            string fileName = $"{hosting.WebRootPath}\\Files\\{file.FileName}";
            using (FileStream stramFile = System.IO.File.Create(fileName))
            {
                file.CopyTo(stramFile);
                stramFile.Flush();
            }

            this.SetEquiposList(file.FileName);
            return View(Data.Instance.equipoList);
        }

        private void SetEquiposList(string fileName)
        {
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
                var validacion = EquipoModel.Guardar(new EquipoModel
                {
                    NombreEquipo = collection["NombreEquipo"],
                    Coach = collection["Coach"],
                    Liga = collection["Liga"],
                    FechaCreacion = Convert.ToDateTime(collection["FechaCreacion"])
                });
                if (validacion)
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
                var validacion = EquipoModel.Eliminar(id);
                if (validacion)
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
