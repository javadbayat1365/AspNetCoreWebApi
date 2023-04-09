using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Employee
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int EmployeeGradeId { get; set; }
    }
}
