using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContex>
    {
        protected override void Seed(DatabaseContex context)
        {
            //Adding Fake Admin user

            EvernoteUser admin = new EvernoteUser()
            {
                Username = "Aytach",
                Name = "Aytaç",
                Surname = "YANIK",
                CreatedOn = DateTime.Now,
                Email = "aytacyanik@hotmail.com.tr",
                ActivateGuid = Guid.NewGuid(),
                IsAdmin = true,
                IsActive = true,
                Password = "123456",
                ModifiedUsername = "Aytach",
                ModifiedOn = DateTime.Now.AddMinutes(5)
            };
            //Adding Fake standart user

            EvernoteUser standartUser = new EvernoteUser()
            {
                Username = "aytacynk",
                Name = "AytaçAytaç",
                Surname = "YANIK",
                CreatedOn = DateTime.Now.AddHours(1),
                Email = "aytacyanik@yandex.com",
                ActivateGuid = Guid.NewGuid(),
                IsAdmin = false,
                IsActive = true,
                Password = "123456",
                ModifiedUsername = "Aytach",
                ModifiedOn = DateTime.Now.AddMinutes(65)
            };
            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Username = $"user{i}",
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsAdmin = false,
                    IsActive = true,
                    Password = "123",
                    ModifiedUsername = $"user{i}",
                };
                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            //UserList for using..
            List<EvernoteUser> userList = context.EvernoteUsers.ToList();


            //Adding Fake Categories
            for (int i = 0; i < 10; i++)
            {
                Catergory cat = new Catergory()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "Aytach"
                };

                context.Categories.Add(cat);

                //Adding Fake Notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 10); k++)
                {
                    EvernoteUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Catergory = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 7),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username
                    };
                    cat.Notes.Add(note);

                    //Adding Fake Comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EvernoteUser comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];

                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username,
                            //Note = note,
                            Owner = comment_owner
                        };
                        note.Comments.Add(comment);
                    }

                    //Adding Fake Likes

                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };

                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
