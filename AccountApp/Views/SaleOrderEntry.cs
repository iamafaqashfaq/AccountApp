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
    public partial class SaleOrderEntry : Form
    {
        List<Models.Order> Orders = new List<Models.Order>();
        List<Models.Product> products = new List<Models.Product>();
        int currentIndex = 0;
        bool createClicked = false;
        public SaleOrderEntry()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !((TextBox)sender).Text.Contains('.'))
            {
                
                int id = Convert.ToInt32(((TextBox)sender).Text);
                var check = products.FirstOrDefault(x => x.Id == id);
                if (check != null)
                {
                    textBox3.Text = check.Name + " | " + check.Type;
                }
            }
            else
            {
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaleOrderEntry_Load(object sender, EventArgs e)
        {

            using (var db = new DataContext())
            {
                var saleBook = db.SaleBooks.ToList();
                comboBox1.DataSource = saleBook;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndex = 0;
                
                products = db.Products.ToList();
            }
            LoadOrder();
        }

        private void LoadOrder()
        {
            using(var db = new DataContext())
            {
                Orders = db.Orders.Where(c => c.OrderDate.Date == dateTimePicker1.Value.Date).ToList();
                if(Orders.Count > 0)
                {
                    currentIndex = Orders.Count - 1;
                    LoadControls();
                }
                else
                {
                    foreach (Control control in groupBox1.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    dataGridView1.DataSource = null;
                }
            }
        }

        private void LoadControls()
        {
            textBox1.Text = Orders[currentIndex].Id.ToString();
            if(Orders[currentIndex].ChalanNumber != null)
            {
                textBox4.Text = Orders[currentIndex].ChalanNumber!.ToString();
            }
            textBox2.Text = Orders[currentIndex].ProductCode.ToString();
            var check = products.FirstOrDefault(x => x.Id == Orders[currentIndex].ProductCode);
            if (check != null)
            {
                textBox3.Text = check.Name + " | " + check.Type;
            }
            comboBox1.SelectedValue = Orders[currentIndex].SaleBookId;
            textBox5.Text = Orders[currentIndex].Quantity.ToString();
            textBox6.Text = Orders[currentIndex].SoldQuantity.ToString();
            textBox7.Text = Orders[currentIndex].SaleRate.ToString();
            textBox8.Text = Orders[currentIndex].Sale.ToString();
            textBox9.Text = Orders[currentIndex].SaleWithoutCommission.ToString();
            using(var db = new DataContext())
            {
                var orderDtls = db.OrderDetails.Include(c => c.Customer).Include(x => x.Product).Where(x => x.OrderNum == Orders[currentIndex].Id).Select(x => new
                {
                    Id = x.Id,
                    Product = x.Product!.Name,
                    Customer = x.Customer!.Name,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Commision = x.Commission,
                    TotalAmount = x.TotalAmount,
                }).ToList();
                dataGridView1.DataSource = orderDtls;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "جنس";
                dataGridView1.Columns[2].HeaderText = "گاہک";
                dataGridView1.Columns[3].HeaderText = "تعداد";
                dataGridView1.Columns[4].HeaderText = "ریٹ";
                dataGridView1.Columns[5].HeaderText = "نگانہ";
                dataGridView1.Columns[6].HeaderText = "کل رقم";
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if(Orders.Count > 0)
            {
                if (currentIndex == Orders.Count - 1)
                {
                    return;
                }
                else
                {
                    currentIndex++;
                    LoadControls();
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0 && Orders.Count > 0)
            {
                currentIndex--;
                LoadControls();
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control control in groupBox1.Controls)
                if (control is TextBox)
                {
                    TextBox? textBox = (control as TextBox);
                    textBox!.Clear();
                }
            comboBox1.SelectedIndex = 0;
            dataGridView1.DataSource = null;
            textBox1.Text = "0";
            createClicked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox5.Text) && createClicked)
            {
                using(var db = new DataContext())
                {
                    Models.Order order = new Models.Order();
                    order.ChalanNumber = textBox4.Text;
                    order.ProductCode = Convert.ToInt32(textBox2.Text);
                    order.Quantity = Convert.ToInt32(textBox5.Text);
                    order.OrderDate = dateTimePicker1.Value;
                    order.SaleBookId = Convert.ToInt32(comboBox1.SelectedValue);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    LoadOrder();
                    createClicked = false;
                }
            }
            if(Orders.Count > 0)
            {
                if(Orders[currentIndex].Id.ToString() == textBox1.Text)
                using(var db = new DataContext())
                {
                    Orders[currentIndex].ChalanNumber = textBox4.Text;
                    Orders[currentIndex].ProductCode = Convert.ToInt32(textBox2.Text);
                    Orders[currentIndex].Quantity = Convert.ToInt32(textBox5.Text);
                    Orders[currentIndex].OrderDate = dateTimePicker1.Value;
                    Orders[currentIndex].SaleBookId = Convert.ToInt32(comboBox1.SelectedValue);
                        db.Update(Orders[currentIndex]);
                    db.SaveChanges();
                    LoadControls();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadOrder();
            createClicked = false;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                int id = Convert.ToInt32(((TextBox)sender).Text);
                var check = products.FirstOrDefault(x => x.Id == id);
                if (check == null)
                {
                    textBox2.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Please enter valid Product Code", "Error");

                }
                else
                {
                    textBox3.Text = check.Name + " | " + check.Type;
                    textBox5.Focus();
                }
            }
            else
            {
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                using (var db = new DataContext())
                {
                    int id = Convert.ToInt32(textBox1.Text);   
                    var order = db.Orders.Where(u => u.Id == id).FirstOrDefault();
                    if (order != null)
                    {
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        var orderDtls = db.OrderDetails.Where(u => u.OrderNum == id);
                        if(orderDtls.Count() > 0)
                        {
                            db.OrderDetails.RemoveRange(orderDtls);
                            db.SaveChanges();
                        }
                        LoadOrder();
                    }
                }
            }
            createClicked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                OrderDetailsEntry orderDetailsEntry = new OrderDetailsEntry(Orders[currentIndex]);
                orderDetailsEntry.ShowDialog();
                using(var db = new DataContext())
                {
                    var refreshedOrder = db.Orders.FirstOrDefault(c => c.Id == Orders[currentIndex].Id);
                    if(refreshedOrder != null)
                    {
                        Orders[currentIndex] = refreshedOrder;
                        LoadControls();
                    }
                }
            }   
        }
    }
}
