using System.IO;
using System.Threading.Tasks;

namespace Report.BL
{
    public abstract class ReportExporterBase : IReportExporter
    {
        protected Report Report;

        public abstract string Extension { get; }

        public void SetReport(Report report)
        {
            Report = report;
        }

        public abstract Task<Stream> ExportAsync();
    }
}