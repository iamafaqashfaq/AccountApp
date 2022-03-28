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
    public partial class OpeningGL : Form
    {
        List<Models.Customer> Customers = new List<Models.Customer>();
        public OpeningGL()
        {
            InitializeComponent();
        }

        private void OpeningGL_Load(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                Customers = db.Customers.ToList();
                AutoCompleteStringCollection custCollection = new AutoCompleteStringCollection();
                foreach (var customer in Customers)
                {
                    custCollection.Add(customer.Name + " | " + customer.Area);
                }
            }
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            using (var db = new DataContext())
            {
                var gl = db.GLTrans.Include(c => c.Customer).Where(c => c.TranType == "BB").Select(x => new {
                    TranID = x.Id,
                    CustomerCode = x.CustomerID,
                    Customer = x.Customer!.Name,
                    Credit = x.Credit,
                }).ToList();

                double total = 0;
                foreach (var t in gl)
                {
                    total += t.Credit;
                }
                label4.Text = total.ToString();
                dataGridView1.DataSource = gl.ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].HeaderText = "سیریل نمبر";
                dataGridView1.Columns[1].HeaderText = "گاہک کوڈ";
                dataGridView1.Columns[2].HeaderText = "گاہک";
                dataGridView1.Columns[3].HeaderText = "بنام";
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox3.Text))
            {
                using (var db = new DataContext())
                {
                    DateTime date = DateTime.Now.AddDays(-1);
                    var gl = new Models.GLTran();
                    gl.TranType = "BB";
                    gl.Debit = 0;
                    gl.Credit = Convert.ToDouble(textBox3.Text);
                    gl.TranAmount = Convert.ToDouble(textBox3.Text);
                    gl.TranDetail = "Beginning Balance";
                    gl.TranDate = date;
                    gl.TranDateTimeStamp = date.ToString();
                    gl.CustomerID = Convert.ToInt32(textBox1.Text);
                    db.GLTrans.Add(gl);
                    db.SaveChanges();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    LoadDataGrid();
                }
            }
            textBox1.Focus();
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
                                var checkTran = db.GLTrans.FirstOrDefault(c => c.Id == id);
                                if (checkTran != null)
                                {
                                    db.GLTrans.Remove(checkTran);
                                    db.SaveChanges();
                                }
                            }
                        }

                        LoadDataGrid();
                    }
                }
            }
        }
    }
}
