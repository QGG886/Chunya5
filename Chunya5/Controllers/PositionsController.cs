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
using Chunya5.Helper;
using Chunya5.ViewModels;

namespace Chunya5.Controllers
{
    public class PositionsController : Controller
    {
        private readonly MyDbContext _context;

        public PositionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Positions
        public async Task<IActionResult> Index(string account, DateTime? tradeDate, int page,bool isSearch)
        {
            var pageSize = 5;
            var positions = _context.Positions.Where(x => x.IsDelete == false) as IQueryable<Positions>;
            if (!String.IsNullOrEmpty(account))
            {
                ViewBag.account = account;
                positions = positions
                    .Where(x => (x.Account.Contains(account)));
            }
            if (!string.IsNullOrEmpty(tradeDate.ToString()))
            {
                ViewBag.date = tradeDate;
                positions = positions
                    .Where(x => (x.TradeDate.ToString().Contains(tradeDate.ToString())));
            }
            if (page == 0) page = 1;

            positions.OrderBy(x => x.TradeDate);
            
            return View(await PageList<Positions>.CreatPageListAsync(positions, page, pageSize));
        }
        


        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AddTime,UpdateTime,AddMan,ModifyMan,IsDeleteete,Accout,BondsCode,TradeDate,NetCost,InterestCost,AccInterest,AccUninterestImcome,RealizedInterestIncome,TotalInterestIncome,TradingProloss,FloatingPl,DenominattonHeld")] Positions position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AddTime,UpdateTime,AddMan,ModifyMan,IsDeleteete,Accout,BondsCode,TradeDate,NetCost,InterestCost,AccInterest,AccUninterestImcome,RealizedInterestIncome,TotalInterestIncome,TradingProloss,FloatingPl,DenominattonHeld")] Positions position)
        {
            if (id != position.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.ID))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.ID == id);
        }
    }
}
