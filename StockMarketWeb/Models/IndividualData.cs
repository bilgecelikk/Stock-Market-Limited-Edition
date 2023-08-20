using System.ComponentModel.DataAnnotations;

namespace StockMarketWeb.Models
{
    public class IndividualData
    {
        [Key]
        public int Id { get; set; }
        public int PeriodType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
        public decimal WClose { get; set; }
        public int StockDataId { get; set; }
        public virtual StockData StockData { get; set; }
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