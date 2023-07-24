using BLL;
using DTO;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeMagController : Controller
    {
        // GET: HomeMag
        LayoutBLL layoutbll = new LayoutBLL();
        GeneralBLL bll = new GeneralBLL();
        PostBLL postbll = new PostBLL();
        ContactBLL contactbll = new ContactBLL();

        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = bll.GetAllPosts();
            return View(dto);
        }

        public ActionResult CategoryPostListMag(string CategoryName)
        {
            HomeLayoutDTO layoutdto = layoutbll.GetLayoutData();
            GeneralDTO dto = bll.GetCategoryPostList(CategoryName);
            ViewData["LayoutDTO"] = layoutdto;
            return View(dto);
        }

        public ActionResult PostDetailMag(int? ID)
        {
            HomeLayoutDTO layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = bll.GetPostDetailPageItemsWithID(ID);
            return View(dto);
        }

        //post de comentários
        [HttpPost]
        public ActionResult PostDetailMag(GeneralDTO model)
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

        [Route("contactusMag")]
        public ActionResult ContactUsMag()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();
            return View(dto);
        }

        [Route("contactusMag")]
        [HttpPost]
        public ActionResult ContactUsMag(GeneralDTO model)
        {
            if (model.Name != null && model.Subject != null && model.Email != null && model.Message != null)
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

            HomeLayoutDTO layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = bll.GetContactPageItems();
            return View(dto);
        }

        [Route("SearchMag")]
        [HttpPost]
        public ActionResult SearchMag(GeneralDTO model)
        {
            HomeLayoutDTO layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = bll.GetSearchPosts(model.SearchText);
            return View(dto);
        }
    }
}