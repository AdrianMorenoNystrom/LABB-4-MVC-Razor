using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LABB_4.Data;
using LABB_4.Models;

namespace LABB_4.Controllers
{
    public class BookLoansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookLoansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookLoans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookLoans.Include(b => b.Book).Include(b => b.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoans
                .Include(b => b.Book)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BookLoanId == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // GET: BookLoans/Create
        public IActionResult Create()
        {
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "BookName");
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: BookLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookLoanId,LoanDate,ReturnDate,FkCustomerId,FkBookId")] BookLoan bookLoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookLoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "BookId", bookLoan.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", bookLoan.FkCustomerId);
            return View(bookLoan);
        }

        // GET: BookLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoans.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "BookId", bookLoan.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", bookLoan.FkCustomerId);
            return View(bookLoan);
        }

        // POST: BookLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookLoanId,LoanDate,ReturnDate,FkCustomerId,FkBookId")] BookLoan bookLoan)
        {
            if (id != bookLoan.BookLoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookLoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookLoanExists(bookLoan.BookLoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "BookId", bookLoan.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", bookLoan.FkCustomerId);
            return View(bookLoan);
        }

        // GET: BookLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoans
                .Include(b => b.Book)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BookLoanId == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // POST: BookLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);
            if (bookLoan != null)
            {
                _context.BookLoans.Remove(bookLoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookLoanExists(int id)
        {
            return _context.BookLoans.Any(e => e.BookLoanId == id);
        }
    }
}
