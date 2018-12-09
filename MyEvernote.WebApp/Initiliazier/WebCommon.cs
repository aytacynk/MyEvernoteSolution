using MyEvernote.BusinessLayer;
using MyEvernote.Common;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MyEvernote.WebApp.Initiliazier
{
    public class WebCommon : ICommon
    {
        public string GetCuurentUsername()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                BusinessLayerResult<EvernoteUser> user = HttpContext.Current.Session["login"] as BusinessLayerResult<EvernoteUser>;
                return user.Result.Username;
            }

            return null;
        }
    }
}