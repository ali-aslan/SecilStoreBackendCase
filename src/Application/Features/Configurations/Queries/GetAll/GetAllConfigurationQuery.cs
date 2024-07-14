using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Queries.GetAll;

public class GetAllConfigurationQuery : IRequest<IEnumerable<GetAllConfigurationResponse>>
{
}

public class GetAllConfigurationQueryHandler : IRequestHandler<GetAllConfigurationQuery, IEnumerable<GetAllConfigurationResponse>>
{

    private readonly IMapper _mapper;
    private readonly IConfigurationRepository _configurationRepository;

    public GetAllConfigurationQueryHandler(IMapper mapper, IConfigurationRepository configurationRepository)
    {
        _mapper = mapper;
        _configurationRepository = configurationRepository;
    }

    public async Task<IEnumerable<GetAllConfigurationResponse>> Handle(GetAllConfigurationQuery request, CancellationToken cancellationToken)
    {
        var items = await _configurationRepository.GetAllConfigurationsAsync();
        IEnumerable<GetAllConfigurationResponse> ConfigurationResponses = _mapper.Map<IEnumerable<GetAllConfigurationResponse>>(items);
        return ConfigurationResponses;
    }
}
