using Report.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.BL
{
    public class Report
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; }

        public ICollection<DepartmentReportBlock> DepartmentBlocks { get; set; }

        public decimal Sum => DepartmentBlocks.Sum(d => d.Sum);

        public Report(int year, int month)
        {
            Year = year;
            Month = month;
            MonthName = MonthNameResolver.MonthName.GetName(year, month);
            DepartmentBlocks = new List<DepartmentReportBlock>();
        }

        public void AddDepartmentBlocks(DepartmentReportBlock block)
        {
            if (block == null)
            {
                return;
            }
            DepartmentBlocks.Add(block);
        }
    }
}
