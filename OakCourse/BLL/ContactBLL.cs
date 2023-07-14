using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContactBLL
    {
        ContactDAO dao = new ContactDAO();

        public bool AddContact(GeneralDTO model)
        {
            Contact contact = new Contact
            {
                Subject = model.Subject,
                NameSurname = model.Name,
                Email = model.Email,
                Message = model.Message,
                AddDate = DateTime.Now,
                LastUpdateDate = DateTime.Now
            };

            dao.AddContact(contact);
            return true;
        }

        public void ReadMessage(int ID)
        {
            dao.ReadMessage(ID);
            LogDAO.AddLog(General.ProcessType.ContactRead, General.TableName.Contact, ID);
        }

        public void DeleteMessage(int ID)
        {
            dao.DeleteMessage(ID);
            LogDAO.AddLog(General.ProcessType.ContactDelete, General.TableName.Contact, ID);
        }

        public List<ContactDTO> GetAllMessages()
        {
            return dao.GetAllMessages();
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            return dao.GetUnreadMessages();
        }

        public void UnreadMessage(int ID)
        {
            dao.UnreadMessage(ID);
        }
    }
}
