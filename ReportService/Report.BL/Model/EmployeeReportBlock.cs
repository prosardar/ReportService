namespace Report.BL
{
    public class EmployeeReportBlock
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Inn { get; set; }
        public int Salary { get; set; }
        public string BuhCode { get; set; }

        public virtual DepartmentReportBlock DepartmentBlock { get; set; }
    }
}