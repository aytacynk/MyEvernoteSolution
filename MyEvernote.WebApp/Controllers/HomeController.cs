using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            BusinessLayer.Test test = new BusinessLayer.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.CommentTest();

            //Category Controller üzerinden gelen View talebi.
            //if (TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

            NoteManager nm = new NoteManager();

            return View(nm.GetAllNotes().OrderByDescending(x => x.ModifiedOn).ToList());

            //return View(nm.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Catergory cat = cm.GetCategoryById(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();

            return View("Index", nm.GetAllNotes().OrderByDescending(x => x.LikeCount).ToList());

        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> userResult = eum.LoginUser(model);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                Session["login"] = userResult;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Burda property'ler de ki kontrollerden geçiyormu manasına gelir. 
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> userResult = eum.RegisterUser(model);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}
                //EvernoteUser user = null;

                //try
                //{
                //    user = eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}

                //if (user == null)
                //{
                //    return View(model);
                //}
                return RedirectToAction("RegisterOk");
            }
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid activeId)
        {
            //Kullanıcı aktivasyonu sağlanacak.

            return View();

        }

        public ActionResult Logout()
        {
            return View();

        }



    }
}