using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using NuGet.Protocol.Core.Types;
using System;
using WebAnimalApplicationNET.Models;

namespace WebAnimalApplicationNET.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly AnimalJSONRepository _repertoire;

        public AnimalController()
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"App_data", "liste_animal.json");
            _repertoire = new AnimalJSONRepository(chemin);
        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult Index()
        {
            try
            {
                List<Animal> liste = _repertoire.GetAllAnimals().ToList();
                ViewBag.res = "okey";
                return View(liste);

            }
            catch (Exception ex)
            { 
                ViewBag.res = ex.Message;
                return View(new List<Animal>());
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {

            Animal a = new Animal(animal.id, animal.type, animal.nom, animal.couleur, animal.pattes, animal.image);
            _repertoire.SetAnimal(a);
            Console.WriteLine(a);
            return RedirectToAction("Index", "Animal");
        }


        // Action pour afficher le formulaire de modification d'un animal
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Edit(int id)
        {
            Animal animal = _repertoire.GetAnimalById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // Action de traitement de la modification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Animal updatedAnimal)
        {
            if (id != updatedAnimal.id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repertoire.UpdateAnimal(updatedAnimal);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }

            return View(updatedAnimal);
        }


        //Suppression d'un animal
        private Animal GetAnimalForDelete(int id)
        {
            Animal animal = _repertoire.GetAnimalById(id);
            return animal;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            // Obtenez l'animal à partir de la base de données ou de la source de données
            Animal animal = _repertoire.GetAnimalById(id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Animal animalToDelete = GetAnimalForDelete(id);
            if (animalToDelete == null)
            {
                return NotFound();
            }
            // Effectuer la suppression de l'animal ici
            _repertoire.DeleteAnimal(id);
            return RedirectToAction("Index");
        }



    }
}
