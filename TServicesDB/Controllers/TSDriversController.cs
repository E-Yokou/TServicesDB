using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TServicesDB.Models;

namespace TServicesDB.Controllers
{
    [Authorize(Roles = "admin")]
    public class TSDriversController : Controller
    {
        private readonly TServicesDBContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public TSDriversController(TServicesDBContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: TSDrivers
        public async Task<IActionResult> Index()
        {
            var tServicesDBContext = _context.TSDrivers.Include(t => t.TSBus);
            return View(await tServicesDBContext.ToListAsync());
        }

        // GET: TSDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TSDrivers == null)
            {
                return NotFound();
            }

            var tSDriver = await _context.TSDrivers
                .Include(t => t.TSBus)
                .FirstOrDefaultAsync(m => m.TSDriverID == id);
            if (tSDriver == null)
            {
                return NotFound();
            }

            return View(tSDriver);
        }

        // GET: TSDrivers/Create
        public IActionResult Create()
        {
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "goverment_number");
            return View();
        }

        // POST: TSDrivers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TSDriverID,name,middleName,lastName,TSBusId,Photo")] TSDriver tSDriver, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = "/Files/" + upload.FileName;
                    using (var fileStream = new
                   FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    tSDriver.Photo = path;
                }

                _context.Add(tSDriver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "goverment_number", tSDriver.TSBusId);
            return View(tSDriver);
        }

        // GET: TSDrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TSDrivers == null)
            {
                return NotFound();
            }

            var tSDriver = await _context.TSDrivers.FindAsync(id);
            if (tSDriver == null)
            {
                return NotFound();
            }
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "goverment_number", tSDriver.TSBusId);
            return View(tSDriver);
        }

        // POST: TSDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TSDriverID,name,middleName,lastName,TSBusId,Photo")] TSDriver tSDriver)
        {
            if (id != tSDriver.TSDriverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSDriver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSDriverExists(tSDriver.TSDriverID))
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
            ViewData["TSBusId"] = new SelectList(_context.TSBus, "TSBusID", "goverment_number", tSDriver.TSBusId);
            return View(tSDriver);
        }

        // GET: TSDrivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TSDrivers == null)
            {
                return NotFound();
            }

            var tSDriver = await _context.TSDrivers
                .Include(t => t.TSBus)
                .FirstOrDefaultAsync(m => m.TSDriverID == id);
            if (tSDriver == null)
            {
                return NotFound();
            }

            return View(tSDriver);
        }

        // POST: TSDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TSDrivers == null)
            {
                return Problem("Entity set 'TServicesDBContext.TSDrivers'  is null.");
            }
            var tSDriver = await _context.TSDrivers.FindAsync(id);
            if (tSDriver != null)
            {
                _context.TSDrivers.Remove(tSDriver);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSDriverExists(int id)
        {
            return (_context.TSDrivers?.Any(e => e.TSDriverID == id)).GetValueOrDefault();
        }
    }
}
