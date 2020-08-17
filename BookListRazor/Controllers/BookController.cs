using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data=await _dbContext.Book.ToListAsync()});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book =await _dbContext.Book.FirstOrDefaultAsync(b=>b.ID==id);
            if (book == null)
            {
                return Json(new {success=false,message="can't delete book" });
            }
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
            return Json(new { success = true, message = "delete successful" });

        }
    }
}
