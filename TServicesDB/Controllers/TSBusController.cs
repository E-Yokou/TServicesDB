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
    public class TSBusController : Controller
    {
        private readonly TServicesDBContext _context;

        public TSBusController(TServicesDBContext context)
        {
            _context = context;
        }

        // GET: TSBus
        public async Task<IActionResult> Index()
        {
            var tServicesDBContext = _context.TSBus.Include(t => t.TSRoute);
            return View(await tServicesDBContext.ToListAsync());
        }

        // GET: TSBus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TSBus == null)
            {
                return NotFound();
            }

            var tSBus = await _context.TSBus
                .Include(t => t.TSRoute)
                .FirstOrDefaultAsync(m => m.TSBusID == id);
            if (tSBus == null)
            {
                return NotFound();
            }

            return View(tSBus);
        }

        // GET: TSBus/Create
        public IActionResult Create()
        {
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "TSRouteID");
            return View();
        }

        // POST: TSBus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TSBusID,type_bus,brand,goverment_number,place,TSRouteId")] TSBus tSBus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSBus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "TSRouteID", tSBus.TSRouteId);
            return View(tSBus);
        }

        // GET: TSBus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TSBus == null)
            {
                return NotFound();
            }

            var tSBus = await _context.TSBus.FindAsync(id);
            if (tSBus == null)
            {
                return NotFound();
            }
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "TSRouteID", tSBus.TSRouteId);
            return View(tSBus);
        }

        // POST: TSBus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TSBusID,type_bus,brand,goverment_number,place,TSRouteId")] TSBus tSBus)
        {
            if (id != tSBus.TSBusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSBus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSBusExists(tSBus.TSBusID))
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
            ViewData["TSRouteId"] = new SelectList(_context.TSRoutes, "TSRouteID", "TSRouteID", tSBus.TSRouteId);
            return View(tSBus);
        }

        // GET: TSBus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TSBus == null)
            {
                return NotFound();
            }

            var tSBus = await _context.TSBus
                .Include(t => t.TSRoute)
                .FirstOrDefaultAsync(m => m.TSBusID == id);
            if (tSBus == null)
            {
                return NotFound();
            }

            return View(tSBus);
        }

        // POST: TSBus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TSBus == null)
            {
                return Problem("Entity set 'TServicesDBContext.TSBus'  is null.");
            }
            var tSBus = await _context.TSBus.FindAsync(id);
            if (tSBus != null)
            {
                _context.TSBus.Remove(tSBus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSBusExists(int id)
        {
          return (_context.TSBus?.Any(e => e.TSBusID == id)).GetValueOrDefault();
        }
    }
}
