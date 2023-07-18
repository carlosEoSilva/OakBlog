using BLL;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : Controller //BaseController
    {
        FavBLL bll = new FavBLL();

        public ActionResult UpdateFav()
        {
            FavDTO dto = bll.GetFav();
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.LogoImage != null)
                {
                    HttpPostedFileBase postedfilelogo = model.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfilelogo.InputStream);
                    Bitmap resizefavImage = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedfilelogo.FileName);

                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string logounique = Guid.NewGuid().ToString();
                        string logoname = logounique + postedfilelogo.FileName;
                        logoname = logoname.Remove(0, logoname.Length - 49);
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + logoname));
                        model.Logo = logoname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                if (model.FavImage != null)
                {
                    HttpPostedFileBase postedfilefav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizefavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedfilefav.FileName);

                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string favunique = Guid.NewGuid().ToString();
                        string favname = favunique + postedfilefav.FileName;
                        favname = favname.Remove(0, favname.Length - 49);
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favname));
                        model.Fav = favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                FavDTO returndto = bll.UpdateFav(model);

                //?BUG? não está apagando as imagens anteriores
                if(model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav));
                    }
                }

                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;

            }
            return View(model);
        }
    }
}