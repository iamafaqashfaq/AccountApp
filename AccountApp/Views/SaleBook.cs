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
    public partial class SaleBook : Form
    {
        public SaleBook()
        {
            InitializeComponent();
        }
        private void LoadGridView()
        {
            using (var db = new DataContext())
            {
                var SaleBooks = db.SaleBooks.ToList();
                dataGridView1.DataSource = SaleBooks;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "بک نام";

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var SaleBooks = new AccountApp.Models.SaleBook();
                SaleBooks.Name = textBox1.Text;
                db.SaleBooks.Add(SaleBooks);
                db.SaveChanges();
                LoadGridView();
            }
        }

        private void SaleBook_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var SaleBooks = db.SaleBooks.Where(u => u.Name == textBox1.Text).FirstOrDefault();
                if (SaleBooks != null)
                {
                    db.SaleBooks.Remove(SaleBooks);
                    db.SaveChanges();

                    foreach (Control control in this.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    MessageBox.Show("SaleBook Deleted", "Success");
                    LoadGridView();
                }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string? Name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = Name;
        }
    }
}
