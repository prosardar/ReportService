using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Model.Domain
{
    public class Department : BaseDomain
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
