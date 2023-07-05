using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public FavDTO GetFav()
        {
            var v = db.FavLogoTitles.Count();
            FavLogoTitle fav = new FavLogoTitle();
            fav = db.FavLogoTitles.FirstOrDefault(x => x.isDeleted == false);
            //var fav = db.FavLogoTitles.First();

            FavDTO dto = new FavDTO
            {
                ID = fav.ID,
                Title = fav.Title,
                Logo = fav.Logo,
                Fav = fav.Fav
            };

            return dto;
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            try
            {
                FavLogoTitle fav = db.FavLogoTitles.First();

                FavDTO dto = new FavDTO
                {
                    ID= fav.ID,
                    Fav= fav.Fav,
                    Logo= fav.Logo,
                    Title= model.Title
                };

                //?BUG? gera um erro quando tenta atualizar o title...
                //fav.Title = model.Title;

                if(model.Logo != null)
                {
                    fav.Logo = model.Logo;
                }

                if(model.Fav != null)
                {
                    fav.Fav = model.Fav;
                }

                db.SaveChanges();
                return dto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
