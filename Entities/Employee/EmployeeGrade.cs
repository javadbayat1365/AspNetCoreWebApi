using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Employee
{
    public class EmployeeGrade:IEntity
    {
        public long Id { get; set; }
        public int Grade { get; set; }

    }
}
