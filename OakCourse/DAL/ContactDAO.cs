using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ContactDAO
    {
        public void AddContact(Contact contact)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
                
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<ContactDTO> dtolist = new List<ContactDTO>();
                List<Contact> list = db.Contacts
                    .Where(x => x.isDeleted == false && x.isRead == false)
                    .OrderByDescending(x => x.AddDate)
                    .ToList();

                foreach (var item in list)
                {
                    ContactDTO dto = new ContactDTO
                    {
                        ID = item.ID,
                        Subject = item.Subject,
                        Name = item.NameSurname,
                        Email = item.Email,
                        Message = item.Message,
                        AddDate = item.AddDate,
                        isRead = item.isRead
                    };

                    dtolist.Add(dto);
                }

                return dtolist;
            }
                

        }

        public List<ContactDTO> GetAllMessages()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<ContactDTO> dtolist = new List<ContactDTO>();
                List<Contact> list = db.Contacts
                    .Where(x => x.isDeleted == false)
                    .OrderByDescending(x => x.AddDate)
                    .ToList();

                foreach (var item in list)
                {
                    ContactDTO dto = new ContactDTO
                    {
                        ID = item.ID,
                        Subject = item.Subject,
                        Name = item.NameSurname,
                        Email = item.Email,
                        Message = item.Message,
                        AddDate = item.AddDate,
                        isRead = item.isRead
                    };

                    dtolist.Add(dto);
                }

                return dtolist;
            }
                
        }

        public void UnreadMessage(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Contact contact = db.Contacts.First(x => x.ID == ID);

                contact.isRead = false;
                contact.ReadUserID = UserStatic.UserID;
                contact.LastUpdateDate = DateTime.Now;
                contact.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
        }

        public void DeleteMessage(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Contact contact = db.Contacts.First(x => x.ID == ID);

                contact.isDeleted = true;
                contact.DeletedDate = DateTime.Now;
                contact.LastUpdateDate = DateTime.Now;
                contact.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
                
        }

        public void ReadMessage(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Contact contact = db.Contacts.First(x => x.ID == ID);

                contact.isRead = true;
                contact.ReadUserID = UserStatic.UserID;
                contact.LastUpdateDate = DateTime.Now;
                contact.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
        }
    }
}
