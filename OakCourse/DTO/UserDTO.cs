using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class  UserDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please fill user name area!")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please fill password area!")]
        public string Password { get; set; }
        
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public bool isAdmin { get; set; }
    }
}
