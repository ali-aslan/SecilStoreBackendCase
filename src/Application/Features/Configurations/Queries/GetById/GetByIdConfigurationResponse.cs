using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Queries.GetById;

public record GetByIdConfigurationResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ConfigurationValueType Type { get; set; }
    public string Value { get; set; }
    public bool IsActive { get; set; }
    public string ApplicationName { get; set; }
}
