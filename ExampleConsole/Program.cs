using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleConsole.Entities;

namespace ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterRenderables();
            Console.WriteLine("Please enter your character's name:");
            string playerName = Console.ReadLine();

            Scene scene = new Scene();
            scene.AddEntity(new Wall(0, 0, 64, Direction.Horizontal, "wall"));
            scene.AddEntity(new Wall(0, 1, 20, Direction.Vertical, "vwall"));
            scene.AddEntity(new Wall(0, 21, 16, Direction.Horizontal, "wall"));
            scene.AddEntity(new Wall(16, 22, 8, Direction.Vertical, "vwall"));
            scene.AddEntity(new Wall(16, 30, 34, Direction.Horizontal, "wall"));

            bool active = true;
            while (active)
            {
                scene.Render();
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                    active = false;
            }
        }

        private static void RegisterRenderables()
        {
            RenderFactory.RegisterFactory<IPositioned>(entity=>new EntityRender(entity));
            RenderFactory.RegisterFactory<IBlock>(entity=>new BlockRender(entity));
        }

        static readonly Dictionary<string, char> EntityDisplayLookup = new Dictionary<string, char>()
                                                                       {
                                                                           ["player"] = 'p',
                                                                           ["wall"] = '=',
                                                                           ["vwall"] = '|',
                                                                       };
        public static char LookupEntityDisplay(string key) => EntityDisplayLookup.TryGetValue(key, out var retval) ? retval : 'x';
    }

    internal class BlockRender : IRender
    {
        private readonly IBlock _entity;
        public BlockRender(IBlock entity) { _entity = entity; }

        /// <inheritdoc />
        public void Render()
        {
            var c = Program.LookupEntityDisplay(_entity.Display);
            foreach (var y in Enumerable.Range(_entity.Y, _entity.Height))
            {
                Console.CursorTop = y;
                foreach (var x in Enumerable.Range(_entity.X, _entity.Width))
                {
                    Console.CursorLeft = x;
                    Console.Write(c);
                }
            }
        }
    }

    internal class EntityRender : IRender
    {
        private readonly IPositioned _positioned;

        public EntityRender(IPositioned positioned) { _positioned = positioned; }

        /// <inheritdoc />
        public void Render()
        {
            Console.CursorTop  = _positioned.Y;
            Console.CursorLeft = _positioned.X;
            var c = Program.LookupEntityDisplay(_positioned.Display);
            Console.Write(c);
        }
    }
}
