using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountApp.Views
{
    public partial class Customer : Form
    {
        List<Models.Area> areas = new List<Models.Area>();
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            using(var db = new DataContext())
            {
                var cash = db.Customers.FirstOrDefault(x => x.Id == 1 && x.Name == "نقدی");
                if (cash == null)
                {
                    Models.Customer c = new Models.Customer();
                    c.Name = "نقدی";
                    db.Customers.Add(c);
                    db.SaveChanges();
                }
                areas = db.Areas.ToList();
                AutoCompleteStringCollection myCol = new AutoCompleteStringCollection();
                foreach(var area in areas)
                {
                    myCol.Add(area.AreaName);
                }
                textBox5.AutoCompleteCustomSource = myCol;
            }
            LoadCustomerControls();
        }

        private void LoadCustomerControls()
        {
            using (var db = new DataContext())
            {
                var cust = db.Customers.Where(x => x.Name != "نقدی" && x.Id != 1).ToList();
                dataGridView2.DataSource = cust;
                ////dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[0].HeaderText = "گاہک کوڈ";
                dataGridView2.Columns[1].HeaderText = "گاہک";
                dataGridView2.Columns[2].HeaderText = "پتہ";
                dataGridView2.Columns[3].HeaderText = "شہر";
                dataGridView2.Columns[4].HeaderText = "فون";
                dataGridView2.Columns[5].HeaderText = "علاقہ";
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            custId = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            custId = 0;
            using (var db = new DataContext())
            {
                var cust = db.Customers.Where(u => u.Name == textBox1.Text).FirstOrDefault();
                if (cust != null)
                {
                    db.Customers.Remove(cust);
                    db.SaveChanges();

                    foreach (Control control in this.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    MessageBox.Show("Customer Deleted", "Success");
                    LoadCustomerControls();
                }
            }
        }

        int custId = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                var customerID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                custId = Convert.ToInt32(customerID);
                textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (customerID != "")
                {
                    using (var db = new DataContext())
                    {
                        int id = Convert.ToInt32(customerID);
                        var gl = db.GLTrans.Where(c => c.CustomerID == id).Select(x => new
                        {
                            Date = x.TranDateTimeStamp,
                            Detail = x.TranDetail.Replace('~',' '),
                            Debit = x.Debit,   
                            Credit = x.Credit,
                        }).ToList();
                        dataGridView1.DataSource = gl;
                        dataGridView1.Columns[0].HeaderText = "تاریخ";
                        dataGridView1.Columns[1].HeaderText = "تفصیل";
                        dataGridView1.Columns[2].HeaderText = "جمع";
                        dataGridView1.Columns[3].HeaderText = "بنام";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext()) 
            { 
                if(custId != 0)
                {
                    var cust = db.Customers.FirstOrDefault(c => c.Id == custId);
                    if(cust != null)
                    {
                        cust.Name = textBox1.Text;
                        cust.Address = textBox2.Text;
                        cust.City = textBox3.Text;
                        cust.Area = textBox5.Text;
                        cust.Phone = textBox4.Text;
                        db.Customers.Update(cust);
                        db.SaveChanges();
                        LoadCustomerControls();
                        int id = Convert.ToInt32(cust.Id);
                        var gl = db.GLTrans.Where(c => c.CustomerID == id).Select(x => new
                        {
                            Date = x.TranDateTimeStamp,
                            Detail = x.TranDetail.Replace('~', ' '),
                            Debit = x.Debit,
                            Credit = x.Credit,
                        }).ToList();
                        dataGridView1.DataSource = gl;
                        dataGridView1.Columns[0].HeaderText = "تاریخ";
                        dataGridView1.Columns[1].HeaderText = "تفصیل";
                        dataGridView1.Columns[2].HeaderText = "جمع";
                        dataGridView1.Columns[3].HeaderText = "بنام";
                        custId = 0;
                    }
                }
                else
                {
                    Models.Customer cust = new Models.Customer();
                    cust.Name = textBox1.Text;
                    cust.Address = textBox2.Text;
                    cust.City = textBox3.Text;
                    cust.Area = textBox5.Text;
                    cust.Phone = textBox4.Text;
                    db.Customers.Add(cust);
                    db.SaveChanges();
                    LoadCustomerControls();
                    int id = Convert.ToInt32(cust.Id);
                    var gl = db.GLTrans.Where(c => c.CustomerID == id).Select(x => new
                    {
                        Date = x.TranDateTimeStamp,
                        Detail = x.TranDetail.Replace('~', ' '),
                        Debit = x.Debit,
                        Credit = x.Credit,
                    }).ToList();
                    dataGridView1.DataSource = gl;
                    dataGridView1.Columns[0].HeaderText = "تاریخ";
                    dataGridView1.Columns[1].HeaderText = "تفصیل";
                    dataGridView1.Columns[2].HeaderText = "جمع";
                    dataGridView1.Columns[3].HeaderText = "بنام";
                }
                
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            var check = areas.FirstOrDefault(a => a.AreaName.ToLower() == textBox5.Text.ToLower());
            if(check == null)
            {
                MessageBox.Show("This area is not defined in the system. Please go to Area Maintenance and add this area to update this customers", "Error");
            }
            else
            {
                textBox5.Text = check.AreaName; 
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
