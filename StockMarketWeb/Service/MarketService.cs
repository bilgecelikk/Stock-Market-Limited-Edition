using DocumentFormat.OpenXml.Office2010.Excel;
using StockMarketWeb.Data;
using StockMarketWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace StockMarketWeb.Service
{
    public class MarketService
    {
        private readonly IServiceProvider _serviceProvider;

        public MarketService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private string API_yahoo = "https://query1.finance.yahoo.com/v7/finance/download/";

        public async Task<bool> RenewData(StockData undetailedData)
        {
            undetailedData.DataExtractionTime = DateTime.Now;

            DateTime startDate = new DateTime(2010, 1, 1); // Start date
            DateTime endDate = DateTime.Now; // End date

            // Convert dates to Unix timestamps
            long startTimestamp = (long)(startDate - new DateTime(1970, 1, 1)).TotalSeconds;
            long endTimestamp = (long)(endDate - new DateTime(1970, 1, 1)).TotalSeconds;

            // Build the query URL
            string queryUrl = $"{API_yahoo}{undetailedData.Name}?period1={startTimestamp}&period2={endTimestamp}&interval=1d&events=history&includeAdjustedClose=true";

            // Create an HttpClient instance
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(queryUrl);
                    string csvData = await response.Content.ReadAsStringAsync();
                    List<IndividualData> stockRecords = new List<IndividualData>();

                    string[] lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length > 0)
                    {
                        string headerLine = lines[0];
                        lines = lines.Skip(1).ToArray();
                    }

                    foreach (string line in lines)
                    {
                        string[] fields = line.Split(',');

                        if (fields.Length >= 2) // Assuming at least 2 columns in the CSV
                        {
                            IndividualData obj = new IndividualData
                            {
                                DateTime = DateTime.Parse(fields[0]),
                                Open = decimal.Parse(fields[1]),
                                High = decimal.Parse(fields[2]),
                                Low = decimal.Parse(fields[3]),
                                Close = decimal.Parse(fields[4]),
                                WClose = decimal.Parse(fields[5]),
                                Volume = int.Parse(fields[6]),
                                StockDataId = undetailedData.Id
                            };

                            stockRecords.Add(obj);
                        }
                    }

                    undetailedData.Datas = stockRecords;
                    return true;
                }
            }
        }

        public async Task<bool> creationNoError(StockData obj)
        {
            var result = await RenewData(obj);

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                obj.FullName = (dbContext.Tickers
                    .Where(person => person.Name == obj.Name)
                    .Select(person => person.FullName)).Single();

                if (result)
                {
                    await dbContext.AllStocks.AddAsync(obj);
                    await dbContext.IndividualData.AddRangeAsync(obj.Datas);
                }

                await dbContext.SaveChangesAsync();
            }

            return true;
        }
    }
}
