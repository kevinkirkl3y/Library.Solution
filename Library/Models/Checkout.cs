using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{

  public class Checkout
  {
    public Checkout()
    {
      this.Books = new HashSet<Book>();
      this.Patrons = new HashSet<Patron>();
      this.Libraries = new HashSet<Library>();
      this.BookCopies = new HashSet<BookCopy>();
    }
    public int CheckoutId { get; set; }
    public int BookId { get; set; }
    public int LibCardId { get; set; }
    public int LibraryId { get; set; }
    
    [DisplayName("Checkout Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
    public DateTime CheckoutDate { get; set; }

    [DisplayName("Due Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
    public DateTime DueDate { get; set; }

    [DisplayName("Return Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
    public DateTime ReturnDate { get; set; }

    public ICollection<Book> Books { get; set; }
    public ICollection<Library> Libraries { get; set; }

    public ICollection<Patron> Patrons { get; set; }

    public ICollection<BookCopy> BookCopies { get; set; }
  }

}