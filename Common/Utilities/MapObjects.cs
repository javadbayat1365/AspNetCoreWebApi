using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class MapObjects
    {
        public static void MapObject(object source, object destination)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();
            PropertyInfo[] sourcePropertyInfos = sourceType.GetProperties();
            PropertyInfo[] destinationPropertyInfos = destinationType.GetProperties();
            foreach (var item in sourcePropertyInfos)
            {
                PropertyInfo property = destinationPropertyInfos
                                            .FirstOrDefault(w =>
                                                 w.Name == item.Name && w.PropertyType == item.PropertyType);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(destination, item.GetValue(source));
                }
            }
        }
    }
}
