using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class CreateModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public CreateModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BookID"] = new SelectList(_context.Set<Book>(), "BookID", "BookName");
            ViewData["CanonId"] = new SelectList(_context.Set<Canon>(), "CanonId", "CanonName");
            
            //Console.WriteLine(ViewData["BookId"]);
            return Page();
        }

        [BindProperty]
        public Scripture Scripture { get; set; }

        [BindProperty]
        public Canon Canon { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Scripture.CreatedDate = DateTime.Now;

            _context.Scripture.Add(Scripture);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
