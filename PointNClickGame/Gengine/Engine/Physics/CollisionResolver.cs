using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gengine.Engine.Physics
{
    class CollisionResolver
    {
        public CollisionResolver()
        {

        }
        public void Resolve(GBoundingObject bo1,GBoundingObject bo2)
        {
            //Box box
            if (bo1.GetType() == typeof(GBoundingBox) && bo2.GetType() == typeof(GBoundingBox))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Box Box");
            }//Box sphere
            else if (bo1.GetType() == typeof(GBoundingBox) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Box Sphere");
            }//Box Cilinder
            else if (bo1.GetType() == typeof(GBoundingBox) && bo2.GetType() == typeof(GBoundingCilinder))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Box Cilinder");
            }//Sphere sphere
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Sphere");
            }//Sphere box
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingBox))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Box");
            }//Sphere Cilinder
            else if (bo1.GetType() == typeof(GBoundingSphere) && bo2.GetType() == typeof(GBoundingCilinder))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Sphere Cilinder");
            }//Cilinder cilinder
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingCilinder))
            {
            }//Cilinder box
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingBox))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Cilinder Box");
            }//Cilinder sphere
            else if (bo1.GetType() == typeof(GBoundingCilinder) && bo2.GetType() == typeof(GBoundingSphere))
            {
                //System.Console.WriteLine("CollsionChecker: Intersect Cilinder Sphere");
            }
        }
    }
}
