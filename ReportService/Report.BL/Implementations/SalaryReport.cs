using System;
using System.Collections.Generic;
using System.Text;
using Report.Model.Domain;
using System.IO;
using System.Threading.Tasks;
using Report.Data;

namespace Report.BL
{
    public class SalaryReportService : ISalaryReport
    {
        private IDbContext _dbContext;

        private EmployeeRepository _employeeRepository;
        private DepartmentRepository _departmentRepository;

        public IEmpCodeResolver EmpCodeResolver { get; set; }
        public IEmpSalaryService EmpSalaryService { get; set; }

        private Dictionary<ExportFormat, IReportExporter> _supportedExporters;

        public Dictionary<ExportFormat, IReportExporter> SupportedExporters
        {
            get
            {
                if (_supportedExporters == null)
                {
                    _supportedExporters = new Dictionary<ExportFormat, IReportExporter>
                    {
                        { ExportFormat.TXT, new TextReportExporter() }
                    };
                }
                return _supportedExporters;
            }
        }

        public SalaryReportService(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeRepository = new EmployeeRepository(_dbContext);
            _departmentRepository = new DepartmentRepository(_dbContext);
        }

        public async Task<(Stream, string)> GenerateToStreamAsync(int year, int month, ExportFormat format = ExportFormat.TXT)
        {
            Report report = await CreateReport(year, month);
            if (SupportedExporters.ContainsKey(format) == false)
            {
                // TODO: Необходимо вернуть что-то дружелюбное
                return (null, null);
            }
            IReportExporter exporter = SupportedExporters[format];
            exporter.SetReport(report);
            return (await exporter.ExportAsync(), exporter.Extension);
        }

        private async Task<Report> CreateReport(int year, int month)
        {
            Report report = new Report(year, month);
            IEnumerable<Department> activeDeps = await _departmentRepository.GetActiveAsync();
            foreach (var dep in activeDeps)
            {
                var depblock = new DepartmentReportBlock(dep.Name);
                IEnumerable<Employee> emps = await _employeeRepository.FindAllByDepartmentAsync(dep.Id);
                foreach (var emp in emps)
                {
                    var empBlock = new EmployeeReportBlock()
                    {
                        Name = emp.Name,                     
                        Inn = emp.Inn
                    };
                    empBlock.BuhCode = await EmpCodeResolver.GetCode(emp.Inn);
                    empBlock.Salary = EmpSalaryService.Salary(empBlock);
                    depblock.AddEmployee(empBlock);
                }
                report.AddDepartmentBlocks(depblock);
            }
            return report;
        }
    }
}
