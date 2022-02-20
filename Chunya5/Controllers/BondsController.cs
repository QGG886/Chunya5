#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chunya5.Data;
using Chunya5.Models;

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
        public async Task<IActionResult> Index()
        {

            return View(await _context.Bonds.ToListAsync());
        }

        // GET: Bonds/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Bonds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bonds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BondsCode,BondsName,Market,ParValue,Rate,IsDel,StartDate,EndDate,AddTime,UpdateTime,AddMan,ModifyMan,Term,Frequency")] Bonds bonds)
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

        // POST: Bonds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BondsCode,BondsName,Market,ParValue,Rate,IsDel,StartDate,EndDate,AddTime,UpdateTime,AddMan,ModifyMan,Term,Frequency")] Bonds bonds)
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
            _context.Bonds.Remove(bonds);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BondsExists(int id)
        {
            return _context.Bonds.Any(e => e.Id == id);
        }
    }
}
