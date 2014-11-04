using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Components
{
    public class ComponentManager : Component
    {
        private List<Component> componentsList;
        public ComponentManager()
        {
            componentsList = new List<Component>();
        }
        public void Add(Component component)
        {
            componentsList.Add(component);
        }
        public void AddRange(params Component[] components)
        {
            componentsList.AddRange(components);
        }
        public void Delete(Component component)
        {
            componentsList.Remove(component);
        }
        public void Clear()
        {
            componentsList.Clear();

        }
        public void Update(GameTime gametime)
        {
            foreach (var childComponent in componentsList)
            {
                childComponent.Update(gametime);
                
            }
        }
    }
}
