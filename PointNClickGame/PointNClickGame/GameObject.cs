using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gengine.Engine.Objects;
namespace PointNClickGame
{
    class GameObject : _2DObject
    {
        public int _width = 10;
        public int _height = 10;
        public GameObject(Vector3 position, Texture2D content,int width, int height)
            : base(position, content, width, height, 20f, true, false, true)
        {
            _width = width;
            _height = height;
            AddBoundingBox(new Gengine.Engine.Physics.GBoundingBox(this,new Vector3(0,0,0),new Vector3(width,height,0)),false);
        }
    }
}
