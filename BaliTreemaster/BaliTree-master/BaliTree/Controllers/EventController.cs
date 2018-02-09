using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaliTreeData;
using BaliTree.Models.Event;
using BaliTreeData.Models;
using Microsoft.EntityFrameworkCore;

namespace BaliTree.Controllers
{
    public class EventController : Controller
    {
        private readonly BaliTreeContext _context;
        private IStockChanges _stockChanges;
        private IStockItems _stockItems;
        private IStockTypes _stockTypes;
        private IStockEvents _stockEvents;

        public EventController(BaliTreeContext context, IStockChanges stockChanges, IStockItems stockItems, IStockTypes stockTypes, IStockEvents stockEvents)
        {
            _context = context;
            _stockChanges = stockChanges;
            _stockItems = stockItems;
            _stockTypes = stockTypes;
            _stockEvents = stockEvents;
        }

        public IActionResult Index()
        {
            var VM = new IndexVM();
            VM.AllEvents = _stockEvents.GetAll();
            return View(VM.AllEvents);
        }

        public IActionResult Success()
        {
            return View();
        }

        // GET: Event/Sold
        //works
        [HttpGet]
        public IActionResult Sold()
        {
            var VM = new SoldVM();
            VM.stockEvent = new StockEvent();
            VM.StockItems = _stockItems.GetAll();
            VM.stockEvent.EventType = Event.Sold;

            return View(VM);
        }

        // POST: Event/Sold
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sold([Bind("Id,Date,StockItem,EventType,Change")] StockEvent stockEvent, int stockId)
        {
            stockEvent.StockItem = _stockItems.GetItemByID(stockId);

            int nowInStock = _stockChanges.Sold(stockEvent);

            if (nowInStock >= 0)
            {
                stockEvent.StockItem.InStock = nowInStock;

                if (ModelState.IsValid)
                {
                    _context.Update(stockEvent.StockItem);
                    _context.Add(stockEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Success));
                }
                return View(stockEvent);
            }

            else return View(stockEvent);
        }

        //Get: Event/Broken
        //works
        [HttpGet]
        public IActionResult Broken()
        {
            var VM = new BrokenVM();
            VM.stockEvent = new StockEvent();
            VM.StockItems = _stockItems.GetAll();
            VM.stockEvent.EventType = Event.Sold;

            return View(VM);
        }


        //POST: Event/Broken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Broken([Bind("Id,Date,StockItem,EventType,Change")] StockEvent stockEvent, int stockId)
        {
            stockEvent.StockItem = _stockItems.GetItemByID(stockId);

            int nowInStock = _stockChanges.Broken(stockEvent);

            if (nowInStock >= 0)
            {
                stockEvent.StockItem.InStock = nowInStock;

                if (ModelState.IsValid)
                {
                    _context.Update(stockEvent.StockItem);
                    _context.Add(stockEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Success));
                }
                return View(stockEvent);
            }

            else return View(stockEvent);
        }

        //Get: Event/Recieved
        //works
        [HttpGet]
        public IActionResult Recieved()
        {
            var VM = new RecievedVM();
            VM.stockEvent = new StockEvent();
            VM.StockItems = _stockItems.GetAll();
            VM.stockEvent.EventType = Event.Sold;

            return View(VM);
        }

        //POST: Event/Broken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recieved([Bind("Id,Date,StockItem,EventType,Change")] StockEvent stockEvent, int stockId)
        {
            stockEvent.StockItem = _stockItems.GetItemByID(stockId);

            int nowInStock = _stockChanges.Recieved(stockEvent);

            if (nowInStock >= 0)
            {
                stockEvent.StockItem.InStock = nowInStock;

                if (ModelState.IsValid)
                {
                    _context.Update(stockEvent.StockItem);
                    _context.Add(stockEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Success));
                }
                return View(stockEvent);
            }

            else return View(stockEvent);
        }

        //Get: Event/Adjustment
        //works
        [HttpGet]
        public IActionResult Adjustment()
        {
            var VM = new AdjustmentVM();
            VM.stockEvent = new StockEvent();
            VM.StockItems = _stockItems.GetAll();
            VM.stockEvent.EventType = Event.Adjustment;

            return View(VM);
        }

        //POST: Event/Adjustmebnt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adjustment([Bind("Id,Date,StockItem,EventType,Change")] StockEvent stockEvent, int stockId)
        {
            //backup new level
            int nowInStock = stockEvent.Change;

            //pull item from db
            var item = _stockItems.GetItemByID(stockId);

            if (nowInStock >= 0)
            {
                item.InStock= nowInStock;

                if (ModelState.IsValid)
                {
                    _context.Update(item);
                    _context.Add(stockEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Success));
                }
                return View(stockEvent);
            }

            else return View(stockEvent);
        }

    }
}