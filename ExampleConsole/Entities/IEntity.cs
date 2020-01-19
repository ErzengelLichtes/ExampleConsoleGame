using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole.Entities
{
    interface IEntity
    {
        IRender Renderer { get; }
    }
}
