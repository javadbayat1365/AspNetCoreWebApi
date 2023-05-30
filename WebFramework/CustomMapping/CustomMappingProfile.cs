using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.CustomMapping
{
    internal class CustomMappingProfile:Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomeMapping> haveCustomeMappings)
        {
            foreach (var item in haveCustomeMappings)
            {
                item.CreateMapping(this);
            }
        }
    }
}
