using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using TServicesDB.Areas.Identity.Data;
using TServicesDB.Models;

namespace TServicesDB.Controllers
{
    public class TSStopoversController : Controller
    {
        private readonly TServicesDBContext _context;
        private readonly UserManager<TServicesDBUser> _userManager;

        public TSStopoversController(TServicesDBContext context, UserManager<TServicesDBUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket(int id)
        {
            var stopOver = _context.TSStopovers.Find(id);

            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (stopOver != null && user != null)
            {
                TSRoute tSRoute = _context.TSRoutes.Find(stopOver.TSRouteId);

                TSTicket tickets = new TSTicket()
                {
                    number_route = tSRoute.numberRoute,
                    mail_client = await _userManager.GetUserNameAsync(user),
                    stopover = stopOver.name_stopover,
                    date_sale = DateTime.Now,
                    date_route = stopOver.time,
                    price = stopOver.price
                };

                // Уменьшаем количество мест в автобусе на 1
                TSBus bus = _context.TSBus.Find(stopOver.TSBusId);
                if (bus != null)
                {
                    if (bus.place > 0)
                    {
                        bus.place -= 1;
                        _context.Update(bus);
                        _context.Add(tickets);
                    }
                }

                await _context.SaveChangesAsync();

                // Возвращаем пользователя на страницу, с которой он пришёл
                var referer = Request.Headers["Referer"].ToString();
                return Redirect(referer);
            }
            return Json(new { success = false });
        }

        // GET: TSStopovers
        public async Task<IActionResult> Index()
        {
            var tServicesDBContext = _context.TSStopovers.Include(t => t.TSBus).Include(t => t.TSRoute);
            return View(await tServicesDBContext.ToListAsync());
        }

        // GET: TSStopovers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TSStopovers == null)
            {
                return NotFound();
            }

            var tSStopover = await _context.TSStopovers
                .Include(t => t.TSBus)
                .Include(t => t.TSRoute)
                .FirstOrDefaultAsync(m => m.TSStopoverID == id);
            if (tSStopover == null)
            {
                return NotFound();
            }

            return View(tSStopover);
        }

        // GET: TSStopovers/Create
        public IActionResult Create()
        {
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "place");
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "numberRoute");
            return View();
        }

        // POST: TSStopovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TSStopoverID,TSRouteId,start_city,end_city,name_stopover,price,TSBusId, time")] TSStopover tSStopover)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSStopover);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "place", tSStopover.TSBusId);
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "numberRoute", tSStopover.TSRouteId);
            return View(tSStopover);
        }

        // GET: TSStopovers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TSStopovers == null)
            {
                return NotFound();
            }

            var tSStopover = await _context.TSStopovers.FindAsync(id);
            if (tSStopover == null)
            {
                return NotFound();
            }
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "place", tSStopover.TSBusId);
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "numberRoute", tSStopover.TSRouteId);
            return View(tSStopover);
        }

        // POST: TSStopovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TSStopoverID,TSRouteId,start_city,end_city,name_stopover,price,TSBusId, time")] TSStopover tSStopover)
        {
            if (id != tSStopover.TSStopoverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSStopover);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSStopoverExists(tSStopover.TSStopoverID))
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
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "place", tSStopover.TSBusId);
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "numberRoute", tSStopover.TSRouteId);
            return View(tSStopover);
        }

        // GET: TSStopovers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TSStopovers == null)
            {
                return NotFound();
            }

            var tSStopover = await _context.TSStopovers
                .Include(t => t.TSBus)
                .Include(t => t.TSRoute)
                .FirstOrDefaultAsync(m => m.TSStopoverID == id);
            if (tSStopover == null)
            {
                return NotFound();
            }

            return View(tSStopover);
        }

        // POST: TSStopovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TSStopovers == null)
            {
                return Problem("Entity set 'TServicesDBContext.TSStopovers'  is null.");
            }
            var tSStopover = await _context.TSStopovers.FindAsync(id);
            if (tSStopover != null)
            {
                _context.TSStopovers.Remove(tSStopover);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSStopoverExists(int id)
        {
          return (_context.TSStopovers?.Any(e => e.TSStopoverID == id)).GetValueOrDefault();
        }
    }
}
