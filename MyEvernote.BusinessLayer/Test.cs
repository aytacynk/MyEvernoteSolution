using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        private Repository<Catergory> repo_category = new Repository<Catergory>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();


        public Test()
        {
            List<Catergory> categories = repo_category.List();
            List<Catergory> catergories_filtered = repo_category.List(x => x.Id > 5);
        }

        public void InsertTest()
        {
            int result = repo_user.Insert(new EvernoteUser()
            {
                Username = "CemilYamak",
                Name = "Cemil",
                Surname = "YAMAK",
                Email = "cemilyamak@hotmail.com.tr",
                ActivateGuid = Guid.NewGuid(),
                IsAdmin = true,
                IsActive = true,
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "Aytach"
            });
        }

        public void UpdateTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "CemilYamak");
            if (user != null)
            {
                user.Username = "CEamilYamak";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "CEamilYamak");
            if (user != null)
            {
                int result = repo_user.Delete(user);
            }
        }


        public void CommentTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);   

            Comment comment = new Comment()
            {
                Text = "Bu bir test'dir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "Aytach",
                Note = note,
                Owner = user
            };

            repo_comment.Insert(comment);
        }

    }
}
