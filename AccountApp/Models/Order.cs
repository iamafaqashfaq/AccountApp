using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool Posted { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("Product")]
        public int ProductCode { get; set; }
        public Product? Product { get; set; }
        public string? ChalanNumber { get; set; }
        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }
        public double SaleRate { get; set; }
        public double Sale { get; set; }
        public double SaleWithoutCommission { get; set; }
        [ForeignKey("SaleBook")]
        public int SaleBookId { get; set; }
        public SaleBook? SaleBook { get; set; }
    }
}
