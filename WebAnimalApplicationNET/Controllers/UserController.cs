using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WebAnimalApplicationNET.Models;

namespace WebAnimalApplicationNET.Controllers
{
    public class UserController : Controller
    {
        private readonly UserJSONRepository _userRepository;

        public UserController()
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_data", "users.json");
            _userRepository = new UserJSONRepository(chemin);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Login());
        }

        [HttpPost]
        public IActionResult Index(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                List<Login> users = _userRepository.GetAllUsers();

                // Vérification des informations d'identification ici (exemple simplifié)
                Login user = users.Find(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

                if (user != null)
                {
                    // Connexion réussie
                    loginModel.Message = "Connexion réussie !";
                }
                else
                {
                    // Identifiant ou mot de passe incorrect
                    loginModel.Message = "Identifiant ou mot de passe incorrect.";
                }

                return View(loginModel);
            }

            return View(loginModel);
        }
    }
}
