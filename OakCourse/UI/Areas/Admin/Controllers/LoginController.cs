﻿using BLL;
using DTO;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userbll = new UserBLL();

        // GET: Admin/Login
        [Route("Login")]
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            //model.Password = "enter";
                
                if (model.Username != null && model.Password != null)
                {
                    UserDTO user = userbll.GetUserWithUsernameAndPassword(model);

                    if (user.ID != 0)
                    {
                        UserStatic.UserID = user.ID;
                        UserStatic.isAdmin = user.isAdmin;
                        UserStatic.Namesurname = user.Name;
                        UserStatic.Imagepath = user.ImagePath;

                        LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);

                        return RedirectToAction("PostList", "Post");
                    }
                
                    else
                        return View(model);
                    
                }
                else
                    return View(model);
        }
    }
}