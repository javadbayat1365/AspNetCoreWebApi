using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities;

public static class Assert
{
    public static void NotNull<T>(T obj,string name,string message=null) where T : class
    {
        if(obj is null)
            throw new ArgumentNullException($"{name} : {nameof(obj)}",message);
    }
}
