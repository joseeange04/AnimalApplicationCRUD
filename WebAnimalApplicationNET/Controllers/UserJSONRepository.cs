using Newtonsoft.Json;
using WebAnimalApplicationNET.Models;

namespace WebAnimalApplicationNET.Controllers
{
    public class UserJSONRepository
    {
        private readonly string _fichier;
        public UserJSONRepository(string fichier)
        {
            _fichier = fichier;
        }

        public List<Login> GetAllUsers()
        {
            using (StreamReader reader = new StreamReader(_fichier))
            {
                List<Login> listes = JsonConvert.DeserializeObject<List<Login>>(reader.ReadToEnd());
                return listes;
            }
        }
    }
}
