﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class SocialMediaBLL
    {

        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {

            SocialMedia social = new SocialMedia();

            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.AddDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;
            social.LastUpdateDate = DateTime.Now;

            int ID = dao.AddSocialMedia(social);

            LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.social, ID);

            return true;
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = dao.GetSocialMedias();
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
           SocialMediaDTO dto= dao.getSocialMediaWithID(ID);
            return dto;
        }

        public string UpadateSocialMedia(SocialMediaDTO model)
        {
            string oldImagePath = dao.UpdateSocialMedia(model);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.social, model.ID);
            return oldImagePath;
        }

        public string DeleteSocialMedia(int ID)
        {
            string imagepath = dao.DeleteSocialMedia(ID);
            LogDAO.AddLog(General.ProcessType.SocialDelete, General.TableName.social, ID);
            return imagepath;
        }
    }
}