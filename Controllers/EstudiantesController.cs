using Actividad3LengProg3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Actividad3LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private static List<EstudianteViewModel> estudiantes = new List<EstudianteViewModel>();

       // Lista de carreras, turnos, tipos de ingreso, etc.
        private List<string> carreras = new List<string> { "Ingeniería", "Administración", "Sistemas", "Contabilidad" };
        private List<string> generos = new List<string> { "Masculino", "Femenino", "Otro" };
        private List<string> turnos = new List<string> { "Mañana", "Tarde", "Noche" };
        private List<string> tiposIngreso = new List<string> { "Regular", "Refuerzo", "Reingreso" };

        public ActionResult Index()
        {
            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View();
        }

        // GET: EstudiantesController/Details/5
        public ActionResult Registrar(EstudianteViewModel estudiante)
        {
            if (estudiante.EstaBecado == false)
            {
                estudiante.PorcentajeBeca = null;
            }

            if (ModelState.IsValid)
            {
                estudiantes.Add(estudiante);
                return RedirectToAction("Lista");
            }

            ViewBag.Carreras = new List<string> { "Ingeniería en Sistemas", "Contabilidad", "Administración", "Psicología" };
            ViewBag.Turnos = new List<string> { "Mañana", "Tarde", "Noche" };
            ViewBag.TipoIngreso = new List<string> { "Nuevo Ingreso", "Transferencia", "Reingreso" };
            return View("Index", estudiante);
        }

        // GET: EstudiantesController/Create
        public ActionResult Lista()
        {
            return View(estudiantes);
        }

        // POST: EstudiantesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(string matricula)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante == null) return NotFound();

            ViewBag.Carreras = new List<string> { "Ingeniería en Sistemas", "Contabilidad", "Administración", "Psicología" };
            ViewBag.Turnos = new List<string> { "Mañana", "Tarde", "Noche" };
            ViewBag.TipoIngreso = new List<string> { "Nuevo Ingreso", "Transferencia", "Reingreso" };
            return View(estudiante);
        }

        // GET: EstudiantesController/Edit/5
        public ActionResult Editar(EstudianteViewModel estudiante)
        {
            var existente = estudiantes.FirstOrDefault(e => e.Matricula == estudiante.Matricula);
            if (existente == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = new List<string> { "Ingeniería en Sistemas", "Contabilidad", "Administración", "Psicología" };
                ViewBag.Turnos = new List<string> { "Mañana", "Tarde", "Noche" };
                ViewBag.TipoIngreso = new List<string> { "Nuevo Ingreso", "Transferencia", "Reingreso" };
                return View(estudiante);
            }

            estudiantes.Remove(existente);
            estudiantes.Add(estudiante);
            return RedirectToAction("Lista");
        }

        // POST: EstudiantesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Eliminar(string matricula)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante != null)
            {
                estudiantes.Remove(estudiante);
            }
            return RedirectToAction("Lista");
        }
        
        }
    }

