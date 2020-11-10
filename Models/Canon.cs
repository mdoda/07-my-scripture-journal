using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class Canon
    {
        public int CanonId { get; set; }

        [Display(Name ="Canon")]
        public string CanonName { get; set; }

    }
}
