using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole.Entities
{
    class Player : IEntity, IPositioned
    {
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Renderer = RenderFactory.CreateRenderableFor<IPositioned>(this);
        }

        /// <inheritdoc />
        public IRender Renderer { get; }

        /// <inheritdoc />
        public int X { get; }

        /// <inheritdoc />
        public int Y { get; }

        /// <inheritdoc />
        public string Display => "player";
    }
}
