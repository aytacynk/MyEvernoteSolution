using MyEvernote.BusinessLayer;
using MyEvernote.Common;
using MyEvernote.Entities;
using MyEvernote.WebApp.Models;
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
            //if (HttpContext.Current.Session["login"] != null)
            //{
            //    EvernoteUser user = HttpContext.Current.Session["login"] as EvernoteUser;
            //    return user.Username;
            //}

            EvernoteUser user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";

        }
    }
}