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

namespace Chunya5.Controllers
{
    public class TradesController : Controller
    {
        private readonly MyDbContext _context;

        public TradesController(MyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Trades
        public async Task<IActionResult> Index(string tradeAccount, DateTime tradeDate, int page)
        {
            var pageSize = 8;
            var trades = _context.Trade.Where(x=>x.IsDelete == false) as IQueryable<Trade>;

            if (!String.IsNullOrEmpty(tradeAccount))
            {
                trades = trades.Where(x=>x.Account.Contains(tradeAccount));
            }
            if(tradeDate.Year != 1)
            trades = trades.Where(x => x.TradeDate.Year == tradeDate.Year && x.TradeDate.Month == tradeDate.Month && x.TradeDate.Day == tradeDate.Day);
            

            if (page == 0) page = 1;

            trades.OrderBy(x => x.TradeDate);

            return View(await PageList<Trade>.CreatPageListAsync(trades, page, pageSize));
        }

        // GET: Trades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trade == null)
            {
                return NotFound();
            }

            return View(trade);
        }

        // GET: Trades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trade trade)
        {
            if (ModelState.IsValid)
            {
                var bonds = _context.Bonds.FirstOrDefault(x=>x.BondsCode == trade.BondsCode && x.IsDelete == false);
                if(bonds != null)
                {
                    _context.Add(trade);

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trade);
        }

        // GET: Trades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trade.FindAsync(id);
            
            if (trade == null)
            {
                return NotFound();
            }
            return View(trade);
        }

        // POST: Trades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Trade trade)
        {
            if (id != trade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trade);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradeExists(trade.Id))
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
            return View(trade);
        }

        // GET: Trades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trade == null)
            {
                return NotFound();
            }

            return View(trade);
        }

        // POST: Trades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trade = await _context.Trade.FindAsync(id);
            
            _context.Trade.Remove(trade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TradeExists(int id)
        {
            return _context.Trade.Any(e => e.Id == id);
        }

        

        
        
    }
}
