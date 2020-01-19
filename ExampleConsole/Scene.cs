using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleConsole.Entities;

namespace ExampleConsole
{
    class Scene
    {
        private List<IEntity> _entities = new List<IEntity>();

        public void Render()
        {
            Console.Clear();
            foreach (IRender render in _entities.Select(ent=>ent.Renderer))
            {
                render.Render();
            }
        }

        public void AddEntity(IEntity entity) => _entities.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
    }
}
