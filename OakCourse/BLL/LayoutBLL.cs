using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categorydao = new CategoryDAO();
        SocialMediaDAO socialdao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();

        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();

            dto.Categories = categorydao.GetCategories();
            socialmedialist = socialdao.GetSocialMedias();

            dto = FillSocialMedia(dto, socialmedialist);

            dto.FavDTO = favdao.GetFav();

            

            return dto;
        }

        private HomeLayoutDTO FillSocialMedia(HomeLayoutDTO dto, List<SocialMediaDTO> socialmedialist)
        {
            dto.Facebook = socialmedialist.FirstOrDefault(x => x.Link.Contains("facebook"));
            if (String.IsNullOrEmpty(dto.Facebook.Link))
                dto.Facebook.Link = "https://pt-br.facebook.com";

            dto.Twitter = socialmedialist.FirstOrDefault(x => x.Link.Contains("twitter"));
            if (String.IsNullOrEmpty(dto.Twitter.Link))
                dto.Twitter.Link = "https://twitter.com";

            dto.Instagram = socialmedialist.FirstOrDefault(x => x.Link.Contains("instagram"));
            if (String.IsNullOrEmpty(dto.Instagram.Link))
                dto.Instagram.Link = "https://www.instagram.com";


            dto.Youtube = socialmedialist.FirstOrDefault(x => x.Link.Contains("youtube"));
            if (String.IsNullOrEmpty(dto.Youtube.Link))
                dto.Youtube.Link = "https://www.youtube.com";


            dto.Linkedin = socialmedialist.FirstOrDefault(x => x.Link.Contains("linkedin"));
            if (String.IsNullOrEmpty(dto.Linkedin.Link))
                dto.Linkedin.Link = "https://www.linkedin.com";

            dto.GooglePlus = socialmedialist.FirstOrDefault(x => x.Link.Contains("google"));
            if (String.IsNullOrEmpty(dto.GooglePlus.Link))
                dto.GooglePlus.Link = "https://www.google.com";

            return dto;
        }
    }
}
