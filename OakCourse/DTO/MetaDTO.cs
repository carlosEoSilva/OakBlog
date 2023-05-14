using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class MetaDTO
    {
        public int MetaID { get; set; }
        
        public string Name { get; set; }
        
        [Required(ErrorMessage= "Please fill the content area")]
        public string MetaContent { get; set; }
    }
}
