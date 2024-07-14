using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Commands.Update;

public record UpdatedConfigurationResponse
{
    public bool Success { get; set; }
}
