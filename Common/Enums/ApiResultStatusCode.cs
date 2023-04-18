using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name ="Success Operator")]
        IsSuccess = 0,
        [Display(Name ="Bad Request Parameters")]
        BadRequest = 1,
        [Display(Name ="Server Error")]
        ServerError = 2,
        [Display(Name ="Not Found Error")]
        NotFound = 3,
    }
}
