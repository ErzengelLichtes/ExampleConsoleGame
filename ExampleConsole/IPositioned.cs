using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole
{
    interface IPositioned
    {
        int X { get; }
        int Y { get; }
        string Display { get; }
    }

}
