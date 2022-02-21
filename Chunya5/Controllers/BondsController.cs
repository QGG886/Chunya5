using Chunya5.Data;
using Chunya5.Helper;
using Chunya5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chunya5.Controllers
{
    public class BondsController : Controller
    {
        private readonly MyDbContext _context;

        public BondsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Bonds
        public async Task<IActionResult> Index(string? bondsCode,string? bondsName,int page)
        {
            var pageSize = 5;
            var bonds = _context.Bonds.Where(x=>x.IsDelete == false) as IQueryable<Bonds>; ;
            if (!String.IsNullOrEmpty(bondsCode))
            {
                ViewBag.bondsCode = bondsCode;
                bonds = bonds
                    .Where(x => (x.BondsCode.Contains(bondsCode)));
            }
            if (!String.IsNullOrEmpty(bondsName))
            {
                ViewBag.bondsName = bondsName;
                bonds = bonds
                    .Where(x => (x.BondsName.Contains(bondsName)));
            }
            if (page == 0) page = 1;

            bonds.OrderBy(x => x.StartDate);

            return View(await PageList<Bonds>.CreatPageListAsync(bonds, page, pageSize));
        }



        public async Task<IActionResult> GetBondsByCode(string? code)
        {
            //if (!String.IsNullOrWhiteSpace(code))
            //{
            //    return NotFound();
            //}

            var bonds = await _context.Bonds.FirstOrDefaultAsync(x=>x.BondsCode==code);
            if (bonds == null)
            {
                return NotFound();
            }
            return Ok(bonds);
        }

        // GET: Bonds/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bonds bonds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bonds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(bonds);
        }

        // GET: Bonds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonds = await _context.Bonds.FindAsync(id);
            if (bonds == null)
            {
                return NotFound();
            }
            return View(bonds);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bonds bonds)
        {
            if (id != bonds.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bonds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BondsExists(bonds.Id))
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
            return View(bonds);
        }

        // GET: Bonds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonds = await _context.Bonds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bonds == null)
            {
                return NotFound();
            }

            return View(bonds);
        }

        // POST: Bonds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bonds = await _context.Bonds.FindAsync(id);
            if (bonds != null)
            {
                bonds.IsDelete = true;
            }
            //_context.Bonds.Remove(bonds);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BondsExists(int id)
        {
            return _context.Bonds.Any(e => e.Id == id);
        }
    }
}
