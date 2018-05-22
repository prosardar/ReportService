using Moq;
using Report.BL;
using Report.Data;
using Report.Model.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Report.Tests
{
    public class ReportServiceTest
    {
        [Fact]
        public void GenerateStream()
        {
            var depList = new List<Department>();
            for (int i = 0; i < 2; i++)
            {
                depList.Add(new Department()
                {
                    Name = $"Отдел {i}",
                    Active = true,
                    Id = i,
                });
            }

            var empList = new List<Employee>();
            for (int i = 0; i < 4; i++)
            {
                empList.Add(new Employee()
                {
                    Name = "Шри Вих Ланча",
                    Id = i,
                    Inn = "xxx",
                    BuhCode = "xxx",
                    DepartmentId = i % 2,
                    Salary = i % 2 == 0 ? 1000 : 500
                });
            }

            var context = new Mock<PgContext>();

            context.Setup(c => c.ExecuteQueryAsync<Department>(It.IsAny<string>()))
                    .Returns(Task.FromResult<IEnumerable<Department>>(depList));

            context.Setup(c => c.ExecuteQueryAsync<Employee>(It.IsAny<string>()))
                    .Returns(Task.FromResult<IEnumerable<Employee>>(empList));

            var empSalaryService = new Mock<IEmpSalaryService>();
            empSalaryService.Setup(empSer => empSer.Salary(It.IsAny<EmployeeReportBlock>()))
                .Returns(2000);

            var empCodeResolve = new Mock<IEmpCodeResolver>();
            empCodeResolve.Setup(empSer => empSer.GetCode(It.IsAny<string>()))
                .Returns(Task.FromResult("xxxx"));

            var service = new SalaryReportService(context.Object)
            {
                EmpSalaryService = empSalaryService.Object,
                EmpCodeResolver = empCodeResolve.Object
            };

            (Stream stream, string ext) result = service.GenerateToStreamAsync(2018, 1).Result;

            Assert.NotNull(result.stream);
            string content = "";
            using (StreamReader streamReader = new StreamReader(result.stream, Encoding.UTF8))
            {
                content = streamReader.ReadToEnd();
            }
            Assert.True(content.Contains($"Всего по предприятию 16000р"));
        }

        [Fact]
        public void CheckTextReportExporter()
        {
            BL.Report report = CreateTestReport();

            IReportExporter exporter = new TextReportExporter();
            exporter.SetReport(report);
            Stream stream = exporter.ExportAsync().Result;
            string content = "";
            using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                content = streamReader.ReadToEnd();
            }

            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.True(content.Contains("Всего по предприятию 24000р"));

            Debug.WriteLine("OK");
        }

        private static BL.Report CreateTestReport()
        {
            BL.Report report = new BL.Report(2018, 5);
            for (int i = 0; i < 3; i++)
            {
                var block = new DepartmentReportBlock("Какой-то отдел");
                for (int j = 0; j < 5; j++)
                {
                    block.AddEmployee(new EmployeeReportBlock()
                    {
                        Name = "Иванов Айдар Кирилович",
                        Inn = "12121xxxx",
                        BuhCode = "123xxxx",
                        Salary = j % 2 == 0 ? 2000 : 1000
                    });
                }
                report.AddDepartmentBlocks(block);
            }
            return report;
        }
    }
}
