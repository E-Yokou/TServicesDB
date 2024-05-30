using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TServicesDB.Models;

namespace TServicesDB.Controllers
{
    public class TSTicketsController : Controller
    {
        private readonly TServicesDBContext _context;

        public TSTicketsController(TServicesDBContext context)
        {
            _context = context;
        }

        // GET: TSTickets
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("admin"))
            {
                return _context.TSTickets != null ?
                        View(await _context.TSTickets.ToListAsync()) :
                        Problem("Entity set 'TServicesDBContext.TSTickets'  is null.");
            }
            
            if(User.IsInRole("client"))
            {
                if (_context.TSTickets == null)
                {
                    return Problem("Entity set 'TServicesDBContext.TSTickets' is null.");
                }

                // Получаем почту текущего пользователя
                string currentUserEmail = User.Identity.Name;

                // Ищем все билеты, где почта клиента совпадает с почтой пользователя
                var userTickets = await _context.TSTickets
                    .Where(t => t.mail_client == currentUserEmail)
                    .ToListAsync();

                return View(userTickets);
            }
            return Problem("Пользователь не авторизован!");
        }

        // GET: TSTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TSTickets == null)
            {
                return NotFound();
            }

            var tSTicket = await _context.TSTickets
                .FirstOrDefaultAsync(m => m.TSTicketID == id);
            if (tSTicket == null)
            {
                return NotFound();
            }

            return View(tSTicket);
        }

        // GET: TSTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TSTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TSTicketID,number_route,mail_client,stopover,date_sale,date_route,price,TSBusSecondId")] TSTicket tSTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tSTicket);
        }

        // GET: TSTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TSTickets == null)
            {
                return NotFound();
            }

            var tSTicket = await _context.TSTickets.FindAsync(id);
            if (tSTicket == null)
            {
                return NotFound();
            }
            return View(tSTicket);
        }

        // POST: TSTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TSTicketID,number_route,mail_client,stopover,date_sale,date_route,price,TSBusSecondId")] TSTicket tSTicket)
        {
            if (id != tSTicket.TSTicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSTicketExists(tSTicket.TSTicketID))
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
            return View(tSTicket);
        }

        // GET: TSTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TSTickets == null)
            {
                return NotFound();
            }

            var tSTicket = await _context.TSTickets
                .FirstOrDefaultAsync(m => m.TSTicketID == id);
            if (tSTicket == null)
            {
                return NotFound();
            }

            return View(tSTicket);
        }

        // POST: TSTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stopOver = _context.TSStopovers.Find(id);

            if (_context.TSTickets == null)
            {
                return Problem("Entity set 'TServicesDBContext.TSTickets'  is null.");
            }
            var tSTicket = await _context.TSTickets.FindAsync(id);
            if (tSTicket != null)
            {
                if (stopOver != null)
                {
                    TSBus bus = _context.TSBus.Find(stopOver.TSBusId);
                    if (bus != null)
                    {
                        bus.place += 1;
                        _context.Update(bus);
                    }
                }
                _context.TSTickets.Remove(tSTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSTicketExists(int id)
        {
            return (_context.TSTickets?.Any(e => e.TSTicketID == id)).GetValueOrDefault();
        }
    }
}
