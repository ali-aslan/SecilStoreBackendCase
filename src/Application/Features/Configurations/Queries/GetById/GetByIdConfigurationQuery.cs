using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Queries.GetById;

public class GetByIdConfigurationQuery:IRequest<GetByIdConfigurationResponse>
{
    public required string Id { get; set; }
}

public class GetByIdConfigurationQueryHandler :IRequestHandler<GetByIdConfigurationQuery, GetByIdConfigurationResponse>
{
    private readonly IMapper _mapper;
    private readonly IConfigurationRepository _configurationRepository;

    public GetByIdConfigurationQueryHandler(IMapper mapper, IConfigurationRepository configurationRepository)
    {
        _mapper = mapper;
        _configurationRepository = configurationRepository;
    }

    public async Task<GetByIdConfigurationResponse> Handle(GetByIdConfigurationQuery request, CancellationToken cancellationToken)
    {
        ConfigurationItem item = await _configurationRepository.GetConfigurationByIdAsync(request.Id);
        GetByIdConfigurationResponse configuration = _mapper.Map<GetByIdConfigurationResponse>(item);
        return configuration;
    }
}
