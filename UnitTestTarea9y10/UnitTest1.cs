using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ComunidadDePracticaMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tarea9y10Inge.Controllers;

namespace UnitTestTarea9y10
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexTest_NotNull()
        {
            CalcularNotaController controller = new CalcularNotaController();
            ActionResult result = controller.Index() as ActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CalcularNotaView_NotNull()
        {
            CalcularNotaController controller = new CalcularNotaController();
            FormularioNotaModel model = new FormularioNotaModel();
            ActionResult result = controller.CalcularNota(model) as ActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ModeloValido_ModeloBien()
        {
            //Testea con todos las listas inicializadas
            CalcularNotaController controller = new CalcularNotaController();
            FormularioNotaModel model = new FormularioNotaModel();
            model.NotasGenericas = new List<Nota>();
            model.Examenes = new List<Nota>();
            model.Labs = new List<Nota>();
            model.Investigacion = new List<Nota>();
            Nota nota = new Nota();
            nota.Valor = 1;
            for (int i = 0; i < 10; ++i) {
                model.NotasGenericas.Add(nota);
            }
            for (int i = 0; i < 2; ++i)
            {
                model.Examenes.Add(nota);
            }
            for (int i = 0; i < 10; ++i)
            {
                model.Labs.Add(nota);
            }
            for (int i = 0; i < 4; ++i)
            {
                model.Investigacion.Add(nota);
            }


            bool resultado = controller.ModeloValido(model);
            Assert.IsTrue(resultado);

            //Testea que se puedan poner mas elementos a notasGenericas
            for (int i = 0; i < 20; ++i)
            {
                model.NotasGenericas.Add(nota);
            }
            bool resultado2 = controller.ModeloValido(model);
            Assert.IsTrue(resultado2);
        }

        [TestMethod]
        public void ModeloValido_ModeloMal()
        {
            CalcularNotaController controller = new CalcularNotaController();
            FormularioNotaModel model = new FormularioNotaModel();
            //Testea con todo null
            bool resultado = controller.ModeloValido(model);
            Assert.IsFalse(resultado);

            //Testea con todo no null
            model.NotasGenericas = new List<Nota>();
            model.Examenes = new List<Nota>();
            model.Labs = new List<Nota>();
            model.Investigacion = new List<Nota>();
            bool resultado2 = controller.ModeloValido(model);
            Assert.IsFalse(resultado2);

            //Testea poniendo tamaños de listas no correctos
            Nota nota = new Nota();
            nota.Valor = 1;
            for (int i = 0; i < 10; ++i)
            {
                model.NotasGenericas.Add(nota);
            }
            for (int i = 0; i < 2; ++i)
            {
                model.Examenes.Add(nota);
            }
            bool resultado3 = controller.ModeloValido(model);
            Assert.IsFalse(resultado3);
        }

        [TestMethod]
        public void SacarNotaFinalTest_ResultadoBien()
        {
            CalcularNotaController controller = new CalcularNotaController();
            FormularioNotaModel model = new FormularioNotaModel();
            model.NotasGenericas = new List<Nota>();
            model.Examenes = new List<Nota>();
            model.Labs = new List<Nota>();
            model.Investigacion = new List<Nota>();
            
            for (int i = 0; i < 10; ++i)
            {
                Nota nota = new Nota();
                nota.Valor = 70;
                model.NotasGenericas.Add(nota);
            }
            for (int i = 0; i < 2; ++i)
            {
                Nota nota = new Nota();
                nota.Valor = 80;
                model.Examenes.Add(nota);
            }
            for (int i = 0; i < 10; ++i)
            {
                Nota nota = new Nota();
                nota.Valor = 90;
                model.Labs.Add(nota);
            }
            for (int i = 0; i < 4; ++i)
            {
                Nota nota = new Nota();
                nota.Valor = 100;
                model.Investigacion.Add(nota);
            }
            model.Foros = 90;


            double resultado = controller.SacarNotaFinal(model).NotaFinal;

            Assert.AreEqual(86, (int)resultado);
        }

    }
}
