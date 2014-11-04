using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PointNClickGame
{
    class Player: Person
    {
        public Player(Vector3 position, Texture2D content):base(position, content,50,100){
            //ChangeController( new Gengine.Controllers.FollowMouseController(this));
        }
        public void update()
        {

        }
    }
}
