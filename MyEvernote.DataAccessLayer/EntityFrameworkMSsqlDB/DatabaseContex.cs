using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB
{
    public class DatabaseContex : DbContext
    {
        public DbSet<EvernoteUser> EvernoteUsers { get; set; }
        public DbSet<Catergory> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DatabaseContex()
        {
            Database.SetInitializer(new MyInitializer());
        }

        //override ile yapılan metoddur.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent API : İlişiki database'de ki tabloları "Cascade" olarak yapmaya yaratan metdoddur.

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Comments)
                .WithRequired(c => c.Note)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Likes)
                .WithRequired(c => c.Note)
                .WillCascadeOnDelete(true);

        }

    }
}
