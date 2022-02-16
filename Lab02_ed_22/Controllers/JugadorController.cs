using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab02_ed_22.Models;
using Lab02_ed_22.Helpers;

namespace Lab02_ed_22.Controllers
{
    public class JugadorController : Controller
    {
        // GET: JugadorController
        public ActionResult Index()
        {
            return View(Data.Instance.jugadorlist);
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
                ViewBag["Error"] = "Error while creating new element";
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
                JugadorModel.Editar(Nombre, new JugadorModel
                {
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Rol = collection["Rol"],
                    KDA = double.Parse(collection["KDA"]),
                    CreepScore = int.Parse(collection["CreepScore"]),
                    Equipo = collection["Equipo"]
                });
                return RedirectToAction(nameof(Index));
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
                JugadorModel.Eliminar(Nombre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
