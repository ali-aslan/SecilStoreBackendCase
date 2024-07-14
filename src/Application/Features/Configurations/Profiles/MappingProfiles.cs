using Application.Features.Configurations.Commands.Create;
using Application.Features.Configurations.Commands.Delete;
using Application.Features.Configurations.Commands.Update;
using Application.Features.Configurations.Queries.GetAll;
using Application.Features.Configurations.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        //Create
        CreateMap<CreateConfigurationCommand, ConfigurationItem>().ReverseMap();
        CreateMap<CreatedConfigurationResponse, ConfigurationItem>().ReverseMap();

        //GetById
        CreateMap<GetByIdConfigurationResponse, ConfigurationItem>().ReverseMap();

        //GetAll
        CreateMap<GetAllConfigurationResponse, ConfigurationItem>().ReverseMap();

        //Delete


        //Update
        CreateMap<UpdateConfigurationCommand, ConfigurationItem>().ReverseMap();

    }
}
