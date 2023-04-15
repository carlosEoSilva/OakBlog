using DAL;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userdao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = userdao.GetUserWithUsernameAndPassword(model);
            return dto;
        }
    }
}
