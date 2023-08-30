using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WebAnimalApplicationNET.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

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

        private string GenerateSecurityToken()
        {
            const int tokenLength = 32; // Longueur du jeton en octets
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Créer un générateur de nombres aléatoires
            Random random = new Random();

            // Générer un jeton en choisissant des caractères aléatoires de allowedChars
            char[] tokenChars = new char[tokenLength];
            for (int i = 0; i < tokenLength; i++)
            {
                tokenChars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(tokenChars);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult List()
        {
            try
            {
                List<Login> liste = _userRepository.GetAllUsers().ToList();
                ViewBag.res = "okey";
                return View(liste);

            }
            catch (Exception ex)
            {
                ViewBag.res = ex.Message;
                return View(new List<Login>());
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(new Login());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                List<Login> users = _userRepository.GetAllUsers();

                // Vérification des informations d'identification ici (exemple simplifié)
                Login user = users.FirstOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

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
                return RedirectToAction("Index", "Animal");

            }
            return View(loginModel);
        }
    }
}
