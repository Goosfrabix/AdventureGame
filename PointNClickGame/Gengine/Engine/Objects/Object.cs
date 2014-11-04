using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Gengine.Engine.Components;
using Gengine.Engine.Graphics;

namespace Gengine.Engine.Objects
{
    public class Object
    {
        private Transform _transform;
        protected Component controller;
        private List<Component> _myComponents;
        protected DrawComponent _drawComponent;
        public Object(Vector3 position)
        {
            _myComponents = new List<Component>();
            _transform = new Transform(position);

            controller = new Controllers.NullController();
            AddRangeComponent(_transform, controller);

        }
        public void ChangeController(Component newController)
        {
            DeleteComponent(controller); //delete controller out of list
            controller = newController;
            AddComponent(controller); //add controller into  list
        }
        //getter & setter
       /* public Transform GetTransform()
        {
            return _transform;
        }*/
        public Transform Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        public void CollisionNotify()
        {
        }
        protected void AddBoundingBox(Engine.Physics.GBoundingObject bo,bool observer)
        {
            Engine.GEngine.collisionChecker.Add(bo);
            if (observer)
            {
                Engine.GEngine.collisionChecker.AddObserver(this);
            }
        }
        public void AddComponent(Component component)
        {
            _myComponents.Add(component);
            Engine.GEngine.componentManager.Add(component);
        }
        public void DeleteComponent(Component component)
        {
            Engine.GEngine.componentManager.Delete(component);
            _myComponents.Remove(component);
        }
        public void AddRangeComponent(params Component[] components)
        {
            _myComponents.AddRange(components);
            Engine.GEngine.componentManager.AddRange(components);
        }
        public void Kill()
        {
            for (int i = 0; i < _myComponents.Count;i++ )
            {
                GEngine.componentManager.Delete(_myComponents[i]);
            }
            _myComponents.Clear();
            _transform = null;
            controller = null;
            GEngine.drawManager.Delete(_drawComponent);
        }
    }
}
