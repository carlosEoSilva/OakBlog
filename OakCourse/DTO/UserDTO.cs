using System.ComponentModel.DataAnnotations;
using System.Web;

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

        [Required(ErrorMessage = "Please fill Name area!")]
        public string Name { get; set; }

        public bool isAdmin { get; set; }
        
        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}
