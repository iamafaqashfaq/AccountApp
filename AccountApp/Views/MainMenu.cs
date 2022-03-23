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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserMaintenance us = new UserMaintenance();
            us.Show();
        }

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Area area = new Area();
            area.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Views.Customer c = new Customer();
            c.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ARJournal journal = new ARJournal();
            journal.Show();
        }

        private void openingBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningGL openingGL = new OpeningGL();
            openingGL.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaleOrderEntry saleOrderEntry = new SaleOrderEntry();   
            saleOrderEntry.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductEntry productEntry = new ProductEntry(); 
            productEntry.Show();
        }

        private void salePostingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostSale ps = new PostSale();
            ps.ShowDialog();    
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saleSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleSummaryDateSelect saleSummaryDateSelect = new SaleSummaryDateSelect();
            saleSummaryDateSelect.ShowDialog();
        }
    }
}
