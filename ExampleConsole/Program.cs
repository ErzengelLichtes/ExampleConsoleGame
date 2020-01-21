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
            
            Console.WindowHeight = 32;
            Console.WindowWidth  = 64;
            Console.BufferHeight = 32;
            Console.BufferWidth  = 64;

            RegisterRenderables();
            Console.WriteLine("Please enter your character's name:");
            string playerName = Console.ReadLine();
            if (playerName.Length > 64) playerName = playerName.Substring(0, 59) + "...";



            _scene = new Scene();
            AddWall(0, 0, 64, Direction.Horizontal);
            AddWall(0,1,20,Direction.Vertical);
            AddWall(0,21,16,Direction.Horizontal);
            AddWall(16,22,8,Direction.Vertical);
            AddWall(16,30,48,Direction.Horizontal);
            AddWall(63,1,29,Direction.Vertical);

            AddPlayer(6, 6);

            while (_active)
            {
                Console.CursorVisible = false;
                _scene.Render();
                Console.CursorLeft = 32 - (playerName.Length / 2);
                Console.CursorTop = Console.BufferHeight - 1;
                Console.Write(playerName);
                ProcessInput();
            }
        }
        static Dictionary<Direction, string> _wallDisplayFromDirection = new Dictionary<Direction, string>()
                                                                {
                                                                    [Direction.Vertical] = "vwall",
                                                                    [Direction.Horizontal] = "wall",
                                                                };

        private static void AddWall(int       x
                                  , int       y
                                  , int       length
                                  , Direction direction
        )
        {
            _scene.AddEntity(new Wall(x, y, length, direction, _wallDisplayFromDirection[direction]));
        }

        private static void AddPlayer(int x, int y)
        {
            var player = new Player(x, y);
            _scene.AddEntity(player);
            _scene.AddUpdater(player);
        }

        static Dictionary<ConsoleKey, string> keyMap = new Dictionary<ConsoleKey, string>()
                                                       {
                                                           [ConsoleKey.UpArrow   ] = "up",
                                                           [ConsoleKey.DownArrow ] = "down",
                                                           [ConsoleKey.LeftArrow ] = "left",
                                                           [ConsoleKey.RightArrow] = "right",
                                                       };

        static void ProcessInput()
        {
            var keyInfo = Console.ReadKey(intercept: true);
            ConsoleKey key = keyInfo.Key;
            if (key == ConsoleKey.Escape)
                _active = false;
            if (keyMap.TryGetValue(key, out var message))
            {
                _scene.ExecuteMessage(message);
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

        private static bool _active = true;
        private static Scene _scene;
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
