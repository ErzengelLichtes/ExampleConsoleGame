﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole
{
    interface IBlock : IPositioned
    {
        int Width { get; }
        int Height { get; }
    }
}
