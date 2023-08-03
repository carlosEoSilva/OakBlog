using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class GeneralDAO 
    {
        public List<PostDTO> GetSliderPosts()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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

        public List<VideoDTO> GetAllvideos()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<VideoDTO> dtolist = new List<VideoDTO>();
                List<Video> list = db.Videos.Where(x => x.isDeleted == false)
                    .OrderByDescending(x => x.AddDate)
                    .ToList();

                foreach (var item in list)
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
                
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> dtolist = new List<PostDTO>();

                var list = (from post in db.Posts.Where(x => x.isDeleted == false && x.CategoryID == categoryID)
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
                           ).OrderByDescending(x => x.adddate).ToList();

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

        public List<PostDTO> GetSearchPost(string searchText)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostDTO> dtolist = new List<PostDTO>();

                var list = (from post in db.Posts.Where(x => x.isDeleted == false &&
                                                       (x.Title.Contains(searchText) ||
                                                        x.PostContent.Contains(searchText)))
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
                           ).OrderByDescending(x => x.adddate).ToList();

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

        public PostDTO GetPostDetail(int? ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                post.ViewCount++;
                db.SaveChanges();

                PostDTO dto = new PostDTO
                {
                    ID = post.ID,
                    Title = post.Title,
                    ShortContent = post.ShortContent,
                    PostContent = post.PostContent,
                    Language = post.LanguageName,
                    SeoLink = post.SeoLink,
                    CategoryID = post.CategoryID,
                    ViewCount = post.ViewCount,
                    CategoryName = (db.Categories.First(x => x.ID == post.CategoryID)).CategoryName
                };

                List<PostImage> images = db.PostImages.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
                List<PostImageDTO> imagedtolist = new List<PostImageDTO>();

                foreach (var item in images)
                {
                    PostImageDTO img = new PostImageDTO
                    {
                        ID = item.ID,
                        ImagePath = item.ImagePath
                    };

                    imagedtolist.Add(img);
                }

                dto.PostImages = imagedtolist;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).Count();
                List<Comment> comments = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).ToList();
                List<CommentDTO> commentdtolist = new List<CommentDTO>();

                foreach (var item in comments)
                {
                    CommentDTO cdto = new CommentDTO
                    {
                        ID = item.ID,
                        AddDate = item.AddDate,
                        CommentContent = item.CommentContent,
                        Name = item.NameSurname,
                        Email = item.Email
                    };

                    commentdtolist.Add(cdto);
                }

                dto.CommentList = commentdtolist;
                List<PostTag> tags = db.PostTags.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
                List<TagDTO> taglist = new List<TagDTO>();

                foreach (var item in tags)
                {
                    TagDTO tdto = new TagDTO
                    {
                        ID = item.ID,
                        TagContent = item.TagContent
                    };

                    taglist.Add(tdto);
                }

                dto.TagList = taglist;
                return dto;
            }
                
        }

        public List<VideoDTO> GetVideos()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<VideoDTO> dtolist = new List<VideoDTO>();
                List<Video> list = db.Videos.Where(x => x.isDeleted == false)
                    .OrderByDescending(x => x.AddDate)
                    .Take(3)
                    .ToList();

                foreach (var item in list)
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
                
        }

        public List<PostDTO> GetMostViewedPosts()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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
                
        }

        public List<PostDTO> GetPopularPost()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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
                
        }

        public List<PostDTO> GetBreakingPosts()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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
}
