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
    public class CatergoryController : Controller
    {
        private CategoryManager catManager = new CategoryManager();

        public ActionResult Index()
        {
            return View(catManager.List());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Catergory catergory = catManager.Find(x => x.Id == id.Value);

            if (catergory == null)
            {
                return HttpNotFound();
            }
            return View(catergory);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catergory catergory)
        {
            if (ModelState.IsValid)
            {
                catManager.Insert(catergory);

                return RedirectToAction("Index");
            }

            return View(catergory);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Catergory catergory = catManager.Find(x => x.Id == id.Value);

            if (catergory == null)
            {
                return HttpNotFound();
            }
            return View(catergory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catergory catergory)
        {
            if (ModelState.IsValid)
            {
                // TODO : İncele
                catManager.Update(catergory);

                return RedirectToAction("Index");
            }
            return View(catergory);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Catergory catergory = catManager.Find(x => x.Id == id.Value);


            if (catergory == null)
            {
                return HttpNotFound();
            }

            return View(catergory);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catergory catergory = catManager.Find(x => x.Id == id);
            catManager.Delete(catergory);

            return RedirectToAction("Index");
        }


    }
}
