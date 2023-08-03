using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class VideoDAO 
    {
        public int AddVideo(Video video)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Videos.Add(video);
                    db.SaveChanges();
                    return video.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public List<VideoDTO> GetVideos()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<Video> list = db.Videos.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.AddDate).ToList();

                List<VideoDTO> dtolist = new List<VideoDTO>();

                foreach (var item in list)
                {
                    VideoDTO dto = new VideoDTO
                    {
                        Title = item.Title,
                        VideoPath = item.VideoPath,
                        OriginalVideoPath = item.OriginalVideoPath,
                        ID = item.ID,
                        AddDate = item.AddDate
                    };

                    dtolist.Add(dto);

                }
                return dtolist;
            }
                
        }

        public void UpdateVideo(VideoDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.First(x => x.ID == model.ID);

                    video.VideoPath = model.VideoPath;
                    video.OriginalVideoPath = model.OriginalVideoPath;
                    video.Title = model.Title;
                    video.LastUpdateDate = DateTime.Now;
                    video.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public void DeleteVideo(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Video video = db.Videos.First(x => x.ID == ID);

                video.isDeleted = true;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                video.DeletedDate = DateTime.Now;

                db.SaveChanges();
            }
                
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Video video = db.Videos.First(x => x.ID == ID);

                VideoDTO dto = new VideoDTO
                {
                    ID = video.ID,
                    OriginalVideoPath = video.OriginalVideoPath,
                    Title = video.Title
                };

                return dto;
            }
                
        }
    }
}
