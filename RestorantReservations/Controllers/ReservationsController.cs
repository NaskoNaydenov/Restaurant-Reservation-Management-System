using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestorantReservations.Data;
using RestorantReservations.Models;

namespace RestorantReservations.Controllers
{
    [Authorize]
    
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
                var reservations = _context.Reservation.Include(r => r.table);
                return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.table)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["TableId"] = new SelectList(_context.Table.Where(a => a.available), "id", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,date,note,TableId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var table = await _context.Table.FindAsync(reservation.TableId);
                if (table != null)
                {
                    table.available = false;
                    _context.Update(table);
                }
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TableId"] = new SelectList(_context.Table.Where(a => a.available), "id", "Name", reservation.TableId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var availableTables = _context.Table.Where(a => a.available || a.id == reservation.TableId);
            ViewData["TableId"] = new SelectList(_context.Table, "id", "Name", reservation.TableId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,date,note,TableId")] Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingReservation = await _context.Reservation.AsNoTracking().FirstOrDefaultAsync(r => r.ID == id);
                    if (existingReservation != null && existingReservation.TableId != reservation.TableId)
                    {
                        var oldTable = await _context.Table.FindAsync(existingReservation.TableId);
                        if (oldTable != null)
                        {
                            oldTable.available = true;
                            _context.Update(oldTable);
                        }

                        var newTable = await _context.Table.FindAsync(reservation.TableId);
                        if (newTable != null)
                        {
                            newTable.available = false;
                            _context.Update(newTable);
                        }
                    }

                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ID))
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

            var availableTables = _context.Table.Where(a => a.available || a.id == reservation.TableId);
            ViewData["TableId"] = new SelectList(availableTables, "id", "Name", reservation.TableId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.table)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.Include(r => r.table).FirstOrDefaultAsync(r => r.ID == id); /*.FindAsync(id)*/;

            if (reservation != null)
            {
                if (reservation.TableId.HasValue) 
                {
                    var table = await _context.Table.FindAsync(reservation.TableId);
                    if (table != null)
                    {
                        table.available = true;
                        _context.Update(table);
                    }
                }

                _context.Reservation.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservation?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
