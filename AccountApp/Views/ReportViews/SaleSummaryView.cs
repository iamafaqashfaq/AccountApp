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
            //var items = new[] { new ReportItem { Description = "Widget 6000", Price = 104.99m, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = 1.41m, Qty = 25 } };
            var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };
            using var fs = new FileStream("../../../Views/ReportViews/SaleSummaryReport.rdlc", FileMode.Open);
            reportViewer1.LocalReport.LoadReportDefinition(fs);
            //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Items", items));
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }
    }

    //public class ReportItem
    //{
    //    public string Description { get; set; } = String.Empty;
    //    public decimal Price { get; set; }
    //    public int Qty { get; set; }
    //    public decimal Total => Price * Qty;
    //}
}
