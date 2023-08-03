using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MetaDAO
    {
        public int AddMeta(Meta meta)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Metas.Add(meta);
                    db.SaveChanges();
                    return meta.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public List<MetaDTO> GetMetaData()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<MetaDTO> metalist = new List<MetaDTO>();

                List<Meta> list = db.Metas
                    .Where(x => x.isDeleted == false)
                    .OrderBy(x => x.AddDate)
                    .ToList();

                foreach (var item in list)
                {
                    MetaDTO dto = new MetaDTO();
                    dto.MetaID = item.ID;
                    dto.Name = item.Name;
                    dto.MetaContent = System.Text.Encoding.UTF7.GetString(item.MetaContent);
                    metalist.Add(dto);
                }

                return metalist;
            }
                
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Meta meta = db.Metas.First(x => x.ID == ID);
                MetaDTO dto = new MetaDTO();
                dto.MetaID = meta.ID;
                dto.Name = meta.Name;
                dto.MetaContent = System.Text.Encoding.UTF7.GetString(meta.MetaContent);
                return dto;
            }
                
        }

        public void DeleteMeta(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Meta meta = db.Metas.First(x => x.ID == ID);

                    meta.isDeleted = true;
                    meta.DeletedDate = DateTime.Now;
                    meta.LastUpdateDate = DateTime.Now;
                    meta.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public void UpdateMeta(MetaDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Meta meta = db.Metas.First(x => x.ID == model.MetaID);
                    meta.Name = model.Name;
                    meta.MetaContent = Encoding.UTF7.GetBytes(model.MetaContent);
                    meta.LastUpdateDate = DateTime.Now;
                    meta.LastUpdateUserID = UserStatic.UserID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
