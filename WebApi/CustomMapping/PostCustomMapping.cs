using AutoMapper;
using Entities;
using WebApi.Models;
using WebFramework.CustomMapping;

namespace WebApi.CustomMapping
{
    public class PostCustomMapping : IHaveCustomeMapping
    {
        public void CreateMapping(Profile profile)
        {
            profile.CreateMap<Post,PostDto>()
                //.ForMember(f => f.Title, op => op.Ignore())//Destination Clss => PostDto
                .ReverseMap()
                .ForMember(f =>f.Author,op => op.Ignore()) // Destination Class => Post (After Reverse Method)
                .ForMember(f =>f.Category,op => op.Ignore())
                //.ForSourceMember(f => f.Title , op=> op.DoNotValidate())
                ;
            profile.add
        }
    }
}
