using MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB;
using MyEvernote.Entities;
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
                    layerResult.Errors.Add("Kullanıcı adı kayıtlıdır!!!");
                }

                if (user.Email == data.EMail)
                {
                    layerResult.Errors.Add("E-posta daha önceden kayıtlıdır!!!");
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
                });

                if (dbResult > 0)
                {
                    layerResult.Result = repo_evernoteuser.Find(x => x.Email == data.EMail && x.Username == data.Username);

                    //TODO: aktivasyon maili atılacak.
                    //layerResult.Result.ActivateGuid
                }
            }

            return layerResult;
        }


        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            layerResult.Result = repo_evernoteuser.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.IsActive == false)
                {
                    layerResult.Errors.Add("Kullanıcı aktifleştirilmemiştir. Lütfen E-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                layerResult.Errors.Add("Kullanıcı ya da şifre Uyuşmuyor.");
            }
            return layerResult;
        }
    }
}
