using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<PostDTO> GetSliderPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            var list = (from post in db.Posts.Where(x => x.Slider == true && x.isDeleted == false)
                        join category in db.Categories on post.CategoryID equals category.ID
                        select new
                        {
                            postID = post.ID,
                            title = post.Title,
                            categoryName = category.CategoryName,
                            seolink = post.SeoLink,
                            viewcount = post.ViewCount,
                            adddate = post.AddDate
                        }
                       ).OrderByDescending(x => x.adddate).Take(8).ToList();

            foreach(var item in list)
            {
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                int commentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();

                PostDTO dto = new PostDTO
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    ViewCount = item.viewcount,
                    SeoLink = item.seolink,
                    ImagePath = image.ImagePath,
                    CommentCount = commentCount,
                    AddDate = item.adddate 
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<VideoDTO> GetVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.AddDate)
                .Take(3)
                .ToList();

            foreach(var item in list)
            {
                VideoDTO dto = new VideoDTO
                {
                    ID = item.ID,
                    Title = item.Title,
                    VideoPath = item.VideoPath,
                    OriginalVideoPath = item.OriginalVideoPath,
                    AddDate = item.AddDate
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetMostViewedPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            var list = (from post in db.Posts.Where(x => x.isDeleted == false)
                        join category in db.Categories on post.CategoryID equals category.ID
                        select new
                        {
                            postID = post.ID,
                            title = post.Title,
                            categoryName = category.CategoryName,
                            seolink = post.SeoLink,
                            viewcount = post.ViewCount,
                            adddate = post.AddDate
                        }
                       ).OrderByDescending(x => x.viewcount).Take(5).ToList();

            foreach (var item in list)
            {
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                int commentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();

                PostDTO dto = new PostDTO
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    ViewCount = item.viewcount,
                    SeoLink = item.seolink,
                    ImagePath = image.ImagePath,
                    CommentCount = commentCount,
                    AddDate = item.adddate
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetPopularPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            var list = (from post in db.Posts.Where(x => x.isDeleted == false && x.Area2 == true)
                        join category in db.Categories on post.CategoryID equals category.ID
                        select new
                        {
                            postID = post.ID,
                            title = post.Title,
                            categoryName = category.CategoryName,
                            seolink = post.SeoLink,
                            viewcount = post.ViewCount,
                            adddate = post.AddDate
                        }
                       ).OrderByDescending(x => x.adddate).Take(5).ToList();

            foreach (var item in list)
            {
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                int commentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();

                PostDTO dto = new PostDTO
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    ViewCount = item.viewcount,
                    SeoLink = item.seolink,
                    ImagePath = image.ImagePath,
                    CommentCount = commentCount,
                    AddDate = item.adddate 
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetBreakingPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();

            var list = (from post in db.Posts.Where(x => x.Slider == false && x.isDeleted == false)
                        join category in db.Categories on post.CategoryID equals category.ID
                        select new
                        {
                            postID = post.ID,
                            title = post.Title,
                            categoryName = category.CategoryName,
                            seolink = post.SeoLink,
                            viewcount = post.ViewCount,
                            adddate = post.AddDate
                        }
                       ).OrderByDescending(x => x.adddate).Take(5).ToList();

            foreach (var item in list)
            {
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                int commentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();

                PostDTO dto = new PostDTO
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    ViewCount = item.viewcount,
                    SeoLink = item.seolink,
                    ImagePath = image.ImagePath,
                    CommentCount = commentCount,
                    AddDate = item.adddate 
                };

                dtolist.Add(dto);
            }
            return dtolist;
        }
    }
}
