using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace MyEvernote.WebApp.Controllers
{
    public class EvernoteUserController : Controller
    {
        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();

        public ActionResult Index()
        {
            return View(evernoteUserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.Id == id);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvernoteUser evernoteUser)
        {
            if (ModelState.IsValid)
            {
                // TODO : düzeltilecek..
                evernoteUserManager.Insert(evernoteUser);

                return RedirectToAction("Index");
            }

            return View(evernoteUser);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.Id == id);
            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvernoteUser evernoteUser)
        {
            if (ModelState.IsValid)
            {
                // TODO : Düzenlenecek update işlemine göre..

                //evernoteUserManager.Update(evernoteUser);

                return RedirectToAction("Index");
            }
            return View(evernoteUser);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.Id == id);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.Id == id);
            evernoteUserManager.Delete(evernoteUser);

            return RedirectToAction("Index");
        }
    }
}
