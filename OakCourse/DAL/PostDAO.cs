﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using(POSTDATAEntities db= new POSTDATAEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
                
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
                using(POSTDATAEntities db= new POSTDATAEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }
                
                return item.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int AddTag(PostTag item)
        {
            //note-17
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                db.PostTags.Add(item);
                db.SaveChanges();
            }
            
            return item.ID;
        }

        public List<PostDTO> GetPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> postlist = (from post in db.Posts.Where(x => x.isDeleted == false)
                                join category in db.Categories on post.CategoryID equals category.ID
                                select new
                                {
                                    ID = post.ID,
                                    Title = post.Title,
                                    categoryname = category.CategoryName,
                                    AddDate = post.AddDate
                                })
                                .Select(x => new PostDTO()
                                {
                                    Title = x.Title,
                                    ID = x.ID,
                                    CategoryName = x.categoryname,
                                    AddDate = x.AddDate
                                })
                                .OrderByDescending(x => x.AddDate)
                                .ToList();
            }

            return dtolist;
        }

        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();

            using (POSTDATAEntities db= new POSTDATAEntities())
            {
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }
            
            return dto;
        }

        public List<CommentDTO> GetAllComments()
        {
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                List<CommentDTO> dtolist = (from c in db.Comments.Where(x => x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                isapproved = c.isApproved
                            }
                       )
                       .Select(x => new CommentDTO()
                       {
                           ID = x.ID,
                           PostTitle = x.PostTitle,
                           Email = x.Email,
                           CommentContent = x.Content,
                           AddDate = x.AddDate,
                           isApproved = x.isapproved
                       })
                       .OrderBy(x => x.AddDate)
                       .ToList();
                
                return dtolist;
            }
        }

        public void DeleteComment(int ID)
        {
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);

                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;

                db.SaveChanges();
            } 
        }

        public void ApproveComment(int ID)
        {
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);

                cmt.isApproved = true;
                cmt.ApproveUserID = UserStatic.UserID;
                cmt.ApproveDate = DateTime.Now;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();

            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate
                            }
                       ).OrderBy(x => x.AddDate).ToList();

                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO
                    {
                        ID = item.ID,
                        PostTitle = item.PostTitle,
                        Email = item.Email,
                        CommentContent = item.Content,
                        AddDate = item.AddDate
                    };

                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            using (POSTDATAEntities db= new POSTDATAEntities())
            {
                var postlist = (from post in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join category in db.Categories on post.CategoryID equals category.ID
                                select new
                                {
                                    ID = post.ID,
                                    Title = post.Title,
                                    categoryname = category.CategoryName,
                                    seolink = post.SeoLink,
                                    AddDate = post.AddDate
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();

                

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
            }
            return dtolist;
        }

        public int GetCommentCount()
        {
            int qtdcomments;

            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                qtdcomments = db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }

            return qtdcomments;
        }

        public int GetMessageCount()
        {
            int qtdmessages;

            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                qtdmessages = db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }

            return qtdmessages;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using(POSTDATAEntities db= new POSTDATAEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();

            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);

                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.Language = post.LanguageName;
                dto.Notification = post.Notification;
                dto.SeoLink = post.SeoLink;
                dto.Slider = post.Slider;
                dto.Area1 = post.Area1;
                dto.Area2 = post.Area2;
                dto.Area3 = post.Area3;
                dto.CategoryID = post.CategoryID;
            }

            return dto;
        }

        public List<PostImageDTO> GetPostImagesWithPostID(int PostID)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();

            using (POSTDATAEntities db= new POSTDATAEntities())
            {
                List<PostImage> list = db.PostImages
                .Where(x => x.isDeleted == false && x.PostID == PostID)
                .ToList();

                foreach (var item in list)
                {
                    PostImageDTO dto = new PostImageDTO
                    {
                        ID = item.ID,
                        ImagePath = item.ImagePath
                    };

                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public List<PostTag> GetPostTagsWithPostID(int PostID)
        {
            List<PostTag> list = new List<PostTag>();

            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                list= db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
            }

            return list;
        }

        public void UpdatePost(PostDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == model.ID);

                post.Title = model.Title;
                post.Area1 = model.Area1;
                post.Area2 = model.Area2;
                post.Area3 = model.Area3;
                post.CategoryID = model.CategoryID;
                post.LanguageName = model.Language;
                post.LastUpdateDate = DateTime.Now;
                post.LasUpdateUserID = UserStatic.UserID;
                post.Notification = model.Notification;
                post.PostContent = model.PostContent;
                post.SeoLink = model.SeoLink;
                post.ShortContent = model.ShortContent;
                post.Slider = model.Slider;

                post.T_User.AddDate = DateTime.Now;
                post.T_User1.AddDate = DateTime.Now;

                db.SaveChanges();
            }
        }

        public string DeletePostImage(int ID)
        {
            try
            {
                string imagepath;

                using(POSTDATAEntities db= new POSTDATAEntities())
                {
                    PostImage img = db.PostImages.First(x => x.ID == ID);
                    imagepath = img.ImagePath;

                    img.isDeleted = true;
                    img.DeletedDate = DateTime.Now;
                    img.LastUpdateDate = DateTime.Now;
                    img.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                
                return imagepath;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();

            using (POSTDATAEntities db= new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);

                post.isDeleted = true;
                post.DeletedDate = DateTime.Now;
                post.LastUpdateDate = DateTime.Now;
                post.LasUpdateUserID = UserStatic.UserID;

                db.SaveChanges();

                List<PostImage> imagelist = db.PostImages.Where(x => x.PostID == ID).ToList();

                foreach (var item in imagelist)
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
            }
            return dtolist;
        }

        public void DeleteTags(int PostID)
        {
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                List<PostTag> list = db.PostTags
                .Where(x => x.isDeleted == false && x.PostID == PostID)
                .ToList();

                foreach (var item in list)
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
}
