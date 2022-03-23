using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountApp.Views
{
    public partial class PostSale : Form
    {
        public PostSale()
        {
            InitializeComponent();
        }

        private void PostSale_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadGrid();
        }

        void LoadGrid()
        {
            using(var db = new DataContext())
            {
                var data = db.OrderDetails.Include(c => c.Customer).Include(p => p.Product)
                    .Include(o => o.Order).Where(o => o.Posted == false).OrderBy(c => c.CustomerID).Select(x => new
                    {
                        OrderNum = x.OrderNum,
                        Customer = x.Customer!.Name,
                        Product = x.Product!.Name.ToString() + "  "+ x.Quantity.ToString() + " x " + x.Rate.ToString() + "=" + x.TotalAmount.ToString(),
                        Total = x.TotalAmount
                    }).ToList();
                dataGridView1.DataSource = data;
                dataGridView1.Columns[0].HeaderText = "آرڈر نمبر";
                dataGridView1.Columns[1].HeaderText = "گاہک";
                dataGridView1.Columns[2].HeaderText = "خرید";
                dataGridView1.Columns[3].HeaderText = "کل رقم";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var db = new DataContext())
            {
                var data = db.OrderDetails.Include(c => c.Customer).Include(p => p.Product)
                    .Include(o => o.Order).Where(o => o.Posted == false).OrderBy(c => c.CustomerID).Select(x => new
                    {
                        CustomerID = x.CustomerID,
                        OrderNum = x.OrderNum,
                        Customer = x.Customer!.Name,
                        Product = x.Product!.Name.ToString() + "  " + x.Quantity.ToString() + " x " + x.Rate.ToString() +"=" +x.TotalAmount.ToString(),
                        Total = x.TotalAmount
                    }).ToList();

                

                var customers = db.OrderDetails.Include(o => o.Order).Where(o => o.Posted == false).OrderBy(c => c.CustomerID).Select(x => x.CustomerID).Distinct().ToList();
                foreach (var customer in customers)
                {
                    string purchasedProduct = "";
                    double totalAmount = 0;
                    var orders = data.Where(c => c.CustomerID == customer).ToList();
                    foreach(var order in orders)
                    {
                        purchasedProduct += order.Product+"~";
                        totalAmount += order.Total;
                    }
                    var glTrans = new Models.GLTran();
                    glTrans.Credit = totalAmount;
                    glTrans.TranAmount = totalAmount;
                    glTrans.TranDate = DateTime.Now;
                    glTrans.TranDateTimeStamp = DateTime.Now.ToString();
                    glTrans.TranDetail = purchasedProduct;
                    glTrans.TranType = "PUR";
                    glTrans.CustomerID = customer;
                    db.GLTrans.Add(glTrans);
                    db.SaveChanges();
                }
                foreach(var item in data)
                {
                    var order = db.OrderDetails.Where(u => u.OrderNum == item.OrderNum);
                    if(order.Count() > 0)
                    {
                        foreach(var o in order)
                        {
                            o.Posted = true;
                            db.SaveChanges();
                        }
                    }
                }
                LoadGrid();
            }
        }
    }
}
