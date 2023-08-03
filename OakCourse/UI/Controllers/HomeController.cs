﻿using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL layoutbll = new LayoutBLL();
        GeneralBLL bll = new GeneralBLL();
        PostBLL postbll = new PostBLL();
        ContactBLL contactbll = new ContactBLL();

        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetAllPosts();
            return View(dto);
        }

        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetCategoryPostList(CategoryName);
            ViewData["LayoutDTO"] = layoutdto;
            return View(dto);
        }

        public ActionResult PostDetail(int? ID)
        {
            HomeLayoutDTO layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = bll.GetPostDetailPageItemsWithID(ID);
            return View(dto);
        }

        //post de comentários
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
            if (model.Name != null && model.Email != null && model.Message != null)
            {
                if (postbll.AddComment(model))
                {
                    //note-14
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }

            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            model = bll.GetPostDetailPageItemsWithID(model.PostID);

            return View(model);
        }

        [Route("contactus")]
        public ActionResult ContactUs()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();
            return View(dto);
        }

        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if(model.Name != null && model.Subject != null && model.Email != null && model.Message != null)
            {
                if (contactbll.AddContact(model))
                {
                    ViewData["CommentState"] = "Success";
                }
                else
                {
                    ViewData["CommentState"] = "Success";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }

            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();

            return View(dto);
        }
    
        [Route("Search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetSearchPosts(model.SearchText);
            return View(dto);
        }

        public void test()
        {
            Console.WriteLine("xxxxxx");
        }
    
    
    }
}