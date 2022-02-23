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
            return View();
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

        public ActionResult BusquedaNombre(string Nombre)
        {

            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Nombre))
            {
                model = model.Where(jugador => jugador.Nombre.Contains(Nombre));
            }
            return View(model);
        }

        public ActionResult BusquedaApellido(string Apellido)
        {

            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Apellido))
            {
                model = model.Where(jugador => jugador.Apellido.Contains(Apellido));
            }
            return View(model);
        }
        public ActionResult BusquedaRol(string Rol)
        {

            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Rol))
            {
                model = model.Where(jugador => jugador.Rol.Contains(Rol));
            }
            return View(model);
        }
        public ActionResult BusquedaKDA(string KDA)
        {
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(KDA))
            {
                 double gr = Convert.ToDouble(KDA);
                return View(model.Where(X => X.KDA == gr));
            }
            return View(model);
        }
        public ActionResult BusquedaCreepScore(string CreepScore)
        {
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(CreepScore))
            {
                int gr = int.Parse((CreepScore));
                return View(model.Where(X => X.CreepScore == gr));
            }
            return View(model);
        }
        public ActionResult BusquedaEquipo(string Equipo)
        {

            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Equipo))
            {
                model = model.Where(jugador => jugador.Equipo.Contains(Equipo));
            }
            return View(model);
        }
    }

}

