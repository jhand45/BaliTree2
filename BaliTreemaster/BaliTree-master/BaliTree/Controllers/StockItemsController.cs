using BaliTree.Models.StockItems;
using BaliTreeData;
using BaliTreeData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BaliTree.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly BaliTreeContext _context;
        private IStockChanges _stockChanges;
        private IStockItems _stockItems;
        private IStockTypes _stockTypes;

        public StockItemsController(BaliTreeContext context, IStockChanges stockChanges, IStockItems stockItems, IStockTypes stockTypes)
        {
            _context = context;
            _stockChanges = stockChanges;
            _stockItems = stockItems;
            _stockTypes = stockTypes;
        }

        // GET: StockItems
        //works
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockItems
                .Include(x=> x.ItemType)
                .ToListAsync());
        }

        // GET: StockItems/Details/5
        //works
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(x=>x.ItemType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItems/Create
        //DateTime filled with nonsense
        public IActionResult Create()
        {
            var VM = new CreateVM();
            IEnumerable<StockType> AllTypes = _stockTypes.GetAllTypes();

            VM.StockItem = new StockItem();
            VM.AllTypes = AllTypes;
            return View(VM);
        }

        // POST: StockItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,Date,CostPrice,InStock")] StockItem stockItem, int TypeId) //removed type from binding as coming through null
        {
            StockType stockItemType = _stockTypes.GetTypeByID(TypeId);
            stockItem.ItemType = stockItemType;

            var Event = new StockEvent();
            Event.Date = stockItem.Date;
            Event.StockItem = stockItem;
            Event.Change = stockItem.InStock;

            if (ModelState.IsValid)
            {
                _context.Add(stockItem);
                _context.Add(Event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockItem);
        }

        // GET: StockItems/Edit/5
        // works
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.Include(x=>x.ItemType).SingleOrDefaultAsync(m => m.Id == id); //refactor this

            if (stockItem == null)
            {
                return NotFound();
            }

            var VM = new EditVM();
            VM.StockItem = stockItem;
            VM.AllTypes = _stockTypes.GetAllTypes();

            return View(VM);
        }

        // POST: StockItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind("Id,ItemName,Date,CostPrice,InStock")] StockItem stockItem, int TypeId) //type not being sent, using Id to pick it out
        {
            ////turn int into type
            StockType stockItemType = _stockTypes.GetTypeByID(TypeId);
            stockItem.ItemType = stockItemType;

            //remember to create an event edit so user can alter the initial amount recieved
            if (id != stockItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.Id))
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
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        //works fine
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockItem = await _context.StockItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.StockItems.Remove(stockItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(int id)
        {
            return _context.StockItems.Any(e => e.Id == id);
        }
    }
}
