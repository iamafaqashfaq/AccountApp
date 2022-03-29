using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public DateTime OrderDetailDate { get; set; }
        [ForeignKey("Order")]
        public int OrderNum { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Commission { get; set; }
        public double TotalAmount { get { return (Rate * Quantity) + (Quantity * Commission); } set { } }
        public bool Posted { get; set; }
        public Order? Order { get; set; }
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("SaleBook")]
        public int SaleBookId { get; set; }
        public SaleBook? SaleBook { get; set; }
    }
}
