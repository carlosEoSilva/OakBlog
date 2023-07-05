using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAO : PostContext
    {
        public int AddPost(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return post.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                db.PostImages.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddTag(PostTag item)
        {
            db.PostTags.Add(item);
            db.SaveChanges();
            return item.ID;
        }

        public List<PostDTO> GetPosts()
        {
            var postlist = (from post in db.Posts.Where(x => x.isDeleted == false)
                            join category in db.Categories on post.CategoryID equals category.ID
                            select new
                            {
                                ID = post.ID,
                                Title = post.Title,
                                categoryname = category.CategoryName,
                                AddDate= post.AddDate
                            }).OrderByDescending(x => x.AddDate).ToList();
            
            List<PostDTO> dtolist = new List<PostDTO>();

            foreach(var item in postlist)
            {
                PostDTO dto = new PostDTO
                {
                    Title = item.Title,
                    ID = item.ID,
                    CategoryName = item.categoryname,
                    AddDate = item.AddDate
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetHotNews()
        {
            var postlist = (from post in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                            join category in db.Categories on post.CategoryID equals category.ID
                            select new
                            {
                                ID = post.ID,
                                Title = post.Title,
                                categoryname = category.CategoryName,
                                seolink= post.SeoLink,
                                AddDate = post.AddDate
                            }).OrderByDescending(x => x.AddDate).Take(8).ToList();

            List<PostDTO> dtolist = new List<PostDTO>();

            foreach (var item in postlist)
            {
                PostDTO dto = new PostDTO
                {
                    Title = item.Title,
                    ID = item.ID,
                    CategoryName = item.categoryname,
                    AddDate = item.AddDate
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public PostDTO GetPostWithID(int ID)
        {
            Post post = db.Posts.First(x => x.ID == ID);

            PostDTO dto = new PostDTO
            {
                ID = post.ID,
                Title = post.Title,
                ShortContent= post.ShortContent,
                PostContent= post.PostContent,
                Language= post.LanguageName,
                Notification= post.Notification,
                SeoLink= post.SeoLink,
                Slider= post.Slider,
                Area1= post.Area1,
                Area2= post.Area2,
                Area3= post.Area3,
                CategoryID= post.CategoryID
            };

            return dto;
        }

        public List<PostImageDTO> GetPostImagesWithPostID(int PostID)
        {
            List<PostImage> list = db.PostImages
                .Where(x => x.isDeleted == false && x.PostID == PostID)
                .ToList();

            List<PostImageDTO> dtolist = new List<PostImageDTO>();

            foreach(var item in list)
            {
                PostImageDTO dto = new PostImageDTO
                {
                    ID = item.ID,
                    ImagePath = item.ImagePath
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostTag> GetPostTagsWithPostID(int PostID)
        {
            return db.PostTags
                .Where(x => x.isDeleted == false && x.PostID == PostID)
                .ToList();
        }

        public void UpdatePost(PostDTO model)
        {
            Post post = db.Posts.First(x => x.ID == model.ID);

            post.Title = model.Title;
            post.Area1 = model.Area1;
            post.Area2= model.Area2;
            post.Area3= model.Area3;
            post.CategoryID= model.CategoryID;
            post.LanguageName= model.Language;
            post.LastUpdateDate= DateTime.Now;
            post.LasUpdateUserID= UserStatic.UserID;
            post.Notification= model.Notification;
            post.PostContent= model.PostContent;
            post.SeoLink= model.SeoLink;
            post.ShortContent= model.ShortContent;
            post.Slider= model.Slider;

            post.T_User.AddDate = DateTime.Now.Date;
            post.T_User1.AddDate = DateTime.Now.Date;

            db.SaveChanges();
        }

        public string DeletePostImage(int ID)
        {
            try
            {
                PostImage img = db.PostImages.First(x => x.ID == ID);
                string imagepath = img.ImagePath;

                img.isDeleted = true;
                img.DeletedDate = DateTime.Now;
                img.LastUpdateDate = DateTime.Now;
                img.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
                return imagepath;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            Post post = db.Posts.First(x => x.ID == ID);

            post.isDeleted = true;
            post.DeletedDate = DateTime.Now;
            post.LastUpdateDate = DateTime.Now;
            post.LasUpdateUserID = UserStatic.UserID;

            db.SaveChanges();

            List<PostImage> imagelist = db.PostImages.Where(x => x.PostID == ID).ToList();
            List<PostImageDTO> dtolist = new List<PostImageDTO>();

            foreach(var item in imagelist)
            {
                PostImageDTO dto = new PostImageDTO();

                dto.ImagePath = item.ImagePath;
                
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserID;
                
                dtolist.Add(dto);
            }
            db.SaveChanges();
            return dtolist;
        }

        public void DeleteTags(int PostID)
        {
            List<PostTag> list = db.PostTags
                .Where(x => x.isDeleted == false && x.PostID == PostID)
                .ToList();

            foreach(var item in list)
            {
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserID;
            }

            db.SaveChanges();
        }


    }
}
