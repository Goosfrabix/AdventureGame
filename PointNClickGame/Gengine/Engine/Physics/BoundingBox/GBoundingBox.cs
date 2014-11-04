using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Engine.Physics
{
    public struct GBoundingBox : GBoundingObject
    {
        public String type;
        public Objects.Object _parent;
        public Vector3 _min;
        public Vector3 _max;
        public bool _solid;
        public GBoundingBox(Objects.Object parent, Vector3 min, Vector3 max, bool solid = true)
        {
            type = "Box";
            _parent = parent;
            _min = min;
            _max = max;
            _solid = solid;


            System.Console.WriteLine("GBB:============================");
            System.Console.WriteLine("GBB:PP(" + parent.Transform.Position.X + ", " + parent.Transform.Position.Y + ")");
            System.Console.WriteLine("GBB:+-(" + max.X + ", " + min.X + ")");

            Texture2D blank = new Texture2D(GEngine.graphicsDM.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            Engine.Graphics.DrawSprite _drawComponent = new Engine.Graphics.DrawSprite(_parent, blank, 1, 1, 20, false, false, true);
            Engine.GEngine.drawManager.Add(_drawComponent);
            _drawComponent.Size = new Vector2((int)( max).X, (int)( max).Y);
            //(int)(parent.Transform.Position + max).X
        }
    }
}
