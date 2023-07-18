using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();


        public bool AddPost(PostDTO model)
        {
            Post post = new Post
            {
                Title = model.Title,
                PostContent = model.PostContent,
                ShortContent = model.ShortContent,
                Slider = model.Slider,
                Area1 = model.Area1,
                Area2 = model.Area2,
                Area3 = model.Area3,
                Notification = model.Notification,
                CategoryID = model.CategoryID,
                SeoLink = SeoLink.GenerateUrl(model.Title),
                LanguageName = model.Language,
                AddDate = DateTime.Now,
                AddUserID = UserStatic.UserID,
                LasUpdateUserID = UserStatic.UserID,
                LastUpdateDate = DateTime.Now
            };

            int ID = dao.AddPost(post);

            LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.post, ID);

            SavePostImage(model.PostImages, ID);

            AddTag(model.TagText, ID);

            return true;

        }

        public CountDTO GetAllCounts()
        {
            return dao.GetAllCounts();
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public void DeleteComment(int ID)
        {
            dao.DeleteComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentDelete, General.TableName.Comment, ID);
        }

        public void ApproveComment(int ID)
        {
            dao.ApproveComment(ID);
            LogDAO.AddLog(General.ProcessType.CommentApprove, General.TableName.Comment, ID);
        }

        public List<CommentDTO> GetComments()
        {
            return dao.GetComments();
        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        void AddTag(string tagText, int PostID)
        {
            string[] tags;
            tags = tagText.Split(',');
            List<PostTag> taglist = new List<PostTag>();

            foreach(var item in tags)
            {
                PostTag tag = new PostTag
                {
                    PostID = PostID,
                    TagContent = item,
                    AddDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUserID = UserStatic.UserID
                };

                taglist.Add(tag);
            }

            foreach(var item in taglist)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);

            }
        }

        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment
            {
                PostID= model.PostID,
                NameSurname= model.Name,
                Email= model.Email,
                CommentContent= model.Message,
                AddDate= DateTime.Now,
                isApproved= false,
                isDeleted= false
            };

            dao.AddComment(comment);
            return true;
        }

        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            List<PostImage> imagelist = new List<PostImage>();

            foreach(var item in list)
            {
                PostImage image = new PostImage
                {
                    PostID = PostID,
                    ImagePath = item.ImagePath,
                    AddDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUserID = UserStatic.UserID
                };

                imagelist.Add(image);
            }

            foreach (var item in imagelist)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto= dao.GetPostWithID(ID);
            dto.PostImages = dao.GetPostImagesWithPostID(ID);
            List<PostTag> taglist = dao.GetPostTagsWithPostID(ID);
            string tagValue = "";

            if(taglist.Count > 0)
            {
                foreach (var item in taglist)
                {
                    tagValue += item.TagContent + ",";
                }

                //remover a última vírgula
                tagValue = tagValue.Remove(tagValue.Length - 1, 1);
            }
            
            dto.TagText = tagValue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.post, model.ID);
            
            if(model.PostImages != null)
            {
                SavePostImage(model.PostImages, model.ID);
            }

            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);
            return true;
        }

        public string DeletePostImage(int ID)
        {
            string imagepath = dao.DeletePostImage(ID);
            LogDAO.AddLog(General.ProcessType.ImageDelete, General.TableName.Image, ID);
            return imagepath;

        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> imagelist = dao.DeletePost(ID);
            LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.post, ID);
            return imagelist;
        }

        public CountDTO GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto.MessageCount = dao.GetMessageCount();
            dto.CommentCount = dao.GetCommentCount();
            return dto;
        }
    }
}
