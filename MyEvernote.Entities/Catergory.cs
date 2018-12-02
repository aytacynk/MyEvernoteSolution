using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Categories")]
    public class Catergory : MyEntityBase
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Catergory()
        {
            Notes = new List<Note>();
        }
    }
}
