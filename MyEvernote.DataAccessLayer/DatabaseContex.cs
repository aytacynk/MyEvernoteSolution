using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer
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

    }
}
