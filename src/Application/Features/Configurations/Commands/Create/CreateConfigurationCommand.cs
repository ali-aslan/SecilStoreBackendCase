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

namespace Application.Features.Configurations.Commands.Create;

public class CreateConfigurationCommand : IRequest<CreatedConfigurationResponse>
{
    public  string Id { get; set; }
    public required string Name { get; set; }
    public required ConfigurationValueType Type { get; set; }
    public required string Value { get; set; }
    public required string ApplicationName { get; set; }
}

public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, CreatedConfigurationResponse>
{
    private readonly IMapper _mapper;
    private readonly IConfigurationRepository _configurationRepository;

    public CreateConfigurationCommandHandler(IMapper mapper, IConfigurationRepository configurationRepository)
    {
        _mapper = mapper;
        _configurationRepository = configurationRepository;
    }

    public async Task<CreatedConfigurationResponse> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
    {
        ConfigurationItem item = _mapper.Map<ConfigurationItem>(request);

        item.Id = ObjectId.GenerateNewId().ToString();
        item.LastUpdated = DateTime.UtcNow;

        await _configurationRepository.CreateConfigurationAsync(item);

        CreatedConfigurationResponse response = _mapper.Map<CreatedConfigurationResponse>(item);
        return response;
    }
}


