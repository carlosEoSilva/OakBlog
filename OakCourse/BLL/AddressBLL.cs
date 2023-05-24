using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();

        public bool AddAdress(AddressDTO model)
        {
            Address ads = new Address
            {
                Address1 = model.AddressContent,
                Email = model.Email,
                Phone = model.Phone,
                Phone2 = model.Phone2,
                Fax = model.Fax,
                MapPathLarge = model.LargeMapPath,
                MapPathSmall = model.SmallMapPath,
                AddDate = DateTime.Now,
                LastUpdateDate= DateTime.Now,
                LastUpdateUserID = UserStatic.UserID
            };

            int ID = dao.AddAddress(ads);
            LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, ID);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAdresses();
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.AddAddress(model);
            LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID);
            return true;
        }
    }
}
