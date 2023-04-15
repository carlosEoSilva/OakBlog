using DTO;
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

            T_User test = db.T_User.FirstOrDefault(x => x.Username != null);

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
    }
}
