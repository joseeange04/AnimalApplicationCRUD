using WebAnimalApplicationNET.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WebAnimalApplicationNET.Controllers
{
    public class AnimalJSONRepository
    {
        // Chemin vers le fichier json
        private readonly string _fichier;

        public AnimalJSONRepository(string fichier)
        {
            _fichier = fichier;
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            using (StreamReader reader = new StreamReader(_fichier))
            {
                string json = reader.ReadToEnd();
                List<Animal> listes = JsonConvert.DeserializeObject<List<Animal>>(json);
                return listes;
            }
        }
    }
}