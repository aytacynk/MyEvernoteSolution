using MyEvernote.Entities;
using MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.BusinessLayer.Abstract;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Catergory>
    {
        // İlişki Category nesnesinde bağlı tablolardaki verileri silerek kaldırma işlemleri.

        //public override int Delete(Catergory category)
        //{
        //    NoteManager noteManeger = new NoteManager();
        //    LikedManager likedManager = new LikedManager();
        //    CommentManager commentManager = new CommentManager();

        //    // Kategori ile ilişiki notların silinmesi gerekiyor.

        //    foreach (Note note in category.Notes.ToList())
        //    {
        //        // Note ile ilişkili Like'ları silnmesi.
        //        foreach (Liked like in note.Likes.ToList())
        //        {
        //            likedManager.Delete(like);
        //        }

        //        // Note ile ilişkili Comment'lerin silnmesi.
        //        foreach (Comment comment in note.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }

        //        noteManeger.Delete(note);
        //    }

        //    return base.Delete(category);
        //}

    }
}
