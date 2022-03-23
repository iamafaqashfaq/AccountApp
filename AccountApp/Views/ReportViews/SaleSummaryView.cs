using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
    public partial class SaleSummaryView : Form
    {
        DateTime _date;
        public SaleSummaryView(DateTime date)
        {
            InitializeComponent();
            this._date = date;
            Text = "Report viewer";
            WindowState = FormWindowState.Maximized;
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            Controls.Add(reportViewer1);
        }

        private void SaleSummaryView_Load(object sender, EventArgs e)
        {
            using(var db = new DataContext())
            {
                var data = db.Orders.Include(p => p.Product).Where(c => c.OrderDate.Date == _date.Date).Select(x => new
                {
                    OrderNum = x.Id,
                    Chalan = x.ChalanNumber,
                    OrderDate = x.OrderDate.Date,
                    Product = x.Product!.Name,
                    QuantitySold = x.SoldQuantity,
                    Sale = x.Sale,
                    SaleRate = x.SaleRate,
                    SaleWOCommission = x.SaleWithoutCommission,
                    TotalQuantity = x.Quantity
                }).ToList();
                var ARData = db.GLTrans.Where(c => c.TranDate.Date == _date.Date && c.TranType == "AR").Sum(x=>x.Debit);
                var totalRecovery = new
                {
                    TotalRecovery = ARData,
                };

                List<object> recoveryData = new List<object>();
                recoveryData.Add(totalRecovery);
                var parameters = new[] { new ReportParameter("Title", "Sale Summary") };
                using var fs = new FileStream("../../../Views/ReportViews/SaleSummaryReport.rdlc", FileMode.Open);
                reportViewer1.LocalReport.LoadReportDefinition(fs);
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", recoveryData));
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            
        }
    }
}
