using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAnimalApplicationNET.Models
{
    public class Login
    {
        public int UserId { get; set; }
        [DisplayName("Identifiant *")]
        [Required(ErrorMessage ="Udentifiant obligatoire")]
        public string   UserName { get; set; }
        [DisplayName("Mot magique *")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mot de passe obligatoire")]
        public string Password { get; set; }
        public string Message { get; set; }

    }
}
