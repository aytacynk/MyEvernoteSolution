using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB
{
    public class RepositoryBase
    {
        protected static DatabaseContex contex;
        private static object _lockSync = new object();


        protected RepositoryBase() //Burda protected class'ın new'lenmesini engeller.
        {
            CreateContex();
        }

        private static void CreateContex()
        {
            if (contex == null)
            {
                lock (_lockSync)
                {
                    if (contex == null)
                    {
                        contex = new DatabaseContex();
                    }
                }
            }
        }
    }
}
