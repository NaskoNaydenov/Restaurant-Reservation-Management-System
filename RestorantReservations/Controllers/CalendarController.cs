using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestorantReservations.Data;
using RestorantReservations.Models;

namespace RestorantReservations.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _context.Reservation
                .Include(r => r.table) 
                .Select(r => new
                {
                    id = r.ID, 
                    title = $"Маса {r.table.Name} - {r.table.capacity} места", 
                    start = r.date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = r.date.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss"), 
                    available = r.table.available 
                })
                .ToList();

            return Json(reservations); 
        }
    }
}
