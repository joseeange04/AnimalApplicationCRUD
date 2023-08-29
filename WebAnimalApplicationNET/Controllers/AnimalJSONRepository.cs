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
        private int _nextId = 1; // Variable pour gérer l'auto-incrément

        public AnimalJSONRepository(string fichier)
        {
            _fichier = fichier;

            List<Animal> animals = GetAllAnimals();
            if (animals.Any())
            {
                _nextId = animals.Max(a => a.id) + 1;
            }
        }

        //public IEnumerable<Animal> GetAllAnimals()
        //{
        //    using (StreamReader reader = new StreamReader(_fichier))
        //    {
        //        string json = reader.ReadToEnd();
        //        List<Animal> listes = JsonConvert.DeserializeObject<List<Animal>>(json);
        //        return listes;
        //    }
        //}
        public List<Animal> GetAllAnimals() 
        {
            using (StreamReader reader = new StreamReader(_fichier))
            {
                string json = reader.ReadToEnd();
                List<Animal> listes = JsonConvert.DeserializeObject<List<Animal>>(json);
                return listes;
            }
        }
        public void SetAnimal(Animal animal)
        {
            List<Animal> listes;
            using(StreamReader reader = new StreamReader(_fichier))
            {

                string json = reader.ReadToEnd();
                listes = JsonConvert.DeserializeObject<List<Animal>>(json);
            }
            using (StreamWriter writer = new StreamWriter(_fichier)) 
            {
                // Attribution d'un nouvel ID
                animal.id = GetNextId(listes);
                listes.Add(animal);
                writer.Write(JsonConvert.SerializeObject(listes));
            }
        }

        // Méthode pour obtenir le prochain ID
        private int GetNextId(List<Animal> animals)
        {
            if (animals.Count == 0)
            {
                return 1;
            }

            int maxId = animals.Max(a => a.id);
            return maxId + 1;
        }

        public void UpdateAnimal(Animal updatedAnimal)
        {
            List<Animal> listes = GetAllAnimals();

            Animal existingAnimal = listes.FirstOrDefault(a => a.id == updatedAnimal.id);

            if (existingAnimal != null)
            {
                // Mettre à jour les informations de l'animal existant
                existingAnimal.type = updatedAnimal.type;
                existingAnimal.nom = updatedAnimal.nom;
                existingAnimal.pattes = updatedAnimal.pattes;
                existingAnimal.couleur = updatedAnimal.couleur;

                // Enregistrement de la liste mise à jour
                using (StreamWriter writer = new StreamWriter(_fichier))
                {
                    writer.Write(JsonConvert.SerializeObject(listes));
                }
            }
            else
            {
                throw new Exception("Animal not found");
            }
        }

        // ... (autres méthodes et membres)

        public Animal GetAnimalById(int id)
        {
            List<Animal> listes = GetAllAnimals();
            return listes.FirstOrDefault(a => a.id == id);
        }

        public void SetAnimals(IEnumerable<Animal> animals)
        {
            using (StreamWriter writer = new StreamWriter(_fichier))
            {
                string json = JsonConvert.SerializeObject(animals);
                writer.Write(json);
            }
        }

        //Supression d'un animal
        public void DeleteAnimal(int id)
        {
            List<Animal> animals = new List<Animal>();

            using (StreamReader reader = new StreamReader(_fichier))
            {
                string json = reader.ReadToEnd();
                animals = JsonConvert.DeserializeObject<List<Animal>>(json);
            }

            Animal animalToDelete = animals.Find(a => a.id == id);

            if (animalToDelete != null)
            {
                animals.Remove(animalToDelete);
                SetAnimals(animals);
            }
            else
            {
                throw new ArgumentException("Animal not found");
            }
        }
    }
}