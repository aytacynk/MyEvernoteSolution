﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Kategori Başlığı"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(50, ErrorMessage = "{0} alanı max {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Açıklama"),
            StringLength(150, ErrorMessage = "{0} alanı max {1} karakter olmalıdır.")]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Catergory()
        {
            Notes = new List<Note>();
        }
    }
}
