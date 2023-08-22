using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Animal animal)
        //{
        //    Animal a = new Animal(animal.type, animal.nom, animal.couleur, animal.pattes);
        //    _repertoire.SetAnimal(a);
        //    Console.WriteLine(a);
        //    return RedirectToAction("Index", "Animal");
        //}
    }
}
