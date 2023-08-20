using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StockMarketWeb.Models
{
    public class StockData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? FullName { get; set; }
        public DateTime DataExtractionTime { get; set; }
        // one-to-many relationship
        public virtual List<IndividualData> Datas { get; set; }
    }
}

/*
  +-----------------------------------+
  |           StockData               |
  +-----------------------------------+
  | Id               [PK] (int)       |
  | Name            (string)          |
  | FullName        (string)          |
  | DataExtractionTime (DateTime)     |
  +-----------------------------------+
		  |
		  |   1   N
		  |
		  v
  +-----------------------------------+
  |        IndividualData             |
  +-----------------------------------+
  | Id               [PK] (int)       |
  | DateTime        (DateTime)        |
  | Open            (decimal)         |
  | High            (decimal)         |
  | Low             (decimal)         |
  | Close           (decimal)         |
  | Volume          (int)             |
  | WClose          (decimal)         |
  | StockDataId     [FK] (int)        |
  +-----------------------------------+
*/
