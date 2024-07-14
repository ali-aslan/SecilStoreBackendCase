using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Commands.Delete;

public record DeletedConfigurationResponse
{
    public bool Success { get; set; }
}
