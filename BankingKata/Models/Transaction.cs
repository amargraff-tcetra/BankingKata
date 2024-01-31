﻿namespace BankingKata.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public int account_id { get; set; }
        public DateTime date_time { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public bool success { get; set; }
    }
}
