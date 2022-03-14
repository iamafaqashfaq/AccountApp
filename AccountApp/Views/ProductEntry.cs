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
    public partial class ProductEntry : Form
    {
        public ProductEntry()
        {
            InitializeComponent();
        }

        private void ProductEntry_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
        private void LoadGridView()
        {
            using (var db = new DataContext())
            {
                var product = db.Products.ToList();
                dataGridView1.DataSource = product;
                dataGridView1.Columns[0].HeaderText = "Product Code";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
                if (control is TextBox)
                {
                    TextBox? textBox = (control as TextBox);
                    textBox!.Clear();
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var product = new AccountApp.Models.Product();
                product.Name = textBox1.Text;
                product.Type = textBox2.Text;
                db.Products.Add(product);
                db.SaveChanges();
                LoadGridView();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var product = db.Products.Where(u => u.Name == textBox1.Text).FirstOrDefault();
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();

                    foreach (Control control in this.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    MessageBox.Show("Product Deleted", "Success");
                    LoadGridView();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string? productName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string? typeName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox1.Text = productName;
            textBox2.Text = typeName;
        }
    }
}
