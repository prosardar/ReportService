using System.IO;
using System.Threading.Tasks;

namespace Report.BL
{
    public class ExcellReportExporter : ReportExporterBase
    {
        public override string Extension => "xlsx";

        public override async Task<Stream> ExportAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}