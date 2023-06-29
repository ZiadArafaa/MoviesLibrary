using AutoMapper;
using MoviesLibrary.Core.Dtos;
using MoviesLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}
