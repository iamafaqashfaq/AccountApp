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
    public partial class Area : Form
    {
        public Area()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var area = db.Areas.Where(u => u.AreaName == textBox1.Text).FirstOrDefault();
                if (area != null)
                {
                    db.Areas.Remove(area);
                    db.SaveChanges();

                    foreach (Control control in this.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    MessageBox.Show("Area Deleted", "Success");
                    LoadGridView();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var area = new AccountApp.Models.Area();
                area.AreaName = textBox1.Text;
                db.Areas.Add(area);
                db.SaveChanges();
                LoadGridView();
            }
        }

        private void Area_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void LoadGridView()
        {
            using (var db = new DataContext())
            {
                var area = db.Areas.ToList();
                dataGridView1.DataSource = area;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Area Name";
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
            string? areaName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = areaName;
        }
    }
}
