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
    public partial class OrderDetailsEntry : Form
    {
        List<Models.Customer> Customers = new List<Models.Customer>();
        public Models.Order order;
        int quantity;
        public OrderDetailsEntry(Models.Order order)
        {
            this.order = order;
            InitializeComponent();
        }

        private void OrderDetailsEntry_Load(object sender, EventArgs e)
        {
            label10.Text = "Order Date: "+order.OrderDate.ToString("dd-MM-yyyy");
            LoadGrid();
            using (var db = new DataContext())
            {
                Customers = db.Customers.ToList();
                AutoCompleteStringCollection custCollection = new AutoCompleteStringCollection();
                foreach (var customer in Customers)
                {
                    custCollection.Add(customer.Name + " | " + customer.Area);
                }
                textBox2.AutoCompleteCustomSource = custCollection;
            }
        }

        private void LoadGrid()
        {
            using(var db = new DataContext())
            {
                var orderDetails = db.OrderDetails.Include(c => c.Customer).Where(u => u.OrderNum == order.Id).Select(x => new
                {
                    Id = x.Id,
                    CustomerCode = x.Customer!.Id,
                    Customer = x.Customer.Name + " - " + x.Customer.Area,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Commission = x.Commission,
                    TotalAmount = x.TotalAmount,
                }).ToList();
                dataGridView1.DataSource = orderDetails;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "گاہک کوڈ";
                dataGridView1.Columns[2].HeaderText = "گاہک";
                dataGridView1.Columns[3].HeaderText = "تعداد";
                dataGridView1.Columns[4].HeaderText = "ریٹ";
                dataGridView1.Columns[5].HeaderText = "نگانہ";
                dataGridView1.Columns[6].HeaderText = "کل رقم";
            }
            LoadControls();
        }
        private void LoadControls()
        {
            textBox7.Text = order.Id.ToString();
            using (var db = new DataContext())
            {
                var product = db.Products.Where(u => u.Id == order.ProductCode).FirstOrDefault();
                if(product != null)
                {
                    textBox8.Text = product.Name.ToString() + " | " + product.Type.ToString();
                }
            }
            quantity = order.Quantity;
            double totalOrderAmount = 0;
            double totalOrderCom = 0;
            double totalOrderSale = 0;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if(quantity > 0)
                {
                    quantity -= Convert.ToInt32(row.Cells[3].Value.ToString());
                }
                totalOrderAmount += Convert.ToDouble(row.Cells[6].Value.ToString());
                totalOrderCom += Convert.ToDouble(row.Cells[5].Value.ToString()) * Convert.ToDouble(row.Cells[3].Value.ToString());
                totalOrderSale += Convert.ToDouble(row.Cells[4].Value.ToString()) * Convert.ToDouble(row.Cells[3].Value.ToString());
            }
            label12.Text = totalOrderAmount.ToString();
            label14.Text = totalOrderCom.ToString();
            label15.Text = totalOrderSale.ToString();
            textBox9.Text = quantity.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !((TextBox)sender).Text.Contains('.'))
            {
                int id = Convert.ToInt32(((TextBox)sender).Text);
                var check = Customers.FirstOrDefault(x => x.Id == id);
                if (check != null)
                {
                    textBox2.Text = check.Name + " | " + check.Area;
                }
            }
            else
            {
                textBox2.Clear();
                textBox1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                var check = Customers.Where(c => c.Name.ToLower() + " | " + c.Area.ToLower() == textBox2.Text.ToLower()).FirstOrDefault();
                if (check != null)
                {
                    textBox1.Text = check.Id.ToString();
                    textBox2.Text = check.Name + " | " + check.Area;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                int id = Convert.ToInt32(((TextBox)sender).Text);
                var check = Customers.FirstOrDefault(x => x.Id == id);
                if (check == null)
                {
                    textBox2.Clear();
                    textBox1.Clear();
                    MessageBox.Show("Please enter valid Customer Code", "Error");

                }
                else
                {
                    textBox2.Text = check.Name + " | " + check.Area;
                    textBox3.Focus();
                }
            }
            else
            {
                textBox2.Clear();
                textBox1.Clear();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                int quantity = Convert.ToInt32(textBox3.Text);
                int stockQuantity = Convert.ToInt32(textBox9.Text);
                if(quantity < 1 || quantity > stockQuantity)
                {
                    MessageBox.Show("Please enter correct quantity", "Error");
                    textBox3.Text = "";
                    textBox3.Focus();
                }
            }
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                using (var db = new DataContext())
                {
                    double rate = Convert.ToDouble(textBox4.Text);
                    double commission = Convert.ToDouble(textBox5.Text);
                    double quanitity = Convert.ToDouble(textBox3.Text);
                    double totalCommission = quanitity * commission;
                    double total = rate * quanitity;
                    total = total + totalCommission;
                    Models.OrderDetails orderDetails = new Models.OrderDetails();
                    orderDetails.OrderDetailDate = order.OrderDate;
                    orderDetails.OrderNum = order.Id;
                    orderDetails.CustomerID = Convert.ToInt32(textBox1.Text);
                    orderDetails.ProductID = order.ProductCode;
                    orderDetails.Quantity = Convert.ToInt32(textBox3.Text);
                    orderDetails.Rate = Convert.ToDouble(textBox4.Text);
                    orderDetails.Commission = Convert.ToDouble(textBox5.Text);
                    orderDetails.TotalAmount = total;
                    db.OrderDetails.Add(orderDetails);
                    db.SaveChanges();
                    LoadGrid();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
                }
            }
            textBox1.Focus();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                double rate = Convert.ToDouble(textBox4.Text);
                double commission = Convert.ToDouble(textBox5.Text);
                double quanitity = Convert.ToDouble(textBox3.Text);
                double totalCommission = quanitity * commission;
                double total = rate * quanitity;
                total = total + totalCommission;
                textBox6.Text = total.ToString();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow rows in dataGridView1.SelectedRows)
                        {
                            using (var db = new DataContext())
                            {
                                int id = Convert.ToInt32(rows.Cells[0].Value.ToString());
                                var checkTran = db.OrderDetails.FirstOrDefault(c => c.Id == id);
                                if (checkTran != null)
                                {
                                    db.OrderDetails.Remove(checkTran);
                                    db.SaveChanges();
                                }
                            }
                        }

                        LoadGrid();
                    }
                }
            }
        }

        private void OrderDetailsEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            int soldQuantity = quantity - order.Quantity;
            soldQuantity = Math.Abs(soldQuantity);
            double saleWithoutCommission = Convert.ToDouble(label15.Text);
            double sale = Convert.ToDouble(label12.Text);
            double totalOrderSale = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (quantity > 0)
                {
                    quantity -= Convert.ToInt32(row.Cells[3].Value.ToString());
                }
                totalOrderSale += Convert.ToDouble(row.Cells[4].Value.ToString());
            }
            double saleRate = totalOrderSale / soldQuantity;
            using(var db = new DataContext())
            {
                order.SoldQuantity = soldQuantity;
                order.SaleRate = saleRate;
                order.SaleWithoutCommission = saleWithoutCommission;
                order.Sale = sale;
                db.Update(order);
                db.SaveChanges();
            }
            //MessageBox.Show(saleRate.ToString());
        }
    }
}
