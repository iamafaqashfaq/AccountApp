using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountApp.Views.ReportViews
{
    public partial class CustomerHistory : Form
    {
        int _Id; 
        public CustomerHistory(int Id)
        {
            InitializeComponent();
            Text = "Report viewer";
            _Id = Id;
            WindowState = FormWindowState.Maximized;
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            Controls.Add(reportViewer1);
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var gl = db.GLTrans.Include(x => x.Customer).Where(c => c.CustomerID == _Id).Select(x => new
                {
                    Id = x.CustomerID,
                    Name = x.Customer.Name,
                    TranDate = x.TranDate.ToString("dd-MM-yyyy"),
                    TransactionType = x.TranDetail.Replace('~', ' '),
                    Debit = x.Debit,
                    Credit = x.Credit,
                }).ToList();

                var parameters = new[] { new ReportParameter("ReportDate", DateTime.Now.ToString("dd-MM-yyyy")) };
                using var fs = new FileStream("../../../Views/ReportViews/CustomerHistory.rdlc", FileMode.Open);
                reportViewer1.LocalReport.LoadReportDefinition(fs);
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", gl));
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
        }
    }
}
