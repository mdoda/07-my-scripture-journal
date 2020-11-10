using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }


        public string BookNameSort { get; set; }
        public string DateSort { get; set; }

        public IList<Scripture> Scripture { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString {get; set;}

        public SelectList Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            Scripture = await _context.Scripture
                .Include(s => s.Book)
                .Include(s => s.Book.Canon)
                .ToListAsync();

            BookNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";


            var scriptures = from s in _context.Scripture
                            select s;

            switch (sortOrder)
            {
                case "name_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Book.BookName);
                    break;
                case "Date":
                    scriptures = scriptures.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Book.Canon.CanonName);
                    break;
            }

            IQueryable<string> bookQuery = from b in _context.Book
                                            orderby b.BookName
                                            select b.BookName;

            //var scriptures = from s in _context.Scripture
            //                 select s;


            if(!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(BookName))
            {
                scriptures = scriptures.Where(s => s.Book.BookName == BookName);
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Scripture = await scriptures.ToListAsync();
            //BookNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            //IQueryable<Scripture> scripture = from s in _context.Scripture
            //                                  select s;
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        scripture = scripture.OrderByDescending(s => s.Book.BookName);
            //        break;
            //    case "Date":
            //        scripture = scripture.OrderBy(s => s.CreatedDate);
            //        break;
            //    case "date_desc":
            //        scripture = scripture.OrderByDescending(s => s.CreatedDate);
            //        break;
            //    default:
            //        scripture = scripture.OrderBy(s => s.Book.Canon.CanonName);
            //        break;
            //}
            //Scripture = await scripture.AsNoTracking().ToListAsync();

        }
    }
}
