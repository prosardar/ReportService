using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Report.BL
{
    public class TextReportExporter : ReportExporterBase
    {
        private StringBuilder _sb = new StringBuilder();

        public override string Extension => "txt";

        public override async Task<Stream> ExportAsync()
        {
            _sb.AppendLine(Report.MonthName + " " + Report.Year);
            ExportBlocks();
            _sb.AppendLine("");
            _sb.AppendLine("==================================");
            _sb.AppendLine($"Всего по предприятию {Report.Sum}р");

            string fileName = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            File.WriteAllText(fileName, _sb.ToString());
            return new FileStream(fileName, FileMode.Open);
        }

        private void ExportBlocks()
        {            
            Report.DepartmentBlocks.All(d =>
            {
                ExportBlock(d);
                return true;
            });
        }

        private void ExportBlock(DepartmentReportBlock block)
        {
            _sb.AppendLine("");
            _sb.AppendLine("==================================");
            _sb.AppendLine(block.Name);
            block.Employees.All(e =>
            {
                ExportBlock(e);
                return true;
            });
            _sb.AppendLine($"Всего по отделу {block.Sum}р");
        }

        private void ExportBlock(EmployeeReportBlock block)
        {
            _sb.AppendLine($"{block.Name} {block.Salary}р");
        }
    }
}