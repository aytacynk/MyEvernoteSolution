using MyEvernote.Common.Helpers;
using MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_evernoteuser = new Repository<EvernoteUser>();



        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            EvernoteUser user = repo_evernoteuser.Find(x => x.Username == data.Username || x.Email == data.EMail);

            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddError(ErrorMessagesCode.UsernameAlreadyExist, "Kullanıcı adı kayıtlıdır!!!");
                }

                if (user.Email == data.EMail)
                {
                    layerResult.AddError(ErrorMessagesCode.EmailAlreadyExist, "E-posta daha önceden kayıtlıdır!!!");
                }
            }
            else
            {
                int dbResult = repo_evernoteuser.Insert(new EvernoteUser
                {
                    Username = data.Username,
                    Email = data.EMail,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    ProfileImaFilename = "user.png"
                });

                if (dbResult > 0)
                {
                    layerResult.Result = repo_evernoteuser.Find(x => x.Email == data.EMail && x.Username == data.Username);

                    //TODO: aktivasyon maili atılacak.
                    //layerResult.Result.ActivateGuid
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";

                    string body = ($"Merhaba {layerResult.Result.Username} ;<br><br>Hesabınızı Aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>");

                    MailHelper.SendMail(body, layerResult.Result.Email, "MyEvernote Hesap Aktifleştirme");

                }
            }

            return layerResult;
        }



        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            res.Result = repo_evernoteuser.Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessagesCode.UserNotFound, "Kullanıcı Bulunamadı.");
            }

            return res;
        }



        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            layerResult.Result = repo_evernoteuser.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.IsActive == false)
                {
                    layerResult.AddError(ErrorMessagesCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    layerResult.AddError(ErrorMessagesCode.CheckYourEmail, "Lütfen E-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                layerResult.AddError(ErrorMessagesCode.UsernameOrPassWrong, "Kullanıcı ya da şifre uyuşmuyor.");
            }
            return layerResult;
        }



        public BusinessLayerResult<EvernoteUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            layerResult.Result = repo_evernoteuser.Find(x => x.ActivateGuid == activateId);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessagesCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return layerResult;
                }
                layerResult.Result.IsActive = true;
                repo_evernoteuser.Update(layerResult.Result);

            }

            else
            {
                layerResult.AddError(ErrorMessagesCode.ActivateIdDoesNotExist, "Aktifleştirelecek kullanıcı bulunamadı.");
            }
            return layerResult;
        }



        public BusinessLayerResult<EvernoteUser> RemoveUserById(int id)
        {
            EvernoteUser user = repo_evernoteuser.Find(x => x.Id == id);
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (repo_evernoteuser.Delete(user) == 0)
                {
                    res.AddError(ErrorMessagesCode.UserCoulNotRemove, "Kullanıcı Silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessagesCode.UserCouldNotFound, "KUllanıcı Bulunamadı");
            }
            return res;
        }



        public BusinessLayerResult<EvernoteUser> UpdateProfile(EvernoteUser data)
        {
            EvernoteUser db_user = repo_evernoteuser.Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExist, "Kullanıcı Adı Kayıtlıdır.");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.EmailAlreadyExist, "E-posta Adresi kayıtlıdır");
                }
                return res;
            }
            res.Result = repo_evernoteuser.Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Username = data.Username;
            res.Result.Password = data.Password;

            if (string.IsNullOrEmpty(data.ProfileImaFilename) == false)
            {
                res.Result.ProfileImaFilename = data.ProfileImaFilename;
            }

            if (repo_evernoteuser.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessagesCode.ProfileCouldNotUpdated, "Profil Güncellenemedi.");
            }

            return res;
        }
    }

}
