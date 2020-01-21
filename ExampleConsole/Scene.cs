using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleConsole.Entities;
using JetBrains.Annotations;

namespace ExampleConsole
{
    class Scene
    {
        private readonly List<IEntity> _entities = new List<IEntity>();
        private readonly List<IUpdater> _updaters = new List<IUpdater>();
        private readonly List<string> _validActions = new List<string>();

        public void Render()
        {
            Console.Clear();
            foreach (IRender render in _entities.Select(ent=>ent.Renderer))
            {
                render.Render();
            }
        }

        public void ExecuteMessage(string message)
        {
            foreach (IUpdater updater in _updaters)
            {
                updater.ReceiveMessage(message);
            }
        }

        public void AddEntity([NotNull] IEntity entity) => _entities.Add(entity ?? throw new ArgumentNullException(nameof(entity)));

        public void AddUpdater([NotNull] IUpdater updater) => _updaters.Add(updater ?? throw new ArgumentNullException(nameof(updater)));

        public void RemoveEntity([NotNull] IEntity entity) => _entities.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));

        public IEnumerable<string> GetAllActions() { return _validActions; }
    }
}
