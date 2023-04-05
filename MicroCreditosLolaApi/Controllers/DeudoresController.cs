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
    public class DeudoresController : Controller
    {
        private readonly PruebaContext _context;

        public DeudoresController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Deudores
        public async Task<IActionResult> Index()
        {
            var pruebaContext = _context.Deudores.Include(d => d.IdclienteNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: Deudores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deudores == null)
            {
                return NotFound();
            }

            var deudore = await _context.Deudores
                .Include(d => d.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Iddeudor == id);
            if (deudore == null)
            {
                return NotFound();
            }

            return View(deudore);
        }

        // GET: Deudores/Create
        public IActionResult Create()
        {
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente");
            return View();
        }

        // POST: Deudores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iddeudor,Idcliente,Nombre,Email,Montodebe,Montopagado,Montofinal")] Deudore deudore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deudore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", deudore.Idcliente);
            return View(deudore);
        }

        // GET: Deudores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deudores == null)
            {
                return NotFound();
            }

            var deudore = await _context.Deudores.FindAsync(id);
            if (deudore == null)
            {
                return NotFound();
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", deudore.Idcliente);
            return View(deudore);
        }

        // POST: Deudores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddeudor,Idcliente,Nombre,Email,Montodebe,Montopagado,Montofinal")] Deudore deudore)
        {
            if (id != deudore.Iddeudor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deudore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeudoreExists(deudore.Iddeudor))
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
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Idcliente", deudore.Idcliente);
            return View(deudore);
        }

        // GET: Deudores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deudores == null)
            {
                return NotFound();
            }

            var deudore = await _context.Deudores
                .Include(d => d.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Iddeudor == id);
            if (deudore == null)
            {
                return NotFound();
            }

            return View(deudore);
        }

        // POST: Deudores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deudores == null)
            {
                return Problem("Entity set 'PruebaContext.Deudores'  is null.");
            }
            var deudore = await _context.Deudores.FindAsync(id);
            if (deudore != null)
            {
                _context.Deudores.Remove(deudore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeudoreExists(int id)
        {
          return (_context.Deudores?.Any(e => e.Iddeudor == id)).GetValueOrDefault();
        }
    }
}
