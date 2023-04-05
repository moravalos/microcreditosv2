using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroCreditosLolaApi.Models;

namespace MicroCreditosLolaApi.Controllers
{
    public class HistorialsController : Controller
    {
        private readonly PruebaContext _context;

        public HistorialsController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Historials
        public async Task<IActionResult> Index(int? idclientes)
        {
            var pruebaContext = _context.Historials.Include(h => h.IdclienteNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: Historials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Historials == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials
                .Include(h => h.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Idmonto == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // GET: Historials/Create
        public IActionResult Create()
        {
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente");
            return View();
        }

        // POST: Historials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmonto,Monto,Periodopago,Fechadepago,Status,Idcliente")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", historial.Idcliente);
            return View(historial);
        }

        // GET: Historials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Historials == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", historial.Idcliente);
            return View(historial);
        }

        // POST: Historials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmonto,Monto,Periodopago,Fechadepago,Status,Idcliente")] Historial historial)
        {
            if (id != historial.Idmonto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialExists(historial.Idmonto))
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
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", historial.Idcliente);
            return View(historial);
        }

        // GET: Historials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Historials == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials
                .Include(h => h.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Idmonto == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // POST: Historials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Historials == null)
            {
                return Problem("Entity set 'PruebaContext.Historials'  is null.");
            }
            var historial = await _context.Historials.FindAsync(id);
            if (historial != null)
            {
                _context.Historials.Remove(historial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialExists(int id)
        {
          return (_context.Historials?.Any(e => e.Idmonto == id)).GetValueOrDefault();
        }
    }
}
