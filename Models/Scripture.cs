using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int ScriptureId  { get; set; }

        [DisplayName("Book ID")]
        public int? BookId { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string Verse { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public Book Book { get; set; }

        public string Chapter;
    }
}
