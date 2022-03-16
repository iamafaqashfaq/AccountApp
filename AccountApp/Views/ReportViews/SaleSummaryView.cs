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
        public SaleSummaryView()
        {
            InitializeComponent();
            Text = "Report viewer";
            WindowState = FormWindowState.Maximized;
            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            Controls.Add(reportViewer1);
        }

        private void SaleSummaryView_Load(object sender, EventArgs e)
        {
            Report.Load(reportViewer1.LocalReport);
            reportViewer1.RefreshReport();
        }
    }

    class Report
    {
        public static void Load(LocalReport report)
        {
            var items = new[] { new ReportItem { Description = "Widget 6000", Price = 104.99m, Qty = 1 }, new ReportItem { Description = "Gizmo MAX", Price = 1.41m, Qty = 25 } };
            var parameters = new[] { new ReportParameter("Title", "Invoice 4/2020") };
            using var fs = new FileStream("../../../Views/ReportViews/Report.rdlc", FileMode.Open);
            report.LoadReportDefinition(fs);
            report.DataSources.Add(new ReportDataSource("Items", items));
            report.SetParameters(parameters);
        }
    }

    public class ReportItem
    {
        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Total => Price * Qty;
    }
}
