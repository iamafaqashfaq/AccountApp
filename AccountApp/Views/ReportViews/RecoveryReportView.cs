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
    public partial class RecoveryReportView : Form
    {
        DateTime _date;
        public RecoveryReportView(DateTime date)
        {
            InitializeComponent();
            this._date = date;
            Text = "Report viewer";
            WindowState = FormWindowState.Maximized;
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            Controls.Add(reportViewer1);
        }

        private void RecoveryReportView_Load(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var customers = db.GLTrans
                    .OrderBy(c => c.CustomerID).Select(x => x.CustomerID).Distinct().ToList();

                List<object> recoveryList = new List<object>();
                foreach (var customer in customers)
                {
                    var data = db.GLTrans.Include(x => x.Customer).Where(c => c.CustomerID == customer && c.TranType == "PUR" &&
                    c.TranDate.Date == _date.Date).Select(x => new
                    {
                        Purchase = x.TranDetail,
                        PurchaseAmount = x.Credit,
                    }).ToList();

                    var totalcredit = db.GLTrans.Where(c => c.CustomerID == customer &&
                    c.TranDate.Date < _date.Date).Sum(x => x.Credit);
                    var totalDebit = db.GLTrans.Where(c => c.CustomerID == customer &&
                    c.TranDate.Date < _date.Date).Sum(x => x.Debit);

                    var totalPending = totalcredit - totalDebit;
                    var customerdata = db.Customers.FirstOrDefault(c => c.Id == customer);
                    string purchaseItem = "";
                    double purchaseamount = 0;
                    foreach(var d in data)
                    {
                        purchaseItem += d.Purchase;
                        purchaseamount += d.PurchaseAmount;
                    }
                    if(customerdata != null)
                    {
                        var custObj = new
                        {
                            CustomerID = customerdata.Id,
                            CustomerName = customerdata.Name,
                            Purchase = purchaseItem,
                            PurchaseAmount = purchaseamount,
                            PreviousTotalPending = totalPending,
                            NewTotalPending = purchaseamount + totalPending,
                            Area = customerdata.Area,
                        };
                        recoveryList.Add(custObj);
                    }
                }

                var parameters = new[] { new ReportParameter("ReportDate", _date.ToString("dd-MM-yyyy")) };
                using var fs = new FileStream("../../../Views/ReportViews/SaleRecoveryReport.rdlc", FileMode.Open);
                reportViewer1.LocalReport.LoadReportDefinition(fs);
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", recoveryList));
                //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", recoveryData));
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
        }
    }
}
