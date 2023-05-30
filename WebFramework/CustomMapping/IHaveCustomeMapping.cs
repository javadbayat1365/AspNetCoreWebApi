using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.CustomMapping
{
    public interface IHaveCustomeMapping
    {
        void CreateMapping(Profile profile);
    }
}
