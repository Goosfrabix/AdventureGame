using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Engine.Physics
{
   public class CollisionChecker
    {
        private List<GBoundingObject> boundedObjects;
        private List<Objects.Object> observers;
        public CollisionChecker()
        {
            boundedObjects = new List<GBoundingObject>();
            observers = new List<Objects.Object>();
        }
        public void Add(GBoundingObject boundingObject)
        {
            boundedObjects.Add(boundingObject);
        }
        public void Delete(GBoundingObject boundingObject)
        {
            boundedObjects.Remove(boundingObject);
        }
        public void Update(GameTime gametime)
        {
            for (int i = 0; i < boundedObjects.Count; i++)
            {
                for (int j = i+1; j < boundedObjects.Count; j++)
                {
                    //check collision -> notify()
                    if(Intersect(boundedObjects[i],boundedObjects[j])){
                       System.Console.WriteLine("CollsionChecker: Intersect[" + boundedObjects[i].GetType() + "][" + boundedObjects[j].GetType() + "]");
                    }
                }
            }
        }
        public void AddObserver(Objects.Object obserever)
        {
            observers.Add(obserever);
        }
        public void DeleteObserver(Objects.Object obserever)
        {
            observers.Remove(obserever);
        }
        private void Notify()
        {
            
            // if observer==b01{ observer.notify();
            // collisionHandler.notify();
            
        }
        private bool Intersect(GBoundingObject bo1,GBoundingObject bo2)
        {


            //Box box
            if(bo1.GetType() == typeof(GBoundingBox)&&bo2.GetType() == typeof(GBoundingBox)){
                //System.Console.WriteLine("CollsionChecker: Intersect Box Box");
                return IntersectBoxBox((GBoundingBox)bo1, (GBoundingBox)bo2);
            }//Box sphere
            else if (bo1.GetType() == typeof(GBoundingBox) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Box Sphere");
                return IntersectBoxSphere((GBoundingBox)bo1, (GBoundingSphere)bo2);
            }//Box Cilinder
            else if (bo1.GetType() == typeof(GBoundingBox) && bo2.GetType() == typeof(GBoundingCilinder))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Box Cilinder");
                return IntersectBoxCilinder((GBoundingBox)bo1, (GBoundingCilinder)bo2);
            }//Sphere sphere
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Sphere");
                return IntersectSphereSphere((GBoundingSphere)bo1, (GBoundingSphere)bo2);
            }//Sphere box
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingBox))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Box");
                return IntersectBoxSphere((GBoundingBox)bo2, (GBoundingSphere)bo1);
            }//Sphere Cilinder
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingCilinder))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Cilinder");
                return IntersectSphereCilinder((GBoundingSphere)bo1, (GBoundingCilinder)bo2);
            }//Cilinder cilinder
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingCilinder))
            {
                System.Console.WriteLine("CollsionChecker: Intersect Cilinder Cilinder");
                return IntersectCilinderCilinder((GBoundingCilinder)bo1, (GBoundingCilinder)bo2);
            }//Cilinder box
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingBox))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Cilinder Box");
                return IntersectBoxCilinder((GBoundingBox)bo2, (GBoundingCilinder)bo1);
            }//Cilinder sphere
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Cilinder Sphere");
                return IntersectSphereCilinder((GBoundingSphere)bo2, (GBoundingCilinder)bo1);
            }
            else
            {
                return false;
            }

        }
        private bool IntersectBoxBox(GBoundingBox box1, GBoundingBox box2)
        {
            Vector3 boxx1min = box1._parent.Transform.Position + box1._min;
            Vector3 boxx1max = box1._parent.Transform.Position + box1._max;
            Vector3 boxx2min = box2._parent.Transform.Position + box2._min;
            Vector3 boxx2max = box2._parent.Transform.Position + box2._max;
            if (boxx1min.Z != -2 && boxx1min.Z!= 48)
            {
                //System.Console.WriteLine("CC z:" + boxx1min.Z);
            }

            if (boxx1max.X < boxx2min.X)
                return false;
            if (boxx1min.X > boxx2max.X)
                return false;
            if (boxx1max.Y < boxx2min.Y)
                return false;
            if (boxx1min.Y > boxx2max.Y) 
                return false;
            if (boxx1max.Z < boxx2min.Z)
                return false;
            if (boxx1min.Z > boxx2max.Z)
                return false;

            return true;
        }
        private bool IntersectBoxSphere(GBoundingBox box,GBoundingSphere sphere)
        {
            return false;
        }
        private bool IntersectBoxCilinder(GBoundingBox box, GBoundingCilinder cilinder)
        {
            return false;
        }
        private bool IntersectSphereSphere(GBoundingSphere sphere1, GBoundingSphere sphere2)
        {
            if ((sphere2._parent.Transform.Position - sphere1._parent.Transform.Position).Length() <= (sphere1._radius + sphere2._radius))
            {
                //System.Console.WriteLine("CC Object SPHERE:");
                return true;
            }else{
                return false;
            }
        }
        private bool IntersectSphereCilinder(GBoundingSphere sphere,GBoundingCilinder cilinder)
        {
            return false;
        }
        private bool IntersectCilinderCilinder(GBoundingCilinder cilinder1, GBoundingCilinder cilinder2)
        {
            return false;
        }
    }
        
}
