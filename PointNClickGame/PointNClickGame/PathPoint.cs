using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PointNClickGame
{
    public class PathPoint
    {
        private PathPoint _parent;
        public List<PathPoint> _children;
        public GroundSegment myground;
        private Point _position;
        public int mygroundIndexX;
        public int mygroundIndexY;
        public string status = "path";
        public List<Gengine.Engine.Graphics.DrawSprite> spritelist;
        public PathPoint(Point position)
        {
            spritelist = new List<Gengine.Engine.Graphics.DrawSprite>();
            _children = new List<PathPoint>();
            _position = position;
        }
        public PathPoint Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public void Kill()
        {
            _children.Clear();
            mygroundIndexX =0;
            mygroundIndexY=0;
            status = "path";
            foreach (var sprite in spritelist)
            {
               Gengine.Engine.GEngine.drawManager.Delete(sprite);
                
            }
            spritelist.Clear();
        }
        
    }
}
