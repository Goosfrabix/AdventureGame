using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PointNClickGame
{
using Microsoft.Xna.Framework;
    public class GroundSegment
    {
        private int _x;
        private int _y;
        public int biome;
        public Color color;
        public bool has_blockage;
        public bool has_stairs;
        public bool on_path;
        public GroundSegment(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public Vector2 position
        {
            get { return new Vector2(X,Y); }
            set { Y = (int)value.Y; X = (int)value.X; }
        }
    }
}
