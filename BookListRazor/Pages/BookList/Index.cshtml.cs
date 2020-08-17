using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books =await _dbContext.Book.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var newBook = await _dbContext.Book.FindAsync(id);
            if (newBook == null)
            {
                return NotFound();
            }
            _dbContext.Book.Remove(newBook);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}