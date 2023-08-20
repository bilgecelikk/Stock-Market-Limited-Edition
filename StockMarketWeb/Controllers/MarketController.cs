using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using StockMarketWeb.Data;
using StockMarketWeb.Service;
using System.Reflection.Metadata;
using StockMarketWeb.Models;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using ConnectionState = System.Data.ConnectionState;
using Microsoft.Extensions.DependencyInjection;

namespace StockMarketWeb.Controllers
{
    public class MarketController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly MarketService _ms;
        public MarketController(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            _ms = new MarketService(serviceProvider);
        }
        public IActionResult Index()
        {
            IEnumerable<StockData> objMarketAsAList = _db.AllStocks;
            return View(objMarketAsAList);
        }

        // get method
        public IActionResult Create()
        {
            return View();
        }

        // post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockData obj)
        {
            bool valueExists = _db.AllStocks.Any(stock => stock.Name == obj.Name);
            bool NameinMyDatabase = _db.Tickers.Any(stock => stock.Name == obj.Name);

            if (!valueExists && NameinMyDatabase)
            {
                await _ms.creationNoError(obj);

                return RedirectToAction("Index");
            }

            string alertType = "alert-danger";
            string alertMessage = "<strong>Oh snap!</strong> You already added it to database!";
            if (!NameinMyDatabase)
            {
                alertType = "alert-warning";
                alertMessage = "<strong>Oh snap!</strong> Either you gave a wrong abbreviation or I am not able to fetch the data of this one. Yet!";
            }

            TempData["AlertType"] = alertType;
            TempData["AlertMessage"] = alertMessage;

            return RedirectToAction("Create");
        }


        // get method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.AllStocks.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StockData obj)
        {
            _db.AllStocks.Remove(obj);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            IEnumerable<IndividualData> allIndividualDataFromId = _db.IndividualData.Where(x => x.StockDataId == id);

            return View(allIndividualDataFromId);
        }
    }
}
