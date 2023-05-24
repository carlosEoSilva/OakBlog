using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO:PostContext
    {
        public int AddAds(Ad ads)
        {
            try
            {
                db.Ads.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AdsDTO> GetAds()
        {
            List<Ad> list = db.Ads.Where(x => x.isDeleted == false)
                .OrderBy(x => x.AddDate).ToList();

            List<AdsDTO> dtoList = new List<AdsDTO>();

            foreach( var item in list)
            {
                AdsDTO dto = new AdsDTO
                {
                    ID = item.ID,
                    Name = item.Name,
                    Link = item.Link,
                    ImagePath = item.ImagePath
                };

                dtoList.Add(dto);
            }

            return dtoList;
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ad ads = db.Ads.First(x => x.ID == model.ID);

                string oldImagePath = ads.ImagePath;
                ads.Name = model.Name;
                ads.Link = model.Link;
                ads.Size = model.Imagesize;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;

                if (model.ImagePath != null)
                {
                    ads.ImagePath = model.ImagePath;
                }

                db.SaveChanges();

                return oldImagePath;
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            Ad ads = db.Ads.First(x => x.ID == ID);

            AdsDTO dto = new AdsDTO
            {
                ID = ads.ID,
                Name = ads.Name,
                Link = ads.Link,
                ImagePath = ads.ImagePath,
                Imagesize = ads.Size
            };

            return dto;

        }
    }
}
