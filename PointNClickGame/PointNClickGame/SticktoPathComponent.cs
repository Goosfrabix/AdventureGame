using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gengine.Engine;
using Gengine.Engine.Objects;
using Gengine.Engine.Components;

namespace PointNClickGame
{
    class SticktoPathComponent : Component
    {
        private Person _parent;
        public SticktoPathComponent(Person parent)
        {
           
            _parent = parent;
        }
        void Component.Update(GameTime gametime)
        {
            PathPoint A = Game1.Path.path[1];
            PathPoint B = Game1.Path.path[0];
            Point P = new Point((int)_parent.Transform.Position.X + (_parent._width/2), (int)_parent.Transform.Position.Y + (_parent._height/2));
            foreach (var point in Game1.Path.path)
            {
                if (Vector2.Distance(new Vector2(A.Position.X, A.Position.Y), new Vector2(P.X, P.Y)) > Vector2.Distance(new Vector2(point.Position.X, point.Position.Y), new Vector2(P.X,P.Y)))
                {
                    B = A;
                    A = point;
                }

            }

            Point newpos = Gengine.ExtraMath.Project(A.Position, B.Position, P);

            float X = _parent.Transform.Position.X;
            float Y = _parent.Transform.Position.Y;
            if (A.status != "stairs" && B.status != "stairs")
            {
                Y = newpos.Y - _parent._height;
            }
            if (A.status == "stairs" && B.status == "stairs")
            {
                X = newpos.X;
            }
            _parent.Transform.Position = new Vector3(X, Y, 0);
        }
    }
}
