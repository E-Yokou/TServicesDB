using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TServicesDB.Models;

namespace TServicesDB.Controllers
{
    [Authorize(Roles = "admin")]
    public class TSRoutesController : Controller
    {
        private readonly TServicesDBContext _context;

        public TSRoutesController(TServicesDBContext context)
        {
            _context = context;
        }

        // GET: TSRoutes
        public async Task<IActionResult> Index()
        {
              return _context.TSRoutes != null ? 
                          View(await _context.TSRoutes.ToListAsync()) :
                          Problem("Entity set 'TServicesDBContext.TSRoutes'  is null.");
        }

        // GET: TSRoutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TSRoutes == null)
            {
                return NotFound();
            }

            var tSRoute = await _context.TSRoutes
                .FirstOrDefaultAsync(m => m.TSRouteID == id);
            if (tSRoute == null)
            {
                return NotFound();
            }

            return View(tSRoute);
        }

        // GET: TSRoutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TSRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TSRouteID,numberRoute,firstCity,lastCity,time")] TSRoute tSRoute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tSRoute);
        }

        // GET: TSRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TSRoutes == null)
            {
                return NotFound();
            }

            var tSRoute = await _context.TSRoutes.FindAsync(id);
            if (tSRoute == null)
            {
                return NotFound();
            }
            return View(tSRoute);
        }

        // POST: TSRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TSRouteID,numberRoute,firstCity,lastCity,time")] TSRoute tSRoute)
        {
            if (id != tSRoute.TSRouteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSRouteExists(tSRoute.TSRouteID))
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
            return View(tSRoute);
        }

        // GET: TSRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TSRoutes == null)
            {
                return NotFound();
            }

            var tSRoute = await _context.TSRoutes
                .FirstOrDefaultAsync(m => m.TSRouteID == id);
            if (tSRoute == null)
            {
                return NotFound();
            }

            return View(tSRoute);
        }

        // POST: TSRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.TSRoutes
                              .Include(r => r.TSBus)
                              .FirstOrDefaultAsync(r => r.TSRouteID == id);
            if (route == null)
            {
                return NotFound();
            }

            // Nullify the foreign key in related TSBus records
            foreach (var bus in route.TSBus)
            {
                bus.TSRouteId = null;
            }

            _context.TSRoutes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSRouteExists(int id)
        {
          return (_context.TSRoutes?.Any(e => e.TSRouteID == id)).GetValueOrDefault();
        }
    }
}
