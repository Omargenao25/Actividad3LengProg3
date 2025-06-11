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
        private List<string> tiposIngreso = new List<string> { "Regular", "transferido", "Reingreso" };

        public ActionResult Index()
        {
            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View();
        }

        public ActionResult Registrar(EstudianteViewModel estudiante)
        {
            if (ModelState.IsValid)
            {
                // Verificar que la matrícula no exista
                if (estudiantes.Any(e => e.Matricula == estudiante.Matricula))
                {
                    ModelState.AddModelError("Matricula", "La matrícula ya existe.");
                }
                else
                {
                    estudiantes.Add(estudiante);
                    return RedirectToAction("Lista");
                }
                if (ModelState.IsValid)
                {
                    estudiantes.Add(estudiante);
                    TempData["Mensaje"] = "Estudiante registrado exitosamente";
                    return RedirectToAction("Lista");
                }
            }
            // Si hay errores, volver a la vista y cargar listas
            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View("Index", estudiante);
        }

        public ActionResult Lista()
        {
            return View(estudiantes);
        }


        public ActionResult Editar(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                return RedirectToAction("Lista");
            }

            var estudiante = estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante == null)
            {
                TempData["Error"] = "Estudiante no encontrado";
                return RedirectToAction("Lista");
            }

            ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View("Index", estudiante);
        }

        [HttpPost]
        public ActionResult Editar(EstudianteViewModel estudiante)
        {
            
            var estudianteExistente = estudiantes.FirstOrDefault(e => e.Matricula == estudiante.Matricula);
            if (estudianteExistente == null)
            {
                ModelState.AddModelError("Matricula", "No se encontró el estudiante a editar");
            }

         
            if (estudiante.EstaBecado && (!estudiante.PorcentajeBeca.HasValue || estudiante.PorcentajeBeca <= 0))
            {
                ModelState.AddModelError("PorcentajeBeca", "Debe especificar un porcentaje de beca válido");
            }

            if (ModelState.IsValid)
            {
            
                var index = estudiantes.FindIndex(e => e.Matricula == estudiante.Matricula);
                if (index >= 0)
                {
                    estudiantes[index] = estudiante;
                    TempData["Mensaje"] = "Estudiante actualizado exitosamente";
                    return RedirectToAction("Lista");
                }
            }

            // Si hay errores, volver a mostrar el formulario
             ViewBag.Carreras = carreras;
            ViewBag.Generos = generos;
            ViewBag.Turnos = turnos;
            ViewBag.TiposIngreso = tiposIngreso;
            return View("Index", estudiante);
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

