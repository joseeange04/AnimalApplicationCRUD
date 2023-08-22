namespace WebAnimalApplicationNET.Models
{
    public class Animal
    {
        public string type { get; set; }
        public string nom { get; set; }
        public string couleur { get; set; }
        public int pattes { get; set; }

        public Animal(string t, string n, string c, int p)
        {
            type = t;
            nom = n;
            couleur = c;
            pattes = p;
        }
    }
}
