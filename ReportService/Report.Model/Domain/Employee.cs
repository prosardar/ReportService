using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Model.Domain
{
    public class Employee : BaseDomain
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string  Inn { get; set; }
        public int Salary { get; set; }
        public string BuhCode { get; set; }

        public virtual Department Department { get; set; }
    }
}
