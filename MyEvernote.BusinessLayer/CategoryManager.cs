using MyEvernote.Entities;
using MyEvernote.DataAccessLayer.EntityFrameworkMSsqlDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager
    {
        private Repository<Catergory> repo_category = new Repository<Catergory>();

        public List<Catergory> GetCatergories()
        {
            return repo_category.List();
        }

    }
}
