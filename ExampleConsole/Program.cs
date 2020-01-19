using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterRenderables();
            Console.WriteLine("Please enter your character's name:");
            string playerName = Console.ReadLine();

            Console.Clear();

        }

        private static void RegisterRenderables()
        {
            RenderFactory.RegisterFactory<IEntity>(entity=>new EntityRender(entity));
            RenderFactory.RegisterFactory<IBlock>(entity=>new BlockRender(entity));
        }

        static Dictionary<string, char> entityDisplayLookup = new Dictionary<string, char>()
                                                              {
                                                                  ["player"] = 'p',
                                                                  ["wall"] = '=',
                                                                  ["vwall"] = '|',
                                                              };
        public static char EntityDisplayLookup(string key) { return entityDisplayLookup.TryGetValue(key, out var retval) ? retval : 'x'; }
    }

    internal class BlockRender : IRender
    {
        private readonly IBlock _entity;
        public BlockRender(IBlock entity) { _entity = entity; }

        /// <inheritdoc />
        public void Render()
        {
            var c = Program.EntityDisplayLookup(_entity.Display);
            foreach (var y in Enumerable.Range(_entity.Y, _entity.Y + _entity.Height))
            {
                Console.CursorTop = y;
                foreach (var x in Enumerable.Range(_entity.X, _entity.X + _entity.Width))
                {
                    Console.CursorLeft = x;
                    Console.Write(c);
                }
            }
        }
    }

    internal class EntityRender : IRender
    {
        private readonly IEntity _entity;

        public EntityRender(IEntity entity) { _entity = entity; }

        /// <inheritdoc />
        public void Render()
        {
            Console.CursorTop  = _entity.Y;
            Console.CursorLeft = _entity.X;
            var c = Program.EntityDisplayLookup(_entity.Display);
            Console.Write(c);
        }
    }
}
