using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Models.ViewModel
{
    public class ARJournalVM
    {
        public int TranID { get; set; }
        public int CustomerCode { get; set; }
        public string Customer { get; set; } = string.Empty;
        public double Debit { get; set; }
        public double TotalCredit { get; set; }
    }
}