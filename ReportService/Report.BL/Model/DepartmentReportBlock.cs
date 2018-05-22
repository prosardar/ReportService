using Report.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Report.BL
{
    public class DepartmentReportBlock
    {
        public string Name { get; set; }

        public ICollection<EmployeeReportBlock> Employees { get; set; }

        public decimal Sum => Employees.Sum(e => e.Salary);

        public DepartmentReportBlock()
        {
            Employees = new List<EmployeeReportBlock>();
        }

        public DepartmentReportBlock(string name) : this()
        {
            Name = name;
        }

        public void AddEmployee(EmployeeReportBlock emp)
        {
            if (emp == null)
            {
                return;
            }
            Employees.Add(emp);
        }
    }
}