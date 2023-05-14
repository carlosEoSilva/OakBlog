using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class  SocialMediaDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage= "Please fill name area!")]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please fill link area!")]
        public string Link { get; set; }

        public HttpPostedFileBase SocialImage { get; set; }
    }
}
