using MyEvernote.BusinessLayer;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.WebApp.Models;
using MyEvernote.WebApp.ViewModels;
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
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();

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


            return View(noteManager.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());

            //return View(nm.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }


        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Catergory cat = categoryManager.Find(x => x.Id == id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }


        public ActionResult MostLiked()
        {
            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult ShowProfile()
        {
            //EvernoteUser currentUser = Session["login"] as EvernoteUser;

            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrornotfyError = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    RedirectingUrl = "/Home/Index",
                    Items = res.Errors
                };

                return View("Error", ErrornotfyError);

            }

            return View(res.Result);
        }


        public ActionResult EditProfile()
        {
            //EvernoteUser currentUser = Session["login"] as EvernoteUser;

            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrornotfyError = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    RedirectingUrl = "/Home/Index",
                    Items = res.Errors
                };

                return View("Error", ErrornotfyError);

            }

            return View(res.Result);

        }


        [HttpPost]
        public ActionResult EditProfile(EvernoteUser user, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    user.ProfileImaFilename = filename;
                }

                BusinessLayerResult<EvernoteUser> res = evernoteUserManager.UpdateProfile(user);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel erroeNotfyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", erroeNotfyObj);
                }

                //Session["login"] = res.Result as EvernoteUser;

                //Profil güncellendiği için session güncellendi.
                CurrentSession.Set<EvernoteUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
            }

            return View(user);
        }


        public ActionResult DeleteProfile()
        {
            //EvernoteUser currentUser = Session["login"] as EvernoteUser;


            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotfyObj = new ErrorViewModel
                {
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile",
                    Items = res.Errors
                };

                return View("Error", errorNotfyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }


        public ActionResult TestNotify()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                Header = "Yönlendiriliyorsunuz",
                Title = "Ok Test",
                RedirectingTimeout = 10000,
                Items = new List<ErrorMessageObject>() {
                    new ErrorMessageObject() {  Message="Test Başarılı"},
                    new ErrorMessageObject { Message="Test Başarılı 2" } }
            };

            return View("Error", model);
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
                BusinessLayerResult<EvernoteUser> res = evernoteUserManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessagesCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-78980";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                //Session["login"] = userResult.Result as EvernoteUser;

                CurrentSession.Set<EvernoteUser>("login", res.Result); //Session'a kullanıcı saklama.
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
                BusinessLayerResult<EvernoteUser> userResult = evernoteUserManager.RegisterUser(model);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login"
                };
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz.Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");


                return View("Ok", notifyObj);
            }
            return View(model);
        }


        //public ActionResult RegisterOk()
        //{
        //    return View();
        //}


        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrornotfyError = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    RedirectingUrl = "/Home/Index",
                    Items = res.Errors
                };

                return View("Error", ErrornotfyError);
            }
            OkViewModel okNotfyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi.",
                RedirectingUrl = "/Home/Index"
            };

            okNotfyObj.Items.Add("Hesap Başarılı Bir Şekiilde Aktifleştirildi..Artık Note paylaşabilir ve Beğenme yapabilirsiniz.");

            return View("Ok", okNotfyObj);
        }


        //public ActionResult UserActivateOK()
        //{
        //    return View();
        //}


        //public ActionResult UserActivateCancel()
        //{
        //    List<ErrorMessageObject> errors = null;

        //    if (TempData["errors"] != null)
        //    {
        //        errors = TempData["errors"] as List<ErrorMessageObject>;
        //    }

        //    return View(errors);
        //}


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");

        }

    }
}