using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.BL;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private ISalaryReport _salaryReport;

        public ReportController(ISalaryReport report)
        {
            _salaryReport = report;
        }

        [HttpGet]
        [Route("{year}/{month}/{formatType}")]
        public async Task<IActionResult> Download(int year, int month, string formatType = "TXT")
        {
            Enum.TryParse(formatType, out ExportFormat format);
            (Stream stream, string ext) result = await _salaryReport.GenerateToStreamAsync(year, month, format);
            if (result.stream == null)
            {
                // TODO: Необходимо вернуть что-то дружелюбное
                return StatusCode(500);
            }
            return File(result.stream, "application/octet-stream", $"report_{DateTime.Now.ToShortTimeString()}.{result.ext}");
        }
    }
}
