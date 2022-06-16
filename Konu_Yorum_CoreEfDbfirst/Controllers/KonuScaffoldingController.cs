using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Konu_Yorum_CoreEfDbfirst.DataAccess;

namespace Konu_Yorum_CoreEfDbfirst.Controllers
{
    public class KonuScaffoldingController : Controller
    {
        private readonly BA_KonuYorumCoreContext _context;

        //public KonuScaffoldingController(BA_KonuYorumCoreContext context)
        //{
        //    _context = context;
        //}
        public KonuScaffoldingController()
        {
            _context = new BA_KonuYorumCoreContext();
        }
        // GET: KonuScaffolding
        public async Task<IActionResult> Index()
        {
              return View(await _context.Konu.ToListAsync());
        }

        // GET: KonuScaffolding/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Konu == null)
            {
                return NotFound();
            }

            var konu = await _context.Konu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konu == null)
            {
                return NotFound();
            }

            return View(konu);
        }

        // GET: KonuScaffolding/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KonuScaffolding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Baslik,Aciklama")] Konu konu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(konu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(konu);
        }

        // GET: KonuScaffolding/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Konu == null)
            {
                return NotFound();
            }

            var konu = await _context.Konu.FindAsync(id);
            if (konu == null)
            {
                return NotFound();
            }
            return View(konu);
        }

        // POST: KonuScaffolding/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Aciklama")] Konu konu)
        {
            if (id != konu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonuExists(konu.Id))
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
            return View(konu);
        }

        // GET: KonuScaffolding/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Konu == null)
            {
                return NotFound();
            }

            var konu = await _context.Konu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konu == null)
            {
                return NotFound();
            }

            return View(konu);
        }

        // POST: KonuScaffolding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Konu == null)
            {
                return Problem("Entity set 'BA_KonuYorumCoreContext.Konu'  is null.");
            }
            var konu = await _context.Konu.FindAsync(id);
            if (konu != null)
            {
                _context.Konu.Remove(konu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonuExists(int id)
        {
          return _context.Konu.Any(e => e.Id == id);
        }
    }
}
