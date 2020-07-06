using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ComunidadDePracticaMVC.Models;

namespace ComunidadDePracticaMVC.Models
{

    public class DesgloseNotasModel { 
        public FormularioNotaModel NotasIndividuales { get; set; }

        public int CantidadNotasGenericas { get; set; }

        public double PorcentajeNotasGenericas { get; set; }

        public double PorcentajeForos { get; set; }

        public double PorcentajeExamenes { get; set; }

        public double PorcentajeLabs { get; set; }

        public double PorcentajeInvestigacion { get; set; }

        public double NotaFinal { get; set; }
    }


    public class FormularioNotaModel
    {
        [Required]
        [Display(Name = "Notas generales 20% (Comprobaciones de lectura, exámenes cortos, tareas y clases virtuales): ")]
        public IList<Nota> NotasGenericas{ get; set; }

        [Required(ErrorMessage = "La nota es requerida.")]
        [RegularExpression("^[0-9]*$",
         ErrorMessage = "Solo caracteres del 0-9 son permitidos.")]
        [Range(0, 100, ErrorMessage = "El valor debe de estar entre 0 y 100.")]
        public int Foros { get; set; }

        [Required]
        [Display(Name = "Notas de examenes 20%: ")]
        public IList<Nota> Examenes { get; set; }

        [Required]
        [Display(Name = "Notas de labs 20%: ")]
        public IList<Nota> Labs { get; set; }

        [Required]
        [Display(Name = "Notas de la investigación 20%: ")]
        public IList<Nota> Investigacion { get; set; }

    }

    public class Nota {
        [Required (ErrorMessage = "La nota es requerida.")]
        [RegularExpression("^[0-9]*$",
         ErrorMessage = "Solo caracteres del 0-9 son permitidos.")]
        [Range(0, 100, ErrorMessage = "El valor debe de estar entre 0 y 100.")]
        public int Valor { get; set; }
    }

}