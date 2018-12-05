using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Tempdate ile GetCategories by Id
        //public ActionResult Select(int? id)
        //{
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //CategoryManager cm = new CategoryManager();
            //Catergory cat = cm.GetCategoryById(id.Value);

            //if (cat == null)
            //{
            //    //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            //    //return RedirectToAction("Index", "Home");
            //    return HttpNotFound();
            //}
            //TempData["mm"] = cat.Notes;

            //return RedirectToAction("Index", "Home");
        //}
    }
}