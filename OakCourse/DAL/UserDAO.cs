using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UserDAO
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                UserDTO dto = new UserDTO();

                var modelNameByte = Encoding.UTF7.GetBytes(model.Username);
                var modelPasswordByte = Encoding.UTF7.GetBytes(model.Password);

                //note-11
                T_User user = db.T_User
                    .FirstOrDefault(x =>
                        x.Username.Equals(modelNameByte) &&
                        x.Password.Equals(modelPasswordByte) &&
                        x.isDeleted == false);


                if (user != null && user.ID != 0)
                {
                    dto.ID = user.ID;
                    dto.Username = model.Username;
                    dto.Name = user.NameSurname;
                    dto.ImagePath = user.ImagePath;
                    dto.isAdmin = user.isAdmin;
                }
                return dto;
            }
                
        }

        public int AddUser(T_User user)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.T_User.Add(user);
                    db.SaveChanges();
                    return user.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public List<UserDTO> GetUsers()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                //* Forma mais rápida de realizar a consulta ----------
                List<UserDTO> userlist = db.T_User
                    .Where(x => x.isDeleted == false)
                    .OrderBy(x => x.AddDate)
                    .Select(x => new UserDTO
                    {
                        ID= x.ID,
                        Name= x.NameSurname,
                        Username= MeuUtils.ByteParaString(x.Username),
                        ImagePath= x.ImagePath
                    })
                    .ToList();

                return userlist;

                //* Forma mais lenta de realizar a consulta --------

                //List<T_User> list = db.T_User
                //    .Where(x => x.isDeleted == false || x.isDeleted == null)
                //    .OrderBy(x => x.AddDate)
                //    .ToList();

                //List<UserDTO> userlist = new List<UserDTO>();

                //foreach (var item in list)
                //{
                //    UserDTO dto = new UserDTO()
                //    {
                //        ID = item.ID,
                //        Name = item.NameSurname,
                //        Username = Encoding.UTF7.GetString(item.Username),
                //        ImagePath = item.ImagePath
                //    };
                    
                //    userlist.Add(dto);
                //}

                //return userlist;
            }
                
        }

        public string DeleteUser(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    T_User user = db.T_User.First(x => x.ID == ID);

                    user.isDeleted = true;
                    user.DeletedDate = DateTime.Now;
                    user.LastUpdateDate = DateTime.Now;
                    user.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                    return user.ImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public string UpdateUser(UserDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    T_User user = db.T_User.First(x => x.ID == model.ID);
                    string oldImagePath = user.ImagePath;

                    user.NameSurname = model.Name;
                    user.Username = MeuUtils.StringParaByte(model.Username);

                    if (model.ImagePath != null)
                    {
                        user.ImagePath = model.ImagePath;
                    }

                    user.Email = MeuUtils.StringParaByte(model.Email);
                    user.Password = MeuUtils.StringParaByte(model.Password);
                    user.LastUpdateDate = DateTime.Now;
                    user.LastUpdateUserID = UserStatic.UserID;
                    user.isAdmin = model.isAdmin;

                    db.SaveChanges();
                    return oldImagePath;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }

        public UserDTO GetUserWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                T_User user = db.T_User.First(x => x.ID == ID);
                UserDTO dto = new UserDTO()
                {
                    ID = user.ID,
                    Name = user.NameSurname,
                    Username = Encoding.UTF7.GetString(user.Username),
                    Password = Encoding.UTF7.GetString(user.Password),
                    isAdmin = user.isAdmin,
                    Email = Encoding.UTF7.GetString(user.Email),
                    ImagePath = user.ImagePath
                };

                return dto;
            }
                
        }
    }
}
