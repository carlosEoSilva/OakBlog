using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GeneralDTO
    {
        public List<PostDTO> SliderPost { get; set; }
        public List<PostDTO> PopularPost { get; set; }
        public List<PostDTO> MostViewedPost { get; set; }
        public List<PostDTO> BreakingNews{ get; set; }
        public List<PostDTO> BreakingPosts{ get; set; }
        public List<VideoDTO> Videos { get; set; }
        public List<AdsDTO> Adslist { get; set; }
        public PostDTO PostDetail { get; set; }
    }
}
