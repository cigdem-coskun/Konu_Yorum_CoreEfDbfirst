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
    public class YorumScaffoldingController : Controller
    {
        private readonly BA_KonuYorumCoreContext _context;

        //public YorumScaffoldingController(BA_KonuYorumCoreContext context)
        //{
        //    _context = context;
        //}
        public YorumScaffoldingController()
        {
            _context = new BA_KonuYorumCoreContext();
        }
        // GET: YorumScaffolding
        public async Task<IActionResult> Index()
        {
            var bA_KonuYorumCoreContext = _context.Yorum.Include(y => y.Konu);
            return View(await bA_KonuYorumCoreContext.ToListAsync());
        }

        // GET: YorumScaffolding/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Yorum == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorum
                .Include(y => y.Konu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // GET: YorumScaffolding/Create
        public IActionResult Create()
        {
            ViewData["KonuId"] = new SelectList(_context.Konu, "Id", "Baslik");
            return View();
        }

        // POST: YorumScaffolding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Icerik,Yorumcu,Puan,KonuId")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yorum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KonuId"] = new SelectList(_context.Konu, "Id", "Baslik", yorum.KonuId);
            return View(yorum);
        }

        // GET: YorumScaffolding/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Yorum == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorum.FindAsync(id);
            if (yorum == null)
            {
                return NotFound();
            }
            ViewData["KonuId"] = new SelectList(_context.Konu, "Id", "Baslik", yorum.KonuId);
            return View(yorum);
        }

        // POST: YorumScaffolding/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Icerik,Yorumcu,Puan,KonuId")] Yorum yorum)
        {
            if (id != yorum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yorum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YorumExists(yorum.Id))
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
            ViewData["KonuId"] = new SelectList(_context.Konu, "Id", "Baslik", yorum.KonuId);
            return View(yorum);
        }

        // GET: YorumScaffolding/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Yorum == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorum
                .Include(y => y.Konu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // POST: YorumScaffolding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Yorum == null)
            {
                return Problem("Entity set 'BA_KonuYorumCoreContext.Yorum'  is null.");
            }
            var yorum = await _context.Yorum.FindAsync(id);
            if (yorum != null)
            {
                _context.Yorum.Remove(yorum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YorumExists(int id)
        {
          return _context.Yorum.Any(e => e.Id == id);
        }
    }
}
