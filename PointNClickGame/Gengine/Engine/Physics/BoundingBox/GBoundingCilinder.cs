using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Physics
{
    public struct GBoundingCilinder : GBoundingObject
    {
        public String type;
        public Objects.Object _parent;
        public float _radius;
        public float _height;
        public bool _solid;
        public GBoundingCilinder(Objects.Object parent, float radius, float height, bool solid = true)
        {
            type = "Cilinder";
            _parent = parent;
            _radius = radius;
            _height = height;
            _solid = solid;
        }
    }
}
