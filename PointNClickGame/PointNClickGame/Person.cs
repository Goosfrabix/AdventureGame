using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PointNClickGame
{
    class Person: GameObject
    {
        public Person(Vector3 position, Texture2D content, int width, int height)
            : base(position, content, width, height)
        {
           // AddComponent(new SticktoPathComponent(this));
        }
    }
}
