using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaliTreeData;
using BaliTreeData.Models;
using BaliTree.Models.StockTypes;

namespace BaliTree.Controllers
{
    public class StockTypesController : Controller
    {
        private readonly BaliTreeContext _context;
        private IStockItems _stockItems;
        private IAnalytics _analytics;

        public StockTypesController(BaliTreeContext context, IAnalytics analytics, IStockItems stockItems)
        {
            _context = context;
            _analytics = analytics;
            _stockItems = stockItems;
        }

        // GET: StockTypes

        public async Task<IActionResult> Index()
        {
            //return View(await _context.StockTypes.ToListAsync());
            var VM = new IndexVM();
            VM.SubIndex = new List<SubIndexVM>();
            VM.TypesToList = await _context.StockTypes.ToListAsync();

            //the difficulty is populating the parameters for each item in the listing

            foreach(var type in VM.TypesToList)
            {
                var SVM = new SubIndexVM();
                //SVM.IndexVM = VM;
                SVM.StockType = type;
                SVM.ItemsOfType = _stockItems.ItemsOfType(type);
                SVM.AverageCost = _analytics.AverageCost(SVM.ItemsOfType);
                SVM.InStock = _analytics.InStock(SVM.ItemsOfType);
                SVM.RRP = _analytics.RRP(SVM.AverageCost);
                //to VM subindex?
                VM.SubIndex.Add(SVM);
            }

            return View(VM);
        }

        // GET: StockTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var VM = new DetailsVM();
            VM.StockType = await _context.StockTypes
                .SingleOrDefaultAsync(m => m.Id == id);

            if (VM.StockType == null)
            {
                return NotFound();
            }

            //list of all items of this type
            VM.ItemsOfType = _stockItems.ItemsOfType(VM.StockType);

            VM.AverageCost = _analytics.AverageCost(VM.ItemsOfType);
            VM.InStock = _analytics.InStock(VM.ItemsOfType);
            VM.RRP = _analytics.RRP(VM.AverageCost);


            return View(VM);
        }

        // GET: StockTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,AverageCost,RRP,InStock")] StockType stockType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockType);
        }

        // GET: StockTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockType = await _context.StockTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (stockType == null)
            {
                return NotFound();
            }
            return View(stockType);
        }

        // POST: StockTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,AverageCost,RRP,InStock")] StockType stockType)
        {
            if (id != stockType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTypeExists(stockType.Id))
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
            return View(stockType);
        }

        // GET: StockTypes/Delete/5
        //works fine
        //worked fine, a couple of items dont want to be deleted
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockType = await _context.StockTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stockType == null)
            {
                return NotFound();
            }

            return View(stockType);
        }

        // POST: StockTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockType = await _context.StockTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.StockTypes.Remove(stockType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTypeExists(int id)
        {
            return _context.StockTypes.Any(e => e.Id == id);
        }
    }
}
