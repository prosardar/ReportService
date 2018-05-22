using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Report.BL
{
    public interface ISalaryReport
    {
        Dictionary<ExportFormat, IReportExporter> SupportedExporters { get; }

        Task<(Stream, string)> GenerateToStreamAsync(int year, int month, ExportFormat format);
    }
}