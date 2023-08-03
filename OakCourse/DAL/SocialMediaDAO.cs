using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class SocialMediaDAO 
    {
        public int AddSocialMedia(SocialMedia social)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.SocialMedias.Add(social);
                    db.SaveChanges();
                    return social.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<SocialMedia> list = db.SocialMedias.Where(x => x.isDeleted == false).ToList();
                List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();

                foreach (var item in list)
                {
                    SocialMediaDTO dto = new SocialMediaDTO()
                    {
                        Name = item.Name,
                        Link = item.Link,
                        ImagePath = item.ImagePath,
                        ID = item.ID
                    };
                   
                    dtolist.Add(dto);
                }

                return dtolist;
            }
                
        }

        public SocialMediaDTO getSocialMediaWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                SocialMedia socialmedia = new SocialMedia();
                socialmedia = db.SocialMedias.First(x => x.ID == ID);

                SocialMediaDTO dto = new SocialMediaDTO()
                {
                    ID = socialmedia.ID,
                    Name = socialmedia.Name,
                    Link = socialmedia.Link,
                    ImagePath = socialmedia.ImagePath
                };

                return dto;
            }
                
        }

        public string DeleteSocialMedia(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == ID);
                string imagepath = social.ImagePath;

                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
                return imagepath;
            }
                
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    SocialMedia social = db.SocialMedias.First(x => x.ID == model.ID);
                    string oldImagePath = social.ImagePath;
                    social.Name = model.Name;
                    social.Link = model.Link;

                    if (model.ImagePath != null)
                    {
                        social.ImagePath = model.ImagePath;
                    }

                    social.LastUpdateDate = DateTime.Now;
                    social.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                    return oldImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
