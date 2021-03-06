using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize]
  public class AuthorsController: Controller
  { 
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db, UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userAuthor = _db.Authors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userAuthor);
    }

    public ActionResult Create()
    {
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Author author, int BookId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      author.User = currentUser;
      _db.Authors.Add(author);
      if (BookId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { BookId = BookId, AuthorId = author.AuthorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisAuthor = _db.Authors
          .Include(author => author.AuthorBooks)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title"); // ViewBag only transfers data from controller to view
      return View(thisAuthor);
    }
    
    [HttpPost]
    public async Task<ActionResult> Edit(Author author, int BookId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if (BookId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { BookId = BookId, AuthorId = author.AuthorId });
      }
      _db.Entry(author).State=EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult AddBook(Author author, int BookId)
    {
      if (BookId != 0)
      // Check if CourseId is valid
      {
        var returnedJoin = _db.AuthorBooks.Any(join => join.AuthorId == author.AuthorId && join.BookId == BookId);
        // Check if "Any" of this relationship exists, returns a bool
        if (!returnedJoin) 
        {
        // if the returnedJoin for that relationship if false, then add the relationship
          _db.AuthorBooks.Add(new AuthorBook() { BookId =BookId, AuthorId = author.AuthorId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisAuthor = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
    [HttpPost]
    public ActionResult DeleteBook(int joinId)
    {
      var joinEntry = _db.AuthorBooks.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBooks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }

  
}