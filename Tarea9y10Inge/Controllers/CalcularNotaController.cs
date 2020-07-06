using ComunidadDePracticaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Tarea9y10Inge.Controllers
{
    public class CalcularNotaController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["Desglose"] != null) {
                ViewBag.Desglose = TempData["Desglose"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult CalcularNota(FormularioNotaModel model)
        {
            
            if (ModeloValido(model))
            {
                DesgloseNotasModel desgloseDeNotas = SacarNotaFinal(model);
                ViewBag.Message = "Your application description page.";
                @TempData["Desglose"] = desgloseDeNotas;
            }
            return RedirectToAction("Index", "CalcularNota");
        }

        public DesgloseNotasModel SacarNotaFinal(FormularioNotaModel model) {
            DesgloseNotasModel desglose = new DesgloseNotasModel();

            desglose.NotasIndividuales = model;

            //calcula el porcentaje para las notas de quices, tareas, clases...
            //se promedia cada uno y se multiplica por 0.2 para sacar su porcentaje.
            double sumaNotasGenericas = 0;
            int numeroNotasGenericas = model.NotasGenericas.Count();
            if (numeroNotasGenericas > 30) {
                numeroNotasGenericas = 30;
            }
            for (int i = 0; i < numeroNotasGenericas; i++) {
                sumaNotasGenericas += model.NotasGenericas[i].Valor;
            }
            desglose.CantidadNotasGenericas = numeroNotasGenericas;
            desglose.PorcentajeNotasGenericas = (sumaNotasGenericas / numeroNotasGenericas) * 0.2;

            //Calcula el porcentaje para foros 20%.
            desglose.PorcentajeForos = model.Foros * 0.2;

            //Calcula el porcentaje para examenes cada uno con un 10%.
            desglose.PorcentajeExamenes = (model.Examenes[0].Valor + model.Examenes[1].Valor) * 0.1;

            //Calcula el porcentaje para labs 20% mediante promedio de cada uno. 
            double sumaNotasLabs = 0;
            for (int i = 0; i < 10; i++) {
                sumaNotasLabs += model.Labs[i].Valor;
            }
            desglose.PorcentajeLabs = (sumaNotasLabs / 10) * 0.2;

            //Calcula el porcentaje de la investigacion 20% (20%, 30%, 20%, 30% por individual)
            desglose.PorcentajeInvestigacion = ((model.Investigacion[0].Valor * 0.2) + (model.Investigacion[1].Valor * 0.3)
                                                + (model.Investigacion[2].Valor * 0.2) + (model.Investigacion[3].Valor * 0.3)) * 0.2;

            desglose.NotaFinal = desglose.PorcentajeNotasGenericas + desglose.PorcentajeForos + desglose.PorcentajeExamenes 
                                +  desglose.PorcentajeLabs + desglose.PorcentajeInvestigacion;

            return desglose;
        }

        public bool ModeloValido(FormularioNotaModel model) {
            if (model == null || model.NotasGenericas == null || model.Examenes == null || model.Labs == null || model.Investigacion == null)
            {
                return false;
            }
            else if (model.NotasGenericas.Count > 30 || model.Examenes.Count != 2 || model.Labs.Count != 10 || model.Investigacion.Count != 4)
            {
                return false;
            }
            else {
                return true;
            }
        }

    }   
} 