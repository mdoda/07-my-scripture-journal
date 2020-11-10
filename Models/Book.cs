using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MyScriptureJournal.Models
{
    public class Book
    {
        public int BookID { get; set;}

        public int CanonID { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime CreatedDate { get; set; }

        public string BookName { get; set; }

        public Canon Canon { get; set; }
    }
}
