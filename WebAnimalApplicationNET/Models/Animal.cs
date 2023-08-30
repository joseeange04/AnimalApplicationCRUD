namespace WebAnimalApplicationNET.Models
{
    public class Animal
    {
        public int id { get; set; }
        public string type { get; set; }
        public string nom { get; set; }
        public string couleur { get; set; }
        public int pattes { get; set; }
        public string image { get; set; }
        

        public Animal() {
        }
        public Animal(int i, string t, string n, string c, int p, string img)
        {
            id = i;
            type = t;
            nom = n;
            couleur = c;
            pattes = p;
            image = img;
        }
    }
}
