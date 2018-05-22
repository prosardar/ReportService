using System.IO;
using System.Threading.Tasks;

namespace Report.BL
{
    public interface IReportExporter
    {
        string Extension { get; }

        void SetReport(Report report);

        Task<Stream> ExportAsync();
    }
}