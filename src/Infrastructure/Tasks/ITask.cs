using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tasks;

internal interface ITask
{
    Task Execute(CancellationToken cancellationToken);
    int TimeInterval { get; }
}
