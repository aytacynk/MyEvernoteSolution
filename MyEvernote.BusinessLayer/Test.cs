using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        public Test()
        {
            DataAccessLayer.DatabaseContex db = new DataAccessLayer.DatabaseContex();
            //db.Database.CreateIfNotExists();
            db.EvernoteUsers.ToList();
        }
    }
}
