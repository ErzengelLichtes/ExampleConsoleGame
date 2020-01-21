using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole.Entities
{
    class Player : IEntity, IPositioned, IUpdater
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
        public int X { get; private set; }

        /// <inheritdoc />
        public int Y { get; private set;}

        /// <inheritdoc />
        public string Display => "player";

        private static readonly Dictionary<string, Action<Player>> messageActions = new Dictionary<string, Action<Player>>
                                                                                    {
                                                                                        ["up"   ] = player => player.Y -= 1,
                                                                                        ["down" ] = player => player.Y += 1,
                                                                                        ["left" ] = player => player.X -= 1,
                                                                                        ["right"] = player => player.X += 1,
                                                                                    };

        /// <inheritdoc />
        public IEnumerable<string> ValidMessages => messageActions.Keys;

        /// <inheritdoc />
        public void ReceiveMessage(string message) { messageActions[message](this); }
    }
}
