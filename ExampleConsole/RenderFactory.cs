using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole
{
    static class RenderFactory
    {
        private static readonly Dictionary<Type, Delegate> Factories = new Dictionary<Type, Delegate>();

        public static void RegisterFactory<TRender>(Func<TRender, IRender> factory)
        {
            Factories[typeof(TRender)] = factory;

        }
        public static IRender CreateRenderableFor<TRender>(TRender o)
        {
            if (Factories.TryGetValue(typeof(TRender), out var factory))
            {
                return (IRender)factory.DynamicInvoke(o);
            }
            throw new Exception($"Could not find type '{typeof(TRender).FullName}' in factory");
        }
    }

    interface IRender
    {
        void Render();
    }
}
