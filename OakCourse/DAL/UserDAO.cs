using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UserDAO:PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();

            var modelNameByte = Encoding.UTF7.GetBytes(model.Username);
            var modelPasswordByte = Encoding.UTF7.GetBytes(model.Password);

            //-11
            T_User user = db.T_User
                .FirstOrDefault(x => x.Username.Equals(modelNameByte) && 
                    x.Password.Equals(modelPasswordByte));


            if(user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = model.Username;
                dto.Name = user.NameSurname;
                dto.ImagePath = user.ImagePath;
                dto.isAdmin = user.isAdmin;
            }
            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDTO> GetUsers()
        {
            List<T_User> list = db.T_User
                    .Where(x => x.isDeleted == false || x.isDeleted == null)
                    .OrderBy(x => x.AddDate)
                    .ToList();

            List<UserDTO> userlist = new List<UserDTO>();

            foreach(var item in list)
            {
                UserDTO dto = new UserDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Username = System.Text.Encoding.UTF7.GetString(item.Username);
                dto.ImagePath = item.ImagePath;
                
                userlist.Add(dto);
            }

            return userlist;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                
                user.NameSurname = model.Name;
                user.Username = MyUtils.StringParaByte(model.Username);

                if(model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }

                user.Email = MyUtils.StringParaByte(model.Email);
                user.Password = MyUtils.StringParaByte(model.Password);
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.isAdmin = model.isAdmin;

                db.SaveChanges();
                return oldImagePath;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserDTO GetUserWithID(int ID)
        {
            T_User user = db.T_User.First(x => x.ID == ID);
            UserDTO dto = new UserDTO();

            dto.ID = user.ID;
            dto.Name = user.NameSurname;
            dto.Username = System.Text.Encoding.UTF7.GetString(user.Username);
            dto.Password = System.Text.Encoding.UTF7.GetString(user.Password);
            dto.isAdmin = user.isAdmin;
            dto.Email = System.Text.Encoding.UTF7.GetString(user.Email);
            dto.ImagePath = user.ImagePath;

            return dto;
        }
    }
}
