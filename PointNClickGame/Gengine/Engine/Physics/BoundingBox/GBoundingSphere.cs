using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Physics
{
    public struct GBoundingSphere : GBoundingObject
    {
        public String type;
        public Objects.Object _parent;
        public float _radius;
        public bool _solid;
        public GBoundingSphere(Objects.Object parent, float radius, bool solid = true)
        {
            type = "Sphere";
            _parent = parent;
            _radius = radius;
            _solid = solid;
        }
    }
}
