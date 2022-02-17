using Lab02_ed_22.Helpers;
using Lab02_ed_22.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: EquipoController/Details/5
        public ActionResult Details(string nombre)
        {
            var equipo = Data.Instance.equipoList.Find(model => model.NombreEquipo == nombre);
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
        public ActionResult Edit(string nombre)
        {
            var equipo = Data.Instance.equipoList.Find(modelo => modelo.NombreEquipo == nombre);
            return View(equipo);
        }

        // POST: EquipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string nombre, IFormCollection collection)
        {
            try
            {
                var validacion = EquipoModel.Editar(nombre, new EquipoModel
                {
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
        public ActionResult Delete(string nombre)
        {
            var equipo = Data.Instance.equipoList.Find(modelo => modelo.NombreEquipo == nombre);
            return View(equipo);
        }

        // POST: EquipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string nombre, IFormCollection collection)
        {
            try
            {
                var validacion = EquipoModel.Eliminar(nombre);
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
