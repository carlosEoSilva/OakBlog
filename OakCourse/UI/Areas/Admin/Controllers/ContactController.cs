﻿using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {

        ContactBLL bll = new ContactBLL();

        public ActionResult UnreadMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetUnreadMessages();
            return View(list);
        }

        

        public ActionResult ReadMessage(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("UnreadMessages");
        }

        public JsonResult DeleteMessage(int ID)
        {
            bll.DeleteMessage(ID);
            return Json("");
        }

        public ActionResult AllMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetAllMessages();
            return View(list);
        }

        public ActionResult AllMessagesReadMessage(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("AllMessages");
        }

        public ActionResult AllMessagesUnreadMessage(int ID)
        {
            bll.UnreadMessage(ID);
            return RedirectToAction("AllMessages");

        }
    }
}