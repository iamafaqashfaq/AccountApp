using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Models
{
    public class GLTran
    {
        public int Id { get; set; }
        public string TranType { get; set; } = string.Empty;
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double TranAmount { get; set; }
        public string TranDetail { get; set; } = string.Empty;
        public DateTime TranDate { get; set; }
        public string TranDateTimeStamp { get; set; } = DateTime.Now.ToString();
        public Customer? Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
    }
}
