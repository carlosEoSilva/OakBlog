using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class AddressDAO 
    {
        public int AddAddress(Address ads)
        {
            //note-19
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                try
                {
                    // incrementar o ID manualmente porque a tabela não foi configurada para auto-incrementar o ID...
                    Address lastEntry = db.Addresses.OrderByDescending(x => x.ID).First();
                    ads.ID = lastEntry.ID + 1;

                    db.Addresses.Add(ads);
                    db.SaveChanges();
                    return ads.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            
        }

        public List<AddressDTO> GetAdresses()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<Address> list = db.Addresses.Where(x => x.isDeleted == false)
                .OrderBy(x => x.AddDate)
                .ToList();

                List<AddressDTO> dtolist = new List<AddressDTO>();

                foreach (var item in list)
                {
                    AddressDTO dto = new AddressDTO
                    {
                        ID = item.ID,
                        AddressContent = item.Address1,
                        Email = item.Email,
                        Fax = item.Fax,
                        LargeMapPath = item.MapPathLarge,
                        Phone = item.Phone,
                        Phone2 = item.Phone2,
                        SmallMapPath = item.MapPathSmall
                    };

                    dtolist.Add(dto);
                }

                return dtolist;
            }
                
        }

        public void DeleteAdress(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Address ads = db.Addresses.First(x => x.ID == ID);

                    ads.isDeleted = true;
                    ads.DeletedDate = DateTime.Now;
                    ads.LastUpdateDate = DateTime.Now;
                    ads.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void AddAddress(AddressDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Address ads = db.Addresses.First(x => x.ID == model.ID);

                    ads.Address1 = model.AddressContent;
                    ads.Email = model.Email;
                    ads.Fax = model.Fax;
                    ads.MapPathLarge = model.LargeMapPath;
                    ads.MapPathSmall = model.SmallMapPath;
                    ads.Phone = model.Phone;
                    ads.Phone2 = model.Phone2;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }
    }
}
