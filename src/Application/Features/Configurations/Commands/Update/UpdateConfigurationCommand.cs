using Application.Features.Configurations.Commands.Create;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Commands.Update;

public class UpdateConfigurationCommand:IRequest<UpdatedConfigurationResponse>
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required ConfigurationValueType Type { get; set; }
    public required string Value { get; set; }
    public required bool IsActive { get; set; }
    public required string ApplicationName { get; set; }
}

public class UpdateConfigurationCommandHandler :IRequestHandler<UpdateConfigurationCommand, UpdatedConfigurationResponse>
{
    private readonly IMapper _mapper;
    private readonly IConfigurationRepository _configurationRepository;

    public UpdateConfigurationCommandHandler(IMapper mapper, IConfigurationRepository configurationRepository)
    {
        _mapper = mapper;
        _configurationRepository = configurationRepository;
    }

    public async Task<UpdatedConfigurationResponse> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
    {
        ConfigurationItem item = _mapper.Map<ConfigurationItem>(request);
        item.LastUpdated = DateTime.UtcNow;

        bool success = await _configurationRepository.UpdateConfigurationAsync(item);

        UpdatedConfigurationResponse response = new() { Success = success };
        return response;
    
    }
}
