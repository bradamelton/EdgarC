using System;
using DBu;

using System.Collections.Generic;

namespace EdgarC
{
    [DatabaseName("EdgarC")]
    public class Company : DBuClass
    {
        public string Name { get; set; }
        public Int64 SECNumber { get; set; }
        public string Symbol { get; set; }
        public object TickerId { get; set; }

        public Company() { }
        public Company(Int64 secNumber)
        {
            this.Load(new Dictionary<string, object>() { { "SECNumber", secNumber } });
        }
    }
}