using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : Controller
    {
        
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {

        }
    }
}