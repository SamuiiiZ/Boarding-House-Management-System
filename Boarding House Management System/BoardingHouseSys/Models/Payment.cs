using System;

namespace BoardingHouseSys.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BoarderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string MonthPaid { get; set; }
        public int YearPaid { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        // Navigation property
        public string BoarderName { get; set; }
    }
}
