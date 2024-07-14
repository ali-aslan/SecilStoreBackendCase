using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Commands.Delete;

public class DeleteConfigurationCommand:IRequest<DeletedConfigurationResponse>
{
    public required string Id { get; set; }
}

public class DeleteConfigurationCommandHandler:IRequestHandler<DeleteConfigurationCommand, DeletedConfigurationResponse>
{

    private readonly IConfigurationRepository _configurationRepository;

    public DeleteConfigurationCommandHandler( IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public async Task<DeletedConfigurationResponse> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
    {
        bool response = await _configurationRepository.DeleteAsync(request.Id);
        DeletedConfigurationResponse deletedConfigurationResponse = new DeletedConfigurationResponse() {Success=response};
        return deletedConfigurationResponse;
    }
}
