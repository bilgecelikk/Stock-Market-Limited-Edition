using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StockMarketWeb.Models
{
    public class Ticker
    {
        [Key]
        public string Name { get; set; }
        public string? FullName { get; set; }
    }
}
