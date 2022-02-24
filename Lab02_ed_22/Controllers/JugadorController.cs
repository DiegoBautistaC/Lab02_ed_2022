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
using ClassLibrary;
using System.Diagnostics;

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
            #region Ejemplo Lista doblemente enlazada
            //ListaDoble<JugadorModel> pruebalista = new ListaDoble<JugadorModel>();
            //{
            //    new JugadorModel
            //    {
            //        Nombre = "Douglas",
            //        Apellido = "Salazar",
            //        Rol = "Jungla",
            //        KDA = 2.99,
            //        CreepScore = 10,
            //        Equipo = "SKT1"
            //    };
            //};
            //var jugador1 = new JugadorModel();
            //jugador1.Nombre = "Douglas";
            //jugador1.Apellido = "Salazar";
            //jugador1.Rol = "Jungla";
            //jugador1.KDA = 2.99;
            //jugador1.CreepScore = 10;
            //jugador1.Equipo = "SKT1";

            //var jugador2 = new JugadorModel();
            //jugador2.Nombre = "Segundo";
            //jugador2.Apellido = "2";
            //jugador2.Rol = "22";
            //jugador2.KDA = 2.00;
            //jugador2.CreepScore = 20;
            //jugador2.Equipo = "Equipo2";

            //var jugador3 = new JugadorModel();
            //jugador3.Nombre = "Tercero";
            //jugador3.Apellido = "3";
            //jugador3.Rol = "33";
            //jugador3.KDA = 3.33;
            //jugador3.CreepScore = 30;
            //jugador3.Equipo = "Equipo 3";

            //pruebalista.Agregar(jugador1);
            //pruebalista.Agregar(jugador2);
            //pruebalista.Agregar(jugador1);
            //pruebalista.Agregar(jugador3);
            //pruebalista.Agregar(jugador1);
            //pruebalista.Agregar(jugador3);
            //pruebalista.Agregar(jugador3);
            #endregion
            return View(Data.Instance.jugadorlist);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            if (file != null)
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
            return RedirectToAction(nameof(Index));
        }

        private void SetJugadoresList(string fileName)
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
                    var jugador = csv.GetRecord<JugadorModel>();
                    Data.Instance.jugadorlist.Add(jugador);
                }
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución lectura de jugadores csv: " + reloj.ElapsedMilliseconds + " ms\n");

            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using(var write = new StreamWriter(path + "\\NewFile.csv"))
            using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Data.Instance.jugadorlist);
            }
        }

        // GET: JugadorController/Details/5
        public ActionResult Details(string id)
        {
            var model = Data.Instance.jugadorlist.Find(jugador => jugador.Nombre == id);
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
                var reloj = new Stopwatch();
                reloj.Start();
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
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución creación de nuevo jugador: " + reloj.ElapsedMilliseconds + " ms\n");
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
        public ActionResult Edit(string id)
        {
            var model = Data.Instance.jugadorlist.Find(modelo => modelo.Nombre == id);
            return View(model);
        }

        // POST: JugadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var reloj = new Stopwatch();
                reloj.Start();
                var jugador = Data.Instance.jugadorlist.Find(modelo => modelo.Nombre == id);
                var validacion = JugadorModel.Editar(jugador, new JugadorModel
                {
                    Nombre = jugador.Nombre,
                    Apellido = jugador.Apellido,
                    Rol = collection["Rol"],
                    KDA = jugador.KDA,
                    CreepScore = jugador.CreepScore,
                    Equipo = collection["Equipo"]
                });
                if (validacion)
                {
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución edición de un jugador: " + reloj.ElapsedMilliseconds + " ms\n");
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
        public ActionResult Delete(string id)
        {

            var model = Data.Instance.jugadorlist.Find(jugador => jugador.Nombre == id);
            return View(model);
        }

        // POST: JugadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var reloj = new Stopwatch();
                reloj.Start();
                var response = JugadorModel.Eliminar(id);
                if (response)
                {
                    reloj.Stop();
                    Data.Instance.TiempoEjecucion += ("Tiempo de ejecución eliminación de un jugador: " + reloj.ElapsedMilliseconds + " ms\n");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BusquedaNombre(string Nombre)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Nombre))
            {
                model = model.Where(jugador => jugador.Nombre.Contains(Nombre));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por nombre: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }

        public ActionResult BusquedaApellido(string Apellido)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Apellido))
            {
                model = model.Where(jugador => jugador.Apellido.Contains(Apellido));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por apellido: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaRol(string Rol)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Rol))
            {
                model = model.Where(jugador => jugador.Rol.Contains(Rol));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por rol: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaKDA(string KDA)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(KDA))
            {
                 double gr = Convert.ToDouble(KDA);
                return View(model.Where(X => X.KDA == gr));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por KDA: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaCreepScore(string CreepScore)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(CreepScore))
            {
                int gr = Convert.ToInt32(CreepScore);
                return View(model.Where(X => X.CreepScore == gr));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por CreepScore: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
        }
        public ActionResult BusquedaEquipo(string Equipo)
        {
            var reloj = new Stopwatch();
            reloj.Start();
            var model = from s in Data.Instance.jugadorlist select s;
            if (!string.IsNullOrEmpty(Equipo))
            {
                model = model.Where(jugador => jugador.Equipo.Contains(Equipo));
            }
            reloj.Stop();
            Data.Instance.TiempoEjecucion += ("Tiempo de ejecución busqueda de jugador por Equipo: " + reloj.ElapsedMilliseconds + " ms\n");
            return View(model);
            //Ojala funcioe! 
        }
    }

}

