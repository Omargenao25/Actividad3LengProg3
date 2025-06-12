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
        [HttpGet]
        public ActionResult Editar(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                TempData["Error"] = "Matrícula inválida.";
                return RedirectToAction("Lista");
            }

            var estudiante = estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante == null)
            {
                TempData["Error"] = "Estudiante no encontrado.";
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
                TempData["Error"] = "No se encontró el estudiante a editar.";
                return RedirectToAction("Lista");
            }

            if (estudiante.EstaBecado && (!estudiante.PorcentajeBeca.HasValue || estudiante.PorcentajeBeca <= 0))
            {
                ModelState.AddModelError("PorcentajeBeca", "Debe especificar un porcentaje de beca válido.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = carreras;
                ViewBag.Generos = generos;
                ViewBag.Turnos = turnos;
                ViewBag.TiposIngreso = tiposIngreso;

                return View("Index", estudiante);
            }

            estudianteExistente.NombreCompleto = estudiante.NombreCompleto;
            estudianteExistente.Carrera = estudiante.Carrera;
            estudianteExistente.CorreoInstitucional = estudiante.CorreoInstitucional;
            estudianteExistente.Telefono = estudiante.Telefono;
            estudianteExistente.FechaNacimiento = estudiante.FechaNacimiento;
            estudianteExistente.Genero = estudiante.Genero;
            estudianteExistente.Turno = estudiante.Turno;
            estudianteExistente.TipoIngreso = estudiante.TipoIngreso;
            estudianteExistente.EstaBecado = estudiante.EstaBecado;
            estudianteExistente.PorcentajeBeca = estudiante.PorcentajeBeca;
            estudianteExistente.AceptaTerminos = estudiante.AceptaTerminos;

            TempData["Mensaje"] = "Estudiante actualizado exitosamente.";
            return RedirectToAction("Lista");
        }



        public ActionResult Eliminar(string matricula)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante != null)
            {
                estudiantes.Remove(estudiante);
                TempData["Mensaje"] = "Estudiante eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "Estudiante no encontrado.";
            }

            return RedirectToAction("Lista");
        }

    }
    }

